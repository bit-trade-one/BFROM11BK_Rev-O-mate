#include <string.h>
#include "app.h"
#include "Main_comm.h"
#include "l_flash.h"
#include "l_script.h"

BYTE key_code_new_add[KEY_CODE_NEW_ADD_BUFF_SIZE]={0};
BYTE key_code_new_add_write_idx = 0;
BYTE key_code_new_add_read_idx = 0;
BYTE key_code_new_add_full = 0;
BYTE keyboard_buffer_temp[KEYBOARD_BUFF_SIZE]={0};

void USB_Comm(void)
{
    BYTE fi, fj;
    WORD w_temp;
    BYTE key_code_set_pos;
    int8_t tmp;
    FLASH_ADR flash_address;
    BYTE b_found_flag = 0;

    // スクリプト出力チェック
    if(Get_Script_Output_Status() == 1)
    {   // LED1 点滅
        led1_blink_flag = 1;
    }
    else
    {
        led1_blink_flag = 0;
    }
    
    // エンコーダースクリプト出力チェック
    if(Get_Encoder_Output_Status() == 1)
    {
        Encoder_Output_Script();
    }

// Keyboard
    if(!HIDTxHandleBusy(lastINTransmission_Keyboard))
    {	       	//Load the HID buffer
        // キーボード出力バッファ一旦クリア
//        for(fi = 0; fi < KEYBOARD_BUFF_SIZE; fi++)
//        {
//            keyboard_buffer[fi] = 0;
//        }
        
#if 1
        // SW入力による出力チェック
        if(Get_SW_Input_Output_Status() == 1)
        {
//            SW_Output_Keyboard(keyboard_buffer, KEYBOARD_BUFF_SIZE);
#if 1
            SW_Output_Keyboard(keyboard_buffer_temp, KEYBOARD_BUFF_SIZE);
            for(fi = 0; fi < KEYBOARD_BUFF_SIZE; fi++)
            {
                if(KEYBOARD_KEYCODE_TOP <= fi)
                {
                    if(keyboard_buffer_temp[fi] > 0)
                    {
                        b_found_flag = 0;
                        // 前回出力時に同じキーコードがあるかチェック
                        for(fj = KEYBOARD_KEYCODE_TOP; fj < KEYBOARD_BUFF_SIZE; fj++)
                        {
                            if(keyboard_buffer_temp[fi] == keyboard_buffer_pre[fj])
                            {   // 前回同じキーコード出力している
                                keyboard_buffer[fj] = keyboard_buffer_pre[fj];
                                b_found_flag = 1;
                                break;
                            }
                        }
                        if(b_found_flag == 0)
                        {   // 前回同じキーコード出力していない
                            if(key_code_new_add_full == 0)
                            {   // キーコード新規追加バッファに追加
                                key_code_new_add[key_code_new_add_write_idx++] = keyboard_buffer_temp[fi];
                                key_code_new_add_write_idx &= (KEY_CODE_NEW_ADD_BUFF_SIZE - 1);
                                if(key_code_new_add_write_idx == key_code_new_add_read_idx)
                                {
                                    key_code_new_add_full = 1;
                                }
                            }
                        }
                    }
                }
                else
                {
                    keyboard_buffer[fi] = keyboard_buffer_temp[fi];
                }
            }
#endif
        }
        // エンコーダー出力チェック
        else if(Get_Encoder_Output_Status() == 1)
        {
            Encoder_Output_Keyboard(keyboard_buffer, KEYBOARD_BUFF_SIZE);
#if 0
            Encoder_Output_Keyboard(keyboard_buffer_temp, KEYBOARD_BUFF_SIZE);
            for(fi = 0; fi < KEYBOARD_BUFF_SIZE; fi++)
            {
                if(KEYBOARD_KEYCODE_TOP <= fi)
                {
                    if(keyboard_buffer_pre[fj] > 0)
                    {
                        b_found_flag = 0;
                        // 前回出力時に同じキーコードがあるかチェック
                        for(fj = KEYBOARD_KEYCODE_TOP; fj < KEYBOARD_BUFF_SIZE; fj++)
                        {
                            if(keyboard_buffer_temp[fi] == keyboard_buffer_pre[fj])
                            {   // 前回同じキーコード出力している
                                keyboard_buffer[fj] = keyboard_buffer_pre[fj];
                                b_found_flag = 1;
                                break;
                            }
                        }
                        if(b_found_flag == 0)
                        {   // 前回同じキーコード出力していない
                            if(key_code_new_add_full == 0)
                            {   // キーコード新規追加バッファに追加
                                key_code_new_add[key_code_new_add_write_idx++] = keyboard_buffer_temp[fi];
                                key_code_new_add_write_idx &= (KEY_CODE_NEW_ADD_BUFF_SIZE - 1);
                                if(key_code_new_add_write_idx == key_code_new_add_read_idx)
                                {
                                    key_code_new_add_full = 1;
                                }
                            }
                        }
                    }
                }
                else
                {
                    keyboard_buffer[fi] = keyboard_buffer_temp[fi];
                }
            }
#endif
        }
        // スクリプト出力？
        else if( Get_Script_Output_Status() == 1 )
        {
#if 1
            // スクリプト停止？
            if(keyboard_output_stop_by_macro == 0)
            {   
                for(fi = 0; fi < KEYBOARD_BUFF_SIZE; fi++)
                {
                    keyboard_buffer[fi] = script_exe_info.USB_Send_Code_Keyboard[fi];
                }
            }
            else
            {
                keyboard_output_stop_by_macro--;
            }
#else
            for(fi = 0; fi < KEYBOARD_BUFF_SIZE; fi++)
            {
                if(KEYBOARD_KEYCODE_TOP <= fi)
                {
                    if(script_exe_info.USB_Send_Code_Keyboard[fi] > 0)
                    {
                        b_found_flag = 0;
                        // 前回出力時に同じキーコードがあるかチェック
                        for(fj = KEYBOARD_KEYCODE_TOP; fj < KEYBOARD_BUFF_SIZE; fj++)
                        {
                            if(script_exe_info.USB_Send_Code_Keyboard[fi] == keyboard_buffer_pre[fj])
                            {   // 前回同じキーコード出力している
                                keyboard_buffer[fj] = keyboard_buffer_pre[fj];
                                b_found_flag = 1;
                                break;
                            }
                        }
                        if(b_found_flag == 0)
                        {   // 前回同じキーコード出力していない
                            if(key_code_new_add_full == 0)
                            {   // キーコード新規追加バッファに追加
                                key_code_new_add[key_code_new_add_write_idx++] = script_exe_info.USB_Send_Code_Keyboard[fi];
                                key_code_new_add_write_idx &= (KEY_CODE_NEW_ADD_BUFF_SIZE - 1);
                                if(key_code_new_add_write_idx == key_code_new_add_read_idx)
                                {
                                    key_code_new_add_full = 1;
                                }
                            }
                        }
                    }
                }
                else
                {
                    keyboard_buffer[fi] = script_exe_info.USB_Send_Code_Keyboard[fi];
                }
            }
#endif
        }
#endif
        
#if 1
        // キーコードを前詰めに
        key_code_set_pos = KEYBOARD_KEYCODE_TOP;  // セット位置
        for(fi = KEYBOARD_KEYCODE_TOP; fi < KEYBOARD_BUFF_SIZE; fi++)
        {
            if(keyboard_buffer[fi] != 0)
            {
                if(key_code_set_pos != fi)
                {
                    keyboard_buffer[key_code_set_pos++] = keyboard_buffer[fi];
                    keyboard_buffer[fi] = 0;
                }
                else
                {
                    key_code_set_pos++;
                }
            }
        }
        // 新しいキーが押された？
        if(key_code_new_add_write_idx != key_code_new_add_read_idx || key_code_new_add_full == 1)
        {   // 新しいキーを1つ追加
            if(key_code_set_pos == KEYBOARD_BUFF_SIZE)
            {   // キーコードですべて埋まっているので、１つ削除して追加
                for(fi = KEYBOARD_KEYCODE_TOP; fi < (KEYBOARD_BUFF_SIZE - 1); fi++)
                {
                    keyboard_buffer[fi] = keyboard_buffer[fi+1];
                }
                keyboard_buffer[(KEYBOARD_BUFF_SIZE - 1)] = key_code_new_add[key_code_new_add_read_idx++];
            }
            else
            {
                keyboard_buffer[key_code_set_pos] = key_code_new_add[key_code_new_add_read_idx++];
            }
            key_code_new_add_read_idx &= (KEY_CODE_NEW_ADD_BUFF_SIZE - 1);
            key_code_new_add_full = 0;
        }
#endif
        
        // Report ID セット
        keyboard_buffer[KEYBOARD_REPORT_ID_IDX] = KEYBOARD_REPORT_ID;

        //変化チェック
//        keyboard_input_out_flag = 0;
        for(fi = KEYBOARD_MODIFIER_IDX; fi < KEYBOARD_BUFF_SIZE; fi++)
        {
            if(keyboard_buffer[fi] != keyboard_buffer_pre[fi])
//            if(keyboard_buffer[fi] != keyboard_buffer_pre[fi] || keyboard_buffer[fi] != 0)
            {   // 変化あり のときのみ送信に変更
                keyboard_input_out_flag = 3;
                break;
            }
        }

        // キーボード出力
        if( keyboard_input_out_flag > 0 )
        {
            //出力バッファにデータコピー
            for(fi = 0; fi < KEYBOARD_BUFF_SIZE; fi++)
            {
                keyboard_input[fi] = keyboard_buffer[fi];
            }
            
            lastINTransmission_Keyboard = HIDTxPacket(HID_EP1, (BYTE*)&keyboard_input[0], KEYBOARD_BUFF_SIZE);
            keyboard_input_out_flag--;
        }
        
        // 今回値保存＆クリア
        for(fi = 0; fi < KEYBOARD_BUFF_SIZE; fi++)
        {
            keyboard_buffer_pre[fi] = keyboard_buffer[fi];
            keyboard_buffer[fi] = 0;
        }
    }
	if(!HIDRxHandleBusy(lastOUTTransmission_Keyboard))
	{
		//Num Lock LED state is in Bit0.
		//Caps Lock LED state is in Bit1.
		//Scroll Lock LED state is in Bit2.
		if(keyboard_output[0] & 0x01)
		{	// Num Lock LED ON
//			LED_1_ON();
		}
		else
		{
//			LED_1_OFF();
		}
		if(keyboard_output[0] & 0x02)
		{	// Caps Lock LED ON
//			LED_2_ON();
		}
		else
		{
//			LED_2_OFF();
		}
		if(keyboard_output[0] & 0x04)
		{	// Scroll Lock LED ON
//			LED_3_ON();
		}
		else
		{
//			LED_3_OFF();
		}
		lastOUTTransmission_Keyboard = HIDRxPacket(HID_EP1,(BYTE*)&keyboard_output[0], HID_INT_OUT_EP_SIZE);
	}
// MultiMedia
    if(!HIDTxHandleBusy(lastINTransmission_Keyboard))
    {
//        for(fi = 0; fi < MULTIMEDIA_BUFF_SIZE; fi++)
//        {
//            multimedia_buffer[fi] = 0;
//        }
        
        // SW入力による出力チェック
        if(Get_SW_Input_Output_Status() == 1)
        {
            SW_Output_Multimedia(multimedia_buffer, MULTIMEDIA_BUFF_SIZE);
        }
        // エンコーダー出力チェック
        else if(Get_Encoder_Output_Status() == 1)
        {
            Encoder_Output_Multimedia(multimedia_buffer, MULTIMEDIA_BUFF_SIZE);
        }
        // スクリプト出力？
        else if( Get_Script_Output_Status() == 1 )
        {
            for(fi = 0; fi < MULTIMEDIA_BUFF_SIZE; fi++)
            {
                multimedia_buffer[fi] = script_exe_info.USB_Send_Code_MultiMedia[fi];
            }
        }
        
        // Report ID セット
        multimedia_buffer[KEYBOARD_REPORT_ID_IDX] = MULTIMEDIA_REPORT_ID;
        
        //変化チェック
//        multimedia_input_out_flag = 0;
        for(fi = MULTIMEDIA_DATA_TOP; fi < MULTIMEDIA_BUFF_SIZE; fi++)
        {
            if(multimedia_buffer[fi] != multimedia_buffer_pre[fi])
//            if(multimedia_buffer[fi] != multimedia_buffer_pre[fi] || multimedia_buffer[fi] != 0)
            {   // 変化あり または　0以外
                multimedia_input_out_flag = 3;
                break;
            }
        }
        
        // マルチメディア出力
        if( multimedia_input_out_flag > 0 )
        {
            //出力バッファにデータコピー
            for(fi = 0; fi < MULTIMEDIA_BUFF_SIZE; fi++)
            {
                multimedia_input[fi] = multimedia_buffer[fi];
            }
            
            lastINTransmission_Keyboard = HIDTxPacket(HID_EP1, (BYTE*)&multimedia_input[0], MULTIMEDIA_BUFF_SIZE);
            multimedia_input_out_flag--;
        }
        
        // 今回値保存＆クリア
        for(fi = 0; fi < MULTIMEDIA_BUFF_SIZE; fi++)
        {
            multimedia_buffer_pre[fi] = multimedia_buffer[fi];
            multimedia_buffer[fi] = 0;
        }
    }
// Mouse
    if(!HIDTxHandleBusy(lastTransmission_Mouse))
    {
//        for( fi = 0; fi < MOUSE_BUFF_SIZE; fi++ )
//        {
//            mouse_buffer[fi] = 0;
//        }
        // SW入力による出力チェック
        if(Get_SW_Input_Output_Status() == 1)
        {
            SW_Output_Mouse(mouse_buffer, MOUSE_BUFF_SIZE);
        }
        // エンコーダー出力チェック
        else if(Get_Encoder_Output_Status() == 1)
        {
            Encoder_Output_Mouse(mouse_buffer, MOUSE_BUFF_SIZE);
        }
        // スクリプト出力チェック
        else if(Get_Script_Output_Status() == 1)
        {
            for( fi = 0; fi < MOUSE_BUFF_SIZE; fi++ )
            {
                mouse_buffer[fi] = script_exe_info.USB_Send_Code_Mouse[fi];
            }
            // マウス移動一旦クリア
            script_exe_info.USB_Send_Code_Mouse[USB_MOUSE_MOVE_X_IDX] = 0;
            script_exe_info.USB_Send_Code_Mouse[USB_MOUSE_MOVE_Y_IDX] = 0;
            // wheel cnt set
            mouse_buffer[USB_MOUSE_MOVE_W_IDX] = script_exe_info.USB_Send_Wheel_Scroll & 0xFF;
            script_exe_info.USB_Send_Wheel_Scroll = 0;
        }
        
        //変化チェック
//        mouse_input_out_flag = 0;
        for( fi = 0; fi < MOUSE_BUFF_SIZE; fi++ )
        {
            if(mouse_buffer[fi] != mouse_buffer_pre[fi] || mouse_buffer[fi] != 0)
            {   // 変化あり または 0以外
                mouse_input_out_flag = 1;
                break;
            }
        }

#if 1
	    // ダブルクリックチェック
	    if(mouse_w_click_status != MOUSE_DOUBLE_CLICK_STATUS_NONE)
	    {
		    if(mouse_w_click_status == MOUSE_DOUBLE_CLICK_STATUS_CLICK1)
		    {
				mouse_buffer[USB_MOUSE_BUTTON_IDX_TOP] = MOUSE_DATA_LEFT_CLICK;
				mouse_input_out_flag = 5;
				
				mouse_w_click_interval_counter = MOUSE_DOUBLE_CLICK_INTERVAL;
				mouse_w_click_status = MOUSE_DOUBLE_CLICK_STATUS_INTERVAL;
			}
			else if(mouse_w_click_status == MOUSE_DOUBLE_CLICK_STATUS_INTERVAL)
			{
				if(mouse_w_click_interval_counter == 0)
				{
					mouse_w_click_status = MOUSE_DOUBLE_CLICK_STATUS_CLICK2;
				}
			}
			else if(mouse_w_click_status == MOUSE_DOUBLE_CLICK_STATUS_CLICK2)
			{
				mouse_buffer[USB_MOUSE_BUTTON_IDX_TOP] = MOUSE_DATA_LEFT_CLICK;
				mouse_input_out_flag = 5;
				mouse_w_click_status = MOUSE_DOUBLE_CLICK_STATUS_NONE;
			}	 
		}
#endif


        //Send the 4 byte packet over USB to the host.
        if( mouse_input_out_flag > 0 )
        {
            //出力バッファにデータコピー
            for(fi = 0; fi < MOUSE_BUFF_SIZE; fi++)
            {
                mouse_input[fi] = mouse_buffer[fi];
            }

            lastTransmission_Mouse = HIDTxPacket(HID_EP2, (BYTE*)&mouse_input[0], MOUSE_BUFF_SIZE);
            mouse_input_out_flag--;
        }
        
        // 今回値保存＆クリア
        for(fi = 0; fi < MOUSE_BUFF_SIZE; fi++)
        {
            mouse_buffer_pre[fi] = mouse_buffer[fi];
            mouse_buffer[fi] = 0;
        }
    }
// Joystick
    if(!HIDTxHandleBusy(lastTransmission_Joystick))
    {
        // キーボード出力バッファ一旦クリア
//        for(fi = 0; fi < JOYSTICK_BUFF_SIZE; fi++)
//        {
//            joystick_buffer[fi] = joystick_default_value[fi];
//        }
        
        // SW入力による出力チェック
        if(Get_SW_Input_Output_Status() == 1)
        {
            SW_Output_Joystick(joystick_buffer, JOYSTICK_BUFF_SIZE);
        }
        // エンコーダー出力チェック
        if(Get_Encoder_Output_Status() == 1)
        {
            Encoder_Output_Joystick(joystick_buffer, JOYSTICK_BUFF_SIZE);
        }
        // スクリプト出力チェック
        else if(Get_Script_Output_Status() == 1)
        {
            for(fi = 0; fi < JOYSTICK_BUFF_SIZE; fi++)
            {
                joystick_buffer[fi] = script_exe_info.USB_Send_Code_Joystick[fi];
            }
        }
        
        //変化チェック
//        joystick_input_out_flag = 0;
        for(fi = 0; fi < JOYSTICK_BUFF_SIZE; fi++)
        {
            if(joystick_buffer[fi] != joystick_buffer_pre[fi] || joystick_buffer[fi] != joystick_default_value[fi])
            {   // 変化あり または デフォルト値以外
                joystick_input_out_flag = 1;
                break;
            }
        }

        if( joystick_input_out_flag > 0 )
        {
            //出力バッファにデータコピー
            for(fi = 0; fi < JOYSTICK_BUFF_SIZE; fi++)
            {
                joystick_input[fi] = joystick_buffer[fi];
            }
            
            lastTransmission_Joystick = HIDTxPacket(HID_EP3, (BYTE*)&joystick_input[0], JOYSTICK_BUFF_SIZE);
            joystick_input_out_flag--;
        }
        // 今回値保存＆クリア
        for(fi = 0; fi < JOYSTICK_BUFF_SIZE; fi++)
        {
            joystick_buffer_pre[fi] = joystick_buffer[fi];
            joystick_buffer[fi] = joystick_default_value[fi];
        }
    }
// USB Data
    //Check if we have received an OUT data packet from the host
    if(!HIDRxHandleBusy(USBOutHandle))
    {
        for(fi = 0; fi < TX_DATA_BUFFER_SIZE; fi++)
        {
            ToSendDataBuffer[fi] = 0x00;
        }
        //We just received a packet of data from the USB host.
        //Check the first byte of the packet to see what command the host
        //application software wants us to fulfill.
        switch(ReceivedDataBuffer[0])				//Look at the data the host sent, to see what kind of application specific command it sent.
        {
            case 0x56: // V=0x56 Get Firmware version
                ToSendDataBuffer[0] = 0x56;				//Echo back to the host PC the command we are fulfilling in the first byte.  In this case, the Get Pushbutton State command.
                tmp = strlen(c_version);
                if( 0 < tmp && tmp <= (TX_DATA_BUFFER_SIZE-2) )
                {
                        for( fi = 0; fi < tmp; fi++ )
                        {
                                ToSendDataBuffer[fi+1] = c_version[fi];
                        }
                        // 最後にNULL文字を設定
                        ToSendDataBuffer[fi+1] = 0x00;
                }
                else
                {
                        //バージョン文字列異常
                        ToSendDataBuffer[1] = 0x00;
                }
                if(!HIDTxHandleBusy(USBInHandle))
                {
                    USBInHandle = HIDTxPacket(HID_EP4,(BYTE*)&ToSendDataBuffer[0], TX_DATA_BUFFER_SIZE);
                }
                break;
            /* Flash関係 ID 0x10 - 0x1F *************************************************************************************** */
            case 0x11:  // Flashからデータを読み込み [0x11,先頭アドレスMSB,,,LSB,サイズ] -> アンサ[0x11,サイズ,data1, ... , data58]
                ToSendDataBuffer[0] = 0x11;
                /* パラメータチェック 先頭アドレス(0x000000-0x1FFFFF)+データサイズ(1-58)<=0x200000 サイズ=1-58 */
                flash_address = ReceivedDataBuffer[1];
                flash_address = (flash_address << 8) | ReceivedDataBuffer[2];
                flash_address = (flash_address << 8) | ReceivedDataBuffer[3];
                flash_address = (flash_address << 8) | ReceivedDataBuffer[4];
                if((flash_address + ReceivedDataBuffer[5]) <= FLASH_SIZE && 1 <= ReceivedDataBuffer[5] && ReceivedDataBuffer[5] <= 62)
                {
                    ToSendDataBuffer[1] = ReceivedDataBuffer[5];
                    flash_n_read(flash_address, &ToSendDataBuffer[2], ReceivedDataBuffer[5]);
                }
                else
                {
                    // NGアンサ
                    ToSendDataBuffer[1] = 0xFF;
                }
                
                if(!HIDTxHandleBusy(USBInHandle))
                {
                    USBInHandle = HIDTxPacket(HID_EP4,(BYTE*)&ToSendDataBuffer[0], TX_DATA_BUFFER_SIZE);
                }
                break;
            case 0x12:  // Flashにデータを書込み [0x12,先頭アドレスMSB,,,LSB,サイズ,data1, ... ,data58]
                /* パラメータチェック 先頭アドレス(0x000000-0x1FFFFF)+データサイズ(1-58)<=0x200000 サイズ=1-58 */
                ToSendDataBuffer[0] = 0x12;
                flash_address = ReceivedDataBuffer[1];
                flash_address = (flash_address << 8) | ReceivedDataBuffer[2];
                flash_address = (flash_address << 8) | ReceivedDataBuffer[3];
                flash_address = (flash_address << 8) | ReceivedDataBuffer[4];
                if((flash_address + ReceivedDataBuffer[5]) <= FLASH_SIZE && 1 <= ReceivedDataBuffer[5] && ReceivedDataBuffer[5] <= 58)
                {
                    flash_n_write(flash_address, &ReceivedDataBuffer[6], ReceivedDataBuffer[5]);
                    // OKアンサ
                    ToSendDataBuffer[1] = 0x00;
                }
                else
                {
                    // NGアンサ
                    ToSendDataBuffer[1] = 0xFF;
                }
                if(!HIDTxHandleBusy(USBInHandle))
                {
                    USBInHandle = HIDTxPacket(HID_EP4,(BYTE*)&ToSendDataBuffer[0], TX_DATA_BUFFER_SIZE);
                }
                break;
            case 0x13:  // Flash セクター消去 [0x13,先頭アドレスMSB,,,LSB]
                /* パラメータチェック 先頭アドレス(0x000000-0x1FFFFF) */
                ToSendDataBuffer[0] = 0x13;
                flash_address = ReceivedDataBuffer[1];
                flash_address = (flash_address << 8) | ReceivedDataBuffer[2];
                flash_address = (flash_address << 8) | ReceivedDataBuffer[3];
                flash_address = (flash_address << 8) | ReceivedDataBuffer[4];
                if(( 0 <= flash_address) && (flash_address < FLASH_SIZE))
                {
                    flash_sector_erase(flash_address);
                    // OKアンサ
                    ToSendDataBuffer[1] = 0x00;
                }
                else
                {
                    // NGアンサ
                    ToSendDataBuffer[1] = 0xFF;
                }
                if(!HIDTxHandleBusy(USBInHandle))
                {
                    USBInHandle = HIDTxPacket(HID_EP4,(BYTE*)&ToSendDataBuffer[0], TX_DATA_BUFFER_SIZE);
                }
                break;
#if 0
            case 0x21:  // 基本設定モード情報取得 [0x21,Mode No.] -> アンサ[0x21,Ans,SW1実行スクリプト,...,SW11実行スクリプト,エンコーダーデフォルト機能No.,Mode LED Color No.,Mode LED Color Detail Flag,LED R Duty,LED G Duty,LED B Duty]
                ToSendDataBuffer[0] = 0x21;
                
                if(ReceivedDataBuffer[1] < MODE_NUM)
                {
                    for(fi = 0; fi < sizeof(UN_BASE_INFO); fi++)
                    {
                        ToSendDataBuffer[2+fi] = my_base_infos[ReceivedDataBuffer[1]].byte[fi];
                    }
                    
                    ToSendDataBuffer[1] = 0x00; // OK
                }
                else
                {
                    ToSendDataBuffer[1] = 0xFF; // NG
                }
                if(!HIDTxHandleBusy(USBInHandle))
                {
                    USBInHandle = HIDTxPacket(HID_EP4,(BYTE*)&ToSendDataBuffer[0], TX_DATA_BUFFER_SIZE);
                }
                break;
            case 0x22:  // 基本設定モード情報設定 [0x22,Mode No.,SW1実行スクリプト,...,SW11実行スクリプト,エンコーダーデフォルト機能No.,Mode LED Color No.,Mode LED Color Detail Flag,LED R Duty,LED G Duty,LED B Duty] -> アンサ[0x22,Ans]
                ToSendDataBuffer[0] = 0x22;
                
                if(ReceivedDataBuffer[1] < MODE_NUM)
                {
                    for(fi = 0; fi < sizeof(UN_BASE_INFO); fi++)
                    {
                        my_base_infos[ReceivedDataBuffer[1]].byte[fi] = ReceivedDataBuffer[2+fi];
                    }
                    
                    ToSendDataBuffer[1] = 0x00; // OK
                }
                else
                {
                    ToSendDataBuffer[1] = 0xFF; // NG
                }
                if(!HIDTxHandleBusy(USBInHandle))
                {
                    USBInHandle = HIDTxPacket(HID_EP4,(BYTE*)&ToSendDataBuffer[0], TX_DATA_BUFFER_SIZE);
                }
                break;
            case 0x23:  // 機能設定情報取得  [0x23,Mode No.,Func No.] -> アンサ[0x23,Ans,機能設定情報...]
                ToSendDataBuffer[0] = 0x23;
                
                if(ReceivedDataBuffer[1] < MODE_NUM && ReceivedDataBuffer[2] < MODE_FUNCTION_NUM)
                {
                    for(fi = 0; fi < sizeof(UN_FUNC_INFO); fi++)
                    {
                        ToSendDataBuffer[2+fi] = my_func_infos[ReceivedDataBuffer[1]][ReceivedDataBuffer[2]].byte[fi];
                    }
                    
                    ToSendDataBuffer[1] = 0x00; // OK
                }
                else
                {
                    ToSendDataBuffer[1] = 0xFF; // NG
                }
                if(!HIDTxHandleBusy(USBInHandle))
                {
                    USBInHandle = HIDTxPacket(HID_EP4,(BYTE*)&ToSendDataBuffer[0], TX_DATA_BUFFER_SIZE);
                }
                break;
            case 0x24:  // 機能設定情報設定 [0x24,Mode No.,Func No.,機能設定情報...] -> アンサ[0x24,Ans]
                ToSendDataBuffer[0] = 0x24;
                
                if(ReceivedDataBuffer[1] < MODE_NUM && ReceivedDataBuffer[2] < MODE_FUNCTION_NUM)
                {
                    for(fi = 0; fi < sizeof(UN_FUNC_INFO); fi++)
                    {
                        my_func_infos[ReceivedDataBuffer[1]][ReceivedDataBuffer[2]].byte[fi] = ReceivedDataBuffer[3+fi];
                    }
                    
                    ToSendDataBuffer[1] = 0x00; // OK
                }
                else
                {
                    ToSendDataBuffer[1] = 0xFF; // NG
                }
                if(!HIDTxHandleBusy(USBInHandle))
                {
                    USBInHandle = HIDTxPacket(HID_EP4,(BYTE*)&ToSendDataBuffer[0], TX_DATA_BUFFER_SIZE);
                }
                break;
#endif
            case 0x30:  // SW 状態取得 [0x30]
                ToSendDataBuffer[0] = 0x30;
                
                for(fi = 0; fi < SW_ALL_NUM; fi++)
                {
                    ToSendDataBuffer[1+fi] = sw_now_fix[fi];
                }
                
                if(!HIDTxHandleBusy(USBInHandle))
                {
                    USBInHandle = HIDTxPacket(HID_EP4,(BYTE*)&ToSendDataBuffer[0], TX_DATA_BUFFER_SIZE);
                }
                break;
            case 0x31:  // モード状態取得 [0x31] -> アンサ[0x31,モードNo.,機能No.]
                ToSendDataBuffer[0] = 0x31;
                
                ToSendDataBuffer[1] = mode_no_fix;
                ToSendDataBuffer[2] = mode_func_no_fix;
                
                if(!HIDTxHandleBusy(USBInHandle))
                {
                    USBInHandle = HIDTxPacket(HID_EP4,(BYTE*)&ToSendDataBuffer[0], TX_DATA_BUFFER_SIZE);
                }
                break;
            case 0x33:  // 基本設定　取得 [0x33] -> アンサ[0x33,Ans,Mode,led_sleep,led_light_mode,led_light_func,led_off_time,encoder_typematic]
                ToSendDataBuffer[0] = 0x33;
                ToSendDataBuffer[1] = 0x00; // Ans
                
                for(fi = 0; fi < sizeof(ST_BASE_HEAD); fi++)
                {
                    ToSendDataBuffer[2+fi] = my_base_head.byte[fi]; //                         
                }
                
                if(!HIDTxHandleBusy(USBInHandle))
                {
                    USBInHandle = HIDTxPacket(HID_EP4,(BYTE*)&ToSendDataBuffer[0], TX_DATA_BUFFER_SIZE);
                }
                break;
            case 0x34:  // 基本設定　設定 [0x34,Mode,led_sleep,led_light_mode,led_light_func,led_off_time,encoder_typematic] -> アンサ[0x34,Ans]
                ToSendDataBuffer[0] = 0x34;
                ToSendDataBuffer[1] = 0x00; // OK
//                ToSendDataBuffer[1] = 0xFF; // NG
                
                Set_Base_Head(&ReceivedDataBuffer[1], &my_base_head);
                
                if(!HIDTxHandleBusy(USBInHandle))
                {
                    USBInHandle = HIDTxPacket(HID_EP4,(BYTE*)&ToSendDataBuffer[0], TX_DATA_BUFFER_SIZE);
                }
                break;
            case 0x35:  // 基本設定情報　取得 [0x35,SW No.,] -> アンサ[0x35,Ans,Mode No.,SW1実行スクリプト,...,SW11実行スクリプト,SW1特殊機能,...,SW11特殊機能,エンコーダーデフォルト機能No.,Mode LED Color No.,Mode LED Color Detail Flag,LED R Duty,LED G Duty,LED B Duty,LED Brightness Level]
                ToSendDataBuffer[0] = 0x35;
                
                if(ReceivedDataBuffer[1] < MODE_NUM)
                {   
                    ToSendDataBuffer[1] = 0x00; // OK
                    
                    ToSendDataBuffer[2] = ReceivedDataBuffer[1]; // Mode No.
                    for(fi = 0; fi < sizeof(ST_BASE_INFO); fi++)
                    {
                        ToSendDataBuffer[3+fi] = my_base_infos[ReceivedDataBuffer[1]].byte[fi]; //                         
                    }
                }
                else
                {
                    ToSendDataBuffer[1] = 0xFF; // NG
                }
                
                if(!HIDTxHandleBusy(USBInHandle))
                {
                    USBInHandle = HIDTxPacket(HID_EP4,(BYTE*)&ToSendDataBuffer[0], TX_DATA_BUFFER_SIZE);
                }
                break;
            case 0x36:  // 基本設定情報　設定 [0x36,Mode No.,SW1実行スクリプト,...,SW11実行スクリプト,SW1特殊機能,...,SW11特殊機能,エンコーダーデフォルト機能No.,Mode LED Color No.,Mode LED Color Detail Flag,LED R Duty,LED G Duty,LED B Duty,LED Brightness Level] -> アンサ[0x36,Ans]
                ToSendDataBuffer[0] = 0x36;
                
                if(ReceivedDataBuffer[1] < MODE_NUM)
                {   
                    ToSendDataBuffer[1] = 0x00; // OK
                    
                    Set_Base_Info(ReceivedDataBuffer[1], &ReceivedDataBuffer[2], &my_base_infos[ReceivedDataBuffer[1]]);
//                    for(fi = 0; fi < sizeof(ST_BASE_INFO); fi++)
//                    {
//                        my_base_infos[ReceivedDataBuffer[1]].byte[fi] = ReceivedDataBuffer[2+fi];                      
//                    }
                    
                    led_change_flag_mode = 1;   // LED表示を即反映
                }
                else
                {
                    ToSendDataBuffer[1] = 0xFF; // NG
                }
                if(!HIDTxHandleBusy(USBInHandle))
                {
                    USBInHandle = HIDTxPacket(HID_EP4,(BYTE*)&ToSendDataBuffer[0], TX_DATA_BUFFER_SIZE);
                }
                break;
            case 0x37:  // 機能設定情報取得  [0x37,Mode No.,Func No.] -> アンサ[0x37,Ans,機能設定情報...]
                ToSendDataBuffer[0] = 0x37;
                
                if(ReceivedDataBuffer[1] < MODE_NUM && ReceivedDataBuffer[2] < MODE_FUNCTION_NUM)
                {
                    ToSendDataBuffer[1] = 0x00; // OK
                    
                    ToSendDataBuffer[2] = ReceivedDataBuffer[1];
                    ToSendDataBuffer[3] = ReceivedDataBuffer[2];
                    
                    for(fi = 0; fi < sizeof(UN_FUNC_INFO); fi++)
                    {
                        ToSendDataBuffer[4+fi] = my_func_infos[ReceivedDataBuffer[1]][ReceivedDataBuffer[2]].byte[fi];
                    }
                    
                }
                else
                {
                    ToSendDataBuffer[1] = 0xFF; // NG
                }
                if(!HIDTxHandleBusy(USBInHandle))
                {
                    USBInHandle = HIDTxPacket(HID_EP4,(BYTE*)&ToSendDataBuffer[0], TX_DATA_BUFFER_SIZE);
                }
                break;
            case 0x38:  // 機能設定情報設定 [0x38,Mode No.,Func No.,機能設定情報...] -> アンサ[0x38,Ans]
                ToSendDataBuffer[0] = 0x38;
                
                if(ReceivedDataBuffer[1] < MODE_NUM && ReceivedDataBuffer[2] < MODE_FUNCTION_NUM)
                {
                    Set_Func_Info(ReceivedDataBuffer[1], ReceivedDataBuffer[2], &ReceivedDataBuffer[3], &my_func_infos[ReceivedDataBuffer[1]][ReceivedDataBuffer[2]]);
                    
                    led_change_flag_func = 1;   // LED表示を即反映
                    
                    ToSendDataBuffer[1] = 0x00; // OK
                }
                else
                {
                    ToSendDataBuffer[1] = 0xFF; // NG
                }
                if(!HIDTxHandleBusy(USBInHandle))
                {
                    USBInHandle = HIDTxPacket(HID_EP4,(BYTE*)&ToSendDataBuffer[0], TX_DATA_BUFFER_SIZE);
                }
                break;
            case 0x39:  // ダイアルマクロ設定情報　取得 [0x39,No.,] -> アンサ[0x39,Ans,No.,RecNum,Script1,...,Script32]
                ToSendDataBuffer[0] = 0x39;
                
                if(ReceivedDataBuffer[1] < ENCODER_SCRIPT_NUM)
                {   
                    ToSendDataBuffer[1] = 0x00; // OK
                    
                    ToSendDataBuffer[2] = ReceivedDataBuffer[1]; // No.
                    ToSendDataBuffer[3] = my_encoder_script_infos[ReceivedDataBuffer[1]].encoder_script_info.rec_num; // Rec Num
                    ToSendDataBuffer[4] = my_encoder_script_infos[ReceivedDataBuffer[1]].encoder_script_info.loop_flag; // Loop Flag
                    for(fi = 0; fi < ENCODER_SCRIPT_SCRIPT_REC_MAX; fi++)
                    {
                        ToSendDataBuffer[5+fi] = my_encoder_script_infos[ReceivedDataBuffer[1]].encoder_script_info.encoder_script[fi]; //                         
                    }
                }
                else
                {
                    ToSendDataBuffer[1] = 0xFF; // NG
                }
                
                if(!HIDTxHandleBusy(USBInHandle))
                {
                    USBInHandle = HIDTxPacket(HID_EP4,(BYTE*)&ToSendDataBuffer[0], TX_DATA_BUFFER_SIZE);
                }
                break;
            case 0x3A:  // ダイアルマクロ設定情報　設定 [0x3A,No.,RecNum,Script1,...,Script32] -> アンサ[0x3A,Ans]
                ToSendDataBuffer[0] = 0x3A;
                
                if(ReceivedDataBuffer[1] < ENCODER_SCRIPT_NUM)
                {   
                    ToSendDataBuffer[1] = 0x00; // OK
                    
                    // データを一旦一時バッファにコピー
                    p_encoder_script_infos[ReceivedDataBuffer[1]].encoder_script_info.rec_num = ReceivedDataBuffer[2];
                    p_encoder_script_infos[ReceivedDataBuffer[1]].encoder_script_info.loop_flag = ReceivedDataBuffer[3];
                    for(fi = 0; fi < ENCODER_SCRIPT_SCRIPT_REC_MAX; fi++)
                    {
                        p_encoder_script_infos[ReceivedDataBuffer[1]].encoder_script_info.encoder_script[fi] = ReceivedDataBuffer[4+fi]; //                         
                    }
                    
                    Set_EncoderScript_Info(ReceivedDataBuffer[1], &p_encoder_script_infos[ReceivedDataBuffer[1]], &my_encoder_script_infos[ReceivedDataBuffer[1]]);
                }
                else
                {
                    ToSendDataBuffer[1] = 0xFF; // NG
                }
                if(!HIDTxHandleBusy(USBInHandle))
                {
                    USBInHandle = HIDTxPacket(HID_EP4,(BYTE*)&ToSendDataBuffer[0], TX_DATA_BUFFER_SIZE);
                }
                break;
            case 0x3B:  // SW機能設定情報取得  [0x3B,Mode No.,SW No.] -> アンサ[0x3B,Ans,Mode No.,SW No.,Data0,...,Data7]
                ToSendDataBuffer[0] = 0x3B;
                
                if(ReceivedDataBuffer[1] < MODE_NUM && ReceivedDataBuffer[2] < SW_NUM)
                {
                    ToSendDataBuffer[1] = 0x00; // OK
                    
                    ToSendDataBuffer[2] = ReceivedDataBuffer[1];
                    ToSendDataBuffer[3] = ReceivedDataBuffer[2];
                    
                    for(fi = 0; fi < sizeof(UN_SW_FUNC_INFO); fi++)
                    {
                        ToSendDataBuffer[4+fi] = my_sw_func_infos[ReceivedDataBuffer[1]][ReceivedDataBuffer[2]].byte[fi];
                    }
                    
                }
                else
                {
                    ToSendDataBuffer[1] = 0xFF; // NG
                }
                if(!HIDTxHandleBusy(USBInHandle))
                {
                    USBInHandle = HIDTxPacket(HID_EP4,(BYTE*)&ToSendDataBuffer[0], TX_DATA_BUFFER_SIZE);
                }
                break;
            case 0x3C:  // SW機能設定情報設定 [0x3C,Mode No.,SW No.,Data0,...,Data7] -> アンサ[0x3C,Ans]
                ToSendDataBuffer[0] = 0x3C;
                
                if(ReceivedDataBuffer[1] < MODE_NUM && ReceivedDataBuffer[2] < SW_NUM)
                {
                    Set_SW_Func_Info(ReceivedDataBuffer[1], ReceivedDataBuffer[2], &ReceivedDataBuffer[3], &my_sw_func_infos[ReceivedDataBuffer[1]][ReceivedDataBuffer[2]]);
                    
                    ToSendDataBuffer[1] = 0x00; // OK
                }
                else
                {
                    ToSendDataBuffer[1] = 0xFF; // NG
                }
                if(!HIDTxHandleBusy(USBInHandle))
                {
                    USBInHandle = HIDTxPacket(HID_EP4,(BYTE*)&ToSendDataBuffer[0], TX_DATA_BUFFER_SIZE);
                }
                break;
            case 0x61:  //SW状態取得
                ToSendDataBuffer[0] = 0x61;				//Echo back to the host PC the command we are fulfilling in the first byte.  In this case, the Get Pushbutton State command.
				ToSendDataBuffer[1] = SW_ALL_NUM;
				for(fi = 0; fi < SW_NUM; fi++)
				{
					ToSendDataBuffer[2+fi] = sw_now_fix[fi];
				}
				ToSendDataBuffer[2+SW_NUM] = encoder_input_state;
				ToSendDataBuffer[2+SW_NUM+1] = ENCORDER_A;
				ToSendDataBuffer[2+SW_NUM+2] = ENCORDER_B;

                if(!HIDTxHandleBusy(USBInHandle))
                {
                    USBInHandle = HIDTxPacket(HID_EP4,(BYTE*)&ToSendDataBuffer[0],TX_DATA_BUFFER_SIZE);
                }
                break;
            case 0x62:  //LED出力状態取得
                ToSendDataBuffer[0] = 0x62;				//Echo back to the host PC the command we are fulfilling in the first byte.  In this case, the Get Pushbutton State command.
				ToSendDataBuffer[1] = LED_DATA_NUM;
                ToSendDataBuffer[2] = led_output_brightness_level_fix;
				for(fi = 0; fi < LED_DATA_NUM; fi++)
				{
                    // LED RGB の Dutyセット
					ToSendDataBuffer[3+fi] = led_output_fix[fi];
				}

                if(!HIDTxHandleBusy(USBInHandle))
                {
                    USBInHandle = HIDTxPacket(HID_EP4,(BYTE*)&ToSendDataBuffer[0],TX_DATA_BUFFER_SIZE);
                }
                break;
            case 0x63:  //LED出力設定
                ToSendDataBuffer[0] = 0x63;				//Echo back to the host PC the command we are fulfilling in the first byte.  In this case, the Get Pushbutton State command.
				
				if(ReceivedDataBuffer[1] == LED_DATA_NUM)
				{   // LED出力値設定
                    led_output_brightness_level_fix = ReceivedDataBuffer[2];
					for(fi = 0; fi < LED_DATA_NUM; fi++)
					{
                        // LED RGB の Dutyセット
                        if(ReceivedDataBuffer[3+fi] <= LED_DUTY_MAX)
                        {
                            led_output_fix[fi] = ReceivedDataBuffer[3+fi];
                        }
                        else
                        {
                            led_output_fix[fi] = LED_DUTY_MAX;
                        }
					}
					ToSendDataBuffer[1] = 0x00;	// OK ans
				}
				else
				{	// データ数異常
					ToSendDataBuffer[1] = 0xFF;	// NG ans
				}

                if(!HIDTxHandleBusy(USBInHandle))
                {
                    USBInHandle = HIDTxPacket(HID_EP4,(BYTE*)&ToSendDataBuffer[0],TX_DATA_BUFFER_SIZE);
                }
                break;
            case 0x64:  //LED出力設定（プレビュー）
                ToSendDataBuffer[0] = 0x64;				//Echo back to the host PC the command we are fulfilling in the first byte.  In this case, the Get Pushbutton State command.
				
				if(ReceivedDataBuffer[1] == LED_DATA_NUM)
				{   // デバッグ出力開始
                    w_temp = ReceivedDataBuffer[3];
                    w_temp = (w_temp << 8) | ReceivedDataBuffer[2];
                    led_output_brightness_level_preview = ReceivedDataBuffer[4];
                    
                    set_LED_output_data(0, 1, &ReceivedDataBuffer[5], led_output_preview, led_output_brightness_level_preview);
//					for(fi = 0; fi < LED_DATA_NUM; fi++)
//					{
//                        // LED RGB の Dutyセット
//                        if(ReceivedDataBuffer[5+fi] <= LED_DUTY_MAX)
//                        {
//                            led_output_preview[fi] = ReceivedDataBuffer[5+fi];
//                        }
//                        else
//                        {
//                            led_output_preview[fi] = LED_DUTY_MAX;
//                        }
//					}
                    led_output_preview_time_counter = w_temp;

					ToSendDataBuffer[1] = 0x00;	// OK ans
				}
				else
				{	// データ数異常
					ToSendDataBuffer[1] = 0xFF;	// NG ans
				}

                if(!HIDTxHandleBusy(USBInHandle))
                {
                    USBInHandle = HIDTxPacket(HID_EP4,(BYTE*)&ToSendDataBuffer[0],TX_DATA_BUFFER_SIZE);
                }
                break;
#if 1	//DEBUG
            case 0x40:
                ToSendDataBuffer[0] = 0x40;				//Echo back to the host PC the command we are fulfilling in the first byte.  In this case, the Get Pushbutton State command.
                ToSendDataBuffer[1] = debug_arr1[0];
                ToSendDataBuffer[2] = debug_arr1[1];
                ToSendDataBuffer[3] = debug_arr1[2];
                ToSendDataBuffer[4] = debug_arr1[3];
                ToSendDataBuffer[5] = debug_arr1[4];
                ToSendDataBuffer[6] = debug_arr1[5];
                ToSendDataBuffer[7] = debug_arr1[6];
                ToSendDataBuffer[8] = debug_arr1[7];
                ToSendDataBuffer[9] = debug_arr1[8];
                ToSendDataBuffer[10] = debug_arr1[9];
                ToSendDataBuffer[11] = debug_arr1[10];
                ToSendDataBuffer[12] = debug_arr1[11];
                ToSendDataBuffer[13] = debug_arr1[12];
                ToSendDataBuffer[14] = debug_arr1[13];
                ToSendDataBuffer[15] = debug_arr1[14];
                ToSendDataBuffer[16] = debug_arr1[15];

                if(!HIDTxHandleBusy(USBInHandle))
                {
                    USBInHandle = HIDTxPacket(HID_EP4,(BYTE*)&ToSendDataBuffer[0],TX_DATA_BUFFER_SIZE);
                }
                break;
            case 0x41:
                ToSendDataBuffer[0] = 0x41;				//Echo back to the host PC the command we are fulfilling in the first byte.  In this case, the Get Pushbutton State command.
#if 0
                ToSendDataBuffer[1] = debug_arr2[0];
                ToSendDataBuffer[2] = debug_arr2[1];
                ToSendDataBuffer[3] = debug_arr2[2];
                ToSendDataBuffer[4] = debug_arr2[3];
                ToSendDataBuffer[5] = debug_arr2[4];
                ToSendDataBuffer[6] = debug_arr2[5];
                ToSendDataBuffer[7] = debug_arr2[6];
                ToSendDataBuffer[8] = debug_arr2[7];
                ToSendDataBuffer[9] = debug_arr2[8];
                ToSendDataBuffer[10] = debug_arr2[9];
                ToSendDataBuffer[11] = debug_arr2[10];
                ToSendDataBuffer[12] = debug_arr2[11];
                ToSendDataBuffer[13] = debug_arr2[12];
                ToSendDataBuffer[14] = debug_arr2[13];
                ToSendDataBuffer[15] = debug_arr2[14];
                ToSendDataBuffer[16] = debug_arr2[15];
#endif
#if 1
                ToSendDataBuffer[1] = debug_arr2[0];
                ToSendDataBuffer[2] = debug_arr2[1];
                ToSendDataBuffer[3] = debug_arr2[2];
                ToSendDataBuffer[4] = debug_arr2[3];
                ToSendDataBuffer[5] = debug_arr2[4];
                ToSendDataBuffer[6] = debug_arr2[5];
                ToSendDataBuffer[7] = debug_arr2[6];
                ToSendDataBuffer[8] = debug_arr2[7];
                ToSendDataBuffer[9] = debug_arr2[8];
                ToSendDataBuffer[10] = debug_arr2[9];
                ToSendDataBuffer[11] = debug_arr2[10];
                ToSendDataBuffer[12] = debug_arr2[11];
                ToSendDataBuffer[13] = debug_arr2[12];
                ToSendDataBuffer[14] = mode_func_no_fix;
                ToSendDataBuffer[15] = mode_func_no_pre_led;
                ToSendDataBuffer[16] = led_change_flag_func;
#endif

                if(!HIDTxHandleBusy(USBInHandle))
                {
                    USBInHandle = HIDTxPacket(HID_EP4,(BYTE*)&ToSendDataBuffer[0],TX_DATA_BUFFER_SIZE);
                }
                break;
            case 0x42:
                ToSendDataBuffer[0] = 0x42;				//Echo back to the host PC the command we are fulfilling in the first byte.  In this case, the Get Pushbutton State command.
#if 1
                ToSendDataBuffer[1] = debug_arr3[0];
                ToSendDataBuffer[2] = debug_arr3[1];
                ToSendDataBuffer[3] = debug_arr3[2];
                ToSendDataBuffer[4] = debug_arr3[3];
                ToSendDataBuffer[5] = debug_arr3[4];
                ToSendDataBuffer[6] = debug_arr3[5];
                ToSendDataBuffer[7] = debug_arr3[6];
                ToSendDataBuffer[8] = debug_arr3[7];
                ToSendDataBuffer[9] = debug_arr3[8];
                ToSendDataBuffer[10] = debug_arr3[9];
                ToSendDataBuffer[11] = debug_arr3[10];
                ToSendDataBuffer[12] = debug_arr3[11];
                ToSendDataBuffer[13] = debug_arr3[12];
                ToSendDataBuffer[14] = debug_arr3[13];
                ToSendDataBuffer[15] = debug_arr3[14];
                ToSendDataBuffer[16] = debug_arr3[15];
#endif
#if 0
                ToSendDataBuffer[1] = sw_now_fix[0];
                ToSendDataBuffer[2] = sw_now_fix[1];
                ToSendDataBuffer[3] = sw_now_fix[2];
                ToSendDataBuffer[4] = sw_now_fix[3];
                ToSendDataBuffer[5] = sw_now_fix[4];
                ToSendDataBuffer[6] = sw_now_fix[5];
                ToSendDataBuffer[7] = sw_now_fix[6];
                ToSendDataBuffer[8] = sw_now_fix[7];
                ToSendDataBuffer[9] = sw_now_fix[8];
                ToSendDataBuffer[10] = sw_now_fix[9];
                ToSendDataBuffer[11] = sw_now_fix[10];
                ToSendDataBuffer[12] = state_re;
                ToSendDataBuffer[13] = ENCORDER_A;
                ToSendDataBuffer[14] = ENCORDER_B;
                ToSendDataBuffer[15] = 0;
                ToSendDataBuffer[16] = 0;
#endif
#if 0
                ToSendDataBuffer[1] = my_base_head.byte[0];
                ToSendDataBuffer[2] = my_base_head.byte[1];
                ToSendDataBuffer[3] = my_base_head.byte[2];
                ToSendDataBuffer[4] = my_base_head.byte[3];
                ToSendDataBuffer[5] = my_base_head.byte[4];
                ToSendDataBuffer[6] = my_base_head.byte[5];
                ToSendDataBuffer[7] = my_base_head.byte[6];
                ToSendDataBuffer[8] = my_base_head.byte[7];
                ToSendDataBuffer[9] = led_light_on_type;
                ToSendDataBuffer[10] = led_light_func_type;
                ToSendDataBuffer[11] = led_light_status;
                ToSendDataBuffer[12] = (BYTE)((led_light_time_counter >> 8) & 0xFF);
                ToSendDataBuffer[13] = (BYTE)(led_light_time_counter & 0xFF);
                ToSendDataBuffer[14] = debug_arr3[13];
                ToSendDataBuffer[15] = debug_arr3[14];
                ToSendDataBuffer[16] = debug_arr3[15];
#endif
#if 0
                ToSendDataBuffer[1] = my_encoder_script_infos[0].encoder_script_info.rec_num;
                ToSendDataBuffer[2] = my_encoder_script_infos[0].encoder_script_info.loop_flag;
                ToSendDataBuffer[3] = my_encoder_script_infos[0].encoder_script_info.encoder_script[0];
                ToSendDataBuffer[4] = my_encoder_script_infos[0].encoder_script_info.encoder_script[1];
                ToSendDataBuffer[5] = my_encoder_script_infos[0].encoder_script_info.encoder_script[2];
                ToSendDataBuffer[6] = my_encoder_script_infos[0].encoder_script_info.encoder_script[3];
                ToSendDataBuffer[7] = my_encoder_script_infos[0].encoder_script_info.encoder_script[4];
                ToSendDataBuffer[8] = my_encoder_script_infos[0].encoder_script_info.encoder_script[5];
                ToSendDataBuffer[9] = my_encoder_script_infos[0].encoder_script_info.encoder_script[6];
                ToSendDataBuffer[10] = my_encoder_script_infos[0].encoder_script_info.encoder_script[7];
                ToSendDataBuffer[11] = my_encoder_script_infos[0].encoder_script_info.encoder_script[8];
                ToSendDataBuffer[12] = my_encoder_script_infos[0].encoder_script_info.encoder_script[9];
                ToSendDataBuffer[13] = my_encoder_script_infos[0].encoder_script_info.encoder_script[31];
                ToSendDataBuffer[14] = encoder_script_exe_now_idx[0];
                ToSendDataBuffer[15] = encoder_script_exe_now_idx[1];
                ToSendDataBuffer[16] = encoder_script_exe_now_idx[2];
#endif
                if(!HIDTxHandleBusy(USBInHandle))
                {
                    USBInHandle = HIDTxPacket(HID_EP4,(BYTE*)&ToSendDataBuffer[0],TX_DATA_BUFFER_SIZE);
                }
                break;
#endif	//DEBUG
        }
        //Re-arm the OUT endpoint, so we can receive the next OUT data packet
        //that the host may try to send us.
        USBOutHandle = HIDRxPacket(HID_EP4, (BYTE*)&ReceivedDataBuffer, RX_DATA_BUFFER_SIZE);
    }


//    return;
}//end USB_Comm()


