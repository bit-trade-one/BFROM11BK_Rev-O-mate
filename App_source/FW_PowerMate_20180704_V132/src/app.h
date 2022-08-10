/*******************************************************************************
 CAN WiFi

  Company:


  File Name:
    app.h

  Summary:
    Application specific header file.

  Description:
    Application specific header file.
 *******************************************************************************/

#ifndef APP_H
#define	APP_H


// *****************************************************************************
// Section: Included Files
// *****************************************************************************
// *****************************************************************************
#include "xc.h"
#include <stdint.h>
#include <stdbool.h>
#include <stddef.h>
#include "GenericTypeDefs.h"
#include "./USB/usb.h"
//#include "usb_config.h"


#if 0
typedef uint8_t		BYTE;                           /* 8-bit unsigned  */
typedef uint16_t	WORD;                           /* 16-bit unsigned */
typedef uint32_t	DWORD;                          /* 32-bit unsigned */

typedef union
{
    WORD Val;
    BYTE v[2];
    struct
    {
        BYTE LB;
        BYTE HB;
    } byte;
} WORD_VAL;

typedef union
{
    DWORD Val;
    WORD w[2];
    BYTE v[4];
    struct
    {
        WORD LW;
        WORD HW;
    } word;
    struct
    {
        BYTE LB;
        BYTE HB;
        BYTE UB;
        BYTE MB;
    } byte;
} DWORD_VAL;
#endif

// Flash ��{�ݒ�
typedef struct
{
	BYTE		mode;                   // 
    BYTE        led_sleep;              // LED�X���[�v�ݒ� 0:�X���[�v����A1:�X���[�v���Ȃ�
    BYTE        led_light_mode;         // LED�_���^�C�v�@���[�h
    BYTE        led_light_func;         // LED�_���^�C�v FUNC
    BYTE        led_off_time;           // LED��������[s]
    BYTE        encoder_typematic;      // �G���R�[�_�[�^�C�v�}�e�B�b�N 0:�A�ŁA1�������ςȂ�
	BYTE        reserved[2];
} ST_BASE_HEAD;
typedef union
{
	BYTE byte[8];
	ST_BASE_HEAD BaseHead;
} UN_BASE_HEAD;
typedef struct
{
    BYTE        sw_exe_script_no[11];   // SW_NUM
    BYTE        sw_sp_func_no[11];   // SW_NUM
    BYTE        encoder_func_no;        //
    BYTE        mode_led_color_no;      //
    BYTE        mode_led_color_flag;    //
    BYTE        mode_led_RGB[3];   //
    BYTE        mode_led_brightness_level;     // LED�P�x
	BYTE        reserved[3];
} ST_BASE_INFO;
typedef union
{
	BYTE byte[32];
	ST_BASE_INFO BaseInfo;
} UN_BASE_INFO;

// �@�\�ݒ���
typedef struct
{
//	BYTE        FuncCWCCWData[2][8];      // 
	BYTE        FuncCWData[8];      // 
	BYTE        FuncCCWData[8];     // 
    BYTE        led_color_no;       //
    BYTE        led_color_flag;     //
    BYTE        led_RGB[3];         //
    BYTE        led_brightness_level;          //
	BYTE        reserved[2];   
} ST_FUNC_INFO;
typedef union
{
	BYTE byte[24];
	ST_FUNC_INFO func_info;
} UN_FUNC_INFO;

// �G���R�[�_�[�X�N���v�g�@�\�ݒ���
typedef struct
{
	BYTE        rec_num;            // 
	BYTE        loop_flag;          // 
	BYTE        reserved1[14];
	BYTE        encoder_script[32];      // 
} ST_ENCODER_SCRIPT_INFO;
typedef union
{
	BYTE byte[48];
	ST_ENCODER_SCRIPT_INFO encoder_script_info;
} UN_ENCODER_SCRIPT_INFO;

// SW�@�\�ݒ���
typedef struct
{
	BYTE        SWFuncData[8];      // 
} ST_SW_FUNC_INFO;
typedef union
{
	BYTE byte[8];
	ST_SW_FUNC_INFO sw_func_info;
} UN_SW_FUNC_INFO;

// Flash �X�N���v�g���
typedef struct
{
	WORD_VAL		Check_SUM;			// �`�F�b�N�T���l
	BYTE			Rec_Num;			// �L�^�X�N���v�g��
	BYTE			reserve;			// �\��
	DWORD_VAL		Script_Total_Size;	// �S�X�N���v�g�f�[�^�T�C�Y
} ST_SCRIPT_HEAD;
typedef union
{
	BYTE byte[8];
	ST_SCRIPT_HEAD Script_Head;
} UN_SCRIPT_HEAD;

typedef struct
{
	DWORD_VAL		Script_Adress;		// �X�N���v�g�f�[�^�ۑ��A�h���X
	DWORD_VAL		Script_Size;		// �X�N���v�g�f�[�^�T�C�Y
	BYTE			Mode;				// ���[�h
	BYTE			Script_Name_Size;	// �X�N���v�g���̃T�C�Y
} ST_SCRIPT_INFO;
typedef union
{
	BYTE byte[10];
	ST_SCRIPT_INFO Script_Info;
} UN_SCRIPT_INFO;	

#if 0
#define BUTTON_FUNC_NUM					11
#define BUTTON_FUNC_BUTTON_1_IDX		0
#define BUTTON_FUNC_BUTTON_2_IDX		1
#define BUTTON_FUNC_BUTTON_3_IDX		2
#define BUTTON_FUNC_BUTTON_4_IDX		3
#define BUTTON_FUNC_BUTTON_5_IDX		4
#define BUTTON_FUNC_BUTTON_6_IDX		5
#define BUTTON_FUNC_BUTTON_7_IDX		6
#define BUTTON_FUNC_BUTTON_8_IDX		7
#define BUTTON_FUNC_BUTTON_9_IDX		8
#define BUTTON_FUNC_BUTTON_10_IDX		9
#define BUTTON_FUNC_BUTTON_11_IDX		10
#define BUTTON_FUNC_DEFAULT				0x00
#define BUTTON_FUNC_DISABLED			0x01
#define BUTTON_FUNC_SCRIPT				0x02
typedef struct
{
	BYTE			Func_No;			// �ݒ�
	BYTE			Script_No;			// �X�N���v�gNo.
} ST_MOUSE_MODE_INFO_BASE;
typedef struct
{
	ST_MOUSE_MODE_INFO_BASE			Button[BUTTON_FUNC_NUM];			// �e��{�^���ݒ�
} ST_MOUSE_MODE_INFO;
typedef union
{
	BYTE byte[BUTTON_FUNC_NUM*2];
	ST_MOUSE_MODE_INFO Mouse_Mode_Info;
} UN_MOUSE_MODE_INFO;
#endif

