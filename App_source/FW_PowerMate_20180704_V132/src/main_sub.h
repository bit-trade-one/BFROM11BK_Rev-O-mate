#ifndef MAIN_SUB_H
#define MAIN_SUB_H

#include "app.h"

// Timer4
// 50MHz プリスケーラ2
// TMRのカウントは0.04us
#define SOFT_DELAY_1US      25
#define SOFT_DELAY_5US      125
#define SOFT_DELAY_10US     250
#define SOFT_DELAY_100US    2500
#define SOFT_DELAY_1MS      25000
#define SOFT_DELAY_10MS     250000
#define SOFT_DELAY_100MS    2500000

extern void Soft_Delay(DWORD delayValue);
extern void Switch_Input(void);
extern void Encoder_Input(void);

extern BYTE Get_SW_Input_Output_Status(void);
extern void SW_Output_Mouse(BYTE* out_buff, BYTE buff_size);
extern void SW_Output_Keyboard(BYTE* out_buff, BYTE buff_size);
extern void SW_Output_Multimedia(BYTE* out_buff, BYTE buff_size);
extern void SW_Output_Joystick(BYTE* out_buff, BYTE buff_size);

extern BYTE Get_Encoder_Output_Status(void);
extern void Encoder_Output_Mouse(BYTE* out_buff, BYTE buff_size);
extern void Encoder_Output_Keyboard(BYTE* out_buff, BYTE buff_size);
extern void Encoder_Output_Multimedia(BYTE* out_buff, BYTE buff_size);
extern void Encoder_Output_Joystick(BYTE* out_buff, BYTE buff_size);
extern void Encoder_Output_Script(void);
extern void Set_Base_Head(UN_BASE_HEAD* p_base_head_source, UN_BASE_HEAD* p_base_head_set);
extern void Set_Base_Info(BYTE p_mode_no, UN_BASE_INFO* p_base_info_source, UN_BASE_INFO* p_base_info_set);
extern void Set_Func_Info(BYTE p_mode_no, BYTE p_func_no, UN_FUNC_INFO* p_func_info_source, UN_FUNC_INFO* p_func_info_set);
extern void Set_EncoderScript_Info(BYTE p_no, UN_ENCODER_SCRIPT_INFO* p_encoder_script_info_source, UN_ENCODER_SCRIPT_INFO* p_encoder_script_info_set);
extern void set_LED(BYTE no, BYTE onoff);
extern void set_LED_output_data(BYTE p_color_no, BYTE p_color_detail_flag, BYTE* p_rgb, BYTE* o_led_out, BYTE p_led_brightness_level);

extern void Int2_Task_ISR(void);
extern void Int4_Task_ISR(void);

#endif //MAIN_SUB_H

