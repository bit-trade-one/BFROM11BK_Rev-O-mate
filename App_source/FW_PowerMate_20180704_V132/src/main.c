// Rev-O-mate
//
//  ver 1.3.2   2018.07.04  量産 8版    SW設定の０?９数字入力、９?０数字入力の連続で入力されるバグ修正
//                                      SW設定のマウスの移動とスクロールで連続入力されるバグ修正
//  ver 1.3.1   2018.05.31  量産 7版    LED制御のバグ修正
//  ver 1.3.0   2018.05.26  量産 6版    LED周期アップ
//                                      SW設定に、キー、マウス、マルチメディア、ジョイパッド設定機能を追加
//  ver 1.2.0   2018.04.19  量産 5版    SW特殊機能のダイアル機能変更を追加
//  ver 1.1.1   2018.03.20  量産 4版    ダイアル設定=未設定、SW設定=マクロ割り当ての場合に、ダイアルを先に操作するとSWマクロが動作しない不具合修正
//  ver 1.1.0   2018.03.14  量産 3版    スクリプト最大数を200に変更
//                                      スクリプトモードにホールドモード追加（押している間１回だけ実行し続ける）
//  ver 1.0.1   2018.03.14  量産 2版    マルチメディアのスクリプト用のUSB送信バッファクリア漏れ修正
//  ver 1.0.0   2018.01.22  量産 初版
//  ver 0.0.2   2017.10.16  試作 2版 不具合修正および要望追加
//                          感度のデフォルト値を100->50に変更
//                          USBのPCへの出力を変化時のみ行うように修正
//                          スリープ時にLED消灯するように変更
//                          PCからキーボード情報を受信するように変更
//                          エンコーダーの数字入力機能の変更(1?0の範囲で入力)
//  ver 0.0.1   2016.08.26  試作 初版
//

/********************************************************************
 FileName:      main.c
 Dependencies:  See INCLUDES section
 Processor:     PIC18, PIC24, dsPIC, and PIC32 USB Microcontrollers
 Hardware:      This demo is natively intended to be used on Microchip USB demo
                boards supported by the MCHPFSUSB stack.  See release notes for
                support matrix.  This demo can be modified for use on other 
                hardware platforms.
 Complier:      Microchip C18 (for PIC18), XC16 (for PIC24/dsPIC), XC32 (for PIC32)
 Company:       Microchip Technology, Inc.

 Software License Agreement:

 The software supplied herewith by Microchip Technology Incorporated
 (the "Company") for its PICﾂｮ Microcontroller is intended and
 supplied to you, the Company's customer, for use solely and
 exclusively on Microchip PIC Microcontroller products. The
 software is owned by the Company and/or its supplier, and is
 protected under applicable copyright laws. All rights are reserved.
 Any use in violation of the foregoing restrictions may subject the
 user to criminal sanctions under applicable laws, as well as to
 civil liability for the breach of the terms and conditions of this
 license.

 THIS SOFTWARE IS PROVIDED IN AN "AS IS" CONDITION. NO WARRANTIES,
 WHETHER EXPRESS, IMPLIED OR STATUTORY, INCLUDING, BUT NOT LIMITED
 TO, IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A
 PARTICULAR PURPOSE APPLY TO THIS SOFTWARE. THE COMPANY SHALL NOT,
 IN ANY CIRCUMSTANCES, BE LIABLE FOR SPECIAL, INCIDENTAL OR
 CONSEQUENTIAL DAMAGES, FOR ANY REASON WHATSOEVER.

********************************************************************
 File Description:

 Change History:
  Rev   Description
  ----  -----------------------------------------
  1.0   Initial release
  2.1   Updated for simplicity and to use common
                     coding style
  2.7b  Improvements to USBCBSendResume(), to make it easier to use.
  2.9f  Adding new part support
********************************************************************/

#ifndef MAIN_C
#define MAIN_C

/** INCLUDES *******************************************************/
#include <xc.h>
#include <string.h>
#include "./USB/usb.h"
#include "HardwareProfile.h"
#include "./USB/usb_function_hid.h"
#include "app.h"
#include "main_sub.h"
#include "main_comm.h"
#include "l_spi.h"
#include "l_flash.h"
#include "l_timer.h"
#include "l_script.h"


/** CONFIGURATION **************************************************/

// *****************************************************************************
/*
 Crystal 20MHz
 system clock 50MHz = 20MHz / 4 * 20 * 2
 USB clock 48MHz = 20MHz / 5 * 24 / 2
 PLL Input Divider (FPLLIDIV)                   = Divide by 4
 PLL Multiplier (FPLLMUL)                       = Multiply by 20
 USB PLL Input Divider (UPLLIDIV)               = Divide by 5
 USB PLL Enable (UPLLEN)                        = Enabled
 System PLL Output Clock Divider (FPLLODIV)     = Divide by 2
 Watchdog Timer Enable (FWDTEN)                 = Disabled
 Clock Switching and Monitor Selection (FCKSM)  = Clock Switch Enable,
                                                  Fail Safe Clock Monitoring Enable
 Peripheral Clock Divisor (FPBDIV)              = Divide by 1
*/

// DEVCFG3
#pragma config FVBUSONIO = OFF, FUSBIDIO = OFF
// DEVCFG2
#pragma config FPLLIDIV = DIV_4, FPLLMUL = MUL_20, UPLLIDIV = DIV_5, UPLLEN = ON, FPLLODIV = DIV_2
// DEVCFG1
#pragma config FNOSC = PRIPLL, FSOSCEN = OFF, IESO = OFF, POSCMOD = HS, OSCIOFNC = OFF, FPBDIV = DIV_1, FCKSM = CSDCMD, WDTPS = PS65536, FWDTEN = OFF
// DEVCFG0
#pragma config DEBUG = OFF, JTAGEN = OFF, ICESEL = ICS_PGx1, PWP = OFF, BWP = OFF, CP = OFF


/** DECLARATIONS ***************************************************/

/** VARIABLES ******************************************************/
int8_t	c_version[]="1.3.2";

BYTE ReceivedDataBuffer[RX_DATA_BUFFER_SIZE] RX_DATA_BUFFER_ADDRESS;
BYTE ToSendDataBuffer[TX_DATA_BUFFER_SIZE] TX_DATA_BUFFER_ADDRESS;
BYTE mouse_input[MOUSE_BUFF_SIZE]={0};
//BYTE volume_input[VOLUME_BUFF_SIZE];
BYTE joystick_input[JOYSTICK_BUFF_SIZE]={0};
BYTE keyboard_input[KEYBOARD_BUFF_SIZE]={0};
BYTE keyboard_output[HID_INT_OUT_EP_SIZE]={0};
BYTE multimedia_input[MULTIMEDIA_BUFF_SIZE]={0};