/** DECLARATIONS ***************************************************/
#define SW_1_NO                         PORTBbits.RB13
#define SW_2_NO                         PORTAbits.RA7
#define SW_3_NO                         PORTBbits.RB14
#define SW_4_NO                         PORTBbits.RB3
#define SW_5_NO                         PORTCbits.RC0
#define SW_6_NO                         PORTCbits.RC5
#define SW_7_NO                         PORTBbits.RB5
#define SW_8_NO                         PORTBbits.RB7
#define SW_9_NO                         PORTBbits.RB8
#define SW_10_NO                        PORTBbits.RB9
#define SW_11_NO                        PORTAbits.RA0
#define SW_ALL_NUM			13		// SW+Encoder���͑���
#define SW_NUM				11		// SW���͐�
#define SW_IDX_MIN          0       // SW �C���f�b�N�X�ŏ��l
#define SW_IDX_MAX          10      // SW �C���f�b�N�X�ő�l
#define ENCODER_NUM         2		// Encoder���͐�
#define ENCODER_IDX_MIN     11      // Encoder �C���f�b�N�X�ŏ��l
#define ENCODER_IDX_MAX     12      // Encoder �C���f�b�N�X�ő�l
#define	SW_1_NO_IDX			0
#define	SW_2_NO_IDX			1
#define	SW_3_NO_IDX			2
#define	SW_4_NO_IDX			3
#define	SW_5_NO_IDX			4
#define	SW_6_NO_IDX			5
#define	SW_7_NO_IDX			6
#define	SW_8_NO_IDX			7
#define	SW_9_NO_IDX			8
#define	SW_10_NO_IDX		9
#define	SW_11_NO_IDX		10
#define	SW_ENCORDER_A_IDX	11
#define	SW_ENCORDER_B_IDX	12
#define SW_ON_DEBOUNCE_TIME		20		// SW ON�f�o�E���X�^�C��
#define SW_OFF_DEBOUNCE_TIME	20		// SW OFF�f�o�E���X�^�C��

#define	ENCORDER_A	PORTCbits.RC2
#define	ENCORDER_B	PORTCbits.RC1
#define ENCORDER_INPUT_COUNT        50      // �G���R�[�_�[�`���^�����O�h�~�@������͂���v�œ��͏��������邩
#define ENCORDER_INPUT_PLUS_COUNT   2       // �G���R�[�_�[���p���X�łP����͂Ƃ��邩
#define MOVE_OFF	0
#define MOVE_ON		1

#define MODE_NUM                3       // ���[�h��
#define MODE1_NO                0       // ���[�h1 No.
#define MODE2_NO                1       // ���[�h2 No.
#define MODE3_NO                2       // ���[�h3 No.
#define MODE_FUNCTION_NUM       4       // �e���[�h�̋@�\��
#define MODE_FUNCTION1_NO       0       // �@�\1 No.
#define MODE_FUNCTION2_NO       1       // �@�\2 No.
#define MODE_FUNCTION3_NO       2       // �@�\3 No.
#define MODE_FUNCTION4_NO       3       // �@�\4 No.

#define ENCODER_SCRIPT_NUM              3       // �G���R�[�_�[�X�N���v�g�ݒ萔
#define ENCODER_SCRIPT_SCRIPT_REC_MAX   32      // �G���R�[�_�[�X�N���v�g�@�P������̃X�N���v�g�ݒ�ő吔
#define ENCODER_SCRIPT_LOOP_SET_NUM     2       // �G���R�[�_�[�X�N���v�g Loop�ݒ萔
#define ENCODER_SCRIPT_LOOP_SET_NONE    0       // �G���R�[�_�[�X�N���v�g Loot�ݒ� Loop�Ȃ�
#define ENCODER_SCRIPT_LOOP_SET_LOOP    1       // �G���R�[�_�[�X�N���v�g Loot�ݒ� Loop����

//#define LED1_ONOFF_INTERVAL     1000        // LED1�̓_�Ŏ���[ms] ���̎��ԊԊu��ON/OFF���J��Ԃ�
//#define LED1_ON_TIME            100
#if false   // Timer2 0.05ms
#define LED1_BLINK_ON_TIME      2000            // ON TIME 100ms : 100ms / 0.05ms(��Timer2������) = 2000
#define LED1_BLINK_OFF_TIME     2000            // OFF TIME 100ms : 100ms / 0.05ms(��Timer2������) = 2000
#endif
#if true   // Timer2 0.07ms
#define LED1_BLINK_ON_TIME      1429            // ON TIME 100ms : 100ms / 0.07ms(��Timer2������) = 1429
#define LED1_BLINK_OFF_TIME     1429            // OFF TIME 100ms : 100ms / 0.07ms(��Timer2������) = 1429
#endif
//#define LED2_ON_TIME            10
//#define LED2_ON_TIME_CYCLE      20
#define LED_NUM                 1   // LED NUM
#define LED_DATA_NUM            3   // LED data num (LED NUM * 3RGB)
#define LED_DUTY_MAX            100
#define LED_OFF                 0
#define LED_ON                  1
#define LED1_RED                LATAbits.LATA1
#define LED1_RED_ON()           LATAbits.LATA1=1
#define LED1_RED_OFF()          LATAbits.LATA1=0
#define LED1_GREEN              LATAbits.LATA10
#define LED1_GREEN_ON()         LATAbits.LATA10=1
#define LED1_GREEN_OFF()        LATAbits.LATA10=0
#define LED1_BLUE               LATBbits.LATB2
#define LED1_BLUE_ON()          LATBbits.LATB2=1
#define LED1_BLUE_OFF()         LATBbits.LATB2=0
//#define LED2_RED                LATAbits.LATA8
//#define LED2_RED_ON()           LATAbits.LATA8=1
//#define LED2_RED_OFF()          LATAbits.LATA8=0
//#define LED2_GREEN              LATCbits.LATC4
//#define LED2_GREEN_ON()         LATCbits.LATC4=1
//#define LED2_GREEN_OFF()        LATCbits.LATC4=0
//#define LED2_BLUE               LATBbits.LATB4
//#define LED2_BLUE_ON()          LATBbits.LATB4=1
//#define LED2_BLUE_OFF()         LATBbits.LATB4=0
// LED�ݒ�l
#define LED_COLOR_TYPE_NUM          9       // LED �J���[�ݒ萔
#define LED_COLOR_TYPE_OFF          0       // LED�J���[ ����
#define LED_COLOR_TYPE_WHITE        1       // LED�J���[ �z���C�g
#define LED_COLOR_TYPE_RED          2       // LED�J���[ ���b�h
#define LED_COLOR_TYPE_ORANGE       3       // LED�J���[ �I�����W
#define LED_COLOR_TYPE_YELLOW       4       // LED�J���[ �C�G���[
#define LED_COLOR_TYPE_TURQUOISE    5       // LED�J���[ �^�[�R�C�Y
#define LED_COLOR_TYPE_GREEN        6       // LED�J���[ �O���[��
#define LED_COLOR_TYPE_BLUE         7       // LED�J���[ �u���[
#define LED_COLOR_TYPE_PURPLE       8       // LED�J���[ �p�[�v��

