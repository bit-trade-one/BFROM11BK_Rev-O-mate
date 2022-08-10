#include "l_script.h"

//=========================================================
// 変数
//=========================================================
ST_SCRIPT_EXE_INFO script_exe_info;
//ST_SCRIPT_EXE_SCRIPT_DATAS script_exe_script_datas;
//ST_SCRIPT_EXE_BUFF script_exe_buff;
//ST_SCRIPT_EXE_COMMAND script_exe_command;
//ST_SCRIPT_EXE_SEND_CODE script_exe_send_code;
UN_SCRIPT_HEAD script_head;
UN_SCRIPT_INFO script_info;

BYTE next_script_exe_req_scriptNo = 0;

//=========================================================
// プロトタイプ宣言
//=========================================================
BYTE Set_Script_Exe_Info(BYTE p_idx, BYTE p_ScriptNo);
BYTE Script_Exe_Info_Clr(void);
BYTE Script_Reset(BYTE p_idx);
BYTE Get_Next_Script_Data(void);
BYTE Get_Next_Script_Command(void);
BYTE Set_USB_Send_Code(void);
BYTE Get_Script_Output_Status( void );
//BYTE Get_Script_Output_Status_Multi(BYTE p_idx);
//BYTE Script_Interval_Update(void);
BYTE Script_Run(BYTE p_idx);
BYTE Script_Stop(BYTE p_idx);
BYTE Script_Fire_Mode_Stop(BYTE p_idx, BYTE p_ScriptNo);
BYTE Script_Hold_Mode_Stop(BYTE p_idx, BYTE p_ScriptNo);
BYTE Get_Exe_Script_No(BYTE p_idx);
BYTE Get_Script_Attribute(BYTE p_idx);






//========================================================
// Set_Script_Exe_Info
//========================================================
BYTE Set_Script_Exe_Info(BYTE p_idx, BYTE p_ScriptNo)
{
    BYTE ret_val = 0;
	if(SCRIPT_EXE_INFO_NUM <= p_idx )
	{
		ret_val = 0xFF;
	}
	if(p_ScriptNo < 1 || SCRIPT_MAX_NUM < p_ScriptNo )
	{
		ret_val = 0xFF;
	}

    if(ret_val == 0)
    {   // ここまで正常
        // Flash Memoryからスクリプト情報を取得
        u_GetScriptInfo( p_ScriptNo, &script_info );

        script_exe_info.Script_No = p_ScriptNo;
        script_exe_info.Address.Val = script_info.Script_Info.Script_Adress.Val;
        script_exe_info.Script_Size.Val = script_info.Script_Info.Script_Size.Val;
        script_exe_info.Control_Flag0 = script_info.Script_Info.Mode;
    }
    else
    {   // エラー
        script_exe_info.Script_No = 0;
        script_exe_info.Address.Val = 0;
        script_exe_info.Script_Size.Val = 0;
        script_exe_info.Control_Flag0 = 0;
    }

	return ret_val;
}

BYTE Script_Buff_Clr(void)
{
	BYTE fi, fj;
    
    for( fi = 0; fi < SCRIPT_EXE_DATA_BUFF_BANK_NUM; fi++ )
    {
        for( fj = 0; fj < SCRIPT_EXE_DATA_BUFF_SIZE; fj++ )
        {
            script_exe_info.Script_Data[fi][fj] = 0;
        }
    }
    script_exe_info.reserve1 = 0;
    
    for( fi = 0; fi < MOUSE_BUFF_SIZE; fi++ )
    {
        script_exe_info.USB_Send_Code_Mouse[fi] = 0;
    }
    script_exe_info.USB_Send_Wheel_Scroll = 0;
    for( fi = 0; fi < KEYBOARD_BUFF_SIZE; fi++ )
    {
        script_exe_info.USB_Send_Code_Keyboard[fi] = 0;
    }
    for( fi = 0; fi < JOYSTICK_BUFF_SIZE; fi++ )
    {
        script_exe_info.USB_Send_Code_Joystick[fi] = joystick_default_value[fi];
    }
    for(fi = 0; fi < MULTIMEDIA_BUFF_SIZE; fi++)
    {
        script_exe_info.USB_Send_Code_MultiMedia[fi] = 0;
    }
    script_exe_info.reserve3 = 0;
}
//========================================================
// Script_Exe_Info_Clr 
// SCRIPT実行情報　初期化
//========================================================
BYTE Script_Exe_Info_Clr(void)
{
//	BYTE fi, fj;
	
    script_exe_info.Exe_Flag = 0;
    script_exe_info.Script_No = 0;
    script_exe_info.Pause_Flag = 0;
    script_exe_info.Control_Flag0 = 0;
    script_exe_info.Control_Flag1 = 0;
    script_exe_info.Control_Flag2 = 0;
    script_exe_info.Control_Flag3 = 0;
//    for( fi = 0; fi < SCRIPT_EXE_DATA_BUFF_BANK_NUM; fi++ )
//    {
//        for( fj = 0; fj < SCRIPT_EXE_DATA_BUFF_SIZE; fj++ )
//        {
//            script_exe_info.Script_Data[fi][fj] = 0;
//        }
//    }
//    script_exe_info.reserve1 = 0;

    script_exe_info.Command_ID = 0;
    script_exe_info.Command_DATA.Val = 0;
    script_exe_info.Interval.Val = 0;
    script_exe_info.Address.Val = 0;
    script_exe_info.Script_Size.Val = 0;
    script_exe_info.Script_Exe_Pos.Val = 0;
    script_exe_info.Next_Read_Address.Val = 0;
    script_exe_info.reserve2 = 0;

//    for( fi = 0; fi < MOUSE_BUFF_SIZE; fi++ )
//    {
//        script_exe_info.USB_Send_Code_Mouse[fi] = 0;
//    }
//    script_exe_info.USB_Send_Wheel_Scroll = 0;
//    for( fi = 0; fi < KEYBOARD_BUFF_SIZE; fi++ )
//    {
//        script_exe_info.USB_Send_Code_Keyboard[fi] = 0;
//    }
//    for( fi = 0; fi < JOYSTICK_BUFF_SIZE; fi++ )
//    {
//        script_exe_info.USB_Send_Code_Joystick[fi] = joystick_default_value[fi];
//    }
//    for(fi = 0; fi < MULTIMEDIA_BUFF_SIZE; fi++)
//    {
//        script_exe_info.USB_Send_Code_MultiMedia[fi] = 0;
//    }
//    script_exe_info.reserve3 = 0;
	
    Script_Buff_Clr();
            
	return 0;
}

