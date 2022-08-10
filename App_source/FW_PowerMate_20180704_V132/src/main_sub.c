#include "Main_sub.h"
#include "l_script.h"


    
// *****************************************************************************
// *****************************************************************************
// Section: Software Delay
// *****************************************************************************
// *****************************************************************************
void Soft_Delay(DWORD delayValue)
{
    DWORD tmp32=0;

    TMR4 = 0;
    TMR5 = 0;
    while(1)
    {
        tmp32 = TMR5;                   // Timer32 上位16bit
        tmp32 = (tmp32 << 16) | TMR4;   // Timer32 下位16bit
        if(tmp32 >= delayValue)
        {
            break;
        }
    }
}

void Switch_Input(void)
{
    BYTE fi, fj;
    BYTE sw_now[SW_NUM];
	BYTE exe_script_no;
	BYTE script_attribute;

    sw_now[SW_1_NO_IDX] 	= SW_1_NO;
    sw_now[SW_2_NO_IDX] 	= SW_2_NO;
    sw_now[SW_3_NO_IDX] 	= SW_3_NO;
    sw_now[SW_4_NO_IDX] 	= SW_4_NO;
    sw_now[SW_5_NO_IDX] 	= SW_5_NO;
    sw_now[SW_6_NO_IDX] 	= SW_6_NO;
    sw_now[SW_7_NO_IDX] 	= SW_7_NO;
    sw_now[SW_8_NO_IDX] 	= SW_8_NO;
    sw_now[SW_9_NO_IDX] 	= SW_9_NO;
    sw_now[SW_10_NO_IDX] 	= SW_10_NO;
    sw_now[SW_11_NO_IDX] 	= SW_11_NO;

    for( fi = 0; fi < SW_NUM; fi++ )
    {
        if(sw_now[fi] == R_ON)
        {
            sw_press_on_cnt[fi]++;
            if( sw_press_on_cnt[fi] >= SW_ON_DEBOUNCE_TIME)
            {
                sw_press_off_cnt[fi] = 0;
                sw_now_fix[fi] = N_ON;
                sw_press_on_cnt[fi] = SW_ON_DEBOUNCE_TIME;
                
                if(USB_Sleep_Flag == 1)
                {
                    USBCBSendResume();
                }
            }
        }
        else
        {
            sw_press_off_cnt[fi]++;
            if(sw_press_off_cnt[fi] >= SW_OFF_DEBOUNCE_TIME)
            {
                sw_press_on_cnt[fi] = 0;
                sw_now_fix[fi] = N_OFF;
                sw_press_off_cnt[fi] = SW_OFF_DEBOUNCE_TIME;
            }
        }

        // SW Push時の処理追加
        if( sw_now_fix_pre[fi] == N_OFF && sw_now_fix[fi] == N_ON )
        {	// 前回=OFF  今回=ON
            
            led_change_flag_mode = 1;   // 操作でLED点灯
            
            // 特殊機能チェック
            if(my_base_infos[mode_no_fix].BaseInfo.sw_sp_func_no[fi] > 0)
            {   // 特殊機能設定あり
                if(SW_SP_FUNC_MODE_CHANGE_MIN <= my_base_infos[mode_no_fix].BaseInfo.sw_sp_func_no[fi] && my_base_infos[mode_no_fix].BaseInfo.sw_sp_func_no[fi] <= SW_SP_FUNC_MODE_CHANGE_MAX)
                {   // モード変更
                    switch(my_base_infos[mode_no_fix].BaseInfo.sw_sp_func_no[fi])
                    {
                        case SW_SP_FUNC_MODE:
                            mode_no_set++;
                            if(mode_no_set >= MODE_NUM)
                            {
                                mode_no_set = MODE1_NO;
                            }
                            mode_no_fix = mode_no_set;
                            mode_func_no_set = my_base_infos[mode_no_fix].BaseInfo.encoder_func_no;
                            mode_func_no_fix = mode_func_no_set;
                            mode_func_no_pre_led = mode_func_no_fix;    // 機能変更によるLEDは変更しない
                            led_func_lighting_flag = 0;
                            break;
                        case SW_SP_FUNC_MODE1:
                            mode_no_set = MODE1_NO;
                            mode_no_fix = MODE1_NO;
                            mode_func_no_set = my_base_infos[mode_no_fix].BaseInfo.encoder_func_no;
                            mode_func_no_fix = mode_func_no_set;
                            mode_func_no_pre_led = mode_func_no_fix;    // 機能変更によるLEDは変更しない
                            led_func_lighting_flag = 0;
                            break;
                        case SW_SP_FUNC_MODE2:
                            mode_no_set = MODE2_NO;
                            mode_no_fix = MODE2_NO;
                            mode_func_no_set = my_base_infos[mode_no_fix].BaseInfo.encoder_func_no;
                            mode_func_no_fix = mode_func_no_set;
                            mode_func_no_pre_led = mode_func_no_fix;    // 機能変更によるLEDは変更しない
                            led_func_lighting_flag = 0;
                            break;
                        case SW_SP_FUNC_MODE3:
                            mode_no_set = MODE3_NO;
                            mode_no_fix = MODE3_NO;
                            mode_func_no_set = my_base_infos[mode_no_fix].BaseInfo.encoder_func_no;
                            mode_func_no_fix = mode_func_no_set;
                            mode_func_no_pre_led = mode_func_no_fix;    // 機能変更によるLEDは変更しない
                            led_func_lighting_flag = 0;
                            break;
                    }
                    led_change_flag_mode = 1;
                }
                else if((SW_SP_FUNC_FUNC_CHANGE_MIN <= my_base_infos[mode_no_fix].BaseInfo.sw_sp_func_no[fi] && my_base_infos[mode_no_fix].BaseInfo.sw_sp_func_no[fi] <= SW_SP_FUNC_FUNC_CHANGE_MAX)
                        || (SW_SP_FUNC_FUNC_CHANGE_MIN2 <= my_base_infos[mode_no_fix].BaseInfo.sw_sp_func_no[fi] && my_base_infos[mode_no_fix].BaseInfo.sw_sp_func_no[fi] <= SW_SP_FUNC_FUNC_CHANGE_MAX2))
                {   // 機能変更
                    switch(my_base_infos[mode_no_fix].BaseInfo.sw_sp_func_no[fi])
                    {
                        case SW_SP_FUNC_FUNC:
                            for(fj = 0; fj < MODE_FUNCTION_NUM; fj++)
                            {
                                mode_func_no_set++;
                                if(mode_func_no_set >= MODE_FUNCTION_NUM)
                                {
                                    mode_func_no_set = MODE_FUNCTION1_NO;
                                }
                                
                                // 設定済みかチェック、未設定なら次
                                if(my_func_infos[mode_no_fix][mode_func_no_set].func_info.FuncCWData[DEVICE_DATA_SET_TYPE_IDX] != 0
                                        || my_func_infos[mode_no_fix][mode_func_no_set].func_info.FuncCCWData[DEVICE_DATA_SET_TYPE_IDX] != 0)
                                {   // 設定ありの場合は抜ける
                                    break;
                                }
                            }
                            mode_func_no_fix = mode_func_no_set;
                            led_change_flag_func = 1;   // LED点灯
                            break;
                        case SW_SP_FUNC_FUNC1:
                            mode_func_no_set = MODE_FUNCTION1_NO;
                            mode_func_no_fix = MODE_FUNCTION1_NO;
                            led_change_flag_func = 1;   // LED点灯
                            break;
                        case SW_SP_FUNC_FUNC2:
                            mode_func_no_set = MODE_FUNCTION2_NO;
                            mode_func_no_fix = MODE_FUNCTION2_NO;
                            led_change_flag_func = 1;   // LED点灯
                            break;
                        case SW_SP_FUNC_FUNC3:
                            mode_func_no_set = MODE_FUNCTION3_NO;
                            mode_func_no_fix = MODE_FUNCTION3_NO;
                            led_change_flag_func = 1;   // LED点灯
                            break;
                        case SW_SP_FUNC_FUNC4:
                            mode_func_no_set = MODE_FUNCTION4_NO;
                            mode_func_no_fix = MODE_FUNCTION4_NO;
                            led_change_flag_func = 1;   // LED点灯
                            break;
                    }
                    led_change_flag_func = 1;
                }
                else if(SW_SP_FUNC_FUNC_CHANGE_TEMP_MIN <= my_base_infos[mode_no_fix].BaseInfo.sw_sp_func_no[fi] && my_base_infos[mode_no_fix].BaseInfo.sw_sp_func_no[fi] <= SW_SP_FUNC_FUNC_CHANGE_TEMP_MAX)
                {   // 機能変更一時
                    switch(my_base_infos[mode_no_fix].BaseInfo.sw_sp_func_no[fi])
                    {
                        case SW_SP_FUNC_FUNC1_TEMP:
//                            mode_func_no_set = MODE_FUNCTION1_NO;
                            mode_func_no_fix = MODE_FUNCTION1_NO;
                            led_change_flag_func = 1;   // LED点灯
                            led_light_on_type_temp_func = led_light_on_type;
                            break;
                        case SW_SP_FUNC_FUNC2_TEMP:
//                            mode_func_no_set = MODE_FUNCTION2_NO;
                            mode_func_no_fix = MODE_FUNCTION2_NO;
                            led_change_flag_func = 1;   // LED点灯
                            led_light_on_type_temp_func = led_light_on_type;
                            break;
                        case SW_SP_FUNC_FUNC3_TEMP:
//                            mode_func_no_set = MODE_FUNCTION3_NO;
                            mode_func_no_fix = MODE_FUNCTION3_NO;
                            led_change_flag_func = 1;   // LED点灯
                            led_light_on_type_temp_func = led_light_on_type;
                            break;
                        case SW_SP_FUNC_FUNC4_TEMP:
//                            mode_func_no_set = MODE_FUNCTION4_NO;
                            mode_func_no_fix = MODE_FUNCTION4_NO;
                            led_change_flag_func = 1;   // LED点灯
                            led_light_on_type_temp_func = led_light_on_type;
                            break;
                    }
                    led_change_flag_func = 1;
                }
            }
            else if(my_base_infos[mode_no_fix].BaseInfo.sw_exe_script_no[fi] > 0)
            {   // スクリプト設定あり
                exe_script_no = Get_Exe_Script_No(0);		// 実行中のスクリプトNo.取得
                script_attribute = Get_Script_Attribute(0);	// スクリプト属性取得
                if(exe_script_no > 0)
                {	// すでにスクリプトを実行中

                    // ループモードで同じスクリプト実行要求の時は停止
                    if(script_attribute == SCRIPT_EXE_CTRL_LOOP_MODE && exe_script_no == my_base_infos[mode_no_fix].BaseInfo.sw_exe_script_no[fi])
                    {
                        Script_Stop(0);
                        Script_Reset(0);
                    }
                    else
                    {
                        // 即実行
                        next_script_exe_req_scriptNo = 0;
                        Script_Stop(0);
                        Set_Script_Exe_Info(0, my_base_infos[mode_no_fix].BaseInfo.sw_exe_script_no[fi]);
                        Script_Reset(0);
                        Script_Run(0);
                        // 終了後実行
                        next_script_exe_req_scriptNo = my_base_infos[mode_no_fix].BaseInfo.sw_exe_script_no[fi];
                         // 無視
                        next_script_exe_req_scriptNo = 0;
                        
                        keyboard_output_stop_by_macro = 1;
                    }
                }
                else
                {	// スクリプト実行中でない　とき
                    Set_Script_Exe_Info(0, my_base_infos[mode_no_fix].BaseInfo.sw_exe_script_no[fi]);
                    Script_Reset(0);
                    Script_Run(0);
                }
            }
            else if(my_sw_func_infos[mode_no_fix][fi].sw_func_info.SWFuncData[DEVICE_DATA_SET_TYPE_IDX] > 0)
            {   // SW 機能設定あり
                sw_func_out_flag[fi] = N_ON;
            }
            // SWの場合は、状態セット
//            sw_output_flag[fi] = sw_now_fix[fi];
        }
        else if( sw_now_fix_pre[fi] == N_ON && sw_now_fix[fi] == N_OFF )
        {	// 前回=ON  今回=OFF
            
            // 特殊機能チェック
            if(my_base_infos[mode_no_fix].BaseInfo.sw_sp_func_no[fi] > 0)
            {   // 特殊機能設定あり
                if(SW_SP_FUNC_FUNC_CHANGE_TEMP_MIN <= my_base_infos[mode_no_fix].BaseInfo.sw_sp_func_no[fi] && my_base_infos[mode_no_fix].BaseInfo.sw_sp_func_no[fi] <= SW_SP_FUNC_FUNC_CHANGE_TEMP_MAX)
                {   // 機能変更一時のため、もとに戻す
                    mode_func_no_fix = mode_func_no_set;
                    if(led_light_on_type_temp_func == LED_LIGHT_ON_TYPE_FUNC)
                    {   // 機能一時変更前のLED点灯が機能の場合
                        led_change_flag_func = 1;
                    }
                    else if(led_light_on_type_temp_func == LED_LIGHT_ON_TYPE_MODE)
                    {   // 機能一時変更前のLED点灯がモードの場合
                        led_func_lighting_flag = 0;
                        led_change_flag_mode = 1;
                    }
                    
                    // エンコーダーSWで機能一時変更後にエンコーダー入力を一定時間無効にする時間セット
                    if(fi == SW_11_NO_IDX)
                    {
                        dial_function_enocder_disabled_time = DIAL_FUNC_TEMP_CHANGE_ENCODER_DISABLED_TIME;
                    }
                }
            }
            //スクリプトの場合でファイヤーモードのときは停止
            else if( Script_Fire_Mode_Stop( 0, my_base_infos[mode_no_fix].BaseInfo.sw_exe_script_no[fi]) == 0 )
            {	// 停止した場合はリセット
               Script_Reset(0);
            }
            //スクリプトの場合でホールドモードのときは停止
            else if( Script_Hold_Mode_Stop( 0, my_base_infos[mode_no_fix].BaseInfo.sw_exe_script_no[fi]) == 0 )
            {	// 停止した場合はリセット
               Script_Reset(0);
            }
            // SWの場合は、状態セット
//            sw_output_flag[fi] = sw_now_fix[fi];
            sw_func_out_flag[fi] = N_OFF;
            sw_output_mouse_first_flag[fi] = N_OFF;
            sw_output_multimedia_flag[fi] = N_OFF;
            sw_output_joystick_first_flag[fi] = N_OFF;

            // SW操作によるマウス出力をクリア
            if(set_type_move_sw_no == fi)
            {
                set_type_move_sw_no = 0xFF;
            }
            if(set_type_ws_sw_no == fi)
            {
                set_type_ws_sw_no = 0xFF;
            }
            // SW操作によるジョイスティック出力をクリア
            if(set_type_hat_sw_no == fi)
            {
                set_type_hat_sw_no = 0xFF;
            }
            if(set_type_xy_sw_no == fi)
            {
                set_type_xy_sw_no = 0xFF;
            }
            if(set_type_zrz_sw_no == fi)
            {
                set_type_zrz_sw_no = 0xFF;
            }
        }
        // SWの場合は、状態セット
        sw_output_flag[fi] = sw_now_fix[fi];

        // 今回値保存
        sw_now_fix_pre[fi] = sw_now_fix[fi];
    }
}
void Encoder_Input(void)
{
	char tmp;
    
//	エンコーダー入力
	encoder_input_state = ENCORDER_A + (ENCORDER_B << 1);
//ロータリエンコーダの処理
	tmp = 0;
    // チャタリング防止に、前回と同じだったら入力処理
    if(encoder_input_state == encoder_input_state_pre)
    {
        encoder_input_cnt++;
        if(encoder_input_cnt > ENCORDER_INPUT_COUNT)
        {
            encoder_input_cnt = ENCORDER_INPUT_COUNT;
        }
    }
    else
    {
        encoder_input_cnt = 0;
    }
    encoder_input_state_pre = encoder_input_state;
    if(encoder_input_cnt >= ENCORDER_INPUT_COUNT)
    {
        switch(encoder_input_fix_pre)
        {
            case 0: tmp = (encoder_input_state == 0) ? 0    : (encoder_input_state == 1) ? 1    : (encoder_input_state == 2) ? -1   : 0; break;
            case 1: tmp = (encoder_input_state == 0) ? -1   : (encoder_input_state == 1) ? 0    : (encoder_input_state == 2) ? 0    : 1; break;
            case 2: tmp = (encoder_input_state == 0) ? 1    : (encoder_input_state == 1) ? 0    : (encoder_input_state == 2) ? 0    : -1; break;
            case 3: tmp = (encoder_input_state == 0) ? 0    : (encoder_input_state == 1) ? -1   : (encoder_input_state == 2) ? 1    : 0; break;
            default: break;
        }
        encoder_input_fix_pre = encoder_input_state;
    }
    
    
#if 1
    // 試作時 -1, 台湾量産 1
	if(dial_function_enocder_disabled_time == 0 && tmp == 1)
	{   //CCW
//debug_arr1[7]++;
        encoder_input_puls_cw = 0;
        encoder_input_puls_ccw++;
        if(encoder_input_puls_ccw >= ENCORDER_INPUT_PLUS_COUNT)
        {
//            encoder_input_puls_ccw = 0;
            encoder_input_puls_ccw = ENCORDER_INPUT_PLUS_COUNT;
            
            led_change_flag_mode = 1;   // 操作でLED点灯
            
            temp_input_sense_left += my_func_infos[mode_no_fix][mode_func_no_fix].func_info.FuncCCWData[DEVICE_DATA_SENSE_IDX];
            temp_input_sense_right = 0;
            rotate_state++;
            if(rotate_state == 200)
            {
                rotate_state = 0;
            }

            if(temp_input_sense_left >= MASTER_INPUT_SENSE_MAX)
            {
debug_arr1[8]++;
                temp_input_sense_left -= MASTER_INPUT_SENSE_MAX;
                sw_output_flag[SW_ENCORDER_A_IDX] = N_ON;
                encoder_key_out_flag[SW_ENCORDER_B_IDX] = 0;
            }
        }
	}
    // 試作時 1, 台湾量産 -1
	else if(dial_function_enocder_disabled_time == 0 && tmp == -1)
	{   // CW
//debug_arr1[9]++;
        encoder_input_puls_ccw = 0;
        encoder_input_puls_cw++;
        if(encoder_input_puls_cw >= ENCORDER_INPUT_PLUS_COUNT)
        {
//            encoder_input_puls_cw = 0;
            encoder_input_puls_cw = ENCORDER_INPUT_PLUS_COUNT;

            led_change_flag_mode = 1;   // 操作でLED点灯
            
            temp_input_sense_right += my_func_infos[mode_no_fix][mode_func_no_fix].func_info.FuncCWData[DEVICE_DATA_SENSE_IDX];
            temp_input_sense_left = 0;		
            if(rotate_state == 0)
            {
                rotate_state = 199;
            }
            else
            {
                rotate_state--;
            }

            if(temp_input_sense_right >= MASTER_INPUT_SENSE_MAX)
            {
debug_arr1[10]++;
                temp_input_sense_right -= MASTER_INPUT_SENSE_MAX;
                sw_output_flag[SW_ENCORDER_B_IDX] = N_ON;
                encoder_key_out_flag[SW_ENCORDER_A_IDX] = 0;
            }
        }
	}
    else
    {
        // 一旦リセット 2018.03.20
        sw_output_flag[SW_ENCORDER_A_IDX] = N_OFF;
        sw_output_flag[SW_ENCORDER_B_IDX] = N_OFF;
    }
#endif
}