// LED�P�x�ݒ�
#define LED_BRIGHTNESS_LEVEL_NUM        3   // LED�P�x�ݒ萔
#define LED_BRIGHTNESS_LEVEL_NORMAL     0   // LED�P�x�ݒ� �ʏ�
#define LED_BRIGHTNESS_LEVEL_DARK       1   // LED�P�x�ݒ� �Â�
#define LED_BRIGHTNESS_LEVEL_LIGHT      2   // LED�P�x�ݒ� ���邢

#define LED_SLEEP_ENABLED           0       // LED�X���[�v�ݒ�@�L��
#define LED_SLEEP_DISABLED          1       // LED�X���[�v�ݒ�@����
#define LED_LIGHT_ON_TYPE_MODE      0
#define LED_LIGHT_ON_TYPE_FUNC      1
#define LED_LIGHT_TYPE_MODE_MIN     0
#define LED_LIGHT_TYPE_MODE_MAX     1
#define LED_LIGHT_TYPE_MODE_ON      0       // LED�_���^�C�v�@���[�h�@�펞ON
#define LED_LIGHT_TYPE_MODE_OFF     1       // LED�_���^�C�v�@���[�h�@����
#define LED_LIGHT_TYPE_FUNC_MIN     0
#define LED_LIGHT_TYPE_FUNC_MAX     2
#define LED_LIGHT_TYPE_FUNC_ON      0       // LED�_���^�C�v�@�{�^���@���̕ύX�܂�ON
#define LED_LIGHT_TYPE_FUNC_SLOW    1       // LED�_���^�C�v�@�{�^���@����������
#define LED_LIGHT_TYPE_FUNC_FLASH   2       // LED�_���^�C�v�@�{�^���@�t���b�V�����ď���
#define LED_LIGHT_MODE_OFF_MAX_TIME     180 // LED�������ԁ@�ݒ�ő厞��
#define LED_LIGHT_MODE_OFF_DEFAULT_TIME 60  // LED�������ԁ@�f�t�H���g����
#if false   // Timer2 0.05ms
#define LED_LIGHT_MODE_OFF_TIME_MUL     20000   // LED�������ԁ@�������ԃJ�E���g�v�Z�p �����ݒ莞��[�b]��TIMER2�̃J�E���g�l�ɕϊ�����v�Z�l 1000 / 0.05ms(��Timer2������) = 20000
#define LED_LIGHT_TYPE_FUNC_SLOW_ON_TIME        10000 // LED�_���^�C�v�@�{�^���@������������ON���� 500ms : 500ms / 0.05ms(��Timer2������) = 10000
#define LED_LIGHT_TYPE_FUNC_SLOW_FADE_OUT_TIME  20000    // LED�_���^�C�v�@�{�^���@�����������̃t�F�[�h�A�E�g���� 1000ms : 1000ms / 0.05ms(��Timer2������) = 20000
#define LED_LIGHT_TYPE_FUNC_FLASH_TIME          3       // LED�_���^�C�v�@�{�^���@�t���b�V�����ď����̃t���b�V����
#define LED_LIGHT_TYPE_FUNC_FLASH_ON_TIME       2000       // LED�_���^�C�v�@�{�^���@�t���b�V�����ď����̃t���b�V��ON���� 100ms : 100ms / 0.05ms(��Timer2������) = 2000
#define LED_LIGHT_TYPE_FUNC_FLASH_OFF_TIME      2000       // LED�_���^�C�v�@�{�^���@�t���b�V�����ď����̃t���b�V��ON���� 100ms : 100ms / 0.05ms(��Timer2������) = 2000
#endif
#if true   // Timer2 0.07ms
#define LED_LIGHT_MODE_OFF_TIME_MUL     14286   // LED�������ԁ@�������ԃJ�E���g�v�Z�p �����ݒ莞��[�b]��TIMER2�̃J�E���g�l�ɕϊ�����v�Z�l 1000 / 0.07ms(��Timer2������) = 14286
#define LED_LIGHT_TYPE_FUNC_SLOW_ON_TIME        7143 // LED�_���^�C�v�@�{�^���@������������ON���� 500ms : 500ms / 0.07ms(��Timer2������) = 7143
#define LED_LIGHT_TYPE_FUNC_SLOW_FADE_OUT_TIME  14285    // LED�_���^�C�v�@�{�^���@�����������̃t�F�[�h�A�E�g���� 1000ms : 1000ms / 0.07ms(��Timer2������) = 14286
#define LED_LIGHT_TYPE_FUNC_FLASH_TIME          3       // LED�_���^�C�v�@�{�^���@�t���b�V�����ď����̃t���b�V����
#define LED_LIGHT_TYPE_FUNC_FLASH_ON_TIME       1429       // LED�_���^�C�v�@�{�^���@�t���b�V�����ď����̃t���b�V��ON���� 100ms : 100ms / 0.07ms(��Timer2������) = 1429
#define LED_LIGHT_TYPE_FUNC_FLASH_OFF_TIME      1429       // LED�_���^�C�v�@�{�^���@�t���b�V�����ď����̃t���b�V��ON���� 100ms : 100ms / 0.07ms(��Timer2������) = 1429
#endif
#define ENCODER_TYPEMATIC_TYPE_MIN     0
#define ENCODER_TYPEMATIC_TYPE_MAX     1
#define ENCODER_TYPEMATIC_TYPE_HIT_REPEAT      0    // �G���R�[�_�[�^�C�v�}�e�B�b�N�@�A��
#define ENCODER_TYPEMATIC_TYPE_PRESS_LEAVE     1    // �G���R�[�_�[�^�C�v�}�e�B�b�N�@�������ςȂ�


#define RX_DATA_BUFFER_ADDRESS
#define TX_DATA_BUFFER_ADDRESS

#define RX_DATA_BUFFER_SIZE     64
#define TX_DATA_BUFFER_SIZE     64
#define MOUSE_BUFF_SIZE         4
#define JOYSTICK_BUFF_SIZE      7
#define MULTIMEDIA_BUFF_SIZE    3
//#define KEYBOARD_BUFF_SIZE      8
#define KEYBOARD_BUFF_SIZE      9

#define KEYBOARD_REPORT_ID_IDX      0		// �L�[�{�[�h�o�́@REPORT ID�i�[�ʒu
#define KEYBOARD_REPORT_ID          1
#define MULTIMEDIA_REPORT_ID        2