BYTE mouse_buffer[MOUSE_BUFF_SIZE]={0};
BYTE mouse_buffer_pre[MOUSE_BUFF_SIZE]={0};
//BYTE volume_buffer[VOLUME_BUFF_SIZE];
BYTE joystick_buffer[JOYSTICK_BUFF_SIZE]={0};
BYTE joystick_buffer_pre[JOYSTICK_BUFF_SIZE]={0};
BYTE joystick_default_value[JOYSTICK_BUFF_SIZE] = {0x00, 0x00, HAT_SWITCH_NULL, 0x80, 0x80, 0x80, 0x80};
BYTE keyboard_buffer[KEYBOARD_BUFF_SIZE]={0};
BYTE keyboard_buffer_pre[KEYBOARD_BUFF_SIZE]={0};
BYTE multimedia_buffer[MULTIMEDIA_BUFF_SIZE]={0};
BYTE multimedia_buffer_pre[MULTIMEDIA_BUFF_SIZE]={0};

BYTE mouse_input_out_flag = 0;
//BYTE volume_input_out_flag = 0;
BYTE joystick_input_out_flag = 0;
BYTE keyboard_input_out_flag = 0;
BYTE multimedia_input_out_flag = 0;

// SW入力
BYTE sw_now_fix[SW_ALL_NUM]={0};
BYTE sw_now_fix_pre[SW_ALL_NUM]={0};
BYTE sw_press_on_cnt[SW_ALL_NUM]={0};
BYTE sw_press_off_cnt[SW_ALL_NUM]={0};
BYTE sw_output_flag[SW_ALL_NUM]={0};
BYTE sw_output_multimedia_flag[SW_ALL_NUM]={0};	// multimediaは、変化時に１回のみ出力するための出力状態記憶用
BYTE encoder_key_out_flag[SW_ALL_NUM]={0};	// エンコーダー入力でキーボード出力時に、一定時間出力するためのフラグ 出力時間を設定する
BYTE sw_func_out_flag[SW_ALL_NUM]={0};       // SW入力で機能出力フラグ
BYTE sw_output_mouse_first_flag[SW_ALL_NUM]={0};       // SW入力でマウス出力初回判定用フラグ
BYTE sw_output_joystick_first_flag[SW_ALL_NUM]={0};       // SW入力でジョイスティック出力初回判定用フラグ
BYTE set_type_move_sw_no = 0xFF;
BYTE set_type_move_val[2] = {0};
BYTE set_type_ws_sw_no = 0xFF;
BYTE set_type_ws_val = 0;
BYTE set_type_xy_sw_no = 0xFF;
BYTE set_type_xy_val[2] = {0};
BYTE set_type_zrz_sw_no = 0xFF;
BYTE set_type_zrz_val[2] = {0};
BYTE set_type_hat_sw_no = 0xFF;
BYTE set_type_hat_val = 0;
//BYTE sw_on_time_measure_flag[SW_NUM] = {0};
//BYTE sw_on_time_measure_status[SW_NUM] = {SW_ON_TIME_MEASURE_STATUS_NONE};
//WORD sw_on_time_measure_time[SW_NUM] = {0};
// 基本設定情報
UN_BASE_HEAD my_base_head;                                  // [8byte] 基本設定情報読み込みバッファ
UN_BASE_INFO my_base_infos[MODE_NUM];                       // [32byte]*3=96 基本設定モード情報読み込みバッファ
// 機能設定情報
UN_FUNC_INFO my_func_infos[MODE_NUM][MODE_FUNCTION_NUM];    // [24byte]*4*3=288 機能設定情報読み込みバッファ
UN_ENCODER_SCRIPT_INFO my_encoder_script_infos[ENCODER_SCRIPT_NUM]; // [48byte]*3=144 エンコーダースクリプト設定情報読み込みバッファ
UN_SW_FUNC_INFO my_sw_func_infos[MODE_NUM][SW_NUM];         // [8byte]*3*11=264 SW機能設定情報読み込みバッファ

BYTE keyboard_output_stop_by_macro = 0;

//エンコーダ処理用
BYTE encoder_input_state = 0;       // エンコーダー入力状態
BYTE encoder_input_state_pre = 0;   // エンコーダー入力状態前回値（チャタリング防止）
BYTE encoder_input_cnt = 0;         // エンコーダー入力状態カウント用
BYTE encoder_input_fix_pre = 0;     // エンコーダー入力状態前回確定値
BYTE encoder_input_puls_cw = 0;     // エンコーダー入力CWカウント用　
BYTE encoder_input_puls_ccw = 0;    // エンコーダー入力CCWカウント用　
BYTE rotate_state = 0;
WORD temp_input_sense_left = 0;
WORD temp_input_sense_right = 0;
BYTE encoder_script_exe_now_idx[ENCODER_SCRIPT_NUM] = {0};
WORD dial_function_enocder_disabled_time = 0;

// 数字連続入力用
BYTE number_input_up_down_flag = NUMBER_INPUT_UPDOWN_FLAG_NONE;
BYTE number_input_now_val = 0;
// USBコード 1,2,3,4,5,6,7,8,9,0
BYTE number_input_usb_code[NUMBER_INPUT_USB_CODE_NUM] = {0x1E, 0x1F, 0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27};

// Mode
BYTE mode_no_set = MODE1_NO;                    // 設定モードNo.
BYTE mode_func_no_set = MODE_FUNCTION1_NO;      // 設定モード機能No.
BYTE mode_no_fix = MODE1_NO;                    // 実行中モードNo.
BYTE mode_func_no_fix = MODE_FUNCTION1_NO;      // 実行中モード機能No.
BYTE mode_no_pre_led = MODE1_NO;                // モードNo.変化　LED用モードNo.前回値
BYTE mode_func_no_pre_led = MODE_FUNCTION1_NO;  // 機能No.変化　LED用機能No.前回値