// local
BYTE mouse_buff_set(BYTE* p_device_data, BYTE* o_buff)
{
    BYTE out_flag = 1;
    
    switch(p_device_data[DEVICE_DATA_SET_TYPE_IDX])
    {
        case SET_TYPE_MOUSE_LCLICK:
            o_buff[USB_MOUSE_BUTTON_IDX_TOP] |= MOUSE_DATA_LEFT_CLICK;
            break;
        case SET_TYPE_MOUSE_RCLICK:
            o_buff[USB_MOUSE_BUTTON_IDX_TOP] |= MOUSE_DATA_RIGHT_CLICK;
            break;
        case SET_TYPE_MOUSE_WHCLICK:
            o_buff[USB_MOUSE_BUTTON_IDX_TOP] |= MOUSE_DATA_WHEEL_CLICK;
            break;
        case SET_TYPE_MOUSE_B4CLICK:
            o_buff[USB_MOUSE_BUTTON_IDX_TOP] |= MOUSE_DATA_B4_CLICK;
            break;
        case SET_TYPE_MOUSE_B5CLICK:
            o_buff[USB_MOUSE_BUTTON_IDX_TOP] |= MOUSE_DATA_B5_CLICK;
            break;
        case SET_TYPE_MOUSE_DCLICK:
            o_buff[USB_MOUSE_BUTTON_IDX_TOP] |= MOUSE_DATA_LEFT_CLICK;
            mouse_w_click_status = MOUSE_DOUBLE_CLICK_STATUS_CLICK1;
            break;
        case SET_TYPE_MOUSE_MOVE:
            o_buff[USB_MOUSE_MOVE_X_IDX] = p_device_data[DEVICE_DATA_X_MOVE_IDX];	// 左右
            o_buff[USB_MOUSE_MOVE_Y_IDX] = p_device_data[DEVICE_DATA_Y_MOVE_IDX];	// 上下
            break;
        case SET_TYPE_MOUSE_WHSCROLL:
            o_buff[USB_MOUSE_MOVE_W_IDX] = p_device_data[DEVICE_DATA_WHEEL_IDX];	// スクロール
            break;
        default:
            out_flag = 0; // 出力なし
            break;
    }
    return out_flag;
}
// local
BYTE keyboard_buff_set(BYTE* p_device_data, BYTE p_sw_output_flag, BYTE* o_buff, BYTE* o_set_code)
{
    BYTE out_flag = 1;

    *o_set_code = 0;
    switch(p_device_data[DEVICE_DATA_SET_TYPE_IDX])
    {
        case SET_TYPE_KEYBOARD_KEY:
            o_buff[KEYBOARD_MODIFIER_IDX] |= p_device_data[DEVICE_DATA_MODIFIER_IDX];
            *o_set_code = p_device_data[DEVICE_DATA_KEY1_IDX];
            break;
        case SET_TYPE_NUMBER_UP:
            if(p_sw_output_flag == N_ON)
            {
                number_input_now_val++;
                if(number_input_now_val >= NUMBER_INPUT_USB_CODE_NUM)
                {
                    number_input_now_val = NUMBER_INPUT_USB_CODE_NUM - 1;
                }
                else
                {
                    *o_set_code = number_input_usb_code[number_input_now_val];
                }
            }
//                        out_buff[KEYBOARD_KEYCODE_TOP] = number_input_usb_code[number_input_now_val];
            break;
        case SET_TYPE_NUMBER_DOWN:
            if(p_sw_output_flag == N_ON)
            {
                number_input_now_val--;
                if(number_input_now_val >= NUMBER_INPUT_USB_CODE_NUM)
                {
                    number_input_now_val = 0;
                }
                else
                {
                    *o_set_code = number_input_usb_code[number_input_now_val];
                }
            }
//                        out_buff[KEYBOARD_KEYCODE_TOP] = number_input_usb_code[number_input_now_val];
            break;
        default:
            out_flag = 0; // 出力なし
            break;
    }
    
    return out_flag;
}
// local
BYTE multimedia_buff_set(BYTE p_set_type, BYTE* o_buff)
{
    BYTE out_flag = 1;

    switch(p_set_type)
    {
        case SET_TYPE_MULTIMEDIA_PLAY:
            o_buff[USB_MULTIMEDIA_DATA_PLAY_IDX] |= MULTIMEDIA_DATA_PLAY;
            break;
        case SET_TYPE_MULTIMEDIA_PAUSE:
            o_buff[USB_MULTIMEDIA_DATA_PAUSE_IDX] |= MULTIMEDIA_DATA_PAUSE;
            break;
        case SET_TYPE_MULTIMEDIA_STOP:
            o_buff[USB_MULTIMEDIA_DATA_STOP_IDX] |= MULTIMEDIA_DATA_STOP;
            break;
        case SET_TYPE_MULTIMEDIA_REC:
            o_buff[USB_MULTIMEDIA_DATA_REC_IDX] |= MULTIMEDIA_DATA_REC;
            break;
        case SET_TYPE_MULTIMEDIA_FORWARD:
            o_buff[USB_MULTIMEDIA_DATA_FORWARD_IDX] |= MULTIMEDIA_DATA_FORWARD;
            break;
        case SET_TYPE_MULTIMEDIA_REWIND:
            o_buff[USB_MULTIMEDIA_DATA_REWIND_IDX] |= MULTIMEDIA_DATA_REWIND;
            break;
        case SET_TYPE_MULTIMEDIA_NEXT:
            o_buff[USB_MULTIMEDIA_DATA_NEXT_IDX] |= MULTIMEDIA_DATA_NEXT;
            break;
        case SET_TYPE_MULTIMEDIA_PREVIOUS:
            o_buff[USB_MULTIMEDIA_DATA_PREVIOUS_IDX] |= MULTIMEDIA_DATA_PREVIOUS;
            break;
        case SET_TYPE_MULTIMEDIA_MUTE:
            o_buff[USB_MULTIMEDIA_DATA_VOLUME_MUTE_IDX] |= MULTIMEDIA_DATA_VOLUME_MUTE;
            break;
        case SET_TYPE_MULTIMEDIA_VOLUMEUP:
            o_buff[USB_MULTIMEDIA_DATA_VOLUME_UP_IDX] |= MULTIMEDIA_DATA_VOLUME_UP;
            break;
        case SET_TYPE_MULTIMEDIA_VOLUMEDOWN:
            o_buff[USB_ULTIMEDIA_DATA_VOLUME_DOWN_IDX] |= MULTIMEDIA_DATA_VOLUME_DOWN;
            break;
        default:
            out_flag = 0; // 出力なし
            break;
    }
                
    return out_flag;
}
// local
BYTE joystick_buff_set(BYTE* p_device_data, BYTE* o_buff)
{
    BYTE out_flag = 1;
    BYTE set_bit = 0;

    switch(p_device_data[DEVICE_DATA_SET_TYPE_IDX])
    {
        case SET_TYPE_JOYPAD_XY:
            o_buff[USB_JOYSTICK_LEVER_L_IDX_TOP]      = 0x80 + p_device_data[DEVICE_DATA_JOY_X_MOVE_IDX];
            o_buff[USB_JOYSTICK_LEVER_L_IDX_TOP+1]    = 0x80 - p_device_data[DEVICE_DATA_JOY_Y_MOVE_IDX];
            break;
        case SET_TYPE_JOYPAD_ZRZ:
            o_buff[USB_JOYSTICK_LEVER_R_IDX_TOP]      = 0x80 + p_device_data[DEVICE_DATA_JOY_X_MOVE_IDX];
            o_buff[USB_JOYSTICK_LEVER_R_IDX_TOP+1]    = 0x80 - p_device_data[DEVICE_DATA_JOY_Y_MOVE_IDX];
            break;
        case SET_TYPE_JOYPAD_B01:
        case SET_TYPE_JOYPAD_B02:
        case SET_TYPE_JOYPAD_B03:
        case SET_TYPE_JOYPAD_B04:
        case SET_TYPE_JOYPAD_B05:
        case SET_TYPE_JOYPAD_B06:
        case SET_TYPE_JOYPAD_B07:
        case SET_TYPE_JOYPAD_B08:
        case SET_TYPE_JOYPAD_B09:
        case SET_TYPE_JOYPAD_B10:
        case SET_TYPE_JOYPAD_B11:
        case SET_TYPE_JOYPAD_B12:
        case SET_TYPE_JOYPAD_B13:
            set_bit = 0x01 << ((p_device_data[DEVICE_DATA_SET_TYPE_IDX] - SET_TYPE_JOYPAD_B01) % 8);
            o_buff[USB_JOYSTICK_BUTTON_IDX_TOP + ((p_device_data[DEVICE_DATA_SET_TYPE_IDX] - SET_TYPE_JOYPAD_B01) / 8)] |= set_bit;
            break;
        case SET_TYPE_JOYPAD_HSW_NORTH:
            o_buff[USB_JOYSTICK_HATSW_IDX] = HAT_SWITCH_NORTH;
            break;
        case SET_TYPE_JOYPAD_HSW_SOUTH:
            o_buff[USB_JOYSTICK_HATSW_IDX] = HAT_SWITCH_SOUTH;
            break;
        case SET_TYPE_JOYPAD_HSW_WEST:
            o_buff[USB_JOYSTICK_HATSW_IDX] = HAT_SWITCH_WEST;
            break;
        case SET_TYPE_JOYPAD_HSW_EAST:
            o_buff[USB_JOYSTICK_HATSW_IDX] = HAT_SWITCH_EAST;
            break;
        default:
            out_flag = 0; // 出力なし
            break;
    }
    
    return out_flag;
}