#define KEYBOARD_MODIFIER_IDX			1		// �L�[�{�[�h�o�́@Modifier�i�[�ʒu
#define KEYBOARD_KEYCODE_TOP			3		// �L�[�{�[�h�o�́@Keycode�i�[�擪�ʒu
//#define KEYBOARD_MODIFIER_IDX			0		// �L�[�{�[�h�o�́@Modifier�i�[�ʒu
//#define KEYBOARD_KEYCODE_TOP			2		// �L�[�{�[�h�o�́@Keycode�i�[�擪�ʒu
#define KEYBOARD_KEYCODE_L_CTRL			0xE0	// USB�L�[�{�[�h�@��Ctrl
#define KEYBOARD_KEYCODE_L_SHIFT		0xE1	// USB�L�[�{�[�h�@��Shift
#define KEYBOARD_KEYCODE_L_ALT			0xE2	// USB�L�[�{�[�h�@��Alt
#define KEYBOARD_KEYCODE_L_GUI			0xE3	// USB�L�[�{�[�h�@��GUI
#define KEYBOARD_KEYCODE_R_CTRL			0xE4	// USB�L�[�{�[�h�@�ECtrl
#define KEYBOARD_KEYCODE_R_SHIFT		0xE5	// USB�L�[�{�[�h�@�EShift
#define KEYBOARD_KEYCODE_R_ALT			0xE6	// USB�L�[�{�[�h�@�EAlt
#define KEYBOARD_KEYCODE_R_GUI			0xE7	// USB�L�[�{�[�h�@�EGUI
#define MULTIMEDIA_DATA_TOP             1       // �}���`���f�B�A�f�[�^�i�[�擪�ʒu
#define MULTIMEDIA_DATA_DATA1           1       // �}���`���f�B�A�f�[�^1�i�[�ʒu
#define MULTIMEDIA_DATA_DATA2           2       // �}���`���f�B�A�f�[�^2�i�[�ʒu


#define SET_DATA_LEN				8			// �ݒ�f�[�^��

#define SET_TYPE_VAL_MIN			0			// �ݒ�^�C�v�ŏ��l
#define SET_TYPE_VAL_MAX			44			// �ݒ�^�C�v�ő�l
#define SET_TYPE_NONE				0			// �ݒ�^�C�v�@�Ȃ�
#define SET_TYPE_MOUSE_LCLICK		1			// �ݒ�^�C�v�@�}�E�X�@���N���b�N
#define SET_TYPE_MOUSE_RCLICK		2			// �ݒ�^�C�v�@�}�E�X�@�E�N���b�N
#define SET_TYPE_MOUSE_WHCLICK		3			// �ݒ�^�C�v�@�}�E�X�@�z�C�[���N���b�N
#define SET_TYPE_MOUSE_B4CLICK		4			// �ݒ�^�C�v�@�}�E�X�@�{�^��4�N���b�N
#define SET_TYPE_MOUSE_B5CLICK		5			// �ݒ�^�C�v�@�}�E�X�@�{�^��5�N���b�N
#define SET_TYPE_MOUSE_DCLICK		6			// �ݒ�^�C�v�@�}�E�X�@W�N���b�N
#define SET_TYPE_MOUSE_MOVE			7			// �ݒ�^�C�v�@�}�E�X�@�㉺���E�ړ�
#define SET_TYPE_MOUSE_WHSCROLL		8			// �ݒ�^�C�v�@�}�E�X�@�z�C�[���X�N���[��
#define SET_TYPE_KEYBOARD_KEY		9			// �ݒ�^�C�v�@�L�[�{�[�h�@�L�[
#define SET_TYPE_MULTIMEDIA_PLAY    10          // �ݒ�^�C�v�@�}���`���f�B�A�@�Đ�
#define SET_TYPE_MULTIMEDIA_PAUSE   11          // �ݒ�^�C�v�@�}���`���f�B�A�@�ꎞ��~
#define SET_TYPE_MULTIMEDIA_STOP    12          // �ݒ�^�C�v�@�}���`���f�B�A�@��~
#define SET_TYPE_MULTIMEDIA_REC     13          // �ݒ�^�C�v�@�}���`���f�B�A�@REC
#define SET_TYPE_MULTIMEDIA_FORWARD 14          // �ݒ�^�C�v�@�}���`���f�B�A�@������
#define SET_TYPE_MULTIMEDIA_REWIND  15          // �ݒ�^�C�v�@�}���`���f�B�A�@���߂�
#define SET_TYPE_MULTIMEDIA_NEXT    16          // �ݒ�^�C�v�@�}���`���f�B�A�@��
#define SET_TYPE_MULTIMEDIA_PREVIOUS    17      // �ݒ�^�C�v�@�}���`���f�B�A�@�O
#define SET_TYPE_MULTIMEDIA_MUTE    18          // �ݒ�^�C�v�@�}���`���f�B�A�@����
#define SET_TYPE_MULTIMEDIA_VOLUMEUP    19      // �ݒ�^�C�v�@�}���`���f�B�A�@�{�����[���A�b�v
#define SET_TYPE_MULTIMEDIA_VOLUMEDOWN  20      // �ݒ�^�C�v�@�}���`���f�B�A�@�{�����[���_�E��
#define SET_TYPE_JOYPAD_XY			21			// �ݒ�^�C�v�@�W���C�p�b�h�@X-Y��
#define SET_TYPE_JOYPAD_ZRZ			22			// �ݒ�^�C�v�@�W���C�p�b�h�@Z-Rz��
#define SET_TYPE_JOYPAD_B01			23			// �ݒ�^�C�v�@�W���C�p�b�h�@�{�^��1
#define SET_TYPE_JOYPAD_B02			24			// �ݒ�^�C�v�@�W���C�p�b�h�@�{�^��2
#define SET_TYPE_JOYPAD_B03			25			// �ݒ�^�C�v�@�W���C�p�b�h�@�{�^��3
#define SET_TYPE_JOYPAD_B04			26			// �ݒ�^�C�v�@�W���C�p�b�h�@�{�^��4
#define SET_TYPE_JOYPAD_B05			27			// �ݒ�^�C�v�@�W���C�p�b�h�@�{�^��5
#define SET_TYPE_JOYPAD_B06			28			// �ݒ�^�C�v�@�W���C�p�b�h�@�{�^��6
#define SET_TYPE_JOYPAD_B07			29			// �ݒ�^�C�v�@�W���C�p�b�h�@�{�^��7
#define SET_TYPE_JOYPAD_B08			30			// �ݒ�^�C�v�@�W���C�p�b�h�@�{�^��8
#define SET_TYPE_JOYPAD_B09			31			// �ݒ�^�C�v�@�W���C�p�b�h�@�{�^��9
#define SET_TYPE_JOYPAD_B10			32			// �ݒ�^�C�v�@�W���C�p�b�h�@�{�^��10
#define SET_TYPE_JOYPAD_B11			33			// �ݒ�^�C�v�@�W���C�p�b�h�@�{�^��11
#define SET_TYPE_JOYPAD_B12			34			// �ݒ�^�C�v�@�W���C�p�b�h�@�{�^��12
#define SET_TYPE_JOYPAD_B13			35			// �ݒ�^�C�v�@�W���C�p�b�h�@�{�^��13
#define SET_TYPE_JOYPAD_HSW_NORTH		36		// �ݒ�^�C�v�@�W���C�p�b�h�@�n�b�g�X�C�b�`�@�k
#define SET_TYPE_JOYPAD_HSW_SOUTH		37		// �ݒ�^�C�v�@�W���C�p�b�h�@�n�b�g�X�C�b�`�@��
#define SET_TYPE_JOYPAD_HSW_WEST		38		// �ݒ�^�C�v�@�W���C�p�b�h�@�n�b�g�X�C�b�`�@��
#define SET_TYPE_JOYPAD_HSW_EAST		39		// �ݒ�^�C�v�@�W���C�p�b�h�@�n�b�g�X�C�b�`�@��
#define SET_TYPE_NUMBER_UP          40          // �ݒ�^�C�v�@����@�����A������ UP
#define SET_TYPE_NUMBER_DOWN        41          // �ݒ�^�C�v�@����@�����A������ DOWN
#define SET_TYPE_ENCODER_SCRIPT1    42          // �ݒ�^�C�v�@�G���R�[�_�[�X�N���v�g1
#define SET_TYPE_ENCODER_SCRIPT2    43          // �ݒ�^�C�v�@�G���R�[�_�[�X�N���v�g2
#define SET_TYPE_ENCODER_SCRIPT3    44          // �ݒ�^�C�v�@�G���R�[�_�[�X�N���v�g3