//========================================================
// Script_Reset
//========================================================
BYTE Script_Reset(BYTE p_idx)
{
//	BYTE fi, fj;

//	script_exe_info.Exe_Flag = 0;
//	script_exe_info.Script_No = 0;
    script_exe_info.Pause_Flag = 0;
//	script_exe_info.Control_Flag0 = 0;
    script_exe_info.Control_Flag1 = SCRIPT_EXE_BUFF_BANK0_EXE;
    script_exe_info.Control_Flag2 = 0;
    script_exe_info.Control_Flag3 = SCRIPT_EXE_READ_REQ_BANK0;
//    for( fi = 0; fi < SCRIPT_EXE_DATA_BUFF_BANK_NUM; fi++ )
//    {
//        for( fj = 0; fj < SCRIPT_EXE_DATA_BUFF_SIZE; fj++ )
//        {
//            script_exe_info.Script_Data[fi][fj] = 0;
//        }
//    }
    
    script_exe_info.Command_ID = 0;
    script_exe_info.Command_DATA.Val = 0;
    script_exe_info.Interval.Val = 0;
//	script_exe_info.Address.Val = 0;
//	script_exe_info.Script_Size.Val = 0;
    script_exe_info.Script_Exe_Pos.Val = 0;
    script_exe_info.Next_Read_Address.Val = script_exe_info.Address.Val;
    
//    for( fi = 0; fi < MOUSE_BUFF_SIZE; fi++ )
//    {
//        script_exe_info.USB_Send_Code_Mouse[fi] = 0;
//    }
//    script_exe_info.USB_Send_Wheel_Scroll = 0;
//    for( fi = 0; fi < KEYBOARD_BUFF_SIZE; fi++ )
//    {
//        script_exe_info.USB_Send_Code_Keyboard[fi] = 0;
//    }
//    for( fi = 0; fi < JOYSTICK_BUFF_SIZE; fi++ )
//    {
//        script_exe_info.USB_Send_Code_Joystick[fi] = joystick_default_value[fi];
//    }
//    for(fi = 0; fi < MULTIMEDIA_BUFF_SIZE; fi++)
//    {
//        script_exe_info.USB_Send_Code_MultiMedia[fi] = 0;
//    }
//    script_exe_info.reserve3 = 0;

    Script_Buff_Clr();
    
	return 0;
}

//========================================================
// Get_Next_Script_Data
//========================================================
BYTE Get_Next_Script_Data(void)
{
	FLASH_ADR adr;
	DWORD read_size;
//	BYTE fi;
	
    // スクリプト実行中でない
    if(script_exe_info.Exe_Flag != SCRIPT_EXE_FLAG_EXE )
    {
		return 0xFF;
    }
    // スクリプトNoがなし
    else if(script_exe_info.Script_No < 1 || SCRIPT_MAX_NUM < script_exe_info.Script_No )
    {
		return 0xFE;
    }
    // 読み込み要求なし
    else if(script_exe_info.Control_Flag3 != SCRIPT_EXE_READ_REQ_BANK0
        && script_exe_info.Control_Flag3 != SCRIPT_EXE_READ_REQ_BANK1)
    {
		return 0xFD;
    }
    // スクリプトサイズチェック
    else if(script_exe_info.Script_Size.Val == 0 || (FLASH_SIZE - FLASH_SCRIPT_DATA_ADR) < script_exe_info.Script_Size.Val )
    {	// スクリプトサイズが 0 または　FlashMemoryのスクリプトデータ格納サイズよりも大きい
		return 0xFC;
    }

    // スクリプトの未読み込みバイト数 および　読み込みアドレス チェック
    if(script_exe_info.Script_Size.Val > (script_exe_info.Next_Read_Address.Val - script_exe_info.Address.Val) )
    {		
        read_size = script_exe_info.Script_Size.Val - (script_exe_info.Next_Read_Address.Val - script_exe_info.Address.Val);
        if(read_size > (DWORD)SCRIPT_EXE_DATA_BUFF_SIZE)
        {
            read_size = (DWORD)SCRIPT_EXE_DATA_BUFF_SIZE;
        }
    }
    else
    {
        read_size = 0;
    }

    if( script_exe_info.Next_Read_Address.Val < FLASH_SCRIPT_DATA_ADR 
        ||  FLASH_SIZE < (script_exe_info.Next_Read_Address.Val + read_size)
        || read_size == 0 )
    {	// 読み込みアドレスがスクリプトデータ格納アドレスより小さい　または　Flashメモリサイズより次に読み込むアドレス＋読み込みサイズが大きい　または　読み込みサイズが０
		return 0xFB;
    }

    adr = (FLASH_ADR)script_exe_info.Next_Read_Address.Val;
    if(script_exe_info.Control_Flag3 == SCRIPT_EXE_READ_REQ_BANK0)
    {
        flash_n_read(adr, (unsigned char *)&script_exe_info.Script_Data[SCRIPT_EXE_BUFF_BANK0_EXE], (int)read_size);
        script_exe_info.Control_Flag3 = SCRIPT_EXE_READ_COMP_BANK0;
        script_exe_info.Next_Read_Address.Val += read_size;
    }
    else if(script_exe_info.Control_Flag3 == SCRIPT_EXE_READ_REQ_BANK1)
    {
        flash_n_read(adr, (unsigned char *)&script_exe_info.Script_Data[SCRIPT_EXE_BUFF_BANK1_EXE], (int)read_size);
        script_exe_info.Control_Flag3 = SCRIPT_EXE_READ_COMP_BANK1;
        script_exe_info.Next_Read_Address.Val += read_size;
    }

	return 0;
}