// SW入力時のキーボード出力有無を取得
// 戻り値 0=出力しない 1=出力する
BYTE Get_SW_Input_Output_Status(void)
{
    BYTE ret_status = 0;
    BYTE fi;
    
    for(fi = 0; fi < SW_NUM; fi++)
    {
        if(sw_output_flag[fi] == N_ON && sw_func_out_flag[fi] > 0)
        {
            ret_status = 1;
            break;
        }
    }
    
    return ret_status;
}
void SW_Output_Mouse(BYTE* out_buff, BYTE buff_size)
{
    BYTE fi;
    BYTE out_flag = 1;
    BYTE *device_data;
    
    set_type_move_val[0] = 0;
    set_type_move_val[1] = 0;
    set_type_ws_val = 0;
        
    for(fi = 0; fi < SW_NUM; fi++)
    {
        if(sw_output_flag[fi] == N_ON && sw_func_out_flag[fi] > 0)
        {
            device_data = my_sw_func_infos[mode_no_fix][fi].sw_func_info.SWFuncData;

            out_flag = mouse_buff_set(device_data, out_buff);
#if 0
            switch(device_data[DEVICE_DATA_SET_TYPE_IDX])
            {
                case SET_TYPE_MOUSE_LCLICK:
                    out_buff[USB_MOUSE_BUTTON_IDX_TOP] = MOUSE_DATA_LEFT_CLICK;
                    break;
                case SET_TYPE_MOUSE_RCLICK:
                    out_buff[USB_MOUSE_BUTTON_IDX_TOP] = MOUSE_DATA_RIGHT_CLICK;
                    break;
                case SET_TYPE_MOUSE_WHCLICK:
                    out_buff[USB_MOUSE_BUTTON_IDX_TOP] = MOUSE_DATA_WHEEL_CLICK;
                    break;
                case SET_TYPE_MOUSE_B4CLICK:
                    out_buff[USB_MOUSE_BUTTON_IDX_TOP] = MOUSE_DATA_B4_CLICK;
                    break;
                case SET_TYPE_MOUSE_B5CLICK:
                    out_buff[USB_MOUSE_BUTTON_IDX_TOP] = MOUSE_DATA_B5_CLICK;
                    break;
                case SET_TYPE_MOUSE_DCLICK:
                    out_buff[USB_MOUSE_BUTTON_IDX_TOP] = MOUSE_DATA_LEFT_CLICK;
                    mouse_w_click_status = MOUSE_DOUBLE_CLICK_STATUS_CLICK1;
                    break;
                case SET_TYPE_MOUSE_MOVE:
                    out_buff[USB_MOUSE_MOVE_X_IDX] = device_data[DEVICE_DATA_X_MOVE_IDX];	// 左右
                    out_buff[USB_MOUSE_MOVE_Y_IDX] = device_data[DEVICE_DATA_Y_MOVE_IDX];	// 上下
                    break;
                case SET_TYPE_MOUSE_WHSCROLL:
                    out_buff[USB_MOUSE_MOVE_W_IDX] = device_data[DEVICE_DATA_WHEEL_IDX];	// スクロール
                    break;
                default:
                    out_flag = 0; // 出力なし
                    break;
            }
#endif
            
            // 新規SW ON
            if(sw_output_mouse_first_flag[fi] == N_OFF && out_flag == 1)
            {   // 出力あり
                sw_output_mouse_first_flag[fi] = N_ON;
                
                if(device_data[DEVICE_DATA_SET_TYPE_IDX] == SET_TYPE_MOUSE_MOVE)
                {
                    set_type_move_sw_no = fi;
                    set_type_move_val[0] = out_buff[USB_MOUSE_MOVE_X_IDX];
                    set_type_move_val[1] = out_buff[USB_MOUSE_MOVE_Y_IDX];
                }
                else if(device_data[DEVICE_DATA_SET_TYPE_IDX] == SET_TYPE_MOUSE_WHSCROLL)
                {
                    set_type_ws_sw_no = fi;
                    set_type_ws_val = out_buff[USB_MOUSE_MOVE_W_IDX];
                }
            }
        }
//        else
//        {
//            if(set_type_move_sw_no == fi)
//            {
//                set_type_move_sw_no = 0xFF;
//            }
//            if(set_type_ws_sw_no == fi)
//            {
//                set_type_ws_sw_no = 0xFF;
//            }
//        }
    }
    
    // 新規ONを優先してセット
    if(set_type_move_sw_no != 0xFF)
    {
        out_buff[USB_MOUSE_MOVE_X_IDX] = set_type_move_val[0];
        out_buff[USB_MOUSE_MOVE_Y_IDX] = set_type_move_val[1];
    }
    if(set_type_ws_sw_no != 0xFF)
    {
        out_buff[USB_MOUSE_MOVE_W_IDX] = set_type_ws_val;
    }
}
void SW_Output_Keyboard(BYTE* out_buff, BYTE buff_size)
{
    BYTE fi;
    BYTE out_flag = 1;
    BYTE *device_data;
    BYTE set_pos = KEYBOARD_KEYCODE_TOP;
    BYTE set_code = 0;
    BYTE tmp_sw_func_out_flag = N_OFF;
    
    // 出力バッファクリア
    for(fi = 0; fi < buff_size; fi++)
    {
        out_buff[fi] = 0;
    }
    
    for(fi = 0; fi < SW_NUM; fi++)
    {
        if(sw_output_flag[fi] == N_ON && sw_func_out_flag[fi] > 0)
        {
            tmp_sw_func_out_flag = sw_func_out_flag[fi];
            
            device_data = my_sw_func_infos[mode_no_fix][fi].sw_func_info.SWFuncData;
            // 数字アップダウン時は１回のみ入力受付
            if(device_data[DEVICE_DATA_SET_TYPE_IDX] == SET_TYPE_NUMBER_UP
                    || device_data[DEVICE_DATA_SET_TYPE_IDX] == SET_TYPE_NUMBER_DOWN)
            {
                sw_func_out_flag[fi] = N_OFF;
            }
            set_code = 0;
                    
            out_flag = keyboard_buff_set(device_data, tmp_sw_func_out_flag, out_buff, &set_code);
#if 0
            switch(device_data[DEVICE_DATA_SET_TYPE_IDX])
            {
                case SET_TYPE_KEYBOARD_KEY:
                    out_buff[KEYBOARD_MODIFIER_IDX] |= device_data[DEVICE_DATA_MODIFIER_IDX];
                    set_code = device_data[DEVICE_DATA_KEY1_IDX];
                    break;
                case SET_TYPE_NUMBER_UP:
                    if(sw_output_flag[fi] == N_ON)
                    {
                        number_input_now_val++;
                        if(number_input_now_val >= NUMBER_INPUT_USB_CODE_NUM)
                        {
                            number_input_now_val = NUMBER_INPUT_USB_CODE_NUM - 1;
                        }
                        else
                        {
                            set_code = number_input_usb_code[number_input_now_val];
                        }
                    }
//                        out_buff[KEYBOARD_KEYCODE_TOP] = number_input_usb_code[number_input_now_val];
                    break;
                case SET_TYPE_NUMBER_DOWN:
                    if(sw_output_flag[fi] == N_ON)
                    {
                        number_input_now_val--;
                        if(number_input_now_val >= NUMBER_INPUT_USB_CODE_NUM)
                        {
                            number_input_now_val = 0;
                        }
                        else
                        {
                            set_code = number_input_usb_code[number_input_now_val];
                        }
                    }
//                        out_buff[KEYBOARD_KEYCODE_TOP] = number_input_usb_code[number_input_now_val];
                    break;
                default:
                    out_flag = 0; // 出力なし
                    break;
            }
#endif

            if(set_code != 0 && set_pos < KEYBOARD_BUFF_SIZE)
            {
                out_buff[set_pos++] = set_code;
            }
        }
    }
}
void SW_Output_Multimedia(BYTE* out_buff, BYTE buff_size)
{
    BYTE fi;
    BYTE out_flag = 1;
    BYTE *device_data;
    
    for(fi = 0; fi < SW_NUM; fi++)
    {
        if(sw_output_flag[fi] == N_ON && sw_output_multimedia_flag[fi] == N_OFF)
        {
            device_data = my_sw_func_infos[mode_no_fix][fi].sw_func_info.SWFuncData;

            out_flag = multimedia_buff_set(device_data[DEVICE_DATA_SET_TYPE_IDX], out_buff);
#if 0
            switch(device_data[DEVICE_DATA_SET_TYPE_IDX])
            {
                case SET_TYPE_MULTIMEDIA_PLAY:
                    out_buff[USB_MULTIMEDIA_DATA_PLAY_IDX] = MULTIMEDIA_DATA_PLAY;
                    break;
                case SET_TYPE_MULTIMEDIA_PAUSE:
                    out_buff[USB_MULTIMEDIA_DATA_PAUSE_IDX] = MULTIMEDIA_DATA_PAUSE;
                    break;
                case SET_TYPE_MULTIMEDIA_STOP:
                    out_buff[USB_MULTIMEDIA_DATA_STOP_IDX] = MULTIMEDIA_DATA_STOP;
                    break;
                case SET_TYPE_MULTIMEDIA_REC:
                    out_buff[USB_MULTIMEDIA_DATA_REC_IDX] = MULTIMEDIA_DATA_REC;
                    break;
                case SET_TYPE_MULTIMEDIA_FORWARD:
                    out_buff[USB_MULTIMEDIA_DATA_FORWARD_IDX] = MULTIMEDIA_DATA_FORWARD;
                    break;
                case SET_TYPE_MULTIMEDIA_REWIND:
                    out_buff[USB_MULTIMEDIA_DATA_REWIND_IDX] = MULTIMEDIA_DATA_REWIND;
                    break;
                case SET_TYPE_MULTIMEDIA_NEXT:
                    out_buff[USB_MULTIMEDIA_DATA_NEXT_IDX] = MULTIMEDIA_DATA_NEXT;
                    break;
                case SET_TYPE_MULTIMEDIA_PREVIOUS:
                    out_buff[USB_MULTIMEDIA_DATA_PREVIOUS_IDX] = MULTIMEDIA_DATA_PREVIOUS;
                    break;
                case SET_TYPE_MULTIMEDIA_MUTE:
                    out_buff[USB_MULTIMEDIA_DATA_VOLUME_MUTE_IDX] = MULTIMEDIA_DATA_VOLUME_MUTE;
                    break;
                case SET_TYPE_MULTIMEDIA_VOLUMEUP:
                    out_buff[USB_MULTIMEDIA_DATA_VOLUME_UP_IDX] = MULTIMEDIA_DATA_VOLUME_UP;
                    break;
                case SET_TYPE_MULTIMEDIA_VOLUMEDOWN:
                    out_buff[USB_ULTIMEDIA_DATA_VOLUME_DOWN_IDX] = MULTIMEDIA_DATA_VOLUME_DOWN;
                    break;
                default:
                    out_flag = 0; // 出力なし
                    break;
            }
#endif
            if(sw_output_multimedia_flag[fi] == N_OFF && out_flag == 1)
            {   // 出力あり
                sw_output_multimedia_flag[fi] = N_ON;
            }
        }
    }
}
void SW_Output_Joystick(BYTE* out_buff, BYTE buff_size)
{
    BYTE fi;
    BYTE out_flag = 1;
    BYTE *device_data;
    
    for(fi = 0; fi < SW_NUM; fi++)
    {
        if(sw_output_flag[fi] == N_ON && sw_func_out_flag[fi] > 0)
        {
            device_data = my_sw_func_infos[mode_no_fix][fi].sw_func_info.SWFuncData;

            out_flag = joystick_buff_set(device_data, out_buff);
#if 0
            switch(device_data[DEVICE_DATA_SET_TYPE_IDX])
            {
                case SET_TYPE_JOYPAD_XY:
                    out_buff[USB_JOYSTICK_LEVER_L_IDX_TOP]      = 0x80 + device_data[DEVICE_DATA_JOY_X_MOVE_IDX];
                    out_buff[USB_JOYSTICK_LEVER_L_IDX_TOP+1]    = 0x80 - device_data[DEVICE_DATA_JOY_Y_MOVE_IDX];
                    break;
                case SET_TYPE_JOYPAD_ZRZ:
                    out_buff[USB_JOYSTICK_LEVER_R_IDX_TOP]      = 0x80 + device_data[DEVICE_DATA_JOY_X_MOVE_IDX];
                    out_buff[USB_JOYSTICK_LEVER_R_IDX_TOP+1]    = 0x80 - device_data[DEVICE_DATA_JOY_Y_MOVE_IDX];
                    break;
                case SET_TYPE_JOYPAD_B01:
                case SET_TYPE_JOYPAD_B02:
                case SET_TYPE_JOYPAD_B03:
                case SET_TYPE_JOYPAD_B04:
                case SET_TYPE_JOYPAD_B05:
                case SET_TYPE_JOYPAD_B06:
                case SET_TYPE_JOYPAD_B07:
                case SET_TYPE_JOYPAD_B08:
                case SET_TYPE_JOYPAD_B09:
                case SET_TYPE_JOYPAD_B10:
                case SET_TYPE_JOYPAD_B11:
                case SET_TYPE_JOYPAD_B12:
                case SET_TYPE_JOYPAD_B13:
                    out_buff[USB_JOYSTICK_BUTTON_IDX_TOP + ((device_data[DEVICE_DATA_SET_TYPE_IDX] - SET_TYPE_JOYPAD_B01) / 8)] = 0x01 << ((device_data[DEVICE_DATA_SET_TYPE_IDX] - SET_TYPE_JOYPAD_B01) % 8);
                    break;
                case SET_TYPE_JOYPAD_HSW_NORTH:
                    out_buff[USB_JOYSTICK_HATSW_IDX] = HAT_SWITCH_NORTH;
                    break;
                case SET_TYPE_JOYPAD_HSW_SOUTH:
                    out_buff[USB_JOYSTICK_HATSW_IDX] = HAT_SWITCH_SOUTH;
                    break;
                case SET_TYPE_JOYPAD_HSW_WEST:
                    out_buff[USB_JOYSTICK_HATSW_IDX] = HAT_SWITCH_WEST;
                    break;
                case SET_TYPE_JOYPAD_HSW_EAST:
                    out_buff[USB_JOYSTICK_HATSW_IDX] = HAT_SWITCH_EAST;
                    break;
                default:
                    out_flag = 0; // 出力なし
                    break;
            }
#endif

            if(sw_output_joystick_first_flag[fi] == N_OFF && out_flag == 1)
            {   // 出力あり
                sw_output_joystick_first_flag[fi] = N_ON;
                
                if(SET_TYPE_JOYPAD_HSW_NORTH <= device_data[DEVICE_DATA_SET_TYPE_IDX]
                        && device_data[DEVICE_DATA_SET_TYPE_IDX] <= SET_TYPE_JOYPAD_HSW_EAST)
                {
                    set_type_hat_sw_no = fi;
                    set_type_hat_val = out_buff[USB_JOYSTICK_HATSW_IDX];
                }
                else if(device_data[DEVICE_DATA_SET_TYPE_IDX] == SET_TYPE_JOYPAD_XY)
                {
                    set_type_xy_sw_no = fi;
                    set_type_xy_val[0] = out_buff[USB_JOYSTICK_LEVER_L_IDX_TOP];
                    set_type_xy_val[1] = out_buff[USB_JOYSTICK_LEVER_L_IDX_TOP+1];
                }
                else if(device_data[DEVICE_DATA_SET_TYPE_IDX] == SET_TYPE_JOYPAD_ZRZ)
                {
                    set_type_zrz_sw_no = fi;
                    set_type_zrz_val[0] = out_buff[USB_JOYSTICK_LEVER_R_IDX_TOP];
                    set_type_zrz_val[1] = out_buff[USB_JOYSTICK_LEVER_R_IDX_TOP+1];
                }
            }
        }
    }
    
    // 新規ONを優先してセット
    if(set_type_hat_sw_no != 0xFF)
    {
        out_buff[USB_JOYSTICK_HATSW_IDX] = set_type_hat_val;
    }
    if(set_type_xy_sw_no != 0xFF)
    {
        out_buff[USB_JOYSTICK_LEVER_L_IDX_TOP] = set_type_xy_val[0];
        out_buff[USB_JOYSTICK_LEVER_L_IDX_TOP+1] = set_type_xy_val[1];
    }
    if(set_type_zrz_sw_no != 0xFF)
    {
        out_buff[USB_JOYSTICK_LEVER_R_IDX_TOP] = set_type_zrz_val[0];
        out_buff[USB_JOYSTICK_LEVER_R_IDX_TOP+1] = set_type_zrz_val[1];
    }
}
// エンコーダーの出力有無を取得
// 戻り値 0=出力しない 1=出力する
BYTE Get_Encoder_Output_Status(void)
{
    BYTE ret_status = 0;
    BYTE fi;

#if 1
    for(fi = ENCODER_IDX_MIN; fi <= ENCODER_IDX_MAX; fi++)
    {
        if(sw_output_flag[fi] == N_ON || encoder_key_out_flag[fi] > 0)
        {
            ret_status = 1;
            break;
        }
    }
#endif
#if 0
    for(fi = 0; fi < SW_ALL_NUM; fi++)
    {
        if(ENCODER_IDX_MIN <= fi && fi <= ENCODER_IDX_MAX)
        {
            if(sw_output_flag[fi] == N_ON || encoder_key_out_flag[fi] > 0)
            {
                ret_status = 1;
                break;
            }
        }
    }
#endif
    
    return ret_status;
}
void Encoder_Output_Mouse(BYTE* out_buff, BYTE buff_size)
{
    BYTE fi;
    BYTE out_flag = 1;
    BYTE *device_data;
    
    for(fi = ENCODER_IDX_MIN; fi <= ENCODER_IDX_MAX; fi++)
    {
        if(sw_output_flag[fi] == N_ON || encoder_key_out_flag[fi] > 0)
        {
            device_data = my_func_infos[mode_no_fix][mode_func_no_fix].func_info.FuncCCWData;
            if(fi == SW_ENCORDER_A_IDX)
            {
                device_data = my_func_infos[mode_no_fix][mode_func_no_fix].func_info.FuncCCWData;
            }
            else if(fi == SW_ENCORDER_B_IDX)
            {
                device_data = my_func_infos[mode_no_fix][mode_func_no_fix].func_info.FuncCWData;
            }

            out_flag = mouse_buff_set(device_data, out_buff);
#if 0
            switch(device_data[DEVICE_DATA_SET_TYPE_IDX])
            {
                case SET_TYPE_MOUSE_LCLICK:
                    out_buff[USB_MOUSE_BUTTON_IDX_TOP] = MOUSE_DATA_LEFT_CLICK;
                    break;
                case SET_TYPE_MOUSE_RCLICK:
                    out_buff[USB_MOUSE_BUTTON_IDX_TOP] = MOUSE_DATA_RIGHT_CLICK;
                    break;
                case SET_TYPE_MOUSE_WHCLICK:
                    out_buff[USB_MOUSE_BUTTON_IDX_TOP] = MOUSE_DATA_WHEEL_CLICK;
                    break;
                case SET_TYPE_MOUSE_B4CLICK:
                    out_buff[USB_MOUSE_BUTTON_IDX_TOP] = MOUSE_DATA_B4_CLICK;
                    break;
                case SET_TYPE_MOUSE_B5CLICK:
                    out_buff[USB_MOUSE_BUTTON_IDX_TOP] = MOUSE_DATA_B5_CLICK;
                    break;
                case SET_TYPE_MOUSE_DCLICK:
                    out_buff[USB_MOUSE_BUTTON_IDX_TOP] = MOUSE_DATA_LEFT_CLICK;
                    mouse_w_click_status = MOUSE_DOUBLE_CLICK_STATUS_CLICK1;
                    break;
                case SET_TYPE_MOUSE_MOVE:
                    out_buff[USB_MOUSE_MOVE_X_IDX] = device_data[DEVICE_DATA_X_MOVE_IDX];	// 左右
                    out_buff[USB_MOUSE_MOVE_Y_IDX] = device_data[DEVICE_DATA_Y_MOVE_IDX];	// 上下
                    break;
                case SET_TYPE_MOUSE_WHSCROLL:
                    out_buff[USB_MOUSE_MOVE_W_IDX] = device_data[DEVICE_DATA_WHEEL_IDX];	// スクロール
                    break;
                default:
                    out_flag = 0; // 出力なし
                    break;
            }
#endif

            if(sw_output_flag[fi] == N_ON && out_flag == 1)
            {   // 出力あり
                sw_output_flag[fi] = N_OFF;
            }
        }
    }
}
void Encoder_Output_Keyboard(BYTE* out_buff, BYTE buff_size)
{
    BYTE fi;
    BYTE out_flag = 1;
    BYTE *device_data;
    BYTE set_code = 0;
    
    // 出力バッファクリア
    for(fi = 0; fi < buff_size; fi++)
    {
        out_buff[fi] = 0;
    }
    
    for(fi = ENCODER_IDX_MIN; fi <= ENCODER_IDX_MAX; fi++)
    {
        if(sw_output_flag[fi] == N_ON || encoder_key_out_flag[fi] > 0)
        {
            device_data = my_func_infos[mode_no_fix][mode_func_no_fix].func_info.FuncCCWData;
            if(fi == SW_ENCORDER_A_IDX)
            {
                device_data = my_func_infos[mode_no_fix][mode_func_no_fix].func_info.FuncCCWData;
            }
            else if(fi == SW_ENCORDER_B_IDX)
            {
                device_data = my_func_infos[mode_no_fix][mode_func_no_fix].func_info.FuncCWData;
            }

            out_flag = keyboard_buff_set(device_data, sw_output_flag[fi], out_buff, &set_code);
#if 0
            switch(device_data[DEVICE_DATA_SET_TYPE_IDX])
            {
                case SET_TYPE_KEYBOARD_KEY:
                    out_buff[KEYBOARD_MODIFIER_IDX] = device_data[DEVICE_DATA_MODIFIER_IDX];
                    out_buff[KEYBOARD_KEYCODE_TOP] = device_data[DEVICE_DATA_KEY1_IDX];
                    break;
                case SET_TYPE_NUMBER_UP:
                    if(sw_output_flag[fi] == N_ON)
                    {
                        number_input_now_val++;
                        if(number_input_now_val >= NUMBER_INPUT_USB_CODE_NUM)
                        {
                            number_input_now_val = NUMBER_INPUT_USB_CODE_NUM - 1;
                        }
                        else
                        {
                            out_buff[KEYBOARD_KEYCODE_TOP] = number_input_usb_code[number_input_now_val];
                        }
                    }
//                        out_buff[KEYBOARD_KEYCODE_TOP] = number_input_usb_code[number_input_now_val];
                    break;
                case SET_TYPE_NUMBER_DOWN:
                    if(sw_output_flag[fi] == N_ON)
                    {
                        number_input_now_val--;
                        if(number_input_now_val >= NUMBER_INPUT_USB_CODE_NUM)
                        {
                            number_input_now_val = 0;
                        }
                        else
                        {
                            out_buff[KEYBOARD_KEYCODE_TOP] = number_input_usb_code[number_input_now_val];
                        }
                    }
//                        out_buff[KEYBOARD_KEYCODE_TOP] = number_input_usb_code[number_input_now_val];
                    break;
                default:
                    out_flag = 0; // 出力なし
                    break;
            }
#endif

            if(sw_output_flag[fi] == N_ON && out_flag == 1)
            {   // 出力あり
                out_buff[KEYBOARD_KEYCODE_TOP] = set_code;
                sw_output_flag[fi] = N_OFF;
                if(my_base_head.BaseHead.encoder_typematic == ENCODER_TYPEMATIC_TYPE_PRESS_LEAVE)
                {   // 押しっぱなし
                    encoder_key_out_flag[fi] = ENCODER_KEYBORD_OUTPUT_TIME_PRESS;
                }
                else
                {   // 連打
                    encoder_key_out_flag[fi] = ENCODER_KEYBORD_OUTPUT_TIME_HIT;
                }
            }
        }
    }
}
void Encoder_Output_Multimedia(BYTE* out_buff, BYTE buff_size)
{
    BYTE fi;
    BYTE out_flag = 1;
    BYTE *device_data;
    
    for(fi = ENCODER_IDX_MIN; fi <= ENCODER_IDX_MAX; fi++)
    {
        if(sw_output_flag[fi] == N_ON && sw_output_multimedia_flag[fi] == N_OFF)
        {
            device_data = my_func_infos[mode_no_fix][mode_func_no_fix].func_info.FuncCCWData;
            if(fi == SW_ENCORDER_A_IDX)
            {
                device_data = my_func_infos[mode_no_fix][mode_func_no_fix].func_info.FuncCCWData;
            }
            else if(fi == SW_ENCORDER_B_IDX)
            {
                device_data = my_func_infos[mode_no_fix][mode_func_no_fix].func_info.FuncCWData;
            }

            out_flag = multimedia_buff_set(device_data[DEVICE_DATA_SET_TYPE_IDX], out_buff);
#if 0
            switch(device_data[DEVICE_DATA_SET_TYPE_IDX])
            {
                case SET_TYPE_MULTIMEDIA_PLAY:
                    out_buff[USB_MULTIMEDIA_DATA_PLAY_IDX] = MULTIMEDIA_DATA_PLAY;
                    break;
                case SET_TYPE_MULTIMEDIA_PAUSE:
                    out_buff[USB_MULTIMEDIA_DATA_PAUSE_IDX] = MULTIMEDIA_DATA_PAUSE;
                    break;
                case SET_TYPE_MULTIMEDIA_STOP:
                    out_buff[USB_MULTIMEDIA_DATA_STOP_IDX] = MULTIMEDIA_DATA_STOP;
                    break;
                case SET_TYPE_MULTIMEDIA_REC:
                    out_buff[USB_MULTIMEDIA_DATA_REC_IDX] = MULTIMEDIA_DATA_REC;
                    break;
                case SET_TYPE_MULTIMEDIA_FORWARD:
                    out_buff[USB_MULTIMEDIA_DATA_FORWARD_IDX] = MULTIMEDIA_DATA_FORWARD;
                    break;
                case SET_TYPE_MULTIMEDIA_REWIND:
                    out_buff[USB_MULTIMEDIA_DATA_REWIND_IDX] = MULTIMEDIA_DATA_REWIND;
                    break;
                case SET_TYPE_MULTIMEDIA_NEXT:
                    out_buff[USB_MULTIMEDIA_DATA_NEXT_IDX] = MULTIMEDIA_DATA_NEXT;
                    break;
                case SET_TYPE_MULTIMEDIA_PREVIOUS:
                    out_buff[USB_MULTIMEDIA_DATA_PREVIOUS_IDX] = MULTIMEDIA_DATA_PREVIOUS;
                    break;
                case SET_TYPE_MULTIMEDIA_MUTE:
                    out_buff[USB_MULTIMEDIA_DATA_VOLUME_MUTE_IDX] = MULTIMEDIA_DATA_VOLUME_MUTE;
                    break;
                case SET_TYPE_MULTIMEDIA_VOLUMEUP:
                    out_buff[USB_MULTIMEDIA_DATA_VOLUME_UP_IDX] = MULTIMEDIA_DATA_VOLUME_UP;
                    break;
                case SET_TYPE_MULTIMEDIA_VOLUMEDOWN:
                    out_buff[USB_ULTIMEDIA_DATA_VOLUME_DOWN_IDX] = MULTIMEDIA_DATA_VOLUME_DOWN;
                    break;
                default:
                    out_flag = 0; // 出力なし
                    break;
            }
#endif

            if(sw_output_flag[fi] == N_ON && out_flag == 1)
            {   // 出力あり
                sw_output_flag[fi] = N_OFF;
                sw_output_multimedia_flag[fi] = N_ON;
            }
        }
        else
        {
            sw_output_multimedia_flag[fi] = N_OFF;
        }
    }
}
void Encoder_Output_Joystick(BYTE* out_buff, BYTE buff_size)
{
    BYTE fi;
    BYTE out_flag = 1;
    BYTE *device_data;
    
    for(fi = ENCODER_IDX_MIN; fi <= ENCODER_IDX_MAX; fi++)
    {
        if(sw_output_flag[fi] == N_ON || encoder_key_out_flag[fi] > 0)
        {
            device_data = my_func_infos[mode_no_fix][mode_func_no_fix].func_info.FuncCCWData;
            if(fi == SW_ENCORDER_A_IDX)
            {
                device_data = my_func_infos[mode_no_fix][mode_func_no_fix].func_info.FuncCCWData;
            }
            else if(fi == SW_ENCORDER_B_IDX)
            {
                device_data = my_func_infos[mode_no_fix][mode_func_no_fix].func_info.FuncCWData;
            }

            out_flag = joystick_buff_set(device_data, out_buff);
#if 0
            switch(device_data[DEVICE_DATA_SET_TYPE_IDX])
            {
                case SET_TYPE_JOYPAD_XY:
                    out_buff[USB_JOYSTICK_LEVER_L_IDX_TOP]      = 0x80 + device_data[DEVICE_DATA_JOY_X_MOVE_IDX];
                    out_buff[USB_JOYSTICK_LEVER_L_IDX_TOP+1]    = 0x80 - device_data[DEVICE_DATA_JOY_Y_MOVE_IDX];
                    break;
                case SET_TYPE_JOYPAD_ZRZ:
                    out_buff[USB_JOYSTICK_LEVER_R_IDX_TOP]      = 0x80 + device_data[DEVICE_DATA_JOY_X_MOVE_IDX];
                    out_buff[USB_JOYSTICK_LEVER_R_IDX_TOP+1]    = 0x80 - device_data[DEVICE_DATA_JOY_Y_MOVE_IDX];
                    break;
                case SET_TYPE_JOYPAD_B01:
                case SET_TYPE_JOYPAD_B02:
                case SET_TYPE_JOYPAD_B03:
                case SET_TYPE_JOYPAD_B04:
                case SET_TYPE_JOYPAD_B05:
                case SET_TYPE_JOYPAD_B06:
                case SET_TYPE_JOYPAD_B07:
                case SET_TYPE_JOYPAD_B08:
                case SET_TYPE_JOYPAD_B09:
                case SET_TYPE_JOYPAD_B10:
                case SET_TYPE_JOYPAD_B11:
                case SET_TYPE_JOYPAD_B12:
                case SET_TYPE_JOYPAD_B13:
                    out_buff[USB_JOYSTICK_BUTTON_IDX_TOP + ((device_data[DEVICE_DATA_SET_TYPE_IDX] - SET_TYPE_JOYPAD_B01) / 8)] = 0x01 << ((device_data[DEVICE_DATA_SET_TYPE_IDX] - SET_TYPE_JOYPAD_B01) % 8);
                    break;
                case SET_TYPE_JOYPAD_HSW_NORTH:
                    out_buff[USB_JOYSTICK_HATSW_IDX] = HAT_SWITCH_NORTH;
                    break;
                case SET_TYPE_JOYPAD_HSW_SOUTH:
                    out_buff[USB_JOYSTICK_HATSW_IDX] = HAT_SWITCH_SOUTH;
                    break;
                case SET_TYPE_JOYPAD_HSW_WEST:
                    out_buff[USB_JOYSTICK_HATSW_IDX] = HAT_SWITCH_WEST;
                    break;
                case SET_TYPE_JOYPAD_HSW_EAST:
                    out_buff[USB_JOYSTICK_HATSW_IDX] = HAT_SWITCH_EAST;
                    break;
                default:
                    out_flag = 0; // 出力なし
                    break;
            }
#endif

            if(sw_output_flag[fi] == N_ON && out_flag == 1)
            {   // 出力あり
                sw_output_flag[fi] = N_OFF;
            }
        }
    }
}
void Encoder_Output_Script(void)
{
    BYTE fi;
    BYTE out_flag = 1;
    BYTE *device_data;
    BYTE idx = 0;
	BYTE exe_script_no;
	BYTE script_attribute;
    BYTE temp_encoder_script_exe_now_idx;
    
    for(fi = ENCODER_IDX_MIN; fi <= ENCODER_IDX_MAX; fi++)
    {
        if(sw_output_flag[fi] == N_ON || encoder_key_out_flag[fi] > 0)
        {
            device_data = my_func_infos[mode_no_fix][mode_func_no_fix].func_info.FuncCCWData;
            if(fi == SW_ENCORDER_A_IDX)
            {
                device_data = my_func_infos[mode_no_fix][mode_func_no_fix].func_info.FuncCCWData;
            }
            else if(fi == SW_ENCORDER_B_IDX)
            {
                device_data = my_func_infos[mode_no_fix][mode_func_no_fix].func_info.FuncCWData;
            }

            switch(device_data[DEVICE_DATA_SET_TYPE_IDX])
            {
                case SET_TYPE_ENCODER_SCRIPT1:
                case SET_TYPE_ENCODER_SCRIPT2:
                case SET_TYPE_ENCODER_SCRIPT3:
                    if(sw_output_flag[fi] == N_ON)
                    {
                        idx = device_data[DEVICE_DATA_SET_TYPE_IDX]-SET_TYPE_ENCODER_SCRIPT1;
                        if(0 < my_encoder_script_infos[idx].encoder_script_info.rec_num)
                        {   // エンコーダースクリプト設定あり
                            if(fi == SW_ENCORDER_A_IDX)
                            {
                                temp_encoder_script_exe_now_idx = encoder_script_exe_now_idx[idx];
                                encoder_script_exe_now_idx[idx]--;
                                if(0 <= encoder_script_exe_now_idx[idx] && encoder_script_exe_now_idx[idx] < my_encoder_script_infos[idx].encoder_script_info.rec_num)
                                {
                                    // スクリプト実行
                                    // スクリプト出力 ただし実行中のときは出力しない
                                    if(Get_Exe_Script_No(0) > 0)
                                    {
                                        encoder_script_exe_now_idx[idx] = temp_encoder_script_exe_now_idx;
                                        out_flag = 0; // 出力なし
                                        sw_output_flag[fi] = N_OFF;
                                    }
                                }
                                else
                                {
                                    if(my_encoder_script_infos[idx].encoder_script_info.loop_flag == ENCODER_SCRIPT_LOOP_SET_LOOP)
                                    {   // 繰り返し
                                        encoder_script_exe_now_idx[idx] = my_encoder_script_infos[idx].encoder_script_info.rec_num - 1;

                                        // スクリプト実行中のときは出力しない
                                        if(Get_Exe_Script_No(0) > 0)
                                        {
                                            encoder_script_exe_now_idx[idx] = temp_encoder_script_exe_now_idx;
                                            out_flag = 0; // 出力なし
                                            sw_output_flag[fi] = N_OFF;
                                        }
                                    }
                                    else
                                    {
                                        encoder_script_exe_now_idx[idx] = 0xFF;  // 
                                        out_flag = 0; // 出力なし
                                        sw_output_flag[fi] = N_OFF;
                                    }
                                }

                            }
                            else if(fi == SW_ENCORDER_B_IDX)
                            {
                                temp_encoder_script_exe_now_idx = encoder_script_exe_now_idx[idx];
                                encoder_script_exe_now_idx[idx]++;
                                if(0 <= encoder_script_exe_now_idx[idx] && encoder_script_exe_now_idx[idx] < my_encoder_script_infos[idx].encoder_script_info.rec_num)
                                {
                                    // スクリプト実行
                                    // スクリプト出力 ただし実行中のときは出力しない
                                    if(Get_Exe_Script_No(0) > 0)
                                    {
                                        encoder_script_exe_now_idx[idx] = temp_encoder_script_exe_now_idx;
                                        out_flag = 0; // 出力なし
                                        sw_output_flag[fi] = N_OFF;
                                    }
                                }
                                else
                                {
                                    if(my_encoder_script_infos[idx].encoder_script_info.loop_flag == ENCODER_SCRIPT_LOOP_SET_LOOP)
                                    {   // 繰り返し
                                        encoder_script_exe_now_idx[idx] = 0;

                                        // スクリプト実行中のときは出力しない
                                        if(Get_Exe_Script_No(0) > 0)
                                        {
                                            encoder_script_exe_now_idx[idx] = temp_encoder_script_exe_now_idx;
                                            out_flag = 0; // 出力なし
                                            sw_output_flag[fi] = N_OFF;
                                        }
                                    }
                                    else
                                    {
                                        encoder_script_exe_now_idx[idx] = my_encoder_script_infos[idx].encoder_script_info.rec_num;  // 
                                        out_flag = 0; // 出力なし
                                        sw_output_flag[fi] = N_OFF;
                                    }
                                }
                            }
                        }
                        else
                        {   // エンコーダースクリプト設定なし
                            out_flag = 0; // 出力なし
                            sw_output_flag[fi] = N_OFF;
                        }
                    }
                    break;
                default:
                    out_flag = 0; // 出力なし
                    break;
            }

            if(sw_output_flag[fi] == N_ON && out_flag == 1)
            {   // 出力あり
                sw_output_flag[fi] = N_OFF;
                idx = device_data[DEVICE_DATA_SET_TYPE_IDX]-SET_TYPE_ENCODER_SCRIPT1;

                exe_script_no = Get_Exe_Script_No(0);		// 実行中のスクリプトNo.取得
                script_attribute = Get_Script_Attribute(0);	// スクリプト属性取得
                if(exe_script_no > 0)
                {	// すでにスクリプトを実行中

//                        // ループモードで同じスクリプト実行要求の時は停止
//                        if(script_attribute == SCRIPT_EXE_CTRL_LOOP_MODE && exe_script_no == my_base_infos[mode_no_fix].BaseInfo.sw_exe_script_no[fi])
//                        {
//                            Script_Stop(0);
//                            Script_Reset(0);
//                        }
//                        else
//                        {
//                            // 即実行
//                            next_script_exe_req_scriptNo = 0;
//                            Script_Stop(0);
//                            Set_Script_Exe_Info(0, my_base_infos[mode_no_fix].BaseInfo.sw_exe_script_no[fi]);
//                            Script_Reset(0);
//                            Script_Run(0);
//                            // 終了後実行
//                            next_script_exe_req_scriptNo = my_base_infos[mode_no_fix].BaseInfo.sw_exe_script_no[fi];
//                             // 無視
//                            next_script_exe_req_scriptNo = 0;
//                        }
                }
                else
                {	// スクリプト実行中でない　とき
                    if(0 < my_encoder_script_infos[idx].encoder_script_info.rec_num)
                    {
                        if(encoder_script_exe_now_idx[idx] < my_encoder_script_infos[idx].encoder_script_info.rec_num)
                        {
                            Set_Script_Exe_Info(0, my_encoder_script_infos[idx].encoder_script_info.encoder_script[encoder_script_exe_now_idx[idx]]);
                            Script_Reset(0);
                            Script_Run(0);
                        }
                    }
                }
            }
        }
    }
}

