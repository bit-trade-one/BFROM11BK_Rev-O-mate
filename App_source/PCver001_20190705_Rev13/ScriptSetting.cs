using System;
using System.IO;
using System.Collections;
//using System.Collections.Generic;
//using System.Collections.Specialized;
//using System.ComponentModel;
//using System.Configuration;
using System.Text;
using System.Windows.Forms;
//using System.Data;
using System.Drawing;
//using Microsoft.Win32.SafeHandles;
//using System.Runtime.InteropServices;
//using System.Threading;
//using System.Xml.Serialization;
//using System.Runtime.Serialization;
//using System.Runtime.Serialization.Formatters.Soap;
using System.Diagnostics;
//using System.Resources;
using System.Reflection;

namespace RevOmate
{
    public partial class Form1 : Form
    {
        internal struct STR_SCRIPT_EDITOR_DISP_DATA
        {
            internal byte Script_Mode;
            internal ArrayList al_Command_ID;
            internal ArrayList al_Command_Data1;
            internal ArrayList al_Command_Data2;
            internal ArrayList al_Command_Size;
            internal ArrayList al_Command_Icon_idx;
            internal ArrayList al_Disp_Update_Flag;

            internal void init_data()
            {
                al_Command_ID = new ArrayList();
                al_Command_Data1 = new ArrayList();
                al_Command_Data2 = new ArrayList();
                al_Command_Size = new ArrayList();
                al_Command_Icon_idx = new ArrayList();
                al_Disp_Update_Flag = new ArrayList();
                // 先頭はモード表示
                Script_Mode = Constants.SCRIPT_MODE_ONE_MODE;
                //Disp_Script_Name[0] = Constants.SCRIPT_MODE_ONE_MODE_TEXT;
            }
            internal void init_disp_data(byte p_disp_num)
            {
            }
            internal void add(int index, byte p_cmd_id, byte p_data1, byte p_data2, int size, Stopwatch p_sw, bool p_change_flag)
            {

                // 先頭はモードのため、index == 1が0として追加
                if (0 < index)
                {
                    index -= 1;
                }

                //index check
                if (index < 0 || al_Command_ID.Count < index)
                {   // 負の場合は最後尾に追加
                    index = al_Command_ID.Count;
                }

                // 間隔取得
                p_sw.Stop();
                long millisec = p_sw.ElapsedMilliseconds;
                if (millisec > 0xFFFF)
                {
                    millisec = 0xFFFF;
                }
                else if (millisec == 0)
                {
                    millisec = 1;
                }
                p_sw.Reset();
                p_sw.Start();

                // 最初の追加項目で、手動追加でないときは追加しない
                if (p_cmd_id == Constants.SCRIPT_COMMAND_INTERVAL && al_Command_ID.Count == 0 && p_data1 == 0 && p_data2 == 0)
                {
                    index = -1;
                }


                if (0 <= index && index <= al_Command_ID.Count && p_change_flag == false)
                {   // 追加
                    al_Command_ID.Insert(index, p_cmd_id);
                    if (p_cmd_id == Constants.SCRIPT_COMMAND_INTERVAL)
                    {
                        // パラメータが0なら計測した時間を設定
                        if (p_data1 == 0 && p_data2 == 0)
                        {
                            p_data1 = (byte)((millisec >> 8) & 0xFF);
                            p_data2 = (byte)(millisec & 0xFF);
                        }

                        al_Command_Data1.Insert(index, p_data1);
                        al_Command_Data2.Insert(index, p_data2);
                        al_Command_Size.Insert(index, size);
                        al_Command_Icon_idx.Insert(index, Constants.SCRIPT_ICON_IDX_INTERVAL);
                        al_Disp_Update_Flag.Insert(index, Constants.DISPLAY_UPDATE_TYPE_ADD);
                    }
                    else if (p_cmd_id == Constants.SCRIPT_COMMAND_KEY_PRESS)
                    {
                        al_Command_Data1.Insert(index, p_data1);
                        al_Command_Data2.Insert(index, (byte)0);
                        al_Command_Size.Insert(index, size);
                        al_Command_Icon_idx.Insert(index, Constants.SCRIPT_ICON_IDX_KEY_PRESS);
                        al_Disp_Update_Flag.Insert(index, Constants.DISPLAY_UPDATE_TYPE_ADD);
                    }
                    else if (p_cmd_id == Constants.SCRIPT_COMMAND_KEY_RELEASE)
                    {
                        al_Command_Data1.Insert(index, p_data1);
                        al_Command_Data2.Insert(index, (byte)0);
                        al_Command_Size.Insert(index, size);
                        al_Command_Icon_idx.Insert(index, Constants.SCRIPT_ICON_IDX_KEY_RELEASE);
                        al_Disp_Update_Flag.Insert(index, Constants.DISPLAY_UPDATE_TYPE_ADD);
                    }
                    else if (p_cmd_id == Constants.SCRIPT_COMMAND_L_CLICK
                            || p_cmd_id == Constants.SCRIPT_COMMAND_R_CLICK
                            || p_cmd_id == Constants.SCRIPT_COMMAND_W_CLICK
                            || p_cmd_id == Constants.SCRIPT_COMMAND_B4_CLICK
                            || p_cmd_id == Constants.SCRIPT_COMMAND_B5_CLICK)
                    {
                        al_Command_Data1.Insert(index, (byte)0);
                        al_Command_Data2.Insert(index, (byte)0);
                        al_Command_Size.Insert(index, size);
                        al_Command_Icon_idx.Insert(index, Constants.SCRIPT_ICON_IDX_MOUSE_CLICK);
                        al_Disp_Update_Flag.Insert(index, Constants.DISPLAY_UPDATE_TYPE_ADD);
                    }
                    else if (p_cmd_id == Constants.SCRIPT_COMMAND_L_RELEASE
                            || p_cmd_id == Constants.SCRIPT_COMMAND_R_RELEASE
                            || p_cmd_id == Constants.SCRIPT_COMMAND_W_RELEASE
                            || p_cmd_id == Constants.SCRIPT_COMMAND_B4_RELEASE
                            || p_cmd_id == Constants.SCRIPT_COMMAND_B5_RELEASE)
                    {
                        al_Command_Data1.Insert(index, (byte)0);
                        al_Command_Data2.Insert(index, (byte)0);
                        al_Command_Size.Insert(index, size);
                        al_Command_Icon_idx.Insert(index, Constants.SCRIPT_ICON_IDX_MOUSE_RELEASE);
                        al_Disp_Update_Flag.Insert(index, Constants.DISPLAY_UPDATE_TYPE_ADD);
                    }
                    else if (p_cmd_id == Constants.SCRIPT_COMMAND_WHEEL_UP
                            || p_cmd_id == Constants.SCRIPT_COMMAND_WHEEL_DOWN)
                    {
                        al_Command_Data1.Insert(index, p_data1);
                        al_Command_Data2.Insert(index, (byte)0);
                        al_Command_Size.Insert(index, size);
                        al_Command_Icon_idx.Insert(index, Constants.SCRIPT_ICON_IDX_WHEEL_SCROLL);
                        al_Disp_Update_Flag.Insert(index, Constants.DISPLAY_UPDATE_TYPE_ADD);
                    }
                    else if (p_cmd_id == Constants.SCRIPT_COMMAND_JOY_L_LEVER
                            || p_cmd_id == Constants.SCRIPT_COMMAND_JOY_R_LEVER)
                    {
                        al_Command_Data1.Insert(index, p_data1);
                        al_Command_Data2.Insert(index, p_data2);
                        al_Command_Size.Insert(index, size);
                        al_Command_Icon_idx.Insert(index, Constants.SCRIPT_ICON_IDX_JOY_LEVER_L_PRESS);
                        al_Disp_Update_Flag.Insert(index, Constants.DISPLAY_UPDATE_TYPE_ADD);
                    }
                    else if (p_cmd_id == Constants.SCRIPT_COMMAND_JOY_L_LEVER_CENTER
                            || p_cmd_id == Constants.SCRIPT_COMMAND_JOY_R_LEVER_CENTER)
                    {
                        al_Command_Data1.Insert(index, (byte)0);
                        al_Command_Data2.Insert(index, (byte)0);
                        al_Command_Size.Insert(index, size);
                        al_Command_Icon_idx.Insert(index, Constants.SCRIPT_ICON_IDX_JOY_LEVER_L_RELEASE);
                        al_Disp_Update_Flag.Insert(index, Constants.DISPLAY_UPDATE_TYPE_ADD);
                    }
                    else if (p_cmd_id == Constants.SCRIPT_COMMAND_JOY_BUTTON_PRESS)
                    {
                        al_Command_Data1.Insert(index, p_data1);
                        al_Command_Data2.Insert(index, (byte)0);
                        al_Command_Size.Insert(index, size);
                        al_Command_Icon_idx.Insert(index, Constants.SCRIPT_ICON_IDX_JOY_BUTTON_PRESS);
                        al_Disp_Update_Flag.Insert(index, Constants.DISPLAY_UPDATE_TYPE_ADD);
                    }
                    else if (p_cmd_id == Constants.SCRIPT_COMMAND_JOY_BUTTON_RELESE)
                    {
                        al_Command_Data1.Insert(index, p_data1);
                        al_Command_Data2.Insert(index, (byte)0);
                        al_Command_Size.Insert(index, size);
                        al_Command_Icon_idx.Insert(index, Constants.SCRIPT_ICON_IDX_JOY_BUTTON_RELEASE);
                        al_Disp_Update_Flag.Insert(index, Constants.DISPLAY_UPDATE_TYPE_ADD);
                    }
                    else if (p_cmd_id == Constants.SCRIPT_COMMAND_JOY_HATSW_PRESS)
                    {
                        al_Command_Data1.Insert(index, p_data1);
                        al_Command_Data2.Insert(index, (byte)0);
                        al_Command_Size.Insert(index, size);
                        al_Command_Icon_idx.Insert(index, Constants.SCRIPT_ICON_IDX_JOY_HAT_SW_PRESS);
                        al_Disp_Update_Flag.Insert(index, Constants.DISPLAY_UPDATE_TYPE_ADD);
                    }
                    else if (p_cmd_id == Constants.SCRIPT_COMMAND_JOY_HATSW_RELESE)
                    {
                        al_Command_Data1.Insert(index, (byte)0);
                        al_Command_Data2.Insert(index, (byte)0);
                        al_Command_Size.Insert(index, size);
                        al_Command_Icon_idx.Insert(index, Constants.SCRIPT_ICON_IDX_JOY_HAT_SW_RELEASE);
                        al_Disp_Update_Flag.Insert(index, Constants.DISPLAY_UPDATE_TYPE_ADD);
                    }
                    else if (p_cmd_id == Constants.SCRIPT_COMMAND_MOUSE_MOVE)
                    {
                        al_Command_Data1.Insert(index, p_data1);
                        al_Command_Data2.Insert(index, p_data2);
                        al_Command_Size.Insert(index, size);
                        al_Command_Icon_idx.Insert(index, Constants.SCRIPT_ICON_IDX_MOUSE_MOVE);
                        al_Disp_Update_Flag.Insert(index, Constants.DISPLAY_UPDATE_TYPE_ADD);
                    }
                    else if (p_cmd_id == Constants.SCRIPT_COMMAND_MULTIMEDIA_PRESS)
                    {
                        al_Command_Data1.Insert(index, p_data1);
                        al_Command_Data2.Insert(index, (byte)0);
                        al_Command_Size.Insert(index, size);
                        al_Command_Icon_idx.Insert(index, Constants.SCRIPT_ICON_IDX_MULTIMEDIA_PRESS);
                        al_Disp_Update_Flag.Insert(index, Constants.DISPLAY_UPDATE_TYPE_ADD);
                    }
                    else if (p_cmd_id == Constants.SCRIPT_COMMAND_MULTIMEDIA_RELESE)
                    {
                        al_Command_Data1.Insert(index, p_data1);
                        al_Command_Data2.Insert(index, (byte)0);
                        al_Command_Size.Insert(index, size);
                        al_Command_Icon_idx.Insert(index, Constants.SCRIPT_ICON_IDX_MULTIMEDIA_RELEASE);
                        al_Disp_Update_Flag.Insert(index, Constants.DISPLAY_UPDATE_TYPE_ADD);
                    }
                }
                else if (0 <= index && index < al_Command_ID.Count && p_change_flag == true)
                {   // 変更
                    al_Command_ID[index] = p_cmd_id;
                    if (p_cmd_id == Constants.SCRIPT_COMMAND_INTERVAL)
                    {
                        // パラメータが0なら計測した時間を設定
                        if (p_data1 == 0 && p_data2 == 0)
                        {
                            p_data1 = (byte)((millisec >> 8) & 0xFF);
                            p_data2 = (byte)(millisec & 0xFF);
                        }

                        al_Command_Data1[index] = p_data1;
                        al_Command_Data2[index] = p_data2;
                        al_Command_Size[index] = size;
                        al_Command_Icon_idx[index] = Constants.SCRIPT_ICON_IDX_INTERVAL;
                        al_Disp_Update_Flag.Insert(index, Constants.DISPLAY_UPDATE_TYPE_RE);
                    }
                    else if (p_cmd_id == Constants.SCRIPT_COMMAND_KEY_PRESS)
                    {
                        al_Command_Data1[index] = p_data1;
                        al_Command_Data2[index] = (byte)0;
                        al_Command_Size[index] = size;
                        al_Command_Icon_idx[index] = Constants.SCRIPT_ICON_IDX_KEY_PRESS;
                        al_Disp_Update_Flag.Insert(index, Constants.DISPLAY_UPDATE_TYPE_RE);
                    }
                    else if (p_cmd_id == Constants.SCRIPT_COMMAND_KEY_RELEASE)
                    {
                        al_Command_Data1[index] = p_data1;
                        al_Command_Data2[index] = (byte)0;
                        al_Command_Size[index] = size;
                        al_Command_Icon_idx[index] = Constants.SCRIPT_ICON_IDX_KEY_RELEASE;
                        al_Disp_Update_Flag.Insert(index, Constants.DISPLAY_UPDATE_TYPE_RE);
                    }
                    else if (p_cmd_id == Constants.SCRIPT_COMMAND_L_CLICK
                            || p_cmd_id == Constants.SCRIPT_COMMAND_R_CLICK
                            || p_cmd_id == Constants.SCRIPT_COMMAND_W_CLICK
                            || p_cmd_id == Constants.SCRIPT_COMMAND_B4_CLICK
                            || p_cmd_id == Constants.SCRIPT_COMMAND_B5_CLICK)
                    {
                        al_Command_Data1[index] = (byte)0;
                        al_Command_Data2[index] = (byte)0;
                        al_Command_Size[index] = size;
                        al_Command_Icon_idx[index] = Constants.SCRIPT_ICON_IDX_MOUSE_CLICK;
                        al_Disp_Update_Flag.Insert(index, Constants.DISPLAY_UPDATE_TYPE_RE);
                    }
                    else if (p_cmd_id == Constants.SCRIPT_COMMAND_L_RELEASE
                            || p_cmd_id == Constants.SCRIPT_COMMAND_R_RELEASE
                            || p_cmd_id == Constants.SCRIPT_COMMAND_W_RELEASE
                            || p_cmd_id == Constants.SCRIPT_COMMAND_B4_RELEASE
                            || p_cmd_id == Constants.SCRIPT_COMMAND_B5_RELEASE)
                    {
                        al_Command_Data1[index] = (byte)0;
                        al_Command_Data2[index] = (byte)0;
                        al_Command_Size[index] = size;
                        al_Command_Icon_idx[index] = Constants.SCRIPT_ICON_IDX_MOUSE_RELEASE;
                        al_Disp_Update_Flag.Insert(index, Constants.DISPLAY_UPDATE_TYPE_RE);
                    }
                    else if (p_cmd_id == Constants.SCRIPT_COMMAND_WHEEL_UP
                            || p_cmd_id == Constants.SCRIPT_COMMAND_WHEEL_DOWN)
                    {
                        al_Command_Data1[index] = p_data1;
                        al_Command_Data2[index] = (byte)0;
                        al_Command_Size[index] = size;
                        al_Command_Icon_idx[index] = Constants.SCRIPT_ICON_IDX_WHEEL_SCROLL;
                        al_Disp_Update_Flag.Insert(index, Constants.DISPLAY_UPDATE_TYPE_RE);
                    }
                    else if (p_cmd_id == Constants.SCRIPT_COMMAND_JOY_L_LEVER
                            || p_cmd_id == Constants.SCRIPT_COMMAND_JOY_R_LEVER)
                    {
                        al_Command_Data1[index] = p_data1;
                        al_Command_Data2[index] = p_data2;
                        al_Command_Size[index] = size;
                        al_Command_Icon_idx[index] = Constants.SCRIPT_ICON_IDX_JOY_LEVER_L_PRESS;
                        al_Disp_Update_Flag.Insert(index, Constants.DISPLAY_UPDATE_TYPE_RE);
                    }
                    else if (p_cmd_id == Constants.SCRIPT_COMMAND_JOY_L_LEVER_CENTER
                            || p_cmd_id == Constants.SCRIPT_COMMAND_JOY_R_LEVER_CENTER)
                    {
                        al_Command_Data1[index] = (byte)0;
                        al_Command_Data2[index] = (byte)0;
                        al_Command_Size[index] = size;
                        al_Command_Icon_idx[index] = Constants.SCRIPT_ICON_IDX_JOY_LEVER_L_RELEASE;
                        al_Disp_Update_Flag.Insert(index, Constants.DISPLAY_UPDATE_TYPE_RE);
                    }
                    else if (p_cmd_id == Constants.SCRIPT_COMMAND_JOY_BUTTON_PRESS)
                    {
                        al_Command_Data1[index] = p_data1;
                        al_Command_Data2[index] = (byte)0;
                        al_Command_Size[index] = size;
                        al_Command_Icon_idx[index] = Constants.SCRIPT_ICON_IDX_JOY_BUTTON_PRESS;
                        al_Disp_Update_Flag.Insert(index, Constants.DISPLAY_UPDATE_TYPE_RE);
                    }
                    else if (p_cmd_id == Constants.SCRIPT_COMMAND_JOY_BUTTON_RELESE)
                    {
                        al_Command_Data1[index] = p_data1;
                        al_Command_Data2[index] = (byte)0;
                        al_Command_Size[index] = size;
                        al_Command_Icon_idx[index] = Constants.SCRIPT_ICON_IDX_JOY_BUTTON_RELEASE;
                        al_Disp_Update_Flag.Insert(index, Constants.DISPLAY_UPDATE_TYPE_RE);
                    }
                    else if (p_cmd_id == Constants.SCRIPT_COMMAND_JOY_HATSW_PRESS)
                    {
                        al_Command_Data1[index] = p_data1;
                        al_Command_Data2[index] = (byte)0;
                        al_Command_Size[index] = size;
                        al_Command_Icon_idx[index] = Constants.SCRIPT_ICON_IDX_JOY_HAT_SW_PRESS;
                        al_Disp_Update_Flag.Insert(index, Constants.DISPLAY_UPDATE_TYPE_RE);
                    }
                    else if (p_cmd_id == Constants.SCRIPT_COMMAND_JOY_HATSW_RELESE)
                    {
                        al_Command_Data1[index] = (byte)0;
                        al_Command_Data2[index] = (byte)0;
                        al_Command_Size[index] = size;
                        al_Command_Icon_idx[index] = Constants.SCRIPT_ICON_IDX_JOY_HAT_SW_RELEASE;
                        al_Disp_Update_Flag.Insert(index, Constants.DISPLAY_UPDATE_TYPE_RE);
                    }
                    else if (p_cmd_id == Constants.SCRIPT_COMMAND_MOUSE_MOVE)
                    {
                        al_Command_Data1[index] = p_data1;
                        al_Command_Data2[index] = p_data2;
                        al_Command_Size.Insert(index, size);
                        al_Command_Icon_idx[index] = Constants.SCRIPT_ICON_IDX_MOUSE_MOVE;
                        al_Disp_Update_Flag.Insert(index, Constants.DISPLAY_UPDATE_TYPE_RE);
                    }
                    else if (p_cmd_id == Constants.SCRIPT_COMMAND_MULTIMEDIA_PRESS)
                    {
                        al_Command_Data1[index] = p_data1;
                        al_Command_Data2[index] = (byte)0;
                        al_Command_Size.Insert(index, size);
                        al_Command_Icon_idx[index] = Constants.SCRIPT_ICON_IDX_MULTIMEDIA_PRESS;
                        al_Disp_Update_Flag.Insert(index, Constants.DISPLAY_UPDATE_TYPE_RE);
                    }
                    else if (p_cmd_id == Constants.SCRIPT_COMMAND_MULTIMEDIA_RELESE)
                    {
                        al_Command_Data1[index] = p_data1;
                        al_Command_Data2[index] = (byte)0;
                        al_Command_Size.Insert(index, size);
                        al_Command_Icon_idx[index] = Constants.SCRIPT_ICON_IDX_MULTIMEDIA_RELEASE;
                        al_Disp_Update_Flag.Insert(index, Constants.DISPLAY_UPDATE_TYPE_RE);
                    }
                }
            }
            internal void del(int index)
            {
                // 先頭のモードは削除しない
                if (0 < index && index <= al_Command_ID.Count)
                {
                    al_Command_ID.RemoveAt(index - 1);
                    al_Command_Data1.RemoveAt(index - 1);
                    al_Command_Data2.RemoveAt(index - 1);
                    al_Command_Size.RemoveAt(index - 1);
                    al_Command_Icon_idx.RemoveAt(index - 1);
                    al_Disp_Update_Flag.RemoveAt(index - 1);
                }
            }
            internal void clear()
            {
                init_data();
            }
            internal void set_mode(byte p_set_mode)
            {
                Script_Mode = p_set_mode;
            }
            internal void disp_update_comp(int p_index)
            {
                if (0 < p_index && p_index <= al_Command_ID.Count)
                {
                    al_Disp_Update_Flag[p_index - 1] = Constants.DISPLAY_UPDATE_TYPE_NONE;
                }
            }
            // 行の入れ替え
            // p_idx1 と p_idx2　の行を入れ替える
            internal void replace(int p_idx1, int p_idx2)
            {
                byte byte_tmp;
                int int_tmp;
                if (0 < p_idx1 && p_idx1 <= al_Command_ID.Count && 0 < p_idx2 && p_idx2 <= al_Command_ID.Count)
                {
                    p_idx1--;
                    p_idx2--;

                    byte_tmp = (byte)al_Command_ID[p_idx1];
                    al_Command_ID[p_idx1] = al_Command_ID[p_idx2];
                    al_Command_ID[p_idx2] = byte_tmp;
                    byte_tmp = (byte)al_Command_Data1[p_idx1];
                    al_Command_Data1[p_idx1] = al_Command_Data1[p_idx2];
                    al_Command_Data1[p_idx2] = byte_tmp;
                    byte_tmp = (byte)al_Command_Data2[p_idx1];
                    al_Command_Data2[p_idx1] = al_Command_Data2[p_idx2];
                    al_Command_Data2[p_idx2] = byte_tmp;

                    int_tmp = (int)al_Command_Size[p_idx1];
                    al_Command_Size[p_idx1] = al_Command_Size[p_idx2];
                    al_Command_Size[p_idx2] = int_tmp;
                    int_tmp = (int)al_Command_Icon_idx[p_idx1];
                    al_Command_Icon_idx[p_idx1] = al_Command_Icon_idx[p_idx2];
                    al_Command_Icon_idx[p_idx2] = int_tmp;
                    int_tmp = (int)al_Disp_Update_Flag[p_idx1];
                    al_Disp_Update_Flag[p_idx1] = al_Disp_Update_Flag[p_idx2];
                    al_Disp_Update_Flag[p_idx2] = int_tmp;
                }
            }
            // 選択行を指定位置に移動する
            // p_data_idx の行のデータを、p_move_idxの位置に移動する
            internal void move(int p_data_idx, int p_move_idx)
            {
                byte byte_tmp;
                int int_tmp;
                if (0 < p_data_idx && p_data_idx <= al_Command_ID.Count && 0 < p_move_idx && p_move_idx <= al_Command_ID.Count)
                {
                    p_data_idx--;
                    p_move_idx--;

                    if (p_data_idx > p_move_idx)
                    {   // 上の位置へ移動
                        // 選択行を１行ずつ上へ移動
                        for (int fi = 0; fi < (p_data_idx - p_move_idx); fi++)
                        {
                            byte_tmp = (byte)al_Command_ID[p_data_idx - 1 - fi];
                            al_Command_ID[p_data_idx - 1 - fi] = al_Command_ID[p_data_idx - fi];
                            al_Command_ID[p_data_idx - fi] = byte_tmp;
                            byte_tmp = (byte)al_Command_Data1[p_data_idx - 1 - fi];
                            al_Command_Data1[p_data_idx - 1 - fi] = al_Command_Data1[p_data_idx - fi];
                            al_Command_Data1[p_data_idx - fi] = byte_tmp;
                            byte_tmp = (byte)al_Command_Data2[p_data_idx - 1 - fi];
                            al_Command_Data2[p_data_idx - 1 - fi] = al_Command_Data2[p_data_idx - fi];
                            al_Command_Data2[p_data_idx - fi] = byte_tmp;

                            int_tmp = (int)al_Command_Size[p_data_idx - 1 - fi];
                            al_Command_Size[p_data_idx - 1 - fi] = al_Command_Size[p_data_idx - fi];
                            al_Command_Size[p_data_idx - fi] = int_tmp;
                            int_tmp = (int)al_Command_Icon_idx[p_data_idx - 1 - fi];
                            al_Command_Icon_idx[p_data_idx - 1 - fi] = al_Command_Icon_idx[p_data_idx - fi];
                            al_Command_Icon_idx[p_data_idx - fi] = int_tmp;
                            int_tmp = (int)al_Disp_Update_Flag[p_data_idx - 1 - fi];
                            al_Disp_Update_Flag[p_data_idx - 1 - fi] = al_Disp_Update_Flag[p_data_idx - fi];
                            al_Disp_Update_Flag[p_data_idx - fi] = int_tmp;
                        }
                    }
                    else if (p_data_idx < p_move_idx)
                    {   // 下の位置へ移動
                        // 選択行を１行ずつ下へ移動
                        for (int fi = 0; fi < (p_move_idx - p_data_idx); fi++)
                        {
                            byte_tmp = (byte)al_Command_ID[p_data_idx + 1 + fi];
                            al_Command_ID[p_data_idx + 1 + fi] = al_Command_ID[p_data_idx + fi];
                            al_Command_ID[p_data_idx + fi] = byte_tmp;
                            byte_tmp = (byte)al_Command_Data1[p_data_idx + 1 + fi];
                            al_Command_Data1[p_data_idx + 1 + fi] = al_Command_Data1[p_data_idx + fi];
                            al_Command_Data1[p_data_idx + fi] = byte_tmp;
                            byte_tmp = (byte)al_Command_Data2[p_data_idx + 1 + fi];
                            al_Command_Data2[p_data_idx + 1 + fi] = al_Command_Data2[p_data_idx + fi];
                            al_Command_Data2[p_data_idx + fi] = byte_tmp;

                            int_tmp = (int)al_Command_Size[p_data_idx + 1 + fi];
                            al_Command_Size[p_data_idx + 1 + fi] = al_Command_Size[p_data_idx + fi];
                            al_Command_Size[p_data_idx + fi] = int_tmp;
                            int_tmp = (int)al_Command_Icon_idx[p_data_idx + 1 + fi];
                            al_Command_Icon_idx[p_data_idx + 1 + fi] = al_Command_Icon_idx[p_data_idx + fi];
                            al_Command_Icon_idx[p_data_idx + fi] = int_tmp;
                            int_tmp = (int)al_Disp_Update_Flag[p_data_idx + 1 + fi];
                            al_Disp_Update_Flag[p_data_idx + 1 + fi] = al_Disp_Update_Flag[p_data_idx + fi];
                            al_Disp_Update_Flag[p_data_idx + fi] = int_tmp;
                        }
                    }
                }
            }
        }