#define DEVICE_DATA_SET_TYPE_IDX        0			// �f�o�C�X�ݒ�f�[�^ �ݒ�^�C�v�i�[�ʒu
#define DEVICE_DATA_CLICK_IDX           1			// �f�o�C�X�ݒ�f�[�^�@�}�E�X�f�[�^�@�N���b�N�f�[�^�i�[�ʒu
#define DEVICE_DATA_X_MOVE_IDX          2			// �f�o�C�X�ݒ�f�[�^�@�}�E�X�f�[�^�@X�ړ��ʊi�[�ʒu
#define DEVICE_DATA_Y_MOVE_IDX          3			// �f�o�C�X�ݒ�f�[�^�@�}�E�X�f�[�^�@Y�ړ��ʊi�[�ʒu
#define DEVICE_DATA_WHEEL_IDX           4			// �f�o�C�X�ݒ�f�[�^�@�}�E�X�f�[�^�@�z�C�[���X�N���[���ʊi�[�ʒu
#define DEVICE_DATA_MODIFIER_IDX        1			// �f�o�C�X�ݒ�f�[�^�@�L�[�{�[�h�f�[�^�@���f�B�t�@�C�f�[�^�i�[�ʒu
#define DEVICE_DATA_KEY1_IDX            2			// �f�o�C�X�ݒ�f�[�^�@�L�[�{�[�h�f�[�^�@�L�[�f�[�^1�i�[�ʒu
#define DEVICE_DATA_KEY2_IDX            3			// �f�o�C�X�ݒ�f�[�^�@�L�[�{�[�h�f�[�^�@�L�[�f�[�^2�i�[�ʒu
#define DEVICE_DATA_KEY3_IDX            4			// �f�o�C�X�ݒ�f�[�^�@�L�[�{�[�h�f�[�^�@�L�[�f�[�^3�i�[�ʒu
#define DEVICE_DATA_MULTIMEDIA_VAL1_IDX 1			// �f�o�C�X�ݒ�f�[�^�@�}���`���f�B�A�@�f�[�^1�i�[�ʒu
#define DEVICE_DATA_MULTIMEDIA_VAL2_IDX 2			// �f�o�C�X�ݒ�f�[�^�@�}���`���f�B�A�@�f�[�^2�i�[�ʒu
#define DEVICE_DATA_JOY_BUTTON1_IDX     1			// �f�o�C�X�ݒ�f�[�^�@�W���C�p�b�h�f�[�^�@�{�^��1�f�[�^�i�[�ʒu
#define DEVICE_DATA_JOY_BUTTON2_IDX     2			// �f�o�C�X�ݒ�f�[�^�@�W���C�p�b�h�f�[�^�@�{�^��2�f�[�^�i�[�ʒu
#define DEVICE_DATA_JOY_HAT_SW_IDX      3			// �f�o�C�X�ݒ�f�[�^�@�W���C�p�b�h�f�[�^�@HAT SW�i�[�ʒu
#define DEVICE_DATA_JOY_X_MOVE_IDX      4			// �f�o�C�X�ݒ�f�[�^�@�W���C�p�b�h�f�[�^�@X���ړ��ʊi�[�ʒu
#define DEVICE_DATA_JOY_Y_MOVE_IDX      5			// �f�o�C�X�ݒ�f�[�^�@�W���C�p�b�h�f�[�^�@Y���ړ��ʊi�[�ʒu
#define DEVICE_DATA_SENSE_IDX           7           // �f�o�C�X�ݒ�f�[�^�@���[�^���G���R�[�_�̊��x�����i�[�ʒu

#define USB_MOUSE_BUTTON_IDX_TOP        0
#define USB_MOUSE_MOVE_IDX_TOP          1
#define USB_MOUSE_MOVE_X_IDX            1
#define USB_MOUSE_MOVE_Y_IDX            2
#define USB_MOUSE_MOVE_W_IDX            3
#define USB_JOYSTICK_BUTTON_IDX_TOP     0
#define USB_JOYSTICK_BUTTON_IDX_SIZE	2
#define USB_JOYSTICK_HATSW_IDX          2
#define USB_JOYSTICK_LEVER_IDX_TOP      3
#define USB_JOYSTICK_LEVER_IDX_SIZE 	4
#define USB_JOYSTICK_LEVER_L_IDX_TOP    3
#define USB_JOYSTICK_LEVER_L_IDX_SIZE 	2
#define USB_JOYSTICK_LEVER_R_IDX_TOP    5
#define USB_JOYSTICK_LEVER_R_IDX_SIZE 	2

#define USB_JOYSTICK_BUTTON_ID_MIN	0
#define USB_JOYSTICK_BUTTON_ID_MAX	12
#define USB_JOYSTICK_BUTTON_ID_01	0
#define USB_JOYSTICK_BUTTON_ID_02	1
#define USB_JOYSTICK_BUTTON_ID_03	2
#define USB_JOYSTICK_BUTTON_ID_04	3
#define USB_JOYSTICK_BUTTON_ID_05	4
#define USB_JOYSTICK_BUTTON_ID_06	5
#define USB_JOYSTICK_BUTTON_ID_07	6
#define USB_JOYSTICK_BUTTON_ID_08	7
#define USB_JOYSTICK_BUTTON_ID_09	8
#define USB_JOYSTICK_BUTTON_ID_10	9
#define USB_JOYSTICK_BUTTON_ID_11	10
#define USB_JOYSTICK_BUTTON_ID_12	11
#define USB_JOYSTICK_BUTTON_ID_13	12

#define USB_JOYSTICK_LEVER_CENTER   0x80

#define HAT_SWITCH_PRESS_MASK       0x07
#define HAT_SWITCH_NORTH            0x00
#define HAT_SWITCH_NORTH_EAST       0x01
#define HAT_SWITCH_EAST             0x02
#define HAT_SWITCH_SOUTH_EAST       0x03
#define HAT_SWITCH_SOUTH            0x04
#define HAT_SWITCH_SOUTH_WEST       0x05
#define HAT_SWITCH_WEST             0x06
#define HAT_SWITCH_NORTH_WEST       0x07
#define HAT_SWITCH_NULL             0x08