// LED出力
BYTE led_output_brightness_level_fix = 0;   // LED 出力　輝度
BYTE led_output_fix[LED_DATA_NUM]={0};      // LED 出力値
BYTE led_output_mode_fix[LED_DATA_NUM]={0}; // LED Mode出力値
BYTE led_output_brightness_level_preview = 0;   // LED プレビュー出力　輝度
BYTE led_output_preview[LED_DATA_NUM]={0};  // LED プレビュー出力値
WORD led_output_preview_time_counter = 0;   // LED プレビュー時間
BYTE led_output_duty_count = 0;	//led点灯用dutyの内部値
BYTE led1_blink_flag = 0;
WORD led1_blink_counter = 0;
BYTE led_change_flag_mode = 0;                  // LED変更フラグ モード用
BYTE led_change_flag_func = 0;                  // LED変更フラグ 機能用
BYTE led_light_on_type = LED_LIGHT_ON_TYPE_MODE;    // LED点灯 タイプ Mode or Func
BYTE led_light_func_type = LED_LIGHT_TYPE_FUNC_ON;  // LED点灯 機能点灯タイプ
BYTE led_func_lighting_flag = 0;                // LED機能点灯中フラグ
BYTE led_light_on_type_temp_func = LED_LIGHT_ON_TYPE_MODE;    // 機能一時変更前のLED点灯タイプを記憶 Mode or Func
DWORD led_light_time_counter = 0;
BYTE led_light_status = 0;
int timer1_counter = 0;
int timer2_counter = 0;
#if 0
const BYTE led_color_data[LED_COLOR_TYPE_NUM][3] = {
    {0x00,0x00,0x00},   // 消灯
    {0x3C,0x3C,0x32},   // WHITE
    {0x32,0x00,0x00},   // RED
    {0x32,0x0A,0x00},   // ORANGE
    {0x32,0x23,0x00},   // YELLOW
    {0x00,0x32,0x19},   // TURQUOISE BLUE
    {0x00,0x32,0x00},   // GREEN
    {0x00,0x00,0x3C},   // BLUE
    {0x1E,0x05,0x3C},   // PURPLE
};
#endif
#if 1
// 各モードのLEDデフォルト値
BYTE led_color_mode_default_data[MODE_NUM][3] = {0};
// 各モードの各機能のLEDデフォルト値
BYTE led_color_func_default_data[MODE_NUM * MODE_FUNCTION_NUM][3] = {0};
#else
// 各モードのLEDデフォルト値
const BYTE led_color_mode_default_data[MODE_NUM][3] = {
    {0x00,0x00,0x00},   // Mode 1
    {0x00,0x00,0x00},   // Mode 2
    {0x00,0x00,0x00},   // Mode 3
};
// 各モードの各機能のLEDデフォルト値
const BYTE led_color_func_default_data[MODE_NUM * MODE_FUNCTION_NUM][3] = {
    {0x00,0x00,0x00},   // Mode 1 Func 1
    {0x00,0x00,0x00},   // Mode 1 Func 2
    {0x00,0x00,0x00},   // Mode 1 Func 3
    {0x00,0x00,0x00},   // Mode 1 Func 4
    {0x00,0x00,0x00},   // Mode 2 Func 1
    {0x00,0x00,0x00},   // Mode 2 Func 2
    {0x00,0x00,0x00},   // Mode 2 Func 3
    {0x00,0x00,0x00},   // Mode 2 Func 4
    {0x00,0x00,0x00},   // Mode 3 Func 1
    {0x00,0x00,0x00},   // Mode 3 Func 2
    {0x00,0x00,0x00},   // Mode 3 Func 3
    {0x00,0x00,0x00},   // Mode 3 Func 4
};
#endif

BYTE mouse_w_click_status = MOUSE_DOUBLE_CLICK_STATUS_NONE;
WORD mouse_w_click_interval_counter = 0;

// FLASH
BYTE flash_read_req_flag = 0;
BYTE flash_write_req_flag = 0;



USB_HANDLE USBOutHandle = 0;    //USB handle.  Must be initialized to 0 at startup.
USB_HANDLE USBInHandle = 0;     //USB handle.  Must be initialized to 0 at startup.
USB_HANDLE lastTransmission_Mouse = 0;
//USB_HANDLE lastTransmission_Volume = 0;
USB_HANDLE lastTransmission_Joystick = 0;
USB_HANDLE lastINTransmission_Keyboard = 0;
USB_HANDLE lastOUTTransmission_Keyboard = 0;

BYTE USB_Sleep_Flag = 0;

// DEBUG
BYTE debug_arr1[16] ={0};
BYTE debug_arr2[16] ={0};
BYTE debug_arr3[16] ={0};
WORD debug_array_w1[16] = {0};

/** PRIVATE PROTOTYPES *********************************************/
static void InitializeSystem(void);
void ProcessIO(void);
void UserInit(void);
void USBCBSendResume(void);

/** VECTOR REMAPPING ***********************************************/



/********************************************************************
 * Function:        void main(void)
 *
 * PreCondition:    None
 *
 * Input:           None
 *
 * Output:          None
 *
 * Side Effects:    None
 *
 * Overview:        Main program entry point.
 *
 * Note:            None
 *******************************************************************/
#if defined(__18CXX)
void main(void)
#else
int main(void)
#endif
{   
    InitializeSystem();

    #if defined(USB_INTERRUPT)
        USBDeviceAttach();
    #endif

    while(1)
    {
        #if defined(USB_POLLING)
		// Check bus status and service USB interrupts.
        USBDeviceTasks(); // Interrupt or polling method.  If using polling, must call
        				  // this function periodically.  This function will take care
        				  // of processing and responding to SETUP transactions 
        				  // (such as during the enumeration process when you first
        				  // plug in).  USB hosts require that USB devices should accept
        				  // and process SETUP packets in a timely fashion.  Therefore,
        				  // when using polling, this function should be called 
        				  // regularly (such as once every 1.8ms or faster** [see 
        				  // inline code comments in usb_device.c for explanation when
        				  // "or faster" applies])  In most cases, the USBDeviceTasks() 
        				  // function does not take very long to execute (ex: <100 
        				  // instruction cycles) before it returns.
        #endif
    				  

		// Application-specific tasks.
		// Application related code may be added here, or in the ProcessIO() function.
        ProcessIO();        
    }//end while
}//end main


/********************************************************************
 * Function:        static void InitializeSystem(void)
 *
 * PreCondition:    None
 *
 * Input:           None
 *
 * Output:          None
 *
 * Side Effects:    None
 *
 * Overview:        InitializeSystem is a centralize initialization
 *                  routine. All required USB initialization routines
 *                  are called from here.
 *
 *                  User application initialization routine should
 *                  also be called from here.                  
 *
 * Note:            None
 *******************************************************************/
