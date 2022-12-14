using System;
using System.IO;
//using System.Windows.Input;
using System.Windows;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;

namespace RevOmate
{
    class KeyCode
    {
        // DLLをインポートする必要がある
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern short GetKeyState(int nVirtKey);

        //private int control_key_status = 0;
        //public void Clr_ControlKeyStatus()
        //{
        //    control_key_status = 0;
        //}

        public byte Get_VKtoUSBkey(int p_VK_Code, byte p_keyboard_type, bool p_LR_flag)
        {
            byte ret_val = 0;
            try
            {
                if (0 <= p_VK_Code && p_VK_Code < VKtoUSBkey.Length)
                {
                    if (p_keyboard_type == Constants.KEYBOARD_TYPE_JA)
                    {
                        ret_val = VKtoUSBkey[p_VK_Code];
                    }
                    else
                    {
                        ret_val = VKtoUSBkey_US[p_VK_Code];
                    }
                }

#if true
                if (p_LR_flag == true)
                {   // 左右キーを判別

                    // 左右キーの判定
                    if (ret_val == Constants.USB_KEY_CODE_CTRL_L)
                    {   // Ctrl key
                        int tmp_int = GetKeyState((int)Keys.RControlKey);
                        if (tmp_int < 0)
                        {   // 右shift?
                            ret_val = Constants.USB_KEY_CODE_CTRL_R;
                        }
                    }
                    else if (ret_val == Constants.USB_KEY_CODE_SHIFT_L)
                    {   // Shift key
                        int tmp_int = GetKeyState((int)Keys.RShiftKey);
                        if (tmp_int < 0)
                        {   // 右shift?
                            ret_val = Constants.USB_KEY_CODE_SHIFT_R;
                        }
                    }
                    else if (ret_val == Constants.USB_KEY_CODE_ALT_L)
                    {   // Alt key
                        int tmp_int = GetKeyState((int)Keys.RMenu);
                        if (tmp_int < 0)
                        {   // 右shift?
                            ret_val = Constants.USB_KEY_CODE_ALT_R;
                        }
                    }
                    else if (ret_val == Constants.USB_KEY_CODE_WIN_L)
                    {   // Win key
                        int tmp_int = GetKeyState((int)Keys.RWin);
                        if (tmp_int < 0)
                        {   // 右shift?
                            ret_val = Constants.USB_KEY_CODE_WIN_R;
                        }
                    }
                }

#endif
            }
            catch
            {
            }
            return ret_val;
        }
        public string Get_KeyCode_Name(byte p_usb_key_code, byte p_keyboard_type)
        {
            string ret_key_name = "";
            try
            {
                if (p_keyboard_type == Constants.KEYBOARD_TYPE_JA)
                {
                    ret_key_name = USB_KeyCode_Name[p_usb_key_code];
                }
                else
                {
                    ret_key_name = USB_KeyCode_Name_US[p_usb_key_code];
                }
            }
            catch
            {
            }
            return ret_key_name;
        }
#if true
        //バーチャルキーコードとUSBキーコードの変換用配列
        // 日本語キーボード
        private byte[] VKtoUSBkey = new byte[256]{
            0x00,   //0
            0x00,   //1
            0x00,   //2
            0x00,   //3
            0x00,   //4
            0x00,   //5
            0x00,   //6
            0x00,   //7
            0x2A,   //8
            0x2B,   //9
            0x00,   //10
            0x00,   //11
            0x00,   //12
            0x28,   //13
            0x00,   //14
            0x00,   //15
            0xE1,   //16
            0xE0,   //17
            0xE2,   //18
            0x48,   //19
            0x39,   //20
            0x88,   //21
            0x00,   //22
            0x00,   //23
            0x00,   //24
            0x35,   //25
            0x00,   //26
            0x29,   //27
            0x8A,   //28
            0x8B,   //29
            0x00,   //30
            0x00,   //31
            0x2C,   //32
            0x4B,   //33
            0x4E,   //34
            0x4D,   //35
            0x4A,   //36
            0x50,   //37
            0x52,   //38
            0x4F,   //39
            0x51,   //40
            0x00,   //41
            0x00,   //42
            0x00,   //43
            0x46,   //44
            0x49,   //45
            0x4C,   //46
            0x00,   //47
            0x27,   //48
            0x1E,   //49
            0x1F,   //50
            0x20,   //51
            0x21,   //52
            0x22,   //53
            0x23,   //54
            0x24,   //55
            0x25,   //56
            0x26,   //57
            0x00,   //58
            0x00,   //59
            0x00,   //60
            0x00,   //61
            0x00,   //62
            0x00,   //63
            0x00,   //64
            0x04,   //65
            0x05,   //66
            0x06,   //67
            0x07,   //68
            0x08,   //69
            0x09,   //70
            0x0A,   //71
            0x0B,   //72
            0x0C,   //73
            0x0D,   //74
            0x0E,   //75
            0x0F,   //76
            0x10,   //77
            0x11,   //78
            0x12,   //79
            0x13,   //80
            0x14,   //81
            0x15,   //82
            0x16,   //83
            0x17,   //84
            0x18,   //85
            0x19,   //86
            0x1A,   //87
            0x1B,   //88
            0x1C,   //89
            0x1D,   //90
            0xE3,   //91
            0xE7,   //92
            0x65,   //93
            0x00,   //94
            0x00,   //95
            0x62,   //96
            0x59,   //97
            0x5A,   //98
            0x5B,   //99
            0x5C,   //100
            0x5D,   //101
            0x5E,   //102
            0x5F,   //103
            0x60,   //104
            0x61,   //105
            0x55,   //106
            0x57,   //107
            0x85,   //108
            0x56,   //109
            0x63,   //110
            0x54,   //111
            0x3A,   //112
            0x3B,   //113
            0x3C,   //114
            0x3D,   //115
            0x3E,   //116
            0x3F,   //117
            0x40,   //118
            0x41,   //119
            0x42,   //120
            0x43,   //121
            0x44,   //122
            0x45,   //123
            0x68,   //124
            0x69,   //125
            0x6A,   //126
            0x6B,   //127
            0x6C,   //128
            0x6D,   //129
            0x6E,   //130
            0x6F,   //131
            0x70,   //132
            0x71,   //133
            0x72,   //134
            0x73,   //135
            0x00,   //136
            0x00,   //137
            0x00,   //138
            0x00,   //139
            0x00,   //140
            0x00,   //141
            0x00,   //142
            0x00,   //143
            0x53,   //144
            0x47,   //145
            0x67,   //146
            0x00,   //147
            0x00,   //148
            0x00,   //149
            0x00,   //150
            0x00,   //151
            0x00,   //152
            0x00,   //153
            0x00,   //154
            0x00,   //155
            0x00,   //156
            0x00,   //157
            0x00,   //158
            0x00,   //159
            0xE1,   //160
            0xE5,   //161
            0xE0,   //162
            0xE4,   //163
            0xE2,   //164
            0xE6,   //165
            0x00,   //166
            0x00,   //167
            0x00,   //168
            0x00,   //169
            0x00,   //170
            0x00,   //171
            0x00,   //172
            0x00,   //173
            0x00,   //174
            0x00,   //175
            0x00,   //176
            0x00,   //177
            0x00,   //178
            0x00,   //179
            0x00,   //180
            0x00,   //181
            0x00,   //182
            0x00,   //183
            0x00,   //184
            0x00,   //185
            0x34,   //186
            0x33,   //187
            0x36,   //188
            0x2D,   //189
            0x37,   //190
            0x38,   //191
            0x2F,   //192
            0x00,   //193
            0x00,   //194
            0x00,   //195
            0x00,   //196
            0x00,   //197
            0x00,   //198
            0x00,   //199
            0x00,   //200
            0x00,   //201
            0x00,   //202
            0x00,   //203
            0x00,   //204
            0x00,   //205
            0x00,   //206
            0x00,   //207
            0x00,   //208
            0x00,   //209
            0x00,   //210
            0x00,   //211
            0x00,   //212
            0x00,   //213
            0x00,   //214
            0x00,   //215
            0x00,   //216
            0x00,   //217
            0x00,   //218
            0x30,   //219
            0x89,   //220
            0x32,   //221
            0x2E,   //222
            0x00,   //223
            0x00,   //224
            0x00,   //225
            0x87,   //226
            0x00,   //227
            0x00,   //228
            0x35,   //229
            0x00,   //230
            0x00,   //231
            0x00,   //232
            0x00,   //233
            0x00,   //234
            0x00,   //235
            0x00,   //236
            0x00,   //237
            0x00,   //238
            0x00,   //239
            0x39,   //240
            0x00,   //241
            0x39,   //242
            0x35,   //243
            0x35,   //244
            0x00,   //245
            0x00,   //246
            0x00,   //247
            0x00,   //248
            0x00,   //249
            0x00,   //250
            0x00,   //251
            0x00,   //252
            0x00,   //253
            0x00,   //254
            0x00,   //255
       };
        // USキーボード
        private byte[] VKtoUSBkey_US = new byte[256]{
            0x00,   //0
            0x00,   //1
            0x00,   //2
            0x00,   //3
            0x00,   //4
            0x00,   //5
            0x00,   //6
            0x00,   //7
            0x2A,   //8
            0x2B,   //9
            0x00,   //10
            0x00,   //11
            0x00,   //12
            0x28,   //13
            0x00,   //14
            0x00,   //15
            0xE1,   //16
            0xE0,   //17
            0xE2,   //18
            0x48,   //19
            0x39,   //20
            0x88,   //21
            0x00,   //22
            0x00,   //23
            0x00,   //24
            0x35,   //25
            0x00,   //26
            0x29,   //27
            0x8A,   //28
            0x8B,   //29
            0x00,   //30
            0x00,   //31
            0x2C,   //32
            0x4B,   //33
            0x4E,   //34
            0x4D,   //35
            0x4A,   //36
            0x50,   //37
            0x52,   //38
            0x4F,   //39
            0x51,   //40
            0x00,   //41
            0x00,   //42
            0x00,   //43
            0x46,   //44
            0x49,   //45
            0x4C,   //46
            0x00,   //47
            0x27,   //48
            0x1E,   //49
            0x1F,   //50
            0x20,   //51
            0x21,   //52
            0x22,   //53
            0x23,   //54
            0x24,   //55
            0x25,   //56
            0x26,   //57
            0x00,   //58
            0x00,   //59
            0x00,   //60
            0x00,   //61
            0x00,   //62
            0x00,   //63
            0x00,   //64
            0x04,   //65
            0x05,   //66
            0x06,   //67
            0x07,   //68
            0x08,   //69
            0x09,   //70
            0x0A,   //71
            0x0B,   //72
            0x0C,   //73
            0x0D,   //74
            0x0E,   //75
            0x0F,   //76
            0x10,   //77
            0x11,   //78
            0x12,   //79
            0x13,   //80
            0x14,   //81
            0x15,   //82
            0x16,   //83
            0x17,   //84
            0x18,   //85
            0x19,   //86
            0x1A,   //87
            0x1B,   //88
            0x1C,   //89
            0x1D,   //90
            0xE3,   //91
            0xE7,   //92
            0x65,   //93
            0x00,   //94
            0x00,   //95
            0x62,   //96
            0x59,   //97
            0x5A,   //98
            0x5B,   //99
            0x5C,   //100
            0x5D,   //101
            0x5E,   //102
            0x5F,   //103
            0x60,   //104
            0x61,   //105
            0x55,   //106
            0x57,   //107
            0x85,   //108
            0x56,   //109
            0x63,   //110
            0x54,   //111
            0x3A,   //112
            0x3B,   //113
            0x3C,   //114
            0x3D,   //115
            0x3E,   //116
            0x3F,   //117
            0x40,   //118
            0x41,   //119
            0x42,   //120
            0x43,   //121
            0x44,   //122
            0x45,   //123
            0x68,   //124
            0x69,   //125
            0x6A,   //126
            0x6B,   //127
            0x6C,   //128
            0x6D,   //129
            0x6E,   //130
            0x6F,   //131
            0x70,   //132
            0x71,   //133
            0x72,   //134
            0x73,   //135
            0x00,   //136
            0x00,   //137
            0x00,   //138
            0x00,   //139
            0x00,   //140
            0x00,   //141
            0x00,   //142
            0x00,   //143
            0x53,   //144
            0x47,   //145
            0x67,   //146
            0x00,   //147
            0x00,   //148
            0x00,   //149
            0x00,   //150
            0x00,   //151
            0x00,   //152
            0x00,   //153
            0x00,   //154
            0x00,   //155
            0x00,   //156
            0x00,   //157
            0x00,   //158
            0x00,   //159
            0xE1,   //160
            0xE5,   //161
            0xE0,   //162
            0xE4,   //163
            0xE2,   //164
            0xE6,   //165
            0x00,   //166
            0x00,   //167
            0x00,   //168
            0x00,   //169
            0x00,   //170
            0x00,   //171
            0x00,   //172
            0x00,   //173
            0x00,   //174
            0x00,   //175
            0x00,   //176
            0x00,   //177
            0x00,   //178
            0x00,   //179
            0x00,   //180
            0x00,   //181
            0x00,   //182
            0x00,   //183
            0x00,   //184
            0x00,   //185
            0x33,   //186
            0x2E,   //187
            0x36,   //188
            0x2D,   //189
            0x37,   //190
            0x38,   //191
            0x35,   //192
            0x00,   //193
            0x00,   //194
            0x00,   //195
            0x00,   //196
            0x00,   //197
            0x00,   //198
            0x00,   //199
            0x00,   //200
            0x00,   //201
            0x00,   //202
            0x00,   //203
            0x00,   //204
            0x00,   //205
            0x00,   //206
            0x00,   //207
            0x00,   //208
            0x00,   //209
            0x00,   //210
            0x00,   //211
            0x00,   //212
            0x00,   //213
            0x00,   //214
            0x00,   //215
            0x00,   //216
            0x00,   //217
            0x00,   //218
            0x2F,   //219
            0x31,   //220
            0x30,   //221
            0x34,   //222
            0x00,   //223
            0x00,   //224
            0x00,   //225
            0x87,   //226
            0x00,   //227
            0x00,   //228
            0x35,   //229
            0x00,   //230
            0x00,   //231
            0x00,   //232
            0x00,   //233
            0x00,   //234
            0x00,   //235
            0x00,   //236
            0x00,   //237
            0x00,   //238
            0x00,   //239
            0x39,   //240
            0x00,   //241
            0x39,   //242
            0x35,   //243
            0x35,   //244
            0x00,   //245
            0x00,   //246
            0x00,   //247
            0x00,   //248
            0x00,   //249
            0x00,   //250
            0x00,   //251
            0x00,   //252
            0x00,   //253
            0x00,   //254
            0x00,   //255
       };
#endif

#if false
        //USBキーコードの名称配列
        public string[] USB_KeyCode_Name = new string[256]{
            "",   //0
            "",   //1
            "",   //2
            "",   //3
            "Ａ",   //4
            "Ｂ",   //5
            "Ｃ",   //6
            "Ｄ",   //7
            "Ｅ",   //8
            "Ｆ",   //9
            "Ｇ",   //10
            "Ｈ",   //11
            "Ｉ",   //12
            "Ｊ",   //13
            "Ｋ",   //14
            "Ｌ",   //15
            "Ｍ",   //16
            "Ｎ",   //17
            "Ｏ",   //18
            "Ｐ",   //19
            "Ｑ",   //20
            "Ｒ",   //21
            "Ｓ",   //22
            "Ｔ",   //23
            "Ｕ",   //24
            "Ｖ",   //25
            "Ｗ",   //26
            "Ｘ",   //27
            "Ｙ",   //28
            "Ｚ",   //29
            "１",   //30
            "２",   //31
            "３",   //32
            "４",   //33
            "５",   //34
            "６",   //35
            "７",   //36
            "８",   //37
            "９",   //38
            "０",   //39
            "Ｅｎｔｅｒ",   //40
            "ＥＳＣ",   //41
            "ＢＳ",   //42
            "Ｔａｂ",   //43
            "Ｓｐａｃｅ",   //44
            "ー",   //45
            "＾",   //46
            "＠",   //47
            "［",   //48
            "",   //49
            "］",   //50
            "；",   //51
            "：",   //52
            "半角／全角",   //53
            "、",   //54
            "。",   //55
            "／",   //56
            "ＣａｐｓＬｏｃｋ",   //57
            "Ｆ１",   //58
            "Ｆ２",   //59
            "Ｆ３",   //60
            "Ｆ４",   //61
            "Ｆ５",   //62
            "Ｆ６",   //63
            "Ｆ７",   //64
            "Ｆ８",   //65
            "Ｆ９",   //66
            "Ｆ１０",   //67
            "Ｆ１１",   //68
            "Ｆ１２",   //69
            "ＰｒｉｎｔＳｃｒｅｅｎ",   //70
            "ＳｃｒｏｌｌＬｏｃｋ",   //71
            "Ｐａｕｓｅ",   //72
            "Ｉｎｓｅｒｔ",   //73
            "Ｈｏｍｅ",   //74
            "ＰａｇｅＵｐ",   //75
            "Ｄｅｌｅｔｅ",   //76
            "Ｅｎｄ",   //77
            "ＰａｇｅＤｏｗｎ",   //78
            "→",   //79
            "←",   //80
            "↓",   //81
            "↑",   //82
            "ＮｕｍＬｏｃｋ",   //83
            "Ｎｕｍ／",   //84
            "Ｎｕｍ＊",   //85
            "Ｎｕｍ－",   //86
            "Ｎｕｍ＋",   //87
            "ＮｕｍＥｎｔｅｒ",   //88
            "Ｎｕｍ１",   //89
            "Ｎｕｍ２",   //90
            "Ｎｕｍ３",   //91
            "Ｎｕｍ４",   //92
            "Ｎｕｍ５",   //93
            "Ｎｕｍ６",   //94
            "Ｎｕｍ７",   //95
            "Ｎｕｍ８",   //96
            "Ｎｕｍ９",   //97
            "Ｎｕｍ０",   //98
            "Ｎｕｍ．",   //99
            "",   //100
            "Ｍｅｎｕ",   //101
            "",   //102
            "",   //103
            "",   //104
            "",   //105
            "",   //106
            "",   //107
            "",   //108
            "",   //109
            "",   //110
            "",   //111
            "",   //112
            "",   //113
            "",   //114
            "",   //115
            "",   //116
            "",   //117
            "",   //118
            "",   //119
            "",   //120
            "",   //121
            "",   //122
            "",   //123
            "",   //124
            "",   //125
            "",   //126
            "",   //127
            "",   //128
            "",   //129
            "",   //130
            "",   //131
            "",   //132
            "",   //133
            "",   //134
            "ＢａｃｋＳｌａｓｈ",   //135
            "カナ／ひら",   //136
            "￥",   //137
            "変換",   //138
            "無変換",   //139
            "",   //140
            "",   //141
            "",   //142
            "",   //143
            "",   //144
            "",   //145
            "",   //146
            "",   //147
            "",   //148
            "",   //149
            "",   //150
            "",   //151
            "",   //152
            "",   //153
            "",   //154
            "",   //155
            "",   //156
            "",   //157
            "",   //158
            "",   //159
            "",   //160
            "",   //161
            "",   //162
            "",   //163
            "",   //164
            "",   //165
            "",   //166
            "",   //167
            "",   //168
            "",   //169
            "",   //170
            "",   //171
            "",   //172
            "",   //173
            "",   //174
            "",   //175
            "",   //176
            "",   //177
            "",   //178
            "",   //179
            "",   //180
            "",   //181
            "",   //182
            "",   //183
            "",   //184
            "",   //185
            "",   //186
            "",   //187
            "",   //188
            "",   //189
            "",   //190
            "",   //191
            "",   //192
            "",   //193
            "",   //194
            "",   //195
            "",   //196
            "",   //197
            "",   //198
            "",   //199
            "",   //200
            "",   //201
            "",   //202
            "",   //203
            "",   //204
            "",   //205
            "",   //206
            "",   //207
            "",   //208
            "",   //209
            "",   //210
            "",   //211
            "",   //212
            "",   //213
            "",   //214
            "",   //215
            "",   //216
            "",   //217
            "",   //218
            "",   //219
            "",   //220
            "",   //221
            "",   //222
            "",   //223
            "ＬｅｆｔＣｔｒｌ",   //224
            "ＬｅｆｔＳｈｉｆｔ",   //225
            "ＬｅｆｔＡｌｔ",   //226
            "ＬｅｆｔＷｉｎ",   //227
            "ＲｉｇｈｔＣｔｒｌ",   //228
            "ＲｉｇｈｔＳｈｉｆｔ",   //229
            "ＲｉｇｈｔＡｌｔ",   //230
            "ＲｉｇｈｔＷｉｎ",   //231
            "",   //232
            "",   //233
            "",   //234
            "",   //235
            "",   //236
            "",   //237
            "",   //238
            "",   //239
            "",   //240
            "",   //241
            "",   //242
            "",   //243
            "",   //244
            "",   //245
            "",   //246
            "",   //247
            "",   //248
            "",   //249
            "",   //250
            "",   //251
            "",   //252
            "",   //253
            "",   //254
            "",   //255
       };
#endif
#if true
        //USBキーコードの名称配列
        // 日本語キーボード
        private string[] USB_KeyCode_Name = new string[256]{
            "",   //0
            "",   //1
            "",   //2
            "",   //3
            "A",   //4
            "B",   //5
            "C",   //6
            "D",   //7
            "E",   //8
            "F",   //9
            "G",   //10
            "H",   //11
            "I",   //12
            "J",   //13
            "K",   //14
            "L",   //15
            "M",   //16
            "N",   //17
            "O",   //18
            "P",   //19
            "Q",   //20
            "R",   //21
            "S",   //22
            "T",   //23
            "U",   //24
            "V",   //25
            "W",   //26
            "X",   //27
            "Y",   //28
            "Z",   //29
            "1",   //30
            "2",   //31
            "3",   //32
            "4",   //33
            "5",   //34
            "6",   //35
            "7",   //36
            "8",   //37
            "9",   //38
            "0",   //39
            "Enter",   //40
            "ESC",   //41
            "BS",   //42
            "Tab",   //43
            "Space",   //44
            "-",   //45
            "^",   //46
            "@",   //47
            "[",   //48
            "",   //49
            "]",   //50
            ";",   //51
            ":",   //52
            "ZenHan",   //53
            ",",   //54
            ".",   //55
            "/",   //56
            "CapsLock",   //57
            "F1",   //58
            "F2",   //59
            "F3",   //60
            "F4",   //61
            "F5",   //62
            "F6",   //63
            "F7",   //64
            "F8",   //65
            "F9",   //66
            "F10",   //67
            "F11",   //68
            "F12",   //69
            "PrintScreen",   //70
            "ScrollLock",   //71
            "Pause",   //72
            "Insert",   //73
            "Home",   //74
            "PageUp",   //75
            "Delete",   //76
            "End",   //77
            "PageDown",   //78
            "→",   //79
            "←",   //80
            "↓",   //81
            "↑",   //82
            "NumLock",   //83
            "Num/",   //84
            "Num*",   //85
            "Num-",   //86
            "Num+",   //87
            "NumEnter",   //88
            "Num1",   //89
            "Num2",   //90
            "Num3",   //91
            "Num4",   //92
            "Num5",   //93
            "Num6",   //94
            "Num7",   //95
            "Num8",   //96
            "Num9",   //97
            "Num0",   //98
            "Num.",   //99
            "",   //100
            "Menu",   //101
            "",   //102
            "",   //103
            "F13",   //104
            "F14",   //105
            "F15",   //106
            "F16",   //107
            "F17",   //108
            "F18",   //109
            "F19",   //110
            "F20",   //111
            "F21",   //112
            "F22",   //113
            "F23",   //114
            "F24",   //115
            "",   //116
            "",   //117
            "",   //118
            "",   //119
            "",   //120
            "",   //121
            "",   //122
            "",   //123
            "",   //124
            "",   //125
            "",   //126
            "",   //127
            "",   //128
            "",   //129
            "",   //130
            "",   //131
            "",   //132
            "",   //133
            "",   //134
            "BackSL",   //135
            "k/Hira",   //136
            "￥",   //137
            "変換",   //138
            "無変換",   //139
            "",   //140
            "",   //141
            "",   //142
            "",   //143
            "",   //144
            "",   //145
            "",   //146
            "",   //147
            "",   //148
            "",   //149
            "",   //150
            "",   //151
            "",   //152
            "",   //153
            "",   //154
            "",   //155
            "",   //156
            "",   //157
            "",   //158
            "",   //159
            "",   //160
            "",   //161
            "",   //162
            "",   //163
            "",   //164
            "",   //165
            "",   //166
            "",   //167
            "",   //168
            "",   //169
            "",   //170
            "",   //171
            "",   //172
            "",   //173
            "",   //174
            "",   //175
            "",   //176
            "",   //177
            "",   //178
            "",   //179
            "",   //180
            "",   //181
            "",   //182
            "",   //183
            "",   //184
            "",   //185
            "",   //186
            "",   //187
            "",   //188
            "",   //189
            "",   //190
            "",   //191
            "",   //192
            "",   //193
            "",   //194
            "",   //195
            "",   //196
            "",   //197
            "",   //198
            "",   //199
            "",   //200
            "",   //201
            "",   //202
            "",   //203
            "",   //204
            "",   //205
            "",   //206
            "",   //207
            "",   //208
            "",   //209
            "",   //210
            "",   //211
            "",   //212
            "",   //213
            "",   //214
            "",   //215
            "",   //216
            "",   //217
            "",   //218
            "",   //219
            "",   //220
            "",   //221
            "",   //222
            "",   //223
            "Ctrl L",   //224
            "Shift L",   //225
            "Alt L",   //226
            "Win L",   //227
            "Ctrl R",   //228
            "Shift R",   //229
            "Alt R",   //230
            "Win R",   //231
            "",   //232
            "",   //233
            "",   //234
            "",   //235
            "",   //236
            "",   //237
            "",   //238
            "",   //239
            "",   //240
            "",   //241
            "",   //242
            "",   //243
            "",   //244
            "",   //245
            "",   //246
            "",   //247
            "",   //248
            "",   //249
            "",   //250
            "",   //251
            "",   //252
            "",   //253
            "",   //254
            "",   //255
       };
        // USキーボード
        private string[] USB_KeyCode_Name_US = new string[256]{
            "",   //x0
            "",   //x1
            "",   //x2
            "",   //x3
            "A",   //4
            "B",   //5
            "C",   //6
            "D",   //7
            "E",   //8
            "F",   //9
            "G",   //10
            "H",   //11
            "I",   //12
            "J",   //13
            "K",   //14
            "L",   //15
            "M",   //16
            "N",   //17
            "O",   //18
            "P",   //19
            "Q",   //20
            "R",   //21
            "S",   //22
            "T",   //23
            "U",   //24
            "V",   //25
            "W",   //26
            "X",   //27
            "Y",   //28
            "Z",   //29
            "1",   //30
            "2",   //31
            "3",   //32
            "4",   //33
            "5",   //34
            "6",   //35
            "7",   //36
            "8",   //37
            "9",   //38
            "0",   //39
            "Enter",   //40
            "ESC",   //41
            "BS",   //42
            "Tab",   //43
            "Space",   //44
            "-",   //45
            "=",   //46
            "[",   //47
            "]",   //48
            "BackSL",   //49
            "",   //50
            ";",   //51
            "'",   //52
            "`",   //53
            ",",   //54
            ".",   //55
            "/",   //56
            "CapsLock",   //57
            "F1",   //58
            "F2",   //59
            "F3",   //60
            "F4",   //61
            "F5",   //62
            "F6",   //63
            "F7",   //64
            "F8",   //65
            "F9",   //66
            "F10",   //67
            "F11",   //68
            "F12",   //69
            "PrintScreen",   //70
            "ScrollLock",   //71
            "Pause",   //72
            "Insert",   //73
            "Home",   //74
            "PageUp",   //75
            "Delete",   //76
            "End",   //77
            "PageDown",   //78
            "→",   //79
            "←",   //80
            "↓",   //81
            "↑",   //82
            "NumLock",   //83
            "Num/",   //84
            "Num*",   //85
            "Num-",   //86
            "Num+",   //87
            "NumEnter",   //88
            "Num1",   //89
            "Num2",   //90
            "Num3",   //91
            "Num4",   //92
            "Num5",   //93
            "Num6",   //94
            "Num7",   //95
            "Num8",   //96
            "Num9",   //97
            "Num0",   //98
            "Num.",   //99
            "",   //x100
            "Menu",   //101
            "",   //x102
            "",   //x103
            "F13",   //104
            "F14",   //105
            "F15",   //106
            "F16",   //107
            "F17",   //108
            "F18",   //109
            "F19",   //110
            "F20",   //111
            "F21",   //112
            "F22",   //113
            "F23",   //114
            "F24",   //115
            "",   //x116
            "",   //x117
            "",   //x118
            "",   //x119
            "",   //x120
            "",   //x121
            "",   //x122
            "",   //x123
            "",   //x124
            "",   //x125
            "",   //x126
            "",   //x127
            "",   //x128
            "",   //x129
            "",   //x130
            "",   //x131
            "",   //x132
            "",   //x133
            "",   //x134
            "",   //x135
            "",   //x136
            "",   //x137
            "",   //X138
            "",   //X139
            "",   //x140
            "",   //x141
            "",   //x142
            "",   //x143
            "",   //x144
            "",   //x145
            "",   //x146
            "",   //x147
            "",   //x148
            "",   //x149
            "",   //x150
            "",   //x151
            "",   //x152
            "",   //x153
            "",   //x154
            "",   //x155
            "",   //x156
            "",   //x157
            "",   //x158
            "",   //x159
            "",   //x160
            "",   //x161
            "",   //x162
            "",   //x163
            "",   //x164
            "",   //x165
            "",   //x166
            "",   //x167
            "",   //x168
            "",   //x169
            "",   //x170
            "",   //x171
            "",   //x172
            "",   //x173
            "",   //x174
            "",   //x175
            "",   //x176
            "",   //x177
            "",   //x178
            "",   //x179
            "",   //x180
            "",   //x181
            "",   //x182
            "",   //x183
            "",   //x184
            "",   //x185
            "",   //x186
            "",   //x187
            "",   //x188
            "",   //x189
            "",   //x190
            "",   //x191
            "",   //x192
            "",   //x193
            "",   //x194
            "",   //x195
            "",   //x196
            "",   //x197
            "",   //x198
            "",   //x199
            "",   //x200
            "",   //x201
            "",   //x202
            "",   //x203
            "",   //x204
            "",   //x205
            "",   //x206
            "",   //x207
            "",   //x208
            "",   //x209
            "",   //x210
            "",   //x211
            "",   //x212
            "",   //x213
            "",   //x214
            "",   //x215
            "",   //x216
            "",   //x217
            "",   //x218
            "",   //x219
            "",   //x220
            "",   //x221
            "",   //x222
            "",   //x223
            "Ctrl L",   //224
            "Shift L",   //225
            "Alt L",   //226
            "Win L",   //227
            "Ctrl R",   //228
            "Shift R",   //229
            "Alt R",   //230
            "Win R",   //231
            "",   //x232
            "",   //x233
            "",   //x234
            "",   //x235
            "",   //x236
            "",   //x237
            "",   //x238
            "",   //x239
            "",   //x240
            "",   //x241
            "",   //x242
            "",   //x243
            "",   //x244
            "",   //x245
            "",   //x246
            "",   //x247
            "",   //x248
            "",   //x249
            "",   //x250
            "",   //x251
            "",   //x252
            "",   //x253
            "",   //x254
            "",   //x255
       };
#endif
    }
}