//========================================================
// Get_Next_Script_Command
//========================================================
BYTE Get_Next_Script_Command(void)
{
	BYTE ret_code = 0;
	BYTE *p_script_data;
	BYTE copy_size = 0;
	BYTE fi;
	WORD w_data;
    

    // スクリプト実行中でない
    if(script_exe_info.Exe_Flag != SCRIPT_EXE_FLAG_EXE )
    {
        ret_code = 0xFF;
    }
    // スクリプトNoがなし
    else if(script_exe_info.Script_No < 1 || SCRIPT_MAX_NUM < script_exe_info.Script_No )
    {
        ret_code = 0xFE;
    }
    // 読み込みバッファバンクチェック
    else if(script_exe_info.Control_Flag1 != SCRIPT_EXE_BUFF_BANK0_EXE
        && script_exe_info.Control_Flag1 != SCRIPT_EXE_BUFF_BANK1_EXE)
    {
        ret_code = 0xFD;
    }
    // スクリプトサイズチェック
    else if(script_exe_info.Script_Size.Val == 0 || (FLASH_SIZE - FLASH_SCRIPT_DATA_ADR) < script_exe_info.Script_Size.Val )
    {	// スクリプトサイズが 0 または　FlashMemoryのスクリプトデータ格納サイズよりも大きい
        ret_code = 0xFC;
    }
    // WAIT時間チェック
    else if(script_exe_info.Interval.Val > 0)
    {
        ret_code = 0xFB;
    }
    // 次のコマンドがあるか、コマンドサイズと実行済みコマンドサイズでチェック
    else if(script_exe_info.Script_Size.Val <= script_exe_info.Script_Exe_Pos.Val)
    {	// 全コマンド終了
        if(script_exe_info.Control_Flag0 == SCRIPT_EXE_CTRL_LOOP_MODE
            || script_exe_info.Control_Flag0 == SCRIPT_EXE_CTRL_FIRE_MODE)
        {	//　スクリプト繰り返しのためリセット
            Script_Reset(0);
            
            // Scriptデータ読み込み
            Get_Next_Script_Data();
        }
        else if(script_exe_info.Control_Flag0 == SCRIPT_EXE_CTRL_HOLD_MODE)
        {   // スクリプトキープ 最後の状態をキープ
            
        }
        else
        {	// スクリプト終了
            Script_Stop(0);
            Script_Reset(0);
            
            ret_code = 0xFA;
        }
//        ret_code = 0xFA;
    }	

    // ここまでのチェックOK
    if(ret_code == 0x00)
    {
        // 次の読み込みバッファにスクリプトデータ読み込み要求を出す
        if(script_exe_info.Control_Flag1 == SCRIPT_EXE_BUFF_BANK0_EXE && script_exe_info.Control_Flag3 == SCRIPT_EXE_READ_COMP_BANK0)
        {	// バンク0を実行中で、バンク0の読み込み完了のとき
            // バンク1の読み込み要求を出す
            script_exe_info.Control_Flag3 = SCRIPT_EXE_READ_REQ_BANK1;
        }
        else if(script_exe_info.Control_Flag1 == SCRIPT_EXE_BUFF_BANK1_EXE && script_exe_info.Control_Flag3 == SCRIPT_EXE_READ_COMP_BANK1)
        {	// バンク1を実行中で、バンク1の読み込み完了のとき
            // バンク0の読み込み要求を出す
            script_exe_info.Control_Flag3 = SCRIPT_EXE_READ_REQ_BANK0;
        }

        // 読み込みバッファバ位置チェック
        if(script_exe_info.Control_Flag2 >= SCRIPT_EXE_DATA_BUFF_SIZE)
        {
            script_exe_info.Control_Flag1++;
            if(script_exe_info.Control_Flag1 >= SCRIPT_EXE_DATA_BUFF_BANK_NUM)
            {
                script_exe_info.Control_Flag1 = SCRIPT_EXE_BUFF_BANK0_EXE;
            }	
            script_exe_info.Control_Flag2 = 0;
        }
    }

    if(ret_code == 0)
    {	// チェックOKなら、次のコマンドを読み込みバッファから取り出す
        if(script_exe_info.Control_Flag1 == SCRIPT_EXE_BUFF_BANK1_EXE)
        {
            p_script_data = &script_exe_info.Script_Data[SCRIPT_EXE_BUFF_BANK1_EXE][script_exe_info.Control_Flag2];
        }
        else
        {
            p_script_data = &script_exe_info.Script_Data[SCRIPT_EXE_BUFF_BANK0_EXE][script_exe_info.Control_Flag2];
        }	
        // スクリプトコマンドIDコピー
        script_exe_info.Command_ID = *p_script_data++;
        script_exe_info.Control_Flag2++;
        script_exe_info.Script_Exe_Pos.Val++;
        // スクリプトデータ部コピー
        if(script_exe_info.Command_ID == SCRIPT_COMMAND_ID_WAIT
                || script_exe_info.Command_ID == SCRIPT_COMMAND_ID_MOUSE_MOVE
                || script_exe_info.Command_ID == SCRIPT_COMMAND_ID_JOY_L_LEVER
                || script_exe_info.Command_ID == SCRIPT_COMMAND_ID_JOY_R_LEVER)
        {	// データ2byte読み込み
            copy_size = 2;
        }
        else if(script_exe_info.Command_ID == SCRIPT_COMMAND_ID_KEY_PRESS
            || script_exe_info.Command_ID == SCRIPT_COMMAND_ID_KEY_RELESE
            || script_exe_info.Command_ID == SCRIPT_COMMAND_ID_MULTI_PRESS
            || script_exe_info.Command_ID == SCRIPT_COMMAND_ID_MULTI_RELESE
            || script_exe_info.Command_ID == SCRIPT_COMMAND_ID_MOUSE_SCROLL_UP
            || script_exe_info.Command_ID == SCRIPT_COMMAND_ID_MOUSE_SCROLL_DOWN
            || script_exe_info.Command_ID == SCRIPT_COMMAND_ID_JOY_BTN_PRESS
            || script_exe_info.Command_ID == SCRIPT_COMMAND_ID_JOY_BTN_RELESE
            || script_exe_info.Command_ID == SCRIPT_COMMAND_ID_JOY_HATSW_PRESS
            || script_exe_info.Command_ID == SCRIPT_COMMAND_ID_JOY_HATSW_RELESE)
        {	// データ1byte読み込み
            copy_size = 1;
        }
        else
        {
            copy_size = 0;
        }
        // スクリプトデータコピー
        w_data = 0;
        script_exe_info.Command_DATA.Val = 0;
        for( fi = 0; fi < copy_size; fi++ )
        {
            // 読み込みバッファのバンク切り替えチェック
            if(script_exe_info.Control_Flag2 >= SCRIPT_EXE_DATA_BUFF_SIZE)
            {
                // 読み込みバッファのバンクをずらす
                script_exe_info.Control_Flag1++;
                if(script_exe_info.Control_Flag1 >= SCRIPT_EXE_DATA_BUFF_BANK_NUM)
                {	// 先頭バンクへ
                    script_exe_info.Control_Flag1 = SCRIPT_EXE_BUFF_BANK0_EXE;
                    p_script_data = &script_exe_info.Script_Data[0][0];
                }
                // バッファの読み込み位置を先頭に
                script_exe_info.Control_Flag2 = 0;
            }
            // 

//			w_data = *p_script_data++;
//			script_exe_info.Command_DATA.Val = script_exe_info.Command_DATA.Val | (w_data << (fi * 8));
            script_exe_info.Command_DATA.Val = (script_exe_info.Command_DATA.Val << 8) | *p_script_data++;
            script_exe_info.Control_Flag2++;
            script_exe_info.Script_Exe_Pos.Val++;
        }
    }
    else
    {	// 未実行またはエラー
        script_exe_info.Command_ID = 0;
        script_exe_info.Command_DATA.Val = 0;
    }

	return 0;
}

