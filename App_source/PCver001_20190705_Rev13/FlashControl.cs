using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace RevOmate
{
    class FlashControl
    {
        private System.Timers.Timer myFlashTimer;

        //public byte[] flash_data;
        //public bool[] sector_change_flag;

        //public uint read_address;
        //public uint read_size;
        //public uint usb_read_address;
        //public uint usb_read_size;
        //public byte[] read_buffer;

        //public uint write_address;
        //public uint write_size;
        //public uint usb_write_address;
        //public uint usb_write_size;
        //public byte[] write_buffer;

        public byte read_comp_flag;
        public byte all_read_flag;
        public byte read_flag_base_setting;
        public byte read_flag_function_setting;
        public byte read_flag_encoder_script_setting;
        public byte read_flag_sw_function_setting;
        public byte read_flag_script_info;
        public byte read_flag_script_data;
        //public byte read_flag_mode;

        public byte write_comp_flag;
        public byte all_write_flag;
        public byte write_flag_base_setting;
        public byte write_flag_function_setting;
        public byte write_flag_encoder_script_setting;
        public byte write_flag_sw_function_setting;
        public byte write_flag_script_info;
        public byte write_flag_script_data;
        //public byte write_flag_mode;

        byte[] my_LED_Duty_Max = new byte[] { Constants.LED_R_DUTY_MAX, Constants.LED_G_DUTY_MAX, Constants.LED_B_DUTY_MAX };


        public const uint FM_TOTAL_SIZE =           0x200000;		// FLASH MEMORY 総サイズ
        public const uint FM_SECTOR_NUM =           0x20;   		// FLASH MEMORY セクター数
        public const uint FM_SECTOR_SIZE =          0x10000;        // FLASH MEMORY セクターサイズ
        public const uint FM_USB_READ_DATA_SIZE =   62;             // USBリードサイズ（1回の通信での）
        public const uint FM_USB_WRITE_DATA_SIZE =  58;             // USBライトサイズ（1回の通信での）

        public const int FM_ADR_BASE_INFO               = 0x000000;   // 基本設定情報 保存先頭アドレス
        public const int FM_BASE_INFO_AREA_SIZE         = 0x000070;   // 基本設定情報 保存エリアサイズ
        public const int FM_ADR_FUNCTION_SETTING        = 0x000100;   // 機能設定情報 保存先頭アドレス
        public const int FM_ADR_FUNCTION_SETTING_NAME   = 0x000600;   // 機能設定情報 機能名称保存先頭アドレス
        public const int FM_FUNCTION_SETTING_AREA_SIZE  = 0x000800;   // 機能設定情報 保存エリアサイズ
        public const int FM_ADR_ENCODER_SCRIPT_SETTING  = 0x000A00;   // 機能設定情報 エンコーダースクリプト先頭アドレス
        public const int FM_ENCODER_SCRIPT_SETTING_AREA_SIZE = 0x000090;   // 機能設定情報 エンコーダースクリプト保存エリアサイズ
        public const int FM_ADR_SW_FUNCTION_SETTING     = 0x000B00;   // SW機能設定先頭アドレス
        public const int FM_SW_FUNCTION_SETTING_AREA_SIZE = 0x000108;   // SW機能設定保存エリアサイズ
#if false    // スクリプト保存数120
        public const int FM_ADR_SCRIPT_INFO             = 0x010000;   // スクリプト情報
        public const int FM_SCRIPT_INFO_AREA_SIZE       = 0x007F90;   // スクリプト情報保存エリアサイズ 0x10+0x110*120=0x7F90
        public const int FM_ADR_SCRIPT_DATA             = 0x020000;    // スクリプトデータ
        public const int FM_SCRIPT_DATA_AREA_SIZE       = 0x1E0000;    // スクリプトデータ保存エリアサイズ
        public const int FM_SCRIPT_DATA_SECTOR_NUM      = 0x001E;        // スクリプトデータ保存セクター数
        public const int FM_SCRIPT_DATA_NUM_PER_SECTOR  = 0x0004;        // １セクター当たりのスクリプトデータ保存数
        public const int FM_SCRIPT_DATA_MAX_SIZE        = 0x4000;      // １スクリプトデータの最大サイズ 0x10000 / 4 = 0x4000(16384)
#endif
#if true    // スクリプト保存数200
        public const int FM_ADR_SCRIPT_INFO             = 0x010000;   // スクリプト情報
        public const int FM_SCRIPT_INFO_AREA_SIZE       = 0x00D490;   // スクリプト情報保存エリアサイズ 0x10+0x110*200=0xD490
        public const int FM_ADR_SCRIPT_DATA             = 0x020000;    // スクリプトデータ
        public const int FM_SCRIPT_DATA_AREA_SIZE       = 0x1E0000;    // スクリプトデータ保存エリアサイズ
        public const int FM_SCRIPT_DATA_SECTOR_NUM      = 0x001E;        // スクリプトデータ保存セクター数
        public const int FM_SCRIPT_DATA_NUM_PER_SECTOR  = 0x0007;        // １セクター当たりのスクリプトデータ保存数
        public const int FM_SCRIPT_DATA_MAX_SIZE        = 0x2492;      // １スクリプトデータの最大サイズ 0x10000 / 7 = 0x2492(9362)
#endif

        public const int FM_BASE_SETTING_ST_ADR         = 0x000000;		// 基本設定情報　先頭アドレス
        public const int FM_BASE_SETTING_SIZE           = 0x000008;		// 基本設定情報　サイズ
        //基本設定情報格納位置オフセット
        public const int FM_OFSET_BASE_MODE             = 0x000000;		// 基本設定情報　モード
        public const int FM_OFSET_BASE_LED_SLEEP        = 0x000001;		// 基本設定情報　LEDスリープ設定
        public const int FM_OFSET_BASE_LED_LIGHT_MODE   = 0x000002;		// 基本設定情報　LED点灯設定MODE
        public const int FM_OFSET_BASE_LED_LIGHT_FUNC   = 0x000003;		// 基本設定情報　LED点灯設定FUNC
        public const int FM_OFSET_BASE_LED_OFF_TIME     = 0x000004;		// 基本設定情報　LED消灯までの時間
        public const int FM_OFSET_BASE_ENCODER_TYPEMATIC = 0x000005;    // 基本設定情報　エンコーダタイプマティック

        public const int FM_BASE_MODE_SETTING_ST_ADR    = 0x000010;		// 基本設定情報　モード　先頭アドレス
        public const int FM_BASE_MODE_SETTING_SIZE      = 0x000020;		// 基本設定情報　モード　サイズ
        //基本設定モード情報格納位置オフセット
#if true
        public const int FM_OFSET_BASE_SW1_SCRIPT_NO    = 0x000000;		// 基本設定情報　SW1実行スクリプトNo.
        public const int FM_OFSET_BASE_SW2_SCRIPT_NO    = 0x000001;		// 基本設定情報　SW2実行スクリプトNo.
        public const int FM_OFSET_BASE_SW3_SCRIPT_NO    = 0x000002;		// 基本設定情報　SW3実行スクリプトNo.
        public const int FM_OFSET_BASE_SW4_SCRIPT_NO    = 0x000003;		// 基本設定情報　SW4実行スクリプトNo.
        public const int FM_OFSET_BASE_SW5_SCRIPT_NO    = 0x000004;		// 基本設定情報　SW5実行スクリプトNo.
        public const int FM_OFSET_BASE_SW6_SCRIPT_NO    = 0x000005;		// 基本設定情報　SW6実行スクリプトNo.
        public const int FM_OFSET_BASE_SW7_SCRIPT_NO    = 0x000006;		// 基本設定情報　SW7実行スクリプトNo.
        public const int FM_OFSET_BASE_SW8_SCRIPT_NO    = 0x000007;		// 基本設定情報　SW8実行スクリプトNo.
        public const int FM_OFSET_BASE_SW9_SCRIPT_NO    = 0x000008;		// 基本設定情報　SW9実行スクリプトNo.
        public const int FM_OFSET_BASE_SW10_SCRIPT_NO   = 0x000009;		// 基本設定情報　SW10実行スクリプトNo.
        public const int FM_OFSET_BASE_SW11_SCRIPT_NO   = 0x00000A;		// 基本設定情報　SW11実行スクリプトNo.
        public const int FM_OFSET_BASE_SW1_SP_FUNC_NO   = 0x00000B;		// 基本設定情報　SW1特殊機能No.
        public const int FM_OFSET_BASE_SW2_SP_FUNC_NO   = 0x00000C;		// 基本設定情報　SW2特殊機能No.
        public const int FM_OFSET_BASE_SW3_SP_FUNC_NO   = 0x00000D;		// 基本設定情報　SW3特殊機能No.
        public const int FM_OFSET_BASE_SW4_SP_FUNC_NO   = 0x00000E;		// 基本設定情報　SW4特殊機能No.
        public const int FM_OFSET_BASE_SW5_SP_FUNC_NO   = 0x00000F;		// 基本設定情報　SW5特殊機能No.
        public const int FM_OFSET_BASE_SW6_SP_FUNC_NO   = 0x000010;		// 基本設定情報　SW6特殊機能No.
        public const int FM_OFSET_BASE_SW7_SP_FUNC_NO   = 0x000011;		// 基本設定情報　SW7特殊機能No.
        public const int FM_OFSET_BASE_SW8_SP_FUNC_NO   = 0x000012;		// 基本設定情報　SW8特殊機能No.
        public const int FM_OFSET_BASE_SW9_SP_FUNC_NO   = 0x000013;		// 基本設定情報　SW9特殊機能No.
        public const int FM_OFSET_BASE_SW10_SP_FUNC_NO  = 0x000014;		// 基本設定情報　SW10特殊機能No.
        public const int FM_OFSET_BASE_SW11_SP_FUNC_NO  = 0x000015;		// 基本設定情報　SW11特殊機能No.
        public const int FM_OFSET_BASE_ENCODER_DEFAULT  = 0x000016;		// 基本設定情報　エンコーダーデフォルト機能No.
        public const int FM_OFSET_BASE_LED_COLOR_NO     = 0x000017;		// 基本設定情報　LED Color No.
        public const int FM_OFSET_BASE_LED_DETAIL_FLAG  = 0x000018;		// 基本設定情報　LED詳細設定フラグ
        public const int FM_OFSET_BASE_LED_RGB_DUTY     = 0x000019;		// 基本設定情報　LED RGB Duty TOP
        public const int FM_OFSET_BASE_LED_R_DUTY       = 0x000019;		// 基本設定情報　LED R Duty
        public const int FM_OFSET_BASE_LED_G_DUTY       = 0x00001A;		// 基本設定情報　LED G Duty
        public const int FM_OFSET_BASE_LED_B_DUTY       = 0x00001B;		// 基本設定情報　LED B Duty
        public const int FM_OFSET_BASE_LED_BRIGHTNESS_LEVEL = 0x00001C;		// 基本設定情報　LED輝度レベル設定
#endif
#if false
        public const int FM_OFSET_BASE_SW1_SCRIPT_NO = 0x000000;		// 基本設定情報　SW1実行スクリプトNo.
        public const int FM_OFSET_BASE_SW2_SCRIPT_NO = 0x000001;		// 基本設定情報　SW2実行スクリプトNo.
        public const int FM_OFSET_BASE_SW3_SCRIPT_NO = 0x000002;		// 基本設定情報　SW3実行スクリプトNo.
        public const int FM_OFSET_BASE_SW4_SCRIPT_NO = 0x000003;		// 基本設定情報　SW4実行スクリプトNo.
        public const int FM_OFSET_BASE_SW5_SCRIPT_NO = 0x000004;		// 基本設定情報　SW5実行スクリプトNo.
        public const int FM_OFSET_BASE_SW6_SCRIPT_NO = 0x000005;		// 基本設定情報　SW6実行スクリプトNo.
        public const int FM_OFSET_BASE_SW7_SCRIPT_NO = 0x000006;		// 基本設定情報　SW7実行スクリプトNo.
        public const int FM_OFSET_BASE_SW8_SCRIPT_NO = 0x000007;		// 基本設定情報　SW8実行スクリプトNo.
        public const int FM_OFSET_BASE_SW9_SCRIPT_NO = 0x000008;		// 基本設定情報　SW9実行スクリプトNo.
        public const int FM_OFSET_BASE_SW10_SCRIPT_NO = 0x000009;		// 基本設定情報　SW10実行スクリプトNo.
        public const int FM_OFSET_BASE_SW11_SCRIPT_NO = 0x00000A;		// 基本設定情報　SW11実行スクリプトNo.
        public const int FM_OFSET_BASE_ENCODER_DEFAULT = 0x00000B;		// 基本設定情報　エンコーダーデフォルト機能No.
        public const int FM_OFSET_BASE_LED_COLOR_NO = 0x00000C;		// 基本設定情報　LED Color No.
        public const int FM_OFSET_BASE_LED_DETAIL_FLAG = 0x00000D;		// 基本設定情報　LED詳細設定フラグ
        public const int FM_OFSET_BASE_LED_RGB_DUTY = 0x00000E;		// 基本設定情報　LED RGB Duty TOP
        public const int FM_OFSET_BASE_LED_R_DUTY = 0x00000E;		// 基本設定情報　LED R Duty
        public const int FM_OFSET_BASE_LED_G_DUTY = 0x00000F;		// 基本設定情報　LED G Duty
        public const int FM_OFSET_BASE_LED_B_DUTY = 0x000010;		// 基本設定情報　LED B Duty
#endif

        //機能設定情報
        //機能設定情報格納位置オフセット
        public const int FM_FUNCTION_SETTING_ST_ADR     = 0x000100;		// 機能設定情報　先頭アドレス
        public const int FM_FUNCTION_SETTING_SIZE_MODE  = 0x000060;		// 機能設定情報　モード単位のサイズ
        public const int FM_FUNCTION_SETTING_SIZE_FUNC  = 0x000018;		// 機能設定情報　機能単位のサイズ
        //格納位置オフセット
        public const int FM_OFSET_FUNCTION_CW_DATA_TOP      = 0x000000; // 機能 右回転 DATA先頭
        public const int FM_OFSET_FUNCTION_CCW_DATA_TOP     = 0x000008; // 機能 左回転 DATA先頭
        public const int FM_OFSET_FUNCTION_DATA_SIZE        = 0x000008; // 機能 左右回転 DATAサイズ
        public const int FM_OFSET_FUNCTION_LED_COLOR_NO     = 0x000010; // LED Color No.
        public const int FM_OFSET_FUNCTION_LED_DETAIL_FLAG  = 0x000011; // LED詳細設定フラグ
        public const int FM_OFSET_FUNCTION_LED_RGB_DUTY_TOP = 0x000012; // LED RGB Duty DATA先頭
        public const int FM_OFSET_FUNCTION_LED_R_DUTY       = 0x000012; // LED R Duty
        public const int FM_OFSET_FUNCTION_LED_G_DUTY       = 0x000013; // LED G Duty
        public const int FM_OFSET_FUNCTION_LED_B_DUTY       = 0x000014; // LED B Duty
        public const int FM_OFSET_FUNCTION_LED_BRIGHTNESS_LEVEL = 0x000015; // LED 輝度設定
        // 機能設定　機能名称
        public const int FM_FUNCTION_NAME_ST_ADR            = 0x000600;		// 機能名称文字列　先頭アドレス
        public const int FM_FUNCTION_NAME_ST_SIZE           = 0x000040;		// 機能名称文字列　サイズ
        public const int FM_FUNCTION_NAME_MAX_SIZE          = 0x00003F;		// 機能名称文字列最大サイズ
        //格納位置オフセット
        public const int FM_OFSET_FUNCTION_NAME_SIZE        = 0x000000;		// 機能名称文字列長
        public const int FM_OFSET_FUNCTION_NAME_DATA        = 0x000001;		// 機能名称文字列データ
        // 機能設定　エンコーダースクリプト
        public const int FM_ENCODER_SCRIPT_ST_ADR           = 0x000A00; // 機能　エンコーダースクリプト　先頭アドレス
        public const int FM_ENCODER_SCRIPT_ST_SIZE          = 0x000030; // 機能　エンコーダースクリプト　サイズ
        public const int FM_ENCODER_SCRIPT_SCRIPT_MAX_SIZE  = 0x000020; // 機能　エンコーダースクリプト　スクリプト設定最大サイズ
        //格納位置オフセット
        public const int FM_OFSET_ENCODER_SCRIPT_SCRIPT_NUM_TOP = 0x000000; // 機能　エンコーダースクリプト 設定数
        public const int FM_OFSET_ENCODER_SCRIPT_LOOP_SET_TOP   = 0x000001; // 機能　エンコーダースクリプト 繰り返し設定
        public const int FM_OFSET_ENCODER_SCRIPT_DATA_TOP       = 0x000010; // 機能　エンコーダースクリプト スクリプト設定データ先頭


        // アプリ用設定情報 ※※※Flashの空き領域の0x300～0x5FFにアプリ用のデータを保存する※※※
        //SW機能 0x300～0x4EE使用
        public const int FM_ADR_APP_SW_SETTING                  = 0x000300;   // アプリ用SW機能設定情報 保存先頭アドレス
        public const int FM_APP_SW_SETTING_SIZE_DATA            = 0x00000B;		// アプリ用SW機能設定情報　データ単位のサイズ 11 = 3 + 8
        public const int FM_APP_SW_SETTING_SIZE_MODE            = 0x0000A5;		// アプリ用SW機能設定情報　モード単位のサイズ 165 = 11 * 15
        //格納位置オフセット
        public const int FM_OFSET_APP_SETTING_SW_DATA_TOP       = 0x000000; // アプリ用設定情報 SW DATA先頭
        public const int FM_OFSET_APP_SETTING_SW1_DATA_TOP      = 0x000000; // アプリ用設定情報 SW1 DATA先頭
        public const int FM_OFSET_APP_SETTING_SW2_DATA_TOP      = 0x00000B; // アプリ用設定情報 SW2 DATA先頭
        public const int FM_OFSET_APP_SETTING_SW3_DATA_TOP      = 0x000016; // アプリ用設定情報 SW3 DATA先頭
        public const int FM_OFSET_APP_SETTING_SW4_DATA_TOP      = 0x000021; // アプリ用設定情報 SW4 DATA先頭
        public const int FM_OFSET_APP_SETTING_SW5_DATA_TOP      = 0x00002C; // アプリ用設定情報 SW5 DATA先頭
        public const int FM_OFSET_APP_SETTING_SW6_DATA_TOP      = 0x000037; // アプリ用設定情報 SW6 DATA先頭
        public const int FM_OFSET_APP_SETTING_SW7_DATA_TOP      = 0x000042; // アプリ用設定情報 SW7 DATA先頭
        public const int FM_OFSET_APP_SETTING_SW8_DATA_TOP      = 0x00004D; // アプリ用設定情報 SW8 DATA先頭
        public const int FM_OFSET_APP_SETTING_SW9_DATA_TOP      = 0x000058; // アプリ用設定情報 SW9 DATA先頭
        public const int FM_OFSET_APP_SETTING_SW10_DATA_TOP     = 0x000063; // アプリ用設定情報 SW10 DATA先頭
        public const int FM_OFSET_APP_SETTING_ENCSW_DATA_TOP    = 0x00006E; // アプリ用設定情報 ENCODER SW DATA先頭
        public const int FM_OFSET_APP_SETTING_ENC_FUNC1_DATA_TOP = 0x000079; // アプリ用設定情報 ENCODER FUNC1 DATA先頭
        public const int FM_OFSET_APP_SETTING_ENC_FUNC2_DATA_TOP = 0x000084; // アプリ用設定情報 ENCODER FUNC2 DATA先頭
        public const int FM_OFSET_APP_SETTING_ENC_FUNC3_DATA_TOP = 0x00008F; // アプリ用設定情報 ENCODER FUNC3 DATA先頭
        public const int FM_OFSET_APP_SETTING_ENC_FUNC4_DATA_TOP = 0x00009A; // アプリ用設定情報 ENCODER FUNC4 DATA先頭
        // ダイヤル機能 0x500～0x577
        public const int FM_ADR_APP_FUNC_SETTING                        = 0x000500;   // アプリ用ダイヤル機能設定情報 保存先頭アドレス
        public const int FM_APP_FUNC_SETTING_SIZE_DATA                  = 0x000005;		// アプリ用ダイヤル機能設定情報　データ単位のサイズ 5
        public const int FM_APP_FUNC_SETTING_SIZE_FUNC                  = 0x00000A;		// アプリ用ダイヤル機能設定情報　機能単位のサイズ 10 = 5 * 2
        public const int FM_APP_FUNC_SETTING_SIZE_MODE                  = 0x000028;		// アプリ用ダイヤル機能設定情報　モード単位のサイズ 40 = 5 * (4 * 2)
        //格納位置オフセット
        public const int FM_OFSET_APP_FUNC_SETTING_FUNC_DATA_TOP        = 0x00000; // アプリ用設定情報 ENCODER FUNC DATA先頭
        public const int FM_OFSET_APP_FUNC_SETTING_FUNC1_CW_DATA_TOP    = 0x00000; // アプリ用設定情報 ENCODER FUNC1 CW DATA先頭
        public const int FM_OFSET_APP_FUNC_SETTING_FUNC1_CCW_DATA_TOP   = 0x00005; // アプリ用設定情報 ENCODER FUNC1 CCW DATA先頭
        public const int FM_OFSET_APP_FUNC_SETTING_FUNC2_CW_DATA_TOP    = 0x0000A; // アプリ用設定情報 ENCODER FUNC2 CW DATA先頭
        public const int FM_OFSET_APP_FUNC_SETTING_FUNC2_CCW_DATA_TOP   = 0x0000F; // アプリ用設定情報 ENCODER FUNC2 CCW DATA先頭
        public const int FM_OFSET_APP_FUNC_SETTING_FUNC3_CW_DATA_TOP    = 0x00014; // アプリ用設定情報 ENCODER FUNC3 CW DATA先頭
        public const int FM_OFSET_APP_FUNC_SETTING_FUNC3_CCW_DATA_TOP   = 0x00019; // アプリ用設定情報 ENCODER FUNC3 CCW DATA先頭
        public const int FM_OFSET_APP_FUNC_SETTING_FUNC4_CW_DATA_TOP    = 0x0001E; // アプリ用設定情報 ENCODER FUNC4 CW DATA先頭
        public const int FM_OFSET_APP_FUNC_SETTING_FUNC4_CCW_DATA_TOP   = 0x00023; // アプリ用設定情報 ENCODER FUNC4 CCW DATA先頭


        //SW機能設定情報
        //SW機能設定情報格納位置オフセット
        public const int FM_SW_FUNCTION_SETTING_SIZE_MODE = 0x000058;		// SW機能設定情報　モード単位のサイズ 8*11=88(0x58)
        public const int FM_SW_FUNCTION_SETTING_SIZE_FUNC = 0x000008;		// SW機能設定情報　機能単位のサイズ

        //スクリプト情報格納位置オフセット
        public const int FM_OFSET_SCRIPT_CHK_SUM_L =        0x000000;		// チェクサムL
        public const int FM_OFSET_SCRIPT_CHK_SUM_H =        0x000001;		// チェクサムH
        public const int FM_OFSET_SCRIPT_REC_NUM =          0x000002;		// 記録スクリプト数
        //public const int FM_OFSET_SCRIPT_ = 0x;		// 
        public const int FM_OFSET_SCRIPT_TOTAL_SIZE_L0 =    0x000004;		// 全スクリプトデータサイズLSB
        public const int FM_OFSET_SCRIPT_TOTAL_SIZE_L1 =    0x000005;		// 全スクリプトデータサイズ
        public const int FM_OFSET_SCRIPT_TOTAL_SIZE_L2 =    0x000006;		// 全スクリプトデータサイズ
        public const int FM_OFSET_SCRIPT_TOTAL_SIZE_L3 =    0x000007;		// 全スクリプトデータサイズMSB

        public const int FM_SCRIPT_INFO_DATA_ST_ADR =       0x000010;		// スクリプト情報データ　先頭アドレス
        public const int FM_SCRIPT_INFO_DATA_SIZE =         0x000110;		// スクリプト情報データ　サイズ
        public const int FM_SCRIPT_INFO_DATA_NAME_SIZE =    0x00FE;         // スクリプト名称文字列最大サイズ
        //格納位置オフセット
        public const int FM_OFSET_SCRIPT_REC_ADR =          0x000000;		// スクリプトデータ保存アドレス
        public const int FM_OFSET_SCRIPT_DATA_SIZE =        0x000004;		// スクリプトデータサイズ
        public const int FM_OFSET_SCRIPT_MODE =             0x000008;		// スクリプトモード
        public const int FM_OFSET_SCRIPT_NAME_SIZE =        0x000009;		// スクリプト名称文字列長
        public const int FM_OFSET_SCRIPT_NAME_DATA =        0x00000A;		// スクリプト名称文字列データ


        public const uint FM_READ_TYPE_BASE_SETTING             = 0x0001;     // 
        public const uint FM_READ_TYPE_FUNCTION_SETTING         = 0x0002;     // 
        public const uint FM_READ_TYPE_ENCODER_SCRIPT_SETTING   = 0x0004;     // 
        public const uint FM_READ_TYPE_SW_FUNCTION_SETTING      = 0x0008;     // 
        public const uint FM_READ_TYPE_SCRIPT_INFO              = 0x0010;     // 
        public const uint FM_READ_TYPE_SCRIPT_DATA              = 0x0020;     // 
        //public const uint FM_READ_TYPE_MODE                   = 0x0040;     // 
        public const uint FM_WRITE_TYPE_BASE_SETTING            = 0x0001;     // 
        public const uint FM_WRITE_TYPE_FUNCTION_SETTING        = 0x0002;     // 
        public const uint FM_WRITE_TYPE_ENCODER_SCRIPT_SETTING  = 0x0004;     // 
        public const uint FM_WRITE_TYPE_SW_FUNCTION_SETTING     = 0x0008;     // 
        public const uint FM_WRITE_TYPE_SCRIPT_INFO             = 0x0010;     // 
        public const uint FM_WRITE_TYPE_SCRIPT_DATA             = 0x0020;     // 
        //public const uint FM_WRITE_TYPE_MODE                  = 0x0040;     // 

        public const byte FM_READ_STATUS_NA             = 0x00;     // リード状態　N/A
        public const byte FM_READ_STATUS_RQ             = 0x01;     // リード状態　読み込みリクエスト
        public const byte FM_READ_STATUS_READING        = 0x02;     // リード状態　読み込み中
        public const byte FM_READ_STATUS_COMP           = 0x04;     // リード状態　読み込み完了
        public const byte FM_WRITE_STATUS_NA            = 0x00;     // ライト状態　N/A
        public const byte FM_WRITE_STATUS_RQ            = 0x01;     // ライト状態　書き込みリクエスト
        public const byte FM_WRITE_STATUS_WRITTING      = 0x02;     // ライト状態　書き込み中
        public const byte FM_WRITE_STATUS_COMP          = 0x04;     // ライト状態　書き込み完了


        //public const byte FM_DEFAULT_LIFT_CUT = 0x00;		// デフォルト値　リフトカット機能フラグ
        //public const byte FM_DEFAULT_SNAP_ANGLE = 0x00;		// デフォルト値　スナップアングル機能フラグ
        //public const uint FM_DEFAULT_REPORT_RATE_L = 0x01F4;		// デフォルト値　レポートレート
        //public const uint FM_DEFAULT_LIFT_ADJUST_L = 0x0010;		// デフォルト値　リフトアジャスト
        //public const byte FM_DEFAULT_CPI_SYNC_FLAG = 0x03;		// デフォルト値　リフトカット機能フラグ
        //public const uint FM_DEFAULT_CPI_MODE1_X = 0x0320;		// デフォルト値　CPIモード1 X軸
        //public const uint FM_DEFAULT_CPI_MODE1_Y = 0x0320;		// デフォルト値　CPIモード1 Y軸
        //public const uint FM_DEFAULT_CPI_MODE2_X = 0x0640;		// デフォルト値　CPIモード2 X軸
        //public const uint FM_DEFAULT_CPI_MODE2_Y = 0x0640;		// デフォルト値　CPIモード2 Y軸
        //public const byte FM_DEFAULT_DEBOUNCE_TIME_ON = 0x10;		// デフォルト値　デバウンスタイムON
        //public const byte FM_DEFAULT_DEBOUNCE_TIME_OFF = 0x10;		// デフォルト値　デバウンスタイムOFF

        public FlashControl()
        {
            //flash_data = new byte[FM_TOTAL_SIZE];
            //sector_change_flag = new bool[FM_SECTOR_NUM];

            //read_address = 0;
            //read_size = 0;
            //usb_read_address = 0;
            //usb_read_size = 0;
            //read_buffer = new byte[FM_USB_READ_DATA_SIZE];

            //write_address = 0;
            //write_size = 0;
            //usb_write_address = 0;
            //usb_write_size = 0;
            //write_buffer = new byte[FM_USB_WRITE_DATA_SIZE];

            read_comp_flag = FM_READ_STATUS_NA;
            all_read_flag = FM_READ_STATUS_NA;
            read_flag_base_setting = FM_READ_STATUS_NA;
            read_flag_function_setting = FM_READ_STATUS_NA;
            read_flag_encoder_script_setting = FM_READ_STATUS_NA;
            read_flag_sw_function_setting = FM_READ_STATUS_NA;
            read_flag_script_info = FM_READ_STATUS_NA;
            read_flag_script_data = FM_READ_STATUS_NA;

            write_comp_flag = FM_WRITE_STATUS_NA;
            all_write_flag = FM_WRITE_STATUS_NA;
            write_flag_base_setting = FM_WRITE_STATUS_NA;
            write_flag_function_setting = FM_WRITE_STATUS_NA;
            write_flag_encoder_script_setting = FM_WRITE_STATUS_NA;
            write_flag_sw_function_setting = FM_WRITE_STATUS_NA;
            write_flag_script_info = FM_WRITE_STATUS_NA;
            write_flag_script_data = FM_WRITE_STATUS_NA;

            //s_lines = new string[0];
            //s_dups = new string[0];
            //al_lines = new ArrayList();
            //al_dups = new ArrayList();
            //num_of_frames = 0;
            ////return (string [])list.ToArray(typeof(string));


            myFlashTimer = new System.Timers.Timer();
            myFlashTimer.Enabled = true;
            myFlashTimer.AutoReset = true;
            myFlashTimer.Interval = 1000;
            myFlashTimer.Elapsed += new ElapsedEventHandler(FlashTimer_Tick);

            StartTimer();
        }
        ~FlashControl()
        {
            StopTimer();
        }

        private void FlashTimer_Tick(object source, ElapsedEventArgs e)
        {
            // TODO:
            Console.WriteLine(DateTime.Now);
        }

        public void StartTimer()
        {
            myFlashTimer.Start();
        }
        public void StopTimer()
        {
            myFlashTimer.Stop();
        }

        public void Set_Flash_Read_Status(uint read_type, byte set_status)
        {
            try
            {
                switch (read_type)
                {
                    case FM_READ_TYPE_BASE_SETTING:
                        if (set_status == FM_READ_STATUS_RQ 
                            && (read_flag_base_setting == FM_READ_STATUS_NA || read_flag_base_setting == FM_READ_STATUS_COMP))
                        {
                            read_flag_base_setting = FM_READ_STATUS_RQ;
                        }
                        if (set_status == FM_READ_STATUS_READING
                            && (read_flag_base_setting == FM_READ_STATUS_RQ))
                        {
                            read_flag_base_setting = FM_READ_STATUS_READING;
                        }
                        if (set_status == FM_READ_STATUS_COMP
                            && (read_flag_base_setting == FM_READ_STATUS_READING))
                        {
                            read_flag_base_setting = FM_READ_STATUS_COMP;
                        }
                        if (set_status == FM_READ_STATUS_NA)
                        {
                            read_flag_base_setting = FM_READ_STATUS_NA;
                        }
                        break;
                    case FM_READ_TYPE_FUNCTION_SETTING:
                        if (set_status == FM_READ_STATUS_RQ
                            && (read_flag_function_setting == FM_READ_STATUS_NA || read_flag_function_setting == FM_READ_STATUS_COMP))
                        {
                            read_flag_function_setting = FM_READ_STATUS_RQ;
                        }
                        if (set_status == FM_READ_STATUS_READING
                            && (read_flag_function_setting == FM_READ_STATUS_RQ))
                        {
                            read_flag_function_setting = FM_READ_STATUS_READING;
                        }
                        if (set_status == FM_READ_STATUS_COMP
                            && (read_flag_function_setting == FM_READ_STATUS_READING))
                        {
                            read_flag_function_setting = FM_READ_STATUS_COMP;
                        }
                        if (set_status == FM_READ_STATUS_NA)
                        {
                            read_flag_function_setting = FM_READ_STATUS_NA;
                        }
                        break;
                    case FM_READ_TYPE_ENCODER_SCRIPT_SETTING:
                        if (set_status == FM_READ_STATUS_RQ
                            && (read_flag_encoder_script_setting == FM_READ_STATUS_NA || read_flag_encoder_script_setting == FM_READ_STATUS_COMP))
                        {
                            read_flag_encoder_script_setting = FM_READ_STATUS_RQ;
                        }
                        if (set_status == FM_READ_STATUS_READING
                            && (read_flag_encoder_script_setting == FM_READ_STATUS_RQ))
                        {
                            read_flag_encoder_script_setting = FM_READ_STATUS_READING;
                        }
                        if (set_status == FM_READ_STATUS_COMP
                            && (read_flag_encoder_script_setting == FM_READ_STATUS_READING))
                        {
                            read_flag_encoder_script_setting = FM_READ_STATUS_COMP;
                        }
                        if (set_status == FM_READ_STATUS_NA)
                        {
                            read_flag_encoder_script_setting = FM_READ_STATUS_NA;
                        }
                        break;
                    case FM_READ_TYPE_SW_FUNCTION_SETTING:
                        if (set_status == FM_READ_STATUS_RQ
                            && (read_flag_sw_function_setting == FM_READ_STATUS_NA || read_flag_sw_function_setting == FM_READ_STATUS_COMP))
                        {
                            read_flag_sw_function_setting = FM_READ_STATUS_RQ;
                        }
                        if (set_status == FM_READ_STATUS_READING
                            && (read_flag_sw_function_setting == FM_READ_STATUS_RQ))
                        {
                            read_flag_sw_function_setting = FM_READ_STATUS_READING;
                        }
                        if (set_status == FM_READ_STATUS_COMP
                            && (read_flag_sw_function_setting == FM_READ_STATUS_READING))
                        {
                            read_flag_sw_function_setting = FM_READ_STATUS_COMP;
                        }
                        if (set_status == FM_READ_STATUS_NA)
                        {
                            read_flag_sw_function_setting = FM_READ_STATUS_NA;
                        }
                        break;
                    case FM_READ_TYPE_SCRIPT_INFO:
                        if (set_status == FM_READ_STATUS_RQ
                            && (read_flag_script_info == FM_READ_STATUS_NA || read_flag_script_info == FM_READ_STATUS_COMP))
                        {
                            read_flag_script_info = FM_READ_STATUS_RQ;
                        }
                        if (set_status == FM_READ_STATUS_READING
                            && (read_flag_script_info == FM_READ_STATUS_RQ))
                        {
                            read_flag_script_info = FM_READ_STATUS_READING;
                        }
                        if (set_status == FM_READ_STATUS_COMP
                            && (read_flag_script_info == FM_READ_STATUS_READING))
                        {
                            read_flag_script_info = FM_READ_STATUS_COMP;
                        }
                        if (set_status == FM_READ_STATUS_NA)
                        {
                            read_flag_script_info = FM_READ_STATUS_NA;
                        }
                        break;
                    case FM_READ_TYPE_SCRIPT_DATA:
                        if (set_status == FM_READ_STATUS_RQ
                            && (read_flag_script_data == FM_READ_STATUS_NA || read_flag_script_data == FM_READ_STATUS_COMP))
                        {
                            read_flag_script_data = FM_READ_STATUS_RQ;
                        }
                        if (set_status == FM_READ_STATUS_READING
                            && (read_flag_script_data == FM_READ_STATUS_RQ))
                        {
                            read_flag_script_data = FM_READ_STATUS_READING;
                        }
                        if (set_status == FM_READ_STATUS_COMP
                            && (read_flag_script_data == FM_READ_STATUS_READING))
                        {
                            read_flag_script_data = FM_READ_STATUS_COMP;
                        }
                        if (set_status == FM_READ_STATUS_NA)
                        {
                            read_flag_script_data = FM_READ_STATUS_NA;
                        }
                        break;
                }
            }
            catch
            {
            }
            return;
        }
        public byte Get_Flash_Read_Status(uint read_type)
        {
            byte b_ret = FM_READ_STATUS_NA;
            try
            {
                switch (read_type)
                {
                    case FM_READ_TYPE_BASE_SETTING:
                        b_ret = read_flag_base_setting;
                        break;
                    case FM_READ_TYPE_FUNCTION_SETTING:
                        b_ret = read_flag_function_setting;
                        break;
                    case FM_READ_TYPE_ENCODER_SCRIPT_SETTING:
                        b_ret = read_flag_encoder_script_setting;
                        break;
                    case FM_READ_TYPE_SW_FUNCTION_SETTING:
                        b_ret = read_flag_sw_function_setting;
                        break;
                    case FM_READ_TYPE_SCRIPT_INFO:
                        b_ret = read_flag_script_info;
                        break;
                    case FM_READ_TYPE_SCRIPT_DATA:
                        b_ret = read_flag_script_data;
                        break;
                }
            }
            catch
            {
            }
            return b_ret;
        }
        public void Set_Flash_Read_Status_Clear(uint read_type)
        {
            try
            {
                if ((read_type & FM_READ_TYPE_BASE_SETTING) != 0)
                {
                    read_flag_base_setting = FM_READ_STATUS_NA;
                }
                if ((read_type & FM_READ_TYPE_FUNCTION_SETTING) != 0)
                {
                    read_flag_function_setting = FM_READ_STATUS_NA;
                }
                if ((read_type & FM_READ_TYPE_ENCODER_SCRIPT_SETTING) != 0)
                {
                    read_flag_encoder_script_setting = FM_READ_STATUS_NA;
                }
                if ((read_type & FM_READ_TYPE_SW_FUNCTION_SETTING) != 0)
                {
                    read_flag_sw_function_setting = FM_READ_STATUS_NA;
                }
                if ((read_type & FM_READ_TYPE_SCRIPT_INFO) != 0)
                {
                    read_flag_script_info = FM_READ_STATUS_NA;
                }
                if ((read_type & FM_READ_TYPE_SCRIPT_DATA) != 0)
                {
                    read_flag_script_data = FM_READ_STATUS_NA;
                }
            }
            catch
            {
            }
        }

        public bool Set_Flash_Read_Base_Info(ref Form1.STR_BASE_INFO p_base_info, ref Form1.STR_BASE_MODE_INFOS p_base_mode_infos, ArrayList p_al)
        {
            bool b_ret = false;
            byte byte_temp = 0;
            //uint uint_temp = 0;
            try
            {
                if (p_al.Count == FM_BASE_INFO_AREA_SIZE)
                {
                    // Mode
                    byte_temp = (byte)p_al[FM_BASE_SETTING_ST_ADR + FM_OFSET_BASE_MODE];
                    if (Constants.MODE_NUM <= byte_temp)
                    {   // 異常値
                        byte_temp = 0;
                    }
                    p_base_info.mode = byte_temp;
                    // LED SLEEP
                    byte_temp = (byte)p_al[FM_BASE_SETTING_ST_ADR + FM_OFSET_BASE_LED_SLEEP];
                    if (Constants.LED_SLEEP_SETTING_NUM <= byte_temp)
                    {   // 異常値
                        byte_temp = 0;
                    }
                    p_base_info.led_sleep = byte_temp;
                    // LED点灯設定MODE
                    byte_temp = (byte)p_al[FM_BASE_SETTING_ST_ADR + FM_OFSET_BASE_LED_LIGHT_MODE];
                    if (Constants.LED_LIGHT_TYPE_MODE_SETTING_NUM <= byte_temp)
                    {   // 異常値
                        byte_temp = 0;
                    }
                    p_base_info.led_light_mode = byte_temp;
                    // LED点灯設定FUNC
                    byte_temp = (byte)p_al[FM_BASE_SETTING_ST_ADR + FM_OFSET_BASE_LED_LIGHT_FUNC];
                    if (Constants.LED_LIGHT_TYPE_FUNC_SETTING_NUM <= byte_temp)
                    {   // 異常値
                        byte_temp = 0;
                    }
                    p_base_info.led_light_func = byte_temp;
                    // LED消灯までの時間
                    byte_temp = (byte)p_al[FM_BASE_SETTING_ST_ADR + FM_OFSET_BASE_LED_OFF_TIME];
                    if (Constants.LED_LIGHT_OFF_MODE_TIME_MAX < byte_temp)
                    {   // 異常値
                        byte_temp = Constants.LED_LIGHT_OFF_MODE_TIME_MAX;
                    }
                    else if (byte_temp == 0)
                    {
                        byte_temp = 1;
                    }
                    p_base_info.led_off_time = byte_temp;
                    // エンコーダタイプマティック
                    byte_temp = (byte)p_al[FM_BASE_SETTING_ST_ADR + FM_OFSET_BASE_ENCODER_TYPEMATIC];
                    if (Constants.ENCODER_TYPEMATIC_SETTING_NUM <= byte_temp)
                    {   // 異常値
                        byte_temp = 0;
                    }
                    p_base_info.encoder_typematic = byte_temp;


                    for (int fi = 0; fi < p_base_mode_infos.base_mode_infos.Length; fi++)
                    {
                        for (int fj = 0; fj < Constants.BUTTON_NUM; fj++ )
                        {
                            byte_temp = (byte)p_al[FM_BASE_MODE_SETTING_ST_ADR + (FM_BASE_MODE_SETTING_SIZE * fi) + FM_OFSET_BASE_SW1_SCRIPT_NO + fj];
                            if (Constants.SCRIPT_NUM < byte_temp)
                            {   // 異常値
                                byte_temp = 0;
                            }
                            p_base_mode_infos.base_mode_infos[fi].sw_exe_script_no[fj] = byte_temp;

                            byte_temp = (byte)p_al[FM_BASE_MODE_SETTING_ST_ADR + (FM_BASE_MODE_SETTING_SIZE * fi) + FM_OFSET_BASE_SW1_SP_FUNC_NO + fj];
                            if (Constants.SP_FUNC_NUM < byte_temp)
                            {   // 異常値
                                byte_temp = 0;
                            }
                            p_base_mode_infos.base_mode_infos[fi].sw_sp_func_no[fj] = byte_temp;
                        }


                        p_base_mode_infos.base_mode_infos[fi].encoder_func_no = (byte)p_al[FM_BASE_MODE_SETTING_ST_ADR + (FM_BASE_MODE_SETTING_SIZE * fi) + FM_OFSET_BASE_ENCODER_DEFAULT];
                        if (Constants.FUNCTION_NUM <= p_base_mode_infos.base_mode_infos[fi].encoder_func_no)
                        {
                            p_base_mode_infos.base_mode_infos[fi].encoder_func_no = 0;
                        }
                        p_base_mode_infos.base_mode_infos[fi].LED_color_no = (byte)p_al[FM_BASE_MODE_SETTING_ST_ADR + (FM_BASE_MODE_SETTING_SIZE * fi) + FM_OFSET_BASE_LED_COLOR_NO];
                        if (Constants.LED_COLOR_DEFAULT_SET_NUM <= p_base_mode_infos.base_mode_infos[fi].LED_color_no)
                        {
                            p_base_mode_infos.base_mode_infos[fi].LED_color_no = 0;
                        }
                        p_base_mode_infos.base_mode_infos[fi].LED_color_detail_flag = (byte)p_al[FM_BASE_MODE_SETTING_ST_ADR + (FM_BASE_MODE_SETTING_SIZE * fi) + FM_OFSET_BASE_LED_DETAIL_FLAG];
                        if (Constants.LED_COLOR_DETAIL_OFF != p_base_mode_infos.base_mode_infos[fi].LED_color_detail_flag && Constants.LED_COLOR_DETAIL_ON != p_base_mode_infos.base_mode_infos[fi].LED_color_detail_flag)
                        {
                            p_base_mode_infos.base_mode_infos[fi].LED_color_detail_flag = Constants.LED_COLOR_DETAIL_OFF;
                        }
                        for (int fj = 0; fj < Constants.LED_RGB_COLOR_NUM; fj++ )
                        {
                            p_base_mode_infos.base_mode_infos[fi].LED_RGB_duty[fj] = (byte)p_al[FM_BASE_MODE_SETTING_ST_ADR + (FM_BASE_MODE_SETTING_SIZE * fi) + FM_OFSET_BASE_LED_RGB_DUTY + fj];
                            if (0xFF == p_base_mode_infos.base_mode_infos[fi].LED_RGB_duty[fj])
                            {
                                p_base_mode_infos.base_mode_infos[fi].LED_RGB_duty[fj] = 0;
                            }
                            else if (my_LED_Duty_Max[fj] < p_base_mode_infos.base_mode_infos[fi].LED_RGB_duty[fj])
                            {
                                p_base_mode_infos.base_mode_infos[fi].LED_RGB_duty[fj] = my_LED_Duty_Max[fj];
                            }
                        }
                        p_base_mode_infos.base_mode_infos[fi].LED_brightness_level = (byte)p_al[FM_BASE_MODE_SETTING_ST_ADR + (FM_BASE_MODE_SETTING_SIZE * fi) + FM_OFSET_BASE_LED_BRIGHTNESS_LEVEL];
                        if (Constants.LED_BRIGHTNESS_LEVEL_SET_NUM <= p_base_mode_infos.base_mode_infos[fi].LED_brightness_level)
                        {
                            p_base_mode_infos.base_mode_infos[fi].LED_brightness_level = Constants.LED_BRIGHTNESS_LEVEL_NORMAL;
                        }
                    }
                }
            }
            catch
            {
            }
            return b_ret;
        }
        public bool Set_Flash_Read_Func_Info(ref Form1.STR_FUNC_DATAS p_func_datas, ArrayList p_al, ref Form1.STR_APP_SW_DATAS p_app_sw_datas, ref Form1.STR_APP_FUNC_DATAS p_app_func_datas)
        {
            bool b_ret = false;
            byte[] by_name;
            try
            {
                if (p_al.Count == FM_FUNCTION_SETTING_AREA_SIZE)
                {
                    // 各モードの機能設定データをセット
                    for (int mode_idx = 0; mode_idx < Constants.MODE_NUM; mode_idx++)
                    {
                        for (int func_idx = 0; func_idx < Constants.FUNCTION_NUM; func_idx++)
                        {
                            // 機能設定情報
                            for (int cw_ccw_idx = 0; cw_ccw_idx < Constants.CW_CCW_NUM; cw_ccw_idx++)
                            {
                                for (int data_idx = 0; data_idx < Constants.FUNCTION_CWCCW_DATA_LEN; data_idx++)
                                {
                                    p_func_datas.func_datas[mode_idx].func_data[func_idx].cw_ccw_data[cw_ccw_idx, data_idx] = (byte)p_al[(FM_FUNCTION_SETTING_ST_ADR - FM_ADR_FUNCTION_SETTING) + (FM_FUNCTION_SETTING_SIZE_MODE * mode_idx) + (FM_FUNCTION_SETTING_SIZE_FUNC * func_idx) + FM_OFSET_FUNCTION_CW_DATA_TOP + (FM_OFSET_FUNCTION_DATA_SIZE * cw_ccw_idx) + data_idx];
                                }

                                // 設定タイプの範囲チェック
                                if (p_func_datas.func_datas[mode_idx].func_data[func_idx].cw_ccw_data[cw_ccw_idx, Constants.DEVICE_DATA_SET_TYPE_IDX] < Constants.SET_TYPE_MIN || Constants.SET_TYPE_MAX < p_func_datas.func_datas[mode_idx].func_data[func_idx].cw_ccw_data[cw_ccw_idx, Constants.DEVICE_DATA_SET_TYPE_IDX])
                                {
                                    p_func_datas.func_datas[mode_idx].func_data[func_idx].cw_ccw_data[cw_ccw_idx, Constants.DEVICE_DATA_SET_TYPE_IDX] = Constants.SET_TYPE_NONE;
                                    for (int data_idx = 1; data_idx < Constants.FUNCTION_CWCCW_DATA_LEN; data_idx++)
                                    {
                                        p_func_datas.func_datas[mode_idx].func_data[func_idx].cw_ccw_data[cw_ccw_idx, data_idx] = 0;
                                    }
                                }
                                // 感度設定範囲チェック
                                if (p_func_datas.func_datas[mode_idx].func_data[func_idx].cw_ccw_data[cw_ccw_idx, Constants.DEVICE_DATA_SENSE_IDX] < Constants.SENSITIVITY_MIN || Constants.SENSITIVITY_MAX < p_func_datas.func_datas[mode_idx].func_data[func_idx].cw_ccw_data[cw_ccw_idx, Constants.DEVICE_DATA_SENSE_IDX])
                                {
                                    p_func_datas.func_datas[mode_idx].func_data[func_idx].cw_ccw_data[cw_ccw_idx, Constants.DEVICE_DATA_SENSE_IDX] = Constants.SENSITIVITY_DEFAULT;
                                }
                            }

                            p_func_datas.func_datas[mode_idx].func_data[func_idx].LED_color_no = (byte)p_al[(FM_FUNCTION_SETTING_ST_ADR - FM_ADR_FUNCTION_SETTING) + (FM_FUNCTION_SETTING_SIZE_MODE * mode_idx) + (FM_FUNCTION_SETTING_SIZE_FUNC * func_idx) + FM_OFSET_FUNCTION_LED_COLOR_NO];
                            if (Constants.LED_COLOR_DEFAULT_SET_NUM <= p_func_datas.func_datas[mode_idx].func_data[func_idx].LED_color_no)
                            {
                                p_func_datas.func_datas[mode_idx].func_data[func_idx].LED_color_no = 0;
                            }
                            p_func_datas.func_datas[mode_idx].func_data[func_idx].LED_color_detail_flag = (byte)p_al[(FM_FUNCTION_SETTING_ST_ADR - FM_ADR_FUNCTION_SETTING) + (FM_FUNCTION_SETTING_SIZE_MODE * mode_idx) + (FM_FUNCTION_SETTING_SIZE_FUNC * func_idx) + FM_OFSET_FUNCTION_LED_DETAIL_FLAG];
                            if (p_func_datas.func_datas[mode_idx].func_data[func_idx].LED_color_detail_flag != 0 && p_func_datas.func_datas[mode_idx].func_data[func_idx].LED_color_detail_flag != 1)
                            {
                                p_func_datas.func_datas[mode_idx].func_data[func_idx].LED_color_detail_flag = 0;
                            }
                            for (int data_idx = 0; data_idx < Constants.LED_RGB_COLOR_NUM; data_idx++)
                            {
                                p_func_datas.func_datas[mode_idx].func_data[func_idx].LED_RGB_duty[data_idx] = (byte)p_al[(FM_FUNCTION_SETTING_ST_ADR - FM_ADR_FUNCTION_SETTING) + (FM_FUNCTION_SETTING_SIZE_MODE * mode_idx) + (FM_FUNCTION_SETTING_SIZE_FUNC * func_idx) + FM_OFSET_FUNCTION_LED_RGB_DUTY_TOP + data_idx];
                                if (0xFF == p_func_datas.func_datas[mode_idx].func_data[func_idx].LED_RGB_duty[data_idx])
                                {
                                    p_func_datas.func_datas[mode_idx].func_data[func_idx].LED_RGB_duty[data_idx] = 0;
                                }
                                else if (my_LED_Duty_Max[data_idx] < p_func_datas.func_datas[mode_idx].func_data[func_idx].LED_RGB_duty[data_idx])
                                {
                                    p_func_datas.func_datas[mode_idx].func_data[func_idx].LED_RGB_duty[data_idx] = my_LED_Duty_Max[data_idx];
                                }
                            }
                            p_func_datas.func_datas[mode_idx].func_data[func_idx].LED_brightness_level = (byte)p_al[(FM_FUNCTION_SETTING_ST_ADR - FM_ADR_FUNCTION_SETTING) + (FM_FUNCTION_SETTING_SIZE_MODE * mode_idx) + (FM_FUNCTION_SETTING_SIZE_FUNC * func_idx) + FM_OFSET_FUNCTION_LED_BRIGHTNESS_LEVEL];
                            if (Constants.LED_BRIGHTNESS_LEVEL_SET_NUM <= p_func_datas.func_datas[mode_idx].func_data[func_idx].LED_brightness_level)
                            {
                                p_func_datas.func_datas[mode_idx].func_data[func_idx].LED_brightness_level = Constants.LED_BRIGHTNESS_LEVEL_NORMAL;
                            }


                            // 機能名称情報
                            p_func_datas.func_datas[mode_idx].func_data[func_idx].func_name_size = (byte)p_al[(FM_FUNCTION_NAME_ST_ADR - FM_ADR_FUNCTION_SETTING) + (FM_FUNCTION_NAME_ST_SIZE * ((Constants.FUNCTION_NUM * mode_idx) + func_idx)) + FM_OFSET_FUNCTION_NAME_SIZE];
                            //文字列長チェック
                            if (p_func_datas.func_datas[mode_idx].func_data[func_idx].func_name_size > FM_FUNCTION_NAME_MAX_SIZE)
                            {
                                p_func_datas.func_datas[mode_idx].func_data[func_idx].func_name_size = 0;
                            }
                            //文字列設定
                            if (p_func_datas.func_datas[mode_idx].func_data[func_idx].func_name_size > 0)
                            {   //文字列あり
                                by_name = new byte[p_func_datas.func_datas[mode_idx].func_data[func_idx].func_name_size];
                                for (int fj = 0; fj < p_func_datas.func_datas[mode_idx].func_data[func_idx].func_name_size; fj++)
                                {
                                    by_name[fj] = (byte)p_al[(FM_FUNCTION_NAME_ST_ADR - FM_ADR_FUNCTION_SETTING) + (FM_FUNCTION_NAME_ST_SIZE * ((Constants.FUNCTION_NUM * mode_idx) + func_idx)) + FM_OFSET_FUNCTION_NAME_DATA + fj];
                                }
                                p_func_datas.func_datas[mode_idx].func_data[func_idx].func_name = System.Text.Encoding.Unicode.GetString(by_name);
                                //by_name = System.Text.Encoding.Unicode.GetBytes(p_pattern_datas.pattern_datas[fi].pattern_name);
                            }
                            else
                            {   // 文字列なし
                                p_func_datas.func_datas[mode_idx].func_data[func_idx].func_name = RevOmate.Properties.Resources.FUNCTION_NAME_UNDEFINE;
                            }
                        }
                    }


                    // アプリケーション用データをセット
                    int tmp_address = 0;
                    // SW
                    for (int mode_idx = 0; mode_idx < Constants.MODE_NUM; mode_idx++)
                    {
                        for (int sw_idx = 0; sw_idx < Constants.BUTTON_NUM; sw_idx++)
                        {
                            for (int fi = 0; fi < Constants.APP_SW_DATA_SELECT_DATA_LEN; fi++)
                            {
                                tmp_address = (FM_ADR_APP_SW_SETTING - FM_ADR_FUNCTION_SETTING) + (FM_APP_SW_SETTING_SIZE_MODE * mode_idx) + (FM_OFSET_APP_SETTING_SW1_DATA_TOP + (FM_APP_SW_SETTING_SIZE_DATA * sw_idx) + fi);
                                p_app_sw_datas.mode[mode_idx].app_data[sw_idx].select_data[fi] = (byte)p_al[(FM_ADR_APP_SW_SETTING - FM_ADR_FUNCTION_SETTING) + (FM_APP_SW_SETTING_SIZE_MODE * mode_idx) + (FM_OFSET_APP_SETTING_SW1_DATA_TOP + (FM_APP_SW_SETTING_SIZE_DATA * sw_idx) + fi)];
                                if (p_app_sw_datas.mode[mode_idx].app_data[sw_idx].select_data[fi] == 0xFF)
                                {
                                    p_app_sw_datas.mode[mode_idx].app_data[sw_idx].select_data[fi] = 0;
                                }
                            }
                            for (int fi = 0; fi < Constants.APP_SW_DATA_DATA_LEN; fi++)
                            {
                                tmp_address = (FM_ADR_APP_SW_SETTING - FM_ADR_FUNCTION_SETTING) + (FM_APP_SW_SETTING_SIZE_MODE * mode_idx) + (FM_OFSET_APP_SETTING_SW1_DATA_TOP + (FM_APP_SW_SETTING_SIZE_DATA * sw_idx) + Constants.APP_SW_DATA_SELECT_DATA_LEN + fi);
                                p_app_sw_datas.mode[mode_idx].app_data[sw_idx].data[fi] = (byte)p_al[(FM_ADR_APP_SW_SETTING - FM_ADR_FUNCTION_SETTING) + (FM_APP_SW_SETTING_SIZE_MODE * mode_idx) + (FM_OFSET_APP_SETTING_SW1_DATA_TOP + (FM_APP_SW_SETTING_SIZE_DATA * sw_idx) + Constants.APP_SW_DATA_SELECT_DATA_LEN + fi)];
                                if (p_app_sw_datas.mode[mode_idx].app_data[sw_idx].data[fi] == 0xFF)
                                {
                                    p_app_sw_datas.mode[mode_idx].app_data[sw_idx].data[fi] = 0;
                                }
                            }
                        }
                    }
                    // FUNC
                    for (int mode_idx = 0; mode_idx < Constants.MODE_NUM; mode_idx++)
                    {
                        for (int func_idx = 0; func_idx < Constants.FUNCTION_NUM; func_idx++)
                        {
                            for (int cwccw_idx = 0; cwccw_idx < Constants.CW_CCW_NUM; cwccw_idx++)
                            {
                                for (int fi = 0; fi < Constants.APP_FUNC_DATA_SELECT_DATA_LEN; fi++)
                                {
                                    tmp_address = (FM_ADR_APP_FUNC_SETTING - FM_ADR_FUNCTION_SETTING) + (FM_APP_FUNC_SETTING_SIZE_MODE * mode_idx) + (FM_APP_FUNC_SETTING_SIZE_FUNC * func_idx) + (FM_APP_FUNC_SETTING_SIZE_DATA * cwccw_idx) + fi;
                                    p_app_func_datas.mode[mode_idx].func[func_idx].cwccw[cwccw_idx].app_data.select_data[fi] = (byte)p_al[(FM_ADR_APP_FUNC_SETTING - FM_ADR_FUNCTION_SETTING) + (FM_APP_FUNC_SETTING_SIZE_MODE * mode_idx) + (FM_APP_FUNC_SETTING_SIZE_FUNC * func_idx) + (FM_APP_FUNC_SETTING_SIZE_DATA * cwccw_idx) + fi];
                                    if (p_app_func_datas.mode[mode_idx].func[func_idx].cwccw[cwccw_idx].app_data.select_data[fi] == 0xFF)
                                    {
                                        p_app_func_datas.mode[mode_idx].func[func_idx].cwccw[cwccw_idx].app_data.select_data[fi] = 0;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            return b_ret;
        }
        public bool Set_Flash_Read_Encoder_Script_Info(ref Form1.STR_ENCODER_SCRIPT_DATAS p_encoder_script_datas, ArrayList p_al)
        {
            bool b_ret = false;
            try
            {
                if (p_al.Count == FM_ENCODER_SCRIPT_SETTING_AREA_SIZE)
                {
                    // エンコーダスクリプト機能設定データをセット
                    for (int idx = 0; idx < Constants.ENCODER_SCRIPT_NUM; idx++)
                    {
                        p_encoder_script_datas.encoder_script_datas[idx].rec_num = (byte)p_al[(FM_ENCODER_SCRIPT_ST_SIZE * idx) + FM_OFSET_ENCODER_SCRIPT_SCRIPT_NUM_TOP];
                        if (Constants.ENCODER_SCRIPT_SCRIPTSET_MAX_NUM < p_encoder_script_datas.encoder_script_datas[idx].rec_num)
                        {
                            p_encoder_script_datas.encoder_script_datas[idx].rec_num = 0;
                        }
                        p_encoder_script_datas.encoder_script_datas[idx].loop_flag = (byte)p_al[(FM_ENCODER_SCRIPT_ST_SIZE * idx) + FM_OFSET_ENCODER_SCRIPT_LOOP_SET_TOP];
                        if (Constants.ENCODER_SCRIPT_LOOP_SET_NUM <= p_encoder_script_datas.encoder_script_datas[idx].loop_flag)
                        {
                            p_encoder_script_datas.encoder_script_datas[idx].loop_flag = Constants.ENCODER_SCRIPT_LOOP_SET_NONE;
                        }

                        for (int script_no_idx = 0; script_no_idx < Constants.ENCODER_SCRIPT_SCRIPTSET_MAX_NUM; script_no_idx++ )
                        {
                            p_encoder_script_datas.encoder_script_datas[idx].script_no[script_no_idx] = (byte)p_al[(FM_ENCODER_SCRIPT_ST_SIZE * idx) + FM_OFSET_ENCODER_SCRIPT_DATA_TOP + script_no_idx];
                            if (Constants.SCRIPT_NUM < p_encoder_script_datas.encoder_script_datas[idx].script_no[script_no_idx])
                            {
                                p_encoder_script_datas.encoder_script_datas[idx].script_no[script_no_idx] = 0;
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            return b_ret;
        }
        public bool Set_Flash_Read_SW_Func_Info(ref Form1.STR_SW_FUNC_DATAS p_sw_func_datas, ArrayList p_al)
        {
            bool b_ret = false;
            try
            {
                if (p_al.Count == FM_SW_FUNCTION_SETTING_AREA_SIZE)
                {
                    // 各モードのSW機能設定データをセット
                    for (int mode_idx = 0; mode_idx < Constants.MODE_NUM; mode_idx++)
                    {
                        for (int sw_idx = 0; sw_idx < Constants.BUTTON_NUM; sw_idx++)
                        {
                            for (int data_idx = 0; data_idx < Constants.SW_FUNCTION_DATA_LEN; data_idx++)
                            {
                                p_sw_func_datas.sw_func_datas[mode_idx].sw_func_data[sw_idx].sw_data[data_idx] = (byte)p_al[(FM_SW_FUNCTION_SETTING_SIZE_MODE * mode_idx) + (FM_SW_FUNCTION_SETTING_SIZE_FUNC * sw_idx) + data_idx];
                            }

                            // 設定タイプの範囲チェック
                            if (p_sw_func_datas.sw_func_datas[mode_idx].sw_func_data[sw_idx].sw_data[Constants.DEVICE_DATA_SET_TYPE_IDX] < Constants.SET_TYPE_MIN || Constants.SET_TYPE_MAX < p_sw_func_datas.sw_func_datas[mode_idx].sw_func_data[sw_idx].sw_data[Constants.DEVICE_DATA_SET_TYPE_IDX])
                            {
                                p_sw_func_datas.sw_func_datas[mode_idx].sw_func_data[sw_idx].sw_data[Constants.DEVICE_DATA_SET_TYPE_IDX] = Constants.SET_TYPE_NONE;
                                for (int data_idx = 1; data_idx < Constants.SW_FUNCTION_DATA_LEN; data_idx++)
                                {
                                    p_sw_func_datas.sw_func_datas[mode_idx].sw_func_data[sw_idx].sw_data[data_idx] = 0;
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            return b_ret;
        }
        public bool Set_Flash_Read_Script_Info_Data(ref Form1.STR_SCRIPT_INFO p_script_info, ref Form1.STR_SCRIPT_INFO_DATAS p_script_info_datas, ArrayList p_al)
        {
            bool b_ret = false;
            byte[] by_name;
            try
            {
                if (p_al.Count == FM_SCRIPT_INFO_AREA_SIZE)
                {
                    p_script_info.check_sum = (byte)p_al[FM_OFSET_SCRIPT_CHK_SUM_H];
                    p_script_info.check_sum = (p_script_info.check_sum << 8) | (byte)p_al[FM_OFSET_SCRIPT_CHK_SUM_L];
                    p_script_info.Record_Num = (byte)p_al[FM_OFSET_SCRIPT_REC_NUM];
                    if (p_script_info.Record_Num > Constants.SCRIPT_NUM)
                    {
                        p_script_info.Record_Num = 0;
                    }
                    p_script_info.Total_Size = (byte)p_al[FM_OFSET_SCRIPT_TOTAL_SIZE_L3];
                    p_script_info.Total_Size = (p_script_info.Total_Size << 8) | (byte)p_al[FM_OFSET_SCRIPT_TOTAL_SIZE_L2];
                    p_script_info.Total_Size = (p_script_info.Total_Size << 8) | (byte)p_al[FM_OFSET_SCRIPT_TOTAL_SIZE_L1];
                    p_script_info.Total_Size = (p_script_info.Total_Size << 8) | (byte)p_al[FM_OFSET_SCRIPT_TOTAL_SIZE_L0];

                    for (int fi = 0; fi < Constants.SCRIPT_NUM; fi++ )
                    {
                        p_script_info_datas.Script_Info_Datas[fi].Recode_Address = (byte)p_al[FM_SCRIPT_INFO_DATA_ST_ADR + (FM_SCRIPT_INFO_DATA_SIZE * fi) + FM_OFSET_SCRIPT_REC_ADR + 3];
                        p_script_info_datas.Script_Info_Datas[fi].Recode_Address = (p_script_info_datas.Script_Info_Datas[fi].Recode_Address << 8) | (byte)p_al[FM_SCRIPT_INFO_DATA_ST_ADR + (FM_SCRIPT_INFO_DATA_SIZE * fi) + FM_OFSET_SCRIPT_REC_ADR + 2];
                        p_script_info_datas.Script_Info_Datas[fi].Recode_Address = (p_script_info_datas.Script_Info_Datas[fi].Recode_Address << 8) | (byte)p_al[FM_SCRIPT_INFO_DATA_ST_ADR + (FM_SCRIPT_INFO_DATA_SIZE * fi) + FM_OFSET_SCRIPT_REC_ADR + 1];
                        p_script_info_datas.Script_Info_Datas[fi].Recode_Address = (p_script_info_datas.Script_Info_Datas[fi].Recode_Address << 8) | (byte)p_al[FM_SCRIPT_INFO_DATA_ST_ADR + (FM_SCRIPT_INFO_DATA_SIZE * fi) + FM_OFSET_SCRIPT_REC_ADR + 0];
                        if (p_script_info_datas.Script_Info_Datas[fi].Recode_Address == -1)
                        {
                            p_script_info_datas.Script_Info_Datas[fi].Recode_Address = 0;
                        }
                        p_script_info_datas.Script_Info_Datas[fi].Script_Size = (byte)p_al[FM_SCRIPT_INFO_DATA_ST_ADR + (FM_SCRIPT_INFO_DATA_SIZE * fi) + FM_OFSET_SCRIPT_DATA_SIZE + 3];
                        p_script_info_datas.Script_Info_Datas[fi].Script_Size = (p_script_info_datas.Script_Info_Datas[fi].Script_Size << 8) | (byte)p_al[FM_SCRIPT_INFO_DATA_ST_ADR + (FM_SCRIPT_INFO_DATA_SIZE * fi) + FM_OFSET_SCRIPT_DATA_SIZE + 2];
                        p_script_info_datas.Script_Info_Datas[fi].Script_Size = (p_script_info_datas.Script_Info_Datas[fi].Script_Size << 8) | (byte)p_al[FM_SCRIPT_INFO_DATA_ST_ADR + (FM_SCRIPT_INFO_DATA_SIZE * fi) + FM_OFSET_SCRIPT_DATA_SIZE + 1];
                        p_script_info_datas.Script_Info_Datas[fi].Script_Size = (p_script_info_datas.Script_Info_Datas[fi].Script_Size << 8) | (byte)p_al[FM_SCRIPT_INFO_DATA_ST_ADR + (FM_SCRIPT_INFO_DATA_SIZE * fi) + FM_OFSET_SCRIPT_DATA_SIZE + 0];
                        if (p_script_info_datas.Script_Info_Datas[fi].Script_Size == -1)
                        {
                            p_script_info_datas.Script_Info_Datas[fi].Script_Size = 0;
                        }
                        p_script_info_datas.Script_Info_Datas[fi].Script_Mode = (byte)p_al[FM_SCRIPT_INFO_DATA_ST_ADR + (FM_SCRIPT_INFO_DATA_SIZE * fi) + FM_OFSET_SCRIPT_MODE];
                        
                        p_script_info_datas.Script_Info_Datas[fi].Name_Size = (byte)p_al[FM_SCRIPT_INFO_DATA_ST_ADR + (FM_SCRIPT_INFO_DATA_SIZE * fi) + FM_OFSET_SCRIPT_NAME_SIZE];
                        //文字列長チェック
                        if (p_script_info_datas.Script_Info_Datas[fi].Name_Size > FM_SCRIPT_INFO_DATA_NAME_SIZE)
                        {
                            p_script_info_datas.Script_Info_Datas[fi].Name_Size = 0;
                        }
                        //文字列設定
                        if (p_script_info_datas.Script_Info_Datas[fi].Name_Size > 0)
                        {   //文字列あり
                            by_name = new byte[p_script_info_datas.Script_Info_Datas[fi].Name_Size];
                            for (int fj = 0; fj < p_script_info_datas.Script_Info_Datas[fi].Name_Size; fj++)
                            {
                                by_name[fj] = (byte)p_al[FM_SCRIPT_INFO_DATA_ST_ADR + (FM_SCRIPT_INFO_DATA_SIZE * fi) + FM_OFSET_SCRIPT_NAME_DATA + fj];
                            }
                            p_script_info_datas.Script_Info_Datas[fi].Name = System.Text.Encoding.Unicode.GetString(by_name);
                            //by_name = System.Text.Encoding.Unicode.GetBytes(p_script_info_datas.Script_Info_Datas[fi].Name);
                        }
                        else
                        {   // 文字列なし
                            p_script_info_datas.Script_Info_Datas[fi].Name = "";
                        }
                    }
                }
            }
            catch
            {
            }
            return b_ret;
        }



        public void Set_Flash_Write_Status(uint write_type, byte set_status)
        {
            try
            {
                switch (write_type)
                {
                    case FM_WRITE_TYPE_BASE_SETTING:
                        if (set_status == FM_WRITE_STATUS_RQ
                            && (write_flag_base_setting == FM_WRITE_STATUS_NA || write_flag_base_setting == FM_WRITE_STATUS_COMP))
                        {
                            write_flag_base_setting = FM_WRITE_STATUS_RQ;
                        }
                        if (set_status == FM_WRITE_STATUS_WRITTING
                            && (write_flag_base_setting == FM_WRITE_STATUS_RQ))
                        {
                            write_flag_base_setting = FM_WRITE_STATUS_WRITTING;
                        }
                        if (set_status == FM_WRITE_STATUS_COMP
                            && (write_flag_base_setting == FM_WRITE_STATUS_WRITTING))
                        {
                            write_flag_base_setting = FM_WRITE_STATUS_COMP;
                        }
                        if (set_status == FM_WRITE_STATUS_NA)
                        {
                            write_flag_base_setting = FM_WRITE_STATUS_NA;
                        }
                        break;
                    case FM_WRITE_TYPE_FUNCTION_SETTING:
                        if (set_status == FM_WRITE_STATUS_RQ
                            && (write_flag_function_setting == FM_WRITE_STATUS_NA || write_flag_function_setting == FM_WRITE_STATUS_COMP))
                        {
                            write_flag_function_setting = FM_WRITE_STATUS_RQ;
                        }
                        if (set_status == FM_WRITE_STATUS_WRITTING
                            && (write_flag_function_setting == FM_WRITE_STATUS_RQ))
                        {
                            write_flag_function_setting = FM_WRITE_STATUS_WRITTING;
                        }
                        if (set_status == FM_WRITE_STATUS_COMP
                            && (write_flag_function_setting == FM_WRITE_STATUS_WRITTING))
                        {
                            write_flag_function_setting = FM_WRITE_STATUS_COMP;
                        }
                        if (set_status == FM_WRITE_STATUS_NA)
                        {
                            write_flag_function_setting = FM_WRITE_STATUS_NA;
                        }
                        break;
                    case FM_WRITE_TYPE_ENCODER_SCRIPT_SETTING:
                        if (set_status == FM_WRITE_STATUS_RQ
                            && (write_flag_encoder_script_setting == FM_WRITE_STATUS_NA || write_flag_encoder_script_setting == FM_WRITE_STATUS_COMP))
                        {
                            write_flag_encoder_script_setting = FM_WRITE_STATUS_RQ;
                        }
                        if (set_status == FM_WRITE_STATUS_WRITTING
                            && (write_flag_encoder_script_setting == FM_WRITE_STATUS_RQ))
                        {
                            write_flag_encoder_script_setting = FM_WRITE_STATUS_WRITTING;
                        }
                        if (set_status == FM_WRITE_STATUS_COMP
                            && (write_flag_encoder_script_setting == FM_WRITE_STATUS_WRITTING))
                        {
                            write_flag_encoder_script_setting = FM_WRITE_STATUS_COMP;
                        }
                        if (set_status == FM_WRITE_STATUS_NA)
                        {
                            write_flag_encoder_script_setting = FM_WRITE_STATUS_NA;
                        }
                        break;
                    case FM_WRITE_TYPE_SW_FUNCTION_SETTING:
                        if (set_status == FM_WRITE_STATUS_RQ
                            && (write_flag_sw_function_setting == FM_WRITE_STATUS_NA || write_flag_sw_function_setting == FM_WRITE_STATUS_COMP))
                        {
                            write_flag_sw_function_setting = FM_WRITE_STATUS_RQ;
                        }
                        if (set_status == FM_WRITE_STATUS_WRITTING
                            && (write_flag_sw_function_setting == FM_WRITE_STATUS_RQ))
                        {
                            write_flag_sw_function_setting = FM_WRITE_STATUS_WRITTING;
                        }
                        if (set_status == FM_WRITE_STATUS_COMP
                            && (write_flag_sw_function_setting == FM_WRITE_STATUS_WRITTING))
                        {
                            write_flag_sw_function_setting = FM_WRITE_STATUS_COMP;
                        }
                        if (set_status == FM_WRITE_STATUS_NA)
                        {
                            write_flag_sw_function_setting = FM_WRITE_STATUS_NA;
                        }
                        break;
                    case FM_WRITE_TYPE_SCRIPT_INFO:
                        if (set_status == FM_WRITE_STATUS_RQ
                            && (write_flag_script_info == FM_WRITE_STATUS_NA || write_flag_script_info == FM_WRITE_STATUS_COMP))
                        {
                            write_flag_script_info = FM_WRITE_STATUS_RQ;
                        }
                        if (set_status == FM_WRITE_STATUS_WRITTING
                            && (write_flag_script_info == FM_WRITE_STATUS_RQ))
                        {
                            write_flag_script_info = FM_WRITE_STATUS_WRITTING;
                        }
                        if (set_status == FM_WRITE_STATUS_COMP
                            && (write_flag_script_info == FM_WRITE_STATUS_WRITTING))
                        {
                            write_flag_script_info = FM_WRITE_STATUS_COMP;
                        }
                        if (set_status == FM_WRITE_STATUS_NA)
                        {
                            write_flag_script_info = FM_WRITE_STATUS_NA;
                        }
                        break;
                    case FM_WRITE_TYPE_SCRIPT_DATA:
                        if (set_status == FM_WRITE_STATUS_RQ
                            && (write_flag_script_data == FM_WRITE_STATUS_NA || write_flag_script_data == FM_WRITE_STATUS_COMP))
                        {
                            write_flag_script_data = FM_WRITE_STATUS_RQ;
                        }
                        if (set_status == FM_WRITE_STATUS_WRITTING
                            && (write_flag_script_data == FM_WRITE_STATUS_RQ))
                        {
                            write_flag_script_data = FM_WRITE_STATUS_WRITTING;
                        }
                        if (set_status == FM_WRITE_STATUS_COMP
                            && (write_flag_script_data == FM_WRITE_STATUS_WRITTING))
                        {
                            write_flag_script_data = FM_WRITE_STATUS_COMP;
                        }
                        if (set_status == FM_WRITE_STATUS_NA)
                        {
                            write_flag_script_data = FM_WRITE_STATUS_NA;
                        }
                        break;
                }
            }
            catch
            {
            }
            return;
        }
        public byte Get_Flash_Write_Status(uint write_type)
        {
            byte b_ret = FM_WRITE_STATUS_NA;
            try
            {
                switch (write_type)
                {
                    case FM_WRITE_TYPE_BASE_SETTING:
                        b_ret = write_flag_base_setting;
                        break;
                    case FM_WRITE_TYPE_FUNCTION_SETTING:
                        b_ret = write_flag_function_setting;
                        break;
                    case FM_WRITE_TYPE_ENCODER_SCRIPT_SETTING:
                        b_ret = write_flag_encoder_script_setting;
                        break;
                    case FM_WRITE_TYPE_SW_FUNCTION_SETTING:
                        b_ret = write_flag_sw_function_setting;
                        break;
                    case FM_WRITE_TYPE_SCRIPT_INFO:
                        b_ret = write_flag_script_info;
                        break;
                    case FM_WRITE_TYPE_SCRIPT_DATA:
                        b_ret = write_flag_script_data;
                        break;
                }
            }
            catch
            {
            }
            return b_ret;
        }
        public void Set_Flash_Write_Status_Clear(uint write_type)
        {
            try
            {
                if ((write_type & FM_WRITE_TYPE_BASE_SETTING) != 0)
                {
                    write_flag_base_setting = FM_WRITE_STATUS_NA;
                }
                if ((write_type & FM_WRITE_TYPE_FUNCTION_SETTING) != 0)
                {
                    write_flag_function_setting = FM_WRITE_STATUS_NA;
                }
                if ((write_type & FM_WRITE_TYPE_ENCODER_SCRIPT_SETTING) != 0)
                {
                    write_flag_encoder_script_setting = FM_WRITE_STATUS_NA;
                }
                if ((write_type & FM_WRITE_TYPE_SW_FUNCTION_SETTING) != 0)
                {
                    write_flag_sw_function_setting = FM_WRITE_STATUS_NA;
                }
                if ((write_type & FM_WRITE_TYPE_SCRIPT_INFO) != 0)
                {
                    write_flag_script_info = FM_WRITE_STATUS_NA;
                }
                if ((write_type & FM_WRITE_TYPE_SCRIPT_DATA) != 0)
                {
                    write_flag_script_data = FM_WRITE_STATUS_NA;
                }
            }
            catch
            {
            }
        }

        public bool Get_Flash_Write_Base_Info(Form1.STR_BASE_INFO p_base_info, Form1.STR_BASE_MODE_INFOS p_base_mode_infos, ref ArrayList p_al)
        {
            bool b_ret = false;
            try
            {
                // バッファ初期化
                p_al.Clear();

                byte byte_temp = 0xFF;
                for (int fi = 0; fi < FM_BASE_INFO_AREA_SIZE; fi++)
                {
                    p_al.Add(byte_temp);
                }

                p_al[FM_BASE_SETTING_ST_ADR + FM_OFSET_BASE_MODE] = (byte)(p_base_info.mode);
                p_al[FM_BASE_SETTING_ST_ADR + FM_OFSET_BASE_LED_SLEEP] = (byte)(p_base_info.led_sleep);
                p_al[FM_BASE_SETTING_ST_ADR + FM_OFSET_BASE_LED_LIGHT_MODE] = (byte)(p_base_info.led_light_mode);
                p_al[FM_BASE_SETTING_ST_ADR + FM_OFSET_BASE_LED_LIGHT_FUNC] = (byte)(p_base_info.led_light_func);
                p_al[FM_BASE_SETTING_ST_ADR + FM_OFSET_BASE_LED_OFF_TIME] = (byte)(p_base_info.led_off_time);
                p_al[FM_BASE_SETTING_ST_ADR + FM_OFSET_BASE_ENCODER_TYPEMATIC] = (byte)(p_base_info.encoder_typematic);

                for (int fi = 0; fi < p_base_mode_infos.base_mode_infos.Length; fi++)
                {
                    for (int fj = 0; fj < p_base_mode_infos.base_mode_infos[fi].sw_exe_script_no.Length; fj++)
                    {
                        p_al[FM_BASE_MODE_SETTING_ST_ADR + (FM_BASE_MODE_SETTING_SIZE * fi) + FM_OFSET_BASE_SW1_SCRIPT_NO + fj] = (byte)(p_base_mode_infos.base_mode_infos[fi].sw_exe_script_no[fj]);
                    }
                    for (int fj = 0; fj < p_base_mode_infos.base_mode_infos[fi].sw_sp_func_no.Length; fj++)
                    {
                        p_al[FM_BASE_MODE_SETTING_ST_ADR + (FM_BASE_MODE_SETTING_SIZE * fi) + FM_OFSET_BASE_SW1_SP_FUNC_NO + fj] = (byte)(p_base_mode_infos.base_mode_infos[fi].sw_sp_func_no[fj]);
                    }
                    p_al[FM_BASE_MODE_SETTING_ST_ADR + (FM_BASE_MODE_SETTING_SIZE * fi) + FM_OFSET_BASE_ENCODER_DEFAULT] = (byte)(p_base_mode_infos.base_mode_infos[fi].encoder_func_no);
                    p_al[FM_BASE_MODE_SETTING_ST_ADR + (FM_BASE_MODE_SETTING_SIZE * fi) + FM_OFSET_BASE_LED_COLOR_NO] = (byte)(p_base_mode_infos.base_mode_infos[fi].LED_color_no);
                    p_al[FM_BASE_MODE_SETTING_ST_ADR + (FM_BASE_MODE_SETTING_SIZE * fi) + FM_OFSET_BASE_LED_DETAIL_FLAG] = (byte)(p_base_mode_infos.base_mode_infos[fi].LED_color_detail_flag);
                    for (int fj = 0; fj < p_base_mode_infos.base_mode_infos[fi].LED_RGB_duty.Length; fj++)
                    {
                        p_al[FM_BASE_MODE_SETTING_ST_ADR + (FM_BASE_MODE_SETTING_SIZE * fi) + FM_OFSET_BASE_LED_RGB_DUTY + fj] = (byte)(p_base_mode_infos.base_mode_infos[fi].LED_RGB_duty[fj]);
                    }
                    p_al[FM_BASE_MODE_SETTING_ST_ADR + (FM_BASE_MODE_SETTING_SIZE * fi) + FM_OFSET_BASE_LED_BRIGHTNESS_LEVEL] = (byte)(p_base_mode_infos.base_mode_infos[fi].LED_brightness_level);
                }
            }
            catch
            {
            }
            return b_ret;
        }
        public bool Get_Flash_Write_Function_Info(Form1.STR_FUNC_DATAS p_func_datas, ref ArrayList p_al, Form1.STR_APP_SW_DATAS p_app_sw_datas, Form1.STR_APP_FUNC_DATAS p_app_func_datas)
        {
            bool b_ret = false;
            byte[] by_name;
            int i_name_size;
            try
            {
                // バッファ初期化
                p_al.Clear();

                byte byte_temp = 0xFF;
                for (int fi = 0; fi < FM_FUNCTION_SETTING_AREA_SIZE; fi++)
                {
                    p_al.Add(byte_temp);
                }

                for (int mode_idx = 0; mode_idx < Constants.MODE_NUM; mode_idx++ )
                {
                    for (int func_idx = 0; func_idx < Constants.FUNCTION_NUM; func_idx++)
                    {
                        for (int cw_ccw_idx = 0; cw_ccw_idx < Constants.CW_CCW_NUM; cw_ccw_idx++)
                        {
                            for (int data_idx = 0; data_idx < Constants.FUNCTION_CWCCW_DATA_LEN; data_idx++)
                            {
                                p_al[(FM_FUNCTION_SETTING_ST_ADR - FM_ADR_FUNCTION_SETTING) + (FM_FUNCTION_SETTING_SIZE_MODE * mode_idx) + (FM_FUNCTION_SETTING_SIZE_FUNC * func_idx) + FM_OFSET_FUNCTION_CW_DATA_TOP + (FM_OFSET_FUNCTION_DATA_SIZE * cw_ccw_idx) + data_idx] = p_func_datas.func_datas[mode_idx].func_data[func_idx].cw_ccw_data[cw_ccw_idx, data_idx];
                            }
                        }

                        p_al[(FM_FUNCTION_SETTING_ST_ADR - FM_ADR_FUNCTION_SETTING) + (FM_FUNCTION_SETTING_SIZE_MODE * mode_idx) + (FM_FUNCTION_SETTING_SIZE_FUNC * func_idx) + FM_OFSET_FUNCTION_LED_COLOR_NO] = p_func_datas.func_datas[mode_idx].func_data[func_idx].LED_color_no;
                        p_al[(FM_FUNCTION_SETTING_ST_ADR - FM_ADR_FUNCTION_SETTING) + (FM_FUNCTION_SETTING_SIZE_MODE * mode_idx) + (FM_FUNCTION_SETTING_SIZE_FUNC * func_idx) + FM_OFSET_FUNCTION_LED_DETAIL_FLAG] = p_func_datas.func_datas[mode_idx].func_data[func_idx].LED_color_detail_flag;

                        for (int data_idx = 0; data_idx < Constants.LED_RGB_COLOR_NUM; data_idx++)
                        {
                            p_al[(FM_FUNCTION_SETTING_ST_ADR - FM_ADR_FUNCTION_SETTING) + (FM_FUNCTION_SETTING_SIZE_MODE * mode_idx) + (FM_FUNCTION_SETTING_SIZE_FUNC * func_idx) + FM_OFSET_FUNCTION_LED_RGB_DUTY_TOP + data_idx] = p_func_datas.func_datas[mode_idx].func_data[func_idx].LED_RGB_duty[data_idx];
                        }
                        p_al[(FM_FUNCTION_SETTING_ST_ADR - FM_ADR_FUNCTION_SETTING) + (FM_FUNCTION_SETTING_SIZE_MODE * mode_idx) + (FM_FUNCTION_SETTING_SIZE_FUNC * func_idx) + FM_OFSET_FUNCTION_LED_BRIGHTNESS_LEVEL] = p_func_datas.func_datas[mode_idx].func_data[func_idx].LED_brightness_level;



                        // 機能名称
                        if (p_func_datas.func_datas[mode_idx].func_data[func_idx].func_name.Length > 0)
                        {
                            by_name = System.Text.Encoding.Unicode.GetBytes(p_func_datas.func_datas[mode_idx].func_data[func_idx].func_name);
                            i_name_size = by_name.Length;
                            if (i_name_size > FM_FUNCTION_NAME_MAX_SIZE)
                            {
                                i_name_size = FM_FUNCTION_NAME_MAX_SIZE;
                            }
                            p_al[(FM_FUNCTION_NAME_ST_ADR - FM_ADR_FUNCTION_SETTING) + (FM_FUNCTION_NAME_ST_SIZE * ((Constants.FUNCTION_NUM * mode_idx) + func_idx)) + FM_OFSET_FUNCTION_NAME_SIZE] = (byte)(i_name_size & 0xFF);
                            for (int fj = 0; fj < i_name_size; fj++)
                            {
                                p_al[(FM_FUNCTION_NAME_ST_ADR - FM_ADR_FUNCTION_SETTING) + (FM_FUNCTION_NAME_ST_SIZE * ((Constants.FUNCTION_NUM * mode_idx) + func_idx)) + FM_OFSET_FUNCTION_NAME_DATA + fj] = by_name[fj];
                            }
                        }
                        else
                        {   // パターン名称設定なし
                            p_al[(FM_FUNCTION_NAME_ST_ADR - FM_ADR_FUNCTION_SETTING) + (FM_FUNCTION_NAME_ST_SIZE * ((Constants.FUNCTION_NUM * mode_idx) + func_idx)) + FM_OFSET_FUNCTION_NAME_SIZE] = (byte)0;
                        }
                    }
                }


                // アプリケーション用データをセット
                int tmp_address = 0;
                // SW
                for (int mode_idx = 0; mode_idx < Constants.MODE_NUM; mode_idx++)
                {
                    for (int sw_idx = 0; sw_idx < Constants.BUTTON_NUM; sw_idx++)
                    {
                        for (int fi = 0; fi < Constants.APP_SW_DATA_SELECT_DATA_LEN; fi++)
                        {
                            tmp_address = (FM_ADR_APP_SW_SETTING - FM_ADR_FUNCTION_SETTING) + (FM_APP_SW_SETTING_SIZE_MODE * mode_idx) + (FM_OFSET_APP_SETTING_SW1_DATA_TOP + (FM_APP_SW_SETTING_SIZE_DATA * sw_idx) + fi);
                            p_al[(FM_ADR_APP_SW_SETTING - FM_ADR_FUNCTION_SETTING) + (FM_APP_SW_SETTING_SIZE_MODE * mode_idx) + (FM_OFSET_APP_SETTING_SW1_DATA_TOP + (FM_APP_SW_SETTING_SIZE_DATA * sw_idx) + fi)] = p_app_sw_datas.mode[mode_idx].app_data[sw_idx].select_data[fi];
                        }
                        for (int fi = 0; fi < Constants.APP_SW_DATA_DATA_LEN; fi++)
                        {
                            tmp_address = (FM_ADR_APP_SW_SETTING - FM_ADR_FUNCTION_SETTING) + (FM_APP_SW_SETTING_SIZE_MODE * mode_idx) + (FM_OFSET_APP_SETTING_SW1_DATA_TOP + (FM_APP_SW_SETTING_SIZE_DATA * sw_idx) + Constants.APP_SW_DATA_SELECT_DATA_LEN + fi);
                            p_al[(FM_ADR_APP_SW_SETTING - FM_ADR_FUNCTION_SETTING) + (FM_APP_SW_SETTING_SIZE_MODE * mode_idx) + (FM_OFSET_APP_SETTING_SW1_DATA_TOP + (FM_APP_SW_SETTING_SIZE_DATA * sw_idx) + Constants.APP_SW_DATA_SELECT_DATA_LEN + fi)] = p_app_sw_datas.mode[mode_idx].app_data[sw_idx].data[fi];
                        }
                    }
                }
                // FUNC
                for (int mode_idx = 0; mode_idx < Constants.MODE_NUM; mode_idx++)
                {
                    for (int func_idx = 0; func_idx < Constants.FUNCTION_NUM; func_idx++)
                    {
                        for (int cwccw_idx = 0; cwccw_idx < Constants.CW_CCW_NUM; cwccw_idx++)
                        {
                            for (int fi = 0; fi < Constants.APP_FUNC_DATA_SELECT_DATA_LEN; fi++)
                            {
                                tmp_address = (FM_ADR_APP_FUNC_SETTING - FM_ADR_FUNCTION_SETTING) + (FM_APP_FUNC_SETTING_SIZE_MODE * mode_idx) + (FM_APP_FUNC_SETTING_SIZE_FUNC * func_idx) + (FM_APP_FUNC_SETTING_SIZE_DATA * cwccw_idx) + fi;
                                p_al[(FM_ADR_APP_FUNC_SETTING - FM_ADR_FUNCTION_SETTING) + (FM_APP_FUNC_SETTING_SIZE_MODE * mode_idx) + (FM_APP_FUNC_SETTING_SIZE_FUNC * func_idx) + (FM_APP_FUNC_SETTING_SIZE_DATA * cwccw_idx) + fi] = p_app_func_datas.mode[mode_idx].func[func_idx].cwccw[cwccw_idx].app_data.select_data[fi];
                            }
                        }
                    }
                }

            }
            catch
            {
            }
            return b_ret;
        }
        public bool Get_Flash_Write_Encoder_Script_Info(Form1.STR_ENCODER_SCRIPT_DATAS p_encoder_script_datas, ref ArrayList p_al)
        {
            bool b_ret = false;
            try
            {
                // バッファ初期化
                p_al.Clear();

                byte byte_temp = 0xFF;
                for (int fi = 0; fi < FM_ENCODER_SCRIPT_SETTING_AREA_SIZE; fi++)
                {
                    p_al.Add(byte_temp);
                }

                for (int idx = 0; idx < Constants.ENCODER_SCRIPT_NUM; idx++)
                {
                    p_al[(FM_ENCODER_SCRIPT_ST_SIZE * idx) + FM_OFSET_ENCODER_SCRIPT_SCRIPT_NUM_TOP] = p_encoder_script_datas.encoder_script_datas[idx].rec_num;
                    p_al[(FM_ENCODER_SCRIPT_ST_SIZE * idx) + FM_OFSET_ENCODER_SCRIPT_LOOP_SET_TOP] = p_encoder_script_datas.encoder_script_datas[idx].loop_flag;

                    for (int script_no_idx = 0; script_no_idx < Constants.ENCODER_SCRIPT_SCRIPTSET_MAX_NUM; script_no_idx++)
                    {
                        p_al[(FM_ENCODER_SCRIPT_ST_SIZE * idx) + FM_OFSET_ENCODER_SCRIPT_DATA_TOP + script_no_idx] = p_encoder_script_datas.encoder_script_datas[idx].script_no[script_no_idx];
                    }
                }
            }
            catch
            {
            }
            return b_ret;
        }
        public bool Get_Flash_Write_SW_Function_Info(Form1.STR_SW_FUNC_DATAS p_sw_func_datas, ref ArrayList p_al)
        {
            bool b_ret = false;
            try
            {
                // バッファ初期化
                p_al.Clear();

                byte byte_temp = 0xFF;
                for (int fi = 0; fi < FM_SW_FUNCTION_SETTING_AREA_SIZE; fi++)
                {
                    p_al.Add(byte_temp);
                }

                for (int mode_idx = 0; mode_idx < Constants.MODE_NUM; mode_idx++)
                {
                    for (int sw_idx = 0; sw_idx < Constants.BUTTON_NUM; sw_idx++)
                    {
                        for (int data_idx = 0; data_idx < Constants.SW_FUNCTION_DATA_LEN; data_idx++)
                        {
                            p_al[(FM_SW_FUNCTION_SETTING_SIZE_MODE * mode_idx) + (FM_SW_FUNCTION_SETTING_SIZE_FUNC * sw_idx) + data_idx] = p_sw_func_datas.sw_func_datas[mode_idx].sw_func_data[sw_idx].sw_data[data_idx];
                        }
                    }
                }
            }
            catch
            {
            }
            return b_ret;
        }
        public bool Get_Flash_Write_Script_Info_Data(Form1.STR_SCRIPT_INFO p_script_info, Form1.STR_SCRIPT_INFO_DATAS p_script_info_datas, ref ArrayList p_al)
        {
            bool b_ret = false;
            byte[] by_name;
            int i_name_size;
            try
            {
                // バッファ初期化
                p_al.Clear();

                byte byte_temp = 0xFF;
                for (int fi = 0; fi < FM_SCRIPT_INFO_AREA_SIZE; fi++)
                {
                    p_al.Add(byte_temp);
                }

                p_al[FM_OFSET_SCRIPT_CHK_SUM_L] = (byte)(p_script_info.check_sum & 0xFF);
                p_al[FM_OFSET_SCRIPT_CHK_SUM_H] = (byte)((p_script_info.check_sum >> 8) & 0xFF);
                p_al[FM_OFSET_SCRIPT_REC_NUM] = p_script_info.Record_Num;

                p_al[FM_OFSET_SCRIPT_TOTAL_SIZE_L0] = (byte)(p_script_info.Total_Size & 0xFF);
                p_al[FM_OFSET_SCRIPT_TOTAL_SIZE_L1] = (byte)((p_script_info.Total_Size >> 8) & 0xFF);
                p_al[FM_OFSET_SCRIPT_TOTAL_SIZE_L2] = (byte)((p_script_info.Total_Size >> 16) & 0xFF);
                p_al[FM_OFSET_SCRIPT_TOTAL_SIZE_L3] = (byte)((p_script_info.Total_Size >> 24) & 0xFF);

                for (int fi = 0; fi < Constants.SCRIPT_NUM; fi++ )
                {
                    p_al[FM_SCRIPT_INFO_DATA_ST_ADR + (FM_SCRIPT_INFO_DATA_SIZE * fi) + FM_OFSET_SCRIPT_REC_ADR + 0] = (byte)(p_script_info_datas.Script_Info_Datas[fi].Recode_Address & 0xFF);
                    p_al[FM_SCRIPT_INFO_DATA_ST_ADR + (FM_SCRIPT_INFO_DATA_SIZE * fi) + FM_OFSET_SCRIPT_REC_ADR + 1] = (byte)((p_script_info_datas.Script_Info_Datas[fi].Recode_Address >> 8) & 0xFF);
                    p_al[FM_SCRIPT_INFO_DATA_ST_ADR + (FM_SCRIPT_INFO_DATA_SIZE * fi) + FM_OFSET_SCRIPT_REC_ADR + 2] = (byte)((p_script_info_datas.Script_Info_Datas[fi].Recode_Address >> 16) & 0xFF);
                    p_al[FM_SCRIPT_INFO_DATA_ST_ADR + (FM_SCRIPT_INFO_DATA_SIZE * fi) + FM_OFSET_SCRIPT_REC_ADR + 3] = (byte)((p_script_info_datas.Script_Info_Datas[fi].Recode_Address >> 24) & 0xFF);
                    p_al[FM_SCRIPT_INFO_DATA_ST_ADR + (FM_SCRIPT_INFO_DATA_SIZE * fi) + FM_OFSET_SCRIPT_DATA_SIZE + 0] = (byte)(p_script_info_datas.Script_Info_Datas[fi].Script_Size & 0xFF);
                    p_al[FM_SCRIPT_INFO_DATA_ST_ADR + (FM_SCRIPT_INFO_DATA_SIZE * fi) + FM_OFSET_SCRIPT_DATA_SIZE + 1] = (byte)((p_script_info_datas.Script_Info_Datas[fi].Script_Size >> 8) & 0xFF);
                    p_al[FM_SCRIPT_INFO_DATA_ST_ADR + (FM_SCRIPT_INFO_DATA_SIZE * fi) + FM_OFSET_SCRIPT_DATA_SIZE + 2] = (byte)((p_script_info_datas.Script_Info_Datas[fi].Script_Size >> 16) & 0xFF);
                    p_al[FM_SCRIPT_INFO_DATA_ST_ADR + (FM_SCRIPT_INFO_DATA_SIZE * fi) + FM_OFSET_SCRIPT_DATA_SIZE + 3] = (byte)((p_script_info_datas.Script_Info_Datas[fi].Script_Size >> 24) & 0xFF);
                    p_al[FM_SCRIPT_INFO_DATA_ST_ADR + (FM_SCRIPT_INFO_DATA_SIZE * fi) + FM_OFSET_SCRIPT_MODE] = p_script_info_datas.Script_Info_Datas[fi].Script_Mode;
                    

                    //文字列長チェック
                    if(p_script_info_datas.Script_Info_Datas[fi].Name.Length > 0)
                    {
                        by_name = System.Text.Encoding.Unicode.GetBytes(p_script_info_datas.Script_Info_Datas[fi].Name);
                        i_name_size = by_name.Length;
                        if (i_name_size > FM_SCRIPT_INFO_DATA_NAME_SIZE)
                        {
                            i_name_size = FM_SCRIPT_INFO_DATA_NAME_SIZE;
                        }
                        p_al[FM_SCRIPT_INFO_DATA_ST_ADR + (FM_SCRIPT_INFO_DATA_SIZE * fi) + FM_OFSET_SCRIPT_NAME_SIZE] = (byte)(i_name_size & 0xFF);
                        for (int fj = 0; fj < i_name_size; fj++)
                        {
                            p_al[FM_SCRIPT_INFO_DATA_ST_ADR + (FM_SCRIPT_INFO_DATA_SIZE * fi) + FM_OFSET_SCRIPT_NAME_DATA + fj] = by_name[fj];
                        }
                    }
                    else
                    {
                        p_al[FM_SCRIPT_INFO_DATA_ST_ADR + (FM_SCRIPT_INFO_DATA_SIZE * fi) + FM_OFSET_SCRIPT_NAME_SIZE] = (byte)0;
                    }
                }
            }
            catch
            {
            }
            return b_ret;
        }
        //public bool Get_Flash_Write_Mode(Form1.STR_MOUSE_MODE p_mouse_mode, ref ArrayList p_al)
        //{
        //    bool b_ret = false;
        //    try
        //    {
        //        // バッファ初期化
        //        p_al.Clear();

        //        byte byte_temp = 0xFF;
        //        for (int fi = 0; fi < FM_MODE_INFO_AREA_SIZE; fi++)
        //        {
        //            p_al.Add(byte_temp);
        //        }

        //        p_al[FM_OFSET_MODE] = p_mouse_mode.Mode;
        //    }
        //    catch
        //    {
        //    }
        //    return b_ret;
        //}



    }
}