void Set_Base_Head(UN_BASE_HEAD* p_base_head_source, UN_BASE_HEAD* p_base_head_set)
{
    BYTE fi;
    
    // コピー
    for(fi = 0; fi < sizeof(UN_BASE_HEAD); fi++)
    {
        p_base_head_set->byte[fi] = p_base_head_source->byte[fi];
    }
    
    // 値チェック
    // Mode
    if(p_base_head_set->BaseHead.mode >= MODE_NUM)
    {
        p_base_head_set->BaseHead.mode = 0;
    }
    // LEDスリープ設定
    if(p_base_head_set->BaseHead.led_sleep != LED_SLEEP_ENABLED && p_base_head_set->BaseHead.led_sleep != LED_SLEEP_DISABLED)
    {
        p_base_head_set->BaseHead.led_sleep = LED_SLEEP_ENABLED;
    }
    // LED点灯設定　モード
//    if(p_base_head_set->BaseHead.led_light_mode < LED_LIGHT_TYPE_MODE_MIN || LED_LIGHT_TYPE_MODE_MAX < p_base_head_set->BaseHead.led_light_mode)
    if(LED_LIGHT_TYPE_MODE_MAX < p_base_head_set->BaseHead.led_light_mode)
    {
        p_base_head_set->BaseHead.led_light_mode = LED_LIGHT_TYPE_MODE_ON;
    }
    // LED点灯設定　機能
//    if(p_base_head_set->BaseHead.led_light_func < LED_LIGHT_TYPE_FUNC_MIN || LED_LIGHT_TYPE_FUNC_MAX < p_base_head_set->BaseHead.led_light_func)
    if(LED_LIGHT_TYPE_FUNC_MAX < p_base_head_set->BaseHead.led_light_func)
    {
        p_base_head_set->BaseHead.led_light_func = LED_LIGHT_TYPE_FUNC_ON;
    }
    // LED消灯時間
    if(p_base_head_set->BaseHead.led_off_time > LED_LIGHT_MODE_OFF_MAX_TIME)
    {
        p_base_head_set->BaseHead.led_off_time = LED_LIGHT_MODE_OFF_DEFAULT_TIME;
    }
    // エンコーダータイプマティック
    if(ENCODER_TYPEMATIC_TYPE_MAX < p_base_head_set->BaseHead.encoder_typematic)
    {
        p_base_head_set->BaseHead.encoder_typematic = ENCODER_TYPEMATIC_TYPE_HIT_REPEAT;
    }
}