//========================================================
// Set_USB_Send_Code
//========================================================
BYTE Set_USB_Send_Code(void)
{
	BYTE ret_code = 0;
	BYTE fi, idx, loop_idx;
	BYTE temp_key_Code[KEYBOARD_BUFF_SIZE];
	BYTE count = 0;
	BYTE command_data = 0;
	BYTE modifier_data = 0;
	BYTE joy_button_data = 0;
	BYTE joy_button_no = 0;
	BYTE byte_temp1, byte_temp2, byte_temp3;
	short s_temp;
	
    // スクリプト実行中でない
    if(script_exe_info.Exe_Flag != SCRIPT_EXE_FLAG_EXE )
    {
        ret_code = 0xFF;
    }
    // スクリプトNoがなし
    else if(script_exe_info.Script_No < 1 || SCRIPT_MAX_NUM < script_exe_info.Script_No)
    {
        ret_code = 0xFE;
    }
    // WAIT時間チェック
    else if(script_exe_info.Interval.Val > 0)
    {
        ret_code = 0xFC;
    }

    if(ret_code == 0x00)
    {	// ここまでのチェックOK

        script_exe_info.Interval.Val = 1;	// デフォルトで1ms間隔を開ける
        switch(script_exe_info.Command_ID)
        {
            case SCRIPT_COMMAND_ID_WAIT:
                // wait
                script_exe_info.Interval.Val = script_exe_info.Command_DATA.Val;
                break;
            case SCRIPT_COMMAND_ID_KEY_PRESS:
            case SCRIPT_COMMAND_ID_KEY_RELESE:
                modifier_data = script_exe_info.USB_Send_Code_Keyboard[KEYBOARD_MODIFIER_IDX];
                command_data = (BYTE)(script_exe_info.Command_DATA.Val & 0xFF);

                if(command_data == KEYBOARD_KEYCODE_L_CTRL)
                {
                    if( script_exe_info.Command_ID == SCRIPT_COMMAND_ID_KEY_PRESS )
                        modifier_data = modifier_data | 0x01;	// L Ctrl press
                    else
                        modifier_data = modifier_data & 0xFE;	// L Ctrl release
                }
                else if(command_data == KEYBOARD_KEYCODE_L_SHIFT)
                {
                    if(script_exe_info.Command_ID == SCRIPT_COMMAND_ID_KEY_PRESS)
                        modifier_data = modifier_data | 0x02;	// L Shift press
                    else
                        modifier_data = modifier_data & 0xFD;	// L Shift release
                }
                else if(command_data == KEYBOARD_KEYCODE_L_ALT)
                {
                    if(script_exe_info.Command_ID == SCRIPT_COMMAND_ID_KEY_PRESS)
                        modifier_data = modifier_data | 0x04;	// L Alt press
                    else
                        modifier_data = modifier_data & 0xFB;	// L Alt release
                }
                else if(command_data == KEYBOARD_KEYCODE_L_GUI)
                {
                    if(script_exe_info.Command_ID == SCRIPT_COMMAND_ID_KEY_PRESS)
                        modifier_data = modifier_data | 0x08;	// L GUI press
                    else
                        modifier_data = modifier_data & 0xF7;	// L GUI release
                }
                else if(command_data == KEYBOARD_KEYCODE_R_CTRL)
                {
                    if(script_exe_info.Command_ID == SCRIPT_COMMAND_ID_KEY_PRESS)
                        modifier_data = modifier_data | 0x10;	// R Ctrl press
                    else
                        modifier_data = modifier_data & 0xEF;	// R Ctrl release
                }
                else if(command_data == KEYBOARD_KEYCODE_R_SHIFT)
                {
                    if(script_exe_info.Command_ID == SCRIPT_COMMAND_ID_KEY_PRESS)
                        modifier_data = modifier_data | 0x20;	// R Shift press
                    else
                        modifier_data = modifier_data & 0xDF;	// R Shift release
                }
                else if(command_data == KEYBOARD_KEYCODE_R_ALT)
                {
                    if(script_exe_info.Command_ID == SCRIPT_COMMAND_ID_KEY_PRESS)
                        modifier_data = modifier_data | 0x40;	// R Alt press
                    else
                        modifier_data = modifier_data & 0xBF;	// R Alt release
                }
                else if(command_data == KEYBOARD_KEYCODE_R_GUI)
                {
                    if(script_exe_info.Command_ID == SCRIPT_COMMAND_ID_KEY_PRESS)
                        modifier_data = modifier_data | 0x80;	// R GUI press
                    else
                        modifier_data = modifier_data & 0x7F;	// R GUI release
                }
                else
                {	// その他のキー
                    // 前回の送信コードを一旦コピー ＆　格納キーコード数をカウント
                    // ただし、今回追加または削除するコードはコピーしない(最新のコードとして追加するため)
                    for( fi = KEYBOARD_KEYCODE_TOP; fi < KEYBOARD_BUFF_SIZE; fi++ )
                    {
                        if((BYTE)(script_exe_info.Command_DATA.Val & 0xFF) != script_exe_info.USB_Send_Code_Keyboard[fi])
                        {
                            temp_key_Code[fi] = script_exe_info.USB_Send_Code_Keyboard[fi];
                            if(script_exe_info.USB_Send_Code_Keyboard[fi] != 0x00)
                            {
                                count++;
                            }
                        }
                        else
                        {
                            temp_key_Code[fi] = 0x00;
                        }
                        script_exe_info.USB_Send_Code_Keyboard[fi] = 0x00;
                    }
                    // 今回コード追加で、格納コード数が一杯の場合は一番古いコードを削除する
                    if(script_exe_info.Command_ID == SCRIPT_COMMAND_ID_KEY_PRESS && count >= (KEYBOARD_BUFF_SIZE - KEYBOARD_KEYCODE_TOP))
                    {
                        temp_key_Code[KEYBOARD_KEYCODE_TOP] = 0x00;
                    }
                    // 送信コードをセット
                    idx = KEYBOARD_KEYCODE_TOP;
                    for( fi = KEYBOARD_KEYCODE_TOP; fi < KEYBOARD_BUFF_SIZE; fi++ )
                    {	// 前回までの送信してーたコードを前詰めでセット
                        if(temp_key_Code[fi] != 0x00)
                        {
                            script_exe_info.USB_Send_Code_Keyboard[idx] = temp_key_Code[fi];
                            idx++;
                        }
                    }
                    // コード追加の場合は最後に追加する
                    if(script_exe_info.Command_ID == SCRIPT_COMMAND_ID_KEY_PRESS && idx < KEYBOARD_BUFF_SIZE)
                    {
                        script_exe_info.USB_Send_Code_Keyboard[idx] = (BYTE)(script_exe_info.Command_DATA.Val & 0xFF);
                    }
                }
                script_exe_info.USB_Send_Code_Keyboard[KEYBOARD_MODIFIER_IDX] = modifier_data;
                break;
            case SCRIPT_COMMAND_ID_MULTI_PRESS:
            case SCRIPT_COMMAND_ID_MULTI_RELESE:
                switch((BYTE)(script_exe_info.Command_DATA.Val & 0xFF))
                {
                    case MULTIMEDIA_TYPE_PLAY:
                        if(script_exe_info.Command_ID == SCRIPT_COMMAND_ID_MULTI_PRESS)
                        {
                            script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA1] = script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA1] | MULTIMEDIA_DATA_PLAY;
                        }
                        else
                        {
                            script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA1] = script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA1] & ~MULTIMEDIA_DATA_PLAY;
                        }
                        break;
                    case MULTIMEDIA_TYPE_PAUSE:
                        if(script_exe_info.Command_ID == SCRIPT_COMMAND_ID_MULTI_PRESS)
                            script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA1] = script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA1] | MULTIMEDIA_DATA_PAUSE;
                        else
                            script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA1] = script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA1] & ~MULTIMEDIA_DATA_PAUSE;
                        break;
                    case MULTIMEDIA_TYPE_STOP:
                        if(script_exe_info.Command_ID == SCRIPT_COMMAND_ID_MULTI_PRESS)
                            script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA1] = script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA1] | MULTIMEDIA_DATA_STOP;
                        else
                            script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA1] = script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA1] & ~MULTIMEDIA_DATA_STOP;
                        break;
                    case MULTIMEDIA_TYPE_REC:
                        if(script_exe_info.Command_ID == SCRIPT_COMMAND_ID_MULTI_PRESS)
                            script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA1] = script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA1] | MULTIMEDIA_DATA_REC;
                        else
                            script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA1] = script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA1] & ~MULTIMEDIA_DATA_REC;
                        break;
                    case MULTIMEDIA_TYPE_FORWARD:
                        if(script_exe_info.Command_ID == SCRIPT_COMMAND_ID_MULTI_PRESS)
                            script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA1] = script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA1] | MULTIMEDIA_DATA_FORWARD;
                        else
                            script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA1] = script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA1] & ~MULTIMEDIA_DATA_FORWARD;
                        break;
                    case MULTIMEDIA_TYPE_REWIND:
                        if(script_exe_info.Command_ID == SCRIPT_COMMAND_ID_MULTI_PRESS)
                            script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA1] = script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA1] | MULTIMEDIA_DATA_REWIND;
                        else
                            script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA1] = script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA1] & ~MULTIMEDIA_DATA_REWIND;
                        break;
                    case MULTIMEDIA_TYPE_NEXT:
                        if(script_exe_info.Command_ID == SCRIPT_COMMAND_ID_MULTI_PRESS)
                            script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA1] = script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA1] | MULTIMEDIA_DATA_NEXT;
                        else
                            script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA1] = script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA1] & ~MULTIMEDIA_DATA_NEXT;
                        break;
                    case MULTIMEDIA_TYPE_PREVIOUS:
                        if(script_exe_info.Command_ID == SCRIPT_COMMAND_ID_MULTI_PRESS)
                            script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA1] = script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA1] | MULTIMEDIA_DATA_PREVIOUS;
                        else
                            script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA1] = script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA1] & ~MULTIMEDIA_DATA_PREVIOUS;
                        break;
                    case MULTIMEDIA_TYPE_VOLUME_MUTE:
                        if(script_exe_info.Command_ID == SCRIPT_COMMAND_ID_MULTI_PRESS)
                            script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA2] = script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA2] | MULTIMEDIA_DATA_VOLUME_MUTE;
                        else
                            script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA2] = script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA2] & ~MULTIMEDIA_DATA_VOLUME_MUTE;
                        break;
                    case MULTIMEDIA_TYPE_VOLUME_UP:
                        if(script_exe_info.Command_ID == SCRIPT_COMMAND_ID_MULTI_PRESS)
                            script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA2] = script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA2] | MULTIMEDIA_DATA_VOLUME_UP;
                        else
                            script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA2] = script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA2] & ~MULTIMEDIA_DATA_VOLUME_UP;
                        break;
                    case MULTIMEDIA_TYPE_VOLUME_DOWN:
                        if(script_exe_info.Command_ID == SCRIPT_COMMAND_ID_MULTI_PRESS)
                            script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA2] = script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA2] | MULTIMEDIA_DATA_VOLUME_DOWN;
                        else
                            script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA2] = script_exe_info.USB_Send_Code_MultiMedia[MULTIMEDIA_DATA_DATA2] & ~MULTIMEDIA_DATA_VOLUME_DOWN;
                        break;
                }
                break;
            case SCRIPT_COMMAND_ID_MOUSE_PRESS_L:
                script_exe_info.USB_Send_Code_Mouse[USB_MOUSE_BUTTON_IDX_TOP] = script_exe_info.USB_Send_Code_Mouse[USB_MOUSE_BUTTON_IDX_TOP] | 0x01;
                break;
            case SCRIPT_COMMAND_ID_MOUSE_RELESE_L:
                script_exe_info.USB_Send_Code_Mouse[USB_MOUSE_BUTTON_IDX_TOP] = script_exe_info.USB_Send_Code_Mouse[USB_MOUSE_BUTTON_IDX_TOP] & 0xFE;
                break;
            case SCRIPT_COMMAND_ID_MOUSE_PRESS_R:
                script_exe_info.USB_Send_Code_Mouse[USB_MOUSE_BUTTON_IDX_TOP] = script_exe_info.USB_Send_Code_Mouse[USB_MOUSE_BUTTON_IDX_TOP] | 0x02;
                break;
            case SCRIPT_COMMAND_ID_MOUSE_RELESE_R:
                script_exe_info.USB_Send_Code_Mouse[USB_MOUSE_BUTTON_IDX_TOP] = script_exe_info.USB_Send_Code_Mouse[USB_MOUSE_BUTTON_IDX_TOP] & 0xFD;
                break;
            case SCRIPT_COMMAND_ID_MOUSE_PRESS_W:
                script_exe_info.USB_Send_Code_Mouse[USB_MOUSE_BUTTON_IDX_TOP] = script_exe_info.USB_Send_Code_Mouse[USB_MOUSE_BUTTON_IDX_TOP] | 0x04;
                break;
            case SCRIPT_COMMAND_ID_MOUSE_RELESE_W:
                script_exe_info.USB_Send_Code_Mouse[USB_MOUSE_BUTTON_IDX_TOP] = script_exe_info.USB_Send_Code_Mouse[USB_MOUSE_BUTTON_IDX_TOP] & 0xFB;
                break;
            case SCRIPT_COMMAND_ID_MOUSE_PRESS_B4:
                script_exe_info.USB_Send_Code_Mouse[USB_MOUSE_BUTTON_IDX_TOP] = script_exe_info.USB_Send_Code_Mouse[USB_MOUSE_BUTTON_IDX_TOP] | 0x08;
                break;
            case SCRIPT_COMMAND_ID_MOUSE_RELESE_B4:
                script_exe_info.USB_Send_Code_Mouse[USB_MOUSE_BUTTON_IDX_TOP] = script_exe_info.USB_Send_Code_Mouse[USB_MOUSE_BUTTON_IDX_TOP] & 0xF7;
                break;
            case SCRIPT_COMMAND_ID_MOUSE_PRESS_B5:
                script_exe_info.USB_Send_Code_Mouse[USB_MOUSE_BUTTON_IDX_TOP] = script_exe_info.USB_Send_Code_Mouse[USB_MOUSE_BUTTON_IDX_TOP] | 0x10;
                break;
            case SCRIPT_COMMAND_ID_MOUSE_RELESE_B5:
                script_exe_info.USB_Send_Code_Mouse[USB_MOUSE_BUTTON_IDX_TOP] = script_exe_info.USB_Send_Code_Mouse[USB_MOUSE_BUTTON_IDX_TOP] & 0xEF;
                break;
            case SCRIPT_COMMAND_ID_MOUSE_SCROLL_UP:
