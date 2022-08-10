//-------------------------------------------------------------------------------------------------------------------------------------------------------------------
//-------------------------------------------------------BEGIN CUT AND PASTE BLOCK-----------------------------------------------------------------------------------
/********************************************************************
 FileName:		Form1.cs
 Dependencies:	When compiled, needs .NET framework 2.0 redistributable to run.
 Hardware:		Need a free USB port to connect USB peripheral device
				programmed with appropriate Generic HID firmware.  VID and
				PID in firmware must match the VID and PID in this
				program.
 Compiler:  	Microsoft Visual C# 2005 Express Edition (or better)
 Company:		Microchip Technology, Inc.

 Software License Agreement:

 The software supplied herewith by Microchip Technology Incorporated
 (the 鼎ompany・ for its PICｮ Microcontroller is intended and
 supplied to you, the Company痴 customer, for use solely and
 exclusively with Microchip PIC Microcontroller products. The
 software is owned by the Company and/or its supplier, and is
 protected under applicable copyright laws. All rights are reserved.
 Any use in violation of the foregoing restrictions may subject the
 user to criminal sanctions under applicable laws, as well as to
 civil liability for the breach of the terms and conditions of this
 license.

 THIS SOFTWARE IS PROVIDED IN AN 鄭S IS・CONDITION. NO WARRANTIES,
 WHETHER EXPRESS, IMPLIED OR STATUTORY, INCLUDING, BUT NOT LIMITED
 TO, IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A
 PARTICULAR PURPOSE APPLY TO THIS SOFTWARE. THE COMPANY SHALL NOT,
 IN ANY CIRCUMSTANCES, BE LIABLE FOR SPECIAL, INCIDENTAL OR
 CONSEQUENTIAL DAMAGES, FOR ANY REASON WHATSOEVER.

********************************************************************
 File Description:

 Change History:
  Rev   Date         Description
  2.5a	07/17/2009	 Initial Release.  Ported from HID PnP Demo
                     application source, which was originally 
                     written in MSVC++ 2005 Express Edition.
********************************************************************
NOTE:	All user made code contained in this project is in the Form1.cs file.
		All other code and files were generated automatically by either the
		new project wizard, or by the development environment (ex: code is
		automatically generated if you create a new button on the form, and
		then double click on it, which creates a click event handler
		function).  User developed code (code not developed by the IDE) has
        been marked in "cut and paste blocks" to make it easier to identify.
********************************************************************/

//NOTE: In order for this program to "find" a USB device with a given VID and PID, 
//both the VID and PID in the USB device descriptor (in the USB firmware on the 
//microcontroller), as well as in this PC application source code, must match.
//To change the VID/PID in this PC application source code, scroll down to the 
//CheckIfPresentAndGetUSBDevicePath() function, and change the line that currently
//reads:

//   String DeviceIDToFind = "Vid_04d8&Pid_003f";


//NOTE 2: This C# program makes use of several functions in setupapi.dll and
//other Win32 DLLs.  However, one cannot call the functions directly in a 
//32-bit DLL if the project is built in "Any CPU" mode, when run on a 64-bit OS.
//When configured to build an "Any CPU" executable, the executable will "become"
//a 64-bit executable when run on a 64-bit OS.  On a 32-bit OS, it will run as 
//a 32-bit executable, and the pointer sizes and other aspects of this 
//application will be compatible with calling 32-bit DLLs.

//Therefore, on a 64-bit OS, this application will not work unless it is built in
//"x86" mode.  When built in this mode, the exectuable always runs in 32-bit mode
//even on a 64-bit OS.  This allows this application to make 32-bit DLL function 
//calls, when run on either a 32-bit or 64-bit OS.

//By default, on a new project, C# normally wants to build in "Any CPU" mode.  
//To switch to "x86" mode, open the "Configuration Manager" window.  In the 
//"Active solution platform:" drop down box, select "x86".  If this option does
//not appear, select: "<New...>" and then select the x86 option in the 
//"Type or select the new platform:" drop down box.  

//-------------------------------------------------------END CUT AND PASTE BLOCK-------------------------------------------------------------------------------------
//-------------------------------------------------------------------------------------------------------------------------------------------------------------------



using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using System.Globalization;

namespace RevOmate
{
    public partial class Form1 : Form
    {
#if DEBUG
        internal const bool __DEBUG_FLAG__ = true;    // デバッグ時
#else
        internal const bool __DEBUG_FLAG__ = false;     // リリース時
#endif

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //-------------------------------------------------------BEGIN CUT AND PASTE BLOCK-----------------------------------------------------------------------------------

        //Constant definitions from setupapi.h, which we aren't allowed to include directly since this is C#
        internal const uint DIGCF_PRESENT = 0x02;
        internal const uint DIGCF_DEVICEINTERFACE = 0x10;
        //Constants for CreateFile() and other file I/O functions
        internal const short FILE_ATTRIBUTE_NORMAL = 0x80;
        internal const short INVALID_HANDLE_VALUE = -1;
        internal const uint GENERIC_READ = 0x80000000;
        internal const uint GENERIC_WRITE = 0x40000000;
        internal const uint CREATE_NEW = 1;
        internal const uint CREATE_ALWAYS = 2;
        internal const uint OPEN_EXISTING = 3;
        internal const uint FILE_SHARE_READ = 0x00000001;
        internal const uint FILE_SHARE_WRITE = 0x00000002;
        //Constant definitions for certain WM_DEVICECHANGE messages
        internal const uint WM_DEVICECHANGE = 0x0219;
        internal const uint DBT_DEVICEARRIVAL = 0x8000;
        internal const uint DBT_DEVICEREMOVEPENDING = 0x8003;
        internal const uint DBT_DEVICEREMOVECOMPLETE = 0x8004;
        internal const uint DBT_CONFIGCHANGED = 0x0018;
        //Other constant definitions
        internal const uint DBT_DEVTYP_DEVICEINTERFACE = 0x05;
        internal const uint DEVICE_NOTIFY_WINDOW_HANDLE = 0x00;
        internal const uint ERROR_SUCCESS = 0x00;
        internal const uint ERROR_NO_MORE_ITEMS = 0x00000103;
        internal const uint SPDRP_HARDWAREID = 0x00000001;

        //Various structure definitions for structures that this code will be using
        internal struct SP_DEVICE_INTERFACE_DATA
        {
            internal uint cbSize;               //DWORD
            internal Guid InterfaceClassGuid;   //GUID
            internal uint Flags;                //DWORD
            internal uint Reserved;             //ULONG_PTR MSDN says ULONG_PTR is "typedef unsigned __int3264 ULONG_PTR;"  
        }

        internal struct SP_DEVICE_INTERFACE_DETAIL_DATA
        {
            internal uint cbSize;               //DWORD
            internal char[] DevicePath;         //TCHAR array of any size
        }
        
        internal struct SP_DEVINFO_DATA
        {
            internal uint cbSize;       //DWORD
            internal Guid ClassGuid;    //GUID
            internal uint DevInst;      //DWORD
            internal uint Reserved;     //ULONG_PTR  MSDN says ULONG_PTR is "typedef unsigned __int3264 ULONG_PTR;"  
        }

        internal struct DEV_BROADCAST_DEVICEINTERFACE
        {
            internal uint dbcc_size;            //DWORD
            internal uint dbcc_devicetype;      //DWORD
            internal uint dbcc_reserved;        //DWORD
            internal Guid dbcc_classguid;       //GUID
            internal char[] dbcc_name;          //TCHAR array
        }

        internal struct STR_BASE_INFO
        {
            internal byte mode;             // モード（未使用）
            internal byte led_sleep;        // LEDスリープ設定
            internal byte led_light_mode;   // LED点灯設定 MODE
            internal byte led_light_func;   // LED点灯設定 FUNC
            internal byte led_off_time;     // LED消灯までの時間
            internal byte encoder_typematic;    // エンコーダ　タイプマティック
            internal void init_data()
            {
                mode = 0;
                led_sleep = 0;
                led_light_mode = 0;
                led_light_func = 0;
                led_off_time = 3;
                encoder_typematic = 0;
            }
        }
        internal struct STR_BASE_MODE_INFO
        {
            internal byte[] sw_exe_script_no;   // SW 実行スクリプト番号
            internal byte[] sw_sp_func_no;      // SW 特殊機能番号
            internal byte encoder_func_no;      // エンコーダーデフォルト機能番号
            internal byte LED_color_no;         // LEDカラー番号
            internal byte LED_color_detail_flag;     // LEDカラー詳細フラグ
            internal byte[] LED_RGB_duty;       // LED RGB Duty
            internal byte LED_brightness_level; // LED輝度
            internal void init_data(int sw_num, int rgb_data_len)
            {
                sw_exe_script_no = new byte[sw_num];
                sw_sp_func_no = new byte[sw_num];
                for (int fi = 0; fi < sw_num; fi++ )
                {
                    sw_exe_script_no[fi] = 0;
                    sw_sp_func_no[fi] = 0;
                }

                encoder_func_no = 0;
                LED_color_no = 0;
                LED_color_detail_flag = 0;
                LED_RGB_duty = new byte[rgb_data_len];
                for (int fi = 0; fi < rgb_data_len; fi++)
                {
                    LED_RGB_duty[fi] = 0;
                }
                LED_brightness_level = 0;
            }
        }
        public struct STR_BASE_MODE_INFOS
        {
            internal STR_BASE_MODE_INFO[] base_mode_infos;
            internal void init_data(int p_mode_num, int p_sw_num, int p_rgb_data_len)
            {
                base_mode_infos = new STR_BASE_MODE_INFO[p_mode_num];
                for (int fi = 0; fi < p_mode_num; fi++)
                {
                    base_mode_infos[fi].init_data(p_sw_num, p_rgb_data_len);
                }
            }
        }
        internal struct STR_FUNC_INFO
        {
            internal byte[,] cw_ccw_data;       // 左右回転データ
            internal byte LED_color_no;         // LEDカラー番号
            internal byte LED_color_detail_flag;     // LEDカラー詳細フラグ
            internal byte[] LED_RGB_duty;       // LED RGB Duty
            internal byte LED_brightness_level; // LED輝度
            internal byte func_name_size;    //パターン名称サイズ
            internal string func_name;       //パターン名称
            internal void init_data(int cw_ccw_num, int data_len, int rgb_data_len)
            {
                cw_ccw_data = new byte[cw_ccw_num,data_len];
                LED_RGB_duty = new byte[rgb_data_len];
                for (int fi = 0; fi < cw_ccw_num; fi++)
                {
                    for (int fj = 0; fj < data_len; fj++)
                    {
                        cw_ccw_data[fi, fj] = 0;
                    }
                }
                LED_color_no = 0;
                LED_color_detail_flag = 0;
                for (int fi = 0; fi < rgb_data_len; fi++)
                {
                    LED_RGB_duty[fi] = 0;
                }
                LED_brightness_level = 0;
                func_name_size = 0;
                func_name = "";
            }
            internal void clear()
            {
                for (int fi = 0; fi <= cw_ccw_data.GetUpperBound(0); fi++)
                {
                    for (int fj = 0; fj <= cw_ccw_data.GetUpperBound(1); fj++)
                    {
                        cw_ccw_data[fi, fj] = 0;
                    }
                }
                LED_color_no = 0;
                LED_color_detail_flag = 0;
                for (int fi = 0; fi < LED_RGB_duty.Length; fi++)
                {
                    LED_RGB_duty[fi] = 0;
                }
                LED_brightness_level = 0;
                func_name_size = 0;
                func_name = "";
            }
        }
        internal struct STR_FUNC_DATA
        {
            internal STR_FUNC_INFO[] func_data;
            internal void init_data(byte p_func_num, int p_cw_ccw_num, byte p_cw_ccw_data_len, int p_led_rgb_data_len)
            {
                func_data = new STR_FUNC_INFO[p_func_num];
                for (int fi = 0; fi < p_func_num; fi++)
                {
                    func_data[fi].init_data(p_cw_ccw_num, p_cw_ccw_data_len, p_led_rgb_data_len);
                }
            }
        }
        public struct STR_FUNC_DATAS
        {
            internal STR_FUNC_DATA[] func_datas;
            internal void init_data(int p_mode_num, byte p_func_num, int p_cw_ccw_num, byte p_cw_ccw_data_len, int p_led_rgb_data_len)
            {
                func_datas = new STR_FUNC_DATA[p_mode_num];
                for (int fi = 0; fi < p_mode_num; fi++)
                {
                    func_datas[fi].init_data(p_func_num, p_cw_ccw_num, p_cw_ccw_data_len, p_led_rgb_data_len);
                }
            }
            internal void clear(int p_mode_idx, int p_func_idx)
            {
                if (0 <= p_mode_idx && p_mode_idx < func_datas.Length)
                {
                    if (0 <= p_func_idx && p_func_idx < func_datas[p_mode_idx].func_data.Length)
                    {
                        func_datas[p_mode_idx].func_data[p_mode_idx].clear();
                    }
                }
            }
            internal void all_clear()
            {
                for (int fi = 0; fi < func_datas.Length; fi++)
                {
                    for (int fj = 0; fj < func_datas[fi].func_data.Length; fj++)
                    {
                        clear(fi, fj);
                    }
                }
            }
        }

        internal struct STR_SW_FUNC_INFO
        {
            internal byte[] sw_data;       // SW機能データ
            internal void init_data(int p_data_len)
            {
                sw_data = new byte[p_data_len];
                for (int fi = 0; fi < p_data_len; fi++)
                {
                    sw_data[fi] = 0;
                }
            }
            internal void clear()
            {
                for (int fi = 0; fi <= sw_data.GetUpperBound(0); fi++)
                {
                    sw_data[fi] = 0;
                }
            }
        }
        internal struct STR_SW_FUNC_DATA
        {
            internal STR_SW_FUNC_INFO[] sw_func_data;
            internal void init_data(byte p_sw_num, byte p_data_len)
            {
                sw_func_data = new STR_SW_FUNC_INFO[p_sw_num];
                for (int fi = 0; fi < p_sw_num; fi++)
                {
                    sw_func_data[fi].init_data(p_data_len);
                }
            }
        }
        public struct STR_SW_FUNC_DATAS
        {
            internal STR_SW_FUNC_DATA[] sw_func_datas;
            internal void init_data(int p_mode_num, byte p_sw_num, byte p_data_len)
            {
                sw_func_datas = new STR_SW_FUNC_DATA[p_mode_num];
                for (int fi = 0; fi < p_mode_num; fi++)
                {
                    sw_func_datas[fi].init_data(p_sw_num, p_data_len);
                }
            }
            internal void clear(int p_mode_idx, int p_sw_idx)
            {
                if (0 <= p_mode_idx && p_mode_idx < sw_func_datas.Length)
                {
                    if (0 <= p_sw_idx && p_sw_idx < sw_func_datas[p_mode_idx].sw_func_data.Length)
                    {
                        sw_func_datas[p_mode_idx].sw_func_data[p_sw_idx].clear();
                    }
                }
            }
            internal void all_clear()
            {
                for (int fi = 0; fi < sw_func_datas.Length; fi++)
                {
                    for (int fj = 0; fj < sw_func_datas[fi].sw_func_data.Length; fj++)
                    {
                        clear(fi, fj);
                    }
                }
            }
        }


        internal struct STR_APP_SW_INFO
        {
            internal byte[] select_data;    // 選択データ
            internal byte[] data;           // データ
            internal void init_data(int select_data_len, int data_len)
            {
                select_data = new byte[select_data_len];
                data = new byte[data_len];
                for (int fi = 0; fi < select_data_len; fi++)
                {
                    select_data[fi] = 0;
                }
                for (int fi = 0; fi < data_len; fi++)
                {
                    data[fi] = 0;
                }
            }
            internal void clear()
            {
                for (int fi = 0; fi < select_data.Length; fi++)
                {
                    select_data[fi] = 0;
                }
                for (int fi = 0; fi < data.Length; fi++)
                {
                    data[fi] = 0;
                }
            }
            internal void Copy(STR_APP_SW_INFO p_copy_data)
            {
                for (int fi = 0; fi < select_data.Length; fi++)
                {
                    select_data[fi] = p_copy_data.select_data[fi];
                }
                for (int fi = 0; fi < data.Length; fi++)
                {
                    data[fi] = p_copy_data.data[fi];
                }
            }
        }
        internal struct STR_APP_SW_MODE
        {
            internal STR_APP_SW_INFO[] app_data;
            internal void init_data(byte p_sw_num, int p_select_data_len, int p_data_len)
            {
                if (p_sw_num == Constants.BUTTON_NUM)
                {
                    app_data = new STR_APP_SW_INFO[p_sw_num];
                    for (int fi = 0; fi < app_data.Length; fi++)
                    {
                        app_data[fi].init_data(p_select_data_len, p_data_len);
                    }
                }
            }
            internal void Copy(STR_APP_SW_MODE p_app_sw_mode)
            {
                for (int fi = 0; fi < p_app_sw_mode.app_data.Length; fi++)
                {
                    app_data[fi].Copy(p_app_sw_mode.app_data[fi]);
                }
            }
        }
        public struct STR_APP_SW_DATAS
        {
            internal STR_APP_SW_MODE[] mode;
            internal void init_data(byte p_mode_num, byte p_sw_num, int p_select_data_len, int p_data_len)
            {
                if (p_mode_num == Constants.MODE_NUM && p_sw_num == Constants.BUTTON_NUM)
                {
                    mode = new STR_APP_SW_MODE[p_mode_num];
                    for (int fi = 0; fi < mode.Length; fi++)
                    {
                        mode[fi].init_data(p_sw_num, p_select_data_len, p_data_len);
                    }
                }
            }
            internal void Copy(STR_APP_SW_DATAS p_app_sw_datas)
            {
                for (int fi = 0; fi < p_app_sw_datas.mode.Length; fi++)
                {
                    mode[fi].Copy(p_app_sw_datas.mode[fi]);
                }
            }
        }
        internal struct STR_APP_FUNC_INFO
        {
            internal byte[] select_data;    // 選択データ
            internal void init_data(int select_data_len)
            {
                select_data = new byte[select_data_len];
                for (int fi = 0; fi < select_data_len; fi++)
                {
                    select_data[fi] = 0;
                }
            }
            internal void clear()
            {
                for (int fi = 0; fi < select_data.Length; fi++)
                {
                    select_data[fi] = 0;
                }
            }
            internal void Copy(STR_APP_FUNC_INFO p_copy_data)
            {
                for (int fi = 0; fi < select_data.Length; fi++)
                {
                    select_data[fi] = p_copy_data.select_data[fi];
                }
            }
            internal bool DataDiff(STR_APP_FUNC_INFO p_datas)
            {
                bool ret_val = false;
                for (int fi = 0; fi < select_data.Length; fi++)
                {
                    if (select_data[fi] != p_datas.select_data[fi])
                    {
                        ret_val = true;
                        break;
                    }
                }
                return ret_val;
            }
        }
        public struct STR_APP_FUNC_CWCCW
        {
            internal STR_APP_FUNC_INFO app_data;
            internal void init_data(int p_select_data_len)
            {
                app_data = new STR_APP_FUNC_INFO();
                app_data.init_data(p_select_data_len);
            }
            internal void Copy(STR_APP_FUNC_CWCCW p_app_func_datas)
            {
                app_data.Copy(p_app_func_datas.app_data);
            }
            internal bool DataDiff(STR_APP_FUNC_CWCCW p_app_func_datas)
            {
                bool ret_val = false;
                if (app_data.DataDiff(p_app_func_datas.app_data) == true)
                {
                    ret_val = true;
                }
                return ret_val;
            }
        }
        public struct STR_APP_FUNC_FUNC
        {
            internal STR_APP_FUNC_CWCCW[] cwccw;
            internal void init_data(byte p_cw_ccw_num, int p_select_data_len)
            {
                if (p_cw_ccw_num == Constants.CW_CCW_NUM)
                {
                    cwccw = new STR_APP_FUNC_CWCCW[p_cw_ccw_num];
                    for (int fi = 0; fi < cwccw.Length; fi++)
                    {
                        cwccw[fi].init_data(p_select_data_len);
                    }
                }
            }
            internal void Copy(STR_APP_FUNC_FUNC p_app_func_func)
            {
                for (int fi = 0; fi < cwccw.Length; fi++)
                {
                    cwccw[fi].Copy(p_app_func_func.cwccw[fi]);
                }
            }
            internal bool DataDiff(STR_APP_FUNC_FUNC p_app_func_func)
            {
                bool ret_val = false;
                for (int fi = 0; fi < cwccw.Length; fi++)
                {
                    if (cwccw[fi].DataDiff(p_app_func_func.cwccw[fi]) == true)
                    {
                        ret_val = true;
                        break;
                    }
                }
                return ret_val;
            }
        }
        public struct STR_APP_FUNC_MODE
        {
            internal STR_APP_FUNC_FUNC[] func;
            internal void init_data(byte p_func_num, byte p_cw_ccw_num, int p_select_data_len)
            {
                if (p_func_num == Constants.FUNCTION_NUM && p_cw_ccw_num == Constants.CW_CCW_NUM)
                {
                    func = new STR_APP_FUNC_FUNC[p_func_num];
                    for (int fi = 0; fi < func.Length; fi++)
                    {
                        func[fi].init_data(p_cw_ccw_num, p_select_data_len);
                    }
                }
            }
            internal void Copy(STR_APP_FUNC_MODE p_app_func_mode)
            {
                for (int fi = 0; fi < func.Length; fi++)
                {
                    func[fi].Copy(p_app_func_mode.func[fi]);
                }
            }
            internal bool DataDiff(STR_APP_FUNC_MODE p_app_func_mode)
            {
                bool ret_val = false;
                for (int fi = 0; fi < func.Length; fi++)
                {
                    if (func[fi].DataDiff(p_app_func_mode.func[fi]) == true)
                    {
                        ret_val = true;
                        break;
                    }
                }
                return ret_val;
            }
        }
        public struct STR_APP_FUNC_DATAS
        {
            internal STR_APP_FUNC_MODE[] mode;
            internal void init_data(byte p_mode_num, byte p_func_num, byte p_cw_ccw_num, int p_select_data_len)
            {
                if (p_mode_num == Constants .MODE_NUM && p_func_num == Constants.FUNCTION_NUM && p_cw_ccw_num == Constants.CW_CCW_NUM)
                {
                    mode = new STR_APP_FUNC_MODE[p_mode_num];
                    for (int fi = 0; fi < mode.Length; fi++)
                    {
                        mode[fi].init_data(p_func_num, p_cw_ccw_num, p_select_data_len);
                    }
                }
            }
            internal void Copy(STR_APP_FUNC_DATAS p_app_func_datas)
            {
                for (int fi = 0; fi < mode.Length; fi++)
                {
                    mode[fi].Copy(p_app_func_datas.mode[fi]);
                }
            }
            internal bool DataDiff(STR_APP_FUNC_DATAS p_app_func_datas)
            {
                bool ret_val = false;
                for (int fi = 0; fi < mode.Length; fi++)
                {
                    if (mode[fi].DataDiff(p_app_func_datas.mode[fi]) == true)
                    {
                        ret_val = true;
                        break;

                    }
                }
                return ret_val;
            }
        }
#if false
        public struct STR_APP_DATAS
        {
            internal STR_APP_SW_DATAS[] app_sw_datas;
            internal STR_APP_FUNC_DATAS[] app_func_datas;
            internal void init_data(int p_mode_num, byte p_sw_num, byte p_func_num, int p_select_data_len, int p_data_len)
            {
                if (p_mode_num == Constants.MODE_NUM)
                {
                    app_sw_datas = new STR_APP_SW_DATAS[p_mode_num];
                    for (int fi = 0; fi < p_mode_num; fi++)
                    {
                        app_sw_datas[fi].init_data(p_sw_num, p_select_data_len, p_data_len);
                    }
                    app_func_datas = new STR_APP_FUNC_DATAS[p_mode_num];
                    for (int fi = 0; fi < p_mode_num; fi++)
                    {
                        app_func_datas[fi].init_data(p_func_num, p_select_data_len, p_data_len);
                    }
                }
            }
            internal void clear_sw(int p_mode_idx, int p_sw_idx)
            {
                if (0 <= p_mode_idx && p_mode_idx < app_sw_datas.Length)
                {
                    if (0 <= p_sw_idx && p_sw_idx < app_sw_datas[p_mode_idx].app_data.Length)
                    {
                        app_sw_datas[p_mode_idx].app_data[p_sw_idx].clear();
                    }
                }
            }
            internal void clear_func(int p_mode_idx, int p_func_idx)
            {
                if (0 <= p_mode_idx && p_mode_idx < app_func_datas.Length)
                {
                    if (0 <= p_func_idx && p_func_idx < app_func_datas[p_mode_idx].app_data.Length)
                    {
                        app_func_datas[p_mode_idx].app_data[p_func_idx].clear();
                    }
                }
            }
            internal void all_clear()
            {
                for (int fi = 0; fi < Constants.MODE_NUM; fi++)
                {
                    for (int fj = 0; fj < app_sw_datas[fi].app_data.Length; fj++)
                    {
                        clear_sw(fi, fj);
                    }
                    for (int fj = 0; fj < app_func_datas[fi].app_data.Length; fj++)
                    {
                        clear_func(fi, fj);
                    }
                }
            }
            internal void Copy(STR_APP_DATAS p_app_datas)
            {
                for (int fi = 0; fi < app_sw_datas.Length; fi++)
                {
                    for (int fj = 0; fj < app_sw_datas[fi].app_data.Length; fj++)
                    {
                        app_sw_datas[fi].app_data[fj].Copy(p_app_datas.app_sw_datas[fi].app_data[fj]);
                    }
                }
                for (int fi = 0; fi < app_func_datas.Length; fi++)
                {
                    for (int fj = 0; fj < app_func_datas[fi].app_data.Length; fj++)
                    {
                        app_func_datas[fi].app_data[fj].Copy(p_app_datas.app_func_datas[fi].app_data[fj]);
                    }
                }
            }
            internal void CopySWData(STR_APP_SW_INFO p_Copy_Datas, byte p_set_mode, byte p_set_sw)
            {
                if (p_set_mode < Constants.MODE_NUM && p_set_sw < Constants.BUTTON_NUM)
                {
                    app_sw_datas[p_set_mode].app_data[p_set_sw].Copy(p_Copy_Datas);
                }
            }
            internal void CopyFuncData(STR_APP_SW_INFO p_Copy_Datas, byte p_set_mode, byte p_set_func_idx)
            {
                if (p_set_mode < Constants.MODE_NUM && p_set_func_idx < Constants.FUNCTION_NUM)
                {
                    app_func_datas[p_set_mode].app_data[p_set_func_idx].Copy(p_Copy_Datas);
                }
            }
        }
#endif

        internal struct STR_ENCODER_SCRIPT_DATA
        {
            internal byte rec_num;          //記憶スクリプト数
            internal byte loop_flag;        //繰り返し設定
            internal byte[] script_no;      //スクリプトNo.
            internal void init_data(byte p_script_len)
            {
                rec_num = 0;
                loop_flag = 0;
                script_no = new byte[p_script_len];
                for (int fi = 0; fi < script_no.Length; fi++)
                {
                    script_no[fi] = 0;
                }
            }
            internal void clear()
            {
                rec_num = 0;
                loop_flag = 0;
                for (int fi = 0; fi < script_no.Length; fi++)
                {
                    script_no[fi] = 0;
                }
            }
        }
        internal struct STR_ENCODER_SCRIPT_DATAS
        {
            internal STR_ENCODER_SCRIPT_DATA[] encoder_script_datas;
            internal void init_data(int p_encoder_script_num, byte p_script_len)
            {
                encoder_script_datas = new STR_ENCODER_SCRIPT_DATA[p_encoder_script_num];
                for (int fi = 0; fi < p_encoder_script_num; fi++)
                {
                    encoder_script_datas[fi].init_data(p_script_len);
                }
            }
            internal void clear(int p_idx)
            {
                if (0 <= p_idx && p_idx < encoder_script_datas.Length)
                {
                    encoder_script_datas[p_idx].clear();
                }
            }
            internal void all_clear()
            {
                for (int fi = 0; fi < encoder_script_datas.Length; fi++)
                {
                    clear(fi);
                }
            }
        }
        internal struct STR_SCRIPT_INFO
        {
            internal uint check_sum;            //チェックサム
            internal byte Record_Num;           //記録スクリプト数
            internal byte Reserve;              //予備
            internal ulong Total_Size;           //全スクリプトデータサイズ
            internal void init_data()
            {
                check_sum = 0;
                Record_Num = 0;
                Reserve = 0;
                Total_Size = 0;
            }
        }
        internal struct STR_SCRIPT_INFO_DATA
        {
            internal int Recode_Address;        // スクリプトデータ保存アドレス
            internal int Script_Size;           // スクリプトデータサイズ
            internal byte Script_Mode;          // スクリプトモード
            internal byte Name_Size;            // スクリプト名称文字列長
            internal string Name;               // スクリプト名称文字列
            internal void init_data()
            {
                Recode_Address = 0;
                Script_Size = 0;
                Script_Mode = 0;
                Name_Size = 0;
                Name = "";
            }
        }
        public struct STR_SCRIPT_INFO_DATAS
        {
            internal STR_SCRIPT_INFO_DATA[] Script_Info_Datas;         //
            internal void init_data(int script_num)
            {
                Script_Info_Datas = new STR_SCRIPT_INFO_DATA[script_num];
            }
        }

        internal struct STR_APP_SETTING_DATA
        {
            //internal string[] App_File_Path;
            //internal bool[] App_Active_Exe_Flag;
            //internal bool Button_Setting_Change_Flag;
            //internal bool Script_Setting_Change_Flag;
            //internal int Mouse_Memory_Disp_Idx;
            internal int Script_Editor_Disp_Idx;
            //internal int Script_List_Disp_Idx;
            //internal int Mouse_Memory_Select_Idx;
            internal int Script_Editor_Select_Idx;
            //internal int Script_List_Select_Idx;
            internal bool Script_Rec_Flag;
            ////internal DateTime Script_Interval_Start;
            ////internal DateTime Script_Interval_Stop;
            //internal bool Script_Interval_Flag;
            internal int Script_Setting_Drag_Start_Control;
            internal int Script_Setting_Drag_Target_Control;
            internal int Script_Add_Manual_Control;
            internal bool Script_Edit_Item_Change_Flag;
            internal int Script_Drag_Target_idx;
            internal int Backup_Restore_Flag;
            internal string Backup_file_Path;
            internal int Backup_Restore_Progress_Value;
            internal int Backup_Restore_Progress_Max_Value;
            internal int Backup_Restore_Error_Code;
            //internal int Script_Setting_Scroll_Targrt_Ctrl;

            internal void init_data()
            //internal void init_data(byte p_mode_num)
            {
                //App_File_Path = new string[p_mode_num];
                //App_Active_Exe_Flag = new bool[p_mode_num];
                //for (int fi = 0; fi < p_mode_num; fi++)
                //{
                //    App_File_Path[fi] = "";
                //    App_Active_Exe_Flag[fi] = false;
                //}
                //Button_Setting_Change_Flag = false;
                //Script_Setting_Change_Flag = false;
                //Mouse_Memory_Disp_Idx = 0;
                Script_Editor_Disp_Idx = 0;
                //Script_List_Disp_Idx = 0;
                //Mouse_Memory_Select_Idx = -1;
                Script_Editor_Select_Idx = -1;
                //Script_List_Select_Idx = -1;
                Script_Rec_Flag = false;
                //Script_Interval_Flag = false;
                Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                Script_Setting_Drag_Target_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_NON;
                Script_Edit_Item_Change_Flag = false;
                Script_Drag_Target_idx = -1;
                Backup_Restore_Flag = Constants.BACKUP_FLAG_NON;
                Backup_file_Path = "";
                Backup_Restore_Progress_Value = 0;
                Backup_Restore_Progress_Max_Value = 0;
                Backup_Restore_Error_Code = 0;
                //Script_Setting_Scroll_Targrt_Ctrl = Constants.SCRIPT_SCROLL_TARGET_CTRL_NON;
            }
        }

        internal struct STR_FLASH_READ_WRITE_BUFFER
        {
            internal ArrayList al_Read_Address;
            internal ArrayList al_Read_Size;
            internal ArrayList[] al_Read_Data;
            internal ArrayList al_Read_Idx;
            internal byte Script_Mode;
            internal string Script_Name;
            internal int Write_Idx;
            internal ArrayList al_Write_Data;
            internal int Write_Top_Address;

            internal void init_data(int script_num)
            {
                al_Read_Address = new ArrayList();
                al_Read_Size = new ArrayList();
                al_Read_Data = new ArrayList[script_num];
                for (int fi = 0; fi < script_num; fi++)
                {
                    al_Read_Data[fi] = new ArrayList();
                }
                al_Read_Idx = new ArrayList();
                Script_Mode = 0;
                Script_Name = "";
                Write_Idx = 0;
                al_Write_Data = new ArrayList();
                Write_Top_Address = 0;
            }
            internal void clear()
            {
                al_Read_Address.Clear();
                al_Read_Size.Clear();
                for (int fi = 0; fi <= al_Read_Data.GetUpperBound(0); fi++)
                {
                    al_Read_Data[fi].Clear();
                }
                al_Read_Idx.Clear();
                Script_Mode = 0;
                Script_Name = "";
                Write_Idx = 0;
                al_Write_Data.Clear();
                Write_Top_Address = 0;
            }
            internal void set_read_address(int p_address, int p_size, int p_idx)
            {
                al_Read_Address.Add(p_address);
                al_Read_Size.Add(p_size);
                al_Read_Idx.Add(p_idx);
            }
            internal void set_read_data(int p_range, byte p_data)
            {
                if (0 <= p_range && p_range <= al_Read_Data.GetUpperBound(0))
                {
                    al_Read_Data[p_range].Add(p_data);
                }
            }
            internal byte get_read_data(int p_range, int p_index)
            {
                byte b_ret = 0;
                if (0 <= p_range && p_range <= al_Read_Data.GetUpperBound(0)
                    && 0 <= p_index && p_index <= al_Read_Data.GetUpperBound(1))
                {
                    b_ret = (byte)al_Read_Data[p_range][p_index];
                }
                return b_ret;
            }
            internal void set_write_data(ArrayList p_data, int p_idx)
            {
                al_Write_Data = p_data;
                Write_Idx = p_idx;
            }
            internal void set_info(byte p_set_mode, string p_name)
            {
                Script_Mode = p_set_mode;
                Script_Name = p_name;
            }
        }


        //DLL Imports.  Need these to access various C style unmanaged functions contained in their respective DLL files.
        //--------------------------------------------------------------------------------------------------------------
        //Returns a HDEVINFO type for a device information set.  We will need the 
        //HDEVINFO as in input parameter for calling many of the other SetupDixxx() functions.
        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern IntPtr SetupDiGetClassDevs(
            ref Guid ClassGuid,     //LPGUID    Input: Need to supply the class GUID. 
            IntPtr Enumerator,      //PCTSTR    Input: Use NULL here, not important for our purposes
            IntPtr hwndParent,      //HWND      Input: Use NULL here, not important for our purposes
            uint Flags);            //DWORD     Input: Flags describing what kind of filtering to use.

	    //Gives us "PSP_DEVICE_INTERFACE_DATA" which contains the Interface specific GUID (different
	    //from class GUID).  We need the interface GUID to get the device path.
        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern bool SetupDiEnumDeviceInterfaces(
            IntPtr DeviceInfoSet,           //Input: Give it the HDEVINFO we got from SetupDiGetClassDevs()
            IntPtr DeviceInfoData,          //Input (optional)
            ref Guid InterfaceClassGuid,    //Input 
            uint MemberIndex,               //Input: "Index" of the device you are interested in getting the path for.
            ref SP_DEVICE_INTERFACE_DATA DeviceInterfaceData);    //Output: This function fills in an "SP_DEVICE_INTERFACE_DATA" structure.

        //SetupDiDestroyDeviceInfoList() frees up memory by destroying a DeviceInfoList
        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern bool SetupDiDestroyDeviceInfoList(
            IntPtr DeviceInfoSet);          //Input: Give it a handle to a device info list to deallocate from RAM.

        //SetupDiEnumDeviceInfo() fills in an "SP_DEVINFO_DATA" structure, which we need for SetupDiGetDeviceRegistryProperty()
        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern bool SetupDiEnumDeviceInfo(
            IntPtr DeviceInfoSet,
            uint MemberIndex,
            ref SP_DEVINFO_DATA DeviceInterfaceData);

        //SetupDiGetDeviceRegistryProperty() gives us the hardware ID, which we use to check to see if it has matching VID/PID
        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern bool SetupDiGetDeviceRegistryProperty(
            IntPtr DeviceInfoSet,
            ref SP_DEVINFO_DATA DeviceInfoData,
            uint Property,
            ref uint PropertyRegDataType,
            IntPtr PropertyBuffer,
            uint PropertyBufferSize,
            ref uint RequiredSize);

        //SetupDiGetDeviceInterfaceDetail() gives us a device path, which is needed before CreateFile() can be used.
        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern bool SetupDiGetDeviceInterfaceDetail(
            IntPtr DeviceInfoSet,                   //Input: Wants HDEVINFO which can be obtained from SetupDiGetClassDevs()
            ref SP_DEVICE_INTERFACE_DATA DeviceInterfaceData,                    //Input: Pointer to an structure which defines the device interface.  
            IntPtr  DeviceInterfaceDetailData,      //Output: Pointer to a SP_DEVICE_INTERFACE_DETAIL_DATA structure, which will receive the device path.
            uint DeviceInterfaceDetailDataSize,     //Input: Number of bytes to retrieve.
            ref uint RequiredSize,                  //Output (optional): The number of bytes needed to hold the entire struct 
            IntPtr DeviceInfoData);                 //Output (optional): Pointer to a SP_DEVINFO_DATA structure

        //Overload for SetupDiGetDeviceInterfaceDetail().  Need this one since we can't pass NULL pointers directly in C#.
        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern bool SetupDiGetDeviceInterfaceDetail(
            IntPtr DeviceInfoSet,                   //Input: Wants HDEVINFO which can be obtained from SetupDiGetClassDevs()
            ref SP_DEVICE_INTERFACE_DATA DeviceInterfaceData,               //Input: Pointer to an structure which defines the device interface.  
            IntPtr DeviceInterfaceDetailData,       //Output: Pointer to a SP_DEVICE_INTERFACE_DETAIL_DATA structure, which will contain the device path.
            uint DeviceInterfaceDetailDataSize,     //Input: Number of bytes to retrieve.
            IntPtr RequiredSize,                    //Output (optional): Pointer to a DWORD to tell you the number of bytes needed to hold the entire struct 
            IntPtr DeviceInfoData);                 //Output (optional): Pointer to a SP_DEVINFO_DATA structure

        //Need this function for receiving all of the WM_DEVICECHANGE messages.  See MSDN documentation for
        //description of what this function does/how to use it. Note: name is remapped "RegisterDeviceNotificationUM" to
        //avoid possible build error conflicts.
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern IntPtr RegisterDeviceNotification(
            IntPtr hRecipient,
            IntPtr NotificationFilter,
            uint Flags);

        //Takes in a device path and opens a handle to the device.
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern SafeFileHandle CreateFile(
            string lpFileName,
            uint dwDesiredAccess,
            uint dwShareMode, 
            IntPtr lpSecurityAttributes, 
            uint dwCreationDisposition,
            uint dwFlagsAndAttributes, 
            IntPtr hTemplateFile);

        //Uses a handle (created with CreateFile()), and lets us write USB data to the device.
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern bool WriteFile(
            SafeFileHandle hFile,
            byte[] lpBuffer,
            uint nNumberOfBytesToWrite,
            ref uint lpNumberOfBytesWritten,
            IntPtr lpOverlapped);

        //Uses a handle (created with CreateFile()), and lets us read USB data from the device.
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern bool ReadFile(
            SafeFileHandle hFile,
            IntPtr lpBuffer,
            uint nNumberOfBytesToRead,
            ref uint lpNumberOfBytesRead,
            IntPtr lpOverlapped);



	    //--------------- Global Varibles Section ------------------
	    //USB related variables that need to have wide scope.
	    bool AttachedState = false;						//Need to keep track of the USB device attachment status for proper plug and play operation.
	    bool AttachedButBroken = false;
        SafeFileHandle WriteHandleToUSBDevice = null;
        SafeFileHandle ReadHandleToUSBDevice = null;
        String DevicePath = null;   //Need the find the proper device path before you can open file handles.


        bool ConnectFirstTime = true;
        bool UnConnectFirstTime = true;
        bool VersionReadReq = true;
        bool VersionReadComp = false;
        byte[] version_buff = new byte[64];
        bool advance_mode_flag = false;

        int contextMenu_sw_id = 0;
        Bitmap canvas;


        bool b_FlashRead = false;
        bool b_FlashWrite = false;
        bool b_FlashErase = false;
        long l_ReadAddress = -1;
        byte byte_ReadSize = 0;
        byte[] FlashReadData = new byte[64];
        long l_WriteAddress = -1;
        byte byte_WriteSize = 0;
        byte[] FlashWriteData = new byte[64];
        int byte_FlashWrite_Ans = -1;
        long l_EraseAddress = -1;
        int byte_FlashErase_Ans = -1;
        int Script_Rec_Now_Key = 0;

        Label[] lbl_Script_Icons;
        Label[] lbl_Arrow_Icons;
        int[] my_Script_Icon_arrow_Idx;

        Stopwatch sw_Script_Interval = new Stopwatch();

        static STR_APP_SETTING_DATA my_App_Setting_Data;
        static KeyCode const_Key_Code = new KeyCode();

        public SystemSettingInfo system_setting_info = new SystemSettingInfo();
        STR_BASE_INFO my_base_info;
        STR_BASE_MODE_INFOS my_base_mode_infos;
        STR_FUNC_DATAS my_func_datas;
        STR_SW_FUNC_DATAS my_sw_func_datas;
        STR_APP_SW_DATAS my_app_sw_datas;
        STR_APP_FUNC_DATAS my_app_func_datas;
        STR_ENCODER_SCRIPT_DATAS my_encoder_script_datas;
        STR_SCRIPT_INFO my_script_info;
        STR_SCRIPT_INFO_DATAS my_script_info_datas;

        STR_SCRIPT_EDITOR_DISP_DATA my_script_editor_disp_data;
        STR_FLASH_READ_WRITE_BUFFER my_flash_read_write_buffer;

        //STR_MODE_DATA my_mouse_mode_data;

        ComboBox[,] my_cmbbx_set_type;
        NumericUpDown[,] my_encoder_sensitivity;
        TextBox[,] my_keyboard_key;
        CheckBox[,,] my_keyboard_modifier;
        Button[,] my_keyboard_key_clr;
        Label[,] my_lbl_title;
        NumericUpDown[,] my_mouse_x;
        NumericUpDown[,] my_mouse_y;
        NumericUpDown[,] my_joypad_x;
        NumericUpDown[,] my_joypad_y;

        public SetData my_set_data = new SetData();

        // セットタイプ
        string[] set_type_list = new string[] { "なし", "ダイアルマクロ1", "ダイアルマクロ2", "ダイアルマクロ3", "0～9数字連続入力(UP)", "0～9数字連続入力(DOWN)", "キーボード 任意キー",
                                            "マウス 左クリック", "マウス 右クリック", "マウス ホイールクリック", "マウス ボタン4クリック", "マウス ボタン5クリック", "マウス ダブルクリック", "マウス 上下左右移動", "マウス ホイールスクロール",
                                            "マルチメディア 再生","マルチメディア 一時停止","マルチメディア 停止","マルチメディア REC","マルチメディア 早送り","マルチメディア 巻戻し","マルチメディア 次","マルチメディア 前","マルチメディア 消音","マルチメディア ボリュームアップ","マルチメディア ボリュームダウン",
                                            "ジョイパッド 左 X-Y軸", "ジョイパッド 右 X-Y軸",
                                            "ジョイパッド ボタン1", "ジョイパッド ボタン2", "ジョイパッド ボタン3", "ジョイパッド ボタン4", "ジョイパッド ボタン5", "ジョイパッド ボタン6", "ジョイパッド ボタン7", "ジョイパッド ボタン8", "ジョイパッド ボタン9", "ジョイパッド ボタン10", "ジョイパッド ボタン11", "ジョイパッド ボタン12", "ジョイパッド ボタン13",
                                            "ジョイパッド ハットスイッチ上","ジョイパッド ハットスイッチ下","ジョイパッド ハットスイッチ左","ジョイパッド ハットスイッチ右"};
        int[] set_type_no_list = new int[] { Constants.SET_TYPE_NONE, Constants.SET_TYPE_ENCODER_SCRIPT1, Constants.SET_TYPE_ENCODER_SCRIPT2, Constants.SET_TYPE_ENCODER_SCRIPT3, Constants.SET_TYPE_NUMBER_UP, Constants.SET_TYPE_NUMBER_DOWN, Constants.SET_TYPE_KEYBOARD_KEY,
                                            Constants.SET_TYPE_MOUSE_LCLICK, Constants.SET_TYPE_MOUSE_RCLICK, Constants.SET_TYPE_MOUSE_WHCLICK, Constants.SET_TYPE_MOUSE_B4CLICK, Constants.SET_TYPE_MOUSE_B5CLICK, Constants.SET_TYPE_MOUSE_DCLICK, Constants.SET_TYPE_MOUSE_MOVE, Constants.SET_TYPE_MOUSE_WHSCROLL,
                                            Constants.SET_TYPE_MULTIMEDIA_PLAY, Constants.SET_TYPE_MULTIMEDIA_PAUSE, Constants.SET_TYPE_MULTIMEDIA_STOP, Constants.SET_TYPE_MULTIMEDIA_REC, Constants.SET_TYPE_MULTIMEDIA_FORWORD, Constants.SET_TYPE_MULTIMEDIA_REWIND, Constants.SET_TYPE_MULTIMEDIA_NEXT, Constants.SET_TYPE_MULTIMEDIA_PREVIOUS, Constants.SET_TYPE_MULTIMEDIA_MUTE, Constants.SET_TYPE_MULTIMEDIA_VOLUMEUP, Constants.SET_TYPE_MULTIMEDIA_VOLUMEDOWN,
                                            Constants.SET_TYPE_JOYPAD_XY, Constants.SET_TYPE_JOYPAD_ZRZ,
                                            Constants.SET_TYPE_JOYPAD_B01, Constants.SET_TYPE_JOYPAD_B02, Constants.SET_TYPE_JOYPAD_B03, Constants.SET_TYPE_JOYPAD_B04, Constants.SET_TYPE_JOYPAD_B05, Constants.SET_TYPE_JOYPAD_B06, Constants.SET_TYPE_JOYPAD_B07, Constants.SET_TYPE_JOYPAD_B08, Constants.SET_TYPE_JOYPAD_B09, Constants.SET_TYPE_JOYPAD_B10, Constants.SET_TYPE_JOYPAD_B11, Constants.SET_TYPE_JOYPAD_B12, Constants.SET_TYPE_JOYPAD_B13,
                                            Constants.SET_TYPE_JOYPAD_HSW_NORTH, Constants.SET_TYPE_JOYPAD_HSW_SOUTH, Constants.SET_TYPE_JOYPAD_HSW_WEST, Constants.SET_TYPE_JOYPAD_HSW_EAST
                                            };
        //string[] device_type_disp = new string[] { "", "Mouse", "Keyboard", "Volume" };

        public string[] keyboard_modifier_name = new string[] { "Ctrl L", "Shift L", "Alt L", "Win L", "Ctrl R", "Shift R", "Alt R", "Win R" };
        public byte[] keyboard_modifier_bit = new byte[] { 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80 };
        // モディファイ対象キー [左CTRL,左SHIFT,左ALT,左WIN,右CTRL,右SHIFT,右ALT,右WIN]
        byte[,] keyborad_modifier_data = new byte[,] {  { Constants.USB_KEY_CODE_CTRL_L, Constants.USB_KEY_BIT_CTRL_L }, { Constants.USB_KEY_CODE_SHIFT_L, Constants.USB_KEY_BIT_SHIFT_L }, { Constants.USB_KEY_CODE_ALT_L, Constants.USB_KEY_BIT_ALT_L }, { Constants.USB_KEY_CODE_WIN_L, Constants.USB_KEY_BIT_WIN_L },
                                                        { Constants.USB_KEY_CODE_CTRL_R, Constants.USB_KEY_BIT_CTRL_R }, { Constants.USB_KEY_CODE_SHIFT_R, Constants.USB_KEY_BIT_SHIFT_R }, { Constants.USB_KEY_CODE_ALT_R, Constants.USB_KEY_BIT_ALT_R }, { Constants.USB_KEY_CODE_WIN_R, Constants.USB_KEY_BIT_WIN_R } };
        // マルチメディアキーセットタイプ 11種
        string[] multimedia_set_type_name = new string[] { "再生", "一時停止", "停止", "REC", "早送り", "巻戻し", "次", "前", "消音", "ボリュームアップ", "ボリュームダウン" };
        byte[] multimedia_set_type = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B };
        // マウスセットタイプ 5種
        string[] mouse_set_type_name = new string[] { "左クリック", "ホイールクリック", "右クリック", "ボタン4クリック", "ボタン5クリック"};
        byte[] mouse_set_type = new byte[] { Constants.MOUSE_DATA_LEFT_CLICK, Constants.MOUSE_DATA_WHEEL_CLICK, Constants.MOUSE_DATA_RIGHT_CLICK, Constants.MOUSE_DATA_BUTTON4_CLICK, Constants.MOUSE_DATA_BUTTON5_CLICK };
        // 特殊機能
        string[] sp_func_type_list = new string[] { "モード変更", "モード1に変更", "モード2に変更", "モード3に変更", "エンコーダー機能1に変更", "エンコーダー機能2に変更", "エンコーダー機能3に変更", "エンコーダー機能4に変更", "エンコーダー機能1に変更（押している間）", "エンコーダー機能2に変更（押している間）", "エンコーダー機能3に変更（押している間）", "エンコーダー機能4に変更（押している間）", "エンコーダー機能変更" };
        byte[] sp_func_type_no_list = new byte[] { Constants.SP_FUNC_MODE, Constants.SP_FUNC_MODE1, Constants.SP_FUNC_MODE2, Constants.SP_FUNC_MODE3,
                                                    Constants.SP_FUNC_FUNC1, Constants.SP_FUNC_FUNC2, Constants.SP_FUNC_FUNC3, Constants.SP_FUNC_FUNC4,
                                                    Constants.SP_FUNC_FUNC1TEMP, Constants.SP_FUNC_FUNC2TEMP, Constants.SP_FUNC_FUNC3TEMP, Constants.SP_FUNC_FUNC4TEMP, Constants.SP_FUNC_FUNC};

        Label[] my_lbl_LED_Color_set_button;
        TrackBar[] my_tb_LED_Duty_set_button;
        RadioButton[] my_rbtn_LED_Brightness_Level_set_button;
        Label[,] my_lbl_LED_Color_set_func;
        Label[] my_lbl_LED_Color_now_func;
        TrackBar[,] my_tb_LED_Duty_set_func;
        RadioButton[,] my_rbtn_LED_Brightness_Level_set_func;

        int function_setting_mode_select_no = Constants.MODE_1_ID;
        int button_setting_mode_select_no = Constants.MODE_1_ID;
        int encoder_script_select_no = 0;

        byte[] my_LED_Duty_Max = new byte[] { Constants.LED_R_DUTY_MAX, Constants.LED_G_DUTY_MAX, Constants.LED_B_DUTY_MAX };
        byte[,] my_LED_Color_default = new byte[Constants.LED_COLOR_DEFAULT_SET_NUM, Constants.LED_RGB_COLOR_NUM] {
                                                        {   0,  0,  0   },
                                                        {   60, 60, 50  },
                                                        {   50, 0,  0   },
                                                        {   50, 10, 0   },
                                                        {   50, 35, 0   },
                                                        {   0,  50, 25  },
                                                        {   0,  50, 0   },
                                                        {   0,  0,  60  },
                                                        {   30, 5,  60  }};
        byte[] LED_preview_set_data = new byte[3];
        bool LED_preview_set_flag = false;
        byte LED_preview_brightness_level = 0;
        // LED輝度設定タイプ 3種
        string[] led_brightness_set_type_name = new string[] { "普通", "暗", "明" };

        FlashControl fData = new FlashControl();
        bool FlashReadFirstTime = true;

        bool set_base_info_flag = false;
        bool set_func_setting_flag = false;
        bool set_encoder_script_flag = false;
        bool set_sw_func_setting_flag = false;

        Bitmap[] bmp_disp_icon;

        ComboBox[] my_button_macro_select;

        RadioButton[] my_rbtn_mouse_button_set;

        RadioButton[] my_rbtn_button_set;
        TextBox[] my_txtbx_joystick_lever;

        RadioButton[] my_rbtn_multimedia_key;

        TextBox[] my_txtbx_func_name;
        Label[] my_lbl_func_mode_select;
        Label[] my_lbl_button_mode_select;

        Label[] my_sw_func_names;
        Label[] my_encoder_func_names;
        Label[] my_profile_colors;
        Label[] my_profile_select;
        Label[] my_profile_border;
        Label[] my_func_colors;

        string[] my_hat_sw_disp_text = new string[9] { "上", "右上", "右", "右下", "下", "左下", "左", "左上", "中立" };
        //string[] my_pattern_name_text = new string[3] { "", "短", "長" };
        //string[] my_pattern_name_text = new string[3] { "無", "短", "長" };

        byte mode_no_now = 0;
        byte function_no_now = 0;

        int keyboard_setup_assist_status = Constants.KEYBOARD_SETUP_ASSIST_STATUS_NONE;
        byte[,] keyboard_setup_assist_key_code = new byte[,] { { 0x5A, 0xBF }, { 0x5A, 0xE2 } };
        byte[] keyboard_setup_assist_set_type_list = new byte[Constants.KEYBOARD_TYPE_NUM] { Constants.KEYBOARD_TYPE_US, Constants.KEYBOARD_TYPE_JA };
        byte[] keyboard_setup_assist_input_key_code = new byte[2];
        byte keyboard_setup_assist_set_type = 0;

        // DEBUG
        int[] Debug_Array = new int[16];    //DEBUG
        //byte[] Debug_Arr = new byte[64]{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        //                                0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        //                                0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        //                                0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
        int[] Debug_Arr2 = new int[16];
        int[] Debug_Arr3 = new int[16];    //DEBUG
        bool dbg_write_flag = false;
        long dbg_write_address;
        ArrayList dbg_al_write_data = new ArrayList();
        bool debug_flash_read_req = false;
        bool debug_flash_read_comp = false;
        uint debug_flash_read_start_addr = 0;
        uint debug_flash_read_size = 0;
        byte[] debug_flash_read_buff = new byte[0x10000];
        byte[] debug_led_rgb_set_val = new byte[3];
        byte debug_led_brightness_level_set_val = 0;
        bool debug_led_rgb_set_req = false;
        bool debug_led_rgb_flash_save_req = false;



        //Globally Unique Identifier (GUID) for HID class devices.  Windows uses GUIDs to identify things.
        Guid InterfaceClassGuid = new Guid(0x4d1e55b2, 0xf16f, 0x11cf, 0x88, 0xcb, 0x00, 0x11, 0x11, 0x00, 0x00, 0x30); 
	    //--------------- End of Global Varibles ------------------

        //-------------------------------------------------------END CUT AND PASTE BLOCK-------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------


        //Need to check "Allow unsafe code" checkbox in build properties to use unsafe keyword.  Unsafe is needed to
        //properly interact with the unmanged C++ style APIs used to find and connect with the USB device.
        public unsafe Form1()
        {
            InitializeComponent();

            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseWheel);

            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------
            //-------------------------------------------------------BEGIN CUT AND PASTE BLOCK-----------------------------------------------------------------------------------
			//Additional constructor code

            try
            {
                if (__DEBUG_FLAG__ == false)
                {   // Release
                    this.Size = new System.Drawing.Size(1205, 672);
                    //this.Size = new System.Drawing.Size(995, 519);


                    tabControl1.Visible = false;
                    pnl_macro_editor.Visible = false;
                    pnl_dial_macro_editor.Visible = false;
                    pnl_system_setup.Visible = false;
                    pnl_keyboard_type_assist.Visible = false;
                    pnl_macro_editor.Location = new Point(0, 0);
                    pnl_dial_macro_editor.Location = new Point(0, 0);
                    pnl_system_setup.Location = new Point(0, 0);
                    pnl_keyboard_type_assist.Location = new Point(0, 0);
                    //gbx_macro_editor.Location = new Point(pnl_main.Location.X, pnl_main.Location.Y);
                    //gbx_dial_macro_editor.Location = new Point(pnl_main.Location.X, pnl_main.Location.Y);
                    //gbx_system_setup.Location = new Point(pnl_main.Location.X, pnl_main.Location.Y);

                    btn_ScriptWrite.Visible = false;
                    btn_ScriptRead.Visible = false;
                    btn_Export.Visible = false;
                    btn_Import.Visible = false;
                    btn_FlashErase.Visible = false;

                    dgv_FlashMemory.Visible = false;
                    groupBox1.Visible = false;
                    groupBox5.Visible = false;
                }
                else
                {   // DEBUG
                    this.Size = new System.Drawing.Size(1900, 950);
                }

                this.KeyPreview = true;

                this.Text = RevOmate.Properties.Resources.APPLICATION_NAME;
                //自分自身のバージョン情報を取得する
                System.Diagnostics.FileVersionInfo fver = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
                this.Text += " " + fver.FileVersion;
                StatusBox_lbl.Text = RevOmate.Properties.Resources.STATUS_MESSAGE_BOX_TEXT;

                canvas = new Bitmap(pbx_mode_color.Width, pbx_mode_color.Height);

                btn_dial_macro_editor_submit.Text = RevOmate.Properties.Resources.BTN_SUBMIT;
                //btn_dial_macro_editor_cancel.Text = RevOmate.Properties.Resources.BTN_CANCEL;
                btn_dial_macro_editor_cancel.Text = RevOmate.Properties.Resources.BTN_CLOSE;
                btn_macro_editor_close.Text = RevOmate.Properties.Resources.BTN_CLOSE;
                btn_system_setup_close.Text = RevOmate.Properties.Resources.BTN_CLOSE;
                btn_dial_macro_editor.Text = RevOmate.Properties.Resources.BTN_DIAL_MACRO_EDITOR;
                btn_macro_editor.Text = RevOmate.Properties.Resources.BTN_MACRO_EDITOR;
                btn_system_setup.Text = RevOmate.Properties.Resources.BTN_SETUP;

                lbl_keyboard_setup_assist_title.Text = RevOmate.Properties.Resources.KEYBOARD_SETUP_ASSIST_TITLE;
                lbl_keyboard_setup_assist_msg1.Text = RevOmate.Properties.Resources.KEYBOARD_SETUP_ASSIST_MSG2;
                lbl_keyboard_setup_assist_msg2.Text = RevOmate.Properties.Resources.KEYBOARD_SETUP_ASSIST_MSG1;
                rbtn_keyboard_setup_assist_type1.Text = RevOmate.Properties.Resources.SYSTEM_SETTING_KEYBOARD_SETTING_JIS;
                rbtn_keyboard_setup_assist_type1.Tag = Constants.KEYBOARD_TYPE_JA.ToString();
                rbtn_keyboard_setup_assist_type2.Text = RevOmate.Properties.Resources.SYSTEM_SETTING_KEYBOARD_SETTING_ANSI;
                rbtn_keyboard_setup_assist_type2.Tag = Constants.KEYBOARD_TYPE_US.ToString();
                lbl_keyboard_setup_assist_comp_type1.Text = RevOmate.Properties.Resources.SYSTEM_SETTING_KEYBOARD_SETTING_JIS_DESC;
                lbl_keyboard_setup_assist_comp_type2.Text = RevOmate.Properties.Resources.SYSTEM_SETTING_KEYBOARD_SETTING_ANSI_DESC;
                lbl_keyboard_setup_assist_comp_msg1.Text = RevOmate.Properties.Resources.KEYBOARD_SETUP_ASSIST_COMP_MSG1;
                lbl_keyboard_setup_assist_comp_msg2.Text = RevOmate.Properties.Resources.KEYBOARD_SETUP_ASSIST_COMP_MSG2;
                lbl_keyboard_setup_assist_comp_msg3.Text = RevOmate.Properties.Resources.KEYBOARD_SETUP_ASSIST_COMP_MSG3;
                llbl_keyboard_setup_help.Text = RevOmate.Properties.Resources.HELP_QUESTION_MARK;
                btn_keyboard_setup_cancel.Text = RevOmate.Properties.Resources.BTN_CANCEL;
                btn_keyboard_setup_set.Text = RevOmate.Properties.Resources.BTN_SETUP;
                // マクロ編集画面
                lbl_macro_list_title.Text = RevOmate.Properties.Resources.MACRO_EDITOR_MACRO_LIST_TITLE;
                lbl_macro_editor_area_title.Text = RevOmate.Properties.Resources.MACRO_EDITOR_MACRO_EDITOR_AREA_TITLE;
                lbl_macro_editor_now_no.Text = RevOmate.Properties.Resources.MACRO_EDITOR_LIST_NO;
                lbl_macro_editor_now_macro_name.Text = RevOmate.Properties.Resources.MACRO_EDITOR_LIST_MACRO_NAME;
                lbl_REC_Btn.Text = RevOmate.Properties.Resources.MACRO_EDITOR_OPERATION_REC_START;
                dgv_ScriptList.Columns[0].HeaderText = RevOmate.Properties.Resources.MACRO_EDITOR_LIST_NO;
                dgv_ScriptList.Columns[1].HeaderText = RevOmate.Properties.Resources.MACRO_EDITOR_LIST_MACRO_NAME;
                dgv_ScriptEditor.Columns[0].HeaderText = RevOmate.Properties.Resources.MACRO_EDITOR_EDITOR_LIST_SIZE;
                dgv_ScriptEditor.Columns[1].HeaderText = RevOmate.Properties.Resources.MACRO_EDITOR_EDITOR_LIST_ITEM;
                dgv_ScriptEditor.Columns[2].HeaderText = RevOmate.Properties.Resources.MACRO_EDITOR_EDITOR_LIST_VALUE;
                lbl_Clear_btn.Text = RevOmate.Properties.Resources.MACRO_EDITOR_OPERATION_REC_DELETE;
                lbl_MacroFileImport_Icon.Text = RevOmate.Properties.Resources.MACRO_EDITOR_MACRO_DATA_READ;
                lbl_MacroFileExport_Icon.Text = RevOmate.Properties.Resources.MACRO_EDITOR_MACRO_DATA_SAVE;
                lbl_Dustbox_Icon.Text = RevOmate.Properties.Resources.MACRO_EDITOR_DELETE;
                lbl_JoyLeftLeverPress_Icon.Text = RevOmate.Properties.Resources.SCRIPT_JOY_LEVER_L_TEXT;
                lbl_JoyLeftLeverRelease_Icon.Text = RevOmate.Properties.Resources.SCRIPT_JOY_LEVER_L_CENTER_TEXT;
                lbl_JoyRightLeverPress_Icon.Text = RevOmate.Properties.Resources.SCRIPT_JOY_LEVER_R_TEXT;
                lbl_JoyRightLeverRelease_Icon.Text = RevOmate.Properties.Resources.SCRIPT_JOY_LEVER_R_CENTER_TEXT;
                lbl_JoyHatSWPress_Icon.Text = RevOmate.Properties.Resources.SCRIPT_JOY_HATSW_PRESS_TEXT;
                lbl_JoyHatSWRelease_Icon.Text = RevOmate.Properties.Resources.SCRIPT_JOY_HATSW_RELEASE_TEXT;
                lbl_JoyButtonPress_Icon.Text = RevOmate.Properties.Resources.SCRIPT_JOY_BUTTON_PRESS_TEXT;
                lbl_JoyButtonRelease_Icon.Text = RevOmate.Properties.Resources.SCRIPT_JOY_BUTTON_RELEASE_TEXT;
                lbl_KeyPress_Icon.Text = RevOmate.Properties.Resources.SCRIPT_KEY_PRESS_TEXT;
                lbl_KeyRelease_Icon.Text = RevOmate.Properties.Resources.SCRIPT_KEY_RELEASE_TEXT;
                lbl_MouseClick_Icon.Text = RevOmate.Properties.Resources.SCRIPT_MOUSE_CLICK_TEXT;
                lbl_MouseRelease_Icon.Text = RevOmate.Properties.Resources.SCRIPT_MOUSE_RELEASE_TEXT;
                lbl_WheelScroll_Icon.Text = RevOmate.Properties.Resources.SCRIPT_WHEEL_SCROLL_TEXT;
                lbl_Interval_Icon.Text = RevOmate.Properties.Resources.SCRIPT_INTERVAL_TEXT;
                lbl_MouseMovePress_Icon.Text = RevOmate.Properties.Resources.SCRIPT_MOUSE_MOVE_TEXT;
                lbl_MultiMediaPress_Icon.Text = RevOmate.Properties.Resources.SCRIPT_MULTIMEDIA_PRESS_TEXT;
                lbl_MultiMediaRelease_Icon.Text = RevOmate.Properties.Resources.SCRIPT_MULTIMEDIA_RELEASE_TEXT;
                lbl_script_command_title.Text = RevOmate.Properties.Resources.MACRO_EDITOR_OPERATION_SETTING;
                lbl_MacroWrite_txt.Text = RevOmate.Properties.Resources.MACRO_EDITOR_MACRO_WRITE;
                lbl_MacroRead_txt.Text = RevOmate.Properties.Resources.MACRO_EDITOR_MACRO_READ;
                lbl_REC_Icon.Text = RevOmate.Properties.Resources.MACRO_EDITOR_OPERATION_REC;
                lbl_MacroFileImportExportTitle_Icon.Text = RevOmate.Properties.Resources.MACRO_EDITOR_MACRO_DATA_RW_TITLE;
                lbl_MacroWrite_Icon.Text = RevOmate.Properties.Resources.MACRO_EDITOR_MACRO_WRITE;
                lbl_MacroRead_Icon.Text = RevOmate.Properties.Resources.MACRO_EDITOR_MACRO_READ;
                lbl_macro_editor_macro_create_title.Text = RevOmate.Properties.Resources.MACRO_EDITOR_MACRO_CREATE_TITLE;

                // 設定画面
                gbx_base_setting.Text = RevOmate.Properties.Resources.SYSTEM_SETTING_SETTING_TITLE;
                gbx_mode_LED_setting.Text = RevOmate.Properties.Resources.SYSTEM_SETTING_MODE_LED_SETTING;
                chkbx_mode_led_off.Text = RevOmate.Properties.Resources.SYSTEM_SETTING_MODE_LED_OFF;
                lbl_mode_led_off_unit.Text = RevOmate.Properties.Resources.SYSTEM_SETTING_MODE_LED_OFF_UNIT;
                chkbx_led_sleep.Text = RevOmate.Properties.Resources.SYSTEM_SETTING_SLEEP_LED_OFF;
                chkbx_encoder_typematic.Text = RevOmate.Properties.Resources.SYSTEM_SETTING_ENCODER_TYPEMATIC;
                gbx_dial_func_LED_setting.Text = RevOmate.Properties.Resources.SYSTEM_SETTING_DIAL_FUNC_LED_SETTING_TITLE;
                rbtn_func_led_slow.Text = RevOmate.Properties.Resources.SYSTEM_SETTING_DIAL_FUNC_LED_SLOW;
                rbtn_func_led_flash.Text = RevOmate.Properties.Resources.SYSTEM_SETTING_DIAL_FUNC_LED_FLASH;
                rbtn_func_led_on.Text = RevOmate.Properties.Resources.SYSTEM_SETTING_DIAL_FUNC_LED_ON;
                btn_base_setting_set.Text = RevOmate.Properties.Resources.BTN_SUBMIT;
                gbx_system_backup.Text = RevOmate.Properties.Resources.SYSTEM_SETTING_BACKUP_TITLE;
                lbl_system_backup_title.Text = RevOmate.Properties.Resources.SYSTEM_SETTING_BACKUP_FILE_TITLE;
                btn_system_backupfile_read.Text = RevOmate.Properties.Resources.SYSTEM_SETTING_BACKUP_FILE_READ;
                btn_system_backupfile_save.Text = RevOmate.Properties.Resources.SYSTEM_SETTING_BACKUP_FILE_SAVE;
                lbl_default_setting_title.Text = RevOmate.Properties.Resources.SYSTEM_SETTING_FACTORY_RESET_TITLE;
                btn_system_default_set.Text = RevOmate.Properties.Resources.SYSTEM_SETTING_FACTORY_RESET;
                llbl_help.Text = RevOmate.Properties.Resources.HELP_QUESTION_MARK;
                //llbl_help.Text = RevOmate.Properties.Resources.HELP_LINK_BUTTON_TEXT;
                llbl_system_setup_help.Text = RevOmate.Properties.Resources.HELP_QUESTION_MARK;
                llbl_dial_macro_editor_help.Text = RevOmate.Properties.Resources.HELP_QUESTION_MARK;
                llbl_macro_editor_help.Text = RevOmate.Properties.Resources.HELP_QUESTION_MARK;
                gbx_keyboard_setting.Text = RevOmate.Properties.Resources.SYSTEM_SETTING_KEYBOARD_SETTING;
                rbtn_keyboard_us.Text = RevOmate.Properties.Resources.SYSTEM_SETTING_KEYBOARD_SETTING_US;
                rbtn_keyboard_ja.Text = RevOmate.Properties.Resources.SYSTEM_SETTING_KEYBOARD_SETTING_JA;


                chkbx_encoder_script_loop.Text = RevOmate.Properties.Resources.DIAL_MACRO_LOOP_MODE_TEXT;

                int set_idx = 0;
                // セットタイプの文字列設定
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_NONE;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_ENCODER_SCRIPT1;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_ENCODER_SCRIPT2;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_ENCODER_SCRIPT3;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_NUMBER_UP;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_NUMBER_DOWN;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_KEYBOARD;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_MOUSE_L_CLICK;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_MOUSE_R_CLICK;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_MOUSE_WH_CLICK;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_MOUSE_B4_CLICK;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_MOUSE_B5_CLICK;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_MOUSE_DOUBLE_CLICK;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_MOUSE_MOVE;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_MOUSE_WH_SCROLL;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_MULTIMEDIA_PLAY;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_MULTIMEDIA_PAUSE;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_MULTIMEDIA_STOP;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_MULTIMEDIA_REC;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_MULTIMEDIA_FORWORD;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_MULTIMEDIA_REWIND;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_MULTIMEDIA_NEXT;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_MULTIMEDIA_PREVIOUS;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_MULTIMEDIA_MUTE;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_MULTIMEDIA_VOLUMEUP;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_MULTIMEDIA_VOLUMEDOWN;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_JOYSTICK_LEFT_ANALOG;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_JOYSTICK_RIGHT_ANALOG;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_JOYSTICK_B01;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_JOYSTICK_B02;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_JOYSTICK_B03;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_JOYSTICK_B04;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_JOYSTICK_B05;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_JOYSTICK_B06;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_JOYSTICK_B07;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_JOYSTICK_B08;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_JOYSTICK_B09;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_JOYSTICK_B10;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_JOYSTICK_B11;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_JOYSTICK_B12;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_JOYSTICK_B13;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_JOYSTICK_HAT_N;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_JOYSTICK_HAT_S;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_JOYSTICK_HAT_W;
                set_type_list[set_idx++] = RevOmate.Properties.Resources.SET_TYPE_JOYSTICK_HAT_E;
                //
                set_idx = 0;
                keyboard_modifier_name[set_idx++] = RevOmate.Properties.Resources.SETTING_KEY_CTRL_KEY_CTRL;
                keyboard_modifier_name[set_idx++] = RevOmate.Properties.Resources.SETTING_KEY_CTRL_KEY_SHIFT;
                keyboard_modifier_name[set_idx++] = RevOmate.Properties.Resources.SETTING_KEY_CTRL_KEY_ALT;
                keyboard_modifier_name[set_idx++] = RevOmate.Properties.Resources.SETTING_KEY_CTRL_KEY_WIN;
                keyboard_modifier_name[set_idx++] = RevOmate.Properties.Resources.SETTING_KEY_CTRL_KEY_CTRL;
                keyboard_modifier_name[set_idx++] = RevOmate.Properties.Resources.SETTING_KEY_CTRL_KEY_SHIFT;
                keyboard_modifier_name[set_idx++] = RevOmate.Properties.Resources.SETTING_KEY_CTRL_KEY_ALT;
                keyboard_modifier_name[set_idx++] = RevOmate.Properties.Resources.SETTING_KEY_CTRL_KEY_WIN;
                //
                set_idx = 0;
                multimedia_set_type_name[set_idx++] = RevOmate.Properties.Resources.MULTIMEDIA_NAME_PLAY;
                multimedia_set_type_name[set_idx++] = RevOmate.Properties.Resources.MULTIMEDIA_NAM_PAUSE;
                multimedia_set_type_name[set_idx++] = RevOmate.Properties.Resources.MULTIMEDIA_NAM_STOP;
                multimedia_set_type_name[set_idx++] = RevOmate.Properties.Resources.MULTIMEDIA_NAM_REC;
                multimedia_set_type_name[set_idx++] = RevOmate.Properties.Resources.MULTIMEDIA_NAM_FORWORD;
                multimedia_set_type_name[set_idx++] = RevOmate.Properties.Resources.MULTIMEDIA_NAM_REWIND;
                multimedia_set_type_name[set_idx++] = RevOmate.Properties.Resources.MULTIMEDIA_NAM_NEXT;
                multimedia_set_type_name[set_idx++] = RevOmate.Properties.Resources.MULTIMEDIA_NAM_PREVIOUS;
                multimedia_set_type_name[set_idx++] = RevOmate.Properties.Resources.MULTIMEDIA_NAM_MUTE;
                multimedia_set_type_name[set_idx++] = RevOmate.Properties.Resources.MULTIMEDIA_NAM_VOLUMEUP;
                multimedia_set_type_name[set_idx++] = RevOmate.Properties.Resources.MULTIMEDIA_NAM_VOLUMEDOWN;
                //
                set_idx = 0;
                mouse_set_type_name[set_idx++] = RevOmate.Properties.Resources.MOUSE_NAME_L_CLICK;
                mouse_set_type_name[set_idx++] = RevOmate.Properties.Resources.MOUSE_NAME_WH_CLICK;
                mouse_set_type_name[set_idx++] = RevOmate.Properties.Resources.MOUSE_NAME_R_CLICK;
                mouse_set_type_name[set_idx++] = RevOmate.Properties.Resources.MOUSE_NAME_B4_CLICK;
                mouse_set_type_name[set_idx++] = RevOmate.Properties.Resources.MOUSE_NAME_B5_CLICK;
                //
                set_idx = 0;
                sp_func_type_list[set_idx++] = RevOmate.Properties.Resources.SP_FUNC_TYPE_MODE;
                sp_func_type_list[set_idx++] = RevOmate.Properties.Resources.SP_FUNC_TYPE_MODE1;
                sp_func_type_list[set_idx++] = RevOmate.Properties.Resources.SP_FUNC_TYPE_MODE2;
                sp_func_type_list[set_idx++] = RevOmate.Properties.Resources.SP_FUNC_TYPE_MODE3;
                sp_func_type_list[set_idx++] = RevOmate.Properties.Resources.SP_FUNC_TYPE_FUNC1;
                sp_func_type_list[set_idx++] = RevOmate.Properties.Resources.SP_FUNC_TYPE_FUNC2;
                sp_func_type_list[set_idx++] = RevOmate.Properties.Resources.SP_FUNC_TYPE_FUNC3;
                sp_func_type_list[set_idx++] = RevOmate.Properties.Resources.SP_FUNC_TYPE_FUNC4;
                sp_func_type_list[set_idx++] = RevOmate.Properties.Resources.SP_FUNC_TYPE_FUNC1_PUSH;
                sp_func_type_list[set_idx++] = RevOmate.Properties.Resources.SP_FUNC_TYPE_FUNC2_PUSH;
                sp_func_type_list[set_idx++] = RevOmate.Properties.Resources.SP_FUNC_TYPE_FUNC3_PUSH;
                sp_func_type_list[set_idx++] = RevOmate.Properties.Resources.SP_FUNC_TYPE_FUNC4_PUSH;
                sp_func_type_list[set_idx++] = RevOmate.Properties.Resources.SP_FUNC_TYPE_FUNC;
                //
                set_idx = 0;
                led_brightness_set_type_name[set_idx++] = RevOmate.Properties.Resources.LED_BRIGHTNESS_LEVEL_NORMAL;
                led_brightness_set_type_name[set_idx++] = RevOmate.Properties.Resources.LED_BRIGHTNESS_LEVEL_DARK;
                led_brightness_set_type_name[set_idx++] = RevOmate.Properties.Resources.LED_BRIGHTNESS_LEVEL_LIGHT;
                //
                set_idx = 0;
                my_hat_sw_disp_text[set_idx++] = RevOmate.Properties.Resources.JOYSTICK_HATSW_UP;
                my_hat_sw_disp_text[set_idx++] = RevOmate.Properties.Resources.JOYSTICK_HATSW_NE;
                my_hat_sw_disp_text[set_idx++] = RevOmate.Properties.Resources.JOYSTICK_HATSW_RIGHT;
                my_hat_sw_disp_text[set_idx++] = RevOmate.Properties.Resources.JOYSTICK_HATSW_SE;
                my_hat_sw_disp_text[set_idx++] = RevOmate.Properties.Resources.JOYSTICK_HATSW_DOWN;
                my_hat_sw_disp_text[set_idx++] = RevOmate.Properties.Resources.JOYSTICK_HATSW_SW;
                my_hat_sw_disp_text[set_idx++] = RevOmate.Properties.Resources.JOYSTICK_HATSW_LEFT;
                my_hat_sw_disp_text[set_idx++] = RevOmate.Properties.Resources.JOYSTICK_HATSW_NW;
                my_hat_sw_disp_text[set_idx++] = RevOmate.Properties.Resources.JOYSTICK_HATSW_CENTER;

                lbl_profile_1_select.Text = RevOmate.Properties.Resources.MODE1_TEXT;
                lbl_profile_2_select.Text = RevOmate.Properties.Resources.MODE2_TEXT;
                lbl_profile_3_select.Text = RevOmate.Properties.Resources.MODE3_TEXT;

                lbl_Script_Icons = new Label[] { lbl_Interval_Icon, lbl_KeyPress_Icon, lbl_KeyRelease_Icon, lbl_MouseClick_Icon, lbl_MouseRelease_Icon, lbl_WheelScroll_Icon,
                                                    lbl_JoyLeftLeverPress_Icon, lbl_JoyLeftLeverRelease_Icon, lbl_JoyRightLeverPress_Icon, lbl_JoyRightLeverRelease_Icon,
                                                    lbl_JoyHatSWPress_Icon, lbl_JoyHatSWRelease_Icon, lbl_JoyButtonPress_Icon, lbl_JoyButtonRelease_Icon,
                                                    lbl_MouseMovePress_Icon, lbl_MultiMediaPress_Icon, lbl_MultiMediaRelease_Icon};
                my_Script_Icon_arrow_Idx = new int[] {6,4,4,5,5,6,0,0,1,1,2,2,3,3,6,6,6};
                lbl_Arrow_Icons = new Label[] { lbl_Arrow_Icon1, lbl_Arrow_Icon2, lbl_Arrow_Icon3, lbl_Arrow_Icon4, lbl_Arrow_Icon5, lbl_Arrow_Icon6, lbl_Arrow_Icon7 };
                my_button_macro_select = new ComboBox[] { cmbbx_button_1, cmbbx_button_2, cmbbx_button_3, cmbbx_button_4, cmbbx_button_5, cmbbx_button_6, cmbbx_button_7, cmbbx_button_8, cmbbx_button_9, cmbbx_button_10 };
                my_rbtn_mouse_button_set = new RadioButton[] { rbtn_Mouse01, rbtn_Mouse02, rbtn_Mouse03, rbtn_Mouse04, rbtn_Mouse05 };
                my_rbtn_button_set = new RadioButton[] { rbtn_JoystickButton01, rbtn_JoystickButton02, rbtn_JoystickButton03, rbtn_JoystickButton04, rbtn_JoystickButton05, rbtn_JoystickButton06, rbtn_JoystickButton07, rbtn_JoystickButton08 ,
                                                        rbtn_JoystickButton09, rbtn_JoystickButton10, rbtn_JoystickButton11, rbtn_JoystickButton12, rbtn_JoystickButton13};
                my_txtbx_joystick_lever = new TextBox[] { txtbx_Script_Add_Info_JoysticLever_X, txtbx_Script_Add_Info_JoysticLever_Y };
                my_rbtn_multimedia_key = new RadioButton[] { rbtn_MultiMedia01, rbtn_MultiMedia02, rbtn_MultiMedia03, rbtn_MultiMedia04, rbtn_MultiMedia05, rbtn_MultiMedia06, rbtn_MultiMedia07, rbtn_MultiMedia08, rbtn_MultiMedia09, rbtn_MultiMedia10, rbtn_MultiMedia11 };

                my_lbl_LED_Color_set_button = new Label[] { lbl_LED_COLOR_1, lbl_LED_COLOR_2, lbl_LED_COLOR_3, lbl_LED_COLOR_4, lbl_LED_COLOR_5, lbl_LED_COLOR_6, lbl_LED_COLOR_7, lbl_LED_COLOR_8, lbl_LED_COLOR_9 };
                my_tb_LED_Duty_set_button = new TrackBar[] { trackBar_R, trackBar_G, trackBar_B };
                my_rbtn_LED_Brightness_Level_set_button = new RadioButton[] { rbtn_LED_Level_Normal, rbtn_LED_Level_Dark, rbtn_LED_Level_Light };
                my_lbl_LED_Color_set_func = new Label[,] { { lbl_LED_COLOR_1_func1, lbl_LED_COLOR_2_func1, lbl_LED_COLOR_3_func1, lbl_LED_COLOR_4_func1, lbl_LED_COLOR_5_func1, lbl_LED_COLOR_6_func1, lbl_LED_COLOR_7_func1, lbl_LED_COLOR_8_func1, lbl_LED_COLOR_9_func1 },
                                                            {  lbl_LED_COLOR_1_func2, lbl_LED_COLOR_2_func2, lbl_LED_COLOR_3_func2, lbl_LED_COLOR_4_func2, lbl_LED_COLOR_5_func2, lbl_LED_COLOR_6_func2, lbl_LED_COLOR_7_func2, lbl_LED_COLOR_8_func2, lbl_LED_COLOR_9_func2},
                                                            {  lbl_LED_COLOR_1_func3, lbl_LED_COLOR_2_func3, lbl_LED_COLOR_3_func3, lbl_LED_COLOR_4_func3, lbl_LED_COLOR_5_func3, lbl_LED_COLOR_6_func3, lbl_LED_COLOR_7_func3, lbl_LED_COLOR_8_func3, lbl_LED_COLOR_9_func3},
                                                            {  lbl_LED_COLOR_1_func4, lbl_LED_COLOR_2_func4, lbl_LED_COLOR_3_func4, lbl_LED_COLOR_4_func4, lbl_LED_COLOR_5_func4, lbl_LED_COLOR_6_func4, lbl_LED_COLOR_7_func4, lbl_LED_COLOR_8_func4, lbl_LED_COLOR_9_func4} };
                my_tb_LED_Duty_set_func = new TrackBar[,] { { trackBar_R_func1, trackBar_G_func1, trackBar_B_func1 },
                                                            { trackBar_R_func2, trackBar_G_func2, trackBar_B_func2 },
                                                            { trackBar_R_func3, trackBar_G_func3, trackBar_B_func3 },
                                                            { trackBar_R_func4, trackBar_G_func4, trackBar_B_func4 } };
                my_lbl_LED_Color_now_func = new Label[] { lbl_LED_COLOR_NOW_func1, lbl_LED_COLOR_NOW_func2, lbl_LED_COLOR_NOW_func3, lbl_LED_COLOR_NOW_func4 };
                my_rbtn_LED_Brightness_Level_set_func = new RadioButton[,] { { rbtn_LED_Level_Normal_func1, rbtn_LED_Level_Dark_func1, rbtn_LED_Level_Light_func1 },
                                                                            { rbtn_LED_Level_Normal_func2, rbtn_LED_Level_Dark_func2, rbtn_LED_Level_Light_func2 },
                                                                            { rbtn_LED_Level_Normal_func3, rbtn_LED_Level_Dark_func3, rbtn_LED_Level_Light_func3 },
                                                                            { rbtn_LED_Level_Normal_func4, rbtn_LED_Level_Dark_func4, rbtn_LED_Level_Light_func4 } };
                my_lbl_func_mode_select = new Label[] { lbl_func_mode1_select, lbl_func_mode2_select, lbl_func_mode3_select };
                my_lbl_button_mode_select = new Label[] { lbl_button_mode1_select, lbl_button_mode2_select, lbl_button_mode3_select };
                my_txtbx_func_name = new TextBox[] { txtbx_function1_name, txtbx_function2_name, txtbx_function3_name, txtbx_function4_name };

                // 機能設定の設定用コントロール
                my_cmbbx_set_type = new ComboBox[,] { { cmbbx_func1_set_type_cw, cmbbx_func1_set_type_ccw }, { cmbbx_func2_set_type_cw, cmbbx_func2_set_type_ccw }, { cmbbx_func3_set_type_cw, cmbbx_func3_set_type_ccw }, { cmbbx_func4_set_type_cw, cmbbx_func4_set_type_ccw } };
                my_encoder_sensitivity = new NumericUpDown[,] { { num_func1_sensivity_cw, num_func1_sensivity_ccw }, { num_func2_sensivity_cw, num_func2_sensivity_ccw }, { num_func3_sensivity_cw, num_func3_sensivity_ccw }, { num_func4_sensivity_cw, num_func4_sensivity_ccw } };
                my_keyboard_key = new TextBox[,] { { txtbx_func1_key_cw, txtbx_func1_key_ccw }, { txtbx_func2_key_cw, txtbx_func2_key_ccw }, { txtbx_func3_key_cw, txtbx_func3_key_ccw }, { txtbx_func4_key_cw, txtbx_func4_key_ccw } };
                my_keyboard_modifier = new CheckBox[,,] { { { chk_func1_ctrl_cw, chk_func1_shift_cw, chk_func1_alt_cw, chk_func1_win_cw }, { chk_func1_ctrl_ccw, chk_func1_shift_ccw, chk_func1_alt_ccw, chk_func1_win_ccw } },
                                                        { { chk_func2_ctrl_cw, chk_func2_shift_cw, chk_func2_alt_cw, chk_func2_win_cw }, { chk_func2_ctrl_ccw, chk_func2_shift_ccw, chk_func2_alt_ccw, chk_func2_win_ccw } },
                                                        { { chk_func3_ctrl_cw, chk_func3_shift_cw, chk_func3_alt_cw, chk_func3_win_cw }, { chk_func3_ctrl_ccw, chk_func3_shift_ccw, chk_func3_alt_ccw, chk_func3_win_ccw } },
                                                        { { chk_func4_ctrl_cw, chk_func4_shift_cw, chk_func4_alt_cw, chk_func4_win_cw }, { chk_func4_ctrl_ccw, chk_func4_shift_ccw, chk_func4_alt_ccw, chk_func4_win_ccw } }};
                my_keyboard_key_clr = new Button[,] { { btn_func1_key_clr_cw, btn_func1_key_clr_ccw }, { btn_func2_key_clr_cw, btn_func2_key_clr_ccw }, { btn_func3_key_clr_cw, btn_func3_key_clr_ccw }, { btn_func4_key_clr_cw, btn_func4_key_clr_ccw } };
                my_lbl_title = new Label[,] { { lbl_func1_title_cw, lbl_func1_title_ccw }, { lbl_func2_title_cw, lbl_func2_title_ccw }, { lbl_func3_title_cw, lbl_func3_title_ccw }, { lbl_func4_title_cw, lbl_func4_title_ccw } };
                my_mouse_x = new NumericUpDown[,] { { num_func1_x_cw, num_func1_x_ccw }, { num_func2_x_cw, num_func2_x_ccw }, { num_func3_x_cw, num_func3_x_ccw }, { num_func4_x_cw, num_func4_x_ccw } };
                my_mouse_y = new NumericUpDown[,] { { num_func1_y_cw, num_func1_y_ccw }, { num_func2_y_cw, num_func2_y_ccw }, { num_func3_y_cw, num_func3_y_ccw }, { num_func4_y_cw, num_func4_y_ccw } };
                my_joypad_x = new NumericUpDown[,] { { num_func1_x_cw, num_func1_x_ccw }, { num_func2_x_cw, num_func2_x_ccw }, { num_func3_x_cw, num_func3_x_ccw }, { num_func4_x_cw, num_func4_x_ccw } };
                my_joypad_y = new NumericUpDown[,] { { num_func1_y_cw, num_func1_y_ccw }, { num_func2_y_cw, num_func2_y_ccw }, { num_func3_y_cw, num_func3_y_ccw }, { num_func4_y_cw, num_func4_y_ccw } };


                // SW機能名称
                my_sw_func_names = new Label[] { lbl_sw1_func_name, lbl_sw2_func_name, lbl_sw3_func_name, lbl_sw4_func_name, lbl_sw5_func_name, lbl_sw6_func_name, lbl_sw7_func_name, lbl_sw8_func_name, lbl_sw9_func_name, lbl_sw10_func_name, lbl_Dial_func_name };
                my_encoder_func_names = new Label[] { lbl_Dial_func_name1, lbl_Dial_func_name2, lbl_Dial_func_name3, lbl_Dial_func_name4 };
                my_profile_colors = new Label[] { lbl_profile_color_1, lbl_profile_color_2, lbl_profile_color_3 };
                my_profile_select = new Label[] { lbl_profile_1_select, lbl_profile_2_select, lbl_profile_3_select };
                my_profile_border = new Label[] { lbl_profile1_border, lbl_profile2_border, lbl_profile3_border };
                my_func_colors = new Label[] { lbl_func_color_1, lbl_func_color_2, lbl_func_color_3, lbl_func_color_4 };
                

                // マクロ編集画面　情報表示コントロール表示位置調整
                gbx_Script_Add_Info.Location = new Point(dgv_ScriptEditor.Location.X + (dgv_ScriptEditor.Width - gbx_Script_Add_Info.Width) / 2, dgv_ScriptEditor.Location.Y + (dgv_ScriptEditor.Height - gbx_Script_Add_Info.Height) / 2);
                gbx_Script_Add_Info_JoysticLever.Location = new Point(dgv_ScriptEditor.Location.X + (dgv_ScriptEditor.Width - gbx_Script_Add_Info_JoysticLever.Width) / 2, dgv_ScriptEditor.Location.Y + (dgv_ScriptEditor.Height - gbx_Script_Add_Info_JoysticLever.Height) / 2);
                gbx_Script_Add_Info_JoysticButton.Location = new Point(dgv_ScriptEditor.Location.X + (dgv_ScriptEditor.Width - gbx_Script_Add_Info_JoysticButton.Width) / 2, dgv_ScriptEditor.Location.Y + (dgv_ScriptEditor.Height - gbx_Script_Add_Info_JoysticButton.Height) / 2);
                gbx_Script_Add_Info_MultiMedia.Location = new Point(dgv_ScriptEditor.Location.X + (dgv_ScriptEditor.Width - gbx_Script_Add_Info_MultiMedia.Width) / 2, dgv_ScriptEditor.Location.Y + (dgv_ScriptEditor.Height - gbx_Script_Add_Info_MultiMedia.Height) / 2);

                //for (int fi = 0; fi < my_button_on_time_set.Length; fi++ )
                //{
                //    my_button_on_time_set[fi].Text = Constants.BUTTON_ON_TIME_START_TEXT;
                //}
                
                //現在のコードを実行しているAssemblyを取得
                System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
                //指定されたマニフェストリソースを読み込む
                Bitmap bmp_mode0 = new Bitmap(myAssembly.GetManifestResourceStream("RevOmate.Resources.Mode_Once.png"));
                Bitmap bmp_mode1 = new Bitmap(myAssembly.GetManifestResourceStream("RevOmate.Resources.Mode_Loop.png"));
                Bitmap bmp_mode2 = new Bitmap(myAssembly.GetManifestResourceStream("RevOmate.Resources.Mode_Fire.png"));
                Bitmap bmp_script0 = new Bitmap(myAssembly.GetManifestResourceStream("RevOmate.Resources.Interval.png"));
                Bitmap bmp_script1 = new Bitmap(myAssembly.GetManifestResourceStream("RevOmate.Resources.KeyPress.png"));
                Bitmap bmp_script2 = new Bitmap(myAssembly.GetManifestResourceStream("RevOmate.Resources.KeyRelease.png"));
                Bitmap bmp_script3 = new Bitmap(myAssembly.GetManifestResourceStream("RevOmate.Resources.MouseClick.png"));
                Bitmap bmp_script4 = new Bitmap(myAssembly.GetManifestResourceStream("RevOmate.Resources.MouseRelease.png"));
                Bitmap bmp_script5 = new Bitmap(myAssembly.GetManifestResourceStream("RevOmate.Resources.WheelScroll.png"));
                bmp_disp_icon = new Bitmap[] { bmp_mode0, bmp_mode1, bmp_mode2, bmp_script0, bmp_script1, bmp_script2, bmp_script3, bmp_script4, bmp_script5 };

                // コンテキストメニュー
                contextMenuStrip_sw_func.Items.Clear();
                contextMenuStrip_sw_func.Items.Add(RevOmate.Properties.Resources.CONTEXTMENU_FUNCSET);
                contextMenuStrip_sw_func.Items.Add(RevOmate.Properties.Resources.CONTEXTMENU_DEFAULTSET);
                contextMenuStrip_sw_func.Items.Add(RevOmate.Properties.Resources.CONTEXTMENU_CLEAR);
                contextMenuStrip_sw_func.Items.Add("-");
                contextMenuStrip_sw_func.Items.Add(RevOmate.Properties.Resources.CONTEXTMENU_IMPORT);
                contextMenuStrip_sw_func.Items.Add(RevOmate.Properties.Resources.CONTEXTMENU_EXPORT);
                contextMenuStrip_sw_func.Items[0].Click += ToolStripMenuItem_func_setting_Click;

                // 初期設定ファイル読み込み
                string ini_file_path = Path.Combine(Application.UserAppDataPath, Constants.SYSTEM_SETTING_FILE_NAME);
                //string ini_file_path = Path.Combine(System.Environment.CurrentDirectory, Constants.SYSTEM_SETTING_FILE_NAME);
                if (File.Exists(ini_file_path) == true)
                {   // ファイルあり
                    int i_ret = my_system_setting_file_read(ini_file_path);

                    if (i_ret != 0)
                    {   // ファイル読み込みエラー
                        system_setting_info.init();
                    }
                    my_system_setting_file_save(ini_file_path);
                }
                else
                {
                    // ファイルなし
                    // 前バージョンの設定ファイルがあればコピーしてそのファイルを読み込む
                    string folder_path = Application.UserAppDataPath;
                    folder_path = folder_path.Substring(0, folder_path.LastIndexOf("\\"));
                    string[] sub_folder_path = Directory.GetDirectories(folder_path, "*", System.IO.SearchOption.TopDirectoryOnly);
                    bool copy_flag = false;
                    for (int fi = (sub_folder_path.Length - 1); fi >= 0; fi-- )
                    {   // 前バージョンの設定ファイルをコピー

                        // プロダクトバージョン抜き出し
                        string tmp_product_ver = sub_folder_path[fi].Substring(sub_folder_path[fi].LastIndexOf("\\")+1, sub_folder_path[fi].Length - (sub_folder_path[fi].LastIndexOf("\\")+1));
                        if (tmp_product_ver != Application.ProductVersion)
                        {   // 現バージョンと違うバージョン
                            string source_file_path = Path.Combine(sub_folder_path[fi], Constants.SYSTEM_SETTING_FILE_NAME);
                            string dest_file_path = Path.Combine(Application.UserAppDataPath, Constants.SYSTEM_SETTING_FILE_NAME);
                            try
                            {
                                File.Copy(source_file_path, dest_file_path, true);
                                copy_flag = true;
                            }
                            catch
                            {
                            }
                            break;
                        }
                    }

                    if (copy_flag == false)
                    {   // コピーしていないため新規作成
                        my_system_setting_file_save(ini_file_path);

                        // 設定ファイルなしの場合は、まず、キーボード設定画面を表示させる
                        keyboard_setup_display(true);
                    }
                    else
                    {   // コピーしたのでコピーしたファイルを読み込み
                        if (File.Exists(ini_file_path) == true)
                        {
                            int i_ret = my_system_setting_file_read(ini_file_path);

                            if (i_ret != 0)
                            {   // ファイル読み込みエラー
                                system_setting_info.init();
                            }
                        }
                        my_system_setting_file_save(ini_file_path);
                    }
                }


                // 設定タイプ選択肢設定
                for (int fi = 0; fi <= my_cmbbx_set_type.GetUpperBound(0); fi++)
                {
                    for (int fj = 0; fj <= my_cmbbx_set_type.GetUpperBound(1); fj++ )
                    {
                        my_cmbbx_set_type[fi, fj].Items.AddRange(set_type_list);
                        my_cmbbx_set_type[fi, fj].Tag = fi * Constants.CW_CCW_NUM + fj;
                        my_cmbbx_set_type[fi, fj].SelectedIndex = 0;
                        //my_cmbbx_set_type[fi, fj].Parent = BackGround_pb;
                    }
                }
                // 感度設定コントローラ設定
                for(int fi = 0; fi <= my_encoder_sensitivity.GetUpperBound(0); fi++)
                {
                    for(int fj = 0; fj <= my_encoder_sensitivity.GetUpperBound(1); fj++)
                    {
                        my_encoder_sensitivity[fi, fj].Minimum = Constants.SENSITIVITY_MIN;
                        my_encoder_sensitivity[fi, fj].Maximum = Constants.SENSITIVITY_MAX;
                        my_encoder_sensitivity[fi, fj].Value = Constants.SENSITIVITY_DEFAULT;
                    }
                }

                // LED Duty設定コントローラ設定
                for (int fi = 0; fi < my_tb_LED_Duty_set_button.Length; fi++)
                {
                    my_tb_LED_Duty_set_button[fi].Maximum = my_LED_Duty_Max[fi];
                }
                for (int fi = 0; fi <= my_tb_LED_Duty_set_func.GetUpperBound(0); fi++)
                {
                    for (int fj = 0; fj <= my_tb_LED_Duty_set_func.GetUpperBound(1); fj++)
                    {
                        my_tb_LED_Duty_set_func[fi, fj].Maximum = my_LED_Duty_Max[fj];
                    }
                }
                // LED輝度設定コントローラ設定
                for (int fi = 0; fi < my_rbtn_LED_Brightness_Level_set_button.Length; fi++)
                {
                    my_rbtn_LED_Brightness_Level_set_button[fi].Text = led_brightness_set_type_name[fi];
                }
                for (int fi = 0; fi <= my_rbtn_LED_Brightness_Level_set_func.GetUpperBound(0); fi++)
                {
                    for (int fj = 0; fj <= my_rbtn_LED_Brightness_Level_set_func.GetUpperBound(1); fj++)
                    {
                        my_rbtn_LED_Brightness_Level_set_func[fi, fj].Text = led_brightness_set_type_name[fj];
                    }
                }

                //
                for (int fi = 0; fi < multimedia_set_type_name.Length && fi < my_rbtn_multimedia_key.Length; fi++)
                {
                    my_rbtn_multimedia_key[fi].Text = multimedia_set_type_name[fi];
                }
                //
                for (int fi = 0; fi < mouse_set_type_name.Length && fi < my_rbtn_mouse_button_set.Length; fi++)
                {
                    my_rbtn_mouse_button_set[fi].Text = mouse_set_type_name[fi];
                }

                num_mode_led_off_time.Maximum = Constants.LED_LIGHT_OFF_MODE_TIME_MAX;


                // データ格納用の構造体初期化
                my_App_Setting_Data.init_data();

                my_base_info.init_data();
                my_base_mode_infos.init_data(Constants.MODE_NUM, Constants.BUTTON_NUM, Constants.LED_RGB_COLOR_NUM);
                my_func_datas.init_data(Constants.MODE_NUM, Constants.FUNCTION_NUM, Constants.CW_CCW_NUM, Constants.FUNCTION_CWCCW_DATA_LEN, Constants.LED_RGB_COLOR_NUM);
                my_sw_func_datas.init_data(Constants.MODE_NUM, Constants.BUTTON_NUM, Constants.SW_FUNCTION_DATA_LEN);
                my_app_sw_datas.init_data(Constants.MODE_NUM, Constants.BUTTON_NUM, Constants.APP_SW_DATA_SELECT_DATA_LEN, Constants.APP_SW_DATA_DATA_LEN);
                my_app_func_datas.init_data(Constants.MODE_NUM, Constants.FUNCTION_NUM, Constants.CW_CCW_NUM, Constants.APP_FUNC_DATA_SELECT_DATA_LEN);
                my_encoder_script_datas.init_data(Constants.ENCODER_SCRIPT_NUM, Constants.ENCODER_SCRIPT_SCRIPTSET_MAX_NUM);

                my_script_info.init_data();
                my_script_info_datas.init_data(Constants.SCRIPT_NUM);

                my_script_editor_disp_data.init_data();
                my_flash_read_write_buffer.init_data(FlashControl.FM_SCRIPT_DATA_NUM_PER_SECTOR);


                // Script List
#if DEBUG
                int macro_max_num = Constants.SCRIPT_NUM;
#else
                int macro_max_num = Constants.SCRIPT_USER_USE_NUM;
#endif
                for (int fi = 0; fi < macro_max_num; fi++)
                {
                    try
                    {
                        dgv_ScriptList.Rows.Add(string.Format("{0:000}", fi + 1), RevOmate.Properties.Resources.MACRO_NAME_UNDEFINE);
                    }
                    catch
                    {
                    }
                }

                // Encoder Script
                //ENCODER_SCRIPT
                for (int fi = 0; fi < Constants.ENCODER_SCRIPT_NUM; fi++)
                {
                    cmbbx_encoder_script_select_no.Items.Add(RevOmate.Properties.Resources.ENCODER_SCRIPT + string.Format("{0}", fi + 1));
                }
                cmbbx_encoder_script_select_no.SelectedIndex = encoder_script_select_no;
                // スクリプトの選択肢を作成
                DataGridViewComboBoxColumn dgv_combbx_col = new DataGridViewComboBoxColumn();
                dgv_combbx_col.Width = 390;
                dgv_combbx_col.Items.Add(string.Format("{0}", RevOmate.Properties.Resources.MACRO_NAME_UNSETTING));
                for (int fi = 0; fi < Constants.SCRIPT_USER_USE_NUM && fi < my_script_info_datas.Script_Info_Datas.Length; fi++)
                {
                    try
                    {
                        dgv_combbx_col.Items.Add(string.Format("{0}{1:000}:{2}", RevOmate.Properties.Resources.MACRO_NAME_NO_MACRO, fi + 1, RevOmate.Properties.Resources.MACRO_NAME_UNDEFINE));
                    }
                    catch
                    {
                    }
                }
                dgv_script_name.DataPropertyName = dgv_encoder_script.Columns["dgv_script_name"].DataPropertyName;
                int add_idx = dgv_encoder_script.Columns["dgv_script_name"].Index;
                dgv_encoder_script.Columns.Insert(dgv_encoder_script.Columns["dgv_script_name"].Index, dgv_combbx_col);
                dgv_encoder_script.Columns.Remove("dgv_script_name");
                dgv_encoder_script.Columns[add_idx].Name = "dgv_script_name";
                dgv_encoder_script.Columns[add_idx].HeaderText = RevOmate.Properties.Resources.ENCODER_SCRIPT_LIST_MACRO_NAME;

                for (int fi = 0; fi < Constants.ENCODER_SCRIPT_SCRIPTSET_MAX_NUM; fi++)
                {
                    try
                    {
                        dgv_encoder_script.Rows.Add(string.Format("{0:00}", fi + 1), string.Format("{0}", RevOmate.Properties.Resources.MACRO_NAME_UNSETTING));
                        //dgv_script_name.DataPropertyName = dgv_pattern.Columns["dgv_script_name"].DataPropertyName;
                        //dgv_pattern.Columns.Insert(dgv_pattern.Columns["dgv_script_name"].Index, dgv_combbx_col);
                        //dgv_pattern.Columns.Remove("dgv_script_name");
                        ////dgv_combbx_col.Name = "dgv_script_name";
                        //dgv_pattern[dgv_pattern.Columns["dgv_script_name"].Index, fi].Value = "000:名称未設定";
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
            }

            //Register for WM_DEVICECHANGE notifications.  This code uses these messages to detect plug and play connection/disconnection events for USB devices
            DEV_BROADCAST_DEVICEINTERFACE DeviceBroadcastHeader = new DEV_BROADCAST_DEVICEINTERFACE();
            DeviceBroadcastHeader.dbcc_devicetype = DBT_DEVTYP_DEVICEINTERFACE;
            DeviceBroadcastHeader.dbcc_size = (uint)Marshal.SizeOf(DeviceBroadcastHeader);
            DeviceBroadcastHeader.dbcc_reserved = 0;	//Reserved says not to use...
            DeviceBroadcastHeader.dbcc_classguid = InterfaceClassGuid;

            //Need to get the address of the DeviceBroadcastHeader to call RegisterDeviceNotification(), but
            //can't use "&DeviceBroadcastHeader".  Instead, using a roundabout means to get the address by 
            //making a duplicate copy using Marshal.StructureToPtr().
            IntPtr pDeviceBroadcastHeader = IntPtr.Zero;  //Make a pointer.
            pDeviceBroadcastHeader = Marshal.AllocHGlobal(Marshal.SizeOf(DeviceBroadcastHeader)); //allocate memory for a new DEV_BROADCAST_DEVICEINTERFACE structure, and return the address 
            Marshal.StructureToPtr(DeviceBroadcastHeader, pDeviceBroadcastHeader, false);  //Copies the DeviceBroadcastHeader structure into the memory already allocated at DeviceBroadcastHeaderWithPointer
            RegisterDeviceNotification(this.Handle, pDeviceBroadcastHeader, DEVICE_NOTIFY_WINDOW_HANDLE);
 

			//Now make an initial attempt to find the USB device, if it was already connected to the PC and enumerated prior to launching the application.
			//If it is connected and present, we should open read and write handles to the device so we can communicate with it later.
			//If it was not connected, we will have to wait until the user plugs the device in, and the WM_DEVICECHANGE callback function can process
			//the message and again search for the device.
			if(CheckIfPresentAndGetUSBDevicePath())	//Check and make sure at least one device with matching VID/PID is attached
			{
				uint ErrorStatusWrite;
				uint ErrorStatusRead;


				//We now have the proper device path, and we can finally open read and write handles to the device.
                WriteHandleToUSBDevice = CreateFile(DevicePath, GENERIC_WRITE, FILE_SHARE_READ | FILE_SHARE_WRITE, IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);
                ErrorStatusWrite = (uint)Marshal.GetLastWin32Error();
                ReadHandleToUSBDevice = CreateFile(DevicePath, GENERIC_READ, FILE_SHARE_READ | FILE_SHARE_WRITE, IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);
                ErrorStatusRead = (uint)Marshal.GetLastWin32Error();

				if((ErrorStatusWrite == ERROR_SUCCESS) && (ErrorStatusRead == ERROR_SUCCESS))
				{
					AttachedState = true;		//Let the rest of the PC application know the USB device is connected, and it is safe to read/write to it
					AttachedButBroken = false;
                    StatusBox_txtbx.Text = RevOmate.Properties.Resources.STATUS_MSG_USB_CONNECT;
				}
				else //for some reason the device was physically plugged in, but one or both of the read/write handles didn't open successfully...
				{
					AttachedState = false;		//Let the rest of this application known not to read/write to the device.
					AttachedButBroken = true;	//Flag so that next time a WM_DEVICECHANGE message occurs, can retry to re-open read/write pipes
					if(ErrorStatusWrite == ERROR_SUCCESS)
						WriteHandleToUSBDevice.Close();
					if(ErrorStatusRead == ERROR_SUCCESS)
						ReadHandleToUSBDevice.Close();
				}
			}
			else	//Device must not be connected (or not programmed with correct firmware)
			{
				AttachedState = false;
				AttachedButBroken = false;
			}

            if (AttachedState == true)
            {
                StatusBox_txtbx.Text = RevOmate.Properties.Resources.STATUS_MSG_USB_CONNECT;
            }
            else
            {
                StatusBox_txtbx.Text = RevOmate.Properties.Resources.STATUS_MSG_USB_UNCONNECT;
            }

			ReadWriteThread.RunWorkerAsync();	//Recommend performing USB read/write operations in a separate thread.  Otherwise,
												//the Read/Write operations are effectively blocking functions and can lock up the
												//user interface if the I/O operations take a long time to complete.

            //-------------------------------------------------------END CUT AND PASTE BLOCK-------------------------------------------------------------------------------------
            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                //初期設定ファイル書き込み
                string system_file_path = Path.Combine(Application.UserAppDataPath, Constants.SYSTEM_SETTING_FILE_NAME);
                my_system_setting_file_save(system_file_path);
            }
            catch
            {
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //-------------------------------------------------------BEGIN CUT AND PASTE BLOCK-----------------------------------------------------------------------------------

        //FUNCTION:	CheckIfPresentAndGetUSBDevicePath()
        //PURPOSE:	Check if a USB device is currently plugged in with a matching VID and PID
        //INPUT:	Uses globally declared String DevicePath, globally declared GUID, and the MY_DEVICE_ID constant.
        //OUTPUT:	Returns BOOL.  TRUE when device with matching VID/PID found.  FALSE if device with VID/PID could not be found.
        //			When returns TRUE, the globally accessable "DetailedInterfaceDataStructure" will contain the device path
        //			to the USB device with the matching VID/PID.

        bool CheckIfPresentAndGetUSBDevicePath()
        {
		    /* 
		    Before we can "connect" our application to our USB embedded device, we must first find the device.
		    A USB bus can have many devices simultaneously connected, so somehow we have to find our device only.
		    This is done with the Vendor ID (VID) and Product ID (PID).  Each USB product line should have
		    a unique combination of VID and PID.  

		    Microsoft has created a number of functions which are useful for finding plug and play devices.  Documentation
		    for each function used can be found in the MSDN library.  We will be using the following functions (unmanaged C functions):

		    SetupDiGetClassDevs()					//provided by setupapi.dll, which comes with Windows
		    SetupDiEnumDeviceInterfaces()			//provided by setupapi.dll, which comes with Windows
		    GetLastError()							//provided by kernel32.dll, which comes with Windows
		    SetupDiDestroyDeviceInfoList()			//provided by setupapi.dll, which comes with Windows
		    SetupDiGetDeviceInterfaceDetail()		//provided by setupapi.dll, which comes with Windows
		    SetupDiGetDeviceRegistryProperty()		//provided by setupapi.dll, which comes with Windows
		    CreateFile()							//provided by kernel32.dll, which comes with Windows

            In order to call these unmanaged functions, the Marshal class is very useful.
             
		    We will also be using the following unusual data types and structures.  Documentation can also be found in
		    the MSDN library:

		    PSP_DEVICE_INTERFACE_DATA
		    PSP_DEVICE_INTERFACE_DETAIL_DATA
		    SP_DEVINFO_DATA
		    HDEVINFO
		    HANDLE
		    GUID

		    The ultimate objective of the following code is to get the device path, which will be used elsewhere for getting
		    read and write handles to the USB device.  Once the read/write handles are opened, only then can this
		    PC application begin reading/writing to the USB device using the WriteFile() and ReadFile() functions.

		    Getting the device path is a multi-step round about process, which requires calling several of the
		    SetupDixxx() functions provided by setupapi.dll.
		    */

            try
            {
		        IntPtr DeviceInfoTable = IntPtr.Zero;
		        SP_DEVICE_INTERFACE_DATA InterfaceDataStructure = new SP_DEVICE_INTERFACE_DATA();
                SP_DEVICE_INTERFACE_DETAIL_DATA DetailedInterfaceDataStructure = new SP_DEVICE_INTERFACE_DETAIL_DATA();
                SP_DEVINFO_DATA DevInfoData = new SP_DEVINFO_DATA();

		        uint InterfaceIndex = 0;
		        uint dwRegType = 0;
		        uint dwRegSize = 0;
                uint dwRegSize2 = 0;
		        uint StructureSize = 0;
		        IntPtr PropertyValueBuffer = IntPtr.Zero;
		        bool MatchFound = false;
                bool MatchFound2 = false;
                uint ErrorStatus;
		        uint LoopCounter = 0;

                //Use the formatting: "Vid_xxxx&Pid_xxxx" where xxxx is a 16-bit hexadecimal number.
                //Make sure the value appearing in the parathesis matches the USB device descriptor
                //of the device that this aplication is intending to find.
                String DeviceIDToFind = "Vid_22ea&Pid_004B";
                String DeviceIDToFind2 = "Mi_03";

		        //First populate a list of plugged in devices (by specifying "DIGCF_PRESENT"), which are of the specified class GUID. 
		        DeviceInfoTable = SetupDiGetClassDevs(ref InterfaceClassGuid, IntPtr.Zero, IntPtr.Zero, DIGCF_PRESENT | DIGCF_DEVICEINTERFACE);

                if(DeviceInfoTable != IntPtr.Zero)
                {
		            //Now look through the list we just populated.  We are trying to see if any of them match our device. 
		            while(true)
		            {
                        InterfaceDataStructure.cbSize = (uint)Marshal.SizeOf(InterfaceDataStructure);
			            if(SetupDiEnumDeviceInterfaces(DeviceInfoTable, IntPtr.Zero, ref InterfaceClassGuid, InterfaceIndex, ref InterfaceDataStructure))
			            {
                            ErrorStatus = (uint)Marshal.GetLastWin32Error();
                            if (ErrorStatus == ERROR_NO_MORE_ITEMS)	//Did we reach the end of the list of matching devices in the DeviceInfoTable?
				            {	//Cound not find the device.  Must not have been attached.
					            SetupDiDestroyDeviceInfoList(DeviceInfoTable);	//Clean up the old structure we no longer need.
					            return false;		
				            }
			            }
			            else	//Else some other kind of unknown error ocurred...
			            {
                            ErrorStatus = (uint)Marshal.GetLastWin32Error();
				            SetupDiDestroyDeviceInfoList(DeviceInfoTable);	//Clean up the old structure we no longer need.
				            return false;	
			            }

			            //Now retrieve the hardware ID from the registry.  The hardware ID contains the VID and PID, which we will then 
			            //check to see if it is the correct device or not.

			            //Initialize an appropriate SP_DEVINFO_DATA structure.  We need this structure for SetupDiGetDeviceRegistryProperty().
                        DevInfoData.cbSize = (uint)Marshal.SizeOf(DevInfoData);
			            SetupDiEnumDeviceInfo(DeviceInfoTable, InterfaceIndex, ref DevInfoData);

			            //First query for the size of the hardware ID, so we can know how big a buffer to allocate for the data.
			            SetupDiGetDeviceRegistryProperty(DeviceInfoTable, ref DevInfoData, SPDRP_HARDWAREID, ref dwRegType, IntPtr.Zero, 0, ref dwRegSize);

			            //Allocate a buffer for the hardware ID.
                        //Should normally work, but could throw exception "OutOfMemoryException" if not enough resources available.
                        PropertyValueBuffer = Marshal.AllocHGlobal((int)dwRegSize);

			            //Retrieve the hardware IDs for the current device we are looking at.  PropertyValueBuffer gets filled with a 
			            //REG_MULTI_SZ (array of null terminated strings).  To find a device, we only care about the very first string in the
			            //buffer, which will be the "device ID".  The device ID is a string which contains the VID and PID, in the example 
			            //format "Vid_04d8&Pid_003f".
                        SetupDiGetDeviceRegistryProperty(DeviceInfoTable, ref DevInfoData, SPDRP_HARDWAREID, ref dwRegType, PropertyValueBuffer, dwRegSize, ref dwRegSize2);

			            //Now check if the first string in the hardware ID matches the device ID of the USB device we are trying to find.
			            String DeviceIDFromRegistry = Marshal.PtrToStringUni(PropertyValueBuffer); //Make a new string, fill it with the contents from the PropertyValueBuffer

			            Marshal.FreeHGlobal(PropertyValueBuffer);		//No longer need the PropertyValueBuffer, free the memory to prevent potential memory leaks

			            //Convert both strings to lower case.  This makes the code more robust/portable accross OS Versions
			            DeviceIDFromRegistry = DeviceIDFromRegistry.ToLowerInvariant();	
			            DeviceIDToFind = DeviceIDToFind.ToLowerInvariant();
                        DeviceIDToFind2 = DeviceIDToFind2.ToLowerInvariant();

                        //Now check if the hardware ID we are looking at contains the correct VID/PID
			            MatchFound = DeviceIDFromRegistry.Contains(DeviceIDToFind);
                        MatchFound2 = DeviceIDFromRegistry.Contains(DeviceIDToFind2);

                        if((MatchFound == true) && (MatchFound2 == true))
			            {
                            //Device must have been found.  In order to open I/O file handle(s), we will need the actual device path first.
				            //We can get the path by calling SetupDiGetDeviceInterfaceDetail(), however, we have to call this function twice:  The first
				            //time to get the size of the required structure/buffer to hold the detailed interface data, then a second time to actually 
				            //get the structure (after we have allocated enough memory for the structure.)
                            DetailedInterfaceDataStructure.cbSize = (uint)Marshal.SizeOf(DetailedInterfaceDataStructure);
				            //First call populates "StructureSize" with the correct value
				            SetupDiGetDeviceInterfaceDetail(DeviceInfoTable, ref InterfaceDataStructure, IntPtr.Zero, 0, ref StructureSize, IntPtr.Zero);
                            //Need to call SetupDiGetDeviceInterfaceDetail() again, this time specifying a pointer to a SP_DEVICE_INTERFACE_DETAIL_DATA buffer with the correct size of RAM allocated.
                            //First need to allocate the unmanaged buffer and get a pointer to it.
                            IntPtr pUnmanagedDetailedInterfaceDataStructure = IntPtr.Zero;  //Declare a pointer.
                            pUnmanagedDetailedInterfaceDataStructure = Marshal.AllocHGlobal((int)StructureSize);    //Reserve some unmanaged memory for the structure.
                            DetailedInterfaceDataStructure.cbSize = 6; //Initialize the cbSize parameter (4 bytes for DWORD + 2 bytes for unicode null terminator)
                            Marshal.StructureToPtr(DetailedInterfaceDataStructure, pUnmanagedDetailedInterfaceDataStructure, false); //Copy managed structure contents into the unmanaged memory buffer.

                            //Now call SetupDiGetDeviceInterfaceDetail() a second time to receive the device path in the structure at pUnmanagedDetailedInterfaceDataStructure.
                            if (SetupDiGetDeviceInterfaceDetail(DeviceInfoTable, ref InterfaceDataStructure, pUnmanagedDetailedInterfaceDataStructure, StructureSize, IntPtr.Zero, IntPtr.Zero))
                            {
                                //Need to extract the path information from the unmanaged "structure".  The path starts at (pUnmanagedDetailedInterfaceDataStructure + sizeof(DWORD)).
                                IntPtr pToDevicePath = new IntPtr((uint)pUnmanagedDetailedInterfaceDataStructure.ToInt32() + 4);  //Add 4 to the pointer (to get the pointer to point to the path, instead of the DWORD cbSize parameter)
                                DevicePath = Marshal.PtrToStringUni(pToDevicePath); //Now copy the path information into the globally defined DevicePath String.
                                
                                //We now have the proper device path, and we can finally use the path to open I/O handle(s) to the device.
                                SetupDiDestroyDeviceInfoList(DeviceInfoTable);	//Clean up the old structure we no longer need.
                                Marshal.FreeHGlobal(pUnmanagedDetailedInterfaceDataStructure);  //No longer need this unmanaged SP_DEVICE_INTERFACE_DETAIL_DATA buffer.  We already extracted the path information.
                                return true;    //Returning the device path in the global DevicePath String
                            }
                            else //Some unknown failure occurred
                            {
                                uint ErrorCode = (uint)Marshal.GetLastWin32Error();
                                SetupDiDestroyDeviceInfoList(DeviceInfoTable);	//Clean up the old structure.
                                Marshal.FreeHGlobal(pUnmanagedDetailedInterfaceDataStructure);  //No longer need this unmanaged SP_DEVICE_INTERFACE_DETAIL_DATA buffer.  We already extracted the path information.
                                return false;    
                            }
                        }

			            InterfaceIndex++;	
			            //Keep looping until we either find a device with matching VID and PID, or until we run out of devices to check.
			            //However, just in case some unexpected error occurs, keep track of the number of loops executed.
			            //If the number of loops exceeds a very large number, exit anyway, to prevent inadvertent infinite looping.
			            LoopCounter++;
			            if(LoopCounter == 10000000)	//Surely there aren't more than 10 million devices attached to any forseeable PC...
			            {
				            return false;
			            }
		            }//end of while(true)
                }
                return false;
            }//end of try
            catch
            {
                //Something went wrong if PC gets here.  Maybe a Marshal.AllocHGlobal() failed due to insufficient resources or something.
			    return false;	
            }
        }
        //-------------------------------------------------------END CUT AND PASTE BLOCK-------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------


        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //-------------------------------------------------------BEGIN CUT AND PASTE BLOCK-----------------------------------------------------------------------------------
        //This is a callback function that gets called when a Windows message is received by the form.
        //We will receive various different types of messages, but the ones we really want to use are the WM_DEVICECHANGE messages.
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_DEVICECHANGE)
            {
                if (((int)m.WParam == DBT_DEVICEARRIVAL) || ((int)m.WParam == DBT_DEVICEREMOVEPENDING) || ((int)m.WParam == DBT_DEVICEREMOVECOMPLETE) || ((int)m.WParam == DBT_CONFIGCHANGED))
                {
                    //WM_DEVICECHANGE messages by themselves are quite generic, and can be caused by a number of different
                    //sources, not just your USB hardware device.  Therefore, must check to find out if any changes relavant
                    //to your device (with known VID/PID) took place before doing any kind of opening or closing of handles/endpoints.
                    //(the message could have been totally unrelated to your application/USB device)

                    if (CheckIfPresentAndGetUSBDevicePath())	//Check and make sure at least one device with matching VID/PID is attached
                    {
                        //If executes to here, this means the device is currently attached and was found.
                        //This code needs to decide however what to do, based on whether or not the device was previously known to be
                        //attached or not.
                        if ((AttachedState == false) || (AttachedButBroken == true))	//Check the previous attachment state
                        {
                            uint ErrorStatusWrite;
                            uint ErrorStatusRead;

                            //We obtained the proper device path (from CheckIfPresentAndGetUSBDevicePath() function call), and it
                            //is now possible to open read and write handles to the device.
                            WriteHandleToUSBDevice = CreateFile(DevicePath, GENERIC_WRITE, FILE_SHARE_READ | FILE_SHARE_WRITE, IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);
                            ErrorStatusWrite = (uint)Marshal.GetLastWin32Error();
                            ReadHandleToUSBDevice = CreateFile(DevicePath, GENERIC_READ, FILE_SHARE_READ | FILE_SHARE_WRITE, IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);
                            ErrorStatusRead = (uint)Marshal.GetLastWin32Error();

                            if ((ErrorStatusWrite == ERROR_SUCCESS) && (ErrorStatusRead == ERROR_SUCCESS))
                            {
                                AttachedState = true;		//Let the rest of the PC application know the USB device is connected, and it is safe to read/write to it
                                AttachedButBroken = false;
                                StatusBox_txtbx.Text = RevOmate.Properties.Resources.STATUS_MSG_USB_CONNECT;
                            }
                            else //for some reason the device was physically plugged in, but one or both of the read/write handles didn't open successfully...
                            {
                                AttachedState = false;		//Let the rest of this application known not to read/write to the device.
                                AttachedButBroken = true;	//Flag so that next time a WM_DEVICECHANGE message occurs, can retry to re-open read/write pipes
                                if (ErrorStatusWrite == ERROR_SUCCESS)
                                    WriteHandleToUSBDevice.Close();
                                if (ErrorStatusRead == ERROR_SUCCESS)
                                    ReadHandleToUSBDevice.Close();
                            }
                        }
                        //else we did find the device, but AttachedState was already true.  In this case, don't do anything to the read/write handles,
                        //since the WM_DEVICECHANGE message presumably wasn't caused by our USB device.  
                    }
                    else	//Device must not be connected (or not programmed with correct firmware)
                    {
                        if (AttachedState == true)		//If it is currently set to true, that means the device was just now disconnected
                        {
                            AttachedState = false;
                            WriteHandleToUSBDevice.Close();
                            ReadHandleToUSBDevice.Close();
                        }
                        AttachedState = false;
                        AttachedButBroken = false;
                    }
                }
            } //end of: if(m.Msg == WM_DEVICECHANGE)

            base.WndProc(ref m);
        } //end of: WndProc() function
        //-------------------------------------------------------END CUT AND PASTE BLOCK-------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------


        private void ReadWriteThread_DoWork(object sender, DoWorkEventArgs e)
        {
            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------
            //-------------------------------------------------------BEGIN CUT AND PASTE BLOCK-----------------------------------------------------------------------------------

            /*This thread does the actual USB read/write operations (but only when AttachedState == true) to the USB device.
            It is generally preferrable to write applications so that read and write operations are handled in a separate
            thread from the main form.  This makes it so that the main form can remain responsive, even if the I/O operations
            take a very long time to complete.

            Since this is a separate thread, this code below executes independently from the rest of the
            code in this application.  All this thread does is read and write to the USB device.  It does not update
            the form directly with the new information it obtains (such as the ANxx/POT Voltage or pushbutton state).
            The information that this thread obtains is stored in atomic global variables.
            Form updates are handled by the FormUpdateTimer Tick event handler function.

            This application sends packets to the endpoint buffer on the USB device by using the "WriteFile()" function.
            This application receives packets from the endpoint buffer on the USB device by using the "ReadFile()" function.
            Both of these functions are documented in the MSDN library.  Calling ReadFile() is a not perfectly straight
            foward in C# environment, since one of the input parameters is a pointer to a buffer that gets filled by ReadFile().
            The ReadFile() function is therefore called through a wrapper function ReadFileManagedBuffer().

            All ReadFile() and WriteFile() operations in this example project are synchronous.  They are blocking function
            calls and only return when they are complete, or if they fail because of some event, such as the user unplugging
            the device.  It is possible to call these functions with "overlapped" structures, and use them as non-blocking
            asynchronous I/O function calls.  

            Note:  This code may perform differently on some machines when the USB device is plugged into the host through a
            USB 2.0 hub, as opposed to a direct connection to a root port on the PC.  In some cases the data rate may be slower
            when the device is connected through a USB 2.0 hub.  This performance difference is believed to be caused by
            the issue described in Microsoft knowledge base article 940021:
            http://support.microsoft.com/kb/940021/en-us 

            Higher effective bandwidth (up to the maximum offered by interrupt endpoints), both when connected
            directly and through a USB 2.0 hub, can generally be achieved by queuing up multiple pending read and/or
            write requests simultaneously.  This can be done when using	asynchronous I/O operations (calling ReadFile() and
            WriteFile()	with overlapped structures).  The Microchip	HID USB Bootloader application uses asynchronous I/O
            for some USB operations and the source code can be used	as an example.*/


            Byte[] OUTBuffer = new byte[65];	//Allocate a memory buffer equal to the OUT endpoint size + 1
		    Byte[] INBuffer = new byte[65];		//Allocate a memory buffer equal to the IN endpoint size + 1
		    uint BytesWritten = 0;
            uint BytesRead = 0;
            byte byte_temp;
            //byte byte_temp2;
            ArrayList al_temp1 = new ArrayList();
            //ArrayList al_temp2 = new ArrayList();
            //ArrayList al_temp3 = new ArrayList();
            long l_address;
            long l_size;
            long l_comp_size;
            int i_send_data_pos;
            byte b_size;
            bool b_Error_Flag;
            bool b_usb_write_ret = false;

		    while(true)
		    {
                try
                {
                    if (AttachedState == true)	//Do not try to use the read/write handles unless the USB device is attached and ready
                    {

                        /* Flash Read */
                        if (b_FlashRead == true)
                        {
                            b_FlashRead = false;

                            //Get the pushbutton state from the microcontroller firmware.
                            OUTBuffer[0] = 0;			//The first byte is the "Report ID" and does not get sent over the USB bus.  Always set = 0.
                            OUTBuffer[1] = 0x11;		//0x21 is the "Get Pushbutton State" command in the firmware
                            OUTBuffer[2] = (byte)((l_ReadAddress >> 24) & 0xFF);		//Address
                            OUTBuffer[3] = (byte)((l_ReadAddress >> 16) & 0xFF);		//
                            OUTBuffer[4] = (byte)((l_ReadAddress >> 8) & 0xFF);		//
                            OUTBuffer[5] = (byte)(l_ReadAddress & 0xFF);		    //
                            OUTBuffer[6] = byte_ReadSize;		            //Size
                            for (uint i = 7; i < 65; i++)	//This loop is not strictly necessary.  Simply initializes unused bytes to
                                OUTBuffer[i] = 0xFF;				//0xFF for lower EMI and power consumption when driving the USB cable.

                            //To get the pushbutton state, first, we send a packet with our "Get Pushbutton State" command in it.
                            b_usb_write_ret = WriteFile(WriteHandleToUSBDevice, OUTBuffer, 65, ref BytesWritten, IntPtr.Zero);
                            if (b_usb_write_ret == true)	//Blocking function, unless an "overlapped" structure is used
                            {
                                //Now get the response packet from the firmware.
                                INBuffer[0] = 0;
                                {
                                    if (ReadFileManagedBuffer(ReadHandleToUSBDevice, INBuffer, 65, ref BytesRead, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                    {
                                        //INBuffer[0] is the report ID, which we don't care about.
                                        //INBuffer[1] is an echo back of the command (see microcontroller firmware).
                                        //INBuffer[2] contains the I/O port pin value for the pushbutton (see microcontroller firmware).  
                                        if (INBuffer[1] == 0x11)
                                        {
                                            if (INBuffer[2] == byte_ReadSize)
                                            {
                                                // OK
                                                FlashReadData[0] = INBuffer[2];
                                                for (int fi = 0; fi < INBuffer[2]; fi++)
                                                {
                                                    FlashReadData[1 + fi] = INBuffer[3 + fi];
                                                }
                                            }
                                            else
                                            {
                                                // NG
                                                FlashReadData[0] = INBuffer[2];
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                AttachedState = false;
                            }
                        }
                        /* Flash Write */
                        if (b_FlashWrite == true)
                        {
                            b_FlashWrite = false;

                            //Get the pushbutton state from the microcontroller firmware.
                            OUTBuffer[0] = 0;			//The first byte is the "Report ID" and does not get sent over the USB bus.  Always set = 0.
                            OUTBuffer[1] = 0x12;		//0x22 is the "Get Pushbutton State" command in the firmware
                            OUTBuffer[2] = (byte)((l_WriteAddress >> 24) & 0xFF);		//Address
                            OUTBuffer[3] = (byte)((l_WriteAddress >> 16) & 0xFF);		//
                            OUTBuffer[4] = (byte)((l_WriteAddress >> 8) & 0xFF);		    //
                            OUTBuffer[5] = (byte)(l_WriteAddress & 0xFF);		        //
                            OUTBuffer[6] = byte_WriteSize;		                           //Size
                            for (uint i = 0; i < byte_WriteSize; i++)
                            {
                                OUTBuffer[7 + i] = FlashWriteData[i];
                            }
                            for (uint i = (7 + (uint)byte_WriteSize); i < 65; i++)	//This loop is not strictly necessary.  Simply initializes unused bytes to
                                OUTBuffer[i] = 0xFF;				//0xFF for lower EMI and power consumption when driving the USB cable.

                            //To get the pushbutton state, first, we send a packet with our "Get Pushbutton State" command in it.
                            b_usb_write_ret = WriteFile(WriteHandleToUSBDevice, OUTBuffer, 65, ref BytesWritten, IntPtr.Zero);
                            if (b_usb_write_ret == true)	//Blocking function, unless an "overlapped" structure is used
                            {
                                //Now get the response packet from the firmware.
                                INBuffer[0] = 0;
                                {
                                    if (ReadFileManagedBuffer(ReadHandleToUSBDevice, INBuffer, 65, ref BytesRead, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                    {
                                        //INBuffer[0] is the report ID, which we don't care about.
                                        //INBuffer[1] is an echo back of the command (see microcontroller firmware).
                                        //INBuffer[2] contains the I/O port pin value for the pushbutton (see microcontroller firmware).  
                                        if (INBuffer[1] == 0x12)
                                        {
                                            //ANS
                                            byte_FlashWrite_Ans = INBuffer[2];
                                        }
                                    }
                                }
                            }
                            else
                            {
                                AttachedState = false;
                            }
                        }
                        /* Flash Erase */
                        if (b_FlashErase == true)
                        {
                            b_FlashErase = false;

                            //Get the pushbutton state from the microcontroller firmware.
                            OUTBuffer[0] = 0;			//The first byte is the "Report ID" and does not get sent over the USB bus.  Always set = 0.
                            OUTBuffer[1] = 0x13;		//0x22 is the "Get Pushbutton State" command in the firmware
                            OUTBuffer[2] = (byte)((l_EraseAddress >> 24) & 0xFF);		//Address
                            OUTBuffer[3] = (byte)((l_EraseAddress >> 16) & 0xFF);		//
                            OUTBuffer[4] = (byte)((l_EraseAddress >> 8) & 0xFF);		    //
                            OUTBuffer[5] = (byte)(l_EraseAddress & 0xFF);		        //
                            for (uint i = 6; i < 65; i++)	//This loop is not strictly necessary.  Simply initializes unused bytes to
                                OUTBuffer[i] = 0xFF;				//0xFF for lower EMI and power consumption when driving the USB cable.

                            //To get the pushbutton state, first, we send a packet with our "Get Pushbutton State" command in it.
                            b_usb_write_ret = WriteFile(WriteHandleToUSBDevice, OUTBuffer, 65, ref BytesWritten, IntPtr.Zero);
                            if (b_usb_write_ret == true)	//Blocking function, unless an "overlapped" structure is used
                            {
                                //Now get the response packet from the firmware.
                                INBuffer[0] = 0;
                                {
                                    if (ReadFileManagedBuffer(ReadHandleToUSBDevice, INBuffer, 65, ref BytesRead, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                    {
                                        //INBuffer[0] is the report ID, which we don't care about.
                                        //INBuffer[1] is an echo back of the command (see microcontroller firmware).
                                        //INBuffer[2] contains the I/O port pin value for the pushbutton (see microcontroller firmware).  
                                        if (INBuffer[1] == 0x13)
                                        {
                                            //ANS
                                            byte_FlashErase_Ans = INBuffer[2];
                                        }
                                    }
                                }
                            }
                            else
                            {
                                AttachedState = false;
                            }
                        }
                        // LED RGB Set REQ
                        if (debug_led_rgb_set_req == true)
                        {
                            debug_led_rgb_set_req = false;

                            //Get the pushbutton state from the microcontroller firmware.
                            OUTBuffer[0] = 0;			//The first byte is the "Report ID" and does not get sent over the USB bus.  Always set = 0.
                            OUTBuffer[1] = 0x63;		//0x81 is the "Get Pushbutton State" command in the firmware
                            if (chkbx_led_debug.Checked == true)
                            {   // デバッグ LED出力 有効
                                OUTBuffer[2] = 0x03;    // LED data num
                                OUTBuffer[3] = debug_led_brightness_level_set_val;    // LED Brightness Level
                                OUTBuffer[4] = debug_led_rgb_set_val[0];    // LED1 R
                                OUTBuffer[5] = debug_led_rgb_set_val[1];    // LED1 G
                                OUTBuffer[6] = debug_led_rgb_set_val[2];    // LED1 B
                            }
                            else
                            {   // デバッグ LED出力 無効
                                OUTBuffer[2] = 0x00;    // LED data num
                                OUTBuffer[3] = 0x00;    // LED Brightness Level
                                for (uint i = 4; i < 7; i++)
                                {
                                    OUTBuffer[i] = 0x00;
                                }
                            }

                            for (uint i = 7; i < 65; i++)	//This loop is not strictly necessary.  Simply initializes unused bytes to
                                OUTBuffer[i] = 0xFF;				//0xFF for lower EMI and power consumption when driving the USB cable.

                            //To get the pushbutton state, first, we send a packet with our "Get Pushbutton State" command in it.
                            if (WriteFile(WriteHandleToUSBDevice, OUTBuffer, 65, ref BytesWritten, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                            {
                                //Now get the response packet from the firmware.
                                INBuffer[0] = 0;
                                {
                                    if (ReadFileManagedBuffer(ReadHandleToUSBDevice, INBuffer, 65, ref BytesRead, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                    {
                                        //INBuffer[0] is the report ID, which we don't care about.
                                        //INBuffer[1] is an echo back of the command (see microcontroller firmware).
                                        //INBuffer[2] contains the I/O port pin value for the pushbutton (see microcontroller firmware).  
                                        if (INBuffer[1] == 0x63)
                                        {
                                        }
                                    }
                                }
                            }
                            else
                            {
                                AttachedState = false;
                            }
                        }
                        // LED Preview set req
                        if (LED_preview_set_flag == true)
                        {
                            LED_preview_set_flag = false;

                            //Get the pushbutton state from the microcontroller firmware.
                            OUTBuffer[0] = 0;			//The first byte is the "Report ID" and does not get sent over the USB bus.  Always set = 0.
                            OUTBuffer[1] = 0x64;		//0x81 is the "Get Pushbutton State" command in the firmware
                            OUTBuffer[2] = 3;   // LED SET DATA Num
                            OUTBuffer[3] = (byte)(Constants.LED_PREVIEW_TIME & 0xFF);
                            OUTBuffer[4] = (byte)((Constants.LED_PREVIEW_TIME >> 8) & 0xFF);
                            OUTBuffer[5] = LED_preview_brightness_level;   // LED Brightness Level

                            OUTBuffer[6] = LED_preview_set_data[0];
                            OUTBuffer[7] = LED_preview_set_data[1];
                            OUTBuffer[8] = LED_preview_set_data[2];

                            for (uint i = 9; i < 65; i++)	//This loop is not strictly necessary.  Simply initializes unused bytes to
                                OUTBuffer[i] = 0xFF;				//0xFF for lower EMI and power consumption when driving the USB cable.

                            //To get the pushbutton state, first, we send a packet with our "Get Pushbutton State" command in it.
                            if (WriteFile(WriteHandleToUSBDevice, OUTBuffer, 65, ref BytesWritten, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                            {
                                //Now get the response packet from the firmware.
                                INBuffer[0] = 0;
                                {
                                    if (ReadFileManagedBuffer(ReadHandleToUSBDevice, INBuffer, 65, ref BytesRead, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                    {
                                        //INBuffer[0] is the report ID, which we don't care about.
                                        //INBuffer[1] is an echo back of the command (see microcontroller firmware).
                                        //INBuffer[2] contains the I/O port pin value for the pushbutton (see microcontroller firmware).  
                                        if (INBuffer[1] == 0x64)
                                        {
                                        }
                                    }
                                }
                            }
                            else
                            {
                                AttachedState = false;
                            }
                        }

                        //VersionReadReq = false;
                        if (VersionReadReq == true)
                        {
                            VersionReadReq = false;

                            //Get the pushbutton state from the microcontroller firmware.
                            OUTBuffer[0] = 0;			//The first byte is the "Report ID" and does not get sent over the USB bus.  Always set = 0.
                            OUTBuffer[1] = 0x56;		//0x81 is the "Get Pushbutton State" command in the firmware

                            for (uint i = 2; i < 65; i++)	//This loop is not strictly necessary.  Simply initializes unused bytes to
                                OUTBuffer[i] = 0xFF;				//0xFF for lower EMI and power consumption when driving the USB cable.

                            //To get the pushbutton state, first, we send a packet with our "Get Pushbutton State" command in it.
                            if (WriteFile(WriteHandleToUSBDevice, OUTBuffer, 65, ref BytesWritten, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                            {
                                //Now get the response packet from the firmware.
                                INBuffer[0] = 0;
                                {
                                    if (ReadFileManagedBuffer(ReadHandleToUSBDevice, INBuffer, 65, ref BytesRead, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                    {
                                        //INBuffer[0] is the report ID, which we don't care about.
                                        //INBuffer[1] is an echo back of the command (see microcontroller firmware).
                                        //INBuffer[2] contains the I/O port pin value for the pushbutton (see microcontroller firmware).  
                                        if (INBuffer[1] == 0x56)
                                        {
                                            for (uint i = 2; i < 65; i++)
                                            {
                                                version_buff[i - 2] = INBuffer[i];
                                            }
                                            VersionReadComp = true;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                AttachedState = false;
                            }
                        }



                        // RESTORE関係
                        if (my_App_Setting_Data.Backup_Restore_Flag != Constants.BACKUP_FLAG_NON)
                        {
                            b_Error_Flag = false;

                            // リストアとリセット時は、再読み込み要求をセット
                            if (my_App_Setting_Data.Backup_Restore_Flag == Constants.BACKUP_FLAG_RESET || my_App_Setting_Data.Backup_Restore_Flag == Constants.BACKUP_FLAG_RESTORE)
                            {
                                // 基本設定情報読み込み要求
                                fData.Set_Flash_Read_Status(FlashControl.FM_READ_TYPE_BASE_SETTING, FlashControl.FM_READ_STATUS_RQ);
                                // 機能設定情報読み込み要求
                                fData.Set_Flash_Read_Status(FlashControl.FM_READ_TYPE_FUNCTION_SETTING, FlashControl.FM_READ_STATUS_RQ);
                                // エンコーダスクリプト設定情報読み込み要求
                                fData.Set_Flash_Read_Status(FlashControl.FM_READ_TYPE_ENCODER_SCRIPT_SETTING, FlashControl.FM_READ_STATUS_RQ);
                                // スクリプト情報読み込み要求
                                fData.Set_Flash_Read_Status(FlashControl.FM_READ_TYPE_SCRIPT_INFO, FlashControl.FM_READ_STATUS_RQ);
                                FlashReadFirstTime = true;
                            }

                            // RESTOREデータの読み出し
                            if (b_Error_Flag == false && my_App_Setting_Data.Backup_Restore_Flag == Constants.BACKUP_FLAG_RESTORE)
                            {
                                if (my_Load_Backup_File(my_App_Setting_Data.Backup_file_Path, ref al_temp1) == true)
                                {   //読み込み異常
                                    b_Error_Flag = true;
                                    my_App_Setting_Data.Backup_Restore_Error_Code = 1001;
                                }
                            }
                            // RESET時でプリセットファイル指定時
                            if (b_Error_Flag == false && my_App_Setting_Data.Backup_Restore_Flag == Constants.BACKUP_FLAG_RESET && my_App_Setting_Data.Backup_file_Path != "")
                            {
                                // リストアに変更
                                my_App_Setting_Data.Backup_Restore_Flag = Constants.BACKUP_FLAG_RESTORE;

                                if (my_Load_Backup_File(my_App_Setting_Data.Backup_file_Path, ref al_temp1) == true)
                                {   //読み込み異常
                                    b_Error_Flag = true;
                                    my_App_Setting_Data.Backup_Restore_Error_Code = 1001;
                                }
                            }

                            // 全セクター消去
                            if (b_Error_Flag == false && (my_App_Setting_Data.Backup_Restore_Flag == Constants.BACKUP_FLAG_RESET || my_App_Setting_Data.Backup_Restore_Flag == Constants.BACKUP_FLAG_RESTORE))
                            {
                                l_address = 0;
                                l_comp_size = 0;
                                i_send_data_pos = 0;

                                while (l_address < (long)FlashControl.FM_TOTAL_SIZE && b_Error_Flag == false)
                                {
                                    //進捗更新
                                    my_App_Setting_Data.Backup_Restore_Progress_Value++;

                                    // セクター消去
                                    //Get the pushbutton state from the microcontroller firmware.
                                    OUTBuffer[0] = 0;			//The first byte is the "Report ID" and does not get sent over the USB bus.  Always set = 0.
                                    OUTBuffer[1] = 0x13;		//0x22 is the "Get Pushbutton State" command in the firmware
                                    OUTBuffer[2] = (byte)((l_address >> 24) & 0xFF);		//Address
                                    OUTBuffer[3] = (byte)((l_address >> 16) & 0xFF);		//
                                    OUTBuffer[4] = (byte)((l_address >> 8) & 0xFF);		    //
                                    OUTBuffer[5] = (byte)(l_address & 0xFF);		        //
                                    for (uint i = 6; i < 65; i++)	//This loop is not strictly necessary.  Simply initializes unused bytes to
                                        OUTBuffer[i] = 0xFF;				//0xFF for lower EMI and power consumption when driving the USB cable.

                                    //To get the pushbutton state, first, we send a packet with our "Get Pushbutton State" command in it.
                                    if (WriteFile(WriteHandleToUSBDevice, OUTBuffer, 65, ref BytesWritten, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                    {
                                        //Now get the response packet from the firmware.
                                        INBuffer[0] = 0;
                                        {
                                            if (ReadFileManagedBuffer(ReadHandleToUSBDevice, INBuffer, 65, ref BytesRead, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                            {
                                                //INBuffer[0] is the report ID, which we don't care about.
                                                //INBuffer[1] is an echo back of the command (see microcontroller firmware).
                                                //INBuffer[2] contains the I/O port pin value for the pushbutton (see microcontroller firmware).  
                                                if (INBuffer[1] == 0x13)
                                                {
                                                    //ANS
                                                    if (INBuffer[2] != 0x00)
                                                    {
                                                        b_Error_Flag = true;
                                                        my_App_Setting_Data.Backup_Restore_Error_Code = 2001;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        AttachedState = false;
                                    }

                                    // 次の消去セクター
                                    l_address += (long)FlashControl.FM_SECTOR_SIZE;
                                }
                            }

                            // 全データ読み出し
                            if (b_Error_Flag == false && my_App_Setting_Data.Backup_Restore_Flag == Constants.BACKUP_FLAG_BACKUP)
                            {
                                al_temp1.Clear();
                                l_address = 0;
                                l_comp_size = 0;

                                while (l_comp_size < FlashControl.FM_TOTAL_SIZE && b_Error_Flag == false)
                                {
                                    //進捗更新
                                    my_App_Setting_Data.Backup_Restore_Progress_Value++;

                                    l_size = FlashControl.FM_TOTAL_SIZE - l_comp_size;
                                    if (l_size > FlashControl.FM_USB_READ_DATA_SIZE)
                                    {
                                        l_size = FlashControl.FM_USB_READ_DATA_SIZE;
                                    }
                                    l_comp_size += l_size;

                                    //Get the pushbutton state from the microcontroller firmware.
                                    OUTBuffer[0] = 0;			//The first byte is the "Report ID" and does not get sent over the USB bus.  Always set = 0.
                                    OUTBuffer[1] = 0x11;		//0x21 is the "Get Pushbutton State" command in the firmware
                                    OUTBuffer[2] = (byte)((l_address >> 24) & 0xFF);    //Address
                                    OUTBuffer[3] = (byte)((l_address >> 16) & 0xFF);    //
                                    OUTBuffer[4] = (byte)((l_address >> 8) & 0xFF);     //
                                    OUTBuffer[5] = (byte)(l_address & 0xFF);            //
                                    b_size = (byte)(l_size & 0xFF);
                                    OUTBuffer[6] = b_size;               //Size
                                    for (uint i = 7; i < 65; i++)	//This loop is not strictly necessary.  Simply initializes unused bytes to
                                        OUTBuffer[i] = 0xFF;				//0xFF for lower EMI and power consumption when driving the USB cable.

                                    //To get the pushbutton state, first, we send a packet with our "Get Pushbutton State" command in it.
                                    if (WriteFile(WriteHandleToUSBDevice, OUTBuffer, 65, ref BytesWritten, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                    {
                                        //Now get the response packet from the firmware.
                                        INBuffer[0] = 0;
                                        {
                                            if (ReadFileManagedBuffer(ReadHandleToUSBDevice, INBuffer, 65, ref BytesRead, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                            {
                                                //INBuffer[0] is the report ID, which we don't care about.
                                                //INBuffer[1] is an echo back of the command (see microcontroller firmware).
                                                //INBuffer[2] contains the I/O port pin value for the pushbutton (see microcontroller firmware).  
                                                if (INBuffer[1] == 0x11)
                                                {
                                                    if (INBuffer[2] == b_size)
                                                    {
                                                        // OK
                                                        for (int fi = 0; fi < INBuffer[2]; fi++)
                                                        {
                                                            al_temp1.Add(INBuffer[3 + fi]);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // NG
                                                        b_Error_Flag = true;
                                                        my_App_Setting_Data.Backup_Restore_Error_Code = 3001;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        AttachedState = false;
                                    }

                                    l_address += l_size;
                                }


                                if (al_temp1.Count == FlashControl.FM_TOTAL_SIZE)
                                {
                                    my_Save_Backup_File(my_App_Setting_Data.Backup_file_Path, al_temp1);
                                }
                            }

                            // 全データ書き込み
                            if (b_Error_Flag == false && my_App_Setting_Data.Backup_Restore_Flag == Constants.BACKUP_FLAG_RESTORE)
                            {
                                l_address = 0;
                                l_comp_size = 0;
                                i_send_data_pos = 0;

                                while (l_comp_size < FlashControl.FM_TOTAL_SIZE && b_Error_Flag == false)
                                {
                                    //進捗更新
                                    my_App_Setting_Data.Backup_Restore_Progress_Value++;

                                    l_size = FlashControl.FM_TOTAL_SIZE - l_comp_size;
                                    if (l_size > FlashControl.FM_USB_WRITE_DATA_SIZE)
                                    {
                                        l_size = FlashControl.FM_USB_WRITE_DATA_SIZE;
                                    }
                                    // 0x100ごとのセクションをまたぐときは分割
                                    long l_temp = 0x100 - (l_address & 0xFF);
                                    if (l_temp < l_size)
                                    {
                                        l_size = l_temp;
                                    }
                                    l_comp_size += l_size;


                                    OUTBuffer[0] = 0;			//The first byte is the "Report ID" and does not get sent over the USB bus.  Always set = 0.
                                    OUTBuffer[1] = 0x12;		//0x22 is the "Get Pushbutton State" command in the firmware
                                    OUTBuffer[2] = (byte)((l_address >> 24) & 0xFF);		//Address
                                    OUTBuffer[3] = (byte)((l_address >> 16) & 0xFF);		//
                                    OUTBuffer[4] = (byte)((l_address >> 8) & 0xFF);		//
                                    OUTBuffer[5] = (byte)(l_address & 0xFF);		    //

                                    //送信バイトデータを出力バッファにコピー
                                    for (int fi = 0; fi < l_size; fi++, i_send_data_pos++)
                                    {
                                        OUTBuffer[7 + fi] = (byte)al_temp1[i_send_data_pos];
                                    }
                                    OUTBuffer[6] = (byte)(l_size & 0xFF);		        //Size

                                    //To get the pushbutton state, first, we send a packet with our "Get Pushbutton State" command in it.
                                    if (WriteFile(WriteHandleToUSBDevice, OUTBuffer, 65, ref BytesWritten, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                    {
                                        //Now get the response packet from the firmware.
                                        INBuffer[0] = 0;
                                        {
                                            if (ReadFileManagedBuffer(ReadHandleToUSBDevice, INBuffer, 65, ref BytesRead, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                            {
                                                //INBuffer[0] is the report ID, which we don't care about.
                                                //INBuffer[1] is an echo back of the command (see microcontroller firmware).
                                                //INBuffer[2] contains the I/O port pin value for the pushbutton (see microcontroller firmware).  
                                                if (INBuffer[1] == 0x12)
                                                {
                                                    //ANS
                                                    if (INBuffer[2] != 0x00)
                                                    {
                                                        b_Error_Flag = true;
                                                        my_App_Setting_Data.Backup_Restore_Error_Code = 4001;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        AttachedState = false;
                                    }

                                    l_address += l_size;
                                }
                            }

                            //
                            //my_App_Setting_Data.Backup_Restore_Progress_Value = pgb_process_status.Maximum;
                            my_App_Setting_Data.Backup_Restore_Flag = Constants.BACKUP_FLAG_NON;

                            // バッファクリア
                            al_temp1.Clear();

                        }

                        // 基本設定変更要求
                        if (set_base_info_flag == true)
                        {
                            set_base_info_flag = false;
                            // 基本
                            OUTBuffer[0] = 0;			//The first byte is the "Report ID" and does not get sent over the USB bus.  Always set = 0.
                            OUTBuffer[1] = 0x34;		//0x81 is the "Get Pushbutton State" command in the firmware

                            OUTBuffer[2] = my_base_info.mode;           // Mode
                            OUTBuffer[3] = my_base_info.led_sleep;      // led sleep
                            OUTBuffer[4] = my_base_info.led_light_mode; // led light mode
                            OUTBuffer[5] = my_base_info.led_light_func; // led light func
                            OUTBuffer[6] = my_base_info.led_off_time;   // led off time
                            OUTBuffer[7] = my_base_info.encoder_typematic;   // encoder typematic
                            for (uint i = 8; i < 65; i++)	//This loop is not strictly necessary.  Simply initializes unused bytes to
                                OUTBuffer[i] = 0xFF;				//0xFF for lower EMI and power consumption when driving the USB cable.

                            //To get the pushbutton state, first, we send a packet with our "Get Pushbutton State" command in it.
                            if (WriteFile(WriteHandleToUSBDevice, OUTBuffer, 65, ref BytesWritten, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                            {
                                //Now get the response packet from the firmware.
                                INBuffer[0] = 0;
                                {
                                    if (ReadFileManagedBuffer(ReadHandleToUSBDevice, INBuffer, 65, ref BytesRead, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                    {
                                        //INBuffer[0] is the report ID, which we don't care about.
                                        //INBuffer[1] is an echo back of the command (see microcontroller firmware).
                                        //INBuffer[2] contains the I/O port pin value for the pushbutton (see microcontroller firmware).  
                                        if (INBuffer[1] == 0x34)
                                        {
                                            if (INBuffer[2] == 0x00)
                                            {   // OK
                                            }
                                            else
                                            {   // NG
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                AttachedState = false;
                            }


                            // モード数分　設定
                            for (int fi = 0; fi < Constants.MODE_NUM; fi++)
                            {
                                uint outbuffer_idx = 0;
                                //Get the pushbutton state from the microcontroller firmware.
                                OUTBuffer[outbuffer_idx++] = 0;			//The first byte is the "Report ID" and does not get sent over the USB bus.  Always set = 0.
                                OUTBuffer[outbuffer_idx++] = 0x36;		//0x81 is the "Get Pushbutton State" command in the firmware

                                OUTBuffer[outbuffer_idx++] = (byte)(fi & 0xFF);  // Mode No.
                                for (int fj = 0; fj < Constants.BUTTON_NUM; fj++)
                                {
                                    OUTBuffer[outbuffer_idx++] = my_base_mode_infos.base_mode_infos[fi].sw_exe_script_no[fj]; // Button Script exe No.
                                }
                                for (int fj = 0; fj < Constants.BUTTON_NUM; fj++)
                                {
                                    OUTBuffer[outbuffer_idx++] = my_base_mode_infos.base_mode_infos[fi].sw_sp_func_no[fj]; // Button sp function No.
                                }
                                OUTBuffer[outbuffer_idx++] = my_base_mode_infos.base_mode_infos[fi].encoder_func_no;          // エンコーダーデフォルト機能番号
                                OUTBuffer[outbuffer_idx++] = my_base_mode_infos.base_mode_infos[fi].LED_color_no;             // LEDカラー番号
                                OUTBuffer[outbuffer_idx++] = my_base_mode_infos.base_mode_infos[fi].LED_color_detail_flag;    // LEDカラー詳細フラグ
                                for (int fj = 0; fj < Constants.LED_RGB_COLOR_NUM; fj++)
                                {
                                    OUTBuffer[outbuffer_idx++] = my_base_mode_infos.base_mode_infos[fi].LED_RGB_duty[fj];     // LED RGB Duty
                                }
                                OUTBuffer[outbuffer_idx++] = my_base_mode_infos.base_mode_infos[fi].LED_brightness_level;     // LED Brightness Level

                                for (uint i = outbuffer_idx; i < 65; i++)	//This loop is not strictly necessary.  Simply initializes unused bytes to
                                    OUTBuffer[i] = 0xFF;				//0xFF for lower EMI and power consumption when driving the USB cable.

                                //To get the pushbutton state, first, we send a packet with our "Get Pushbutton State" command in it.
                                if (WriteFile(WriteHandleToUSBDevice, OUTBuffer, 65, ref BytesWritten, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                {
                                    //Now get the response packet from the firmware.
                                    INBuffer[0] = 0;
                                    {
                                        if (ReadFileManagedBuffer(ReadHandleToUSBDevice, INBuffer, 65, ref BytesRead, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                        {
                                            //INBuffer[0] is the report ID, which we don't care about.
                                            //INBuffer[1] is an echo back of the command (see microcontroller firmware).
                                            //INBuffer[2] contains the I/O port pin value for the pushbutton (see microcontroller firmware).  
                                            if (INBuffer[1] == 0x36)
                                            {
                                                if (INBuffer[2] == 0x00)
                                                {   // OK
                                                }
                                                else
                                                {   // NG
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    AttachedState = false;
                                }
                            }
                        }
                        // 機能設定変更要求
                        if (set_func_setting_flag == true)
                        {
                            set_func_setting_flag = false;

                            // モード数分　設定
                            for (int mode_no = 0; mode_no < Constants.MODE_NUM; mode_no++)
                            {
                                // 機能数分　設定
                                for (int func_no = 0; func_no < Constants.FUNCTION_NUM; func_no++)
                                {
                                    uint outbuffer_idx = 0;
                                    //Get the pushbutton state from the microcontroller firmware.
                                    OUTBuffer[outbuffer_idx++] = 0;			//The first byte is the "Report ID" and does not get sent over the USB bus.  Always set = 0.
                                    OUTBuffer[outbuffer_idx++] = 0x38;		//0x81 is the "Get Pushbutton State" command in the firmware

                                    OUTBuffer[outbuffer_idx++] = (byte)(mode_no & 0xFF);  // Mode No.
                                    OUTBuffer[outbuffer_idx++] = (byte)(func_no & 0xFF);  // func No.
                                    for (int cw_ccw_idx = 0; cw_ccw_idx < Constants.CW_CCW_NUM; cw_ccw_idx++)
                                    {
                                        for (int fi = 0; fi < Constants.FUNCTION_CWCCW_DATA_LEN; fi++)
                                        {
                                            OUTBuffer[outbuffer_idx++] = my_func_datas.func_datas[mode_no].func_data[func_no].cw_ccw_data[cw_ccw_idx, fi];
                                        }
                                    }
                                    OUTBuffer[outbuffer_idx++] = my_func_datas.func_datas[mode_no].func_data[func_no].LED_color_no;             // LEDカラー番号
                                    OUTBuffer[outbuffer_idx++] = my_func_datas.func_datas[mode_no].func_data[func_no].LED_color_detail_flag;    // LEDカラー詳細フラグ
                                    for (int fj = 0; fj < Constants.LED_RGB_COLOR_NUM; fj++)
                                    {
                                        OUTBuffer[outbuffer_idx++] = my_func_datas.func_datas[mode_no].func_data[func_no].LED_RGB_duty[fj];     // LED RGB Duty
                                    }
                                    OUTBuffer[outbuffer_idx++] = my_func_datas.func_datas[mode_no].func_data[func_no].LED_brightness_level;     // LED Brightness Level

                                    for (uint i = outbuffer_idx; i < 65; i++)	//This loop is not strictly necessary.  Simply initializes unused bytes to
                                        OUTBuffer[i] = 0xFF;				//0xFF for lower EMI and power consumption when driving the USB cable.

                                    //To get the pushbutton state, first, we send a packet with our "Get Pushbutton State" command in it.
                                    if (WriteFile(WriteHandleToUSBDevice, OUTBuffer, 65, ref BytesWritten, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                    {
                                        //Now get the response packet from the firmware.
                                        INBuffer[0] = 0;
                                        {
                                            if (ReadFileManagedBuffer(ReadHandleToUSBDevice, INBuffer, 65, ref BytesRead, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                            {
                                                //INBuffer[0] is the report ID, which we don't care about.
                                                //INBuffer[1] is an echo back of the command (see microcontroller firmware).
                                                //INBuffer[2] contains the I/O port pin value for the pushbutton (see microcontroller firmware).  
                                                if (INBuffer[1] == 0x38)
                                                {
                                                    if (INBuffer[2] == 0x00)
                                                    {   // OK
                                                    }
                                                    else
                                                    {   // NG
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        AttachedState = false;
                                    }
                                }
                            }
                        }
                        if (set_encoder_script_flag == true)
                        {
                            set_encoder_script_flag = false;

                            // エンコーダースクリプト設定数分　設定
                            for (int encoder_script_idx = 0; encoder_script_idx < Constants.ENCODER_SCRIPT_NUM; encoder_script_idx++)
                            {
                                uint outbuffer_idx = 0;
                                //Get the pushbutton state from the microcontroller firmware.
                                OUTBuffer[outbuffer_idx++] = 0;			//The first byte is the "Report ID" and does not get sent over the USB bus.  Always set = 0.
                                OUTBuffer[outbuffer_idx++] = 0x3A;		//0x81 is the "Get Pushbutton State" command in the firmware

                                OUTBuffer[outbuffer_idx++] = (byte)(encoder_script_idx & 0xFF);  // No.
                                OUTBuffer[outbuffer_idx++] = my_encoder_script_datas.encoder_script_datas[encoder_script_idx].rec_num;  // rec num
                                OUTBuffer[outbuffer_idx++] = my_encoder_script_datas.encoder_script_datas[encoder_script_idx].loop_flag;  // loop flag
                                for (int script_no = 0; script_no < my_encoder_script_datas.encoder_script_datas[encoder_script_idx].script_no.Length; script_no++)
                                {
                                    OUTBuffer[outbuffer_idx++] = my_encoder_script_datas.encoder_script_datas[encoder_script_idx].script_no[script_no];
                                }

                                for (uint i = outbuffer_idx; i < 65; i++)	//This loop is not strictly necessary.  Simply initializes unused bytes to
                                    OUTBuffer[i] = 0xFF;				//0xFF for lower EMI and power consumption when driving the USB cable.

                                //To get the pushbutton state, first, we send a packet with our "Get Pushbutton State" command in it.
                                if (WriteFile(WriteHandleToUSBDevice, OUTBuffer, 65, ref BytesWritten, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                {
                                    //Now get the response packet from the firmware.
                                    INBuffer[0] = 0;
                                    {
                                        if (ReadFileManagedBuffer(ReadHandleToUSBDevice, INBuffer, 65, ref BytesRead, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                        {
                                            //INBuffer[0] is the report ID, which we don't care about.
                                            //INBuffer[1] is an echo back of the command (see microcontroller firmware).
                                            //INBuffer[2] contains the I/O port pin value for the pushbutton (see microcontroller firmware).  
                                            if (INBuffer[1] == 0x3A)
                                            {
                                                if (INBuffer[2] == 0x00)
                                                {   // OK
                                                }
                                                else
                                                {   // NG
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    AttachedState = false;
                                }
                            }
                        }
                        // SW機能設定変更要求
                        if (set_sw_func_setting_flag == true)
                        {
                            set_sw_func_setting_flag = false;

                            // モード数分　設定
                            for (int mode_no = 0; mode_no < Constants.MODE_NUM; mode_no++)
                            {
                                // SW数分　設定
                                for (int sw_no = 0; sw_no < Constants.BUTTON_NUM; sw_no++)
                                {
                                    uint outbuffer_idx = 0;
                                    //Get the pushbutton state from the microcontroller firmware.
                                    OUTBuffer[outbuffer_idx++] = 0;			//The first byte is the "Report ID" and does not get sent over the USB bus.  Always set = 0.
                                    OUTBuffer[outbuffer_idx++] = 0x3C;		//0x81 is the "Get Pushbutton State" command in the firmware

                                    OUTBuffer[outbuffer_idx++] = (byte)(mode_no & 0xFF);  // Mode No.
                                    OUTBuffer[outbuffer_idx++] = (byte)(sw_no & 0xFF);  // func No.
                                    for (int fi = 0; fi < Constants.SW_FUNCTION_DATA_LEN; fi++)
                                    {
                                        OUTBuffer[outbuffer_idx++] = my_sw_func_datas.sw_func_datas[mode_no].sw_func_data[sw_no].sw_data[fi];
                                    }

                                    for (uint i = outbuffer_idx; i < 65; i++)	//This loop is not strictly necessary.  Simply initializes unused bytes to
                                        OUTBuffer[i] = 0xFF;				//0xFF for lower EMI and power consumption when driving the USB cable.

                                    //To get the pushbutton state, first, we send a packet with our "Get Pushbutton State" command in it.
                                    if (WriteFile(WriteHandleToUSBDevice, OUTBuffer, 65, ref BytesWritten, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                    {
                                        //Now get the response packet from the firmware.
                                        INBuffer[0] = 0;
                                        {
                                            if (ReadFileManagedBuffer(ReadHandleToUSBDevice, INBuffer, 65, ref BytesRead, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                            {
                                                //INBuffer[0] is the report ID, which we don't care about.
                                                //INBuffer[1] is an echo back of the command (see microcontroller firmware).
                                                //INBuffer[2] contains the I/O port pin value for the pushbutton (see microcontroller firmware).  
                                                if (INBuffer[1] == 0x3C)
                                                {
                                                    if (INBuffer[2] == 0x00)
                                                    {   // OK
                                                    }
                                                    else
                                                    {   // NG
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        AttachedState = false;
                                    }
                                }
                            }
                        }


                        // 基本設定情報または機能設定情報、エンコーダースクリプト設定情報、SW機能設定情報　書き込み要求あり？
                        byte ws_base_setting = fData.Get_Flash_Write_Status(FlashControl.FM_WRITE_TYPE_BASE_SETTING);
                        byte ws_function_setting = fData.Get_Flash_Write_Status(FlashControl.FM_WRITE_TYPE_FUNCTION_SETTING);
                        byte ws_encoder_script_setting = fData.Get_Flash_Write_Status(FlashControl.FM_WRITE_TYPE_ENCODER_SCRIPT_SETTING);
                        byte ws_sw_function_setting = fData.Get_Flash_Write_Status(FlashControl.FM_WRITE_TYPE_SW_FUNCTION_SETTING);
                        if (ws_base_setting == FlashControl.FM_WRITE_STATUS_RQ || ws_function_setting == FlashControl.FM_WRITE_STATUS_RQ || ws_encoder_script_setting == FlashControl.FM_WRITE_STATUS_RQ || ws_sw_function_setting == FlashControl.FM_WRITE_STATUS_RQ)
                        {
                            //状態を書込み中に変更
                            fData.Set_Flash_Write_Status(FlashControl.FM_WRITE_TYPE_BASE_SETTING, FlashControl.FM_WRITE_STATUS_WRITTING);
                            fData.Set_Flash_Write_Status(FlashControl.FM_WRITE_TYPE_FUNCTION_SETTING, FlashControl.FM_WRITE_STATUS_WRITTING);
                            fData.Set_Flash_Write_Status(FlashControl.FM_WRITE_TYPE_ENCODER_SCRIPT_SETTING, FlashControl.FM_WRITE_STATUS_WRITTING);
                            fData.Set_Flash_Write_Status(FlashControl.FM_WRITE_TYPE_SW_FUNCTION_SETTING, FlashControl.FM_WRITE_STATUS_WRITTING);

                            l_address = FlashControl.FM_ADR_BASE_INFO;
                            l_comp_size = 0;
                            i_send_data_pos = 0;
                            b_Error_Flag = false;

                            // セクター消去
                            //Get the pushbutton state from the microcontroller firmware.
                            OUTBuffer[0] = 0;			//The first byte is the "Report ID" and does not get sent over the USB bus.  Always set = 0.
                            OUTBuffer[1] = 0x13;		//0x22 is the "Get Pushbutton State" command in the firmware
                            OUTBuffer[2] = (byte)((l_address >> 24) & 0xFF);		//Address
                            OUTBuffer[3] = (byte)((l_address >> 16) & 0xFF);		//
                            OUTBuffer[4] = (byte)((l_address >> 8) & 0xFF);		    //
                            OUTBuffer[5] = (byte)(l_address & 0xFF);		        //
                            for (uint i = 6; i < 65; i++)	//This loop is not strictly necessary.  Simply initializes unused bytes to
                                OUTBuffer[i] = 0xFF;				//0xFF for lower EMI and power consumption when driving the USB cable.

                            //To get the pushbutton state, first, we send a packet with our "Get Pushbutton State" command in it.
                            if (WriteFile(WriteHandleToUSBDevice, OUTBuffer, 65, ref BytesWritten, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                            {
                                //Now get the response packet from the firmware.
                                INBuffer[0] = 0;
                                {
                                    if (ReadFileManagedBuffer(ReadHandleToUSBDevice, INBuffer, 65, ref BytesRead, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                    {
                                        //INBuffer[0] is the report ID, which we don't care about.
                                        //INBuffer[1] is an echo back of the command (see microcontroller firmware).
                                        //INBuffer[2] contains the I/O port pin value for the pushbutton (see microcontroller firmware).  
                                        if (INBuffer[1] == 0x13)
                                        {
                                            //ANS
                                            if (INBuffer[2] != 0x00)
                                            {
                                                b_Error_Flag = true;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                AttachedState = false;
                            }

                            // エラー無し
                            if (b_Error_Flag == false)
                            {
                                // 基本設定情報を書き込み
                                l_address = FlashControl.FM_ADR_BASE_INFO;
                                l_comp_size = 0;
                                i_send_data_pos = 0;

                                // 基本設定情報書き込みデータ取得
                                fData.Get_Flash_Write_Base_Info(my_base_info, my_base_mode_infos, ref al_temp1);

                                while (l_comp_size < FlashControl.FM_BASE_INFO_AREA_SIZE && b_Error_Flag == false)
                                {
                                    l_size = FlashControl.FM_BASE_INFO_AREA_SIZE - l_comp_size;
                                    if (l_size > FlashControl.FM_USB_WRITE_DATA_SIZE)
                                    {
                                        l_size = FlashControl.FM_USB_WRITE_DATA_SIZE;
                                    }
                                    // 0x100ごとのセクションをまたぐときは分割
                                    long l_temp = 0x100 - (l_address & 0xFF);
                                    if (l_temp < l_size)
                                    {
                                        l_size = l_temp;
                                    }
                                    l_comp_size += l_size;


                                    OUTBuffer[0] = 0;			//The first byte is the "Report ID" and does not get sent over the USB bus.  Always set = 0.
                                    OUTBuffer[1] = 0x12;		//0x22 is the "Get Pushbutton State" command in the firmware
                                    OUTBuffer[2] = (byte)((l_address >> 24) & 0xFF);		//Address
                                    OUTBuffer[3] = (byte)((l_address >> 16) & 0xFF);		//
                                    OUTBuffer[4] = (byte)((l_address >> 8) & 0xFF);		//
                                    OUTBuffer[5] = (byte)(l_address & 0xFF);		    //

                                    //送信バイトデータを出力バッファにコピー
                                    for (int fi = 0; fi < l_size; fi++, i_send_data_pos++)
                                    {
                                        OUTBuffer[7 + fi] = (byte)al_temp1[i_send_data_pos];
                                    }
                                    OUTBuffer[6] = (byte)(l_size & 0xFF);		        //Size

                                    //To get the pushbutton state, first, we send a packet with our "Get Pushbutton State" command in it.
                                    if (WriteFile(WriteHandleToUSBDevice, OUTBuffer, 65, ref BytesWritten, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                    {
                                        //Now get the response packet from the firmware.
                                        INBuffer[0] = 0;
                                        {
                                            if (ReadFileManagedBuffer(ReadHandleToUSBDevice, INBuffer, 65, ref BytesRead, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                            {
                                                //INBuffer[0] is the report ID, which we don't care about.
                                                //INBuffer[1] is an echo back of the command (see microcontroller firmware).
                                                //INBuffer[2] contains the I/O port pin value for the pushbutton (see microcontroller firmware).  
                                                if (INBuffer[1] == 0x12)
                                                {
                                                    //ANS
                                                    if (INBuffer[2] != 0x00)
                                                    {
                                                        b_Error_Flag = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        AttachedState = false;
                                    }

                                    l_address += l_size;
                                }

                                // 機能設定情報を書き込み
                                l_address = FlashControl.FM_ADR_FUNCTION_SETTING;
                                l_comp_size = 0;
                                i_send_data_pos = 0;

                                // 機能設定情報書き込みデータ取得
                                fData.Get_Flash_Write_Function_Info(my_func_datas, ref al_temp1, my_app_sw_datas, my_app_func_datas);

                                while (l_comp_size < FlashControl.FM_FUNCTION_SETTING_AREA_SIZE && b_Error_Flag == false)
                                {
                                    l_size = FlashControl.FM_FUNCTION_SETTING_AREA_SIZE - l_comp_size;
                                    if (l_size > FlashControl.FM_USB_WRITE_DATA_SIZE)
                                    {
                                        l_size = FlashControl.FM_USB_WRITE_DATA_SIZE;
                                    }
                                    // 0x100ごとのセクションをまたぐときは分割
                                    long l_temp = 0x100 - (l_address & 0xFF);
                                    if (l_temp < l_size)
                                    {
                                        l_size = l_temp;
                                    }
                                    l_comp_size += l_size;


                                    OUTBuffer[0] = 0;			//The first byte is the "Report ID" and does not get sent over the USB bus.  Always set = 0.
                                    OUTBuffer[1] = 0x12;		//0x22 is the "Get Pushbutton State" command in the firmware
                                    OUTBuffer[2] = (byte)((l_address >> 24) & 0xFF);		//Address
                                    OUTBuffer[3] = (byte)((l_address >> 16) & 0xFF);		//
                                    OUTBuffer[4] = (byte)((l_address >> 8) & 0xFF);		//
                                    OUTBuffer[5] = (byte)(l_address & 0xFF);		    //

                                    //送信バイトデータを出力バッファにコピー
                                    for (int fi = 0; fi < l_size; fi++, i_send_data_pos++)
                                    {
                                        OUTBuffer[7 + fi] = (byte)al_temp1[i_send_data_pos];
                                    }
                                    OUTBuffer[6] = (byte)(l_size & 0xFF);		        //Size

                                    //To get the pushbutton state, first, we send a packet with our "Get Pushbutton State" command in it.
                                    if (WriteFile(WriteHandleToUSBDevice, OUTBuffer, 65, ref BytesWritten, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                    {
                                        //Now get the response packet from the firmware.
                                        INBuffer[0] = 0;
                                        {
                                            if (ReadFileManagedBuffer(ReadHandleToUSBDevice, INBuffer, 65, ref BytesRead, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                            {
                                                //INBuffer[0] is the report ID, which we don't care about.
                                                //INBuffer[1] is an echo back of the command (see microcontroller firmware).
                                                //INBuffer[2] contains the I/O port pin value for the pushbutton (see microcontroller firmware).  
                                                if (INBuffer[1] == 0x12)
                                                {
                                                    //ANS
                                                    if (INBuffer[2] != 0x00)
                                                    {
                                                        b_Error_Flag = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        AttachedState = false;
                                    }

                                    l_address += l_size;
                                }

                                // エンコーダスクリプト設定情報を書き込み
                                l_address = FlashControl.FM_ADR_ENCODER_SCRIPT_SETTING;
                                l_comp_size = 0;
                                i_send_data_pos = 0;

                                // エンコーダスクリプト設定情報書き込みデータ取得
                                fData.Get_Flash_Write_Encoder_Script_Info(my_encoder_script_datas, ref al_temp1);

                                while (l_comp_size < FlashControl.FM_ENCODER_SCRIPT_SETTING_AREA_SIZE && b_Error_Flag == false)
                                {
                                    l_size = FlashControl.FM_ENCODER_SCRIPT_SETTING_AREA_SIZE - l_comp_size;
                                    if (l_size > FlashControl.FM_USB_WRITE_DATA_SIZE)
                                    {
                                        l_size = FlashControl.FM_USB_WRITE_DATA_SIZE;
                                    }
                                    // 0x100ごとのセクションをまたぐときは分割
                                    long l_temp = 0x100 - (l_address & 0xFF);
                                    if (l_temp < l_size)
                                    {
                                        l_size = l_temp;
                                    }
                                    l_comp_size += l_size;


                                    OUTBuffer[0] = 0;			//The first byte is the "Report ID" and does not get sent over the USB bus.  Always set = 0.
                                    OUTBuffer[1] = 0x12;		//0x22 is the "Get Pushbutton State" command in the firmware
                                    OUTBuffer[2] = (byte)((l_address >> 24) & 0xFF);		//Address
                                    OUTBuffer[3] = (byte)((l_address >> 16) & 0xFF);		//
                                    OUTBuffer[4] = (byte)((l_address >> 8) & 0xFF);		//
                                    OUTBuffer[5] = (byte)(l_address & 0xFF);		    //

                                    //送信バイトデータを出力バッファにコピー
                                    for (int fi = 0; fi < l_size; fi++, i_send_data_pos++)
                                    {
                                        OUTBuffer[7 + fi] = (byte)al_temp1[i_send_data_pos];
                                    }
                                    OUTBuffer[6] = (byte)(l_size & 0xFF);		        //Size

                                    //To get the pushbutton state, first, we send a packet with our "Get Pushbutton State" command in it.
                                    if (WriteFile(WriteHandleToUSBDevice, OUTBuffer, 65, ref BytesWritten, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                    {
                                        //Now get the response packet from the firmware.
                                        INBuffer[0] = 0;
                                        {
                                            if (ReadFileManagedBuffer(ReadHandleToUSBDevice, INBuffer, 65, ref BytesRead, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                            {
                                                //INBuffer[0] is the report ID, which we don't care about.
                                                //INBuffer[1] is an echo back of the command (see microcontroller firmware).
                                                //INBuffer[2] contains the I/O port pin value for the pushbutton (see microcontroller firmware).  
                                                if (INBuffer[1] == 0x12)
                                                {
                                                    //ANS
                                                    if (INBuffer[2] != 0x00)
                                                    {
                                                        b_Error_Flag = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        AttachedState = false;
                                    }

                                    l_address += l_size;
                                }


                                // SW機能設定情報を書き込み
                                l_address = FlashControl.FM_ADR_SW_FUNCTION_SETTING;
                                l_comp_size = 0;
                                i_send_data_pos = 0;

                                // SW機能設定情報書き込みデータ取得
                                fData.Get_Flash_Write_SW_Function_Info(my_sw_func_datas, ref al_temp1);

                                while (l_comp_size < FlashControl.FM_SW_FUNCTION_SETTING_AREA_SIZE && b_Error_Flag == false)
                                {
                                    l_size = FlashControl.FM_SW_FUNCTION_SETTING_AREA_SIZE - l_comp_size;
                                    if (l_size > FlashControl.FM_USB_WRITE_DATA_SIZE)
                                    {
                                        l_size = FlashControl.FM_USB_WRITE_DATA_SIZE;
                                    }
                                    // 0x100ごとのセクションをまたぐときは分割
                                    long l_temp = 0x100 - (l_address & 0xFF);
                                    if (l_temp < l_size)
                                    {
                                        l_size = l_temp;
                                    }
                                    l_comp_size += l_size;


                                    OUTBuffer[0] = 0;			//The first byte is the "Report ID" and does not get sent over the USB bus.  Always set = 0.
                                    OUTBuffer[1] = 0x12;		//0x22 is the "Get Pushbutton State" command in the firmware
                                    OUTBuffer[2] = (byte)((l_address >> 24) & 0xFF);		//Address
                                    OUTBuffer[3] = (byte)((l_address >> 16) & 0xFF);		//
                                    OUTBuffer[4] = (byte)((l_address >> 8) & 0xFF);		//
                                    OUTBuffer[5] = (byte)(l_address & 0xFF);		    //

                                    //送信バイトデータを出力バッファにコピー
                                    for (int fi = 0; fi < l_size; fi++, i_send_data_pos++)
                                    {
                                        OUTBuffer[7 + fi] = (byte)al_temp1[i_send_data_pos];
                                    }
                                    OUTBuffer[6] = (byte)(l_size & 0xFF);		        //Size

                                    //To get the pushbutton state, first, we send a packet with our "Get Pushbutton State" command in it.
                                    if (WriteFile(WriteHandleToUSBDevice, OUTBuffer, 65, ref BytesWritten, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                    {
                                        //Now get the response packet from the firmware.
                                        INBuffer[0] = 0;
                                        {
                                            if (ReadFileManagedBuffer(ReadHandleToUSBDevice, INBuffer, 65, ref BytesRead, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                            {
                                                //INBuffer[0] is the report ID, which we don't care about.
                                                //INBuffer[1] is an echo back of the command (see microcontroller firmware).
                                                //INBuffer[2] contains the I/O port pin value for the pushbutton (see microcontroller firmware).  
                                                if (INBuffer[1] == 0x12)
                                                {
                                                    //ANS
                                                    if (INBuffer[2] != 0x00)
                                                    {
                                                        b_Error_Flag = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        AttachedState = false;
                                    }

                                    l_address += l_size;
                                }
                            }

                            // 書き込みOK
                            if (b_Error_Flag == false)
                            {
                                fData.Set_Flash_Write_Status(FlashControl.FM_WRITE_TYPE_BASE_SETTING, FlashControl.FM_WRITE_STATUS_COMP);
                                fData.Set_Flash_Write_Status(FlashControl.FM_WRITE_TYPE_FUNCTION_SETTING, FlashControl.FM_WRITE_STATUS_COMP);
                                fData.Set_Flash_Write_Status(FlashControl.FM_WRITE_TYPE_ENCODER_SCRIPT_SETTING, FlashControl.FM_WRITE_STATUS_COMP);
                                fData.Set_Flash_Write_Status(FlashControl.FM_WRITE_TYPE_SW_FUNCTION_SETTING, FlashControl.FM_WRITE_STATUS_COMP);
                            }
                            else
                            {   // 書き込みNG
                                fData.Set_Flash_Write_Status(FlashControl.FM_WRITE_TYPE_BASE_SETTING, FlashControl.FM_WRITE_STATUS_NA);
                                fData.Set_Flash_Write_Status(FlashControl.FM_WRITE_TYPE_FUNCTION_SETTING, FlashControl.FM_WRITE_STATUS_NA);
                                fData.Set_Flash_Write_Status(FlashControl.FM_WRITE_TYPE_ENCODER_SCRIPT_SETTING, FlashControl.FM_WRITE_STATUS_NA);
                                fData.Set_Flash_Write_Status(FlashControl.FM_WRITE_TYPE_SW_FUNCTION_SETTING, FlashControl.FM_WRITE_STATUS_NA);
                            }

                            // バッファクリア
                            al_temp1.Clear();
                        }

                        // スクリプトデータ書き込み要求あり？
                        byte_temp = fData.Get_Flash_Write_Status(FlashControl.FM_WRITE_TYPE_SCRIPT_DATA);
                        if (byte_temp == FlashControl.FM_WRITE_STATUS_RQ)
                        {
                            fData.Set_Flash_Write_Status(FlashControl.FM_WRITE_TYPE_SCRIPT_DATA, FlashControl.FM_WRITE_STATUS_WRITTING);

                            b_Error_Flag = false;

                            // 書き換え対象セクターのデータを読み込む
                            for (int fi = 0; fi < my_flash_read_write_buffer.al_Read_Address.Count; fi++)
                            {
                                l_address = (int)my_flash_read_write_buffer.al_Read_Address[fi];
                                long l_read_size = (int)my_flash_read_write_buffer.al_Read_Size[fi];
                                l_comp_size = 0;

                                while (l_comp_size < l_read_size && b_Error_Flag == false)
                                {
                                    l_size = l_read_size - l_comp_size;
                                    if (l_size > FlashControl.FM_USB_READ_DATA_SIZE)
                                    {
                                        l_size = FlashControl.FM_USB_READ_DATA_SIZE;
                                    }
                                    l_comp_size += l_size;

                                    //Get the pushbutton state from the microcontroller firmware.
                                    OUTBuffer[0] = 0;			//The first byte is the "Report ID" and does not get sent over the USB bus.  Always set = 0.
                                    OUTBuffer[1] = 0x11;		//0x21 is the "Get Pushbutton State" command in the firmware
                                    OUTBuffer[2] = (byte)((l_address >> 24) & 0xFF);    //Address
                                    OUTBuffer[3] = (byte)((l_address >> 16) & 0xFF);    //
                                    OUTBuffer[4] = (byte)((l_address >> 8) & 0xFF);     //
                                    OUTBuffer[5] = (byte)(l_address & 0xFF);            //
                                    b_size = (byte)(l_size & 0xFF);
                                    OUTBuffer[6] = b_size;               //Size
                                    for (uint i = 7; i < 65; i++)	//This loop is not strictly necessary.  Simply initializes unused bytes to
                                        OUTBuffer[i] = 0xFF;				//0xFF for lower EMI and power consumption when driving the USB cable.

                                    //To get the pushbutton state, first, we send a packet with our "Get Pushbutton State" command in it.
                                    if (WriteFile(WriteHandleToUSBDevice, OUTBuffer, 65, ref BytesWritten, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                    {
                                        //Now get the response packet from the firmware.
                                        INBuffer[0] = 0;
                                        {
                                            if (ReadFileManagedBuffer(ReadHandleToUSBDevice, INBuffer, 65, ref BytesRead, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                            {
                                                //INBuffer[0] is the report ID, which we don't care about.
                                                //INBuffer[1] is an echo back of the command (see microcontroller firmware).
                                                //INBuffer[2] contains the I/O port pin value for the pushbutton (see microcontroller firmware).  
                                                if (INBuffer[1] == 0x11)
                                                {
                                                    if (INBuffer[2] == b_size)
                                                    {
                                                        // OK
                                                        for (int fj = 0; fj < INBuffer[2]; fj++)
                                                        {
                                                            my_flash_read_write_buffer.al_Read_Data[fi].Add(INBuffer[3 + fj]);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // NG
                                                        b_Error_Flag = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        AttachedState = false;
                                    }

                                    l_address += l_size;
                                }
                            }

                            // 書き込みデータセット
                            if (b_Error_Flag == false)
                            {
                                // 書き込みデータ
                                al_temp1.Clear();

                                if (FlashControl.FM_ADR_SCRIPT_DATA <= my_flash_read_write_buffer.Write_Top_Address && my_flash_read_write_buffer.Write_Top_Address < FlashControl.FM_TOTAL_SIZE)
                                {
                                    // Scrupt Data Set
                                    bool over_write = false;    // 今回編集したスクリプトをセットしたか記憶
                                    int set_address = my_flash_read_write_buffer.Write_Top_Address;
                                    for (int fi = 0; fi < my_flash_read_write_buffer.al_Read_Address.Count; fi++)
                                    {
                                        if ((int)my_flash_read_write_buffer.al_Read_Idx[fi] >= my_flash_read_write_buffer.Write_Idx && over_write == false)
                                        {   // 今回編集したデータをセット
                                            over_write = true;

                                            for (int fj = 0; fj < my_flash_read_write_buffer.al_Write_Data.Count; fj++)
                                            {
                                                al_temp1.Add(my_flash_read_write_buffer.al_Write_Data[fj]);
                                            }
                                            my_script_info_datas.Script_Info_Datas[my_flash_read_write_buffer.Write_Idx].Script_Size = my_flash_read_write_buffer.al_Write_Data.Count;
                                            if (my_script_info_datas.Script_Info_Datas[my_flash_read_write_buffer.Write_Idx].Script_Size > 0)
                                            {
                                                my_script_info_datas.Script_Info_Datas[my_flash_read_write_buffer.Write_Idx].Recode_Address = set_address;
                                                set_address += my_script_info_datas.Script_Info_Datas[my_flash_read_write_buffer.Write_Idx].Script_Size;
                                            }
                                            else
                                            {
                                                my_script_info_datas.Script_Info_Datas[my_flash_read_write_buffer.Write_Idx].Recode_Address = 0;
                                            }
                                            my_script_info_datas.Script_Info_Datas[my_flash_read_write_buffer.Write_Idx].Script_Mode = my_flash_read_write_buffer.Script_Mode;
                                            my_script_info_datas.Script_Info_Datas[my_flash_read_write_buffer.Write_Idx].Name = my_flash_read_write_buffer.Script_Name;
                                        }

                                        if ((int)my_flash_read_write_buffer.al_Read_Idx[fi] != my_flash_read_write_buffer.Write_Idx)
                                        {
                                            for (int fj = 0; fj < (int)my_flash_read_write_buffer.al_Read_Size[fi]; fj++)
                                            {
                                                al_temp1.Add(my_flash_read_write_buffer.al_Read_Data[fi][fj]);
                                            }
                                            my_script_info_datas.Script_Info_Datas[(int)my_flash_read_write_buffer.al_Read_Idx[fi]].Recode_Address = set_address;
                                            set_address += (int)my_flash_read_write_buffer.al_Read_Size[fi];
                                        }
                                    }

                                    if (over_write == false)
                                    {   // 今回編集したデータ未セットなためセット
                                        for (int fj = 0; fj < my_flash_read_write_buffer.al_Write_Data.Count; fj++)
                                        {
                                            al_temp1.Add(my_flash_read_write_buffer.al_Write_Data[fj]);
                                        }
                                        my_script_info_datas.Script_Info_Datas[my_flash_read_write_buffer.Write_Idx].Script_Size = my_flash_read_write_buffer.al_Write_Data.Count;
                                        if (my_script_info_datas.Script_Info_Datas[my_flash_read_write_buffer.Write_Idx].Script_Size > 0)
                                        {
                                            my_script_info_datas.Script_Info_Datas[my_flash_read_write_buffer.Write_Idx].Recode_Address = set_address;
                                            set_address += my_script_info_datas.Script_Info_Datas[my_flash_read_write_buffer.Write_Idx].Script_Size;
                                        }
                                        else
                                        {
                                            my_script_info_datas.Script_Info_Datas[my_flash_read_write_buffer.Write_Idx].Recode_Address = 0;
                                        }
                                        my_script_info_datas.Script_Info_Datas[my_flash_read_write_buffer.Write_Idx].Script_Mode = my_flash_read_write_buffer.Script_Mode;
                                        my_script_info_datas.Script_Info_Datas[my_flash_read_write_buffer.Write_Idx].Name = my_flash_read_write_buffer.Script_Name;
                                    }


                                    // 記録スクリプト数と全スクリプトデータサイズを計算
                                    byte rec_count = 0;
                                    ulong total_size = 0;
                                    for (int fi = 0; fi < my_script_info_datas.Script_Info_Datas.Length; fi++)
                                    {
                                        //if(my_script_info_datas.Script_Info_Datas[fi].Name != "" || my_script_info_datas.Script_Info_Datas[fi].Script_Size > 0)
                                        if (my_script_info_datas.Script_Info_Datas[fi].Script_Size > 0)
                                        {
                                            rec_count++;
                                            total_size += (ulong)my_script_info_datas.Script_Info_Datas[fi].Script_Size;
                                        }
                                    }
                                    my_script_info.Record_Num = rec_count;
                                    my_script_info.Total_Size = total_size;
                                }
                                else
                                {
                                    // NG
                                    b_Error_Flag = true;
                                }
                            }


                            // ここまでOKなら　セクター消去して書き込み
                            if (b_Error_Flag == false)
                            {

                                l_address = my_flash_read_write_buffer.Write_Top_Address;
                                l_comp_size = 0;
                                i_send_data_pos = 0;
                                b_Error_Flag = false;

                                // セクター消去
                                //Get the pushbutton state from the microcontroller firmware.
                                OUTBuffer[0] = 0;			//The first byte is the "Report ID" and does not get sent over the USB bus.  Always set = 0.
                                OUTBuffer[1] = 0x13;		//0x22 is the "Get Pushbutton State" command in the firmware
                                OUTBuffer[2] = (byte)((l_address >> 24) & 0xFF);		//Address
                                OUTBuffer[3] = (byte)((l_address >> 16) & 0xFF);		//
                                OUTBuffer[4] = (byte)((l_address >> 8) & 0xFF);		    //
                                OUTBuffer[5] = (byte)(l_address & 0xFF);		        //
                                for (uint i = 6; i < 65; i++)	//This loop is not strictly necessary.  Simply initializes unused bytes to
                                    OUTBuffer[i] = 0xFF;				//0xFF for lower EMI and power consumption when driving the USB cable.

                                //To get the pushbutton state, first, we send a packet with our "Get Pushbutton State" command in it.
                                if (WriteFile(WriteHandleToUSBDevice, OUTBuffer, 65, ref BytesWritten, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                {
                                    //Now get the response packet from the firmware.
                                    INBuffer[0] = 0;
                                    {
                                        if (ReadFileManagedBuffer(ReadHandleToUSBDevice, INBuffer, 65, ref BytesRead, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                        {
                                            //INBuffer[0] is the report ID, which we don't care about.
                                            //INBuffer[1] is an echo back of the command (see microcontroller firmware).
                                            //INBuffer[2] contains the I/O port pin value for the pushbutton (see microcontroller firmware).  
                                            if (INBuffer[1] == 0x13)
                                            {
                                                //ANS
                                                if (INBuffer[2] != 0x00)
                                                {
                                                    b_Error_Flag = true;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    AttachedState = false;
                                }

                                // エラー無し
                                if (b_Error_Flag == false)
                                {
                                    while (l_comp_size < al_temp1.Count && b_Error_Flag == false)
                                    {
                                        l_size = al_temp1.Count - l_comp_size;
                                        if (l_size > FlashControl.FM_USB_WRITE_DATA_SIZE)
                                        {
                                            l_size = FlashControl.FM_USB_WRITE_DATA_SIZE;
                                        }
                                        // 0x100ごとのセクションをまたぐときは分割
                                        long l_temp = 0x100 - (l_address & 0xFF);
                                        if (l_temp < l_size)
                                        {
                                            l_size = l_temp;
                                        }
                                        l_comp_size += l_size;


                                        OUTBuffer[0] = 0;			//The first byte is the "Report ID" and does not get sent over the USB bus.  Always set = 0.
                                        OUTBuffer[1] = 0x12;		//0x22 is the "Get Pushbutton State" command in the firmware
                                        OUTBuffer[2] = (byte)((l_address >> 24) & 0xFF);		//Address
                                        OUTBuffer[3] = (byte)((l_address >> 16) & 0xFF);		//
                                        OUTBuffer[4] = (byte)((l_address >> 8) & 0xFF);		//
                                        OUTBuffer[5] = (byte)(l_address & 0xFF);		    //

                                        //送信バイトデータを出力バッファにコピー
                                        for (int fi = 0; fi < l_size; fi++, i_send_data_pos++)
                                        {
                                            OUTBuffer[7 + fi] = (byte)al_temp1[i_send_data_pos];
                                        }
                                        OUTBuffer[6] = (byte)(l_size & 0xFF);		        //Size

                                        //To get the pushbutton state, first, we send a packet with our "Get Pushbutton State" command in it.
                                        if (WriteFile(WriteHandleToUSBDevice, OUTBuffer, 65, ref BytesWritten, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                        {
                                            //Now get the response packet from the firmware.
                                            INBuffer[0] = 0;
                                            {
                                                if (ReadFileManagedBuffer(ReadHandleToUSBDevice, INBuffer, 65, ref BytesRead, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                                {
                                                    //INBuffer[0] is the report ID, which we don't care about.
                                                    //INBuffer[1] is an echo back of the command (see microcontroller firmware).
                                                    //INBuffer[2] contains the I/O port pin value for the pushbutton (see microcontroller firmware).  
                                                    if (INBuffer[1] == 0x12)
                                                    {
                                                        //ANS
                                                        if (INBuffer[2] != 0x00)
                                                        {
                                                            b_Error_Flag = true;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            AttachedState = false;
                                        }

                                        l_address += l_size;
                                    }
                                }
                            }


                            // 書き込みOK
                            if (b_Error_Flag == false)
                            {

                                my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                                my_App_Setting_Data.Script_Setting_Drag_Target_Control = Constants.SCRIPT_DRAG_CTRL_NON;

                                fData.Set_Flash_Write_Status(FlashControl.FM_WRITE_TYPE_SCRIPT_DATA, FlashControl.FM_WRITE_STATUS_COMP);


                                // スクリプト情報書き込み要求
                                fData.Set_Flash_Write_Status(FlashControl.FM_WRITE_TYPE_SCRIPT_INFO, FlashControl.FM_WRITE_STATUS_RQ);

                                FlashReadFirstTime = true;
                                // スクリプト情報読み込み要求
                                fData.Set_Flash_Read_Status(FlashControl.FM_READ_TYPE_SCRIPT_INFO, FlashControl.FM_READ_STATUS_RQ);
                            }
                            else
                            {   // 書き込みNG
                                fData.Set_Flash_Write_Status(FlashControl.FM_WRITE_TYPE_SCRIPT_DATA, FlashControl.FM_WRITE_STATUS_NA);
                            }



                            // バッファクリア
                            al_temp1.Clear();
                            my_flash_read_write_buffer.clear();


                        }
                        // スクリプト情報書き込み要求あり？
                        byte_temp = fData.Get_Flash_Write_Status(FlashControl.FM_WRITE_TYPE_SCRIPT_INFO);
                        if (byte_temp == FlashControl.FM_WRITE_STATUS_RQ)
                        {
                            fData.Set_Flash_Write_Status(FlashControl.FM_WRITE_TYPE_SCRIPT_INFO, FlashControl.FM_WRITE_STATUS_WRITTING);

                            // 書き込みデータ取得
                            al_temp1.Clear();
                            fData.Get_Flash_Write_Script_Info_Data(my_script_info, my_script_info_datas, ref al_temp1);

                            l_address = FlashControl.FM_ADR_SCRIPT_INFO;
                            l_comp_size = 0;
                            i_send_data_pos = 0;
                            b_Error_Flag = false;

                            // セクター消去
                            //Get the pushbutton state from the microcontroller firmware.
                            OUTBuffer[0] = 0;			//The first byte is the "Report ID" and does not get sent over the USB bus.  Always set = 0.
                            OUTBuffer[1] = 0x13;		//0x22 is the "Get Pushbutton State" command in the firmware
                            OUTBuffer[2] = (byte)((l_address >> 24) & 0xFF);		//Address
                            OUTBuffer[3] = (byte)((l_address >> 16) & 0xFF);		//
                            OUTBuffer[4] = (byte)((l_address >> 8) & 0xFF);		    //
                            OUTBuffer[5] = (byte)(l_address & 0xFF);		        //
                            for (uint i = 6; i < 65; i++)	//This loop is not strictly necessary.  Simply initializes unused bytes to
                                OUTBuffer[i] = 0xFF;				//0xFF for lower EMI and power consumption when driving the USB cable.

                            //To get the pushbutton state, first, we send a packet with our "Get Pushbutton State" command in it.
                            if (WriteFile(WriteHandleToUSBDevice, OUTBuffer, 65, ref BytesWritten, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                            {
                                //Now get the response packet from the firmware.
                                INBuffer[0] = 0;
                                {
                                    if (ReadFileManagedBuffer(ReadHandleToUSBDevice, INBuffer, 65, ref BytesRead, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                    {
                                        //INBuffer[0] is the report ID, which we don't care about.
                                        //INBuffer[1] is an echo back of the command (see microcontroller firmware).
                                        //INBuffer[2] contains the I/O port pin value for the pushbutton (see microcontroller firmware).  
                                        if (INBuffer[1] == 0x13)
                                        {
                                            //ANS
                                            if (INBuffer[2] != 0x00)
                                            {
                                                b_Error_Flag = true;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                AttachedState = false;
                            }

                            // エラー無し
                            if (b_Error_Flag == false)
                            {
                                while (l_comp_size < FlashControl.FM_SCRIPT_INFO_AREA_SIZE && b_Error_Flag == false)
                                {
                                    l_size = FlashControl.FM_SCRIPT_INFO_AREA_SIZE - l_comp_size;
                                    if (l_size > FlashControl.FM_USB_WRITE_DATA_SIZE)
                                    {
                                        l_size = FlashControl.FM_USB_WRITE_DATA_SIZE;
                                    }
                                    // 0x100ごとのセクションをまたぐときは分割
                                    long l_temp = 0x100 - (l_address & 0xFF);
                                    if (l_temp < l_size)
                                    {
                                        l_size = l_temp;
                                    }
                                    l_comp_size += l_size;


                                    OUTBuffer[0] = 0;			//The first byte is the "Report ID" and does not get sent over the USB bus.  Always set = 0.
                                    OUTBuffer[1] = 0x12;		//0x22 is the "Get Pushbutton State" command in the firmware
                                    OUTBuffer[2] = (byte)((l_address >> 24) & 0xFF);		//Address
                                    OUTBuffer[3] = (byte)((l_address >> 16) & 0xFF);		//
                                    OUTBuffer[4] = (byte)((l_address >> 8) & 0xFF);		//
                                    OUTBuffer[5] = (byte)(l_address & 0xFF);		    //

                                    //送信バイトデータを出力バッファにコピー
                                    for (int fi = 0; fi < l_size; fi++, i_send_data_pos++)
                                    {
                                        OUTBuffer[7 + fi] = (byte)al_temp1[i_send_data_pos];
                                    }
                                    OUTBuffer[6] = (byte)(l_size & 0xFF);		        //Size

                                    //To get the pushbutton state, first, we send a packet with our "Get Pushbutton State" command in it.
                                    if (WriteFile(WriteHandleToUSBDevice, OUTBuffer, 65, ref BytesWritten, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                    {
                                        //Now get the response packet from the firmware.
                                        INBuffer[0] = 0;
                                        {
                                            if (ReadFileManagedBuffer(ReadHandleToUSBDevice, INBuffer, 65, ref BytesRead, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                            {
                                                //INBuffer[0] is the report ID, which we don't care about.
                                                //INBuffer[1] is an echo back of the command (see microcontroller firmware).
                                                //INBuffer[2] contains the I/O port pin value for the pushbutton (see microcontroller firmware).  
                                                if (INBuffer[1] == 0x12)
                                                {
                                                    //ANS
                                                    if (INBuffer[2] != 0x00)
                                                    {
                                                        b_Error_Flag = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        AttachedState = false;
                                    }

                                    l_address += l_size;
                                }
                            }

                            // 書き込みOK
                            if (b_Error_Flag == false)
                            {
                                fData.Set_Flash_Write_Status(FlashControl.FM_WRITE_TYPE_SCRIPT_INFO, FlashControl.FM_WRITE_STATUS_COMP);
                            }
                            else
                            {   // 書き込みNG
                                fData.Set_Flash_Write_Status(FlashControl.FM_WRITE_TYPE_SCRIPT_INFO, FlashControl.FM_WRITE_STATUS_NA);
                            }

                            // バッファクリア
                            al_temp1.Clear();
                        }




                        // 基本設定情報読み込み要求あり？
                        byte_temp = fData.Get_Flash_Read_Status(FlashControl.FM_READ_TYPE_BASE_SETTING);
                        if (byte_temp == FlashControl.FM_READ_STATUS_RQ)
                        {
                            fData.Set_Flash_Read_Status(FlashControl.FM_READ_TYPE_BASE_SETTING, FlashControl.FM_READ_STATUS_READING);

                            al_temp1.Clear();
                            l_address = FlashControl.FM_ADR_BASE_INFO;
                            l_comp_size = 0;
                            b_Error_Flag = false;

                            while (l_comp_size < FlashControl.FM_BASE_INFO_AREA_SIZE && b_Error_Flag == false)
                            {
                                l_size = FlashControl.FM_BASE_INFO_AREA_SIZE - l_comp_size;
                                if (l_size > FlashControl.FM_USB_READ_DATA_SIZE)
                                {
                                    l_size = FlashControl.FM_USB_READ_DATA_SIZE;
                                }
                                l_comp_size += l_size;

                                //Get the pushbutton state from the microcontroller firmware.
                                OUTBuffer[0] = 0;			//The first byte is the "Report ID" and does not get sent over the USB bus.  Always set = 0.
                                OUTBuffer[1] = 0x11;		//0x21 is the "Get Pushbutton State" command in the firmware
                                OUTBuffer[2] = (byte)((l_address >> 24) & 0xFF);    //Address
                                OUTBuffer[3] = (byte)((l_address >> 16) & 0xFF);    //
                                OUTBuffer[4] = (byte)((l_address >> 8) & 0xFF);     //
                                OUTBuffer[5] = (byte)(l_address & 0xFF);            //
                                b_size = (byte)(l_size & 0xFF);
                                OUTBuffer[6] = b_size;               //Size
                                for (uint i = 7; i < 65; i++)	//This loop is not strictly necessary.  Simply initializes unused bytes to
                                    OUTBuffer[i] = 0xFF;				//0xFF for lower EMI and power consumption when driving the USB cable.

                                //To get the pushbutton state, first, we send a packet with our "Get Pushbutton State" command in it.
                                if (WriteFile(WriteHandleToUSBDevice, OUTBuffer, 65, ref BytesWritten, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                {
                                    //Now get the response packet from the firmware.
                                    INBuffer[0] = 0;
                                    {
                                        if (ReadFileManagedBuffer(ReadHandleToUSBDevice, INBuffer, 65, ref BytesRead, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                        {
                                            //INBuffer[0] is the report ID, which we don't care about.
                                            //INBuffer[1] is an echo back of the command (see microcontroller firmware).
                                            //INBuffer[2] contains the I/O port pin value for the pushbutton (see microcontroller firmware).  
                                            if (INBuffer[1] == 0x11)
                                            {
                                                if (INBuffer[2] == b_size)
                                                {
                                                    // OK
                                                    for (int fi = 0; fi < INBuffer[2]; fi++)
                                                    {
                                                        al_temp1.Add(INBuffer[3 + fi]);
                                                    }
                                                }
                                                else
                                                {
                                                    // NG
                                                    b_Error_Flag = true;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    AttachedState = false;
                                }

                                l_address += l_size;
                            }

                            // 読み込みOK
                            if (b_Error_Flag == false)
                            {
                                //読み込み情報セット
                                fData.Set_Flash_Read_Base_Info(ref my_base_info, ref my_base_mode_infos, al_temp1);

                                fData.Set_Flash_Read_Status(FlashControl.FM_READ_TYPE_BASE_SETTING, FlashControl.FM_READ_STATUS_COMP);
                            }
                            else
                            {   // 読み込みNG
                                fData.Set_Flash_Read_Status(FlashControl.FM_READ_TYPE_BASE_SETTING, FlashControl.FM_READ_STATUS_NA);
                            }

                            // バッファクリア
                            al_temp1.Clear();
                        }

                        // 機能設定情報読み込み要求あり？
                        byte_temp = fData.Get_Flash_Read_Status(FlashControl.FM_READ_TYPE_FUNCTION_SETTING);
                        if (byte_temp == FlashControl.FM_READ_STATUS_RQ)
                        {
                            fData.Set_Flash_Read_Status(FlashControl.FM_READ_TYPE_FUNCTION_SETTING, FlashControl.FM_READ_STATUS_READING);

                            al_temp1.Clear();
                            l_address = FlashControl.FM_ADR_FUNCTION_SETTING;
                            l_comp_size = 0;
                            b_Error_Flag = false;

                            while (l_comp_size < FlashControl.FM_FUNCTION_SETTING_AREA_SIZE && b_Error_Flag == false)
                            {
                                l_size = FlashControl.FM_FUNCTION_SETTING_AREA_SIZE - l_comp_size;
                                if (l_size > FlashControl.FM_USB_READ_DATA_SIZE)
                                {
                                    l_size = FlashControl.FM_USB_READ_DATA_SIZE;
                                }
                                l_comp_size += l_size;

                                //Get the pushbutton state from the microcontroller firmware.
                                OUTBuffer[0] = 0;			//The first byte is the "Report ID" and does not get sent over the USB bus.  Always set = 0.
                                OUTBuffer[1] = 0x11;		//0x21 is the "Get Pushbutton State" command in the firmware
                                OUTBuffer[2] = (byte)((l_address >> 24) & 0xFF);    //Address
                                OUTBuffer[3] = (byte)((l_address >> 16) & 0xFF);    //
                                OUTBuffer[4] = (byte)((l_address >> 8) & 0xFF);     //
                                OUTBuffer[5] = (byte)(l_address & 0xFF);            //
                                b_size = (byte)(l_size & 0xFF);
                                OUTBuffer[6] = b_size;               //Size
                                for (uint i = 7; i < 65; i++)	//This loop is not strictly necessary.  Simply initializes unused bytes to
                                    OUTBuffer[i] = 0xFF;				//0xFF for lower EMI and power consumption when driving the USB cable.

                                //To get the pushbutton state, first, we send a packet with our "Get Pushbutton State" command in it.
                                if (WriteFile(WriteHandleToUSBDevice, OUTBuffer, 65, ref BytesWritten, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                {
                                    //Now get the response packet from the firmware.
                                    INBuffer[0] = 0;
                                    {
                                        if (ReadFileManagedBuffer(ReadHandleToUSBDevice, INBuffer, 65, ref BytesRead, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                        {
                                            //INBuffer[0] is the report ID, which we don't care about.
                                            //INBuffer[1] is an echo back of the command (see microcontroller firmware).
                                            //INBuffer[2] contains the I/O port pin value for the pushbutton (see microcontroller firmware).  
                                            if (INBuffer[1] == 0x11)
                                            {
                                                if (INBuffer[2] == b_size)
                                                {
                                                    // OK
                                                    for (int fi = 0; fi < INBuffer[2]; fi++)
                                                    {
                                                        al_temp1.Add(INBuffer[3 + fi]);
                                                    }
                                                }
                                                else
                                                {
                                                    // NG
                                                    b_Error_Flag = true;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    AttachedState = false;
                                }

                                l_address += l_size;
                            }


                            // 読み込みOK
                            if (b_Error_Flag == false)
                            {
                                //読み込み情報セット
                                fData.Set_Flash_Read_Func_Info(ref my_func_datas, al_temp1, ref my_app_sw_datas, ref my_app_func_datas);

                                fData.Set_Flash_Read_Status(FlashControl.FM_READ_TYPE_FUNCTION_SETTING, FlashControl.FM_READ_STATUS_COMP);
                            }
                            else
                            {   // 読み込みNG
                                fData.Set_Flash_Read_Status(FlashControl.FM_READ_TYPE_FUNCTION_SETTING, FlashControl.FM_READ_STATUS_NA);
                            }


                            // バッファクリア
                            al_temp1.Clear();
                        }

                        // エンコーダスクリプト設定情報読み込み要求あり？
                        byte_temp = fData.Get_Flash_Read_Status(FlashControl.FM_READ_TYPE_ENCODER_SCRIPT_SETTING);
                        if (byte_temp == FlashControl.FM_READ_STATUS_RQ)
                        {
                            fData.Set_Flash_Read_Status(FlashControl.FM_READ_TYPE_ENCODER_SCRIPT_SETTING, FlashControl.FM_READ_STATUS_READING);

                            al_temp1.Clear();
                            l_address = FlashControl.FM_ADR_ENCODER_SCRIPT_SETTING;
                            l_comp_size = 0;
                            b_Error_Flag = false;

                            while (l_comp_size < FlashControl.FM_ENCODER_SCRIPT_SETTING_AREA_SIZE && b_Error_Flag == false)
                            {
                                l_size = FlashControl.FM_ENCODER_SCRIPT_SETTING_AREA_SIZE - l_comp_size;
                                if (l_size > FlashControl.FM_USB_READ_DATA_SIZE)
                                {
                                    l_size = FlashControl.FM_USB_READ_DATA_SIZE;
                                }
                                l_comp_size += l_size;

                                //Get the pushbutton state from the microcontroller firmware.
                                OUTBuffer[0] = 0;			//The first byte is the "Report ID" and does not get sent over the USB bus.  Always set = 0.
                                OUTBuffer[1] = 0x11;		//0x21 is the "Get Pushbutton State" command in the firmware
                                OUTBuffer[2] = (byte)((l_address >> 24) & 0xFF);    //Address
                                OUTBuffer[3] = (byte)((l_address >> 16) & 0xFF);    //
                                OUTBuffer[4] = (byte)((l_address >> 8) & 0xFF);     //
                                OUTBuffer[5] = (byte)(l_address & 0xFF);            //
                                b_size = (byte)(l_size & 0xFF);
                                OUTBuffer[6] = b_size;               //Size
                                for (uint i = 7; i < 65; i++)	//This loop is not strictly necessary.  Simply initializes unused bytes to
                                    OUTBuffer[i] = 0xFF;				//0xFF for lower EMI and power consumption when driving the USB cable.

                                //To get the pushbutton state, first, we send a packet with our "Get Pushbutton State" command in it.
                                if (WriteFile(WriteHandleToUSBDevice, OUTBuffer, 65, ref BytesWritten, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                {
                                    //Now get the response packet from the firmware.
                                    INBuffer[0] = 0;
                                    {
                                        if (ReadFileManagedBuffer(ReadHandleToUSBDevice, INBuffer, 65, ref BytesRead, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                        {
                                            //INBuffer[0] is the report ID, which we don't care about.
                                            //INBuffer[1] is an echo back of the command (see microcontroller firmware).
                                            //INBuffer[2] contains the I/O port pin value for the pushbutton (see microcontroller firmware).  
                                            if (INBuffer[1] == 0x11)
                                            {
                                                if (INBuffer[2] == b_size)
                                                {
                                                    // OK
                                                    for (int fi = 0; fi < INBuffer[2]; fi++)
                                                    {
                                                        al_temp1.Add(INBuffer[3 + fi]);
                                                    }
                                                }
                                                else
                                                {
                                                    // NG
                                                    b_Error_Flag = true;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    AttachedState = false;
                                }

                                l_address += l_size;
                            }

                            // 読み込みOK
                            if (b_Error_Flag == false)
                            {
                                //読み込み情報セット
                                fData.Set_Flash_Read_Encoder_Script_Info(ref my_encoder_script_datas, al_temp1);

                                fData.Set_Flash_Read_Status(FlashControl.FM_READ_TYPE_ENCODER_SCRIPT_SETTING, FlashControl.FM_READ_STATUS_COMP);
                            }
                            else
                            {   // 読み込みNG
                                fData.Set_Flash_Read_Status(FlashControl.FM_READ_TYPE_ENCODER_SCRIPT_SETTING, FlashControl.FM_READ_STATUS_NA);
                            }

                            // バッファクリア
                            al_temp1.Clear();
                        }

                        // SW機能設定情報読み込み要求あり？
                        byte_temp = fData.Get_Flash_Read_Status(FlashControl.FM_READ_TYPE_SW_FUNCTION_SETTING);
                        if (byte_temp == FlashControl.FM_READ_STATUS_RQ)
                        {
                            fData.Set_Flash_Read_Status(FlashControl.FM_READ_TYPE_SW_FUNCTION_SETTING, FlashControl.FM_READ_STATUS_READING);

                            al_temp1.Clear();
                            l_address = FlashControl.FM_ADR_SW_FUNCTION_SETTING;
                            l_comp_size = 0;
                            b_Error_Flag = false;

                            while (l_comp_size < FlashControl.FM_SW_FUNCTION_SETTING_AREA_SIZE && b_Error_Flag == false)
                            {
                                l_size = FlashControl.FM_SW_FUNCTION_SETTING_AREA_SIZE - l_comp_size;
                                if (l_size > FlashControl.FM_USB_READ_DATA_SIZE)
                                {
                                    l_size = FlashControl.FM_USB_READ_DATA_SIZE;
                                }
                                l_comp_size += l_size;

                                //Get the pushbutton state from the microcontroller firmware.
                                OUTBuffer[0] = 0;			//The first byte is the "Report ID" and does not get sent over the USB bus.  Always set = 0.
                                OUTBuffer[1] = 0x11;		//0x21 is the "Get Pushbutton State" command in the firmware
                                OUTBuffer[2] = (byte)((l_address >> 24) & 0xFF);    //Address
                                OUTBuffer[3] = (byte)((l_address >> 16) & 0xFF);    //
                                OUTBuffer[4] = (byte)((l_address >> 8) & 0xFF);     //
                                OUTBuffer[5] = (byte)(l_address & 0xFF);            //
                                b_size = (byte)(l_size & 0xFF);
                                OUTBuffer[6] = b_size;               //Size
                                for (uint i = 7; i < 65; i++)	//This loop is not strictly necessary.  Simply initializes unused bytes to
                                    OUTBuffer[i] = 0xFF;				//0xFF for lower EMI and power consumption when driving the USB cable.

                                //To get the pushbutton state, first, we send a packet with our "Get Pushbutton State" command in it.
                                if (WriteFile(WriteHandleToUSBDevice, OUTBuffer, 65, ref BytesWritten, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                {
                                    //Now get the response packet from the firmware.
                                    INBuffer[0] = 0;
                                    {
                                        if (ReadFileManagedBuffer(ReadHandleToUSBDevice, INBuffer, 65, ref BytesRead, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                        {
                                            //INBuffer[0] is the report ID, which we don't care about.
                                            //INBuffer[1] is an echo back of the command (see microcontroller firmware).
                                            //INBuffer[2] contains the I/O port pin value for the pushbutton (see microcontroller firmware).  
                                            if (INBuffer[1] == 0x11)
                                            {
                                                if (INBuffer[2] == b_size)
                                                {
                                                    // OK
                                                    for (int fi = 0; fi < INBuffer[2]; fi++)
                                                    {
                                                        al_temp1.Add(INBuffer[3 + fi]);
                                                    }
                                                }
                                                else
                                                {
                                                    // NG
                                                    b_Error_Flag = true;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    AttachedState = false;
                                }

                                l_address += l_size;
                            }


                            // 読み込みOK
                            if (b_Error_Flag == false)
                            {
                                //読み込み情報セット
                                fData.Set_Flash_Read_SW_Func_Info(ref my_sw_func_datas, al_temp1);

                                fData.Set_Flash_Read_Status(FlashControl.FM_READ_TYPE_SW_FUNCTION_SETTING, FlashControl.FM_READ_STATUS_COMP);
                            }
                            else
                            {   // 読み込みNG
                                fData.Set_Flash_Read_Status(FlashControl.FM_READ_TYPE_SW_FUNCTION_SETTING, FlashControl.FM_READ_STATUS_NA);
                            }


                            // バッファクリア
                            al_temp1.Clear();
                        }



                        // スクリプト情報読み込み要求あり？
                        byte_temp = fData.Get_Flash_Read_Status(FlashControl.FM_READ_TYPE_SCRIPT_INFO);
                        if (byte_temp == FlashControl.FM_READ_STATUS_RQ)
                        {
                            fData.Set_Flash_Read_Status(FlashControl.FM_READ_TYPE_SCRIPT_INFO, FlashControl.FM_READ_STATUS_READING);

                            al_temp1.Clear();
                            l_address = FlashControl.FM_ADR_SCRIPT_INFO;
                            l_comp_size = 0;
                            b_Error_Flag = false;

                            while (l_comp_size < FlashControl.FM_SCRIPT_INFO_AREA_SIZE && b_Error_Flag == false)
                            {
                                l_size = FlashControl.FM_SCRIPT_INFO_AREA_SIZE - l_comp_size;
                                if (l_size > FlashControl.FM_USB_READ_DATA_SIZE)
                                {
                                    l_size = FlashControl.FM_USB_READ_DATA_SIZE;
                                }
                                l_comp_size += l_size;

                                //Get the pushbutton state from the microcontroller firmware.
                                OUTBuffer[0] = 0;			//The first byte is the "Report ID" and does not get sent over the USB bus.  Always set = 0.
                                OUTBuffer[1] = 0x11;		//0x21 is the "Get Pushbutton State" command in the firmware
                                OUTBuffer[2] = (byte)((l_address >> 24) & 0xFF);    //Address
                                OUTBuffer[3] = (byte)((l_address >> 16) & 0xFF);    //
                                OUTBuffer[4] = (byte)((l_address >> 8) & 0xFF);     //
                                OUTBuffer[5] = (byte)(l_address & 0xFF);            //
                                b_size = (byte)(l_size & 0xFF);
                                OUTBuffer[6] = b_size;               //Size
                                for (uint i = 7; i < 65; i++)	//This loop is not strictly necessary.  Simply initializes unused bytes to
                                    OUTBuffer[i] = 0xFF;				//0xFF for lower EMI and power consumption when driving the USB cable.

                                //To get the pushbutton state, first, we send a packet with our "Get Pushbutton State" command in it.
                                if (WriteFile(WriteHandleToUSBDevice, OUTBuffer, 65, ref BytesWritten, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                {
                                    //Now get the response packet from the firmware.
                                    INBuffer[0] = 0;
                                    {
                                        if (ReadFileManagedBuffer(ReadHandleToUSBDevice, INBuffer, 65, ref BytesRead, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                        {
                                            //INBuffer[0] is the report ID, which we don't care about.
                                            //INBuffer[1] is an echo back of the command (see microcontroller firmware).
                                            //INBuffer[2] contains the I/O port pin value for the pushbutton (see microcontroller firmware).  
                                            if (INBuffer[1] == 0x11)
                                            {
                                                if (INBuffer[2] == b_size)
                                                {
                                                    // OK
                                                    for (int fi = 0; fi < INBuffer[2]; fi++)
                                                    {
                                                        al_temp1.Add(INBuffer[3 + fi]);
                                                    }
                                                }
                                                else
                                                {
                                                    // NG
                                                    b_Error_Flag = true;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    AttachedState = false;
                                }

                                l_address += l_size;
                            }


                            // 読み込みOK
                            if (b_Error_Flag == false)
                            {
                                //読み込み情報セット
                                fData.Set_Flash_Read_Script_Info_Data(ref my_script_info, ref my_script_info_datas, al_temp1);

                                fData.Set_Flash_Read_Status(FlashControl.FM_READ_TYPE_SCRIPT_INFO, FlashControl.FM_READ_STATUS_COMP);
                            }
                            else
                            {   // 読み込みNG
                                fData.Set_Flash_Read_Status(FlashControl.FM_READ_TYPE_SCRIPT_INFO, FlashControl.FM_READ_STATUS_NA);
                            }


                            // バッファクリア
                            al_temp1.Clear();
                        }

                        // スクリプトデータ読み込み要求あり？
                        byte_temp = fData.Get_Flash_Read_Status(FlashControl.FM_READ_TYPE_SCRIPT_DATA);
                        if (byte_temp == FlashControl.FM_READ_STATUS_RQ)
                        {
                            fData.Set_Flash_Read_Status(FlashControl.FM_READ_TYPE_SCRIPT_DATA, FlashControl.FM_READ_STATUS_READING);

                            b_Error_Flag = false;
                            for (int fi = 0; fi < my_flash_read_write_buffer.al_Read_Address.Count; fi++)
                            {
                                l_address = (int)my_flash_read_write_buffer.al_Read_Address[fi];
                                long l_read_size = (int)my_flash_read_write_buffer.al_Read_Size[fi];
                                l_comp_size = 0;

                                while (l_comp_size < l_read_size && b_Error_Flag == false)
                                {
                                    l_size = l_read_size - l_comp_size;
                                    if (l_size > FlashControl.FM_USB_READ_DATA_SIZE)
                                    {
                                        l_size = FlashControl.FM_USB_READ_DATA_SIZE;
                                    }
                                    l_comp_size += l_size;

                                    //Get the pushbutton state from the microcontroller firmware.
                                    OUTBuffer[0] = 0;			//The first byte is the "Report ID" and does not get sent over the USB bus.  Always set = 0.
                                    OUTBuffer[1] = 0x11;		//0x21 is the "Get Pushbutton State" command in the firmware
                                    OUTBuffer[2] = (byte)((l_address >> 24) & 0xFF);    //Address
                                    OUTBuffer[3] = (byte)((l_address >> 16) & 0xFF);    //
                                    OUTBuffer[4] = (byte)((l_address >> 8) & 0xFF);     //
                                    OUTBuffer[5] = (byte)(l_address & 0xFF);            //
                                    b_size = (byte)(l_size & 0xFF);
                                    OUTBuffer[6] = b_size;               //Size
                                    for (uint i = 7; i < 65; i++)	//This loop is not strictly necessary.  Simply initializes unused bytes to
                                        OUTBuffer[i] = 0xFF;				//0xFF for lower EMI and power consumption when driving the USB cable.

                                    //To get the pushbutton state, first, we send a packet with our "Get Pushbutton State" command in it.
                                    if (WriteFile(WriteHandleToUSBDevice, OUTBuffer, 65, ref BytesWritten, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                    {
                                        //Now get the response packet from the firmware.
                                        INBuffer[0] = 0;
                                        {
                                            if (ReadFileManagedBuffer(ReadHandleToUSBDevice, INBuffer, 65, ref BytesRead, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                            {
                                                //INBuffer[0] is the report ID, which we don't care about.
                                                //INBuffer[1] is an echo back of the command (see microcontroller firmware).
                                                //INBuffer[2] contains the I/O port pin value for the pushbutton (see microcontroller firmware).  
                                                if (INBuffer[1] == 0x11)
                                                {
                                                    if (INBuffer[2] == b_size)
                                                    {
                                                        // OK
                                                        for (int fj = 0; fj < INBuffer[2]; fj++)
                                                        {
                                                            my_flash_read_write_buffer.al_Read_Data[fi].Add(INBuffer[3 + fj]);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // NG
                                                        b_Error_Flag = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        AttachedState = false;
                                    }

                                    l_address += l_size;
                                }
                            }


                            // 読み込みOK
                            if (b_Error_Flag == false)
                            {
                                if (my_App_Setting_Data.Script_Setting_Drag_Start_Control == Constants.SCRIPT_DRAG_CTRL_MEMORY)
                                {   // Mouse Memory から Drag & Drop　での読み込み要求
                                    if (my_App_Setting_Data.Script_Setting_Drag_Target_Control == Constants.SCRIPT_DRAG_CTRL_EDIT)
                                    {   // Script Edit への Drag & Drop による Script Data 追加
                                        my_Script_Edit_Disp_Data_Add(my_flash_read_write_buffer.al_Read_Data[0], my_flash_read_write_buffer.Script_Mode);

                                    }
                                }

                                my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                                my_App_Setting_Data.Script_Setting_Drag_Target_Control = Constants.SCRIPT_DRAG_CTRL_NON;

                                fData.Set_Flash_Read_Status(FlashControl.FM_READ_TYPE_SCRIPT_DATA, FlashControl.FM_READ_STATUS_COMP);

                                FlashReadFirstTime = true;
                            }
                            else
                            {   // 読み込みNG
                                fData.Set_Flash_Read_Status(FlashControl.FM_READ_TYPE_SCRIPT_DATA, FlashControl.FM_READ_STATUS_NA);
                            }
                        }

                        // モード取得
                        //Get the pushbutton state from the microcontroller firmware.
                        OUTBuffer[0] = 0;			//The first byte is the "Report ID" and does not get sent over the USB bus.  Always set = 0.
                        OUTBuffer[1] = 0x31;		//0x81 is the "Get Pushbutton State" command in the firmware
                        for (uint i = 2; i < 65; i++)	//This loop is not strictly necessary.  Simply initializes unused bytes to
                            OUTBuffer[i] = 0xFF;				//0xFF for lower EMI and power consumption when driving the USB cable.

                        //To get the pushbutton state, first, we send a packet with our "Get Pushbutton State" command in it.
                        if (WriteFile(WriteHandleToUSBDevice, OUTBuffer, 65, ref BytesWritten, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                        {
                            //Now get the response packet from the firmware.
                            INBuffer[0] = 0;
                            {
                                if (ReadFileManagedBuffer(ReadHandleToUSBDevice, INBuffer, 65, ref BytesRead, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                {
                                    //INBuffer[0] is the report ID, which we don't care about.
                                    //INBuffer[1] is an echo back of the command (see microcontroller firmware).
                                    //INBuffer[2] contains the I/O port pin value for the pushbutton (see microcontroller firmware).  
                                    if (INBuffer[1] == 0x31)
                                    {
                                        mode_no_now = INBuffer[2];
                                        function_no_now = INBuffer[3];
                                    }
                                }
                            }
                        }

#pragma warning disable 0162
                        //DEBUG DEBUG DEBUG *****************************************************************************************************
                        if (__DEBUG_FLAG__ == true)
                        {

                            //Get the pushbutton state from the microcontroller firmware.
                            OUTBuffer[0] = 0;			//The first byte is the "Report ID" and does not get sent over the USB bus.  Always set = 0.
                            OUTBuffer[1] = 0x40;		//0x81 is the "Get Pushbutton State" command in the firmware
                            for (uint i = 2; i < 65; i++)	//This loop is not strictly necessary.  Simply initializes unused bytes to
                                OUTBuffer[i] = 0xFF;				//0xFF for lower EMI and power consumption when driving the USB cable.

                            //To get the pushbutton state, first, we send a packet with our "Get Pushbutton State" command in it.
                            if (WriteFile(WriteHandleToUSBDevice, OUTBuffer, 65, ref BytesWritten, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                            {
                                //Now get the response packet from the firmware.
                                INBuffer[0] = 0;
                                {
                                    if (ReadFileManagedBuffer(ReadHandleToUSBDevice, INBuffer, 65, ref BytesRead, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                    {
                                        //INBuffer[0] is the report ID, which we don't care about.
                                        //INBuffer[1] is an echo back of the command (see microcontroller firmware).
                                        //INBuffer[2] contains the I/O port pin value for the pushbutton (see microcontroller firmware).  
                                        if (INBuffer[1] == 0x40)
                                        {
                                            for (int fi = 0; fi < Debug_Array.Length; fi++ )
                                            {
                                                Debug_Array[fi] = (int)(INBuffer[2+fi]);
                                            }
                                        }
                                    }
                                }
                            }
                            //Get the pushbutton state from the microcontroller firmware.
                            OUTBuffer[0] = 0;			//The first byte is the "Report ID" and does not get sent over the USB bus.  Always set = 0.
                            OUTBuffer[1] = 0x41;		//0x81 is the "Get Pushbutton State" command in the firmware
                            for (uint i = 2; i < 65; i++)	//This loop is not strictly necessary.  Simply initializes unused bytes to
                                OUTBuffer[i] = 0xFF;				//0xFF for lower EMI and power consumption when driving the USB cable.

                            //To get the pushbutton state, first, we send a packet with our "Get Pushbutton State" command in it.
                            if (WriteFile(WriteHandleToUSBDevice, OUTBuffer, 65, ref BytesWritten, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                            {
                                //Now get the response packet from the firmware.
                                INBuffer[0] = 0;
                                {
                                    if (ReadFileManagedBuffer(ReadHandleToUSBDevice, INBuffer, 65, ref BytesRead, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                    {
                                        //INBuffer[0] is the report ID, which we don't care about.
                                        //INBuffer[1] is an echo back of the command (see microcontroller firmware).
                                        //INBuffer[2] contains the I/O port pin value for the pushbutton (see microcontroller firmware).  
                                        if (INBuffer[1] == 0x41)
                                        {
                                            for (int fi = 0; fi < Debug_Array.Length; fi++)
                                            {
                                                Debug_Arr2[fi] = (int)(INBuffer[2 + fi]);
                                            }
                                        }
                                    }
                                }
                            }
                            //Get the pushbutton state from the microcontroller firmware.
                            OUTBuffer[0] = 0;			//The first byte is the "Report ID" and does not get sent over the USB bus.  Always set = 0.
                            OUTBuffer[1] = 0x42;		//0x81 is the "Get Pushbutton State" command in the firmware
                            for (uint i = 2; i < 65; i++)	//This loop is not strictly necessary.  Simply initializes unused bytes to
                                OUTBuffer[i] = 0xFF;				//0xFF for lower EMI and power consumption when driving the USB cable.

                            //To get the pushbutton state, first, we send a packet with our "Get Pushbutton State" command in it.
                            if (WriteFile(WriteHandleToUSBDevice, OUTBuffer, 65, ref BytesWritten, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                            {
                                //Now get the response packet from the firmware.
                                INBuffer[0] = 0;
                                {
                                    if (ReadFileManagedBuffer(ReadHandleToUSBDevice, INBuffer, 65, ref BytesRead, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                    {
                                        //INBuffer[0] is the report ID, which we don't care about.
                                        //INBuffer[1] is an echo back of the command (see microcontroller firmware).
                                        //INBuffer[2] contains the I/O port pin value for the pushbutton (see microcontroller firmware).  
                                        if (INBuffer[1] == 0x42)
                                        {
                                            for (int fi = 0; fi < Debug_Array.Length; fi++)
                                            {
                                                Debug_Arr3[fi] = (int)(INBuffer[2 + fi]);
                                            }
                                        }
                                    }
                                }
                            }
                            if (dbg_write_flag == true)
                            {
                                dbg_write_flag = false;


                                l_address = dbg_write_address;
                                l_comp_size = 0;
                                i_send_data_pos = 0;
                                b_Error_Flag = false;

                                // セクター消去
                                //Get the pushbutton state from the microcontroller firmware.
                                OUTBuffer[0] = 0;			//The first byte is the "Report ID" and does not get sent over the USB bus.  Always set = 0.
                                OUTBuffer[1] = 0x13;		//0x22 is the "Get Pushbutton State" command in the firmware
                                OUTBuffer[2] = (byte)((l_address >> 24) & 0xFF);		//Address
                                OUTBuffer[3] = (byte)((l_address >> 16) & 0xFF);		//
                                OUTBuffer[4] = (byte)((l_address >> 8) & 0xFF);		    //
                                OUTBuffer[5] = (byte)(l_address & 0xFF);		        //
                                for (uint i = 6; i < 65; i++)	//This loop is not strictly necessary.  Simply initializes unused bytes to
                                    OUTBuffer[i] = 0xFF;				//0xFF for lower EMI and power consumption when driving the USB cable.

                                //To get the pushbutton state, first, we send a packet with our "Get Pushbutton State" command in it.
                                if (WriteFile(WriteHandleToUSBDevice, OUTBuffer, 65, ref BytesWritten, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                {
                                    //Now get the response packet from the firmware.
                                    INBuffer[0] = 0;
                                    {
                                        if (ReadFileManagedBuffer(ReadHandleToUSBDevice, INBuffer, 65, ref BytesRead, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                        {
                                            //INBuffer[0] is the report ID, which we don't care about.
                                            //INBuffer[1] is an echo back of the command (see microcontroller firmware).
                                            //INBuffer[2] contains the I/O port pin value for the pushbutton (see microcontroller firmware).  
                                            if (INBuffer[1] == 0x13)
                                            {
                                                //ANS
                                                if (INBuffer[2] != 0x00)
                                                {
                                                    b_Error_Flag = true;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    AttachedState = false;
                                }
                                
                                // エラー無し
                                if (b_Error_Flag == false)
                                {
                                    // 全データ書き込み
                                    while (l_comp_size < dbg_al_write_data.Count && b_Error_Flag == false)
                                    {
                                        l_size = (long)dbg_al_write_data.Count - l_comp_size;
                                        if (l_size > FlashControl.FM_USB_WRITE_DATA_SIZE)
                                        {
                                            l_size = FlashControl.FM_USB_WRITE_DATA_SIZE;
                                        }
                                        // 0x100ごとのセクションをまたぐときは分割
                                        long l_temp = 0x100 - (l_address & 0xFF);
                                        if (l_temp < l_size)
                                        {
                                            l_size = l_temp;
                                        }
                                        l_comp_size += l_size;


                                        OUTBuffer[0] = 0;			//The first byte is the "Report ID" and does not get sent over the USB bus.  Always set = 0.
                                        OUTBuffer[1] = 0x12;		//0x22 is the "Get Pushbutton State" command in the firmware
                                        OUTBuffer[2] = (byte)((l_address >> 24) & 0xFF);		//Address
                                        OUTBuffer[3] = (byte)((l_address >> 16) & 0xFF);		//
                                        OUTBuffer[4] = (byte)((l_address >> 8) & 0xFF);		//
                                        OUTBuffer[5] = (byte)(l_address & 0xFF);		    //

                                        //送信バイトデータを出力バッファにコピー
                                        for (int fi = 0; fi < l_size; fi++, i_send_data_pos++)
                                        {
                                            OUTBuffer[7 + fi] = (byte)dbg_al_write_data[i_send_data_pos];
                                        }
                                        OUTBuffer[6] = (byte)(l_size & 0xFF);		        //Size

                                        //To get the pushbutton state, first, we send a packet with our "Get Pushbutton State" command in it.
                                        if (WriteFile(WriteHandleToUSBDevice, OUTBuffer, 65, ref BytesWritten, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                        {
                                            //Now get the response packet from the firmware.
                                            INBuffer[0] = 0;
                                            {
                                                if (ReadFileManagedBuffer(ReadHandleToUSBDevice, INBuffer, 65, ref BytesRead, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                                {
                                                    //INBuffer[0] is the report ID, which we don't care about.
                                                    //INBuffer[1] is an echo back of the command (see microcontroller firmware).
                                                    //INBuffer[2] contains the I/O port pin value for the pushbutton (see microcontroller firmware).  
                                                    if (INBuffer[1] == 0x12)
                                                    {
                                                        //ANS
                                                        if (INBuffer[2] != 0x00)
                                                        {
                                                            b_Error_Flag = true;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            AttachedState = false;
                                        }

                                        l_address += l_size;
                                    }
                                }

                                // バッファクリア
                                dbg_al_write_data.Clear();
                            }
                            // Flash Read
                            if (debug_flash_read_req == true)
                            {
                                debug_flash_read_req = false;

                                for (uint i = 0; i < 65; i++)	//This loop is not strictly necessary.  Simply initializes unused bytes to
                                {
                                    OUTBuffer[i] = 0xFF;				//0xFF for lower EMI and power consumption when driving the USB cable.
                                }

                                uint tmp_flash_read_address = debug_flash_read_start_addr;
                                uint tmp_flash_read_size = 0;
                                uint tmp_buff_set_pos = 0;
                                while (true)
                                {
                                    OUTBuffer[0] = 0;
                                    OUTBuffer[1] = 0x11;
                                    OUTBuffer[2] = (byte)((tmp_flash_read_address >> 24) & 0xFF);		//Address
                                    OUTBuffer[3] = (byte)((tmp_flash_read_address >> 16) & 0xFF);		//
                                    OUTBuffer[4] = (byte)((tmp_flash_read_address >> 8) & 0xFF);		//
                                    OUTBuffer[5] = (byte)(tmp_flash_read_address & 0xFF);		        //

                                    tmp_flash_read_size = (debug_flash_read_start_addr + debug_flash_read_size) - tmp_flash_read_address;
                                    if (tmp_flash_read_size > 60)
                                    {
                                        tmp_flash_read_size = 60;
                                    }
                                    else if (tmp_flash_read_size == 0)
                                    {   // すべて読み込み完了
                                        debug_flash_read_comp = true;
                                        break;
                                    }
                                    OUTBuffer[6] = (byte)(tmp_flash_read_size & 0xFF);		            //Size
                                    tmp_flash_read_address += tmp_flash_read_size;

                                    //To get the pushbutton state, first, we send a packet with our "Get Pushbutton State" command in it.
                                    b_usb_write_ret = WriteFile(WriteHandleToUSBDevice, OUTBuffer, 65, ref BytesWritten, IntPtr.Zero);
                                    if (b_usb_write_ret == true)	//Blocking function, unless an "overlapped" structure is used
                                    {
                                        //Now get the response packet from the firmware.
                                        INBuffer[0] = 0;
                                        {
                                            if (ReadFileManagedBuffer(ReadHandleToUSBDevice, INBuffer, 65, ref BytesRead, IntPtr.Zero))	//Blocking function, unless an "overlapped" structure is used
                                            {
                                                //INBuffer[0] is the report ID, which we don't care about.
                                                //INBuffer[1] is an echo back of the command (see microcontroller firmware).
                                                //INBuffer[2] contains the I/O port pin value for the pushbutton (see microcontroller firmware).  
                                                if (INBuffer[1] == 0x11)
                                                {
                                                    if (INBuffer[2] == tmp_flash_read_size)
                                                    {
                                                        // OK
                                                        for (int fi = 0; fi < INBuffer[2]; fi++)
                                                        {
                                                            debug_flash_read_buff[tmp_buff_set_pos++] = INBuffer[3 + fi];
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // NG
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        //DEBUG DEBUG DEBUG *****************************************************************************************************
#pragma warning restore 0162

                    } //end of: if(AttachedState == true)
                    else
                    {
                        Thread.Sleep(5);	//Add a small delay.  Otherwise, this while(true) loop can execute very fast and cause 
                                            //high CPU utilization, with no particular benefit to the application.
                    }
                }
                catch
                {
                    //Exceptions can occur during the read or write operations.  For example,
                    //exceptions may occur if for instance the USB device is physically unplugged
                    //from the host while the above read/write functions are executing.

                    //Don't need to do anything special in this case.  The application will automatically
                    //re-establish communications based on the global AttachedState boolean variable used
                    //in conjunction with the WM_DEVICECHANGE messages to dyanmically respond to Plug and Play
                    //USB connection events.
                }

		    } //end of while(true) loop
            //-------------------------------------------------------END CUT AND PASTE BLOCK-------------------------------------------------------------------------------------
            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------
        }



        private void FormUpdateTimer_Tick(object sender, EventArgs e)
        {
            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------
            //-------------------------------------------------------BEGIN CUT AND PASTE BLOCK-----------------------------------------------------------------------------------
            //This timer tick event handler function is used to update the user interface on the form, based on data
            //obtained asynchronously by the ReadWriteThread and the WM_DEVICECHANGE event handler functions.

            //Check if user interface on the form should be enabled or not, based on the attachment state of the USB device.
            if (AttachedState == true)
            {

                if (ConnectFirstTime == true)
                {
                    StatusBox_txtbx.Text = RevOmate.Properties.Resources.STATUS_MSG_USB_CONNECT;

                    VersionReadReq = true;
                    VersionReadComp = false;

                    ConnectFirstTime = false;
                }

                if (VersionReadReq == false && VersionReadComp == true)
                {
                    VersionReadComp = false;
                    // Version
                    System.Diagnostics.FileVersionInfo ver = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
                    lbl_FW_Version.Text = RevOmate.Properties.Resources.FIRMWARE_VERSION + " " + Encoding.Default.GetString(version_buff);

                    // 基本設定情報読み込み要求
                    fData.Set_Flash_Read_Status(FlashControl.FM_READ_TYPE_BASE_SETTING, FlashControl.FM_READ_STATUS_RQ);
                    // 機能設定情報読み込み要求
                    fData.Set_Flash_Read_Status(FlashControl.FM_READ_TYPE_FUNCTION_SETTING, FlashControl.FM_READ_STATUS_RQ);
                    // エンコーダスクリプト設定情報読み込み要求
                    fData.Set_Flash_Read_Status(FlashControl.FM_READ_TYPE_ENCODER_SCRIPT_SETTING, FlashControl.FM_READ_STATUS_RQ);
                    // SW機能設定情報読み込み要求
                    fData.Set_Flash_Read_Status(FlashControl.FM_READ_TYPE_SW_FUNCTION_SETTING, FlashControl.FM_READ_STATUS_RQ);
                    // スクリプト情報読み込み要求
                    fData.Set_Flash_Read_Status(FlashControl.FM_READ_TYPE_SCRIPT_INFO, FlashControl.FM_READ_STATUS_RQ);
                }


                byte read_status = fData.Get_Flash_Read_Status(FlashControl.FM_READ_TYPE_BASE_SETTING);
                read_status &= fData.Get_Flash_Read_Status(FlashControl.FM_READ_TYPE_FUNCTION_SETTING);
                read_status &= fData.Get_Flash_Read_Status(FlashControl.FM_READ_TYPE_ENCODER_SCRIPT_SETTING);
                read_status &= fData.Get_Flash_Read_Status(FlashControl.FM_READ_TYPE_SW_FUNCTION_SETTING);
                read_status &= fData.Get_Flash_Read_Status(FlashControl.FM_READ_TYPE_SCRIPT_INFO);

                byte script_data_read_status = fData.Get_Flash_Read_Status(FlashControl.FM_READ_TYPE_SCRIPT_DATA);
                if ((read_status & FlashControl.FM_READ_STATUS_COMP) == FlashControl.FM_READ_STATUS_COMP && my_App_Setting_Data.Backup_Restore_Flag == Constants.BACKUP_FLAG_NON)
                {
                    // 基本設定情報　＆　機能設定情報　＆　SW機能設定情報　＆ スクリプト情報読み込み完了

                    my_progress_bar_display(false, 0, 0); 

                    if (FlashReadFirstTime == true)
                    {
                        FlashReadFirstTime = false;

                        // コントロール有効
                        my_Display_Enabled(true);

                        // デバイスデータをコピー
                        my_device_data_get();

                        //表示更新
                        my_Set_Display(my_script_info, my_script_info_datas);
                        my_profile_select_disp(button_setting_mode_select_no, true);
                    }


                    // RESTOREでエラー発生
                    if (my_App_Setting_Data.Backup_Restore_Error_Code > 0)
                    {
                        string tmp_msg = string.Format(RevOmate.Properties.Resources.RESTORE_ERROR_MSG1, my_App_Setting_Data.Backup_Restore_Error_Code);
                        my_App_Setting_Data.Backup_Restore_Error_Code = 0;
                        MessageBox.Show(tmp_msg, RevOmate.Properties.Resources.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (my_App_Setting_Data.Backup_Restore_Flag != Constants.BACKUP_FLAG_NON)
                {
                    // コントロール無効
                    my_Display_Enabled(false);

                    FlashReadFirstTime = true;

                    // 進捗表示
                    if (0 <= my_App_Setting_Data.Backup_Restore_Progress_Value && my_App_Setting_Data.Backup_Restore_Progress_Value <= my_App_Setting_Data.Backup_Restore_Progress_Max_Value)
                    {
                        my_progress_bar_display(true, my_App_Setting_Data.Backup_Restore_Progress_Value, my_App_Setting_Data.Backup_Restore_Progress_Max_Value); 
                    }
                    else
                    {
                        my_progress_bar_display(true, my_App_Setting_Data.Backup_Restore_Progress_Max_Value, my_App_Setting_Data.Backup_Restore_Progress_Max_Value); 
                    }
                }
                else
                {
                    // コントロール無効
                    my_Display_Enabled(false);
                }

                UnConnectFirstTime = true;

                // モード、機能番号表示
                lbl_mode_status.Text = string.Format(RevOmate.Properties.Resources.MODE_NO_STATUS, mode_no_now + 1, function_no_now + 1);
            }
            if ((AttachedState == false) || (AttachedButBroken == true))
            {
                //Device not available to communicate. Disable user interface on the form.
                if (UnConnectFirstTime == true)
                {
                    StatusBox_txtbx.Text = RevOmate.Properties.Resources.STATUS_MSG_USB_UNCONNECT;

                    // コントロール無効
                    my_Display_Enabled(false);

                    UnConnectFirstTime = false;
                }


                // Flash読み込み状態クリア
                fData.Set_Flash_Read_Status_Clear(0xFFFF);
                FlashReadFirstTime = true;

                ConnectFirstTime = true;

                // モード、機能番号表示クリア
                lbl_mode_status.Text = "";
            }

            

            //Update the various status indicators on the form with the latest info obtained from the ReadWriteThread()
            if (AttachedState == true)
            {
                //DEBUG

                //DEBUG
                string debug_str = ""; 
                for (int fi = 0; fi < 16; fi++)
                {
                    debug_str += string.Format("{0:000} : ", fi);
                }
                colum_lbl.Text = debug_str;
                debug_str = "";
                for (int fi = 0; fi < Debug_Array.Length; fi++)
                {
                    debug_str += string.Format("{0:000} : ", Debug_Array[fi]);
                }
                Debug_label1.Text = debug_str;
                debug_str = "";
                for (int fi = 0; fi < Debug_Array.Length; fi++)
                {
                    debug_str += string.Format(" {0:X2} : ", Debug_Array[fi]);
                }
                Debug_label2.Text = debug_str;
                debug_str = "";
                for (int fi = 0; fi < Debug_Arr2.Length; fi++)
                {
                    debug_str += string.Format(" {0:X2} : ", Debug_Arr2[fi]);
                }
                Debug_label3.Text = debug_str;
                debug_str = "";
                for (int fi = 0; fi < Debug_Arr3.Length; fi++)
                {
                    debug_str += string.Format(" {0:X2} : ", Debug_Arr3[fi]);
                }
                Debug_label4.Text = debug_str;

                //Flash Read
                if (FlashReadData[0] >= 1)
                {
                    if (FlashReadData[0] == 0xFF)
                    {
                        txtbx_ReadData.Text = "[FF]";
                    }
                    else
                    {
                        string st_tmp = "[";
                        for (int fi = 0; fi < FlashReadData[0]; fi++)
                        {
                            st_tmp += string.Format("{0:X2}:", FlashReadData[1 + fi]);
                        }
                        st_tmp = st_tmp.Remove(st_tmp.Length - 1, 1);
                        st_tmp += "]";
                        txtbx_ReadData.Text = st_tmp;
                    }
                    FlashReadData[0] = 0;
                }
                //Flash Write
                if (byte_FlashWrite_Ans != -1)
                {
                    txtbx_WriteAns.Text = string.Format("{0:X2}", byte_FlashWrite_Ans);

                    byte_FlashWrite_Ans = -1;
                }
                //Flash Erase
                if (byte_FlashErase_Ans != -1)
                {
                    txtbx_EraseAns.Text = string.Format("{0:X2}", byte_FlashErase_Ans);

                    byte_FlashErase_Ans = -1;
                }
                //DEBUG
                if (debug_flash_read_comp == true)
                {
                    debug_flash_read_comp = false;

                    string out_str = "0xXXXXXX:";
                    rtxtbx_debug_flash_read.Text = "";

                    // Header
                    for (int fi = 0; fi < 16; fi++)
                    {
                        out_str += string.Format(" {0:X2}", fi);
                    }

                    uint tmp_addr = debug_flash_read_start_addr & 0x1FFFF0;
                    uint tmp_buff_read_pos = 0;
                    // data
                    while (true)
                    {
                        if ((tmp_addr & 0xF) == 0)
                        {
                            out_str += string.Format("\n0x{0:X6}:", tmp_addr);
                        }
                        if (tmp_addr < debug_flash_read_start_addr)
                        {
                            out_str += string.Format("   ");
                        }
                        else if (tmp_addr < (debug_flash_read_start_addr + debug_flash_read_size))
                        {
                            if (tmp_buff_read_pos < debug_flash_read_buff.Length)
                            {
                                out_str += string.Format(" {0:X2}", debug_flash_read_buff[tmp_buff_read_pos++]);
                            }
                        }
                        else
                        {
                            break;
                        }
                        tmp_addr++;
                    }


                    rtxtbx_debug_flash_read.Text = out_str;
                }
                // DEBUG


            }
            //-------------------------------------------------------END CUT AND PASTE BLOCK-------------------------------------------------------------------------------------
            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------
        }


        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //-------------------------------------------------------BEGIN CUT AND PASTE BLOCK-----------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------------------------------------
        //FUNCTION:	ReadFileManagedBuffer()
        //PURPOSE:	Wrapper function to call ReadFile()
        //
        //INPUT:	Uses managed versions of the same input parameters as ReadFile() uses.
        //
        //OUTPUT:	Returns boolean indicating if the function call was successful or not.
        //          Also returns data in the byte[] INBuffer, and the number of bytes read. 
        //
        //Notes:    Wrapper function used to call the ReadFile() function.  ReadFile() takes a pointer to an unmanaged buffer and deposits
        //          the bytes read into the buffer.  However, can't pass a pointer to a managed buffer directly to ReadFile().
        //          This ReadFileManagedBuffer() is a wrapper function to make it so application code can call ReadFile() easier
        //          by specifying a managed buffer.
        //--------------------------------------------------------------------------------------------------------------------------
        public unsafe bool ReadFileManagedBuffer(SafeFileHandle hFile, byte[] INBuffer, uint nNumberOfBytesToRead, ref uint lpNumberOfBytesRead, IntPtr lpOverlapped)
        {
            IntPtr pINBuffer = IntPtr.Zero;

            try
            {
                pINBuffer = Marshal.AllocHGlobal((int)nNumberOfBytesToRead);    //Allocate some unmanged RAM for the receive data buffer.

                if (ReadFile(hFile, pINBuffer, nNumberOfBytesToRead, ref lpNumberOfBytesRead, lpOverlapped))
                {
                    Marshal.Copy(pINBuffer, INBuffer, 0, (int)lpNumberOfBytesRead);    //Copy over the data from unmanged memory into the managed byte[] INBuffer
                    Marshal.FreeHGlobal(pINBuffer);
                    return true;
                }
                else
                {
                    Marshal.FreeHGlobal(pINBuffer);
                    return false;
                }

            }
            catch
            {
                if (pINBuffer != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(pINBuffer);
                }
                return false;
            }
        }

        private void btn_Read_Click(object sender, EventArgs e)
        {
            try
            {
                l_ReadAddress = (long)Convert.ToInt32(txtbx_ReadAddress.Text, 16);
                byte_ReadSize = Convert.ToByte(txtbx_ReadSize.Text, 16);
                FlashReadData[0] = 0;
                b_FlashRead = true;
            }
            catch
            {
                MessageBox.Show("入力データを確認してください。", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Write_Click(object sender, EventArgs e)
        {
            try
            {
                l_WriteAddress = (long)Convert.ToInt32(txtbx_WriteAddress.Text, 16);
                FlashWriteData[0] = Convert.ToByte(txtbx_WriteData.Text, 16);
                byte_WriteSize = 1;
                //                byte_WriteData = Convert.ToByte(txtbx_WriteData.Text, 16);
                byte_FlashWrite_Ans = -1;
                b_FlashWrite = true;

                txtbx_WriteAns.Text = "";
            }
            catch
            {
                MessageBox.Show("入力データを確認してください。", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Erase_Click(object sender, EventArgs e)
        {
            try
            {
                l_EraseAddress = (long)Convert.ToInt32(txtbx_EraseAddress.Text, 16);
                byte_FlashErase_Ans = -1;
                b_FlashErase = true;

                txtbx_EraseAns.Text = "";
            }
            catch
            {
                MessageBox.Show("入力データを確認してください。", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 表示コントロールの有効/無効設定
        /// </summary>
        /// <param name="flag"></param>
        private void my_Display_Enabled(bool flag)
        {
            pnl_main.Enabled = flag;

            // SCRIPT SETTING
            dgv_FlashMemory.Enabled = flag;

            // マクロ設定
            dgv_ScriptList.Enabled = flag;
            gbx_FileImportExport.Enabled = flag;
            lbl_MacroWrite_Icon.Enabled = flag;
            lbl_MacroRead_Icon.Enabled = flag;
            lbl_MacroWrite_txt.Enabled = flag;
            lbl_MacroRead_txt.Enabled = flag;
            txtbx_script_no.Enabled = flag;
            txtbx_script_name.Enabled = flag;
            dgv_ScriptEditor.Enabled = flag;
            lbl_Dustbox_Icon.Enabled = flag;
            for (int fi = 0; fi < lbl_Script_Icons.Length; fi++)
            {
                lbl_Script_Icons[fi].Enabled = flag;
            }
            lbl_REC_Btn.Enabled = flag;
            lbl_Clear_btn.Enabled = flag;
            gbx_MacroFileImportExport.Enabled = flag;
            gbx_MacroREC.Enabled = flag;
            // 機能設定
            lbl_func_mode1_select.Enabled = flag;
            lbl_func_mode2_select.Enabled = flag;
            lbl_func_mode3_select.Enabled = flag;
            gbx_func_setup.Enabled = flag;
            gbx_func1_setup.Enabled = flag;
            gbx_func2_setup.Enabled = flag;
            gbx_func3_setup.Enabled = flag;
            gbx_func4_setup.Enabled = flag;
            lbl_function_set_icon.Enabled = flag;
            // ボタン設定
            lbl_button_mode1_select.Enabled = flag;
            lbl_button_mode2_select.Enabled = flag;
            lbl_button_mode3_select.Enabled = flag;
            gbx_encoder_setup.Enabled = flag;
            gbx_button_setup.Enabled = flag;
            gbx_LED_set.Enabled = flag;
            lbl_button_setting_set_icon.Enabled = flag;
            lbl_button_factory_reset_icon.Enabled = flag;
            // システム設定
            gbx_base_setting.Enabled = flag;
            gbx_system_backup.Enabled = flag;
            btn_system_setup_close.Enabled = flag;

            if (flag == true)
            {
            }
            else
            {
            }
        }
        /// <summary>
        /// 表示コントロールの有効/無効設定
        /// </summary>
        /// <param name="flag"></param>
        private void my_Display_Enabled_All(bool flag)
        {

            // SCRIPT SETTING
            dgv_FlashMemory.Enabled = flag;

            // マクロ設定
            dgv_ScriptList.Enabled = flag;
            gbx_FileImportExport.Enabled = flag;
            lbl_MacroWrite_Icon.Enabled = flag;
            lbl_MacroRead_Icon.Enabled = flag;
            lbl_MacroWrite_txt.Enabled = flag;
            lbl_MacroRead_txt.Enabled = flag;
            txtbx_script_no.Enabled = flag;
            txtbx_script_name.Enabled = flag;
            dgv_ScriptEditor.Enabled = flag;
            lbl_Dustbox_Icon.Enabled = flag;
            for (int fi = 0; fi < lbl_Script_Icons.Length; fi++)
            {
                lbl_Script_Icons[fi].Enabled = flag;
            }
            lbl_REC_Btn.Enabled = flag;
            lbl_Clear_btn.Enabled = flag;
            gbx_MacroFileImportExport.Enabled = flag;
            gbx_MacroREC.Enabled = flag;
            // 機能設定
            lbl_func_mode1_select.Enabled = flag;
            lbl_func_mode2_select.Enabled = flag;
            lbl_func_mode3_select.Enabled = flag;
            gbx_func_setup.Enabled = flag;
            gbx_func1_setup.Enabled = flag;
            gbx_func2_setup.Enabled = flag;
            gbx_func3_setup.Enabled = flag;
            gbx_func4_setup.Enabled = flag;
            lbl_function_set_icon.Enabled = flag;
            // ボタン設定
            lbl_button_mode1_select.Enabled = flag;
            lbl_button_mode2_select.Enabled = flag;
            lbl_button_mode3_select.Enabled = flag;
            gbx_encoder_setup.Enabled = flag;
            gbx_button_setup.Enabled = flag;
            gbx_LED_set.Enabled = flag;
            lbl_button_setting_set_icon.Enabled = flag;
            lbl_button_factory_reset_icon.Enabled = flag;

            if (flag == true)
            {
            }
            else
            {
            }
        }

        private void lbl_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                if (((System.Windows.Forms.Label)sender).ImageList != null)
                {
                    ((System.Windows.Forms.Label)sender).ImageIndex++;
                }
            }
            catch
            {
            }
        }

        private void lbl_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (((System.Windows.Forms.Label)sender).ImageList != null)
                {
                    ((System.Windows.Forms.Label)sender).ImageIndex--;
                }
            }
            catch
            {
            }
        }

        private void lbl_Dustbox_Icon_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                toolTip1.SetToolTip(((System.Windows.Forms.Label)sender), ((System.Windows.Forms.Label)sender).Text);

                if (((System.Windows.Forms.Label)sender).ImageList != null)
                {
                    ((System.Windows.Forms.Label)sender).ImageIndex = 1;
                }
            }
            catch
            {
            }
        }

        private void lbl_Dustbox_Icon_MouseLeave(object sender, EventArgs e)
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





        private void btn_debug1_Click(object sender, EventArgs e)
        {
            try
            {
                // モード設定
                dbg_write_address = 0x000000;

                dbg_al_write_data.Clear();
                dbg_al_write_data.Add((byte)0x01);  // モード

                dbg_write_flag = true;
            }
            catch
            {
            }
        }

        private void btn_debug2_Click(object sender, EventArgs e)
        {
            try
            {
                // ボタン設定
                dbg_write_address = 0x010000;

                dbg_al_write_data.Clear();
                dbg_al_write_data.Add((byte)0x02);  // 設定
                dbg_al_write_data.Add((byte)0x01);  // スクリプトNo.

                dbg_write_flag = true;
            }
            catch
            {
            }
        }

        private void btn_debug3_Click(object sender, EventArgs e)
        {
            try
            {
                // スクリプト情報設定
                dbg_write_address = 0x000600;

                dbg_al_write_data.Clear();
                dbg_al_write_data.Add((byte)0x00);
                dbg_al_write_data.Add((byte)0x00);
                dbg_al_write_data.Add((byte)0x01);  // スクリプト数
                dbg_al_write_data.Add((byte)0x00);
                dbg_al_write_data.Add((byte)0x01);  // 全スクリプトサイズLSB
                dbg_al_write_data.Add((byte)0x00);
                dbg_al_write_data.Add((byte)0x00);
                dbg_al_write_data.Add((byte)0x00);  // 全スクリプトサイズMSB
                dbg_al_write_data.Add((byte)0x00);
                dbg_al_write_data.Add((byte)0x00);
                dbg_al_write_data.Add((byte)0x00);
                dbg_al_write_data.Add((byte)0x00);
                dbg_al_write_data.Add((byte)0x00);
                dbg_al_write_data.Add((byte)0x00);
                dbg_al_write_data.Add((byte)0x00);
                dbg_al_write_data.Add((byte)0x00);
                dbg_al_write_data.Add((byte)0x00);  // Script1 スクリプト保存アドレスLSB
                dbg_al_write_data.Add((byte)0x00);
                dbg_al_write_data.Add((byte)0x03);
                dbg_al_write_data.Add((byte)0x00);  // Script1 スクリプト保存アドレスMSB
                dbg_al_write_data.Add((byte)0x1B);  // Script1 スクリプトサイズLSB
                dbg_al_write_data.Add((byte)0x00);
                dbg_al_write_data.Add((byte)0x00);
                dbg_al_write_data.Add((byte)0x00);  // Script1 スクリプトサイズMSB
                dbg_al_write_data.Add((byte)0x00);  // Script1 スクリプトモード
                dbg_al_write_data.Add((byte)0x04);  // Script1 スクリプト名称サイズ
                dbg_al_write_data.Add((byte)0x61);  // Script1 スクリプト名称
                dbg_al_write_data.Add((byte)0x00);
                dbg_al_write_data.Add((byte)0x62);
                dbg_al_write_data.Add((byte)0x00);

                dbg_write_flag = true;
            }
            catch
            {
            }
        }

        private void btn_debug4_Click(object sender, EventArgs e)
        {
            try
            {
                // スクリプトデータ設定
                dbg_write_address = 0x010000;

                dbg_al_write_data.Clear();
                dbg_al_write_data.Add((byte)0x41);	// A PUSH
                dbg_al_write_data.Add((byte)0x04);
                dbg_al_write_data.Add((byte)0x70);	// 1000ms
                dbg_al_write_data.Add((byte)0x03);
                dbg_al_write_data.Add((byte)0xE8);
                dbg_al_write_data.Add((byte)0x40);	// A RELEASE
                dbg_al_write_data.Add((byte)0x04);
                dbg_al_write_data.Add((byte)0x70);	// 1500ms
                dbg_al_write_data.Add((byte)0x05);
                dbg_al_write_data.Add((byte)0xDC);
                dbg_al_write_data.Add((byte)0x41);	// B PUSH
                dbg_al_write_data.Add((byte)0x05);
                dbg_al_write_data.Add((byte)0x70);	// 1000ms
                dbg_al_write_data.Add((byte)0x03);
                dbg_al_write_data.Add((byte)0xE8);
                dbg_al_write_data.Add((byte)0x40);	// B RELEASE
                dbg_al_write_data.Add((byte)0x05);
                dbg_al_write_data.Add((byte)0x70);	// 2000ms
                dbg_al_write_data.Add((byte)0x07);
                dbg_al_write_data.Add((byte)0xD0);
                dbg_al_write_data.Add((byte)0x41);	// C PUSH
                dbg_al_write_data.Add((byte)0x06);
                dbg_al_write_data.Add((byte)0x70);	// 1000ms
                dbg_al_write_data.Add((byte)0x03);
                dbg_al_write_data.Add((byte)0xE8);
                dbg_al_write_data.Add((byte)0x40);	// C RELEASE
                dbg_al_write_data.Add((byte)0x06);

                dbg_write_flag = true;
            }
            catch
            {
            }
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptFile_Save_saveFileDialog.DefaultExt = Constants.SCRIPT_FILE_EXTENSION;
                ScriptFile_Save_saveFileDialog.Filter = Constants.SCRIPT_FILE_FILTER;
                if (ScriptFile_Save_saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Script Editorのデータ取得
                    ArrayList al_buff = new ArrayList();
                    my_Convert_SE_Disp_Data_2_Array_List(ref al_buff);

                    int buff_max_size = Constants.SCRIPT_FILE_SIGNATURE_SIZE_LEN + Constants.SCRIPT_FILE_SIGNATURE_SIZE_MAX + Constants.SCRIPT_FILE_FILE_SIZE_LEN + Constants.SCRIPT_FILE_MODE_SIZE_LEN + Constants.SCRIPT_FILE_SCRIPT_SIZE_LEN + FlashControl.FM_SCRIPT_DATA_MAX_SIZE;
                    byte[] save_buff = new byte[buff_max_size];
                    int save_size = my_Convert_SE_Disp_Data_2_SL_byte_Buff(ref save_buff, al_buff, my_script_editor_disp_data.Script_Mode);

                    // スクリプトデータを保存する
                    my_Save_Script_File(ScriptFile_Save_saveFileDialog.FileName, save_buff, save_size);

                    // 表示更新
                    //FlashReadFirstTime = true;
                }
            }
            catch
            {
            }
        }

        private void btn_Import_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptFile_Import_openFileDialog.DefaultExt = Constants.SCRIPT_FILE_EXTENSION;
                ScriptFile_Import_openFileDialog.Filter = Constants.SCRIPT_FILE_FILTER;
                if (ScriptFile_Import_openFileDialog.ShowDialog() == DialogResult.OK)
                {

                    FileStream fs = null;
                    try
                    {
                        int buff_max_size = Constants.SCRIPT_FILE_SIGNATURE_SIZE_LEN + Constants.SCRIPT_FILE_SIGNATURE_SIZE_MAX + Constants.SCRIPT_FILE_FILE_SIZE_LEN + Constants.SCRIPT_FILE_MODE_SIZE_LEN + Constants.SCRIPT_FILE_SCRIPT_SIZE_LEN + FlashControl.FM_SCRIPT_DATA_MAX_SIZE;
                        byte[] read_buff = new byte[buff_max_size];
                        fs = new FileStream(ScriptFile_Import_openFileDialog.FileName, FileMode.Open, FileAccess.Read);
                        if (fs.Length <= buff_max_size)
                        {
                            int read_size = fs.Read(read_buff, 0, (int)fs.Length);
                            if (read_size == fs.Length)
                            {
                                int sig_name_size = 0;
                                string sig_name = "";
                                int script_size = 0;
                                byte mode = 0;
                                // ファイル内容のチェック
                                if (my_Check_Script_File_Format(read_buff, read_size, ref sig_name_size, ref sig_name, ref script_size, ref mode) == true)
                                {
                                    ArrayList al_temp = new ArrayList();

                                    // 読み込みデータ変換
                                    my_Convert_Byte_Array_2_Array_List(read_buff, Constants.SCRIPT_FILE_SIGNATURE_SIZE_LEN + sig_name_size + Constants.SCRIPT_FILE_FILE_SIZE_LEN + Constants.SCRIPT_FILE_MODE_SIZE_LEN + Constants.SCRIPT_FILE_SCRIPT_SIZE_LEN, script_size, ref al_temp);

                                    // Script Editorに追加
                                    my_Script_Edit_Disp_Data_Add(al_temp, mode);

                                    // 表示更新
                                    FlashReadFirstTime = true;
                                }
                                else
                                {   // スクリプトファイルのフォーマットエラー
                                    MessageBox.Show(RevOmate.Properties.Resources.SCRIPT_FILE_IMPORT_ERROR_MSG, RevOmate.Properties.Resources.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                                }
                            }
                        }
                        else
                        {   // ファイルサイズ異常（大きい）
                            MessageBox.Show(RevOmate.Properties.Resources.SCRIPT_FILE_IMPORT_ERROR_MSG, RevOmate.Properties.Resources.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        }
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
            }
            catch
            {
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (keyboard_setup_assist_status != Constants.KEYBOARD_SETUP_ASSIST_STATUS_NONE)
                {
                    switch (keyboard_setup_assist_status)
                    {
                        case Constants.KEYBOARD_SETUP_ASSIST_STATUS_KEY1:
                            keyboard_setup_assist_input_key_code[0] = (byte)(((int)e.KeyCode) & 0xFF);
                            //keyboard_setup_assist_input_key_code[0] = (byte)(e.KeyValue & 0xFF);

                            System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
                            Bitmap bmp = new Bitmap(myAssembly.GetManifestResourceStream("RevOmate.Resources.KEY_B.png"));
                            pic_keyboard_setup_assist.Image = bmp;

                            lbl_keyboard_setup_assist_msg1.Text = RevOmate.Properties.Resources.KEYBOARD_SETUP_ASSIST_MSG3;

                            keyboard_setup_assist_status = Constants.KEYBOARD_SETUP_ASSIST_STATUS_KEY2;
                            break;
                        case Constants.KEYBOARD_SETUP_ASSIST_STATUS_KEY2:
                            keyboard_setup_assist_input_key_code[1] = (byte)(((int)e.KeyCode) & 0xFF);
                            //keyboard_setup_assist_input_key_code[1] = (byte)(e.KeyValue & 0xFF);

                            int match_count = 0;
                            for (int fi = 0; fi < keyboard_setup_assist_key_code.GetLength(0); fi++)
                            {
                                match_count = 0;
                                for (int fj = 0; fj < keyboard_setup_assist_key_code.GetLength(1); fj++)
                                {
                                    if (keyboard_setup_assist_key_code[fi, fj] == keyboard_setup_assist_input_key_code[fj])
                                    {
                                        match_count++;
                                    }
                                }
                                if (match_count == keyboard_setup_assist_key_code.GetLength(1))
                                {
                                    keyboard_setup_assist_set_type = keyboard_setup_assist_set_type_list[fi];
                                    break;
                                }
                            }

                            switch (keyboard_setup_assist_set_type)
                            {
                                case Constants.KEYBOARD_TYPE_US:
                                    rbtn_keyboard_setup_assist_type1.Checked = false;
                                    rbtn_keyboard_setup_assist_type2.Checked = true;
                                    break;
                                case Constants.KEYBOARD_TYPE_JA:
                                    rbtn_keyboard_setup_assist_type1.Checked = true;
                                    rbtn_keyboard_setup_assist_type2.Checked = false;
                                    break;
                                default:
                                    keyboard_setup_assist_set_type = Constants.KEYBOARD_TYPE_US;
                                    rbtn_keyboard_setup_assist_type1.Checked = false;
                                    rbtn_keyboard_setup_assist_type2.Checked = true;
                                    break;
                            }

                            pic_keyboard_setup_assist.Visible = false;
                            gbx_keyboard_setup_assis_complete.Visible = true;

                            keyboard_setup_assist_status = Constants.KEYBOARD_SETUP_ASSIST_STATUS_COMP;
                            break;
                    }
                }
                else if (my_App_Setting_Data.Script_Rec_Flag == true)
                {
                    byte tmp_usb_key = const_Key_Code.Get_VKtoUSBkey(e.KeyValue.GetHashCode(), system_setting_info.Keyboard_Type, false);
                    if (Script_Rec_Now_Key != (int)tmp_usb_key)
                    {
                        Script_Rec_Now_Key = (int)tmp_usb_key;

                        //if (chkbx_Script_Interval.Checked == true)
                        //{
                        my_script_editor_disp_data.add(-1, Constants.SCRIPT_COMMAND_INTERVAL, 0, 0, 3, sw_Script_Interval, false);
                        //}
                        //my_Set_Script_Editor_Display_Position(-1, -1);

                        my_Form_KeyDown((int)tmp_usb_key, -1, false);
                    }
                    e.Handled = true;
                }
                else if (my_App_Setting_Data.Script_Add_Manual_Control == Constants.SCRIPT_ADD_MANUAL_KEYDOWN
                        || my_App_Setting_Data.Script_Add_Manual_Control == Constants.SCRIPT_ADD_MANUAL_KEYUP)
                {
                    byte tmp_usb_key = const_Key_Code.Get_VKtoUSBkey(e.KeyValue.GetHashCode(), system_setting_info.Keyboard_Type, true);
                    if (my_App_Setting_Data.Script_Add_Manual_Control == Constants.SCRIPT_ADD_MANUAL_KEYDOWN)
                    {
                        my_Form_KeyDown((int)tmp_usb_key, my_App_Setting_Data.Script_Drag_Target_idx, my_App_Setting_Data.Script_Edit_Item_Change_Flag);
                    }
                    if (my_App_Setting_Data.Script_Add_Manual_Control == Constants.SCRIPT_ADD_MANUAL_KEYUP)
                    {
                        my_Form_KeyUp((int)tmp_usb_key, my_App_Setting_Data.Script_Drag_Target_idx, my_App_Setting_Data.Script_Edit_Item_Change_Flag);
                    }
                    e.Handled = true;
                    //this.KeyPreview = false;
                    gbx_Script_Add_Info.Visible = false;
                    my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_NON;
                    my_App_Setting_Data.Script_Edit_Item_Change_Flag = false;
                    my_App_Setting_Data.Script_Drag_Target_idx = -1;
                }
                else if (my_App_Setting_Data.Script_Add_Manual_Control == Constants.SCRIPT_ADD_MANUAL_INTERVAL)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        my_Set_SE_Interval(my_App_Setting_Data.Script_Drag_Target_idx, txtbx_Interval_Inputbox.Text);

                        txtbx_Interval_Inputbox.Visible = false;
                        gbx_Script_Add_Info.Visible = false;
                        my_App_Setting_Data.Script_Drag_Target_idx = -1;

                        my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_NON;
                    }
                }
                else if (my_App_Setting_Data.Script_Add_Manual_Control == Constants.SCRIPT_ADD_MANUAL_JOYHATSWDOWN)
                {
                    if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
                    {
                        byte set_hat_sw = Constants.HAT_SWITCH_NULL;
                        switch (e.KeyCode)
                        {
                            case Keys.Up:
                                set_hat_sw = Constants.HAT_SWITCH_NORTH;
                                break;
                            case Keys.Down:
                                set_hat_sw = Constants.HAT_SWITCH_SOUTH;
                                break;
                            case Keys.Left:
                                set_hat_sw = Constants.HAT_SWITCH_WEST;
                                break;
                            case Keys.Right:
                                set_hat_sw = Constants.HAT_SWITCH_EAST;
                                break;
                        }
                        //my_Set_SE_HatSW(my_App_Setting_Data.Script_Drag_Target_idx, set_hat_sw);

                        my_script_editor_disp_data.add(my_App_Setting_Data.Script_Drag_Target_idx, Constants.SCRIPT_COMMAND_JOY_HATSW_PRESS, set_hat_sw, 0, 2, sw_Script_Interval, my_App_Setting_Data.Script_Edit_Item_Change_Flag);
                        FlashReadFirstTime = true;

                        e.Handled = true;
                        //this.KeyPreview = false;
                        gbx_Script_Add_Info.Visible = false;
                        my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_NON;
                        my_App_Setting_Data.Script_Edit_Item_Change_Flag = false;
                        my_App_Setting_Data.Script_Drag_Target_idx = -1;
                    }
                }
                else if (my_App_Setting_Data.Script_Add_Manual_Control == Constants.SCRIPT_ADD_MANUAL_NON)
                {
                    if (e.KeyCode == Keys.Delete)
                    {
                        if (dgv_ScriptEditor.SelectedRows.Count == 1)
                        {   // 選択行が１行のとき
                            DataGridViewRow row = dgv_ScriptEditor.SelectedRows[0];
                            if (row.Index >= 1)
                            {   // 2行目以降の場合に削除
                                my_script_editor_disp_data.del(row.Index);
                                dgv_ScriptEditor.Rows.RemoveAt(row.Index);
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (my_App_Setting_Data.Script_Rec_Flag == true)
                {
                    //if (chkbx_Script_Interval.Checked == true)
                    //{
                    my_script_editor_disp_data.add(-1, Constants.SCRIPT_COMMAND_INTERVAL, 0, 0, 3, sw_Script_Interval, false);
                    //}
                    //my_Set_Script_Editor_Display_Position(-1, -1);

                    byte tmp_usb_key = const_Key_Code.Get_VKtoUSBkey(e.KeyValue.GetHashCode(), system_setting_info.Keyboard_Type, true);
                    my_Form_KeyUp((int)tmp_usb_key, -1, false);
                    e.Handled = true;

                    if (Script_Rec_Now_Key == (int)tmp_usb_key)
                    {
                        Script_Rec_Now_Key = 0;
                    }
                }
            }
            catch
            {
            }
        }

        public void my_Form_KeyDown(int USB_Key_Code, int p_add_idx, bool p_change_flag)
        {
            try
            {
                //lbl_Debug01.Text = USB_Key_Code.ToString();
                //lbl_Debug02.Text = const_Key_Code.USB_KeyCode_Name[USB_Key_Code & 0xFF];

                my_script_editor_disp_data.add(p_add_idx, Constants.SCRIPT_COMMAND_KEY_PRESS, (byte)(USB_Key_Code & 0xFF), 0, 2, sw_Script_Interval, p_change_flag);
                FlashReadFirstTime = true;
            }
            catch
            {
            }
        }

        public void my_Form_KeyUp(int USB_Key_Code, int p_add_idx, bool p_change_flag)
        {
            try
            {
                //lbl_Debug01.Text = USB_Key_Code.ToString();
                //lbl_Debug02.Text = const_Key_Code.USB_KeyCode_Name[USB_Key_Code & 0xFF];

                my_script_editor_disp_data.add(p_add_idx, Constants.SCRIPT_COMMAND_KEY_RELEASE, (byte)(USB_Key_Code & 0xFF), 0, 2, sw_Script_Interval, p_change_flag);
                FlashReadFirstTime = true;
            }
            catch
            {
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                my_Form_MouseDown(e.Button, 0, -1);
            }
            catch
            {
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                my_Form_MouseUp(e.Button, 0, -1);
            }
            catch
            {
            }
        }

        public void my_Form_MouseDown(MouseButtons p_MouseButtons, int p_interval, int p_add_idx)
        {
            byte[] b_interval = new byte[2];
            try
            {
                if (my_App_Setting_Data.Script_Rec_Flag == true)
                {
                    //lbl_Debug01.Text = p_MouseButtons.ToString();

                    b_interval[0] = (byte)((p_interval >> 8) & 0xFF);
                    b_interval[1] = (byte)(p_interval & 0xFF);
                    if ((p_MouseButtons & MouseButtons.Left) == MouseButtons.Left)
                    {
                        //if (chkbx_Script_Interval.Checked == true)
                        //{
                        my_script_editor_disp_data.add(p_add_idx, Constants.SCRIPT_COMMAND_INTERVAL, b_interval[0], b_interval[1], 3, sw_Script_Interval, false);
                        //}
                        my_script_editor_disp_data.add(p_add_idx, Constants.SCRIPT_COMMAND_L_CLICK, 0, 0, 1, sw_Script_Interval, false);
                    }
                    else if ((p_MouseButtons & MouseButtons.Right) == MouseButtons.Right)
                    {
                        //if (chkbx_Script_Interval.Checked == true)
                        //{
                        my_script_editor_disp_data.add(p_add_idx, Constants.SCRIPT_COMMAND_INTERVAL, b_interval[0], b_interval[1], 3, sw_Script_Interval, false);
                        //}
                        my_script_editor_disp_data.add(p_add_idx, Constants.SCRIPT_COMMAND_R_CLICK, 0, 0, 1, sw_Script_Interval, false);
                    }
                    else if ((p_MouseButtons & MouseButtons.Middle) == MouseButtons.Middle)
                    {
                        //if (chkbx_Script_Interval.Checked == true)
                        //{
                        my_script_editor_disp_data.add(p_add_idx, Constants.SCRIPT_COMMAND_INTERVAL, b_interval[0], b_interval[1], 3, sw_Script_Interval, false);
                        //}
                        my_script_editor_disp_data.add(p_add_idx, Constants.SCRIPT_COMMAND_W_CLICK, 0, 0, 1, sw_Script_Interval, false);
                    }
                    else if ((p_MouseButtons & MouseButtons.XButton1) == MouseButtons.XButton1)
                    {
                        //if (chkbx_Script_Interval.Checked == true)
                        //{
                        my_script_editor_disp_data.add(p_add_idx, Constants.SCRIPT_COMMAND_INTERVAL, b_interval[0], b_interval[1], 3, sw_Script_Interval, false);
                        //}
                        my_script_editor_disp_data.add(p_add_idx, Constants.SCRIPT_COMMAND_B4_CLICK, 0, 0, 1, sw_Script_Interval, false);
                    }
                    else if ((p_MouseButtons & MouseButtons.XButton2) == MouseButtons.XButton2)
                    {
                        //if (chkbx_Script_Interval.Checked == true)
                        //{
                        my_script_editor_disp_data.add(p_add_idx, Constants.SCRIPT_COMMAND_INTERVAL, b_interval[0], b_interval[1], 3, sw_Script_Interval, false);
                        //}
                        my_script_editor_disp_data.add(p_add_idx, Constants.SCRIPT_COMMAND_B5_CLICK, 0, 0, 1, sw_Script_Interval, false);
                    }
                    FlashReadFirstTime = true;
                }
                else if (my_App_Setting_Data.Script_Add_Manual_Control == Constants.SCRIPT_ADD_MANUAL_MOUSECLICK)
                {   // 
                    if ((p_MouseButtons & MouseButtons.Left) == MouseButtons.Left)
                    {
                        my_script_editor_disp_data.add(my_App_Setting_Data.Script_Drag_Target_idx, Constants.SCRIPT_COMMAND_L_CLICK, 0, 0, 1, sw_Script_Interval, my_App_Setting_Data.Script_Edit_Item_Change_Flag);
                    }
                    else if ((p_MouseButtons & MouseButtons.Right) == MouseButtons.Right)
                    {
                        my_script_editor_disp_data.add(my_App_Setting_Data.Script_Drag_Target_idx, Constants.SCRIPT_COMMAND_R_CLICK, 0, 0, 1, sw_Script_Interval, my_App_Setting_Data.Script_Edit_Item_Change_Flag);
                    }
                    else if ((p_MouseButtons & MouseButtons.Middle) == MouseButtons.Middle)
                    {
                        my_script_editor_disp_data.add(my_App_Setting_Data.Script_Drag_Target_idx, Constants.SCRIPT_COMMAND_W_CLICK, 0, 0, 1, sw_Script_Interval, my_App_Setting_Data.Script_Edit_Item_Change_Flag);
                    }
                    else if ((p_MouseButtons & MouseButtons.XButton1) == MouseButtons.XButton1)
                    {
                        my_script_editor_disp_data.add(my_App_Setting_Data.Script_Drag_Target_idx, Constants.SCRIPT_COMMAND_B4_CLICK, 0, 0, 1, sw_Script_Interval, my_App_Setting_Data.Script_Edit_Item_Change_Flag);
                    }
                    else if ((p_MouseButtons & MouseButtons.XButton2) == MouseButtons.XButton2)
                    {
                        my_script_editor_disp_data.add(my_App_Setting_Data.Script_Drag_Target_idx, Constants.SCRIPT_COMMAND_B5_CLICK, 0, 0, 1, sw_Script_Interval, my_App_Setting_Data.Script_Edit_Item_Change_Flag);
                    }
                    gbx_Script_Add_Info.Visible = false;
                    my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_NON;
                    my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                    my_App_Setting_Data.Script_Edit_Item_Change_Flag = false;
                    my_App_Setting_Data.Script_Drag_Target_idx = -1;

                    FlashReadFirstTime = true;
                }
                else if (my_App_Setting_Data.Script_Add_Manual_Control == Constants.SCRIPT_ADD_MANUAL_MOUSERELEASE)
                {   // 
                    if ((p_MouseButtons & MouseButtons.Left) == MouseButtons.Left)
                    {
                        my_script_editor_disp_data.add(my_App_Setting_Data.Script_Drag_Target_idx, Constants.SCRIPT_COMMAND_L_RELEASE, 0, 0, 1, sw_Script_Interval, my_App_Setting_Data.Script_Edit_Item_Change_Flag);
                    }
                    else if ((p_MouseButtons & MouseButtons.Right) == MouseButtons.Right)
                    {
                        my_script_editor_disp_data.add(my_App_Setting_Data.Script_Drag_Target_idx, Constants.SCRIPT_COMMAND_R_RELEASE, 0, 0, 1, sw_Script_Interval, my_App_Setting_Data.Script_Edit_Item_Change_Flag);
                    }
                    else if ((p_MouseButtons & MouseButtons.Middle) == MouseButtons.Middle)
                    {
                        my_script_editor_disp_data.add(my_App_Setting_Data.Script_Drag_Target_idx, Constants.SCRIPT_COMMAND_W_RELEASE, 0, 0, 1, sw_Script_Interval, my_App_Setting_Data.Script_Edit_Item_Change_Flag);
                    }
                    else if ((p_MouseButtons & MouseButtons.XButton1) == MouseButtons.XButton1)
                    {
                        my_script_editor_disp_data.add(my_App_Setting_Data.Script_Drag_Target_idx, Constants.SCRIPT_COMMAND_B4_RELEASE, 0, 0, 1, sw_Script_Interval, my_App_Setting_Data.Script_Edit_Item_Change_Flag);
                    }
                    else if ((p_MouseButtons & MouseButtons.XButton2) == MouseButtons.XButton2)
                    {
                        my_script_editor_disp_data.add(my_App_Setting_Data.Script_Drag_Target_idx, Constants.SCRIPT_COMMAND_B5_RELEASE, 0, 0, 1, sw_Script_Interval, my_App_Setting_Data.Script_Edit_Item_Change_Flag);
                    }
                    gbx_Script_Add_Info.Visible = false;
                    my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_NON;
                    my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                    my_App_Setting_Data.Script_Edit_Item_Change_Flag = false;
                    my_App_Setting_Data.Script_Drag_Target_idx = -1;

                    FlashReadFirstTime = true;
                }
            }
            catch
            {
            }
        }

        public void my_Form_MouseUp(MouseButtons p_MouseButtons, int p_interval, int p_add_idx)
        {
            byte[] b_interval = new byte[2];
            try
            {
                if (my_App_Setting_Data.Script_Rec_Flag == true)
                {
                    //lbl_Debug01.Text = p_MouseButtons.ToString();

                    b_interval[0] = (byte)((p_interval >> 8) & 0xFF);
                    b_interval[1] = (byte)(p_interval & 0xFF);
                    if ((p_MouseButtons & MouseButtons.Left) == MouseButtons.Left)
                    {
                        //if (chkbx_Script_Interval.Checked == true)
                        //{
                        my_script_editor_disp_data.add(p_add_idx, Constants.SCRIPT_COMMAND_INTERVAL, b_interval[0], b_interval[1], 3, sw_Script_Interval, false);
                        //}
                        my_script_editor_disp_data.add(p_add_idx, Constants.SCRIPT_COMMAND_L_RELEASE, 0, 0, 1, sw_Script_Interval, false);
                    }
                    else if ((p_MouseButtons & MouseButtons.Right) == MouseButtons.Right)
                    {
                        //if (chkbx_Script_Interval.Checked == true)
                        //{
                        my_script_editor_disp_data.add(p_add_idx, Constants.SCRIPT_COMMAND_INTERVAL, b_interval[0], b_interval[1], 3, sw_Script_Interval, false);
                        //}
                        my_script_editor_disp_data.add(p_add_idx, Constants.SCRIPT_COMMAND_R_RELEASE, 0, 0, 1, sw_Script_Interval, false);
                    }
                    else if ((p_MouseButtons & MouseButtons.Middle) == MouseButtons.Middle)
                    {
                        //if (chkbx_Script_Interval.Checked == true)
                        //{
                        my_script_editor_disp_data.add(p_add_idx, Constants.SCRIPT_COMMAND_INTERVAL, b_interval[0], b_interval[1], 3, sw_Script_Interval, false);
                        //}
                        my_script_editor_disp_data.add(p_add_idx, Constants.SCRIPT_COMMAND_W_RELEASE, 0, 0, 1, sw_Script_Interval, false);
                    }
                    else if ((p_MouseButtons & MouseButtons.XButton1) == MouseButtons.XButton1)
                    {
                        //if (chkbx_Script_Interval.Checked == true)
                        //{
                        my_script_editor_disp_data.add(p_add_idx, Constants.SCRIPT_COMMAND_INTERVAL, b_interval[0], b_interval[1], 3, sw_Script_Interval, false);
                        //}
                        my_script_editor_disp_data.add(p_add_idx, Constants.SCRIPT_COMMAND_B4_RELEASE, 0, 0, 1, sw_Script_Interval, false);
                    }
                    else if ((p_MouseButtons & MouseButtons.XButton2) == MouseButtons.XButton2)
                    {
                        //if (chkbx_Script_Interval.Checked == true)
                        //{
                        my_script_editor_disp_data.add(p_add_idx, Constants.SCRIPT_COMMAND_INTERVAL, b_interval[0], b_interval[1], 3, sw_Script_Interval, false);
                        //}
                        my_script_editor_disp_data.add(p_add_idx, Constants.SCRIPT_COMMAND_B5_RELEASE, 0, 0, 1, sw_Script_Interval, false);
                    }
                    FlashReadFirstTime = true;
                }
            }
            catch
            {
            }
        }

        private void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                my_Form_MouseWheel(e.Button, 0, e.Delta, -1);
            }
            catch
            {
            }
        }
        public void my_Form_MouseWheel(MouseButtons p_MouseButtons, int p_interval, int p_Delta, int p_add_idx)
        {
            byte[] b_interval = new byte[2];
            try
            {
                if (my_App_Setting_Data.Script_Rec_Flag == true)
                {
                    int scroll_count = p_Delta / 120;
                    b_interval[0] = (byte)((p_interval >> 8) & 0xFF);
                    b_interval[1] = (byte)(p_interval & 0xFF);
                    if (scroll_count > 0)
                    {
                        //if (chkbx_Script_Interval.Checked == true)
                        //{
                        my_script_editor_disp_data.add(p_add_idx, Constants.SCRIPT_COMMAND_INTERVAL, b_interval[0], b_interval[1], 3, sw_Script_Interval, false);
                        //}
                        my_script_editor_disp_data.add(p_add_idx, Constants.SCRIPT_COMMAND_WHEEL_UP, 0x01, 0, 1, sw_Script_Interval, false);
                    }
                    else if (scroll_count < 0)
                    {
                        //if (chkbx_Script_Interval.Checked == true)
                        //{
                        my_script_editor_disp_data.add(p_add_idx, Constants.SCRIPT_COMMAND_INTERVAL, b_interval[0], b_interval[1], 3, sw_Script_Interval, false);
                        //}
                        my_script_editor_disp_data.add(p_add_idx, Constants.SCRIPT_COMMAND_WHEEL_DOWN, 0xFF, 0, 1, sw_Script_Interval, false);
                    }
                    FlashReadFirstTime = true;

                    //lbl_Debug01.Text = p_MouseButtons.ToString();
                    ////lbl_Debug02.Text = e.Delta.ToString();
                    //lbl_Debug02.Text = scroll_count.ToString();
                }
                else if (my_App_Setting_Data.Script_Add_Manual_Control == Constants.SCRIPT_ADD_MANUAL_MOUSEWHEEL)
                {   // 
                    int scroll_count = p_Delta / 120;
                    b_interval[0] = (byte)((p_interval >> 8) & 0xFF);
                    b_interval[1] = (byte)(p_interval & 0xFF);
                    if (scroll_count > 0)
                    {
                        my_script_editor_disp_data.add(my_App_Setting_Data.Script_Drag_Target_idx, Constants.SCRIPT_COMMAND_WHEEL_UP, 0x01, 0, 1, sw_Script_Interval, my_App_Setting_Data.Script_Edit_Item_Change_Flag);
                    }
                    else if (scroll_count < 0)
                    {
                        my_script_editor_disp_data.add(my_App_Setting_Data.Script_Drag_Target_idx, Constants.SCRIPT_COMMAND_WHEEL_DOWN, 0xFF, 0, 1, sw_Script_Interval, my_App_Setting_Data.Script_Edit_Item_Change_Flag);
                    }
                    gbx_Script_Add_Info.Visible = false;
                    my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_NON;
                    my_App_Setting_Data.Script_Setting_Drag_Start_Control = Constants.SCRIPT_DRAG_CTRL_NON;
                    my_App_Setting_Data.Script_Edit_Item_Change_Flag = false;
                    my_App_Setting_Data.Script_Drag_Target_idx = -1;

                    FlashReadFirstTime = true;
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Script Editorにコマンドを追加
        /// modeは、Script Editoにコマンドがないときのみ変更
        /// </summary>
        /// <param name="p_al"></param>
        /// <param name="p_mode"></param>
        private void my_Script_Edit_Disp_Data_Add(ArrayList p_al, byte p_mode)
        {
            bool b_error_flag = false;
            try
            {
                int fi = 0;
                byte id = 0; ;
                byte data1 = 0;
                byte data2 = 0;
                byte size = 0;

                // コマンドの登録がないときのみモードを変更する
                if (my_script_editor_disp_data.al_Command_ID.Count == 0)
                {
                    my_script_editor_disp_data.Script_Mode = p_mode;
                }

                // コマンドを追加
                while (fi < p_al.Count && b_error_flag == false)
                {
                    id = (byte)p_al[fi++];
                    switch (id)
                    {
                        case Constants.SCRIPT_COMMAND_INTERVAL:
                        case Constants.SCRIPT_COMMAND_MOUSE_MOVE:
                        case Constants.SCRIPT_COMMAND_JOY_L_LEVER:
                        case Constants.SCRIPT_COMMAND_JOY_R_LEVER:
                            if ((fi + 1) < p_al.Count)
                            {
                                data1 = (byte)p_al[fi++];
                                data2 = (byte)p_al[fi++];
                                size = 3;
                            }
                            else
                            {
                                b_error_flag = true;
                            }
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
                            if (fi < p_al.Count)
                            {
                                data1 = (byte)p_al[fi++];
                                data2 = 0;
                                size = 2;
                            }
                            else
                            {
                                b_error_flag = true;
                            }
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
                            data1 = 0;
                            data2 = 0;
                            size = 1;
                            break;
                        default:
                            b_error_flag = true;
                            break;
                    }

                    if (b_error_flag == false)
                    {
                        my_script_editor_disp_data.add(-1, id, data1, data2, size, sw_Script_Interval, false);
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 表示の更新をする
        /// </summary>
        /// <param name="display_type">更新対象画面</param>
        /// <param name="p_mouse_mode"></param>
        /// <param name="p_mouse_setting_data"></param>
        /// <param name="p_p_script_info"></param>
        /// <param name="p_script_info_datas"></param>
        private void my_Set_Display( STR_SCRIPT_INFO p_script_info, STR_SCRIPT_INFO_DATAS p_script_info_datas)
        {
            try
            {
                my_Set_Script_Editor_Display();
                my_macro_name_update();
                my_function_name_update();

                // 機能設定画面とボタン設定画面の表示
                my_function_setting_disp(function_setting_mode_select_no, true);
                my_button_setting_disp(button_setting_mode_select_no, true);
                my_sw_func_name_disp(button_setting_mode_select_no, true);
                my_encoder_script_setting_disp(encoder_script_select_no, true);
                my_base_setting_disp(true);
            }
            catch
            {
            }
        }

        private void btn_debug_flash_read_Click(object sender, EventArgs e)
        {
            try
            {
                debug_flash_read_start_addr = Convert.ToUInt32(txtbx_debug_flash_read_addr.Text, 16);
                debug_flash_read_size = Convert.ToUInt32(txtbx_debug_flash_read_size.Text, 16);
                if (debug_flash_read_size < debug_flash_read_buff.Length
                    && (debug_flash_read_start_addr + debug_flash_read_size) <= 0x200000)
                {
                    debug_flash_read_comp = false;
                    debug_flash_read_req = true;
                }
            }
            catch
            {
            }
        }

        private void dgv_ScriptList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || dgv_ScriptList.RowCount <= 0)
                {
                    return;
                }

                if (e.ColumnIndex == dgv_ScriptList.Columns["dgv_ScriptList_No"].Index)
                {
                    if (dgv_ScriptList[dgv_ScriptList.Columns["dgv_ScriptList_No"].Index, e.RowIndex].Value != null)
                    {
                    }
                }

                if (e.ColumnIndex == dgv_ScriptList.Columns["dgv_ScriptList_ScriptName"].Index)
                {
                }
            }
            catch
            {
            }
        }


        public void my_macro_name_set(int p_set_idx, string p_set_macro_name)
        {
            byte[] by_name;
            try
            {
                if (0 <= p_set_idx && p_set_idx < my_script_info_datas.Script_Info_Datas.Length)
                {
                    by_name = System.Text.Encoding.Unicode.GetBytes(p_set_macro_name);
                    if (by_name.Length <= FlashControl.FM_SCRIPT_INFO_DATA_NAME_SIZE)
                    {
                        my_script_info_datas.Script_Info_Datas[p_set_idx].Name_Size = (byte)(by_name.Length & 0xFF);
                        my_script_info_datas.Script_Info_Datas[p_set_idx].Name = p_set_macro_name;
                    }
                }
            }
            catch
            {
            }
        }
        private void my_macro_name_update()
        {
            try
            {
                // マクロ設定タブ
                my_macro_setting_macro_name_update();
                // ボタン設定タブ
                my_button_setting_macro_name_update();
                // ダイアルマクロ設定タブ
                my_encoder_script_macro_name_update();
            }
            catch
            {
            }
        }
        // マクロ設定タブのマクロ名称をアップデート
        private void my_macro_setting_macro_name_update()
        {
            try
            {
                // 現在の選択行を記憶
                int sel_idx = 0;
                if (dgv_ScriptList.CurrentRow != null)
                {
                    sel_idx = dgv_ScriptList.CurrentRow.Index;
                }
                // マクロ名称を更新
                int script_name_idx = dgv_ScriptList.Columns["dgv_ScriptList_ScriptName"].Index;
#if DEBUG
                int macro_max_num = Constants.SCRIPT_NUM;
#else
                int macro_max_num = Constants.SCRIPT_USER_USE_NUM;
#endif
                for (int fi = 0; fi < macro_max_num; fi++)
                {
                    if (my_script_info_datas.Script_Info_Datas[fi].Name != "")
                    {
                        dgv_ScriptList.Rows[fi].Cells[script_name_idx].Value = my_script_info_datas.Script_Info_Datas[fi].Name;
                    }
                    else
                    {
                        dgv_ScriptList.Rows[fi].Cells[script_name_idx].Value = RevOmate.Properties.Resources.MACRO_NAME_UNDEFINE;
                    }
                }

            }
            catch
            {
            }
        }

        // 機能設定タブの機能設定名称をアップデート
        private void my_function_setting_function_name_update()
        {
            try
            {
                // 機能設定画面　機能１～４名称更新
                for (int fi = 0; fi < my_txtbx_func_name.Length; fi++)
                {
                    if (my_func_datas.func_datas[function_setting_mode_select_no].func_data[fi].func_name != "")
                    {
                        my_txtbx_func_name[fi].Text = my_func_datas.func_datas[function_setting_mode_select_no].func_data[fi].func_name;
                    }
                    else
                    {
                        my_txtbx_func_name[fi].Text = RevOmate.Properties.Resources.FUNCTION_NAME_UNDEFINE;
                    }
                }
            }
            catch
            {
            }
        }
        // ボタン設定タブの機能設定名称をアップデート
        private void my_button_setting_function_name_update()
        {
            try
            {
                // 現在の選択を記憶
                int sel_idx = cmbbx_encoder_default.SelectedIndex;

                cmbbx_encoder_default.Items.Clear();
                // 先頭に未設定を追加
                string temp_func_name = "";
                //temp_func_name += Constants.FUNCTION_NAME_UNSETTING;
                //cmbbx_encoder_default.Items.Add(temp_func_name);
                for (int fi = 0; fi < Constants.FUNCTION_NUM; fi++)
                {
                    temp_func_name = string.Format("機能{0:0}:", fi + 1);
                    if (my_func_datas.func_datas[button_setting_mode_select_no].func_data[fi].func_name != "")
                    {
                        temp_func_name += my_func_datas.func_datas[button_setting_mode_select_no].func_data[fi].func_name;
                    }
                    else
                    {
                        temp_func_name += RevOmate.Properties.Resources.FUNCTION_NAME_UNDEFINE;
                    }
                    cmbbx_encoder_default.Items.Add(temp_func_name);
                }

                //設定されているマクロ番号を選択する
                sel_idx = my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].encoder_func_no;
                if (sel_idx < 0 || cmbbx_encoder_default.Items.Count <= sel_idx)
                {   // エラー（未設定）
                    sel_idx = 0;
                }
                cmbbx_encoder_default.SelectedIndex = sel_idx;
            }
            catch
            {
            }
        }
        // ボタン設定タブのマクロ名称をアップデート
        private void my_button_setting_macro_name_update()
        {
            int sel_idx = 0;
            string temp_macro_name = "";
            try
            {
                // ボタン１～１０
                for (int fi = 0; fi < my_button_macro_select.Length; fi++)
                {
                    // 現在の選択を記憶
                    sel_idx = my_button_macro_select[fi].SelectedIndex;

                    my_button_macro_select[fi].Items.Clear();
                    // 先頭に未設定を追加
                    temp_macro_name = "";
                    //temp_macro_name += string.Format("{0:000}:", 0);
                    temp_macro_name += RevOmate.Properties.Resources.MACRO_NAME_UNSETTING;
                    my_button_macro_select[fi].Items.Add(temp_macro_name);
                    // 特殊機能追加
                    for (int fj = 0; fj < sp_func_type_list.Length; fj++)
                    {
                        my_button_macro_select[fi].Items.Add(sp_func_type_list[fj]);
                    }
                    // スクリプトリスト追加
                    for (int fj = 0; fj < my_script_info_datas.Script_Info_Datas.Length; fj++)
                    {
                        temp_macro_name = string.Format("{0}{1:000}:", RevOmate.Properties.Resources.MACRO_NAME_NO_MACRO, fj + 1);
                        if (my_script_info_datas.Script_Info_Datas[fj].Name != "")
                        {
                            temp_macro_name += my_script_info_datas.Script_Info_Datas[fj].Name;
                        }
                        else
                        {
                            temp_macro_name += RevOmate.Properties.Resources.MACRO_NAME_UNDEFINE;
                        }
                        my_button_macro_select[fi].Items.Add(temp_macro_name);
                    }

                    //設定されている特殊機能番号またはマクロ番号を選択する
                    sel_idx = 0;
                    if (my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].sw_sp_func_no[fi] > 0)
                    {
                        sel_idx = my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].sw_sp_func_no[fi];
                        if (sel_idx < 0 || Constants.SP_FUNC_NUM < sel_idx)
                        {   // エラー（未設定）
                            sel_idx = 0;
                        }
                    }
                    else if (my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].sw_exe_script_no[fi] > 0)
                    {
                        sel_idx = my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].sw_exe_script_no[fi];
                        if (sel_idx < 0 || Constants.SCRIPT_NUM < sel_idx)
                        {   // エラー（未設定）
                            sel_idx = 0;
                        }
                        else
                        {
                            // 特殊機能の分、ずらす
                            sel_idx += Constants.SP_FUNC_NUM;
                        }
                    }
                    if (0 <= sel_idx && sel_idx < my_button_macro_select[fi].Items.Count)
                    {
                        my_button_macro_select[fi].SelectedIndex = sel_idx;
                    }
                    else
                    {
                        my_button_macro_select[fi].SelectedIndex = 0;
                    }
                }


                // エンコーダーボタン
                sel_idx = cmbbx_encoder_button.SelectedIndex;

                cmbbx_encoder_button.Items.Clear();
                // 先頭に未設定を追加
                temp_macro_name = "";
                //temp_macro_name += string.Format("{0:000}:", 0);
                temp_macro_name += RevOmate.Properties.Resources.MACRO_NAME_UNSETTING;
                cmbbx_encoder_button.Items.Add(temp_macro_name);
                // 特殊機能追加
                for (int fj = 0; fj < sp_func_type_list.Length; fj++)
                {
                    cmbbx_encoder_button.Items.Add(sp_func_type_list[fj]);
                }
                // スクリプトリスト追加
                for (int fj = 0; fj < my_script_info_datas.Script_Info_Datas.Length; fj++)
                {
                    temp_macro_name = string.Format("{0}{1:000}:", RevOmate.Properties.Resources.MACRO_NAME_NO_MACRO, fj + 1);
                    if (my_script_info_datas.Script_Info_Datas[fj].Name != "")
                    {
                        temp_macro_name += my_script_info_datas.Script_Info_Datas[fj].Name;
                    }
                    else
                    {
                        temp_macro_name += RevOmate.Properties.Resources.MACRO_NAME_UNDEFINE;
                    }
                    cmbbx_encoder_button.Items.Add(temp_macro_name);
                }
                //設定されている特殊機能番号またはマクロ番号を選択する
                sel_idx = 0;
                if (my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].sw_sp_func_no[Constants.ENCODER_BUTTON_ID] > 0)
                {
                    sel_idx = my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].sw_sp_func_no[Constants.ENCODER_BUTTON_ID];
                    if (sel_idx < 0 || Constants.SP_FUNC_NUM < sel_idx)
                    {   // エラー（未設定）
                        sel_idx = 0;
                    }
                }
                else if (my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].sw_exe_script_no[Constants.ENCODER_BUTTON_ID] > 0)
                {
                    sel_idx = my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].sw_exe_script_no[Constants.ENCODER_BUTTON_ID];
                    if (sel_idx < 0 || Constants.SCRIPT_NUM < sel_idx)
                    {   // エラー（未設定）
                        sel_idx = 0;
                    }
                    else
                    {
                        // 特殊機能の分、ずらす
                        sel_idx += Constants.SP_FUNC_NUM;
                    }
                }
                if (0 <= sel_idx && sel_idx < cmbbx_encoder_button.Items.Count)
                {
                    cmbbx_encoder_button.SelectedIndex = sel_idx;
                }
                else
                {
                    cmbbx_encoder_button.SelectedIndex = 0;
                }

            }
            catch
            {
            }
        }

        private void my_func_name_set(int p_mode_idx, int p_func_idx, string p_set_func_name)
        {
            try
            {
                byte[] by_name;
                try
                {
                    if (0 <= p_mode_idx && p_mode_idx < Constants.MODE_NUM)
                    {
                        if (0 <= p_func_idx && p_func_idx < Constants.FUNCTION_NUM)
                        {
                            by_name = System.Text.Encoding.Unicode.GetBytes(p_set_func_name);
                            if (by_name.Length <= FlashControl.FM_FUNCTION_NAME_MAX_SIZE)
                            {
                                my_func_datas.func_datas[p_mode_idx].func_data[p_func_idx].func_name_size = (byte)(by_name.Length & 0xFF);
                                my_func_datas.func_datas[p_mode_idx].func_data[p_func_idx].func_name = p_set_func_name;
                            }
                        }
                    }
                }
                catch
                {
                }
            }
            catch
            {
            }
        }
        private void my_function_name_update()
        {
            try
            {
                // 機能設定タブ
                my_function_setting_function_name_update();
                // ボタン設定タブ
                my_button_setting_function_name_update();
            }
            catch
            {
            }

        }


        // 指定モードの機能設定画面を表示
        private void my_function_setting_disp(int p_mode_no, int p_func_no, int p_cw_ccw_idx, bool p_visible)
        {
            sbyte tmp_sbyte;
            decimal tmp_dec;
            try
            {
                if (0 <= p_mode_no && p_mode_no < Constants.MODE_NUM)
                {
                    if (0 <= p_func_no && p_func_no < Constants.FUNCTION_NUM)
                    {
                        // LED
                        for (int led_idx = 0; led_idx < Constants.LED_RGB_COLOR_NUM; led_idx++)
                        {
                            my_tb_LED_Duty_set_func[p_func_no, led_idx].Value = my_func_datas.func_datas[p_mode_no].func_data[p_func_no].LED_RGB_duty[led_idx];
                        }
                        // LED輝度設定
                        if (my_func_datas.func_datas[p_mode_no].func_data[p_func_no].LED_brightness_level < Constants.LED_BRIGHTNESS_LEVEL_SET_NUM)
                        {
                            my_rbtn_LED_Brightness_Level_set_func[p_func_no, my_func_datas.func_datas[p_mode_no].func_data[p_func_no].LED_brightness_level].Checked = true;
                        }
                        else
                        {
                            my_rbtn_LED_Brightness_Level_set_func[p_func_no, Constants.LED_BRIGHTNESS_LEVEL_NORMAL].Checked = true;
                        }


                        if (0 <= p_cw_ccw_idx && p_cw_ccw_idx < Constants.CW_CCW_NUM)
                        {
                            int data_idx = (p_mode_no * Constants.FUNCTION_NUM * Constants.CW_CCW_NUM) + (p_func_no * Constants.CW_CCW_NUM) + p_cw_ccw_idx;

                            my_set_disp_set_type(my_set_data.set_type[data_idx], p_func_no, p_cw_ccw_idx);
                            //if (0 <= my_set_data.set_type[data_idx] && my_set_data.set_type[data_idx] < my_cmbbx_set_type[p_func_no, p_cw_ccw_idx].Items.Count)
                            //{
                            //    my_cmbbx_set_type[p_func_no, p_cw_ccw_idx].SelectedIndex = my_set_data.set_type[data_idx];
                            //}

                            switch (my_set_data.set_type[data_idx])
                            {
                                case Constants.SET_TYPE_NONE:
                                    my_lbl_title[p_func_no, p_cw_ccw_idx].Visible = false;
                                    my_mouse_x[p_func_no, p_cw_ccw_idx].Visible = false;
                                    my_mouse_y[p_func_no, p_cw_ccw_idx].Visible = false;
                                    for (int fi = 0; fi < my_keyboard_modifier.GetLength(2); fi++)
                                    {
                                        my_keyboard_modifier[p_func_no, p_cw_ccw_idx, fi].Visible = false;
                                    }
                                    my_keyboard_key[p_func_no, p_cw_ccw_idx].Visible = false;
                                    my_keyboard_key_clr[p_func_no, p_cw_ccw_idx].Visible = false;
                                    //my_joypad_x[p_func_no, p_cw_ccw_idx].Visible = false;
                                    //my_joypad_y[p_func_no, p_cw_ccw_idx].Visible = false;
                                    break;
                                case Constants.SET_TYPE_MOUSE_LCLICK:
                                case Constants.SET_TYPE_MOUSE_RCLICK:
                                case Constants.SET_TYPE_MOUSE_WHCLICK:
                                case Constants.SET_TYPE_MOUSE_B4CLICK:
                                case Constants.SET_TYPE_MOUSE_B5CLICK:
                                case Constants.SET_TYPE_MOUSE_DCLICK:
                                    my_lbl_title[p_func_no, p_cw_ccw_idx].Visible = false;
                                    my_mouse_x[p_func_no, p_cw_ccw_idx].Visible = false;
                                    my_mouse_y[p_func_no, p_cw_ccw_idx].Visible = false;
                                    for (int fi = 0; fi < my_keyboard_modifier.GetLength(2); fi++)
                                    {
                                        my_keyboard_modifier[p_func_no, p_cw_ccw_idx, fi].Visible = false;
                                    }
                                    my_keyboard_key[p_func_no, p_cw_ccw_idx].Visible = false;
                                    my_keyboard_key_clr[p_func_no, p_cw_ccw_idx].Visible = false;
                                    //my_joypad_x[p_func_no, p_cw_ccw_idx].Visible = false;
                                    //my_joypad_y[p_func_no, p_cw_ccw_idx].Visible = false;
                                    break;
                                case Constants.SET_TYPE_MOUSE_MOVE:
                                    my_lbl_title[p_func_no, p_cw_ccw_idx].Text = RevOmate.Properties.Resources.MOUSE_MOVE_TEXT;
                                    my_lbl_title[p_func_no, p_cw_ccw_idx].Visible = p_visible;
                                    my_mouse_x[p_func_no, p_cw_ccw_idx].Visible = p_visible;
                                    my_mouse_x[p_func_no, p_cw_ccw_idx].Minimum = Constants.MOUSE_MOVE_MIN;
                                    my_mouse_x[p_func_no, p_cw_ccw_idx].Maximum = Constants.MOUSE_MOVE_MAX;
                                    if (my_set_data.mouse_data[data_idx, Constants.MOUSE_DATA_X_MOVE_IDX] > 0x7F)
                                    {
                                        tmp_sbyte = (sbyte)(my_set_data.mouse_data[data_idx, Constants.MOUSE_DATA_X_MOVE_IDX] - 0x100);
                                    }
                                    else
                                    {
                                        tmp_sbyte = (sbyte)(my_set_data.mouse_data[data_idx, Constants.MOUSE_DATA_X_MOVE_IDX]);
                                    }
                                    //tmp_sbyte = (sbyte)(my_set_data.mouse_data[data_idx, Constants.MOUSE_DATA_X_MOVE_IDX]);
                                    tmp_dec = tmp_sbyte;
                                    my_mouse_x[p_func_no, p_cw_ccw_idx].Value = tmp_dec;
                                    my_mouse_y[p_func_no, p_cw_ccw_idx].Visible = p_visible;
                                    my_mouse_y[p_func_no, p_cw_ccw_idx].Minimum = Constants.MOUSE_MOVE_MIN;
                                    my_mouse_y[p_func_no, p_cw_ccw_idx].Maximum = Constants.MOUSE_MOVE_MAX;
                                    if (my_set_data.mouse_data[data_idx, Constants.MOUSE_DATA_Y_MOVE_IDX] > 0x7F)
                                    {
                                        tmp_sbyte = (sbyte)(my_set_data.mouse_data[data_idx, Constants.MOUSE_DATA_Y_MOVE_IDX] - 0x100);
                                    }
                                    else
                                    {
                                        tmp_sbyte = (sbyte)(my_set_data.mouse_data[data_idx, Constants.MOUSE_DATA_Y_MOVE_IDX]);
                                    }
                                    //tmp_sbyte = (sbyte)(my_set_data.mouse_data[data_idx, Constants.MOUSE_DATA_Y_MOVE_IDX]);
                                    tmp_dec = tmp_sbyte;
                                    my_mouse_y[p_func_no, p_cw_ccw_idx].Value = tmp_dec;
                                    for (int fi = 0; fi < my_keyboard_modifier.GetLength(2); fi++)
                                    {
                                        my_keyboard_modifier[p_func_no, p_cw_ccw_idx, fi].Visible = false;
                                    }
                                    my_keyboard_key[p_func_no, p_cw_ccw_idx].Visible = false;
                                    my_keyboard_key_clr[p_func_no, p_cw_ccw_idx].Visible = false;
                                    //my_joypad_x[p_func_no, p_cw_ccw_idx].Visible = false;
                                    //my_joypad_y[p_func_no, p_cw_ccw_idx].Visible = false;
                                    break;
                                case Constants.SET_TYPE_MOUSE_WHSCROLL:
                                    my_lbl_title[p_func_no, p_cw_ccw_idx].Text = RevOmate.Properties.Resources.MOUSE_SCROLL_TEXT;
                                    my_lbl_title[p_func_no, p_cw_ccw_idx].Visible = p_visible;
                                    my_mouse_x[p_func_no, p_cw_ccw_idx].Visible = p_visible;
                                    my_mouse_x[p_func_no, p_cw_ccw_idx].Minimum = Constants.MOUSE_SCROLL_MIN;
                                    my_mouse_x[p_func_no, p_cw_ccw_idx].Maximum = Constants.MOUSE_SCROLL_MAX;
                                    if (my_set_data.mouse_data[data_idx, Constants.MOUSE_DATA_WHEEL_IDX] > 0x7F)
                                    {
                                        tmp_sbyte = (sbyte)(my_set_data.mouse_data[data_idx, Constants.MOUSE_DATA_WHEEL_IDX] - 0x100);
                                    }
                                    else
                                    {
                                        tmp_sbyte = (sbyte)(my_set_data.mouse_data[data_idx, Constants.MOUSE_DATA_WHEEL_IDX]);
                                    }
                                    //tmp_sbyte = (sbyte)(my_set_data.mouse_data[data_idx, Constants.MOUSE_DATA_WHEEL_IDX]);
                                    tmp_dec = tmp_sbyte;
                                    my_mouse_x[p_func_no, p_cw_ccw_idx].Value = tmp_dec;
                                    my_mouse_y[p_func_no, p_cw_ccw_idx].Visible = false;
                                    for (int fi = 0; fi < my_keyboard_modifier.GetLength(2); fi++)
                                    {
                                        my_keyboard_modifier[p_func_no, p_cw_ccw_idx, fi].Visible = false;
                                    }
                                    my_keyboard_key[p_func_no, p_cw_ccw_idx].Visible = false;
                                    my_keyboard_key_clr[p_func_no, p_cw_ccw_idx].Visible = false;
                                    //my_joypad_x[p_func_no, p_cw_ccw_idx].Visible = false;
                                    //my_joypad_y[p_func_no, p_cw_ccw_idx].Visible = false;
                                    break;
                                case Constants.SET_TYPE_KEYBOARD_KEY:
                                    my_lbl_title[p_func_no, p_cw_ccw_idx].Visible = false;
                                    //my_lbl_title[p_func_no, p_cw_ccw_idx].Text = RevOmate.Properties.Resources.KEYBOARD_INPUTKEY_TEXT;
                                    //my_lbl_title[p_func_no, p_cw_ccw_idx].Visible = p_visible;
                                    my_mouse_x[p_func_no, p_cw_ccw_idx].Visible = false;
                                    my_mouse_y[p_func_no, p_cw_ccw_idx].Visible = false;
                                    for (int fi = 0; fi < my_keyboard_modifier.GetLength(2); fi++)
                                    {
                                        my_keyboard_modifier[p_func_no, p_cw_ccw_idx, fi].Visible = p_visible;
                                        if ((my_set_data.keyboard_data[data_idx, Constants.KEYBOARD_DATA_MODIFIER_IDX] & keyboard_modifier_bit[fi]) != 0)
                                        {
                                            my_keyboard_modifier[p_func_no, p_cw_ccw_idx, fi].Checked = true;
                                        }
                                        else
                                        {
                                            my_keyboard_modifier[p_func_no, p_cw_ccw_idx, fi].Checked = false;
                                        }
                                    }
                                    my_keyboard_key[p_func_no, p_cw_ccw_idx].Visible = p_visible;
                                    if (my_set_data.keyboard_data[data_idx, Constants.KEYBOARD_DATA_KEY1_IDX] == 0)
                                    {
                                        my_keyboard_key[p_func_no, p_cw_ccw_idx].Text = RevOmate.Properties.Resources.KEYBOARD_SET_KEY_EMPTY;
                                    }
                                    else
                                    {
                                        my_keyboard_key[p_func_no, p_cw_ccw_idx].Text = const_Key_Code.Get_KeyCode_Name(my_set_data.keyboard_data[data_idx, Constants.KEYBOARD_DATA_KEY1_IDX], system_setting_info.Keyboard_Type);
                                        //for (int fj = 0; fj < VKtoUSBkey.Length; fj++)
                                        //{
                                        //    if (my_set_data.keyboard_data[p_idx, Constants.KEYBOARD_DATA_KEY1_IDX + fi] == VKtoUSBkey[fj])
                                        //    {
                                        //        my_keyboard_key[p_idx, fi].Text = ((Keys)fj).ToString();
                                        //        break;
                                        //    }
                                        //}
                                    }
                                    my_keyboard_key_clr[p_func_no, p_cw_ccw_idx].Visible = p_visible;
                                    //my_joypad_x[p_func_no, p_cw_ccw_idx].Visible = false;
                                    //my_joypad_y[p_func_no, p_cw_ccw_idx].Visible = false;
                                    break;
                                case Constants.SET_TYPE_MULTIMEDIA_PLAY:
                                case Constants.SET_TYPE_MULTIMEDIA_PAUSE:
                                case Constants.SET_TYPE_MULTIMEDIA_STOP:
                                case Constants.SET_TYPE_MULTIMEDIA_REC:
                                case Constants.SET_TYPE_MULTIMEDIA_FORWORD:
                                case Constants.SET_TYPE_MULTIMEDIA_REWIND:
                                case Constants.SET_TYPE_MULTIMEDIA_NEXT:
                                case Constants.SET_TYPE_MULTIMEDIA_PREVIOUS:
                                case Constants.SET_TYPE_MULTIMEDIA_MUTE:
                                case Constants.SET_TYPE_MULTIMEDIA_VOLUMEUP:
                                case Constants.SET_TYPE_MULTIMEDIA_VOLUMEDOWN:
                                    my_lbl_title[p_func_no, p_cw_ccw_idx].Visible = false;
                                    my_mouse_x[p_func_no, p_cw_ccw_idx].Visible = false;
                                    my_mouse_y[p_func_no, p_cw_ccw_idx].Visible = false;
                                    for (int fi = 0; fi < my_keyboard_modifier.GetLength(2); fi++)
                                    {
                                        my_keyboard_modifier[p_func_no, p_cw_ccw_idx, fi].Visible = false;
                                    }
                                    my_keyboard_key[p_func_no, p_cw_ccw_idx].Visible = false;
                                    my_keyboard_key_clr[p_func_no, p_cw_ccw_idx].Visible = false;
                                    //my_joypad_x[p_func_no, p_cw_ccw_idx].Visible = false;
                                    //my_joypad_y[p_func_no, p_cw_ccw_idx].Visible = false;
                                    break;
                                case Constants.SET_TYPE_JOYPAD_XY:
                                case Constants.SET_TYPE_JOYPAD_ZRZ:
                                    my_lbl_title[p_func_no, p_cw_ccw_idx].Text = RevOmate.Properties.Resources.JOYPAD_MOVE_TEXT;
                                    my_lbl_title[p_func_no, p_cw_ccw_idx].Visible = p_visible;
                                    //my_mouse_x[p_func_no, p_cw_ccw_idx].Visible = false;
                                    //my_mouse_y[p_func_no, p_cw_ccw_idx].Visible = false;
                                    for (int fi = 0; fi < my_keyboard_modifier.GetLength(2); fi++)
                                    {
                                        my_keyboard_modifier[p_func_no, p_cw_ccw_idx, fi].Visible = false;
                                    }
                                    my_keyboard_key[p_func_no, p_cw_ccw_idx].Visible = false;
                                    my_keyboard_key_clr[p_func_no, p_cw_ccw_idx].Visible = false;
                                    my_joypad_x[p_func_no, p_cw_ccw_idx].Visible = p_visible;
                                    my_joypad_x[p_func_no, p_cw_ccw_idx].Minimum = Constants.JOYPAD_MOVE_MIN;
                                    my_joypad_x[p_func_no, p_cw_ccw_idx].Maximum = Constants.JOYPAD_MOVE_MAX;
                                    if (my_set_data.joypad_data[data_idx, Constants.JOYPAD_DATA_X_MOVE_IDX] > 0x7F)
                                    {
                                        tmp_sbyte = (sbyte)(my_set_data.joypad_data[data_idx, Constants.JOYPAD_DATA_X_MOVE_IDX] - 0x100);
                                    }
                                    else
                                    {
                                        tmp_sbyte = (sbyte)(my_set_data.joypad_data[data_idx, Constants.JOYPAD_DATA_X_MOVE_IDX]);
                                    }
                                    //tmp_sbyte = (sbyte)(my_set_data.joypad_data[p_idx, Constants.JOYPAD_DATA_X_MOVE_IDX]);
                                    tmp_dec = tmp_sbyte;
                                    my_joypad_x[p_func_no, p_cw_ccw_idx].Value = tmp_dec;
                                    my_joypad_y[p_func_no, p_cw_ccw_idx].Visible = p_visible;
                                    my_joypad_y[p_func_no, p_cw_ccw_idx].Minimum = Constants.JOYPAD_MOVE_MIN;
                                    my_joypad_y[p_func_no, p_cw_ccw_idx].Maximum = Constants.JOYPAD_MOVE_MAX;
                                    if (my_set_data.joypad_data[data_idx, Constants.JOYPAD_DATA_Y_MOVE_IDX] > 0x7F)
                                    {
                                        tmp_sbyte = (sbyte)(my_set_data.joypad_data[data_idx, Constants.JOYPAD_DATA_Y_MOVE_IDX] - 0x100);
                                    }
                                    else
                                    {
                                        tmp_sbyte = (sbyte)(my_set_data.joypad_data[data_idx, Constants.JOYPAD_DATA_Y_MOVE_IDX]);
                                    }
                                    //tmp_sbyte = (sbyte)(my_set_data.joypad_data[p_idx, Constants.JOYPAD_DATA_Y_MOVE_IDX]);
                                    tmp_dec = tmp_sbyte;
                                    my_joypad_y[p_func_no, p_cw_ccw_idx].Value = tmp_dec;
                                    break;
                                case Constants.SET_TYPE_JOYPAD_B01:
                                case Constants.SET_TYPE_JOYPAD_B02:
                                case Constants.SET_TYPE_JOYPAD_B03:
                                case Constants.SET_TYPE_JOYPAD_B04:
                                case Constants.SET_TYPE_JOYPAD_B05:
                                case Constants.SET_TYPE_JOYPAD_B06:
                                case Constants.SET_TYPE_JOYPAD_B07:
                                case Constants.SET_TYPE_JOYPAD_B08:
                                case Constants.SET_TYPE_JOYPAD_B09:
                                case Constants.SET_TYPE_JOYPAD_B10:
                                case Constants.SET_TYPE_JOYPAD_B11:
                                case Constants.SET_TYPE_JOYPAD_B12:
                                case Constants.SET_TYPE_JOYPAD_B13:
                                    my_lbl_title[p_func_no, p_cw_ccw_idx].Visible = false;
                                    //my_mouse_x[func_no, p_cw_ccw_idx].Visible = false;
                                    //my_mouse_y[func_no, p_cw_ccw_idx].Visible = false;
                                    for (int fi = 0; fi < my_keyboard_modifier.GetLength(2); fi++)
                                    {
                                        my_keyboard_modifier[p_func_no, p_cw_ccw_idx, fi].Visible = false;
                                    }
                                    my_keyboard_key[p_func_no, p_cw_ccw_idx].Visible = false;
                                    my_keyboard_key_clr[p_func_no, p_cw_ccw_idx].Visible = false;
                                    my_joypad_x[p_func_no, p_cw_ccw_idx].Visible = false;
                                    my_joypad_y[p_func_no, p_cw_ccw_idx].Visible = false;
                                    break;
                                case Constants.SET_TYPE_JOYPAD_HSW_NORTH:
                                case Constants.SET_TYPE_JOYPAD_HSW_EAST:
                                case Constants.SET_TYPE_JOYPAD_HSW_SOUTH:
                                case Constants.SET_TYPE_JOYPAD_HSW_WEST:
                                    my_lbl_title[p_func_no, p_cw_ccw_idx].Visible = false;
                                    //my_mouse_x[func_no, p_cw_ccw_idx].Visible = false;
                                    //my_mouse_y[func_no, p_cw_ccw_idx].Visible = false;
                                    for (int fi = 0; fi < my_keyboard_modifier.GetLength(2); fi++)
                                    {
                                        my_keyboard_modifier[p_func_no, p_cw_ccw_idx, fi].Visible = false;
                                    }
                                    my_keyboard_key[p_func_no, p_cw_ccw_idx].Visible = false;
                                    my_keyboard_key_clr[p_func_no, p_cw_ccw_idx].Visible = false;
                                    my_joypad_x[p_func_no, p_cw_ccw_idx].Visible = false;
                                    my_joypad_y[p_func_no, p_cw_ccw_idx].Visible = false;
                                    break;
                                case Constants.SET_TYPE_NUMBER_UP:
                                case Constants.SET_TYPE_NUMBER_DOWN:
                                    my_lbl_title[p_func_no, p_cw_ccw_idx].Visible = false;
                                    my_mouse_x[p_func_no, p_cw_ccw_idx].Visible = false;
                                    my_mouse_y[p_func_no, p_cw_ccw_idx].Visible = false;
                                    for (int fi = 0; fi < my_keyboard_modifier.GetLength(2); fi++)
                                    {
                                        my_keyboard_modifier[p_func_no, p_cw_ccw_idx, fi].Visible = false;
                                    }
                                    my_keyboard_key[p_func_no, p_cw_ccw_idx].Visible = false;
                                    my_keyboard_key_clr[p_func_no, p_cw_ccw_idx].Visible = false;
                                    //my_joypad_x[p_func_no, p_cw_ccw_idx].Visible = false;
                                    //my_joypad_y[p_func_no, p_cw_ccw_idx].Visible = false;
                                    break;
                                case Constants.SET_TYPE_ENCODER_SCRIPT1:
                                case Constants.SET_TYPE_ENCODER_SCRIPT2:
                                case Constants.SET_TYPE_ENCODER_SCRIPT3:
                                    my_lbl_title[p_func_no, p_cw_ccw_idx].Visible = false;
                                    my_mouse_x[p_func_no, p_cw_ccw_idx].Visible = false;
                                    my_mouse_y[p_func_no, p_cw_ccw_idx].Visible = false;
                                    for (int fi = 0; fi < my_keyboard_modifier.GetLength(2); fi++)
                                    {
                                        my_keyboard_modifier[p_func_no, p_cw_ccw_idx, fi].Visible = false;
                                    }
                                    my_keyboard_key[p_func_no, p_cw_ccw_idx].Visible = false;
                                    my_keyboard_key_clr[p_func_no, p_cw_ccw_idx].Visible = false;
                                    //my_joypad_x[p_func_no, p_cw_ccw_idx].Visible = false;
                                    //my_joypad_y[p_func_no, p_cw_ccw_idx].Visible = false;
                                    break;
                                default:
                                    break;
                            }


                            // 感度
                            my_encoder_sensitivity[p_func_no, p_cw_ccw_idx].Value = my_set_data.sensitivity[data_idx];
                        }
                    }
                }
            }
            catch
            {
            }
        }
        // 指定モードの機能設定画面を表示
        private void my_function_setting_disp(int p_mode_no, bool p_visible)
        {
            try
            {
                if (0 <= p_mode_no && p_mode_no < Constants.MODE_NUM)
                {
                    for (int func_no = 0; func_no < Constants.FUNCTION_NUM; func_no++)
                    {
                        for (int cw_ccw_idx = 0; cw_ccw_idx < Constants.CW_CCW_NUM; cw_ccw_idx++)
                        {
                            my_function_setting_disp(p_mode_no, func_no, cw_ccw_idx, p_visible);
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void my_base_setting_disp(bool p_visible)
        {
            try
            {
                if (my_base_info.led_sleep == Constants.LED_SLEEP_ENABLED)
                {
                    chkbx_led_sleep.Checked = true;
                }
                else
                {
                    chkbx_led_sleep.Checked = false;
                }
                if (my_base_info.led_light_mode == Constants.LED_LIGHT_TYPE_MODE_OFF)
                {
                    chkbx_mode_led_off.Checked = true;
                    num_mode_led_off_time.Enabled = true;
                    num_mode_led_off_time.Value = my_base_info.led_off_time;
                }
                else
                {
                    chkbx_mode_led_off.Checked = false;
                    num_mode_led_off_time.Enabled = false;
                    num_mode_led_off_time.Value = my_base_info.led_off_time;
                }

                if (my_base_info.led_light_func == Constants.LED_LIGHT_TYPE_FUNC_ON)
                {
                    rbtn_func_led_on.Checked = true;
                }
                else if (my_base_info.led_light_func == Constants.LED_LIGHT_TYPE_FUNC_SLOW)
                {
                    rbtn_func_led_slow.Checked = true;
                }
                else if (my_base_info.led_light_func == Constants.LED_LIGHT_TYPE_FUNC_FLASH)
                {
                    rbtn_func_led_flash.Checked = true;
                }
                else
                {
                    rbtn_func_led_on.Checked = true;
                }
                if (my_base_info.encoder_typematic == Constants.ENCODER_TYPEMATIC_HIT)
                {
                    chkbx_encoder_typematic.Checked = true;
                }
                else
                {
                    chkbx_encoder_typematic.Checked = false;
                }

                my_base_setting_keyboard_settin_update(p_visible);
                //if (system_setting_info.Keyboard_Type == Constants.KEYBOARD_TYPE_JA)
                //{
                //    rbtn_keyboard_ja.Checked = true;
                //}
                //else
                //{
                //    rbtn_keyboard_us.Checked = true;
                //}
            }
            catch
            {
            }
        }
        private void my_base_setting_keyboard_settin_update(bool p_visible)
        {
            try
            {
                if (system_setting_info.Keyboard_Type == Constants.KEYBOARD_TYPE_JA)
                {
                    rbtn_keyboard_ja.Checked = true;
                }
                else
                {
                    rbtn_keyboard_us.Checked = true;
                }
            }
            catch
            {
            }
        }

        private void my_button_setting_disp(int p_mode_no, bool p_visible)
        {
            int sel_idx = 0;
            byte tmp_led_duty = 0;
            try
            {
                // ボタン１～１０
                for (int fi = 0; fi < my_button_macro_select.Length; fi++)
                {
                    //設定されている特殊機能番号またはマクロ番号を選択する
                    sel_idx = 0;
                    if (my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].sw_sp_func_no[fi] > 0)
                    {
                        sel_idx = my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].sw_sp_func_no[fi];
                        if (sel_idx < 0 || Constants.SP_FUNC_NUM < sel_idx)
                        {   // エラー（未設定）
                            sel_idx = 0;
                        }
                    }
                    else if (my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].sw_exe_script_no[fi] > 0)
                    {
                        sel_idx = my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].sw_exe_script_no[fi];
                        if (sel_idx < 0 || Constants.SCRIPT_NUM < sel_idx)
                        {   // エラー（未設定）
                            sel_idx = 0;
                        }
                        else
                        {
                            // 特殊機能の分、ずらす
                            sel_idx += Constants.SP_FUNC_NUM;
                        }
                    }
                    if (0 <= sel_idx && sel_idx < my_button_macro_select[fi].Items.Count)
                    {
                        my_button_macro_select[fi].SelectedIndex = sel_idx;
                    }
                    else
                    {
                        my_button_macro_select[fi].SelectedIndex = 0;
                    }
                }

                // エンコーダーボタン
                //設定されている特殊機能番号またはマクロ番号を選択する
                sel_idx = 0;
                if (my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].sw_sp_func_no[Constants.ENCODER_BUTTON_ID] > 0)
                {
                    sel_idx = my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].sw_sp_func_no[Constants.ENCODER_BUTTON_ID];
                    if (sel_idx < 0 || Constants.SP_FUNC_NUM < sel_idx)
                    {   // エラー（未設定）
                        sel_idx = 0;
                    }
                }
                else if (my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].sw_exe_script_no[Constants.ENCODER_BUTTON_ID] > 0)
                {
                    sel_idx = my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].sw_exe_script_no[Constants.ENCODER_BUTTON_ID];
                    if (sel_idx < 0 || Constants.SCRIPT_NUM < sel_idx)
                    {   // エラー（未設定）
                        sel_idx = 0;
                    }
                    else
                    {
                        // 特殊機能の分、ずらす
                        sel_idx += Constants.SP_FUNC_NUM;
                    }
                }
                if (0 <= sel_idx && sel_idx < cmbbx_encoder_button.Items.Count)
                {
                    cmbbx_encoder_button.SelectedIndex = sel_idx;
                }
                else
                {
                    cmbbx_encoder_button.SelectedIndex = 0;
                }


                // エンコーダーデフォルト機能
                sel_idx = my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].encoder_func_no;
                if (sel_idx < 0 || cmbbx_encoder_default.Items.Count <= sel_idx)
                {   // 選択肢異常
                    sel_idx = 0;
                }
                cmbbx_encoder_default.SelectedIndex = sel_idx;

                // LED設定
                for (int fi = 0; fi < Constants.LED_RGB_COLOR_NUM; fi++)
                {
                    tmp_led_duty = my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].LED_RGB_duty[fi];
                    if (my_LED_Duty_Max[fi] < tmp_led_duty)
                    {
                        tmp_led_duty = my_LED_Duty_Max[fi];
                    }
                    my_tb_LED_Duty_set_button[fi].Value = tmp_led_duty;
                }
                // LED輝度設定
                if (my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].LED_brightness_level < Constants.LED_BRIGHTNESS_LEVEL_SET_NUM)
                {
                    my_rbtn_LED_Brightness_Level_set_button[my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].LED_brightness_level].Checked = true;
                }
                else
                {
                    my_rbtn_LED_Brightness_Level_set_button[Constants.LED_BRIGHTNESS_LEVEL_NORMAL].Checked = true;
                }




                my_sw_func_name_disp(button_setting_mode_select_no, true);
#if false
                // SW機能名称設定
                for (int fi = 0; fi < my_sw_func_names.Length; fi++)
                {
                    //設定されている特殊機能番号またはマクロ番号を表示する
                    sel_idx = 0;
                    sel_name = RevOmate.Properties.Resources.MACRO_NAME_UNSETTING;
                    if (my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].sw_sp_func_no[fi] > 0)
                    {
                        sel_idx = my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].sw_sp_func_no[fi];
                        if (sel_idx <= 0 || Constants.SP_FUNC_NUM < sel_idx)
                        {   // エラー（未設定）
                            sel_idx = 0;
                            sel_name = RevOmate.Properties.Resources.MACRO_NAME_UNSETTING;
                        }
                        else
                        {
                            sel_name = sp_func_type_list[sel_idx-1];
                        }
                    }
                    else if (my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].sw_exe_script_no[fi] > 0)
                    {
                        sel_idx = my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].sw_exe_script_no[fi];
                        if (sel_idx <= 0 || Constants.SCRIPT_NUM < sel_idx)
                        {   // エラー（未設定）
                            sel_idx = 0;
                            sel_name = RevOmate.Properties.Resources.MACRO_NAME_UNSETTING;
                        }
                        else
                        {
                            sel_name = string.Format("{0}{1:000}:", RevOmate.Properties.Resources.MACRO_NAME_NO_MACRO, sel_idx);
                            if (my_script_info_datas.Script_Info_Datas[sel_idx-1].Name != "")
                            {
                                sel_name += my_script_info_datas.Script_Info_Datas[sel_idx-1].Name;
                            }
                            else
                            {
                                sel_name += RevOmate.Properties.Resources.MACRO_NAME_UNDEFINE;
                            }

                            // 特殊機能の分、ずらす
                            sel_idx += Constants.SP_FUNC_NUM;
                        }
                    }
                    if (0 <= sel_idx && sel_idx < my_button_macro_select[fi].Items.Count)
                    {
                        my_sw_func_names[fi].Text = sel_name;
                    }
                    else
                    {
                        my_sw_func_names[fi].Text = RevOmate.Properties.Resources.MACRO_NAME_UNSETTING;
                    }
                }
                // エンコーダーボタン
                //設定されている特殊機能番号またはマクロ番号を表示する
                sel_idx = 0;
                sel_name = RevOmate.Properties.Resources.MACRO_NAME_UNSETTING;
                if (my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].sw_sp_func_no[Constants.ENCODER_BUTTON_ID] > 0)
                {
                    sel_idx = my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].sw_sp_func_no[Constants.ENCODER_BUTTON_ID];
                    if (sel_idx <= 0 || Constants.SP_FUNC_NUM < sel_idx)
                    {   // エラー（未設定）
                        sel_idx = 0;
                        sel_name = RevOmate.Properties.Resources.MACRO_NAME_UNSETTING;
                    }
                    else
                    {
                        sel_name = sp_func_type_list[sel_idx - 1];
                    }
                }
                else if (my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].sw_exe_script_no[Constants.ENCODER_BUTTON_ID] > 0)
                {
                    sel_idx = my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].sw_exe_script_no[Constants.ENCODER_BUTTON_ID];
                    if (sel_idx <= 0 || Constants.SCRIPT_NUM < sel_idx)
                    {   // エラー（未設定）
                        sel_idx = 0;
                        sel_name = RevOmate.Properties.Resources.MACRO_NAME_UNSETTING;
                    }
                    else
                    {
                        sel_name = string.Format("{0}{1:000}:", RevOmate.Properties.Resources.MACRO_NAME_NO_MACRO, sel_idx);
                        if (my_script_info_datas.Script_Info_Datas[sel_idx - 1].Name != "")
                        {
                            sel_name += my_script_info_datas.Script_Info_Datas[sel_idx - 1].Name;
                        }
                        else
                        {
                            sel_name += RevOmate.Properties.Resources.MACRO_NAME_UNDEFINE;
                        }

                        // 特殊機能の分、ずらす
                        sel_idx += Constants.SP_FUNC_NUM;
                    }
                }
                if (0 <= sel_idx && sel_idx < cmbbx_encoder_button.Items.Count)
                {
                    lbl_Dial_func_name.Text = sel_name;
                }
                else
                {
                    lbl_Dial_func_name.Text = RevOmate.Properties.Resources.MACRO_NAME_UNSETTING;
                }
#endif

            }
            catch
            {
            }
        }

        private void my_sw_func_name_disp(int p_mode_no, bool p_visible)
        {
            string sel_name = "";
            try
            {
                FuncSetting fs = new FuncSetting(this, button_setting_mode_select_no, 0, false, my_app_sw_datas.mode[p_mode_no].app_data[0].select_data, my_app_sw_datas.mode[p_mode_no].app_data[0].data, sp_func_type_list, sp_func_type_no_list, my_script_info_datas, my_base_mode_infos, my_func_datas, my_sw_func_datas);
                // SW機能名称設定
                for (int fi = 0; fi < my_sw_func_names.Length; fi++)
                {
                    sel_name = fs.my_Get_Setting_Name(my_app_sw_datas.mode[p_mode_no].app_data[fi].select_data[Constants.APP_SW_DATA_SELECT_DATA_CATEGORY1_IDX], my_app_sw_datas.mode[p_mode_no].app_data[fi].select_data[Constants.APP_SW_DATA_SELECT_DATA_CATEGORY2_IDX], my_app_sw_datas.mode[p_mode_no].app_data[fi].select_data[Constants.APP_SW_DATA_SELECT_DATA_FUNC_LIST_IDX], my_sw_func_datas.sw_func_datas[p_mode_no].sw_func_data[fi].sw_data);
                    my_sw_func_names[fi].Text = sel_name;
                }
#if false
                // SW機能名称設定
                for (int fi = 0; fi < my_sw_func_names.Length; fi++)
                {
                    //設定されている特殊機能番号またはマクロ番号を表示する
                    sel_idx = 0;
                    sel_name = RevOmate.Properties.Resources.MACRO_NAME_UNSETTING;
                    if (my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].sw_sp_func_no[fi] > 0)
                    {
                        sel_idx = my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].sw_sp_func_no[fi];
                        if (sel_idx <= 0 || Constants.SP_FUNC_NUM < sel_idx)
                        {   // エラー（未設定）
                            sel_idx = 0;
                            sel_name = RevOmate.Properties.Resources.MACRO_NAME_UNSETTING;
                        }
                        else
                        {
                            sel_name = sp_func_type_list[sel_idx - 1];
                        }
                    }
                    else if (my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].sw_exe_script_no[fi] > 0)
                    {
                        sel_idx = my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].sw_exe_script_no[fi];
                        if (sel_idx <= 0 || Constants.SCRIPT_NUM < sel_idx)
                        {   // エラー（未設定）
                            sel_idx = 0;
                            sel_name = RevOmate.Properties.Resources.MACRO_NAME_UNSETTING;
                        }
                        else
                        {
                            sel_name = string.Format("{0}{1:000}:", RevOmate.Properties.Resources.MACRO_NAME_NO_MACRO, sel_idx);
                            if (my_script_info_datas.Script_Info_Datas[sel_idx - 1].Name != "")
                            {
                                sel_name += my_script_info_datas.Script_Info_Datas[sel_idx - 1].Name;
                            }
                            else
                            {
                                sel_name += RevOmate.Properties.Resources.MACRO_NAME_UNDEFINE;
                            }

                            // 特殊機能の分、ずらす
                            sel_idx += Constants.SP_FUNC_NUM;
                        }
                    }
                    if (0 <= sel_idx && sel_idx < my_button_macro_select[fi].Items.Count)
                    {
                        my_sw_func_names[fi].Text = sel_name;
                    }
                    else
                    {
                        my_sw_func_names[fi].Text = RevOmate.Properties.Resources.MACRO_NAME_UNSETTING;
                    }
                }
                // エンコーダーボタン
                //設定されている特殊機能番号またはマクロ番号を表示する
                sel_idx = 0;
                sel_name = RevOmate.Properties.Resources.MACRO_NAME_UNSETTING;
                if (my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].sw_sp_func_no[Constants.ENCODER_BUTTON_ID] > 0)
                {
                    sel_idx = my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].sw_sp_func_no[Constants.ENCODER_BUTTON_ID];
                    if (sel_idx <= 0 || Constants.SP_FUNC_NUM < sel_idx)
                    {   // エラー（未設定）
                        sel_idx = 0;
                        sel_name = RevOmate.Properties.Resources.MACRO_NAME_UNSETTING;
                    }
                    else
                    {
                        sel_name = sp_func_type_list[sel_idx - 1];
                    }
                }
                else if (my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].sw_exe_script_no[Constants.ENCODER_BUTTON_ID] > 0)
                {
                    sel_idx = my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].sw_exe_script_no[Constants.ENCODER_BUTTON_ID];
                    if (sel_idx <= 0 || Constants.SCRIPT_NUM < sel_idx)
                    {   // エラー（未設定）
                        sel_idx = 0;
                        sel_name = RevOmate.Properties.Resources.MACRO_NAME_UNSETTING;
                    }
                    else
                    {
                        sel_name = string.Format("{0}{1:000}:", RevOmate.Properties.Resources.MACRO_NAME_NO_MACRO, sel_idx);
                        if (my_script_info_datas.Script_Info_Datas[sel_idx - 1].Name != "")
                        {
                            sel_name += my_script_info_datas.Script_Info_Datas[sel_idx - 1].Name;
                        }
                        else
                        {
                            sel_name += RevOmate.Properties.Resources.MACRO_NAME_UNDEFINE;
                        }

                        // 特殊機能の分、ずらす
                        sel_idx += Constants.SP_FUNC_NUM;
                    }
                }
                if (0 <= sel_idx && sel_idx < cmbbx_encoder_button.Items.Count)
                {
                    lbl_Dial_func_name.Text = sel_name;
                }
                else
                {
                    lbl_Dial_func_name.Text = RevOmate.Properties.Resources.MACRO_NAME_UNSETTING;
                }
#endif



                // 機能設定画面　機能１～４名称更新
                for (int fi = 0; fi < my_encoder_func_names.Length; fi++)
                {
                    if (my_func_datas.func_datas[button_setting_mode_select_no].func_data[fi].func_name != "")
                    {
                        my_encoder_func_names[fi].Text = my_func_datas.func_datas[button_setting_mode_select_no].func_data[fi].func_name;
                    }
                    else
                    {
                        my_encoder_func_names[fi].Text = RevOmate.Properties.Resources.FUNCTION_NAME_UNDEFINE;
                    }

                    // デフォルト機能を太字にする
                    if (my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].encoder_func_no == fi)
                    {   // 頭に＊を付け、太字＆斜体
                        my_encoder_func_names[fi].Text = "*" + my_encoder_func_names[fi].Text;
                        my_encoder_func_names[fi].Font = new Font(my_encoder_func_names[fi].Font, FontStyle.Bold | FontStyle.Italic);
                    }
                    else
                    {   // 通常文字
                        my_encoder_func_names[fi].Font = new Font(my_encoder_func_names[fi].Font, FontStyle.Regular);
                    }
                }

            }
            catch
            {
            }
        }

        private void my_encoder_script_setting_disp(int p_no, bool p_visible)
        {
            int sel_idx = 0;
            try
            {
                if (0 <= p_no && p_no < Constants.ENCODER_SCRIPT_NUM)
                {
                    int script_name_idx = dgv_encoder_script.Columns["dgv_script_name"].Index;
                    int rec_num = my_encoder_script_datas.encoder_script_datas[p_no].rec_num;
                    for (int fi = 0; fi < Constants.ENCODER_SCRIPT_SCRIPTSET_MAX_NUM; fi++)
                    {
                        //設定されているマクロ番号を選択する
                        sel_idx = my_encoder_script_datas.encoder_script_datas[p_no].script_no[fi];
                        string select_script_name = "";

                        if (fi >= rec_num)
                        {   // 記録数超え
                            sel_idx = 0; // 未設定
                            select_script_name = string.Format("{0}", RevOmate.Properties.Resources.MACRO_NAME_UNSETTING);
                        }
                        else if (sel_idx <= 0 || Constants.SCRIPT_USER_USE_NUM < sel_idx)
                        {   // マクロ未設定
                            sel_idx = 0; // 未設定
                            select_script_name = string.Format("{0}", RevOmate.Properties.Resources.MACRO_NAME_UNSETTING);
                        }
                        else
                        {
                            if (my_script_info_datas.Script_Info_Datas[sel_idx - 1].Name != "")
                            {   // マクロ名称設定済み
                                select_script_name = string.Format("{0}{1:000}:{2}", RevOmate.Properties.Resources.MACRO_NAME_NO_MACRO, sel_idx, my_script_info_datas.Script_Info_Datas[sel_idx - 1].Name);
                            }
                            else
                            {   // マクロ名称未設定
                                select_script_name = string.Format("{0}{1:000}:{2}", RevOmate.Properties.Resources.MACRO_NAME_NO_MACRO, sel_idx, RevOmate.Properties.Resources.MACRO_NAME_UNDEFINE);
                            }
                        }
                        dgv_encoder_script.Rows[fi].Cells[script_name_idx].Value = select_script_name;
                    }

                    // Loop 設定
                    if (my_encoder_script_datas.encoder_script_datas[p_no].loop_flag == Constants.ENCODER_SCRIPT_LOOP_SET_LOOP)
                    {
                        chkbx_encoder_script_loop.Checked = true;
                    }
                    else
                    {
                        chkbx_encoder_script_loop.Checked = false;
                    }
                }
            }
            catch
            {
            }
        }
        // 画面表示内容を取得して設定データバッファに格納する
        private void my_function_setting_get_by_disp(int p_mode_no)
        {
            int move_val = 0;
            try
            {
                if (0 <= p_mode_no && p_mode_no < Constants.MODE_NUM)
                {
                    for (int func_no = 0; func_no < Constants.FUNCTION_NUM; func_no++)
                    {

                        // 機能名称
                        my_func_datas.func_datas[p_mode_no].func_data[func_no].func_name = my_txtbx_func_name[func_no].Text;
                        // LED
                        for (int led_idx = 0; led_idx < Constants.LED_RGB_COLOR_NUM; led_idx++)
                        {
                            my_func_datas.func_datas[p_mode_no].func_data[func_no].LED_RGB_duty[led_idx] = (byte)(my_tb_LED_Duty_set_func[func_no, led_idx].Value & 0xFF);
                        }
                        // LED輝度設定
                        for (int fi = 0; fi <= my_rbtn_LED_Brightness_Level_set_func.GetUpperBound(1); fi++)
                        {
                            if (my_rbtn_LED_Brightness_Level_set_func[func_no, fi].Checked == true)
                            {
                                my_func_datas.func_datas[p_mode_no].func_data[func_no].LED_brightness_level = (byte)(fi & 0xFF);
                                break;
                            }
                        }

                        for (int cw_ccw_idx = 0; cw_ccw_idx < Constants.CW_CCW_NUM; cw_ccw_idx++)
                        {
                            int data_idx = (p_mode_no * Constants.FUNCTION_NUM * Constants.CW_CCW_NUM) + (func_no * Constants.CW_CCW_NUM) + cw_ccw_idx;
                            byte set_type_no = (byte)(my_get_disp_set_type(my_cmbbx_set_type[func_no, cw_ccw_idx].SelectedIndex) & 0xFF);
                            switch (set_type_no)
                            {
                                case Constants.SET_TYPE_MOUSE_LCLICK:
                                case Constants.SET_TYPE_MOUSE_RCLICK:
                                case Constants.SET_TYPE_MOUSE_WHCLICK:
                                case Constants.SET_TYPE_MOUSE_B4CLICK:
                                case Constants.SET_TYPE_MOUSE_B5CLICK:
                                case Constants.SET_TYPE_MOUSE_DCLICK:
                                    my_set_data.set_type[data_idx] = set_type_no;
                                    for (int fj = 0; fj < Constants.MOUSE_DATA_LEN; fj++)
                                    {
                                        my_set_data.mouse_data[data_idx, fj] = 0;
                                    }
                                    switch (set_type_no)
                                    {
                                        case Constants.SET_TYPE_MOUSE_LCLICK:
                                            my_set_data.mouse_data[data_idx, Constants.MOUSE_DATA_CLICK_IDX] = Constants.MOUSE_DATA_LEFT_CLICK;
                                            break;
                                        case Constants.SET_TYPE_MOUSE_RCLICK:
                                            my_set_data.mouse_data[data_idx, Constants.MOUSE_DATA_CLICK_IDX] = Constants.MOUSE_DATA_RIGHT_CLICK;
                                            break;
                                        case Constants.SET_TYPE_MOUSE_WHCLICK:
                                            my_set_data.mouse_data[data_idx, Constants.MOUSE_DATA_CLICK_IDX] = Constants.MOUSE_DATA_WHEEL_CLICK;
                                            break;
                                        case Constants.SET_TYPE_MOUSE_B4CLICK:
                                            my_set_data.mouse_data[data_idx, Constants.MOUSE_DATA_CLICK_IDX] = Constants.MOUSE_DATA_BUTTON4_CLICK;
                                            break;
                                        case Constants.SET_TYPE_MOUSE_B5CLICK:
                                            my_set_data.mouse_data[data_idx, Constants.MOUSE_DATA_CLICK_IDX] = Constants.MOUSE_DATA_BUTTON5_CLICK;
                                            break;
                                        case Constants.SET_TYPE_MOUSE_DCLICK:
                                            my_set_data.mouse_data[data_idx, Constants.MOUSE_DATA_CLICK_IDX] = Constants.MOUSE_DATA_LEFT_CLICK;
                                            break;
                                    }
                                    break;
                                case Constants.SET_TYPE_MOUSE_MOVE:
                                    my_set_data.set_type[data_idx] = set_type_no;
                                    for (int fj = 0; fj < Constants.MOUSE_DATA_LEN; fj++)
                                    {
                                        my_set_data.mouse_data[data_idx, fj] = 0;
                                    }
                                    move_val = 0;
                                    try
                                    {
                                        move_val = int.Parse(my_mouse_x[func_no, cw_ccw_idx].Value.ToString());
                                    }
                                    catch
                                    {
                                    }
                                    my_set_data.mouse_data[data_idx, Constants.MOUSE_DATA_X_MOVE_IDX] = (byte)(move_val & 0xFF);
                                    move_val = 0;
                                    try
                                    {
                                        move_val = int.Parse(my_mouse_y[func_no, cw_ccw_idx].Value.ToString());
                                    }
                                    catch
                                    {
                                    }
                                    my_set_data.mouse_data[data_idx, Constants.MOUSE_DATA_Y_MOVE_IDX] = (byte)(move_val & 0xFF);
                                    break;
                                case Constants.SET_TYPE_MOUSE_WHSCROLL:
                                    my_set_data.set_type[data_idx] = set_type_no;
                                    for (int fj = 0; fj < Constants.MOUSE_DATA_LEN; fj++)
                                    {
                                        my_set_data.mouse_data[data_idx, fj] = 0;
                                    }
                                    move_val = 0;
                                    try
                                    {
                                        move_val = int.Parse(my_mouse_x[func_no, cw_ccw_idx].Value.ToString());
                                    }
                                    catch
                                    {
                                    }
                                    my_set_data.mouse_data[data_idx, Constants.MOUSE_DATA_WHEEL_IDX] = (byte)(move_val & 0xFF);
                                    break;
                                case Constants.SET_TYPE_KEYBOARD_KEY:
                                    my_set_data.set_type[data_idx] = set_type_no;
                                    // モディファイビットをクリア
                                    for (int fj = 0; fj < Constants.KEYBOARD_DATA_LEN; fj++)
                                    {
                                        if (fj == Constants.KEYBOARD_DATA_MODIFIER_IDX)
                                        {
                                            my_set_data.keyboard_data[data_idx, fj] = 0;
                                            break;
                                        }
                                    }
                                    // モディファイがセットされているかチェックして、ビットセット
                                    for (int fj = 0; fj < my_keyboard_modifier.GetLength(2); fj++)
                                    {
                                        if (my_keyboard_modifier[func_no, cw_ccw_idx, fj].Checked == true)
                                        {
                                            my_set_data.keyboard_data[data_idx, Constants.KEYBOARD_DATA_MODIFIER_IDX] |= keyboard_modifier_bit[fj];
                                        }
                                    }
                                    break;
                                case Constants.SET_TYPE_MULTIMEDIA_PLAY:
                                case Constants.SET_TYPE_MULTIMEDIA_PAUSE:
                                case Constants.SET_TYPE_MULTIMEDIA_STOP:
                                case Constants.SET_TYPE_MULTIMEDIA_REC:
                                case Constants.SET_TYPE_MULTIMEDIA_FORWORD:
                                case Constants.SET_TYPE_MULTIMEDIA_REWIND:
                                case Constants.SET_TYPE_MULTIMEDIA_NEXT:
                                case Constants.SET_TYPE_MULTIMEDIA_PREVIOUS:
                                case Constants.SET_TYPE_MULTIMEDIA_MUTE:
                                case Constants.SET_TYPE_MULTIMEDIA_VOLUMEUP:
                                case Constants.SET_TYPE_MULTIMEDIA_VOLUMEDOWN:
                                    my_set_data.set_type[data_idx] = set_type_no;
                                    for (int fj = 0; fj < Constants.MULTIMEDIA_DATA_LEN; fj++)
                                    {
                                        my_set_data.multimedia_data[data_idx, fj] = 0;
                                    }
                                    switch (set_type_no)
                                    {
                                        case Constants.SET_TYPE_MULTIMEDIA_PLAY:
                                            my_set_data.multimedia_data[data_idx, Constants.MULTIMEDIA_DATA_VAL1_IDX] = Constants.MULTIMEDIA_DATA_PLAY;
                                            break;
                                        case Constants.SET_TYPE_MULTIMEDIA_PAUSE:
                                            my_set_data.multimedia_data[data_idx, Constants.MULTIMEDIA_DATA_VAL1_IDX] = Constants.MULTIMEDIA_DATA_PAUSE;
                                            break;
                                        case Constants.SET_TYPE_MULTIMEDIA_STOP:
                                            my_set_data.multimedia_data[data_idx, Constants.MULTIMEDIA_DATA_VAL1_IDX] = Constants.MULTIMEDIA_DATA_STOP;
                                            break;
                                        case Constants.SET_TYPE_MULTIMEDIA_REC:
                                            my_set_data.multimedia_data[data_idx, Constants.MULTIMEDIA_DATA_VAL1_IDX] = Constants.MULTIMEDIA_DATA_REC;
                                            break;
                                        case Constants.SET_TYPE_MULTIMEDIA_FORWORD:
                                            my_set_data.multimedia_data[data_idx, Constants.MULTIMEDIA_DATA_VAL1_IDX] = Constants.MULTIMEDIA_DATA_FORWARD;
                                            break;
                                        case Constants.SET_TYPE_MULTIMEDIA_REWIND:
                                            my_set_data.multimedia_data[data_idx, Constants.MULTIMEDIA_DATA_VAL1_IDX] = Constants.MULTIMEDIA_DATA_REWIND;
                                            break;
                                        case Constants.SET_TYPE_MULTIMEDIA_NEXT:
                                            my_set_data.multimedia_data[data_idx, Constants.MULTIMEDIA_DATA_VAL1_IDX] = Constants.MULTIMEDIA_DATA_NEXT;
                                            break;
                                        case Constants.SET_TYPE_MULTIMEDIA_PREVIOUS:
                                            my_set_data.multimedia_data[data_idx, Constants.MULTIMEDIA_DATA_VAL1_IDX] = Constants.MULTIMEDIA_DATA_PREVIOUS;
                                            break;
                                        case Constants.SET_TYPE_MULTIMEDIA_MUTE:
                                            my_set_data.multimedia_data[data_idx, Constants.MULTIMEDIA_DATA_VAL2_IDX] = Constants.MULTIMEDIA_DATA_MUTE;
                                            break;
                                        case Constants.SET_TYPE_MULTIMEDIA_VOLUMEUP:
                                            my_set_data.multimedia_data[data_idx, Constants.MULTIMEDIA_DATA_VAL2_IDX] = Constants.MULTIMEDIA_DATA_VOL_UP;
                                            break;
                                        case Constants.SET_TYPE_MULTIMEDIA_VOLUMEDOWN:
                                            my_set_data.multimedia_data[data_idx, Constants.MULTIMEDIA_DATA_VAL2_IDX] = Constants.MULTIMEDIA_DATA_VOL_DOWN;
                                            break;
                                    }
                                    break;
                                case Constants.SET_TYPE_JOYPAD_XY:
                                case Constants.SET_TYPE_JOYPAD_ZRZ:
                                    my_set_data.set_type[data_idx] = set_type_no;
                                    for (int fj = 0; fj < Constants.JOYPAD_DATA_LEN; fj++)
                                    {
                                        my_set_data.joypad_data[data_idx, fj] = 0;
                                    }
                                    my_set_data.joypad_data[data_idx, Constants.JOYPAD_DATA_HAT_SW_IDX] = Constants.HAT_SWITCH_NULL;
                                    move_val = 0;
                                    try
                                    {
                                        move_val = int.Parse(my_joypad_x[func_no, cw_ccw_idx].Value.ToString());
                                    }
                                    catch
                                    {
                                    }
                                    my_set_data.joypad_data[data_idx, Constants.JOYPAD_DATA_X_MOVE_IDX] = (byte)(move_val & 0xFF);
                                    move_val = 0;
                                    try
                                    {
                                        move_val = int.Parse(my_joypad_y[func_no, cw_ccw_idx].Value.ToString());
                                    }
                                    catch
                                    {
                                    }
                                    my_set_data.joypad_data[data_idx, Constants.JOYPAD_DATA_Y_MOVE_IDX] = (byte)(move_val & 0xFF);
                                    break;
                                case Constants.SET_TYPE_JOYPAD_B01:
                                case Constants.SET_TYPE_JOYPAD_B02:
                                case Constants.SET_TYPE_JOYPAD_B03:
                                case Constants.SET_TYPE_JOYPAD_B04:
                                case Constants.SET_TYPE_JOYPAD_B05:
                                case Constants.SET_TYPE_JOYPAD_B06:
                                case Constants.SET_TYPE_JOYPAD_B07:
                                case Constants.SET_TYPE_JOYPAD_B08:
                                case Constants.SET_TYPE_JOYPAD_B09:
                                case Constants.SET_TYPE_JOYPAD_B10:
                                case Constants.SET_TYPE_JOYPAD_B11:
                                case Constants.SET_TYPE_JOYPAD_B12:
                                case Constants.SET_TYPE_JOYPAD_B13:
                                    my_set_data.set_type[data_idx] = set_type_no;
                                    for (int fj = 0; fj < Constants.JOYPAD_DATA_LEN; fj++)
                                    {
                                        my_set_data.joypad_data[data_idx, fj] = 0;
                                    }
                                    my_set_data.joypad_data[data_idx, Constants.JOYPAD_DATA_HAT_SW_IDX] = Constants.HAT_SWITCH_NULL;
                                    int tmp_byte_pos = (set_type_no - Constants.SET_TYPE_JOYPAD_B01) / 8;
                                    int tmp_bit_pos = (set_type_no - Constants.SET_TYPE_JOYPAD_B01) % 8;
                                    int tmp_bit_ope = my_set_data.joypad_data[data_idx, Constants.JOYPAD_DATA_BUTTON1_IDX + tmp_byte_pos];
                                    tmp_bit_ope |= (0x01 << tmp_bit_pos);
                                    my_set_data.joypad_data[data_idx, Constants.JOYPAD_DATA_BUTTON1_IDX + tmp_byte_pos] = (byte)(tmp_bit_ope & 0xFF);
                                    break;
                                case Constants.SET_TYPE_JOYPAD_HSW_NORTH:
                                case Constants.SET_TYPE_JOYPAD_HSW_EAST:
                                case Constants.SET_TYPE_JOYPAD_HSW_SOUTH:
                                case Constants.SET_TYPE_JOYPAD_HSW_WEST:
                                    my_set_data.set_type[data_idx] = set_type_no;
                                    for (int fj = 0; fj < Constants.JOYPAD_DATA_LEN; fj++)
                                    {
                                        my_set_data.joypad_data[data_idx, fj] = 0;
                                    }
                                    my_set_data.joypad_data[data_idx, Constants.JOYPAD_DATA_HAT_SW_IDX] = Constants.HAT_SWITCH_NULL;
                                    switch (my_set_data.set_type[data_idx])
                                    {
                                        case Constants.SET_TYPE_JOYPAD_HSW_NORTH:
                                            my_set_data.joypad_data[data_idx, Constants.JOYPAD_DATA_HAT_SW_IDX] = Constants.HAT_SWITCH_NORTH;
                                            break;
                                        case Constants.SET_TYPE_JOYPAD_HSW_EAST:
                                            my_set_data.joypad_data[data_idx, Constants.JOYPAD_DATA_HAT_SW_IDX] = Constants.HAT_SWITCH_EAST;
                                            break;
                                        case Constants.SET_TYPE_JOYPAD_HSW_SOUTH:
                                            my_set_data.joypad_data[data_idx, Constants.JOYPAD_DATA_HAT_SW_IDX] = Constants.HAT_SWITCH_SOUTH;
                                            break;
                                        case Constants.SET_TYPE_JOYPAD_HSW_WEST:
                                            my_set_data.joypad_data[data_idx, Constants.JOYPAD_DATA_HAT_SW_IDX] = Constants.HAT_SWITCH_WEST;
                                            break;
                                    }
                                    break;
                                case Constants.SET_TYPE_NUMBER_UP:
                                case Constants.SET_TYPE_NUMBER_DOWN:
                                    my_set_data.set_type[data_idx] = set_type_no;
                                    break;
                                case Constants.SET_TYPE_ENCODER_SCRIPT1:
                                case Constants.SET_TYPE_ENCODER_SCRIPT2:
                                case Constants.SET_TYPE_ENCODER_SCRIPT3:
                                    my_set_data.set_type[data_idx] = set_type_no;
                                    break;
                                case Constants.SET_TYPE_NONE:
                                default:
                                    my_set_data.set_type[data_idx] = (byte)(Constants.SET_TYPE_NONE & 0xFF);
                                    break;
                            }


                            // 感度
                            my_set_data.sensitivity[data_idx] = (byte)((int)(my_encoder_sensitivity[func_no, cw_ccw_idx].Value) & 0xFF);
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void my_button_setting_get_by_disp(int p_mode_no)
        {
            int sel_idx = 0;
            byte tmp_led_duty = 0;
            try
            {
                // ボタン１～１０
                for (int fi = 0; fi < my_button_macro_select.Length; fi++)
                {
                    // 現在の選択を記憶
                    sel_idx = my_button_macro_select[fi].SelectedIndex;
                    // スクリプト？
                    if (sel_idx > Constants.SP_FUNC_NUM)
                    {   // スクリプト選択
                        sel_idx -= Constants.SP_FUNC_NUM;
                        if (sel_idx < 0 || Constants.SCRIPT_NUM < sel_idx)
                        {   // 選択肢異常
                            sel_idx = 0;
                        }
                        my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].sw_exe_script_no[fi] = (byte)(sel_idx & 0xFF);
                        my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].sw_sp_func_no[fi] = 0;
                    }
                    else
                    {   // 特殊機能選択
                        if (sel_idx < 0 || Constants.SP_FUNC_NUM < sel_idx)
                        {   // 選択肢異常
                            sel_idx = 0;
                        }
                        my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].sw_sp_func_no[fi] = (byte)(sel_idx & 0xFF);
                        my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].sw_exe_script_no[fi] = 0;
                    }
                }

                // エンコーダーボタン
                sel_idx = cmbbx_encoder_button.SelectedIndex;
                // スクリプト？
                if (sel_idx > Constants.SP_FUNC_NUM)
                {   // スクリプト選択
                    sel_idx -= Constants.SP_FUNC_NUM;
                    if (sel_idx < 0 || Constants.SCRIPT_NUM < sel_idx)
                    {   // 選択肢異常
                        sel_idx = 0;
                    }
                    my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].sw_exe_script_no[Constants.ENCODER_BUTTON_ID] = (byte)(sel_idx & 0xFF);
                    my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].sw_sp_func_no[Constants.ENCODER_BUTTON_ID] = 0;
                }
                else
                {   // 特殊機能選択
                    if (sel_idx < 0 || Constants.SP_FUNC_NUM < sel_idx)
                    {   // 選択肢異常
                        sel_idx = 0;
                    }
                    my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].sw_sp_func_no[Constants.ENCODER_BUTTON_ID] = (byte)(sel_idx & 0xFF);
                    my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].sw_exe_script_no[Constants.ENCODER_BUTTON_ID] = 0;
                }

                // エンコーダーデフォルト機能
                sel_idx = cmbbx_encoder_default.SelectedIndex;
                if (sel_idx < 0 || cmbbx_encoder_default.Items.Count <= sel_idx)
                {   // 選択肢異常
                    sel_idx = 0;
                }
                my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].encoder_func_no = (byte)(sel_idx & 0xFF);

                // LED設定
                for (int fi = 0; fi < Constants.LED_RGB_COLOR_NUM; fi++)
                {
                    tmp_led_duty = (byte)(my_tb_LED_Duty_set_button[fi].Value & 0xFF);
                    if (my_LED_Duty_Max[fi] < tmp_led_duty)
                    {
                        tmp_led_duty = my_LED_Duty_Max[fi];
                    }

                    my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].LED_RGB_duty[fi] = tmp_led_duty;
                }
                // LED輝度設定
                for (int fi = 0; fi < my_rbtn_LED_Brightness_Level_set_button.Length; fi++)
                {
                    if (my_rbtn_LED_Brightness_Level_set_button[fi].Checked == true)
                    {
                        my_base_mode_infos.base_mode_infos[button_setting_mode_select_no].LED_brightness_level = (byte)(fi & 0xFF);
                        break;
                    }
                }
            }
            catch
            {
            }
        }

        private void my_base_setting_get_by_disp()
        {
            try
            {

                if (chkbx_led_sleep.Checked == true)
                {
                    my_base_info.led_sleep = Constants.LED_SLEEP_ENABLED;
                }
                else
                {
                    my_base_info.led_sleep = Constants.LED_SLEEP_DISABLED;
                }

                if (chkbx_mode_led_off.Checked == true)
                {
                    my_base_info.led_light_mode = Constants.LED_LIGHT_TYPE_MODE_OFF;
                    my_base_info.led_off_time = (byte)((int)(num_mode_led_off_time.Value) & 0xFF);
                }
                else
                {
                    my_base_info.led_light_mode = Constants.LED_LIGHT_TYPE_MODE_ON;
                    my_base_info.led_off_time = (byte)((int)(num_mode_led_off_time.Value) & 0xFF);
                }

                if (rbtn_func_led_on.Checked == true)
                {
                    my_base_info.led_light_func = Constants.LED_LIGHT_TYPE_FUNC_ON;
                }
                else if (rbtn_func_led_slow.Checked == true)
                {
                    my_base_info.led_light_func = Constants.LED_LIGHT_TYPE_FUNC_SLOW;
                }
                else if (rbtn_func_led_flash.Checked == true)
                {
                    my_base_info.led_light_func = Constants.LED_LIGHT_TYPE_FUNC_FLASH;
                }
                else
                {
                    my_base_info.led_light_func = Constants.LED_LIGHT_TYPE_FUNC_ON;
                }

                if (chkbx_encoder_typematic.Checked == true)
                {
                    my_base_info.encoder_typematic = Constants.ENCODER_TYPEMATIC_HIT;
                }
                else
                {
                    my_base_info.encoder_typematic = Constants.ENCODER_TYPEMATIC_PRESS;
                }

                if (rbtn_keyboard_ja.Checked == true)
                {
                    system_setting_info.Keyboard_Type = Constants.KEYBOARD_TYPE_JA;
                }
                else
                {
                    system_setting_info.Keyboard_Type = Constants.KEYBOARD_TYPE_US;
                }
            }
            catch
            {
            }
        }
        // 画面表示内容を取得してエンコーダスクリプト設定データバッファに格納する
        private bool my_encoder_script_setting_get_by_disp(int p_no)
        {
            bool change_flag = false;
            try
            {
                if (0 <= p_no && p_no < Constants.ENCODER_SCRIPT_NUM)
                {
                    byte rec_count = 0;

                    int script_name_idx = dgv_encoder_script.Columns["dgv_script_name"].Index;

                    // 現在の設定の選択肢を記憶
                    int[] sel_data_idx = new int[my_encoder_script_datas.encoder_script_datas[p_no].script_no.Length];
                    for (int idx = 0; idx < my_encoder_script_datas.encoder_script_datas[p_no].script_no.Length; idx++)
                    {
                        DataGridViewComboBoxCell dgvcbc = (DataGridViewComboBoxCell)dgv_encoder_script.Rows[idx].Cells[script_name_idx];
                        sel_data_idx[idx] = dgvcbc.Items.IndexOf(dgvcbc.Value);
                    }

                    // 選択と選択の間に未選択がある場合は詰める
                    for (int idx = 0; idx < sel_data_idx.Length; idx++)
                    {
                        if (0 < sel_data_idx[idx] && sel_data_idx[idx] <= Constants.SCRIPT_NUM)
                        {
                            if (idx > rec_count)
                            {
                                sel_data_idx[rec_count] = sel_data_idx[idx];
                                sel_data_idx[idx] = 0;
                            }
                            rec_count++;
                        }
                    }

                    // 設定を設定バッファにセット
                    if (my_encoder_script_datas.encoder_script_datas[p_no].rec_num != rec_count)
                    {
                        my_encoder_script_datas.encoder_script_datas[p_no].rec_num = rec_count;
                        change_flag = true;
                    }
                    for (int idx = 0; idx < sel_data_idx.Length; idx++)
                    {
                        if (my_encoder_script_datas.encoder_script_datas[p_no].script_no[idx] != sel_data_idx[idx])
                        {
                            my_encoder_script_datas.encoder_script_datas[p_no].script_no[idx] = (byte)(sel_data_idx[idx] & 0xFF);
                            change_flag = true;
                        }
                    }

                    // 繰り返し設定
                    if (chkbx_encoder_script_loop.Checked == true)
                    {
                        if (my_encoder_script_datas.encoder_script_datas[p_no].loop_flag != Constants.ENCODER_SCRIPT_LOOP_SET_LOOP)
                        {
                            change_flag = true;
                        }
                        my_encoder_script_datas.encoder_script_datas[p_no].loop_flag = Constants.ENCODER_SCRIPT_LOOP_SET_LOOP;
                    }
                    else
                    {
                        if (my_encoder_script_datas.encoder_script_datas[p_no].loop_flag != Constants.ENCODER_SCRIPT_LOOP_SET_NONE)
                        {
                            change_flag = true;
                        }
                        my_encoder_script_datas.encoder_script_datas[p_no].loop_flag = Constants.ENCODER_SCRIPT_LOOP_SET_NONE;
                    }
                }
            }
            catch
            {
            }
            return change_flag;
        }

        private void rbx_button_input_type_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int index = int.Parse(((RadioButton)sender).Tag.ToString());

                int sw_no = index % Constants.BUTTON_NUM;
                int button_type = index / Constants.BUTTON_NUM;

                if (sw_no < Constants.BUTTON_NUM)
                {
                    if (button_type == 0)
                    {   // SW設定
                        my_button_macro_select[sw_no].Enabled = true;
                    }
                    else if (button_type == 1)
                    {   // パターン設定
                        my_button_macro_select[sw_no].Enabled = false;
                    }
                }
            }
            catch
            {
            }
        }

        private void lbl_MacroWrite_Icon_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                // マクロリスト変更後に、書き込みボタンを押すと値が変更前のままなので、カレントセルを変更して確定させる
                if (dgv_ScriptEditor.CurrentCell != null)
                {
                    int x = dgv_ScriptEditor.CurrentCellAddress.X;
                    int y = dgv_ScriptEditor.CurrentCellAddress.Y;
                    dgv_ScriptEditor.CurrentCell = dgv_ScriptEditor[0, y];
                }

                int target_idx = 0;    // index 0 固定
                target_idx = dgv_ScriptList.CurrentRow.Index;
                //if (target_idx < 0 || Constants.SCRIPT_NUM <= target_idx)
                //{
                //    return;
                //}

                if (0 <= target_idx && target_idx < Constants.SCRIPT_NUM)
                {   // 書込位置OK

                    // 書込確認
                    string rewrite_msg = string.Format(RevOmate.Properties.Resources.DEVICE_MACRO_WRITE_CONFIRM_MSG, target_idx + 1);
                    if (MessageBox.Show(rewrite_msg, RevOmate.Properties.Resources.APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
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
                }
                else
                {   // 書込位置エラー
                    MessageBox.Show(RevOmate.Properties.Resources.DEVICE_MACRO_WRITE_POS_ERR_MSG, RevOmate.Properties.Resources.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
            }
        }

        private void lbl_MacroRead_Icon_MouseClick(object sender, MouseEventArgs e)
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

        private void lbl_FileExport_Icon_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                //ファイル名作成
                string file_name = "backup_";
                DateTime dt = DateTime.Now;

                //日付追加
                file_name += string.Format("{0:0000}{1:00}{2:00}_{3:00}{4:00}{5:00}", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
                ScriptFile_Save_saveFileDialog.FileName = file_name;

                if (ScriptFile_Save_saveFileDialog.InitialDirectory == "")
                {
                    ScriptFile_Save_saveFileDialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                }
                ScriptFile_Save_saveFileDialog.Title = RevOmate.Properties.Resources.BACKUP_FILE_SAVE_DIALOG_TITLE;
                ScriptFile_Save_saveFileDialog.DefaultExt = Constants.BACKUP_FILE_EXTENSION;
                ScriptFile_Save_saveFileDialog.Filter = RevOmate.Properties.Resources.BACKUP_FILE_FILTER;

                if (ScriptFile_Save_saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // プログレスバーの最大値を設定
                    int prog_max = 0;
                    // Backupの場合は、FlashMemoryの読み込み回数を加算
                    prog_max = prog_max + (int)(FlashControl.FM_TOTAL_SIZE / FlashControl.FM_USB_READ_DATA_SIZE) + 1;
                    my_progress_bar_display(true, 0, prog_max);
                    my_App_Setting_Data.Backup_Restore_Progress_Value = 0;
                    my_App_Setting_Data.Backup_Restore_Progress_Max_Value = prog_max;

                    my_App_Setting_Data.Backup_Restore_Error_Code = 0;
                    my_App_Setting_Data.Backup_file_Path = ScriptFile_Save_saveFileDialog.FileName;
                    my_App_Setting_Data.Backup_Restore_Flag = Constants.BACKUP_FLAG_BACKUP;
                }
            }
            catch
            {
            }
        }

        private void lbl_FileImport_Icon_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (MessageBox.Show(RevOmate.Properties.Resources.RESTORE_WARNING_MSG, RevOmate.Properties.Resources.APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    ScriptFile_Import_openFileDialog.Title = RevOmate.Properties.Resources.BACKUP_FILE_OPEN_DIALOG_TITLE;
                    ScriptFile_Import_openFileDialog.DefaultExt = Constants.BACKUP_FILE_EXTENSION;
                    ScriptFile_Import_openFileDialog.Filter = RevOmate.Properties.Resources.BACKUP_FILE_FILTER;
                    ScriptFile_Import_openFileDialog.FileName = "";
                    if (ScriptFile_Import_openFileDialog.InitialDirectory == "")
                    {
                        ScriptFile_Import_openFileDialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    }

                    if (ScriptFile_Import_openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // プログレスバーの最大値を設定
                        int prog_max = 0;
                        // Flash消去の場合は、セクター数を加算
                        prog_max += (int)FlashControl.FM_SECTOR_NUM;
                        // Restoreの場合は、FlashMemoryの書き込み回数を加算
                        prog_max = prog_max + (((int)FlashControl.FM_TOTAL_SIZE / 0x100) * ((0x100 / (int)FlashControl.FM_USB_WRITE_DATA_SIZE) + 1));
                        my_progress_bar_display(true, 0, prog_max);
                        my_App_Setting_Data.Backup_Restore_Progress_Value = 0;
                        my_App_Setting_Data.Backup_Restore_Progress_Max_Value = prog_max;

                        my_App_Setting_Data.Backup_Restore_Error_Code = 0;
                        my_App_Setting_Data.Backup_file_Path = ScriptFile_Import_openFileDialog.FileName;
                        my_App_Setting_Data.Backup_Restore_Flag = Constants.BACKUP_FLAG_RESTORE;
                    }
                }
            }
            catch
            {
            }
        }

        private void lbl_function_set_icon_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (MessageBox.Show(RevOmate.Properties.Resources.DEVICE_FUNCTION_WRITE_CONFIRM_MSG, RevOmate.Properties.Resources.APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    my_function_setting_get_by_disp(function_setting_mode_select_no);
                    my_function_name_update();
                    my_device_data_set();
                    // 設定変更要求
                    set_func_setting_flag = true;
                    fData.Set_Flash_Write_Status(FlashControl.FM_WRITE_TYPE_FUNCTION_SETTING, FlashControl.FM_WRITE_STATUS_RQ);
                }
            }
            catch
            {
            }
        }

        private void lbl_button_setting_set_icon_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (MessageBox.Show(RevOmate.Properties.Resources.DEVICE_BASE_BUTTON_SETUP_WRITE_CONFIRM_MSG, RevOmate.Properties.Resources.APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    // ボタン設定内容を画面から取得
                    my_button_setting_get_by_disp(button_setting_mode_select_no);
                    // 設定変更要求
                    set_base_info_flag = true;
                    // 書き換え要求セット
                    fData.Set_Flash_Write_Status(FlashControl.FM_WRITE_TYPE_BASE_SETTING, FlashControl.FM_WRITE_STATUS_RQ);
                }
            }
            catch
            {
            }
        }

        //private void dgv_pattern_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    try
        //    {
        //        int idx_temp = dgv_pattern.Columns["dgv_pattern_img_data0"].Index;
        //        int idx_row = dgv_pattern.CurrentRow.Index;
        //        int pattern_idx = e.ColumnIndex - dgv_pattern.Columns["dgv_pattern_img_data0"].Index;
        //        if (dgv_pattern.Columns["dgv_pattern_img_data0"].Index <= e.ColumnIndex && e.ColumnIndex < (dgv_pattern.Columns["dgv_pattern_img_data0"].Index + Constants.PATTERN_DATA_SIZE))
        //        {
        //            if(0 <= idx_row && idx_row < Constants.PATTERN_NUM)
        //            {
        //                my_pattern_datas.pattern_datas[idx_row].pattern_data_disp[pattern_idx]++;
        //                if (my_pattern_datas.pattern_datas[idx_row].pattern_data_disp[pattern_idx] > Constants.PATTERN_SET_VAL_MAX)
        //                {
        //                    my_pattern_datas.pattern_datas[idx_row].pattern_data_disp[pattern_idx] = Constants.PATTERN_SET_VAL_MIN;
        //                }

        //                dgv_pattern[e.ColumnIndex, idx_row].Value = bmp_pattern_disp_icon[my_pattern_datas.pattern_datas[idx_row].pattern_data_disp[pattern_idx]];

        //            }
        //        }
        //    }
        //    catch
        //    {
        //    }
        //}

        private void lbl_Script_Add_Info_JoysticButton_Set_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                byte button_no = 0;
                for (int fi = 0; fi < my_rbtn_button_set.Length; fi++)
                {
                    if (my_rbtn_button_set[fi].Checked == true)
                    {
                        button_no = (byte)(fi & 0xFF);
                        break;
                    }
                }

                //my_Set_SE_JoyButton(indexOfItemUnderMouseToDrag, button_no);
                my_script_add_info_joystick_button(false, button_no);

                if (my_App_Setting_Data.Script_Add_Manual_Control == Constants.SCRIPT_ADD_MANUAL_JOYBUTTONDOWN)
                {
                    my_script_editor_disp_data.add(my_App_Setting_Data.Script_Drag_Target_idx, Constants.SCRIPT_COMMAND_JOY_BUTTON_PRESS, button_no, 0, 2, sw_Script_Interval, my_App_Setting_Data.Script_Edit_Item_Change_Flag);
                    FlashReadFirstTime = true;

                    my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_NON;
                }
                else if (my_App_Setting_Data.Script_Add_Manual_Control == Constants.SCRIPT_ADD_MANUAL_JOYBUTTONUP)
                {
                    my_script_editor_disp_data.add(my_App_Setting_Data.Script_Drag_Target_idx, Constants.SCRIPT_COMMAND_JOY_BUTTON_RELESE, button_no, 0, 2, sw_Script_Interval, my_App_Setting_Data.Script_Edit_Item_Change_Flag);
                    FlashReadFirstTime = true;

                    my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_NON;
                }
                my_App_Setting_Data.Script_Edit_Item_Change_Flag = false;
            }
            catch
            {
            }
        }

        private void lbl_Script_Add_Info_JoysticLever_Set_MouseClick(object sender, MouseEventArgs e)
        {
            bool error_flag = false;
            int[] read_joy_lever;
            byte[] set_joy_lever = new byte[my_txtbx_joystick_lever.Length];
            //int read_y = 0;
            try
            {
                try
                {
                    // X軸、Y軸を取り込み
                    read_joy_lever = new int[my_txtbx_joystick_lever.Length];
                    for (int fi = 0; fi < my_txtbx_joystick_lever.Length; fi++)
                    {
                        read_joy_lever[fi] = int.Parse(my_txtbx_joystick_lever[fi].Text);

                        // 範囲チェック
                        if (my_App_Setting_Data.Script_Add_Manual_Control == Constants.SCRIPT_ADD_MANUAL_MOUSEMOVE)
                        {
                            if (read_joy_lever[fi] < Constants.MOUSE_MOVE_SET_VAL_MIN)
                            {
                                read_joy_lever[fi] = Constants.MOUSE_MOVE_SET_VAL_MIN;
                            }
                            else if (read_joy_lever[fi] > Constants.MOUSE_MOVE_SET_VAL_MAX)
                            {
                                read_joy_lever[fi] = Constants.MOUSE_MOVE_SET_VAL_MAX;
                            }
                        }
                        else
                        {
                            if (read_joy_lever[fi] < Constants.JOY_LEVER_SET_VAL_MIN)
                            {
                                read_joy_lever[fi] = Constants.JOY_LEVER_SET_VAL_MIN;
                            }
                            else if (read_joy_lever[fi] > Constants.JOY_LEVER_SET_VAL_MAX)
                            {
                                read_joy_lever[fi] = Constants.JOY_LEVER_SET_VAL_MAX;
                            }
                        }

                        set_joy_lever[fi] = (byte)(read_joy_lever[fi] & 0xFF);

                        my_txtbx_joystick_lever[fi].Text = read_joy_lever[fi].ToString();
                    }
                }
                catch
                {
                    error_flag = true;
                }

                if (error_flag == false)
                {
                    if (my_App_Setting_Data.Script_Add_Manual_Control == Constants.SCRIPT_ADD_MANUAL_MOUSEMOVE)
                    {
                        my_script_editor_disp_data.add(my_App_Setting_Data.Script_Drag_Target_idx, Constants.SCRIPT_COMMAND_MOUSE_MOVE, set_joy_lever[0], set_joy_lever[1], 3, sw_Script_Interval, my_App_Setting_Data.Script_Edit_Item_Change_Flag);
                        FlashReadFirstTime = true;

                        my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_NON;

                        my_script_add_info_mouse_move(false, 0, 0);
                    }
                    else
                    {
                        if (my_App_Setting_Data.Script_Add_Manual_Control == Constants.SCRIPT_ADD_MANUAL_JOYLLEVERDOWN)
                        {
                            my_script_editor_disp_data.add(my_App_Setting_Data.Script_Drag_Target_idx, Constants.SCRIPT_COMMAND_JOY_L_LEVER, set_joy_lever[0], set_joy_lever[1], 3, sw_Script_Interval, my_App_Setting_Data.Script_Edit_Item_Change_Flag);
                            FlashReadFirstTime = true;

                            my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_NON;
                        }
                        else if (my_App_Setting_Data.Script_Add_Manual_Control == Constants.SCRIPT_ADD_MANUAL_JOYRLEVERDOWN)
                        {
                            my_script_editor_disp_data.add(my_App_Setting_Data.Script_Drag_Target_idx, Constants.SCRIPT_COMMAND_JOY_R_LEVER, set_joy_lever[0], set_joy_lever[1], 3, sw_Script_Interval, my_App_Setting_Data.Script_Edit_Item_Change_Flag);
                            FlashReadFirstTime = true;

                            my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_NON;
                        }

                        my_script_add_info_joystick_lever(false, 0, 0);
                    }
                }
                else
                {
                    MessageBox.Show("設定値が設定範囲外です", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                my_App_Setting_Data.Script_Edit_Item_Change_Flag = false;
            }
            catch
            {
            }
        }

        private void lbl_Script_Add_Info_MultiMedia_Set_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                //my_rbtn_multimedia_key
                byte button_no = 0;
                for (int fi = 0; fi < my_rbtn_multimedia_key.Length; fi++)
                {
                    if (my_rbtn_multimedia_key[fi].Checked == true)
                    {
                        button_no = (byte)(fi & 0xFF);
                        break;
                    }
                }

                byte set_type_data = 0;
                if (button_no < multimedia_set_type.Length)
                {
                    set_type_data = multimedia_set_type[button_no];
                }
                else
                {
                    set_type_data = multimedia_set_type[0];
                    button_no = 0;
                }

                my_script_add_info_multimedia_key(false, button_no);

                if (my_App_Setting_Data.Script_Add_Manual_Control == Constants.SCRIPT_ADD_MANUAL_MULTIMEDIADOWN)
                {
                    my_script_editor_disp_data.add(my_App_Setting_Data.Script_Drag_Target_idx, Constants.SCRIPT_COMMAND_MULTIMEDIA_PRESS, set_type_data, 0, 2, sw_Script_Interval, my_App_Setting_Data.Script_Edit_Item_Change_Flag);
                    FlashReadFirstTime = true;

                    my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_NON;
                }
                else if (my_App_Setting_Data.Script_Add_Manual_Control == Constants.SCRIPT_ADD_MANUAL_MULTIMEDIAUP)
                {
                    my_script_editor_disp_data.add(my_App_Setting_Data.Script_Drag_Target_idx, Constants.SCRIPT_COMMAND_MULTIMEDIA_RELESE, set_type_data, 0, 2, sw_Script_Interval, my_App_Setting_Data.Script_Edit_Item_Change_Flag);
                    FlashReadFirstTime = true;

                    my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_NON;
                }
                my_App_Setting_Data.Script_Edit_Item_Change_Flag = false;
            }
            catch
            {
            }
        }

        private void lbl_Script_Add_Info_Mouse_Set_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                byte button_no = 0;
                for (int fi = 0; fi < my_rbtn_mouse_button_set.Length; fi++)
                {
                    if (my_rbtn_mouse_button_set[fi].Checked == true)
                    {
                        button_no = (byte)(fi & 0xFF);
                        break;
                    }
                }

                byte set_type_data = 0;
                if (button_no < mouse_set_type.Length)
                {
                    set_type_data = mouse_set_type[button_no];
                }
                else
                {
                    set_type_data = mouse_set_type[0];
                    button_no = 0;
                }

                my_script_add_info_mouse_button(false, button_no);

                if (my_App_Setting_Data.Script_Add_Manual_Control == Constants.SCRIPT_ADD_MANUAL_MOUSECLICK)
                {
                    switch (set_type_data)
                    {
                        case Constants.MOUSE_DATA_LEFT_CLICK:
                            my_script_editor_disp_data.add(my_App_Setting_Data.Script_Drag_Target_idx, Constants.SCRIPT_COMMAND_L_CLICK, 0, 0, 1, sw_Script_Interval, my_App_Setting_Data.Script_Edit_Item_Change_Flag);
                            break;
                        case Constants.MOUSE_DATA_RIGHT_CLICK:
                            my_script_editor_disp_data.add(my_App_Setting_Data.Script_Drag_Target_idx, Constants.SCRIPT_COMMAND_R_CLICK, 0, 0, 1, sw_Script_Interval, my_App_Setting_Data.Script_Edit_Item_Change_Flag);
                            break;
                        case Constants.MOUSE_DATA_WHEEL_CLICK:
                            my_script_editor_disp_data.add(my_App_Setting_Data.Script_Drag_Target_idx, Constants.SCRIPT_COMMAND_W_CLICK, 0, 0, 1, sw_Script_Interval, my_App_Setting_Data.Script_Edit_Item_Change_Flag);
                            break;
                        case Constants.MOUSE_DATA_BUTTON4_CLICK:
                            my_script_editor_disp_data.add(my_App_Setting_Data.Script_Drag_Target_idx, Constants.SCRIPT_COMMAND_B4_CLICK, 0, 0, 1, sw_Script_Interval, my_App_Setting_Data.Script_Edit_Item_Change_Flag);
                            break;
                        case Constants.MOUSE_DATA_BUTTON5_CLICK:
                            my_script_editor_disp_data.add(my_App_Setting_Data.Script_Drag_Target_idx, Constants.SCRIPT_COMMAND_B5_CLICK, 0, 0, 1, sw_Script_Interval, my_App_Setting_Data.Script_Edit_Item_Change_Flag);
                            break;
                    }
                    FlashReadFirstTime = true;

                    my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_NON;
                }
                else if (my_App_Setting_Data.Script_Add_Manual_Control == Constants.SCRIPT_ADD_MANUAL_MOUSERELEASE)
                {
                    switch (set_type_data)
                    {
                        case Constants.MOUSE_DATA_LEFT_CLICK:
                            my_script_editor_disp_data.add(my_App_Setting_Data.Script_Drag_Target_idx, Constants.SCRIPT_COMMAND_L_RELEASE, 0, 0, 1, sw_Script_Interval, my_App_Setting_Data.Script_Edit_Item_Change_Flag);
                            break;
                        case Constants.MOUSE_DATA_RIGHT_CLICK:
                            my_script_editor_disp_data.add(my_App_Setting_Data.Script_Drag_Target_idx, Constants.SCRIPT_COMMAND_R_RELEASE, 0, 0, 1, sw_Script_Interval, my_App_Setting_Data.Script_Edit_Item_Change_Flag);
                            break;
                        case Constants.MOUSE_DATA_WHEEL_CLICK:
                            my_script_editor_disp_data.add(my_App_Setting_Data.Script_Drag_Target_idx, Constants.SCRIPT_COMMAND_W_RELEASE, 0, 0, 1, sw_Script_Interval, my_App_Setting_Data.Script_Edit_Item_Change_Flag);
                            break;
                        case Constants.MOUSE_DATA_BUTTON4_CLICK:
                            my_script_editor_disp_data.add(my_App_Setting_Data.Script_Drag_Target_idx, Constants.SCRIPT_COMMAND_B4_RELEASE, 0, 0, 1, sw_Script_Interval, my_App_Setting_Data.Script_Edit_Item_Change_Flag);
                            break;
                        case Constants.MOUSE_DATA_BUTTON5_CLICK:
                            my_script_editor_disp_data.add(my_App_Setting_Data.Script_Drag_Target_idx, Constants.SCRIPT_COMMAND_B5_RELEASE, 0, 0, 1, sw_Script_Interval, my_App_Setting_Data.Script_Edit_Item_Change_Flag);
                            break;
                    }
                    FlashReadFirstTime = true;

                    my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_NON;
                }

                my_App_Setting_Data.Script_Edit_Item_Change_Flag = false;
            }
            catch
            {
            }
        }

        private void dgv_pattern_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int ii = 0;
                ii++;
            }
            catch
            {
            }
        }

        private void lbl_MacroFileImport_Icon_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                ScriptFile_Import_openFileDialog.Title = RevOmate.Properties.Resources.SCRIPT_FILE_OPEN_DIALOG_TITLE;
                ScriptFile_Import_openFileDialog.DefaultExt = Constants.SCRIPT_FILE_EXTENSION;
                ScriptFile_Import_openFileDialog.Filter = Constants.SCRIPT_FILE_FILTER;
                ScriptFile_Import_openFileDialog.FileName = "";
                if (ScriptFile_Import_openFileDialog.InitialDirectory == "")
                {
                    ScriptFile_Import_openFileDialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                }
                if (ScriptFile_Import_openFileDialog.ShowDialog() == DialogResult.OK)
                {

                    FileStream fs = null;
                    try
                    {
                        int buff_max_size = Constants.SCRIPT_FILE_SIGNATURE_SIZE_LEN + Constants.SCRIPT_FILE_SIGNATURE_SIZE_MAX + Constants.SCRIPT_FILE_FILE_SIZE_LEN + Constants.SCRIPT_FILE_MODE_SIZE_LEN + Constants.SCRIPT_FILE_SCRIPT_SIZE_LEN + FlashControl.FM_SCRIPT_DATA_MAX_SIZE;
                        byte[] read_buff = new byte[buff_max_size];
                        fs = new FileStream(ScriptFile_Import_openFileDialog.FileName, FileMode.Open, FileAccess.Read);
                        if (fs.Length <= buff_max_size)
                        {
                            int read_size = fs.Read(read_buff, 0, (int)fs.Length);
                            if (read_size == fs.Length)
                            {
                                int sig_name_size = 0;
                                string sig_name = "";
                                int script_size = 0;
                                byte mode = 0;
                                // ファイル内容のチェック
                                if (my_Check_Script_File_Format(read_buff, read_size, ref sig_name_size, ref sig_name, ref script_size, ref mode) == true)
                                {
                                    ArrayList al_temp = new ArrayList();

                                    // 読み込みデータ変換
                                    my_Convert_Byte_Array_2_Array_List(read_buff, Constants.SCRIPT_FILE_SIGNATURE_SIZE_LEN + sig_name_size + Constants.SCRIPT_FILE_FILE_SIZE_LEN + Constants.SCRIPT_FILE_MODE_SIZE_LEN + Constants.SCRIPT_FILE_SCRIPT_SIZE_LEN, script_size, ref al_temp);

                                    // Script Editorに追加
                                    my_Script_Edit_Disp_Data_Add(al_temp, mode);

                                    // 表示更新
                                    FlashReadFirstTime = true;
                                }
                                else
                                {   // スクリプトファイルのフォーマットエラー
                                    MessageBox.Show(RevOmate.Properties.Resources.SCRIPT_FILE_IMPORT_ERROR_MSG, RevOmate.Properties.Resources.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                                }
                            }
                        }
                        else
                        {   // ファイルサイズ異常（大きい）
                            MessageBox.Show(RevOmate.Properties.Resources.SCRIPT_FILE_IMPORT_ERROR_MSG, RevOmate.Properties.Resources.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        }
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
            }
            catch
            {
            }
        }

        private void lbl_MacroFileExport_Icon_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                ScriptFile_Save_saveFileDialog.Title = RevOmate.Properties.Resources.SCRIPT_FILE_SAVE_DIALOG_TITLE;
                ScriptFile_Save_saveFileDialog.DefaultExt = Constants.SCRIPT_FILE_EXTENSION;
                ScriptFile_Save_saveFileDialog.Filter = Constants.SCRIPT_FILE_FILTER;
                ScriptFile_Save_saveFileDialog.FileName = "";
                if (ScriptFile_Save_saveFileDialog.InitialDirectory == "")
                {
                    ScriptFile_Save_saveFileDialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                }
                if (ScriptFile_Save_saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Script Editorのデータ取得
                    ArrayList al_buff = new ArrayList();
                    my_Convert_SE_Disp_Data_2_Array_List(ref al_buff);

                    int buff_max_size = Constants.SCRIPT_FILE_SIGNATURE_SIZE_LEN + Constants.SCRIPT_FILE_SIGNATURE_SIZE_MAX + Constants.SCRIPT_FILE_FILE_SIZE_LEN + Constants.SCRIPT_FILE_MODE_SIZE_LEN + Constants.SCRIPT_FILE_SCRIPT_SIZE_LEN + FlashControl.FM_SCRIPT_DATA_MAX_SIZE;
                    byte[] save_buff = new byte[buff_max_size];
                    int save_size = my_Convert_SE_Disp_Data_2_SL_byte_Buff(ref save_buff, al_buff, my_script_editor_disp_data.Script_Mode);

                    // スクリプトデータを保存する
                    my_Save_Script_File(ScriptFile_Save_saveFileDialog.FileName, save_buff, save_size);

                    // 表示更新
                    //FlashReadFirstTime = true;
                }
            }
            catch
            {
            }
        }

        private void my_progress_bar_display(bool p_visible, int p_now_val, int p_max_val)
        {
            try
            {
                if (p_visible == true)
                {   // 表示
                    pgb_process_status.Value = p_now_val;
                    pgb_process_status.Maximum = p_max_val;
                    lbl_progress_now_value.Text = p_now_val.ToString();
                    lbl_progress_total_value.Text = p_max_val.ToString();

                    pgb_process_status.Visible = p_visible;
                    lbl_progress_now_value.Visible = p_visible;
                    lbl_progress_per.Visible = p_visible;
                    lbl_progress_total_value.Visible = p_visible;
                }
                else
                {   // 非表示
                    pgb_process_status.Visible = p_visible;
                    lbl_progress_now_value.Visible = p_visible;
                    lbl_progress_per.Visible = p_visible;
                    lbl_progress_total_value.Visible = p_visible;

                    pgb_process_status.Value = 0;
                    pgb_process_status.Maximum = 0;
                    lbl_progress_now_value.Text = "0";
                    lbl_progress_total_value.Text = "0";
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Backup ファイルの読み出し
        /// </summary>
        /// <param name="p_File_Path"></param>
        /// <param name="p_al_data"></param>
        /// <returns></returns>
        private bool my_Load_Backup_File(string p_File_Path, ref ArrayList p_al_data)
        {
            bool b_ret = false;
            try
            {
                // ファイルの有無をチェック
                if (File.Exists(p_File_Path) == true)
                {
                    try
                    {
                        byte[] bs = System.IO.File.ReadAllBytes(p_File_Path);

                        // サイズチェック
                        if (bs.Length == FlashControl.FM_TOTAL_SIZE)
                        {
                            p_al_data.Clear();

                            p_al_data.AddRange(bs);
                        }
                        else
                        {
                            // サイズエラー
                            b_ret = true;
                        }
                    }
                    catch
                    {   // 例外発生
                        b_ret = true;
                    }
                }
                else
                {   //ファイルなし
                    b_ret = true;
                }

            }
            catch
            {
            }
            return b_ret;
        }

        /// <summary>
        /// Backup ファイルの保存
        /// </summary>
        /// <param name="p_File_Path"></param>
        /// <param name="p_al_data"></param>
        /// <returns></returns>
        private bool my_Save_Backup_File(string p_File_Path, ArrayList p_al_data)
        {
            bool b_ret = false;
            byte[] byte_buff = new byte[1024];
            int pos = 0;
            int out_size = 0;
            try
            {
                FileStream fs = null;
                try
                {
                    fs = new FileStream(p_File_Path, FileMode.Create, FileAccess.Write);
                    while (pos < p_al_data.Count)
                    {
                        out_size = 0;
                        //バイト配列にコピー
                        for (int fi = 0; fi < byte_buff.Length && pos < p_al_data.Count; fi++)
                        {
                            byte_buff[fi] = (byte)p_al_data[pos];
                            out_size++;
                            pos++;
                        }
                        fs.Write(byte_buff, 0, out_size);
                    }
                }
                catch
                {
                    b_ret = true;
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
            catch
            {
            }
            return b_ret;
        }

        private void btn_FlashErase_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(RevOmate.Properties.Resources.FLASHERASE_WARNING_MSG, RevOmate.Properties.Resources.APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    my_progress_bar_display(true, 0, (int)FlashControl.FM_SECTOR_NUM);
                    my_App_Setting_Data.Backup_Restore_Progress_Value = 0;
                    my_App_Setting_Data.Backup_Restore_Progress_Max_Value = (int)FlashControl.FM_SECTOR_NUM;

                    my_App_Setting_Data.Backup_Restore_Error_Code = 0;
                    my_App_Setting_Data.Backup_Restore_Flag = Constants.BACKUP_FLAG_RESET;
                }
            }
            catch
            {
            }
        }
        
        private void lbl_button_factory_reset_icon_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                string w_msg = RevOmate.Properties.Resources.RESET_WARNING_MSG1;
                if (MessageBox.Show(w_msg, RevOmate.Properties.Resources.APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    w_msg = RevOmate.Properties.Resources.RESET_WARNING_MSG2 + "\n" + RevOmate.Properties.Resources.RESET_WARNING_MSG3;
                    if (MessageBox.Show(w_msg, RevOmate.Properties.Resources.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        my_progress_bar_display(true, 0, (int)FlashControl.FM_SECTOR_NUM);
                        my_App_Setting_Data.Backup_Restore_Progress_Value = 0;
                        my_App_Setting_Data.Backup_Restore_Progress_Max_Value = (int)FlashControl.FM_SECTOR_NUM;

                        my_App_Setting_Data.Backup_Restore_Error_Code = 0;
                        my_App_Setting_Data.Backup_Restore_Flag = Constants.BACKUP_FLAG_RESET;
                    }
                }
            }
            catch
            {
            }
        }

        private void btn_debug_led_duty_set_Click(object sender, EventArgs e)
        {
            try
            {
                debug_led_rgb_set_val[0] = (byte)(((int)numud_led_r_set_val.Value) & 0xFF);
                debug_led_rgb_set_val[1] = (byte)(((int)numud_led_g_set_val.Value) & 0xFF);
                debug_led_rgb_set_val[2] = (byte)(((int)numud_led_b_set_val.Value) & 0xFF);

                debug_led_brightness_level_set_val = (byte)(((int)numud_led_brightness_level_set_val.Value) & 0xFF);

                debug_led_rgb_set_req = true;
            }
            catch
            {
            }
        }

        private void btn_LED_preview_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] tmp_rgb = new byte[LED_preview_set_data.Length];
                for (int fi = 0; fi < tmp_rgb.Length && fi < my_tb_LED_Duty_set_button.Length; fi++)
                {
                    tmp_rgb[fi] = (byte)(my_tb_LED_Duty_set_button[fi].Value & 0xFF);
                }

                byte tmp_brightness_level = 0;
                for (int fi = 0; fi < my_rbtn_LED_Brightness_Level_set_button.Length; fi++)
                {
                    if (my_rbtn_LED_Brightness_Level_set_button[fi].Checked == true)
                    {
                        tmp_brightness_level = (byte)(fi & 0xFF);
                        break;
                    }
                }
                my_LED_Preview_Set(tmp_rgb, tmp_brightness_level);
            }
            catch
            {
            }
        }

        public void my_LED_Preview_Set(byte[] p_LED_RGB, byte p_brightlevel)
        {
            try
            {
                if (LED_preview_set_data.Length == p_LED_RGB.Length && p_brightlevel < Constants.LED_BRIGHTNESS_LEVEL_SET_NUM)
                {   // パラメーターOK

                    // RGBデータ・セット
                    for (int fi = 0; fi < LED_preview_set_data.Length && fi < p_LED_RGB.Length; fi++)
                    {
                        LED_preview_set_data[fi] = p_LED_RGB[fi];
                    }
                    // LED輝度セット
                    LED_preview_brightness_level = p_brightlevel;

                    LED_preview_set_flag = true;
                }
            }
            catch
            {
            }
        }

        private void btn_LED_preview_func_Click(object sender, EventArgs e)
        {
            try
            {
                int func_no = int.Parse(((Button)sender).Tag.ToString());

                if (0 <= func_no && func_no <= my_tb_LED_Duty_set_func.GetUpperBound(0))
                {
                    LED_preview_set_data[0] = (byte)(my_tb_LED_Duty_set_func[func_no, 0].Value & 0xFF);
                    LED_preview_set_data[1] = (byte)(my_tb_LED_Duty_set_func[func_no, 1].Value & 0xFF);
                    LED_preview_set_data[2] = (byte)(my_tb_LED_Duty_set_func[func_no, 2].Value & 0xFF);

                    LED_preview_brightness_level = 0;
                    for (int fi = 0; fi <= my_rbtn_LED_Brightness_Level_set_func.GetUpperBound(1); fi++)
                    {
                        if (my_rbtn_LED_Brightness_Level_set_func[func_no, fi].Checked == true)
                        {
                            LED_preview_brightness_level = (byte)(fi & 0xFF);
                            break;
                        }
                    }

                    LED_preview_set_flag = true;
                }
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
                    for (int fi = 0; fi < my_tb_LED_Duty_set_button.Length && fi < my_LED_Color_default.GetLength(1); fi++)
                    {
                        my_tb_LED_Duty_set_button[fi].Value = my_LED_Color_default[color_idx, fi];
                    }
                }
            }
            catch
            {
            }
        }

        private void chkbx_LED_color_detail_set_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
            }
            catch
            {
            }
        }

        private void trackBar_RGB_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int tag_no = int.Parse(((TrackBar)sender).Tag.ToString());

                int r_set = trackBar_R.Value * 0xFF / trackBar_R.Maximum;
                int g_set = trackBar_G.Value * 0xFF / trackBar_G.Maximum;
                int b_set = trackBar_B.Value * 0xFF / trackBar_B.Maximum;
                lbl_LED_COLOR_NOW.BackColor = Color.FromArgb(r_set, g_set, b_set);
            }
            catch
            {
            }
        }

        private void lbl_LED_COLOR_func_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                int tag_no = int.Parse(((Label)sender).Tag.ToString());
                int func_idx = tag_no / 100;
                int color_idx = tag_no % 100;

                if (0 <= func_idx && func_idx <= my_tb_LED_Duty_set_func.GetUpperBound(0) && 0 <= color_idx && color_idx < my_LED_Color_default.GetLength(0))
                {
                    for (int fi = 0; fi <= my_tb_LED_Duty_set_func.GetUpperBound(1) && fi < my_LED_Color_default.GetLength(1); fi++)
                    {
                        my_tb_LED_Duty_set_func[func_idx, fi].Value = my_LED_Color_default[color_idx, fi];
                    }
                }
            }
            catch
            {
            }
        }

        private void chkbx_LED_color_detail_set_func_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
            }
            catch
            {
            }
        }

        private void trackBar_RGB_func_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int tag_no = int.Parse(((TrackBar)sender).Tag.ToString());
                int func_no = tag_no / 10;

                if (func_no < my_lbl_LED_Color_now_func.Length && func_no <= my_tb_LED_Duty_set_func.GetUpperBound(0))
                {
                    int r_set = my_tb_LED_Duty_set_func[func_no, 0].Value * 0xFF / my_tb_LED_Duty_set_func[func_no, 0].Maximum;
                    int g_set = my_tb_LED_Duty_set_func[func_no, 1].Value * 0xFF / my_tb_LED_Duty_set_func[func_no, 1].Maximum;
                    int b_set = my_tb_LED_Duty_set_func[func_no, 2].Value * 0xFF / my_tb_LED_Duty_set_func[func_no, 2].Maximum;
                    my_lbl_LED_Color_now_func[func_no].BackColor = Color.FromArgb(r_set, g_set, b_set);
                }
            }
            catch
            {
            }
        }

        private void lbl_func_mode_select_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                int select_mode = int.Parse(((Label)sender).Tag.ToString());
                if (0 <= select_mode && select_mode < Constants.MODE_NUM)
                {
                    if (function_setting_mode_select_no != select_mode)
                    {   // 表示モード変更あり

                        // 変更前のモードの設定値を取得
                        my_function_setting_get_by_disp(function_setting_mode_select_no);
                        // 変更後のモードへ
                        function_setting_mode_select_no = select_mode;
                        my_function_name_update();
                        my_function_setting_disp(function_setting_mode_select_no, true);
                    }
                }
            }
            catch
            {
            }
        }

        private void lbl_button_mode_select_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                int select_mode = int.Parse(((Label)sender).Tag.ToString());
                if (0 <= select_mode && select_mode < Constants.MODE_NUM)
                {
                    if (button_setting_mode_select_no != select_mode)
                    {   // 表示モード変更あり

                        // 変更前のモードの設定値を取得
                        my_button_setting_get_by_disp(button_setting_mode_select_no);
                        // 変更後のモードへ
                        button_setting_mode_select_no = select_mode;
                        my_function_name_update();
                        my_button_setting_disp(button_setting_mode_select_no, true);
                        my_sw_func_name_disp(button_setting_mode_select_no, true);
                        //my_button_setting_macro_name_update();
                    }
                    
                }
            }
            catch
            {
            }
        }


        // デバイスへセットするデータ形式でデータをセット
        private void my_device_data_set()
        {
            try
            {
                // バッファクリア
                for (int fi = 0; fi < Constants.SETTING_NUM; fi++)
                {
                    for (int fj = 0; fj < Constants.DEVICE_DATA_LEN; fj++)
                    {
                        my_set_data.setting_data[fi, fj] = 0;
                    }
                }

                for (int fi = 0; fi < Constants.SETTING_NUM; fi++)
                {
                    switch (my_set_data.set_type[fi])
                    {
                        case Constants.SET_TYPE_MOUSE_LCLICK:
                        case Constants.SET_TYPE_MOUSE_RCLICK:
                        case Constants.SET_TYPE_MOUSE_WHCLICK:
                        case Constants.SET_TYPE_MOUSE_B4CLICK:
                        case Constants.SET_TYPE_MOUSE_B5CLICK:
                        case Constants.SET_TYPE_MOUSE_DCLICK:
                        case Constants.SET_TYPE_MOUSE_MOVE:
                        case Constants.SET_TYPE_MOUSE_WHSCROLL:
                            my_set_data.setting_data[fi, Constants.DEVICE_DATA_SET_TYPE_IDX] = my_set_data.set_type[fi];
                            for (int fj = 0; fj < Constants.MOUSE_DATA_LEN; fj++)
                            {
                                my_set_data.setting_data[fi, Constants.DEVICE_DATA_CLICK_IDX + fj] = my_set_data.mouse_data[fi, fj];
                            }
                            break;
                        case Constants.SET_TYPE_KEYBOARD_KEY:
                            my_set_data.setting_data[fi, Constants.DEVICE_DATA_SET_TYPE_IDX] = my_set_data.set_type[fi];
                            for (int fj = 0; fj < Constants.KEYBOARD_DATA_LEN; fj++)
                            {
                                my_set_data.setting_data[fi, Constants.DEVICE_DATA_MODIFIER_IDX + fj] = my_set_data.keyboard_data[fi, fj];
                            }
                            break;
                        case Constants.SET_TYPE_MULTIMEDIA_PLAY:
                        case Constants.SET_TYPE_MULTIMEDIA_PAUSE:
                        case Constants.SET_TYPE_MULTIMEDIA_STOP:
                        case Constants.SET_TYPE_MULTIMEDIA_REC:
                        case Constants.SET_TYPE_MULTIMEDIA_FORWORD:
                        case Constants.SET_TYPE_MULTIMEDIA_REWIND:
                        case Constants.SET_TYPE_MULTIMEDIA_NEXT:
                        case Constants.SET_TYPE_MULTIMEDIA_PREVIOUS:
                        case Constants.SET_TYPE_MULTIMEDIA_MUTE:
                        case Constants.SET_TYPE_MULTIMEDIA_VOLUMEUP:
                        case Constants.SET_TYPE_MULTIMEDIA_VOLUMEDOWN:
                            my_set_data.setting_data[fi, Constants.DEVICE_DATA_SET_TYPE_IDX] = my_set_data.set_type[fi];
                            for (int fj = 0; fj < Constants.MULTIMEDIA_DATA_LEN; fj++)
                            {
                                my_set_data.setting_data[fi, Constants.DEVICE_DATA_MULTIMEDIA_VAL1_IDX + fj] = my_set_data.multimedia_data[fi, fj];
                            }
                            break;
                        case Constants.SET_TYPE_JOYPAD_XY:
                        case Constants.SET_TYPE_JOYPAD_ZRZ:
                        case Constants.SET_TYPE_JOYPAD_B01:
                        case Constants.SET_TYPE_JOYPAD_B02:
                        case Constants.SET_TYPE_JOYPAD_B03:
                        case Constants.SET_TYPE_JOYPAD_B04:
                        case Constants.SET_TYPE_JOYPAD_B05:
                        case Constants.SET_TYPE_JOYPAD_B06:
                        case Constants.SET_TYPE_JOYPAD_B07:
                        case Constants.SET_TYPE_JOYPAD_B08:
                        case Constants.SET_TYPE_JOYPAD_B09:
                        case Constants.SET_TYPE_JOYPAD_B10:
                        case Constants.SET_TYPE_JOYPAD_B11:
                        case Constants.SET_TYPE_JOYPAD_B12:
                        case Constants.SET_TYPE_JOYPAD_B13:
                        case Constants.SET_TYPE_JOYPAD_HSW_NORTH:
                        case Constants.SET_TYPE_JOYPAD_HSW_EAST:
                        case Constants.SET_TYPE_JOYPAD_HSW_SOUTH:
                        case Constants.SET_TYPE_JOYPAD_HSW_WEST:
                            my_set_data.setting_data[fi, Constants.DEVICE_DATA_SET_TYPE_IDX] = my_set_data.set_type[fi];
                            for (int fj = 0; fj < Constants.JOYPAD_DATA_LEN; fj++)
                            {
                                my_set_data.setting_data[fi, Constants.DEVICE_DATA_JOY_BUTTON1_IDX + fj] = my_set_data.joypad_data[fi, fj];
                            }
                            break;
                        case Constants.SET_TYPE_NUMBER_UP:
                        case Constants.SET_TYPE_NUMBER_DOWN:
                            my_set_data.setting_data[fi, Constants.DEVICE_DATA_SET_TYPE_IDX] = my_set_data.set_type[fi];
                            break;
                        case Constants.SET_TYPE_ENCODER_SCRIPT1:
                        case Constants.SET_TYPE_ENCODER_SCRIPT2:
                        case Constants.SET_TYPE_ENCODER_SCRIPT3:
                            my_set_data.setting_data[fi, Constants.DEVICE_DATA_SET_TYPE_IDX] = my_set_data.set_type[fi];
                            break;
                        case Constants.SET_TYPE_NONE:
                        default:
                            my_set_data.setting_data[fi, Constants.DEVICE_DATA_SET_TYPE_IDX] = my_set_data.set_type[fi];
                            break;
                    }

                    // 感度
                    my_set_data.setting_data[fi, Constants.DEVICE_DATA_SENSE_IDX] = my_set_data.sensitivity[fi];

                }


                for (int mode_no = 0; mode_no < Constants.MODE_NUM; mode_no++)
                {
                    for (int func_no = 0; func_no < Constants.FUNCTION_NUM; func_no++)
                    {
                        for (int cw_ccw_idx = 0; cw_ccw_idx < Constants.CW_CCW_NUM; cw_ccw_idx++)
                        {
                            int data_idx = (mode_no * Constants.FUNCTION_NUM * Constants.CW_CCW_NUM) + (func_no * Constants.CW_CCW_NUM) + cw_ccw_idx;
                            for (int fi = 0; fi < Constants.DEVICE_DATA_LEN; fi++)
                            {
                                my_func_datas.func_datas[mode_no].func_data[func_no].cw_ccw_data[cw_ccw_idx, fi] = my_set_data.setting_data[data_idx, fi];
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }
        // デバイスのデータ形式から、本アプリ用のデータ形式で取得
        private void my_device_data_get()
        {
            try
            {
                // データクリア
                //my_set_data.Init();

                for (int mode_no = 0; mode_no < Constants.MODE_NUM; mode_no++)
                {
                    for (int func_no = 0; func_no < Constants.FUNCTION_NUM; func_no++)
                    {
                        for (int cw_ccw_idx = 0; cw_ccw_idx < Constants.CW_CCW_NUM; cw_ccw_idx++)
                        {
                            int data_idx = (mode_no * Constants.FUNCTION_NUM * Constants.CW_CCW_NUM) + (func_no * Constants.CW_CCW_NUM) + cw_ccw_idx;
                            for (int fi = 0; fi < Constants.DEVICE_DATA_LEN; fi++)
                            {
                                my_set_data.setting_data[data_idx, fi] = my_func_datas.func_datas[mode_no].func_data[func_no].cw_ccw_data[cw_ccw_idx, fi];
                            }
                        }
                    }
                }

                for (int fi = 0; fi < Constants.SETTING_NUM; fi++)
                {
                    // 表示データクリア
                    my_set_data.disp_data_clear(fi);

                    // 感度
                    my_set_data.sensitivity[fi] = my_set_data.setting_data[fi, Constants.DEVICE_DATA_SENSE_IDX];

                    switch (my_set_data.setting_data[fi, Constants.DEVICE_DATA_SET_TYPE_IDX])
                    {
                        case Constants.SET_TYPE_MOUSE_LCLICK:
                        case Constants.SET_TYPE_MOUSE_RCLICK:
                        case Constants.SET_TYPE_MOUSE_WHCLICK:
                        case Constants.SET_TYPE_MOUSE_B4CLICK:
                        case Constants.SET_TYPE_MOUSE_B5CLICK:
                        case Constants.SET_TYPE_MOUSE_DCLICK:
                        case Constants.SET_TYPE_MOUSE_MOVE:
                        case Constants.SET_TYPE_MOUSE_WHSCROLL:
                            my_set_data.set_type[fi] = my_set_data.setting_data[fi, Constants.DEVICE_DATA_SET_TYPE_IDX];
                            for (int fj = 0; fj < Constants.MOUSE_DATA_LEN; fj++)
                            {
                                my_set_data.mouse_data[fi, fj] = my_set_data.setting_data[fi, Constants.DEVICE_DATA_CLICK_IDX + fj];
                            }
                            break;
                        case Constants.SET_TYPE_KEYBOARD_KEY:
                            my_set_data.set_type[fi] = my_set_data.setting_data[fi, Constants.DEVICE_DATA_SET_TYPE_IDX];
                            for (int fj = 0; fj < Constants.KEYBOARD_DATA_LEN; fj++)
                            {
                                my_set_data.keyboard_data[fi, fj] = my_set_data.setting_data[fi, Constants.DEVICE_DATA_MODIFIER_IDX + fj];
                            }
                            break;
                        case Constants.SET_TYPE_MULTIMEDIA_PLAY:
                        case Constants.SET_TYPE_MULTIMEDIA_PAUSE:
                        case Constants.SET_TYPE_MULTIMEDIA_STOP:
                        case Constants.SET_TYPE_MULTIMEDIA_REC:
                        case Constants.SET_TYPE_MULTIMEDIA_FORWORD:
                        case Constants.SET_TYPE_MULTIMEDIA_REWIND:
                        case Constants.SET_TYPE_MULTIMEDIA_NEXT:
                        case Constants.SET_TYPE_MULTIMEDIA_PREVIOUS:
                        case Constants.SET_TYPE_MULTIMEDIA_MUTE:
                        case Constants.SET_TYPE_MULTIMEDIA_VOLUMEUP:
                        case Constants.SET_TYPE_MULTIMEDIA_VOLUMEDOWN:
                            my_set_data.set_type[fi] = my_set_data.setting_data[fi, Constants.DEVICE_DATA_SET_TYPE_IDX];
                            for (int fj = 0; fj < Constants.MULTIMEDIA_DATA_LEN; fj++)
                            {
                                my_set_data.multimedia_data[fi, fj] = my_set_data.setting_data[fi, Constants.DEVICE_DATA_MULTIMEDIA_VAL1_IDX + fj];
                            }
                            break;
                        case Constants.SET_TYPE_JOYPAD_XY:
                        case Constants.SET_TYPE_JOYPAD_ZRZ:
                        case Constants.SET_TYPE_JOYPAD_B01:
                        case Constants.SET_TYPE_JOYPAD_B02:
                        case Constants.SET_TYPE_JOYPAD_B03:
                        case Constants.SET_TYPE_JOYPAD_B04:
                        case Constants.SET_TYPE_JOYPAD_B05:
                        case Constants.SET_TYPE_JOYPAD_B06:
                        case Constants.SET_TYPE_JOYPAD_B07:
                        case Constants.SET_TYPE_JOYPAD_B08:
                        case Constants.SET_TYPE_JOYPAD_B09:
                        case Constants.SET_TYPE_JOYPAD_B10:
                        case Constants.SET_TYPE_JOYPAD_B11:
                        case Constants.SET_TYPE_JOYPAD_B12:
                        case Constants.SET_TYPE_JOYPAD_B13:
                        case Constants.SET_TYPE_JOYPAD_HSW_NORTH:
                        case Constants.SET_TYPE_JOYPAD_HSW_EAST:
                        case Constants.SET_TYPE_JOYPAD_HSW_SOUTH:
                        case Constants.SET_TYPE_JOYPAD_HSW_WEST:
                            my_set_data.set_type[fi] = my_set_data.setting_data[fi, Constants.DEVICE_DATA_SET_TYPE_IDX];
                            for (int fj = 0; fj < Constants.JOYPAD_DATA_LEN; fj++)
                            {
                                my_set_data.joypad_data[fi, fj] = my_set_data.setting_data[fi, Constants.DEVICE_DATA_JOY_BUTTON1_IDX + fj];
                            }
                            break;
                        case Constants.SET_TYPE_NUMBER_UP:
                        case Constants.SET_TYPE_NUMBER_DOWN:
                            my_set_data.set_type[fi] = my_set_data.setting_data[fi, Constants.DEVICE_DATA_SET_TYPE_IDX];
                            break;
                        case Constants.SET_TYPE_ENCODER_SCRIPT1:
                        case Constants.SET_TYPE_ENCODER_SCRIPT2:
                        case Constants.SET_TYPE_ENCODER_SCRIPT3:
                            my_set_data.set_type[fi] = my_set_data.setting_data[fi, Constants.DEVICE_DATA_SET_TYPE_IDX];
                            break;
                        case Constants.SET_TYPE_NONE:
                        default:
                            my_set_data.set_type[fi] = my_set_data.setting_data[fi, Constants.DEVICE_DATA_SET_TYPE_IDX];
                            break;
                    }
                }
            }
            catch
            {
            }
        }

        private void txtbx_func_key_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                int idx = int.Parse(((System.Windows.Forms.TextBox)sender).Tag.ToString());
                if (0 <= idx && idx < (Constants.FUNCTION_NUM * Constants.CW_CCW_NUM))
                {
                    if (e.KeyCode == Keys.Tab)
                    {
                        if (my_keyboard_key[idx / Constants.CW_CCW_NUM, idx % Constants.CW_CCW_NUM].Text != e.KeyValue.ToString())
                        {
                            byte tmp_usb_key = const_Key_Code.Get_VKtoUSBkey(e.KeyValue.GetHashCode(), system_setting_info.Keyboard_Type, true);
                            my_keyboard_key[idx / Constants.CW_CCW_NUM, idx % Constants.CW_CCW_NUM].Text = const_Key_Code.Get_KeyCode_Name(tmp_usb_key, system_setting_info.Keyboard_Type);
                            int data_idx = (function_setting_mode_select_no * Constants.FUNCTION_NUM * Constants.CW_CCW_NUM) + idx;
                            my_set_data.keyboard_data[data_idx, Constants.KEYBOARD_DATA_KEY1_IDX] = tmp_usb_key;
                            e.IsInputKey = true;
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void txtbx_func_key_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                int idx = int.Parse(((System.Windows.Forms.TextBox)sender).Tag.ToString());
                if (0 <= idx && idx < (Constants.FUNCTION_NUM * Constants.CW_CCW_NUM))
                {
                    if (e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.Alt)
                    //if ((e.KeyCode & Keys.ControlKey) == Keys.ControlKey || (e.KeyCode & Keys.ShiftKey) == Keys.ShiftKey || (e.KeyCode & Keys.Alt) == Keys.Alt)
                    {
                        e.Handled = true;
                    }
                    else
                    {
                        byte tmp_usb_key = const_Key_Code.Get_VKtoUSBkey(e.KeyValue.GetHashCode(), system_setting_info.Keyboard_Type, true);
                        my_keyboard_key[idx / Constants.CW_CCW_NUM, idx % Constants.CW_CCW_NUM].Text = const_Key_Code.Get_KeyCode_Name(tmp_usb_key, system_setting_info.Keyboard_Type);
                        int data_idx = (function_setting_mode_select_no * Constants.FUNCTION_NUM * Constants.CW_CCW_NUM) + idx;
                        my_set_data.keyboard_data[data_idx, Constants.KEYBOARD_DATA_KEY1_IDX] = tmp_usb_key;
                        e.SuppressKeyPress = true;
                    }
                }
            }
            catch
            {
            }
        }

        private void txtbx_func_key_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                int idx = int.Parse(((System.Windows.Forms.TextBox)sender).Tag.ToString());
                if (0 <= idx && idx < (Constants.FUNCTION_NUM * Constants.CW_CCW_NUM))
                {
                    if (e.KeyCode == Keys.PrintScreen)
                    {
                        byte tmp_usb_key = const_Key_Code.Get_VKtoUSBkey(e.KeyValue.GetHashCode(), system_setting_info.Keyboard_Type, true);
                        my_keyboard_key[idx / Constants.CW_CCW_NUM, idx % Constants.CW_CCW_NUM].Text = const_Key_Code.Get_KeyCode_Name(tmp_usb_key, system_setting_info.Keyboard_Type);
                        int data_idx = (function_setting_mode_select_no * Constants.FUNCTION_NUM * Constants.CW_CCW_NUM) + idx;
                        my_set_data.keyboard_data[data_idx, Constants.KEYBOARD_DATA_KEY1_IDX] = tmp_usb_key;
                        e.SuppressKeyPress = true;
                    }
                }
            }
            catch
            {
            }
        }

        private void cmbbx_func_set_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idx = int.Parse(((System.Windows.Forms.ComboBox)sender).Tag.ToString());
                if (0 <= idx && idx < (Constants.FUNCTION_NUM * Constants.CW_CCW_NUM))
                {
                    byte set_type_no = (byte)(my_get_disp_set_type(my_cmbbx_set_type[idx / Constants.CW_CCW_NUM, idx % Constants.CW_CCW_NUM].SelectedIndex) & 0xFF);
                    my_set_data.set_type[(function_setting_mode_select_no * Constants.FUNCTION_NUM * Constants.CW_CCW_NUM) + idx] = set_type_no;
                    my_function_setting_disp(function_setting_mode_select_no, idx / Constants.CW_CCW_NUM, idx % Constants.CW_CCW_NUM, true);
                }
            }
            catch
            {
            }
        }

        private void btn_func_key_clr_Click(object sender, EventArgs e)
        {
            int idx = int.Parse(((System.Windows.Forms.Button)sender).Tag.ToString());
            if (0 <= idx && idx < (Constants.FUNCTION_NUM * Constants.CW_CCW_NUM))
            {
                for (int fi = 0; fi < my_keyboard_modifier.GetLength(2); fi++)
                {
                    my_keyboard_modifier[idx / Constants.CW_CCW_NUM, idx % Constants.CW_CCW_NUM, fi].Checked = false;
                }
                my_keyboard_key[idx / Constants.CW_CCW_NUM, idx % Constants.CW_CCW_NUM].Text = RevOmate.Properties.Resources.KEYBOARD_SET_KEY_EMPTY;
                int data_idx = (function_setting_mode_select_no * Constants.FUNCTION_NUM * Constants.CW_CCW_NUM) + idx;
                my_set_data.keyboard_data[data_idx, Constants.KEYBOARD_DATA_KEY1_IDX] = 0x00;
            }
        }

        private void my_set_disp_set_type(byte p_set_type_no, int p_func_no, int p_cw_ccw_idx)
        {
            try
            {
                int set_idx = 0;
                for (int fi = 0; fi < set_type_no_list.Length; fi++)
                {
                    if (set_type_no_list[fi] == p_set_type_no)
                    {
                        set_idx = fi;
                        break;
                    }
                }

                if (0 <= set_idx && set_idx < my_cmbbx_set_type[p_func_no, p_cw_ccw_idx].Items.Count)
                {
                    my_cmbbx_set_type[p_func_no, p_cw_ccw_idx].SelectedIndex = set_idx;
                }
            }
            catch
            {
            }
        }

        private int my_get_disp_set_type(int p_select_type_idx)
        {
            int ret_set_type = 0;
            try
            {
                if (0 <= p_select_type_idx && p_select_type_idx < set_type_no_list.Length)
                {
                    ret_set_type = set_type_no_list[p_select_type_idx];
                }
            }
            catch
            {
            }
            return ret_set_type;
        }

        private void btn_base_setting_set_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(RevOmate.Properties.Resources.DEVICE_BASE_SETUP_WRITE_CONFIRM_MSG, RevOmate.Properties.Resources.APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    // 基本設定内容を画面から取得
                    my_base_setting_get_by_disp();
                    // 設定変更要求
                    set_base_info_flag = true;
                    // 書き換え要求セット
                    fData.Set_Flash_Write_Status(FlashControl.FM_WRITE_TYPE_BASE_SETTING, FlashControl.FM_WRITE_STATUS_RQ);
                }
            }
            catch
            {
            }
        }

        private void chkbx_mode_led_off_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                num_mode_led_off_time.Enabled = chkbx_mode_led_off.Checked;
            }
            catch
            {
            }
        }

        private void lbl_encoder_script_setting_set_icon_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                // 実行スクリプトNoを変更後に、設定ボタンを押すと値が変更前のままなので、カレントセルを変更して確定させる
                if (dgv_encoder_script.CurrentCell != null)
                {
                    int x = dgv_encoder_script.CurrentCellAddress.X;
                    int y = dgv_encoder_script.CurrentCellAddress.Y;
                    dgv_encoder_script.CurrentCell = dgv_encoder_script[0, y];
                }

                if (MessageBox.Show(RevOmate.Properties.Resources.DEVICE_ENCODER_SCRIPT_WRITE_CONFIRM_MSG, RevOmate.Properties.Resources.APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if (my_encoder_script_setting_get_by_disp(encoder_script_select_no) == true)
                    {   // 変更あり
                        // 設定変更要求
                        set_encoder_script_flag = true;
                        fData.Set_Flash_Write_Status(FlashControl.FM_WRITE_TYPE_ENCODER_SCRIPT_SETTING, FlashControl.FM_WRITE_STATUS_RQ);
                    }
                    // 表示更新
                    my_encoder_script_setting_disp(encoder_script_select_no, true);
                }
            }
            catch
            {
            }
        }

        // エンコーダーマクロ設定のマクロ名称をアップデート
        private void my_encoder_script_macro_name_update()
        {
            //int sel_idx = 0;
            //string temp_macro_name = "";
            try
            {
                int script_name_idx = dgv_encoder_script.Columns["dgv_script_name"].Index;

                // 現在の設定の選択肢を記憶
                int[] sel_data_idx = new int[my_encoder_script_datas.encoder_script_datas[encoder_script_select_no].script_no.Length];
                for (int idx = 0; idx < my_encoder_script_datas.encoder_script_datas[encoder_script_select_no].script_no.Length; idx++)
                {
                    DataGridViewComboBoxCell dgvcbc = (DataGridViewComboBoxCell)dgv_encoder_script.Rows[idx].Cells[script_name_idx];
                    sel_data_idx[idx] = dgvcbc.Items.IndexOf(dgvcbc.Value);
                }

                // マクロ設定のコンボボックスを差し替え
                DataGridViewComboBoxColumn dgv_combbx_col = new DataGridViewComboBoxColumn();
                dgv_combbx_col.Width = 900;
                dgv_combbx_col.Items.Add(string.Format("{0}", RevOmate.Properties.Resources.MACRO_NAME_UNSETTING));
                for (int script_idx = 0; script_idx < Constants.SCRIPT_USER_USE_NUM; script_idx++)
                {
                    try
                    {
                        string set_script_name = RevOmate.Properties.Resources.MACRO_NAME_UNDEFINE;
                        if (my_script_info_datas.Script_Info_Datas[script_idx].Name != "")
                        {
                            set_script_name = my_script_info_datas.Script_Info_Datas[script_idx].Name;
                        }
                        dgv_combbx_col.Items.Add(string.Format("{0}{1:000}:{2}", RevOmate.Properties.Resources.MACRO_NAME_NO_MACRO, script_idx + 1, set_script_name));
                    }
                    catch
                    {
                    }
                }
                dgv_script_name.DataPropertyName = dgv_encoder_script.Columns["dgv_script_name"].DataPropertyName;
                //int add_idx = dgv_pattern.Columns["dgv_script_name"].Index;
                dgv_encoder_script.Columns.Insert(script_name_idx, dgv_combbx_col);
                dgv_encoder_script.Columns.Remove("dgv_script_name");
                dgv_encoder_script.Columns[script_name_idx].Name = "dgv_script_name";
                dgv_encoder_script.Columns[script_name_idx].HeaderText = RevOmate.Properties.Resources.ENCODER_SCRIPT_LIST_MACRO_NAME;

                // 選択肢を元に戻す
                for (int idx = 0; idx < my_encoder_script_datas.encoder_script_datas[encoder_script_select_no].script_no.Length; idx++)
                {

                    int select_idx = 0;
                    string select_script_name = "";
                    // 選択されていた項目を選択状態に戻す
                    //if (0 < sel_data_idx[pattern_idx] && sel_data_idx[pattern_idx] <= my_script_info_datas.Script_Info_Datas.Length)
                    //{
                    //    select_idx = sel_data_idx[pattern_idx];
                    //}
                    // 設定されている実行マクロを選択
                    select_idx = my_encoder_script_datas.encoder_script_datas[encoder_script_select_no].script_no[idx];
                    if (select_idx <= 0 || my_script_info_datas.Script_Info_Datas.Length < select_idx)
                    {   // マクロ未設定
                        select_idx = 0; // 未設定
                        select_script_name = string.Format("{0}", RevOmate.Properties.Resources.MACRO_NAME_UNSETTING);
                    }
                    else
                    {
                        if (my_script_info_datas.Script_Info_Datas[select_idx - 1].Name != "")
                        {   // マクロ名称設定済み
                            select_script_name = string.Format("{0}{1:000}:{2}", RevOmate.Properties.Resources.MACRO_NAME_NO_MACRO, select_idx, my_script_info_datas.Script_Info_Datas[select_idx - 1].Name);
                        }
                        else
                        {   // マクロ名称未設定
                            select_script_name = string.Format("{0}{1:000}:{2}", RevOmate.Properties.Resources.MACRO_NAME_NO_MACRO, select_idx, RevOmate.Properties.Resources.MACRO_NAME_UNDEFINE);
                        }
                    }
                    dgv_encoder_script.Rows[idx].Cells[script_name_idx].Value = select_script_name;
                }
            }
            catch
            {
            }
        }

        private void cmbbx_encoder_script_select_no_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (0 <= cmbbx_encoder_script_select_no.SelectedIndex && cmbbx_encoder_script_select_no.SelectedIndex < Constants.ENCODER_SCRIPT_NUM)
                {
                    encoder_script_select_no = cmbbx_encoder_script_select_no.SelectedIndex;
                    my_encoder_script_setting_disp(encoder_script_select_no, true);
                }
            }
            catch
            {
            }
        }
        
        private void lbl_sw_func_name_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    int sw_id = int.Parse(((System.Windows.Forms.Label)sender).Tag.ToString());

                    my_func_setting_disp(sw_id, advance_mode_flag);
                }
            }
            catch
            {

            }
        }

        private void ToolStripMenuItem_func_setting_Click(object sender, EventArgs e)
        {
            try
            {
                int sw_id = contextMenu_sw_id;

                my_func_setting_disp(sw_id, advance_mode_flag);

            }
            catch
            {

            }
        }

        private int my_func_setting_disp(int p_sw_id, bool p_advance_mode_flag)
        {
            int i_ret = 0;
            try
            {
                FuncSetting fs = new FuncSetting(this, button_setting_mode_select_no, p_sw_id, p_advance_mode_flag, my_app_sw_datas.mode[button_setting_mode_select_no].app_data[p_sw_id].select_data, my_app_sw_datas.mode[button_setting_mode_select_no].app_data[p_sw_id].data, sp_func_type_list, sp_func_type_no_list, my_script_info_datas, my_base_mode_infos, my_func_datas, my_sw_func_datas);
                DialogResult dr = fs.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {   // Submit
                    //MessageBox.Show("OK");
                    my_sw_func_name_disp(button_setting_mode_select_no, true);
                }
                else
                {   // Cancel
                    //MessageBox.Show("Cancel");
                }
                fs.Dispose();
            }
            catch
            {

            }
            return i_ret;
        }

        private void lbl_sw_func_name_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                contextMenu_sw_id = int.Parse(((System.Windows.Forms.Label)sender).Tag.ToString());

                toolTip1.SetToolTip(((System.Windows.Forms.Label)sender), ((System.Windows.Forms.Label)sender).Text);
            }
            catch
            {
            }
        }

        private void lbl_profile_select_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                int select_mode = int.Parse(((Label)sender).Tag.ToString());
                if (0 <= select_mode && select_mode < Constants.MODE_NUM)
                {

                    if (button_setting_mode_select_no != select_mode)
                    {   // 表示モード変更あり

                        // 変更前のモードの設定値を取得
                        //my_button_setting_get_by_disp(button_setting_mode_select_no);
                        // 変更後のモードへ
                        button_setting_mode_select_no = select_mode;
                        my_function_name_update();
                        my_button_setting_disp(button_setting_mode_select_no, true);
                        my_sw_func_name_disp(button_setting_mode_select_no, true);
                        //my_button_setting_macro_name_update();

                        my_profile_select_disp(button_setting_mode_select_no, true);
                    }

                }
            }
            catch
            {
            }
        }

        private void my_profile_select_disp(int p_select_mode_no, bool p_visible)
        {
            int tmp_alpha = 0;
            try
            {
                // MODE色設定
                for (int fi = 0; fi < my_profile_colors.Length; fi++)
                {
                    int[] tmp_LED_RGB = new int[Constants.LED_RGB_COLOR_NUM];
                    int[] tmp_Preview_RGB = new int[Constants.LED_RGB_COLOR_NUM];
                    tmp_LED_RGB[0] = my_base_mode_infos.base_mode_infos[fi].LED_RGB_duty[0] * 0xFF / my_LED_Duty_Max[0];
                    tmp_LED_RGB[1] = my_base_mode_infos.base_mode_infos[fi].LED_RGB_duty[1] * 0xFF / my_LED_Duty_Max[1];
                    tmp_LED_RGB[2] = my_base_mode_infos.base_mode_infos[fi].LED_RGB_duty[2] * 0xFF / my_LED_Duty_Max[2];

                    CommonFunc.my_LED_RGB_to_Preview_RGB(tmp_LED_RGB, tmp_Preview_RGB, out tmp_alpha);
                    my_profile_colors[fi].BackColor = Color.FromArgb(tmp_alpha, tmp_Preview_RGB[0], tmp_Preview_RGB[1], tmp_Preview_RGB[2]);

                    if (p_select_mode_no == fi)
                    {
                        Color set_color = Color.FromArgb(tmp_alpha, tmp_Preview_RGB[0], tmp_Preview_RGB[1], tmp_Preview_RGB[2]);
                        my_circle_draw(set_color);
                    }
                }
                // 機能色設定
                for (int fi = 0; fi < my_func_colors.Length; fi++)
                {
                    int[] tmp_LED_RGB = new int[Constants.LED_RGB_COLOR_NUM];
                    int[] tmp_Preview_RGB = new int[Constants.LED_RGB_COLOR_NUM];
                    tmp_LED_RGB[0] = my_func_datas.func_datas[p_select_mode_no].func_data[fi].LED_RGB_duty[0] * 0xFF / my_LED_Duty_Max[0];
                    tmp_LED_RGB[1] = my_func_datas.func_datas[p_select_mode_no].func_data[fi].LED_RGB_duty[1] * 0xFF / my_LED_Duty_Max[1];
                    tmp_LED_RGB[2] = my_func_datas.func_datas[p_select_mode_no].func_data[fi].LED_RGB_duty[2] * 0xFF / my_LED_Duty_Max[2];

                    CommonFunc.my_LED_RGB_to_Preview_RGB(tmp_LED_RGB, tmp_Preview_RGB, out tmp_alpha);
                    my_func_colors[fi].BackColor = Color.FromArgb(tmp_alpha, tmp_Preview_RGB[0], tmp_Preview_RGB[1], tmp_Preview_RGB[2]);
                }

                // 選択を太字に、ボーダー表示
                for (int fi = 0; fi < my_profile_select.Length; fi++)
                {
                    if (p_select_mode_no == fi)
                    {   // 選択　太字
                        my_profile_select[fi].Font = new Font(my_profile_select[fi].Font, FontStyle.Bold);
                        my_profile_border[fi].Visible = true;
                    }
                    else
                    {   // 非選択　標準
                        my_profile_select[fi].Font = new Font(my_profile_select[fi].Font, FontStyle.Regular);
                        my_profile_border[fi].Visible = false;
                    }
                }
            }
            catch
            {
            }
        }

        private void my_circle_draw(Color p_draw_color)
        {
            try
            {
                Graphics g = Graphics.FromImage(canvas);

                // LED点灯色を表示
                int pen_width = 18;
                Pen myPen = new Pen(Color.FromArgb(p_draw_color.A, p_draw_color.R, p_draw_color.G, p_draw_color.B), pen_width);
                // 枠を表示
                int pen_width_waku = 2;
                Pen myPen_waku = new Pen(Color.FromArgb(200, 230, 230, 230), pen_width_waku);

                g.DrawEllipse(myPen, pen_width, pen_width, pbx_mode_color.Width - (pen_width * 2), pbx_mode_color.Height - (pen_width * 2));
                // 外枠
                g.DrawEllipse(myPen_waku, pen_width - (pen_width / 2), pen_width - (pen_width / 2), pbx_mode_color.Width - (pen_width * 2) + pen_width, pbx_mode_color.Height - (pen_width * 2) + pen_width);
                // 内枠
                g.DrawEllipse(myPen_waku, pen_width + (pen_width / 2), pen_width + (pen_width / 2), pbx_mode_color.Width - (pen_width * 2) - pen_width, pbx_mode_color.Height - (pen_width * 2) - pen_width);

                g.Dispose();
                myPen.Dispose();
                myPen_waku.Dispose();

                pbx_mode_color.Image = canvas;
            }
            catch
            {
            }
        }

        private void lbl_profile_color_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                int select_mode = int.Parse(((Label)sender).Tag.ToString());
                my_led_color_setting_disp(select_mode, -1);
            }
            catch
            {
            }
        }

        private void lbl_func_color_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                int select_func_no = int.Parse(((Label)sender).Tag.ToString());
                my_led_color_setting_disp(button_setting_mode_select_no, select_func_no);
            }
            catch
            {
            }
        }

        public int my_led_color_setting_disp(int p_mode, int p_func_id)
        {
            int i_ret = 0;
            try
            {
                LEDColorSetting LEDcs = new LEDColorSetting(this, p_mode, p_func_id, my_base_mode_infos, my_func_datas);
                DialogResult dr = LEDcs.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {   // Submit
                    //MessageBox.Show("OK");
                    my_profile_select_disp(button_setting_mode_select_no, true);
                }
                else
                {   // Cancel
                    //MessageBox.Show("Cancel");
                }
                LEDcs.Dispose();
            }
            catch
            {

            }
            return i_ret;
        }

        public void my_LED_color_set_req_by_mode(int p_mode, byte[] p_rgb, byte p_brightness_level)
        {
            byte tmp_led_duty = 0;
            try
            {
                if (MessageBox.Show(RevOmate.Properties.Resources.DEVICE_BASE_SETUP_WRITE_CONFIRM_MSG, RevOmate.Properties.Resources.APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if (p_mode < Constants.MODE_NUM && p_rgb.Length == Constants.LED_RGB_COLOR_NUM && p_brightness_level < Constants.LED_BRIGHTNESS_LEVEL_SET_NUM)
                    {
                        // LED rgb colorセット
                        // LED設定
                        for (int fi = 0; fi < Constants.LED_RGB_COLOR_NUM && fi < p_rgb.Length; fi++)
                        {
                            tmp_led_duty = p_rgb[fi];
                            if (my_LED_Duty_Max[fi] < tmp_led_duty)
                            {
                                tmp_led_duty = my_LED_Duty_Max[fi];
                            }

                            my_base_mode_infos.base_mode_infos[p_mode].LED_RGB_duty[fi] = tmp_led_duty;
                        }
                        // LED輝度設定
                        my_base_mode_infos.base_mode_infos[p_mode].LED_brightness_level = p_brightness_level;

                        // 設定変更要求
                        set_base_info_flag = true;
                        // 書き換え要求セット
                        fData.Set_Flash_Write_Status(FlashControl.FM_WRITE_TYPE_BASE_SETTING, FlashControl.FM_WRITE_STATUS_RQ);
                    }
                }
            }
            catch
            {
            }
        }

        public void my_LED_color_set_req_by_func(int p_mode, int p_func_no, byte[] p_rgb, byte p_brightness_level)
        {
            byte tmp_led_duty = 0;
            try
            {
                if (MessageBox.Show(RevOmate.Properties.Resources.DEVICE_BASE_SETUP_WRITE_CONFIRM_MSG, RevOmate.Properties.Resources.APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if (p_mode < Constants.MODE_NUM && p_func_no < Constants.FUNCTION_NUM && p_rgb.Length == Constants.LED_RGB_COLOR_NUM && p_brightness_level < Constants.LED_BRIGHTNESS_LEVEL_SET_NUM)
                    {
                        // LED rgb colorセット
                        // LED設定
                        for (int fi = 0; fi < Constants.LED_RGB_COLOR_NUM && fi < p_rgb.Length; fi++)
                        {
                            tmp_led_duty = p_rgb[fi];
                            if (my_LED_Duty_Max[fi] < tmp_led_duty)
                            {
                                tmp_led_duty = my_LED_Duty_Max[fi];
                            }

                            my_func_datas.func_datas[p_mode].func_data[p_func_no].LED_RGB_duty[fi] = tmp_led_duty;
                        }
                        // LED輝度設定
                        my_func_datas.func_datas[p_mode].func_data[p_func_no].LED_brightness_level = p_brightness_level;

                        // 設定変更要求
                        set_func_setting_flag = true;
                        // 書き換え要求セット
                        fData.Set_Flash_Write_Status(FlashControl.FM_WRITE_TYPE_FUNCTION_SETTING, FlashControl.FM_WRITE_STATUS_RQ);
                    }
                }
            }
            catch
            {
            }
        }

        public void my_Button_Setting_Write_req_by_func_setting(byte p_mode, byte p_sw_no, byte p_encoder_default, byte p_category1_id, byte p_category2_id, byte p_set_val, byte[] p_sw_func_data)
        {
            try
            {
                if (p_mode < Constants.MODE_NUM && p_sw_no < Constants.BUTTON_NUM && p_encoder_default < Constants.FUNCTION_NUM)
                {
                    for (int fi = 0; fi < Constants.SW_FUNCTION_DATA_LEN; fi++)
                    {
                        my_sw_func_datas.sw_func_datas[p_mode].sw_func_data[p_sw_no].sw_data[fi] = 0;
                    }

                    switch (p_category1_id)
                    {
                        case Constants.APP_DATA_FUNC_CATEGORY_ID_NOT_SET:
                            my_base_mode_infos.base_mode_infos[p_mode].sw_sp_func_no[p_sw_no] = 0;
                            my_base_mode_infos.base_mode_infos[p_mode].sw_exe_script_no[p_sw_no] = 0;
                            break;
                        case Constants.APP_DATA_FUNC_CATEGORY_ID_GENERAL:
                            my_base_mode_infos.base_mode_infos[p_mode].sw_sp_func_no[p_sw_no] = (byte)(p_set_val + 1);
                            my_base_mode_infos.base_mode_infos[p_mode].sw_exe_script_no[p_sw_no] = 0;
                            break;
                        case Constants.APP_DATA_FUNC_CATEGORY_ID_MACRO:
                            my_base_mode_infos.base_mode_infos[p_mode].sw_sp_func_no[p_sw_no] = 0;
                            my_base_mode_infos.base_mode_infos[p_mode].sw_exe_script_no[p_sw_no] = (byte)(p_set_val + 1);
                            break;
                        case Constants.APP_DATA_FUNC_CATEGORY_ID_PRESET:
                            my_base_mode_infos.base_mode_infos[p_mode].sw_sp_func_no[p_sw_no] = 0;
                            my_base_mode_infos.base_mode_infos[p_mode].sw_exe_script_no[p_sw_no] = (byte)(Constants.SCRIPT_USER_USE_NUM + (Constants.BUTTON_NUM * p_mode) + p_sw_no + 1);
                            break;
                        case Constants.APP_DATA_FUNC_CATEGORY_ID_KEY:
                        case Constants.APP_DATA_FUNC_CATEGORY_ID_MOUSE:
                        case Constants.APP_DATA_FUNC_CATEGORY_ID_GAMEPAD:
                        case Constants.APP_DATA_FUNC_CATEGORY_ID_MULTIMEDIA:
                            my_base_mode_infos.base_mode_infos[p_mode].sw_sp_func_no[p_sw_no] = 0;
                            my_base_mode_infos.base_mode_infos[p_mode].sw_exe_script_no[p_sw_no] = 0;
                            for (int fi = 0; fi < Constants.SW_FUNCTION_DATA_LEN; fi++)
                            {
                                my_sw_func_datas.sw_func_datas[p_mode].sw_func_data[p_sw_no].sw_data[fi] = p_sw_func_data[fi];
                            }
                            break;
                        default:
                            break;
                    }

                    // エンコーダーボタン
                    if (p_sw_no == Constants.ENCODER_BUTTON_ID)
                    {
                        my_base_mode_infos.base_mode_infos[p_mode].encoder_func_no = p_encoder_default;
                    }

                    my_app_sw_datas.mode[p_mode].app_data[p_sw_no].select_data[Constants.APP_SW_DATA_SELECT_DATA_CATEGORY1_IDX] = p_category1_id;
                    my_app_sw_datas.mode[p_mode].app_data[p_sw_no].select_data[Constants.APP_SW_DATA_SELECT_DATA_CATEGORY2_IDX] = p_category2_id;
                    my_app_sw_datas.mode[p_mode].app_data[p_sw_no].select_data[Constants.APP_SW_DATA_SELECT_DATA_FUNC_LIST_IDX] = p_set_val;


                    // 設定変更要求
                    set_base_info_flag = true;
                    // 書き換え要求セット
                    fData.Set_Flash_Write_Status(FlashControl.FM_WRITE_TYPE_BASE_SETTING, FlashControl.FM_WRITE_STATUS_RQ);
                    //// 設定変更要求
                    //set_func_setting_flag = true;
                    //// 書き換え要求セット
                    //fData.Set_Flash_Write_Status(FlashControl.FM_WRITE_TYPE_FUNCTION_SETTING, FlashControl.FM_WRITE_STATUS_RQ);
                    // SW機能設定変更要求
                    set_sw_func_setting_flag = true;
                    // 書き換え要求セット
                    fData.Set_Flash_Write_Status(FlashControl.FM_WRITE_TYPE_SW_FUNCTION_SETTING, FlashControl.FM_WRITE_STATUS_RQ);
                }
            }
            catch
            {
            }
        }

        public void my_Function_Setting_Write_req_by_dial_func_setting(byte p_mode, byte p_func_no, SetData p_set_data, string[] p_function_name, STR_APP_FUNC_MODE p_app_func_mode_datas)
        {
            try
            {
                if (p_mode < Constants.MODE_NUM && p_func_no < Constants.FUNCTION_NUM && p_function_name.Length == Constants.FUNCTION_NUM)
                {
                    // 名称変更チェック
                    bool func_name_change_flag = false;
                    for (int fi = 0; fi < p_function_name.Length; fi++)
                    {
                        if (p_function_name[fi] != my_func_datas.func_datas[p_mode].func_data[fi].func_name)
                        {
                            my_func_datas.func_datas[p_mode].func_data[fi].func_name = p_function_name[fi];
                            func_name_change_flag = true;
                        }
                    }
                    // アプリ用データ変更チェック
                    bool app_data_change_flag = false;
                    if (my_app_func_datas.mode[p_mode].DataDiff(p_app_func_mode_datas) == true)
                    {   // 変更あり
                        // データコピー　変更フラグセット
                        my_app_func_datas.mode[p_mode].Copy(p_app_func_mode_datas);
                        app_data_change_flag = true;
                    }

                    if (my_set_data.DataChangeCheck(p_set_data, p_mode) == true || func_name_change_flag == true || app_data_change_flag == true)
                    {   // 変更あり

                        // データコピー
                        my_set_data.Copy(p_set_data);
                        my_device_data_set();
                        // 設定変更要求
                        set_func_setting_flag = true;
                        fData.Set_Flash_Write_Status(FlashControl.FM_WRITE_TYPE_FUNCTION_SETTING, FlashControl.FM_WRITE_STATUS_RQ);

                        p_mode++;
                    }
                    else
                    {
                        // 変更なし
                        p_mode++;
                    }
                }
            }
            catch
            {
            }
        }

        private int my_Preset_Data_File_Read(string p_file_path, ref ArrayList o_read_buff, ref byte o_script_mode)
        {
            int ret_val = -1;
            try
            {
                o_read_buff.Clear();
                o_script_mode = 0;

                if (File.Exists(p_file_path) == true)
                {
                    System.IO.FileStream fs = null;
                    try
                    {
                        int buff_max_size = Constants.SCRIPT_FILE_SIGNATURE_SIZE_LEN + Constants.SCRIPT_FILE_SIGNATURE_SIZE_MAX + Constants.SCRIPT_FILE_FILE_SIZE_LEN + Constants.SCRIPT_FILE_MODE_SIZE_LEN + Constants.SCRIPT_FILE_SCRIPT_SIZE_LEN + FlashControl.FM_SCRIPT_DATA_MAX_SIZE;
                        byte[] read_buff = new byte[buff_max_size];
                        fs = new System.IO.FileStream(p_file_path, FileMode.Open, FileAccess.Read);
                        if (fs.Length <= buff_max_size)
                        {
                            int read_size = fs.Read(read_buff, 0, (int)fs.Length);
                            if (read_size == fs.Length)
                            {
                                int sig_name_size = 0;
                                string sig_name = "";
                                int script_size = 0;
                                byte mode = 0;
                                // ファイル内容のチェック
                                if (my_Check_Script_File_Format(read_buff, read_size, ref sig_name_size, ref sig_name, ref script_size, ref mode) == true)
                                {
                                    ArrayList al_temp = new ArrayList();

                                    //// 読み込みデータ変換
                                    my_Convert_Byte_Array_2_Array_List(read_buff, Constants.SCRIPT_FILE_SIGNATURE_SIZE_LEN + sig_name_size + Constants.SCRIPT_FILE_FILE_SIZE_LEN + Constants.SCRIPT_FILE_MODE_SIZE_LEN + Constants.SCRIPT_FILE_SCRIPT_SIZE_LEN, script_size, ref al_temp);

                                    for (int fi = 0; fi < al_temp.Count; fi++)
                                    {
                                        o_read_buff.Add((byte)al_temp[fi]);
                                    }
                                    o_script_mode = mode;
                                    ret_val = al_temp.Count;
                                }
                                else
                                {   // スクリプトファイルのフォーマットエラー
                                    ret_val = -3;
                                }
                            }
                        }
                        else
                        {   // ファイルサイズ異常（大きい）
                            ret_val = -2;
                        }
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
            }
            catch
            {
            }
            return ret_val;
        }

        public void my_Preset_Write_req_by_func_setting(byte p_mode, byte p_sw_no, byte p_category1_id, byte p_category2_id, byte p_set_val)
        {
            try
            {
                // プリセットデータファイルチェック
                string preset_data_file_path = System.Environment.CurrentDirectory;
                string tmp_file_path = "";
                if (system_setting_info.Keyboard_Type == Constants.KEYBOARD_TYPE_JA)
                {
                    tmp_file_path = Constants.APP_DATA_FUNC_PRESET_DATA_PATH_JA + string.Format("{0:000}\\{1:000}", p_category2_id, p_set_val) + Constants.APP_DATA_FUNC_PRESET_DATA_FILE_EXTENSION;
                }
                else
                {
                    tmp_file_path = Constants.APP_DATA_FUNC_PRESET_DATA_PATH_US + string.Format("{0:000}\\{1:000}", p_category2_id, p_set_val) + Constants.APP_DATA_FUNC_PRESET_DATA_FILE_EXTENSION;
                }
                preset_data_file_path = Path.Combine(preset_data_file_path, tmp_file_path);
                if (File.Exists(preset_data_file_path) == true)
                {
                    // プリセットデータファイル読み込み
                    ArrayList preset_data = new ArrayList();
                    byte script_mode = 0;
                    int read_size = my_Preset_Data_File_Read(preset_data_file_path, ref preset_data, ref script_mode);
                    if (0 < read_size && preset_data.Count == read_size)
                    {   // プリセットデータ読み込み完了

                        // マクロ書き込み位置
                        int macro_write_idx = Constants.SCRIPT_USER_USE_NUM + (Constants.BUTTON_NUM * p_mode) + p_sw_no;

                        my_flash_read_write_buffer.clear();

                        // スクリプトサイズチェック
                        if (my_Flash_Write_Script_Size_Check(preset_data.Count, true) == false)
                        {   // Size OK

                            my_flash_read_write_buffer.set_write_data(preset_data, macro_write_idx);
                            my_flash_read_write_buffer.set_info(script_mode, "");
                            my_macro_name_set(macro_write_idx, "");

                            my_Flash_Write_Sector_Read_Address_Set(ref my_flash_read_write_buffer);

                            fData.Set_Flash_Write_Status(FlashControl.FM_WRITE_TYPE_SCRIPT_DATA, FlashControl.FM_WRITE_STATUS_RQ);
                        }
                    }
                    else
                    {
                        StatusBox_txtbx.Text = RevOmate.Properties.Resources.PRESET_DATA_FILE_READ_ERROR + "[" + read_size .ToString() + "][" + tmp_file_path + "]";
                    }
                }
                else
                {
                    StatusBox_txtbx.Text = RevOmate.Properties.Resources.PRESET_DATA_FILE_READ_ERROR + "[" + preset_data_file_path + "]";
                }
            }
            catch
            {
            }
        }

        private void lbl_Dial_func_name_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    int func_id = int.Parse(((System.Windows.Forms.Label)sender).Tag.ToString());

                    my_dial_func_setting_disp(func_id, advance_mode_flag);
                }
            }
            catch
            {
            }
        }

        private int my_dial_func_setting_disp(int p_func_id, bool p_advance_mode_flag)
        {
            int i_ret = 0;
            try
            {
                DialFuncSetting dfs = new DialFuncSetting(this, button_setting_mode_select_no, p_func_id, p_advance_mode_flag, my_app_func_datas, my_set_data, my_func_datas);
                DialogResult dr = dfs.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {   // Submit
                    //MessageBox.Show("OK");
                    my_sw_func_name_disp(button_setting_mode_select_no, true);
                }
                else
                {   // Cancel
                    //MessageBox.Show("Cancel");
                }
                dfs.Dispose();
            }
            catch
            {

            }
            return i_ret;
        }

        private void btn_debug_tab_disp_Click(object sender, EventArgs e)
        {
            try
            {
                tabControl1.BringToFront();
            }
            catch
            {
            }
        }

        private void btn_debug_macro_disp_Click(object sender, EventArgs e)
        {
            try
            {
                pnl_macro_editor.BringToFront();
            }
            catch
            {
            }
        }

        private void btn_debug_dial_macro_disp_Click(object sender, EventArgs e)
        {
            try
            {
                pnl_dial_macro_editor.BringToFront();
            }
            catch
            {
            }
        }

        private void btn_dial_macro_editor_Click(object sender, EventArgs e)
        {
            try
            {
                pnl_main.Visible = false;
                pnl_dial_macro_editor.Visible = true;
            }
            catch
            {
            }
        }

        private void btn_macro_editor_Click(object sender, EventArgs e)
        {
            try
            {
                pnl_main.Visible = false;
                pnl_macro_editor.Visible = true;
            }
            catch
            {
            }
        }

        private void btn_system_setup_Click(object sender, EventArgs e)
        {
            try
            {
                pnl_main.Visible = false;
                my_base_setting_disp(true);
                pnl_system_setup.Visible = true;
            }
            catch
            {
            }
        }

        private void btn_macro_editor_close_Click(object sender, EventArgs e)
        {
            try
            {
                // マクロ記録停止
                my_Script_Auto_Recored_Stop();

                //マクロ項目編集画面をすべて非表示とする
                gbx_Script_Add_Info.Visible = false;
                gbx_Script_Add_Info_JoysticButton.Visible = false;
                gbx_Script_Add_Info_JoysticLever.Visible = false;
                gbx_Script_Add_Info_Mouse.Visible = false;
                gbx_Script_Add_Info_MultiMedia.Visible = false;

                my_App_Setting_Data.Script_Add_Manual_Control = Constants.SCRIPT_ADD_MANUAL_NON;
                my_App_Setting_Data.Script_Edit_Item_Change_Flag = false;
                my_App_Setting_Data.Script_Drag_Target_idx = -1;

                pnl_macro_editor.Visible = false;
                pnl_main.Visible = true;
            }
            catch
            {
            }
        }

        private void btn_dial_macro_editor_submit_Click(object sender, EventArgs e)
        {
            try
            {
                // 実行スクリプトNoを変更後に、設定ボタンを押すと値が変更前のままなので、カレントセルを変更して確定させる
                if (dgv_encoder_script.CurrentCell != null)
                {
                    int x = dgv_encoder_script.CurrentCellAddress.X;
                    int y = dgv_encoder_script.CurrentCellAddress.Y;
                    dgv_encoder_script.CurrentCell = dgv_encoder_script[0, y];
                }

                if (MessageBox.Show(RevOmate.Properties.Resources.DEVICE_ENCODER_SCRIPT_WRITE_CONFIRM_MSG, RevOmate.Properties.Resources.APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if (my_encoder_script_setting_get_by_disp(encoder_script_select_no) == true)
                    {   // 変更あり
                        // 設定変更要求
                        set_encoder_script_flag = true;
                        fData.Set_Flash_Write_Status(FlashControl.FM_WRITE_TYPE_ENCODER_SCRIPT_SETTING, FlashControl.FM_WRITE_STATUS_RQ);
                    }
                    // 表示更新
                    my_encoder_script_setting_disp(encoder_script_select_no, true);
                }
            }
            catch
            {
            }
        }

        private void btn_dial_macro_editor_cancel_Click(object sender, EventArgs e)
        {
            try
            {
                pnl_dial_macro_editor.Visible = false;
                pnl_main.Visible = true;
            }
            catch
            {
            }
        }

        private void btn_system_setup_close_Click(object sender, EventArgs e)
        {
            try
            {
                pnl_system_setup.Visible = false;
                pnl_main.Visible = true;
            }
            catch
            {
            }
        }

        private void btn_system_backupfile_read_Click(object sender, EventArgs e)
        {
            try
            {
                string w_msg = RevOmate.Properties.Resources.RESTORE_WARNING_MSG;
                if (MessageBox.Show(w_msg, RevOmate.Properties.Resources.APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    w_msg = RevOmate.Properties.Resources.RESTORE_WARNING_MSG2 + "\n" + RevOmate.Properties.Resources.RESTORE_WARNING_MSG3;
                    if (MessageBox.Show(w_msg, RevOmate.Properties.Resources.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        ScriptFile_Import_openFileDialog.Title = RevOmate.Properties.Resources.BACKUP_FILE_OPEN_DIALOG_TITLE;
                        ScriptFile_Import_openFileDialog.DefaultExt = Constants.BACKUP_FILE_EXTENSION;
                        ScriptFile_Import_openFileDialog.Filter = RevOmate.Properties.Resources.BACKUP_FILE_FILTER;
                        ScriptFile_Import_openFileDialog.FileName = "";
                        if (ScriptFile_Import_openFileDialog.InitialDirectory == "")
                        {
                            ScriptFile_Import_openFileDialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                        }

                        if (ScriptFile_Import_openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            // プログレスバーの最大値を設定
                            int prog_max = 0;
                            // Flash消去の場合は、セクター数を加算
                            prog_max += (int)FlashControl.FM_SECTOR_NUM;
                            // Restoreの場合は、FlashMemoryの書き込み回数を加算
                            prog_max = prog_max + (((int)FlashControl.FM_TOTAL_SIZE / 0x100) * ((0x100 / (int)FlashControl.FM_USB_WRITE_DATA_SIZE) + 1));
                            my_progress_bar_display(true, 0, prog_max);
                            my_App_Setting_Data.Backup_Restore_Progress_Value = 0;
                            my_App_Setting_Data.Backup_Restore_Progress_Max_Value = prog_max;

                            my_App_Setting_Data.Backup_Restore_Error_Code = 0;
                            my_App_Setting_Data.Backup_file_Path = ScriptFile_Import_openFileDialog.FileName;
                            my_App_Setting_Data.Backup_Restore_Flag = Constants.BACKUP_FLAG_RESTORE;
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void btn_system_backupfile_save_Click(object sender, EventArgs e)
        {
            try
            {
                //ファイル名作成
                string file_name = "backup_";
                DateTime dt = DateTime.Now;

                //日付追加
                file_name += string.Format("{0:0000}{1:00}{2:00}_{3:00}{4:00}{5:00}", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
                ScriptFile_Save_saveFileDialog.FileName = file_name;

                if (ScriptFile_Save_saveFileDialog.InitialDirectory == "")
                {
                    ScriptFile_Save_saveFileDialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                }
                ScriptFile_Save_saveFileDialog.Title = RevOmate.Properties.Resources.BACKUP_FILE_SAVE_DIALOG_TITLE;
                ScriptFile_Save_saveFileDialog.DefaultExt = Constants.BACKUP_FILE_EXTENSION;
                ScriptFile_Save_saveFileDialog.Filter = RevOmate.Properties.Resources.BACKUP_FILE_FILTER;

                if (ScriptFile_Save_saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // プログレスバーの最大値を設定
                    int prog_max = 0;
                    // Backupの場合は、FlashMemoryの読み込み回数を加算
                    prog_max = prog_max + (int)(FlashControl.FM_TOTAL_SIZE / FlashControl.FM_USB_READ_DATA_SIZE) + 1;
                    my_progress_bar_display(true, 0, prog_max);
                    my_App_Setting_Data.Backup_Restore_Progress_Value = 0;
                    my_App_Setting_Data.Backup_Restore_Progress_Max_Value = prog_max;

                    my_App_Setting_Data.Backup_Restore_Error_Code = 0;
                    my_App_Setting_Data.Backup_file_Path = ScriptFile_Save_saveFileDialog.FileName;
                    my_App_Setting_Data.Backup_Restore_Flag = Constants.BACKUP_FLAG_BACKUP;
                }
            }
            catch
            {
            }
        }

        private void btn_system_default_set_Click(object sender, EventArgs e)
        {
            try
            {
#if true
                string w_msg = RevOmate.Properties.Resources.RESET_WARNING_MSG1;
                if (MessageBox.Show(w_msg, RevOmate.Properties.Resources.APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    w_msg = RevOmate.Properties.Resources.RESET_WARNING_MSG2 + "\n" + RevOmate.Properties.Resources.RESET_WARNING_MSG3;
                    if (MessageBox.Show(w_msg, RevOmate.Properties.Resources.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        // プログレスバーの最大値を設定
                        int prog_max = 0;
                        // Flash消去の場合は、セクター数を加算
                        prog_max += (int)FlashControl.FM_SECTOR_NUM;
                        // Restoreの場合は、FlashMemoryの書き込み回数を加算
                        prog_max = prog_max + (((int)FlashControl.FM_TOTAL_SIZE / 0x100) * ((0x100 / (int)FlashControl.FM_USB_WRITE_DATA_SIZE) + 1));
                        my_progress_bar_display(true, 0, prog_max);
                        my_App_Setting_Data.Backup_Restore_Progress_Value = 0;
                        my_App_Setting_Data.Backup_Restore_Progress_Max_Value = prog_max;

                        my_App_Setting_Data.Backup_Restore_Error_Code = 0;
                        string preset_data_file_path = System.Environment.CurrentDirectory;
                        preset_data_file_path = Path.Combine(preset_data_file_path, Constants.SYSTEM_FACTORY_PRESET_DATA_FILE_PATH);
                        my_App_Setting_Data.Backup_file_Path = preset_data_file_path;
                        my_App_Setting_Data.Backup_Restore_Flag = Constants.BACKUP_FLAG_RESET;
                    }
                }
#endif
#if false
                string w_msg = RevOmate.Properties.Resources.RESET_WARNING_MSG1;
                if (MessageBox.Show(w_msg, RevOmate.Properties.Resources.APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    w_msg = RevOmate.Properties.Resources.RESET_WARNING_MSG2 + "\n" + RevOmate.Properties.Resources.RESET_WARNING_MSG3;
                    if (MessageBox.Show(w_msg, RevOmate.Properties.Resources.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        my_progress_bar_display(true, 0, (int)FlashControl.FM_SECTOR_NUM);
                        my_App_Setting_Data.Backup_Restore_Progress_Value = 0;
                        my_App_Setting_Data.Backup_Restore_Progress_Max_Value = (int)FlashControl.FM_SECTOR_NUM;

                        my_App_Setting_Data.Backup_Restore_Error_Code = 0;
                        my_App_Setting_Data.Backup_Restore_Flag = Constants.BACKUP_FLAG_RESET;
                    }
                }
#endif
            }
            catch
            {
            }
        }

        private void btn_system_factory_set_Click(object sender, EventArgs e)
        {
            try
            {
                string w_msg = RevOmate.Properties.Resources.RESET_WARNING_MSG1;
                if (MessageBox.Show(w_msg, RevOmate.Properties.Resources.APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    w_msg = RevOmate.Properties.Resources.RESET_WARNING_MSG2 + "\n" + RevOmate.Properties.Resources.RESET_WARNING_MSG3;
                    if (MessageBox.Show(w_msg, RevOmate.Properties.Resources.APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        my_progress_bar_display(true, 0, (int)FlashControl.FM_SECTOR_NUM);
                        my_App_Setting_Data.Backup_Restore_Progress_Value = 0;
                        my_App_Setting_Data.Backup_Restore_Progress_Max_Value = (int)FlashControl.FM_SECTOR_NUM;

                        my_App_Setting_Data.Backup_Restore_Error_Code = 0;
                        my_App_Setting_Data.Backup_Restore_Flag = Constants.BACKUP_FLAG_RESET;
                    }
                }
            }
            catch
            {
            }
        }

        private void lbl_Dial_func_name_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                toolTip1.SetToolTip(((System.Windows.Forms.Label)sender), ((System.Windows.Forms.Label)sender).Text);
            }
            catch
            {
            }
        }

        private void lbl_Macro_Editor_Icon_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                toolTip1.SetToolTip(((System.Windows.Forms.Label)sender), ((System.Windows.Forms.Label)sender).Text);

                if(((System.Windows.Forms.Label)sender).ImageList != null)
                {
                    if ((((System.Windows.Forms.Label)sender).ImageIndex % 2) == 0)
                    {   // 偶数ならインクリメント
                        ((System.Windows.Forms.Label)sender).ImageIndex++;
                    }
                }
            }
            catch
            {
            }
        }

        private void lbl_Macro_Editor_Icon_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (((System.Windows.Forms.Label)sender).ImageList != null)
                {
                    if ((((System.Windows.Forms.Label)sender).ImageIndex % 2) == 1)
                    {   // 奇数ならインクリメント
                        ((System.Windows.Forms.Label)sender).ImageIndex--;
                    }
                }
            }
            catch
            {
            }
        }

        public void my_help_disp()
        {
            try
            {
                string now_cul = Thread.CurrentThread.CurrentUICulture.Name;
                string link_url = Constants.HELP_LINK_URL_FIRST;
                if (now_cul.IndexOf(Constants.CULTURE_CODE_NAME_JP_S) >= 0)
                {   // ja-JP
                    link_url += Constants.HELP_LINK_URL_JP;
                }
                else if (now_cul.IndexOf(Constants.CULTURE_CODE_NAME_CN_S) >= 0)
                {   // zh-CN

                    if (now_cul.IndexOf(Constants.CULTURE_CODE_NAME_TW) >= 0)
                    {   // TW
                        link_url += Constants.HELP_LINK_URL_TW;
                    }
                    else
                    {   // CN
                        link_url += Constants.HELP_LINK_URL_CN;
                    }
                }
                else if (now_cul.IndexOf(Constants.CULTURE_CODE_NAME_KR_S) >= 0)
                {   // ko-KR
                    link_url += Constants.HELP_LINK_URL_KR;
                }
                else
                {   // EN
                    link_url += Constants.HELP_LINK_URL_EN;
                }
                link_url += Constants.HELP_LINK_URL_LAST;

                System.Diagnostics.Process.Start(link_url);
            }
            catch
            {
            }
        }
        private void llbl_help_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                my_help_disp();
            }
            catch
            {
            }
        }

        private void llbl_system_setup_help_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                my_help_disp();
            }
            catch
            {
            }
        }

        private int my_system_setting_file_read(string file_name)
        {
            int ret = -1;
            try
            {
                // ファイル有無チェック
                if (File.Exists(file_name) == true)
                {
                    System.Xml.Serialization.XmlSerializer serializer = null;
                    System.IO.FileStream fs = null;
                    try
                    {
                        serializer = new System.Xml.Serialization.XmlSerializer(typeof(SystemSettingInfo));
                        fs = new System.IO.FileStream(file_name, System.IO.FileMode.Open);
                        SystemSettingInfo ssi_tmp = (SystemSettingInfo)serializer.Deserialize(fs);


                        //読み込み正常
                        system_setting_info = ssi_tmp;
                        ret = 0;
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
            }
            catch
            {
            }
            return ret;
        }

        private int my_system_setting_file_save(string file_name)
        {
            int ret = -1;
            try
            {
                System.Xml.Serialization.XmlSerializer serializer = null;
                System.IO.FileStream fs = null;

                try
                {
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(SystemSettingInfo));
                    fs = new System.IO.FileStream(file_name, System.IO.FileMode.Create);
                    serializer.Serialize(fs, system_setting_info);

                    ret = 0;
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
            catch
            {
            }
            return ret;
        }

        private void my_Script_Auto_Recored_Start()
        {
            try
            {
                my_App_Setting_Data.Script_Rec_Flag = true;
                //this.KeyPreview = true;
                lbl_REC_Btn.Text = RevOmate.Properties.Resources.MACRO_EDITOR_OPERATION_REC_STOP;

                sw_Script_Interval.Stop();
                sw_Script_Interval.Reset();
                sw_Script_Interval.Start();

                dgv_ScriptEditor.Focus();
            }
            catch
            {
            }
        }
        private void my_Script_Auto_Recored_Stop()
        {
            try
            {
                my_App_Setting_Data.Script_Rec_Flag = false;
                //this.KeyPreview = false;
                lbl_REC_Btn.Text = RevOmate.Properties.Resources.MACRO_EDITOR_OPERATION_REC_START;

                sw_Script_Interval.Stop();
            }
            catch
            {
            }
        }

        private void llbl_keyboard_setup_help_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                my_help_disp();
            }
            catch
            {
            }
        }

        private void btn_keyboard_setup_cancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (btn_keyboard_setup_cancel.Visible == true)
                {
                    pnl_system_setup.Visible = true;
                }
                else
                {   // 起動時
                    pnl_main.Visible = true;
                }
                pnl_keyboard_type_assist.Visible = false;
            }
            catch
            {
            }
        }

        private void btn_keyboard_setup_set_Click(object sender, EventArgs e)
        {
            try
            {
                if (keyboard_setup_assist_status == Constants.KEYBOARD_SETUP_ASSIST_STATUS_COMP)
                {
                    system_setting_info.Keyboard_Type = keyboard_setup_assist_set_type;
                }
                my_base_setting_keyboard_settin_update(true);

                if (btn_keyboard_setup_cancel.Visible == true)
                {
                    pnl_system_setup.Visible = true;
                }
                else
                {   // 起動時
                    pnl_main.Visible = true;
                }
                pnl_keyboard_type_assist.Visible = false;
            }
            catch
            {
            }
        }

        private void btn_keyboard_setup_assistant_Click(object sender, EventArgs e)
        {
            try
            {
                keyboard_setup_display(false);
            }
            catch
            {
            }
        }

        private void keyboard_setup_display(bool p_start_up_flag)
        {
            try
            {
                System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
                Bitmap bmp = new Bitmap(myAssembly.GetManifestResourceStream("RevOmate.Resources.KEY_A.png"));
                pic_keyboard_setup_assist.Image = bmp;
                pic_keyboard_setup_assist.Visible = true;
                gbx_keyboard_setup_assis_complete.Visible = false;
                lbl_keyboard_setup_assist_msg1.Text = RevOmate.Properties.Resources.KEYBOARD_SETUP_ASSIST_MSG2;
                keyboard_setup_assist_status = Constants.KEYBOARD_SETUP_ASSIST_STATUS_KEY1;

                if (p_start_up_flag == true)
                {   // 起動時は設定ボタンのみ表示して、キャンセルボタンは非表示とする
                    btn_keyboard_setup_cancel.Visible = false;
                }
                else
                {
                    btn_keyboard_setup_cancel.Visible = true;
                }

                pnl_keyboard_type_assist.Visible = true;
                pnl_system_setup.Visible = false;
                pnl_main.Visible = false;
            }
            catch
            {
            }
        }

        private void rbtn_keyboard_setup_assist_type_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (((RadioButton)sender).Checked == true)
                {
                    byte set_keyboard_type = (byte)(int.Parse(((RadioButton)sender).Tag.ToString()) & 0xFF);
                    keyboard_setup_assist_set_type = set_keyboard_type;
                }
            }
            catch
            {
            }
        }
























        //-------------------------------------------------------END CUT AND PASTE BLOCK-------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------


    } //public partial class Form1 : Form


    [Serializable()]
    public class SetData
    {
        [System.Xml.Serialization.XmlElement("INDEX")]
        public int[] index;                     // インデックス
        [System.Xml.Serialization.XmlElement("SETTING_DATA")]
        public byte[,] setting_data;            // 設定データ
        [System.Xml.Serialization.XmlElement("SET_TYPE")]
        public byte[] set_type;                 // 設定タイプ
        [System.Xml.Serialization.XmlElement("MOUSE_DATA")]
        public byte[,] mouse_data;              // マウスデータ
        [System.Xml.Serialization.XmlElement("KEYBOARD_DATA")]
        public byte[,] keyboard_data;           // キーボードデータ
        [System.Xml.Serialization.XmlElement("JOYPAD_DATA")]
        public byte[,] joypad_data;             // ジョイスティックデータ
        [System.Xml.Serialization.XmlElement("MULTIMEDIA_DATA")]
        public byte[,] multimedia_data;         // マルチメディアデータ
        [System.Xml.Serialization.XmlElement("SENSITIVITY")]
        public byte[] sensitivity;              // 感度

        public SetData()
        {
            int fi, fj;
            try
            {
                index = new int[Constants.MODE_NUM * Constants.FUNCTION_NUM * Constants.CW_CCW_NUM];
                setting_data = new byte[Constants.MODE_NUM * Constants.FUNCTION_NUM * Constants.CW_CCW_NUM, Constants.DEVICE_DATA_LEN];
                set_type = new byte[Constants.MODE_NUM * Constants.FUNCTION_NUM * Constants.CW_CCW_NUM];
                mouse_data = new byte[Constants.MODE_NUM * Constants.FUNCTION_NUM * Constants.CW_CCW_NUM, Constants.MOUSE_DATA_LEN];
                keyboard_data = new byte[Constants.MODE_NUM * Constants.FUNCTION_NUM * Constants.CW_CCW_NUM, Constants.KEYBOARD_DATA_LEN];
                joypad_data = new byte[Constants.MODE_NUM * Constants.FUNCTION_NUM * Constants.CW_CCW_NUM, Constants.JOYPAD_DATA_LEN];
                multimedia_data = new byte[Constants.MODE_NUM * Constants.FUNCTION_NUM * Constants.CW_CCW_NUM, Constants.MULTIMEDIA_DATA_LEN];
                sensitivity = new byte[Constants.MODE_NUM * Constants.FUNCTION_NUM * Constants.CW_CCW_NUM];

                for (fi = 0; fi < (Constants.MODE_NUM * Constants.FUNCTION_NUM * Constants.CW_CCW_NUM); fi++)
                {
                    index[fi] = fi;
                    for (fj = 0; fj < Constants.DEVICE_DATA_LEN; fj++)
                    {
                        setting_data[fi, fj] = 0;
                    }
                    set_type[fi] = Constants.SET_TYPE_NONE;
                    for (fj = 0; fj < Constants.MOUSE_DATA_LEN; fj++)
                    {
                        mouse_data[fi, fj] = 0;
                    }
                    for (fj = 0; fj < Constants.KEYBOARD_DATA_LEN; fj++)
                    {
                        keyboard_data[fi, fj] = 0;
                    }
                    for (fj = 0; fj < Constants.JOYPAD_DATA_LEN; fj++)
                    {
                        joypad_data[fi, fj] = 0;
                    }
                    for (fj = 0; fj < Constants.MULTIMEDIA_DATA_LEN; fj++)
                    {
                        multimedia_data[fi, fj] = 0;
                    }
                    sensitivity[fi] = Constants.SENSITIVITY_DEFAULT;
                }
            }
            catch
            {
            }
        }

        public int Init()
        {
            int ret = 0;
            int fi, fj;
            try
            {
                for (fi = 0; fi < (Constants.MODE_NUM * Constants.FUNCTION_NUM * Constants.CW_CCW_NUM); fi++)
                {
                    index[fi] = fi;
                    for (fj = 0; fj < Constants.DEVICE_DATA_LEN; fj++)
                    {
                        setting_data[fi, fj] = 0;
                    }
                    set_type[fi] = Constants.SET_TYPE_NONE;
                    for (fj = 0; fj < Constants.MOUSE_DATA_LEN; fj++)
                    {
                        mouse_data[fi, fj] = 0;
                    }
                    for (fj = 0; fj < Constants.KEYBOARD_DATA_LEN; fj++)
                    {
                        keyboard_data[fi, fj] = 0;
                    }
                    for (fj = 0; fj < Constants.JOYPAD_DATA_LEN; fj++)
                    {
                        joypad_data[fi, fj] = 0;
                    }
                    for (fj = 0; fj < Constants.MULTIMEDIA_DATA_LEN; fj++)
                    {
                        multimedia_data[fi, fj] = 0;
                    }
                    sensitivity[fi] = Constants.SENSITIVITY_DEFAULT;
                }
            }
            catch
            {
            }
            return ret;
        }
        public int Dispose()
        {
            int ret = 0;
            try
            {
            }
            catch
            {
            }
            return ret;
        }

        public int Copy(SetData p_CopyData)
        {
            int ret = 0;
            try
            {
                // index
                if (index.Length == p_CopyData.index.Length)
                {
                    for (int fi = 0; fi < index.Length; fi++)
                    {
                        index[fi] = p_CopyData.index[fi];
                    }
                }
                else
                {
                    ret = -1;
                }
                // setting_data
                if (setting_data.Length == p_CopyData.setting_data.Length)
                {
                    for (int fi = 0; fi < setting_data.GetLength(0); fi++)
                    {
                        for (int fj = 0; fj < setting_data.GetLength(1); fj++)
                        {
                            setting_data[fi, fj] = p_CopyData.setting_data[fi, fj];
                        }
                    }
                }
                else
                {
                    ret = -1;
                }
                // set_type
                if (set_type.Length == p_CopyData.set_type.Length)
                {
                    for (int fi = 0; fi < set_type.Length; fi++)
                    {
                        set_type[fi] = p_CopyData.set_type[fi];
                    }
                }
                else
                {
                    ret = -1;
                }
                // mouse_data
                if (mouse_data.Length == p_CopyData.mouse_data.Length)
                {
                    for (int fi = 0; fi < mouse_data.GetLength(0); fi++)
                    {
                        for (int fj = 0; fj < mouse_data.GetLength(1); fj++)
                        {
                            mouse_data[fi, fj] = p_CopyData.mouse_data[fi, fj];
                        }
                    }
                }
                else
                {
                    ret = -1;
                }
                // keyboard_data
                if (keyboard_data.Length == p_CopyData.keyboard_data.Length)
                {
                    for (int fi = 0; fi < keyboard_data.GetLength(0); fi++)
                    {
                        for (int fj = 0; fj < keyboard_data.GetLength(1); fj++)
                        {
                            keyboard_data[fi, fj] = p_CopyData.keyboard_data[fi, fj];
                        }
                    }
                }
                else
                {
                    ret = -1;
                }
                // joypad_data
                if (joypad_data.Length == p_CopyData.joypad_data.Length)
                {
                    for (int fi = 0; fi < joypad_data.GetLength(0); fi++)
                    {
                        for (int fj = 0; fj < joypad_data.GetLength(1); fj++)
                        {
                            joypad_data[fi, fj] = p_CopyData.joypad_data[fi, fj];
                        }
                    }
                }
                else
                {
                    ret = -1;
                }
                // multimedia_data
                if (multimedia_data.Length == p_CopyData.multimedia_data.Length)
                {
                    for (int fi = 0; fi < multimedia_data.GetLength(0); fi++)
                    {
                        for (int fj = 0; fj < multimedia_data.GetLength(1); fj++)
                        {
                            multimedia_data[fi, fj] = p_CopyData.multimedia_data[fi, fj];
                        }
                    }
                }
                else
                {
                    ret = -1;
                }
                // sensitivity
                if (sensitivity.Length == p_CopyData.sensitivity.Length)
                {
                    for (int fi = 0; fi < sensitivity.Length; fi++)
                    {
                        sensitivity[fi] = p_CopyData.sensitivity[fi];
                    }
                }
                else
                {
                    ret = -1;
                }
            }
            catch
            {
            }
            return ret;
        }
        // データに変更があるかチェックする
        // 変更なし : false
        // 変更あり : true
        public bool DataChangeCheck(SetData p_CopyData, byte p_mode_no)
        {
            bool ret_b = false;
            try
            {
                // チェックするインデックスmin, 繰り返し回数
                int check_idx_min = (p_mode_no * Constants.FUNCTION_NUM * Constants.CW_CCW_NUM);
                int check_idx_num = Constants.FUNCTION_NUM * Constants.CW_CCW_NUM;
                // index
                if (index.Length == p_CopyData.index.Length && (check_idx_min + check_idx_num) <= index.Length)
                {
                    for (int fi = check_idx_min; fi < (check_idx_min + check_idx_num); fi++)
                    {
                        if(index[fi] != p_CopyData.index[fi])
                        {
                            ret_b = true;
                            break;
                        }
                    }
                }
                // setting_data
                if (setting_data.Length == p_CopyData.setting_data.Length && (check_idx_min + check_idx_num) <= setting_data.Length)
                {
                    for (int fi = check_idx_min; fi < (check_idx_min + check_idx_num); fi++)
                    {
                        for (int fj = 0; fj < setting_data.GetLength(1); fj++)
                        {
                            if(setting_data[fi, fj] != p_CopyData.setting_data[fi, fj])
                            {
                                ret_b = true;
                                break;
                            }
                        }
                        if(ret_b == true)
                        {
                            break;
                        }
                    }
                }
                // set_type
                if (set_type.Length == p_CopyData.set_type.Length && (check_idx_min + check_idx_num) <= set_type.Length)
                {
                    for (int fi = check_idx_min; fi < (check_idx_min + check_idx_num); fi++)
                    {
                        if (set_type[fi] != p_CopyData.set_type[fi])
                        {
                            ret_b = true;
                            break;
                        }
                    }
                }
                // mouse_data
                if (mouse_data.Length == p_CopyData.mouse_data.Length && (check_idx_min + check_idx_num) <= mouse_data.Length)
                {
                    for (int fi = check_idx_min; fi < (check_idx_min + check_idx_num); fi++)
                    {
                        for (int fj = 0; fj < mouse_data.GetLength(1); fj++)
                        {
                            if (mouse_data[fi, fj] != p_CopyData.mouse_data[fi, fj])
                            {
                                ret_b = true;
                                break;
                            }
                        }
                        if (ret_b == true)
                        {
                            break;
                        }
                    }
                }
                // keyboard_data
                if (keyboard_data.Length == p_CopyData.keyboard_data.Length && (check_idx_min + check_idx_num) <= keyboard_data.Length)
                {
                    for (int fi = check_idx_min; fi < (check_idx_min + check_idx_num); fi++)
                    {
                        for (int fj = 0; fj < keyboard_data.GetLength(1); fj++)
                        {
                            if (keyboard_data[fi, fj] != p_CopyData.keyboard_data[fi, fj])
                            {
                                ret_b = true;
                                break;
                            }
                        }
                        if (ret_b == true)
                        {
                            break;
                        }
                    }
                }
                // joypad_data
                if (joypad_data.Length == p_CopyData.joypad_data.Length && (check_idx_min + check_idx_num) <= joypad_data.Length)
                {
                    for (int fi = check_idx_min; fi < (check_idx_min + check_idx_num); fi++)
                    {
                        for (int fj = 0; fj < joypad_data.GetLength(1); fj++)
                        {
                            if(joypad_data[fi, fj] != p_CopyData.joypad_data[fi, fj])
                            {
                                ret_b = true;
                                break;
                            }
                        }
                        if (ret_b == true)
                        {
                            break;
                        }
                    }
                }
                // multimedia_data
                if (multimedia_data.Length == p_CopyData.multimedia_data.Length && (check_idx_min + check_idx_num) <= multimedia_data.Length)
                {
                    for (int fi = check_idx_min; fi < (check_idx_min + check_idx_num); fi++)
                    {
                        for (int fj = 0; fj < multimedia_data.GetLength(1); fj++)
                        {
                            if (multimedia_data[fi, fj] != p_CopyData.multimedia_data[fi, fj])
                            {
                                ret_b = true;
                                break;
                            }
                        }
                        if (ret_b == true)
                        {
                            break;
                        }
                    }
                }
                // sensitivity
                if (sensitivity.Length == p_CopyData.sensitivity.Length && (check_idx_min + check_idx_num) <= sensitivity.Length)
                {
                    for (int fi = check_idx_min; fi < (check_idx_min + check_idx_num); fi++)
                    {
                        if (sensitivity[fi] != p_CopyData.sensitivity[fi])
                        {
                            ret_b = true;
                            break;
                        }
                    }
                }
            }
            catch
            {
            }
            return ret_b;
        }

        // デバイスデータをセット
        public int device_data_set(int p_idx, byte[,] p_device_data)
        {
            int ret = 0;
            try
            {
                if (setting_data.GetLength(0) == p_device_data.GetLength(0) && setting_data.GetLength(1) == p_device_data.GetLength(1))
                {
                    for (int fi = 0; fi < setting_data.GetLength(0); fi++)
                    {
                        for (int fj = 0; fj < setting_data.GetLength(1); fj++)
                        {
                            if ((fi * setting_data.GetLength(1) + fj) < p_device_data.Length)
                            {
                                setting_data[fi, fj] = p_device_data[fi, fj];
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            return ret;
        }

        // デバイスデータを取得
        public int device_data_get(int p_idx, ref byte[,] p_device_data)
        {
            int ret = 0;
            try
            {
                if (setting_data.GetLength(0) == p_device_data.GetLength(0) && setting_data.GetLength(1) == p_device_data.GetLength(1))
                {
                    for (int fi = 0; fi < setting_data.GetLength(0); fi++)
                    {
                        for (int fj = 0; fj < setting_data.GetLength(1); fj++)
                        {
                            if ((fi * setting_data.GetLength(1) + fj) < p_device_data.Length)
                            {
                                p_device_data[fi, fj] = setting_data[fi, fj];
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            return ret;
        }

        public int data_clear(int p_idx)
        {
            int ret = 0;
            int fj;
            try
            {
                if (0 <= p_idx && p_idx < (Constants.MODE_NUM * Constants.FUNCTION_NUM * Constants.CW_CCW_NUM))
                {
                    for (fj = 0; fj < Constants.DEVICE_DATA_LEN; fj++)
                    {
                        setting_data[p_idx, fj] = 0;
                    }
                    set_type[p_idx] = Constants.SET_TYPE_NONE;
                    for (fj = 0; fj < Constants.MOUSE_DATA_LEN; fj++)
                    {
                        mouse_data[p_idx, fj] = 0;
                    }
                    for (fj = 0; fj < Constants.KEYBOARD_DATA_LEN; fj++)
                    {
                        keyboard_data[p_idx, fj] = 0;
                    }
                    for (fj = 0; fj < Constants.JOYPAD_DATA_LEN; fj++)
                    {
                        joypad_data[p_idx, fj] = 0;
                    }
                    for (fj = 0; fj < Constants.MULTIMEDIA_DATA_LEN; fj++)
                    {
                        multimedia_data[p_idx, fj] = 0;
                    }
                    sensitivity[p_idx] = Constants.SENSITIVITY_DEFAULT;
                }
                else
                {
                    ret = 1;
                }
            }
            catch
            {
            }
            return ret;
        }

        public int device_data_clear(int p_idx)
        {
            int ret = 0;
            int fj;
            try
            {
                if (0 <= p_idx && p_idx < (Constants.MODE_NUM * Constants.FUNCTION_NUM * Constants.CW_CCW_NUM))
                {
                    for (fj = 0; fj < Constants.DEVICE_DATA_LEN; fj++)
                    {
                        setting_data[p_idx, fj] = 0;
                    }
                }
                else
                {
                    ret = 1;
                }
            }
            catch
            {
            }
            return ret;
        }

        public int disp_data_clear(int p_idx)
        {
            int ret = 0;
            int fj;
            try
            {
                if (0 <= p_idx && p_idx < (Constants.MODE_NUM * Constants.FUNCTION_NUM * Constants.CW_CCW_NUM))
                {
                    set_type[p_idx] = Constants.SET_TYPE_NONE;
                    for (fj = 0; fj < Constants.MOUSE_DATA_LEN; fj++)
                    {
                        mouse_data[p_idx, fj] = 0;
                    }
                    for (fj = 0; fj < Constants.KEYBOARD_DATA_LEN; fj++)
                    {
                        keyboard_data[p_idx, fj] = 0;
                    }
                    for (fj = 0; fj < Constants.JOYPAD_DATA_LEN; fj++)
                    {
                        joypad_data[p_idx, fj] = 0;
                    }
                    for (fj = 0; fj < Constants.MULTIMEDIA_DATA_LEN; fj++)
                    {
                        multimedia_data[p_idx, fj] = 0;
                    }
                    sensitivity[p_idx] = Constants.SENSITIVITY_DEFAULT;
                }
                else
                {
                    ret = 1;
                }
            }
            catch
            {
            }
            return ret;
        }

        public int ir_data_all_clear()
        {
            int ret = 0;
            try
            {
                Init();
            }
            catch
            {
            }
            return ret;
        }
    }

    static class Constants
    {
        //public const string APPLICATION_NAME = "Rev-O-mate";        /* アプリケーション名 */
        public const string SHORTCUT_FILE_NAME = "Rev-O-mate.lnk";    /* ショートカットファイル名 */
        public const string SHORTCUT_STARTUP_PARAM = "/s";
        public const string SYSTEM_SETTING_FILE_NAME = "Rev-O-mate.xml";
        public const string SYSTEM_FACTORY_PRESET_DATA_FILE_PATH = "presetdata\\factory_preset.mbf";   /* アプリ用データ 機能設定プリセットデータ格納パス 日本語キーボード用 */
        public const byte MODE_NUM = 3;                     /* Mode 数 */
        public const int MODE_1_ID = 0;
        public const int MODE_2_ID = 1;
        public const int MODE_3_ID = 2;
        public const byte FUNCTION_NUM = 4;                 /* 機能設定 数 */
        public const int FUNCTION_1_ID = 0;
        public const int FUNCTION_2_ID = 1;
        public const int FUNCTION_3_ID = 2;
        public const int FUNCTION_4_ID = 3;
        public const byte CW_CCW_NUM = 2;                   /* 回転方向 数 */
        public const byte FUNCTION_CWCCW_DATA_LEN = 8;      /* 機能設定 左右回転データ長 */
        public const byte SENSITIVITY_MIN = 1;              /* 感度設定最小値 */
        public const byte SENSITIVITY_MAX = 100;            /* 感度設定最大値 */
        public const byte SENSITIVITY_DEFAULT = 100;        /* 感度デフォルト値 */
        public const byte SP_FUNC_NUM = 13;                 /* 特殊機能最大数 */
        public const byte SP_FUNC_MODE = 1;                 /* 特殊機能 モード変更 */
        public const byte SP_FUNC_MODE1 = 2;                /* 特殊機能 Mode1 */
        public const byte SP_FUNC_MODE2 = 3;                /* 特殊機能 Mode2 */
        public const byte SP_FUNC_MODE3 = 4;                /* 特殊機能 Mode3 */
        public const byte SP_FUNC_FUNC1 = 5;                /* 特殊機能 エンコーダー機能1 */
        public const byte SP_FUNC_FUNC2 = 6;                /* 特殊機能 エンコーダー機能2 */
        public const byte SP_FUNC_FUNC3 = 7;                /* 特殊機能 エンコーダー機能3 */
        public const byte SP_FUNC_FUNC4 = 8;                /* 特殊機能 エンコーダー機能4 */
        public const byte SP_FUNC_FUNC1TEMP = 9;            /* 特殊機能 エンコーダー機能1(押している間) */
        public const byte SP_FUNC_FUNC2TEMP = 10;           /* 特殊機能 エンコーダー機能2(押している間) */
        public const byte SP_FUNC_FUNC3TEMP = 11;           /* 特殊機能 エンコーダー機能3(押している間) */
        public const byte SP_FUNC_FUNC4TEMP = 12;           /* 特殊機能 エンコーダー機能4(押している間) */
        public const byte SP_FUNC_FUNC = 13;                /* 特殊機能 エンコーダー機能変更 */
        public const byte ENCODER_SCRIPT_NUM = 3;           /* エンコーダースクリプト設定数 */

        public const byte SW_FUNCTION_DATA_LEN = 8;         /* SW機能設定 データ長 */

        // アプリ用データ　SW用
        public const int APP_SW_DATA_SELECT_DATA_LEN                = 3;    /* アプリ用データ 選択データサイズ */
        public const int APP_SW_DATA_SELECT_DATA_CATEGORY1_IDX      = 0;    /* アプリ用データ 選択データ カテゴリ選択インデックス */
        public const int APP_SW_DATA_SELECT_DATA_CATEGORY2_IDX      = 1;    /* アプリ用データ 選択データ カテゴリ選択インデックス */
        public const int APP_SW_DATA_SELECT_DATA_FUNC_LIST_IDX      = 2;    /* アプリ用データ 選択データ 機能リスト選択インデックス */
        public const int APP_SW_DATA_DATA_LEN                       = 8;    /* アプリ用データ 選択データサイズ */
        // アプリ用データ　FUNC用
        public const int APP_FUNC_DATA_SELECT_DATA_LEN              = 5;    /* アプリ用データ 選択データサイズ */
        public const int APP_FUNC_DATA_SELECT_DATA_CATEGORY1_IDX    = 0;    /* アプリ用データ 選択データ カテゴリ選択インデックス */
        public const int APP_FUNC_DATA_SELECT_DATA_CATEGORY2_IDX    = 1;    /* アプリ用データ 選択データ カテゴリ選択インデックス */
        public const int APP_FUNC_DATA_SELECT_DATA_FUNC_LIST_IDX    = 2;    /* アプリ用データ 選択データ 機能リスト選択インデックス */
        public const int APP_FUNC_DATA_SELECT_DATA_4_IDX            = 3;    /* アプリ用データ 選択データ 4インデックス */
        public const int APP_FUNC_DATA_SELECT_DATA_5_IDX            = 4;    /* アプリ用データ 選択データ 5インデックス */

        // 機能設定　カテゴリ定義
        public const byte APP_DATA_FUNC_CATEGORY_ID_NUM                 = 8;        /* アプリ用データ 機能設定カテゴリID 設定数 */
        public const byte APP_DATA_FUNC_CATEGORY_ID_NOT_SET             = 0;        /* アプリ用データ 機能設定カテゴリID NOT SET */
        public const byte APP_DATA_FUNC_CATEGORY_ID_GENERAL             = 1;        /* アプリ用データ 機能設定カテゴリID GENERAL */
        public const byte APP_DATA_FUNC_CATEGORY_ID_MACRO               = 2;        /* アプリ用データ 機能設定カテゴリID MACRO */
        public const byte APP_DATA_FUNC_CATEGORY_ID_PRESET              = 3;        /* アプリ用データ 機能設定カテゴリID PRESET */
        public const byte APP_DATA_FUNC_CATEGORY_ID_KEY                 = 4;        /* アプリ用データ 機能設定カテゴリID KEY */
        public const byte APP_DATA_FUNC_CATEGORY_ID_MOUSE               = 5;        /* アプリ用データ 機能設定カテゴリID MOUSE */
        public const byte APP_DATA_FUNC_CATEGORY_ID_GAMEPAD             = 6;        /* アプリ用データ 機能設定カテゴリID GAMEPAD */
        public const byte APP_DATA_FUNC_CATEGORY_ID_MULTIMEDIA          = 7;        /* アプリ用データ 機能設定カテゴリID MULTIMEDIA */
        //public const byte APP_DATA_FUNC_CATEGORY_ID_PRESET_PHOTOSHOP    = 3;     /* アプリ用データ 機能設定カテゴリID PRESET PHOTOSHOP */
        //public const byte APP_DATA_FUNC_CATEGORY_ID_PRESET_SAI          = 4;     /* アプリ用データ 機能設定カテゴリID PRESET SAI */
        //public const byte APP_DATA_FUNC_CATEGORY_ID_PRESET_CLIPSTUDIO   = 5;     /* アプリ用データ 機能設定カテゴリID PRESET CLIPSTUDIO */
        //public const byte APP_DATA_FUNC_CATEGORY_ID_KEY = 1;     /* アプリ用データ 機能設定カテゴリID KEY */
        //public const byte APP_DATA_FUNC_CATEGORY_ID_MOUSE = 2;     /* アプリ用データ 機能設定カテゴリID MOUSE */
        //public const byte APP_DATA_FUNC_CATEGORY_ID_GAMEPAD = 3;     /* アプリ用データ 機能設定カテゴリID GAMEPAD */
        //public const byte APP_DATA_FUNC_CATEGORY_ID_MULTIMEDIA = 4;     /* アプリ用データ 機能設定カテゴリID MULTIMEDIA */
        //public const byte APP_DATA_FUNC_CATEGORY_ID_TOOL = 5;     /* アプリ用データ 機能設定カテゴリID TOOL */
        //public const byte APP_DATA_FUNC_CATEGORY_ID_MODFILE = 6;     /* アプリ用データ 機能設定カテゴリID MODFILE */
        //public const byte APP_DATA_FUNC_CATEGORY_ID_USERAREA = 7;     /* アプリ用データ 機能設定カテゴリID USERAREA */
        // 機能設定　プリセット定義
        public const string APP_DATA_FUNC_PRESET_DATA_PATH_JA = "presetdata\\JA\\macro_sw\\";   /* アプリ用データ 機能設定プリセットデータ格納パス 日本語キーボード用 */
        public const string APP_DATA_FUNC_PRESET_DATA_PATH_US = "presetdata\\US\\macro_sw\\";   /* アプリ用データ 機能設定プリセットデータ格納パス USキーボード用 */
        public const string APP_DATA_FUNC_PRESET_DATA_FILE_EXTENSION = ".mcdf";
        public const byte APP_DATA_FUNC_PRESET_ID_NUM                       = 5;    /* アプリ用データ 機能設定プリセットID 設定数 */
        public const byte APP_DATA_FUNC_PRESET_ID_PHOTOSHOP                 = 0;    /* アプリ用データ 機能設定プリセットID PhotoShop */
        public const byte APP_DATA_FUNC_PRESET_ID_CLIPSTUDIO                = 1;    /* アプリ用データ 機能設定プリセットID ClipStudio */
        public const byte APP_DATA_FUNC_PRESET_ID_SAI                       = 2;    /* アプリ用データ 機能設定プリセットID SAI */
        public const byte APP_DATA_FUNC_PRESET_ID_ILLUSTRATOR               = 3;    /* アプリ用データ 機能設定プリセットID Illustrator */
        public const byte APP_DATA_FUNC_PRESET_ID_LIGHTROOM                 = 4;    /* アプリ用データ 機能設定プリセットID Lightroom */
        public const byte APP_DATA_FUNC_PRESET_ID_MULTIMEDIA                = 5;    /* アプリ用データ 機能設定プリセットID MultiMedia */
        // 機能設定　各アプリ毎のプリセット定義
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_NUM                     = 46;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID 設定数 */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_SPACE                   = 0;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID スペース */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_CTRL                    = 1;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID CTRL */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_ERASER                  = 2;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID 消しゴム */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_SHIFT                   = 3;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID SHIFT */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_PEN                     = 4;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID ペン */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_BRUSH                   = 5;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID ブラシ */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_SYRINGE                 = 6;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID スポイト */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_PALM                    = 7;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID 手のひら */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_COPY_STAMP_TOOL         = 8;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID コピースタンプツール */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_SYRINGE_TOOL            = 9;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID スポイトツール */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_CROP_TOOL               = 10;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID 切り抜きツール */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_FILL_GRADATION_TOOL     = 11;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID 塗りつぶし、グラデーション */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_ROTATIONG_VIEW_TOOL     = 12;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID 回転ビューツール */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_DODGE_TOOL              = 13;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID 覆い焼きツール */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_DRAWING_COLOR_BACKGROUND_COLOR = 14;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID 描画色・背景色の切り替え */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_MOVE_TOOL               = 15;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID 移動ツール */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_SELECTION_TOOL          = 16;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID 選択ツール */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_REPAIR_BRUSH_TOOL       = 17;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID 修復ブラシツール */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_HISTORY_BRUSH_TOOL      = 18;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID ヒストリーブラシツール */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_CHARACTER_TOOL          = 19;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID 文字ツール */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_PATH_COMPONENT_SELECTION_TOOL = 20;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID パスコンポーネント選択ツール */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_RECTANGLE_ROUNDED_RECTANGLE_OVAL_TOOL = 21;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID 長方形・角丸長方形・楕円形ツール */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_ZOOM_TOOL               = 22;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID ズームツール */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_TONE_CURVE              = 23;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID トーンカーブ */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_LEVEL_CORRECTION        = 24;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID レベル補正 */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_AUTO_SELECTION          = 25;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID 自動選択 */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_GROUP_LAYERS            = 26;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID レイヤのグループ化 */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_PALETTE_DISPLAY_OFF     = 27;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID パレットの表示のOFFフルスクリーンON */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_CREATE_NEW_LAYER        = 28;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID レイヤー新規作成 */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_SELECT_ALL_IN_THE_LAYER = 29;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID レイヤー内の全選択 */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_LAYER_INTEGRATION       = 30;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID レイヤーの統合 */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_SHOW_HIDE_RULER         = 31;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID 定規の表示 */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_SWITCHING_COLOR_MODE    = 32;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID カラーモードの切り替え */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_CANCEL_SELECTION        = 33;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID 選択範囲の解除 */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_FREE_DEFORMATION_MODE   = 34;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID 自由変形モード */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_UNDO_ONE_TASK           = 35;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID 作業を1つ元に戻す */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_UNDO_ONE_MORE_TASK      = 36;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID 作業を1つ以上前に戻す */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_SAVE                    = 37;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID 保存 */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_RESTORE_DISPLAY_TO_NORMAL_SIZE = 38;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID 表示を通常サイズに戻す */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_LAYER_COMBINING_ALL_DISPLAY_LAYERS = 39;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID 表示レイヤーを全て結合したレイヤー */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_SHOW_HIDE_GRID          = 40;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID グリッドの表示 */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_TAB_SWITCHING           = 41;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID タブの切り替え */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_ESC                     = 42;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID ESC */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_INVERT_SELECTION        = 43;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID 選択範囲の反転 */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_HIDE_SELECTION          = 44;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID 選択範囲を隠す */
        public const byte APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_SHOW_OPTIONS            = 45;    /* アプリ用データ 機能設定PHOTOSHOPプリセットID オプション表示 */
        public const byte APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_NUM                    = 36;        /* アプリ用データ 機能設定CLIPSTUDIOプリセットID 設定数 */
        public const byte APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_SWITCH_PRE_SUB_TOOL    = 0;    /* アプリ用データ 機能設定CLIPSTUDIOプリセットID 前のサブツールに切り替え */
        public const byte APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_SWITCH_NEXT_SUB_TOOL   = 1;    /* アプリ用データ 機能設定CLIPSTUDIOプリセットID 次のサブツールに切り替え */
        public const byte APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_PALM                   = 2;    /* アプリ用データ 機能設定CLIPSTUDIOプリセットID 手のひら */
        public const byte APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_ROTATION               = 3;    /* アプリ用データ 機能設定CLIPSTUDIOプリセットID 回転 */
        public const byte APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_MAGNIFYING_GLASS_LARGE = 4;    /* アプリ用データ 機能設定CLIPSTUDIOプリセットID 虫めがね(拡大) */
        public const byte APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_MAGNIFYING_GLASS_SHRINK = 5;    /* アプリ用データ 機能設定CLIPSTUDIOプリセットID 虫めがね(縮小) */
        public const byte APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_MAIN_COLOR_SUB_COLOR   = 6;    /* アプリ用データ 機能設定CLIPSTUDIOプリセットID メインカラーとサブカラーを切り替え */
        public const byte APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_DRAWING_COLOR_TRANSPARENT_COLOR = 7;    /* アプリ用データ 機能設定CLIPSTUDIOプリセットID 描画色と透明色を切り替え */
        public const byte APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_LAYER_SELECTION        = 8;    /* アプリ用データ 機能設定CLIPSTUDIOプリセットID レイヤー選択 */
        public const byte APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_MAGNIFYING_GLASS       = 9;    /* アプリ用データ 機能設定CLIPSTUDIOプリセットID 虫めがね */
        public const byte APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_MOVE_PALM              = 10;    /* アプリ用データ 機能設定CLIPSTUDIOプリセットID 移動（手のひら） */
        public const byte APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_MOVE_ROTATION          = 11;    /* アプリ用データ 機能設定CLIPSTUDIOプリセットID 移動（回転） */
        public const byte APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_OPERATION_OBJECT       = 12;    /* アプリ用データ 機能設定CLIPSTUDIOプリセットID 操作（オブジェクト） */
        public const byte APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_OPERATION_LAYER_SELECTION = 13;    /* アプリ用データ 機能設定CLIPSTUDIOプリセットID 操作（レイヤー選択） */
        public const byte APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_LIGHT_TABLE_TIMELINE_EDIT = 14;    /* アプリ用データ 機能設定CLIPSTUDIOプリセットID ライトテーブル/タイムライン編集 */
        public const byte APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_MOVE_LAYER             = 15;    /* アプリ用データ 機能設定CLIPSTUDIOプリセットID レイヤー移動 */
        public const byte APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_SELECTION_RANGE        = 16;    /* アプリ用データ 機能設定CLIPSTUDIOプリセットID 選択範囲 */
        public const byte APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_AUTO_SELECTION         = 17;    /* アプリ用データ 機能設定CLIPSTUDIOプリセットID 自動選択 */
        public const byte APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_SYRINGE                = 18;    /* アプリ用データ 機能設定CLIPSTUDIOプリセットID スポイト */
        public const byte APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_PEN_PENCIL             = 19;    /* アプリ用データ 機能設定CLIPSTUDIOプリセットID ペン/鉛筆 */
        public const byte APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_BRUSH_AIRBRUSH         = 20;    /* アプリ用データ 機能設定CLIPSTUDIOプリセットID 筆/エアブラシ*デコレーション */
        public const byte APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_ERASER                 = 21;    /* アプリ用データ 機能設定CLIPSTUDIOプリセットID 消しゴム */
        public const byte APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_COLOR_MIX              = 22;    /* アプリ用データ 機能設定CLIPSTUDIOプリセットID 色混ぜ */
        public const byte APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_FILL_GRADATION         = 23;    /* アプリ用データ 機能設定CLIPSTUDIOプリセットID 塗りつぶし/グラデーション */
        public const byte APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_FIGURE_FRAME_RULER     = 24;    /* アプリ用データ 機能設定CLIPSTUDIOプリセットID 図形/コマ枠/定規 */
        public const byte APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_TEXT_BALLOON           = 25;    /* アプリ用データ 機能設定CLIPSTUDIOプリセットID テキスト/フキダシ */
        public const byte APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_LINE_CORRECTION        = 26;    /* アプリ用データ 機能設定CLIPSTUDIOプリセットID 線修正 */
        public const byte APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_SYRINGE2               = 27;    /* アプリ用データ 機能設定CLIPSTUDIOプリセットID スポイト */
        public const byte APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_OBJECT                 = 28;    /* アプリ用データ 機能設定CLIPSTUDIOプリセットID オブジェクト */
        public const byte APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_DRAW_STRAIGHT_LINE     = 29;    /* アプリ用データ 機能設定CLIPSTUDIOプリセットID 直線を描く */
        public const byte APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_SWITCHING_LAYER_COLOR  = 30;    /* アプリ用データ 機能設定CLIPSTUDIOプリセットID レイヤーカラーの使用切り替え */
        public const byte APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_CANCEL                 = 31;    /* アプリ用データ 機能設定CLIPSTUDIOプリセットID 取り消し */
        public const byte APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_CUT                    = 32;    /* アプリ用データ 機能設定CLIPSTUDIOプリセットID 切り取り */
        public const byte APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_PASTE                  = 33;    /* アプリ用データ 機能設定CLIPSTUDIOプリセットID 貼り付け */
        public const byte APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_COPY                   = 34;    /* アプリ用データ 機能設定CLIPSTUDIOプリセットID コピー */
        public const byte APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_ESC                    = 35;    /* アプリ用データ 機能設定CLIPSTUDIOプリセットID ESC */
        public const byte APP_DATA_FUNC_PRESET_SAI_ID_NUM                           = 33;        /* アプリ用データ 機能設定SAIプリセットID 設定数 */
        public const byte APP_DATA_FUNC_PRESET_SAI_ID_RESTORE                       = 0;    /* アプリ用データ 機能設定SAIプリセットID 元に戻す */
        public const byte APP_DATA_FUNC_PRESET_SAI_ID_REDOING                       = 1;    /* アプリ用データ 機能設定SAIプリセットID やり直し */
        public const byte APP_DATA_FUNC_PRESET_SAI_ID_CUT                           = 2;    /* アプリ用データ 機能設定SAIプリセットID 切り取り */
        public const byte APP_DATA_FUNC_PRESET_SAI_ID_COPY                          = 3;    /* アプリ用データ 機能設定SAIプリセットID コピー */
        public const byte APP_DATA_FUNC_PRESET_SAI_ID_PASTE                         = 4;    /* アプリ用データ 機能設定SAIプリセットID 貼り付け */
        public const byte APP_DATA_FUNC_PRESET_SAI_ID_CLEAR_LAYER                   = 5;    /* アプリ用データ 機能設定SAIプリセットID レイヤーを消去 */
        public const byte APP_DATA_FUNC_PRESET_SAI_ID_COMBINE_LOWER_LAYER           = 6;    /* アプリ用データ 機能設定SAIプリセットID 下のレイヤーと結合/レイヤーセットを結合 */
        public const byte APP_DATA_FUNC_PRESET_SAI_ID_TRANSFER_TO_LOWER_LAYER       = 7;    /* アプリ用データ 機能設定SAIプリセットID 下のレイヤーに転写 */
        public const byte APP_DATA_FUNC_PRESET_SAI_ID_FILL_LAYER                    = 8;    /* アプリ用データ 機能設定SAIプリセットID レイヤーを塗りつぶす */
        public const byte APP_DATA_FUNC_PRESET_SAI_ID_FREE_DEFORMATION              = 9;    /* アプリ用データ 機能設定SAIプリセットID 自由変形 */
        public const byte APP_DATA_FUNC_PRESET_SAI_ID_CANCEL_SELECTION_AREA         = 10;    /* アプリ用データ 機能設定SAIプリセットID 選択領域の解除 */
        public const byte APP_DATA_FUNC_PRESET_SAI_ID_DISPLAY_SELECTION_AREA        = 11;    /* アプリ用データ 機能設定SAIプリセットID 選択領域の表示 */
        public const byte APP_DATA_FUNC_PRESET_SAI_ID_SELECT_ALL                    = 12;    /* アプリ用データ 機能設定SAIプリセットID 全て選択 */
        public const byte APP_DATA_FUNC_PRESET_SAI_ID_HUE_SATURATION_LIGHTNESS      = 13;    /* アプリ用データ 機能設定SAIプリセットID 色相、彩度、明度 */
        public const byte APP_DATA_FUNC_PRESET_SAI_ID_FLIP_HORIZONTAL               = 14;    /* アプリ用データ 機能設定SAIプリセットID 左右反転 */
        public const byte APP_DATA_FUNC_PRESET_SAI_ID_PIXEL_EQUAL_MAGNIFICATION     = 15;    /* アプリ用データ 機能設定SAIプリセットID ピクセル等倍 */
        public const byte APP_DATA_FUNC_PRESET_SAI_ID_POSITION_RESET                = 16;    /* アプリ用データ 機能設定SAIプリセットID 位置リセット */
        public const byte APP_DATA_FUNC_PRESET_SAI_ID_ROTATION_RESET                = 17;    /* アプリ用データ 機能設定SAIプリセットID 回転リセット */
        public const byte APP_DATA_FUNC_PRESET_SAI_ID_HIDE_CONTROL_PANEL            = 18;    /* アプリ用データ 機能設定SAIプリセットID 操作パネルを隠す/レイヤー関連パネルを右に表示 */
        public const byte APP_DATA_FUNC_PRESET_SAI_ID_DRAW_COLOR_SECONDARY_COLOR    = 19;    /* アプリ用データ 機能設定SAIプリセットID 描画色/二次色切り替え */
        public const byte APP_DATA_FUNC_PRESET_SAI_ID_DRAW_COLOR_TRANSPARENT_COLOR  = 20;    /* アプリ用データ 機能設定SAIプリセットID 描画色/透明色切り替え */
        public const byte APP_DATA_FUNC_PRESET_SAI_ID_PENCIL                        = 21;    /* アプリ用データ 機能設定SAIプリセットID 鉛筆 */
        public const byte APP_DATA_FUNC_PRESET_SAI_ID_AIRBRUSH                      = 22;    /* アプリ用データ 機能設定SAIプリセットID エアブラシ */
        public const byte APP_DATA_FUNC_PRESET_SAI_ID_BRUSH                         = 23;    /* アプリ用データ 機能設定SAIプリセットID 筆 */
        public const byte APP_DATA_FUNC_PRESET_SAI_ID_WATERCOLOR_BRUSH              = 24;    /* アプリ用データ 機能設定SAIプリセットID 水彩筆 */
        public const byte APP_DATA_FUNC_PRESET_SAI_ID_ERASER                        = 25;    /* アプリ用データ 機能設定SAIプリセットID 消しゴム */
        public const byte APP_DATA_FUNC_PRESET_SAI_ID_SELECTOR_PEN                  = 26;    /* アプリ用データ 機能設定SAIプリセットID 選択ペン */
        public const byte APP_DATA_FUNC_PRESET_SAI_ID_SELECTIVE_ERASE               = 27;    /* アプリ用データ 機能設定SAIプリセットID 選択消し */
        public const byte APP_DATA_FUNC_PRESET_SAI_ID_NEW_CANVAS                    = 28;    /* アプリ用データ 機能設定SAIプリセットID 新規キャンバス */
        public const byte APP_DATA_FUNC_PRESET_SAI_ID_ESC                           = 29;    /* アプリ用データ 機能設定SAIプリセットID ESC */
        public const byte APP_DATA_FUNC_PRESET_SAI_ID_CTRL                          = 30;    /* アプリ用データ 機能設定SAIプリセットID CTRL */
        public const byte APP_DATA_FUNC_PRESET_SAI_ID_SPACE                         = 31;    /* アプリ用データ 機能設定SAIプリセットID SPACE */
        public const byte APP_DATA_FUNC_PRESET_SAI_ID_SHIFT                         = 32;    /* アプリ用データ 機能設定SAIプリセットID shift */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_NUM                   = 59;        /* アプリ用データ 機能設定ILLUSTRATORプリセットID 設定数 */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_CANCEL                = 0;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID 取り消し */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_REDOING               = 1;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID やり直し */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_CUT                   = 2;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID カット */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_COPY                  = 3;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID コピー */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_PASTE                 = 4;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID ペースト */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_PASTE_FRONT           = 5;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID 前面へペースト */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_PASTE_BACK            = 6;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID 背面へペースト */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_PASTE_SAME_POSITION   = 7;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID 同じ位置にペースト */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_PASTE_ARTBOARD        = 8;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID すべてのアートボードにペースト */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_SPELL_CHECK           = 9;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID スペルチェック */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_OPEN_COLOR_SETTING_DIALOG_BOX = 10;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID カラー設定ダイアログボックスを開く */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_OPEN_KEYBOARD_SHORTCUT_DIALOG_BOX = 11;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID キーボードショートカットダイアログボックスを開く */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_OPEN_PREFERENCES_DIALOG_BOX = 12;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID 環境設定ダイアログボックスを開く */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_ARTBOARD_TOOL         = 13;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID アートボードツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_SELECTION_TOOL        = 14;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID 選択ツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_DIRECT_SELECTION_TOOL = 15;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID ダイレクト選択ツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_AUTOMATIC_SELECTION_TOOL = 16;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID 自動選択ツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_LASSO_TOOL            = 17;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID なげなわツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_PEN_TOOL              = 18;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID ペンツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_CURVED_TOOL           = 19;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID 曲線ツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_FILL_BRUSH_TOOL       = 20;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID 塗りブラシツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_ADD_ANCHOR_POINT      = 21;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID アンカーポイントの追加ツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_DELETE_ANCHOR_POINT   = 22;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID アンカーポイントの削除ツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_SWITCH_ANCHOR_POINT   = 23;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID アンカーポイントに切り替えツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_CHARACTER_TOOL        = 24;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID 文字ツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_CHARACTER_TOUCH_TOOL  = 25;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID 文字タッチツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_LINE_TOOL             = 26;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID 直線ツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_RECTANGULAR_TOOL      = 27;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID 長方形ツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_OVAL_TOOL             = 28;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID 楕円形ツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_BRUSH_TOOL            = 29;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID ブラシツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_PENCIL_TOOL           = 30;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID 鉛筆ツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_SHAPER_TOOL           = 31;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID Shaper ツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_ROTATING_TOOL         = 32;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID 回転ツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_REFLECT_TOOL          = 33;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID リフレクトツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_SCALE_TOOL            = 34;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID 拡大・縮小ツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_WARP_TOOL             = 35;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID ワープツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_LINE_WIDTH_TOOL       = 36;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID 線幅ツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_FREE_TRANSFORM_TOOL   = 37;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID 自由変形ツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_SHAPE_FORMING_TOOL    = 38;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID シェイプ形成ツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_PERSPECTIVE_GRID_TOOL = 39;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID 遠近グリッドツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_PERSPECTIVE_SELECTION_TOOL = 40;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID 遠近図形選択ツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_SYMBOL_SPRAY_TOOL     = 41;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID シンボルスプレーツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_BAR_GRAPH_TOOL        = 42;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID 棒グラフツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_MESH_TOOL             = 43;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID メッシュツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_GRADATION_TOOL        = 44;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID グラデーションツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_SYRINGE_TOOL          = 45;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID スポイトツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_BLEND_TOOL            = 46;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID ブレンドツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_LIVE_PAINT_TOOL       = 47;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID ライブペイントツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_LIVE_PAINT_SELECTION_TOOL = 48;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID ライブペイント選択ツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_SLICE_TOOL            = 49;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID スライスツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_ERASER_TOOL           = 50;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID 消しゴムツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_SCISSOR_TOOL          = 51;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID はさみツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_HAND_TOOL             = 52;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID 手のひらツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_ZOOM_TOOL             = 53;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID ズームツール */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_SWITCH_SMOOTH_TOOL    = 54;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID 塗りブラシツールを使用しているときにスムーズツールに切り替える */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_ESC                   = 55;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID ESC */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_CTRL                  = 56;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID CTRL */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_HAND_TOOL_SPACE       = 57;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID 手のひらツール（SPACE） */
        public const byte APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_SHIFT                 = 58;    /* アプリ用データ 機能設定ILLUSTRATORプリセットID SHIFT */
        public const byte APP_DATA_FUNC_PRESET_LIGHTROOM_ID_NUM = 1;        /* アプリ用データ 機能設定LIGHTROOMプリセットID 設定数 */
        public const byte APP_DATA_FUNC_PRESET_LIGHTROOM_ID_ = 0;    /* アプリ用データ 機能設定LIGHTROOMプリセットID  */
        public const byte APP_DATA_FUNC_PRESET_MULTIMEDIA_ID_NUM = 1;        /* アプリ用データ 機能設定MULTIMEDIAプリセットID 設定数 */
        public const byte APP_DATA_FUNC_PRESET_MULTIMEDIA_ID_ = 0;    /* アプリ用データ 機能設定MULTIMEDIAプリセットID  */
        // ダイアル機能設定　カテゴリ定義
        public const byte APP_DATA_DIAL_FUNC_CATEGORY_ID_NUM            = 7;     /* アプリ用データ 機能設定カテゴリID 設定数 */
        public const byte APP_DATA_DIAL_FUNC_CATEGORY_ID_NOT_SET        = 0;     /* アプリ用データ 機能設定カテゴリID 未設定 */
        public const byte APP_DATA_DIAL_FUNC_CATEGORY_ID_KEY            = 1;     /* アプリ用データ 機能設定カテゴリID KEY */
        public const byte APP_DATA_DIAL_FUNC_CATEGORY_ID_MOUSE          = 2;     /* アプリ用データ 機能設定カテゴリID MOUSE */
        public const byte APP_DATA_DIAL_FUNC_CATEGORY_ID_GAMEPAD        = 3;     /* アプリ用データ 機能設定カテゴリID GAMEPAD */
        public const byte APP_DATA_DIAL_FUNC_CATEGORY_ID_MULTIMEDIA     = 4;     /* アプリ用データ 機能設定カテゴリID MULTIMEDIA */
        public const byte APP_DATA_DIAL_FUNC_CATEGORY_ID_USERDIALMACRO  = 5;     /* アプリ用データ 機能設定カテゴリID USER DIAL MACRO */
        public const byte APP_DATA_DIAL_FUNC_CATEGORY_ID_PRESET         = 6;     /* アプリ用データ 機能設定カテゴリID PRESET */
        // ダイアル機能設定　機能定義
        public const byte APP_DATA_DIAL_FUNC_KEY_ID_NUM                 = 3;     /* アプリ用データ 機能設定 キーボードID 設定数 */
        public const byte APP_DATA_DIAL_FUNC_KEY_ID_NUM_UP              = 0;     /* アプリ用データ 機能設定 キーボードID 数字入力0-9 */
        public const byte APP_DATA_DIAL_FUNC_KEY_ID_NUM_DOWN            = 1;     /* アプリ用データ 機能設定 キーボードID 数字入力9-0 */
        public const byte APP_DATA_DIAL_FUNC_KEY_ID_KEY                 = 2;     /* アプリ用データ 機能設定 キーボードID KEY */
        public const byte APP_DATA_DIAL_FUNC_MOUSE_ID_NUM               = 8;     /* アプリ用データ 機能設定 マウスID 設定数 */
        public const byte APP_DATA_DIAL_FUNC_MOUSE_ID_L_CLICK           = 0;     /* アプリ用データ 機能設定 マウスID L Click */
        public const byte APP_DATA_DIAL_FUNC_MOUSE_ID_R_CLICK           = 1;     /* アプリ用データ 機能設定 マウスID R Click */
        public const byte APP_DATA_DIAL_FUNC_MOUSE_ID_WH_CLICK          = 2;     /* アプリ用データ 機能設定 マウスID WH Click */
        public const byte APP_DATA_DIAL_FUNC_MOUSE_ID_B4                = 3;     /* アプリ用データ 機能設定 マウスID B4 */
        public const byte APP_DATA_DIAL_FUNC_MOUSE_ID_B5                = 4;     /* アプリ用データ 機能設定 マウスID B5 */
        public const byte APP_DATA_DIAL_FUNC_MOUSE_ID_DCLICK            = 5;     /* アプリ用データ 機能設定 マウスID Double Click */
        public const byte APP_DATA_DIAL_FUNC_MOUSE_ID_MOVE              = 6;     /* アプリ用データ 機能設定 マウスID Move */
        public const byte APP_DATA_DIAL_FUNC_MOUSE_ID_WH_SCROLL         = 7;     /* アプリ用データ 機能設定 マウスID WH Scroll */
        public const byte APP_DATA_DIAL_FUNC_GAMEPAD_ID_NUM             = 19;     /* アプリ用データ 機能設定 ゲームパッドID 設定数 */
        public const byte APP_DATA_DIAL_FUNC_GAMEPAD_ID_LEFT_ANALOG     = 0;     /* アプリ用データ 機能設定 ゲームパッドID Left Analog */
        public const byte APP_DATA_DIAL_FUNC_GAMEPAD_ID_RIGHT_ANALOG    = 1;     /* アプリ用データ 機能設定 ゲームパッドID Right Analog */
        public const byte APP_DATA_DIAL_FUNC_GAMEPAD_ID_B01             = 2;     /* アプリ用データ 機能設定 ゲームパッドID Button01 */
        public const byte APP_DATA_DIAL_FUNC_GAMEPAD_ID_B02             = 3;     /* アプリ用データ 機能設定 ゲームパッドID Button02 */
        public const byte APP_DATA_DIAL_FUNC_GAMEPAD_ID_B03             = 4;     /* アプリ用データ 機能設定 ゲームパッドID Button03 */
        public const byte APP_DATA_DIAL_FUNC_GAMEPAD_ID_B04             = 5;     /* アプリ用データ 機能設定 ゲームパッドID Button04 */
        public const byte APP_DATA_DIAL_FUNC_GAMEPAD_ID_B05             = 6;     /* アプリ用データ 機能設定 ゲームパッドID Button05 */
        public const byte APP_DATA_DIAL_FUNC_GAMEPAD_ID_B06             = 7;     /* アプリ用データ 機能設定 ゲームパッドID Button06 */
        public const byte APP_DATA_DIAL_FUNC_GAMEPAD_ID_B07             = 8;     /* アプリ用データ 機能設定 ゲームパッドID Button07 */
        public const byte APP_DATA_DIAL_FUNC_GAMEPAD_ID_B08             = 9;     /* アプリ用データ 機能設定 ゲームパッドID Button08 */
        public const byte APP_DATA_DIAL_FUNC_GAMEPAD_ID_B09             = 10;     /* アプリ用データ 機能設定 ゲームパッドID Button09 */
        public const byte APP_DATA_DIAL_FUNC_GAMEPAD_ID_B10             = 11;     /* アプリ用データ 機能設定 ゲームパッドID Button10 */
        public const byte APP_DATA_DIAL_FUNC_GAMEPAD_ID_B11             = 12;     /* アプリ用データ 機能設定 ゲームパッドID Button11 */
        public const byte APP_DATA_DIAL_FUNC_GAMEPAD_ID_B12             = 13;     /* アプリ用データ 機能設定 ゲームパッドID Button12 */
        public const byte APP_DATA_DIAL_FUNC_GAMEPAD_ID_B13             = 14;     /* アプリ用データ 機能設定 ゲームパッドID Button13 */
        public const byte APP_DATA_DIAL_FUNC_GAMEPAD_ID_HAT_N           = 15;     /* アプリ用データ 機能設定 ゲームパッドID Hat SW N */
        public const byte APP_DATA_DIAL_FUNC_GAMEPAD_ID_HAT_S           = 16;     /* アプリ用データ 機能設定 ゲームパッドID Hat SW S */
        public const byte APP_DATA_DIAL_FUNC_GAMEPAD_ID_HAT_W           = 17;     /* アプリ用データ 機能設定 ゲームパッドID Hat SW W */
        public const byte APP_DATA_DIAL_FUNC_GAMEPAD_ID_HAT_E           = 18;     /* アプリ用データ 機能設定 ゲームパッドID Hat SW E */
        public const byte APP_DATA_DIAL_FUNC_MULTIMEDIA_ID_NUM = 11;     /* アプリ用データ 機能設定 マルチメディアID 設定数 */
        public const byte APP_DATA_DIAL_FUNC_MULTIMEDIA_ID_PLAY = 0;     /* アプリ用データ 機能設定 マルチメディアID Play */
        public const byte APP_DATA_DIAL_FUNC_MULTIMEDIA_ID_PAUSE = 1;     /* アプリ用データ 機能設定 マルチメディアID  */
        public const byte APP_DATA_DIAL_FUNC_MULTIMEDIA_ID_STOP = 2;     /* アプリ用データ 機能設定 マルチメディアID  */
        public const byte APP_DATA_DIAL_FUNC_MULTIMEDIA_ID_REC = 3;     /* アプリ用データ 機能設定 マルチメディアID  */
        public const byte APP_DATA_DIAL_FUNC_MULTIMEDIA_ID_FORWORD = 4;     /* アプリ用データ 機能設定 マルチメディアID  */
        public const byte APP_DATA_DIAL_FUNC_MULTIMEDIA_ID_REWIND = 5;     /* アプリ用データ 機能設定 マルチメディアID  */
        public const byte APP_DATA_DIAL_FUNC_MULTIMEDIA_ID_NEXT = 6;     /* アプリ用データ 機能設定 マルチメディアID  */
        public const byte APP_DATA_DIAL_FUNC_MULTIMEDIA_ID_PREVIOUS = 7;     /* アプリ用データ 機能設定 マルチメディアID  */
        public const byte APP_DATA_DIAL_FUNC_MULTIMEDIA_ID_MUTE = 8;     /* アプリ用データ 機能設定 マルチメディアID  */
        public const byte APP_DATA_DIAL_FUNC_MULTIMEDIA_ID_VOLUMEUP = 9;     /* アプリ用データ 機能設定 マルチメディアID  */
        public const byte APP_DATA_DIAL_FUNC_MULTIMEDIA_ID_VOLUMEDOWN = 10;     /* アプリ用データ 機能設定 マルチメディアID  */
        // ダイアル機能設定　プリセット定義
        public const string APP_DATA_DIAL_FUNC_PRESET_DATA_PATH_JA = "presetdata\\JA\\macro_dial\\";   /* アプリ用データ ダイアル機能設定プリセットデータ格納パス 日本語キーボード用 */
        public const string APP_DATA_DIAL_FUNC_PRESET_DATA_PATH_US = "presetdata\\US\\macro_dial\\";   /* アプリ用データ ダイアル機能設定プリセットデータ格納パス USキーボード用 */
        public const string APP_DATA_DIAL_FUNC_PRESET_DATA_FILE_EXTENSION = ".dat";
        public const int APP_DATA_DIAL_FUNC_PRESET_DATA_DATA_LEN = 6;               /* アプリ用データ ダイアル機能設定プリセットデータ データ長（set typと感度除く） */
        public const byte APP_DATA_DIAL_FUNC_PRESET_ID_NUM                  = 5;    /* アプリ用データ ダイアル機能設定プリセットID 設定数 */
        public const byte APP_DATA_DIAL_FUNC_PRESET_ID_PHOTOSHOP            = 0;    /* アプリ用データ ダイアル機能設定プリセットID PhotoShop */
        public const byte APP_DATA_DIAL_FUNC_PRESET_ID_CLIPSTUDIO           = 1;    /* アプリ用データ ダイアル機能設定プリセットID ClipStudio */
        public const byte APP_DATA_DIAL_FUNC_PRESET_ID_SAI                  = 2;    /* アプリ用データ ダイアル機能設定プリセットID SAI */
        public const byte APP_DATA_DIAL_FUNC_PRESET_ID_ILLUSTRATOR          = 3;    /* アプリ用データ ダイアル機能設定プリセットID Illustrator */
        public const byte APP_DATA_DIAL_FUNC_PRESET_ID_LIGHTROOM            = 4;    /* アプリ用データ ダイアル機能設定プリセットID Lightroom */
        // 機能設定　各アプリ毎のプリセット定義
        public const byte APP_DATA_DIAL_FUNC_PRESET_PHOTOSHOP_ID_NUM            = 16;    /* アプリ用データ ダイアル機能設定PHOTOSHOPプリセットID 設定数 */
        public const byte APP_DATA_DIAL_FUNC_PRESET_PHOTOSHOP_ID_BRUSH_L        = 0;    /* アプリ用データ ダイアル機能設定PHOTOSHOPプリセットID ブラシサイズ大 */
        public const byte APP_DATA_DIAL_FUNC_PRESET_PHOTOSHOP_ID_BRUSH_S        = 1;    /* アプリ用データ ダイアル機能設定PHOTOSHOPプリセットID ブラシサイズ小 */
        public const byte APP_DATA_DIAL_FUNC_PRESET_PHOTOSHOP_ID_ROTATION_R     = 2;    /* アプリ用データ ダイアル機能設定PHOTOSHOPプリセットID 回転右 */
        public const byte APP_DATA_DIAL_FUNC_PRESET_PHOTOSHOP_ID_ROTATION_L     = 3;    /* アプリ用データ ダイアル機能設定PHOTOSHOPプリセットID 回転左 */
        public const byte APP_DATA_DIAL_FUNC_PRESET_PHOTOSHOP_ID_IMAGE_ENLARGE  = 4;    /* アプリ用データ ダイアル機能設定PHOTOSHOPプリセットID 画像拡大 */
        public const byte APP_DATA_DIAL_FUNC_PRESET_PHOTOSHOP_ID_IMAGE_REDUCE   = 5;    /* アプリ用データ ダイアル機能設定PHOTOSHOPプリセットID 画像縮小 */
        public const byte APP_DATA_DIAL_FUNC_PRESET_PHOTOSHOP_ID_CANVAS_ENLARGE = 6;    /* アプリ用データ ダイアル機能設定PHOTOSHOPプリセットID キャンバス拡大 */
        public const byte APP_DATA_DIAL_FUNC_PRESET_PHOTOSHOP_ID_CANVAS_REDUCE  = 7;    /* アプリ用データ ダイアル機能設定PHOTOSHOPプリセットID キャンバス縮小 */
        public const byte APP_DATA_DIAL_FUNC_PRESET_PHOTOSHOP_ID_LINE_L         = 8;    /* アプリ用データ ダイアル機能設定PHOTOSHOPプリセットID 行間拡大 */
        public const byte APP_DATA_DIAL_FUNC_PRESET_PHOTOSHOP_ID_LINE_S         = 9;    /* アプリ用データ ダイアル機能設定PHOTOSHOPプリセットID 行間縮小 */
        public const byte APP_DATA_DIAL_FUNC_PRESET_PHOTOSHOP_ID_LETTER_L       = 10;    /* アプリ用データ ダイアル機能設定PHOTOSHOPプリセットID 字間拡大 */
        public const byte APP_DATA_DIAL_FUNC_PRESET_PHOTOSHOP_ID_LETTER_S       = 11;    /* アプリ用データ ダイアル機能設定PHOTOSHOPプリセットID 字間縮小 */
        public const byte APP_DATA_DIAL_FUNC_PRESET_PHOTOSHOP_ID_BLUSH_HARD_UP  = 12;    /* アプリ用データ ダイアル機能設定PHOTOSHOPプリセットID ブラシの硬さ UP */
        public const byte APP_DATA_DIAL_FUNC_PRESET_PHOTOSHOP_ID_BLUSH_HARD_DOWN = 13;    /* アプリ用データ ダイアル機能設定PHOTOSHOPプリセットID ブラシの硬さ DOWN */
        public const byte APP_DATA_DIAL_FUNC_PRESET_PHOTOSHOP_ID_OPACITY_UP     = 14;    /* アプリ用データ ダイアル機能設定PHOTOSHOPプリセットID 不透明度　大 */
        public const byte APP_DATA_DIAL_FUNC_PRESET_PHOTOSHOP_ID_OPACITY_DOWN   = 15;    /* アプリ用データ ダイアル機能設定PHOTOSHOPプリセットID 不透明度　小 */
        public const byte APP_DATA_DIAL_FUNC_PRESET_CLIPSTUDIO_ID_NUM           = 10;    /* アプリ用データ ダイアル機能設定CLIPSTUDIOプリセットID 設定数 */
        public const byte APP_DATA_DIAL_FUNC_PRESET_CLIPSTUDIO_ID_BRUSH_L       = 0;    /* アプリ用データ ダイアル機能設定CLIPSTUDIOプリセットID ブラシサイズ大 */
        public const byte APP_DATA_DIAL_FUNC_PRESET_CLIPSTUDIO_ID_BRUSH_S       = 1;    /* アプリ用データ ダイアル機能設定CLIPSTUDIOプリセットID ブラシサイズ小 */
        public const byte APP_DATA_DIAL_FUNC_PRESET_CLIPSTUDIO_ID_ROTATION_R    = 2;    /* アプリ用データ ダイアル機能設定CLIPSTUDIOプリセットID 回転右 */
        public const byte APP_DATA_DIAL_FUNC_PRESET_CLIPSTUDIO_ID_ROTATION_L    = 3;    /* アプリ用データ ダイアル機能設定CLIPSTUDIOプリセットID 回転左 */
        public const byte APP_DATA_DIAL_FUNC_PRESET_CLIPSTUDIO_ID_IMAGE_ENLARGE = 4;    /* アプリ用データ ダイアル機能設定CLIPSTUDIOプリセットID 画像拡大 */
        public const byte APP_DATA_DIAL_FUNC_PRESET_CLIPSTUDIO_ID_IMAGE_REDUCE  = 5;    /* アプリ用データ ダイアル機能設定CLIPSTUDIOプリセットID 画像縮小 */
        public const byte APP_DATA_DIAL_FUNC_PRESET_CLIPSTUDIO_ID_BLUSH_OPACITY_UP = 6;    /* アプリ用データ ダイアル機能設定CLIPSTUDIOプリセットID ブラシの不透明度と濃度アップ */
        public const byte APP_DATA_DIAL_FUNC_PRESET_CLIPSTUDIO_ID_BLUSH_OPACITY_DOWN = 7;    /* アプリ用データ ダイアル機能設定CLIPSTUDIOプリセットID ブラシの不透明度と濃度ダウン */
        public const byte APP_DATA_DIAL_FUNC_PRESET_CLIPSTUDIO_ID_ZOOM_IN       = 8;    /* アプリ用データ ダイアル機能設定CLIPSTUDIOプリセットID ズームイン */
        public const byte APP_DATA_DIAL_FUNC_PRESET_CLIPSTUDIO_ID_ZOOM_OUT      = 9;    /* アプリ用データ ダイアル機能設定CLIPSTUDIOプリセットID ズームアウト */
        public const byte APP_DATA_DIAL_FUNC_PRESET_SAI_ID_NUM                  = 10;    /* アプリ用データ ダイアル機能設定SAIプリセットID 設定数 */
        public const byte APP_DATA_DIAL_FUNC_PRESET_SAI_ID_BRUSH_L              = 0;    /* アプリ用データ ダイアル機能設定SAIプリセットID ブラシサイズ大 */
        public const byte APP_DATA_DIAL_FUNC_PRESET_SAI_ID_BRUSH_S              = 1;    /* アプリ用データ ダイアル機能設定SAIプリセットID ブラシサイズ小 */
        public const byte APP_DATA_DIAL_FUNC_PRESET_SAI_ID_ROTATION_R           = 2;    /* アプリ用データ ダイアル機能設定SAIプリセットID 回転右 */
        public const byte APP_DATA_DIAL_FUNC_PRESET_SAI_ID_ROTATION_L           = 3;    /* アプリ用データ ダイアル機能設定SAIプリセットID 回転左 */
        public const byte APP_DATA_DIAL_FUNC_PRESET_SAI_ID_IMAGE_ENLARGE        = 4;    /* アプリ用データ ダイアル機能設定SAIプリセットID 画像拡大 */
        public const byte APP_DATA_DIAL_FUNC_PRESET_SAI_ID_IMAGE_REDUCE         = 5;    /* アプリ用データ ダイアル機能設定SAIプリセットID 画像縮小 */
        public const byte APP_DATA_DIAL_FUNC_PRESET_SAI_ID_CANVAS_ENLARGE       = 6;    /* アプリ用データ ダイアル機能設定SAIプリセットID キャンバス拡大 */
        public const byte APP_DATA_DIAL_FUNC_PRESET_SAI_ID_CANVAS_REDUCE        = 7;    /* アプリ用データ ダイアル機能設定SAIプリセットID キャンバス縮小 */
        public const byte APP_DATA_DIAL_FUNC_PRESET_SAI_ID_CURSOR_ENLARGE       = 8;    /* アプリ用データ ダイアル機能設定SAIプリセットID カーソル位置を中心に拡大 */
        public const byte APP_DATA_DIAL_FUNC_PRESET_SAI_ID_CURSOR_REDUCE        = 9;    /* アプリ用データ ダイアル機能設定SAIプリセットID カーソル位置を中心に縮小 */
        public const byte APP_DATA_DIAL_FUNC_PRESET_ILLUSTRATOR_ID_NUM          = 2;    /* アプリ用データ ダイアル機能設定ILLUSTRATORプリセットID 設定数 */
        public const byte APP_DATA_DIAL_FUNC_PRESET_ILLUSTRATOR_ID_ZOOMIN       = 0;    /* アプリ用データ ダイアル機能設定ILLUSTRATORプリセットID ZOOMIN */
        public const byte APP_DATA_DIAL_FUNC_PRESET_ILLUSTRATOR_ID_ZOOMOUT      = 1;    /* アプリ用データ ダイアル機能設定ILLUSTRATORプリセットID ZOOMOUT */
        public const byte APP_DATA_DIAL_FUNC_PRESET_LIGHTROOM_ID_NUM            = 8;    /* アプリ用データ ダイアル機能設定LIGHTROOMプリセットID 設定数 */
        public const byte APP_DATA_DIAL_FUNC_PRESET_LIGHTROOM_ID_SLIDER_S_UP    = 0;    /* アプリ用データ ダイアル機能設定LIGHTROOMプリセットID SLIDER S UP */
        public const byte APP_DATA_DIAL_FUNC_PRESET_LIGHTROOM_ID_SLIDER_S_DOWN  = 1;    /* アプリ用データ ダイアル機能設定LIGHTROOMプリセットID SLIDER S DOWN */
        public const byte APP_DATA_DIAL_FUNC_PRESET_LIGHTROOM_ID_SLIDER_L_UP    = 2;    /* アプリ用データ ダイアル機能設定LIGHTROOMプリセットID SLIDER L UP */
        public const byte APP_DATA_DIAL_FUNC_PRESET_LIGHTROOM_ID_SLIDER_L_DOWN  = 3;    /* アプリ用データ ダイアル機能設定LIGHTROOMプリセットID SLIDER L DOWN */
        public const byte APP_DATA_DIAL_FUNC_PRESET_LIGHTROOM_ID_BRUSH_L        = 4;    /* アプリ用データ ダイアル機能設定LIGHTROOMプリセットID ブラシサイズ大 */
        public const byte APP_DATA_DIAL_FUNC_PRESET_LIGHTROOM_ID_BRUSH_S        = 5;    /* アプリ用データ ダイアル機能設定LIGHTROOMプリセットID ブラシサイズ小 */
        public const byte APP_DATA_DIAL_FUNC_PRESET_LIGHTROOM_ID_BRUSH_BLUR_UP  = 6;    /* アプリ用データ ダイアル機能設定LIGHTROOMプリセットID ブラシのぼかし拡大 */
        public const byte APP_DATA_DIAL_FUNC_PRESET_LIGHTROOM_ID_BRUSH_BLUR_DOWN = 7;    /* アプリ用データ ダイアル機能設定LIGHTROOMプリセットID ブラシのぼかし縮小 */

        public const byte BUTTON_NUM = 11;                  /* ボタン数 */
        public const byte BUTTON_1_ID = 0;
        public const byte BUTTON_2_ID = 1;
        public const byte BUTTON_3_ID = 2;
        public const byte BUTTON_4_ID = 3;
        public const byte BUTTON_5_ID = 4;
        public const byte BUTTON_6_ID = 5;
        public const byte BUTTON_7_ID = 6;
        public const byte BUTTON_8_ID = 7;
        public const byte BUTTON_9_ID = 8;
        public const byte BUTTON_10_ID = 9;
        public const byte BUTTON_11_ID = 10;
        public const byte ENCODER_BUTTON_ID = 10;
#if false
        public const byte SCRIPT_NUM = 120;                 /* Script最大数 */
        public const byte SCRIPT_USER_USE_NUM = 85;         /* Scriptユーザ使用最大数 */
        public const byte SCRIPT_SYSTEM_USE_NUM = 35;       /* Scriptシステム使用最大数 */
#endif
#if true
        public const byte SCRIPT_NUM = 200;                 /* Script最大数 */
        public const byte SCRIPT_USER_USE_NUM = 150;         /* Scriptユーザ使用最大数 */
        public const byte SCRIPT_SYSTEM_USE_NUM = 50;       /* Scriptシステム使用最大数 */
#endif

        public const byte LED_COLOR_DETAIL_OFF = 0;         /* LEDカラー詳細設定フラグ OFF */
        public const byte LED_COLOR_DETAIL_ON = 1;          /* LEDカラー詳細設定フラグ ON */
        //public const byte BUTTON_SET_TYPE_NUM           = 2;           /* ボタン設定 設定項目数 */
        //public const byte BUTTON_SET_TYPE_SW_TYPE       = 0;           /* ボタン設定 スイッチ入力設定 */
        //public const byte BUTTON_SET_TYPE_PATTERN_TYPE  = 1;           /* ボタン設定 パターン入力設定 */
        //public const byte BUTTON_SET_TYPE_MIN           = 0;           /* ボタン設定最小値 */
        //public const byte BUTTON_SET_TYPE_MAX           = 1;           /* ボタン設定最大値 */
        public const byte MACRO_SET_VAL_TYPE1           = 0;           /* マクロ実行設定 キャンセル後実行 */
        public const byte MACRO_SET_VAL_TYPE2           = 1;           /* マクロ実行設定 実行終了後実行 */
        public const byte MACRO_SET_VAL_TYPE3           = 2;           /* マクロ実行設定 無視 */
        public const byte MACRO_SET_VAL_MIN             = 0;           /* マクロ実行設定最小値 */
        public const byte MACRO_SET_VAL_MAX             = 2;           /* マクロ実行設定最大値 */

        //public const string MODE_NAME_UNDEFINE = "Undefine";    /* モード名称が未定義の時の表示 */
        //public const string MACRO_NAME_UNSETTING = "未設定";       /* マクロ名称が未設定の時の表示 */
        //public const string MACRO_NAME_UNDEFINE = "名称未設定";      /* マクロ名称が未定義の時の表示 */
        //public const string FUNCTION_NAME_UNSETTING = "未設定";      /* 機能名称が未設定の時の表示 */
        //public const string FUNCTION_NAME_UNDEFINE = "名称未設定";   /* 機能名称が未定義の時の表示 */
        //public const string ENCODER_SCRIPT_NAME_UNDEFINE = "名称未設定";         /* パターン名称が未定義の時の表示 */
        //public const string ENCODER_SCRIPT_LIST_MACRO_NAME = "実行マクロ名称";     /* パターンリスト　実行マクロ名称のヘッダ表示 */
        public const byte ENCODER_SCRIPT_SCRIPTSET_MAX_NUM = 32;                    /* エンコーダースクリプト　スクリプト設定最大数 */
        public const byte ENCODER_SCRIPT_LOOP_SET_NUM = 2;                          /* エンコーダースクリプト　繰り返し設定　設定数 */
        public const byte ENCODER_SCRIPT_LOOP_SET_NONE = 0;                          /* エンコーダースクリプト　繰り返し設定　繰り返しなし */
        public const byte ENCODER_SCRIPT_LOOP_SET_LOOP = 1;                          /* エンコーダースクリプト　繰り返し設定　繰り返し */


        // ボタン設定値
        public const byte BUTTON_SETTING_DEFAULT = 0;       // ボタン設定値　デフォルト
        public const byte BUTTON_SETTING_DISABLED = 1;      // ボタン設定値　無効
        public const byte BUTTON_SETTING_SCRIPT = 2;        // ボタン設定値　スクリプト

        // SCRIPT
        // COMMAND
        public const byte SCRIPT_COMMAND_INTERVAL               = 0x70;
        public const byte SCRIPT_COMMAND_KEY_PRESS              = 0x41;
        public const byte SCRIPT_COMMAND_KEY_RELEASE            = 0x40;
        public const byte SCRIPT_COMMAND_MULTIMEDIA_PRESS       = 0x43;
        public const byte SCRIPT_COMMAND_MULTIMEDIA_RELESE      = 0x42;
        public const byte SCRIPT_COMMAND_L_CLICK                = 0x29;
        public const byte SCRIPT_COMMAND_L_RELEASE              = 0x21;
        public const byte SCRIPT_COMMAND_R_CLICK                = 0x2A;
        public const byte SCRIPT_COMMAND_R_RELEASE              = 0x22;
        public const byte SCRIPT_COMMAND_W_CLICK                = 0x2C;
        public const byte SCRIPT_COMMAND_W_RELEASE              = 0x24;
        public const byte SCRIPT_COMMAND_B4_CLICK               = 0x2D;
        public const byte SCRIPT_COMMAND_B4_RELEASE             = 0x25;
        public const byte SCRIPT_COMMAND_B5_CLICK               = 0x2E;
        public const byte SCRIPT_COMMAND_B5_RELEASE             = 0x26;
        public const byte SCRIPT_COMMAND_WHEEL_UP               = 0x31;
        public const byte SCRIPT_COMMAND_WHEEL_DOWN             = 0x32;
        public const byte SCRIPT_COMMAND_MOUSE_MOVE             = 0x33;
        public const byte SCRIPT_COMMAND_JOY_BUTTON_PRESS       = 0x69;	// スクリプトコマンドID ジョイスティックボタンプレス
        public const byte SCRIPT_COMMAND_JOY_BUTTON_RELESE      = 0x61;	// スクリプトコマンドID ジョイスティックボタンリリース
        public const byte SCRIPT_COMMAND_JOY_HATSW_PRESS        = 0x6A;	// スクリプトコマンドID ジョイスティックHATSWプレス
        public const byte SCRIPT_COMMAND_JOY_HATSW_RELESE       = 0x62;	// スクリプトコマンドID ジョイスティックHATSWリリース
        public const byte SCRIPT_COMMAND_JOY_L_LEVER            = 0x6B;	// スクリプトコマンドID ジョイスティック左レバー出力
        public const byte SCRIPT_COMMAND_JOY_L_LEVER_CENTER     = 0x63;	// スクリプトコマンドID ジョイスティック左レバー中立
        public const byte SCRIPT_COMMAND_JOY_R_LEVER            = 0x6C;	// スクリプトコマンドID ジョイスティック右レバー出力
        public const byte SCRIPT_COMMAND_JOY_R_LEVER_CENTER     = 0x64;	// スクリプトコマンドID ジョイスティック右レバー中立
        public const int SCRIPT_ICON_IDX_INTERVAL               = 0;
        public const int SCRIPT_ICON_IDX_KEY_PRESS              = 1;
        public const int SCRIPT_ICON_IDX_KEY_RELEASE            = 2;
        public const int SCRIPT_ICON_IDX_MOUSE_CLICK            = 3;
        public const int SCRIPT_ICON_IDX_MOUSE_RELEASE          = 4;
        public const int SCRIPT_ICON_IDX_WHEEL_SCROLL           = 5;
        public const int SCRIPT_ICON_IDX_JOY_LEVER_L_PRESS      = 6;
        public const int SCRIPT_ICON_IDX_JOY_LEVER_L_RELEASE    = 7;
        public const int SCRIPT_ICON_IDX_JOY_LEVER_R_PRESS      = 8;
        public const int SCRIPT_ICON_IDX_JOY_LEVER_R_RELEASE    = 9;
        public const int SCRIPT_ICON_IDX_JOY_HAT_SW_PRESS       = 10;
        public const int SCRIPT_ICON_IDX_JOY_HAT_SW_RELEASE     = 11;
        public const int SCRIPT_ICON_IDX_JOY_BUTTON_PRESS       = 12;
        public const int SCRIPT_ICON_IDX_JOY_BUTTON_RELEASE     = 13;
        public const int SCRIPT_ICON_IDX_MOUSE_MOVE             = 14;
        public const int SCRIPT_ICON_IDX_MULTIMEDIA_PRESS       = 15;
        public const int SCRIPT_ICON_IDX_MULTIMEDIA_RELEASE     = 16;
        // SCRIPT MODE
        //public const byte SCRIPT_MODE_NUM = 2;              // スクリプトモード数 [１回、ループモード]
        //public const byte SCRIPT_MODE_NUM = 3;              // スクリプトモード数 [１回、ループモード、ファイヤーモード]
        public const byte SCRIPT_MODE_NUM = 4;              // スクリプトモード数 [１回、ループモード、ファイヤーモード、ホールド]
        public const byte SCRIPT_MODE_ONE_MODE = 0;         // スクリプトモード　1回
        public const byte SCRIPT_MODE_LOOP_MODE = 1;        // スクリプトモード　ループモード
        public const byte SCRIPT_MODE_FIRE_MODE = 2;        // スクリプトモード　ファイヤーモード
        public const byte SCRIPT_MODE_HOLD_MODE = 3;        // スクリプトモード　ホールドモード
        public const int SCRIPT_ICON_IDX_ONE_MODE = 0;
        public const int SCRIPT_ICON_IDX_LOOP_MODE = 1;
        public const int SCRIPT_ICON_IDX_FIRE_MODE = 2;
        public const int SCRIPT_ICON_IDX_HOLD_MODE = 3;
        //　SCRIPT EDITOR 表示文字列
        //public const string SCRIPT_MODE_ONE_MODE_TEXT = "１回だけ実行";         // スクリプトモード　1回
        //public const string SCRIPT_MODE_LOOP_MODE_TEXT = "繰り返し実行";        // スクリプトモード　ループモード
        //public const string SCRIPT_MODE_FIRE_MODE_TEXT = "押している間実行";        // スクリプトモード　ファイヤーモード
        //public const string SCRIPT_MODE_LOOP_MODE_TEXT = "ループモード";        // スクリプトモード　ループモード
        //public const string SCRIPT_MODE_FIRE_MODE_TEXT = "ファイヤーキーモード";        // スクリプトモード　ファイヤーモード
        //public const string SCRIPT_KEY_PRESS_TEXT = "キープレス";
        //public const string SCRIPT_KEY_RELEASE_TEXT = "キーリリース";
        //public const string SCRIPT_INTERVAL_TEXT = "インターバル[ms]";
        //public const string SCRIPT_INTERVAL_TEXT = "ｍｓ間隔";
        //public const string SCRIPT_MOUSE_CLICK_TEXT = "マウスのボタンを押し続ける";
        //public const string SCRIPT_MOUSE_RELEASE_TEXT = "マウスのボタンを離す";
        //public const string SCRIPT_MOUSE_L_CLICK_TEXT = "マウスのボタンを押し続ける[左]";
        //public const string SCRIPT_MOUSE_L_RELEASE_TEXT = "マウスのボタンを離す[左]";
        //public const string SCRIPT_MOUSE_R_CLICK_TEXT = "マウスのボタンを押し続ける[右]";
        //public const string SCRIPT_MOUSE_R_RELEASE_TEXT = "マウスのボタンを離す[右]";
        //public const string SCRIPT_MOUSE_W_CLICK_TEXT = "マウスのボタンを押し続ける[センター]";
        //public const string SCRIPT_MOUSE_W_RELEASE_TEXT = "マウスのボタンを離す[センター]";
        //public const string SCRIPT_MOUSE_B4_CLICK_TEXT = "マウスのボタンを押し続ける[ボタン4]";
        //public const string SCRIPT_MOUSE_B4_RELEASE_TEXT = "マウスのボタンを離す[ボタン4]";
        //public const string SCRIPT_MOUSE_B5_CLICK_TEXT = "マウスのボタンを押し続ける[ボタン5]";
        //public const string SCRIPT_MOUSE_B5_RELEASE_TEXT = "マウスのボタンを離す[ボタン5]";
        //public const string SCRIPT_WHEEL_UP_TEXT = "ホイールのスクロール　－１カウント(unit)";
        //public const string SCRIPT_WHEEL_DOWN_TEXT = "ホイールのスクロール　１カウント(unit)";
        //public const string SCRIPT_MOUSE_MOVE_TEXT = "マウス移動";
        //public const string SCRIPT_JOY_LEVER_L_TEXT = "ジョイスティックを押し続ける[左]";
        //public const string SCRIPT_JOY_LEVER_R_TEXT = "ジョイスティックを押し続ける[右]";
        //public const string SCRIPT_JOY_LEVER_L_CENTER_TEXT = "ジョイスティックを離す[左]";
        //public const string SCRIPT_JOY_LEVER_R_CENTER_TEXT = "ジョイスティックを離す[右]";
        //public const string SCRIPT_JOY_HATSW_PRESS_TEXT = "十字ボタンを押し続ける";
        //public const string SCRIPT_JOY_HATSW_RELEASE_TEXT = "十字ボタンを離す";
        //public const string SCRIPT_JOY_BUTTON_PRESS_TEXT = "ゲームパッドボタンを押し続ける";
        //public const string SCRIPT_JOY_BUTTON_RELEASE_TEXT = "ゲームパッドボタンを離す";
        //public const string SCRIPT_MULTIMEDIA_PRESS_TEXT = "マルチメディアボタンを押し続ける";
        //public const string SCRIPT_MULTIMEDIA_RELEASE_TEXT = "マルチメディアボタンを離す";
        //public const string SCRIPT_ADD_INFO_INTERVAL_TEXT = "インターバル時間を入力してください";
        //public const string SCRIPT_ADD_INFO_KEY_TEXT = "キーボードのキーを押してください";
        //public const string SCRIPT_ADD_INFO_MOUSE_TEXT = "マウスのボタンをクリックしてください";
        //public const string SCRIPT_ADD_INFO_MOUSE_BUTTON_TEXT = "マウスのボタンを設定してください";
        //public const string SCRIPT_ADD_INFO_WHEEL_TEXT = "マウスホイールをスクロールしてください";
        //public const string SCRIPT_ADD_INFO_MOUSE_MOVE_TEXT = "マウス移動量を設定してください";
        //public const string SCRIPT_ADD_INFO_MOUSE_MOVE_X_TEXT = "X方向";
        //public const string SCRIPT_ADD_INFO_MOUSE_MOVE_Y_TEXT = "Y方向";
        //public const string SCRIPT_ADD_INFO_JOY_LEVER_TEXT = "ジョイスティックの出力値を設定してください";
        //public const string SCRIPT_ADD_INFO_JOY_LEVER_X_TEXT = "X軸";
        //public const string SCRIPT_ADD_INFO_JOY_LEVER_Y_TEXT = "Y軸";
        //public const string SCRIPT_ADD_INFO_JOY_HATSW_TEXT = "十字ボタンを設定します。上下左右キーを押してください";
        //public const string SCRIPT_ADD_INFO_JOY_BUTTON_TEXT = "ゲームパッドボタンを設定してください";
        //public const string SCRIPT_ADD_INFO_MM_BUTTON_TEXT = "マルチメディアボタンを設定してください";
        //public const string SCRIPT_SIZE_OVER_MSG = "スクリプト当たりの保存サイズが制限値を超えています";
        // DISPLAY UPDATE FLAG 
        public const int DISPLAY_UPDATE_TYPE_NONE = 0x00;  //表示更新なし
        public const int DISPLAY_UPDATE_TYPE_ADD = 0x01;   //表示更新　項目追加
        public const int DISPLAY_UPDATE_TYPE_DEL = 0x02;   //表示更新　項目削除
        public const int DISPLAY_UPDATE_TYPE_RE = 0x04;    //表示更新　項目内容変更
        // DISPLAY ICON TYPE
        public const int DISPLAY_ICON_TYPE_1TIME = 0;
        public const int DISPLAY_ICON_TYPE_LOOP = 1;
        public const int DISPLAY_ICON_TYPE_FIRE = 2;
        public const int DISPLAY_ICON_TYPE_INTERVAL = 3;
        public const int DISPLAY_ICON_TYPE_KEYDOWN = 4;
        public const int DISPLAY_ICON_TYPE_KEYUP = 5;
        public const int DISPLAY_ICON_TYPE_MUSDOWN = 6;
        public const int DISPLAY_ICON_TYPE_MUSUP = 7;
        public const int DISPLAY_ICON_TYPE_MUSWHEEL = 8;
        public const int DISPLAY_ICON_TYPE_JOY_LEVER_DOWN = 9;
        public const int DISPLAY_ICON_TYPE_JOY_LEVER_UP = 10;
        public const int DISPLAY_ICON_TYPE_JOY_HATSW_DOWN = 11;
        public const int DISPLAY_ICON_TYPE_JOY_HATSW_UP = 12;
        public const int DISPLAY_ICON_TYPE_JOY_BUTTON_DOWN = 13;
        public const int DISPLAY_ICON_TYPE_JOY_BUTTON_UP = 14;
        public const int DISPLAY_ICON_TYPE_MUSMOVE = 15;
        public const int DISPLAY_ICON_TYPE_MM_BUTTON_DOWN = 16;
        public const int DISPLAY_ICON_TYPE_MM_BUTTON_UP = 17;
        public const int DISPLAY_ICON_TYPE_HOLD = 18;
        // DRAG CONTROL ID
        public const int SCRIPT_DRAG_CTRL_NON               = 0x000000;   // ドラッグを開始したコントロール種別　なし
        public const int SCRIPT_DRAG_CTRL_MEMORY            = 0x000001;   // 
        public const int SCRIPT_DRAG_CTRL_EDIT              = 0x000002;   // 
        public const int SCRIPT_DRAG_CTRL_LIST              = 0x000004;   // 
        public const int SCRIPT_DRAG_CTRL_DUSTBOX           = 0x000008;   // 
        public const int SCRIPT_DRAG_CTRL_INTERVAL          = 0x000010;   // 
        public const int SCRIPT_DRAG_CTRL_KEYDOWN           = 0x000020;   // 
        public const int SCRIPT_DRAG_CTRL_KEYUP             = 0x000040;   // 
        public const int SCRIPT_DRAG_CTRL_MOUSECLICK        = 0x000080;   // 
        public const int SCRIPT_DRAG_CTRL_MOUSERELEASE      = 0x000100;   // 
        public const int SCRIPT_DRAG_CTRL_MOUSEWHEEL        = 0x000200;   // 
        public const int SCRIPT_DRAG_CTRL_JOYLLEVERDOWN     = 0x000400;   // 
        public const int SCRIPT_DRAG_CTRL_JOYLLEVERUP       = 0x000800;   // 
        public const int SCRIPT_DRAG_CTRL_JOYRLEVERDOWN     = 0x001000;   // 
        public const int SCRIPT_DRAG_CTRL_JOYRLEVERUP       = 0x002000;   // 
        public const int SCRIPT_DRAG_CTRL_JOYHATSWDOWN      = 0x004000;   // 
        public const int SCRIPT_DRAG_CTRL_JOYHATSWUP        = 0x008000;   // 
        public const int SCRIPT_DRAG_CTRL_JOYBUTTONDOWN     = 0x010000;   // 
        public const int SCRIPT_DRAG_CTRL_JOYBUTTONUP       = 0x020000;   // 
        public const int SCRIPT_DRAG_CTRL_MOUSEMOVE         = 0x040000;   // 
        public const int SCRIPT_DRAG_CTRL_MLTIMEDIADOWN     = 0x080000;   // 
        public const int SCRIPT_DRAG_CTRL_MLTIMEDIAUP       = 0x100000;   // 
        public const int SCRIPT_ADD_MANUAL_NON              = -1;   // スクリプトのアイコンをドラッグして追加する場合のコントロール種別
        public const int SCRIPT_ADD_MANUAL_INTERVAL         = 0x000000;   // 
        public const int SCRIPT_ADD_MANUAL_KEYDOWN          = 0x000001;   // 
        public const int SCRIPT_ADD_MANUAL_KEYUP            = 0x000002;   // 
        public const int SCRIPT_ADD_MANUAL_MOUSECLICK       = 0x000003;   // 
        public const int SCRIPT_ADD_MANUAL_MOUSERELEASE     = 0x000004;   // 
        public const int SCRIPT_ADD_MANUAL_MOUSEWHEEL       = 0x000005;   // 
        public const int SCRIPT_ADD_MANUAL_JOYLLEVERDOWN    = 0x000006;   // 
        public const int SCRIPT_ADD_MANUAL_JOYLLEVERUP      = 0x000007;   // 
        public const int SCRIPT_ADD_MANUAL_JOYRLEVERDOWN    = 0x000008;   // 
        public const int SCRIPT_ADD_MANUAL_JOYRLEVERUP      = 0x000009;   // 
        public const int SCRIPT_ADD_MANUAL_JOYHATSWDOWN     = 0x00000A;   // 
        public const int SCRIPT_ADD_MANUAL_JOYHATSWUP       = 0x00000B;   // 
        public const int SCRIPT_ADD_MANUAL_JOYBUTTONDOWN    = 0x00000C;   // 
        public const int SCRIPT_ADD_MANUAL_JOYBUTTONUP      = 0x00000D;   // 
        public const int SCRIPT_ADD_MANUAL_MOUSEMOVE        = 0x00000E;   // 
        public const int SCRIPT_ADD_MANUAL_MULTIMEDIADOWN   = 0x00000F;   // 
        public const int SCRIPT_ADD_MANUAL_MULTIMEDIAUP     = 0x000010;   // 
        public const int SCRIPT_SCROLL_TARGET_CTRL_NON      = 0x0000;   // スクロールするコントロール　なし
        public const int SCRIPT_SCROLL_TARGET_CTRL_MEMORY   = 0x0001;   // 
        public const int SCRIPT_SCROLL_TARGET_CTRL_EDIT     = 0x0002;   // 
        public const int SCRIPT_SCROLL_TARGET_CTRL_LIST     = 0x0004;   // 

        // Key
        public const int VIRTUAL_KEY_CODE_ENTER = 0x28;     // VK ENTER

        public const int JOY_LEVER_SET_VAL_MIN = -127;  // ジョイスティック出力設定値 最小値
        public const int JOY_LEVER_SET_VAL_MAX = 127;   // ジョイスティック出力設定値 最大値
        public const int MOUSE_MOVE_SET_VAL_MIN = -127;  // マウス移動設定値 最小値
        public const int MOUSE_MOVE_SET_VAL_MAX = 127;   // マウス移動設定値 最大値

        // Device Macro Read/Write
        //public const string DEVICE_MACRO_WRITE_CONFIRM_MSG = "No.{0}位置に書き込みます。よろしいですか？";
        //public const string DEVICE_MACRO_WRITE_POS_ERR_MSG = "書き込み位置を選択してください。";
        //public const string DEVICE_FUNCTION_WRITE_CONFIRM_MSG = "機能設定を書き込みます。よろしいですか？";
        //public const string DEVICE_ENCODER_SCRIPT_WRITE_CONFIRM_MSG = "ダイアルマクロ設定を書き込みます。よろしいですか？";
        //public const string DEVICE_BASE_BUTTON_SETUP_WRITE_CONFIRM_MSG = "ボタン設定を書き込みます。よろしいですか？";
        //public const string DEVICE_BASE_SETUP_WRITE_CONFIRM_MSG = "設定を書き込みます。よろしいですか？";

        // Script File
        //public const string SCRIPT_FILE_SAVE_DIALOG_TITLE = "マクロデータファイルを保存します";
        //public const string SCRIPT_FILE_OPEN_DIALOG_TITLE = "マクロデータファイルを選択して下さい";
        public const string SCRIPT_FILE_EXTENSION = ".mcdf";
        public const string SCRIPT_FILE_FILTER = "MaCro Data File(*.mcdf)|*.mcdf";
        public const string SCRIPT_FILE_SIGNATURE = "BTO";
        public const int SCRIPT_FILE_SIGNATURE_SIZE_LEN = 0x0001;       // SIGNATUREのサイズ長
        public const int SCRIPT_FILE_SIGNATURE_SIZE_MAX = 0x00FF;       // SIGNATUREの最大サイズ
        public const int SCRIPT_FILE_FILE_SIZE_LEN = 0x0004;            // ScriptFileのサイズ長
        public const int SCRIPT_FILE_MODE_SIZE_LEN = 0x0001;            // Modeのデータ長
        public const int SCRIPT_FILE_SCRIPT_SIZE_LEN = 0x0004;          // Script Sizeのデータ長
        //public const string SCRIPT_FILE_IMPORT_ERROR_MSG = "マクロデータファイルの読み込みに失敗しました。";
        public const int SCRIPT_FILE_MIN_SIZE = 0x000A;                 // Script File最小サイズ シグネチャ格納サイズ１＋ファイルサイズ４＋モードサイズ１＋スクリプトサイズ４＝１０バイト

        // Pattern List File
        //public const string PATTERN_LIST_FILE_SAVE_DIALOG_TITLE = "パターンリストファイルを保存します";
        //public const string PATTERN_LIST_FILE_OPEN_DIALOG_TITLE = "パターンリストファイルを選択して下さい";
        //public const string PATTERN_LIST_FILE_EXTENSION = ".txt";
        //public const string PATTERN_LIST_FILE_FILTER = "テキスト（タブ区切り）(*.txt)|*.txt";
        //public const string PATTERN_LIST_FILE_IMPORT_WARNING_MSG = "パターンリストファイルを読み込みます。読み込み後、設定ボタンをクリックすると変わる君本体に保存されます。よろしいですか？";
        //public const string PATTERN_LIST_FILE_HEADER = "#No.\tPD1\tPD2\tPD3\tPD4\tPD5\tPD6\tパターン名称\t実行マクロ名称";

        // Backup File
        //public const string BACKUP_FILE_SAVE_DIALOG_TITLE = "バックアップファイルを保存します";
        //public const string BACKUP_FILE_OPEN_DIALOG_TITLE = "バックアップファイルを選択して下さい";
        public const string BACKUP_FILE_EXTENSION = ".mbf";
        //public const string BACKUP_FILE_FILTER = "バックアップファイル(*.mbf)|*.mbf|すべてのファイル(*.*)|*.*";

        // Backup Restore
        public const int BACKUP_FLAG_NON = 0x0000;
        public const int BACKUP_FLAG_BACKUP = 0x0001;
        public const int BACKUP_FLAG_RESTORE = 0x0002;
        public const int BACKUP_FLAG_RESET = 0x0003;
        //public const string RESTORE_WARNING_MSG = "設定がすべて上書きされ、書き換わります。よろしいですか？";
        //public const string FLASHERASE_WARNING_MSG = "設定をすべて消去します。よろしいですか？";
        //public const string RESET_WARNING_MSG = "すべての設定を工場出荷状態に戻します。よろしいですか？";
        //public const string RESTORE_ERROR_MSG1 = "エラーが発生し処理を中断しました。\n(ERROR CODE={0})";

        // Status Message
        //public const string STATUS_MSG_USB_UNCONNECT = "USB未接続";
        //public const string STATUS_MSG_USB_CONNECT = "USB接続中";

        // LED Setup
        public const int LED_RGB_COLOR_NUM = 3;
        public const int LED_COLOR_DEFAULT_SET_NUM = 9;
        //public const int LED_DUTY_MAX = 60;
        public const int LED_R_DUTY_MAX = 100;
        public const int LED_G_DUTY_MAX = 100;
        public const int LED_B_DUTY_MAX = 100;
        //public const int LED_R_DUTY_MAX = 90;
        //public const int LED_G_DUTY_MAX = 90;
        //public const int LED_B_DUTY_MAX = 90;
        public const int LED_PREVIEW_TIME = 20000;   // Preview 時間[単位100us] 設定値20000 = 2000ms
        public const byte LED_BRIGHTNESS_LEVEL_SET_NUM = 3;
        public const byte LED_BRIGHTNESS_LEVEL_NORMAL = 0;
        public const byte LED_BRIGHTNESS_LEVEL_DARK = 1;
        public const byte LED_BRIGHTNESS_LEVEL_LIGHT = 2;
        public const byte LED_SLEEP_SETTING_NUM = 2;
        public const byte LED_SLEEP_ENABLED = 0;
        public const byte LED_SLEEP_DISABLED = 1;
        public const byte LED_LIGHT_TYPE_MODE_SETTING_NUM = 2;
        public const byte LED_LIGHT_TYPE_MODE_ON = 0;
        public const byte LED_LIGHT_TYPE_MODE_OFF = 1;
        public const byte LED_LIGHT_TYPE_FUNC_SETTING_NUM = 3;
        public const byte LED_LIGHT_TYPE_FUNC_ON = 0;
        public const byte LED_LIGHT_TYPE_FUNC_SLOW = 1;
        public const byte LED_LIGHT_TYPE_FUNC_FLASH = 2;
        public const byte LED_LIGHT_OFF_MODE_TIME_MAX = 180;            // MODE時のLED OFFまでの最大設定値[s]
        public const byte ENCODER_TYPEMATIC_SETTING_NUM = 2;
        public const byte ENCODER_TYPEMATIC_HIT = 0;
        public const byte ENCODER_TYPEMATIC_PRESS = 1;


        // 機能設定の設定
        public const int SETTING_NUM = 24;                  // 設定数 mode 3 * func 4 * cc ccw 2
        public const int DEVICE_DATA_LEN = 8;               // デバイス設定データ長
        public const int DEVICE_DATA_SET_TYPE_IDX = 0;      // デバイス設定データ 設定タイプ格納位置
        public const int DEVICE_DATA_CLICK_IDX = 1;         // デバイス設定データ　マウスデータ　クリックデータ格納位置
        public const int DEVICE_DATA_X_MOVE_IDX = 2;        // デバイス設定データ　マウスデータ　X移動量格納位置
        public const int DEVICE_DATA_Y_MOVE_IDX = 3;        // デバイス設定データ　マウスデータ　Y移動量格納位置
        public const int DEVICE_DATA_WHEEL_IDX = 4;         // デバイス設定データ　マウスデータ　ホイールスクロール量格納位置
        public const int DEVICE_DATA_MODIFIER_IDX = 1;      // デバイス設定データ　キーボードデータ　モディファイデータ格納位置
        public const int DEVICE_DATA_KEY1_IDX = 2;          // デバイス設定データ　キーボードデータ　キーデータ1格納位置
        public const int DEVICE_DATA_KEY2_IDX = 3;          // デバイス設定データ　キーボードデータ　キーデータ2格納位置
        public const int DEVICE_DATA_KEY3_IDX = 4;          // デバイス設定データ　キーボードデータ　キーデータ3格納位置
        public const int DEVICE_DATA_MULTIMEDIA_VAL1_IDX = 1;// デバイス設定データ　マルチメディア　データ1格納位置
        public const int DEVICE_DATA_MULTIMEDIA_VAL2_IDX = 2;// デバイス設定データ　マルチメディア　データ2格納位置
        public const int DEVICE_DATA_JOY_BUTTON1_IDX = 1;   // デバイス設定データ　ジョイパッドデータ　ボタンデータ格納位置
        public const int DEVICE_DATA_JOY_BUTTON2_IDX = 2;   // デバイス設定データ　ジョイパッドデータ　ボタンデータ格納位置
        public const int DEVICE_DATA_JOY_HAT_SW_IDX = 3;	// デバイス設定データ　ジョイパッドデータ　HAT SW格納位置
        public const int DEVICE_DATA_JOY_X_MOVE_IDX = 4;	// デバイス設定データ　ジョイパッドデータ　X軸移動量格納位置
        public const int DEVICE_DATA_JOY_Y_MOVE_IDX = 5;	// デバイス設定データ　ジョイパッドデータ　Y軸移動量格納位置
        public const int DEVICE_DATA_SENSE_IDX = 7;         // デバイス設定データ　ロータリエンコーダの感度調整格納位置
        public const int MOUSE_DATA_LEN = 4;                // マウスデータ長
        public const int MOUSE_DATA_CLICK_IDX = 0;          // マウスデータ　クリックデータ格納位置
        public const int MOUSE_DATA_X_MOVE_IDX = 1;         // マウスデータ　X移動量格納位置
        public const int MOUSE_DATA_Y_MOVE_IDX = 2;         // マウスデータ　Y移動量格納位置
        public const int MOUSE_DATA_WHEEL_IDX = 3;          // マウスデータ　ホイールスクロール量格納位置
        public const byte MOUSE_DATA_LEFT_CLICK = 0x01;     // マウスデータ　左クリック
        public const byte MOUSE_DATA_RIGHT_CLICK = 0x02;    // マウスデータ　右クリック
        public const byte MOUSE_DATA_WHEEL_CLICK = 0x04;    // マウスデータ　ホイールクリック
        public const byte MOUSE_DATA_BUTTON4_CLICK = 0x08;  // マウスデータ　ボタン4
        public const byte MOUSE_DATA_BUTTON5_CLICK = 0x10;  // マウスデータ　ボタン5
        public const int KEYBOARD_DATA_LEN = 4;             // キーボードデータ長
        public const int KEYBOARD_DATA_MODIFIER_IDX = 0;    // キーボードデータ　モディファイデータ格納位置
        public const int KEYBOARD_DATA_KEY1_IDX = 1;        // キーボードデータ　キーデータ1格納位置
        public const int KEYBOARD_DATA_KEY2_IDX = 2;        // キーボードデータ　キーデータ2格納位置
        public const int KEYBOARD_DATA_KEY3_IDX = 3;        // キーボードデータ　キーデータ3格納位置
        public const int MULTIMEDIA_DATA_LEN = 2;           // マルチメディアデータ長
        public const int MULTIMEDIA_DATA_VAL1_IDX = 0;      // マルチメディアデータ　データ1格納位置
        public const byte MULTIMEDIA_DATA_PLAY = 0x01;      // マルチメディアデータ　再生
        public const byte MULTIMEDIA_DATA_PAUSE = 0x02;     // マルチメディアデータ　一時停止
        public const byte MULTIMEDIA_DATA_REC = 0x04;       // マルチメディアデータ　REC
        public const byte MULTIMEDIA_DATA_FORWARD = 0x08;   // マルチメディアデータ　早送り
        public const byte MULTIMEDIA_DATA_REWIND = 0x10;    // マルチメディアデータ　巻戻し
        public const byte MULTIMEDIA_DATA_NEXT = 0x20;      // マルチメディアデータ　次
        public const byte MULTIMEDIA_DATA_PREVIOUS = 0x40;  // マルチメディアデータ　前
        public const byte MULTIMEDIA_DATA_STOP = 0x80;      // マルチメディアデータ　停止
        public const int MULTIMEDIA_DATA_VAL2_IDX = 1;      // マルチメディアデータ　データ2格納位置
        public const byte MULTIMEDIA_DATA_PLAYPAUSE = 0x01; // マルチメディアデータ　再生/一時停止
        public const byte MULTIMEDIA_DATA_MUTE = 0x02;      // マルチメディアデータ　消音
        public const byte MULTIMEDIA_DATA_VOL_UP = 0x04;    // マルチメディアデータ　ボリュームアップ
        public const byte MULTIMEDIA_DATA_VOL_DOWN = 0x08;  // マルチメディアデータ　ボリュームダウン
        public const int JOYPAD_DATA_LEN = 5;               // ジョイパッドデータ長
        public const int JOYPAD_DATA_BUTTON1_IDX = 0;       // ジョイパッドデータ　ボタンデータ格納位置
        public const int JOYPAD_DATA_BUTTON2_IDX = 1;       // ジョイパッドデータ　ボタンデータ格納位置
        public const int JOYPAD_DATA_HAT_SW_IDX = 2;        // ジョイパッドデータ　ハットスイッチデータ格納位置
        public const int JOYPAD_DATA_X_MOVE_IDX = 3;        // ジョイパッドデータ　X軸移動量格納位置
        public const int JOYPAD_DATA_Y_MOVE_IDX = 4;        // ジョイパッドデータ　Y軸移動量格納位置
        public const byte HAT_SWITCH_NORTH = 0x00;          // ジョイパッドデータ　ハットスイッチ　北
        public const byte HAT_SWITCH_NORTH_EAST = 0x01;     // ジョイパッドデータ　ハットスイッチ　北東
        public const byte HAT_SWITCH_EAST = 0x02;           // ジョイパッドデータ　ハットスイッチ　東
        public const byte HAT_SWITCH_SOUTH_EAST = 0x03;     // ジョイパッドデータ　ハットスイッチ　南東
        public const byte HAT_SWITCH_SOUTH = 0x04;          // ジョイパッドデータ　ハットスイッチ　南
        public const byte HAT_SWITCH_SOUTH_WEST = 0x05;     // ジョイパッドデータ　ハットスイッチ　南西
        public const byte HAT_SWITCH_WEST = 0x06;           // ジョイパッドデータ　ハットスイッチ　西
        public const byte HAT_SWITCH_NORTH_WEST = 0x07;     // ジョイパッドデータ　ハットスイッチ　北西
        public const byte HAT_SWITCH_NULL = 0x08;           // ジョイパッドデータ　ハットスイッチ　なし

        public const int SET_TYPE_MIN = 0;                  // 設定タイプ　最小値
        public const int SET_TYPE_MAX = 44;                 // 設定タイプ　最大値
        public const int SET_TYPE_MOUSE_MIN = 1;            // 設定タイプ　マウス最小値
        public const int SET_TYPE_MOUSE_MAX = 8;            // 設定タイプ　マウス最大値
        public const int SET_TYPE_MULTIMEDIA_MIN = 10;      // 設定タイプ　マルチメディア最小値
        public const int SET_TYPE_MULTIMEDIA_MAX = 20;      // 設定タイプ　マルチメディア最大値
        public const int SET_TYPE_JOYPAD_MIN = 21;          // 設定タイプ　ジョイパッド最小値
        public const int SET_TYPE_JOYPAD_MAX = 39;          // 設定タイプ　ジョイパッド最大値
        public const int SET_TYPE_NONE = 0;                 // 設定タイプ　なし
        public const int SET_TYPE_MOUSE_LCLICK = 1;         // 設定タイプ　マウス　左クリック
        public const int SET_TYPE_MOUSE_RCLICK = 2;         // 設定タイプ　マウス　右クリック
        public const int SET_TYPE_MOUSE_WHCLICK = 3;        // 設定タイプ　マウス　ホイールクリック
        public const int SET_TYPE_MOUSE_B4CLICK = 4;        // 設定タイプ　マウス　ボタン4
        public const int SET_TYPE_MOUSE_B5CLICK = 5;        // 設定タイプ　マウス　ボタン5
        public const int SET_TYPE_MOUSE_DCLICK = 6;         // 設定タイプ　マウス　ダブルクリック
        public const int SET_TYPE_MOUSE_MOVE = 7;           // 設定タイプ　マウス　上下左右移動
        public const int SET_TYPE_MOUSE_WHSCROLL = 8;       // 設定タイプ　マウス　ホイールスクロール
        public const int SET_TYPE_KEYBOARD_KEY = 9;         // 設定タイプ　キーボード　キー
        public const int SET_TYPE_MULTIMEDIA_PLAY = 10;     // 設定タイプ　マルチメディア　再生
        public const int SET_TYPE_MULTIMEDIA_PAUSE = 11;    // 設定タイプ　マルチメディア　一時停止
        public const int SET_TYPE_MULTIMEDIA_STOP = 12;     // 設定タイプ　マルチメディア　停止
        public const int SET_TYPE_MULTIMEDIA_REC = 13;      // 設定タイプ　マルチメディア　REC
        public const int SET_TYPE_MULTIMEDIA_FORWORD = 14;  // 設定タイプ　マルチメディア　早送り
        public const int SET_TYPE_MULTIMEDIA_REWIND = 15;   // 設定タイプ　マルチメディア　巻戻し
        public const int SET_TYPE_MULTIMEDIA_NEXT = 16;     // 設定タイプ　マルチメディア　次
        public const int SET_TYPE_MULTIMEDIA_PREVIOUS = 17; // 設定タイプ　マルチメディア　前
        public const int SET_TYPE_MULTIMEDIA_MUTE = 18;     // 設定タイプ　マルチメディア　消音
        public const int SET_TYPE_MULTIMEDIA_VOLUMEUP = 19; // 設定タイプ　マルチメディア　ボリュームアップ
        public const int SET_TYPE_MULTIMEDIA_VOLUMEDOWN = 20;// 設定タイプ　マルチメディア　ボリュームダウン
        public const int SET_TYPE_JOYPAD_XY = 21;            // 設定タイプ　ジョイパッド　左X-Y軸
        public const int SET_TYPE_JOYPAD_ZRZ = 22;          // 設定タイプ　ジョイパッド　右X-Y軸
        public const int SET_TYPE_JOYPAD_B01 = 23;          // 設定タイプ　ジョイパッド　ボタン1
        public const int SET_TYPE_JOYPAD_B02 = 24;          // 設定タイプ　ジョイパッド　ボタン2
        public const int SET_TYPE_JOYPAD_B03 = 25;          // 設定タイプ　ジョイパッド　ボタン3
        public const int SET_TYPE_JOYPAD_B04 = 26;          // 設定タイプ　ジョイパッド　ボタン4
        public const int SET_TYPE_JOYPAD_B05 = 27;          // 設定タイプ　ジョイパッド　ボタン5
        public const int SET_TYPE_JOYPAD_B06 = 28;          // 設定タイプ　ジョイパッド　ボタン6
        public const int SET_TYPE_JOYPAD_B07 = 29;          // 設定タイプ　ジョイパッド　ボタン7
        public const int SET_TYPE_JOYPAD_B08 = 30;          // 設定タイプ　ジョイパッド　ボタン8
        public const int SET_TYPE_JOYPAD_B09 = 31;          // 設定タイプ　ジョイパッド　ボタン9
        public const int SET_TYPE_JOYPAD_B10 = 32;          // 設定タイプ　ジョイパッド　ボタン10
        public const int SET_TYPE_JOYPAD_B11 = 33;          // 設定タイプ　ジョイパッド　ボタン11
        public const int SET_TYPE_JOYPAD_B12 = 34;          // 設定タイプ　ジョイパッド　ボタン12
        public const int SET_TYPE_JOYPAD_B13 = 35;          // 設定タイプ　ジョイパッド　ボタン13
        public const int SET_TYPE_JOYPAD_HSW_NORTH = 36;    // 設定タイプ　ジョイパッド　ハットスイッチ　北
        public const int SET_TYPE_JOYPAD_HSW_SOUTH = 37;    // 設定タイプ　ジョイパッド　ハットスイッチ　南
        public const int SET_TYPE_JOYPAD_HSW_WEST = 38;     // 設定タイプ　ジョイパッド　ハットスイッチ　西
        public const int SET_TYPE_JOYPAD_HSW_EAST = 39;	    // 設定タイプ　ジョイパッド　ハットスイッチ　東
        public const int SET_TYPE_NUMBER_UP = 40;           // 設定タイプ　特殊　数字連続入力 UP
        public const int SET_TYPE_NUMBER_DOWN = 41;         // 設定タイプ　特殊　数字連続入力 DOWN
        public const int SET_TYPE_ENCODER_SCRIPT1 = 42;     // 設定タイプ　ダイアルスクリプト1
        public const int SET_TYPE_ENCODER_SCRIPT2 = 43;     // 設定タイプ　ダイアルスクリプト2
        public const int SET_TYPE_ENCODER_SCRIPT3 = 44;     // 設定タイプ　ダイアルスクリプト3

        //       public const string MOUSE_MOVE_TEXT = "X,Y移動量";
        public const decimal MOUSE_MOVE_MIN = -127;
        public const decimal MOUSE_MOVE_MAX = 127;
        //public const string MOUSE_SCROLL_TEXT = "スクロール数";
        public const decimal MOUSE_SCROLL_MIN = -127;
        public const decimal MOUSE_SCROLL_MAX = 127;
        public const int KEYBOARD_SET_KEY_NUM = 3;          // キーボード　設定可能キー数
        //public const string KEYBOARD_SET_KEY_EMPTY = "ここに入力";          // 
        //public const string KEYBOARD_INPUTKEY_TEXT = "設定キー";
        //public const string JOYPAD_MOVE_TEXT = "X,Y軸移動量";
        public const decimal JOYPAD_MOVE_MIN = -127;
        public const decimal JOYPAD_MOVE_MAX = 127;

        public const byte USB_KEY_CODE_CTRL_L   = 0xE0;      // USBキーコード ctrl left
        public const byte USB_KEY_CODE_CTRL_R   = 0xE4;      // USBキーコード ctrl right
        public const byte USB_KEY_CODE_SHIFT_L  = 0xE1;      // USBキーコード shift left
        public const byte USB_KEY_CODE_SHIFT_R  = 0xE5;      // USBキーコード shift right
        public const byte USB_KEY_CODE_ALT_L    = 0xE2;      // USBキーコード alt left
        public const byte USB_KEY_CODE_ALT_R    = 0xE6;      // USBキーコード alt right
        public const byte USB_KEY_CODE_WIN_L    = 0xE3;      // USBキーコード win left
        public const byte USB_KEY_CODE_WIN_R    = 0xE7;      // USBキーコード win right

        public const byte USB_KEY_BIT_CTRL_L    = 0x01;      // USBキーコード ctrl left
        public const byte USB_KEY_BIT_CTRL_R    = 0x10;      // USBキーコード ctrl right
        public const byte USB_KEY_BIT_SHIFT_L   = 0x02;      // USBキーコード shift left
        public const byte USB_KEY_BIT_SHIFT_R   = 0x20;      // USBキーコード shift right
        public const byte USB_KEY_BIT_ALT_L     = 0x04;      // USBキーコード alt left
        public const byte USB_KEY_BIT_ALT_R     = 0x40;      // USBキーコード alt right
        public const byte USB_KEY_BIT_WIN_L     = 0x08;      // USBキーコード win left
        public const byte USB_KEY_BIT_WIN_R     = 0x80;      // USBキーコード win right

        public const string HELP_LINK_URL_FIRST = "http://bit-trade-one.co.jp/product/bitferrous/bfrom11/";
        public const string HELP_LINK_URL_LAST = "";
        public const string HELP_LINK_URL_EN = "manual_eng/";
        public const string HELP_LINK_URL_JP = "manual_jpn/";
        public const string HELP_LINK_URL_CN = "manual_cn/";
        public const string HELP_LINK_URL_KR = "manual_ko/";
        public const string HELP_LINK_URL_TW = "manual_tw/";

        public const string CULTURE_CODE_NAME_EN = "en-US";
        public const string CULTURE_CODE_NAME_EN_S = "en";
        public const string CULTURE_CODE_NAME_JP = "ja-JP";
        public const string CULTURE_CODE_NAME_JP_S = "ja";
        public const string CULTURE_CODE_NAME_CN = "zh-CN";
        public const string CULTURE_CODE_NAME_CN_S = "zh";
        public const string CULTURE_CODE_NAME_KR = "ko-KR";
        public const string CULTURE_CODE_NAME_KR_S = "ko";
        public const string CULTURE_CODE_NAME_TW = "zh-TW";
        public const string CULTURE_CODE_NAME_TW_S = "zh";

        public const byte KEYBOARD_TYPE_NUM = 2;
        public const byte KEYBOARD_TYPE_US = 0;
        public const byte KEYBOARD_TYPE_JA = 1;

        public const int KEYBOARD_SETUP_ASSIST_STATUS_NONE = 0;
        public const int KEYBOARD_SETUP_ASSIST_STATUS_KEY1 = 1;
        public const int KEYBOARD_SETUP_ASSIST_STATUS_KEY2 = 2;
        public const int KEYBOARD_SETUP_ASSIST_STATUS_COMP = 3;
    }

    [Serializable()]
    public class SystemSettingInfo
    {
        [System.Xml.Serialization.XmlElement("KeyboardType")]
        public byte Keyboard_Type;                    // 

        public SystemSettingInfo()
        {
            try
            {
                if (CultureInfo.CurrentCulture.ToString() == "ja-JP")
                {
                    Keyboard_Type = Constants.KEYBOARD_TYPE_JA;
                }
                else
                {
                    Keyboard_Type = Constants.KEYBOARD_TYPE_US;
                }
            }
            catch
            {
            }
        }
        public void init()
        {
            try
            {
                if (CultureInfo.CurrentCulture.ToString() == "ja-JP")
                {
                    Keyboard_Type = Constants.KEYBOARD_TYPE_JA;
                }
                else
                {
                    Keyboard_Type = Constants.KEYBOARD_TYPE_US;
                }
            }
            catch
            {
            }
        }
    }
} //namespace HID_PnP_Demo