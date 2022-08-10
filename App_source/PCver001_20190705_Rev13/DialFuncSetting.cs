using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RevOmate
{
    public partial class DialFuncSetting : Form
    {
        Form1 my_form1;
        byte select_mode = 0;
        byte select_func_no = 0;
        byte select_func_no_now = 0;

        byte[] category_id_now_set_val = new byte[Constants.CW_CCW_NUM];   // 現在の設定値
        byte[] category2_id_now_set_val = new byte[Constants.CW_CCW_NUM];
        byte[] func_list_now_set_val = new byte[Constants.CW_CCW_NUM];
        byte[] category_id_select_val = new byte[Constants.CW_CCW_NUM];   // 現在の選択値
        byte[] category2_id_select_val = new byte[Constants.CW_CCW_NUM];
        byte[] func_list_select_val = new byte[Constants.CW_CCW_NUM];

        bool advance_mode_flag = false;
        byte[,] category_id = new byte[Constants.CW_CCW_NUM, Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_NUM];
        byte[,] category2_id = new byte[Constants.CW_CCW_NUM, 100];
        byte[] category2_id_rec_num = new byte[Constants.CW_CCW_NUM];
        byte[,] func_list_id = new byte[Constants.CW_CCW_NUM, 100];
        byte[] func_list_rec_num = new byte[Constants.CW_CCW_NUM];

        Form1.STR_APP_FUNC_MODE my_app_func_mode_datas = new Form1.STR_APP_FUNC_MODE();
        Form1.STR_FUNC_DATAS my_func_datas;
        static KeyCode const_Key_Code = new KeyCode();
        SetData my_set_data = new SetData();

        Label[] my_func_title;
        Label[] my_func_colors;
        Label[] my_func_select;
        TextBox[] my_func_name;
        string[] function_name = new string[Constants.FUNCTION_NUM];


        GroupBox[] my_cw_ccw_group;
        ListBox[] my_category;
        ListBox[] my_category2;
        ListBox[] my_func_list;
        Panel[] my_func_setting;

        Label[] my_cw_ccw_title;
        Label[] my_func_setting_title;
        TextBox[] my_func_key;
        Button[] my_func_key_clr;
        CheckBox[] my_func_ctrl;
        CheckBox[] my_func_shift;
        CheckBox[] my_func_alt;
        CheckBox[] my_func_win;
        NumericUpDown[] my_mouse_x;
        NumericUpDown[] my_mouse_y;
        NumericUpDown[] my_joypad_x;
        NumericUpDown[] my_joypad_y;
        TextBox[] my_keyboard_key;
        CheckBox[,] my_keyboard_modifier;
        Button[] my_keyboard_key_clr;
        Label[] my_sensitivity_title;
        NumericUpDown[] my_encoder_sensitivity;
        TrackBar[] my_tbar_encoder_sensitivity;


        byte[] my_LED_Duty_Max = new byte[] { Constants.LED_R_DUTY_MAX, Constants.LED_G_DUTY_MAX, Constants.LED_B_DUTY_MAX };


        // カテゴリデータ定義
        string[] CATEGORY_LIST = new string[] { RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_NOT_SET,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_KEY,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_MOUSE,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_GAMEPAD,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_MULTIMEDIA,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_USER_DIAL_MACRO,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET};
        byte[] CATEGORY_ID_LIST = new byte[] { Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_NOT_SET,
                                                        Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_KEY,
                                                        Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_MOUSE,
                                                        Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_GAMEPAD,
                                                        Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_MULTIMEDIA,
                                                        Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_USERDIALMACRO,
                                                        Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_PRESET};

        // 機能データ定義
        // キーボード
        string[] FUNC_KEY_LIST = new string[] { RevOmate.Properties.Resources.SET_TYPE_NUMBER_UP,
                                                        RevOmate.Properties.Resources.SET_TYPE_NUMBER_DOWN,
                                                        RevOmate.Properties.Resources.SET_TYPE_KEYBOARD};
        byte[] FUNC_KEY_ID_LIST = new byte[] { Constants.APP_DATA_DIAL_FUNC_KEY_ID_NUM_UP,
                                                        Constants.APP_DATA_DIAL_FUNC_KEY_ID_NUM_DOWN,
                                                        Constants.APP_DATA_DIAL_FUNC_KEY_ID_KEY};
        int[] func_key_set_type_no_list = new int[] { Constants.SET_TYPE_NUMBER_UP,
                                                        Constants.SET_TYPE_NUMBER_DOWN,
                                                        Constants.SET_TYPE_KEYBOARD_KEY };
        // マウス
        string[] FUNC_MOUSE_LIST = new string[] { RevOmate.Properties.Resources.SET_TYPE_MOUSE_L_CLICK,
                                                        RevOmate.Properties.Resources.SET_TYPE_MOUSE_R_CLICK,
                                                        RevOmate.Properties.Resources.SET_TYPE_MOUSE_WH_CLICK,
                                                        RevOmate.Properties.Resources.SET_TYPE_MOUSE_B4_CLICK,
                                                        RevOmate.Properties.Resources.SET_TYPE_MOUSE_B5_CLICK,
                                                        RevOmate.Properties.Resources.SET_TYPE_MOUSE_DOUBLE_CLICK,
                                                        RevOmate.Properties.Resources.SET_TYPE_MOUSE_MOVE,
                                                        RevOmate.Properties.Resources.SET_TYPE_MOUSE_WH_SCROLL};
        byte[] FUNC_MOUSE_ID_LIST = new byte[] { Constants.APP_DATA_DIAL_FUNC_MOUSE_ID_L_CLICK,
                                                        Constants.APP_DATA_DIAL_FUNC_MOUSE_ID_R_CLICK,
                                                        Constants.APP_DATA_DIAL_FUNC_MOUSE_ID_WH_CLICK,
                                                        Constants.APP_DATA_DIAL_FUNC_MOUSE_ID_B4,
                                                        Constants.APP_DATA_DIAL_FUNC_MOUSE_ID_B5,
                                                        Constants.APP_DATA_DIAL_FUNC_MOUSE_ID_DCLICK,
                                                        Constants.APP_DATA_DIAL_FUNC_MOUSE_ID_MOVE,
                                                        Constants.APP_DATA_DIAL_FUNC_MOUSE_ID_WH_SCROLL};
        int[] func_mouse_set_type_no_list = new int[] { Constants.SET_TYPE_MOUSE_LCLICK,
                                                        Constants.SET_TYPE_MOUSE_RCLICK,
                                                        Constants.SET_TYPE_MOUSE_WHCLICK,
                                                        Constants.SET_TYPE_MOUSE_B4CLICK,
                                                        Constants.SET_TYPE_MOUSE_B5CLICK,
                                                        Constants.SET_TYPE_MOUSE_DCLICK,
                                                        Constants.SET_TYPE_MOUSE_MOVE,
                                                        Constants.SET_TYPE_MOUSE_WHSCROLL };
        // ゲームパッド
        string[] FUNC_GAMEPAD_LIST = new string[] { RevOmate.Properties.Resources.SET_TYPE_JOYSTICK_LEFT_ANALOG,
                                                        RevOmate.Properties.Resources.SET_TYPE_JOYSTICK_RIGHT_ANALOG,
                                                        RevOmate.Properties.Resources.SET_TYPE_JOYSTICK_B01,
                                                        RevOmate.Properties.Resources.SET_TYPE_JOYSTICK_B02,
                                                        RevOmate.Properties.Resources.SET_TYPE_JOYSTICK_B03,
                                                        RevOmate.Properties.Resources.SET_TYPE_JOYSTICK_B04,
                                                        RevOmate.Properties.Resources.SET_TYPE_JOYSTICK_B05,
                                                        RevOmate.Properties.Resources.SET_TYPE_JOYSTICK_B06,
                                                        RevOmate.Properties.Resources.SET_TYPE_JOYSTICK_B07,
                                                        RevOmate.Properties.Resources.SET_TYPE_JOYSTICK_B08,
                                                        RevOmate.Properties.Resources.SET_TYPE_JOYSTICK_B09,
                                                        RevOmate.Properties.Resources.SET_TYPE_JOYSTICK_B10,
                                                        RevOmate.Properties.Resources.SET_TYPE_JOYSTICK_B11,
                                                        RevOmate.Properties.Resources.SET_TYPE_JOYSTICK_B12,
                                                        RevOmate.Properties.Resources.SET_TYPE_JOYSTICK_B13,
                                                        RevOmate.Properties.Resources.SET_TYPE_JOYSTICK_HAT_N,
                                                        RevOmate.Properties.Resources.SET_TYPE_JOYSTICK_HAT_S,
                                                        RevOmate.Properties.Resources.SET_TYPE_JOYSTICK_HAT_W,
                                                        RevOmate.Properties.Resources.SET_TYPE_JOYSTICK_HAT_E};
        byte[] FUNC_GAMEPAD_ID_LIST = new byte[] { Constants.APP_DATA_DIAL_FUNC_GAMEPAD_ID_LEFT_ANALOG,
                                                        Constants.APP_DATA_DIAL_FUNC_GAMEPAD_ID_RIGHT_ANALOG,
                                                        Constants.APP_DATA_DIAL_FUNC_GAMEPAD_ID_B01,
                                                        Constants.APP_DATA_DIAL_FUNC_GAMEPAD_ID_B02,
                                                        Constants.APP_DATA_DIAL_FUNC_GAMEPAD_ID_B03,
                                                        Constants.APP_DATA_DIAL_FUNC_GAMEPAD_ID_B04,
                                                        Constants.APP_DATA_DIAL_FUNC_GAMEPAD_ID_B05,
                                                        Constants.APP_DATA_DIAL_FUNC_GAMEPAD_ID_B06,
                                                        Constants.APP_DATA_DIAL_FUNC_GAMEPAD_ID_B07,
                                                        Constants.APP_DATA_DIAL_FUNC_GAMEPAD_ID_B08,
                                                        Constants.APP_DATA_DIAL_FUNC_GAMEPAD_ID_B09,
                                                        Constants.APP_DATA_DIAL_FUNC_GAMEPAD_ID_B10,
                                                        Constants.APP_DATA_DIAL_FUNC_GAMEPAD_ID_B11,
                                                        Constants.APP_DATA_DIAL_FUNC_GAMEPAD_ID_B12,
                                                        Constants.APP_DATA_DIAL_FUNC_GAMEPAD_ID_B13,
                                                        Constants.APP_DATA_DIAL_FUNC_GAMEPAD_ID_HAT_N,
                                                        Constants.APP_DATA_DIAL_FUNC_GAMEPAD_ID_HAT_S,
                                                        Constants.APP_DATA_DIAL_FUNC_GAMEPAD_ID_HAT_W,
                                                        Constants.APP_DATA_DIAL_FUNC_GAMEPAD_ID_HAT_E};
        int[] func_gamepad_set_type_no_list = new int[] { Constants.SET_TYPE_JOYPAD_XY,
                                                        Constants.SET_TYPE_JOYPAD_ZRZ,
                                                        Constants.SET_TYPE_JOYPAD_B01,
                                                        Constants.SET_TYPE_JOYPAD_B02,
                                                        Constants.SET_TYPE_JOYPAD_B03,
                                                        Constants.SET_TYPE_JOYPAD_B04,
                                                        Constants.SET_TYPE_JOYPAD_B05,
                                                        Constants.SET_TYPE_JOYPAD_B06,
                                                        Constants.SET_TYPE_JOYPAD_B07,
                                                        Constants.SET_TYPE_JOYPAD_B08,
                                                        Constants.SET_TYPE_JOYPAD_B09,
                                                        Constants.SET_TYPE_JOYPAD_B10,
                                                        Constants.SET_TYPE_JOYPAD_B11,
                                                        Constants.SET_TYPE_JOYPAD_B12,
                                                        Constants.SET_TYPE_JOYPAD_B13,
                                                        Constants.SET_TYPE_JOYPAD_HSW_NORTH,
                                                        Constants.SET_TYPE_JOYPAD_HSW_SOUTH,
                                                        Constants.SET_TYPE_JOYPAD_HSW_WEST,
                                                        Constants.SET_TYPE_JOYPAD_HSW_EAST };
        // マルチメディア
        string[] FUNC_MULTIMEDIA_LIST = new string[] { RevOmate.Properties.Resources.SET_TYPE_MULTIMEDIA_PLAY,
                                                        RevOmate.Properties.Resources.SET_TYPE_MULTIMEDIA_PAUSE,
                                                        RevOmate.Properties.Resources.SET_TYPE_MULTIMEDIA_STOP,
                                                        RevOmate.Properties.Resources.SET_TYPE_MULTIMEDIA_REC,
                                                        RevOmate.Properties.Resources.SET_TYPE_MULTIMEDIA_FORWORD,
                                                        RevOmate.Properties.Resources.SET_TYPE_MULTIMEDIA_REWIND,
                                                        RevOmate.Properties.Resources.SET_TYPE_MULTIMEDIA_NEXT,
                                                        RevOmate.Properties.Resources.SET_TYPE_MULTIMEDIA_PREVIOUS,
                                                        RevOmate.Properties.Resources.SET_TYPE_MULTIMEDIA_MUTE,
                                                        RevOmate.Properties.Resources.SET_TYPE_MULTIMEDIA_VOLUMEUP,
                                                        RevOmate.Properties.Resources.SET_TYPE_MULTIMEDIA_VOLUMEDOWN };
        byte[] FUNC_MULTIMEDIA_ID_LIST = new byte[] { Constants.APP_DATA_DIAL_FUNC_MULTIMEDIA_ID_PLAY,
                                                        Constants.APP_DATA_DIAL_FUNC_MULTIMEDIA_ID_PAUSE,
                                                        Constants.APP_DATA_DIAL_FUNC_MULTIMEDIA_ID_STOP,
                                                        Constants.APP_DATA_DIAL_FUNC_MULTIMEDIA_ID_REC,
                                                        Constants.APP_DATA_DIAL_FUNC_MULTIMEDIA_ID_FORWORD,
                                                        Constants.APP_DATA_DIAL_FUNC_MULTIMEDIA_ID_REWIND,
                                                        Constants.APP_DATA_DIAL_FUNC_MULTIMEDIA_ID_NEXT,
                                                        Constants.APP_DATA_DIAL_FUNC_MULTIMEDIA_ID_PREVIOUS,
                                                        Constants.APP_DATA_DIAL_FUNC_MULTIMEDIA_ID_MUTE,
                                                        Constants.APP_DATA_DIAL_FUNC_MULTIMEDIA_ID_VOLUMEUP,
                                                        Constants.APP_DATA_DIAL_FUNC_MULTIMEDIA_ID_VOLUMEDOWN };
        int[] func_multimedia_set_type_no_list = new int[] { Constants.SET_TYPE_MULTIMEDIA_PLAY,
                                                        Constants.SET_TYPE_MULTIMEDIA_PAUSE,
                                                        Constants.SET_TYPE_MULTIMEDIA_STOP,
                                                        Constants.SET_TYPE_MULTIMEDIA_REC,
                                                        Constants.SET_TYPE_MULTIMEDIA_FORWORD,
                                                        Constants.SET_TYPE_MULTIMEDIA_REWIND,
                                                        Constants.SET_TYPE_MULTIMEDIA_NEXT,
                                                        Constants.SET_TYPE_MULTIMEDIA_PREVIOUS,
                                                        Constants.SET_TYPE_MULTIMEDIA_MUTE,
                                                        Constants.SET_TYPE_MULTIMEDIA_VOLUMEUP,
                                                        Constants.SET_TYPE_MULTIMEDIA_VOLUMEDOWN };
        // ユーザダイアルマクロ
        int[] func_user_dial_macro_set_type_no_list = new int[] { Constants.SET_TYPE_ENCODER_SCRIPT1,
                                                        Constants.SET_TYPE_ENCODER_SCRIPT2,
                                                        Constants.SET_TYPE_ENCODER_SCRIPT3 };


        // カテゴリ2 プリセットデータ定義
        string[] CATEGORY_PRESET_LIST = new string[] { RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_SAI,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_LIGHTROOM};
        byte[] CATEGORY_PRESET_ID_LIST = new byte[] { Constants.APP_DATA_DIAL_FUNC_PRESET_ID_PHOTOSHOP,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_ID_CLIPSTUDIO,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_ID_SAI,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_ID_ILLUSTRATOR,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_ID_LIGHTROOM};

        // プリセットデータ定義
        // PhtoShop
        string[] PRESET_PHOTOSHOP_LIST = new string[] { RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_1_BRUSH_L,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_2_BRUSH_S,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_3_ROTATION_R,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_4_ROTATION_L,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_5_IMAGE_ENLARGE,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_6_IMAGE_REDUCE,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_7_CANVAS_ENLARGE,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_8_CANVAS_REDUCE,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_9_LINE_SPACE_SPREAD,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_10_LINE_SPACE_NARROW,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_11_LETTER_SPACE_SPREAD,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_12_LETTER_SPACE_NARROW,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_15_BLUSH_HARD_UP,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_16_BLUSH_HARD_DOWN,
                                                        RevOmate.Properties.Resources.FUNCTION_NAME_UNSETTING,
                                                        RevOmate.Properties.Resources.FUNCTION_NAME_UNSETTING};
        byte[] PRESET_PHOTOSHOP_ID_LIST = new byte[] { Constants.APP_DATA_DIAL_FUNC_PRESET_PHOTOSHOP_ID_BRUSH_L,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_PHOTOSHOP_ID_BRUSH_S,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_PHOTOSHOP_ID_ROTATION_R,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_PHOTOSHOP_ID_ROTATION_L,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_PHOTOSHOP_ID_IMAGE_ENLARGE,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_PHOTOSHOP_ID_IMAGE_REDUCE,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_PHOTOSHOP_ID_CANVAS_ENLARGE,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_PHOTOSHOP_ID_CANVAS_REDUCE,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_PHOTOSHOP_ID_LINE_L,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_PHOTOSHOP_ID_LINE_S,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_PHOTOSHOP_ID_LETTER_L,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_PHOTOSHOP_ID_LETTER_S,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_PHOTOSHOP_ID_BLUSH_HARD_UP,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_PHOTOSHOP_ID_BLUSH_HARD_DOWN,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_PHOTOSHOP_ID_OPACITY_UP,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_PHOTOSHOP_ID_OPACITY_DOWN};
        // ClipStudio
        string[] PRESET_CLIPSTUDIO_LIST = new string[] { RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_1_BRUSH_L,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_2_BRUSH_S,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_3_ROTATION_R,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_4_ROTATION_L,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_5_IMAGE_ENLARGE,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_6_IMAGE_REDUCE,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_7_BLUSH_OPACITY_UP,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_8_BLUSH_OPACITY_DOWN,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_9_ZOOM_IN,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_10_ZOOM_OUT};
        byte[] PRESET_CLIPSTUDIO_ID_LIST = new byte[] { Constants.APP_DATA_DIAL_FUNC_PRESET_CLIPSTUDIO_ID_BRUSH_L,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_CLIPSTUDIO_ID_BRUSH_S,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_CLIPSTUDIO_ID_ROTATION_R,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_CLIPSTUDIO_ID_ROTATION_L,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_CLIPSTUDIO_ID_IMAGE_ENLARGE,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_CLIPSTUDIO_ID_IMAGE_REDUCE,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_CLIPSTUDIO_ID_BLUSH_OPACITY_UP,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_CLIPSTUDIO_ID_BLUSH_OPACITY_DOWN,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_CLIPSTUDIO_ID_ZOOM_IN,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_CLIPSTUDIO_ID_ZOOM_OUT};
        // SAI
        string[] PRESET_SAI_LIST = new string[] { RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_SAI_1_BRUSH_L,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_SAI_2_BRUSH_S,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_SAI_3_ROTATION_R,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_SAI_4_ROTATION_L,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_SAI_5_IMAGE_ENLARGE,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_SAI_6_IMAGE_REDUCE,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_SAI_7_CANVAS_ENLARGE,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_SAI_8_CANVAS_REDUCE,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_SAI_9_CURSOR_ENLARGE,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_SAI_10_CURSOR_REDUCE};
        byte[] PRESET_SAI_ID_LIST = new byte[] { Constants.APP_DATA_DIAL_FUNC_PRESET_SAI_ID_BRUSH_L,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_SAI_ID_BRUSH_S,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_SAI_ID_ROTATION_R,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_SAI_ID_ROTATION_L,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_SAI_ID_IMAGE_ENLARGE,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_SAI_ID_IMAGE_REDUCE,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_SAI_ID_CANVAS_ENLARGE,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_SAI_ID_CANVAS_REDUCE,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_SAI_ID_CURSOR_ENLARGE,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_SAI_ID_CURSOR_REDUCE};
        // ILLUSTRATOR
        string[] PRESET_ILLUSTRATOR_LIST = new string[] { RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_1_ZOOMIN,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_2_ZOOMOUT};
        byte[] PRESET_ILLUSTRATOR_ID_LIST = new byte[] { Constants.APP_DATA_DIAL_FUNC_PRESET_ILLUSTRATOR_ID_ZOOMIN,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_ILLUSTRATOR_ID_ZOOMOUT};
        // LIGHTROOM
        string[] PRESET_LIGHTROOM_LIST = new string[] { RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_LIGHTROOM_1_SLIDER_S_UP,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_LIGHTROOM_2_SLIDER_S_DOWN,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_LIGHTROOM_3_SLIDER_L_UP,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_LIGHTROOM_4_SLIDER_L_DOWN,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_LIGHTROOM_5_BRUSH_L,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_LIGHTROOM_6_BRUSH_S,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_LIGHTROOM_7_BLUSH_BLUR_SIZE_UP,
                                                        RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CATEGOLY_PRESET_LIGHTROOM_8_BLUSH_BLUR_SIZE_DOWN};
        byte[] PRESET_LIGHTROOM_ID_LIST = new byte[] { Constants.APP_DATA_DIAL_FUNC_PRESET_LIGHTROOM_ID_SLIDER_S_UP,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_LIGHTROOM_ID_SLIDER_S_DOWN,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_LIGHTROOM_ID_SLIDER_L_UP,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_LIGHTROOM_ID_SLIDER_L_DOWN,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_LIGHTROOM_ID_BRUSH_L,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_LIGHTROOM_ID_BRUSH_S,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_LIGHTROOM_ID_BRUSH_BLUR_UP,
                                                        Constants.APP_DATA_DIAL_FUNC_PRESET_LIGHTROOM_ID_BRUSH_BLUR_DOWN};

        public DialFuncSetting()
        {
            InitializeComponent();
        }

        public DialFuncSetting(Form1 p_fm, int p_mode, int p_func_no, bool p_advance_flag, Form1.STR_APP_FUNC_DATAS p_app_func_datas, SetData p_set_data, Form1.STR_FUNC_DATAS p_func_datas)
        {
            my_form1 = p_fm;
            select_mode = (byte)(p_mode & 0xFF);
            select_func_no = (byte)(p_func_no & 0xFF);
            select_func_no_now = (byte)(p_func_no & 0xFF);
            advance_mode_flag = p_advance_flag;

            for (int fi = 0; fi < Constants.FUNCTION_NUM; fi++)
            {
                function_name[fi] = p_func_datas.func_datas[select_mode].func_data[fi].func_name;
            }

            my_app_func_mode_datas.init_data(Constants.FUNCTION_NUM, Constants.CW_CCW_NUM, Constants.APP_FUNC_DATA_SELECT_DATA_LEN);
            my_app_func_mode_datas.Copy(p_app_func_datas.mode[select_mode]);
            my_set_data.Copy(p_set_data);
            my_func_datas = p_func_datas;

            InitializeComponent();
        }

        private void DialFuncSetting_Load(object sender, EventArgs e)
        {
            try
            {
#if DEBUG
                // デバッグ時
                lbl_debug01.Text = "mode=" + select_mode.ToString() + ", func_no=" + select_func_no.ToString();
#else
        // リリース時
                lbl_debug01.Visible = false;
#endif

                this.Text = RevOmate.Properties.Resources.SCREEN_DIAL_FUNCTION_SETTING;
                btn_submit.Text = RevOmate.Properties.Resources.BTN_SUBMIT;
                btn_cancel.Text = RevOmate.Properties.Resources.BTN_CANCEL;
                llbl_help.Text = RevOmate.Properties.Resources.HELP_QUESTION_MARK;

                my_func_title = new Label[] { lbl_func_1_title, lbl_func_2_title, lbl_func_3_title, lbl_func_4_title };
                my_func_colors = new Label[] { lbl_func_color_1, lbl_func_color_2, lbl_func_color_3, lbl_func_color_4 };
                my_func_select = new Label[] { lbl_func_1_select, lbl_func_2_select, lbl_func_3_select, lbl_func_4_select };
                my_func_name = new TextBox[] { txtbx_func_name_1, txtbx_func_name_2, txtbx_func_name_3, txtbx_func_name_4 };

                my_cw_ccw_group = new GroupBox[] { gbx_dial_func_cw, gbx_dial_func_ccw };
                my_category = new ListBox[] { lbx_category_cw, lbx_category_ccw };
                my_category2 = new ListBox[] { lbx_category2_cw, lbx_category2_ccw };
                my_func_list = new ListBox[] { lbx_func_list_cw, lbx_func_list_ccw };
                my_func_setting = new Panel[] { pnl_func_setting_cw, pnl_func_setting_ccw };

                my_cw_ccw_title = new Label[] { lbl_cw_title, lbl_ccw_title };
                my_func_setting_title = new Label[] { lbl_func1_title_cw, lbl_func1_title_ccw };
                my_func_key = new TextBox[] { txtbx_func1_key_cw, txtbx_func1_key_ccw };
                my_func_key_clr = new Button[] { btn_func1_key_clr_cw, btn_func1_key_clr_ccw };
                my_func_ctrl = new CheckBox[] { chk_func1_ctrl_cw, chk_func1_ctrl_ccw };
                my_func_shift = new CheckBox[] { chk_func1_shift_cw, chk_func1_shift_ccw };
                my_func_alt = new CheckBox[] { chk_func1_alt_cw, chk_func1_alt_ccw };
                my_func_win = new CheckBox[] { chk_func1_win_cw, chk_func1_win_ccw };
                my_mouse_x = new NumericUpDown[] { num_func1_x_cw, num_func1_x_ccw };
                my_mouse_y = new NumericUpDown[] { num_func1_y_cw, num_func1_y_ccw };
                my_joypad_x = new NumericUpDown[] { num_func1_x_cw, num_func1_x_ccw };
                my_joypad_y = new NumericUpDown[] { num_func1_y_cw, num_func1_y_ccw };
                my_keyboard_key = new TextBox[] {  txtbx_func1_key_cw, txtbx_func1_key_ccw };
                my_keyboard_modifier = new CheckBox[,] { { chk_func1_ctrl_cw, chk_func1_shift_cw, chk_func1_alt_cw, chk_func1_win_cw }, { chk_func1_ctrl_ccw, chk_func1_shift_ccw, chk_func1_alt_ccw, chk_func1_win_ccw }};
                my_keyboard_key_clr = new Button[] { btn_func1_key_clr_cw, btn_func1_key_clr_ccw };
                my_sensitivity_title = new Label[] { lbl_cw_sensitivity_title, lbl_ccw_sensitivity_title };
                my_encoder_sensitivity = new NumericUpDown[] { num_func1_sensivity_cw, num_func1_sensivity_ccw };
                my_tbar_encoder_sensitivity = new TrackBar[] { tbar__func1_sensivity_cw, tbar__func1_sensivity_ccw };


                for (int fi = 0; fi < my_func_select.Length; fi++)
                {
                    my_func_title[fi].Text = RevOmate.Properties.Resources.DIAL_FUNC_SETTING_DIAL + string.Format("{0}:", fi + 1);
                    my_func_select[fi].Text = function_name[fi];
                }

                for (int fi = 0; fi < Constants.CW_CCW_NUM; fi++)
                {
                    if (fi == 0)
                    {   // 右回転
                        my_cw_ccw_group[fi].Text = RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CW;
                        my_cw_ccw_title[fi].Text = RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CW;
                    }
                    else
                    {   // 左回転
                        my_cw_ccw_group[fi].Text = RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CCW;
                        my_cw_ccw_title[fi].Text = RevOmate.Properties.Resources.DIAL_FUNC_SETTING_CCW;
                    }

                    my_func_key[fi].Text = RevOmate.Properties.Resources.SETTING_KEY_INPUT_BOX_MSG;
                    my_func_key_clr[fi].Text = RevOmate.Properties.Resources.SETTING_KEY_INPUT_CLR_BUTTON;
                    my_func_ctrl[fi].Text = RevOmate.Properties.Resources.SETTING_KEY_CTRL_KEY_CTRL;
                    my_func_shift[fi].Text = RevOmate.Properties.Resources.SETTING_KEY_CTRL_KEY_SHIFT;
                    my_func_alt[fi].Text = RevOmate.Properties.Resources.SETTING_KEY_CTRL_KEY_ALT;
                    my_func_win[fi].Text = RevOmate.Properties.Resources.SETTING_KEY_CTRL_KEY_WIN;


                    my_encoder_sensitivity[fi].Minimum = Constants.SENSITIVITY_MIN;
                    my_encoder_sensitivity[fi].Maximum = Constants.SENSITIVITY_MAX;
                    my_encoder_sensitivity[fi].Value = Constants.SENSITIVITY_DEFAULT;

                    my_tbar_encoder_sensitivity[fi].Minimum = Constants.SENSITIVITY_MIN;
                    my_tbar_encoder_sensitivity[fi].Maximum = Constants.SENSITIVITY_MAX;
                    my_tbar_encoder_sensitivity[fi].Value = Constants.SENSITIVITY_DEFAULT;
                }


                my_func_mode_select_disp(select_func_no, true);

                // 画面更新
                my_func_disp_all_update();

            }
            catch
            {
            }
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(RevOmate.Properties.Resources.DEVICE_FUNCTION_WRITE_CONFIRM_MSG, RevOmate.Properties.Resources.APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    my_dial_function_setting_get_by_disp(select_mode, select_func_no_now);

                    my_form1.my_Function_Setting_Write_req_by_dial_func_setting(select_mode, select_func_no_now, my_set_data, function_name, my_app_func_mode_datas);


                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
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

        private void lbl_func_select_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                int tmp_select_func = int.Parse(((Label)sender).Tag.ToString());
                if (0 <= tmp_select_func && tmp_select_func < Constants.FUNCTION_NUM)
                {

                    if (select_func_no_now != tmp_select_func)
                    {   // 表示モード変更あり

                        // 変更前のモードの設定値を取得
                        my_dial_function_setting_get_by_disp(select_mode, select_func_no_now);

                        // 変更後のモードへ
                        select_func_no_now = (byte)(tmp_select_func & 0xFF);
                        my_func_mode_select_disp(select_func_no_now, true);

                        // 画面更新
                        my_func_disp_all_update();
                    }

                }
            }
            catch
            {
            }
        }

        private void my_create_category_id_list(int p_cw_ccw_idx)
        {
            try
            {
                if (0 <= p_cw_ccw_idx && p_cw_ccw_idx < Constants.CW_CCW_NUM)
                {
                    for (int fi = 0; fi < CATEGORY_ID_LIST.Length; fi++)
                    {
                        category_id[p_cw_ccw_idx, fi] = CATEGORY_ID_LIST[fi];
                    }
                }
            }
            catch
            {
            }
        }

        private void my_create_category2_id_list(byte p_category_id, int p_cw_ccw_idx)
        {
            try
            {
                if (0 <= p_cw_ccw_idx && p_cw_ccw_idx < Constants.CW_CCW_NUM)
                {
                    switch (p_category_id)
                    {
                        case Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_PRESET:
                            category2_id_rec_num[p_cw_ccw_idx] = (byte)(CATEGORY_PRESET_ID_LIST.Length & 0xFF);
                            for (int fi = 0; fi < CATEGORY_PRESET_ID_LIST.Length; fi++)
                            {
                                category2_id[p_cw_ccw_idx, fi] = CATEGORY_PRESET_ID_LIST[fi];
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch
            {
            }
        }

        private void my_create_func_id_list(byte p_category_id, byte p_category2_id, byte p_cw_ccw_idx)
        {
            try
            {
                if(p_cw_ccw_idx < Constants.CW_CCW_NUM)
                {
                }
                switch (p_category_id)
                {
                    case Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_NOT_SET:
                        // IDセット
                        func_list_id[p_cw_ccw_idx, 0] = 0;
                        func_list_rec_num[p_cw_ccw_idx] = 1;
                        break;
                    case Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_KEY:
                        // IDセット
                        for (int fi = 0; fi < FUNC_KEY_ID_LIST.Length; fi++)
                        {
                            func_list_id[p_cw_ccw_idx, fi] = FUNC_KEY_ID_LIST[fi];
                        }
                        func_list_rec_num[p_cw_ccw_idx] = (byte)(FUNC_KEY_ID_LIST.Length);
                        break;
                    case Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_MOUSE:
                        // IDセット
                        for (int fi = 0; fi < FUNC_MOUSE_ID_LIST.Length; fi++)
                        {
                            func_list_id[p_cw_ccw_idx, fi] = FUNC_MOUSE_ID_LIST[fi];
                        }
                        func_list_rec_num[p_cw_ccw_idx] = (byte)(FUNC_MOUSE_ID_LIST.Length);
                        break;
                    case Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_GAMEPAD:
                        // IDセット
                        for (int fi = 0; fi < FUNC_GAMEPAD_ID_LIST.Length; fi++)
                        {
                            func_list_id[p_cw_ccw_idx, fi] = FUNC_GAMEPAD_ID_LIST[fi];
                        }
                        func_list_rec_num[p_cw_ccw_idx] = (byte)(FUNC_GAMEPAD_ID_LIST.Length);
                        break;
                    case Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_MULTIMEDIA:
                        // IDセット
                        for (int fi = 0; fi < FUNC_MULTIMEDIA_ID_LIST.Length; fi++)
                        {
                            func_list_id[p_cw_ccw_idx, fi] = FUNC_MULTIMEDIA_ID_LIST[fi];
                        }
                        func_list_rec_num[p_cw_ccw_idx] = (byte)(FUNC_MULTIMEDIA_ID_LIST.Length);
                        break;
                    case Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_USERDIALMACRO:
                        // IDセット
                        for (int fi = 0; fi < Constants.ENCODER_SCRIPT_NUM; fi++)
                        {
                            func_list_id[p_cw_ccw_idx, fi] = (byte)(fi & 0xFF);
                        }
                        func_list_rec_num[p_cw_ccw_idx] = Constants.ENCODER_SCRIPT_NUM;
                        break;
                    case Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_PRESET:

                        switch (p_category2_id)
                        {
                            case Constants.APP_DATA_DIAL_FUNC_PRESET_ID_PHOTOSHOP:
                                for (int fi = 0; fi < PRESET_PHOTOSHOP_ID_LIST.Length; fi++)
                                {
                                    func_list_id[p_cw_ccw_idx, fi] = PRESET_PHOTOSHOP_ID_LIST[fi];
                                }
                                func_list_rec_num[p_cw_ccw_idx] = (byte)(PRESET_PHOTOSHOP_ID_LIST.Length);
                                break;
                            case Constants.APP_DATA_DIAL_FUNC_PRESET_ID_CLIPSTUDIO:
                                for (int fi = 0; fi < PRESET_CLIPSTUDIO_ID_LIST.Length; fi++)
                                {
                                    func_list_id[p_cw_ccw_idx, fi] = PRESET_CLIPSTUDIO_ID_LIST[fi];
                                }
                                func_list_rec_num[p_cw_ccw_idx] = (byte)(PRESET_CLIPSTUDIO_ID_LIST.Length);
                                break;
                            case Constants.APP_DATA_DIAL_FUNC_PRESET_ID_SAI:
                                for (int fi = 0; fi < PRESET_SAI_ID_LIST.Length; fi++)
                                {
                                    func_list_id[p_cw_ccw_idx, fi] = PRESET_SAI_ID_LIST[fi];
                                }
                                func_list_rec_num[p_cw_ccw_idx] = (byte)(PRESET_SAI_ID_LIST.Length);
                                break;
                            case Constants.APP_DATA_DIAL_FUNC_PRESET_ID_ILLUSTRATOR:
                                for (int fi = 0; fi < PRESET_ILLUSTRATOR_ID_LIST.Length; fi++)
                                {
                                    func_list_id[p_cw_ccw_idx, fi] = PRESET_ILLUSTRATOR_ID_LIST[fi];
                                }
                                func_list_rec_num[p_cw_ccw_idx] = (byte)(PRESET_ILLUSTRATOR_ID_LIST.Length);
                                break;
                            case Constants.APP_DATA_DIAL_FUNC_PRESET_ID_LIGHTROOM:
                                for (int fi = 0; fi < PRESET_LIGHTROOM_ID_LIST.Length; fi++)
                                {
                                    func_list_id[p_cw_ccw_idx, fi] = PRESET_LIGHTROOM_ID_LIST[fi];
                                }
                                func_list_rec_num[p_cw_ccw_idx] = (byte)(PRESET_LIGHTROOM_ID_LIST.Length);
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch
            {
            }
        }

        private void my_func_mode_select_disp(int p_select_func_no, bool p_visible)
        {
            try
            {
                if (0 <= p_select_func_no && p_select_func_no < Constants.FUNCTION_NUM)
                {

#if false   // 選択機能のみ表示
                    int[] tmp_LED_RGB = new int[Constants.LED_RGB_COLOR_NUM];
                    int[] tmp_Preview_RGB = new int[Constants.LED_RGB_COLOR_NUM];
                    tmp_LED_RGB[0] = my_func_datas.func_datas[select_mode].func_data[p_select_func_no].LED_RGB_duty[0] * 0xFF / my_LED_Duty_Max[0];
                    tmp_LED_RGB[1] = my_func_datas.func_datas[select_mode].func_data[p_select_func_no].LED_RGB_duty[1] * 0xFF / my_LED_Duty_Max[1];
                    tmp_LED_RGB[2] = my_func_datas.func_datas[select_mode].func_data[p_select_func_no].LED_RGB_duty[2] * 0xFF / my_LED_Duty_Max[2];

                    CommonFunc.my_LED_RGB_to_Preview_RGB(tmp_LED_RGB, tmp_Preview_RGB);
                    my_func_colors[0].BackColor = Color.FromArgb(tmp_Preview_RGB[0], tmp_Preview_RGB[1], tmp_Preview_RGB[2]);

                    // 選択を太字に
                    for (int fi = 0; fi < Constants.FUNCTION_NUM; fi++)
                    {
                        if (0 == fi)
                        {   // 太字
                            my_func_select[0].Visible = false;
                            my_func_name[0].Text = my_func_datas.func_datas[select_mode].func_data[p_select_func_no].func_name;
                            my_func_name[0].Font = new Font(my_func_name[0].Font, FontStyle.Bold);
                            my_func_name[0].Visible = true;

                            my_func_title[0].Text = RevOmate.Properties.Resources.DIAL_FUNC_SETTING_DIAL + string.Format("{0}:", p_select_func_no + 1);
                            my_func_title[0].Visible = true;
                            my_func_colors[0].Visible = true;
                        }
                        else
                        {   // 標準
                            my_func_name[fi].Visible = false;
                            my_func_select[fi].Text = my_func_datas.func_datas[select_mode].func_data[fi].func_name;
                            my_func_select[fi].Font = new Font(my_func_select[fi].Font, FontStyle.Regular);
                            my_func_select[fi].Visible = false;

                            my_func_title[fi].Visible = false;
                            my_func_colors[fi].Visible = false;
                        }
                    }
#endif
#if true   // 4機能表示して切換可能
                    // ダイアル機能のLED設定表示
                    my_func_mode_LED_disp(select_mode);

                    // 選択を太字に
                    for (int fi = 0; fi < Constants.FUNCTION_NUM; fi++)
                    {
                        if (p_select_func_no == fi)
                        {   // 太字
                            my_func_title[fi].Font = new Font(my_func_title[fi].Font, FontStyle.Bold);
                            my_func_select[fi].Visible = false;
                            my_func_name[fi].Text = function_name[fi];
                            my_func_name[fi].Font = new Font(my_func_name[fi].Font, FontStyle.Bold);
                            my_func_name[fi].Visible = true;
                        }
                        else
                        {   // 標準
                            my_func_title[fi].Font = new Font(my_func_title[fi].Font, FontStyle.Regular);
                            my_func_name[fi].Visible = false;
                            my_func_select[fi].Text = function_name[fi];
                            my_func_select[fi].Font = new Font(my_func_select[fi].Font, FontStyle.Regular);
                            my_func_select[fi].Visible = true;
                        }
                    }
#endif
                }
            }
            catch
            {
            }
        }


        private void my_category_disp(bool p_advance_mode_flag, int p_select_id, int p_cw_ccw_idx)
        {
            int select_idx = 0;
            try
            {
                if (0 <= p_cw_ccw_idx && p_cw_ccw_idx < my_category.Length)
                {
                    my_category[p_cw_ccw_idx].Items.Clear();
                    // カテゴリ表示
                    for (int fi = 0; fi < CATEGORY_LIST.Length && fi < CATEGORY_ID_LIST.Length; fi++)
                    {
                        my_category[p_cw_ccw_idx].Items.Add(CATEGORY_LIST[fi]);
                    }

                    //
                    for (int fi = 0; fi < category_id.Length; fi++)
                    {
                        if (category_id[p_cw_ccw_idx, fi] == p_select_id)
                        {
                            select_idx = fi;
                            break;
                        }
                    }
                    if (0 <= select_idx && select_idx < my_category[p_cw_ccw_idx].Items.Count)
                    {
                        my_category[p_cw_ccw_idx].SelectedIndex = select_idx;
                    }
                    else
                    {
                    }
                }
            }
            catch
            {
            }
        }

        private void my_category2_disp(bool p_advance_mode_flag, int p_select_category_id, int p_select_category_id2, int p_cw_ccw_idx)
        {
            int select_idx = p_select_category_id2;
            try
            {
                my_category2[p_cw_ccw_idx].Items.Clear();

                switch (p_select_category_id)
                {
                    case Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_PRESET:
                        for (int fi = 0; fi < CATEGORY_PRESET_LIST.Length; fi++)
                        {
                            my_category2[p_cw_ccw_idx].Items.Add(CATEGORY_PRESET_LIST[fi]);
                        }
                        break;
                    default:
                        break;
                }

                if (0 <= select_idx && select_idx < my_category2[p_cw_ccw_idx].Items.Count)
                {
                    my_category2[p_cw_ccw_idx].SelectedIndex = select_idx;
                }
                else
                {
                }
            }
            catch
            {
            }
        }

        private void my_func_list_disp(bool p_advance_mode_flag, int p_category_id, int p_category_id2, int p_select_id, int p_cw_ccw_idx)
        {
            int select_idx = p_select_id;
            try
            {
                if (0 <= p_cw_ccw_idx && p_cw_ccw_idx < my_func_list.Length)
                {
                    my_func_list[p_cw_ccw_idx].Items.Clear();

                    switch (p_category_id)
                    {
                        case Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_NOT_SET:
                            // 表示なし
                            break;
                        case Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_KEY:
                            for (int fi = 0; fi < FUNC_KEY_LIST.Length; fi++)
                            {
                                my_func_list[p_cw_ccw_idx].Items.Add(FUNC_KEY_LIST[fi]);
                            }
                            break;
                        case Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_MOUSE:
                            for (int fi = 0; fi < FUNC_MOUSE_LIST.Length; fi++)
                            {
                                my_func_list[p_cw_ccw_idx].Items.Add(FUNC_MOUSE_LIST[fi]);
                            }
                            break;
                        case Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_GAMEPAD:
                            for (int fi = 0; fi < FUNC_GAMEPAD_LIST.Length; fi++)
                            {
                                my_func_list[p_cw_ccw_idx].Items.Add(FUNC_GAMEPAD_LIST[fi]);
                            }
                            break;
                        case Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_MULTIMEDIA:
                            for (int fi = 0; fi < FUNC_MULTIMEDIA_LIST.Length; fi++)
                            {
                                my_func_list[p_cw_ccw_idx].Items.Add(FUNC_MULTIMEDIA_LIST[fi]);
                            }
                            break;
                        case Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_USERDIALMACRO:
                            for (int fi = 0; fi < Constants.ENCODER_SCRIPT_NUM; fi++)
                            {
                                my_func_list[p_cw_ccw_idx].Items.Add(RevOmate.Properties.Resources.ENCODER_SCRIPT + string.Format("{0}", fi + 1));
                            }
                            break;
                        case Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_PRESET:

                            switch (p_category_id2)
                            {
                                case Constants.APP_DATA_DIAL_FUNC_PRESET_ID_PHOTOSHOP:
                                    for (int fi = 0; fi < PRESET_PHOTOSHOP_LIST.Length; fi++)
                                    {
                                        my_func_list[p_cw_ccw_idx].Items.Add(PRESET_PHOTOSHOP_LIST[fi]);
                                    }
                                    break;
                                case Constants.APP_DATA_DIAL_FUNC_PRESET_ID_CLIPSTUDIO:
                                    for (int fi = 0; fi < PRESET_CLIPSTUDIO_LIST.Length; fi++)
                                    {
                                        my_func_list[p_cw_ccw_idx].Items.Add(PRESET_CLIPSTUDIO_LIST[fi]);
                                    }
                                    break;
                                case Constants.APP_DATA_DIAL_FUNC_PRESET_ID_SAI:
                                    for (int fi = 0; fi < PRESET_SAI_LIST.Length; fi++)
                                    {
                                        my_func_list[p_cw_ccw_idx].Items.Add(PRESET_SAI_LIST[fi]);
                                    }
                                    break;
                                case Constants.APP_DATA_DIAL_FUNC_PRESET_ID_ILLUSTRATOR:
                                    for (int fi = 0; fi < PRESET_ILLUSTRATOR_LIST.Length; fi++)
                                    {
                                        my_func_list[p_cw_ccw_idx].Items.Add(PRESET_ILLUSTRATOR_LIST[fi]);
                                    }
                                    break;
                                case Constants.APP_DATA_DIAL_FUNC_PRESET_ID_LIGHTROOM:
                                    for (int fi = 0; fi < PRESET_LIGHTROOM_LIST.Length; fi++)
                                    {
                                        my_func_list[p_cw_ccw_idx].Items.Add(PRESET_LIGHTROOM_LIST[fi]);
                                    }
                                    break;
                                default:
                                    break;
                            }

                            break;
                        default:
                            break;
                    }
                    
                    if (0 <= select_idx && select_idx < my_func_list[p_cw_ccw_idx].Items.Count)
                    {
                        my_func_list[p_cw_ccw_idx].SelectedIndex = select_idx;
                    }
                    else
                    {
                    }
                }
            }
            catch
            {
            }
        }

        private void lbx_category_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int cw_ccw_idx = int.Parse(((System.Windows.Forms.ListBox)sender).Tag.ToString());

                if (0 <= cw_ccw_idx && cw_ccw_idx < Constants.CW_CCW_NUM)
                {
                    int tmp_category_id = 0;
                    if (0 <= my_category[cw_ccw_idx].SelectedIndex && my_category[cw_ccw_idx].SelectedIndex < category_id.GetLength(1))
                    {
                        tmp_category_id = category_id[cw_ccw_idx, my_category[cw_ccw_idx].SelectedIndex];
                    }
                    category_id_select_val[cw_ccw_idx] = (byte)(tmp_category_id & 0xFF);
                    if (category_id_now_set_val[cw_ccw_idx] != category_id_select_val[cw_ccw_idx])
                    {
                        category2_id_select_val[cw_ccw_idx] = 0;
                        func_list_select_val[cw_ccw_idx] = 0;
                    }
                    else
                    {
                        category2_id_select_val[cw_ccw_idx] = category2_id_now_set_val[cw_ccw_idx];
                        func_list_select_val[cw_ccw_idx] = func_list_now_set_val[cw_ccw_idx];
                    }

                    my_create_category2_id_list(category_id_select_val[cw_ccw_idx], cw_ccw_idx);
                    my_create_func_id_list(category_id_select_val[cw_ccw_idx], category2_id_select_val[cw_ccw_idx], (byte)(cw_ccw_idx & 0xFF));

                    my_category2_disp(advance_mode_flag, category_id_select_val[cw_ccw_idx], category2_id_select_val[cw_ccw_idx], cw_ccw_idx);
                    my_func_list_disp(advance_mode_flag, category_id_select_val[cw_ccw_idx], category2_id_select_val[cw_ccw_idx], func_list_select_val[cw_ccw_idx], cw_ccw_idx);
                    my_function_setting_control_disp(category_id_select_val[cw_ccw_idx], func_list_select_val[cw_ccw_idx], cw_ccw_idx, true);
                }
            }
            catch
            {
            }
        }

        private void lbx_category2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int cw_ccw_idx = int.Parse(((System.Windows.Forms.ListBox)sender).Tag.ToString());

                if (0 <= cw_ccw_idx && cw_ccw_idx < Constants.CW_CCW_NUM)
                {
                    int tmp_category_id = 0;
                    if (0 <= my_category2[cw_ccw_idx].SelectedIndex && my_category2[cw_ccw_idx].SelectedIndex < category2_id.GetLength(1))
                    {
                        tmp_category_id = category2_id[cw_ccw_idx, my_category2[cw_ccw_idx].SelectedIndex];
                    }
                    category2_id_select_val[cw_ccw_idx] = (byte)(tmp_category_id & 0xFF);
                    if (category2_id_now_set_val[cw_ccw_idx] != category2_id_select_val[cw_ccw_idx])
                    {
                        func_list_select_val[cw_ccw_idx] = 0;
                    }
                    else
                    {
                        if (category_id_now_set_val[cw_ccw_idx] == category_id_select_val[cw_ccw_idx])
                        {
                            func_list_select_val[cw_ccw_idx] = func_list_now_set_val[cw_ccw_idx];
                        }
                        else
                        {
                            func_list_select_val[cw_ccw_idx] = 0;
                        }
                    }

                    my_create_func_id_list(category_id_select_val[cw_ccw_idx], category2_id_select_val[cw_ccw_idx], (byte)(cw_ccw_idx & 0xFF));

                    my_func_list_disp(advance_mode_flag, category_id_select_val[cw_ccw_idx], category2_id_select_val[cw_ccw_idx], func_list_select_val[cw_ccw_idx], cw_ccw_idx);
                    my_function_setting_control_disp(category_id_select_val[cw_ccw_idx], func_list_select_val[cw_ccw_idx], cw_ccw_idx, true);
                }
            }
            catch
            {
            }
        }
        private void lbx_func_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int cw_ccw_idx = int.Parse(((System.Windows.Forms.ListBox)sender).Tag.ToString());
                if (0 <= cw_ccw_idx && cw_ccw_idx < Constants.CW_CCW_NUM)
                {
                    byte tmp_func_id = 0;
                    if (0 <= my_func_list[cw_ccw_idx].SelectedIndex && my_func_list[cw_ccw_idx].SelectedIndex < func_list_id.GetLength(1))
                    {
                        tmp_func_id = func_list_id[cw_ccw_idx, my_func_list[cw_ccw_idx].SelectedIndex];
                    }
                    func_list_select_val[cw_ccw_idx] = tmp_func_id;


                    my_create_func_id_list(category_id_select_val[cw_ccw_idx], category2_id_select_val[cw_ccw_idx],(byte)(cw_ccw_idx & 0xFF));
                    my_function_setting_control_disp(category_id_select_val[cw_ccw_idx], func_list_select_val[cw_ccw_idx], cw_ccw_idx, true);
                }
            }
            catch
            {
            }
        }

        private void my_function_setting_control_disp(int p_category_id, int p_func_select_idx, int p_cw_ccw_idx, bool p_visible)
        {
            sbyte tmp_sbyte;
            decimal tmp_dec;
            try
            {
                int data_idx = (select_mode * Constants.FUNCTION_NUM * Constants.CW_CCW_NUM) + (select_func_no_now * Constants.CW_CCW_NUM) + p_cw_ccw_idx;

                switch (p_category_id)
                {
                    case Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_NOT_SET:
                        // 非表示
                        my_cw_ccw_title[p_cw_ccw_idx].Visible = false;
                        my_func_setting_title[p_cw_ccw_idx].Visible = false;
                        my_mouse_x[p_cw_ccw_idx].Visible = false;
                        my_mouse_y[p_cw_ccw_idx].Visible = false;
                        for (int fi = 0; fi < my_keyboard_modifier.GetLength(1); fi++)
                        {
                            my_keyboard_modifier[p_cw_ccw_idx, fi].Visible = false;
                        }
                        my_keyboard_key[p_cw_ccw_idx].Visible = false;
                        my_keyboard_key_clr[ p_cw_ccw_idx].Visible = false;
                        my_sensitivity_title[p_cw_ccw_idx].Visible = false;
                        my_encoder_sensitivity[p_cw_ccw_idx].Visible = false;
                        my_tbar_encoder_sensitivity[p_cw_ccw_idx].Visible = false;
                        break;
                    case Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_KEY:
                        switch (p_func_select_idx)
                        {
                            case Constants.APP_DATA_DIAL_FUNC_KEY_ID_NUM_UP:
                            case Constants.APP_DATA_DIAL_FUNC_KEY_ID_NUM_DOWN:
                                // 非表示
                                my_cw_ccw_title[p_cw_ccw_idx].Visible = p_visible;
                                my_func_setting_title[p_cw_ccw_idx].Visible = false;
                                my_mouse_x[p_cw_ccw_idx].Visible = false;
                                my_mouse_y[p_cw_ccw_idx].Visible = false;
                                for (int fi = 0; fi < my_keyboard_modifier.GetLength(1); fi++)
                                {
                                    my_keyboard_modifier[p_cw_ccw_idx, fi].Visible = false;
                                }
                                my_keyboard_key[p_cw_ccw_idx].Visible = false;
                                my_keyboard_key_clr[p_cw_ccw_idx].Visible = false;
                                my_sensitivity_title[p_cw_ccw_idx].Visible = p_visible;
                                my_encoder_sensitivity[p_cw_ccw_idx].Visible = p_visible;
                                my_encoder_sensitivity[p_cw_ccw_idx].Value = my_set_data.sensitivity[data_idx];
                                my_tbar_encoder_sensitivity[p_cw_ccw_idx].Visible = p_visible;
                                my_tbar_encoder_sensitivity[p_cw_ccw_idx].Value = my_set_data.sensitivity[data_idx];
                                break;
                            case Constants.APP_DATA_DIAL_FUNC_KEY_ID_KEY:
                                // KEY SET表示
                                my_cw_ccw_title[p_cw_ccw_idx].Visible = p_visible;
                                my_func_setting_title[p_cw_ccw_idx].Visible = false;
                                my_mouse_x[p_cw_ccw_idx].Visible = false;
                                my_mouse_y[p_cw_ccw_idx].Visible = false;
                        
                                for (int fi = 0; fi < my_keyboard_modifier.GetLength(1); fi++)
                                {
                                    my_keyboard_modifier[p_cw_ccw_idx, fi].Visible = p_visible;
                                    if ((my_set_data.keyboard_data[data_idx, Constants.KEYBOARD_DATA_MODIFIER_IDX] & my_form1.keyboard_modifier_bit[fi]) != 0)
                                    {
                                        my_keyboard_modifier[p_cw_ccw_idx, fi].Checked = true;
                                    }
                                    else
                                    {
                                        // 右の制御キービットもチェック
                                        if ((my_keyboard_modifier.GetLength(1) * 2) == my_form1.keyboard_modifier_bit.Length)
                                        {
                                            if ((my_set_data.keyboard_data[data_idx, Constants.KEYBOARD_DATA_MODIFIER_IDX] & my_form1.keyboard_modifier_bit[my_keyboard_modifier.GetLength(1) + fi]) != 0)
                                            {
                                                my_keyboard_modifier[p_cw_ccw_idx, fi].Checked = true;
                                            }
                                            else
                                            {
                                                my_keyboard_modifier[p_cw_ccw_idx, fi].Checked = false;
                                            }
                                        }
                                        else
                                        {
                                            my_keyboard_modifier[p_cw_ccw_idx, fi].Checked = false;
                                        }
                                    }
                                }
                                my_keyboard_key[p_cw_ccw_idx].Visible = p_visible;
                                if (my_set_data.keyboard_data[data_idx, Constants.KEYBOARD_DATA_KEY1_IDX] == 0)
                                {
                                    my_keyboard_key[p_cw_ccw_idx].Text = RevOmate.Properties.Resources.KEYBOARD_SET_KEY_EMPTY;
                                }
                                else
                                {
                                    my_keyboard_key[p_cw_ccw_idx].Text = const_Key_Code.Get_KeyCode_Name(my_set_data.keyboard_data[data_idx, Constants.KEYBOARD_DATA_KEY1_IDX], my_form1.system_setting_info.Keyboard_Type);
                                }
                                my_keyboard_key_clr[p_cw_ccw_idx].Visible = p_visible;
                                my_sensitivity_title[p_cw_ccw_idx].Visible = p_visible;
                                my_encoder_sensitivity[p_cw_ccw_idx].Visible = p_visible;
                                my_encoder_sensitivity[p_cw_ccw_idx].Value = my_set_data.sensitivity[data_idx];
                                my_tbar_encoder_sensitivity[p_cw_ccw_idx].Visible = p_visible;
                                my_tbar_encoder_sensitivity[p_cw_ccw_idx].Value = my_set_data.sensitivity[data_idx];
                                break;
                            default:
                                break;
                        }
                        break;
                    case Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_MOUSE:
                        switch (p_func_select_idx)
                        {
                            case Constants.APP_DATA_DIAL_FUNC_MOUSE_ID_MOVE:
                                // マウス移動
                                my_cw_ccw_title[p_cw_ccw_idx].Visible = p_visible;
                                my_func_setting_title[p_cw_ccw_idx].Text = RevOmate.Properties.Resources.MOUSE_MOVE_TEXT;
                                my_func_setting_title[p_cw_ccw_idx].Visible = p_visible;
                                my_mouse_x[p_cw_ccw_idx].Visible = p_visible;
                                my_mouse_x[p_cw_ccw_idx].Minimum = Constants.MOUSE_MOVE_MIN;
                                my_mouse_x[p_cw_ccw_idx].Maximum = Constants.MOUSE_MOVE_MAX;
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
                                my_mouse_x[p_cw_ccw_idx].Value = tmp_dec;
                                my_mouse_y[p_cw_ccw_idx].Visible = p_visible;
                                my_mouse_y[p_cw_ccw_idx].Minimum = Constants.MOUSE_MOVE_MIN;
                                my_mouse_y[p_cw_ccw_idx].Maximum = Constants.MOUSE_MOVE_MAX;
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
                                my_mouse_y[p_cw_ccw_idx].Value = tmp_dec;
                                for (int fi = 0; fi < my_keyboard_modifier.GetLength(1); fi++)
                                {
                                    my_keyboard_modifier[p_cw_ccw_idx, fi].Visible = false;
                                }
                                my_keyboard_key[p_cw_ccw_idx].Visible = false;
                                my_keyboard_key_clr[p_cw_ccw_idx].Visible = false;
                                my_sensitivity_title[p_cw_ccw_idx].Visible = p_visible;
                                my_encoder_sensitivity[p_cw_ccw_idx].Visible = p_visible;
                                my_encoder_sensitivity[p_cw_ccw_idx].Value = my_set_data.sensitivity[data_idx];
                                my_tbar_encoder_sensitivity[p_cw_ccw_idx].Visible = p_visible;
                                my_tbar_encoder_sensitivity[p_cw_ccw_idx].Value = my_set_data.sensitivity[data_idx];
                                break;
                            case Constants.APP_DATA_DIAL_FUNC_MOUSE_ID_WH_SCROLL:
                                // ホイールスクロール
                                my_cw_ccw_title[p_cw_ccw_idx].Visible = p_visible;
                                my_func_setting_title[p_cw_ccw_idx].Text = RevOmate.Properties.Resources.MOUSE_SCROLL_TEXT;
                                my_func_setting_title[p_cw_ccw_idx].Visible = p_visible;
                                my_mouse_x[p_cw_ccw_idx].Visible = p_visible;
                                my_mouse_x[p_cw_ccw_idx].Minimum = Constants.MOUSE_SCROLL_MIN;
                                my_mouse_x[p_cw_ccw_idx].Maximum = Constants.MOUSE_SCROLL_MAX;
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
                                my_mouse_x[p_cw_ccw_idx].Value = tmp_dec;
                                my_mouse_y[p_cw_ccw_idx].Visible = false;
                                for (int fi = 0; fi < my_keyboard_modifier.GetLength(1); fi++)
                                {
                                    my_keyboard_modifier[p_cw_ccw_idx, fi].Visible = false;
                                }
                                my_keyboard_key[p_cw_ccw_idx].Visible = false;
                                my_keyboard_key_clr[p_cw_ccw_idx].Visible = false;
                                my_sensitivity_title[p_cw_ccw_idx].Visible = p_visible;
                                my_encoder_sensitivity[p_cw_ccw_idx].Visible = p_visible;
                                my_encoder_sensitivity[p_cw_ccw_idx].Value = my_set_data.sensitivity[data_idx];
                                my_tbar_encoder_sensitivity[p_cw_ccw_idx].Visible = p_visible;
                                my_tbar_encoder_sensitivity[p_cw_ccw_idx].Value = my_set_data.sensitivity[data_idx];
                                break;
                            default:
                                // 非表示
                                my_cw_ccw_title[p_cw_ccw_idx].Visible = p_visible;
                                my_func_setting_title[p_cw_ccw_idx].Visible = false;
                                my_mouse_x[p_cw_ccw_idx].Visible = false;
                                my_mouse_y[p_cw_ccw_idx].Visible = false;
                                for (int fi = 0; fi < my_keyboard_modifier.GetLength(1); fi++)
                                {
                                    my_keyboard_modifier[p_cw_ccw_idx, fi].Visible = false;
                                }
                                my_keyboard_key[p_cw_ccw_idx].Visible = false;
                                my_keyboard_key_clr[p_cw_ccw_idx].Visible = false;
                                my_sensitivity_title[p_cw_ccw_idx].Visible = p_visible;
                                my_encoder_sensitivity[p_cw_ccw_idx].Visible = p_visible;
                                my_encoder_sensitivity[p_cw_ccw_idx].Value = my_set_data.sensitivity[data_idx];
                                my_tbar_encoder_sensitivity[p_cw_ccw_idx].Visible = p_visible;
                                my_tbar_encoder_sensitivity[p_cw_ccw_idx].Value = my_set_data.sensitivity[data_idx];
                                break;
                        }
                        break;
                    case Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_GAMEPAD:
                        switch (p_func_select_idx)
                        {
                            case Constants.APP_DATA_DIAL_FUNC_GAMEPAD_ID_LEFT_ANALOG:
                            case Constants.APP_DATA_DIAL_FUNC_GAMEPAD_ID_RIGHT_ANALOG:
                                // 左右レバー
                                my_cw_ccw_title[p_cw_ccw_idx].Visible = p_visible;
                                my_func_setting_title[p_cw_ccw_idx].Text = RevOmate.Properties.Resources.JOYPAD_MOVE_TEXT;
                                my_func_setting_title[p_cw_ccw_idx].Visible = p_visible;
                                my_joypad_x[p_cw_ccw_idx].Visible = p_visible;
                                my_joypad_x[p_cw_ccw_idx].Minimum = Constants.JOYPAD_MOVE_MIN;
                                my_joypad_x[p_cw_ccw_idx].Maximum = Constants.JOYPAD_MOVE_MAX;
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
                                my_joypad_x[p_cw_ccw_idx].Value = tmp_dec;
                                my_joypad_y[p_cw_ccw_idx].Visible = p_visible;
                                my_joypad_y[p_cw_ccw_idx].Minimum = Constants.JOYPAD_MOVE_MIN;
                                my_joypad_y[p_cw_ccw_idx].Maximum = Constants.JOYPAD_MOVE_MAX;
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
                                my_joypad_y[p_cw_ccw_idx].Value = tmp_dec;
                                for (int fi = 0; fi < my_keyboard_modifier.GetLength(1); fi++)
                                {
                                    my_keyboard_modifier[p_cw_ccw_idx, fi].Visible = false;
                                }
                                my_keyboard_key[p_cw_ccw_idx].Visible = false;
                                my_keyboard_key_clr[p_cw_ccw_idx].Visible = false;
                                my_sensitivity_title[p_cw_ccw_idx].Visible = p_visible;
                                my_encoder_sensitivity[p_cw_ccw_idx].Visible = p_visible;
                                my_encoder_sensitivity[p_cw_ccw_idx].Value = my_set_data.sensitivity[data_idx];
                                my_tbar_encoder_sensitivity[p_cw_ccw_idx].Visible = p_visible;
                                my_tbar_encoder_sensitivity[p_cw_ccw_idx].Value = my_set_data.sensitivity[data_idx];
                                break;
                            default:
                                // 非表示
                                my_cw_ccw_title[p_cw_ccw_idx].Visible = p_visible;
                                my_func_setting_title[p_cw_ccw_idx].Visible = false;
                                my_mouse_x[p_cw_ccw_idx].Visible = false;
                                my_mouse_y[p_cw_ccw_idx].Visible = false;
                                for (int fi = 0; fi < my_keyboard_modifier.GetLength(1); fi++)
                                {
                                    my_keyboard_modifier[p_cw_ccw_idx, fi].Visible = false;
                                }
                                my_keyboard_key[p_cw_ccw_idx].Visible = false;
                                my_keyboard_key_clr[p_cw_ccw_idx].Visible = false;
                                my_sensitivity_title[p_cw_ccw_idx].Visible = p_visible;
                                my_encoder_sensitivity[p_cw_ccw_idx].Visible = p_visible;
                                my_encoder_sensitivity[p_cw_ccw_idx].Value = my_set_data.sensitivity[data_idx];
                                my_tbar_encoder_sensitivity[p_cw_ccw_idx].Visible = p_visible;
                                my_tbar_encoder_sensitivity[p_cw_ccw_idx].Value = my_set_data.sensitivity[data_idx];
                                break;
                        }
                        break;
                    case Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_MULTIMEDIA:
                        // 非表示
                        my_cw_ccw_title[p_cw_ccw_idx].Visible = p_visible;
                        my_func_setting_title[p_cw_ccw_idx].Visible = false;
                        my_mouse_x[p_cw_ccw_idx].Visible = false;
                        my_mouse_y[p_cw_ccw_idx].Visible = false;
                        for (int fi = 0; fi < my_keyboard_modifier.GetLength(1); fi++)
                        {
                            my_keyboard_modifier[p_cw_ccw_idx, fi].Visible = false;
                        }
                        my_keyboard_key[p_cw_ccw_idx].Visible = false;
                        my_keyboard_key_clr[p_cw_ccw_idx].Visible = false;
                        my_sensitivity_title[p_cw_ccw_idx].Visible = p_visible;
                        my_encoder_sensitivity[p_cw_ccw_idx].Visible = p_visible;
                        my_encoder_sensitivity[p_cw_ccw_idx].Value = my_set_data.sensitivity[data_idx];
                        my_tbar_encoder_sensitivity[p_cw_ccw_idx].Visible = p_visible;
                        my_tbar_encoder_sensitivity[p_cw_ccw_idx].Value = my_set_data.sensitivity[data_idx];
                        break;
                    case Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_USERDIALMACRO:
                        // 非表示
                        my_cw_ccw_title[p_cw_ccw_idx].Visible = p_visible;
                        my_func_setting_title[p_cw_ccw_idx].Visible = false;
                        my_mouse_x[p_cw_ccw_idx].Visible = false;
                        my_mouse_y[p_cw_ccw_idx].Visible = false;
                        for (int fi = 0; fi < my_keyboard_modifier.GetLength(1); fi++)
                        {
                            my_keyboard_modifier[p_cw_ccw_idx, fi].Visible = false;
                        }
                        my_keyboard_key[p_cw_ccw_idx].Visible = false;
                        my_keyboard_key_clr[p_cw_ccw_idx].Visible = false;
                        my_sensitivity_title[p_cw_ccw_idx].Visible = p_visible;
                        my_encoder_sensitivity[p_cw_ccw_idx].Visible = p_visible;
                        my_encoder_sensitivity[p_cw_ccw_idx].Value = my_set_data.sensitivity[data_idx];
                        my_tbar_encoder_sensitivity[p_cw_ccw_idx].Visible = p_visible;
                        my_tbar_encoder_sensitivity[p_cw_ccw_idx].Value = my_set_data.sensitivity[data_idx];
                        break;
                    case Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_PRESET:
                        // 非表示
                        my_cw_ccw_title[p_cw_ccw_idx].Visible = p_visible;
                        my_func_setting_title[p_cw_ccw_idx].Visible = false;
                        my_mouse_x[p_cw_ccw_idx].Visible = false;
                        my_mouse_y[p_cw_ccw_idx].Visible = false;
                        for (int fi = 0; fi < my_keyboard_modifier.GetLength(1); fi++)
                        {
                            my_keyboard_modifier[p_cw_ccw_idx, fi].Visible = false;
                        }
                        my_keyboard_key[p_cw_ccw_idx].Visible = false;
                        my_keyboard_key_clr[p_cw_ccw_idx].Visible = false;
                        my_sensitivity_title[p_cw_ccw_idx].Visible = p_visible;
                        my_encoder_sensitivity[p_cw_ccw_idx].Visible = p_visible;
                        my_encoder_sensitivity[p_cw_ccw_idx].Value = my_set_data.sensitivity[data_idx];
                        my_tbar_encoder_sensitivity[p_cw_ccw_idx].Visible = p_visible;
                        my_tbar_encoder_sensitivity[p_cw_ccw_idx].Value = my_set_data.sensitivity[data_idx];
                        break;
                    default:
                        break;
                }
            }
            catch
            {
            }
        }

        private void btn_func_key_clr_cw_Click(object sender, EventArgs e)
        {
            try
            {
                int idx = int.Parse(((System.Windows.Forms.Button)sender).Tag.ToString());
                if (0 <= idx && idx < Constants.CW_CCW_NUM)
                {
                    for (int fi = 0; fi < my_keyboard_modifier.GetLength(1); fi++)
                    {
                        my_keyboard_modifier[idx, fi].Checked = false;
                    }
                    my_keyboard_key[idx].Text = RevOmate.Properties.Resources.KEYBOARD_SET_KEY_EMPTY;
                    int data_idx = (select_mode * Constants.FUNCTION_NUM * Constants.CW_CCW_NUM) + (select_func_no_now * Constants.CW_CCW_NUM) + idx;
                    my_set_data.keyboard_data[data_idx, Constants.KEYBOARD_DATA_KEY1_IDX] = 0x00;
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
                if (0 <= idx && idx < Constants.CW_CCW_NUM)
                {
                    // ctrl, shift, altキーも受け付ける
                    byte tmp_usb_key = const_Key_Code.Get_VKtoUSBkey(e.KeyValue.GetHashCode(), my_form1.system_setting_info.Keyboard_Type, true);
                    my_keyboard_key[idx].Text = const_Key_Code.Get_KeyCode_Name(tmp_usb_key, my_form1.system_setting_info.Keyboard_Type);
                    int data_idx = (select_mode * Constants.FUNCTION_NUM * Constants.CW_CCW_NUM) + (select_func_no_now * Constants.CW_CCW_NUM) + idx;
                    my_set_data.keyboard_data[data_idx, Constants.KEYBOARD_DATA_KEY1_IDX] = tmp_usb_key;
                    e.SuppressKeyPress = true;
#if false
                    if (e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.Alt)
                    //if ((e.KeyCode & Keys.ControlKey) == Keys.ControlKey || (e.KeyCode & Keys.ShiftKey) == Keys.ShiftKey || (e.KeyCode & Keys.Alt) == Keys.Alt)
                    {
                        e.Handled = true;
                    }
                    else
                    {
                        byte tmp_usb_key = const_Key_Code.Get_VKtoUSBkey(e.KeyValue.GetHashCode(), true);
                        my_keyboard_key[idx].Text = const_Key_Code.USB_KeyCode_Name[tmp_usb_key];
                        int data_idx = (select_mode * Constants.FUNCTION_NUM * Constants.CW_CCW_NUM) + (select_func_no_now * Constants.CW_CCW_NUM) + idx;
                        my_set_data.keyboard_data[data_idx, Constants.KEYBOARD_DATA_KEY1_IDX] = tmp_usb_key;
                        e.SuppressKeyPress = true;
                    }
#endif
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
                if (0 <= idx && idx < Constants.CW_CCW_NUM)
                {
                    if (e.KeyCode == Keys.PrintScreen)
                    {
                        byte tmp_usb_key = const_Key_Code.Get_VKtoUSBkey(e.KeyValue.GetHashCode(), my_form1.system_setting_info.Keyboard_Type, true);
                        my_keyboard_key[idx].Text = const_Key_Code.Get_KeyCode_Name(tmp_usb_key, my_form1.system_setting_info.Keyboard_Type);
                        int data_idx = (select_mode * Constants.FUNCTION_NUM * Constants.CW_CCW_NUM) + (select_func_no_now * Constants.CW_CCW_NUM) + idx;
                        my_set_data.keyboard_data[data_idx, Constants.KEYBOARD_DATA_KEY1_IDX] = tmp_usb_key;
                        e.SuppressKeyPress = true;
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
                if (0 <= idx && idx < Constants.CW_CCW_NUM)
                {
                    if (e.KeyCode == Keys.Tab)
                    {
                        if (my_keyboard_key[idx].Text != e.KeyValue.ToString())
                        {
                            byte tmp_usb_key = const_Key_Code.Get_VKtoUSBkey(e.KeyValue.GetHashCode(), my_form1.system_setting_info.Keyboard_Type, true);
                            my_keyboard_key[idx].Text = const_Key_Code.Get_KeyCode_Name(tmp_usb_key, my_form1.system_setting_info.Keyboard_Type);
                            int data_idx = (select_mode * Constants.FUNCTION_NUM * Constants.CW_CCW_NUM) + (select_func_no_now * Constants.CW_CCW_NUM) + idx;
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

        // カテゴリと機能のリストIDからset type値を取得する
        private int my_Get_set_type_val_by_list_id(byte p_category_id, byte p_category2_id, byte p_func_id, ref byte o_set_type_val)
        {
            int ret_val = 0;
            int tmp_set_type = Constants.SET_TYPE_NONE;
            try
            {

                switch (p_category_id)
                {
                    case Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_NOT_SET:
                        break;
                    case Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_KEY:
                        for (int fi = 0; fi < FUNC_KEY_ID_LIST.Length && fi < func_key_set_type_no_list.Length; fi++)
                        {
                            if (FUNC_KEY_ID_LIST[fi] == p_func_id)
                            {
                                tmp_set_type = func_key_set_type_no_list[fi];
                                break;
                            }
                        }
                        break;
                    case Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_MOUSE:
                        for (int fi = 0; fi < FUNC_MOUSE_ID_LIST.Length && fi < func_mouse_set_type_no_list.Length; fi++)
                        {
                            if (FUNC_MOUSE_ID_LIST[fi] == p_func_id)
                            {
                                tmp_set_type = func_mouse_set_type_no_list[fi];
                                break;
                            }
                        }
                        break;
                    case Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_GAMEPAD:
                        for (int fi = 0; fi < FUNC_GAMEPAD_ID_LIST.Length && fi < func_gamepad_set_type_no_list.Length; fi++)
                        {
                            if (FUNC_GAMEPAD_ID_LIST[fi] == p_func_id)
                            {
                                tmp_set_type = func_gamepad_set_type_no_list[fi];
                                break;
                            }
                        }
                        break;
                    case Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_MULTIMEDIA:
                        for (int fi = 0; fi < FUNC_MULTIMEDIA_ID_LIST.Length && fi < func_multimedia_set_type_no_list.Length; fi++)
                        {
                            if (FUNC_MULTIMEDIA_ID_LIST[fi] == p_func_id)
                            {
                                tmp_set_type = func_multimedia_set_type_no_list[fi];
                                break;
                            }
                        }
                        break;
                    case Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_USERDIALMACRO:
                        for (int fi = 0; fi < func_user_dial_macro_set_type_no_list.Length; fi++)
                        {
                            if (fi == p_func_id)
                            {
                                tmp_set_type = func_user_dial_macro_set_type_no_list[fi];
                                break;
                            }
                        }
                        break;
                    case Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_PRESET:
                        break;
                    default:
                        break;
                }
            }
            catch
            {
            }
            o_set_type_val = (byte)(tmp_set_type & 0xFF);
            return ret_val;
        }

        // set type値からカテゴリと機能のリストIDを取得する
        private int my_Get_list_id_by_set_type_val(byte p_set_type_val, ref byte o_category_id, ref byte o_func_id)
        {
            int ret_val = 0;
            try
            {
                o_category_id = 0;
                o_func_id = 0;

                if (p_set_type_val == Constants.SET_TYPE_NONE)
                {
                }
                else
                {
                    bool found_flag = false;

                    // KEY
                    for (int fi = 0; fi < func_key_set_type_no_list.Length; fi++)
                    {
                        if (func_key_set_type_no_list[fi] == p_set_type_val)
                        {
                            o_func_id = FUNC_KEY_ID_LIST[fi];
                            found_flag = true;
                            break;
                        }
                    }
                    if(found_flag == true)
                    {
                        o_category_id = Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_KEY;
                    }
                    // MOUSE
                    found_flag = false;
                    for (int fi = 0; fi < func_mouse_set_type_no_list.Length; fi++)
                    {
                        if (func_mouse_set_type_no_list[fi] == p_set_type_val)
                        {
                            o_func_id = FUNC_MOUSE_ID_LIST[fi];
                            found_flag = true;
                            break;
                        }
                    }
                    if (found_flag == true)
                    {
                        o_category_id = Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_MOUSE;
                    }
                    // GAMEPAD
                    found_flag = false;
                    for (int fi = 0; fi < func_gamepad_set_type_no_list.Length; fi++)
                    {
                        if (func_gamepad_set_type_no_list[fi] == p_set_type_val)
                        {
                            o_func_id = FUNC_GAMEPAD_ID_LIST[fi];
                            found_flag = true;
                            break;
                        }
                    }
                    if (found_flag == true)
                    {
                        o_category_id = Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_GAMEPAD;
                    }
                    // MULTIMEDIA
                    found_flag = false;
                    for (int fi = 0; fi < func_multimedia_set_type_no_list.Length; fi++)
                    {
                        if (func_multimedia_set_type_no_list[fi] == p_set_type_val)
                        {
                            o_func_id = FUNC_MULTIMEDIA_ID_LIST[fi];
                            found_flag = true;
                            break;
                        }
                    }
                    if (found_flag == true)
                    {
                        o_category_id = Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_MULTIMEDIA;
                    }
                    // USER DIAL MACRO
                    found_flag = false;
                    for (int fi = 0; fi < func_user_dial_macro_set_type_no_list.Length; fi++)
                    {
                        if (func_user_dial_macro_set_type_no_list[fi] == p_set_type_val)
                        {
                            o_func_id = (byte)(fi & 0xFF);
                            found_flag = true;
                            break;
                        }
                    }
                    if (found_flag == true)
                    {
                        o_category_id = Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_USERDIALMACRO;
                    }
                }
            }
            catch
            {
            }
            return ret_val;
        }

        // カテゴリと機能のリストIDからプリセットデータを取得する
        // 戻り値
        //  Flase : 選択されているカテゴリ等がプリセットでない
        //  True : 選択されているカテゴリ等がプリセットである
        private bool my_Get_preset_data_by_list_id(byte p_category_id, byte p_category2_id, byte p_func_id, ref byte o_set_type_val, ref byte[] o_data)
        {
            bool ret_val = false;
            try
            {

                for (int fi = 0; fi < o_data.Length; fi++)
                {
                    o_data[fi] = 0;
                }

                if (o_data.Length == Constants.APP_DATA_DIAL_FUNC_PRESET_DATA_DATA_LEN)
                {
                    switch (p_category_id)
                    {
                        case Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_PRESET:
                            // プリセットデータファイルチェック
                            string preset_data_file_path = System.Environment.CurrentDirectory;
                            string tmp_file_path = "";
                            if (my_form1.system_setting_info.Keyboard_Type == Constants.KEYBOARD_TYPE_JA)
                            {
                                tmp_file_path = Constants.APP_DATA_DIAL_FUNC_PRESET_DATA_PATH_JA + string.Format("{0:000}\\{1:000}", p_category2_id, p_func_id) + Constants.APP_DATA_DIAL_FUNC_PRESET_DATA_FILE_EXTENSION;
                            }
                            else
                            {
                                tmp_file_path = Constants.APP_DATA_DIAL_FUNC_PRESET_DATA_PATH_US + string.Format("{0:000}\\{1:000}", p_category2_id, p_func_id) + Constants.APP_DATA_DIAL_FUNC_PRESET_DATA_FILE_EXTENSION;
                            }
                            preset_data_file_path = Path.Combine(preset_data_file_path, tmp_file_path);
                            if (File.Exists(preset_data_file_path) == true)
                            {
                                byte read_set_type = Constants.SET_TYPE_NONE;
                                byte[] read_data = new byte[Constants.APP_DATA_DIAL_FUNC_PRESET_DATA_DATA_LEN];
                                int read_size = my_Preset_Data_File_Read(preset_data_file_path, ref read_set_type, ref read_data);
                                if (read_size == Constants.DEVICE_DATA_LEN)
                                {
                                    o_set_type_val = read_set_type;
                                    for (int fi = 0; fi < read_data.Length; fi++)
                                    {
                                        o_data[fi] = read_data[fi];
                                    }
                                    ret_val = true;
                                }
                                else
                                {
                                    StatusBox_txtbx.Text = RevOmate.Properties.Resources.PRESET_DATA_FILE_READ_ERROR + "[" + read_size.ToString() + "][" + tmp_file_path + "]";
                                }
                            }
                            else
                            {
                                StatusBox_txtbx.Text = RevOmate.Properties.Resources.PRESET_DATA_FILE_READ_ERROR + "[" + preset_data_file_path + "]";
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch
            {
            }
            return ret_val;
        }

        // プリセットデータ読み込み
        private int my_Preset_Data_File_Read(string p_file_path, ref byte o_set_type, ref byte[] o_data)
        {
            int i_ret = -1;
            try
            {
                if (File.Exists(p_file_path) == true)
                {
                    System.IO.FileStream fs = null;
                    try
                    {
                        int buff_max_size = Constants.DEVICE_DATA_LEN;
                        byte[] read_buff = new byte[buff_max_size];
                        fs = new System.IO.FileStream(p_file_path, FileMode.Open, FileAccess.Read);
                        if (fs.Length <= buff_max_size)
                        {
                            int read_size = fs.Read(read_buff, 0, (int)fs.Length);
                            if (read_size == fs.Length)
                            {

                                o_set_type = read_buff[0];
                                for (int fi = 0; fi < Constants.APP_DATA_DIAL_FUNC_PRESET_DATA_DATA_LEN; fi++)
                                {
                                    o_data[fi] = read_buff[1+fi];
                                }

                                i_ret = read_size;
                            }
                        }
                        else
                        {   // ファイルサイズ異常（大きい）
                            i_ret = -2;
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
            return i_ret;
        }

        private int my_Set_preset_data(int p_data_idx, byte p_set_type, byte[] p_data)
        {
            int i_ret = 0;
            try
            {
                if (p_data.Length == Constants.APP_DATA_DIAL_FUNC_PRESET_DATA_DATA_LEN
                    && 0 <= p_data_idx && p_data_idx < (Constants.MODE_NUM * Constants.FUNCTION_NUM * Constants.CW_CCW_NUM))
                {
                    switch (p_set_type)
                    {
                        case Constants.SET_TYPE_MOUSE_LCLICK:
                        case Constants.SET_TYPE_MOUSE_RCLICK:
                        case Constants.SET_TYPE_MOUSE_WHCLICK:
                        case Constants.SET_TYPE_MOUSE_B4CLICK:
                        case Constants.SET_TYPE_MOUSE_B5CLICK:
                        case Constants.SET_TYPE_MOUSE_DCLICK:
                        case Constants.SET_TYPE_MOUSE_MOVE:
                        case Constants.SET_TYPE_MOUSE_WHSCROLL:
                            my_set_data.set_type[p_data_idx] = p_set_type;
                            for (int fi = 0; fi < Constants.MOUSE_DATA_LEN; fi++)
                            {
                                my_set_data.mouse_data[p_data_idx, fi] = p_data[fi];
                            }
                            break;
                        case Constants.SET_TYPE_KEYBOARD_KEY:
                            my_set_data.set_type[p_data_idx] = p_set_type;
                            // モディファイビットをクリア
                            for (int fi = 0; fi < Constants.KEYBOARD_DATA_LEN; fi++)
                            {
                                my_set_data.keyboard_data[p_data_idx, fi] = p_data[fi];
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
                            my_set_data.set_type[p_data_idx] = p_set_type;
                            for (int fi = 0; fi < Constants.MULTIMEDIA_DATA_LEN; fi++)
                            {
                                my_set_data.multimedia_data[p_data_idx, fi] = p_data[fi];
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
                            my_set_data.set_type[p_data_idx] = p_set_type;
                            for (int fi = 0; fi < Constants.JOYPAD_DATA_LEN; fi++)
                            {
                                my_set_data.joypad_data[p_data_idx, fi] = p_data[fi];
                            }
                            break;
                        case Constants.SET_TYPE_NUMBER_UP:
                        case Constants.SET_TYPE_NUMBER_DOWN:
                            my_set_data.set_type[p_data_idx] = p_set_type;
                            break;
                        case Constants.SET_TYPE_ENCODER_SCRIPT1:
                        case Constants.SET_TYPE_ENCODER_SCRIPT2:
                        case Constants.SET_TYPE_ENCODER_SCRIPT3:
                            my_set_data.set_type[p_data_idx] = p_set_type;
                            break;
                        case Constants.SET_TYPE_NONE:
                        default:
                            my_set_data.set_type[p_data_idx] = (byte)(Constants.SET_TYPE_NONE & 0xFF);
                            break;
                    }
                }
            }
            catch
            {
            }
            return i_ret;
        }

        // 画面表示内容を取得して設定データバッファに格納する
        private void my_dial_function_setting_get_by_disp(byte p_mode_no, byte p_func_no)
        {
            int move_val = 0;
            try
            {
                if (0 <= p_mode_no && p_mode_no < Constants.MODE_NUM)
                {
                    if (0 <= p_func_no && p_func_no < Constants.FUNCTION_NUM)
                    {

                        // 機能名称
                        function_name[p_func_no] = my_func_name[p_func_no].Text;
                        //// LED
                        //for (int led_idx = 0; led_idx < Constants.LED_RGB_COLOR_NUM; led_idx++)
                        //{
                        //    my_func_datas.func_datas[p_mode_no].func_data[func_no].LED_RGB_duty[led_idx] = (byte)(my_tb_LED_Duty_set_func[func_no, led_idx].Value & 0xFF);
                        //}
                        //// LED輝度設定
                        //for (int fi = 0; fi <= my_rbtn_LED_Brightness_Level_set_func.GetUpperBound(1); fi++)
                        //{
                        //    if (my_rbtn_LED_Brightness_Level_set_func[func_no, fi].Checked == true)
                        //    {
                        //        my_func_datas.func_datas[p_mode_no].func_data[func_no].LED_brightness_level = (byte)(fi & 0xFF);
                        //        break;
                        //    }
                        //}

                        for (byte cw_ccw_idx = 0; cw_ccw_idx < Constants.CW_CCW_NUM; cw_ccw_idx++)
                        {
                            int data_idx = (p_mode_no * Constants.FUNCTION_NUM * Constants.CW_CCW_NUM) + (p_func_no * Constants.CW_CCW_NUM) + cw_ccw_idx;

                            // リストからアプリ用データをセット
                            my_Set_app_data_by_list_id(p_func_no, cw_ccw_idx, category_id_select_val[cw_ccw_idx], category2_id_select_val[cw_ccw_idx], func_list_select_val[cw_ccw_idx]);
                            
                            byte set_type_no = 0;
                            byte[] set_data = new byte[Constants.APP_DATA_DIAL_FUNC_PRESET_DATA_DATA_LEN];
                            if (my_Get_preset_data_by_list_id(category_id_select_val[cw_ccw_idx], category2_id_select_val[cw_ccw_idx], func_list_select_val[cw_ccw_idx], ref set_type_no, ref set_data) == true)
                            {   // プリセット
                                my_Set_preset_data(data_idx, set_type_no, set_data);
                            }
                            else
                            {   // プリセット以外

                                my_Get_set_type_val_by_list_id(category_id_select_val[cw_ccw_idx], category2_id_select_val[cw_ccw_idx], func_list_select_val[cw_ccw_idx], ref set_type_no);
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
                                            move_val = int.Parse(my_mouse_x[cw_ccw_idx].Value.ToString());
                                        }
                                        catch
                                        {
                                        }
                                        my_set_data.mouse_data[data_idx, Constants.MOUSE_DATA_X_MOVE_IDX] = (byte)(move_val & 0xFF);
                                        move_val = 0;
                                        try
                                        {
                                            move_val = int.Parse(my_mouse_y[cw_ccw_idx].Value.ToString());
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
                                            move_val = int.Parse(my_mouse_x[cw_ccw_idx].Value.ToString());
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
                                        for (int fj = 0; fj < my_keyboard_modifier.GetLength(1); fj++)
                                        {
                                            if (my_keyboard_modifier[cw_ccw_idx, fj].Checked == true)
                                            {
                                                my_set_data.keyboard_data[data_idx, Constants.KEYBOARD_DATA_MODIFIER_IDX] |= my_form1.keyboard_modifier_bit[fj];
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
                                            move_val = int.Parse(my_joypad_x[cw_ccw_idx].Value.ToString());
                                        }
                                        catch
                                        {
                                        }
                                        my_set_data.joypad_data[data_idx, Constants.JOYPAD_DATA_X_MOVE_IDX] = (byte)(move_val & 0xFF);
                                        move_val = 0;
                                        try
                                        {
                                            move_val = int.Parse(my_joypad_y[cw_ccw_idx].Value.ToString());
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
                            }


                            // 感度
                            my_set_data.sensitivity[data_idx] = (byte)((int)(my_encoder_sensitivity[cw_ccw_idx].Value) & 0xFF);
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void lbl_func_color_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                int tmp_func_no = int.Parse(((Label)sender).Tag.ToString());
                my_form1.my_led_color_setting_disp(select_mode, tmp_func_no);

                my_func_mode_LED_disp(select_mode);
            }
            catch
            {
            }
        }

        private void my_func_mode_LED_disp(byte p_mode)
        {
            int tmp_alpha = 0;
            try
            {
                for (int fi = 0; fi < my_func_colors.Length; fi++)
                {
                    int[] tmp_LED_RGB = new int[Constants.LED_RGB_COLOR_NUM];
                    int[] tmp_Preview_RGB = new int[Constants.LED_RGB_COLOR_NUM];
                    tmp_LED_RGB[0] = my_func_datas.func_datas[p_mode].func_data[fi].LED_RGB_duty[0] * 0xFF / my_LED_Duty_Max[0];
                    tmp_LED_RGB[1] = my_func_datas.func_datas[p_mode].func_data[fi].LED_RGB_duty[1] * 0xFF / my_LED_Duty_Max[1];
                    tmp_LED_RGB[2] = my_func_datas.func_datas[p_mode].func_data[fi].LED_RGB_duty[2] * 0xFF / my_LED_Duty_Max[2];

                    CommonFunc.my_LED_RGB_to_Preview_RGB(tmp_LED_RGB, tmp_Preview_RGB, out tmp_alpha);
                    my_func_colors[fi].BackColor = Color.FromArgb(tmp_alpha, tmp_Preview_RGB[0], tmp_Preview_RGB[1], tmp_Preview_RGB[2]);
                }
            }
            catch
            {
            }
        }

        // App Func Data値からカテゴリと機能のリストIDを取得する
        // 戻り値
        // false : プリセットでない
        // true  : プリセットである　category id, category2 id, func_listを返す
        private bool my_Get_list_id_by_app_data(byte p_func_id, byte p_cwccw_idx, ref byte o_category_id, ref byte o_category2_id, ref byte o_func_id)
        {
            bool ret_val = false;
            try
            {
                o_category_id = 0;
                o_category2_id = 0;
                o_func_id = 0;

                if (p_func_id < Constants.FUNCTION_NUM && p_cwccw_idx < Constants.CW_CCW_NUM)
                {
                    if (my_app_func_mode_datas.func[p_func_id].cwccw[p_cwccw_idx].app_data.select_data.Length == Constants.APP_FUNC_DATA_SELECT_DATA_LEN)
                    {
                        if (my_app_func_mode_datas.func[p_func_id].cwccw[p_cwccw_idx].app_data.select_data[Constants.APP_FUNC_DATA_SELECT_DATA_CATEGORY1_IDX] == Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_PRESET)
                        {   // プリセットデータ
                            o_category_id = my_app_func_mode_datas.func[p_func_id].cwccw[p_cwccw_idx].app_data.select_data[Constants.APP_FUNC_DATA_SELECT_DATA_CATEGORY1_IDX];
                            o_category2_id = my_app_func_mode_datas.func[p_func_id].cwccw[p_cwccw_idx].app_data.select_data[Constants.APP_FUNC_DATA_SELECT_DATA_CATEGORY2_IDX];
                            o_func_id = my_app_func_mode_datas.func[p_func_id].cwccw[p_cwccw_idx].app_data.select_data[Constants.APP_FUNC_DATA_SELECT_DATA_FUNC_LIST_IDX];

                            ret_val = true;
                        }
                    }
                }
            }
            catch
            {
            }
            return ret_val;
        }

        // カテゴリと機能のリストIDからApp Func Data値をセットする
        // 戻り値
        // false : プリセットでない
        // true  : プリセットである　セット完了
        private bool my_Set_app_data_by_list_id(byte p_func_id, byte p_cwccw_idx, byte p_category_id, byte p_category2_id, byte p_func_list_id)
        {
            bool ret_val = false;
            try
            {
                if (p_func_id < Constants.FUNCTION_NUM && p_cwccw_idx < Constants.CW_CCW_NUM)
                {
                    if (p_category_id == Constants.APP_DATA_DIAL_FUNC_CATEGORY_ID_PRESET)
                    {   // プリセットデータセット
                        my_app_func_mode_datas.func[p_func_id].cwccw[p_cwccw_idx].app_data.select_data[Constants.APP_FUNC_DATA_SELECT_DATA_CATEGORY1_IDX] = p_category_id;
                        my_app_func_mode_datas.func[p_func_id].cwccw[p_cwccw_idx].app_data.select_data[Constants.APP_FUNC_DATA_SELECT_DATA_CATEGORY2_IDX] = p_category2_id;
                        my_app_func_mode_datas.func[p_func_id].cwccw[p_cwccw_idx].app_data.select_data[Constants.APP_FUNC_DATA_SELECT_DATA_FUNC_LIST_IDX] = p_func_list_id;

                        ret_val = true;
                    }
                    else
                    {
                        my_app_func_mode_datas.func[p_func_id].cwccw[p_cwccw_idx].app_data.select_data[Constants.APP_FUNC_DATA_SELECT_DATA_CATEGORY1_IDX] = 0;
                        my_app_func_mode_datas.func[p_func_id].cwccw[p_cwccw_idx].app_data.select_data[Constants.APP_FUNC_DATA_SELECT_DATA_CATEGORY2_IDX] = 0;
                        my_app_func_mode_datas.func[p_func_id].cwccw[p_cwccw_idx].app_data.select_data[Constants.APP_FUNC_DATA_SELECT_DATA_FUNC_LIST_IDX] = 0;
                    }
                }
            }
            catch
            {
            }
            return ret_val;
        }

        // 画面更新
        private void my_func_disp_all_update()
        {
            try
            {

                for (byte fi = 0; fi < Constants.CW_CCW_NUM; fi++)
                {
                    byte tmp_cat1 = 0;
                    byte tmp_cat2 = 0;
                    byte tmp_func = 0;
                    if (my_Get_list_id_by_app_data(select_func_no_now, fi, ref tmp_cat1, ref tmp_cat2, ref tmp_func) == true)
                    {   // プリセットデータ
                        category_id_select_val[fi] = tmp_cat1;
                        category_id_now_set_val[fi] = category_id_select_val[fi];
                        category2_id_select_val[fi] = tmp_cat2;
                        category2_id_now_set_val[fi] = category2_id_select_val[fi];
                        func_list_select_val[fi] = tmp_func;
                        func_list_now_set_val[fi] = func_list_select_val[fi];
                    }
                    else
                    {   // プリセットデータでない
                        int set_type_idx = (select_mode * Constants.FUNCTION_NUM * Constants.CW_CCW_NUM) + (select_func_no_now * Constants.CW_CCW_NUM) + fi;
                        byte set_type_tmp = (byte)(my_set_data.set_type[set_type_idx]);
                        my_Get_list_id_by_set_type_val(set_type_tmp, ref category_id_select_val[fi], ref func_list_select_val[fi]);
                        category_id_now_set_val[fi] = category_id_select_val[fi];
                        func_list_now_set_val[fi] = func_list_select_val[fi];
                    }

                    my_create_category_id_list(fi);
                    my_category_disp(advance_mode_flag, category_id_select_val[fi], fi);
                    my_category2_disp(advance_mode_flag, category_id_select_val[fi], category2_id_select_val[fi], fi);
                    my_func_list_disp(advance_mode_flag, category_id_select_val[fi], category2_id_select_val[fi], func_list_select_val[fi], fi);
                }
            }
            catch
            {
            }
        }

        private void num_func1_sensivity_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int idx = int.Parse(((System.Windows.Forms.NumericUpDown)sender).Tag.ToString());
                my_tbar_encoder_sensitivity[idx].Value = (int)(my_encoder_sensitivity[idx].Value);
            }
            catch
            {
            }
        }

        private void tbar__func1_sensivity_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int idx = int.Parse(((System.Windows.Forms.TrackBar)sender).Tag.ToString());
                my_encoder_sensitivity[idx].Value = my_tbar_encoder_sensitivity[idx].Value;
            }
            catch
            {
            }
        }

        private void llbl_help_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                my_form1.my_help_disp();
            }
            catch
            {
            }
        }
    }
}