//					script_exe_info.USB_Send_Wheel_Scroll = 0x0001;
//					s_temp = (short)((char)(script_exe_info.Command_DATA.Val & 0xFF));
//					script_exe_info.USB_Send_Wheel_Scroll = (WORD)s_temp;
                script_exe_info.USB_Send_Wheel_Scroll = (WORD)(script_exe_info.Command_DATA.Val & 0xFF);
                break;
            case SCRIPT_COMMAND_ID_MOUSE_SCROLL_DOWN:
//					script_exe_info.USB_Send_Wheel_Scroll = 0xFFFF;
//					s_temp = (short)((char)(script_exe_info.Command_DATA.Val & 0xFF));
//					script_exe_info.USB_Send_Wheel_Scroll = (WORD)s_temp;
                script_exe_info.USB_Send_Wheel_Scroll = (WORD)((WORD)0xFF00 | (WORD)(script_exe_info.Command_DATA.Val & 0xFF));
                break;
            case SCRIPT_COMMAND_ID_MOUSE_MOVE:
                script_exe_info.USB_Send_Code_Mouse[USB_MOUSE_MOVE_X_IDX] = (BYTE)((script_exe_info.Command_DATA.Val >> 8) & 0xFF);
                script_exe_info.USB_Send_Code_Mouse[USB_MOUSE_MOVE_Y_IDX] = (BYTE)(script_exe_info.Command_DATA.Val & 0xFF);
