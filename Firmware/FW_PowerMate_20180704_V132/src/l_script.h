#ifndef L_SCRIPT_H
#define L_SCRIPT_H

#include "app.h"
#include "l_flash.h"



// SCRIPT実行情報用　各種DEFINE
#define SCRIPT_EXE_DATA_BUFF_BANK_NUM	2		// スクリプトデータ読み込みバッファバンク数
#define SCRIPT_EXE_DATA_BUFF_SIZE		12		// スクリプトデータ読み込みバッファサイズ
#define SCRIPT_EXE_FLAG_NOEXE			0		// 実行フラグ　未実行
#define SCRIPT_EXE_FLAG_EXE				1		// 実行フラグ　実行
#define SCRIPT_EXE_CTRL_ONE_MODE		0		// スクリプト属性 1回実行
#define SCRIPT_EXE_CTRL_LOOP_MODE		1		// スクリプト属性 ループモード
#define SCRIPT_EXE_CTRL_FIRE_MODE		2		// スクリプト属性 ファイヤーモード
#define SCRIPT_EXE_CTRL_HOLD_MODE		3		// スクリプト属性 ホールドモード
#define SCRIPT_EXE_BUFF_BANK0_EXE		0		// 次コマンド実行バッファ バンク0
#define SCRIPT_EXE_BUFF_BANK1_EXE		1		// 次コマンド実行バッファ バンク1
#define SCRIPT_EXE_READ_NOREQ			0		// バッファ読み込み要求 未要求
#define SCRIPT_EXE_READ_REQ_BANK0		1		// バッファ読み込み要求 バンク0読み込み要求
#define SCRIPT_EXE_READ_COMP_BANK0		2		// バッファ読み込み要求 バンク0読み込み完了
#define SCRIPT_EXE_READ_REQ_BANK1		3		// バッファ読み込み要求 バンク1読み込み要求
#define SCRIPT_EXE_READ_COMP_BANK1		4		// バッファ読み込み要求 バンク1読み込み完了
#define SCRIPT_COMMAND_ID_WAIT			0x70	// スクリプトコマンドID WAIT
#define SCRIPT_COMMAND_ID_KEY_PRESS		0x41	// スクリプトコマンドID キープレス
#define SCRIPT_COMMAND_ID_KEY_RELESE	0x40	// スクリプトコマンドID キーリリース
#define SCRIPT_COMMAND_ID_MULTI_PRESS       0x43	// スクリプトコマンドID マルチプレス
#define SCRIPT_COMMAND_ID_MULTI_RELESE      0x42	// スクリプトコマンドID マルチリリース
#define SCRIPT_COMMAND_ID_MOUSE_PRESS_L		0x29	// スクリプトコマンドID マウスプレス左クリック
#define SCRIPT_COMMAND_ID_MOUSE_RELESE_L	0x21	// スクリプトコマンドID マウスリリース左クリック
#define SCRIPT_COMMAND_ID_MOUSE_PRESS_R		0x2A	// スクリプトコマンドID マウスプレス右クリック
#define SCRIPT_COMMAND_ID_MOUSE_RELESE_R	0x22	// スクリプトコマンドID マウスリリース右クリック
#define SCRIPT_COMMAND_ID_MOUSE_PRESS_W		0x2C	// スクリプトコマンドID マウスプレスホイールクリック
#define SCRIPT_COMMAND_ID_MOUSE_RELESE_W	0x24	// スクリプトコマンドID マウスリリースホイールクリック
#define SCRIPT_COMMAND_ID_MOUSE_PRESS_B4	0x2D	// スクリプトコマンドID マウスプレスボタン4クリック
#define SCRIPT_COMMAND_ID_MOUSE_RELESE_B4	0x25	// スクリプトコマンドID マウスリリースボタン4クリック
#define SCRIPT_COMMAND_ID_MOUSE_PRESS_B5	0x2E	// スクリプトコマンドID マウスプレスボタン5クリック
#define SCRIPT_COMMAND_ID_MOUSE_RELESE_B5	0x26	// スクリプトコマンドID マウスリリースボタン5クリック
#define SCRIPT_COMMAND_ID_MOUSE_SCROLL_UP	0x31	// スクリプトコマンドID マウススクロールアップ
#define SCRIPT_COMMAND_ID_MOUSE_SCROLL_DOWN	0x32	// スクリプトコマンドID マウススクロールダウン
#define SCRIPT_COMMAND_ID_MOUSE_MOVE        0x33	// スクリプトコマンドID マウスムーブ
#define SCRIPT_COMMAND_ID_JOY_BTN_PRESS 	0x69	// スクリプトコマンドID ジョイスティックボタンプレス
#define SCRIPT_COMMAND_ID_JOY_BTN_RELESE 	0x61	// スクリプトコマンドID ジョイスティックボタンリリース
#define SCRIPT_COMMAND_ID_JOY_HATSW_PRESS 	0x6A	// スクリプトコマンドID ジョイスティックHATSWプレス
#define SCRIPT_COMMAND_ID_JOY_HATSW_RELESE 	0x62	// スクリプトコマンドID ジョイスティックHATSWリリース
#define SCRIPT_COMMAND_ID_JOY_L_LEVER           0x6B	// スクリプトコマンドID ジョイスティック左レバー出力
#define SCRIPT_COMMAND_ID_JOY_L_LEVER_CENTER 	0x63	// スクリプトコマンドID ジョイスティック左レバー中立
#define SCRIPT_COMMAND_ID_JOY_R_LEVER           0x6C	// スクリプトコマンドID ジョイスティック右レバー出力
#define SCRIPT_COMMAND_ID_JOY_R_LEVER_CENTER 	0x64	// スクリプトコマンドID ジョイスティック右レバー中立
// SCRIPT実行情報　構造体
#if 0
typedef struct
{
	BYTE	Script_Data[SCRIPT_EXE_DATA_BUFF_BANK_NUM][SCRIPT_EXE_DATA_BUFF_SIZE];
} ST_SCRIPT_EXE_SCRIPT_DATA;
typedef struct
{
	ST_SCRIPT_EXE_SCRIPT_DATA Script_Data[SCRIPT_EXE_INFO_NUM];
} ST_SCRIPT_EXE_SCRIPT_DATAS;
#endif
#if 0
typedef struct
{
	BYTE	Exe_Flag_Integration;						// スクリプト実行フラグ　統合
	BYTE	Exe_Flag[SCRIPT_EXE_INFO_NUM];				// スクリプト実行フラグ 0=未実行 1=実行中
	BYTE	Script_No[SCRIPT_EXE_INFO_NUM];				// スクリプト実行No.
	WORD	Pause_Flag[SCRIPT_EXE_INFO_NUM];				// スクリプト一時停止フラグ　0!=一時停止
	BYTE	Control_Flag0[SCRIPT_EXE_INFO_NUM];			// コントロールフラグ　0=1回実行 1=ループモード 2=ファイヤーモード
	BYTE	Control_Flag1[SCRIPT_EXE_INFO_NUM];			// コントロールフラグ　次コマンド実行バッファ バンク
	BYTE	Control_Flag2[SCRIPT_EXE_INFO_NUM];			// コントロールフラグ 次コマンド実行バッファ位置
	BYTE	Control_Flag3[SCRIPT_EXE_INFO_NUM];			// コントロールフラグ　バッファ読み込み要求 0=未要求 1=バッファA読み込み要求 2=バッファA読み込み完了 3=バッファB読み込み要求 4=バッファB読み込み完了
//	BYTE	Script_Data[SCRIPT_EXE_DATA_BUFF_BANK_NUM][SCRIPT_EXE_DATA_BUFF_SIZE];	// スクリプトデータ読み込みバッファ
	BYTE	reserve1[SCRIPT_EXE_INFO_NUM];				// 
	BYTE	Command_ID[SCRIPT_EXE_INFO_NUM];				// 次実行スクリプトコマンドID
	WORD_VAL	Command_DATA[SCRIPT_EXE_INFO_NUM];		// 次実行スクリプトコマンドDATA
	WORD_VAL	Interval[SCRIPT_EXE_INFO_NUM];			// 実行間隔
	DWORD_VAL	Address[SCRIPT_EXE_INFO_NUM];			// スクリプトデータ保存アドレス
	DWORD_VAL	Script_Size[SCRIPT_EXE_INFO_NUM];		// スクリプトサイズ
	DWORD_VAL	Script_Exe_Pos[SCRIPT_EXE_INFO_NUM];		// スクリプト実行中位置[byte]
	DWORD_VAL	Next_Read_Address[SCRIPT_EXE_INFO_NUM];	// スクリプトデータ次読み込みアドレス
	BYTE	reserve2[SCRIPT_EXE_INFO_NUM];				// 
	BYTE	USB_Send_Code_Mouse[SCRIPT_EXE_INFO_NUM];						// USB送信コード　マウス
	WORD	USB_Send_Wheel_Scroll[SCRIPT_EXE_INFO_NUM];						// USB送信コード　マウス ホイールスクロール
	BYTE	USB_Send_Code_Keyboard[SCRIPT_EXE_INFO_NUM][HID_INT_IN_EP_SIZE];	// USB送信コード　キーボード
	BYTE	USB_Send_Code_Mouse_Fix;										// USB送信コード　マウス 確定
	WORD	USB_Send_Wheel_Scroll_Fix;										// USB送信コード　マウス ホイールスクロール 確定
	BYTE	USB_Send_Code_Keyboard_Fix[HID_INT_IN_EP_SIZE];					// USB送信コード　キーボード 確定
} ST_SCRIPT_EXE_INFO;
#endif
#if 0
typedef struct
{
	BYTE	Exe_Flag_Integration;						// スクリプト実行フラグ　統合
	BYTE	Exe_Flag[SCRIPT_EXE_INFO_NUM];				// スクリプト実行フラグ 0=未実行 1=実行中
	BYTE	Script_No[SCRIPT_EXE_INFO_NUM];				// スクリプト実行No.
	WORD	Pause_Flag[SCRIPT_EXE_INFO_NUM];				// スクリプト一時停止フラグ　0!=一時停止
	BYTE	Control_Flag0[SCRIPT_EXE_INFO_NUM];			// コントロールフラグ　0=1回実行 1=ループモード 2=ファイヤーモード
	BYTE	Control_Flag1[SCRIPT_EXE_INFO_NUM];			// コントロールフラグ　次コマンド実行バッファ バンク
	BYTE	Control_Flag2[SCRIPT_EXE_INFO_NUM];			// コントロールフラグ 次コマンド実行バッファ位置
	BYTE	Control_Flag3[SCRIPT_EXE_INFO_NUM];			// コントロールフラグ　バッファ読み込み要求 0=未要求 1=バッファA読み込み要求 2=バッファA読み込み完了 3=バッファB読み込み要求 4=バッファB読み込み完了
//	BYTE	Script_Data[SCRIPT_EXE_DATA_BUFF_BANK_NUM][SCRIPT_EXE_DATA_BUFF_SIZE];	// スクリプトデータ読み込みバッファ
	BYTE	reserve1[SCRIPT_EXE_INFO_NUM];				// 
//	BYTE	Command_ID[SCRIPT_EXE_INFO_NUM];				// 次実行スクリプトコマンドID
//	WORD_VAL	Command_DATA[SCRIPT_EXE_INFO_NUM];		// 次実行スクリプトコマンドDATA
//	WORD_VAL	Interval[SCRIPT_EXE_INFO_NUM];			// 実行間隔
//	DWORD_VAL	Address[SCRIPT_EXE_INFO_NUM];			// スクリプトデータ保存アドレス
//	DWORD_VAL	Script_Size[SCRIPT_EXE_INFO_NUM];		// スクリプトサイズ
//	DWORD_VAL	Script_Exe_Pos[SCRIPT_EXE_INFO_NUM];		// スクリプト実行中位置[byte]
//	DWORD_VAL	Next_Read_Address[SCRIPT_EXE_INFO_NUM];	// スクリプトデータ次読み込みアドレス
//	BYTE	reserve2[SCRIPT_EXE_INFO_NUM];				// 
	BYTE	USB_Send_Code_Mouse[SCRIPT_EXE_INFO_NUM];						// USB送信コード　マウス
	WORD	USB_Send_Wheel_Scroll[SCRIPT_EXE_INFO_NUM];						// USB送信コード　マウス ホイールスクロール
	BYTE	USB_Send_Code_Keyboard[SCRIPT_EXE_INFO_NUM][HID_INT_IN_EP_SIZE];	// USB送信コード　キーボード
	BYTE	USB_Send_Code_Mouse_Fix;										// USB送信コード　マウス 確定
	WORD	USB_Send_Wheel_Scroll_Fix;										// USB送信コード　マウス ホイールスクロール 確定
	BYTE	USB_Send_Code_Keyboard_Fix[HID_INT_IN_EP_SIZE];					// USB送信コード　キーボード 確定
} ST_SCRIPT_EXE_INFO;
#endif
#if 1
typedef struct
{
	BYTE	Exe_Flag;				// 0 スクリプト実行フラグ 0=未実行 1=実行中
	BYTE	Script_No;				// 1 スクリプト実行No.
	WORD	Pause_Flag;				// 2 スクリプト一時停止フラグ　0!=一時停止
	BYTE	Control_Flag0;			// 4 コントロールフラグ　0=1回実行 1=ループモード 2=ファイヤーモード
	BYTE	Control_Flag1;			// 5 コントロールフラグ　次コマンド実行バッファ バンク
	BYTE	Control_Flag2;			// 6 コントロールフラグ 次コマンド実行バッファ位置
	BYTE	Control_Flag3;			// 7 コントロールフラグ　バッファ読み込み要求 0=未要求 1=バッファA読み込み要求 2=バッファA読み込み完了 3=バッファB読み込み要求 4=バッファB読み込み完了
	BYTE	Script_Data[SCRIPT_EXE_DATA_BUFF_BANK_NUM][SCRIPT_EXE_DATA_BUFF_SIZE];	// 8 スクリプトデータ読み込みバッファ
	BYTE	reserve1;				// 32
	BYTE	Command_ID;				// 33 次実行スクリプトコマンドID
	WORD_VAL	Command_DATA;		// 34 次実行スクリプトコマンドDATA
	WORD_VAL	Interval;			// 36 実行間隔
	DWORD_VAL	Address;			// 38 スクリプトデータ保存アドレス
	DWORD_VAL	Script_Size;		// 42 スクリプトサイズ
	DWORD_VAL	Script_Exe_Pos;		// 46 スクリプト実行中位置[byte]
	DWORD_VAL	Next_Read_Address;	// 50 スクリプトデータ次読み込みアドレス
	BYTE	USB_Send_Code_Mouse[MOUSE_BUFF_SIZE];       // 54 USB送信コード　マウス
	WORD	USB_Send_Wheel_Scroll;						// 58 USB送信コード　マウス ホイールスクロール
	BYTE	USB_Send_Code_Keyboard[KEYBOARD_BUFF_SIZE];	// 60 USB送信コード　キーボード
	BYTE	reserve2;				// 69
	BYTE	USB_Send_Code_Joystick[JOYSTICK_BUFF_SIZE];	// 70 USB送信コード　ジョイスティック
	BYTE	reserve3;				// 77
	BYTE	USB_Send_Code_MultiMedia[MULTIMEDIA_BUFF_SIZE];	// 78 USB送信コード　マルチメディア
	BYTE	reserve4;				// 81
} ST_SCRIPT_EXE_INFO;
#endif