static void InitializeSystem(void)
{
//	The USB specifications require that USB peripheral devices must never source
//	current onto the Vbus pin.  Additionally, USB peripherals should not source
//	current on D+ or D- when the host/hub is not actively powering the Vbus line.
//	When designing a self powered (as opposed to bus powered) USB peripheral
//	device, the firmware should make sure not to turn on the USB module and D+
//	or D- pull up resistor unless Vbus is actively powered.  Therefore, the
//	firmware needs some means to detect when Vbus is being powered by the host.
//	A 5V tolerant I/O pin can be connected to Vbus (through a resistor), and
// 	can be used to detect when Vbus is high (host actively powering), or low
//	(host is shut down or otherwise not supplying power).  The USB firmware
// 	can then periodically poll this I/O pin to know when it is okay to turn on
//	the USB module/D+/D- pull up resistor.  When designing a purely bus powered
//	peripheral device, it is not possible to source current on D+ or D- when the
//	host is not actively providing power on Vbus. Therefore, implementing this
//	bus sense feature is optional.  This firmware can be made to use this bus
//	sense feature by making sure "USE_USB_BUS_SENSE_IO" has been defined in the
//	HardwareProfile.h file.    
    #if defined(USE_USB_BUS_SENSE_IO)
    tris_usb_bus_sense = INPUT_PIN; // See HardwareProfile.h
    #endif
    
//	If the host PC sends a GetStatus (device) request, the firmware must respond
//	and let the host know if the USB peripheral device is currently bus powered
//	or self powered.  See chapter 9 in the official USB specifications for details
//	regarding this request.  If the peripheral device is capable of being both
//	self and bus powered, it should not return a hard coded value for this request.
//	Instead, firmware should check if it is currently self or bus powered, and
//	respond accordingly.  If the hardware has been configured like demonstrated
//	on the PICDEM FS USB Demo Board, an I/O pin can be polled to determine the
//	currently selected power source.  On the PICDEM FS USB Demo Board, "RA2" 
//	is used for	this purpose.  If using this feature, make sure "USE_SELF_POWER_SENSE_IO"
//	has been defined in HardwareProfile - (platform).h, and that an appropriate I/O pin 
//  has been mapped	to it.
    #if defined(USE_SELF_POWER_SENSE_IO)
    tris_self_power = INPUT_PIN;	// See HardwareProfile.h
    #endif

    UserInit();
    
    USBDeviceInit();	//usb_device.c.  Initializes USB module SFRs and firmware
    					//variables to known states.
}//end InitializeSystem



/******************************************************************************
 * Function:        void UserInit(void)
 *
 * PreCondition:    None
 *
 * Input:           None
 *
 * Output:          None
 *
 * Side Effects:    None
 *
 * Overview:        This routine should take care of all of the demo code
 *                  initialization that is required.
 *
 * Note:            
 *
 *****************************************************************************/