//                script_exe_info.USB_Send_Code_Mouse[USB_MOUSE_MOVE_X_IDX] = 0x80 - (BYTE)((script_exe_info.Command_DATA.Val >> 8) & 0xFF);
//                script_exe_info.USB_Send_Code_Mouse[USB_MOUSE_MOVE_Y_IDX] = 0x80 - (BYTE)(script_exe_info.Command_DATA.Val & 0xFF);
                break;
            case SCRIPT_COMMAND_ID_JOY_BTN_PRESS:
            case SCRIPT_COMMAND_ID_JOY_BTN_RELESE:
                joy_button_no = (BYTE)(script_exe_info.Command_DATA.Val & 0xFF);
                if(USB_JOYSTICK_BUTTON_ID_MIN <= joy_button_no && joy_button_no <= USB_JOYSTICK_BUTTON_ID_MAX)
                {
                    byte_temp1 = joy_button_no / 8;  // idx
                    byte_temp2 = joy_button_no % 8;  // bit sift
                    byte_temp3 = 0x01 << byte_temp2;    // mask
                    
                    joy_button_data = script_exe_info.USB_Send_Code_Joystick[USB_JOYSTICK_BUTTON_IDX_TOP + byte_temp1];
                    if(script_exe_info.Command_ID == SCRIPT_COMMAND_ID_JOY_BTN_PRESS)
                    {   // PRESS
                        joy_button_data = joy_button_data | byte_temp3;
                    }
                    else if(script_exe_info.Command_ID == SCRIPT_COMMAND_ID_JOY_BTN_RELESE)
                    {   // RELESE
                        joy_button_data = joy_button_data & ~byte_temp3;
                    }
                    script_exe_info.USB_Send_Code_Joystick[USB_JOYSTICK_BUTTON_IDX_TOP + byte_temp1] = joy_button_data;
                }
                break;
            case SCRIPT_COMMAND_ID_JOY_HATSW_PRESS:
                script_exe_info.USB_Send_Code_Joystick[USB_JOYSTICK_HATSW_IDX] = (BYTE)(script_exe_info.Command_DATA.Val & HAT_SWITCH_PRESS_MASK);
                break;
            case SCRIPT_COMMAND_ID_JOY_HATSW_RELESE:
                script_exe_info.USB_Send_Code_Joystick[USB_JOYSTICK_HATSW_IDX] = HAT_SWITCH_NULL;
                break;
            case SCRIPT_COMMAND_ID_JOY_L_LEVER:
                script_exe_info.USB_Send_Code_Joystick[USB_JOYSTICK_LEVER_L_IDX_TOP] = 0x80 + (BYTE)((script_exe_info.Command_DATA.Val >> 8) & 0xFF);
                script_exe_info.USB_Send_Code_Joystick[USB_JOYSTICK_LEVER_L_IDX_TOP+1] = 0x80 - (BYTE)(script_exe_info.Command_DATA.Val & 0xFF);
                break;
            case SCRIPT_COMMAND_ID_JOY_L_LEVER_CENTER:
                for(fi = 0; fi < USB_JOYSTICK_LEVER_L_IDX_SIZE; fi++)
                {
                    script_exe_info.USB_Send_Code_Joystick[USB_JOYSTICK_LEVER_L_IDX_TOP+fi] = USB_JOYSTICK_LEVER_CENTER;
                }
                break;
            case SCRIPT_COMMAND_ID_JOY_R_LEVER:
                script_exe_info.USB_Send_Code_Joystick[USB_JOYSTICK_LEVER_R_IDX_TOP] = 0x80 + (BYTE)((script_exe_info.Command_DATA.Val >> 8) & 0xFF);
                script_exe_info.USB_Send_Code_Joystick[USB_JOYSTICK_LEVER_R_IDX_TOP+1] = 0x80 - (BYTE)(script_exe_info.Command_DATA.Val & 0xFF);
                break;
            case SCRIPT_COMMAND_ID_JOY_R_LEVER_CENTER:
                for(fi = 0; fi < USB_JOYSTICK_LEVER_R_IDX_SIZE; fi++)
                {
                    script_exe_info.USB_Send_Code_Joystick[USB_JOYSTICK_LEVER_R_IDX_TOP+fi] = USB_JOYSTICK_LEVER_CENTER;
                }
                break;
            default:
                script_exe_info.Interval.Val = 0;
                break;
        }
        script_exe_info.Command_ID = 0;
        script_exe_info.Command_DATA.Val = 0;
    }
	
	return ret_code;
}