#define DEVICE_TYPE_MIN         0
#define DEVICE_TYPE_MAX         4
#define DEVICE_TYPE_NONE        0
#define DEVICE_TYPE_MOUSE       1
#define DEVICE_TYPE_KEYBOARD    2
#define DEVICE_TYPE_JOYSTICK    3
#define DEVICE_TYPE_MULTIMEDIA  4

#define MOUSE_DATA_LEFT_CLICK		0x01		// �}�E�X�f�[�^�@���N���b�N
#define MOUSE_DATA_RIGHT_CLICK		0x02		// �}�E�X�f�[�^�@�E�N���b�N
#define MOUSE_DATA_WHEEL_CLICK		0x04		// �}�E�X�f�[�^�@�z�C�[���N���b�N
#define MOUSE_DATA_B4_CLICK         0x08		// �}�E�X�f�[�^�@�{�^��4�N���b�N
#define MOUSE_DATA_B5_CLICK         0x10		// �}�E�X�f�[�^�@�{�^��5�N���b�N

#define MOUSE_DOUBLE_CLICK_STATUS_NONE		0x00
#define MOUSE_DOUBLE_CLICK_STATUS_CLICK1	0x01
#define MOUSE_DOUBLE_CLICK_STATUS_INTERVAL	0x02
#define MOUSE_DOUBLE_CLICK_STATUS_CLICK2	0x04
#define MOUSE_DOUBLE_CLICK_INTERVAL	100			// �}�E�X�_�u���N���b�N�̃N���b�N�Ԋu

#define MASTER_MOUSE_SPEED		50	//	Mouse�̈ړ����x�̒����l
#define MASTER_WHEEL_SPEED		1000


// �}���`���f�B�A�@�Z�b�g�@�^�C�v
#define MULTIMEDIA_TYPE_PLAY                0x01		// Play
#define MULTIMEDIA_TYPE_PAUSE               0x02		// Pause
#define MULTIMEDIA_TYPE_STOP                0x03		// Stop
#define MULTIMEDIA_TYPE_REC                 0x04		// Record
#define MULTIMEDIA_TYPE_FORWARD             0x05		// Forward
#define MULTIMEDIA_TYPE_REWIND              0x06		// Rewind
#define MULTIMEDIA_TYPE_NEXT                0x07		// Next
#define MULTIMEDIA_TYPE_PREVIOUS            0x08		// Previous
#define MULTIMEDIA_TYPE_VOLUME_MUTE			0x09		// �{�����[���~���[�g
#define MULTIMEDIA_TYPE_VOLUME_UP			0x0A		// �{�����[���A�b�v
#define MULTIMEDIA_TYPE_VOLUME_DOWN			0x0B		// �{�����[���_�E��
//#define MULTIMEDIA_TYPE_PLAYPAUSE           0x0C		// Play/Pause

// �}���`���f�B�A�@�Z�b�g�@�r�b�g�f�[�^
// Data0
#define MULTIMEDIA_DATA_PLAY                0x01		// Play
#define MULTIMEDIA_DATA_PAUSE               0x02		// Pause
#define MULTIMEDIA_DATA_REC                 0x04		// Record
#define MULTIMEDIA_DATA_FORWARD             0x08		// Forward
#define MULTIMEDIA_DATA_REWIND              0x10		// Rewind
#define MULTIMEDIA_DATA_NEXT                0x20		// Next
#define MULTIMEDIA_DATA_PREVIOUS            0x40		// Previous
#define MULTIMEDIA_DATA_STOP                0x80		// Stop
// Data1
#define MULTIMEDIA_DATA_PLAYPAUSE           0x01		// Play/Pause
#define MULTIMEDIA_DATA_VOLUME_MUTE			0x02		// �{�����[���~���[�g
#define MULTIMEDIA_DATA_VOLUME_UP			0x04		// �{�����[���A�b�v
#define MULTIMEDIA_DATA_VOLUME_DOWN			0x08		// �{�����[���_�E��
// �i�[�ʒu
#define USB_MULTIMEDIA_DATA_PLAY_IDX                0x01		// Play
#define USB_MULTIMEDIA_DATA_PAUSE_IDX               0x01		// Pause
#define USB_MULTIMEDIA_DATA_REC_IDX                 0x01		// Record
#define USB_MULTIMEDIA_DATA_FORWARD_IDX             0x01		// Forward
#define USB_MULTIMEDIA_DATA_REWIND_IDX              0x01		// Rewind
#define USB_MULTIMEDIA_DATA_NEXT_IDX                0x01		// Next
#define USB_MULTIMEDIA_DATA_PREVIOUS_IDX            0x01		// Previous
#define USB_MULTIMEDIA_DATA_STOP_IDX                0x01		// Stop
#define USB_MULTIMEDIA_DATA_PLAYPAUSE_IDX           0x02		// Play/Pause
#define USB_MULTIMEDIA_DATA_VOLUME_MUTE_IDX			0x02		// �{�����[���~���[�g
#define USB_MULTIMEDIA_DATA_VOLUME_UP_IDX			0x02		// �{�����[���A�b�v
#define USB_ULTIMEDIA_DATA_VOLUME_DOWN_IDX			0x02		// �{�����[���_�E��

#define MASTER_INPUT_SENSE_MAX      100         // �G���R�[�_�̊��x�����ő�l
#define MASTER_INPUT_SENSE_DEFAULT  50          // �G���R�[�_�̊��x�����f�t�H���g�l
#define ENCODER_KEYBORD_OUTPUT_TIME_PRESS   100 // �G���R�[�_�[�ł̃L�[�{�[�h�o�͎���[ms] �������ςȂ��p
#define ENCODER_KEYBORD_OUTPUT_TIME_HIT     1   // �G���R�[�_�[�ł̃L�[�{�[�h�o�͎���[ms] �A�ŗp
#define NUMBER_INPUT_USB_CODE_MIN   0x1E        // ���l���͎��̍ŏ��l�O��USB�R�[�h
#define NUMBER_INPUT_USB_CODE_MAX   0x27        // ���l���͎��̍ŏ��l�O��USB�R�[�h
#define DIAL_FUNC_TEMP_CHANGE_ENCODER_DISABLED_TIME 250    // �G���R�[�_�[SW�ŋ@�\���ꎞ�ύX��ɃG���R�[�_�[���͂𖳌��Ƃ��鎞�ԁi�G���R�[�_�[SW�ŋ@�\���ꎞ�ύX��ɒʏ�@�\�����͂���Ă��܂��΍�j

#if 0
#define MODE_MOUSE                      0
#define MODE_KEYBOARD                   1
#define	MODE_JOYSTICK                   2
#define	EEPROM_DATA_MODE		0	//	0:���[�h
#define	EEPROM_DATA_VALUE		1	//	1:�l
#define	EEPROM_DATA_MODIFIER            2	//	2:Modifier�i�L�[�{�[�h�p�j