void Set_Base_Info(BYTE p_mode_no, UN_BASE_INFO* p_base_info_source, UN_BASE_INFO* p_base_info_set)
{
    BYTE fi;
    BYTE LED_check_ng = 0;
    BYTE error_check_flag = 0;
    
    // Mode No. チェック
    if(p_mode_no >= MODE_NUM)
    {
        p_mode_no = 0;
        error_check_flag = 1;
    }
    
    if(error_check_flag == 0)
    {   // ここまでエラーなし
        
        // コピー
        for(fi = 0; fi < sizeof(UN_BASE_INFO); fi++)
        {
            p_base_info_set->byte[fi] = p_base_info_source->byte[fi];
        }

        // 値チェック
        // SW実行スクリプトNo.
        for(fi = 0; fi < sizeof(p_base_info_set->BaseInfo.sw_exe_script_no); fi++)
        {
            if(SCRIPT_MAX_NUM < p_base_info_set->BaseInfo.sw_exe_script_no[fi])
            {
                p_base_info_set->BaseInfo.sw_exe_script_no[fi] = 0;
            }
        }
        // SW特殊機能No.
        for(fi = 0; fi < sizeof(p_base_info_set->BaseInfo.sw_sp_func_no); fi++)
        {
            if(SW_SP_FUNC_NUM < p_base_info_set->BaseInfo.sw_sp_func_no[fi])
            {
                p_base_info_set->BaseInfo.sw_sp_func_no[fi] = SW_SP_FUNC_NONE;
            }
        }
        // エンコーダーデフォルト機能No.
        if(MODE_FUNCTION_NUM <= p_base_info_set->BaseInfo.encoder_func_no)
        {
            p_base_info_set->BaseInfo.encoder_func_no = 0;
        }
        // MODE LED COLOR No.
        if(LED_COLOR_TYPE_NUM <= p_base_info_set->BaseInfo.mode_led_color_no)
        {
            p_base_info_set->BaseInfo.mode_led_color_no = 0;
        }
        // MODE LED COLOR 詳細設定フラグ
        if(N_OFF != p_base_info_set->BaseInfo.mode_led_color_flag && N_ON != p_base_info_set->BaseInfo.mode_led_color_flag)
        {
            p_base_info_set->BaseInfo.mode_led_color_flag = N_OFF;
        }
        // LED詳細設定
        // まずチェックして、NG値がある場合は、デフォルト値を設定
        for(fi = 0; fi < sizeof(p_base_info_set->BaseInfo.mode_led_RGB); fi++)
        {
            if(LED_DUTY_MAX < p_base_info_set->BaseInfo.mode_led_RGB[fi])
            {
                LED_check_ng = 1;
                break;
            }
        }
        if(LED_check_ng != 0)
        {   // LED DutyにNG値ありのため、デフォルト値をセット
            for(fi = 0; fi < sizeof(p_base_info_set->BaseInfo.mode_led_RGB); fi++)
            {
                p_base_info_set->BaseInfo.mode_led_RGB[fi] = led_color_mode_default_data[p_mode_no][fi];
            }
        }
        // LED輝度
        if(LED_BRIGHTNESS_LEVEL_NUM <= p_base_info_set->BaseInfo.mode_led_brightness_level)
        {
            p_base_info_set->BaseInfo.mode_led_brightness_level = LED_BRIGHTNESS_LEVEL_NORMAL;
        }
    }
}