// スクリプトの出力有無を取得
// 戻り値 0=出力しない 1=出力する
BYTE Get_Script_Output_Status()
{
	// 
	BYTE ret_val = 0;

    if( script_exe_info.Exe_Flag == SCRIPT_EXE_FLAG_EXE && script_exe_info.Pause_Flag == 0 )
    {	// スクリプト実行中　かつ　一時停止でないとき
        ret_val = 1;	// 出力する
    }

	return ret_val;
}
#if 0
BYTE Get_Script_Output_Status_Multi(BYTE p_idx)
{
	// 
	if(0 <= p_idx && p_idx < SCRIPT_EXE_INFO_NUM)
	{
		if( script_exe_info.Exe_Flag[p_idx] == SCRIPT_EXE_FLAG_EXE && script_exe_info.Pause_Flag[p_idx] == 0 )
		{	// スクリプト実行中　かつ　一時停止でないとき
			return 1;	// 出力する
		}
	}	
	return 0;
}
#endif

// スクリプトのインターバル時間を更新する
#if 0
BYTE Script_Interval_Update()
{
	//
	BYTE fi; 
	for(fi = 0; fi < SCRIPT_EXE_INFO_NUM; fi++)
	{
		if( script_exe_info.Exe_Flag[fi] == SCRIPT_EXE_FLAG_EXE && script_exe_info.Pause_Flag[fi] == 0 )
		{	// スクリプト実行中　かつ　一時停止でないとき
			
			if( script_exe_info.Interval[fi].Val > 0 )
			{	// デクリメント
				script_exe_info.Interval[fi].Val--;
			}
		}
	}
	return 0;
}
#endif