void UserInit(void)
{
    BYTE fi, fj;

    //initialize the variable holding the handle for the last
    // transmission
    USBOutHandle = 0;
    USBInHandle = 0;


    ANSELA = 0x00000000;    // Digital Input (b1=AN1, b0=AN0)
    ANSELB = 0x00000000;    // Digital Input (b15=AN9, b14=AN10, b13=AN11, b3=AN5, b2=AN4, b1=AN3, b0=AN2)
    ANSELC = 0x00000000;    // Digital Input (b3=AN12, b2=AN8, b1=AN7, b0=AN6)
    
    TRISA = 0x0091;     // RA0-4,7-10
    //RA0 = SW11 入力
    //RA1 = LED1 RED 出力
    //RA2 =
    //RA3 =
    //RA4 = SPI SDI 入力
    //RA7 = SW2 入力
    //RA8 = LED2 RED 出力
    //RA9 = SPI SS 出力
    //RA10 = LED1 GREEN 出力
    TRISB = 0x63A8;     // RB0-15
    //RB0 = 
    //RB1 =
    //RB2 = LED1 BLUE 出力
    //RB3 = SW4 入力
    //RB4 = LED2 BLUE 出力
    //RB5 = SW7 入力
    //RB6 = xx
    //RB7 = SW8 入力
    //RB8 = SW9 入力
    //RB9 = SW10 入力
    //RB10 =
    //RB11 =
    //RB12 = xx
    //RB13 = SW1 入力
    //RB14 = SW3 入力
    //RB15 = SPI SCK 出力
    TRISC = 0x0027;     // RC0-9
    //RC0 = SW5 入力
    //RC1 = ENCODER B 入力
    //RC2 = ENCODER A 入力
    //RC3 = SPI SDO 出力
    //RC4 = LED2 GREEN 出力
    //RC5 = SW6 入力
    //RC6 = TP
    //RC7 = TP
    //RC8 = TP
    //RC9 = TP
    LATA = 0;
    LATB = 0;
    LATC = 0;

    // PPS設定F
    CFGCONbits.IOLOCK = 1;  // Unlock
    //Input
//    INT2Rbits.INT2R = 0x04;     // RB2 を INT2
//    INT4Rbits.INT4R = 0x01;     // RB3 を INT4
    //Output
    CFGCONbits.IOLOCK = 0;  //Lock

    INTCONbits.MVEC = 1;    // enable multi-vector mode
//    INTCONbits.MVEC = 0;    // enable single-vector mode


    //変数初期化
    // SW
#if 0
    for( fi = 0; fi < SW_ALL_NUM; fi++ )
    {
        sw_now_fix[fi] = N_OFF;
        sw_now_fix_pre[fi] = N_OFF;
//        sw_press_on_cnt[fi] = 0;
//        sw_press_off_cnt[fi] = 0;
        sw_output_flag[fi] = N_OFF;
        sw_output_multimedia_flag[fi] = N_OFF;
        encoder_key_out_flag[fi] = N_OFF;
    }
#endif
//    for(fi = 0; fi < MOUSE_BUFF_SIZE; fi++)
//    {
//        mouse_input[fi] = 0;
//        mouse_buffer[fi] = 0;
//    }
    for(fi = 0; fi < JOYSTICK_BUFF_SIZE; fi++)
    {
//        joystick_input[fi] = 0;
        joystick_buffer[fi] = joystick_default_value[fi];
//        joystick_buffer_pre[fi] = 0;
    }
//    for(fi = 0; fi < KEYBOARD_BUFF_SIZE; fi++)
//    {
//        keyboard_input[fi] = 0;
//        keyboard_buffer[fi] = 0;
//        keyboard_buffer_pre[fi] = 0;
//    }
//    for(fi = 0; fi < HID_INT_OUT_EP_SIZE; fi++)
//    {
//        keyboard_output[fi] = 0;
//    }
//    for(fi = 0; fi < MULTIMEDIA_BUFF_SIZE; fi++)
//    {
//        multimedia_input[fi] = 0;
//        multimedia_buffer[fi] = 0;
//    }
    for(fi = 0; fi < ENCODER_SCRIPT_NUM; fi++)
    {
        encoder_script_exe_now_idx[fi] = 0xFF;
    }
    

    /* Timer 1 */
    timer1_init();
    /* Timer 2 */
    timer2_init();
    /* Timer 4 */
    timer4_init();

    // SPI1初期化
    l_spi1_init();  // Flash

    
	// Flash Memory読み込みバッファクリア
    u_BaseHead_buffClr(&p_base_head);
    for(fi = 0; fi < MODE_NUM; fi++)
    {
        u_BaseInfo_buffClr(&p_base_infos[fi]);
        u_BaseInfo_buffClr(&my_base_infos[fi]);
    }
    for(fi = 0; fi < MODE_NUM; fi++)
    {
        for(fj = 0; fj < MODE_FUNCTION_NUM; fj++)
        {
            u_FuncInfo_buffClr(&p_func_infos[fi][fj]);
        }
    }
    for(fi = 0; fi < ENCODER_SCRIPT_NUM; fi++)
    {
        u_EncoderScriptInfo_buffClr(&p_encoder_script_infos[fi]);
        u_EncoderScriptInfo_buffClr(&my_encoder_script_infos[fi]);
    }
    u_ScriptHead_buffClr(&p_script_head);
    u_ScriptInfo_buffClr(&p_script_info);
    
    // Flash Memory読み込み
    // 基本設定読み込み
    u_GetBaseHead(&p_base_head);
    Set_Base_Head(&p_base_head, &my_base_head);
    for(fi = 0; fi < MODE_NUM; fi++)
    {
        u_GetBaseInfo(fi, &p_base_infos[fi]);
        Set_Base_Info(fi, &p_base_infos[fi], &my_base_infos[fi]);
    }
    // 機能設定読み込み
    for(fi = 0; fi < MODE_NUM; fi++)
    {
        for(fj = 0; fj < MODE_FUNCTION_NUM; fj++)
        {
            u_GetFuncInfo(fi, fj, &p_func_infos[fi][fj]);
            Set_Func_Info(fi, fj, &p_func_infos[fi][fj], &my_func_infos[fi][fj]);
        }
    }
    for(fi = 0; fi < ENCODER_SCRIPT_NUM; fi++)
    {
        u_GetEncoderScriptInfo(fi, &p_encoder_script_infos[fi]);
        Set_EncoderScript_Info(fi, &p_encoder_script_infos[fi], &my_encoder_script_infos[fi]);
    }
    // SW機能設定読み込み
    for(fi = 0; fi < MODE_NUM; fi++)
    {
        for(fj = 0; fj < SW_NUM; fj++)
        {
            u_GetSWFuncInfo(fi, fj, &p_sw_func_infos[fi][fj]);
            Set_SW_Func_Info(fi, fj, &p_sw_func_infos[fi][fj], &my_sw_func_infos[fi][fj]);
        }
    }

#if 0
    // DEBUG
    my_base_head.BaseHead.led_light_mode = 0;
    my_base_head.BaseHead.led_light_func = 1;
    my_base_head.BaseHead.led_off_time = 5;
    my_base_head.BaseHead.led_sleep = 0;
#endif
    
    // LED set
//    set_LED_output_data(my_base_infos[mode_no_now].BaseInfo.mode_led_color_no, my_base_infos[mode_no_now].BaseInfo.mode_led_color_flag, my_base_infos[mode_no_now].BaseInfo.mode_led_RGB);
    set_LED_output_data(0, 1, my_base_infos[mode_no_fix].BaseInfo.mode_led_RGB, led_output_fix, my_base_infos[mode_no_fix].BaseInfo.mode_led_brightness_level);
    set_LED_output_data(0, 1, my_base_infos[mode_no_fix].BaseInfo.mode_led_RGB, led_output_mode_fix, my_base_infos[mode_no_fix].BaseInfo.mode_led_brightness_level);

    //割り込み設定
//    INTCONbits.INT2EP = 0;	// INT2割り込みエッジ極性制御　0=立ち下り、1=立ち上がり
//    IPC2bits.INT2IP = 4;        // INT2 Priority
//    IPC2bits.INT2IS = 0;        // INT2 Sub-Priority
//    IFS0bits.INT2IF = 0;        // INT2 Interrupt Flag
//    IEC0bits.INT2IE = 1;        // INT2 Interrupt Enable
//    INTCONbits.INT4EP = 0;	// INT4割り込みエッジ極性制御　0=立ち下り、1=立ち上がり
//    IPC4bits.INT4IP = 4;        // INT4 Priority
//    IPC4bits.INT4IS = 0;        // INT4 Sub-Priority
//    IFS0bits.INT4IF = 0;        // INT4 Interrupt Flag
//    IEC0bits.INT4IE = 1;        // INT4 Interrupt Enable

    
    /* 割り込み許可 */
    INTEnableInterrupts();

    // DEBUG
//    u_GetPatternInfo(1, &p_pattern_info);
    
}//end UserInit

/********************************************************************
 * Function:        void ProcessIO(void)
 *
 * PreCondition:    None
 *
 * Input:           None
 *
 * Output:          None
 *
 * Side Effects:    None
 *
 * Overview:        This function is a place holder for other user
 *                  routines. It is a mixture of both USB and
 *                  non-USB tasks.
 *
 * Note:            None
 *******************************************************************/