void Set_Func_Info(BYTE p_mode_no, BYTE p_func_no, UN_FUNC_INFO* p_func_info_source, UN_FUNC_INFO* p_func_info_set)
{
    BYTE fi;
    BYTE LED_check_ng = 0;
    BYTE error_check_flag = 0;
    
    // Mode No. チェック
    if(p_mode_no >= MODE_NUM)
    {
        p_mode_no = 0;
        error_check_flag = 1;
    }
    // Function No. チェック
    if(p_func_no >= MODE_FUNCTION_NUM)
    {
        p_func_no = 0;
        error_check_flag = 1;
    }
    
    if(error_check_flag == 0)
    {   // ここまでエラーなし
    
        // コピー
        for(fi = 0; fi < sizeof(UN_FUNC_INFO); fi++)
        {
            p_func_info_set->byte[fi] = p_func_info_source->byte[fi];
        }

        // 値チェック
        // 機能右回転
        if(p_func_info_set->func_info.FuncCWData[DEVICE_DATA_SET_TYPE_IDX] < SET_TYPE_VAL_MIN || SET_TYPE_VAL_MAX < p_func_info_set->func_info.FuncCWData[DEVICE_DATA_SET_TYPE_IDX])
        {
            for(fi = 0; fi < sizeof(p_func_info_set->func_info.FuncCWData); fi++)
            {
                p_func_info_set->func_info.FuncCWData[fi] = 0;
            }
            p_func_info_set->func_info.FuncCWData[DEVICE_DATA_SET_TYPE_IDX] = SET_TYPE_NONE;
            p_func_info_set->func_info.FuncCWData[DEVICE_DATA_SENSE_IDX] = MASTER_INPUT_SENSE_DEFAULT;
        }
        // 機能左回転
        if(p_func_info_set->func_info.FuncCCWData[DEVICE_DATA_SET_TYPE_IDX] < SET_TYPE_VAL_MIN || SET_TYPE_VAL_MAX < p_func_info_set->func_info.FuncCCWData[DEVICE_DATA_SET_TYPE_IDX])
        {
            for(fi = 0; fi < sizeof(p_func_info_set->func_info.FuncCCWData); fi++)
            {
                p_func_info_set->func_info.FuncCCWData[fi] = 0;
            }
            p_func_info_set->func_info.FuncCCWData[DEVICE_DATA_SET_TYPE_IDX] = SET_TYPE_NONE;
            p_func_info_set->func_info.FuncCCWData[DEVICE_DATA_SENSE_IDX] = MASTER_INPUT_SENSE_DEFAULT;
        }
        // MODE LED COLOR No.
        if(LED_COLOR_TYPE_NUM <= p_func_info_set->func_info.led_color_no)
        {
            p_func_info_set->func_info.led_color_no = 0;
        }
        // MODE LED COLOR 詳細設定フラグ
        if(N_OFF != p_func_info_set->func_info.led_color_flag && N_ON != p_func_info_set->func_info.led_color_flag)
        {
            p_func_info_set->func_info.led_color_flag = N_OFF;
        }
        // LED詳細設定
        // まずチェックして、NG値がある場合は、デフォルト値を設定
        for(fi = 0; fi < sizeof(p_func_info_set->func_info.led_RGB); fi++)
        {
            if(LED_DUTY_MAX < p_func_info_set->func_info.led_RGB[fi])
            {
                LED_check_ng = 1;
                break;
            }
        }
        if(LED_check_ng != 0)
        {   // LED DutyにNG値ありのため、デフォルト値をセット
            for(fi = 0; fi < sizeof(p_func_info_set->func_info.led_RGB); fi++)
            {
                p_func_info_set->func_info.led_RGB[fi] = led_color_func_default_data[(p_mode_no * MODE_FUNCTION_NUM) + p_func_no][fi];
            }
        }
        // LED輝度
        if(LED_BRIGHTNESS_LEVEL_NUM <= p_func_info_set->func_info.led_brightness_level)
        {
            p_func_info_set->func_info.led_brightness_level = LED_BRIGHTNESS_LEVEL_NORMAL;
        }
    }
}

