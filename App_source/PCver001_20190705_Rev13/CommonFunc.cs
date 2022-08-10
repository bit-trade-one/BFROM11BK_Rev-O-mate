using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RevOmate
{
    static class CommonFunc
    {
        /// LEDのRGB値から、コントロールのBackColor用のRGBに変換する
        public static int my_LED_RGB_to_Preview_RGB(int[] p_LED_RGB, int[] o_Preview_RGB, out int o_alpha)
        {
            int i_ret = -1;
            double[] tmp_ratio = new double[Constants.LED_RGB_COLOR_NUM];

            o_alpha = 255;
            try
            {

                if (p_LED_RGB.Length == Constants.LED_RGB_COLOR_NUM && o_Preview_RGB.Length == Constants.LED_RGB_COLOR_NUM)
                {
#if true
                    // 比率で色を設定
                    int max_rgb_val = 0;

                    // RGBの最大値を取得
                    for (int fi = 0; fi < p_LED_RGB.Length; fi++)
                    {
                        if (max_rgb_val < p_LED_RGB[fi])
                        {
                            max_rgb_val = p_LED_RGB[fi];
                        }
                    }

                    // 比率計算
                    for (int fi = 0; fi < p_LED_RGB.Length; fi++)
                    {
                        if (max_rgb_val == 0)
                        {
                            tmp_ratio[fi] = 0;
                        }
                        else
                        {
                            tmp_ratio[fi] = (double)p_LED_RGB[fi] / (double)max_rgb_val;
                        }
                    }

                    for (int fi = 0; fi < p_LED_RGB.Length; fi++)
                    {
                        o_Preview_RGB[fi] = (int)(0xFF * tmp_ratio[fi]);
                    }

                    // 透過度
                    //o_alpha = 0x40 + max_rgb_val;
                    //if (o_alpha > 0xFF)
                    //{
                    //    o_alpha = 0xFF;
                    //}
                    o_alpha = 0xFF;
#endif
#if false
                    // とりあえずコピー
                    for (int fi = 0; fi < p_LED_RGB.Length; fi++)
                    {
                        o_Preview_RGB[fi] = p_LED_RGB[fi];
                    }

                    if (p_LED_RGB[0] > 0 && p_LED_RGB[1] == 0 && p_LED_RGB[2] == 0)
                    {
                        o_Preview_RGB[0] = 0xFF;
                        o_Preview_RGB[1] = 0xFF - p_LED_RGB[0];
                        o_Preview_RGB[2] = 0xFF - p_LED_RGB[0];
                    }
                    else if (p_LED_RGB[1] > 0 && p_LED_RGB[0] == 0 && p_LED_RGB[2] == 0)
                    {
                        o_Preview_RGB[1] = 0xFF;
                        o_Preview_RGB[0] = 0xFF - p_LED_RGB[1];
                        o_Preview_RGB[2] = 0xFF - p_LED_RGB[1];
                    }
                    else if (p_LED_RGB[2] > 0 && p_LED_RGB[0] == 0 && p_LED_RGB[1] == 0)
                    {
                        o_Preview_RGB[2] = 0xFF;
                        o_Preview_RGB[0] = 0xFF - p_LED_RGB[2];
                        o_Preview_RGB[1] = 0xFF - p_LED_RGB[2];
                    }
                    else if (p_LED_RGB[0] == 0 && p_LED_RGB[1] == 0 && p_LED_RGB[2] == 0)
                    {
                    }
                    else
                    {
                        //o_Preview_RGB[0] = 0xFF - p_LED_RGB[0];
                        //o_Preview_RGB[1] = 0xFF - p_LED_RGB[1];
                        //o_Preview_RGB[2] = 0xFF - p_LED_RGB[2];
                    }
#endif

                    i_ret = 0;
                }
            }
            catch
            {
            }

            return i_ret;
        }
    }
}