extern BYTE Set_Script_Exe_Info(BYTE p_idx, BYTE p_ScriptNo);
extern BYTE Script_Exe_Info_Clr(void);
extern BYTE Script_Reset(BYTE p_idx);
extern BYTE Get_Next_Script_Data(void);
extern BYTE Get_Next_Script_Command(void);
extern BYTE Set_USB_Send_Code(void);
extern BYTE Get_Script_Output_Status( void );
//extern BYTE Get_Script_Output_Status_Multi(BYTE p_idx);
//extern BYTE Script_Interval_Update(void);
extern BYTE Script_Run(BYTE p_idx);
extern BYTE Script_Stop(BYTE p_idx);
extern BYTE Script_Fire_Mode_Stop(BYTE p_idx, BYTE p_ScriptNo);
extern BYTE Script_Hold_Mode_Stop(BYTE p_idx, BYTE p_ScriptNo);
extern BYTE Get_Exe_Script_No(BYTE p_idx);
extern BYTE Get_Script_Attribute(BYTE p_idx);


extern ST_SCRIPT_EXE_INFO script_exe_info;
//extern ST_SCRIPT_EXE_SCRIPT_DATAS script_exe_script_datas;
//extern ST_SCRIPT_EXE_BUFF script_exe_buff;
//extern ST_SCRIPT_EXE_COMMAND script_exe_command;
//extern ST_SCRIPT_EXE_SEND_CODE script_exe_send_code;

extern BYTE next_script_exe_req_scriptNo;


#endif//L_SCRIPT_H