void Set_EncoderScript_Info(BYTE p_no, UN_ENCODER_SCRIPT_INFO* p_encoder_script_info_source, UN_ENCODER_SCRIPT_INFO* p_encoder_script_info_set)
{
    BYTE fi;
    BYTE error_check_flag = 0;
    
    // No. チェック
    if(p_no >= ENCODER_SCRIPT_NUM)
    {
        p_no = 0;
        error_check_flag = 1;
    }
    
    if(error_check_flag == 0)
    {   // ここまでエラーなし
        
        // コピー
        for(fi = 0; fi < sizeof(UN_ENCODER_SCRIPT_INFO); fi++)
        {
            p_encoder_script_info_set->byte[fi] = p_encoder_script_info_source->byte[fi];
        }

        // 値チェック
        // rec数
        if(ENCODER_SCRIPT_SCRIPT_REC_MAX < p_encoder_script_info_set->encoder_script_info.rec_num)
        {
            p_encoder_script_info_set->encoder_script_info.rec_num = 0;
        }
        // Loop flag
        if(ENCODER_SCRIPT_LOOP_SET_NUM <= p_encoder_script_info_set->encoder_script_info.loop_flag)
        {
            p_encoder_script_info_set->encoder_script_info.loop_flag = ENCODER_SCRIPT_LOOP_SET_NONE;
        }
        // マクロNo.
        for(fi = 0; fi < sizeof(p_encoder_script_info_set->encoder_script_info.encoder_script); fi++)
        {
            if(SCRIPT_MAX_NUM < p_encoder_script_info_set->encoder_script_info.encoder_script[fi])
            {
                p_encoder_script_info_set->encoder_script_info.encoder_script[fi] = 0;
            }
        }
    }
}

