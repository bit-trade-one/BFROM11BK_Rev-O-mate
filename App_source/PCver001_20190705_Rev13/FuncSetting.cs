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
    public partial class FuncSetting : Form
    {
        Form1 my_form1;
        byte select_mode = 0;
        byte sw_id = 0;

        byte category1_id_now_set_val = 0;   // 現在の設定値
        byte category2_id_now_set_val = 0;
        byte func_list_now_set_val = 0;
        byte encoder_default_now_set_val = 0;
        byte category1_id_select_val = 0;   // 現在の選択値
        byte category2_id_select_val = 0;
        byte func_list_select_val = 0;
        byte encoder_default_select_val = 0;

        bool advance_mode_flag = false;
        byte[] app_select_data;
        byte[] app_data;
        byte[] category_id = new byte[1];
        byte[] category2_id = new byte[1];
        byte[] func_list_id = new byte[1];
        byte[] my_set_sw_func_data;
        static KeyCode const_Key_Code = new KeyCode();

        CheckBox[] my_keyboard_modifier;

        string[] sp_func_type_list;
        byte[] sp_func_type_no_list;
        Form1.STR_SCRIPT_INFO_DATAS my_script_info_datas;
        Form1.STR_BASE_MODE_INFOS my_base_mode_infos;
        Form1.STR_FUNC_DATAS my_func_datas;
        Form1.STR_SW_FUNC_DATAS my_sw_func_datas;

        // カテゴリデータ定義
        string[] CATEGORY_LIST = new string[] { RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_NOT_SET,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_GENERAL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_MACRO,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_KEY,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_MOUSE,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_GAMEPAD,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_MULTIMEDIA};
        byte[] CATEGORY_ID_LIST = new byte[] { Constants.APP_DATA_FUNC_CATEGORY_ID_NOT_SET,
                                                        Constants.APP_DATA_FUNC_CATEGORY_ID_GENERAL,
                                                        Constants.APP_DATA_FUNC_CATEGORY_ID_MACRO,
                                                        Constants.APP_DATA_FUNC_CATEGORY_ID_PRESET,
                                                        Constants.APP_DATA_FUNC_CATEGORY_ID_KEY,
                                                        Constants.APP_DATA_FUNC_CATEGORY_ID_MOUSE,
                                                        Constants.APP_DATA_FUNC_CATEGORY_ID_GAMEPAD,
                                                        Constants.APP_DATA_FUNC_CATEGORY_ID_MULTIMEDIA};

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
#if true
        // カテゴリ2 プリセットデータ定義
        string[] CATEGORY_PRESET_LIST = new string[] { RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_SAI,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR};
        byte[] CATEGORY_PRESET_ID_LIST = new byte[] { Constants.APP_DATA_FUNC_PRESET_ID_PHOTOSHOP,
                                                        Constants.APP_DATA_FUNC_PRESET_ID_CLIPSTUDIO,
                                                        Constants.APP_DATA_FUNC_PRESET_ID_SAI,
                                                        Constants.APP_DATA_FUNC_PRESET_ID_ILLUSTRATOR};
#endif
#if false
        // カテゴリ2 プリセットデータ定義
        string[] CATEGORY_PRESET_LIST = new string[] { RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_SAI,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_LIGHTROOM,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_MULTIMEDIA};
        byte[] CATEGORY_PRESET_ID_LIST = new byte[] { Constants.APP_DATA_FUNC_PRESET_ID_PHOTOSHOP,
                                                        Constants.APP_DATA_FUNC_PRESET_ID_CLIPSTUDIO,
                                                        Constants.APP_DATA_FUNC_PRESET_ID_SAI,
                                                        Constants.APP_DATA_FUNC_PRESET_ID_ILLUSTRATOR,
                                                        Constants.APP_DATA_FUNC_PRESET_ID_LIGHTROOM,
                                                        Constants.APP_DATA_FUNC_PRESET_ID_MULTIMEDIA};
#endif

        // プリセットデータ定義
        // PhtoShop
        string[] PRESET_PHOTOSHOP_LIST = new string[] { RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_01_SPACE,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_02_CTRL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_03_ERASER,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_04_SHIFT,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_05_PEN,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_06_BRUSH,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_07_SYRINGE,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_08_PALM,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_09_COPY_STAMP_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_10_SYRINGE_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_11_CROP_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_12_FILL_GRADATION_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_13_ROTATIONG_VIEW_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_14_DODGE_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_15_DRAWING_COLOR_BACKGROUND_COLOR,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_16_MOVE_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_17_SELECTION_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_18_REPAIR_BRUSH_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_19_HISTORY_BRUSH_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_20_CHARACTER_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_21_PATH_COMPONENT_SELECTION_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_22_RECTANGLE_ROUNDED_RECTANGLE_OVAL_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_23_ZOOM_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_24_TONE_CURVE,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_25_LEVEL_CORRECTION,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_26_AUTO_SELECTION,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_27_GROUP_LAYERS,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_28_PALETTE_DISPLAY_OFF,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_29_CREATE_NEW_LAYER,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_30_SELECT_ALL_IN_THE_LAYER,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_31_LAYER_INTEGRATION,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_32_SHOW_HIDE_RULER,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_33_SWITCHING_COLOR_MODE,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_34_CANCEL_SELECTION,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_35_FREE_DEFORMATION_MODE,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_36_UNDO_ONE_TASK,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_37_UNDO_ONE_MORE_TASK,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_38_SAVE,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_39_RESTORE_DISPLAY_TO_NORMAL_SIZE,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_40_LAYER_COMBINING_ALL_DISPLAY_LAYERS,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_41_SHOW_HIDE_GRID,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_42_TAB_SWITCHING,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_43_ESC,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_44_INVERT_SELECTION,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_45_HIDE_SELECTION,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_PHOTOSHOP_46_SHOW_OPTIONS};
        byte[] PRESET_PHOTOSHOP_ID_LIST = new byte[] { Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_SPACE,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_CTRL,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_ERASER,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_SHIFT,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_PEN,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_BRUSH,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_SYRINGE,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_PALM,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_COPY_STAMP_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_SYRINGE_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_CROP_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_FILL_GRADATION_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_ROTATIONG_VIEW_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_DODGE_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_DRAWING_COLOR_BACKGROUND_COLOR,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_MOVE_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_SELECTION_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_REPAIR_BRUSH_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_HISTORY_BRUSH_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_CHARACTER_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_PATH_COMPONENT_SELECTION_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_RECTANGLE_ROUNDED_RECTANGLE_OVAL_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_ZOOM_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_TONE_CURVE,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_LEVEL_CORRECTION,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_AUTO_SELECTION,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_GROUP_LAYERS,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_PALETTE_DISPLAY_OFF,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_CREATE_NEW_LAYER,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_SELECT_ALL_IN_THE_LAYER,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_LAYER_INTEGRATION,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_SHOW_HIDE_RULER,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_SWITCHING_COLOR_MODE,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_CANCEL_SELECTION,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_FREE_DEFORMATION_MODE,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_UNDO_ONE_TASK,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_UNDO_ONE_MORE_TASK,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_SAVE,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_RESTORE_DISPLAY_TO_NORMAL_SIZE,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_LAYER_COMBINING_ALL_DISPLAY_LAYERS,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_SHOW_HIDE_GRID,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_TAB_SWITCHING,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_ESC,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_INVERT_SELECTION,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_HIDE_SELECTION,
                                                        Constants.APP_DATA_FUNC_PRESET_PHOTOSHOP_ID_SHOW_OPTIONS};
        // ClipStudio
        string[] PRESET_CLIPSTUDIO_LIST = new string[] { RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_01_SWITCH_PRE_SUB_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_02_SWITCH_NEXT_SUB_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_03_PALM,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_04_ROTATION,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_05_MAGNIFYING_GLASS_LARGE,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_06_MAGNIFYING_GLASS_SHRINK,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_07_MAIN_COLOR_SUB_COLOR,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_08_DRAWING_COLOR_TRANSPARENT_COLOR,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_09_LAYER_SELECTION,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_10_MAGNIFYING_GLASS,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_11_MOVE_PALM,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_12_MOVE_ROTATION,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_13_OPERATION_OBJECT,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_14_OPERATION_LAYER_SELECTION,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_15_LIGHT_TABLE_TIMELINE_EDIT,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_16_MOVE_LAYER,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_17_SELECTION_RANGE,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_18_AUTO_SELECTION,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_19_SYRINGE,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_20_PEN_PENCIL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_21_BRUSH_AIRBRUSH,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_22_ERASER,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_23_COLOR_MIX,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_24_FILL_GRADATION,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_25_FIGURE_FRAME_RULER,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_26_TEXT_BALLOON,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_27_LINE_CORRECTION,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_28_SYRINGE,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_29_OBJECT,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_30_DRAW_STRAIGHT_LINE,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_31_SWITCHING_LAYER_COLOR,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_32_CANCEL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_33_CUT,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_34_PASTE,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_35_COPY,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_CLIPSTUDIO_36_ESC};
        byte[] PRESET_CLIPSTUDIO_ID_LIST = new byte[] { Constants.APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_SWITCH_PRE_SUB_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_SWITCH_NEXT_SUB_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_PALM,
                                                        Constants.APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_ROTATION,
                                                        Constants.APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_MAGNIFYING_GLASS_LARGE,
                                                        Constants.APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_MAGNIFYING_GLASS_SHRINK,
                                                        Constants.APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_MAIN_COLOR_SUB_COLOR,
                                                        Constants.APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_DRAWING_COLOR_TRANSPARENT_COLOR,
                                                        Constants.APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_LAYER_SELECTION,
                                                        Constants.APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_MAGNIFYING_GLASS,
                                                        Constants.APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_MOVE_PALM,
                                                        Constants.APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_MOVE_ROTATION,
                                                        Constants.APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_OPERATION_OBJECT,
                                                        Constants.APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_OPERATION_LAYER_SELECTION,
                                                        Constants.APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_LIGHT_TABLE_TIMELINE_EDIT,
                                                        Constants.APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_MOVE_LAYER,
                                                        Constants.APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_SELECTION_RANGE,
                                                        Constants.APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_AUTO_SELECTION,
                                                        Constants.APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_SYRINGE,
                                                        Constants.APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_PEN_PENCIL,
                                                        Constants.APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_BRUSH_AIRBRUSH,
                                                        Constants.APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_ERASER,
                                                        Constants.APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_COLOR_MIX,
                                                        Constants.APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_FILL_GRADATION,
                                                        Constants.APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_FIGURE_FRAME_RULER,
                                                        Constants.APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_TEXT_BALLOON,
                                                        Constants.APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_LINE_CORRECTION,
                                                        Constants.APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_SYRINGE2,
                                                        Constants.APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_OBJECT,
                                                        Constants.APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_DRAW_STRAIGHT_LINE,
                                                        Constants.APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_SWITCHING_LAYER_COLOR,
                                                        Constants.APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_CANCEL,
                                                        Constants.APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_CUT,
                                                        Constants.APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_PASTE,
                                                        Constants.APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_COPY,
                                                        Constants.APP_DATA_FUNC_PRESET_CLIPSTUDIO_ID_ESC};
        // SAI
        string[] PRESET_SAI_LIST = new string[] { RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_SAI_01_RESTORE,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_SAI_02_REDOING,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_SAI_03_CUT,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_SAI_04_COPY,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_SAI_05_PASTE,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_SAI_06_CLEAR_LAYER,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_SAI_07_COMBINE_LOWER_LAYER,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_SAI_08_TRANSFER_TO_LOWER_LAYER,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_SAI_09_FILL_LAYER,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_SAI_10_FREE_DEFORMATION,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_SAI_11_CANCEL_SELECTION_AREA,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_SAI_12_DISPLAY_SELECTION_AREA,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_SAI_13_SELECT_ALL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_SAI_14_HUE_SATURATION_LIGHTNESS,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_SAI_15_FLIP_HORIZONTAL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_SAI_16_PIXEL_EQUAL_MAGNIFICATION,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_SAI_17_POSITION_RESET,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_SAI_18_ROTATION_RESET,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_SAI_19_HIDE_CONTROL_PANEL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_SAI_20_DRAW_COLOR_SECONDARY_COLOR,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_SAI_21_DRAW_COLOR_TRANSPARENT_COLOR,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_SAI_22_PENCIL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_SAI_23_AIRBRUSH,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_SAI_24_BRUSH,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_SAI_25_WATERCOLOR_BRUSH,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_SAI_26_ERASER,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_SAI_27_SELECTOR_PEN,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_SAI_28_SELECTIVE_ERASE,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_SAI_29_NEW_CANVAS,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_SAI_30_ESC,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_SAI_31_CTRL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_SAI_32_SPACE,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_SAI_33_SHIFT};
        byte[] PRESET_SAI_ID_LIST = new byte[] { Constants.APP_DATA_FUNC_PRESET_SAI_ID_RESTORE,
                                                        Constants.APP_DATA_FUNC_PRESET_SAI_ID_REDOING,
                                                        Constants.APP_DATA_FUNC_PRESET_SAI_ID_CUT,
                                                        Constants.APP_DATA_FUNC_PRESET_SAI_ID_COPY,
                                                        Constants.APP_DATA_FUNC_PRESET_SAI_ID_PASTE,
                                                        Constants.APP_DATA_FUNC_PRESET_SAI_ID_CLEAR_LAYER,
                                                        Constants.APP_DATA_FUNC_PRESET_SAI_ID_COMBINE_LOWER_LAYER,
                                                        Constants.APP_DATA_FUNC_PRESET_SAI_ID_TRANSFER_TO_LOWER_LAYER,
                                                        Constants.APP_DATA_FUNC_PRESET_SAI_ID_FILL_LAYER,
                                                        Constants.APP_DATA_FUNC_PRESET_SAI_ID_FREE_DEFORMATION,
                                                        Constants.APP_DATA_FUNC_PRESET_SAI_ID_CANCEL_SELECTION_AREA,
                                                        Constants.APP_DATA_FUNC_PRESET_SAI_ID_DISPLAY_SELECTION_AREA,
                                                        Constants.APP_DATA_FUNC_PRESET_SAI_ID_SELECT_ALL,
                                                        Constants.APP_DATA_FUNC_PRESET_SAI_ID_HUE_SATURATION_LIGHTNESS,
                                                        Constants.APP_DATA_FUNC_PRESET_SAI_ID_FLIP_HORIZONTAL,
                                                        Constants.APP_DATA_FUNC_PRESET_SAI_ID_PIXEL_EQUAL_MAGNIFICATION,
                                                        Constants.APP_DATA_FUNC_PRESET_SAI_ID_POSITION_RESET,
                                                        Constants.APP_DATA_FUNC_PRESET_SAI_ID_ROTATION_RESET,
                                                        Constants.APP_DATA_FUNC_PRESET_SAI_ID_HIDE_CONTROL_PANEL,
                                                        Constants.APP_DATA_FUNC_PRESET_SAI_ID_DRAW_COLOR_SECONDARY_COLOR,
                                                        Constants.APP_DATA_FUNC_PRESET_SAI_ID_DRAW_COLOR_TRANSPARENT_COLOR,
                                                        Constants.APP_DATA_FUNC_PRESET_SAI_ID_PENCIL,
                                                        Constants.APP_DATA_FUNC_PRESET_SAI_ID_AIRBRUSH,
                                                        Constants.APP_DATA_FUNC_PRESET_SAI_ID_BRUSH,
                                                        Constants.APP_DATA_FUNC_PRESET_SAI_ID_WATERCOLOR_BRUSH,
                                                        Constants.APP_DATA_FUNC_PRESET_SAI_ID_ERASER,
                                                        Constants.APP_DATA_FUNC_PRESET_SAI_ID_SELECTOR_PEN,
                                                        Constants.APP_DATA_FUNC_PRESET_SAI_ID_SELECTIVE_ERASE,
                                                        Constants.APP_DATA_FUNC_PRESET_SAI_ID_NEW_CANVAS,
                                                        Constants.APP_DATA_FUNC_PRESET_SAI_ID_ESC,
                                                        Constants.APP_DATA_FUNC_PRESET_SAI_ID_CTRL,
                                                        Constants.APP_DATA_FUNC_PRESET_SAI_ID_SPACE,
                                                        Constants.APP_DATA_FUNC_PRESET_SAI_ID_SHIFT};
        // ILLUSTRATOR
        string[] PRESET_ILLUSTRATOR_LIST = new string[] { RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_01_CANCEL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_02_REDOING,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_03_CUT,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_04_COPY,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_05_PASTE,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_06_PASTE_FRONT,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_07_PASTE_BACK,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_08_PASTE_SAME_POSITION,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_09_PASTE_ARTBOARD,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_10_SPELL_CHECK,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_11_OPEN_COLOR_SETTING_DIALOG_BOX,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_12_OPEN_KEYBOARD_SHORTCUT_DIALOG_BOX,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_13_OPEN_PREFERENCES_DIALOG_BOX,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_14_ARTBOARD_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_15_SELECTION_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_16_DIRECT_SELECTION_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_17_AUTOMATIC_SELECTION_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_18_LASSO_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_19_PEN_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_20_CURVED_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_21_FILL_BRUSH_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_22_ADD_ANCHOR_POINT,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_23_DELETE_ANCHOR_POINT,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_24_SWITCH_ANCHOR_POINT,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_25_CHARACTER_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_26_CHARACTER_TOUCH_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_27_LINE_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_28_RECTANGULAR_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_29_OVAL_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_30_BRUSH_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_31_PENCIL_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_32_SHAPER_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_33_ROTATING_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_34_REFLECT_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_35_SCALE_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_36_WARP_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_37_LINE_WIDTH_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_38_FREE_TRANSFORM_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_39_SHAPE_FORMING_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_40_PERSPECTIVE_GRID_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_41_PERSPECTIVE_SELECTION_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_42_SYMBOL_SPRAY_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_43_BAR_GRAPH_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_44_MESH_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_45_GRADATION_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_46_SYRINGE_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_47_BLEND_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_48_LIVE_PAINT_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_49_LIVE_PAINT_SELECTION_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_50_SLICE_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_51_ERASER_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_52_SCISSOR_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_53_HAND_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_54_ZOOM_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_55_SWITCH_SMOOTH_TOOL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_56_ESC,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_57_CTRL,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_58_HAND_TOOL_SPACE,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_ILLUSTRATOR_59_SHIFT};
        byte[] PRESET_ILLUSTRATOR_ID_LIST = new byte[] { Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_CANCEL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_REDOING,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_CUT,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_COPY,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_PASTE,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_PASTE_FRONT,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_PASTE_BACK,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_PASTE_SAME_POSITION,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_PASTE_ARTBOARD,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_SPELL_CHECK,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_OPEN_COLOR_SETTING_DIALOG_BOX,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_OPEN_KEYBOARD_SHORTCUT_DIALOG_BOX,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_OPEN_PREFERENCES_DIALOG_BOX,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_ARTBOARD_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_SELECTION_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_DIRECT_SELECTION_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_AUTOMATIC_SELECTION_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_LASSO_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_PEN_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_CURVED_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_FILL_BRUSH_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_ADD_ANCHOR_POINT,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_DELETE_ANCHOR_POINT,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_SWITCH_ANCHOR_POINT,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_CHARACTER_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_CHARACTER_TOUCH_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_LINE_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_RECTANGULAR_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_OVAL_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_BRUSH_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_PENCIL_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_SHAPER_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_ROTATING_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_REFLECT_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_SCALE_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_WARP_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_LINE_WIDTH_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_FREE_TRANSFORM_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_SHAPE_FORMING_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_PERSPECTIVE_GRID_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_PERSPECTIVE_SELECTION_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_SYMBOL_SPRAY_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_BAR_GRAPH_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_MESH_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_GRADATION_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_SYRINGE_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_BLEND_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_LIVE_PAINT_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_LIVE_PAINT_SELECTION_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_SLICE_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_ERASER_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_SCISSOR_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_HAND_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_ZOOM_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_SWITCH_SMOOTH_TOOL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_ESC,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_CTRL,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_HAND_TOOL_SPACE,
                                                        Constants.APP_DATA_FUNC_PRESET_ILLUSTRATOR_ID_SHIFT};
        // LIGHTROOM
        string[] PRESET_LIGHTROOM_LIST = new string[] { RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_LIGHTROOM_01_,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_LIGHTROOM_01_};
        byte[] PRESET_LIGHTROOM_ID_LIST = new byte[] { Constants.APP_DATA_FUNC_PRESET_LIGHTROOM_ID_,
                                                        Constants.APP_DATA_FUNC_PRESET_LIGHTROOM_ID_};
        // MULTIMEDIA
        string[] PRESET_MULTIMEDIA_LIST = new string[] { RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_MULTIMEDIA_01_,
                                                        RevOmate.Properties.Resources.FUNC_SETTING_CATEGOLY_PRESET_MULTIMEDIA_01_};
        byte[] PRESET_MULTIMEDIA_ID_LIST = new byte[] { Constants.APP_DATA_FUNC_PRESET_MULTIMEDIA_ID_,
                                                        Constants.APP_DATA_FUNC_PRESET_MULTIMEDIA_ID_};

        public FuncSetting()
        {
            InitializeComponent();
        }
        public FuncSetting(Form1 p_fm, int p_mode, int p_sw_id, bool p_advance_flag, byte[] p_app_select_data, byte[] p_app_data, string[] p_sp_func_type_list, byte[] p_sp_func_type_no_list, Form1.STR_SCRIPT_INFO_DATAS p_script_info_datas, Form1.STR_BASE_MODE_INFOS p_base_mode_infos, Form1.STR_FUNC_DATAS p_func_datas, Form1.STR_SW_FUNC_DATAS p_sw_func_datas)
        {
            my_form1 = p_fm;
            select_mode = (byte)(p_mode & 0xFF);
            sw_id = (byte)(p_sw_id & 0xFF);
            category1_id_now_set_val = p_app_select_data[Constants.APP_SW_DATA_SELECT_DATA_CATEGORY1_IDX];
            category2_id_now_set_val = p_app_select_data[Constants.APP_SW_DATA_SELECT_DATA_CATEGORY2_IDX];
            func_list_now_set_val = p_app_select_data[Constants.APP_SW_DATA_SELECT_DATA_FUNC_LIST_IDX];
            category1_id_select_val = category1_id_now_set_val;
            category2_id_select_val = category2_id_now_set_val;
            func_list_select_val = func_list_now_set_val;
            advance_mode_flag = p_advance_flag;
            app_select_data = new byte[p_app_select_data.Length];
            for (int fi = 0; fi < p_app_select_data.Length; fi++)
            {
                app_select_data[fi] = p_app_select_data[fi];
            }
            app_data = new byte[p_app_data.Length];
            for (int fi = 0; fi < p_app_data.Length; fi++)
            {
                app_data[fi] = p_app_data[fi];
            }
            sp_func_type_list = new string[p_sp_func_type_list.Length];
            for (int fi = 0; fi < p_sp_func_type_list.Length; fi++)
            {
                sp_func_type_list[fi] = p_sp_func_type_list[fi];
            }
            sp_func_type_no_list = new byte[p_sp_func_type_no_list.Length];
            for (int fi = 0; fi < p_sp_func_type_no_list.Length; fi++)
            {
                sp_func_type_no_list[fi] = p_sp_func_type_no_list[fi];
            }
            my_script_info_datas = p_script_info_datas;
            my_base_mode_infos = p_base_mode_infos;
            my_func_datas = p_func_datas;
            my_sw_func_datas = p_sw_func_datas;
            my_set_sw_func_data = new byte[Constants.SW_FUNCTION_DATA_LEN];
            if (select_mode < Constants.MODE_NUM && sw_id < Constants.BUTTON_NUM)
            {
                for (int fi = 0; fi < Constants.SW_FUNCTION_DATA_LEN; fi++)
                {
                    my_set_sw_func_data[fi] = my_sw_func_datas.sw_func_datas[select_mode].sw_func_data[sw_id].sw_data[fi];
                }
            }

            InitializeComponent();
        }

        private void FuncSetting_Load(object sender, EventArgs e)
        {
            try
            {
#if DEBUG
        // デバッグ時
                lbl_debug01.Text = "mode=" + select_mode.ToString() + ", sw_no=" + sw_id.ToString() + ", advance_flag=" + advance_mode_flag.ToString();
#else
        // リリース時
                lbl_debug01.Visible = false;
#endif

                this.Text = RevOmate.Properties.Resources.SCREEN_FUNCTION_SETTING;
                lbl_setting_value_title.Text = RevOmate.Properties.Resources.SETTING_VALUE;
                lbl_setting_value.Text = my_Get_Setting_Name(category1_id_now_set_val, category2_id_now_set_val, func_list_now_set_val, my_set_sw_func_data);
                lbl_title_category.Text = RevOmate.Properties.Resources.FUNC_SETTING_CATEGORY1_LIST;
                lbl_title_category2.Text = RevOmate.Properties.Resources.FUNC_SETTING_CATEGORY2_LIST;
                lbl_title_func.Text = RevOmate.Properties.Resources.FUNC_SETTING_FUNC_LIST;
                btn_submit.Text = RevOmate.Properties.Resources.BTN_SUBMIT;
                btn_cancel.Text = RevOmate.Properties.Resources.BTN_CANCEL;
                lbl_default_func_title.Text = RevOmate.Properties.Resources.FUNC_SETTING_ENCODER_DEFAULT_SETTING_TITLE;
                llbl_help.Text = RevOmate.Properties.Resources.HELP_QUESTION_MARK;

                txtbx_sw_func_key.Text = RevOmate.Properties.Resources.SETTING_KEY_INPUT_BOX_MSG;
                btn_sw_func_key_clr.Text = RevOmate.Properties.Resources.SETTING_KEY_INPUT_CLR_BUTTON;
                chk_sw_func_ctrl.Text = RevOmate.Properties.Resources.SETTING_KEY_CTRL_KEY_CTRL;
                chk_sw_func_shift.Text = RevOmate.Properties.Resources.SETTING_KEY_CTRL_KEY_SHIFT;
                chk_sw_func_alt.Text = RevOmate.Properties.Resources.SETTING_KEY_CTRL_KEY_ALT;
                chk_sw_func_win.Text = RevOmate.Properties.Resources.SETTING_KEY_CTRL_KEY_WIN;

                my_keyboard_modifier = new CheckBox[] { chk_sw_func_ctrl, chk_sw_func_shift, chk_sw_func_alt, chk_sw_func_win };

                // エンコーダデフォルト機能
                if (sw_id == Constants.ENCODER_BUTTON_ID)
                {
                    gbx_encoder_default_func.Visible = true;
                    lbl_default_func_title.Visible = true;
                    cmbbx_encoder_default_func.Visible = true;

                    string temp_func_name = "";
                    for (int fi = 0; fi < Constants.FUNCTION_NUM; fi++)
                    {
                        temp_func_name = string.Format("{0}{1:0}:", RevOmate.Properties.Resources.FUNC_SETTING_ENCODER_DEFAULT_SETTING_FUNC, fi + 1);
                        if (my_func_datas.func_datas[select_mode].func_data[fi].func_name != "")
                        {
                            temp_func_name += my_func_datas.func_datas[select_mode].func_data[fi].func_name;
                        }
                        else
                        {
                            temp_func_name += RevOmate.Properties.Resources.FUNCTION_NAME_UNDEFINE;
                        }
                        cmbbx_encoder_default_func.Items.Add(temp_func_name);
                    }
                    //設定されているマクロ番号を選択する
                    int sel_idx = my_base_mode_infos.base_mode_infos[select_mode].encoder_func_no;
                    if (sel_idx < 0 || cmbbx_encoder_default_func.Items.Count <= sel_idx)
                    {   // エラー（未設定）
                        sel_idx = 0;
                    }
                    cmbbx_encoder_default_func.SelectedIndex = sel_idx;
                    encoder_default_now_set_val = (byte)(sel_idx & 0xFF);
                    encoder_default_select_val = (byte)(sel_idx & 0xFF);
                }

                my_create_category_id_list();
                my_category_disp(advance_mode_flag, category1_id_select_val);
                my_category2_disp(advance_mode_flag, category1_id_select_val, category2_id_select_val);
                my_func_list_disp(advance_mode_flag, category1_id_select_val, category2_id_select_val, func_list_select_val);
            }
            catch
            {
            }
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            bool sw_func_data_change_flag = false;
            try
            {


                if (MessageBox.Show(RevOmate.Properties.Resources.DEVICE_BASE_BUTTON_SETUP_WRITE_CONFIRM_MSG, RevOmate.Properties.Resources.APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {

                    my_function_setting_get_by_disp(select_mode, sw_id);
                    // SW機能データ変更チェック
                    for (int fi = 0; fi < Constants.SW_FUNCTION_DATA_LEN; fi++)
                    {
                        if (my_set_sw_func_data[fi] != my_sw_func_datas.sw_func_datas[select_mode].sw_func_data[sw_id].sw_data[fi])
                        {
                            sw_func_data_change_flag = true;
                            break;
                        }
                    }
                    // エンコーダーデフォルト機能
                    if (sw_id == Constants.ENCODER_BUTTON_ID)
                    {
                        if (encoder_default_select_val != encoder_default_now_set_val)
                        {
                            sw_func_data_change_flag = true;
                        }
                    }

                    if (category1_id_now_set_val != category1_id_select_val
                        || category2_id_now_set_val != category2_id_select_val
                        || func_list_now_set_val != func_list_select_val
                        || sw_func_data_change_flag == true)
                    {   // 変更ありのため書き換え実施

                        bool rewrite_exe = false;
                        // プロフィール変更機能の割り当てチェック
                        bool bool_tmp = my_check_profile_change_function_assign(select_mode, sw_id, category1_id_select_val, func_list_select_val);
                        if (bool_tmp == false)
                        {   // 未割り当て
                            // 未割り当ての場合は確認メッセージを表示して確認する
                            string msg_str = RevOmate.Properties.Resources.PROFILE_CHANGE_NOT_SETTING_WARNING_MSG1 + "\n" + RevOmate.Properties.Resources.PROFILE_CHANGE_NOT_SETTING_WARNING_MSG2;
                            if (MessageBox.Show(msg_str, RevOmate.Properties.Resources.APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                            {   // そのまま書き込み
                                rewrite_exe = true;
                            }
                        }
                        else
                        {   // 割り当て済み
                            rewrite_exe = true;
                        }

                        if (rewrite_exe == true)
                        {   // 書き換え実行
                            if (category1_id_select_val != Constants.APP_DATA_FUNC_CATEGORY_ID_PRESET)
                            {   // カテゴリがプリセット設定以外

                                // ボタン設定書き換え要求
                                my_form1.my_Button_Setting_Write_req_by_func_setting(select_mode, sw_id, encoder_default_select_val, category1_id_select_val, category2_id_select_val, func_list_select_val, my_set_sw_func_data);

                            }
                            else if (category1_id_select_val == Constants.APP_DATA_FUNC_CATEGORY_ID_PRESET)
                            {   // カテゴリがプリセット設定
                                // プリセットマクロファイル
                                // ボタン設定書き換え要求
                                my_form1.my_Button_Setting_Write_req_by_func_setting(select_mode, sw_id, encoder_default_select_val, category1_id_select_val, category2_id_select_val, func_list_select_val, my_set_sw_func_data);
                                // マクロ書き換え要求
                                my_form1.my_Preset_Write_req_by_func_setting(select_mode, sw_id, category1_id_select_val, category2_id_select_val, func_list_select_val);
                            }
                        }
                    }

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
        private void my_create_category_id_list()
        {
            try
            {
                category_id = new byte[CATEGORY_ID_LIST.Length];
                for (int fi = 0; fi < CATEGORY_ID_LIST.Length; fi++)
                {
                    category_id[fi] = CATEGORY_ID_LIST[fi];
                }
            }
            catch
            {
            }
        }
        private void my_create_category2_id_list(byte p_category_id)
        {
            try
            {
                switch (p_category_id)
                {
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_PRESET:
                        category2_id = new byte[CATEGORY_PRESET_ID_LIST.Length];
                        for (int fi = 0; fi < CATEGORY_PRESET_ID_LIST.Length; fi++)
                        {
                            category2_id[fi] = CATEGORY_PRESET_ID_LIST[fi];
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
        private void my_create_func_id_list(byte p_category_id, byte p_category2_id)
        {
            try
            {
                switch (p_category_id)
                {
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_NOT_SET:
                        // IDセット
                        func_list_id = new byte[1];
                        func_list_id[0] = 0;
                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_GENERAL:
                        // IDセット
                        func_list_id = new byte[sp_func_type_list.Length + 1];
                        for (int fi = 0; fi < func_list_id.Length; fi++)
                        {
                            func_list_id[fi] = (byte)(fi & 0xFF);
                        }
                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_MACRO:
                        // IDセット
                        func_list_id = new byte[Constants.SCRIPT_USER_USE_NUM];
                        for (int fi = 0; fi < func_list_id.Length; fi++)
                        {
                            func_list_id[fi] = (byte)(fi & 0xFF);
                        }
                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_PRESET:

                        switch (p_category2_id)
                        {
                            case Constants.APP_DATA_FUNC_PRESET_ID_PHOTOSHOP:
                                func_list_id = new byte[PRESET_PHOTOSHOP_ID_LIST.Length];
                                for (int fi = 0; fi < PRESET_PHOTOSHOP_LIST.Length && fi < PRESET_PHOTOSHOP_ID_LIST.Length; fi++)
                                {
                                    func_list_id[fi] = PRESET_PHOTOSHOP_ID_LIST[fi];
                                }
                                break;
                            case Constants.APP_DATA_FUNC_PRESET_ID_CLIPSTUDIO:
                                func_list_id = new byte[PRESET_CLIPSTUDIO_ID_LIST.Length];
                                for (int fi = 0; fi < PRESET_CLIPSTUDIO_LIST.Length && fi < PRESET_CLIPSTUDIO_ID_LIST.Length; fi++)
                                {
                                    func_list_id[fi] = PRESET_CLIPSTUDIO_ID_LIST[fi];
                                }
                                break;
                            case Constants.APP_DATA_FUNC_PRESET_ID_SAI:
                                func_list_id = new byte[PRESET_SAI_ID_LIST.Length];
                                for (int fi = 0; fi < PRESET_SAI_LIST.Length && fi < PRESET_SAI_ID_LIST.Length; fi++)
                                {
                                    func_list_id[fi] = PRESET_SAI_ID_LIST[fi];
                                }
                                break;
                            case Constants.APP_DATA_FUNC_PRESET_ID_ILLUSTRATOR:
                                func_list_id = new byte[PRESET_ILLUSTRATOR_ID_LIST.Length];
                                for (int fi = 0; fi < PRESET_ILLUSTRATOR_LIST.Length && fi < PRESET_ILLUSTRATOR_ID_LIST.Length; fi++)
                                {
                                    func_list_id[fi] = PRESET_ILLUSTRATOR_ID_LIST[fi];
                                }
                                break;
                            case Constants.APP_DATA_FUNC_PRESET_ID_LIGHTROOM:
                                func_list_id = new byte[PRESET_LIGHTROOM_ID_LIST.Length];
                                for (int fi = 0; fi < PRESET_LIGHTROOM_LIST.Length && fi < PRESET_LIGHTROOM_ID_LIST.Length; fi++)
                                {
                                    func_list_id[fi] = PRESET_LIGHTROOM_ID_LIST[fi];
                                }
                                break;
                            case Constants.APP_DATA_FUNC_PRESET_ID_MULTIMEDIA:
                                func_list_id = new byte[PRESET_MULTIMEDIA_ID_LIST.Length];
                                for (int fi = 0; fi < PRESET_MULTIMEDIA_LIST.Length && fi < PRESET_MULTIMEDIA_ID_LIST.Length; fi++)
                                {
                                    func_list_id[fi] = PRESET_MULTIMEDIA_ID_LIST[fi];
                                }
                                break;
                            default:
                                break;
                        }

                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_KEY:
                        // IDセット
                        func_list_id = new byte[FUNC_KEY_ID_LIST.Length + 1];
                        for (int fi = 0; fi < FUNC_KEY_ID_LIST.Length; fi++)
                        {
                            func_list_id[fi] = FUNC_KEY_ID_LIST[fi];
                        }
                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_MOUSE:
                        // IDセット
                        func_list_id = new byte[FUNC_MOUSE_ID_LIST.Length + 1];
                        for (int fi = 0; fi < FUNC_MOUSE_ID_LIST.Length; fi++)
                        {
                            func_list_id[fi] = FUNC_MOUSE_ID_LIST[fi];
                        }
                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_GAMEPAD:
                        // IDセット
                        func_list_id = new byte[FUNC_GAMEPAD_ID_LIST.Length + 1];
                        for (int fi = 0; fi < FUNC_GAMEPAD_ID_LIST.Length; fi++)
                        {
                            func_list_id[fi] = FUNC_GAMEPAD_ID_LIST[fi];
                        }
                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_MULTIMEDIA:
                        // IDセット
                        func_list_id = new byte[FUNC_MULTIMEDIA_ID_LIST.Length + 1];
                        for (int fi = 0; fi < FUNC_MULTIMEDIA_ID_LIST.Length; fi++)
                        {
                            func_list_id[fi] = FUNC_MULTIMEDIA_ID_LIST[fi];
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
        private void my_category_disp(bool p_advance_mode_flag, int p_select_category_id)
        {
            int select_idx = 0;
            try
            {
                // カテゴリ表示
                for (int fi = 0; fi < CATEGORY_LIST.Length && fi < CATEGORY_ID_LIST.Length; fi++)
                {
                    lbx_category.Items.Add(CATEGORY_LIST[fi]);
                }

                //
                for (int fi = 0; fi < category_id.Length; fi++)
                {
                    if (category_id[fi] == p_select_category_id)
                    {
                        select_idx = fi;
                        break;
                    }
                }

                if (0 <= select_idx && select_idx < lbx_category.Items.Count)
                {
                    lbx_category.SelectedIndex = select_idx;
                }
                else
                {
                }
            }
            catch
            {
            }
        }

        private void my_category2_disp(bool p_advance_mode_flag, int p_select_category_id, int p_select_category_id2)
        {
            int select_idx = p_select_category_id2;
            try
            {
                lbx_category2.Items.Clear();

                switch (p_select_category_id)
                {
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_PRESET:
                        for (int fi = 0; fi < CATEGORY_PRESET_LIST.Length; fi++)
                        {
                            lbx_category2.Items.Add(CATEGORY_PRESET_LIST[fi]);
                        }
                        break;
                    default:
                        break;
                }

                if (0 <= select_idx && select_idx < lbx_category2.Items.Count)
                {
                    lbx_category2.SelectedIndex = select_idx;
                }
                else
                {
                }
            }
            catch
            {
            }
        }

        private void my_func_list_disp(bool p_advance_mode_flag, int p_category_id, int p_category_id2, int p_select_id)
        {
            int select_idx = p_select_id;
            string temp_macro_name = "";
            try
            {
                lbx_func_list.Items.Clear();

                switch (p_category_id)
                {
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_NOT_SET:
                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_GENERAL:
                        //// 先頭に未設定を追加
                        //temp_macro_name = "";
                        ////temp_macro_name += string.Format("{0:000}:", 0);
                        //temp_macro_name += RevOmate.Properties.Resources.MACRO_NAME_UNSETTING;
                        //lbx_func_list.Items.Add(temp_macro_name);

                        // 特殊機能追加
                        for (int fi = 0; fi < sp_func_type_list.Length; fi++)
                        {
                            lbx_func_list.Items.Add(sp_func_type_list[fi]);
                        }
                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_MACRO:
                        // 先頭に未設定を追加
                        //temp_macro_name = "";
                        ////temp_macro_name += string.Format("{0:000}:", 0);
                        //temp_macro_name += RevOmate.Properties.Resources.MACRO_NAME_UNSETTING;
                        //lbx_func_list.Items.Add(temp_macro_name);

                        // スクリプトリスト追加
                        //for (int fj = 0; fj < my_script_info_datas.Script_Info_Datas.Length; fj++)
                        for (int fj = 0; fj < Constants.SCRIPT_USER_USE_NUM; fj++)
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
                            lbx_func_list.Items.Add(temp_macro_name);
                        }
                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_PRESET:

                        switch (p_category_id2)
                        {
                            case Constants.APP_DATA_FUNC_PRESET_ID_PHOTOSHOP:
                                func_list_id = new byte[PRESET_PHOTOSHOP_ID_LIST.Length];
                                for (int fi = 0; fi < PRESET_PHOTOSHOP_LIST.Length && fi < PRESET_PHOTOSHOP_ID_LIST.Length; fi++)
                                {
                                    lbx_func_list.Items.Add(PRESET_PHOTOSHOP_LIST[fi]);
                                    func_list_id[fi] = PRESET_PHOTOSHOP_ID_LIST[fi];
                                }
                                break;
                            case Constants.APP_DATA_FUNC_PRESET_ID_CLIPSTUDIO:
                                func_list_id = new byte[PRESET_CLIPSTUDIO_ID_LIST.Length];
                                for (int fi = 0; fi < PRESET_CLIPSTUDIO_LIST.Length && fi < PRESET_CLIPSTUDIO_ID_LIST.Length; fi++)
                                {
                                    lbx_func_list.Items.Add(PRESET_CLIPSTUDIO_LIST[fi]);
                                    func_list_id[fi] = PRESET_CLIPSTUDIO_ID_LIST[fi];
                                }
                                break;
                            case Constants.APP_DATA_FUNC_PRESET_ID_SAI:
                                func_list_id = new byte[PRESET_SAI_ID_LIST.Length];
                                for (int fi = 0; fi < PRESET_SAI_LIST.Length && fi < PRESET_SAI_ID_LIST.Length; fi++)
                                {
                                    lbx_func_list.Items.Add(PRESET_SAI_LIST[fi]);
                                    func_list_id[fi] = PRESET_SAI_ID_LIST[fi];
                                }
                                break;
                            case Constants.APP_DATA_FUNC_PRESET_ID_ILLUSTRATOR:
                                func_list_id = new byte[PRESET_ILLUSTRATOR_ID_LIST.Length];
                                for (int fi = 0; fi < PRESET_ILLUSTRATOR_LIST.Length && fi < PRESET_ILLUSTRATOR_ID_LIST.Length; fi++)
                                {
                                    lbx_func_list.Items.Add(PRESET_ILLUSTRATOR_LIST[fi]);
                                    func_list_id[fi] = PRESET_ILLUSTRATOR_ID_LIST[fi];
                                }
                                break;
                            case Constants.APP_DATA_FUNC_PRESET_ID_LIGHTROOM:
                                func_list_id = new byte[PRESET_LIGHTROOM_ID_LIST.Length];
                                for (int fi = 0; fi < PRESET_LIGHTROOM_LIST.Length && fi < PRESET_LIGHTROOM_ID_LIST.Length; fi++)
                                {
                                    lbx_func_list.Items.Add(PRESET_LIGHTROOM_LIST[fi]);
                                    func_list_id[fi] = PRESET_LIGHTROOM_ID_LIST[fi];
                                }
                                break;
                            case Constants.APP_DATA_FUNC_PRESET_ID_MULTIMEDIA:
                                func_list_id = new byte[PRESET_MULTIMEDIA_ID_LIST.Length];
                                for (int fi = 0; fi < PRESET_MULTIMEDIA_LIST.Length && fi < PRESET_MULTIMEDIA_ID_LIST.Length; fi++)
                                {
                                    lbx_func_list.Items.Add(PRESET_MULTIMEDIA_LIST[fi]);
                                    func_list_id[fi] = PRESET_MULTIMEDIA_ID_LIST[fi];
                                }
                                break;
                            default:
                                break;
                        }

                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_KEY:
                        for (int fi = 0; fi < FUNC_KEY_LIST.Length; fi++)
                        {
                            lbx_func_list.Items.Add(FUNC_KEY_LIST[fi]);
                        }
                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_MOUSE:
                        for (int fi = 0; fi < FUNC_MOUSE_LIST.Length; fi++)
                        {
                            lbx_func_list.Items.Add(FUNC_MOUSE_LIST[fi]);
                        }
                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_GAMEPAD:
                        for (int fi = 0; fi < FUNC_GAMEPAD_LIST.Length; fi++)
                        {
                            lbx_func_list.Items.Add(FUNC_GAMEPAD_LIST[fi]);
                        }
                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_MULTIMEDIA:
                        for (int fi = 0; fi < FUNC_MULTIMEDIA_LIST.Length; fi++)
                        {
                            lbx_func_list.Items.Add(FUNC_MULTIMEDIA_LIST[fi]);
                        }
                        break;
                    default:
                        break;
                }
                
                if (0 <= select_idx && select_idx < lbx_func_list.Items.Count)
                {
                    lbx_func_list.SelectedIndex = select_idx;
                }
                else
                {
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
                
                byte tmp_category_id = 0;
                if (0 <= lbx_category.SelectedIndex && lbx_category.SelectedIndex < category_id.Length)
                {
                    tmp_category_id = category_id[lbx_category.SelectedIndex];
                }
                category1_id_select_val = tmp_category_id;
                if (category1_id_now_set_val != category1_id_select_val)
                {
                    category2_id_select_val = 0;
                    func_list_select_val = 0;
                }
                else
                {
                    category2_id_select_val = category2_id_now_set_val;
                    func_list_select_val = func_list_now_set_val;
                }

                my_create_category2_id_list(category1_id_select_val);
                my_create_func_id_list(category1_id_select_val, category2_id_select_val);

                my_category2_disp(advance_mode_flag, tmp_category_id, category2_id_select_val);
                my_func_list_disp(advance_mode_flag, tmp_category_id, category2_id_select_val, func_list_select_val);
#if false
                if (advance_mode_flag == false)
                {   // EASYモード
                    my_func_list_disp(advance_mode_flag, tmp_category_id, 0);
                }
                else
                {   // ADVANCEモード
                    my_func_list_disp(advance_mode_flag, tmp_category_id, 0);
                }
#endif

                my_function_setting_control_disp(category1_id_select_val, 0, true);
            }
            catch
            {
            }
        }

        private void lbx_category2_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                byte tmp_category_id2 = 0;
                if (0 <= lbx_category2.SelectedIndex && lbx_category2.SelectedIndex < category2_id.Length)
                {
                    tmp_category_id2 = category2_id[lbx_category2.SelectedIndex];
                }
                category2_id_select_val = tmp_category_id2;
                if (category2_id_now_set_val != category2_id_select_val)
                {
                    func_list_select_val = 0;
                }
                else
                {
                    if (category1_id_now_set_val == category1_id_select_val)
                    {
                        func_list_select_val = func_list_now_set_val;
                    }
                    else
                    {
                        func_list_select_val = 0;
                    }
                }

                my_create_func_id_list(category1_id_select_val, category2_id_select_val);

                //my_category2_disp(advance_mode_flag, category_id_now_select, tmp_category_id2);
                my_func_list_disp(advance_mode_flag, category1_id_select_val, tmp_category_id2, func_list_select_val);
            }
            catch
            {
            }
        }

        private void lbx_func_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                byte tmp_func_list_id = 0;
                if (0 <= lbx_func_list.SelectedIndex && lbx_func_list.SelectedIndex < func_list_id.Length)
                {
                    tmp_func_list_id = func_list_id[lbx_func_list.SelectedIndex];
                }
                func_list_select_val = tmp_func_list_id;

                my_create_func_id_list(category1_id_select_val, category2_id_select_val);
                my_function_setting_control_disp(category1_id_select_val, func_list_select_val, true);
            }
            catch
            {
            }
        }

        private void my_function_setting_control_disp(int p_category_id, int p_func_select_idx, bool p_visible)
        {
            //byte[] temp_set_sw_func_data = new byte[Constants.SW_FUNCTION_DATA_LEN];
            sbyte tmp_sbyte;
            decimal tmp_dec;
            try
            {
                // SW機能設定データを一時コピー
                //for (int fi = 0; fi < Constants.SW_FUNCTION_DATA_LEN; fi++)
                //{
                //    temp_set_sw_func_data[fi] = my_set_sw_func_data[fi];
                //}

                switch (p_category_id)
                {
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_NOT_SET:
                        // 非表示
                        lbl_sw_func_title.Visible = false;
                        num_sw_func_x.Visible = false;
                        num_sw_func_y.Visible = false;
                        for (int fi = 0; fi < my_keyboard_modifier.Length; fi++)
                        {
                            my_keyboard_modifier[fi].Visible = false;
                        }
                        txtbx_sw_func_key.Visible = false;
                        btn_sw_func_key_clr.Visible = false;
                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_GENERAL:
                        // 非表示
                        lbl_sw_func_title.Visible = false;
                        num_sw_func_x.Visible = false;
                        num_sw_func_y.Visible = false;
                        for (int fi = 0; fi < my_keyboard_modifier.Length; fi++)
                        {
                            my_keyboard_modifier[fi].Visible = false;
                        }
                        txtbx_sw_func_key.Visible = false;
                        btn_sw_func_key_clr.Visible = false;
                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_MACRO:
                        // 非表示
                        lbl_sw_func_title.Visible = false;
                        num_sw_func_x.Visible = false;
                        num_sw_func_y.Visible = false;
                        for (int fi = 0; fi < my_keyboard_modifier.Length; fi++)
                        {
                            my_keyboard_modifier[fi].Visible = false;
                        }
                        txtbx_sw_func_key.Visible = false;
                        btn_sw_func_key_clr.Visible = false;
                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_PRESET:
                        // 非表示
                        lbl_sw_func_title.Visible = false;
                        num_sw_func_x.Visible = false;
                        num_sw_func_y.Visible = false;
                        for (int fi = 0; fi < my_keyboard_modifier.Length; fi++)
                        {
                            my_keyboard_modifier[fi].Visible = false;
                        }
                        txtbx_sw_func_key.Visible = false;
                        btn_sw_func_key_clr.Visible = false;
                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_KEY:
                        // SW機能設定がキーではない場合はデータクリア
                        if (my_set_sw_func_data[Constants.DEVICE_DATA_SET_TYPE_IDX] != Constants.SET_TYPE_KEYBOARD_KEY)
                        {
                            for (int fi = (Constants.DEVICE_DATA_SET_TYPE_IDX + 1); fi < Constants.SW_FUNCTION_DATA_LEN; fi++)
                            {
                                my_set_sw_func_data[fi] = 0;
                            }
                        }
                        switch (p_func_select_idx)
                        {
                            case Constants.APP_DATA_DIAL_FUNC_KEY_ID_NUM_UP:
                            case Constants.APP_DATA_DIAL_FUNC_KEY_ID_NUM_DOWN:
                                // 非表示
                                lbl_sw_func_title.Visible = false;
                                num_sw_func_x.Visible = false;
                                num_sw_func_y.Visible = false;
                                for (int fi = 0; fi < my_keyboard_modifier.Length; fi++)
                                {
                                    my_keyboard_modifier[fi].Visible = false;
                                }
                                txtbx_sw_func_key.Visible = false;
                                btn_sw_func_key_clr.Visible = false;
                                break;
                            case Constants.APP_DATA_DIAL_FUNC_KEY_ID_KEY:
                                // KEY SET表示
                                lbl_sw_func_title.Visible = false;
                                num_sw_func_x.Visible = false;
                                num_sw_func_y.Visible = false;
                                for (int fi = 0; fi < my_keyboard_modifier.Length; fi++)
                                {
                                    my_keyboard_modifier[fi].Visible = p_visible;
                                    if ((my_set_sw_func_data[Constants.DEVICE_DATA_MODIFIER_IDX] & my_form1.keyboard_modifier_bit[fi]) != 0)
                                    {
                                        my_keyboard_modifier[fi].Checked = true;
                                    }
                                    else
                                    {
                                        // 右の制御キービットもチェック
                                        if ((my_keyboard_modifier.Length * 2) == my_form1.keyboard_modifier_bit.Length)
                                        {
                                            if ((my_set_sw_func_data[Constants.DEVICE_DATA_MODIFIER_IDX] & my_form1.keyboard_modifier_bit[my_keyboard_modifier.Length + fi]) != 0)
                                            {
                                                my_keyboard_modifier[fi].Checked = true;
                                            }
                                            else
                                            {
                                                my_keyboard_modifier[fi].Checked = false;
                                            }
                                        }
                                        else
                                        {
                                            my_keyboard_modifier[fi].Checked = false;
                                        }
                                    }
                                }
                                txtbx_sw_func_key.Visible = p_visible;
                                if (my_set_sw_func_data[Constants.DEVICE_DATA_KEY1_IDX] == 0)
                                {
                                    txtbx_sw_func_key.Text = RevOmate.Properties.Resources.KEYBOARD_SET_KEY_EMPTY;
                                }
                                else
                                {
                                    txtbx_sw_func_key.Text = const_Key_Code.Get_KeyCode_Name(my_set_sw_func_data[Constants.DEVICE_DATA_KEY1_IDX], my_form1.system_setting_info.Keyboard_Type);
                                }
                                btn_sw_func_key_clr.Visible = p_visible;
                                break;
                        }
                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_MOUSE:
                        // SW機能設定がマウスではない場合はデータクリア
                        if (my_set_sw_func_data[Constants.DEVICE_DATA_SET_TYPE_IDX] < Constants.SET_TYPE_MOUSE_MIN || Constants.SET_TYPE_MOUSE_MAX < my_set_sw_func_data[Constants.DEVICE_DATA_SET_TYPE_IDX])
                        {
                            for (int fi = (Constants.DEVICE_DATA_SET_TYPE_IDX + 1); fi < Constants.SW_FUNCTION_DATA_LEN; fi++)
                            {
                                my_set_sw_func_data[fi] = 0;
                            }
                        }
                        switch (p_func_select_idx)
                        {
                            case Constants.APP_DATA_DIAL_FUNC_MOUSE_ID_MOVE:
                                // マウス移動
                                lbl_sw_func_title.Text = RevOmate.Properties.Resources.MOUSE_MOVE_TEXT;
                                lbl_sw_func_title.Visible = p_visible;
                                num_sw_func_x.Visible = p_visible;
                                num_sw_func_x.Minimum = Constants.MOUSE_MOVE_MIN;
                                num_sw_func_x.Maximum = Constants.MOUSE_MOVE_MAX;
                                if (my_set_sw_func_data[Constants.DEVICE_DATA_X_MOVE_IDX] > 0x7F)
                                {
                                    tmp_sbyte = (sbyte)(my_set_sw_func_data[Constants.DEVICE_DATA_X_MOVE_IDX] - 0x100);
                                }
                                else
                                {
                                    tmp_sbyte = (sbyte)(my_set_sw_func_data[Constants.DEVICE_DATA_X_MOVE_IDX]);
                                }
                                //tmp_sbyte = (sbyte)(my_set_data.mouse_data[data_idx, Constants.DEVICE_DATA_X_MOVE_IDX]);
                                tmp_dec = tmp_sbyte;
                                num_sw_func_x.Value = tmp_dec;
                                num_sw_func_y.Visible = p_visible;
                                num_sw_func_y.Minimum = Constants.MOUSE_MOVE_MIN;
                                num_sw_func_y.Maximum = Constants.MOUSE_MOVE_MAX;
                                if (my_set_sw_func_data[Constants.DEVICE_DATA_Y_MOVE_IDX] > 0x7F)
                                {
                                    tmp_sbyte = (sbyte)(my_set_sw_func_data[Constants.DEVICE_DATA_Y_MOVE_IDX] - 0x100);
                                }
                                else
                                {
                                    tmp_sbyte = (sbyte)(my_set_sw_func_data[Constants.DEVICE_DATA_Y_MOVE_IDX]);
                                }
                                //tmp_sbyte = (sbyte)(my_set_data.mouse_data[data_idx, Constants.DEVICE_DATA_Y_MOVE_IDX]);
                                tmp_dec = tmp_sbyte;
                                num_sw_func_y.Value = tmp_dec;
                                for (int fi = 0; fi < my_keyboard_modifier.Length; fi++)
                                {
                                    my_keyboard_modifier[fi].Visible = false;
                                }
                                txtbx_sw_func_key.Visible = false;
                                btn_sw_func_key_clr.Visible = false;
                                break;
                            case Constants.APP_DATA_DIAL_FUNC_MOUSE_ID_WH_SCROLL:
                                // ホイールスクロール
                                lbl_sw_func_title.Text = RevOmate.Properties.Resources.MOUSE_SCROLL_TEXT;
                                lbl_sw_func_title.Visible = p_visible;
                                num_sw_func_x.Visible = p_visible;
                                num_sw_func_x.Minimum = Constants.MOUSE_SCROLL_MIN;
                                num_sw_func_x.Maximum = Constants.MOUSE_SCROLL_MAX;
                                if (my_set_sw_func_data[Constants.DEVICE_DATA_WHEEL_IDX] > 0x7F)
                                {
                                    tmp_sbyte = (sbyte)(my_set_sw_func_data[Constants.DEVICE_DATA_WHEEL_IDX] - 0x100);
                                }
                                else
                                {
                                    tmp_sbyte = (sbyte)(my_set_sw_func_data[Constants.DEVICE_DATA_WHEEL_IDX]);
                                }
                                //tmp_sbyte = (sbyte)(my_set_data.mouse_data[data_idx, Constants.DEVICE_DATA_WHEEL_IDX]);
                                tmp_dec = tmp_sbyte;
                                num_sw_func_x.Value = tmp_dec;
                                num_sw_func_y.Visible = false;
                                for (int fi = 0; fi < my_keyboard_modifier.Length; fi++)
                                {
                                    my_keyboard_modifier[fi].Visible = false;
                                }
                                txtbx_sw_func_key.Visible = false;
                                btn_sw_func_key_clr.Visible = false;
                                break;
                            default:
                                // 非表示
                                lbl_sw_func_title.Visible = false;
                                num_sw_func_x.Visible = false;
                                num_sw_func_y.Visible = false;
                                for (int fi = 0; fi < my_keyboard_modifier.Length; fi++)
                                {
                                    my_keyboard_modifier[fi].Visible = false;
                                }
                                txtbx_sw_func_key.Visible = false;
                                btn_sw_func_key_clr.Visible = false;
                                break;
                        }
                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_GAMEPAD:
                        // SW機能設定がゲームパッドではない場合はデータクリア
                        if (my_set_sw_func_data[Constants.DEVICE_DATA_SET_TYPE_IDX] < Constants.SET_TYPE_JOYPAD_MIN || Constants.SET_TYPE_JOYPAD_MAX < my_set_sw_func_data[Constants.DEVICE_DATA_SET_TYPE_IDX])
                        {
                            for (int fi = (Constants.DEVICE_DATA_SET_TYPE_IDX + 1); fi < Constants.SW_FUNCTION_DATA_LEN; fi++)
                            {
                                my_set_sw_func_data[fi] = 0;
                            }
                        }
                        switch (p_func_select_idx)
                        {
                            case Constants.APP_DATA_DIAL_FUNC_GAMEPAD_ID_LEFT_ANALOG:
                            case Constants.APP_DATA_DIAL_FUNC_GAMEPAD_ID_RIGHT_ANALOG:
                                // 左右レバー
                                lbl_sw_func_title.Text = RevOmate.Properties.Resources.JOYPAD_MOVE_TEXT;
                                lbl_sw_func_title.Visible = p_visible;
                                num_sw_func_x.Visible = p_visible;
                                num_sw_func_x.Minimum = Constants.JOYPAD_MOVE_MIN;
                                num_sw_func_x.Maximum = Constants.JOYPAD_MOVE_MAX;
                                if (my_set_sw_func_data[Constants.DEVICE_DATA_JOY_X_MOVE_IDX] > 0x7F)
                                {
                                    tmp_sbyte = (sbyte)(my_set_sw_func_data[Constants.DEVICE_DATA_JOY_X_MOVE_IDX] - 0x100);
                                }
                                else
                                {
                                    tmp_sbyte = (sbyte)(my_set_sw_func_data[Constants.DEVICE_DATA_JOY_X_MOVE_IDX]);
                                }
                                //tmp_sbyte = (sbyte)(my_set_data.joypad_data[p_idx, Constants.DEVICE_DATA_JOY_X_MOVE_IDX]);
                                tmp_dec = tmp_sbyte;
                                num_sw_func_x.Value = tmp_dec;
                                num_sw_func_y.Visible = p_visible;
                                num_sw_func_y.Minimum = Constants.JOYPAD_MOVE_MIN;
                                num_sw_func_y.Maximum = Constants.JOYPAD_MOVE_MAX;
                                if (my_set_sw_func_data[Constants.DEVICE_DATA_JOY_Y_MOVE_IDX] > 0x7F)
                                {
                                    tmp_sbyte = (sbyte)(my_set_sw_func_data[Constants.DEVICE_DATA_JOY_Y_MOVE_IDX] - 0x100);
                                }
                                else
                                {
                                    tmp_sbyte = (sbyte)(my_set_sw_func_data[Constants.DEVICE_DATA_JOY_Y_MOVE_IDX]);
                                }
                                //tmp_sbyte = (sbyte)(my_set_data.joypad_data[p_idx, Constants.DEVICE_DATA_JOY_Y_MOVE_IDX]);
                                tmp_dec = tmp_sbyte;
                                num_sw_func_y.Value = tmp_dec;
                                for (int fi = 0; fi < my_keyboard_modifier.Length; fi++)
                                {
                                    my_keyboard_modifier[fi].Visible = false;
                                }
                                txtbx_sw_func_key.Visible = false;
                                btn_sw_func_key_clr.Visible = false;
                                break;
                            default:
                                // 非表示
                                lbl_sw_func_title.Visible = false;
                                num_sw_func_x.Visible = false;
                                num_sw_func_y.Visible = false;
                                for (int fi = 0; fi < my_keyboard_modifier.Length; fi++)
                                {
                                    my_keyboard_modifier[fi].Visible = false;
                                }
                                txtbx_sw_func_key.Visible = false;
                                btn_sw_func_key_clr.Visible = false;
                                break;
                        }
                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_MULTIMEDIA:
                        // SW機能設定がマルチメディアではない場合はデータクリア
                        if (my_set_sw_func_data[Constants.DEVICE_DATA_SET_TYPE_IDX] < Constants.SET_TYPE_MULTIMEDIA_MIN || Constants.SET_TYPE_MULTIMEDIA_MAX < my_set_sw_func_data[Constants.DEVICE_DATA_SET_TYPE_IDX])
                        {
                            for (int fi = (Constants.DEVICE_DATA_SET_TYPE_IDX + 1); fi < Constants.SW_FUNCTION_DATA_LEN; fi++)
                            {
                                my_set_sw_func_data[fi] = 0;
                            }
                        }
                        // 非表示
                        lbl_sw_func_title.Visible = false;
                        num_sw_func_x.Visible = false;
                        num_sw_func_y.Visible = false;
                        for (int fi = 0; fi < my_keyboard_modifier.Length; fi++)
                        {
                            my_keyboard_modifier[fi].Visible = false;
                        }
                        txtbx_sw_func_key.Visible = false;
                        btn_sw_func_key_clr.Visible = false;
                        break;
#if false
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_KEY:
                        // KEY SET表示
                        lbl_func1_title.Visible = false;
                        num_func1_x.Visible = false;
                        num_func1_y.Visible = false;
                        for (int fi = 0; fi < my_keyboard_modifier.Length; fi++)
                        {
                            my_keyboard_modifier[fi].Visible = p_visible;
                        }
                        txtbx_func1_key.Visible = p_visible;
                        btn_func1_key_clr.Visible = p_visible;
                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_MOUSE:
                        // 非表示
                        lbl_func1_title.Visible = false;
                        num_func1_x.Visible = false;
                        num_func1_y.Visible = false;
                        for (int fi = 0; fi < my_keyboard_modifier.Length; fi++)
                        {
                            my_keyboard_modifier[fi].Visible = false;
                        }
                        txtbx_func1_key.Visible = false;
                        btn_func1_key_clr.Visible = false;
                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_GAMEPAD:
                        // 非表示
                        lbl_func1_title.Visible = false;
                        num_func1_x.Visible = false;
                        num_func1_y.Visible = false;
                        for (int fi = 0; fi < my_keyboard_modifier.Length; fi++)
                        {
                            my_keyboard_modifier[fi].Visible = false;
                        }
                        txtbx_func1_key.Visible = false;
                        btn_func1_key_clr.Visible = false;
                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_MULTIMEDIA:
                        // 非表示
                        lbl_func1_title.Visible = false;
                        num_func1_x.Visible = false;
                        num_func1_y.Visible = false;
                        for (int fi = 0; fi < my_keyboard_modifier.Length; fi++)
                        {
                            my_keyboard_modifier[fi].Visible = false;
                        }
                        txtbx_func1_key.Visible = false;
                        btn_func1_key_clr.Visible = false;
                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_TOOL:
                        // 非表示
                        lbl_func1_title.Visible = false;
                        num_func1_x.Visible = false;
                        num_func1_y.Visible = false;
                        for (int fi = 0; fi < my_keyboard_modifier.Length; fi++)
                        {
                            my_keyboard_modifier[fi].Visible = false;
                        }
                        txtbx_func1_key.Visible = false;
                        btn_func1_key_clr.Visible = false;
                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_MODFILE:
                        // 非表示
                        lbl_func1_title.Visible = false;
                        num_func1_x.Visible = false;
                        num_func1_y.Visible = false;
                        for (int fi = 0; fi < my_keyboard_modifier.Length; fi++)
                        {
                            my_keyboard_modifier[fi].Visible = false;
                        }
                        txtbx_func1_key.Visible = false;
                        btn_func1_key_clr.Visible = false;
                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_USERAREA:
                        // 非表示
                        lbl_func1_title.Visible = false;
                        num_func1_x.Visible = false;
                        num_func1_y.Visible = false;
                        for (int fi = 0; fi < my_keyboard_modifier.Length; fi++)
                        {
                            my_keyboard_modifier[fi].Visible = false;
                        }
                        txtbx_func1_key.Visible = false;
                        btn_func1_key_clr.Visible = false;
                        break;
#endif
                    default:
                        break;
                }
            }
            catch
            {
            }
        }

        private void btn_sw_func_key_clr_Click(object sender, EventArgs e)
        {
            try
            {
                for (int fi = 0; fi < my_keyboard_modifier.Length; fi++)
                {
                    my_keyboard_modifier[fi].Checked = false;
                }
                txtbx_sw_func_key.Text = RevOmate.Properties.Resources.KEYBOARD_SET_KEY_EMPTY;
                my_set_sw_func_data[Constants.DEVICE_DATA_KEY1_IDX] = 0x00;
            }
            catch
            {
            }
        }

        private void txtbx_sw_func_key_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                // ctrl, shift, altキーも受け付ける
                byte tmp_usb_key = const_Key_Code.Get_VKtoUSBkey(e.KeyValue.GetHashCode(), my_form1.system_setting_info.Keyboard_Type, true);
                txtbx_sw_func_key.Text = const_Key_Code.Get_KeyCode_Name(tmp_usb_key, my_form1.system_setting_info.Keyboard_Type);
                my_set_sw_func_data[Constants.DEVICE_DATA_KEY1_IDX] = tmp_usb_key;
                e.SuppressKeyPress = true;
            }
            catch
            {
            }
        }

        private void txtbx_sw_func_key_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.PrintScreen)
                {
                    byte tmp_usb_key = const_Key_Code.Get_VKtoUSBkey(e.KeyValue.GetHashCode(), my_form1.system_setting_info.Keyboard_Type, true);
                    txtbx_sw_func_key.Text = const_Key_Code.Get_KeyCode_Name(tmp_usb_key, my_form1.system_setting_info.Keyboard_Type);
                    my_set_sw_func_data[Constants.DEVICE_DATA_KEY1_IDX] = tmp_usb_key;
                    e.SuppressKeyPress = true;
                }
            }
            catch
            {
            }
        }

        private void txtbx_sw_func_key_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Tab)
                {
                    if (txtbx_sw_func_key.Text != e.KeyValue.ToString())
                    {
                        byte tmp_usb_key = const_Key_Code.Get_VKtoUSBkey(e.KeyValue.GetHashCode(), my_form1.system_setting_info.Keyboard_Type, true);
                        txtbx_sw_func_key.Text = const_Key_Code.Get_KeyCode_Name(tmp_usb_key, my_form1.system_setting_info.Keyboard_Type);
                        my_set_sw_func_data[Constants.DEVICE_DATA_KEY1_IDX] = tmp_usb_key;
                        e.IsInputKey = true;
                    }
                }
            }
            catch
            {
            }
        }

        // SW機能の設定名称を取得する
        public string my_Get_Setting_Name(byte p_category_id, byte p_category_id2, byte p_func_list_id, byte[] p_sw_func_data)
        {
            string ret_val = "";
            string tmp_str = "";
            string separate_str = " : ";
            sbyte tmp_sbyte1, tmp_sbyte2;
            try
            {
                int select_idx = -1;
                // カテゴリ1
                for (int fi = 0; fi < CATEGORY_ID_LIST.Length; fi++)
                {
                    if (CATEGORY_ID_LIST[fi] == p_category_id)
                    {
                        select_idx = fi;
                    }
                }
                if (0 <= select_idx && select_idx < CATEGORY_LIST.Length)
                {
                    ret_val = CATEGORY_LIST[select_idx];
                }
                // 機能
                switch (p_category_id)
                {
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_NOT_SET:
                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_GENERAL:
                        select_idx = -1;
                        tmp_str = "";
                        if (0 <= p_func_list_id && p_func_list_id < sp_func_type_list.Length)
                        {
                            tmp_str = sp_func_type_list[p_func_list_id];
                        }
                        if (tmp_str != "")
                        {
                            ret_val += separate_str + tmp_str;
                        }
                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_MACRO:
                        select_idx = -1;
                        tmp_str = "";
                        if (p_func_list_id < Constants.SCRIPT_USER_USE_NUM)
                        {
                            tmp_str = string.Format("{0}{1:000}:", RevOmate.Properties.Resources.MACRO_NAME_NO_MACRO, p_func_list_id + 1);
                            if (my_script_info_datas.Script_Info_Datas[p_func_list_id].Name != "")
                            {
                                tmp_str += my_script_info_datas.Script_Info_Datas[p_func_list_id].Name;
                            }
                            else
                            {
                                tmp_str += RevOmate.Properties.Resources.MACRO_NAME_UNDEFINE;
                            }
                        }
                        if (tmp_str != "")
                        {
                            ret_val += separate_str + tmp_str;
                        }
                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_PRESET:
                        select_idx = -1;
                        for (int fi = 0; fi < CATEGORY_PRESET_ID_LIST.Length; fi++)
                        {
                            if (CATEGORY_PRESET_ID_LIST[fi] == p_category_id2)
                            {
                                select_idx = fi;
                            }
                        }
                        if (0 <= select_idx && select_idx < CATEGORY_PRESET_LIST.Length)
                        {
                            ret_val += separate_str + CATEGORY_PRESET_LIST[select_idx];
                        }

                        switch (p_category_id2)
                        {
                            case Constants.APP_DATA_FUNC_PRESET_ID_PHOTOSHOP:
                                select_idx = -1;
                                for (int fi = 0; fi < PRESET_PHOTOSHOP_ID_LIST.Length; fi++)
                                {
                                    if (PRESET_PHOTOSHOP_ID_LIST[fi] == p_func_list_id)
                                    {
                                        select_idx = fi;
                                    }
                                }
                                if (0 <= select_idx && select_idx < PRESET_PHOTOSHOP_LIST.Length)
                                {
                                    ret_val += separate_str + PRESET_PHOTOSHOP_LIST[select_idx];
                                }
                                break;
                            case Constants.APP_DATA_FUNC_PRESET_ID_CLIPSTUDIO:
                                select_idx = -1;
                                for (int fi = 0; fi < PRESET_CLIPSTUDIO_ID_LIST.Length; fi++)
                                {
                                    if (PRESET_CLIPSTUDIO_ID_LIST[fi] == p_func_list_id)
                                    {
                                        select_idx = fi;
                                    }
                                }
                                if (0 <= select_idx && select_idx < PRESET_CLIPSTUDIO_LIST.Length)
                                {
                                    ret_val += separate_str + PRESET_CLIPSTUDIO_LIST[select_idx];
                                }
                                break;
                            case Constants.APP_DATA_FUNC_PRESET_ID_SAI:
                                select_idx = -1;
                                for (int fi = 0; fi < PRESET_SAI_ID_LIST.Length; fi++)
                                {
                                    if (PRESET_SAI_ID_LIST[fi] == p_func_list_id)
                                    {
                                        select_idx = fi;
                                    }
                                }
                                if (0 <= select_idx && select_idx < PRESET_SAI_LIST.Length)
                                {
                                    ret_val += separate_str + PRESET_SAI_LIST[select_idx];
                                }
                                break;
                            case Constants.APP_DATA_FUNC_PRESET_ID_ILLUSTRATOR:
                                select_idx = -1;
                                for (int fi = 0; fi < PRESET_ILLUSTRATOR_ID_LIST.Length; fi++)
                                {
                                    if (PRESET_ILLUSTRATOR_ID_LIST[fi] == p_func_list_id)
                                    {
                                        select_idx = fi;
                                    }
                                }
                                if (0 <= select_idx && select_idx < PRESET_ILLUSTRATOR_LIST.Length)
                                {
                                    ret_val += separate_str + PRESET_ILLUSTRATOR_LIST[select_idx];
                                }
                                break;
                            case Constants.APP_DATA_FUNC_PRESET_ID_LIGHTROOM:
                                select_idx = -1;
                                for (int fi = 0; fi < PRESET_LIGHTROOM_ID_LIST.Length; fi++)
                                {
                                    if (PRESET_LIGHTROOM_ID_LIST[fi] == p_func_list_id)
                                    {
                                        select_idx = fi;
                                    }
                                }
                                if (0 <= select_idx && select_idx < PRESET_LIGHTROOM_LIST.Length)
                                {
                                    ret_val += separate_str + PRESET_LIGHTROOM_LIST[select_idx];
                                }
                                break;
                            case Constants.APP_DATA_FUNC_PRESET_ID_MULTIMEDIA:
                                select_idx = -1;
                                for (int fi = 0; fi < PRESET_MULTIMEDIA_ID_LIST.Length; fi++)
                                {
                                    if (PRESET_MULTIMEDIA_ID_LIST[fi] == p_func_list_id)
                                    {
                                        select_idx = fi;
                                    }
                                }
                                if (0 <= select_idx && select_idx < PRESET_MULTIMEDIA_LIST.Length)
                                {
                                    ret_val += separate_str + PRESET_MULTIMEDIA_LIST[select_idx];
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_KEY:
                        select_idx = -1;
                        for (int fi = 0; fi < FUNC_KEY_ID_LIST.Length; fi++)
                        {
                            if (FUNC_KEY_ID_LIST[fi] == p_func_list_id)
                            {
                                select_idx = fi;
                            }
                        }
                        if (0 <= select_idx && select_idx < FUNC_KEY_LIST.Length)
                        {
                            ret_val += separate_str + FUNC_KEY_LIST[select_idx];
                            // KEY設定の場合はキー名称も表示する
                            if (select_idx == Constants.APP_DATA_DIAL_FUNC_KEY_ID_KEY)
                            {
                                if (p_sw_func_data.Length == Constants.SW_FUNCTION_DATA_LEN)
                                {
                                    ret_val += "[";
                                    int disp_count = 0;
                                    // 制御ビット
                                    for (int fi = 0; fi < my_form1.keyboard_modifier_bit.Length; fi++)
                                    {
                                        if ((p_sw_func_data[Constants.DEVICE_DATA_MODIFIER_IDX] & my_form1.keyboard_modifier_bit[fi]) != 0)
                                        {
                                            if (disp_count != 0)
                                            {
                                                ret_val += "+";
                                            }
                                            ret_val += my_form1.keyboard_modifier_name[fi];
                                            disp_count++;
                                        }
                                    }
                                    // キー
                                    if (p_sw_func_data[Constants.DEVICE_DATA_KEY1_IDX] == 0)
                                    {
                                        //ret_val += RevOmate.Properties.Resources.KEYBOARD_SET_KEY_EMPTY;
                                    }
                                    else
                                    {
                                        if (disp_count != 0)
                                        {
                                            ret_val += "+";
                                        }
                                        ret_val += const_Key_Code.Get_KeyCode_Name(p_sw_func_data[Constants.DEVICE_DATA_KEY1_IDX], my_form1.system_setting_info.Keyboard_Type);
                                        disp_count++;
                                    }
                                    ret_val += "]";
                                }
                            }
                        }
                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_MOUSE:
                        select_idx = -1;
                        for (int fi = 0; fi < FUNC_MOUSE_ID_LIST.Length; fi++)
                        {
                            if (FUNC_MOUSE_ID_LIST[fi] == p_func_list_id)
                            {
                                select_idx = fi;
                            }
                        }
                        if (0 <= select_idx && select_idx < FUNC_MOUSE_LIST.Length)
                        {
                            ret_val += separate_str + FUNC_MOUSE_LIST[select_idx];
                            // MOVE設定の場合は移動量も表示する
                            if (select_idx == Constants.APP_DATA_DIAL_FUNC_MOUSE_ID_MOVE || select_idx == Constants.APP_DATA_DIAL_FUNC_MOUSE_ID_WH_SCROLL)
                            {
                                if (p_sw_func_data.Length == Constants.SW_FUNCTION_DATA_LEN)
                                {
                                    ret_val += "[";
                                    if(select_idx == Constants.APP_DATA_DIAL_FUNC_MOUSE_ID_MOVE)
                                    {
                                        if (p_sw_func_data[Constants.DEVICE_DATA_X_MOVE_IDX] > 0x7F)
                                        {
                                            tmp_sbyte1 = (sbyte)(p_sw_func_data[Constants.DEVICE_DATA_X_MOVE_IDX] - 0x100);
                                        }
                                        else
                                        {
                                            tmp_sbyte1 = (sbyte)(p_sw_func_data[Constants.DEVICE_DATA_X_MOVE_IDX]);
                                        }
                                        if (p_sw_func_data[Constants.DEVICE_DATA_Y_MOVE_IDX] > 0x7F)
                                        {
                                            tmp_sbyte2 = (sbyte)(p_sw_func_data[Constants.DEVICE_DATA_Y_MOVE_IDX] - 0x100);
                                        }
                                        else
                                        {
                                            tmp_sbyte2 = (sbyte)(p_sw_func_data[Constants.DEVICE_DATA_Y_MOVE_IDX]);
                                        }
                                        ret_val += tmp_sbyte1.ToString() + "," + tmp_sbyte2.ToString();
                                    }
                                    else if (select_idx == Constants.APP_DATA_DIAL_FUNC_MOUSE_ID_WH_SCROLL)
                                    {
                                        if (p_sw_func_data[Constants.DEVICE_DATA_WHEEL_IDX] > 0x7F)
                                        {
                                            tmp_sbyte1 = (sbyte)(p_sw_func_data[Constants.DEVICE_DATA_WHEEL_IDX] - 0x100);
                                        }
                                        else
                                        {
                                            tmp_sbyte1 = (sbyte)(p_sw_func_data[Constants.DEVICE_DATA_WHEEL_IDX]);
                                        }
                                        ret_val += tmp_sbyte1.ToString();
                                    }
                                    ret_val += "]";
                                }
                            }
                        }
                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_GAMEPAD:
                        select_idx = -1;
                        for (int fi = 0; fi < FUNC_GAMEPAD_ID_LIST.Length; fi++)
                        {
                            if (FUNC_GAMEPAD_ID_LIST[fi] == p_func_list_id)
                            {
                                select_idx = fi;
                            }
                        }
                        if (0 <= select_idx && select_idx < FUNC_GAMEPAD_LIST.Length)
                        {
                            ret_val += separate_str + FUNC_GAMEPAD_LIST[select_idx];
                            // レバー設定の場合は移動量も表示する
                            if (select_idx == Constants.APP_DATA_DIAL_FUNC_GAMEPAD_ID_LEFT_ANALOG || select_idx == Constants.APP_DATA_DIAL_FUNC_GAMEPAD_ID_RIGHT_ANALOG)
                            {
                                if (p_sw_func_data.Length == Constants.SW_FUNCTION_DATA_LEN)
                                {
                                    ret_val += "[";
                                    if (p_sw_func_data[Constants.DEVICE_DATA_JOY_X_MOVE_IDX] > 0x7F)
                                    {
                                        tmp_sbyte1 = (sbyte)(p_sw_func_data[Constants.DEVICE_DATA_JOY_X_MOVE_IDX] - 0x100);
                                    }
                                    else
                                    {
                                        tmp_sbyte1 = (sbyte)(p_sw_func_data[Constants.DEVICE_DATA_JOY_X_MOVE_IDX]);
                                    }
                                    if (p_sw_func_data[Constants.DEVICE_DATA_JOY_Y_MOVE_IDX] > 0x7F)
                                    {
                                        tmp_sbyte2 = (sbyte)(p_sw_func_data[Constants.DEVICE_DATA_JOY_Y_MOVE_IDX] - 0x100);
                                    }
                                    else
                                    {
                                        tmp_sbyte2 = (sbyte)(p_sw_func_data[Constants.DEVICE_DATA_JOY_Y_MOVE_IDX]);
                                    }
                                    ret_val += tmp_sbyte1.ToString() + "," + tmp_sbyte2.ToString();
                                    ret_val += "]";
                                }
                            }
                        }
                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_MULTIMEDIA:
                        select_idx = -1;
                        for (int fi = 0; fi < FUNC_MULTIMEDIA_ID_LIST.Length; fi++)
                        {
                            if (FUNC_MULTIMEDIA_ID_LIST[fi] == p_func_list_id)
                            {
                                select_idx = fi;
                            }
                        }
                        if (0 <= select_idx && select_idx < FUNC_MULTIMEDIA_LIST.Length)
                        {
                            ret_val += separate_str + FUNC_MULTIMEDIA_LIST[select_idx];
                        }
                        break;
                    default:
                        break;
                }
            }
            catch
            {
            }
            return ret_val;
        }

        // 現在のプロフィール設定で、プロフィール変更機能が割り当てられているかチェックする
        // 現在のプロフィールで、プロフィール変更機能 または 他のプロフィールへの変更機能が割り当てられていればOKとする
        // 戻り値
        // False : 未割り当て NG
        // true  : 割り当て済み OK
        private bool my_check_profile_change_function_assign(byte p_now_mode, byte p_sw_id, byte p_category1_id, byte p_set_val)
        {
            bool b_ret = false;
            try
            {
                if(p_now_mode < Constants.MODE_NUM && p_sw_id < Constants.BUTTON_NUM)
                {
                    // 変更中のSW以外で割り当てがあるか確認
                    for (int fi = 0; fi < Constants.BUTTON_NUM; fi++)
                    {
                        if (fi != p_sw_id)
                        {
                            if (my_base_mode_infos.base_mode_infos[p_now_mode].sw_sp_func_no[fi] == Constants.SP_FUNC_MODE)
                            {   // プロフィール変更機能割り当て
                                b_ret = true;
                                break;
                            }
                            else if (Constants.SP_FUNC_MODE1 <= my_base_mode_infos.base_mode_infos[p_now_mode].sw_sp_func_no[fi]
                                    && my_base_mode_infos.base_mode_infos[p_now_mode].sw_sp_func_no[fi] < (Constants.SP_FUNC_MODE1 + Constants.MODE_NUM))
                            {   // 各プロフィール変更機能割り当て
                                if (my_base_mode_infos.base_mode_infos[p_now_mode].sw_sp_func_no[fi] != (Constants.SP_FUNC_MODE1 + p_now_mode))
                                {   // 現在のモードへの変更は除く
                                    b_ret = true;
                                    break;
                                }
                            }
                        }
                    }
                    // 変更中のSWが、新しくプロフィール変更機能に割り当てられているか確認
                    if (p_category1_id == Constants.APP_DATA_FUNC_CATEGORY_ID_GENERAL)
                    {
                        if ((p_set_val + 1) == Constants.SP_FUNC_MODE)
                        {   // プロフィール変更機能割り当て
                            b_ret = true;
                        }
                        else if (Constants.SP_FUNC_MODE1 <= (p_set_val + 1)
                                && (p_set_val + 1) < (Constants.SP_FUNC_MODE1 + Constants.MODE_NUM))
                        {   // 各プロフィール変更機能割り当て
                            if ((p_set_val + 1) != (Constants.SP_FUNC_MODE1 + p_now_mode))
                            {   // 現在のモードへの変更は除く
                                b_ret = true;
                            }
                        }
                    }
                }

            }
            catch
            {
            }
            return b_ret;
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

        // カテゴリと機能のリストIDからset type値を取得する
        private int my_Get_set_type_val_by_list_id(byte p_category_id, byte p_category2_id, byte p_func_id, ref byte o_set_type_val)
        {
            int ret_val = 0;
            int tmp_set_type = Constants.SET_TYPE_NONE;
            try
            {

                switch (p_category_id)
                {
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_NOT_SET:
                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_KEY:
                        for (int fi = 0; fi < FUNC_KEY_ID_LIST.Length && fi < func_key_set_type_no_list.Length; fi++)
                        {
                            if (FUNC_KEY_ID_LIST[fi] == p_func_id)
                            {
                                tmp_set_type = func_key_set_type_no_list[fi];
                                break;
                            }
                        }
                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_MOUSE:
                        for (int fi = 0; fi < FUNC_MOUSE_ID_LIST.Length && fi < func_mouse_set_type_no_list.Length; fi++)
                        {
                            if (FUNC_MOUSE_ID_LIST[fi] == p_func_id)
                            {
                                tmp_set_type = func_mouse_set_type_no_list[fi];
                                break;
                            }
                        }
                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_GAMEPAD:
                        for (int fi = 0; fi < FUNC_GAMEPAD_ID_LIST.Length && fi < func_gamepad_set_type_no_list.Length; fi++)
                        {
                            if (FUNC_GAMEPAD_ID_LIST[fi] == p_func_id)
                            {
                                tmp_set_type = func_gamepad_set_type_no_list[fi];
                                break;
                            }
                        }
                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_MULTIMEDIA:
                        for (int fi = 0; fi < FUNC_MULTIMEDIA_ID_LIST.Length && fi < func_multimedia_set_type_no_list.Length; fi++)
                        {
                            if (FUNC_MULTIMEDIA_ID_LIST[fi] == p_func_id)
                            {
                                tmp_set_type = func_multimedia_set_type_no_list[fi];
                                break;
                            }
                        }
                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_GENERAL:
                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_MACRO:
                        break;
                    case Constants.APP_DATA_FUNC_CATEGORY_ID_PRESET:
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

        // 画面表示内容を取得して設定データバッファに格納する
        private void my_function_setting_get_by_disp(byte p_mode_no, byte p_sw_no)
        {
            int move_val = 0;
            byte set_type_no = 0;
            try
            {

                //for (int fi = 0; fi < Constants.SW_FUNCTION_DATA_LEN; fi++)
                //{
                //    my_set_sw_func_data[fi] = 0;
                //}
                //my_set_sw_func_data[Constants.DEVICE_DATA_SET_TYPE_IDX] = Constants.SET_TYPE_NONE;

                my_Get_set_type_val_by_list_id(category1_id_select_val, category2_id_select_val, func_list_select_val, ref set_type_no);
                switch (set_type_no)
                {
                    case Constants.SET_TYPE_MOUSE_LCLICK:
                    case Constants.SET_TYPE_MOUSE_RCLICK:
                    case Constants.SET_TYPE_MOUSE_WHCLICK:
                    case Constants.SET_TYPE_MOUSE_B4CLICK:
                    case Constants.SET_TYPE_MOUSE_B5CLICK:
                    case Constants.SET_TYPE_MOUSE_DCLICK:
                        for (int fi = 0; fi < Constants.SW_FUNCTION_DATA_LEN; fi++)
                        {
                            my_set_sw_func_data[fi] = 0;
                        }
                        my_set_sw_func_data[Constants.DEVICE_DATA_SET_TYPE_IDX] = set_type_no;
                        switch (set_type_no)
                        {
                            case Constants.SET_TYPE_MOUSE_LCLICK:
                                my_set_sw_func_data[Constants.DEVICE_DATA_CLICK_IDX] = Constants.MOUSE_DATA_LEFT_CLICK;
                                break;
                            case Constants.SET_TYPE_MOUSE_RCLICK:
                                my_set_sw_func_data[Constants.DEVICE_DATA_CLICK_IDX] = Constants.MOUSE_DATA_RIGHT_CLICK;
                                break;
                            case Constants.SET_TYPE_MOUSE_WHCLICK:
                                my_set_sw_func_data[Constants.DEVICE_DATA_CLICK_IDX] = Constants.MOUSE_DATA_WHEEL_CLICK;
                                break;
                            case Constants.SET_TYPE_MOUSE_B4CLICK:
                                my_set_sw_func_data[Constants.DEVICE_DATA_CLICK_IDX] = Constants.MOUSE_DATA_BUTTON4_CLICK;
                                break;
                            case Constants.SET_TYPE_MOUSE_B5CLICK:
                                my_set_sw_func_data[Constants.DEVICE_DATA_CLICK_IDX] = Constants.MOUSE_DATA_BUTTON5_CLICK;
                                break;
                            case Constants.SET_TYPE_MOUSE_DCLICK:
                                my_set_sw_func_data[Constants.DEVICE_DATA_CLICK_IDX] = Constants.MOUSE_DATA_LEFT_CLICK;
                                break;
                        }
                        break;
                    case Constants.SET_TYPE_MOUSE_MOVE:
                        for (int fi = 0; fi < Constants.SW_FUNCTION_DATA_LEN; fi++)
                        {
                            my_set_sw_func_data[fi] = 0;
                        }
                        my_set_sw_func_data[Constants.DEVICE_DATA_SET_TYPE_IDX] = set_type_no;
                        move_val = 0;
                        try
                        {
                            move_val = int.Parse(num_sw_func_x.Value.ToString());
                        }
                        catch
                        {
                        }
                        my_set_sw_func_data[Constants.DEVICE_DATA_X_MOVE_IDX] = (byte)(move_val & 0xFF);
                        move_val = 0;
                        try
                        {
                            move_val = int.Parse(num_sw_func_y.Value.ToString());
                        }
                        catch
                        {
                        }
                        my_set_sw_func_data[Constants.DEVICE_DATA_Y_MOVE_IDX] = (byte)(move_val & 0xFF);
                        break;
                    case Constants.SET_TYPE_MOUSE_WHSCROLL:
                        for (int fi = 0; fi < Constants.SW_FUNCTION_DATA_LEN; fi++)
                        {
                            my_set_sw_func_data[fi] = 0;
                        }
                        my_set_sw_func_data[Constants.DEVICE_DATA_SET_TYPE_IDX] = set_type_no;
                        move_val = 0;
                        try
                        {
                            move_val = int.Parse(num_sw_func_x.Value.ToString());
                        }
                        catch
                        {
                        }
                        my_set_sw_func_data[Constants.DEVICE_DATA_WHEEL_IDX] = (byte)(move_val & 0xFF);
                        break;
                    case Constants.SET_TYPE_KEYBOARD_KEY:
                        for (int fi = 0; fi < Constants.SW_FUNCTION_DATA_LEN; fi++)
                        {
                            if (fi != Constants.DEVICE_DATA_KEY1_IDX)
                            {
                                my_set_sw_func_data[fi] = 0;
                            }
                        }
                        my_set_sw_func_data[Constants.DEVICE_DATA_SET_TYPE_IDX] = set_type_no;
                        // モディファイがセットされているかチェックして、ビットセット
                        for (int fj = 0; fj < my_keyboard_modifier.Length; fj++)
                        {
                            if (my_keyboard_modifier[fj].Checked == true)
                            {
                                my_set_sw_func_data[Constants.DEVICE_DATA_MODIFIER_IDX] |= my_form1.keyboard_modifier_bit[fj];
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
                        for (int fi = 0; fi < Constants.SW_FUNCTION_DATA_LEN; fi++)
                        {
                            my_set_sw_func_data[fi] = 0;
                        }
                        my_set_sw_func_data[Constants.DEVICE_DATA_SET_TYPE_IDX] = set_type_no;
                        switch (set_type_no)
                        {
                            case Constants.SET_TYPE_MULTIMEDIA_PLAY:
                                my_set_sw_func_data[Constants.DEVICE_DATA_MULTIMEDIA_VAL1_IDX] = Constants.MULTIMEDIA_DATA_PLAY;
                                break;
                            case Constants.SET_TYPE_MULTIMEDIA_PAUSE:
                                my_set_sw_func_data[Constants.DEVICE_DATA_MULTIMEDIA_VAL1_IDX] = Constants.MULTIMEDIA_DATA_PAUSE;
                                break;
                            case Constants.SET_TYPE_MULTIMEDIA_STOP:
                                my_set_sw_func_data[Constants.DEVICE_DATA_MULTIMEDIA_VAL1_IDX] = Constants.MULTIMEDIA_DATA_STOP;
                                break;
                            case Constants.SET_TYPE_MULTIMEDIA_REC:
                                my_set_sw_func_data[Constants.DEVICE_DATA_MULTIMEDIA_VAL1_IDX] = Constants.MULTIMEDIA_DATA_REC;
                                break;
                            case Constants.SET_TYPE_MULTIMEDIA_FORWORD:
                                my_set_sw_func_data[Constants.DEVICE_DATA_MULTIMEDIA_VAL1_IDX] = Constants.MULTIMEDIA_DATA_FORWARD;
                                break;
                            case Constants.SET_TYPE_MULTIMEDIA_REWIND:
                                my_set_sw_func_data[Constants.DEVICE_DATA_MULTIMEDIA_VAL1_IDX] = Constants.MULTIMEDIA_DATA_REWIND;
                                break;
                            case Constants.SET_TYPE_MULTIMEDIA_NEXT:
                                my_set_sw_func_data[Constants.DEVICE_DATA_MULTIMEDIA_VAL1_IDX] = Constants.MULTIMEDIA_DATA_NEXT;
                                break;
                            case Constants.SET_TYPE_MULTIMEDIA_PREVIOUS:
                                my_set_sw_func_data[Constants.DEVICE_DATA_MULTIMEDIA_VAL1_IDX] = Constants.MULTIMEDIA_DATA_PREVIOUS;
                                break;
                            case Constants.SET_TYPE_MULTIMEDIA_MUTE:
                                my_set_sw_func_data[Constants.DEVICE_DATA_MULTIMEDIA_VAL2_IDX] = Constants.MULTIMEDIA_DATA_MUTE;
                                break;
                            case Constants.SET_TYPE_MULTIMEDIA_VOLUMEUP:
                                my_set_sw_func_data[Constants.DEVICE_DATA_MULTIMEDIA_VAL2_IDX] = Constants.MULTIMEDIA_DATA_VOL_UP;
                                break;
                            case Constants.SET_TYPE_MULTIMEDIA_VOLUMEDOWN:
                                my_set_sw_func_data[Constants.DEVICE_DATA_MULTIMEDIA_VAL2_IDX] = Constants.MULTIMEDIA_DATA_VOL_DOWN;
                                break;
                        }
                        break;
                    case Constants.SET_TYPE_JOYPAD_XY:
                    case Constants.SET_TYPE_JOYPAD_ZRZ:
                        for (int fi = 0; fi < Constants.SW_FUNCTION_DATA_LEN; fi++)
                        {
                            my_set_sw_func_data[fi] = 0;
                        }
                        my_set_sw_func_data[Constants.DEVICE_DATA_SET_TYPE_IDX] = set_type_no;
                        my_set_sw_func_data[Constants.DEVICE_DATA_JOY_HAT_SW_IDX] = Constants.HAT_SWITCH_NULL;
                        move_val = 0;
                        try
                        {
                            move_val = int.Parse(num_sw_func_x.Value.ToString());
                        }
                        catch
                        {
                        }
                        my_set_sw_func_data[Constants.DEVICE_DATA_JOY_X_MOVE_IDX] = (byte)(move_val & 0xFF);
                        move_val = 0;
                        try
                        {
                            move_val = int.Parse(num_sw_func_y.Value.ToString());
                        }
                        catch
                        {
                        }
                        my_set_sw_func_data[Constants.DEVICE_DATA_JOY_Y_MOVE_IDX] = (byte)(move_val & 0xFF);
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
                        for (int fi = 0; fi < Constants.SW_FUNCTION_DATA_LEN; fi++)
                        {
                            my_set_sw_func_data[fi] = 0;
                        }
                        my_set_sw_func_data[Constants.DEVICE_DATA_SET_TYPE_IDX] = set_type_no;
                        my_set_sw_func_data[Constants.DEVICE_DATA_JOY_HAT_SW_IDX] = Constants.HAT_SWITCH_NULL;
                        int tmp_byte_pos = (set_type_no - Constants.SET_TYPE_JOYPAD_B01) / 8;
                        int tmp_bit_pos = (set_type_no - Constants.SET_TYPE_JOYPAD_B01) % 8;
                        int tmp_bit_ope = my_set_sw_func_data[Constants.DEVICE_DATA_JOY_BUTTON1_IDX + tmp_byte_pos];
                        tmp_bit_ope |= (0x01 << tmp_bit_pos);
                        my_set_sw_func_data[Constants.DEVICE_DATA_JOY_BUTTON1_IDX + tmp_byte_pos] = (byte)(tmp_bit_ope & 0xFF);
                        break;
                    case Constants.SET_TYPE_JOYPAD_HSW_NORTH:
                    case Constants.SET_TYPE_JOYPAD_HSW_EAST:
                    case Constants.SET_TYPE_JOYPAD_HSW_SOUTH:
                    case Constants.SET_TYPE_JOYPAD_HSW_WEST:
                        for (int fi = 0; fi < Constants.SW_FUNCTION_DATA_LEN; fi++)
                        {
                            my_set_sw_func_data[fi] = 0;
                        }
                        my_set_sw_func_data[Constants.DEVICE_DATA_SET_TYPE_IDX] = set_type_no;
                        my_set_sw_func_data[Constants.DEVICE_DATA_JOY_HAT_SW_IDX] = Constants.HAT_SWITCH_NULL;
                        switch (my_set_sw_func_data[Constants.DEVICE_DATA_SET_TYPE_IDX])
                        {
                            case Constants.SET_TYPE_JOYPAD_HSW_NORTH:
                                my_set_sw_func_data[Constants.DEVICE_DATA_JOY_HAT_SW_IDX] = Constants.HAT_SWITCH_NORTH;
                                break;
                            case Constants.SET_TYPE_JOYPAD_HSW_EAST:
                                my_set_sw_func_data[Constants.DEVICE_DATA_JOY_HAT_SW_IDX] = Constants.HAT_SWITCH_EAST;
                                break;
                            case Constants.SET_TYPE_JOYPAD_HSW_SOUTH:
                                my_set_sw_func_data[Constants.DEVICE_DATA_JOY_HAT_SW_IDX] = Constants.HAT_SWITCH_SOUTH;
                                break;
                            case Constants.SET_TYPE_JOYPAD_HSW_WEST:
                                my_set_sw_func_data[Constants.DEVICE_DATA_JOY_HAT_SW_IDX] = Constants.HAT_SWITCH_WEST;
                                break;
                        }
                        break;
                    case Constants.SET_TYPE_NUMBER_UP:
                    case Constants.SET_TYPE_NUMBER_DOWN:
                        for (int fi = 0; fi < Constants.SW_FUNCTION_DATA_LEN; fi++)
                        {
                            my_set_sw_func_data[fi] = 0;
                        }
                        my_set_sw_func_data[Constants.DEVICE_DATA_SET_TYPE_IDX] = set_type_no;
                        break;
                    case Constants.SET_TYPE_ENCODER_SCRIPT1:
                    case Constants.SET_TYPE_ENCODER_SCRIPT2:
                    case Constants.SET_TYPE_ENCODER_SCRIPT3:
                        for (int fi = 0; fi < Constants.SW_FUNCTION_DATA_LEN; fi++)
                        {
                            my_set_sw_func_data[fi] = 0;
                        }
                        my_set_sw_func_data[Constants.DEVICE_DATA_SET_TYPE_IDX] = set_type_no;
                        break;
                    case Constants.SET_TYPE_NONE:
                    default:
                        for (int fi = 0; fi < Constants.SW_FUNCTION_DATA_LEN; fi++)
                        {
                            my_set_sw_func_data[fi] = 0;
                        }
                        my_set_sw_func_data[Constants.DEVICE_DATA_SET_TYPE_IDX] = (byte)(Constants.SET_TYPE_NONE & 0xFF);
                        break;
                }
            }
            catch
            {
            }
        }

        private void cmbbx_encoder_default_func_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // エンコーダーデフォルト機能
                if (sw_id == Constants.ENCODER_BUTTON_ID)
                {
                    if (0 <= cmbbx_encoder_default_func.SelectedIndex && cmbbx_encoder_default_func.SelectedIndex < Constants.FUNCTION_NUM)
                    {
                        encoder_default_select_val = (byte)(cmbbx_encoder_default_func.SelectedIndex & 0xFF);
                    }
                    else
                    {
                        encoder_default_select_val = 0;
                    }
                }
            }
            catch
            {
            }
        }

    }
}