        private void lbl_REC_Btn_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
#if true
                if (my_App_Setting_Data.Script_Add_Manual_Control == Constants.SCRIPT_ADD_MANUAL_NON)
                {
                    if (lbl_REC_Btn.Text == RevOmate.Properties.Resources.MACRO_EDITOR_OPERATION_REC_START)
                    {   // REC Start
                        my_Script_Auto_Recored_Start();
                    }
                    else if (lbl_REC_Btn.Text == RevOmate.Properties.Resources.MACRO_EDITOR_OPERATION_REC_STOP)
                    {   // REC Stop
                        my_Script_Auto_Recored_Stop();
                    }
                }
#endif
#if false
                if (my_App_Setting_Data.Script_Add_Manual_Control == Constants.SCRIPT_ADD_MANUAL_NON)
                {
                    if (Script_Editor_Btn_imageList.Images.Count >= 3)
                    {
                        if (lbl_REC_Btn.ImageIndex == 1)
                        {   // REC Start
                            lbl_REC_Btn.ImageIndex = 3;
                            my_App_Setting_Data.Script_Rec_Flag = true;
                            //this.KeyPreview = true;
                            lbl_REC_Btn.Text = RevOmate.Properties.Resources.MACRO_EDITOR_OPERATION_REC_STOP;

                            sw_Script_Interval.Stop();
                            sw_Script_Interval.Reset();
                            sw_Script_Interval.Start();

                            dgv_ScriptEditor.Focus();
                        }
                        else if (lbl_REC_Btn.ImageIndex == 3)
                        {   // REC Stop
                            lbl_REC_Btn.ImageIndex = 1;
                            my_App_Setting_Data.Script_Rec_Flag = false;
                            //this.KeyPreview = false;
                            lbl_REC_Btn.Text = RevOmate.Properties.Resources.MACRO_EDITOR_OPERATION_REC_START;

                            sw_Script_Interval.Stop();
                        }
                    }
                }
#endif
            }
            catch
            {
            }
        }

        private void lbl_Clear_btn_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                // バーチャルキーボードのプッシュ状態をクリア
                //my_vk.my_Key_Push_Clear();

                my_script_editor_disp_data.clear();

                //vSB_SS_SE.Value = 0;
                //my_App_Setting_Data.Script_Editor_Disp_Idx = 0;
                //my_Set_Script_Editor_Display(my_App_Setting_Data.Script_Editor_Disp_Idx, my_App_Setting_Data.Script_Editor_Select_Idx);


                //dgv_ScriptEditor.Rows.Clear();
                int rowCount = dgv_ScriptEditor.Rows.Count;
                for (int fi = (rowCount-1); 0 < fi; fi--)
                {
                    dgv_ScriptEditor.Rows.RemoveAt(fi);
                }

                txtbx_script_no.Text = "";
                txtbx_script_name.Text = "";

            }
            catch
            {
            }
        }

        private void lbl_Interval_Icon_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (my_App_Setting_Data.Script_Rec_Flag == false)
                {
                    if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                    {
                        my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_INTERVAL;

                        int icon_idx = int.Parse(((Label)sender).Tag.ToString());
                        //my_arrow_icon_disp(icon_idx, true);
                        my_arrow_icon_all_disp(true);
                    }
                    else
                    {
                        my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                        my_App_Setting_Data.Script_Setting_Drag_Target_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                    }
                }
            }
            catch
            {
            }
        }

        private void lbl_KeyRelease_Icon_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (my_App_Setting_Data.Script_Rec_Flag == false)
                {
                    if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                    {
                        my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_KEYUP;

                        int icon_idx = int.Parse(((Label)sender).Tag.ToString());
                        //my_arrow_icon_disp(icon_idx, true);
                        my_arrow_icon_all_disp(true);
                    }
                    else
                    {
                        my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                        my_App_Setting_Data.Script_Setting_Drag_Target_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                    }
                }
            }
            catch
            {
            }
        }

        private void lbl_KeyPress_Icon_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (my_App_Setting_Data.Script_Rec_Flag == false)
                {
                    if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                    {
                        my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_KEYDOWN;

                        int icon_idx = int.Parse(((Label)sender).Tag.ToString());
                        //my_arrow_icon_disp(icon_idx, true);
                        my_arrow_icon_all_disp(true);
                    }
                    else
                    {
                        my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                        my_App_Setting_Data.Script_Setting_Drag_Target_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                    }
                }
            }
            catch
            {
            }
        }

        private void lbl_MouseClick_Icon_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (my_App_Setting_Data.Script_Rec_Flag == false)
                {
                    if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                    {
                        my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_MOUSECLICK;

                        int icon_idx = int.Parse(((Label)sender).Tag.ToString());
                        //my_arrow_icon_disp(icon_idx, true);
                        my_arrow_icon_all_disp(true);
                    }
                    else
                    {
                        my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                        my_App_Setting_Data.Script_Setting_Drag_Target_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                    }
                }
            }
            catch
            {
            }
        }

        private void lbl_MouseRelease_Icon_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (my_App_Setting_Data.Script_Rec_Flag == false)
                {
                    if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                    {
                        my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_MOUSERELEASE;

                        int icon_idx = int.Parse(((Label)sender).Tag.ToString());
                        //my_arrow_icon_disp(icon_idx, true);
                        my_arrow_icon_all_disp(true);
                    }
                    else
                    {
                        my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                        my_App_Setting_Data.Script_Setting_Drag_Target_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                    }
                }
            }
            catch
            {
            }
        }

        private void lbl_WheelScroll_Icon_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (my_App_Setting_Data.Script_Rec_Flag == false)
                {
                    if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                    {
                        my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_MOUSEWHEEL;

                        int icon_idx = int.Parse(((Label)sender).Tag.ToString());
                        //my_arrow_icon_disp(icon_idx, true);
                        my_arrow_icon_all_disp(true);
                    }
                    else
                    {
                        my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                        my_App_Setting_Data.Script_Setting_Drag_Target_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                    }
                }
            }
            catch
            {
            }
        }

        private void lbl_JoyLeftLeverPress_Icon_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (my_App_Setting_Data.Script_Rec_Flag == false)
                {
                    if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                    {
                        my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_JOYLLEVERDOWN;

                        int icon_idx = int.Parse(((Label)sender).Tag.ToString());
                        //my_arrow_icon_disp(icon_idx, true);
                        my_arrow_icon_all_disp(true);
                    }
                    else
                    {
                        my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                        my_App_Setting_Data.Script_Setting_Drag_Target_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                    }
                }
            }
            catch
            {
            }
        }

        private void lbl_JoyLeftLeverRelease_Icon_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (my_App_Setting_Data.Script_Rec_Flag == false)
                {
                    if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                    {
                        my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_JOYLLEVERUP;

                        int icon_idx = int.Parse(((Label)sender).Tag.ToString());
                        //my_arrow_icon_disp(icon_idx, true);
                        my_arrow_icon_all_disp(true);
                    }
                    else
                    {
                        my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                        my_App_Setting_Data.Script_Setting_Drag_Target_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                    }
                }
            }
            catch
            {
            }
        }

        private void lbl_JoyRightLeverPress_Icon_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (my_App_Setting_Data.Script_Rec_Flag == false)
                {
                    if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                    {
                        my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_JOYRLEVERDOWN;

                        int icon_idx = int.Parse(((Label)sender).Tag.ToString());
                        //my_arrow_icon_disp(icon_idx, true);
                        my_arrow_icon_all_disp(true);
                    }
                    else
                    {
                        my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                        my_App_Setting_Data.Script_Setting_Drag_Target_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                    }
                }
            }
            catch
            {
            }
        }

        private void lbl_JoyRightLeverRelease_Icon_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (my_App_Setting_Data.Script_Rec_Flag == false)
                {
                    if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                    {
                        my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_JOYRLEVERUP;

                        int icon_idx = int.Parse(((Label)sender).Tag.ToString());
                        //my_arrow_icon_disp(icon_idx, true);
                        my_arrow_icon_all_disp(true);
                    }
                    else
                    {
                        my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                        my_App_Setting_Data.Script_Setting_Drag_Target_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                    }
                }
            }
            catch
            {
            }
        }

        private void lbl_JoyHatSWPress_Icon_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (my_App_Setting_Data.Script_Rec_Flag == false)
                {
                    if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                    {
                        my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_JOYHATSWDOWN;

                        int icon_idx = int.Parse(((Label)sender).Tag.ToString());
                        //my_arrow_icon_disp(icon_idx, true);
                        my_arrow_icon_all_disp(true);
                    }
                    else
                    {
                        my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                        my_App_Setting_Data.Script_Setting_Drag_Target_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                    }
                }
            }
            catch
            {
            }
        }

        private void lbl_JoyHatSWRelease_Icon_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (my_App_Setting_Data.Script_Rec_Flag == false)
                {
                    if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                    {
                        my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_JOYHATSWUP;

                        int icon_idx = int.Parse(((Label)sender).Tag.ToString());
                        //my_arrow_icon_disp(icon_idx, true);
                        my_arrow_icon_all_disp(true);
                    }
                    else
                    {
                        my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                        my_App_Setting_Data.Script_Setting_Drag_Target_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                    }
                }
            }
            catch
            {
            }
        }

        private void lbl_JoyButtonPress_Icon_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (my_App_Setting_Data.Script_Rec_Flag == false)
                {
                    if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                    {
                        my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_JOYBUTTONDOWN;

                        int icon_idx = int.Parse(((Label)sender).Tag.ToString());
                        //my_arrow_icon_disp(icon_idx, true);
                        my_arrow_icon_all_disp(true);
                    }
                    else
                    {
                        my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                        my_App_Setting_Data.Script_Setting_Drag_Target_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                    }
                }
            }
            catch
            {
            }
        }

        private void lbl_JoyButtonRelease_Icon_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (my_App_Setting_Data.Script_Rec_Flag == false)
                {
                    if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                    {
                        my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_JOYBUTTONUP;

                        int icon_idx = int.Parse(((Label)sender).Tag.ToString());
                        //my_arrow_icon_disp(icon_idx, true);
                        my_arrow_icon_all_disp(true);
                    }
                    else
                    {
                        my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                        my_App_Setting_Data.Script_Setting_Drag_Target_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                    }
                }
            }
            catch
            {
            }
        }

        private void lbl_MouseMovePress_Icon_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (my_App_Setting_Data.Script_Rec_Flag == false)
                {
                    if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                    {
                        my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_MOUSEMOVE;

                        int icon_idx = int.Parse(((Label)sender).Tag.ToString());
                        //my_arrow_icon_disp(icon_idx, true);
                        my_arrow_icon_all_disp(true);
                    }
                    else
                    {
                        my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                        my_App_Setting_Data.Script_Setting_Drag_Target_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                    }
                }
            }
            catch
            {
            }
        }

        private void lbl_MultiMediaPress_Icon_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (my_App_Setting_Data.Script_Rec_Flag == false)
                {
                    if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                    {
                        my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_MLTIMEDIADOWN;

                        int icon_idx = int.Parse(((Label)sender).Tag.ToString());
                        //my_arrow_icon_disp(icon_idx, true);
                        my_arrow_icon_all_disp(true);
                    }
                    else
                    {
                        my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                        my_App_Setting_Data.Script_Setting_Drag_Target_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                    }
                }
            }
            catch
            {
            }
        }

        private void lbl_MultiMediaRelease_Icon_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (my_App_Setting_Data.Script_Rec_Flag == false)
                {
                    if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                    {
                        my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_MLTIMEDIAUP;

                        int icon_idx = int.Parse(((Label)sender).Tag.ToString());
                        //my_arrow_icon_disp(icon_idx, true);
                        my_arrow_icon_all_disp(true);
                    }
                    else
                    {
                        my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                        my_App_Setting_Data.Script_Setting_Drag_Target_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                    }
                }
            }
            catch
            {
            }
        }

        private void lbl_Script_Icon_DragOver(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = DragDropEffects.Move;
            }
            catch
            {
            }
        }

        private void lbl_Script_Icon_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                my_App_Setting_Data.Script_Setting_Drag_Target_Control = Constants.SCRIPT_DRAG_CTRL_NON;
            }
            catch
            {
            }
        }

        private void lbl_Script_Icon_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (my_App_Setting_Data.Script_Rec_Flag == false)
                {
                    if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                    {
                        int idx = int.Parse(((System.Windows.Forms.Label)sender).Tag.ToString());
                        if (0 <= idx && idx < lbl_Script_Icons.Length)
                        {
                            DragDropEffects dropEffect = lbl_Script_Icons[idx].DoDragDrop(lbl_Script_Icons[idx].Tag, DragDropEffects.Move);
                        }

                        lbl_Arrow_Dust.Visible = false;
                        //lbl_Arrow_Icon1.Visible = false;
                        int icon_idx = int.Parse(((Label)sender).Tag.ToString());
                        //my_arrow_icon_disp(icon_idx, false);
                        my_arrow_icon_all_disp(false);
                    }
                }
            }
            catch
            {
            }
        }


        private Rectangle dragBoxFromMouseDown;
        private int indexOfItemUnderMouseToDrag;
        private int indexOfItemUnderMouseToDrop;

        private void dgv_ScriptEditor_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                {
                    lbl_Arrow_Dust.Visible = true;
                    // If the mouse moves outside the rectangle, start the drag.
                    if (dragBoxFromMouseDown != Rectangle.Empty &&
                    !dragBoxFromMouseDown.Contains(e.X, e.Y))
                    {
                        // Proceed with the drag and drop, passing in the list item.                   
                        DragDropEffects dropEffect = dgv_ScriptEditor.DoDragDrop(
                        dgv_ScriptEditor.Rows[indexOfItemUnderMouseToDrag],
                        DragDropEffects.Move);
                    }
                    lbl_Arrow_Dust.Visible = false;
                }
            }
            catch
            {
            }
        }

        private void dgv_ScriptEditor_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {


                if (my_App_Setting_Data.Script_Rec_Flag == true)
                {   // スクリプト記録中
                    my_Form_MouseDown(e.Button, 0, -1);
                }
                else if (my_App_Setting_Data.Script_Add_Manual_Control == Constants.SCRIPT_ADD_MANUAL_MOUSECLICK
                    || my_App_Setting_Data.Script_Add_Manual_Control == Constants.SCRIPT_ADD_MANUAL_MOUSERELEASE)
                {   // 手動追加待機中
                    my_Form_MouseDown(e.Button, 0, -1);
                }
                else
                {
                    if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                    {
                        my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_EDIT;

                        //lbl_Arrow_Dust.Visible = true;
                        //lbl_Arrow_MM_SE.Visible = true;
                        //lbl_Arrow_SE_SL.Visible = true;



                        // Get the index of the item the mouse is below.
                        indexOfItemUnderMouseToDrag = dgv_ScriptEditor.HitTest(e.X, e.Y).RowIndex;
                        if (indexOfItemUnderMouseToDrag != -1)
                        {
                            // Remember the point where the mouse down occurred. The DragSize indicates
                            // the size that the mouse can move before a drag event should be started.                
                            Size dragSize = SystemInformation.DragSize;

                            // Create a rectangle using the DragSize, with the mouse position being
                            // at the center of the rectangle.
                            dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2),
                                                                           e.Y - (dragSize.Height / 2)), dragSize);

                        }
                        else
                        {
                            // Reset the rectangle if the mouse is not over an item in the ListBox.
                            dragBoxFromMouseDown = Rectangle.Empty;
                        }
                    }
                    else
                    {
                        my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                        my_App_Setting_Data.Script_Setting_Drag_Target_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                    }
                }
            }
            catch
            {
            }
        }
        private void dgv_ScriptEditor_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (my_App_Setting_Data.Script_Rec_Flag == true)
                {   // スクリプト記録中
                    my_Form_MouseUp(e.Button, 0, -1);
                }
                lbl_Arrow_Dust.Visible = false;
            }
            catch
            {
            }
        }

        private void dgv_ScriptEditor_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void dgv_ScriptEditor_DragDrop(object sender, DragEventArgs e)
        {
            //int ret = 0;
            try
            {
                // The mouse locations are relative to the screen, so they must be
                // converted to client coordinates.
                Point clientPoint = dgv_ScriptEditor.PointToClient(new Point(e.X, e.Y));
                // Get the row index of the item the mouse is below.
                indexOfItemUnderMouseToDrop = dgv_ScriptEditor.HitTest(clientPoint.X, clientPoint.Y).RowIndex;
                // If the drag operation was a move then remove and insert the row.
                if (e.Effect == DragDropEffects.Move)
                {
                    if (e.Data.GetDataPresent(DataFormats.StringFormat))
                    {
                        // マクロ項目追加中？
                        if (my_App_Setting_Data.Script_Add_Manual_Control == Constants.SCRIPT_ADD_MANUAL_NON)
                        {
                            ////ドロップされたすべてのファイル名を取得する
                            //Label tmp_lbl = (Label)(sender);
                            string s_tagNo = (string)e.Data.GetData(typeof(string));
                            int tagNo = int.Parse(s_tagNo);

                            switch (tagNo)
                            {
                                case Constants.SCRIPT_ADD_MANUAL_INTERVAL:
                                    // Interval Icon の Drag&Drop
                                    my_script_editor_disp_data.add(indexOfItemUnderMouseToDrop, Constants.SCRIPT_COMMAND_INTERVAL, 0x00, 0x01, 3, sw_Script_Interval, false);
                                    FlashReadFirstTime = true;
                                    break;
                                case Constants.SCRIPT_ADD_MANUAL_KEYDOWN:
                                    // Key Down の Drag&Drop
                                    my_App_Setting_Data.Script_Drag_Target_idx = indexOfItemUnderMouseToDrop;
                                    my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_KEYDOWN;
                                    lbl_Script_Add_Info.Text = RevOmate.Properties.Resources.SCRIPT_ADD_INFO_KEY_TEXT;
                                    gbx_Script_Add_Info.Visible = true;
                                    break;
                                case Constants.SCRIPT_ADD_MANUAL_KEYUP:
                                    // Key Up の Drag&Drop
                                    my_App_Setting_Data.Script_Drag_Target_idx = indexOfItemUnderMouseToDrop;
                                    my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_KEYUP;
                                    lbl_Script_Add_Info.Text = RevOmate.Properties.Resources.SCRIPT_ADD_INFO_KEY_TEXT;
                                    gbx_Script_Add_Info.Visible = true;
                                    break;
                                case Constants.SCRIPT_ADD_MANUAL_MOUSECLICK:
                                    // Mouse Click の Drag&Drop
                                    my_App_Setting_Data.Script_Drag_Target_idx = indexOfItemUnderMouseToDrop;
                                    my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_MOUSECLICK;
                                    lbl_Script_Add_Info_Mouse_Msg.Text = RevOmate.Properties.Resources.SCRIPT_ADD_INFO_MOUSE_BUTTON_TEXT;
                                    my_script_add_info_mouse_button(true, Constants.SCRIPT_COMMAND_L_CLICK);
                                    //my_App_Setting_Data.Script_Drag_Target_idx = indexOfItemUnderMouseToDrop;
                                    //my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_MOUSECLICK;
                                    //lbl_Script_Add_Info.Text = Constants.SCRIPT_ADD_INFO_MOUSE_TEXT;
                                    //gbx_Script_Add_Info.Visible = true;
                                    break;
                                case Constants.SCRIPT_ADD_MANUAL_MOUSERELEASE:
                                    // Mouse Click Release の Drag&Drop
                                    my_App_Setting_Data.Script_Drag_Target_idx = indexOfItemUnderMouseToDrop;
                                    my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_MOUSERELEASE;
                                    lbl_Script_Add_Info_Mouse_Msg.Text = RevOmate.Properties.Resources.SCRIPT_ADD_INFO_MOUSE_BUTTON_TEXT;
                                    my_script_add_info_mouse_button(true, Constants.SCRIPT_COMMAND_L_RELEASE);
                                    //my_App_Setting_Data.Script_Drag_Target_idx = indexOfItemUnderMouseToDrop;
                                    //my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_MOUSERELEASE;
                                    //lbl_Script_Add_Info.Text = Constants.SCRIPT_ADD_INFO_MOUSE_TEXT;
                                    //gbx_Script_Add_Info.Visible = true;
                                    break;
                                case Constants.SCRIPT_ADD_MANUAL_MOUSEWHEEL:
                                    // Mouse Wheel の Drag&Drop
                                    my_App_Setting_Data.Script_Drag_Target_idx = indexOfItemUnderMouseToDrop;
                                    my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_MOUSEWHEEL;
                                    lbl_Script_Add_Info.Text = RevOmate.Properties.Resources.SCRIPT_ADD_INFO_WHEEL_TEXT;
                                    gbx_Script_Add_Info.Visible = true;
                                    break;
                                case Constants.SCRIPT_ADD_MANUAL_JOYLLEVERDOWN:
                                    // Joystick L Lever Press Icon の Drag&Drop
                                    my_script_editor_disp_data.add(indexOfItemUnderMouseToDrop, Constants.SCRIPT_COMMAND_JOY_L_LEVER, 0x00, 0x00, 3, sw_Script_Interval, false);
                                    FlashReadFirstTime = true;
                                    break;
                                case Constants.SCRIPT_ADD_MANUAL_JOYLLEVERUP:
                                    // Joystick L Lever Release Icon の Drag&Drop
                                    my_script_editor_disp_data.add(indexOfItemUnderMouseToDrop, Constants.SCRIPT_COMMAND_JOY_L_LEVER_CENTER, 0x00, 0x00, 1, sw_Script_Interval, false);
                                    FlashReadFirstTime = true;
                                    break;
                                case Constants.SCRIPT_ADD_MANUAL_JOYRLEVERDOWN:
                                    // Joystick R Lever Press Icon の Drag&Drop
                                    my_script_editor_disp_data.add(indexOfItemUnderMouseToDrop, Constants.SCRIPT_COMMAND_JOY_R_LEVER, 0x00, 0x00, 3, sw_Script_Interval, false);
                                    FlashReadFirstTime = true;
                                    break;
                                case Constants.SCRIPT_ADD_MANUAL_JOYRLEVERUP:
                                    // Joystick R Lever Release Icon の Drag&Drop
                                    my_script_editor_disp_data.add(indexOfItemUnderMouseToDrop, Constants.SCRIPT_COMMAND_JOY_R_LEVER_CENTER, 0x00, 0x00, 1, sw_Script_Interval, false);
                                    FlashReadFirstTime = true;
                                    break;
                                case Constants.SCRIPT_ADD_MANUAL_JOYHATSWDOWN:
                                    // Joystick Hat SW Press Icon の Drag&Drop
                                    my_App_Setting_Data.Script_Drag_Target_idx = indexOfItemUnderMouseToDrop;
                                    my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_JOYHATSWDOWN;
                                    lbl_Script_Add_Info.Text = RevOmate.Properties.Resources.SCRIPT_ADD_INFO_JOY_HATSW_TEXT;
                                    gbx_Script_Add_Info.Visible = true;
                                    break;
                                case Constants.SCRIPT_ADD_MANUAL_JOYHATSWUP:
                                    // Joystick Hat SW Release Icon の Drag&Drop
                                    my_script_editor_disp_data.add(indexOfItemUnderMouseToDrop, Constants.SCRIPT_COMMAND_JOY_HATSW_RELESE, 0x00, 0x00, 1, sw_Script_Interval, false);
                                    FlashReadFirstTime = true;
                                    break;
                                case Constants.SCRIPT_ADD_MANUAL_JOYBUTTONDOWN:
                                    // Joystick Button Press Icon の Drag&Drop
                                    my_App_Setting_Data.Script_Drag_Target_idx = indexOfItemUnderMouseToDrop;
                                    my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_JOYBUTTONDOWN;
                                    lbl_Script_Add_Info_JoysticButton_Msg.Text = RevOmate.Properties.Resources.SCRIPT_ADD_INFO_JOY_BUTTON_TEXT;
                                    my_script_add_info_joystick_button(true, 0);
                                    break;
                                case Constants.SCRIPT_ADD_MANUAL_JOYBUTTONUP:
                                    // Joystick Button Release Icon の Drag&Drop
                                    my_App_Setting_Data.Script_Drag_Target_idx = indexOfItemUnderMouseToDrop;
                                    my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_JOYBUTTONUP;
                                    lbl_Script_Add_Info_JoysticButton_Msg.Text = RevOmate.Properties.Resources.SCRIPT_ADD_INFO_JOY_BUTTON_TEXT;
                                    my_script_add_info_joystick_button(true, 0);
                                    break;
                                case Constants.SCRIPT_ADD_MANUAL_MOUSEMOVE:
                                    // Mouse Move Icon の Drag&Drop
                                    my_script_editor_disp_data.add(indexOfItemUnderMouseToDrop, Constants.SCRIPT_COMMAND_MOUSE_MOVE, 0x00, 0x00, 3, sw_Script_Interval, false);
                                    FlashReadFirstTime = true;
                                    break;
                                case Constants.SCRIPT_ADD_MANUAL_MULTIMEDIADOWN:
                                    // Multi Media Key Press Icon の Drag&Drop
                                    my_App_Setting_Data.Script_Drag_Target_idx = indexOfItemUnderMouseToDrop;
                                    my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_MULTIMEDIADOWN;
                                    lbl_Script_Add_Info_JoysticButton_Msg.Text = RevOmate.Properties.Resources.SCRIPT_ADD_INFO_MM_BUTTON_TEXT;
                                    my_script_add_info_multimedia_key(true, 0);
                                    break;
                                case Constants.SCRIPT_ADD_MANUAL_MULTIMEDIAUP:
                                    // Multi Media Key Release Icon の Drag&Drop
                                    my_App_Setting_Data.Script_Drag_Target_idx = indexOfItemUnderMouseToDrop;
                                    my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_MULTIMEDIAUP;
                                    lbl_Script_Add_Info_JoysticButton_Msg.Text = RevOmate.Properties.Resources.SCRIPT_ADD_INFO_MM_BUTTON_TEXT;
                                    my_script_add_info_multimedia_key(true, 0);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else if (indexOfItemUnderMouseToDrop != -1)
                    {
                        if (indexOfItemUnderMouseToDrag > 0 && indexOfItemUnderMouseToDrop > 0)
                        {
                            DataGridViewRow rowArray = dgv_ScriptEditor.Rows[indexOfItemUnderMouseToDrag];
                            dgv_ScriptEditor.Rows.RemoveAt(indexOfItemUnderMouseToDrag);
                            dgv_ScriptEditor.Rows.InsertRange(indexOfItemUnderMouseToDrop, rowArray);

                            my_script_editor_disp_data.move(indexOfItemUnderMouseToDrag, indexOfItemUnderMouseToDrop);
                            //my_script_editor_disp_data.replace(indexOfItemUnderMouseToDrag, indexOfItemUnderMouseToDrop);
                        }
                    }

                    lbl_Arrow_Dust.Visible = false;
                }
            }
            catch
            {
            }
        }

        private void dgv_ScriptEditor_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (0 < indexOfItemUnderMouseToDrag && indexOfItemUnderMouseToDrag < dgv_ScriptEditor.Rows.Count
                    && my_App_Setting_Data.Script_Add_Manual_Control == Constants.SCRIPT_ADD_MANUAL_NON
                    && my_App_Setting_Data.Script_Rec_Flag == false)
                {
                    byte cmd_id = (byte)my_script_editor_disp_data.al_Command_ID[indexOfItemUnderMouseToDrag-1];
                    if (cmd_id == Constants.SCRIPT_COMMAND_INTERVAL)
                    {
                        int tmp_interval = (int)(byte)my_script_editor_disp_data.al_Command_Data1[indexOfItemUnderMouseToDrag - 1];
                        tmp_interval = (tmp_interval << 8) | (int)(byte)my_script_editor_disp_data.al_Command_Data2[indexOfItemUnderMouseToDrag - 1];
                        txtbx_Interval_Inputbox.Text = tmp_interval.ToString();
                        txtbx_Interval_Inputbox.Visible = true;
                        txtbx_Interval_Inputbox.Focus();
                        txtbx_Interval_Inputbox.SelectAll();
                        lbl_Script_Add_Info.Text = RevOmate.Properties.Resources.SCRIPT_ADD_INFO_INTERVAL_TEXT;
                        gbx_Script_Add_Info.Visible = true;
                        my_App_Setting_Data.Script_Drag_Target_idx = indexOfItemUnderMouseToDrag;
                        my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_INTERVAL;
                        txtbx_Interval_Inputbox.Focus();
                    }
                    else if (cmd_id == Constants.SCRIPT_COMMAND_KEY_PRESS)
                    {
                        my_App_Setting_Data.Script_Drag_Target_idx = indexOfItemUnderMouseToDrag;
                        my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_KEYDOWN;
                        my_App_Setting_Data.Script_Edit_Item_Change_Flag = true;
                        lbl_Script_Add_Info.Text = RevOmate.Properties.Resources.SCRIPT_ADD_INFO_KEY_TEXT;
                        gbx_Script_Add_Info.Visible = true;
                    }
                    else if (cmd_id == Constants.SCRIPT_COMMAND_KEY_RELEASE)
                    {
                        my_App_Setting_Data.Script_Drag_Target_idx = indexOfItemUnderMouseToDrag;
                        my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_KEYUP;
                        my_App_Setting_Data.Script_Edit_Item_Change_Flag = true;
                        lbl_Script_Add_Info.Text = RevOmate.Properties.Resources.SCRIPT_ADD_INFO_KEY_TEXT;
                        gbx_Script_Add_Info.Visible = true;
                    }
                    else if (cmd_id == Constants.SCRIPT_COMMAND_L_CLICK
                        || cmd_id == Constants.SCRIPT_COMMAND_R_CLICK
                        || cmd_id == Constants.SCRIPT_COMMAND_W_CLICK
                        || cmd_id == Constants.SCRIPT_COMMAND_B4_CLICK
                        || cmd_id == Constants.SCRIPT_COMMAND_B5_CLICK)
                    {
                        my_App_Setting_Data.Script_Drag_Target_idx = indexOfItemUnderMouseToDrag;
                        my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_MOUSECLICK;
                        my_App_Setting_Data.Script_Edit_Item_Change_Flag = true;
                        my_script_add_info_mouse_button(true, cmd_id);
                        //my_App_Setting_Data.Script_Drag_Target_idx = indexOfItemUnderMouseToDrag;
                        //my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_MOUSECLICK;
                        //my_App_Setting_Data.Script_Edit_Item_Change_Flag = true;
                        //lbl_Script_Add_Info.Text = Constants.SCRIPT_ADD_INFO_MOUSE_TEXT;
                        //gbx_Script_Add_Info.Visible = true;
                    }
                    else if (cmd_id == Constants.SCRIPT_COMMAND_L_RELEASE
                        || cmd_id == Constants.SCRIPT_COMMAND_R_RELEASE
                        || cmd_id == Constants.SCRIPT_COMMAND_W_RELEASE
                        || cmd_id == Constants.SCRIPT_COMMAND_B4_RELEASE
                        || cmd_id == Constants.SCRIPT_COMMAND_B5_RELEASE)
                    {
                        my_App_Setting_Data.Script_Drag_Target_idx = indexOfItemUnderMouseToDrag;
                        my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_MOUSERELEASE;
                        my_App_Setting_Data.Script_Edit_Item_Change_Flag = true;
                        my_script_add_info_mouse_button(true, cmd_id);
                        //my_App_Setting_Data.Script_Drag_Target_idx = indexOfItemUnderMouseToDrag;
                        //my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_MOUSERELEASE;
                        //my_App_Setting_Data.Script_Edit_Item_Change_Flag = true;
                        //lbl_Script_Add_Info.Text = Constants.SCRIPT_ADD_INFO_MOUSE_TEXT;
                        //gbx_Script_Add_Info.Visible = true;
                    }
                    else if (cmd_id == Constants.SCRIPT_COMMAND_WHEEL_UP
                        || cmd_id == Constants.SCRIPT_COMMAND_WHEEL_DOWN)
                    {
                        my_App_Setting_Data.Script_Drag_Target_idx = indexOfItemUnderMouseToDrag;
                        my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_MOUSEWHEEL;
                        my_App_Setting_Data.Script_Edit_Item_Change_Flag = true;
                        lbl_Script_Add_Info.Text = RevOmate.Properties.Resources.SCRIPT_ADD_INFO_WHEEL_TEXT;
                        gbx_Script_Add_Info.Visible = true;
                    }
                    else if (cmd_id == Constants.SCRIPT_COMMAND_JOY_BUTTON_PRESS)
                    {
                        my_App_Setting_Data.Script_Drag_Target_idx = indexOfItemUnderMouseToDrag;
                        my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_JOYBUTTONDOWN;
                        my_App_Setting_Data.Script_Edit_Item_Change_Flag = true;
                        my_script_add_info_joystick_button(true, (byte)my_script_editor_disp_data.al_Command_Data1[indexOfItemUnderMouseToDrag - 1]);
                    }
                    else if (cmd_id == Constants.SCRIPT_COMMAND_JOY_BUTTON_RELESE)
                    {
                        my_App_Setting_Data.Script_Drag_Target_idx = indexOfItemUnderMouseToDrag;
                        my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_JOYBUTTONUP;
                        my_App_Setting_Data.Script_Edit_Item_Change_Flag = true;
                        my_script_add_info_joystick_button(true, (byte)my_script_editor_disp_data.al_Command_Data1[indexOfItemUnderMouseToDrag - 1]);
                    }
                    else if (cmd_id == Constants.SCRIPT_COMMAND_JOY_HATSW_PRESS)
                    {
                        my_App_Setting_Data.Script_Drag_Target_idx = indexOfItemUnderMouseToDrag;
                        my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_JOYHATSWDOWN;
                        my_App_Setting_Data.Script_Edit_Item_Change_Flag = true;
                        lbl_Script_Add_Info.Text = RevOmate.Properties.Resources.SCRIPT_ADD_INFO_JOY_HATSW_TEXT;
                        gbx_Script_Add_Info.Visible = true;
                    }
                    else if (cmd_id == Constants.SCRIPT_COMMAND_JOY_HATSW_RELESE)
                    {
                        // Releaseは編集不可
                        //my_App_Setting_Data.Script_Drag_Target_idx = indexOfItemUnderMouseToDrag;
                        //my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_JOYHATSWUP;
                        //my_App_Setting_Data.Script_Edit_Item_Change_Flag = true;
                        //lbl_Script_Add_Info.Text = Constants.SCRIPT_ADD_INFO_JOY_HATSW_TEXT;
                        //gbx_Script_Add_Info.Visible = true;
                    }
                    else if (cmd_id == Constants.SCRIPT_COMMAND_JOY_L_LEVER)
                    {
                        my_App_Setting_Data.Script_Drag_Target_idx = indexOfItemUnderMouseToDrag;
                        my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_JOYLLEVERDOWN;
                        my_App_Setting_Data.Script_Edit_Item_Change_Flag = true;
                        my_script_add_info_joystick_lever(true, (byte)my_script_editor_disp_data.al_Command_Data1[indexOfItemUnderMouseToDrag - 1], (byte)my_script_editor_disp_data.al_Command_Data2[indexOfItemUnderMouseToDrag - 1]);
                    }
                    else if (cmd_id == Constants.SCRIPT_COMMAND_JOY_R_LEVER)
                    {
                        my_App_Setting_Data.Script_Drag_Target_idx = indexOfItemUnderMouseToDrag;
                        my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_JOYRLEVERDOWN;
                        my_App_Setting_Data.Script_Edit_Item_Change_Flag = true;
                        my_script_add_info_joystick_lever(true, (byte)my_script_editor_disp_data.al_Command_Data1[indexOfItemUnderMouseToDrag - 1], (byte)my_script_editor_disp_data.al_Command_Data2[indexOfItemUnderMouseToDrag - 1]);
                    }
                    else if (cmd_id == Constants.SCRIPT_COMMAND_MOUSE_MOVE)
                    {
                        my_App_Setting_Data.Script_Drag_Target_idx = indexOfItemUnderMouseToDrag;
                        my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_MOUSEMOVE;
                        my_App_Setting_Data.Script_Edit_Item_Change_Flag = true;
                        my_script_add_info_mouse_move(true, (byte)my_script_editor_disp_data.al_Command_Data1[indexOfItemUnderMouseToDrag - 1], (byte)my_script_editor_disp_data.al_Command_Data2[indexOfItemUnderMouseToDrag - 1]);
                    }
                    else if (cmd_id == Constants.SCRIPT_COMMAND_MULTIMEDIA_PRESS)
                    {
                        my_App_Setting_Data.Script_Drag_Target_idx = indexOfItemUnderMouseToDrag;
                        my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_MULTIMEDIADOWN;
                        my_App_Setting_Data.Script_Edit_Item_Change_Flag = true;
                        byte set_mm_idx = 0;
                        for(byte fi = 0; fi < multimedia_set_type.Length; fi++)
                        {
                            if(multimedia_set_type[fi] == (byte)my_script_editor_disp_data.al_Command_Data1[indexOfItemUnderMouseToDrag - 1])
                            {
                                set_mm_idx = fi;
                                break;
                            }
                        }
                        my_script_add_info_multimedia_key(true, set_mm_idx);
                    }
                    else if (cmd_id == Constants.SCRIPT_COMMAND_MULTIMEDIA_RELESE)
                    {
                        my_App_Setting_Data.Script_Drag_Target_idx = indexOfItemUnderMouseToDrag;
                        my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_MULTIMEDIAUP;
                        my_App_Setting_Data.Script_Edit_Item_Change_Flag = true;
                        byte set_mm_idx = 0;
                        for (byte fi = 0; fi < multimedia_set_type.Length; fi++)
                        {
                            if (multimedia_set_type[fi] == (byte)my_script_editor_disp_data.al_Command_Data1[indexOfItemUnderMouseToDrag - 1])
                            {
                                set_mm_idx = fi;
                                break;
                            }
                        }
                        my_script_add_info_multimedia_key(true, set_mm_idx);
                    }
                }
                else if (indexOfItemUnderMouseToDrag == 0)
                {   // モードをWクリック
                    my_script_editor_disp_data.Script_Mode++;
                    if (my_script_editor_disp_data.Script_Mode >= Constants.SCRIPT_MODE_NUM)
                    {
                        my_script_editor_disp_data.Script_Mode = 0;
                    }

                    FlashReadFirstTime = true;
                }
            }
            catch
            {
            }
        }

        private void lbl_Dustbox_Icon_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Effect == DragDropEffects.Move)
                {
                    if (my_App_Setting_Data.Script_Setting_Drag_Start_Control == Constants.SCRIPT_DRAG_CTRL_EDIT)
                    {   // SCRIPT EDITORからのDrag&Dropは、コマンド削除
                        if (indexOfItemUnderMouseToDrag > 0)
                        {
                            my_script_editor_disp_data.del(indexOfItemUnderMouseToDrag);
                            dgv_ScriptEditor.Rows.RemoveAt(indexOfItemUnderMouseToDrag);
                        }
                    }
                    lbl_Arrow_Dust.Visible = false;
                }
            }
            catch
            {
            }
        }

        private void lbl_Dustbox_Icon_DragOver(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = DragDropEffects.Move;
                if (((System.Windows.Forms.Label)sender).ImageList != null)
                {
                    ((System.Windows.Forms.Label)sender).ImageIndex = 1;
                }
            }
            catch
            {
            }
        }

        private void lbl_Dustbox_Icon_DragLeave(object sender, EventArgs e)
        {
            try
            {
                if (((System.Windows.Forms.Label)sender).ImageList != null)
                {
                    ((System.Windows.Forms.Label)sender).ImageIndex = 0;
                }
            }
            catch
            {
            }
        }


        private void my_Set_Script_Editor_Display()
        {
            bool DataGridView_AddFlag = false;
            int disp_size = 0; ;
            string disp_itemname = "";
            string disp_val = "";
            byte icon_idx = 0;
            try
            {

                // モード表示
                if (dgv_ScriptEditor.Rows.Count >= 0)
                {   // モードを表示する

                    switch (my_script_editor_disp_data.Script_Mode)
                    {
                        case Constants.SCRIPT_MODE_ONE_MODE:
                            disp_itemname = RevOmate.Properties.Resources.SCRIPT_MODE_ONE_MODE_TEXT;
                            icon_idx = Constants.DISPLAY_ICON_TYPE_1TIME;
                            break;
                        case Constants.SCRIPT_MODE_LOOP_MODE:
                            disp_itemname = RevOmate.Properties.Resources.SCRIPT_MODE_LOOP_MODE_TEXT;
                            icon_idx = Constants.DISPLAY_ICON_TYPE_LOOP;
                            break;
                        case Constants.SCRIPT_MODE_FIRE_MODE:
                            disp_itemname = RevOmate.Properties.Resources.SCRIPT_MODE_FIRE_MODE_TEXT;
                            icon_idx = Constants.DISPLAY_ICON_TYPE_FIRE;
                            break;
                        case Constants.SCRIPT_MODE_HOLD_MODE:
                            disp_itemname = RevOmate.Properties.Resources.SCRIPT_MODE_HOLD_MODE_TEXT;
                            icon_idx = Constants.DISPLAY_ICON_TYPE_HOLD;
                            break;
                        default:
                            disp_itemname = RevOmate.Properties.Resources.SCRIPT_MODE_ONE_MODE_TEXT;
                            icon_idx = Constants.DISPLAY_ICON_TYPE_1TIME;
                            break;
                    }

                    if (dgv_ScriptEditor.Rows.Count == 0)
                    {   // モード追加
                        int colCount = dgv_ScriptEditor.ColumnCount;
                        string[] row = new string[colCount];
                        for (int col_idx = 0; col_idx < colCount; col_idx++)
                        {
                            if (dgv_ScriptEditor.Columns[col_idx].Name == "E_SIZE")
                            {
                                row[col_idx] = "0";
                            }
                            //else if (dgv_ScriptEditor.Columns[col_idx].Name == "E_ICON")
                            //{
                            //}
                            else if (dgv_ScriptEditor.Columns[col_idx].Name == "E_NAME")
                            {
                                row[col_idx] = disp_itemname;
                            }
                            else if (dgv_ScriptEditor.Columns[col_idx].Name == "E_VALUE")
                            {
                                row[col_idx] = "";
                            }
                        }
                        dgv_ScriptEditor.Rows.Insert(0, row);

                        // Icon表示
                        //dgv_ScriptEditor["E_ICON", 0].Value = bmp_disp_icon[icon_idx];
                    }
                    else
                    {   // モード更新
                        int colCount = dgv_ScriptEditor.ColumnCount;
                        for (int col_idx = 0; col_idx < colCount; col_idx++)
                        {
                            if (dgv_ScriptEditor.Columns[col_idx].Name == "E_SIZE")
                            {
                                dgv_ScriptEditor[col_idx, 0].Value = "0";
                            }
                            //else if (dgv_ScriptEditor.Columns[col_idx].Name == "E_ICON")
                            //{
                            //    dgv_ScriptEditor[col_idx, 0].Value = bmp_disp_icon[icon_idx];
                            //}
                            else if (dgv_ScriptEditor.Columns[col_idx].Name == "E_NAME")
                            {
                                dgv_ScriptEditor[col_idx, 0].Value = disp_itemname;
                            }
                            else if (dgv_ScriptEditor.Columns[col_idx].Name == "E_VALUE")
                            {
                                dgv_ScriptEditor[col_idx, 0].Value = "";
                            }
                        }
                    }
                }

                for (int fi = 0; fi < my_script_editor_disp_data.al_Command_ID.Count; fi++)
                {
                    if((int)my_script_editor_disp_data.al_Disp_Update_Flag[fi] == Constants.DISPLAY_UPDATE_TYPE_ADD
                        || (int)my_script_editor_disp_data.al_Disp_Update_Flag[fi] == Constants.DISPLAY_UPDATE_TYPE_RE)
                    {   // 項目追加

                        DataGridView_AddFlag = false;
                        disp_val = "";
                        byte cmd_id = (byte)my_script_editor_disp_data.al_Command_ID[fi];
                        if (cmd_id == Constants.SCRIPT_COMMAND_INTERVAL)
                        {
                            // 表示データセット
                            DataGridView_AddFlag = true;
                            disp_size = (int)my_script_editor_disp_data.al_Command_Size[fi];
                            disp_itemname = RevOmate.Properties.Resources.SCRIPT_INTERVAL_TEXT;
                            int i_temp = (byte)my_script_editor_disp_data.al_Command_Data1[fi];
                            i_temp = (i_temp << 8) | (byte)my_script_editor_disp_data.al_Command_Data2[fi];
                            disp_val = i_temp.ToString();

                            icon_idx = Constants.DISPLAY_ICON_TYPE_INTERVAL;
                        }
                        else if (cmd_id == Constants.SCRIPT_COMMAND_KEY_PRESS)
                        {
                            // 表示データセット
                            DataGridView_AddFlag = true;
                            disp_size = (int)my_script_editor_disp_data.al_Command_Size[fi];
                            disp_itemname = RevOmate.Properties.Resources.SCRIPT_KEY_PRESS_TEXT;
                            disp_val = const_Key_Code.Get_KeyCode_Name((byte)my_script_editor_disp_data.al_Command_Data1[fi], system_setting_info.Keyboard_Type);

                            icon_idx = Constants.DISPLAY_ICON_TYPE_KEYDOWN;
                        }
                        else if (cmd_id == Constants.SCRIPT_COMMAND_KEY_RELEASE)
                        {
                            // 表示データセット
                            DataGridView_AddFlag = true;
                            disp_size = (int)my_script_editor_disp_data.al_Command_Size[fi];
                            disp_itemname = RevOmate.Properties.Resources.SCRIPT_KEY_RELEASE_TEXT;
                            disp_val = const_Key_Code.Get_KeyCode_Name((byte)my_script_editor_disp_data.al_Command_Data1[fi], system_setting_info.Keyboard_Type);

                            icon_idx = Constants.DISPLAY_ICON_TYPE_KEYUP;
                        }
                        else if (cmd_id == Constants.SCRIPT_COMMAND_L_CLICK
                                || cmd_id == Constants.SCRIPT_COMMAND_L_RELEASE
                                || cmd_id == Constants.SCRIPT_COMMAND_R_CLICK
                                || cmd_id == Constants.SCRIPT_COMMAND_R_RELEASE
                                || cmd_id == Constants.SCRIPT_COMMAND_W_CLICK
                                || cmd_id == Constants.SCRIPT_COMMAND_W_RELEASE
                                || cmd_id == Constants.SCRIPT_COMMAND_B4_CLICK
                                || cmd_id == Constants.SCRIPT_COMMAND_B4_RELEASE
                                || cmd_id == Constants.SCRIPT_COMMAND_B5_CLICK
                                || cmd_id == Constants.SCRIPT_COMMAND_B5_RELEASE)
                        {
                            // 表示データセット
                            DataGridView_AddFlag = true;
                            disp_size = (int)my_script_editor_disp_data.al_Command_Size[fi];
                            if (cmd_id == Constants.SCRIPT_COMMAND_L_CLICK
                                || cmd_id == Constants.SCRIPT_COMMAND_R_CLICK
                                || cmd_id == Constants.SCRIPT_COMMAND_W_CLICK
                                || cmd_id == Constants.SCRIPT_COMMAND_B4_CLICK
                                || cmd_id == Constants.SCRIPT_COMMAND_B5_CLICK)
                            {
                                disp_itemname = RevOmate.Properties.Resources.SCRIPT_MOUSE_CLICK_TEXT;
                                icon_idx = Constants.DISPLAY_ICON_TYPE_MUSDOWN;
                            }
                            else
                            {
                                disp_itemname = RevOmate.Properties.Resources.SCRIPT_MOUSE_RELEASE_TEXT;
                                icon_idx = Constants.DISPLAY_ICON_TYPE_MUSUP;
                            }
                            byte set_val_name_idx = 0xFF;
                            switch(cmd_id)
                            {
                                case Constants.SCRIPT_COMMAND_L_CLICK:
                                case Constants.SCRIPT_COMMAND_L_RELEASE:
                                    set_val_name_idx = 0;
                                    break;
                                case Constants.SCRIPT_COMMAND_R_CLICK:
                                case Constants.SCRIPT_COMMAND_R_RELEASE:
                                    set_val_name_idx = 2;
                                    break;
                                case Constants.SCRIPT_COMMAND_W_CLICK:
                                case Constants.SCRIPT_COMMAND_W_RELEASE:
                                    set_val_name_idx = 1;
                                    break;
                                case Constants.SCRIPT_COMMAND_B4_CLICK:
                                case Constants.SCRIPT_COMMAND_B4_RELEASE:
                                    set_val_name_idx = 3;
                                    break;
                                case Constants.SCRIPT_COMMAND_B5_CLICK:
                                case Constants.SCRIPT_COMMAND_B5_RELEASE:
                                    set_val_name_idx = 4;
                                    break;
                            }
                            if (0 <= set_val_name_idx && set_val_name_idx < mouse_set_type_name.Length)
                            {
                                disp_val = mouse_set_type_name[set_val_name_idx];
                            }
                            icon_idx = Constants.DISPLAY_ICON_TYPE_MUSDOWN;
                        }
#if false
                        else if (cmd_id == Constants.SCRIPT_COMMAND_L_CLICK
                                || cmd_id == Constants.SCRIPT_COMMAND_R_CLICK
                                || cmd_id == Constants.SCRIPT_COMMAND_W_CLICK
                                || cmd_id == Constants.SCRIPT_COMMAND_B4_CLICK
                                || cmd_id == Constants.SCRIPT_COMMAND_B5_CLICK)
                        {
                            // 表示データセット
                            DataGridView_AddFlag = true;
                            disp_size = (int)my_script_editor_disp_data.al_Command_Size[fi];
                            if (cmd_id == Constants.SCRIPT_COMMAND_L_CLICK)
                            {
                                disp_itemname = Constants.SCRIPT_MOUSE_L_CLICK_TEXT;
                            }
                            else if (cmd_id == Constants.SCRIPT_COMMAND_R_CLICK)
                            {
                                disp_itemname = Constants.SCRIPT_MOUSE_R_CLICK_TEXT;
                            }
                            else if (cmd_id == Constants.SCRIPT_COMMAND_W_CLICK)
                            {
                                disp_itemname = Constants.SCRIPT_MOUSE_W_CLICK_TEXT;
                            }
                            else if (cmd_id == Constants.SCRIPT_COMMAND_B4_CLICK)
                            {
                                disp_itemname = Constants.SCRIPT_MOUSE_B4_CLICK_TEXT;
                            }
                            else if (cmd_id == Constants.SCRIPT_COMMAND_B5_CLICK)
                            {
                                disp_itemname = Constants.SCRIPT_MOUSE_B5_CLICK_TEXT;
                            }

                            icon_idx = Constants.DISPLAY_ICON_TYPE_MUSDOWN;
                        }
                        else if (cmd_id == Constants.SCRIPT_COMMAND_L_RELEASE
                                || cmd_id == Constants.SCRIPT_COMMAND_R_RELEASE
                                || cmd_id == Constants.SCRIPT_COMMAND_W_RELEASE
                                || cmd_id == Constants.SCRIPT_COMMAND_B4_RELEASE
                                || cmd_id == Constants.SCRIPT_COMMAND_B5_RELEASE)
                        {
                            // 表示データセット
                            DataGridView_AddFlag = true;
                            disp_size = (int)my_script_editor_disp_data.al_Command_Size[fi];
                            if (cmd_id == Constants.SCRIPT_COMMAND_L_RELEASE)
                            {
                                disp_itemname = Constants.SCRIPT_MOUSE_L_RELEASE_TEXT;
                            }
                            else if (cmd_id == Constants.SCRIPT_COMMAND_R_RELEASE)
                            {
                                disp_itemname = Constants.SCRIPT_MOUSE_R_RELEASE_TEXT;
                            }
                            else if (cmd_id == Constants.SCRIPT_COMMAND_W_RELEASE)
                            {
                                disp_itemname = Constants.SCRIPT_MOUSE_W_RELEASE_TEXT;
                            }
                            else if (cmd_id == Constants.SCRIPT_COMMAND_B4_RELEASE)
                            {
                                disp_itemname = Constants.SCRIPT_MOUSE_B4_RELEASE_TEXT;
                            }
                            else if (cmd_id == Constants.SCRIPT_COMMAND_B5_RELEASE)
                            {
                                disp_itemname = Constants.SCRIPT_MOUSE_B5_RELEASE_TEXT;
                            }

                            icon_idx = Constants.DISPLAY_ICON_TYPE_MUSUP;
                        }
#endif
                        else if (cmd_id == Constants.SCRIPT_COMMAND_WHEEL_UP
                                || cmd_id == Constants.SCRIPT_COMMAND_WHEEL_DOWN)
                        {
                            // 表示データセット
                            DataGridView_AddFlag = true;
                            disp_size = (int)my_script_editor_disp_data.al_Command_Size[fi];
                            if (cmd_id == Constants.SCRIPT_COMMAND_WHEEL_UP)
                            {
                                disp_itemname = RevOmate.Properties.Resources.SCRIPT_WHEEL_UP_TEXT;
                            }
                            else if (cmd_id == Constants.SCRIPT_COMMAND_WHEEL_DOWN)
                            {
                                disp_itemname = RevOmate.Properties.Resources.SCRIPT_WHEEL_DOWN_TEXT;
                            }

                            icon_idx = Constants.DISPLAY_ICON_TYPE_MUSWHEEL;
                        }
                        else if (cmd_id == Constants.SCRIPT_COMMAND_JOY_BUTTON_PRESS
                                || cmd_id == Constants.SCRIPT_COMMAND_JOY_BUTTON_RELESE)
                        {
                            // 表示データセット
                            DataGridView_AddFlag = true;
                            disp_size = (int)my_script_editor_disp_data.al_Command_Size[fi];
                            if (cmd_id == Constants.SCRIPT_COMMAND_JOY_BUTTON_PRESS)
                            {
                                disp_itemname = RevOmate.Properties.Resources.SCRIPT_JOY_BUTTON_PRESS_TEXT;
                                icon_idx = Constants.DISPLAY_ICON_TYPE_JOY_BUTTON_DOWN;
                            }
                            else if (cmd_id == Constants.SCRIPT_COMMAND_JOY_BUTTON_RELESE)
                            {
                                disp_itemname = RevOmate.Properties.Resources.SCRIPT_JOY_BUTTON_RELEASE_TEXT;
                                icon_idx = Constants.DISPLAY_ICON_TYPE_JOY_BUTTON_UP;
                            }
                            disp_val = string.Format("{0}", ((byte)my_script_editor_disp_data.al_Command_Data1[fi]) + 1);
                            //disp_val = string.Format("0x{0:X2},0x{1:X2}", (byte)my_script_editor_disp_data.al_Command_Data1[fi], (byte)my_script_editor_disp_data.al_Command_Data2[fi]);

                            icon_idx = Constants.DISPLAY_ICON_TYPE_JOY_BUTTON_DOWN;
                        }
                        else if (cmd_id == Constants.SCRIPT_COMMAND_JOY_HATSW_PRESS)
                        {
                            // 表示データセット
                            DataGridView_AddFlag = true;
                            disp_size = (int)my_script_editor_disp_data.al_Command_Size[fi];
                            disp_itemname = RevOmate.Properties.Resources.SCRIPT_JOY_HATSW_PRESS_TEXT;

                            disp_val = "";
                            int hat_sw_val = int.Parse(my_script_editor_disp_data.al_Command_Data1[fi].ToString());
                            if (0 <= hat_sw_val && hat_sw_val < my_hat_sw_disp_text.Length)
                            {
                                disp_val = my_hat_sw_disp_text[hat_sw_val];
                                //string.Format("{0}", (byte)my_script_editor_disp_data.al_Command_Data1[fi]);
                            }

                            icon_idx = Constants.DISPLAY_ICON_TYPE_JOY_HATSW_DOWN;
                        }
                        else if (cmd_id == Constants.SCRIPT_COMMAND_JOY_HATSW_RELESE)
                        {
                            // 表示データセット
                            DataGridView_AddFlag = true;
                            disp_size = (int)my_script_editor_disp_data.al_Command_Size[fi];
                            disp_itemname = RevOmate.Properties.Resources.SCRIPT_JOY_HATSW_RELEASE_TEXT;

                            icon_idx = Constants.DISPLAY_ICON_TYPE_JOY_HATSW_UP;
                        }
                        else if (cmd_id == Constants.SCRIPT_COMMAND_JOY_L_LEVER
                                || cmd_id == Constants.SCRIPT_COMMAND_JOY_R_LEVER)
                        {
                            // 表示データセット
                            DataGridView_AddFlag = true;
                            disp_size = (int)my_script_editor_disp_data.al_Command_Size[fi];
                            if (cmd_id == Constants.SCRIPT_COMMAND_JOY_L_LEVER)
                            {
                                disp_itemname = RevOmate.Properties.Resources.SCRIPT_JOY_LEVER_L_TEXT;
                            }
                            else if (cmd_id == Constants.SCRIPT_COMMAND_JOY_R_LEVER)
                            {
                                disp_itemname = RevOmate.Properties.Resources.SCRIPT_JOY_LEVER_R_TEXT;
                            }
                            sbyte[] sb_lever = new sbyte[2];
                            if ((byte)my_script_editor_disp_data.al_Command_Data1[fi] > 0x7F)
                            {
                                sb_lever[0] = (sbyte)((byte)my_script_editor_disp_data.al_Command_Data1[fi] - 0x100);
                            }
                            else
                            {
                                sb_lever[0] = (sbyte)((byte)my_script_editor_disp_data.al_Command_Data1[fi]);
                            }
                            if ((byte)my_script_editor_disp_data.al_Command_Data2[fi] > 0x7F)
                            {
                                sb_lever[1] = (sbyte)((byte)my_script_editor_disp_data.al_Command_Data2[fi] - 0x100);
                            }
                            else
                            {
                                sb_lever[1] = (sbyte)((byte)my_script_editor_disp_data.al_Command_Data2[fi]);
                            }
                            disp_val = string.Format("{0},{1}", sb_lever[0], sb_lever[1]);

                            icon_idx = Constants.DISPLAY_ICON_TYPE_JOY_LEVER_DOWN;
                        }
                        else if (cmd_id == Constants.SCRIPT_COMMAND_JOY_L_LEVER_CENTER
                                || cmd_id == Constants.SCRIPT_COMMAND_JOY_R_LEVER_CENTER)
                        {
                            // 表示データセット
                            DataGridView_AddFlag = true;
                            disp_size = (int)my_script_editor_disp_data.al_Command_Size[fi];
                            if (cmd_id == Constants.SCRIPT_COMMAND_JOY_L_LEVER_CENTER)
                            {
                                disp_itemname = RevOmate.Properties.Resources.SCRIPT_JOY_LEVER_L_CENTER_TEXT;
                            }
                            else if (cmd_id == Constants.SCRIPT_COMMAND_JOY_R_LEVER_CENTER)
                            {
                                disp_itemname = RevOmate.Properties.Resources.SCRIPT_JOY_LEVER_R_CENTER_TEXT;
                            }

                            icon_idx = Constants.DISPLAY_ICON_TYPE_JOY_LEVER_UP;
                        }
                        else if (cmd_id == Constants.SCRIPT_COMMAND_MOUSE_MOVE)
                        {
                            // 表示データセット
                            DataGridView_AddFlag = true;
                            disp_size = (int)my_script_editor_disp_data.al_Command_Size[fi];
                            disp_itemname = RevOmate.Properties.Resources.SCRIPT_MOUSE_MOVE_TEXT;
                            sbyte[] sb_lever = new sbyte[2];
                            if ((byte)my_script_editor_disp_data.al_Command_Data1[fi] > 0x7F)
                            {
                                sb_lever[0] = (sbyte)((byte)my_script_editor_disp_data.al_Command_Data1[fi] - 0x100);
                            }
                            else
                            {
                                sb_lever[0] = (sbyte)((byte)my_script_editor_disp_data.al_Command_Data1[fi]);
                            }
                            if ((byte)my_script_editor_disp_data.al_Command_Data2[fi] > 0x7F)
                            {
                                sb_lever[1] = (sbyte)((byte)my_script_editor_disp_data.al_Command_Data2[fi] - 0x100);
                            }
                            else
                            {
                                sb_lever[1] = (sbyte)((byte)my_script_editor_disp_data.al_Command_Data2[fi]);
                            }
                            disp_val = string.Format("{0},{1}", sb_lever[0], sb_lever[1]);

                            icon_idx = Constants.DISPLAY_ICON_TYPE_MUSMOVE;
                        }
                        else if (cmd_id == Constants.SCRIPT_COMMAND_MULTIMEDIA_PRESS
                                || cmd_id == Constants.SCRIPT_COMMAND_MULTIMEDIA_RELESE)
                        {
                            // 表示データセット
                            DataGridView_AddFlag = true;
                            disp_size = (int)my_script_editor_disp_data.al_Command_Size[fi];
                            if (cmd_id == Constants.SCRIPT_COMMAND_MULTIMEDIA_PRESS)
                            {
                                disp_itemname = RevOmate.Properties.Resources.SCRIPT_MULTIMEDIA_PRESS_TEXT;
                                icon_idx = Constants.DISPLAY_ICON_TYPE_MM_BUTTON_DOWN;
                            }
                            else if (cmd_id == Constants.SCRIPT_COMMAND_MULTIMEDIA_RELESE)
                            {
                                disp_itemname = RevOmate.Properties.Resources.SCRIPT_MULTIMEDIA_RELEASE_TEXT;
                                icon_idx = Constants.DISPLAY_ICON_TYPE_MM_BUTTON_UP;
                            }
                            byte set_val_name_idx = 0xFF;
                            byte set_mm_val = (byte)my_script_editor_disp_data.al_Command_Data1[fi];
                            for (byte set_type_idx = 0; set_type_idx < multimedia_set_type.Length; set_type_idx++)
                            {
                                if (multimedia_set_type[set_type_idx] == set_mm_val)
                                {
                                    set_val_name_idx = set_type_idx;
                                }
                            }
                            if (0 <= set_val_name_idx && set_val_name_idx < multimedia_set_type_name.Length)
                            {
                                disp_val = multimedia_set_type_name[set_val_name_idx];
                            }

                            icon_idx = Constants.DISPLAY_ICON_TYPE_MM_BUTTON_DOWN;
                        }

                        if (DataGridView_AddFlag == true)
                        {
                            if ((int)my_script_editor_disp_data.al_Disp_Update_Flag[fi] == Constants.DISPLAY_UPDATE_TYPE_RE)
                            {
                                int colCount = dgv_ScriptEditor.ColumnCount;
                                for (int col_idx = 0; col_idx < colCount; col_idx++)
                                {
                                    if (dgv_ScriptEditor.Columns[col_idx].Name == "E_SIZE")
                                    {
                                        dgv_ScriptEditor[col_idx, fi + 1].Value = disp_size.ToString();
                                    }
                                    //else if (dgv_ScriptEditor.Columns[col_idx].Name == "E_ICON")
                                    //{
                                    //    dgv_ScriptEditor[col_idx, fi + 1].Value = bmp_disp_icon[icon_idx];
                                    //}
                                    else if (dgv_ScriptEditor.Columns[col_idx].Name == "E_NAME")
                                    {
                                        dgv_ScriptEditor[col_idx, fi + 1].Value = disp_itemname;
                                    }
                                    else if (dgv_ScriptEditor.Columns[col_idx].Name == "E_VALUE")
                                    {
                                        dgv_ScriptEditor[col_idx, fi + 1].Value = disp_val;
                                    }
                                }
                            }
                            else
                            {
                                int colCount = dgv_ScriptEditor.ColumnCount;
                                string[] row = new string[colCount];
                                for (int col_idx = 0; col_idx < colCount; col_idx++)
                                {
                                    if (dgv_ScriptEditor.Columns[col_idx].Name == "E_SIZE")
                                    {
                                        row[col_idx] = disp_size.ToString();
                                    }
                                    //else if (dgv_ScriptEditor.Columns[col_idx].Name == "E_ICON")
                                    //{
                                    //}
                                    else if (dgv_ScriptEditor.Columns[col_idx].Name == "E_NAME")
                                    {
                                        row[col_idx] = disp_itemname;
                                    }
                                    else if (dgv_ScriptEditor.Columns[col_idx].Name == "E_VALUE")
                                    {
                                        row[col_idx] = disp_val;
                                    }
                                }
                                dgv_ScriptEditor.Rows.Insert(fi + 1, row);

                                // Icon表示
                                //dgv_ScriptEditor["E_ICON", fi + 1].Value = bmp_disp_icon[icon_idx];
                            }
                        }

                        my_script_editor_disp_data.al_Disp_Update_Flag[fi] = Constants.DISPLAY_UPDATE_TYPE_NONE;
                    }
                }
            }
            catch
            {
            }
        }

        private void btn_ScriptRead_Click(object sender, EventArgs e)
        {
            try
            {
                int des_idx = 0;    // index 0 固定
                des_idx = dgv_ScriptList.CurrentRow.Index;
                if (des_idx < 0 || Constants.SCRIPT_NUM <= des_idx)
                {
                    return;
                }

                // マクロNo.表示
                txtbx_script_no.Text = string.Format("{0:000}", des_idx + 1);
                // マクロ名称表示
                if (my_script_info_datas.Script_Info_Datas[des_idx].Name != "")
                {
                    txtbx_script_name.Text = my_script_info_datas.Script_Info_Datas[des_idx].Name;
                }
                else
                {
                    txtbx_script_name.Text = RevOmate.Properties.Resources.MACRO_NAME_UNDEFINE;
                }

                my_flash_read_write_buffer.clear();
                my_flash_read_write_buffer.set_read_address(my_script_info_datas.Script_Info_Datas[des_idx].Recode_Address, my_script_info_datas.Script_Info_Datas[des_idx].Script_Size, des_idx);
                my_flash_read_write_buffer.set_info(my_script_info_datas.Script_Info_Datas[des_idx].Script_Mode, my_script_info_datas.Script_Info_Datas[des_idx].Name);

                my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_MEMORY;
                my_App_Setting_Data.Script_Setting_Drag_Target_Control = Constants.SCRIPT_DRAG_CTRL_EDIT;

                fData.Set_Flash_Read_Status(FlashControl.FM_READ_TYPE_SCRIPT_DATA, FlashControl.FM_READ_STATUS_RQ);
            }
            catch
            {
            }
        }

        private void btn_ScriptWrite_Click(object sender, EventArgs e)
        {
            try
            {
                int target_idx = 0;    // index 0 固定
                target_idx = dgv_ScriptList.CurrentRow.Index;
                if (target_idx < 0 || Constants.SCRIPT_NUM <= target_idx)
                {
                    return;
                }

                ArrayList al_write_data = new ArrayList();

                my_flash_read_write_buffer.clear();
                // 書き込みデータ取得
                my_Convert_SE_Disp_Data_2_Array_List(ref al_write_data);

                // スクリプトサイズチェック
                if (my_Flash_Write_Script_Size_Check(al_write_data.Count, true) == false)
                {   // Size OK

                    // ボタン設定情報書き込み要求
                    //fData.Set_Flash_Write_Status(FlashControl.FM_WRITE_TYPE_FUNCTION_SETTING, FlashControl.FM_WRITE_STATUS_RQ);

                    my_flash_read_write_buffer.set_write_data(al_write_data, target_idx);
                    my_flash_read_write_buffer.set_info(my_script_editor_disp_data.Script_Mode, txtbx_script_name.Text);
                    my_macro_name_set(target_idx, txtbx_script_name.Text);

                    my_Flash_Write_Sector_Read_Address_Set(ref my_flash_read_write_buffer);

                    my_App_Setting_Data.Script_Setting_Drag_Target_Control = Constants.SCRIPT_DRAG_CTRL_MEMORY;

                    fData.Set_Flash_Write_Status(FlashControl.FM_WRITE_TYPE_SCRIPT_DATA, FlashControl.FM_WRITE_STATUS_RQ);


                    // マクロNo.表示
                    txtbx_script_no.Text = string.Format("{0:000}", target_idx + 1);
                    // マクロ名称表示
                    if (my_script_info_datas.Script_Info_Datas[target_idx].Name != "")
                    {
                        txtbx_script_name.Text = my_script_info_datas.Script_Info_Datas[target_idx].Name;
                    }
                    else
                    {
                        txtbx_script_name.Text = RevOmate.Properties.Resources.MACRO_NAME_UNDEFINE;
                    }
                }
            }
            catch
            {
            }
        }


        private void txtbx_Interval_Inputbox_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    my_Set_SE_Interval(indexOfItemUnderMouseToDrag, ((System.Windows.Forms.TextBox)sender).Text);

                    txtbx_Interval_Inputbox.Visible = false;
                    gbx_Script_Add_Info.Visible = false;

                    if (my_App_Setting_Data.Script_Add_Manual_Control == Constants.SCRIPT_ADD_MANUAL_INTERVAL)
                    {
                        my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_NON;
                    }
                }
            }
            catch
            {
            }
        }

        private void txtbx_Interval_Inputbox_Leave(object sender, EventArgs e)
        {
            try
            {
                my_Set_SE_Interval(indexOfItemUnderMouseToDrag, ((System.Windows.Forms.TextBox)sender).Text);

                txtbx_Interval_Inputbox.Visible = false;
                gbx_Script_Add_Info.Visible = false;

                if (my_App_Setting_Data.Script_Add_Manual_Control == Constants.SCRIPT_ADD_MANUAL_INTERVAL)
                {
                    my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_NON;
                }
            }
            catch
            {
            }
        }


        /// <summary>
        /// スクリプト間隔を設定する
        /// </summary>
        /// <param name="p_idx"></param>
        /// <param name="p_interval"></param>
        private void my_Set_SE_Interval(int p_idx, string p_interval)
        {
            int set_interval = 0;
            bool change_error = false;
            try
            {
                try
                {
                    set_interval = int.Parse(p_interval);
                }
                catch
                {
                    change_error = true;
                    //set_interval = 1;
                }

                if (set_interval > 0xFFFF)
                {
                    set_interval = 0xFFFF;
                }

                // 表示位置チェック
                if (0 < p_idx && p_idx < dgv_ScriptEditor.Rows.Count)
                {
                    if (change_error == false)
                    {   // 入力文字が数値
                        // スクリプト名称を設定
                        dgv_ScriptEditor[dgv_ScriptEditor.Columns["E_VALUE"].Index, p_idx].Value = set_interval.ToString();

                        if ((byte)my_script_editor_disp_data.al_Command_ID[p_idx - 1] == Constants.SCRIPT_COMMAND_INTERVAL)
                        {
                            my_script_editor_disp_data.al_Command_Data1[p_idx-1] = (byte)((set_interval >> 8) & 0xFF);
                            my_script_editor_disp_data.al_Command_Data2[p_idx - 1] = (byte)(set_interval & 0xFF);
                        }
                    }
                    else
                    {   // 入力文字が数値以外
                        // 入力値を戻す
                    }
                }
            }
            catch
            {
            }
        }


        /// <summary>
        /// 十字キーを設定する
        /// </summary>
        /// <param name="p_idx"></param>
        /// <param name="p_set_key"></param>
        private void my_Set_SE_HatSW(int p_idx, byte p_set_key)
        {
            uint set_key_val = 0;
            bool change_error = false;
            try
            {
                try
                {
                    set_key_val = p_set_key;
                }
                catch
                {
                    change_error = true;
                    //set_interval = 1;
                }

                if (set_key_val > my_hat_sw_disp_text.Length)
                {
                    set_key_val = (uint)(my_hat_sw_disp_text.Length - 1);
                }

                // 表示位置チェック
                if (0 < p_idx && p_idx < dgv_ScriptEditor.Rows.Count)
                {
                    if (change_error == false)
                    {   // 入力文字が数値
                        // スクリプト名称を設定
                        dgv_ScriptEditor[dgv_ScriptEditor.Columns["E_VALUE"].Index, p_idx].Value = my_hat_sw_disp_text[set_key_val];

                        if ((byte)my_script_editor_disp_data.al_Command_ID[p_idx - 1] == Constants.SCRIPT_COMMAND_INTERVAL)
                        {
                            my_script_editor_disp_data.al_Command_Data1[p_idx - 1] = set_key_val;
                            my_script_editor_disp_data.al_Command_Data2[p_idx - 1] = 0;
                        }
                    }
                    else
                    {   // 入力文字が数値以外
                        // 入力値を戻す
                    }
                }
            }
            catch
            {
            }
        }
        /// <summary>
        /// ジョイスティックのボタンを設定する
        /// </summary>
        /// <param name="p_idx"></param>
        /// <param name="p_set_key"></param>
        private void my_Set_SE_JoyButton(int p_idx, byte p_set_button_no)
        {
            bool change_error = false;
            try
            {

                if (p_set_button_no >= my_rbtn_button_set.Length)
                {
                    p_set_button_no = 0;
                }

                // 表示位置チェック
                if (0 < p_idx && p_idx < dgv_ScriptEditor.Rows.Count)
                {
                    if (change_error == false)
                    {   // 入力文字が数値
                        // button No.
                        my_rbtn_button_set[p_set_button_no].Checked = true;
                        //for (int fi = 0; fi < my_rbtn_button_set.Length; fi++)
                        //{
                        //    if (fi == p_set_button_no)
                        //    {
                        //        my_rbtn_button_set[fi].Checked = true;
                        //    }
                        //    else
                        //    {
                        //        my_rbtn_button_set[fi].Checked = false;
                        //    }
                        //}
                        dgv_ScriptEditor[dgv_ScriptEditor.Columns["E_VALUE"].Index, p_idx].Value = string.Format("{0}", p_set_button_no + 1);

                        if ((byte)my_script_editor_disp_data.al_Command_ID[p_idx - 1] == Constants.SCRIPT_COMMAND_JOY_BUTTON_PRESS)
                        {
                            my_script_editor_disp_data.al_Command_Data1[p_idx - 1] = p_set_button_no;
                            my_script_editor_disp_data.al_Command_Data2[p_idx - 1] = 0;
                        }
                    }
                    else
                    {   // 入力文字が数値以外
                        // 入力値を戻す
                    }
                }
            }
            catch
            {
            }
        }

        private void my_Convert_SE_Disp_Data_2_Array_List(ref ArrayList p_out_data)
        {
            byte id;
            bool b_error_flag = false;
            try
            {
                int data_num = my_script_editor_disp_data.al_Command_ID.Count;

                p_out_data.Clear();

                int fi = 0;
                while (fi < data_num && b_error_flag == false)
                {
                    id = (byte)my_script_editor_disp_data.al_Command_ID[fi];
                    switch (id)
                    {
                        case Constants.SCRIPT_COMMAND_INTERVAL:
                        case Constants.SCRIPT_COMMAND_MOUSE_MOVE:
                        case Constants.SCRIPT_COMMAND_JOY_L_LEVER:
                        case Constants.SCRIPT_COMMAND_JOY_R_LEVER:
                            p_out_data.Add(id);
                            p_out_data.Add((byte)my_script_editor_disp_data.al_Command_Data1[fi]);
                            p_out_data.Add((byte)my_script_editor_disp_data.al_Command_Data2[fi]);
                            break;
                        case Constants.SCRIPT_COMMAND_KEY_PRESS:
                        case Constants.SCRIPT_COMMAND_KEY_RELEASE:
                        case Constants.SCRIPT_COMMAND_MULTIMEDIA_PRESS:
                        case Constants.SCRIPT_COMMAND_MULTIMEDIA_RELESE:
                        case Constants.SCRIPT_COMMAND_WHEEL_UP:
                        case Constants.SCRIPT_COMMAND_WHEEL_DOWN:
                        case Constants.SCRIPT_COMMAND_JOY_BUTTON_PRESS:
                        case Constants.SCRIPT_COMMAND_JOY_BUTTON_RELESE:
                        case Constants.SCRIPT_COMMAND_JOY_HATSW_PRESS:
                            p_out_data.Add(id);
                            p_out_data.Add((byte)my_script_editor_disp_data.al_Command_Data1[fi]);
                            break;
                        case Constants.SCRIPT_COMMAND_L_CLICK:
                        case Constants.SCRIPT_COMMAND_L_RELEASE:
                        case Constants.SCRIPT_COMMAND_R_CLICK:
                        case Constants.SCRIPT_COMMAND_R_RELEASE:
                        case Constants.SCRIPT_COMMAND_W_CLICK:
                        case Constants.SCRIPT_COMMAND_W_RELEASE:
                        case Constants.SCRIPT_COMMAND_B4_CLICK:
                        case Constants.SCRIPT_COMMAND_B4_RELEASE:
                        case Constants.SCRIPT_COMMAND_B5_CLICK:
                        case Constants.SCRIPT_COMMAND_B5_RELEASE:
                        case Constants.SCRIPT_COMMAND_JOY_HATSW_RELESE:
                        case Constants.SCRIPT_COMMAND_JOY_L_LEVER_CENTER:
                        case Constants.SCRIPT_COMMAND_JOY_R_LEVER_CENTER:
                            p_out_data.Add(id);
                            break;
                        default:
                            b_error_flag = true;
                            break;
                    }

                    fi++;
                }
            }
            catch
            {
            }
        }

        private bool my_Flash_Write_Script_Size_Check(int p_size, bool msg_out_flag)
        {
            bool b_ret = true;
            try
            {
                if (p_size > FlashControl.FM_SCRIPT_DATA_MAX_SIZE)
                {   // Size NG
                    if (msg_out_flag == true)
                    {
                        string msg = RevOmate.Properties.Resources.SCRIPT_SIZE_OVER_MSG + "(SIZE=" + p_size.ToString() + "byte / LIMIT=" + FlashControl.FM_SCRIPT_DATA_MAX_SIZE.ToString() + "byte)";
                        MessageBox.Show(msg, RevOmate.Properties.Resources.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {   // Size OK
                    b_ret = false;
                }
            }
            catch
            {
            }
            return b_ret;
        }

        private void my_Flash_Write_Sector_Read_Address_Set(ref STR_FLASH_READ_WRITE_BUFFER p_flash_buff)
        {
            //bool b_error_flag = false;
            try
            {
                int re_write_idx = p_flash_buff.Write_Idx;
                int re_write_sector_no;
                int top_address;
                int end_address;

                if (0 <= re_write_idx && re_write_idx <= my_script_info_datas.Script_Info_Datas.GetUpperBound(0))
                {
                    // Idxから書き換えセクター番号を求める
                    re_write_sector_no = re_write_idx % FlashControl.FM_SCRIPT_DATA_SECTOR_NUM;
                    // 書き換えセクターの先頭と末尾のアドレスを求める
                    top_address = FlashControl.FM_ADR_SCRIPT_DATA + (int)FlashControl.FM_SECTOR_SIZE * re_write_sector_no;
                    end_address = top_address + (int)FlashControl.FM_SECTOR_SIZE - 1;

                    p_flash_buff.Write_Top_Address = top_address;
                }
                else
                {
                    // idx 範囲外
                    //b_error_flag = true;
                    return;
                }


                int script_num = my_script_info_datas.Script_Info_Datas.Length;
                int set_idx = 0;
                for (int fi = 0; fi < script_num; fi++)
                {
                    if (top_address <= my_script_info_datas.Script_Info_Datas[fi].Recode_Address && my_script_info_datas.Script_Info_Datas[fi].Recode_Address <= end_address)
                    {   // 書き換えスクリプトと同じセクターのデータ
                        p_flash_buff.al_Read_Address.Add(my_script_info_datas.Script_Info_Datas[fi].Recode_Address);
                        p_flash_buff.al_Read_Size.Add(my_script_info_datas.Script_Info_Datas[fi].Script_Size);
                        p_flash_buff.al_Read_Idx.Add(fi);

                        set_idx++;
                    }

                    if (set_idx >= FlashControl.FM_SCRIPT_DATA_NUM_PER_SECTOR)
                    {
                        break;
                    }
                }
            }
            catch
            {
            }
        }

        private int my_Convert_SE_Disp_Data_2_SL_byte_Buff(ref byte[] p_out_data, ArrayList p_al_SE_data, byte p_mode)
        {
            int i_ret = 0;
            try
            {
                byte[] byte_name;

                // Script List の byte buffer Sizeを計算
                int buff_size = 0;
                // シグネチャー
                byte_name = System.Text.Encoding.Default.GetBytes(Constants.SCRIPT_FILE_SIGNATURE);
                int name_len = byte_name.Length;
                buff_size += Constants.SCRIPT_FILE_SIGNATURE_SIZE_LEN + name_len;
                // Mode
                buff_size += Constants.SCRIPT_FILE_MODE_SIZE_LEN;
                // Script Size
                int script_size = p_al_SE_data.Count;
                if (script_size > FlashControl.FM_SCRIPT_DATA_MAX_SIZE)
                {
                    script_size = FlashControl.FM_SCRIPT_DATA_MAX_SIZE;
                }
                buff_size += Constants.SCRIPT_FILE_SCRIPT_SIZE_LEN + script_size;
                // File Size
                int file_size = 0;
                buff_size += Constants.SCRIPT_FILE_FILE_SIZE_LEN;
                file_size = buff_size;

                //  buffer size チェック
                if (buff_size <= p_out_data.Length)
                {
                    int idx = 0;
                    // シグネチャーコピー
                    p_out_data[idx++] = (byte)(name_len & 0xFF);
                    Array.Copy(byte_name, 0, p_out_data, 1, name_len);
                    idx += name_len;
                    // ファイルサイズコピー
                    p_out_data[idx++] = (byte)((file_size) & 0xFF);
                    p_out_data[idx++] = (byte)(((file_size) >> 8) & 0xFF);
                    p_out_data[idx++] = (byte)(((file_size) >> 16) & 0xFF);
                    p_out_data[idx++] = (byte)(((file_size) >> 24) & 0xFF);
                    // モードコピー
                    p_out_data[idx++] = p_mode;
                    // スクリプトコピー
                    p_out_data[idx++] = (byte)((script_size) & 0xFF);
                    p_out_data[idx++] = (byte)(((script_size) >> 8) & 0xFF);
                    p_out_data[idx++] = (byte)(((script_size) >> 16) & 0xFF);
                    p_out_data[idx++] = (byte)(((script_size) >> 24) & 0xFF);
                    for (int fi = 0; fi < script_size; fi++)
                    {
                        p_out_data[idx++] = (byte)(p_al_SE_data[fi]);
                    }

                    // コピーサイズチェック
                    if (buff_size == idx)
                    {   // 正常
                        i_ret = buff_size;
                    }
                }
            }
            catch
            {
            }
            return i_ret;
        }

        /// <summary>
        /// Script List Fileを保存する
        /// </summary>
        /// <param name="p_folder_ID"></param>
        /// <param name="p_file_name"></param>
        /// <returns></returns>
        private int my_Save_Script_File(string p_file_name, byte[] p_data, int p_data_size)
        {
            int i_ret = -1;
            bool error_flag = false;
            //string folder_name = "";
            //string folder_path = "";

            if (p_file_name == "" || p_data_size <= 0)
            {   // パラメータエラー
                error_flag = true;
            }
            
            if (error_flag == false)
            {
                FileStream fs = null;
                try
                {
                    // ファイルに書き出し
                    fs = new FileStream(p_file_name, FileMode.Create, FileAccess.Write);
                    fs.Write(p_data, 0, p_data_size);
                    i_ret = 0;
                }
                catch
                {
                }
                finally
                {
                    if (fs != null)
                    {
                        fs.Close();
                        fs.Dispose();
                    }
                }
            }

            return i_ret;
        }

        /// <summary>
        /// Script Fileのフォーマットに合っているかチェックする
        /// Script Info（スクリプトサイズ、モード）を返す
        /// </summary>
        /// <param name="p_in_data"></param>
        /// <param name="p_in_data_size"></param>
        /// <param name="p_out_name_size"></param>
        /// <param name="p_out_name"></param>
        /// <param name="p_out_script_size"></param>
        /// <param name="p_out_mode"></param>
        /// <returns>true=OK, false=NG</returns>
        private bool my_Check_Script_File_Format(byte[] p_in_data, int p_in_data_size, ref int p_out_name_size, ref string p_out_name, ref int p_out_script_size, ref byte p_out_mode)
        {
            bool b_ret = false;
            bool error_flag = false;
            int idx = 0;
            try
            {
                // 出力用変数を初期化
                p_out_name_size = 0;
                p_out_name = "";
                p_out_script_size = 0;
                p_out_mode = 0;

                // 配列長の最初のチェック シグネチャ格納サイズ１＋ファイルサイズ４＋モードサイズ１＋スクリプトサイズ４＝１０バイト
                if (p_in_data.Length >= p_in_data_size && p_in_data_size >= Constants.SCRIPT_FILE_MIN_SIZE)
                {
                    // シグネチャサイズは
                    p_out_name_size = p_in_data[idx];
                    if (p_in_data[idx] > 0)
                    {
                        if (p_in_data_size > (idx + p_in_data[idx]))
                        {   // 文字列コピー
                            byte[] byte_name = new byte[p_in_data[idx]];
                            Array.Copy(p_in_data, idx + 1, byte_name, 0, byte_name.Length);
                            p_out_name = System.Text.Encoding.Default.GetString(byte_name);
                            idx += p_in_data[idx];
                        }
                        else
                        {   // サイズエラー
                            error_flag = true;
                        }
                    }
                    idx++;

                    // ファイルサイズは？
                    if (error_flag == false)
                    {
                        int i_size = 0;

                        if (p_in_data_size >= (idx + Constants.SCRIPT_FILE_FILE_SIZE_LEN))
                        {
                            i_size = p_in_data[idx + 3];
                            i_size = (int)(i_size << 8) | (int)p_in_data[idx + 2];
                            i_size = (int)(i_size << 8) | (int)p_in_data[idx + 1];
                            i_size = (int)(i_size << 8) | (int)p_in_data[idx];

                            idx += Constants.SCRIPT_FILE_FILE_SIZE_LEN;
                            if (p_in_data_size != i_size)
                            {   // スクリプトサイズエラー
                                error_flag = true;
                            }
                        }
                        else
                        {   // サイズエラー
                            error_flag = true;
                        }
                    }

                    // モードは？
                    if (error_flag == false)
                    {
                        if (p_in_data_size >= (idx + Constants.SCRIPT_FILE_MODE_SIZE_LEN))
                        {
                            switch (p_in_data[idx])
                            {
                                case Constants.SCRIPT_MODE_ONE_MODE:
                                    p_out_mode = Constants.SCRIPT_MODE_ONE_MODE;
                                    break;
                                case Constants.SCRIPT_MODE_LOOP_MODE:
                                    p_out_mode = Constants.SCRIPT_MODE_LOOP_MODE;
                                    break;
                                case Constants.SCRIPT_MODE_FIRE_MODE:
                                    p_out_mode = Constants.SCRIPT_MODE_FIRE_MODE;
                                    break;
                                case Constants.SCRIPT_MODE_HOLD_MODE:
                                    p_out_mode = Constants.SCRIPT_MODE_HOLD_MODE;
                                    break;
                                default:
                                    // モードエラー
                                    error_flag = true;
                                    p_out_mode = 0;
                                    break;
                            }

                            idx += Constants.SCRIPT_FILE_MODE_SIZE_LEN;
                        }
                        else
                        {   // サイズエラー
                            error_flag = true;
                        }
                    }

                    // スクリプトサイズは？
                    if (error_flag == false)
                    {
                        int i_size = 0;

                        if (p_in_data_size >= (idx + Constants.SCRIPT_FILE_SCRIPT_SIZE_LEN))
                        {
                            i_size = p_in_data[idx + 3];
                            i_size = (int)(i_size << 8) | (int)p_in_data[idx + 2];
                            i_size = (int)(i_size << 8) | (int)p_in_data[idx + 1];
                            i_size = (int)(i_size << 8) | (int)p_in_data[idx];

                            idx += Constants.SCRIPT_FILE_SCRIPT_SIZE_LEN;
                            p_out_script_size = i_size;
                            if (i_size >= 0)
                            {   //
                                idx += i_size;

                                if (p_in_data_size == idx)
                                {
                                    b_ret = true;
                                }
                            }
                            else
                            {   // スクリプトサイズエラー
                                error_flag = true;
                            }
                        }
                        else
                        {   // サイズエラー
                            error_flag = true;
                        }
                    }
                }
                else
                {   // 最低の長さ以下
                    error_flag = true;
                }

                if (error_flag == true)
                {
                    // 出力用変数を初期化
                    p_out_name_size = 0;
                    p_out_name = "";
                    p_out_script_size = 0;
                    p_out_mode = 0;
                }

            }
            catch
            {
            }
            return b_ret;
        }

        /// <summary>
        /// Byte配列からArrayListへ変換
        /// </summary>
        /// <param name="p_in_data"></param>
        /// <param name="p_in_offset"></param>
        /// <param name="p_in_copy_size"></param>
        /// <param name="p_out_data"></param>
        private void my_Convert_Byte_Array_2_Array_List(byte[] p_in_data, int p_in_offset, int p_in_copy_size, ref ArrayList p_out_data)
        {
            try
            {
                // 出力バッファクリア
                p_out_data.Clear();

                // パラメータチェック
                if (p_in_offset >= 0 && p_in_copy_size > 0)
                {
                    if (p_in_data.Length >= (p_in_offset + p_in_copy_size))
                    {
                        for (int fi = p_in_offset; fi < (p_in_offset + p_in_copy_size); fi++)
                        {
                            p_out_data.Add(p_in_data[fi]);
                        }
                        //p_out_data.AddRange(p_in_data);
                    }
                }
            }
            catch
            {
            }
        }
        /// <summary>
        /// マクロ編集画面の編集アイコンドラッグ時の矢印表示設定
        /// </summary>
        /// <param name="p_icon_id"></param>
        /// <param name="p_disp"></param>
        private void my_arrow_icon_disp(int p_icon_id, bool p_disp)
        {
            try
            {
                if (0 <= p_icon_id && p_icon_id < my_Script_Icon_arrow_Idx.Length)
                {
                    lbl_Arrow_Icons[my_Script_Icon_arrow_Idx[p_icon_id]].Visible = p_disp;
                }
            }
            catch
            {
            }
        }
        private void my_arrow_icon_all_disp(bool p_disp)
        {
            try
            {
                //for (int fi = 0; fi < lbl_Arrow_Icons.Length; fi++ )
                //{
                //    lbl_Arrow_Icons[fi].Visible = p_disp;
                //}
                lbl_Arrow_Icons[3].Visible = p_disp;
            }
            catch
            {
            }
        }
        private void my_script_add_info_joystick_button(bool p_disp_visible, byte p_button_no)
        {
            try
            {
                if(p_disp_visible == true)
                {
                    lbl_Script_Add_Info_JoysticButton_Msg.Text = RevOmate.Properties.Resources.SCRIPT_ADD_INFO_JOY_BUTTON_TEXT;
                    lbl_Script_Add_Info_JoysticButton_Set.Text = RevOmate.Properties.Resources.BTN_SETUP;

                    for (int fi = 0; fi < my_rbtn_button_set.Length; fi++)
                    {
                        if (fi == p_button_no)
                        {
                            my_rbtn_button_set[fi].Checked = true;
                        }
                        else
                        {
                            my_rbtn_button_set[fi].Checked = false;
                        }
                    }

                    gbx_Script_Add_Info_JoysticButton.Visible = true;
                }
                else
                {
                    gbx_Script_Add_Info_JoysticButton.Visible = false;
                }
            }
            catch
            {
            }
        }
        private void my_script_add_info_joystick_lever(bool p_disp_visible, byte p_lever_x, byte p_lever_y)
        {
            try
            {
                if (p_disp_visible == true)
                {
                    lbl_Script_Add_Info_JoysticLever_Msg.Text = RevOmate.Properties.Resources.SCRIPT_ADD_INFO_JOY_LEVER_TEXT;
                    lbl_Script_Add_Info_JoysticLever_X.Text = RevOmate.Properties.Resources.SCRIPT_ADD_INFO_JOY_LEVER_X_TEXT;
                    lbl_Script_Add_Info_JoysticLever_Y.Text = RevOmate.Properties.Resources.SCRIPT_ADD_INFO_JOY_LEVER_Y_TEXT;
                    lbl_Script_Add_Info_JoysticLever_Set.Text = RevOmate.Properties.Resources.BTN_SETUP;

                    sbyte[] sb_lever = new sbyte[2];
                    if (p_lever_x > 0x7F)
                    {
                        sb_lever[0] = (sbyte)(p_lever_x - 0x100);
                    }
                    else
                    {
                        sb_lever[0] = (sbyte)(p_lever_x);
                    }
                    if (p_lever_y > 0x7F)
                    {
                        sb_lever[1] = (sbyte)(p_lever_y - 0x100);
                    }
                    else
                    {
                        sb_lever[1] = (sbyte)(p_lever_y);
                    }
                    txtbx_Script_Add_Info_JoysticLever_X.Text = sb_lever[0].ToString();
                    txtbx_Script_Add_Info_JoysticLever_Y.Text = sb_lever[1].ToString();

                    //for (int fi = 0; fi < my_rbtn_button_set.Length; fi++)
                    //{
                    //    if (fi == p_button_no)
                    //    {
                    //        my_rbtn_button_set[fi].Checked = true;
                    //    }
                    //    else
                    //    {
                    //        my_rbtn_button_set[fi].Checked = false;
                    //    }
                    //}
                    txtbx_Script_Add_Info_JoysticLever_X.Focus();
                    txtbx_Script_Add_Info_JoysticLever_X.SelectAll();
                    //txtbx_Script_Add_Info_JoysticLever_Y.Focus();
                    txtbx_Script_Add_Info_JoysticLever_Y.SelectAll();

                    gbx_Script_Add_Info_JoysticLever.Visible = true;
                }
                else
                {
                    gbx_Script_Add_Info_JoysticLever.Visible = false;
                }
            }
            catch
            {
            }
        }
        private void my_script_add_info_mouse_button(bool p_disp_visible, byte p_button_no)
        {
            try
            {
                if (p_disp_visible == true)
                {
                    lbl_Script_Add_Info_Mouse_Msg.Text = RevOmate.Properties.Resources.SCRIPT_ADD_INFO_MOUSE_BUTTON_TEXT;
                    lbl_Script_Add_Info_Mouse_Set.Text = RevOmate.Properties.Resources.BTN_SETUP;

                    //my_rbtn_mouse_button_set
                    for (int fi = 0; fi < my_rbtn_mouse_button_set.Length; fi++)
                    {
                        my_rbtn_mouse_button_set[fi].Checked = false;
                    }

                    if (p_button_no == Constants.SCRIPT_COMMAND_L_CLICK
                        || p_button_no == Constants.SCRIPT_COMMAND_L_RELEASE)
                    {
                        rbtn_Mouse01.Checked = true;
                    }
                    else if (p_button_no == Constants.SCRIPT_COMMAND_W_CLICK
                        || p_button_no == Constants.SCRIPT_COMMAND_W_RELEASE)
                    {
                        rbtn_Mouse02.Checked = true;
                    }
                    else if (p_button_no == Constants.SCRIPT_COMMAND_R_CLICK
                        || p_button_no == Constants.SCRIPT_COMMAND_R_RELEASE)
                    {
                        rbtn_Mouse03.Checked = true;
                    }
                    else if (p_button_no == Constants.SCRIPT_COMMAND_B4_CLICK
                        || p_button_no == Constants.SCRIPT_COMMAND_B4_RELEASE)
                    {
                        rbtn_Mouse04.Checked = true;
                    }
                    else if (p_button_no == Constants.SCRIPT_COMMAND_B5_CLICK
                        || p_button_no == Constants.SCRIPT_COMMAND_B5_RELEASE)
                    {
                        rbtn_Mouse05.Checked = true;
                    }
                    gbx_Script_Add_Info_Mouse.Visible = true;
                }
                else
                {
                    gbx_Script_Add_Info_Mouse.Visible = false;
                }
            }
            catch
            {
            }
        }
        private void my_script_add_info_mouse_move(bool p_disp_visible, byte p_x, byte p_y)
        {
            try
            {
                if (p_disp_visible == true)
                {
                    lbl_Script_Add_Info_JoysticLever_Msg.Text = RevOmate.Properties.Resources.SCRIPT_ADD_INFO_MOUSE_MOVE_TEXT;
                    lbl_Script_Add_Info_JoysticLever_X.Text = RevOmate.Properties.Resources.SCRIPT_ADD_INFO_MOUSE_MOVE_X_TEXT;
                    lbl_Script_Add_Info_JoysticLever_Y.Text = RevOmate.Properties.Resources.SCRIPT_ADD_INFO_MOUSE_MOVE_Y_TEXT;
                    lbl_Script_Add_Info_JoysticLever_Set.Text = RevOmate.Properties.Resources.BTN_SETUP;

                    sbyte[] sb_lever = new sbyte[2];
                    if (p_x > 0x7F)
                    {
                        sb_lever[0] = (sbyte)(p_x - 0x100);
                    }
                    else
                    {
                        sb_lever[0] = (sbyte)(p_x);
                    }
                    if (p_y > 0x7F)
                    {
                        sb_lever[1] = (sbyte)(p_y - 0x100);
                    }
                    else
                    {
                        sb_lever[1] = (sbyte)(p_y);
                    }
                    txtbx_Script_Add_Info_JoysticLever_X.Text = sb_lever[0].ToString();
                    txtbx_Script_Add_Info_JoysticLever_Y.Text = sb_lever[1].ToString();

                    //for (int fi = 0; fi < my_rbtn_button_set.Length; fi++)
                    //{
                    //    if (fi == p_button_no)
                    //    {
                    //        my_rbtn_button_set[fi].Checked = true;
                    //    }
                    //    else
                    //    {
                    //        my_rbtn_button_set[fi].Checked = false;
                    //    }
                    //}
                    txtbx_Script_Add_Info_JoysticLever_X.Focus();
                    txtbx_Script_Add_Info_JoysticLever_X.SelectAll();
                    //txtbx_Script_Add_Info_JoysticLever_Y.Focus();
                    txtbx_Script_Add_Info_JoysticLever_Y.SelectAll();

                    gbx_Script_Add_Info_JoysticLever.Visible = true;
                }
                else
                {
                    gbx_Script_Add_Info_JoysticLever.Visible = false;
                }
            }
            catch
            {
            }
        }
        private void my_script_add_info_multimedia_key(bool p_disp_visible, byte p_button_no)
        {
            try
            {
                if(p_disp_visible == true)
                {
                    lbl_Script_Add_Info_MultiMedia_Msg.Text = RevOmate.Properties.Resources.SCRIPT_ADD_INFO_MM_BUTTON_TEXT;
                    lbl_Script_Add_Info_MultiMedia_Set.Text = RevOmate.Properties.Resources.BTN_SETUP;

                    for (int fi = 0; fi < my_rbtn_multimedia_key.Length; fi++)
                    {
                        if (fi == p_button_no)
                        {
                            my_rbtn_multimedia_key[fi].Checked = true;
                        }
                        else
                        {
                            my_rbtn_multimedia_key[fi].Checked = false;
                        }
                    }

                    gbx_Script_Add_Info_MultiMedia.Visible = true;
                }
                else
                {
                    gbx_Script_Add_Info_MultiMedia.Visible = false;
                }
            }
            catch
            {
            }
        }
    }
}
