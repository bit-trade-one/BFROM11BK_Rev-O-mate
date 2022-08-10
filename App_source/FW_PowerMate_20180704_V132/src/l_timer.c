#include "app.h"
#include "l_timer.h"
#include "main_sub.h"
#include "l_flash.h"
#include "l_spi.h"
#include "l_script.h"
//#include "l_script_data1.h"
//#include "l_script_data2.h"


void timer1_init(void);
void timer2_init(void);
void timer4_init(void);
void Timer1_Task(void);
void Timer2_Task(void);

WORD l_timer1_counter = 0;
WORD l_timer2_counter = 0;

BYTE sw_input_counter = 0;
BYTE sw_input_flag = 0;

// Timer1 初期化
// 1ms
void timer1_init(void)
{
    T1CONbits.ON = 0;       // Timer1 OFF
    TMR1 = 0;
    
    PR1 = TIMER1_PERIOD;
    T1CONbits.TCKPS = 0;    // 0=1:1, 1=1:8, 2=1:64, 3=1:256 prescale
    T1CONbits.TCS = 0;      // Internal clock
    IPC1bits.T1IP = 3;      // interrupt priority
    IPC1bits.T1IS = 1;      // interrupt subpriority
    IFS0bits.T1IF = 0;      // clear timer1 interrupt flag
    IEC0bits.T1IE = 1;      // enable timer1 interrupt
    T1CONbits.ON = 1;       // Timer1 ON
}

// Timer2 初期化
// 1ms
void timer2_init(void)
{
    T2CONbits.ON = 0;       // Timer2 OFF
    TMR2 = 0;
    
    PR2 = TIMER2_PERIOD;
    T2CONbits.TCKPS = 0;    // 0=1:1, 1=1:2, 2=1:4, 3=1:8, 4=1:16, 5=1:32, 6=1:64, 7=1:256 prescale
    T2CONbits.T32 = 0;      // 16-bit timer
    IPC2bits.T2IP = 3;      // interrupt priority
    IPC2bits.T2IS = 1;      // interrupt subpriority
    IFS0bits.T2IF = 0;      // clear timer2 interrupt flag
    IEC0bits.T2IE = 1;      // enable timer2 interrupt
    T2CONbits.ON = 1;       // Timer2 ON
}

// Timer4 初期化
// 32bit
// 1ms
void timer4_init(void)
{
    T4CONbits.ON = 0;       // Timer4 OFF
    TMR4 = 0;
    PR4 = 0;
    T4CONbits.TCKPS = 1;    // 0=1:1, 1=1:2, 2=1:4, 3=1:8, 4=1:16, 5=1:32, 6=1:64, 7=1:256 prescale
    T4CONbits.T32 = 1;      // 0=16bit, 1=32bit timer
//    IPC4bits.T4IP = 3;      // interrupt priority
//    IPC4bits.T4IS = 1;      // interrupt subpriority
    IFS0bits.T4IF = 0;      // clear timer4 interrupt flag
    IEC0bits.T4IE = 0;      // disable timer4 interrupt
    T4CONbits.ON = 1;       // Timer4 ON
}

// Timer1 Task
void Timer1_Task(void)
{
    BYTE fi;
    IFS0bits.T1IF = 0;

    // SW 入力チェック
//    Switch_Input();
    sw_input_counter++;
    if(sw_input_counter >= SW_INPUT_TIME)
    {
        sw_input_counter = 0;
        sw_input_flag = 1;
    }
    // エンコーダー　キー出力時間
    for(fi = 0; fi < SW_ALL_NUM; fi++)
    {
        if(encoder_key_out_flag[fi] > 0)
        {
            encoder_key_out_flag[fi]--;
        }
    }
    
    // Scriptインターバルデクリメント
#if 1
    if( Get_Script_Output_Status() == 1 )
    {
        if( script_exe_info.Interval.Val > 0 )
        {	// デクリメント
            script_exe_info.Interval.Val--;
        }
    }
#else
    if(Get_Script_Output_Status_Integration() == 1)
    {
        for(fi = 0; fi < SCRIPT_EXE_INFO_NUM; fi++)
        {
            if( script_exe_info.Exe_Flag[fi] == SCRIPT_EXE_FLAG_EXE && script_exe_info.Pause_Flag[fi] == 0 )
            {	// スクリプト実行中　かつ　一時停止でないとき

                if( script_exe_info.Interval[fi].Val > 0 )
                {	// デクリメント
                    script_exe_info.Interval[fi].Val--;
                }
//                if( script_exe_info_2.Interval[fi].Val > 0 )
//                {	// デクリメント
//                    script_exe_info_2.Interval[fi].Val--;
//                }
            }
        }
    }
#endif

    // マウスダブルクリック間隔
    if(mouse_w_click_interval_counter > 0)
    {
        mouse_w_click_interval_counter--;
    }
    // ダイアル一時機能変更後に、無効時間のカウント用
    if(dial_function_enocder_disabled_time > 0)
    {
        dial_function_enocder_disabled_time--;
    }

    debug_arr1[1]++;
}