#define MASTER_MOUSE_SPEED		50	//	Mouse�̈ړ����x�̒����l
#define MASTER_WHEEL_SPEED		1000

#define MOVE_OFF                        0
#define MOVE_ON                         1
#endif


// Script
//#define SCRIPT_NUM                  30    // �X�N���v�g�� 30(sector) (1sector 1script)
//#define SCRIPT_MAX_NUM              120     // �X�N���v�g�ݒ�ő吔 31(sector) * 4(1sector������) = 124 ������悭���� 120
#define SCRIPT_MAX_NUM              200     // �X�N���v�g�ݒ�ő吔 31(sector) * 7(1sector������) = 217 ������悭���� 200
#define SCRIPT_NAME_SIZE            0xFF	// �X�N���v�g���̃T�C�Y
#define SCRIPT_EXE_INFO_NUM         1
#define SCRIPT_EXE_MODE_ONE_TIME    0       // �X�N���v�g���s���[�h �P����s
#define SCRIPT_EXE_MODE_LOOP        1       // �X�N���v�g���s���[�h ���[�v���[�h
#define SCRIPT_EXE_MODE_FIRE        2       // �X�N���v�g���s���[�h �t�@�C���[���[�h
#define SCRIPT_EXE_MODE_HOLD        3       // �X�N���v�g���s���[�h �z�[���h���[�h
#define SCRIPT_EXE_MODE_MIN_VAL     0       // �X�N���v�g���s���[�h �ŏ��ݒ�l
#define SCRIPT_EXE_MODE_MAX_VAL     3       // �X�N���v�g���s���[�h �ő�ݒ�l
#define SW_MACRO_EXE_TYPE_TYPE1     0       // �}�N�����s�ݒ�@�^�C�v�P�@�����s
#define SW_MACRO_EXE_TYPE_TYPE2     1       // �}�N�����s�ݒ�@�^�C�v�Q�@�I������s
#define SW_MACRO_EXE_TYPE_TYPE3     2       // �}�N�����s�ݒ�@�^�C�v�R�@����
#define SW_MACRO_EXE_TYPE_MIN       0       // �}�N�����s�ݒ� �ŏ��ݒ�l
#define SW_MACRO_EXE_TYPE_MAX       2       // �}�N�����s�ݒ� �ő�ݒ�l

// SW Special Function
#define SW_SP_FUNC_NUM              13      // SW ����@�\��
#define SW_SP_FUNC_MODE_CHANGE_MIN  1       // SW ����@�\���[�h�ύXNo.�ŏ��l
#define SW_SP_FUNC_MODE_CHANGE_MAX  4       // SW ����@�\���[�h�ύXNo.�ő�l
#define SW_SP_FUNC_FUNC_CHANGE_MIN  5       // SW ����@�\�@�\�ύXNo.�ŏ��l
#define SW_SP_FUNC_FUNC_CHANGE_MAX  8       // SW ����@�\�@�\�ύXNo.�ő�l
#define SW_SP_FUNC_FUNC_CHANGE_TEMP_MIN  9       // SW ����@�\�@�\�ύX�i�ꎞ�jNo.�ŏ��l
#define SW_SP_FUNC_FUNC_CHANGE_TEMP_MAX  12      // SW ����@�\�@�\�ύX�i�ꎞ�jNo.�ő�l
#define SW_SP_FUNC_FUNC_CHANGE_MIN2 13      // SW ����@�\�@�\�ύXNo.�ŏ��l
#define SW_SP_FUNC_FUNC_CHANGE_MAX2 13      // SW ����@�\�@�\�ύXNo.�ő�l
#define SW_SP_FUNC_NONE             0       // SW ����@�\ �Ȃ�
#define SW_SP_FUNC_MODE             1       // SW ����@�\ Mode
#define SW_SP_FUNC_MODE1            2       // SW ����@�\ Mode1
#define SW_SP_FUNC_MODE2            3       // SW ����@�\ Mode2
#define SW_SP_FUNC_MODE3            4       // SW ����@�\ Mode3
#define SW_SP_FUNC_FUNC1            5       // SW ����@�\ �@�\1
#define SW_SP_FUNC_FUNC2            6       // SW ����@�\ �@�\2
#define SW_SP_FUNC_FUNC3            7       // SW ����@�\ �@�\3
#define SW_SP_FUNC_FUNC4            8       // SW ����@�\ �@�\4
#define SW_SP_FUNC_FUNC1_TEMP       9       // SW ����@�\ �@�\1(�����Ă����)
#define SW_SP_FUNC_FUNC2_TEMP       10      // SW ����@�\ �@�\2(�����Ă����)
#define SW_SP_FUNC_FUNC3_TEMP       11      // SW ����@�\ �@�\3(�����Ă����)
#define SW_SP_FUNC_FUNC4_TEMP       12      // SW ����@�\ �@�\4(�����Ă����)
#define SW_SP_FUNC_FUNC             13      // SW ����@�\ �@�\


// �����A�����͗p
#define NUMBER_INPUT_UPDOWN_FLAG_NONE   0   // �����A�����̓A�b�v�_�E���t���O�@���ݒ�
#define NUMBER_INPUT_UPDOWN_FLAG_UP     1   // �����A�����̓A�b�v�_�E���t���O�@UP
#define NUMBER_INPUT_UPDOWN_FLAG_DOWN   2   // �����A�����̓A�b�v�_�E���t���O�@DOWN
#define NUMBER_INPUT_USB_CODE_NUM       10  // �����A�����́@����USB�R�[�h��
//#define NUMBER_INPUT_MIN_VALUE          0   // �����A�����́@���͍ŏ��l
//#define NUMBER_INPUT_MAX_VALUE          9   // �����A�����́@���͍ő�l

#define N_ON    1		// normal ON
#define N_OFF   0		// normal OFF
#define R_ON    0		// reverse ON
#define R_OFF   1		// reverse OFF



//--------------------------------------------------
//�f�B���C�֐�
//--------------------------------------------------
// 50MHz = 1���� 0.02us
// 1us = 50���� = Delay10TCY(5);
// 5us = 250���� = Delay10TCYx(25);
// 10us = 500���� = Delay100TCYx(5);
// 50us = 2,500���� = Delay100TCYx(25);
// 100us = 5,000���� = Delay100TCYx(50);
// 1ms = 50,000���� = Delay10KTCYx(5);
// 10ms = 500,000���� = Delay10KTCYx(50);
// 100ms = 5,000,000���� = Delay10KTCYx(500)
#define DELAY_1us() Delay10TCYx(5)
#define DELAY_5us() Delay10TCYx(25)
#define DELAY_10us() Delay100TCYx(5)
#define DELAY_50us() Delay100TCYx(25)
#define DELAY_100us() Delay100TCYx(50)
#define DELAY_1ms() Delay10KTCYx(5)
#define DELAY_5ms() Delay10KTCYx(25)
#define DELAY_10ms() Delay10KTCYx(50)
#define DELAY_50ms() Delay10KTCYx(250)
#define DELAY_100ms() Delay10KTCYx(500)