void Set_SW_Func_Info(BYTE p_mode_no, BYTE p_sw_no, UN_SW_FUNC_INFO* p_sw_func_info_source, UN_SW_FUNC_INFO* p_sw_func_info_set)
{
    BYTE fi;
    BYTE error_check_flag = 0;
    
    // Mode No. チェック
    if(p_mode_no >= MODE_NUM)
    {
        p_mode_no = 0;
        error_check_flag = 1;
    }
    // SW No. チェック
    if(p_sw_no >= SW_NUM)
    {
        p_sw_no = 0;
        error_check_flag = 1;
    }
    
    if(error_check_flag == 0)
    {   // ここまでエラーなし
    
        // コピー
        for(fi = 0; fi < sizeof(UN_SW_FUNC_INFO); fi++)
        {
            p_sw_func_info_set->byte[fi] = p_sw_func_info_source->byte[fi];
        }

        // 値チェック
        // SW機能データ
        if(p_sw_func_info_set->sw_func_info.SWFuncData[DEVICE_DATA_SET_TYPE_IDX] < SET_TYPE_VAL_MIN || SET_TYPE_VAL_MAX < p_sw_func_info_set->sw_func_info.SWFuncData[DEVICE_DATA_SET_TYPE_IDX])
        {
            for(fi = 0; fi < sizeof(p_sw_func_info_set->sw_func_info.SWFuncData); fi++)
            {
                p_sw_func_info_set->sw_func_info.SWFuncData[fi] = 0;
            }
            p_sw_func_info_set->sw_func_info.SWFuncData[DEVICE_DATA_SET_TYPE_IDX] = SET_TYPE_NONE;
        }
    }
}

void set_LED(BYTE no, BYTE onoff)
{
	if(onoff == 0 || onoff == 1)
	{
		switch(no)
		{
			case 0:	LED1_RED    = onoff;	break;
			case 1:	LED1_GREEN  = onoff;	break;
			case 2:	LED1_BLUE   = onoff;	break;
//			case 3:	LED2_RED    = onoff;	break;
//			case 4:	LED2_GREEN  = onoff;	break;
//			case 5:	LED2_BLUE   = onoff;	break;
			default: break;
		}
	}
}

void set_LED_output_data(BYTE p_color_no, BYTE p_color_detail_flag, BYTE* p_rgb, BYTE* o_led_out, BYTE p_led_brightness_level)
{
    WORD w_temp;
    
    if(p_color_detail_flag == 1)
    {   // LED詳細出力設定
        o_led_out[0] = p_rgb[0];   // R
        o_led_out[1] = p_rgb[1];   // G
        o_led_out[2] = p_rgb[2];   // B
    }
#if 0
    else
    {   // LEDカラー指定
        if(p_color_no < LED_COLOR_TYPE_NUM)
        {
            o_led_out[0] = led_color_data[p_color_no][0];   // R
            o_led_out[1] = led_color_data[p_color_no][1];   // G
            o_led_out[2] = led_color_data[p_color_no][2];   // B
        }
    }
#endif
    
    if(p_led_brightness_level == LED_BRIGHTNESS_LEVEL_DARK)
    {   // 暗い Duty 1/3
        o_led_out[0] = o_led_out[0] / 3;   // R
        o_led_out[1] = o_led_out[1] / 3;   // G
        o_led_out[2] = o_led_out[2] / 3;   // B
    }
    else if(p_led_brightness_level == LED_BRIGHTNESS_LEVEL_LIGHT)
    {   // 明るい Duty * 1.7
        w_temp = o_led_out[0];
        o_led_out[0] = (BYTE)(w_temp * 17 / 10);   // R 1.7倍
        w_temp = o_led_out[1];
        o_led_out[1] = (BYTE)(w_temp * 17 / 10);   // G 1.7倍
        w_temp = o_led_out[2];
        o_led_out[2] = (BYTE)(w_temp * 17 / 10);   // B 1.7倍
    }
    // Duty Maxチェック
    if(o_led_out[0] > LED_DUTY_MAX)
    {
        o_led_out[0] = LED_DUTY_MAX;
    }
    if(o_led_out[1] > LED_DUTY_MAX)
    {
        o_led_out[1] = LED_DUTY_MAX;
    }
    if(o_led_out[2] > LED_DUTY_MAX)
    {
        o_led_out[2] = LED_DUTY_MAX;
    }
}

void Int2_Task_ISR(void)
{
    IFS0bits.INT2IF = 0;
}
void Int4_Task_ISR(void)
{
    IFS0bits.INT4IF = 0;
}