void ProcessIO(void)
{
    WORD fi;
    
    debug_arr1[0]++;

#if 0
    debug_arr1[3] = mode_no_set;
    debug_arr1[4] = mode_no_fix;
    debug_arr1[5] = mode_func_no_set;
    debug_arr1[6] = mode_func_no_fix;
#endif
    
    // SW 入力チェック
    if(sw_input_flag != 0)
    {
        sw_input_flag = 0;
        // SW入力
        Switch_Input();
    }
    
    // エンコーダー入力
    Encoder_Input();
    
    
    // LED Set
    if(led_func_lighting_flag == 0 &&(mode_no_fix != mode_no_pre_led || led_change_flag_mode != 0))
    {   // 機能LED消灯中　かつ　モード変化あり
        set_LED_output_data(0, 1, my_base_infos[mode_no_fix].BaseInfo.mode_led_RGB, led_output_fix, my_base_infos[mode_no_fix].BaseInfo.mode_led_brightness_level);
        set_LED_output_data(0, 1, my_base_infos[mode_no_fix].BaseInfo.mode_led_RGB, led_output_mode_fix, my_base_infos[mode_no_fix].BaseInfo.mode_led_brightness_level);
        mode_no_pre_led = mode_no_fix;
        led_change_flag_mode = 0;
        
        led_light_time_counter = 0;
        led_light_status = 0;
        led_light_on_type = LED_LIGHT_ON_TYPE_MODE;
    }
//    if(mode_func_no_fix != mode_func_no_pre_led && led_change_flag_func != 0)
    if(led_change_flag_func != 0)
    {   // 機能変化あり
        set_LED_output_data(0, 1, my_func_infos[mode_no_fix][mode_func_no_fix].func_info.led_RGB, led_output_fix, my_func_infos[mode_no_fix][mode_func_no_fix].func_info.led_brightness_level);
        mode_func_no_pre_led = mode_func_no_fix;
        led_change_flag_func = 0;

        led_func_lighting_flag = 1;
        led_light_time_counter = 0;
        led_light_status = 0;
        led_light_func_type = my_base_head.BaseHead.led_light_func;
        led_light_on_type = LED_LIGHT_ON_TYPE_FUNC;
    }
    
    
    // 次に実行するスクリプト要求あり？
    // スクリプト未実行なら実行する
    if(next_script_exe_req_scriptNo > 0)
    {
        if(Get_Exe_Script_No(0) == 0)
        {
            Set_Script_Exe_Info(0, next_script_exe_req_scriptNo);
            next_script_exe_req_scriptNo = 0;
            Script_Reset(0);
            Script_Run(0);
        } 
    }
    
    // Script実行中？
//	if( Get_Script_Output_Status() == 1 )
//	{
		// Scriptデータ読み込み
		Get_Next_Script_Data();
		// Script次コマンドセット
		Get_Next_Script_Command();
		// Script送信コードセット
		Set_USB_Send_Code();
//	}

    // User Application USB tasks
    if((USBDeviceState < CONFIGURED_STATE)||(USBSuspendControl==1))
    {
        USB_Sleep_Flag = 1;
        return;
    }
    else
    {
        if(USBGetRemoteWakeupStatus() == TRUE && USBIsBusSuspended() == TRUE)
        {
            USB_Sleep_Flag = 1;
        }
        else
        {
//            if(USB_Sleep_Flag == 1)
//            {
//
//            }
            USB_Sleep_Flag = 0;
        }
    }

    USB_Comm();

}//end ProcessIO



// ******************************************************************************************************
// ************** USB Callback Functions ****************************************************************
// ******************************************************************************************************
// The USB firmware stack will call the callback functions USBCBxxx() in response to certain USB related
// events.  For example, if the host PC is powering down, it will stop sending out Start of Frame (SOF)
// packets to your device.  In response to this, all USB devices are supposed to decrease their power
// consumption from the USB Vbus to <2.5mA* each.  The USB module detects this condition (which according
// to the USB specifications is 3+ms of no bus activity/SOF packets) and then calls the USBCBSuspend()
// function.  You should modify these callback functions to take appropriate actions for each of these
// conditions.  For example, in the USBCBSuspend(), you may wish to add code that will decrease power
// consumption from Vbus to <2.5mA (such as by clock switching, turning off LEDs, putting the
// microcontroller to sleep, etc.).  Then, in the USBCBWakeFromSuspend() function, you may then wish to
// add code that undoes the power saving things done in the USBCBSuspend() function.

// The USBCBSendResume() function is special, in that the USB stack will not automatically call this
// function.  This function is meant to be called from the application firmware instead.  See the
// additional comments near the function.

// Note *: The "usb_20.pdf" specs indicate 500uA or 2.5mA, depending upon device classification. However,
// the USB-IF has officially issued an ECN (engineering change notice) changing this to 2.5mA for all 
// devices.  Make sure to re-download the latest specifications to get all of the newest ECNs.

/******************************************************************************
 * Function:        void USBCBSuspend(void)
 *
 * PreCondition:    None
 *
 * Input:           None
 *
 * Output:          None
 *
 * Side Effects:    None
 *
 * Overview:        Call back that is invoked when a USB suspend is detected
 *
 * Note:            None
 *****************************************************************************/
void USBCBSuspend(void)
{
	//Example power saving code.  Insert appropriate code here for the desired
	//application behavior.  If the microcontroller will be put to sleep, a
	//process similar to that shown below may be used:
	
	//ConfigureIOPinsForLowPower();
	//SaveStateOfAllInterruptEnableBits();
	//DisableAllInterruptEnableBits();
	//EnableOnlyTheInterruptsWhichWillBeUsedToWakeTheMicro();	//should enable at least USBActivityIF as a wake source
	//Sleep();
	//RestoreStateOfAllPreviouslySavedInterruptEnableBits();	//Preferrably, this should be done in the USBCBWakeFromSuspend() function instead.
	//RestoreIOPinsToNormal();									//Preferrably, this should be done in the USBCBWakeFromSuspend() function instead.

	//IMPORTANT NOTE: Do not clear the USBActivityIF (ACTVIF) bit here.  This bit is 
	//cleared inside the usb_device.c file.  Clearing USBActivityIF here will cause 
	//things to not work as intended.	
	
    
    USB_Sleep_Flag = 1;

}



/******************************************************************************
 * Function:        void USBCBWakeFromSuspend(void)
 *
 * PreCondition:    None
 *
 * Input:           None
 *
 * Output:          None
 *
 * Side Effects:    None
 *
 * Overview:        The host may put USB peripheral devices in low power
 *					suspend mode (by "sending" 3+ms of idle).  Once in suspend
 *					mode, the host may wake the device back up by sending non-
 *					idle state signalling.
 *					
 *					This call back is invoked when a wakeup from USB suspend 
 *					is detected.
 *
 * Note:            None
 *****************************************************************************/
void USBCBWakeFromSuspend(void)
{
	// If clock switching or other power savings measures were taken when
	// executing the USBCBSuspend() function, now would be a good time to
	// switch back to normal full power run mode conditions.  The host allows
	// 10+ milliseconds of wakeup time, after which the device must be 
	// fully back to normal, and capable of receiving and processing USB
	// packets.  In order to do this, the USB module must receive proper
	// clocking (IE: 48MHz clock must be available to SIE for full speed USB
	// operation).  
	// Make sure the selected oscillator settings are consistent with USB 
    // operation before returning from this function.
}