// Timer2 Task
void Timer2_Task(void)
{
    BYTE fi;
    DWORD dw_temp;
    BYTE b_temp;
    BYTE b_led_preview = 0;
    BYTE b_LED_OFF_Set = 0;
    BYTE b_LED_Duty_Set = 0;
    BYTE* p_led_out_data;
    
    IFS0bits.T2IF = 0;

    l_timer2_counter++;

    // 出力データは？
    if(led_output_preview_time_counter > 0)
    {   // LED出力プレビュー
        led_output_preview_time_counter--;
        p_led_out_data = led_output_preview;
        b_led_preview = 1;
    }
    else
    {   // 通常時 LED出力
        p_led_out_data = led_output_fix;
    }

    // LED 点灯Duty制御カウンタ
    led_output_duty_count++;
    if(led_output_duty_count >= LED_DUTY_MAX)
    {
        led_output_duty_count = 0;
    }
    
    if(USB_Sleep_Flag == 1 && my_base_head.BaseHead.led_sleep == LED_SLEEP_ENABLED)
    {   // スリープ時はLED消灯
        b_LED_OFF_Set = 1;
    }
    else if(b_led_preview == 1)
    {   // プレビュー
        b_LED_Duty_Set = 1;
    }
    else if(led1_blink_flag != 0)
    {   // LED1 点滅
        if(led1_blink_counter == 0)
        {
            led1_blink_counter = LED1_BLINK_ON_TIME + LED1_BLINK_OFF_TIME;
        }
        if(led1_blink_counter > LED1_BLINK_OFF_TIME)
        {   // ON
            b_LED_Duty_Set = 1;
        }
        else
        {   // OFF
            b_LED_OFF_Set = 1;
        }
        led1_blink_counter--;

    }
    else if(led_light_on_type == LED_LIGHT_ON_TYPE_FUNC)
    {   // LED点灯 FUNC
        if(led_light_func_type == LED_LIGHT_TYPE_FUNC_SLOW)
        {   // ゆっくり消灯
            if(led_light_status == 0)
            {   // 点灯
                b_LED_Duty_Set = 1;
                if(led_light_time_counter >= LED_LIGHT_TYPE_FUNC_SLOW_ON_TIME)
                {
                    led_light_status++;
                    led_light_time_counter = LED_LIGHT_TYPE_FUNC_SLOW_FADE_OUT_TIME;
                }
                else
                {
                    led_light_time_counter++;
                }
            }
            else if(led_light_status == 1)
            {   // フェードアウト
                for(fi = 0; fi < LED_DATA_NUM; fi++)
                {
                    dw_temp = (DWORD)p_led_out_data[fi] * (DWORD)led_light_time_counter;
                    b_temp = (BYTE)(dw_temp / LED_LIGHT_TYPE_FUNC_SLOW_FADE_OUT_TIME);
                    (led_output_duty_count >= b_temp) ? set_LED(fi,LED_OFF) : set_LED(fi,LED_ON);
                }
                
                if(led_light_time_counter == 0)
                {
                    led_light_status++;
                }
                else
                {
                    led_light_time_counter--;
                }
            }
            else
            {   // モード点灯に戻る
                led_func_lighting_flag = 0;
                led_change_flag_mode = 1;
            }
        }
        else if(led_light_func_type == LED_LIGHT_TYPE_FUNC_FLASH)
        {   // フラッシュして消灯
            if(led_light_status >= LED_LIGHT_TYPE_FUNC_FLASH_TIME)
            {   // モード点灯に戻る
                led_func_lighting_flag = 0;
                led_change_flag_mode = 1;
            }
            else
            {
                led_light_time_counter++;
                if(led_light_time_counter <= LED_LIGHT_TYPE_FUNC_FLASH_ON_TIME)
                {   // ON
                    b_LED_Duty_Set = 1;
//                    for(fi = 0; fi < LED_DATA_NUM; fi++)
//                    {
//                        (led_output_duty_count >= p_led_out_data[fi]) ? set_LED(fi,LED_OFF) : set_LED(fi,LED_ON);
//                    }
                }
                else if(led_light_time_counter <= (LED_LIGHT_TYPE_FUNC_FLASH_ON_TIME + LED_LIGHT_TYPE_FUNC_FLASH_OFF_TIME))
                {   // OFF
                    b_LED_OFF_Set = 1;
//                    for(fi = 0; fi < LED_DATA_NUM; fi++)
//                    {
//                        set_LED(fi,LED_OFF);
//                    }
                }
                else
                {
                    led_light_status++;
                    led_light_time_counter = 0;
                }
            }
        }
        else
        {// 次の機能変更までそのまま
            b_LED_Duty_Set = 1;
        }
    }
    else if(led_light_on_type == LED_LIGHT_ON_TYPE_MODE)
    {
        if(my_base_head.BaseHead.led_light_mode == LED_LIGHT_TYPE_MODE_OFF)
        {
            if(led_light_status == 0)
            {
                led_light_time_counter = LED_LIGHT_MODE_OFF_TIME_MUL * my_base_head.BaseHead.led_off_time;
                led_light_status++;
            }
            if(led_light_status == 1)
            {   // 通常点灯
                b_LED_Duty_Set = 1;
                if(led_light_time_counter == 0)
                {
                    led_light_status++;
                }
                else
                {
                    led_light_time_counter--;
                }
            }
            else
            {   // OFF
                b_LED_OFF_Set = 1;
            }
        }
        else
        {   // 通常点灯
            b_LED_Duty_Set = 1;
        }
    }
    else
    {   // 通常点灯
        b_LED_Duty_Set = 1;
    }
    
    // LED 消灯セット
    if(b_LED_OFF_Set == 1)
    {
        for(fi = 0; fi < LED_DATA_NUM; fi++)
        {
            set_LED(fi,LED_OFF);
        }
    }
    // LED Dutyセット
    if(b_LED_Duty_Set == 1)
    {
        for(fi = 0; fi < LED_DATA_NUM; fi++)
        {
            (led_output_duty_count >= p_led_out_data[fi]) ? set_LED(fi,LED_OFF) : set_LED(fi,LED_ON);
        }
    }
    
    debug_arr1[2]++;
}