//========================================================
// Script_Run
// スクリプトを実行する
//========================================================
BYTE Script_Run(BYTE p_idx)
{
//	if(0 <= p_idx && p_idx < SCRIPT_EXE_INFO_NUM)
//	{
		if( 1 <= script_exe_info.Script_No && script_exe_info.Script_No <= SCRIPT_MAX_NUM )
		{
            if(0 < script_exe_info.Script_Size.Val && script_exe_info.Script_Size.Val <= (FLASH_SIZE - FLASH_SCRIPT_DATA_ADR))
            {
                script_exe_info.Exe_Flag = SCRIPT_EXE_FLAG_EXE;
                return 0;
            }
		}
//	}
	return 0xFF;
}

//========================================================
// Script_Stop
// スクリプトの実行を停止する
//========================================================
BYTE Script_Stop(BYTE p_idx)
{
//	if(0 <= p_idx && p_idx < SCRIPT_EXE_INFO_NUM)
//	{
		script_exe_info.Exe_Flag = SCRIPT_EXE_FLAG_NOEXE;
		return 0;
//	}
//	return 0xFF;
}

//========================================================
// Script_Fire_Mode_Stop
// スクリプトの実行を停止する（ファイヤーモードのときのみ停止）
//========================================================
BYTE Script_Fire_Mode_Stop(BYTE p_idx, BYTE p_ScriptNo)
{
//	if( SCRIPT_EXE_INFO_NUM <= p_idx )
//	{
//		return 0xFE;
//	}
	if( p_ScriptNo < 1 || SCRIPT_MAX_NUM < p_ScriptNo )
	{
		return 0xFE;
	}
	if( script_exe_info.Script_No == p_ScriptNo && script_exe_info.Control_Flag0 == SCRIPT_EXE_CTRL_FIRE_MODE )
	{
		script_exe_info.Exe_Flag = SCRIPT_EXE_FLAG_NOEXE;
		return 0;
	}
	return 0xFF;
}

//========================================================
// Script_Hold_Mode_Stop
// スクリプトの実行を停止する（ホールドモードのときのみ停止）
//========================================================
BYTE Script_Hold_Mode_Stop(BYTE p_idx, BYTE p_ScriptNo)
{
//	if( SCRIPT_EXE_INFO_NUM <= p_idx )
//	{
//		return 0xFE;
//	}
	if( p_ScriptNo < 1 || SCRIPT_MAX_NUM < p_ScriptNo )
	{
		return 0xFE;
	}
	if( script_exe_info.Script_No == p_ScriptNo && script_exe_info.Control_Flag0 == SCRIPT_EXE_CTRL_HOLD_MODE )
	{
		script_exe_info.Exe_Flag = SCRIPT_EXE_FLAG_NOEXE;
		return 0;
	}
	return 0xFF;
}

//========================================================
// Get_Exe_Script_No
// 実行中のスクリプトNo.を取得する
//========================================================
BYTE Get_Exe_Script_No(BYTE p_idx)
{
//	if(0 <= p_idx && p_idx < SCRIPT_EXE_INFO_NUM)
//	{
		if(script_exe_info.Exe_Flag == SCRIPT_EXE_FLAG_EXE)
		{
			return script_exe_info.Script_No;
		}
//	}
	return 0;
}

//========================================================
// Get_Script_Attribute
// スクリプト属性を取得する
//========================================================
BYTE Get_Script_Attribute(BYTE p_idx)
{
//	if(0 <= p_idx && p_idx < SCRIPT_EXE_INFO_NUM)
//	{
		return script_exe_info.Control_Flag0;
//	}
//	return 0;
}	