/********************************************************************
 * Function:        void USBCB_SOF_Handler(void)
 *
 * PreCondition:    None
 *
 * Input:           None
 *
 * Output:          None
 *
 * Side Effects:    None
 *
 * Overview:        The USB host sends out a SOF packet to full-speed
 *                  devices every 1 ms. This interrupt may be useful
 *                  for isochronous pipes. End designers should
 *                  implement callback routine as necessary.
 *
 * Note:            None
 *******************************************************************/
void USBCB_SOF_Handler(void)
{
    // No need to clear UIRbits.SOFIF to 0 here.
    // Callback caller is already doing that.
}

/*******************************************************************
 * Function:        void USBCBErrorHandler(void)
 *
 * PreCondition:    None
 *
 * Input:           None
 *
 * Output:          None
 *
 * Side Effects:    None
 *
 * Overview:        The purpose of this callback is mainly for
 *                  debugging during development. Check UEIR to see
 *                  which error causes the interrupt.
 *
 * Note:            None
 *******************************************************************/
void USBCBErrorHandler(void)
{
    // No need to clear UEIR to 0 here.
    // Callback caller is already doing that.

	// Typically, user firmware does not need to do anything special
	// if a USB error occurs.  For example, if the host sends an OUT
	// packet to your device, but the packet gets corrupted (ex:
	// because of a bad connection, or the user unplugs the
	// USB cable during the transmission) this will typically set
	// one or more USB error interrupt flags.  Nothing specific
	// needs to be done however, since the SIE will automatically
	// send a "NAK" packet to the host.  In response to this, the
	// host will normally retry to send the packet again, and no
	// data loss occurs.  The system will typically recover
	// automatically, without the need for application firmware
	// intervention.
	
	// Nevertheless, this callback function is provided, such as
	// for debugging purposes.
}


/*******************************************************************
 * Function:        void USBCBCheckOtherReq(void)
 *
 * PreCondition:    None
 *
 * Input:           None
 *
 * Output:          None
 *
 * Side Effects:    None
 *
 * Overview:        When SETUP packets arrive from the host, some
 * 					firmware must process the request and respond
 *					appropriately to fulfill the request.  Some of
 *					the SETUP packets will be for standard
 *					USB "chapter 9" (as in, fulfilling chapter 9 of
 *					the official USB specifications) requests, while
 *					others may be specific to the USB device class
 *					that is being implemented.  For example, a HID
 *					class device needs to be able to respond to
 *					"GET REPORT" type of requests.  This
 *					is not a standard USB chapter 9 request, and 
 *					therefore not handled by usb_device.c.  Instead
 *					this request should be handled by class specific 
 *					firmware, such as that contained in usb_function_hid.c.
 *
 * Note:            None
 *******************************************************************/
void USBCBCheckOtherReq(void)
{
    USBCheckHIDRequest();
}//end


/*******************************************************************
 * Function:        void USBCBStdSetDscHandler(void)
 *
 * PreCondition:    None
 *
 * Input:           None
 *
 * Output:          None
 *
 * Side Effects:    None
 *
 * Overview:        The USBCBStdSetDscHandler() callback function is
 *					called when a SETUP, bRequest: SET_DESCRIPTOR request
 *					arrives.  Typically SET_DESCRIPTOR requests are
 *					not used in most applications, and it is
 *					optional to support this type of request.
 *
 * Note:            None
 *******************************************************************/
void USBCBStdSetDscHandler(void)
{
    // Must claim session ownership if supporting this request
}//end


/*******************************************************************
 * Function:        void USBCBInitEP(void)
 *
 * PreCondition:    None
 *
 * Input:           None
 *
 * Output:          None
 *
 * Side Effects:    None
 *
 * Overview:        This function is called when the device becomes
 *                  initialized, which occurs after the host sends a
 * 					SET_CONFIGURATION (wValue not = 0) request.  This 
 *					callback function should initialize the endpoints 
 *					for the device's usage according to the current 
 *					configuration.
 *
 * Note:            None
 *******************************************************************/
void USBCBInitEP(void)
{
    //enable the HID endpoint
    USBEnableEndpoint(HID_EP1,USB_IN_ENABLED|USB_OUT_ENABLED|USB_HANDSHAKE_ENABLED|USB_DISALLOW_SETUP);
    USBEnableEndpoint(HID_EP2,USB_IN_ENABLED|USB_OUT_ENABLED|USB_HANDSHAKE_ENABLED|USB_DISALLOW_SETUP);
    USBEnableEndpoint(HID_EP3,USB_IN_ENABLED|USB_OUT_ENABLED|USB_HANDSHAKE_ENABLED|USB_DISALLOW_SETUP);
    USBEnableEndpoint(HID_EP4,USB_IN_ENABLED|USB_OUT_ENABLED|USB_HANDSHAKE_ENABLED|USB_DISALLOW_SETUP);
    //Re-arm the OUT endpoint for the next packet
    lastOUTTransmission_Keyboard = HIDRxPacket(HID_EP1,(BYTE*)&keyboard_output[0], HID_INT_OUT_EP_SIZE);
    USBOutHandle = HIDRxPacket(HID_EP4,(BYTE*)&ReceivedDataBuffer,RX_DATA_BUFFER_SIZE);
}

