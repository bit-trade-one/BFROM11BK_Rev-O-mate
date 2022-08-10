using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RevOmate
{
    public partial class LEDColorSetting : Form
    {
        Form1 my_form1;
        int select_mode = 0;
        int select_func_no = -1;
        Form1.STR_BASE_MODE_INFOS my_base_mode_infos;
        Form1.STR_FUNC_DATAS my_func_datas;
        bool disp_init_flag = true;

        Label[] my_lbl_LED_Color_set;
        TrackBar[] my_tb_LED_Duty_set;
        NumericUpDown[] my_num_LED_Duty_set;
        RadioButton[] my_rbtn_LED_Brightness_Level_set;


        const byte LED_Duty_Disp_Val_Max = 100;
        byte[] my_LED_Duty_Max = new byte[] { Constants.LED_R_DUTY_MAX, Constants.LED_G_DUTY_MAX, Constants.LED_B_DUTY_MAX };
        byte[,] my_LED_Color_default = new byte[Constants.LED_COLOR_DEFAULT_SET_NUM, Constants.LED_RGB_COLOR_NUM] {
                                                        {   0,  0,  0   },
                                                        {   60, 180, 120  },
                                                        {   60, 0,  0   },
                                                        {   50, 40, 0   },
                                                        {   60, 125, 5   },
                                                        {   0,  75, 15  },
                                                        {   0,  120, 0   },
                                                        {   0,  0,  120  },
                                                        {   60, 5,  120  }};

        const int LED_COLOR_SETTING_FORM_SIZE_X = 515;
        const int LED_COLOR_SETTING_FORM_SIZE_Y = 375;
        const int LED_COLOR_SETTING_FORM_SIZE_COLOR_X = 820;
        const int LED_COLOR_SETTING_FORM_SIZE_COLOR_Y = 375;
        bool led_chromaticity_diagram = false;

        // 色度図からLED PWM値計算用
        double a_blue_green = 10.2737920937;
        double b_blue_green = -1.4043677891;
        double a_green_red = -0.8921925133;
        double b_green_red = 0.9270897967;
        double a_blue_red = 0.5311683463;
        double b_blue_red = -0.0355291526;

        const int LED_CHROMATICITY_DIAGRAM_ZERO_X = 43;
        const int LED_CHROMATICITY_DIAGRAM_ZERO_Y = 253;
        const int LED_CHROMATICITY_DIAGRAM_X_SIZE = 213;
        const int LED_CHROMATICITY_DIAGRAM_Y_SIZE = 242;
        const double LED_CHROMATICITY_DIAGRAM_X_RANGE = 0.75;
        const double LED_CHROMATICITY_DIAGRAM_Y_RANGE = 0.85;

        // x, y coordinates specific to LED
        const double a11 = 0.549995949;
        const double a12 = -0.149095646;
        const double a13 = -0.087085313;
        const double a21 = -0.551785115;
        const double a22 = 1.152833715;
        const double a23 = 0.039553889;
        const double a31 = 0.001789166;
        const double a32 = -0.003738069;
        const double a33 = 0.047531425;

        public LEDColorSetting()
        {
            InitializeComponent();
        }

        public LEDColorSetting(Form1 p_fm, int p_mode, int p_func_no, Form1.STR_BASE_MODE_INFOS p_base_mode_infos, Form1.STR_FUNC_DATAS p_func_datas)
        {
            my_form1 = p_fm;
            select_mode = p_mode;
            select_func_no = p_func_no;
            my_base_mode_infos = p_base_mode_infos;
            my_func_datas = p_func_datas;


            InitializeComponent();
        }

        private void LEDColorSetting_Load(object sender, EventArgs e)
        {
            try
            {
                this.Size = new System.Drawing.Size(LED_COLOR_SETTING_FORM_SIZE_X, LED_COLOR_SETTING_FORM_SIZE_Y);
                this.Text = RevOmate.Properties.Resources.LED_COLOR_SETTING_TITLE;
                gbx_LED_set.Text = RevOmate.Properties.Resources.LED_COLOR_SETTING_TITLE;
                btn_LED_preview.Text = RevOmate.Properties.Resources.LED_COLOR_SETTING_PREVIEW;
                lbl_LED_COLOR_9.Text = RevOmate.Properties.Resources.LED_COLOR_SETTING_OFF;
                lbl_color_r.Text = RevOmate.Properties.Resources.LED_COLOR_SETTING_R;
                lbl_color_g.Text = RevOmate.Properties.Resources.LED_COLOR_SETTING_G;
                lbl_color_b.Text = RevOmate.Properties.Resources.LED_COLOR_SETTING_B;
                lbl_LED_Brightness_title.Text = RevOmate.Properties.Resources.LED_BRIGHTNESS_TITLE;
                rbtn_LED_Level_Normal.Text = RevOmate.Properties.Resources.LED_BRIGHTNESS_LEVEL_NORMAL;
                rbtn_LED_Level_Dark.Text = RevOmate.Properties.Resources.LED_BRIGHTNESS_LEVEL_DARK;
                rbtn_LED_Level_Light.Text = RevOmate.Properties.Resources.LED_BRIGHTNESS_LEVEL_LIGHT;
                btn_submit.Text = RevOmate.Properties.Resources.BTN_SUBMIT;
                btn_cancel.Text = RevOmate.Properties.Resources.BTN_CANCEL;
                btn_color_select.Text = RevOmate.Properties.Resources.LED_COLOR_SETTING_COLOR_SELECT;
                gbx_led_color_select.Text = RevOmate.Properties.Resources.LED_COLOR_SETTING_COLOR_SELECT;

                my_lbl_LED_Color_set = new Label[] { lbl_LED_COLOR_1, lbl_LED_COLOR_2, lbl_LED_COLOR_3, lbl_LED_COLOR_4, lbl_LED_COLOR_5, lbl_LED_COLOR_6, lbl_LED_COLOR_7, lbl_LED_COLOR_8, lbl_LED_COLOR_9 };
                my_tb_LED_Duty_set = new TrackBar[] { trackBar_R, trackBar_G, trackBar_B };
                my_num_LED_Duty_set = new NumericUpDown[] { num_LED_Duty_R, num_LED_Duty_G, num_LED_Duty_B };
                my_rbtn_LED_Brightness_Level_set = new RadioButton[] { rbtn_LED_Level_Normal, rbtn_LED_Level_Dark, rbtn_LED_Level_Light };

                lbl_led_color_select_position.Parent = pic_led_color_select;

                // LED Duty設定コントローラ設定
                for (int fi = 0; fi < my_tb_LED_Duty_set.Length; fi++)
                {
                    my_tb_LED_Duty_set[fi].Maximum = LED_Duty_Disp_Val_Max;
                }
                for (int fi = 0; fi < my_num_LED_Duty_set.Length; fi++)
                {
                    my_num_LED_Duty_set[fi].Maximum = LED_Duty_Disp_Val_Max;
                }


                byte tmp_led_duty = 0;

                if(0 <= select_mode && select_mode < Constants.MODE_NUM && 0 <= select_func_no && select_func_no < Constants.FUNCTION_NUM)
                {   // 機能設定のLED設定

                    // LED設定
                    for (int fi = 0; fi < Constants.LED_RGB_COLOR_NUM; fi++)
                    {
                        tmp_led_duty = my_func_datas.func_datas[select_mode].func_data[select_func_no].LED_RGB_duty[fi];
                        if (my_LED_Duty_Max[fi] < tmp_led_duty)
                        {
                            tmp_led_duty = my_LED_Duty_Max[fi];
                        }
                        tmp_led_duty = my_LED_Duty_Set_Val_to_Disp_Val(tmp_led_duty, fi);
                        my_tb_LED_Duty_set[fi].Value = tmp_led_duty;
                        my_num_LED_Duty_set[fi].Value = tmp_led_duty;
                    }
                    my_LED_Color_preview_disp(my_tb_LED_Duty_set[0].Value, my_tb_LED_Duty_set[1].Value, my_tb_LED_Duty_set[2].Value);
                    // LED輝度設定
                    if (my_func_datas.func_datas[select_mode].func_data[select_func_no].LED_brightness_level < Constants.LED_BRIGHTNESS_LEVEL_SET_NUM)
                    {
                        my_rbtn_LED_Brightness_Level_set[my_func_datas.func_datas[select_mode].func_data[select_func_no].LED_brightness_level].Checked = true;
                    }
                    else
                    {
                        my_rbtn_LED_Brightness_Level_set[Constants.LED_BRIGHTNESS_LEVEL_NORMAL].Checked = true;
                    }
                }
                else if(0 <= select_mode && select_mode < Constants.MODE_NUM)
                {   // モードのLED設定

                    // LED設定
                    for (int fi = 0; fi < Constants.LED_RGB_COLOR_NUM; fi++)
                    {
                        tmp_led_duty = my_base_mode_infos.base_mode_infos[select_mode].LED_RGB_duty[fi];
                        if (my_LED_Duty_Max[fi] < tmp_led_duty)
                        {
                            tmp_led_duty = my_LED_Duty_Max[fi];
                        }
                        tmp_led_duty = my_LED_Duty_Set_Val_to_Disp_Val(tmp_led_duty, fi);
                        my_tb_LED_Duty_set[fi].Value = tmp_led_duty;
                        my_num_LED_Duty_set[fi].Value = tmp_led_duty;
                    }
                    my_LED_Color_preview_disp(my_tb_LED_Duty_set[0].Value, my_tb_LED_Duty_set[1].Value, my_tb_LED_Duty_set[2].Value);
                    // LED輝度設定
                    if (my_base_mode_infos.base_mode_infos[select_mode].LED_brightness_level < Constants.LED_BRIGHTNESS_LEVEL_SET_NUM)
                    {
                        my_rbtn_LED_Brightness_Level_set[my_base_mode_infos.base_mode_infos[select_mode].LED_brightness_level].Checked = true;
                    }
                    else
                    {
                        my_rbtn_LED_Brightness_Level_set[Constants.LED_BRIGHTNESS_LEVEL_NORMAL].Checked = true;
                    }
                }


                disp_init_flag = false;
            }
            catch
            {
            }
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            byte[] tmp_rgb = new byte[Constants.LED_RGB_COLOR_NUM];
            byte tmp_led_duty = 0;
            byte tmp_brightness_level = 0;
            bool change_flag = false;
            try
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;


                // LED
                for (int fi = 0; fi < Constants.LED_RGB_COLOR_NUM; fi++)
                {
                    tmp_led_duty = (byte)(my_tb_LED_Duty_set[fi].Value & 0xFF);
                    tmp_led_duty = my_LED_Duty_Disp_Val_to_Set_Val(tmp_led_duty, fi);
                    if (my_LED_Duty_Max[fi] < tmp_led_duty)
                    {
                        tmp_led_duty = my_LED_Duty_Max[fi];
                    }
                    tmp_rgb[fi] = tmp_led_duty;
                }

                // LED輝度設定
                for (int fi = 0; fi < my_rbtn_LED_Brightness_Level_set.Length; fi++)
                {
                    if (my_rbtn_LED_Brightness_Level_set[fi].Checked == true)
                    {
                        tmp_brightness_level = (byte)(fi & 0xFF);
                        break;
                    }
                }

                if (0 <= select_mode && select_mode < Constants.MODE_NUM && 0 <= select_func_no && select_func_no < Constants.FUNCTION_NUM)
                {   // 機能設定のLED設定

                    // LED
                    for (int fi = 0; fi < Constants.LED_RGB_COLOR_NUM; fi++)
                    {
                        if (tmp_rgb[fi] != my_func_datas.func_datas[select_mode].func_data[select_func_no].LED_RGB_duty[fi])
                        {   // 変更あり
                            change_flag = true;
                            break;
                        }
                    }

                    // LED輝度設定
                    if (tmp_brightness_level != my_func_datas.func_datas[select_mode].func_data[select_func_no].LED_brightness_level)
                    {   // 変更あり
                        change_flag = true;
                    }

                    // 変更ありなら設定書き換え要求
                    if (change_flag == true)
                    {
                        my_form1.my_LED_color_set_req_by_func(select_mode, select_func_no, tmp_rgb, tmp_brightness_level);
                    }
                }
                else if (0 <= select_mode && select_mode < Constants.MODE_NUM)
                {   // モードのLED設定

                    // LED
                    for (int fi = 0; fi < Constants.LED_RGB_COLOR_NUM; fi++)
                    {
                        if (tmp_rgb[fi] != my_base_mode_infos.base_mode_infos[select_mode].LED_RGB_duty[fi])
                        {   // 変更あり
                            change_flag = true;
                            break;
                        }
                    }

                    // LED輝度設定
                    if (tmp_brightness_level != my_base_mode_infos.base_mode_infos[select_mode].LED_brightness_level)
                    {   // 変更あり
                        change_flag = true;
                    }

                    // 変更ありなら設定書き換え要求
                    if (change_flag == true)
                    {
                        my_form1.my_LED_color_set_req_by_mode(select_mode, tmp_rgb, tmp_brightness_level);
                    }
                }
            }
            catch
            {
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch
            {
            }
        }

        private void btn_LED_preview_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    byte[] tmp_rgb = new byte[my_tb_LED_Duty_set.Length];
                    byte tmp_brightness_level = 0;
                    my_Get_Disp_Data(ref tmp_rgb, ref tmp_brightness_level);

                    my_Set_LED_Color_Preview_Req(tmp_rgb, tmp_brightness_level);
                }
                catch
                {
                }
            }
            catch
            {
            }
        }

        private void trackBar_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int tag_no = int.Parse(((TrackBar)sender).Tag.ToString());

                my_LED_Color_preview_disp(trackBar_R.Value, trackBar_G.Value, trackBar_B.Value);

                if (disp_init_flag == false)
                {   // 画面初期化完了済み

                    byte[] tmp_rgb = new byte[my_tb_LED_Duty_set.Length];
                    byte tmp_brightness_level = 0;
                    my_Get_Disp_Data(ref tmp_rgb, ref tmp_brightness_level);

                    for (int fi = 0; fi < tmp_rgb.Length; fi++)
                    {
                        tmp_rgb[fi] = my_LED_Duty_Disp_Val_to_Set_Val(tmp_rgb[fi], fi);
                    }

                    my_Set_LED_Color_Preview_Req(tmp_rgb, tmp_brightness_level);
                }
                my_num_LED_Duty_set[tag_no].Value = my_tb_LED_Duty_set[tag_no].Value;

                lbl_led_color_select_position.Visible = false;
            }
            catch
            {
            }
        }

        private void num_LED_Duty_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int tag_no = int.Parse(((NumericUpDown)sender).Tag.ToString());
                my_tb_LED_Duty_set[tag_no].Value = (int)(my_num_LED_Duty_set[tag_no].Value);

                lbl_led_color_select_position.Visible = false;
            }
            catch
            {
            }
        }

        private void my_LED_Color_preview_disp(int p_r, int p_g, int p_b)
        {
            int tmp_alpha = 0;
            try
            {
                int[] tmp_LED_RGB = new int[Constants.LED_RGB_COLOR_NUM];
                int[] tmp_Preview_RGB = new int[Constants.LED_RGB_COLOR_NUM];
                tmp_LED_RGB[0] = p_r * 0xFF / trackBar_R.Maximum;
                tmp_LED_RGB[1] = p_g * 0xFF / trackBar_G.Maximum;
                tmp_LED_RGB[2] = p_b * 0xFF / trackBar_B.Maximum;

                CommonFunc.my_LED_RGB_to_Preview_RGB(tmp_LED_RGB, tmp_Preview_RGB, out tmp_alpha);
                lbl_LED_COLOR_NOW.BackColor = Color.FromArgb(tmp_alpha, tmp_Preview_RGB[0], tmp_Preview_RGB[1], tmp_Preview_RGB[2]);
            }
            catch
            {
            }
        }

        private void lbl_LED_COLOR_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                int color_idx = int.Parse(((Label)sender).Tag.ToString());

                if (0 <= color_idx && color_idx < my_LED_Color_default.GetLength(0))
                {
                    for (int fi = 0; fi < my_tb_LED_Duty_set.Length && fi < my_LED_Color_default.GetLength(1); fi++)
                    {
                        my_tb_LED_Duty_set[fi].Value = my_LED_Color_default[color_idx, fi];
                    }
                }
            }
            catch
            {
            }
        }

        private void my_Get_Disp_Data(ref byte[] o_rgb, ref byte o_brightness_level)
        {
            try
            {
                for (int fi = 0; fi < o_rgb.Length; fi++)
                {
                    o_rgb[fi] = 0;
                }
                o_brightness_level = 0;

                if (my_tb_LED_Duty_set.Length == o_rgb.Length)
                {
                    for (int fi = 0; fi < o_rgb.Length && fi < my_tb_LED_Duty_set.Length; fi++)
                    {
                        o_rgb[fi] = (byte)(my_tb_LED_Duty_set[fi].Value & 0xFF);
                    }
                }
                for (int fi = 0; fi < my_rbtn_LED_Brightness_Level_set.Length; fi++)
                {
                    if (my_rbtn_LED_Brightness_Level_set[fi].Checked == true)
                    {
                        o_brightness_level = (byte)(fi & 0xFF);
                        break;
                    }
                }
            }
            catch
            {
            }
        }
        private void my_Set_LED_Color_Preview_Req(byte[] p_rgb, byte p_brightness_level)
        {
            try
            {
                my_form1.my_LED_Preview_Set(p_rgb, p_brightness_level);
            }
            catch
            {
            }
        }

        private void rbtn_LED_Level_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (disp_init_flag == false)
                {   // 画面初期化完了済み

                    byte[] tmp_rgb = new byte[my_tb_LED_Duty_set.Length];
                    byte tmp_brightness_level = 0;
                    my_Get_Disp_Data(ref tmp_rgb, ref tmp_brightness_level);

                    my_Set_LED_Color_Preview_Req(tmp_rgb, tmp_brightness_level);
                }
            }
            catch
            {
            }
        }

        // 設定値から画面表示値へ変換
        // 設定値0～90 -> 画面表示 0～100
        private byte my_LED_Duty_Set_Val_to_Disp_Val(byte p_LED_Duty, int p_RGB_idx)
        {
            byte b_ret_val = 0;
            uint uint_tmp = 0;
            try
            {
                if (0 <= p_RGB_idx && p_RGB_idx < my_LED_Duty_Max.Length)
                {
                    uint_tmp = (uint)(p_LED_Duty * LED_Duty_Disp_Val_Max / my_LED_Duty_Max[p_RGB_idx]);
                    b_ret_val = (byte)(uint_tmp & 0xFF);
                }
            }
            catch
            {
            }
            return b_ret_val;
        }
        // 画面表示値から設定値へ変換
        // 画面表示 0～100 -> 設定値0～90
        private byte my_LED_Duty_Disp_Val_to_Set_Val(byte p_LED_Duty, int p_RGB_idx)
        {
            byte b_ret_val = 0;
            uint uint_tmp = 0;
            try
            {
                if (0 <= p_RGB_idx && p_RGB_idx < my_LED_Duty_Max.Length)
                {
                    uint_tmp = (uint)(p_LED_Duty * my_LED_Duty_Max[p_RGB_idx] / LED_Duty_Disp_Val_Max);
                    b_ret_val = (byte)(uint_tmp & 0xFF);
                }
            }
            catch
            {
            }
            return b_ret_val;
        }

        private void btn_color_select_Click(object sender, EventArgs e)
        {
            //double[] tmp_ratio = new double[Constants.LED_RGB_COLOR_NUM];
            //int[] set_rgb = new int[Constants.LED_RGB_COLOR_NUM];
            //int[] temp_rgb = new int[Constants.LED_RGB_COLOR_NUM];
            try
            {
                if (led_chromaticity_diagram == false)
                {
                    led_chromaticity_diagram = true;
                    this.Size = new System.Drawing.Size(LED_COLOR_SETTING_FORM_SIZE_COLOR_X, LED_COLOR_SETTING_FORM_SIZE_COLOR_Y);
                }
                else
                {
                    led_chromaticity_diagram = false;
                    this.Size = new System.Drawing.Size(LED_COLOR_SETTING_FORM_SIZE_X, LED_COLOR_SETTING_FORM_SIZE_Y);
                }

                ////Dutyの最大値取得
                //int max_duty = 0;
                //for(int fi = 0; fi < my_num_LED_Duty_set.Length; fi++)
                //{
                //    if(max_duty < my_num_LED_Duty_set[fi].Value)
                //    {
                //        max_duty = (int)my_num_LED_Duty_set[fi].Value;
                //    }
                //}
                //// 比率計算
                //for (int fi = 0; fi < my_num_LED_Duty_set.Length; fi++)
                //{
                //    if (max_duty == 0)
                //    {
                //        tmp_ratio[fi] = 0;
                //    }
                //    else
                //    {
                //        tmp_ratio[fi] = (double)my_num_LED_Duty_set[fi].Value / (double)max_duty;
                //    }
                //}

                //for (int fi = 0; fi < my_num_LED_Duty_set.Length; fi++)
                //{
                //    set_rgb[fi] = (int)(0xFF * tmp_ratio[fi]);
                //}

                //Color set_color = Color.FromArgb(set_rgb[0], set_rgb[1], set_rgb[2]);
                //colorDialog1.Color = set_color;
                //DialogResult dr = colorDialog1.ShowDialog();
                //if (dr == DialogResult.OK)
                //{
                //    temp_rgb[0] = colorDialog1.Color.R;
                //    temp_rgb[1] = colorDialog1.Color.G;
                //    temp_rgb[2] = colorDialog1.Color.B;

                //    bool change_flag = false;
                //    for (int fi = 0; fi < set_rgb.Length; fi++)
                //    {
                //        if (set_rgb[fi] != temp_rgb[fi])
                //        {
                //            change_flag = true;
                //            break;
                //        }
                //    }
                //    // 変更あり
                //    if (change_flag == true)
                //    {
                //        //rgbの最大値取得
                //        int max_rgb = 0;
                //        for (int fi = 0; fi < temp_rgb.Length; fi++)
                //        {
                //            if (max_rgb < temp_rgb[fi])
                //            {
                //                max_rgb = temp_rgb[fi];
                //            }
                //        }
                //        // 比率計算
                //        for (int fi = 0; fi < temp_rgb.Length; fi++)
                //        {
                //            if (max_rgb == 0)
                //            {
                //                tmp_ratio[fi] = 0;
                //            }
                //            else
                //            {
                //                tmp_ratio[fi] = (double)temp_rgb[fi] / (double)max_rgb;
                //            }
                //        }

                //        for (int fi = 0; fi < my_num_LED_Duty_set.Length; fi++)
                //        {
                //            set_rgb[fi] = (int)(my_LED_Duty_Max[fi] * tmp_ratio[fi]);
                //            my_num_LED_Duty_set[fi].Value = set_rgb[fi];
                //        }
                //    }
                //}
            }
            catch
            {
            }
        }

        private void pic_led_color_select_MouseClick(object sender, MouseEventArgs e)
        {
            bool out_of_range = false;
            double temp_dbl1;
            try
            {
                lbl_led_color_select_msg.Visible = false;

                // LED表示色範囲チェック
                double pos_x = e.Location.X;
                double pos_y = e.Location.Y;
                //色度図内をクリックしたかチェック
                if (LED_CHROMATICITY_DIAGRAM_ZERO_X <= pos_x && pos_x <= (LED_CHROMATICITY_DIAGRAM_ZERO_X + LED_CHROMATICITY_DIAGRAM_X_SIZE)
                    && LED_CHROMATICITY_DIAGRAM_ZERO_Y >= pos_y && pos_y >= (LED_CHROMATICITY_DIAGRAM_ZERO_Y - LED_CHROMATICITY_DIAGRAM_Y_SIZE))
                {   // 色度図内
                    pos_x = pos_x - LED_CHROMATICITY_DIAGRAM_ZERO_X;
                    pos_x = pos_x * LED_CHROMATICITY_DIAGRAM_X_RANGE / LED_CHROMATICITY_DIAGRAM_X_SIZE;
                    pos_y = LED_CHROMATICITY_DIAGRAM_ZERO_Y - pos_y;
                    pos_y = pos_y * LED_CHROMATICITY_DIAGRAM_Y_RANGE / LED_CHROMATICITY_DIAGRAM_Y_SIZE;

                    // Blue - Greenライン
                    temp_dbl1 = (a_blue_green * pos_x) + b_blue_green;
                    if (temp_dbl1 >= pos_y)
                    {
                        // Green - Redライン
                        temp_dbl1 = (a_green_red * pos_x) + b_green_red;
                        if (temp_dbl1 >= pos_y)
                        {
                            // Blue - Redライン
                            temp_dbl1 = (a_blue_red * pos_x) + b_blue_red;
                            if (temp_dbl1 <= pos_y)
                            {
                                double b1 = pos_x / pos_y;  // x/y
                                double b2 = 1;  // 1
                                double b3 = (1 - pos_x - pos_y) / pos_y;  // (1-x-y)/y

                                double r_pwm = (a11 * b1) + (a12 * b2) + (a13 * b3);
                                double g_pwm = (a21 * b1) + (a22 * b2) + (a23 * b3);
                                double b_pwm = (a31 * b1) + (a32 * b2) + (a33 * b3);
                                int r_int = (int)(r_pwm * 100);
                                if (r_int < 0)
                                {
                                    r_int = 0;
                                }
                                else if (r_int > LED_Duty_Disp_Val_Max)
                                {
                                    r_int = LED_Duty_Disp_Val_Max;
                                }
                                int g_int = (int)(g_pwm * 100);
                                if (g_int < 0)
                                {
                                    g_int = 0;
                                }
                                else if (g_int > LED_Duty_Disp_Val_Max)
                                {
                                    g_int = LED_Duty_Disp_Val_Max;
                                }
                                int b_int = (int)(b_pwm * 100);
                                if (b_int < 0)
                                {
                                    b_int = 0;
                                }
                                else if (b_int > LED_Duty_Disp_Val_Max)
                                {
                                    b_int = LED_Duty_Disp_Val_Max;
                                }
                                my_num_LED_Duty_set[0].Value = r_int;
                                my_num_LED_Duty_set[1].Value = g_int;
                                my_num_LED_Duty_set[2].Value = b_int;

                                // 設定範囲内
                                lbl_led_color_select_position.Location = new Point(e.Location.X, e.Location.Y);
                                lbl_led_color_select_position.Visible = true;
                            }
                            else
                            {
                                out_of_range = true;
                            }
                        }
                        else
                        {
                            out_of_range = true;
                        }
                    }
                    else
                    {
                        out_of_range = true;
                    }
                }
                else
                {   // 図の範囲外
                    //out_of_range = true;
                }

                if (out_of_range == true)
                {
                    lbl_led_color_select_position.Visible = false;
                    lbl_led_color_select_msg.Text = RevOmate.Properties.Resources.LED_COLOR_SETTING_SET_COLOR_ERROR_MSG;
                    lbl_led_color_select_msg.Visible = true;
                }
            }
            catch
            {
            }
        }
    }
}