extern void USBCBSendResume(void);

/*********************************************************
 * Extern symbols
 ********************************************************/


extern int8_t	c_version[];
extern BYTE sw_now_fix[SW_ALL_NUM];
extern BYTE sw_now_fix_pre[SW_ALL_NUM];
extern BYTE sw_press_on_cnt[SW_ALL_NUM];
extern BYTE sw_press_off_cnt[SW_ALL_NUM];
extern BYTE sw_output_flag[SW_ALL_NUM];
extern BYTE sw_output_multimedia_flag[SW_ALL_NUM];
extern BYTE encoder_key_out_flag[SW_ALL_NUM];
extern BYTE sw_func_out_flag[SW_ALL_NUM];
extern BYTE sw_output_mouse_first_flag[SW_ALL_NUM];
extern BYTE sw_output_joystick_first_flag[SW_ALL_NUM];
extern BYTE set_type_move_sw_no;
extern BYTE set_type_move_val[2];
extern BYTE set_type_ws_sw_no;
extern BYTE set_type_ws_val;
extern BYTE set_type_xy_sw_no;
extern BYTE set_type_xy_val[2];
extern BYTE set_type_zrz_sw_no;
extern BYTE set_type_zrz_val[2];
extern BYTE set_type_hat_sw_no;
extern BYTE set_type_hat_val;
//extern BYTE sw_on_time_measure_flag[SW_NUM];
//extern BYTE sw_on_time_measure_status[SW_NUM];
//extern WORD sw_on_time_measure_time[SW_NUM];
extern UN_BASE_HEAD my_base_head;
extern UN_BASE_INFO my_base_infos[MODE_NUM];
extern UN_FUNC_INFO my_func_infos[MODE_NUM][MODE_FUNCTION_NUM];
extern UN_ENCODER_SCRIPT_INFO my_encoder_script_infos[ENCODER_SCRIPT_NUM];
extern UN_SW_FUNC_INFO my_sw_func_infos[MODE_NUM][SW_NUM];

extern BYTE keyboard_output_stop_by_macro;

extern BYTE encoder_input_state;
extern BYTE encoder_input_state_pre;
extern BYTE encoder_input_cnt;
extern BYTE encoder_input_fix_pre;
extern BYTE encoder_input_puls_cw;
extern BYTE encoder_input_puls_ccw;
extern BYTE rotate_state;
extern WORD temp_input_sense_left;
extern WORD temp_input_sense_right;
extern BYTE encoder_script_exe_now_idx[ENCODER_SCRIPT_NUM];
extern WORD dial_function_enocder_disabled_time;

extern BYTE number_input_up_down_flag;
extern BYTE number_input_now_val;
extern BYTE number_input_usb_code[NUMBER_INPUT_USB_CODE_NUM];

extern BYTE mode_no_set;
extern BYTE mode_func_no_set;
extern BYTE mode_no_fix;
extern BYTE mode_func_no_fix;
extern BYTE mode_no_pre_led;
extern BYTE mode_func_no_pre_led;


extern BYTE led_output_brightness_level_fix;
extern BYTE led_output_fix[LED_DATA_NUM];
extern BYTE led_output_mode_fix[LED_DATA_NUM];
extern BYTE led_output_brightness_level_preview;
extern BYTE led_output_preview[LED_DATA_NUM];
extern WORD led_output_preview_time_counter;
extern BYTE led_output_duty_count;
extern BYTE led1_blink_flag;
extern WORD led1_blink_counter;
extern BYTE led_change_flag_mode;
extern BYTE led_change_flag_func;
extern BYTE led_light_on_type;
extern BYTE led_light_func_type;
extern BYTE led_func_lighting_flag;
extern BYTE led_light_on_type_temp_func;
extern DWORD led_light_time_counter;
extern BYTE led_light_status;
extern int timer1_counter;
extern int timer2_counter;
//extern const BYTE led_color_data[LED_COLOR_TYPE_NUM][3];
#if 1
extern BYTE led_color_mode_default_data[MODE_NUM][3];
extern BYTE led_color_func_default_data[MODE_NUM * MODE_FUNCTION_NUM][3];
#else
extern const BYTE led_color_mode_default_data[MODE_NUM][3];
extern const BYTE led_color_func_default_data[MODE_NUM * MODE_FUNCTION_NUM][3];
#endif

extern BYTE mouse_w_click_status;
extern WORD mouse_w_click_interval_counter;

extern BYTE ReceivedDataBuffer[RX_DATA_BUFFER_SIZE] RX_DATA_BUFFER_ADDRESS;
extern BYTE ToSendDataBuffer[TX_DATA_BUFFER_SIZE] TX_DATA_BUFFER_ADDRESS;
extern BYTE mouse_input[MOUSE_BUFF_SIZE];
extern BYTE joystick_input[JOYSTICK_BUFF_SIZE];
//extern BYTE volume_input[VOLUME_BUFF_SIZE];
extern BYTE keyboard_input[KEYBOARD_BUFF_SIZE];
extern BYTE keyboard_output[HID_INT_OUT_EP_SIZE];
extern BYTE multimedia_input[MULTIMEDIA_BUFF_SIZE];
extern BYTE mouse_buffer[MOUSE_BUFF_SIZE];
extern BYTE mouse_buffer_pre[MOUSE_BUFF_SIZE];
extern BYTE joystick_buffer[JOYSTICK_BUFF_SIZE];
extern BYTE joystick_buffer_pre[JOYSTICK_BUFF_SIZE];
extern BYTE joystick_default_value[JOYSTICK_BUFF_SIZE];
//extern BYTE volume_buffer[VOLUME_BUFF_SIZE];
extern BYTE keyboard_buffer[KEYBOARD_BUFF_SIZE];
extern BYTE keyboard_buffer_pre[KEYBOARD_BUFF_SIZE];
extern BYTE multimedia_buffer[MULTIMEDIA_BUFF_SIZE];
extern BYTE multimedia_buffer_pre[MULTIMEDIA_BUFF_SIZE];
extern BYTE mouse_input_out_flag;
extern BYTE joystick_input_out_flag;
//extern BYTE volume_input_out_flag;
extern BYTE keyboard_input_out_flag;
extern BYTE multimedia_input_out_flag;


// FLASH
extern BYTE flash_read_req_flag;
extern BYTE flash_write_req_flag;

extern BYTE USB_Sleep_Flag;

extern USB_HANDLE USBOutHandle;
extern USB_HANDLE USBInHandle;
extern USB_HANDLE lastTransmission_Mouse;
extern USB_HANDLE lastTransmission_Joystick;
//extern USB_HANDLE lastTransmission_Volume;
extern USB_HANDLE lastINTransmission_Keyboard;
extern USB_HANDLE lastOUTTransmission_Keyboard;

// DEBUG
extern BYTE debug_arr1[16];
extern BYTE debug_arr2[16];
extern BYTE debug_arr3[16];
extern WORD debug_array_w1[16];


#endif	/* APP_H */