/********************************************************************
 * Function:        void USBCBSendResume(void)
 *
 * PreCondition:    None
 *
 * Input:           None
 *
 * Output:          None
 *
 * Side Effects:    None
 *
 * Overview:        The USB specifications allow some types of USB
 * 					peripheral devices to wake up a host PC (such
 *					as if it is in a low power suspend to RAM state).
 *					This can be a very useful feature in some
 *					USB applications, such as an Infrared remote
 *					control	receiver.  If a user presses the "power"
 *					button on a remote control, it is nice that the
 *					IR receiver can detect this signalling, and then
 *					send a USB "command" to the PC to wake up.
 *					
 *					The USBCBSendResume() "callback" function is used
 *					to send this special USB signalling which wakes 
 *					up the PC.  This function may be called by
 *					application firmware to wake up the PC.  This
 *					function will only be able to wake up the host if
 *                  all of the below are true:
 *					
 *					1.  The USB driver used on the host PC supports
 *						the remote wakeup capability.
 *					2.  The USB configuration descriptor indicates
 *						the device is remote wakeup capable in the
 *						bmAttributes field.
 *					3.  The USB host PC is currently sleeping,
 *						and has previously sent your device a SET 
 *						FEATURE setup packet which "armed" the
 *						remote wakeup capability.   
 *
 *                  If the host has not armed the device to perform remote wakeup,
 *                  then this function will return without actually performing a
 *                  remote wakeup sequence.  This is the required behavior, 
 *                  as a USB device that has not been armed to perform remote 
 *                  wakeup must not drive remote wakeup signalling onto the bus;
 *                  doing so will cause USB compliance testing failure.
 *                  
 *					This callback should send a RESUME signal that
 *                  has the period of 1-15ms.
 *
 * Note:            This function does nothing and returns quickly, if the USB
 *                  bus and host are not in a suspended condition, or are 
 *                  otherwise not in a remote wakeup ready state.  Therefore, it
 *                  is safe to optionally call this function regularly, ex: 
 *                  anytime application stimulus occurs, as the function will
 *                  have no effect, until the bus really is in a state ready
 *                  to accept remote wakeup. 
 *
 *                  When this function executes, it may perform clock switching,
 *                  depending upon the application specific code in 
 *                  USBCBWakeFromSuspend().  This is needed, since the USB
 *                  bus will no longer be suspended by the time this function
 *                  returns.  Therefore, the USB module will need to be ready
 *                  to receive traffic from the host.
 *
 *                  The modifiable section in this routine may be changed
 *                  to meet the application needs. Current implementation
 *                  temporary blocks other functions from executing for a
 *                  period of ~3-15 ms depending on the core frequency.
 *
 *                  According to USB 2.0 specification section 7.1.7.7,
 *                  "The remote wakeup device must hold the resume signaling
 *                  for at least 1 ms but for no more than 15 ms."
 *                  The idea here is to use a delay counter loop, using a
 *                  common value that would work over a wide range of core
 *                  frequencies.
 *                  That value selected is 1800. See table below:
 *                  ==========================================================
 *                  Core Freq(MHz)      MIP         RESUME Signal Period (ms)
 *                  ==========================================================
 *                      48              12          1.05
 *                       4              1           12.6
 *                  ==========================================================
 *                  * These timing could be incorrect when using code
 *                    optimization or extended instruction mode,
 *                    or when having other interrupts enabled.
 *                    Make sure to verify using the MPLAB SIM's Stopwatch
 *                    and verify the actual signal on an oscilloscope.
 *******************************************************************/
void USBCBSendResume(void)
{
    static WORD delay_count;
    
    //First verify that the host has armed us to perform remote wakeup.
    //It does this by sending a SET_FEATURE request to enable remote wakeup,
    //usually just before the host goes to standby mode (note: it will only
    //send this SET_FEATURE request if the configuration descriptor declares
    //the device as remote wakeup capable, AND, if the feature is enabled
    //on the host (ex: on Windows based hosts, in the device manager 
    //properties page for the USB device, power management tab, the 
    //"Allow this device to bring the computer out of standby." checkbox 
    //should be checked).
    if(USBGetRemoteWakeupStatus() == TRUE) 
    {
        //Verify that the USB bus is in fact suspended, before we send
        //remote wakeup signalling.
        if(USBIsBusSuspended() == TRUE)
        {
            USBMaskInterrupts();
            
            //Clock switch to settings consistent with normal USB operation.
            USBCBWakeFromSuspend();
            USBSuspendControl = 0; 
            USBBusIsSuspended = FALSE;  //So we don't execute this code again, 
                                        //until a new suspend condition is detected.

            //Section 7.1.7.7 of the USB 2.0 specifications indicates a USB
            //device must continuously see 5ms+ of idle on the bus, before it sends
            //remote wakeup signalling.  One way to be certain that this parameter
            //gets met, is to add a 2ms+ blocking delay here (2ms plus at 
            //least 3ms from bus idle to USBIsBusSuspended() == TRUE, yeilds
            //5ms+ total delay since start of idle).
            delay_count = 3600U;        
            do
            {
                delay_count--;
            }while(delay_count);
            
            //Now drive the resume K-state signalling onto the USB bus.
            USBResumeControl = 1;       // Start RESUME signaling
            delay_count = 1800U;        // Set RESUME line for 1-13 ms
            do
            {
                delay_count--;
            }while(delay_count);
            USBResumeControl = 0;       //Finished driving resume signalling

            USBUnmaskInterrupts();
        }
    }
}


/*******************************************************************
 * Function:        BOOL USER_USB_CALLBACK_EVENT_HANDLER(
 *                        USB_EVENT event, void *pdata, WORD size)
 *
 * PreCondition:    None
 *
 * Input:           USB_EVENT event - the type of event
 *                  void *pdata - pointer to the event data
 *                  WORD size - size of the event data
 *
 * Output:          None
 *
 * Side Effects:    None
 *
 * Overview:        This function is called from the USB stack to
 *                  notify a user application that a USB event
 *                  occured.  This callback is in interrupt context
 *                  when the USB_INTERRUPT option is selected.
 *
 * Note:            None
 *******************************************************************/
BOOL USER_USB_CALLBACK_EVENT_HANDLER(int event, void *pdata, WORD size)
{
    switch(event)
    {
        case EVENT_TRANSFER:
            //Add application specific callback task or callback function here if desired.
            break;
        case EVENT_SOF:
            USBCB_SOF_Handler();
            break;
        case EVENT_SUSPEND:
            USBCBSuspend();
            break;
        case EVENT_RESUME:
            USBCBWakeFromSuspend();
            break;
        case EVENT_CONFIGURED: 
            USBCBInitEP();
            break;
        case EVENT_SET_DESCRIPTOR:
            USBCBStdSetDscHandler();
            break;
        case EVENT_EP0_REQUEST:
            USBCBCheckOtherReq();
            break;
        case EVENT_BUS_ERROR:
            USBCBErrorHandler();
            break;
        case EVENT_TRANSFER_TERMINATED:
            //Add application specific callback task or callback function here if desired.
            //The EVENT_TRANSFER_TERMINATED event occurs when the host performs a CLEAR
            //FEATURE (endpoint halt) request on an application endpoint which was 
            //previously armed (UOWN was = 1).  Here would be a good place to:
            //1.  Determine which endpoint the transaction that just got terminated was 
            //      on, by checking the handle value in the *pdata.
            //2.  Re-arm the endpoint if desired (typically would be the case for OUT 
            //      endpoints).
            break;
        default:
            break;
    }      
    return TRUE; 
}

/** EOF main.c *************************************************/
#endif
