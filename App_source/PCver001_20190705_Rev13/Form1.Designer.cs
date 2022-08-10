namespace RevOmate
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.StatusBox_lbl = new System.Windows.Forms.Label();
            this.StatusBox_txtbx = new System.Windows.Forms.TextBox();
            this.ReadWriteThread = new System.ComponentModel.BackgroundWorker();
            this.FormUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.colum_lbl = new System.Windows.Forms.Label();
            this.Debug_label1 = new System.Windows.Forms.Label();
            this.Debug_label2 = new System.Windows.Forms.Label();
            this.Debug_label4 = new System.Windows.Forms.Label();
            this.Debug_label3 = new System.Windows.Forms.Label();
            this.txtbx_EraseAns = new System.Windows.Forms.TextBox();
            this.btn_Erase = new System.Windows.Forms.Button();
            this.txtbx_EraseAddress = new System.Windows.Forms.TextBox();
            this.lbl_EraseAddress = new System.Windows.Forms.Label();
            this.lbl_Erase = new System.Windows.Forms.Label();
            this.btn_Write = new System.Windows.Forms.Button();
            this.btn_Read = new System.Windows.Forms.Button();
            this.lbl_WriteData = new System.Windows.Forms.Label();
            this.lbl_ReadSize = new System.Windows.Forms.Label();
            this.lbl_WriteAddress = new System.Windows.Forms.Label();
            this.lbl_ReadAddress = new System.Windows.Forms.Label();
            this.lbl_Write = new System.Windows.Forms.Label();
            this.lbl_Read = new System.Windows.Forms.Label();
            this.txtbx_WriteAns = new System.Windows.Forms.TextBox();
            this.txtbx_WriteData = new System.Windows.Forms.TextBox();
            this.txtbx_WriteAddress = new System.Windows.Forms.TextBox();
            this.txtbx_ReadData = new System.Windows.Forms.TextBox();
            this.txtbx_ReadSize = new System.Windows.Forms.TextBox();
            this.txtbx_ReadAddress = new System.Windows.Forms.TextBox();
            this.dgv_ScriptEditor = new System.Windows.Forms.DataGridView();
            this.E_SIZE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E_VALUE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtbx_debug01 = new System.Windows.Forms.TextBox();
            this.btn_debug4 = new System.Windows.Forms.Button();
            this.btn_debug3 = new System.Windows.Forms.Button();
            this.btn_debug2 = new System.Windows.Forms.Button();
            this.btn_debug1 = new System.Windows.Forms.Button();
            this.Script_Editor_Btn_imageList = new System.Windows.Forms.ImageList(this.components);
            this.dgv_FlashMemory = new System.Windows.Forms.DataGridView();
            this.F_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.F_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.F_SIZE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_Export = new System.Windows.Forms.Button();
            this.btn_ScriptRead = new System.Windows.Forms.Button();
            this.btn_ScriptWrite = new System.Windows.Forms.Button();
            this.lbl_Script_Add_Info = new System.Windows.Forms.Label();
            this.txtbx_Interval_Inputbox = new System.Windows.Forms.TextBox();
            this.btn_Import = new System.Windows.Forms.Button();
            this.ScriptFile_Save_saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.ScriptFile_Import_openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rtxtbx_debug_flash_read = new System.Windows.Forms.RichTextBox();
            this.txtbx_debug_flash_read_size = new System.Windows.Forms.TextBox();
            this.txtbx_debug_flash_read_addr = new System.Windows.Forms.TextBox();
            this.btn_debug_flash_read = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageScript = new System.Windows.Forms.TabPage();
            this.tabPagePattern = new System.Windows.Forms.TabPage();
            this.lbl_func_mode3_select = new System.Windows.Forms.Label();
            this.lbl_func_mode2_select = new System.Windows.Forms.Label();
            this.lbl_func_mode1_select = new System.Windows.Forms.Label();
            this.gbx_func_setup = new System.Windows.Forms.GroupBox();
            this.txtbx_function2_name = new System.Windows.Forms.TextBox();
            this.txtbx_function3_name = new System.Windows.Forms.TextBox();
            this.txtbx_function4_name = new System.Windows.Forms.TextBox();
            this.txtbx_function1_name = new System.Windows.Forms.TextBox();
            this.gbx_func4_setup = new System.Windows.Forms.GroupBox();
            this.txtbx_func4_key_ccw = new System.Windows.Forms.TextBox();
            this.btn_func4_key_clr_ccw = new System.Windows.Forms.Button();
            this.btn_func4_key_clr_cw = new System.Windows.Forms.Button();
            this.num_func4_y_ccw = new System.Windows.Forms.NumericUpDown();
            this.txtbx_func4_key_cw = new System.Windows.Forms.TextBox();
            this.num_func4_x_ccw = new System.Windows.Forms.NumericUpDown();
            this.chk_func4_win_cw = new System.Windows.Forms.CheckBox();
            this.lbl_func4_title_ccw = new System.Windows.Forms.Label();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.rbtn_LED_Level_Light_func4 = new System.Windows.Forms.RadioButton();
            this.lbl_LED_COLOR_NOW_func4 = new System.Windows.Forms.Label();
            this.rbtn_LED_Level_Normal_func4 = new System.Windows.Forms.RadioButton();
            this.label19 = new System.Windows.Forms.Label();
            this.rbtn_LED_Level_Dark_func4 = new System.Windows.Forms.RadioButton();
            this.btn_LED_preview_func4 = new System.Windows.Forms.Button();
            this.lbl_LED_COLOR_9_func4 = new System.Windows.Forms.Label();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.trackBar_B_func4 = new System.Windows.Forms.TrackBar();
            this.trackBar_G_func4 = new System.Windows.Forms.TrackBar();
            this.trackBar_R_func4 = new System.Windows.Forms.TrackBar();
            this.lbl_LED_COLOR_8_func4 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_7_func4 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_6_func4 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_5_func4 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_4_func4 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_3_func4 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_2_func4 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_1_func4 = new System.Windows.Forms.Label();
            this.label75 = new System.Windows.Forms.Label();
            this.num_func4_y_cw = new System.Windows.Forms.NumericUpDown();
            this.chk_func4_alt_cw = new System.Windows.Forms.CheckBox();
            this.num_func4_x_cw = new System.Windows.Forms.NumericUpDown();
            this.num_func4_sensivity_ccw = new System.Windows.Forms.NumericUpDown();
            this.lbl_func4_title_cw = new System.Windows.Forms.Label();
            this.chk_func4_shift_cw = new System.Windows.Forms.CheckBox();
            this.label76 = new System.Windows.Forms.Label();
            this.chk_func4_ctrl_cw = new System.Windows.Forms.CheckBox();
            this.num_func4_sensivity_cw = new System.Windows.Forms.NumericUpDown();
            this.label77 = new System.Windows.Forms.Label();
            this.chk_func4_win_ccw = new System.Windows.Forms.CheckBox();
            this.cmbbx_func4_set_type_ccw = new System.Windows.Forms.ComboBox();
            this.chk_func4_alt_ccw = new System.Windows.Forms.CheckBox();
            this.label78 = new System.Windows.Forms.Label();
            this.chk_func4_shift_ccw = new System.Windows.Forms.CheckBox();
            this.cmbbx_func4_set_type_cw = new System.Windows.Forms.ComboBox();
            this.chk_func4_ctrl_ccw = new System.Windows.Forms.CheckBox();
            this.label79 = new System.Windows.Forms.Label();
            this.gbx_func3_setup = new System.Windows.Forms.GroupBox();
            this.txtbx_func3_key_ccw = new System.Windows.Forms.TextBox();
            this.btn_func3_key_clr_ccw = new System.Windows.Forms.Button();
            this.btn_func3_key_clr_cw = new System.Windows.Forms.Button();
            this.txtbx_func3_key_cw = new System.Windows.Forms.TextBox();
            this.chk_func3_win_cw = new System.Windows.Forms.CheckBox();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.rbtn_LED_Level_Light_func3 = new System.Windows.Forms.RadioButton();
            this.lbl_LED_COLOR_NOW_func3 = new System.Windows.Forms.Label();
            this.rbtn_LED_Level_Normal_func3 = new System.Windows.Forms.RadioButton();
            this.label14 = new System.Windows.Forms.Label();
            this.rbtn_LED_Level_Dark_func3 = new System.Windows.Forms.RadioButton();
            this.btn_LED_preview_func3 = new System.Windows.Forms.Button();
            this.lbl_LED_COLOR_9_func3 = new System.Windows.Forms.Label();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.trackBar_B_func3 = new System.Windows.Forms.TrackBar();
            this.trackBar_G_func3 = new System.Windows.Forms.TrackBar();
            this.trackBar_R_func3 = new System.Windows.Forms.TrackBar();
            this.lbl_LED_COLOR_8_func3 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_7_func3 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_6_func3 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_5_func3 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_4_func3 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_3_func3 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_2_func3 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_1_func3 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.chk_func3_alt_cw = new System.Windows.Forms.CheckBox();
            this.num_func3_sensivity_ccw = new System.Windows.Forms.NumericUpDown();
            this.chk_func3_shift_cw = new System.Windows.Forms.CheckBox();
            this.num_func3_y_ccw = new System.Windows.Forms.NumericUpDown();
            this.label62 = new System.Windows.Forms.Label();
            this.num_func3_x_ccw = new System.Windows.Forms.NumericUpDown();
            this.chk_func3_ctrl_cw = new System.Windows.Forms.CheckBox();
            this.lbl_func3_title_ccw = new System.Windows.Forms.Label();
            this.num_func3_sensivity_cw = new System.Windows.Forms.NumericUpDown();
            this.num_func3_y_cw = new System.Windows.Forms.NumericUpDown();
            this.num_func3_x_cw = new System.Windows.Forms.NumericUpDown();
            this.label63 = new System.Windows.Forms.Label();
            this.lbl_func3_title_cw = new System.Windows.Forms.Label();
            this.chk_func3_win_ccw = new System.Windows.Forms.CheckBox();
            this.cmbbx_func3_set_type_ccw = new System.Windows.Forms.ComboBox();
            this.chk_func3_alt_ccw = new System.Windows.Forms.CheckBox();
            this.label64 = new System.Windows.Forms.Label();
            this.chk_func3_shift_ccw = new System.Windows.Forms.CheckBox();
            this.cmbbx_func3_set_type_cw = new System.Windows.Forms.ComboBox();
            this.chk_func3_ctrl_ccw = new System.Windows.Forms.CheckBox();
            this.label65 = new System.Windows.Forms.Label();
            this.gbx_func2_setup = new System.Windows.Forms.GroupBox();
            this.btn_func2_key_clr_ccw = new System.Windows.Forms.Button();
            this.btn_func2_key_clr_cw = new System.Windows.Forms.Button();
            this.txtbx_func2_key_cw = new System.Windows.Forms.TextBox();
            this.chk_func2_win_cw = new System.Windows.Forms.CheckBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.rbtn_LED_Level_Light_func2 = new System.Windows.Forms.RadioButton();
            this.lbl_LED_COLOR_NOW_func2 = new System.Windows.Forms.Label();
            this.rbtn_LED_Level_Normal_func2 = new System.Windows.Forms.RadioButton();
            this.label12 = new System.Windows.Forms.Label();
            this.rbtn_LED_Level_Dark_func2 = new System.Windows.Forms.RadioButton();
            this.btn_LED_preview_func2 = new System.Windows.Forms.Button();
            this.lbl_LED_COLOR_9_func2 = new System.Windows.Forms.Label();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.trackBar_B_func2 = new System.Windows.Forms.TrackBar();
            this.trackBar_G_func2 = new System.Windows.Forms.TrackBar();
            this.trackBar_R_func2 = new System.Windows.Forms.TrackBar();
            this.lbl_LED_COLOR_8_func2 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_7_func2 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_6_func2 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_5_func2 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_4_func2 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_3_func2 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_2_func2 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_1_func2 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.chk_func2_alt_cw = new System.Windows.Forms.CheckBox();
            this.num_func2_sensivity_ccw = new System.Windows.Forms.NumericUpDown();
            this.chk_func2_shift_cw = new System.Windows.Forms.CheckBox();
            this.label48 = new System.Windows.Forms.Label();
            this.chk_func2_ctrl_cw = new System.Windows.Forms.CheckBox();
            this.num_func2_sensivity_cw = new System.Windows.Forms.NumericUpDown();
            this.txtbx_func2_key_ccw = new System.Windows.Forms.TextBox();
            this.label49 = new System.Windows.Forms.Label();
            this.chk_func2_win_ccw = new System.Windows.Forms.CheckBox();
            this.num_func2_y_ccw = new System.Windows.Forms.NumericUpDown();
            this.cmbbx_func2_set_type_ccw = new System.Windows.Forms.ComboBox();
            this.num_func2_x_ccw = new System.Windows.Forms.NumericUpDown();
            this.chk_func2_alt_ccw = new System.Windows.Forms.CheckBox();
            this.lbl_func2_title_ccw = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.num_func2_y_cw = new System.Windows.Forms.NumericUpDown();
            this.chk_func2_shift_ccw = new System.Windows.Forms.CheckBox();
            this.num_func2_x_cw = new System.Windows.Forms.NumericUpDown();
            this.cmbbx_func2_set_type_cw = new System.Windows.Forms.ComboBox();
            this.lbl_func2_title_cw = new System.Windows.Forms.Label();
            this.chk_func2_ctrl_ccw = new System.Windows.Forms.CheckBox();
            this.label51 = new System.Windows.Forms.Label();
            this.gbx_func1_setup = new System.Windows.Forms.GroupBox();
            this.btn_func1_key_clr_ccw = new System.Windows.Forms.Button();
            this.btn_func1_key_clr_cw = new System.Windows.Forms.Button();
            this.txtbx_func1_key_cw = new System.Windows.Forms.TextBox();
            this.chk_func1_win_cw = new System.Windows.Forms.CheckBox();
            this.chk_func1_alt_cw = new System.Windows.Forms.CheckBox();
            this.chk_func1_shift_cw = new System.Windows.Forms.CheckBox();
            this.chk_func1_ctrl_cw = new System.Windows.Forms.CheckBox();
            this.txtbx_func1_key_ccw = new System.Windows.Forms.TextBox();
            this.chk_func1_win_ccw = new System.Windows.Forms.CheckBox();
            this.chk_func1_alt_ccw = new System.Windows.Forms.CheckBox();
            this.chk_func1_shift_ccw = new System.Windows.Forms.CheckBox();
            this.chk_func1_ctrl_ccw = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbtn_LED_Level_Light_func1 = new System.Windows.Forms.RadioButton();
            this.rbtn_LED_Level_Normal_func1 = new System.Windows.Forms.RadioButton();
            this.rbtn_LED_Level_Dark_func1 = new System.Windows.Forms.RadioButton();
            this.lbl_LED_COLOR_NOW_func1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_LED_preview_func1 = new System.Windows.Forms.Button();
            this.lbl_LED_COLOR_9_func1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.trackBar_B_func1 = new System.Windows.Forms.TrackBar();
            this.trackBar_G_func1 = new System.Windows.Forms.TrackBar();
            this.trackBar_R_func1 = new System.Windows.Forms.TrackBar();
            this.lbl_LED_COLOR_8_func1 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_7_func1 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_6_func1 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_5_func1 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_4_func1 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_3_func1 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_2_func1 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_1_func1 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.num_func1_sensivity_ccw = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.num_func1_sensivity_cw = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbbx_func1_set_type_ccw = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbbx_func1_set_type_cw = new System.Windows.Forms.ComboBox();
            this.num_func1_y_ccw = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.num_func1_x_ccw = new System.Windows.Forms.NumericUpDown();
            this.lbl_func1_title_cw = new System.Windows.Forms.Label();
            this.lbl_func1_title_ccw = new System.Windows.Forms.Label();
            this.num_func1_x_cw = new System.Windows.Forms.NumericUpDown();
            this.num_func1_y_cw = new System.Windows.Forms.NumericUpDown();
            this.lbl_function_set_icon = new System.Windows.Forms.Label();
            this.SetButton_imageList = new System.Windows.Forms.ImageList(this.components);
            this.tabPageButton = new System.Windows.Forms.TabPage();
            this.lbl_button_mode3_select = new System.Windows.Forms.Label();
            this.lbl_button_mode2_select = new System.Windows.Forms.Label();
            this.lbl_button_mode1_select = new System.Windows.Forms.Label();
            this.gbx_encoder_button_setup = new System.Windows.Forms.GroupBox();
            this.gbx_encoder_setup = new System.Windows.Forms.GroupBox();
            this.cmbbx_encoder_default = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.cmbbx_encoder_button = new System.Windows.Forms.ComboBox();
            this.label34 = new System.Windows.Forms.Label();
            this.gbx_LED_set = new System.Windows.Forms.GroupBox();
            this.rbtn_LED_Level_Light = new System.Windows.Forms.RadioButton();
            this.rbtn_LED_Level_Normal = new System.Windows.Forms.RadioButton();
            this.rbtn_LED_Level_Dark = new System.Windows.Forms.RadioButton();
            this.lbl_LED_COLOR_NOW = new System.Windows.Forms.Label();
            this.lbl_LED_color_now_border = new System.Windows.Forms.Label();
            this.btn_LED_preview = new System.Windows.Forms.Button();
            this.lbl_LED_COLOR_9 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.trackBar_B = new System.Windows.Forms.TrackBar();
            this.trackBar_G = new System.Windows.Forms.TrackBar();
            this.trackBar_R = new System.Windows.Forms.TrackBar();
            this.lbl_LED_COLOR_8 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_7 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_6 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_5 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_4 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_3 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_2 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_1 = new System.Windows.Forms.Label();
            this.lbl_LED_color_border = new System.Windows.Forms.Label();
            this.gbx_button_setup = new System.Windows.Forms.GroupBox();
            this.cmbbx_button_10 = new System.Windows.Forms.ComboBox();
            this.label33 = new System.Windows.Forms.Label();
            this.cmbbx_button_9 = new System.Windows.Forms.ComboBox();
            this.label32 = new System.Windows.Forms.Label();
            this.cmbbx_button_8 = new System.Windows.Forms.ComboBox();
            this.label31 = new System.Windows.Forms.Label();
            this.cmbbx_button_7 = new System.Windows.Forms.ComboBox();
            this.label30 = new System.Windows.Forms.Label();
            this.cmbbx_button_6 = new System.Windows.Forms.ComboBox();
            this.label29 = new System.Windows.Forms.Label();
            this.cmbbx_button_5 = new System.Windows.Forms.ComboBox();
            this.label28 = new System.Windows.Forms.Label();
            this.cmbbx_button_4 = new System.Windows.Forms.ComboBox();
            this.label27 = new System.Windows.Forms.Label();
            this.cmbbx_button_3 = new System.Windows.Forms.ComboBox();
            this.label26 = new System.Windows.Forms.Label();
            this.cmbbx_button_2 = new System.Windows.Forms.ComboBox();
            this.label25 = new System.Windows.Forms.Label();
            this.cmbbx_button_1 = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.lbl_button_setting_set_icon = new System.Windows.Forms.Label();
            this.tabPageEncoderScript = new System.Windows.Forms.TabPage();
            this.lbl_encoder_script_setting_set_icon = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.gbx_base_setting = new System.Windows.Forms.GroupBox();
            this.gbx_keyboard_setting = new System.Windows.Forms.GroupBox();
            this.btn_keyboard_setup_assistant = new System.Windows.Forms.Button();
            this.rbtn_keyboard_ja = new System.Windows.Forms.RadioButton();
            this.rbtn_keyboard_us = new System.Windows.Forms.RadioButton();
            this.chkbx_encoder_typematic = new System.Windows.Forms.CheckBox();
            this.gbx_dial_func_LED_setting = new System.Windows.Forms.GroupBox();
            this.rbtn_func_led_on = new System.Windows.Forms.RadioButton();
            this.rbtn_func_led_slow = new System.Windows.Forms.RadioButton();
            this.rbtn_func_led_flash = new System.Windows.Forms.RadioButton();
            this.gbx_mode_LED_setting = new System.Windows.Forms.GroupBox();
            this.chkbx_mode_led_off = new System.Windows.Forms.CheckBox();
            this.lbl_mode_led_off_unit = new System.Windows.Forms.Label();
            this.num_mode_led_off_time = new System.Windows.Forms.NumericUpDown();
            this.btn_base_setting_set = new System.Windows.Forms.Button();
            this.chkbx_led_sleep = new System.Windows.Forms.CheckBox();
            this.chkbx_encoder_script_loop = new System.Windows.Forms.CheckBox();
            this.dgv_encoder_script = new System.Windows.Forms.DataGridView();
            this.dgv_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_script_name = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.cmbbx_encoder_script_select_no = new System.Windows.Forms.ComboBox();
            this.gbx_Script_Add_Info_Mouse = new System.Windows.Forms.GroupBox();
            this.rbtn_Mouse05 = new System.Windows.Forms.RadioButton();
            this.rbtn_Mouse04 = new System.Windows.Forms.RadioButton();
            this.rbtn_Mouse03 = new System.Windows.Forms.RadioButton();
            this.rbtn_Mouse02 = new System.Windows.Forms.RadioButton();
            this.rbtn_Mouse01 = new System.Windows.Forms.RadioButton();
            this.lbl_Script_Add_Info_Mouse_Msg = new System.Windows.Forms.Label();
            this.lbl_Script_Add_Info_Mouse_Set = new System.Windows.Forms.Label();
            this.gbx_Script_Add_Info_MultiMedia = new System.Windows.Forms.GroupBox();
            this.rbtn_MultiMedia11 = new System.Windows.Forms.RadioButton();
            this.rbtn_MultiMedia10 = new System.Windows.Forms.RadioButton();
            this.rbtn_MultiMedia09 = new System.Windows.Forms.RadioButton();
            this.rbtn_MultiMedia08 = new System.Windows.Forms.RadioButton();
            this.rbtn_MultiMedia07 = new System.Windows.Forms.RadioButton();
            this.rbtn_MultiMedia06 = new System.Windows.Forms.RadioButton();
            this.rbtn_MultiMedia05 = new System.Windows.Forms.RadioButton();
            this.rbtn_MultiMedia04 = new System.Windows.Forms.RadioButton();
            this.rbtn_MultiMedia03 = new System.Windows.Forms.RadioButton();
            this.rbtn_MultiMedia02 = new System.Windows.Forms.RadioButton();
            this.rbtn_MultiMedia01 = new System.Windows.Forms.RadioButton();
            this.lbl_Script_Add_Info_MultiMedia_Msg = new System.Windows.Forms.Label();
            this.lbl_Script_Add_Info_MultiMedia_Set = new System.Windows.Forms.Label();
            this.lbl_MultiMediaRelease_Icon = new System.Windows.Forms.Label();
            this.lbl_MultiMediaPress_Icon = new System.Windows.Forms.Label();
            this.lbl_MouseMovePress_Icon = new System.Windows.Forms.Label();
            this.pgb_process_status = new System.Windows.Forms.ProgressBar();
            this.lbl_button_factory_reset_icon = new System.Windows.Forms.Label();
            this.Reset_Icon_imageList = new System.Windows.Forms.ImageList(this.components);
            this.lbl_reset_title = new System.Windows.Forms.Label();
            this.lbl_reset_bg = new System.Windows.Forms.Label();
            this.lbl_REC_Icon = new System.Windows.Forms.Label();
            this.lbl_Arrow_Dust = new System.Windows.Forms.Label();
            this.lbl_progress_total_value = new System.Windows.Forms.Label();
            this.lbl_progress_now_value = new System.Windows.Forms.Label();
            this.lbl_progress_per = new System.Windows.Forms.Label();
            this.gbx_MacroREC = new System.Windows.Forms.GroupBox();
            this.lbl_Clear_btn = new System.Windows.Forms.Label();
            this.lbl_REC_Btn = new System.Windows.Forms.Label();
            this.gbx_MacroFileImportExport = new System.Windows.Forms.GroupBox();
            this.lbl_MacroFileExport_Icon = new System.Windows.Forms.Label();
            this.lbl_MacroFileImportExportTitle_Icon = new System.Windows.Forms.Label();
            this.lbl_MacroFileImport_Icon = new System.Windows.Forms.Label();
            this.MacroFileImportExport_imageList = new System.Windows.Forms.ImageList(this.components);
            this.gbx_Script_Add_Info_JoysticLever = new System.Windows.Forms.GroupBox();
            this.lbl_Script_Add_Info_JoysticLever_Set = new System.Windows.Forms.Label();
            this.lbl_Script_Add_Info_JoysticLever_X = new System.Windows.Forms.Label();
            this.lbl_Script_Add_Info_JoysticLever_Y = new System.Windows.Forms.Label();
            this.lbl_Script_Add_Info_JoysticLever_Msg = new System.Windows.Forms.Label();
            this.txtbx_Script_Add_Info_JoysticLever_Y = new System.Windows.Forms.TextBox();
            this.txtbx_Script_Add_Info_JoysticLever_X = new System.Windows.Forms.TextBox();
            this.gbx_Script_Add_Info_JoysticButton = new System.Windows.Forms.GroupBox();
            this.rbtn_JoystickButton13 = new System.Windows.Forms.RadioButton();
            this.rbtn_JoystickButton12 = new System.Windows.Forms.RadioButton();
            this.rbtn_JoystickButton11 = new System.Windows.Forms.RadioButton();
            this.rbtn_JoystickButton10 = new System.Windows.Forms.RadioButton();
            this.rbtn_JoystickButton09 = new System.Windows.Forms.RadioButton();
            this.rbtn_JoystickButton08 = new System.Windows.Forms.RadioButton();
            this.rbtn_JoystickButton07 = new System.Windows.Forms.RadioButton();
            this.rbtn_JoystickButton06 = new System.Windows.Forms.RadioButton();
            this.rbtn_JoystickButton05 = new System.Windows.Forms.RadioButton();
            this.rbtn_JoystickButton04 = new System.Windows.Forms.RadioButton();
            this.rbtn_JoystickButton03 = new System.Windows.Forms.RadioButton();
            this.rbtn_JoystickButton02 = new System.Windows.Forms.RadioButton();
            this.rbtn_JoystickButton01 = new System.Windows.Forms.RadioButton();
            this.lbl_Script_Add_Info_JoysticButton_Msg = new System.Windows.Forms.Label();
            this.lbl_Script_Add_Info_JoysticButton_Set = new System.Windows.Forms.Label();
            this.gbx_Script_Add_Info = new System.Windows.Forms.GroupBox();
            this.lbl_Script_Add_Info_ClickArea = new System.Windows.Forms.Label();
            this.lbl_Arrow_Icon7 = new System.Windows.Forms.Label();
            this.lbl_Arrow_Icon6 = new System.Windows.Forms.Label();
            this.lbl_Arrow_Icon5 = new System.Windows.Forms.Label();
            this.lbl_Arrow_Icon4 = new System.Windows.Forms.Label();
            this.lbl_Arrow_Icon3 = new System.Windows.Forms.Label();
            this.lbl_Arrow_Icon2 = new System.Windows.Forms.Label();
            this.lbl_MacroRead_Icon = new System.Windows.Forms.Label();
            this.DeviceReadWrite_imageList = new System.Windows.Forms.ImageList(this.components);
            this.lbl_MacroWrite_Icon = new System.Windows.Forms.Label();
            this.gbx_FileImportExport = new System.Windows.Forms.GroupBox();
            this.lbl_FileExport_Icon = new System.Windows.Forms.Label();
            this.FileImportExport_imageList = new System.Windows.Forms.ImageList(this.components);
            this.lbl_FileImportExportTitle_Icon = new System.Windows.Forms.Label();
            this.lbl_FileImport_Icon = new System.Windows.Forms.Label();
            this.lbl_JoyRightLeverRelease_Icon = new System.Windows.Forms.Label();
            this.MacroEditIcon_imageList = new System.Windows.Forms.ImageList(this.components);
            this.lbl_JoyRightLeverPress_Icon = new System.Windows.Forms.Label();
            this.lbl_JoyButtonRelease_Icon = new System.Windows.Forms.Label();
            this.lbl_JoyButtonPress_Icon = new System.Windows.Forms.Label();
            this.lbl_JoyHatSWRelease_Icon = new System.Windows.Forms.Label();
            this.lbl_JoyHatSWPress_Icon = new System.Windows.Forms.Label();
            this.lbl_JoyLeftLeverRelease_Icon = new System.Windows.Forms.Label();
            this.lbl_JoyLeftLeverPress_Icon = new System.Windows.Forms.Label();
            this.txtbx_script_no = new System.Windows.Forms.TextBox();
            this.lbl_macro_editor_now_no = new System.Windows.Forms.Label();
            this.txtbx_script_name = new System.Windows.Forms.TextBox();
            this.lbl_macro_editor_now_macro_name = new System.Windows.Forms.Label();
            this.dgv_ScriptList = new System.Windows.Forms.DataGridView();
            this.dgv_ScriptList_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_ScriptList_ScriptName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbl_Interval_Icon = new System.Windows.Forms.Label();
            this.lbl_KeyRelease_Icon = new System.Windows.Forms.Label();
            this.lbl_KeyPress_Icon = new System.Windows.Forms.Label();
            this.lbl_MouseClick_Icon = new System.Windows.Forms.Label();
            this.lbl_MouseRelease_Icon = new System.Windows.Forms.Label();
            this.lbl_WheelScroll_Icon = new System.Windows.Forms.Label();
            this.lbl_Dustbox_Icon = new System.Windows.Forms.Label();
            this.DustBox_imageList = new System.Windows.Forms.ImageList(this.components);
            this.lbl_Arrow_Icon1 = new System.Windows.Forms.Label();
            this.chkbx_LED_preview = new System.Windows.Forms.CheckBox();
            this.PatternFileImportExport_imageList = new System.Windows.Forms.ImageList(this.components);
            this.Pattern_Icon_imageList = new System.Windows.Forms.ImageList(this.components);
            this.OnTimeSet_imageList = new System.Windows.Forms.ImageList(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numud_led_brightness_level_set_val = new System.Windows.Forms.NumericUpDown();
            this.chkbx_led_debug = new System.Windows.Forms.CheckBox();
            this.btn_debug_led_duty_set = new System.Windows.Forms.Button();
            this.numud_led_g_set_val = new System.Windows.Forms.NumericUpDown();
            this.numud_led_r_set_val = new System.Windows.Forms.NumericUpDown();
            this.numud_led_b_set_val = new System.Windows.Forms.NumericUpDown();
            this.lbl_FW_Version = new System.Windows.Forms.Label();
            this.btn_FlashErase = new System.Windows.Forms.Button();
            this.PatternListFile_saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.PatternListFile_openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.lbl_mode_status = new System.Windows.Forms.Label();
            this.pnl_main = new System.Windows.Forms.Panel();
            this.lbl_profile3_border = new System.Windows.Forms.Label();
            this.lbl_profile2_border = new System.Windows.Forms.Label();
            this.lbl_profile1_border = new System.Windows.Forms.Label();
            this.lbl_sw1_func_name = new System.Windows.Forms.Label();
            this.llbl_help = new System.Windows.Forms.LinkLabel();
            this.lbl_Dial_func_name = new System.Windows.Forms.Label();
            this.btn_system_setup = new System.Windows.Forms.Button();
            this.btn_dial_macro_editor = new System.Windows.Forms.Button();
            this.btn_macro_editor = new System.Windows.Forms.Button();
            this.lbl_func_color_4 = new System.Windows.Forms.Label();
            this.lbl_func_color_3 = new System.Windows.Forms.Label();
            this.lbl_func_color_2 = new System.Windows.Forms.Label();
            this.lbl_func_color_1 = new System.Windows.Forms.Label();
            this.lbl_profile_color_3 = new System.Windows.Forms.Label();
            this.lbl_profile_color_2 = new System.Windows.Forms.Label();
            this.lbl_profile_color_1 = new System.Windows.Forms.Label();
            this.lbl_profile_3_select = new System.Windows.Forms.Label();
            this.lbl_profile_2_select = new System.Windows.Forms.Label();
            this.lbl_profile_1_select = new System.Windows.Forms.Label();
            this.lbl_sw10_func_name = new System.Windows.Forms.Label();
            this.lbl_sw9_func_name = new System.Windows.Forms.Label();
            this.lbl_sw8_func_name = new System.Windows.Forms.Label();
            this.lbl_sw7_func_name = new System.Windows.Forms.Label();
            this.lbl_sw6_func_name = new System.Windows.Forms.Label();
            this.lbl_sw5_func_name = new System.Windows.Forms.Label();
            this.lbl_sw4_func_name = new System.Windows.Forms.Label();
            this.lbl_sw3_func_name = new System.Windows.Forms.Label();
            this.lbl_sw2_func_name = new System.Windows.Forms.Label();
            this.lbl_Dial_func_name4 = new System.Windows.Forms.Label();
            this.lbl_Dial_func_name3 = new System.Windows.Forms.Label();
            this.lbl_Dial_func_name2 = new System.Windows.Forms.Label();
            this.lbl_Dial_func_name1 = new System.Windows.Forms.Label();
            this.pbx_mode_color = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip_sw_func = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.機能設定画面ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.標準設定に戻すToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.割り当て解除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.インポートToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.エクスポートToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_macro_editor_close = new System.Windows.Forms.Button();
            this.btn_dial_macro_editor_cancel = new System.Windows.Forms.Button();
            this.btn_dial_macro_editor_submit = new System.Windows.Forms.Button();
            this.btn_debug_tab_disp = new System.Windows.Forms.Button();
            this.btn_debug_macro_disp = new System.Windows.Forms.Button();
            this.btn_debug_dial_macro_disp = new System.Windows.Forms.Button();
            this.gbx_system_backup = new System.Windows.Forms.GroupBox();
            this.lbl_system_backup_title = new System.Windows.Forms.Label();
            this.btn_system_default_set = new System.Windows.Forms.Button();
            this.lbl_default_setting_title = new System.Windows.Forms.Label();
            this.btn_system_backupfile_save = new System.Windows.Forms.Button();
            this.btn_system_backupfile_read = new System.Windows.Forms.Button();
            this.btn_system_factory_set = new System.Windows.Forms.Button();
            this.lbl_factory_setting_title = new System.Windows.Forms.Label();
            this.btn_system_setup_close = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnl_system_setup = new System.Windows.Forms.Panel();
            this.llbl_system_setup_help = new System.Windows.Forms.LinkLabel();
            this.pnl_dial_macro_editor = new System.Windows.Forms.Panel();
            this.llbl_dial_macro_editor_help = new System.Windows.Forms.LinkLabel();
            this.pnl_macro_editor = new System.Windows.Forms.Panel();
            this.lbl_macro_editor_area_title = new System.Windows.Forms.Label();
            this.lbl_macro_list_title = new System.Windows.Forms.Label();
            this.lbl_macro_editor_macro_create_title = new System.Windows.Forms.Label();
            this.llbl_macro_editor_help = new System.Windows.Forms.LinkLabel();
            this.gbx_script_command_list = new System.Windows.Forms.GroupBox();
            this.lbl_script_command_title = new System.Windows.Forms.Label();
            this.lbl_MacroRead_txt = new System.Windows.Forms.Label();
            this.lbl_MacroWrite_txt = new System.Windows.Forms.Label();
            this.pnl_keyboard_type_assist = new System.Windows.Forms.Panel();
            this.gbx_keyboard_setup_assis_complete = new System.Windows.Forms.GroupBox();
            this.lbl_keyboard_setup_assist_comp_type2 = new System.Windows.Forms.Label();
            this.lbl_keyboard_setup_assist_comp_type1 = new System.Windows.Forms.Label();
            this.rbtn_keyboard_setup_assist_type2 = new System.Windows.Forms.RadioButton();
            this.rbtn_keyboard_setup_assist_type1 = new System.Windows.Forms.RadioButton();
            this.lbl_keyboard_setup_assist_comp_msg3 = new System.Windows.Forms.Label();
            this.lbl_keyboard_setup_assist_comp_msg2 = new System.Windows.Forms.Label();
            this.lbl_keyboard_setup_assist_comp_msg1 = new System.Windows.Forms.Label();
            this.lbl_keyboard_setup_assist_msg2 = new System.Windows.Forms.Label();
            this.lbl_keyboard_setup_assist_msg1 = new System.Windows.Forms.Label();
            this.lbl_keyboard_setup_assist_title = new System.Windows.Forms.Label();
            this.pic_keyboard_setup_assist = new System.Windows.Forms.PictureBox();
            this.btn_keyboard_setup_set = new System.Windows.Forms.Button();
            this.llbl_keyboard_setup_help = new System.Windows.Forms.LinkLabel();
            this.btn_keyboard_setup_cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ScriptEditor)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_FlashMemory)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPagePattern.SuspendLayout();
            this.gbx_func_setup.SuspendLayout();
            this.gbx_func4_setup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_func4_y_ccw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func4_x_ccw)).BeginInit();
            this.groupBox14.SuspendLayout();
            this.groupBox15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_B_func4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_G_func4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_R_func4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func4_y_cw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func4_x_cw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func4_sensivity_ccw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func4_sensivity_cw)).BeginInit();
            this.gbx_func3_setup.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_B_func3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_G_func3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_R_func3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func3_sensivity_ccw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func3_y_ccw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func3_x_ccw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func3_sensivity_cw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func3_y_cw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func3_x_cw)).BeginInit();
            this.gbx_func2_setup.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_B_func2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_G_func2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_R_func2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func2_sensivity_ccw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func2_sensivity_cw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func2_y_ccw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func2_x_ccw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func2_y_cw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func2_x_cw)).BeginInit();
            this.gbx_func1_setup.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_B_func1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_G_func1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_R_func1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func1_sensivity_ccw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func1_sensivity_cw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func1_y_ccw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func1_x_ccw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func1_x_cw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func1_y_cw)).BeginInit();
            this.tabPageButton.SuspendLayout();
            this.gbx_encoder_button_setup.SuspendLayout();
            this.gbx_encoder_setup.SuspendLayout();
            this.gbx_LED_set.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_B)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_G)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_R)).BeginInit();
            this.gbx_button_setup.SuspendLayout();
            this.tabPageEncoderScript.SuspendLayout();
            this.gbx_base_setting.SuspendLayout();
            this.gbx_keyboard_setting.SuspendLayout();
            this.gbx_dial_func_LED_setting.SuspendLayout();
            this.gbx_mode_LED_setting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_mode_led_off_time)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_encoder_script)).BeginInit();
            this.gbx_Script_Add_Info_Mouse.SuspendLayout();
            this.gbx_Script_Add_Info_MultiMedia.SuspendLayout();
            this.gbx_MacroREC.SuspendLayout();
            this.gbx_MacroFileImportExport.SuspendLayout();
            this.gbx_Script_Add_Info_JoysticLever.SuspendLayout();
            this.gbx_Script_Add_Info_JoysticButton.SuspendLayout();
            this.gbx_Script_Add_Info.SuspendLayout();
            this.gbx_FileImportExport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ScriptList)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numud_led_brightness_level_set_val)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_led_g_set_val)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_led_r_set_val)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_led_b_set_val)).BeginInit();
            this.pnl_main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_mode_color)).BeginInit();
            this.contextMenuStrip_sw_func.SuspendLayout();
            this.gbx_system_backup.SuspendLayout();
            this.pnl_system_setup.SuspendLayout();
            this.pnl_dial_macro_editor.SuspendLayout();
            this.pnl_macro_editor.SuspendLayout();
            this.gbx_script_command_list.SuspendLayout();
            this.pnl_keyboard_type_assist.SuspendLayout();
            this.gbx_keyboard_setup_assis_complete.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_keyboard_setup_assist)).BeginInit();
            this.SuspendLayout();
            // 
            // StatusBox_lbl
            // 
            this.StatusBox_lbl.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.StatusBox_lbl, "StatusBox_lbl");
            this.StatusBox_lbl.Name = "StatusBox_lbl";
            // 
            // StatusBox_txtbx
            // 
            this.StatusBox_txtbx.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.StatusBox_txtbx, "StatusBox_txtbx");
            this.StatusBox_txtbx.Name = "StatusBox_txtbx";
            this.StatusBox_txtbx.ReadOnly = true;
            // 
            // ReadWriteThread
            // 
            this.ReadWriteThread.WorkerReportsProgress = true;
            this.ReadWriteThread.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ReadWriteThread_DoWork);
            // 
            // FormUpdateTimer
            // 
            this.FormUpdateTimer.Enabled = true;
            this.FormUpdateTimer.Interval = 50;
            this.FormUpdateTimer.Tick += new System.EventHandler(this.FormUpdateTimer_Tick);
            // 
            // colum_lbl
            // 
            resources.ApplyResources(this.colum_lbl, "colum_lbl");
            this.colum_lbl.BackColor = System.Drawing.Color.Black;
            this.colum_lbl.ForeColor = System.Drawing.Color.White;
            this.colum_lbl.Name = "colum_lbl";
            // 
            // Debug_label1
            // 
            resources.ApplyResources(this.Debug_label1, "Debug_label1");
            this.Debug_label1.BackColor = System.Drawing.Color.Black;
            this.Debug_label1.ForeColor = System.Drawing.Color.White;
            this.Debug_label1.Name = "Debug_label1";
            // 
            // Debug_label2
            // 
            resources.ApplyResources(this.Debug_label2, "Debug_label2");
            this.Debug_label2.BackColor = System.Drawing.Color.Black;
            this.Debug_label2.ForeColor = System.Drawing.Color.White;
            this.Debug_label2.Name = "Debug_label2";
            // 
            // Debug_label4
            // 
            resources.ApplyResources(this.Debug_label4, "Debug_label4");
            this.Debug_label4.BackColor = System.Drawing.Color.Black;
            this.Debug_label4.ForeColor = System.Drawing.Color.White;
            this.Debug_label4.Name = "Debug_label4";
            // 
            // Debug_label3
            // 
            resources.ApplyResources(this.Debug_label3, "Debug_label3");
            this.Debug_label3.BackColor = System.Drawing.Color.Black;
            this.Debug_label3.ForeColor = System.Drawing.Color.White;
            this.Debug_label3.Name = "Debug_label3";
            // 
            // txtbx_EraseAns
            // 
            resources.ApplyResources(this.txtbx_EraseAns, "txtbx_EraseAns");
            this.txtbx_EraseAns.Name = "txtbx_EraseAns";
            this.txtbx_EraseAns.ReadOnly = true;
            this.txtbx_EraseAns.TabStop = false;
            // 
            // btn_Erase
            // 
            resources.ApplyResources(this.btn_Erase, "btn_Erase");
            this.btn_Erase.Name = "btn_Erase";
            this.btn_Erase.TabStop = false;
            this.btn_Erase.UseVisualStyleBackColor = true;
            this.btn_Erase.Click += new System.EventHandler(this.btn_Erase_Click);
            // 
            // txtbx_EraseAddress
            // 
            resources.ApplyResources(this.txtbx_EraseAddress, "txtbx_EraseAddress");
            this.txtbx_EraseAddress.Name = "txtbx_EraseAddress";
            this.txtbx_EraseAddress.TabStop = false;
            // 
            // lbl_EraseAddress
            // 
            resources.ApplyResources(this.lbl_EraseAddress, "lbl_EraseAddress");
            this.lbl_EraseAddress.ForeColor = System.Drawing.Color.White;
            this.lbl_EraseAddress.Name = "lbl_EraseAddress";
            // 
            // lbl_Erase
            // 
            resources.ApplyResources(this.lbl_Erase, "lbl_Erase");
            this.lbl_Erase.ForeColor = System.Drawing.Color.White;
            this.lbl_Erase.Name = "lbl_Erase";
            // 
            // btn_Write
            // 
            resources.ApplyResources(this.btn_Write, "btn_Write");
            this.btn_Write.Name = "btn_Write";
            this.btn_Write.TabStop = false;
            this.btn_Write.UseVisualStyleBackColor = true;
            this.btn_Write.Click += new System.EventHandler(this.btn_Write_Click);
            // 
            // btn_Read
            // 
            resources.ApplyResources(this.btn_Read, "btn_Read");
            this.btn_Read.Name = "btn_Read";
            this.btn_Read.TabStop = false;
            this.btn_Read.UseVisualStyleBackColor = true;
            this.btn_Read.Click += new System.EventHandler(this.btn_Read_Click);
            // 
            // lbl_WriteData
            // 
            resources.ApplyResources(this.lbl_WriteData, "lbl_WriteData");
            this.lbl_WriteData.ForeColor = System.Drawing.Color.White;
            this.lbl_WriteData.Name = "lbl_WriteData";
            // 
            // lbl_ReadSize
            // 
            resources.ApplyResources(this.lbl_ReadSize, "lbl_ReadSize");
            this.lbl_ReadSize.ForeColor = System.Drawing.Color.White;
            this.lbl_ReadSize.Name = "lbl_ReadSize";
            // 
            // lbl_WriteAddress
            // 
            resources.ApplyResources(this.lbl_WriteAddress, "lbl_WriteAddress");
            this.lbl_WriteAddress.ForeColor = System.Drawing.Color.White;
            this.lbl_WriteAddress.Name = "lbl_WriteAddress";
            // 
            // lbl_ReadAddress
            // 
            resources.ApplyResources(this.lbl_ReadAddress, "lbl_ReadAddress");
            this.lbl_ReadAddress.ForeColor = System.Drawing.Color.White;
            this.lbl_ReadAddress.Name = "lbl_ReadAddress";
            // 
            // lbl_Write
            // 
            resources.ApplyResources(this.lbl_Write, "lbl_Write");
            this.lbl_Write.ForeColor = System.Drawing.Color.White;
            this.lbl_Write.Name = "lbl_Write";
            // 
            // lbl_Read
            // 
            resources.ApplyResources(this.lbl_Read, "lbl_Read");
            this.lbl_Read.ForeColor = System.Drawing.Color.White;
            this.lbl_Read.Name = "lbl_Read";
            // 
            // txtbx_WriteAns
            // 
            resources.ApplyResources(this.txtbx_WriteAns, "txtbx_WriteAns");
            this.txtbx_WriteAns.Name = "txtbx_WriteAns";
            this.txtbx_WriteAns.ReadOnly = true;
            this.txtbx_WriteAns.TabStop = false;
            // 
            // txtbx_WriteData
            // 
            resources.ApplyResources(this.txtbx_WriteData, "txtbx_WriteData");
            this.txtbx_WriteData.Name = "txtbx_WriteData";
            this.txtbx_WriteData.TabStop = false;
            // 
            // txtbx_WriteAddress
            // 
            resources.ApplyResources(this.txtbx_WriteAddress, "txtbx_WriteAddress");
            this.txtbx_WriteAddress.Name = "txtbx_WriteAddress";
            this.txtbx_WriteAddress.TabStop = false;
            // 
            // txtbx_ReadData
            // 
            resources.ApplyResources(this.txtbx_ReadData, "txtbx_ReadData");
            this.txtbx_ReadData.Name = "txtbx_ReadData";
            this.txtbx_ReadData.ReadOnly = true;
            this.txtbx_ReadData.TabStop = false;
            // 
            // txtbx_ReadSize
            // 
            resources.ApplyResources(this.txtbx_ReadSize, "txtbx_ReadSize");
            this.txtbx_ReadSize.Name = "txtbx_ReadSize";
            this.txtbx_ReadSize.TabStop = false;
            // 
            // txtbx_ReadAddress
            // 
            resources.ApplyResources(this.txtbx_ReadAddress, "txtbx_ReadAddress");
            this.txtbx_ReadAddress.Name = "txtbx_ReadAddress";
            this.txtbx_ReadAddress.TabStop = false;
            // 
            // dgv_ScriptEditor
            // 
            this.dgv_ScriptEditor.AllowDrop = true;
            this.dgv_ScriptEditor.AllowUserToAddRows = false;
            this.dgv_ScriptEditor.AllowUserToDeleteRows = false;
            this.dgv_ScriptEditor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgv_ScriptEditor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ScriptEditor.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.E_SIZE,
            this.E_NAME,
            this.E_VALUE});
            resources.ApplyResources(this.dgv_ScriptEditor, "dgv_ScriptEditor");
            this.dgv_ScriptEditor.MultiSelect = false;
            this.dgv_ScriptEditor.Name = "dgv_ScriptEditor";
            this.dgv_ScriptEditor.ReadOnly = true;
            this.dgv_ScriptEditor.RowHeadersVisible = false;
            this.dgv_ScriptEditor.RowTemplate.Height = 21;
            this.dgv_ScriptEditor.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_ScriptEditor.DragDrop += new System.Windows.Forms.DragEventHandler(this.dgv_ScriptEditor_DragDrop);
            this.dgv_ScriptEditor.DragOver += new System.Windows.Forms.DragEventHandler(this.dgv_ScriptEditor_DragOver);
            this.dgv_ScriptEditor.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgv_ScriptEditor_MouseDoubleClick);
            this.dgv_ScriptEditor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgv_ScriptEditor_MouseDown);
            this.dgv_ScriptEditor.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dgv_ScriptEditor_MouseMove);
            this.dgv_ScriptEditor.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgv_ScriptEditor_MouseUp);
            // 
            // E_SIZE
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.E_SIZE.DefaultCellStyle = dataGridViewCellStyle7;
            resources.ApplyResources(this.E_SIZE, "E_SIZE");
            this.E_SIZE.Name = "E_SIZE";
            this.E_SIZE.ReadOnly = true;
            this.E_SIZE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // E_NAME
            // 
            resources.ApplyResources(this.E_NAME, "E_NAME");
            this.E_NAME.Name = "E_NAME";
            this.E_NAME.ReadOnly = true;
            this.E_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // E_VALUE
            // 
            resources.ApplyResources(this.E_VALUE, "E_VALUE");
            this.E_VALUE.Name = "E_VALUE";
            this.E_VALUE.ReadOnly = true;
            this.E_VALUE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Black;
            this.groupBox1.Controls.Add(this.txtbx_debug01);
            this.groupBox1.Controls.Add(this.btn_debug4);
            this.groupBox1.Controls.Add(this.btn_debug3);
            this.groupBox1.Controls.Add(this.btn_debug2);
            this.groupBox1.Controls.Add(this.btn_debug1);
            this.groupBox1.Controls.Add(this.colum_lbl);
            this.groupBox1.Controls.Add(this.Debug_label3);
            this.groupBox1.Controls.Add(this.txtbx_EraseAns);
            this.groupBox1.Controls.Add(this.Debug_label4);
            this.groupBox1.Controls.Add(this.btn_Erase);
            this.groupBox1.Controls.Add(this.Debug_label2);
            this.groupBox1.Controls.Add(this.txtbx_EraseAddress);
            this.groupBox1.Controls.Add(this.lbl_EraseAddress);
            this.groupBox1.Controls.Add(this.Debug_label1);
            this.groupBox1.Controls.Add(this.lbl_Erase);
            this.groupBox1.Controls.Add(this.txtbx_ReadAddress);
            this.groupBox1.Controls.Add(this.btn_Write);
            this.groupBox1.Controls.Add(this.txtbx_ReadSize);
            this.groupBox1.Controls.Add(this.btn_Read);
            this.groupBox1.Controls.Add(this.txtbx_ReadData);
            this.groupBox1.Controls.Add(this.lbl_WriteData);
            this.groupBox1.Controls.Add(this.txtbx_WriteAddress);
            this.groupBox1.Controls.Add(this.lbl_ReadSize);
            this.groupBox1.Controls.Add(this.txtbx_WriteData);
            this.groupBox1.Controls.Add(this.lbl_WriteAddress);
            this.groupBox1.Controls.Add(this.txtbx_WriteAns);
            this.groupBox1.Controls.Add(this.lbl_ReadAddress);
            this.groupBox1.Controls.Add(this.lbl_Read);
            this.groupBox1.Controls.Add(this.lbl_Write);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // txtbx_debug01
            // 
            resources.ApplyResources(this.txtbx_debug01, "txtbx_debug01");
            this.txtbx_debug01.Name = "txtbx_debug01";
            // 
            // btn_debug4
            // 
            resources.ApplyResources(this.btn_debug4, "btn_debug4");
            this.btn_debug4.Name = "btn_debug4";
            this.btn_debug4.UseVisualStyleBackColor = true;
            this.btn_debug4.Click += new System.EventHandler(this.btn_debug4_Click);
            // 
            // btn_debug3
            // 
            resources.ApplyResources(this.btn_debug3, "btn_debug3");
            this.btn_debug3.Name = "btn_debug3";
            this.btn_debug3.UseVisualStyleBackColor = true;
            this.btn_debug3.Click += new System.EventHandler(this.btn_debug3_Click);
            // 
            // btn_debug2
            // 
            resources.ApplyResources(this.btn_debug2, "btn_debug2");
            this.btn_debug2.Name = "btn_debug2";
            this.btn_debug2.UseVisualStyleBackColor = true;
            this.btn_debug2.Click += new System.EventHandler(this.btn_debug2_Click);
            // 
            // btn_debug1
            // 
            resources.ApplyResources(this.btn_debug1, "btn_debug1");
            this.btn_debug1.Name = "btn_debug1";
            this.btn_debug1.UseVisualStyleBackColor = true;
            this.btn_debug1.Click += new System.EventHandler(this.btn_debug1_Click);
            // 
            // Script_Editor_Btn_imageList
            // 
            this.Script_Editor_Btn_imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("Script_Editor_Btn_imageList.ImageStream")));
            this.Script_Editor_Btn_imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.Script_Editor_Btn_imageList.Images.SetKeyName(0, "BN_DOUSA-KIROKU-KAISI.png");
            this.Script_Editor_Btn_imageList.Images.SetKeyName(1, "BN_DOUSA-KIROKU-KAISI_ACTIVE.png");
            this.Script_Editor_Btn_imageList.Images.SetKeyName(2, "BN_DOUSA-KIROKU-TEISI.png");
            this.Script_Editor_Btn_imageList.Images.SetKeyName(3, "BN_DOUSA-KIROKU-TEISI_ACTIVE.png");
            this.Script_Editor_Btn_imageList.Images.SetKeyName(4, "BN_DOUSA-SYOUKYO.png");
            this.Script_Editor_Btn_imageList.Images.SetKeyName(5, "BN_DOUSA-SYOUKYO_ACTIVE.png");
            // 
            // dgv_FlashMemory
            // 
            this.dgv_FlashMemory.AllowUserToAddRows = false;
            this.dgv_FlashMemory.AllowUserToDeleteRows = false;
            this.dgv_FlashMemory.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgv_FlashMemory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_FlashMemory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.F_NO,
            this.F_NAME,
            this.F_SIZE});
            resources.ApplyResources(this.dgv_FlashMemory, "dgv_FlashMemory");
            this.dgv_FlashMemory.MultiSelect = false;
            this.dgv_FlashMemory.Name = "dgv_FlashMemory";
            this.dgv_FlashMemory.ReadOnly = true;
            this.dgv_FlashMemory.RowHeadersVisible = false;
            this.dgv_FlashMemory.RowTemplate.Height = 21;
            this.dgv_FlashMemory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // F_NO
            // 
            resources.ApplyResources(this.F_NO, "F_NO");
            this.F_NO.Name = "F_NO";
            this.F_NO.ReadOnly = true;
            this.F_NO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // F_NAME
            // 
            resources.ApplyResources(this.F_NAME, "F_NAME");
            this.F_NAME.Name = "F_NAME";
            this.F_NAME.ReadOnly = true;
            this.F_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // F_SIZE
            // 
            resources.ApplyResources(this.F_SIZE, "F_SIZE");
            this.F_SIZE.Name = "F_SIZE";
            this.F_SIZE.ReadOnly = true;
            this.F_SIZE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // btn_Export
            // 
            resources.ApplyResources(this.btn_Export, "btn_Export");
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.UseVisualStyleBackColor = true;
            this.btn_Export.Click += new System.EventHandler(this.btn_Export_Click);
            // 
            // btn_ScriptRead
            // 
            resources.ApplyResources(this.btn_ScriptRead, "btn_ScriptRead");
            this.btn_ScriptRead.Name = "btn_ScriptRead";
            this.btn_ScriptRead.UseVisualStyleBackColor = true;
            this.btn_ScriptRead.Click += new System.EventHandler(this.btn_ScriptRead_Click);
            // 
            // btn_ScriptWrite
            // 
            resources.ApplyResources(this.btn_ScriptWrite, "btn_ScriptWrite");
            this.btn_ScriptWrite.Name = "btn_ScriptWrite";
            this.btn_ScriptWrite.UseVisualStyleBackColor = true;
            this.btn_ScriptWrite.Click += new System.EventHandler(this.btn_ScriptWrite_Click);
            // 
            // lbl_Script_Add_Info
            // 
            this.lbl_Script_Add_Info.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.lbl_Script_Add_Info, "lbl_Script_Add_Info");
            this.lbl_Script_Add_Info.ForeColor = System.Drawing.Color.White;
            this.lbl_Script_Add_Info.Name = "lbl_Script_Add_Info";
            this.lbl_Script_Add_Info.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.lbl_Script_Add_Info.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            // 
            // txtbx_Interval_Inputbox
            // 
            resources.ApplyResources(this.txtbx_Interval_Inputbox, "txtbx_Interval_Inputbox");
            this.txtbx_Interval_Inputbox.Name = "txtbx_Interval_Inputbox";
            this.txtbx_Interval_Inputbox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtbx_Interval_Inputbox_KeyUp);
            this.txtbx_Interval_Inputbox.Leave += new System.EventHandler(this.txtbx_Interval_Inputbox_Leave);
            // 
            // btn_Import
            // 
            resources.ApplyResources(this.btn_Import, "btn_Import");
            this.btn_Import.Name = "btn_Import";
            this.btn_Import.UseVisualStyleBackColor = true;
            this.btn_Import.Click += new System.EventHandler(this.btn_Import_Click);
            // 
            // ScriptFile_Save_saveFileDialog
            // 
            resources.ApplyResources(this.ScriptFile_Save_saveFileDialog, "ScriptFile_Save_saveFileDialog");
            // 
            // ScriptFile_Import_openFileDialog
            // 
            resources.ApplyResources(this.ScriptFile_Import_openFileDialog, "ScriptFile_Import_openFileDialog");
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.rtxtbx_debug_flash_read);
            this.groupBox5.Controls.Add(this.txtbx_debug_flash_read_size);
            this.groupBox5.Controls.Add(this.txtbx_debug_flash_read_addr);
            this.groupBox5.Controls.Add(this.btn_debug_flash_read);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.label8);
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            // 
            // rtxtbx_debug_flash_read
            // 
            resources.ApplyResources(this.rtxtbx_debug_flash_read, "rtxtbx_debug_flash_read");
            this.rtxtbx_debug_flash_read.Name = "rtxtbx_debug_flash_read";
            // 
            // txtbx_debug_flash_read_size
            // 
            resources.ApplyResources(this.txtbx_debug_flash_read_size, "txtbx_debug_flash_read_size");
            this.txtbx_debug_flash_read_size.Name = "txtbx_debug_flash_read_size";
            // 
            // txtbx_debug_flash_read_addr
            // 
            resources.ApplyResources(this.txtbx_debug_flash_read_addr, "txtbx_debug_flash_read_addr");
            this.txtbx_debug_flash_read_addr.Name = "txtbx_debug_flash_read_addr";
            // 
            // btn_debug_flash_read
            // 
            resources.ApplyResources(this.btn_debug_flash_read, "btn_debug_flash_read");
            this.btn_debug_flash_read.Name = "btn_debug_flash_read";
            this.btn_debug_flash_read.UseVisualStyleBackColor = true;
            this.btn_debug_flash_read.Click += new System.EventHandler(this.btn_debug_flash_read_Click);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageScript);
            this.tabControl1.Controls.Add(this.tabPagePattern);
            this.tabControl1.Controls.Add(this.tabPageButton);
            this.tabControl1.Controls.Add(this.tabPageEncoderScript);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPageScript
            // 
            resources.ApplyResources(this.tabPageScript, "tabPageScript");
            this.tabPageScript.Name = "tabPageScript";
            this.tabPageScript.UseVisualStyleBackColor = true;
            // 
            // tabPagePattern
            // 
            this.tabPagePattern.Controls.Add(this.lbl_func_mode3_select);
            this.tabPagePattern.Controls.Add(this.lbl_func_mode2_select);
            this.tabPagePattern.Controls.Add(this.lbl_func_mode1_select);
            this.tabPagePattern.Controls.Add(this.gbx_func_setup);
            this.tabPagePattern.Controls.Add(this.lbl_function_set_icon);
            resources.ApplyResources(this.tabPagePattern, "tabPagePattern");
            this.tabPagePattern.Name = "tabPagePattern";
            this.tabPagePattern.UseVisualStyleBackColor = true;
            // 
            // lbl_func_mode3_select
            // 
            this.lbl_func_mode3_select.BackColor = System.Drawing.Color.DarkGray;
            resources.ApplyResources(this.lbl_func_mode3_select, "lbl_func_mode3_select");
            this.lbl_func_mode3_select.Name = "lbl_func_mode3_select";
            this.lbl_func_mode3_select.Tag = "2";
            this.lbl_func_mode3_select.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_func_mode_select_MouseClick);
            // 
            // lbl_func_mode2_select
            // 
            this.lbl_func_mode2_select.BackColor = System.Drawing.Color.DarkGray;
            resources.ApplyResources(this.lbl_func_mode2_select, "lbl_func_mode2_select");
            this.lbl_func_mode2_select.Name = "lbl_func_mode2_select";
            this.lbl_func_mode2_select.Tag = "1";
            this.lbl_func_mode2_select.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_func_mode_select_MouseClick);
            // 
            // lbl_func_mode1_select
            // 
            this.lbl_func_mode1_select.BackColor = System.Drawing.Color.DarkGray;
            resources.ApplyResources(this.lbl_func_mode1_select, "lbl_func_mode1_select");
            this.lbl_func_mode1_select.Name = "lbl_func_mode1_select";
            this.lbl_func_mode1_select.Tag = "0";
            this.lbl_func_mode1_select.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_func_mode_select_MouseClick);
            // 
            // gbx_func_setup
            // 
            this.gbx_func_setup.Controls.Add(this.txtbx_function2_name);
            this.gbx_func_setup.Controls.Add(this.txtbx_function3_name);
            this.gbx_func_setup.Controls.Add(this.txtbx_function4_name);
            this.gbx_func_setup.Controls.Add(this.txtbx_function1_name);
            this.gbx_func_setup.Controls.Add(this.gbx_func4_setup);
            this.gbx_func_setup.Controls.Add(this.gbx_func3_setup);
            this.gbx_func_setup.Controls.Add(this.gbx_func2_setup);
            this.gbx_func_setup.Controls.Add(this.gbx_func1_setup);
            resources.ApplyResources(this.gbx_func_setup, "gbx_func_setup");
            this.gbx_func_setup.Name = "gbx_func_setup";
            this.gbx_func_setup.TabStop = false;
            // 
            // txtbx_function2_name
            // 
            resources.ApplyResources(this.txtbx_function2_name, "txtbx_function2_name");
            this.txtbx_function2_name.Name = "txtbx_function2_name";
            this.txtbx_function2_name.Tag = "1";
            // 
            // txtbx_function3_name
            // 
            resources.ApplyResources(this.txtbx_function3_name, "txtbx_function3_name");
            this.txtbx_function3_name.Name = "txtbx_function3_name";
            this.txtbx_function3_name.Tag = "2";
            // 
            // txtbx_function4_name
            // 
            resources.ApplyResources(this.txtbx_function4_name, "txtbx_function4_name");
            this.txtbx_function4_name.Name = "txtbx_function4_name";
            this.txtbx_function4_name.Tag = "3";
            // 
            // txtbx_function1_name
            // 
            resources.ApplyResources(this.txtbx_function1_name, "txtbx_function1_name");
            this.txtbx_function1_name.Name = "txtbx_function1_name";
            this.txtbx_function1_name.Tag = "0";
            // 
            // gbx_func4_setup
            // 
            this.gbx_func4_setup.Controls.Add(this.txtbx_func4_key_ccw);
            this.gbx_func4_setup.Controls.Add(this.btn_func4_key_clr_ccw);
            this.gbx_func4_setup.Controls.Add(this.btn_func4_key_clr_cw);
            this.gbx_func4_setup.Controls.Add(this.num_func4_y_ccw);
            this.gbx_func4_setup.Controls.Add(this.txtbx_func4_key_cw);
            this.gbx_func4_setup.Controls.Add(this.num_func4_x_ccw);
            this.gbx_func4_setup.Controls.Add(this.chk_func4_win_cw);
            this.gbx_func4_setup.Controls.Add(this.lbl_func4_title_ccw);
            this.gbx_func4_setup.Controls.Add(this.groupBox14);
            this.gbx_func4_setup.Controls.Add(this.num_func4_y_cw);
            this.gbx_func4_setup.Controls.Add(this.chk_func4_alt_cw);
            this.gbx_func4_setup.Controls.Add(this.num_func4_x_cw);
            this.gbx_func4_setup.Controls.Add(this.num_func4_sensivity_ccw);
            this.gbx_func4_setup.Controls.Add(this.lbl_func4_title_cw);
            this.gbx_func4_setup.Controls.Add(this.chk_func4_shift_cw);
            this.gbx_func4_setup.Controls.Add(this.label76);
            this.gbx_func4_setup.Controls.Add(this.chk_func4_ctrl_cw);
            this.gbx_func4_setup.Controls.Add(this.num_func4_sensivity_cw);
            this.gbx_func4_setup.Controls.Add(this.label77);
            this.gbx_func4_setup.Controls.Add(this.chk_func4_win_ccw);
            this.gbx_func4_setup.Controls.Add(this.cmbbx_func4_set_type_ccw);
            this.gbx_func4_setup.Controls.Add(this.chk_func4_alt_ccw);
            this.gbx_func4_setup.Controls.Add(this.label78);
            this.gbx_func4_setup.Controls.Add(this.chk_func4_shift_ccw);
            this.gbx_func4_setup.Controls.Add(this.cmbbx_func4_set_type_cw);
            this.gbx_func4_setup.Controls.Add(this.chk_func4_ctrl_ccw);
            this.gbx_func4_setup.Controls.Add(this.label79);
            resources.ApplyResources(this.gbx_func4_setup, "gbx_func4_setup");
            this.gbx_func4_setup.Name = "gbx_func4_setup";
            this.gbx_func4_setup.TabStop = false;
            // 
            // txtbx_func4_key_ccw
            // 
            this.txtbx_func4_key_ccw.ForeColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.txtbx_func4_key_ccw, "txtbx_func4_key_ccw");
            this.txtbx_func4_key_ccw.Name = "txtbx_func4_key_ccw";
            this.txtbx_func4_key_ccw.Tag = "7";
            this.txtbx_func4_key_ccw.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtbx_func_key_KeyDown);
            this.txtbx_func4_key_ccw.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtbx_func_key_KeyUp);
            this.txtbx_func4_key_ccw.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtbx_func_key_PreviewKeyDown);
            // 
            // btn_func4_key_clr_ccw
            // 
            resources.ApplyResources(this.btn_func4_key_clr_ccw, "btn_func4_key_clr_ccw");
            this.btn_func4_key_clr_ccw.Name = "btn_func4_key_clr_ccw";
            this.btn_func4_key_clr_ccw.Tag = "7";
            this.btn_func4_key_clr_ccw.UseVisualStyleBackColor = true;
            this.btn_func4_key_clr_ccw.Click += new System.EventHandler(this.btn_func_key_clr_Click);
            // 
            // btn_func4_key_clr_cw
            // 
            resources.ApplyResources(this.btn_func4_key_clr_cw, "btn_func4_key_clr_cw");
            this.btn_func4_key_clr_cw.Name = "btn_func4_key_clr_cw";
            this.btn_func4_key_clr_cw.Tag = "6";
            this.btn_func4_key_clr_cw.UseVisualStyleBackColor = true;
            this.btn_func4_key_clr_cw.Click += new System.EventHandler(this.btn_func_key_clr_Click);
            // 
            // num_func4_y_ccw
            // 
            resources.ApplyResources(this.num_func4_y_ccw, "num_func4_y_ccw");
            this.num_func4_y_ccw.Name = "num_func4_y_ccw";
            // 
            // txtbx_func4_key_cw
            // 
            this.txtbx_func4_key_cw.ForeColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.txtbx_func4_key_cw, "txtbx_func4_key_cw");
            this.txtbx_func4_key_cw.Name = "txtbx_func4_key_cw";
            this.txtbx_func4_key_cw.Tag = "6";
            this.txtbx_func4_key_cw.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtbx_func_key_KeyDown);
            this.txtbx_func4_key_cw.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtbx_func_key_KeyUp);
            this.txtbx_func4_key_cw.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtbx_func_key_PreviewKeyDown);
            // 
            // num_func4_x_ccw
            // 
            resources.ApplyResources(this.num_func4_x_ccw, "num_func4_x_ccw");
            this.num_func4_x_ccw.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.num_func4_x_ccw.Minimum = new decimal(new int[] {
            127,
            0,
            0,
            -2147483648});
            this.num_func4_x_ccw.Name = "num_func4_x_ccw";
            this.num_func4_x_ccw.Value = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            // 
            // chk_func4_win_cw
            // 
            resources.ApplyResources(this.chk_func4_win_cw, "chk_func4_win_cw");
            this.chk_func4_win_cw.Name = "chk_func4_win_cw";
            this.chk_func4_win_cw.UseVisualStyleBackColor = true;
            // 
            // lbl_func4_title_ccw
            // 
            resources.ApplyResources(this.lbl_func4_title_ccw, "lbl_func4_title_ccw");
            this.lbl_func4_title_ccw.Name = "lbl_func4_title_ccw";
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.rbtn_LED_Level_Light_func4);
            this.groupBox14.Controls.Add(this.lbl_LED_COLOR_NOW_func4);
            this.groupBox14.Controls.Add(this.rbtn_LED_Level_Normal_func4);
            this.groupBox14.Controls.Add(this.label19);
            this.groupBox14.Controls.Add(this.rbtn_LED_Level_Dark_func4);
            this.groupBox14.Controls.Add(this.btn_LED_preview_func4);
            this.groupBox14.Controls.Add(this.lbl_LED_COLOR_9_func4);
            this.groupBox14.Controls.Add(this.groupBox15);
            this.groupBox14.Controls.Add(this.lbl_LED_COLOR_8_func4);
            this.groupBox14.Controls.Add(this.lbl_LED_COLOR_7_func4);
            this.groupBox14.Controls.Add(this.lbl_LED_COLOR_6_func4);
            this.groupBox14.Controls.Add(this.lbl_LED_COLOR_5_func4);
            this.groupBox14.Controls.Add(this.lbl_LED_COLOR_4_func4);
            this.groupBox14.Controls.Add(this.lbl_LED_COLOR_3_func4);
            this.groupBox14.Controls.Add(this.lbl_LED_COLOR_2_func4);
            this.groupBox14.Controls.Add(this.lbl_LED_COLOR_1_func4);
            this.groupBox14.Controls.Add(this.label75);
            resources.ApplyResources(this.groupBox14, "groupBox14");
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.TabStop = false;
            // 
            // rbtn_LED_Level_Light_func4
            // 
            resources.ApplyResources(this.rbtn_LED_Level_Light_func4, "rbtn_LED_Level_Light_func4");
            this.rbtn_LED_Level_Light_func4.Name = "rbtn_LED_Level_Light_func4";
            this.rbtn_LED_Level_Light_func4.TabStop = true;
            this.rbtn_LED_Level_Light_func4.UseVisualStyleBackColor = true;
            // 
            // lbl_LED_COLOR_NOW_func4
            // 
            this.lbl_LED_COLOR_NOW_func4.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lbl_LED_COLOR_NOW_func4, "lbl_LED_COLOR_NOW_func4");
            this.lbl_LED_COLOR_NOW_func4.Name = "lbl_LED_COLOR_NOW_func4";
            this.lbl_LED_COLOR_NOW_func4.Tag = "1";
            // 
            // rbtn_LED_Level_Normal_func4
            // 
            resources.ApplyResources(this.rbtn_LED_Level_Normal_func4, "rbtn_LED_Level_Normal_func4");
            this.rbtn_LED_Level_Normal_func4.Name = "rbtn_LED_Level_Normal_func4";
            this.rbtn_LED_Level_Normal_func4.TabStop = true;
            this.rbtn_LED_Level_Normal_func4.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.DimGray;
            resources.ApplyResources(this.label19, "label19");
            this.label19.Name = "label19";
            // 
            // rbtn_LED_Level_Dark_func4
            // 
            resources.ApplyResources(this.rbtn_LED_Level_Dark_func4, "rbtn_LED_Level_Dark_func4");
            this.rbtn_LED_Level_Dark_func4.Name = "rbtn_LED_Level_Dark_func4";
            this.rbtn_LED_Level_Dark_func4.TabStop = true;
            this.rbtn_LED_Level_Dark_func4.UseVisualStyleBackColor = true;
            // 
            // btn_LED_preview_func4
            // 
            resources.ApplyResources(this.btn_LED_preview_func4, "btn_LED_preview_func4");
            this.btn_LED_preview_func4.Name = "btn_LED_preview_func4";
            this.btn_LED_preview_func4.Tag = "3";
            this.btn_LED_preview_func4.UseVisualStyleBackColor = true;
            this.btn_LED_preview_func4.Click += new System.EventHandler(this.btn_LED_preview_func_Click);
            // 
            // lbl_LED_COLOR_9_func4
            // 
            this.lbl_LED_COLOR_9_func4.BackColor = System.Drawing.Color.Black;
            this.lbl_LED_COLOR_9_func4.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lbl_LED_COLOR_9_func4, "lbl_LED_COLOR_9_func4");
            this.lbl_LED_COLOR_9_func4.Name = "lbl_LED_COLOR_9_func4";
            this.lbl_LED_COLOR_9_func4.Tag = "300";
            this.lbl_LED_COLOR_9_func4.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_func_MouseClick);
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.trackBar_B_func4);
            this.groupBox15.Controls.Add(this.trackBar_G_func4);
            this.groupBox15.Controls.Add(this.trackBar_R_func4);
            resources.ApplyResources(this.groupBox15, "groupBox15");
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.TabStop = false;
            // 
            // trackBar_B_func4
            // 
            resources.ApplyResources(this.trackBar_B_func4, "trackBar_B_func4");
            this.trackBar_B_func4.Maximum = 60;
            this.trackBar_B_func4.Name = "trackBar_B_func4";
            this.trackBar_B_func4.Tag = "32";
            this.trackBar_B_func4.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_B_func4.ValueChanged += new System.EventHandler(this.trackBar_RGB_func_ValueChanged);
            // 
            // trackBar_G_func4
            // 
            resources.ApplyResources(this.trackBar_G_func4, "trackBar_G_func4");
            this.trackBar_G_func4.Maximum = 60;
            this.trackBar_G_func4.Name = "trackBar_G_func4";
            this.trackBar_G_func4.Tag = "31";
            this.trackBar_G_func4.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_G_func4.ValueChanged += new System.EventHandler(this.trackBar_RGB_func_ValueChanged);
            // 
            // trackBar_R_func4
            // 
            resources.ApplyResources(this.trackBar_R_func4, "trackBar_R_func4");
            this.trackBar_R_func4.Maximum = 60;
            this.trackBar_R_func4.Name = "trackBar_R_func4";
            this.trackBar_R_func4.Tag = "30";
            this.trackBar_R_func4.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_R_func4.ValueChanged += new System.EventHandler(this.trackBar_RGB_func_ValueChanged);
            // 
            // lbl_LED_COLOR_8_func4
            // 
            this.lbl_LED_COLOR_8_func4.BackColor = System.Drawing.Color.Purple;
            resources.ApplyResources(this.lbl_LED_COLOR_8_func4, "lbl_LED_COLOR_8_func4");
            this.lbl_LED_COLOR_8_func4.Name = "lbl_LED_COLOR_8_func4";
            this.lbl_LED_COLOR_8_func4.Tag = "308";
            this.lbl_LED_COLOR_8_func4.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_func_MouseClick);
            // 
            // lbl_LED_COLOR_7_func4
            // 
            this.lbl_LED_COLOR_7_func4.BackColor = System.Drawing.Color.Blue;
            resources.ApplyResources(this.lbl_LED_COLOR_7_func4, "lbl_LED_COLOR_7_func4");
            this.lbl_LED_COLOR_7_func4.Name = "lbl_LED_COLOR_7_func4";
            this.lbl_LED_COLOR_7_func4.Tag = "307";
            this.lbl_LED_COLOR_7_func4.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_func_MouseClick);
            // 
            // lbl_LED_COLOR_6_func4
            // 
            this.lbl_LED_COLOR_6_func4.BackColor = System.Drawing.Color.Green;
            resources.ApplyResources(this.lbl_LED_COLOR_6_func4, "lbl_LED_COLOR_6_func4");
            this.lbl_LED_COLOR_6_func4.Name = "lbl_LED_COLOR_6_func4";
            this.lbl_LED_COLOR_6_func4.Tag = "306";
            this.lbl_LED_COLOR_6_func4.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_func_MouseClick);
            // 
            // lbl_LED_COLOR_5_func4
            // 
            this.lbl_LED_COLOR_5_func4.BackColor = System.Drawing.Color.Turquoise;
            resources.ApplyResources(this.lbl_LED_COLOR_5_func4, "lbl_LED_COLOR_5_func4");
            this.lbl_LED_COLOR_5_func4.Name = "lbl_LED_COLOR_5_func4";
            this.lbl_LED_COLOR_5_func4.Tag = "305";
            this.lbl_LED_COLOR_5_func4.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_func_MouseClick);
            // 
            // lbl_LED_COLOR_4_func4
            // 
            this.lbl_LED_COLOR_4_func4.BackColor = System.Drawing.Color.Yellow;
            resources.ApplyResources(this.lbl_LED_COLOR_4_func4, "lbl_LED_COLOR_4_func4");
            this.lbl_LED_COLOR_4_func4.Name = "lbl_LED_COLOR_4_func4";
            this.lbl_LED_COLOR_4_func4.Tag = "304";
            this.lbl_LED_COLOR_4_func4.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_func_MouseClick);
            // 
            // lbl_LED_COLOR_3_func4
            // 
            this.lbl_LED_COLOR_3_func4.BackColor = System.Drawing.Color.Orange;
            resources.ApplyResources(this.lbl_LED_COLOR_3_func4, "lbl_LED_COLOR_3_func4");
            this.lbl_LED_COLOR_3_func4.Name = "lbl_LED_COLOR_3_func4";
            this.lbl_LED_COLOR_3_func4.Tag = "303";
            this.lbl_LED_COLOR_3_func4.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_func_MouseClick);
            // 
            // lbl_LED_COLOR_2_func4
            // 
            this.lbl_LED_COLOR_2_func4.BackColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.lbl_LED_COLOR_2_func4, "lbl_LED_COLOR_2_func4");
            this.lbl_LED_COLOR_2_func4.Name = "lbl_LED_COLOR_2_func4";
            this.lbl_LED_COLOR_2_func4.Tag = "302";
            this.lbl_LED_COLOR_2_func4.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_func_MouseClick);
            // 
            // lbl_LED_COLOR_1_func4
            // 
            this.lbl_LED_COLOR_1_func4.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lbl_LED_COLOR_1_func4, "lbl_LED_COLOR_1_func4");
            this.lbl_LED_COLOR_1_func4.Name = "lbl_LED_COLOR_1_func4";
            this.lbl_LED_COLOR_1_func4.Tag = "301";
            this.lbl_LED_COLOR_1_func4.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_func_MouseClick);
            // 
            // label75
            // 
            this.label75.BackColor = System.Drawing.Color.DimGray;
            resources.ApplyResources(this.label75, "label75");
            this.label75.Name = "label75";
            // 
            // num_func4_y_cw
            // 
            resources.ApplyResources(this.num_func4_y_cw, "num_func4_y_cw");
            this.num_func4_y_cw.Name = "num_func4_y_cw";
            // 
            // chk_func4_alt_cw
            // 
            resources.ApplyResources(this.chk_func4_alt_cw, "chk_func4_alt_cw");
            this.chk_func4_alt_cw.Name = "chk_func4_alt_cw";
            this.chk_func4_alt_cw.UseVisualStyleBackColor = true;
            // 
            // num_func4_x_cw
            // 
            resources.ApplyResources(this.num_func4_x_cw, "num_func4_x_cw");
            this.num_func4_x_cw.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.num_func4_x_cw.Minimum = new decimal(new int[] {
            127,
            0,
            0,
            -2147483648});
            this.num_func4_x_cw.Name = "num_func4_x_cw";
            this.num_func4_x_cw.Value = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            // 
            // num_func4_sensivity_ccw
            // 
            resources.ApplyResources(this.num_func4_sensivity_ccw, "num_func4_sensivity_ccw");
            this.num_func4_sensivity_ccw.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_func4_sensivity_ccw.Name = "num_func4_sensivity_ccw";
            this.num_func4_sensivity_ccw.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // lbl_func4_title_cw
            // 
            resources.ApplyResources(this.lbl_func4_title_cw, "lbl_func4_title_cw");
            this.lbl_func4_title_cw.Name = "lbl_func4_title_cw";
            // 
            // chk_func4_shift_cw
            // 
            resources.ApplyResources(this.chk_func4_shift_cw, "chk_func4_shift_cw");
            this.chk_func4_shift_cw.Name = "chk_func4_shift_cw";
            this.chk_func4_shift_cw.UseVisualStyleBackColor = true;
            // 
            // label76
            // 
            resources.ApplyResources(this.label76, "label76");
            this.label76.Name = "label76";
            // 
            // chk_func4_ctrl_cw
            // 
            resources.ApplyResources(this.chk_func4_ctrl_cw, "chk_func4_ctrl_cw");
            this.chk_func4_ctrl_cw.Name = "chk_func4_ctrl_cw";
            this.chk_func4_ctrl_cw.UseVisualStyleBackColor = true;
            // 
            // num_func4_sensivity_cw
            // 
            resources.ApplyResources(this.num_func4_sensivity_cw, "num_func4_sensivity_cw");
            this.num_func4_sensivity_cw.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_func4_sensivity_cw.Name = "num_func4_sensivity_cw";
            this.num_func4_sensivity_cw.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label77
            // 
            resources.ApplyResources(this.label77, "label77");
            this.label77.Name = "label77";
            // 
            // chk_func4_win_ccw
            // 
            resources.ApplyResources(this.chk_func4_win_ccw, "chk_func4_win_ccw");
            this.chk_func4_win_ccw.Name = "chk_func4_win_ccw";
            this.chk_func4_win_ccw.UseVisualStyleBackColor = true;
            // 
            // cmbbx_func4_set_type_ccw
            // 
            this.cmbbx_func4_set_type_ccw.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbx_func4_set_type_ccw.FormattingEnabled = true;
            resources.ApplyResources(this.cmbbx_func4_set_type_ccw, "cmbbx_func4_set_type_ccw");
            this.cmbbx_func4_set_type_ccw.Name = "cmbbx_func4_set_type_ccw";
            this.cmbbx_func4_set_type_ccw.Tag = "7";
            this.cmbbx_func4_set_type_ccw.SelectedIndexChanged += new System.EventHandler(this.cmbbx_func_set_type_SelectedIndexChanged);
            // 
            // chk_func4_alt_ccw
            // 
            resources.ApplyResources(this.chk_func4_alt_ccw, "chk_func4_alt_ccw");
            this.chk_func4_alt_ccw.Name = "chk_func4_alt_ccw";
            this.chk_func4_alt_ccw.UseVisualStyleBackColor = true;
            // 
            // label78
            // 
            resources.ApplyResources(this.label78, "label78");
            this.label78.Name = "label78";
            // 
            // chk_func4_shift_ccw
            // 
            resources.ApplyResources(this.chk_func4_shift_ccw, "chk_func4_shift_ccw");
            this.chk_func4_shift_ccw.Name = "chk_func4_shift_ccw";
            this.chk_func4_shift_ccw.UseVisualStyleBackColor = true;
            // 
            // cmbbx_func4_set_type_cw
            // 
            this.cmbbx_func4_set_type_cw.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbx_func4_set_type_cw.FormattingEnabled = true;
            resources.ApplyResources(this.cmbbx_func4_set_type_cw, "cmbbx_func4_set_type_cw");
            this.cmbbx_func4_set_type_cw.Name = "cmbbx_func4_set_type_cw";
            this.cmbbx_func4_set_type_cw.Tag = "6";
            this.cmbbx_func4_set_type_cw.SelectedIndexChanged += new System.EventHandler(this.cmbbx_func_set_type_SelectedIndexChanged);
            // 
            // chk_func4_ctrl_ccw
            // 
            resources.ApplyResources(this.chk_func4_ctrl_ccw, "chk_func4_ctrl_ccw");
            this.chk_func4_ctrl_ccw.Name = "chk_func4_ctrl_ccw";
            this.chk_func4_ctrl_ccw.UseVisualStyleBackColor = true;
            // 
            // label79
            // 
            resources.ApplyResources(this.label79, "label79");
            this.label79.Name = "label79";
            // 
            // gbx_func3_setup
            // 
            this.gbx_func3_setup.Controls.Add(this.txtbx_func3_key_ccw);
            this.gbx_func3_setup.Controls.Add(this.btn_func3_key_clr_ccw);
            this.gbx_func3_setup.Controls.Add(this.btn_func3_key_clr_cw);
            this.gbx_func3_setup.Controls.Add(this.txtbx_func3_key_cw);
            this.gbx_func3_setup.Controls.Add(this.chk_func3_win_cw);
            this.gbx_func3_setup.Controls.Add(this.groupBox11);
            this.gbx_func3_setup.Controls.Add(this.chk_func3_alt_cw);
            this.gbx_func3_setup.Controls.Add(this.num_func3_sensivity_ccw);
            this.gbx_func3_setup.Controls.Add(this.chk_func3_shift_cw);
            this.gbx_func3_setup.Controls.Add(this.num_func3_y_ccw);
            this.gbx_func3_setup.Controls.Add(this.label62);
            this.gbx_func3_setup.Controls.Add(this.num_func3_x_ccw);
            this.gbx_func3_setup.Controls.Add(this.chk_func3_ctrl_cw);
            this.gbx_func3_setup.Controls.Add(this.lbl_func3_title_ccw);
            this.gbx_func3_setup.Controls.Add(this.num_func3_sensivity_cw);
            this.gbx_func3_setup.Controls.Add(this.num_func3_y_cw);
            this.gbx_func3_setup.Controls.Add(this.num_func3_x_cw);
            this.gbx_func3_setup.Controls.Add(this.label63);
            this.gbx_func3_setup.Controls.Add(this.lbl_func3_title_cw);
            this.gbx_func3_setup.Controls.Add(this.chk_func3_win_ccw);
            this.gbx_func3_setup.Controls.Add(this.cmbbx_func3_set_type_ccw);
            this.gbx_func3_setup.Controls.Add(this.chk_func3_alt_ccw);
            this.gbx_func3_setup.Controls.Add(this.label64);
            this.gbx_func3_setup.Controls.Add(this.chk_func3_shift_ccw);
            this.gbx_func3_setup.Controls.Add(this.cmbbx_func3_set_type_cw);
            this.gbx_func3_setup.Controls.Add(this.chk_func3_ctrl_ccw);
            this.gbx_func3_setup.Controls.Add(this.label65);
            resources.ApplyResources(this.gbx_func3_setup, "gbx_func3_setup");
            this.gbx_func3_setup.Name = "gbx_func3_setup";
            this.gbx_func3_setup.TabStop = false;
            // 
            // txtbx_func3_key_ccw
            // 
            this.txtbx_func3_key_ccw.ForeColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.txtbx_func3_key_ccw, "txtbx_func3_key_ccw");
            this.txtbx_func3_key_ccw.Name = "txtbx_func3_key_ccw";
            this.txtbx_func3_key_ccw.Tag = "5";
            this.txtbx_func3_key_ccw.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtbx_func_key_KeyDown);
            this.txtbx_func3_key_ccw.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtbx_func_key_KeyUp);
            this.txtbx_func3_key_ccw.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtbx_func_key_PreviewKeyDown);
            // 
            // btn_func3_key_clr_ccw
            // 
            resources.ApplyResources(this.btn_func3_key_clr_ccw, "btn_func3_key_clr_ccw");
            this.btn_func3_key_clr_ccw.Name = "btn_func3_key_clr_ccw";
            this.btn_func3_key_clr_ccw.Tag = "5";
            this.btn_func3_key_clr_ccw.UseVisualStyleBackColor = true;
            this.btn_func3_key_clr_ccw.Click += new System.EventHandler(this.btn_func_key_clr_Click);
            // 
            // btn_func3_key_clr_cw
            // 
            resources.ApplyResources(this.btn_func3_key_clr_cw, "btn_func3_key_clr_cw");
            this.btn_func3_key_clr_cw.Name = "btn_func3_key_clr_cw";
            this.btn_func3_key_clr_cw.Tag = "4";
            this.btn_func3_key_clr_cw.UseVisualStyleBackColor = true;
            this.btn_func3_key_clr_cw.Click += new System.EventHandler(this.btn_func_key_clr_Click);
            // 
            // txtbx_func3_key_cw
            // 
            this.txtbx_func3_key_cw.ForeColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.txtbx_func3_key_cw, "txtbx_func3_key_cw");
            this.txtbx_func3_key_cw.Name = "txtbx_func3_key_cw";
            this.txtbx_func3_key_cw.Tag = "4";
            this.txtbx_func3_key_cw.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtbx_func_key_KeyDown);
            this.txtbx_func3_key_cw.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtbx_func_key_KeyUp);
            this.txtbx_func3_key_cw.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtbx_func_key_PreviewKeyDown);
            // 
            // chk_func3_win_cw
            // 
            resources.ApplyResources(this.chk_func3_win_cw, "chk_func3_win_cw");
            this.chk_func3_win_cw.Name = "chk_func3_win_cw";
            this.chk_func3_win_cw.UseVisualStyleBackColor = true;
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.rbtn_LED_Level_Light_func3);
            this.groupBox11.Controls.Add(this.lbl_LED_COLOR_NOW_func3);
            this.groupBox11.Controls.Add(this.rbtn_LED_Level_Normal_func3);
            this.groupBox11.Controls.Add(this.label14);
            this.groupBox11.Controls.Add(this.rbtn_LED_Level_Dark_func3);
            this.groupBox11.Controls.Add(this.btn_LED_preview_func3);
            this.groupBox11.Controls.Add(this.lbl_LED_COLOR_9_func3);
            this.groupBox11.Controls.Add(this.groupBox12);
            this.groupBox11.Controls.Add(this.lbl_LED_COLOR_8_func3);
            this.groupBox11.Controls.Add(this.lbl_LED_COLOR_7_func3);
            this.groupBox11.Controls.Add(this.lbl_LED_COLOR_6_func3);
            this.groupBox11.Controls.Add(this.lbl_LED_COLOR_5_func3);
            this.groupBox11.Controls.Add(this.lbl_LED_COLOR_4_func3);
            this.groupBox11.Controls.Add(this.lbl_LED_COLOR_3_func3);
            this.groupBox11.Controls.Add(this.lbl_LED_COLOR_2_func3);
            this.groupBox11.Controls.Add(this.lbl_LED_COLOR_1_func3);
            this.groupBox11.Controls.Add(this.label61);
            resources.ApplyResources(this.groupBox11, "groupBox11");
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.TabStop = false;
            // 
            // rbtn_LED_Level_Light_func3
            // 
            resources.ApplyResources(this.rbtn_LED_Level_Light_func3, "rbtn_LED_Level_Light_func3");
            this.rbtn_LED_Level_Light_func3.Name = "rbtn_LED_Level_Light_func3";
            this.rbtn_LED_Level_Light_func3.TabStop = true;
            this.rbtn_LED_Level_Light_func3.UseVisualStyleBackColor = true;
            // 
            // lbl_LED_COLOR_NOW_func3
            // 
            this.lbl_LED_COLOR_NOW_func3.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lbl_LED_COLOR_NOW_func3, "lbl_LED_COLOR_NOW_func3");
            this.lbl_LED_COLOR_NOW_func3.Name = "lbl_LED_COLOR_NOW_func3";
            this.lbl_LED_COLOR_NOW_func3.Tag = "1";
            // 
            // rbtn_LED_Level_Normal_func3
            // 
            resources.ApplyResources(this.rbtn_LED_Level_Normal_func3, "rbtn_LED_Level_Normal_func3");
            this.rbtn_LED_Level_Normal_func3.Name = "rbtn_LED_Level_Normal_func3";
            this.rbtn_LED_Level_Normal_func3.TabStop = true;
            this.rbtn_LED_Level_Normal_func3.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.DimGray;
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // rbtn_LED_Level_Dark_func3
            // 
            resources.ApplyResources(this.rbtn_LED_Level_Dark_func3, "rbtn_LED_Level_Dark_func3");
            this.rbtn_LED_Level_Dark_func3.Name = "rbtn_LED_Level_Dark_func3";
            this.rbtn_LED_Level_Dark_func3.TabStop = true;
            this.rbtn_LED_Level_Dark_func3.UseVisualStyleBackColor = true;
            // 
            // btn_LED_preview_func3
            // 
            resources.ApplyResources(this.btn_LED_preview_func3, "btn_LED_preview_func3");
            this.btn_LED_preview_func3.Name = "btn_LED_preview_func3";
            this.btn_LED_preview_func3.Tag = "2";
            this.btn_LED_preview_func3.UseVisualStyleBackColor = true;
            this.btn_LED_preview_func3.Click += new System.EventHandler(this.btn_LED_preview_func_Click);
            // 
            // lbl_LED_COLOR_9_func3
            // 
            this.lbl_LED_COLOR_9_func3.BackColor = System.Drawing.Color.Black;
            this.lbl_LED_COLOR_9_func3.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lbl_LED_COLOR_9_func3, "lbl_LED_COLOR_9_func3");
            this.lbl_LED_COLOR_9_func3.Name = "lbl_LED_COLOR_9_func3";
            this.lbl_LED_COLOR_9_func3.Tag = "200";
            this.lbl_LED_COLOR_9_func3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_func_MouseClick);
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.trackBar_B_func3);
            this.groupBox12.Controls.Add(this.trackBar_G_func3);
            this.groupBox12.Controls.Add(this.trackBar_R_func3);
            resources.ApplyResources(this.groupBox12, "groupBox12");
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.TabStop = false;
            // 
            // trackBar_B_func3
            // 
            resources.ApplyResources(this.trackBar_B_func3, "trackBar_B_func3");
            this.trackBar_B_func3.Maximum = 60;
            this.trackBar_B_func3.Name = "trackBar_B_func3";
            this.trackBar_B_func3.Tag = "22";
            this.trackBar_B_func3.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_B_func3.ValueChanged += new System.EventHandler(this.trackBar_RGB_func_ValueChanged);
            // 
            // trackBar_G_func3
            // 
            resources.ApplyResources(this.trackBar_G_func3, "trackBar_G_func3");
            this.trackBar_G_func3.Maximum = 60;
            this.trackBar_G_func3.Name = "trackBar_G_func3";
            this.trackBar_G_func3.Tag = "21";
            this.trackBar_G_func3.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_G_func3.ValueChanged += new System.EventHandler(this.trackBar_RGB_func_ValueChanged);
            // 
            // trackBar_R_func3
            // 
            resources.ApplyResources(this.trackBar_R_func3, "trackBar_R_func3");
            this.trackBar_R_func3.Maximum = 60;
            this.trackBar_R_func3.Name = "trackBar_R_func3";
            this.trackBar_R_func3.Tag = "20";
            this.trackBar_R_func3.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_R_func3.ValueChanged += new System.EventHandler(this.trackBar_RGB_func_ValueChanged);
            // 
            // lbl_LED_COLOR_8_func3
            // 
            this.lbl_LED_COLOR_8_func3.BackColor = System.Drawing.Color.Purple;
            resources.ApplyResources(this.lbl_LED_COLOR_8_func3, "lbl_LED_COLOR_8_func3");
            this.lbl_LED_COLOR_8_func3.Name = "lbl_LED_COLOR_8_func3";
            this.lbl_LED_COLOR_8_func3.Tag = "208";
            this.lbl_LED_COLOR_8_func3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_func_MouseClick);
            // 
            // lbl_LED_COLOR_7_func3
            // 
            this.lbl_LED_COLOR_7_func3.BackColor = System.Drawing.Color.Blue;
            resources.ApplyResources(this.lbl_LED_COLOR_7_func3, "lbl_LED_COLOR_7_func3");
            this.lbl_LED_COLOR_7_func3.Name = "lbl_LED_COLOR_7_func3";
            this.lbl_LED_COLOR_7_func3.Tag = "207";
            this.lbl_LED_COLOR_7_func3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_func_MouseClick);
            // 
            // lbl_LED_COLOR_6_func3
            // 
            this.lbl_LED_COLOR_6_func3.BackColor = System.Drawing.Color.Green;
            resources.ApplyResources(this.lbl_LED_COLOR_6_func3, "lbl_LED_COLOR_6_func3");
            this.lbl_LED_COLOR_6_func3.Name = "lbl_LED_COLOR_6_func3";
            this.lbl_LED_COLOR_6_func3.Tag = "206";
            this.lbl_LED_COLOR_6_func3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_func_MouseClick);
            // 
            // lbl_LED_COLOR_5_func3
            // 
            this.lbl_LED_COLOR_5_func3.BackColor = System.Drawing.Color.Turquoise;
            resources.ApplyResources(this.lbl_LED_COLOR_5_func3, "lbl_LED_COLOR_5_func3");
            this.lbl_LED_COLOR_5_func3.Name = "lbl_LED_COLOR_5_func3";
            this.lbl_LED_COLOR_5_func3.Tag = "205";
            this.lbl_LED_COLOR_5_func3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_func_MouseClick);
            // 
            // lbl_LED_COLOR_4_func3
            // 
            this.lbl_LED_COLOR_4_func3.BackColor = System.Drawing.Color.Yellow;
            resources.ApplyResources(this.lbl_LED_COLOR_4_func3, "lbl_LED_COLOR_4_func3");
            this.lbl_LED_COLOR_4_func3.Name = "lbl_LED_COLOR_4_func3";
            this.lbl_LED_COLOR_4_func3.Tag = "204";
            this.lbl_LED_COLOR_4_func3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_func_MouseClick);
            // 
            // lbl_LED_COLOR_3_func3
            // 
            this.lbl_LED_COLOR_3_func3.BackColor = System.Drawing.Color.Orange;
            resources.ApplyResources(this.lbl_LED_COLOR_3_func3, "lbl_LED_COLOR_3_func3");
            this.lbl_LED_COLOR_3_func3.Name = "lbl_LED_COLOR_3_func3";
            this.lbl_LED_COLOR_3_func3.Tag = "203";
            this.lbl_LED_COLOR_3_func3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_func_MouseClick);
            // 
            // lbl_LED_COLOR_2_func3
            // 
            this.lbl_LED_COLOR_2_func3.BackColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.lbl_LED_COLOR_2_func3, "lbl_LED_COLOR_2_func3");
            this.lbl_LED_COLOR_2_func3.Name = "lbl_LED_COLOR_2_func3";
            this.lbl_LED_COLOR_2_func3.Tag = "202";
            this.lbl_LED_COLOR_2_func3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_func_MouseClick);
            // 
            // lbl_LED_COLOR_1_func3
            // 
            this.lbl_LED_COLOR_1_func3.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lbl_LED_COLOR_1_func3, "lbl_LED_COLOR_1_func3");
            this.lbl_LED_COLOR_1_func3.Name = "lbl_LED_COLOR_1_func3";
            this.lbl_LED_COLOR_1_func3.Tag = "201";
            this.lbl_LED_COLOR_1_func3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_func_MouseClick);
            // 
            // label61
            // 
            this.label61.BackColor = System.Drawing.Color.DimGray;
            resources.ApplyResources(this.label61, "label61");
            this.label61.Name = "label61";
            // 
            // chk_func3_alt_cw
            // 
            resources.ApplyResources(this.chk_func3_alt_cw, "chk_func3_alt_cw");
            this.chk_func3_alt_cw.Name = "chk_func3_alt_cw";
            this.chk_func3_alt_cw.UseVisualStyleBackColor = true;
            // 
            // num_func3_sensivity_ccw
            // 
            resources.ApplyResources(this.num_func3_sensivity_ccw, "num_func3_sensivity_ccw");
            this.num_func3_sensivity_ccw.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_func3_sensivity_ccw.Name = "num_func3_sensivity_ccw";
            this.num_func3_sensivity_ccw.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // chk_func3_shift_cw
            // 
            resources.ApplyResources(this.chk_func3_shift_cw, "chk_func3_shift_cw");
            this.chk_func3_shift_cw.Name = "chk_func3_shift_cw";
            this.chk_func3_shift_cw.UseVisualStyleBackColor = true;
            // 
            // num_func3_y_ccw
            // 
            resources.ApplyResources(this.num_func3_y_ccw, "num_func3_y_ccw");
            this.num_func3_y_ccw.Name = "num_func3_y_ccw";
            // 
            // label62
            // 
            resources.ApplyResources(this.label62, "label62");
            this.label62.Name = "label62";
            // 
            // num_func3_x_ccw
            // 
            resources.ApplyResources(this.num_func3_x_ccw, "num_func3_x_ccw");
            this.num_func3_x_ccw.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.num_func3_x_ccw.Minimum = new decimal(new int[] {
            127,
            0,
            0,
            -2147483648});
            this.num_func3_x_ccw.Name = "num_func3_x_ccw";
            this.num_func3_x_ccw.Value = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            // 
            // chk_func3_ctrl_cw
            // 
            resources.ApplyResources(this.chk_func3_ctrl_cw, "chk_func3_ctrl_cw");
            this.chk_func3_ctrl_cw.Name = "chk_func3_ctrl_cw";
            this.chk_func3_ctrl_cw.UseVisualStyleBackColor = true;
            // 
            // lbl_func3_title_ccw
            // 
            resources.ApplyResources(this.lbl_func3_title_ccw, "lbl_func3_title_ccw");
            this.lbl_func3_title_ccw.Name = "lbl_func3_title_ccw";
            // 
            // num_func3_sensivity_cw
            // 
            resources.ApplyResources(this.num_func3_sensivity_cw, "num_func3_sensivity_cw");
            this.num_func3_sensivity_cw.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_func3_sensivity_cw.Name = "num_func3_sensivity_cw";
            this.num_func3_sensivity_cw.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // num_func3_y_cw
            // 
            resources.ApplyResources(this.num_func3_y_cw, "num_func3_y_cw");
            this.num_func3_y_cw.Name = "num_func3_y_cw";
            // 
            // num_func3_x_cw
            // 
            resources.ApplyResources(this.num_func3_x_cw, "num_func3_x_cw");
            this.num_func3_x_cw.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.num_func3_x_cw.Minimum = new decimal(new int[] {
            127,
            0,
            0,
            -2147483648});
            this.num_func3_x_cw.Name = "num_func3_x_cw";
            this.num_func3_x_cw.Value = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            // 
            // label63
            // 
            resources.ApplyResources(this.label63, "label63");
            this.label63.Name = "label63";
            // 
            // lbl_func3_title_cw
            // 
            resources.ApplyResources(this.lbl_func3_title_cw, "lbl_func3_title_cw");
            this.lbl_func3_title_cw.Name = "lbl_func3_title_cw";
            // 
            // chk_func3_win_ccw
            // 
            resources.ApplyResources(this.chk_func3_win_ccw, "chk_func3_win_ccw");
            this.chk_func3_win_ccw.Name = "chk_func3_win_ccw";
            this.chk_func3_win_ccw.UseVisualStyleBackColor = true;
            // 
            // cmbbx_func3_set_type_ccw
            // 
            this.cmbbx_func3_set_type_ccw.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbx_func3_set_type_ccw.FormattingEnabled = true;
            resources.ApplyResources(this.cmbbx_func3_set_type_ccw, "cmbbx_func3_set_type_ccw");
            this.cmbbx_func3_set_type_ccw.Name = "cmbbx_func3_set_type_ccw";
            this.cmbbx_func3_set_type_ccw.Tag = "5";
            this.cmbbx_func3_set_type_ccw.SelectedIndexChanged += new System.EventHandler(this.cmbbx_func_set_type_SelectedIndexChanged);
            // 
            // chk_func3_alt_ccw
            // 
            resources.ApplyResources(this.chk_func3_alt_ccw, "chk_func3_alt_ccw");
            this.chk_func3_alt_ccw.Name = "chk_func3_alt_ccw";
            this.chk_func3_alt_ccw.UseVisualStyleBackColor = true;
            // 
            // label64
            // 
            resources.ApplyResources(this.label64, "label64");
            this.label64.Name = "label64";
            // 
            // chk_func3_shift_ccw
            // 
            resources.ApplyResources(this.chk_func3_shift_ccw, "chk_func3_shift_ccw");
            this.chk_func3_shift_ccw.Name = "chk_func3_shift_ccw";
            this.chk_func3_shift_ccw.UseVisualStyleBackColor = true;
            // 
            // cmbbx_func3_set_type_cw
            // 
            this.cmbbx_func3_set_type_cw.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbx_func3_set_type_cw.FormattingEnabled = true;
            resources.ApplyResources(this.cmbbx_func3_set_type_cw, "cmbbx_func3_set_type_cw");
            this.cmbbx_func3_set_type_cw.Name = "cmbbx_func3_set_type_cw";
            this.cmbbx_func3_set_type_cw.Tag = "4";
            this.cmbbx_func3_set_type_cw.SelectedIndexChanged += new System.EventHandler(this.cmbbx_func_set_type_SelectedIndexChanged);
            // 
            // chk_func3_ctrl_ccw
            // 
            resources.ApplyResources(this.chk_func3_ctrl_ccw, "chk_func3_ctrl_ccw");
            this.chk_func3_ctrl_ccw.Name = "chk_func3_ctrl_ccw";
            this.chk_func3_ctrl_ccw.UseVisualStyleBackColor = true;
            // 
            // label65
            // 
            resources.ApplyResources(this.label65, "label65");
            this.label65.Name = "label65";
            // 
            // gbx_func2_setup
            // 
            this.gbx_func2_setup.Controls.Add(this.btn_func2_key_clr_ccw);
            this.gbx_func2_setup.Controls.Add(this.btn_func2_key_clr_cw);
            this.gbx_func2_setup.Controls.Add(this.txtbx_func2_key_cw);
            this.gbx_func2_setup.Controls.Add(this.chk_func2_win_cw);
            this.gbx_func2_setup.Controls.Add(this.groupBox8);
            this.gbx_func2_setup.Controls.Add(this.chk_func2_alt_cw);
            this.gbx_func2_setup.Controls.Add(this.num_func2_sensivity_ccw);
            this.gbx_func2_setup.Controls.Add(this.chk_func2_shift_cw);
            this.gbx_func2_setup.Controls.Add(this.label48);
            this.gbx_func2_setup.Controls.Add(this.chk_func2_ctrl_cw);
            this.gbx_func2_setup.Controls.Add(this.num_func2_sensivity_cw);
            this.gbx_func2_setup.Controls.Add(this.txtbx_func2_key_ccw);
            this.gbx_func2_setup.Controls.Add(this.label49);
            this.gbx_func2_setup.Controls.Add(this.chk_func2_win_ccw);
            this.gbx_func2_setup.Controls.Add(this.num_func2_y_ccw);
            this.gbx_func2_setup.Controls.Add(this.cmbbx_func2_set_type_ccw);
            this.gbx_func2_setup.Controls.Add(this.num_func2_x_ccw);
            this.gbx_func2_setup.Controls.Add(this.chk_func2_alt_ccw);
            this.gbx_func2_setup.Controls.Add(this.lbl_func2_title_ccw);
            this.gbx_func2_setup.Controls.Add(this.label50);
            this.gbx_func2_setup.Controls.Add(this.num_func2_y_cw);
            this.gbx_func2_setup.Controls.Add(this.chk_func2_shift_ccw);
            this.gbx_func2_setup.Controls.Add(this.num_func2_x_cw);
            this.gbx_func2_setup.Controls.Add(this.cmbbx_func2_set_type_cw);
            this.gbx_func2_setup.Controls.Add(this.lbl_func2_title_cw);
            this.gbx_func2_setup.Controls.Add(this.chk_func2_ctrl_ccw);
            this.gbx_func2_setup.Controls.Add(this.label51);
            resources.ApplyResources(this.gbx_func2_setup, "gbx_func2_setup");
            this.gbx_func2_setup.Name = "gbx_func2_setup";
            this.gbx_func2_setup.TabStop = false;
            // 
            // btn_func2_key_clr_ccw
            // 
            resources.ApplyResources(this.btn_func2_key_clr_ccw, "btn_func2_key_clr_ccw");
            this.btn_func2_key_clr_ccw.Name = "btn_func2_key_clr_ccw";
            this.btn_func2_key_clr_ccw.Tag = "3";
            this.btn_func2_key_clr_ccw.UseVisualStyleBackColor = true;
            this.btn_func2_key_clr_ccw.Click += new System.EventHandler(this.btn_func_key_clr_Click);
            // 
            // btn_func2_key_clr_cw
            // 
            resources.ApplyResources(this.btn_func2_key_clr_cw, "btn_func2_key_clr_cw");
            this.btn_func2_key_clr_cw.Name = "btn_func2_key_clr_cw";
            this.btn_func2_key_clr_cw.Tag = "2";
            this.btn_func2_key_clr_cw.UseVisualStyleBackColor = true;
            this.btn_func2_key_clr_cw.Click += new System.EventHandler(this.btn_func_key_clr_Click);
            // 
            // txtbx_func2_key_cw
            // 
            this.txtbx_func2_key_cw.ForeColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.txtbx_func2_key_cw, "txtbx_func2_key_cw");
            this.txtbx_func2_key_cw.Name = "txtbx_func2_key_cw";
            this.txtbx_func2_key_cw.Tag = "2";
            this.txtbx_func2_key_cw.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtbx_func_key_KeyDown);
            this.txtbx_func2_key_cw.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtbx_func_key_KeyUp);
            this.txtbx_func2_key_cw.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtbx_func_key_PreviewKeyDown);
            // 
            // chk_func2_win_cw
            // 
            resources.ApplyResources(this.chk_func2_win_cw, "chk_func2_win_cw");
            this.chk_func2_win_cw.Name = "chk_func2_win_cw";
            this.chk_func2_win_cw.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.rbtn_LED_Level_Light_func2);
            this.groupBox8.Controls.Add(this.lbl_LED_COLOR_NOW_func2);
            this.groupBox8.Controls.Add(this.rbtn_LED_Level_Normal_func2);
            this.groupBox8.Controls.Add(this.label12);
            this.groupBox8.Controls.Add(this.rbtn_LED_Level_Dark_func2);
            this.groupBox8.Controls.Add(this.btn_LED_preview_func2);
            this.groupBox8.Controls.Add(this.lbl_LED_COLOR_9_func2);
            this.groupBox8.Controls.Add(this.groupBox9);
            this.groupBox8.Controls.Add(this.lbl_LED_COLOR_8_func2);
            this.groupBox8.Controls.Add(this.lbl_LED_COLOR_7_func2);
            this.groupBox8.Controls.Add(this.lbl_LED_COLOR_6_func2);
            this.groupBox8.Controls.Add(this.lbl_LED_COLOR_5_func2);
            this.groupBox8.Controls.Add(this.lbl_LED_COLOR_4_func2);
            this.groupBox8.Controls.Add(this.lbl_LED_COLOR_3_func2);
            this.groupBox8.Controls.Add(this.lbl_LED_COLOR_2_func2);
            this.groupBox8.Controls.Add(this.lbl_LED_COLOR_1_func2);
            this.groupBox8.Controls.Add(this.label47);
            resources.ApplyResources(this.groupBox8, "groupBox8");
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.TabStop = false;
            // 
            // rbtn_LED_Level_Light_func2
            // 
            resources.ApplyResources(this.rbtn_LED_Level_Light_func2, "rbtn_LED_Level_Light_func2");
            this.rbtn_LED_Level_Light_func2.Name = "rbtn_LED_Level_Light_func2";
            this.rbtn_LED_Level_Light_func2.TabStop = true;
            this.rbtn_LED_Level_Light_func2.UseVisualStyleBackColor = true;
            // 
            // lbl_LED_COLOR_NOW_func2
            // 
            this.lbl_LED_COLOR_NOW_func2.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lbl_LED_COLOR_NOW_func2, "lbl_LED_COLOR_NOW_func2");
            this.lbl_LED_COLOR_NOW_func2.Name = "lbl_LED_COLOR_NOW_func2";
            this.lbl_LED_COLOR_NOW_func2.Tag = "1";
            // 
            // rbtn_LED_Level_Normal_func2
            // 
            resources.ApplyResources(this.rbtn_LED_Level_Normal_func2, "rbtn_LED_Level_Normal_func2");
            this.rbtn_LED_Level_Normal_func2.Name = "rbtn_LED_Level_Normal_func2";
            this.rbtn_LED_Level_Normal_func2.TabStop = true;
            this.rbtn_LED_Level_Normal_func2.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.DimGray;
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // rbtn_LED_Level_Dark_func2
            // 
            resources.ApplyResources(this.rbtn_LED_Level_Dark_func2, "rbtn_LED_Level_Dark_func2");
            this.rbtn_LED_Level_Dark_func2.Name = "rbtn_LED_Level_Dark_func2";
            this.rbtn_LED_Level_Dark_func2.TabStop = true;
            this.rbtn_LED_Level_Dark_func2.UseVisualStyleBackColor = true;
            // 
            // btn_LED_preview_func2
            // 
            resources.ApplyResources(this.btn_LED_preview_func2, "btn_LED_preview_func2");
            this.btn_LED_preview_func2.Name = "btn_LED_preview_func2";
            this.btn_LED_preview_func2.Tag = "1";
            this.btn_LED_preview_func2.UseVisualStyleBackColor = true;
            this.btn_LED_preview_func2.Click += new System.EventHandler(this.btn_LED_preview_func_Click);
            // 
            // lbl_LED_COLOR_9_func2
            // 
            this.lbl_LED_COLOR_9_func2.BackColor = System.Drawing.Color.Black;
            this.lbl_LED_COLOR_9_func2.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lbl_LED_COLOR_9_func2, "lbl_LED_COLOR_9_func2");
            this.lbl_LED_COLOR_9_func2.Name = "lbl_LED_COLOR_9_func2";
            this.lbl_LED_COLOR_9_func2.Tag = "100";
            this.lbl_LED_COLOR_9_func2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_func_MouseClick);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.trackBar_B_func2);
            this.groupBox9.Controls.Add(this.trackBar_G_func2);
            this.groupBox9.Controls.Add(this.trackBar_R_func2);
            resources.ApplyResources(this.groupBox9, "groupBox9");
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.TabStop = false;
            // 
            // trackBar_B_func2
            // 
            resources.ApplyResources(this.trackBar_B_func2, "trackBar_B_func2");
            this.trackBar_B_func2.Maximum = 60;
            this.trackBar_B_func2.Name = "trackBar_B_func2";
            this.trackBar_B_func2.Tag = "12";
            this.trackBar_B_func2.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_B_func2.ValueChanged += new System.EventHandler(this.trackBar_RGB_func_ValueChanged);
            // 
            // trackBar_G_func2
            // 
            resources.ApplyResources(this.trackBar_G_func2, "trackBar_G_func2");
            this.trackBar_G_func2.Maximum = 60;
            this.trackBar_G_func2.Name = "trackBar_G_func2";
            this.trackBar_G_func2.Tag = "11";
            this.trackBar_G_func2.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_G_func2.ValueChanged += new System.EventHandler(this.trackBar_RGB_func_ValueChanged);
            // 
            // trackBar_R_func2
            // 
            resources.ApplyResources(this.trackBar_R_func2, "trackBar_R_func2");
            this.trackBar_R_func2.Maximum = 60;
            this.trackBar_R_func2.Name = "trackBar_R_func2";
            this.trackBar_R_func2.Tag = "10";
            this.trackBar_R_func2.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_R_func2.ValueChanged += new System.EventHandler(this.trackBar_RGB_func_ValueChanged);
            // 
            // lbl_LED_COLOR_8_func2
            // 
            this.lbl_LED_COLOR_8_func2.BackColor = System.Drawing.Color.Purple;
            resources.ApplyResources(this.lbl_LED_COLOR_8_func2, "lbl_LED_COLOR_8_func2");
            this.lbl_LED_COLOR_8_func2.Name = "lbl_LED_COLOR_8_func2";
            this.lbl_LED_COLOR_8_func2.Tag = "108";
            this.lbl_LED_COLOR_8_func2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_func_MouseClick);
            // 
            // lbl_LED_COLOR_7_func2
            // 
            this.lbl_LED_COLOR_7_func2.BackColor = System.Drawing.Color.Blue;
            resources.ApplyResources(this.lbl_LED_COLOR_7_func2, "lbl_LED_COLOR_7_func2");
            this.lbl_LED_COLOR_7_func2.Name = "lbl_LED_COLOR_7_func2";
            this.lbl_LED_COLOR_7_func2.Tag = "107";
            this.lbl_LED_COLOR_7_func2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_func_MouseClick);
            // 
            // lbl_LED_COLOR_6_func2
            // 
            this.lbl_LED_COLOR_6_func2.BackColor = System.Drawing.Color.Green;
            resources.ApplyResources(this.lbl_LED_COLOR_6_func2, "lbl_LED_COLOR_6_func2");
            this.lbl_LED_COLOR_6_func2.Name = "lbl_LED_COLOR_6_func2";
            this.lbl_LED_COLOR_6_func2.Tag = "106";
            this.lbl_LED_COLOR_6_func2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_func_MouseClick);
            // 
            // lbl_LED_COLOR_5_func2
            // 
            this.lbl_LED_COLOR_5_func2.BackColor = System.Drawing.Color.Turquoise;
            resources.ApplyResources(this.lbl_LED_COLOR_5_func2, "lbl_LED_COLOR_5_func2");
            this.lbl_LED_COLOR_5_func2.Name = "lbl_LED_COLOR_5_func2";
            this.lbl_LED_COLOR_5_func2.Tag = "105";
            this.lbl_LED_COLOR_5_func2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_func_MouseClick);
            // 
            // lbl_LED_COLOR_4_func2
            // 
            this.lbl_LED_COLOR_4_func2.BackColor = System.Drawing.Color.Yellow;
            resources.ApplyResources(this.lbl_LED_COLOR_4_func2, "lbl_LED_COLOR_4_func2");
            this.lbl_LED_COLOR_4_func2.Name = "lbl_LED_COLOR_4_func2";
            this.lbl_LED_COLOR_4_func2.Tag = "104";
            this.lbl_LED_COLOR_4_func2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_func_MouseClick);
            // 
            // lbl_LED_COLOR_3_func2
            // 
            this.lbl_LED_COLOR_3_func2.BackColor = System.Drawing.Color.Orange;
            resources.ApplyResources(this.lbl_LED_COLOR_3_func2, "lbl_LED_COLOR_3_func2");
            this.lbl_LED_COLOR_3_func2.Name = "lbl_LED_COLOR_3_func2";
            this.lbl_LED_COLOR_3_func2.Tag = "103";
            this.lbl_LED_COLOR_3_func2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_func_MouseClick);
            // 
            // lbl_LED_COLOR_2_func2
            // 
            this.lbl_LED_COLOR_2_func2.BackColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.lbl_LED_COLOR_2_func2, "lbl_LED_COLOR_2_func2");
            this.lbl_LED_COLOR_2_func2.Name = "lbl_LED_COLOR_2_func2";
            this.lbl_LED_COLOR_2_func2.Tag = "102";
            this.lbl_LED_COLOR_2_func2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_func_MouseClick);
            // 
            // lbl_LED_COLOR_1_func2
            // 
            this.lbl_LED_COLOR_1_func2.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lbl_LED_COLOR_1_func2, "lbl_LED_COLOR_1_func2");
            this.lbl_LED_COLOR_1_func2.Name = "lbl_LED_COLOR_1_func2";
            this.lbl_LED_COLOR_1_func2.Tag = "101";
            this.lbl_LED_COLOR_1_func2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_func_MouseClick);
            // 
            // label47
            // 
            this.label47.BackColor = System.Drawing.Color.DimGray;
            resources.ApplyResources(this.label47, "label47");
            this.label47.Name = "label47";
            // 
            // chk_func2_alt_cw
            // 
            resources.ApplyResources(this.chk_func2_alt_cw, "chk_func2_alt_cw");
            this.chk_func2_alt_cw.Name = "chk_func2_alt_cw";
            this.chk_func2_alt_cw.UseVisualStyleBackColor = true;
            // 
            // num_func2_sensivity_ccw
            // 
            resources.ApplyResources(this.num_func2_sensivity_ccw, "num_func2_sensivity_ccw");
            this.num_func2_sensivity_ccw.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_func2_sensivity_ccw.Name = "num_func2_sensivity_ccw";
            this.num_func2_sensivity_ccw.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // chk_func2_shift_cw
            // 
            resources.ApplyResources(this.chk_func2_shift_cw, "chk_func2_shift_cw");
            this.chk_func2_shift_cw.Name = "chk_func2_shift_cw";
            this.chk_func2_shift_cw.UseVisualStyleBackColor = true;
            // 
            // label48
            // 
            resources.ApplyResources(this.label48, "label48");
            this.label48.Name = "label48";
            // 
            // chk_func2_ctrl_cw
            // 
            resources.ApplyResources(this.chk_func2_ctrl_cw, "chk_func2_ctrl_cw");
            this.chk_func2_ctrl_cw.Name = "chk_func2_ctrl_cw";
            this.chk_func2_ctrl_cw.UseVisualStyleBackColor = true;
            // 
            // num_func2_sensivity_cw
            // 
            resources.ApplyResources(this.num_func2_sensivity_cw, "num_func2_sensivity_cw");
            this.num_func2_sensivity_cw.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_func2_sensivity_cw.Name = "num_func2_sensivity_cw";
            this.num_func2_sensivity_cw.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // txtbx_func2_key_ccw
            // 
            this.txtbx_func2_key_ccw.ForeColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.txtbx_func2_key_ccw, "txtbx_func2_key_ccw");
            this.txtbx_func2_key_ccw.Name = "txtbx_func2_key_ccw";
            this.txtbx_func2_key_ccw.Tag = "3";
            this.txtbx_func2_key_ccw.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtbx_func_key_KeyDown);
            this.txtbx_func2_key_ccw.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtbx_func_key_KeyUp);
            this.txtbx_func2_key_ccw.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtbx_func_key_PreviewKeyDown);
            // 
            // label49
            // 
            resources.ApplyResources(this.label49, "label49");
            this.label49.Name = "label49";
            // 
            // chk_func2_win_ccw
            // 
            resources.ApplyResources(this.chk_func2_win_ccw, "chk_func2_win_ccw");
            this.chk_func2_win_ccw.Name = "chk_func2_win_ccw";
            this.chk_func2_win_ccw.UseVisualStyleBackColor = true;
            // 
            // num_func2_y_ccw
            // 
            resources.ApplyResources(this.num_func2_y_ccw, "num_func2_y_ccw");
            this.num_func2_y_ccw.Name = "num_func2_y_ccw";
            // 
            // cmbbx_func2_set_type_ccw
            // 
            this.cmbbx_func2_set_type_ccw.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbx_func2_set_type_ccw.FormattingEnabled = true;
            resources.ApplyResources(this.cmbbx_func2_set_type_ccw, "cmbbx_func2_set_type_ccw");
            this.cmbbx_func2_set_type_ccw.Name = "cmbbx_func2_set_type_ccw";
            this.cmbbx_func2_set_type_ccw.Tag = "3";
            this.cmbbx_func2_set_type_ccw.SelectedIndexChanged += new System.EventHandler(this.cmbbx_func_set_type_SelectedIndexChanged);
            // 
            // num_func2_x_ccw
            // 
            resources.ApplyResources(this.num_func2_x_ccw, "num_func2_x_ccw");
            this.num_func2_x_ccw.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.num_func2_x_ccw.Minimum = new decimal(new int[] {
            127,
            0,
            0,
            -2147483648});
            this.num_func2_x_ccw.Name = "num_func2_x_ccw";
            this.num_func2_x_ccw.Value = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            // 
            // chk_func2_alt_ccw
            // 
            resources.ApplyResources(this.chk_func2_alt_ccw, "chk_func2_alt_ccw");
            this.chk_func2_alt_ccw.Name = "chk_func2_alt_ccw";
            this.chk_func2_alt_ccw.UseVisualStyleBackColor = true;
            // 
            // lbl_func2_title_ccw
            // 
            resources.ApplyResources(this.lbl_func2_title_ccw, "lbl_func2_title_ccw");
            this.lbl_func2_title_ccw.Name = "lbl_func2_title_ccw";
            // 
            // label50
            // 
            resources.ApplyResources(this.label50, "label50");
            this.label50.Name = "label50";
            // 
            // num_func2_y_cw
            // 
            resources.ApplyResources(this.num_func2_y_cw, "num_func2_y_cw");
            this.num_func2_y_cw.Name = "num_func2_y_cw";
            // 
            // chk_func2_shift_ccw
            // 
            resources.ApplyResources(this.chk_func2_shift_ccw, "chk_func2_shift_ccw");
            this.chk_func2_shift_ccw.Name = "chk_func2_shift_ccw";
            this.chk_func2_shift_ccw.UseVisualStyleBackColor = true;
            // 
            // num_func2_x_cw
            // 
            resources.ApplyResources(this.num_func2_x_cw, "num_func2_x_cw");
            this.num_func2_x_cw.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.num_func2_x_cw.Minimum = new decimal(new int[] {
            127,
            0,
            0,
            -2147483648});
            this.num_func2_x_cw.Name = "num_func2_x_cw";
            this.num_func2_x_cw.Value = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            // 
            // cmbbx_func2_set_type_cw
            // 
            this.cmbbx_func2_set_type_cw.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbx_func2_set_type_cw.FormattingEnabled = true;
            resources.ApplyResources(this.cmbbx_func2_set_type_cw, "cmbbx_func2_set_type_cw");
            this.cmbbx_func2_set_type_cw.Name = "cmbbx_func2_set_type_cw";
            this.cmbbx_func2_set_type_cw.Tag = "2";
            this.cmbbx_func2_set_type_cw.SelectedIndexChanged += new System.EventHandler(this.cmbbx_func_set_type_SelectedIndexChanged);
            // 
            // lbl_func2_title_cw
            // 
            resources.ApplyResources(this.lbl_func2_title_cw, "lbl_func2_title_cw");
            this.lbl_func2_title_cw.Name = "lbl_func2_title_cw";
            // 
            // chk_func2_ctrl_ccw
            // 
            resources.ApplyResources(this.chk_func2_ctrl_ccw, "chk_func2_ctrl_ccw");
            this.chk_func2_ctrl_ccw.Name = "chk_func2_ctrl_ccw";
            this.chk_func2_ctrl_ccw.UseVisualStyleBackColor = true;
            // 
            // label51
            // 
            resources.ApplyResources(this.label51, "label51");
            this.label51.Name = "label51";
            // 
            // gbx_func1_setup
            // 
            this.gbx_func1_setup.Controls.Add(this.btn_func1_key_clr_ccw);
            this.gbx_func1_setup.Controls.Add(this.btn_func1_key_clr_cw);
            this.gbx_func1_setup.Controls.Add(this.txtbx_func1_key_cw);
            this.gbx_func1_setup.Controls.Add(this.chk_func1_win_cw);
            this.gbx_func1_setup.Controls.Add(this.chk_func1_alt_cw);
            this.gbx_func1_setup.Controls.Add(this.chk_func1_shift_cw);
            this.gbx_func1_setup.Controls.Add(this.chk_func1_ctrl_cw);
            this.gbx_func1_setup.Controls.Add(this.txtbx_func1_key_ccw);
            this.gbx_func1_setup.Controls.Add(this.chk_func1_win_ccw);
            this.gbx_func1_setup.Controls.Add(this.chk_func1_alt_ccw);
            this.gbx_func1_setup.Controls.Add(this.chk_func1_shift_ccw);
            this.gbx_func1_setup.Controls.Add(this.chk_func1_ctrl_ccw);
            this.gbx_func1_setup.Controls.Add(this.groupBox3);
            this.gbx_func1_setup.Controls.Add(this.num_func1_sensivity_ccw);
            this.gbx_func1_setup.Controls.Add(this.label6);
            this.gbx_func1_setup.Controls.Add(this.num_func1_sensivity_cw);
            this.gbx_func1_setup.Controls.Add(this.label7);
            this.gbx_func1_setup.Controls.Add(this.cmbbx_func1_set_type_ccw);
            this.gbx_func1_setup.Controls.Add(this.label10);
            this.gbx_func1_setup.Controls.Add(this.cmbbx_func1_set_type_cw);
            this.gbx_func1_setup.Controls.Add(this.num_func1_y_ccw);
            this.gbx_func1_setup.Controls.Add(this.label11);
            this.gbx_func1_setup.Controls.Add(this.num_func1_x_ccw);
            this.gbx_func1_setup.Controls.Add(this.lbl_func1_title_cw);
            this.gbx_func1_setup.Controls.Add(this.lbl_func1_title_ccw);
            this.gbx_func1_setup.Controls.Add(this.num_func1_x_cw);
            this.gbx_func1_setup.Controls.Add(this.num_func1_y_cw);
            resources.ApplyResources(this.gbx_func1_setup, "gbx_func1_setup");
            this.gbx_func1_setup.Name = "gbx_func1_setup";
            this.gbx_func1_setup.TabStop = false;
            // 
            // btn_func1_key_clr_ccw
            // 
            resources.ApplyResources(this.btn_func1_key_clr_ccw, "btn_func1_key_clr_ccw");
            this.btn_func1_key_clr_ccw.Name = "btn_func1_key_clr_ccw";
            this.btn_func1_key_clr_ccw.Tag = "1";
            this.btn_func1_key_clr_ccw.UseVisualStyleBackColor = true;
            this.btn_func1_key_clr_ccw.Click += new System.EventHandler(this.btn_func_key_clr_Click);
            // 
            // btn_func1_key_clr_cw
            // 
            resources.ApplyResources(this.btn_func1_key_clr_cw, "btn_func1_key_clr_cw");
            this.btn_func1_key_clr_cw.Name = "btn_func1_key_clr_cw";
            this.btn_func1_key_clr_cw.Tag = "0";
            this.btn_func1_key_clr_cw.UseVisualStyleBackColor = true;
            this.btn_func1_key_clr_cw.Click += new System.EventHandler(this.btn_func_key_clr_Click);
            // 
            // txtbx_func1_key_cw
            // 
            this.txtbx_func1_key_cw.ForeColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.txtbx_func1_key_cw, "txtbx_func1_key_cw");
            this.txtbx_func1_key_cw.Name = "txtbx_func1_key_cw";
            this.txtbx_func1_key_cw.Tag = "0";
            this.txtbx_func1_key_cw.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtbx_func_key_KeyDown);
            this.txtbx_func1_key_cw.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtbx_func_key_KeyUp);
            this.txtbx_func1_key_cw.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtbx_func_key_PreviewKeyDown);
            // 
            // chk_func1_win_cw
            // 
            resources.ApplyResources(this.chk_func1_win_cw, "chk_func1_win_cw");
            this.chk_func1_win_cw.Name = "chk_func1_win_cw";
            this.chk_func1_win_cw.UseVisualStyleBackColor = true;
            // 
            // chk_func1_alt_cw
            // 
            resources.ApplyResources(this.chk_func1_alt_cw, "chk_func1_alt_cw");
            this.chk_func1_alt_cw.Name = "chk_func1_alt_cw";
            this.chk_func1_alt_cw.UseVisualStyleBackColor = true;
            // 
            // chk_func1_shift_cw
            // 
            resources.ApplyResources(this.chk_func1_shift_cw, "chk_func1_shift_cw");
            this.chk_func1_shift_cw.Name = "chk_func1_shift_cw";
            this.chk_func1_shift_cw.UseVisualStyleBackColor = true;
            // 
            // chk_func1_ctrl_cw
            // 
            resources.ApplyResources(this.chk_func1_ctrl_cw, "chk_func1_ctrl_cw");
            this.chk_func1_ctrl_cw.Name = "chk_func1_ctrl_cw";
            this.chk_func1_ctrl_cw.UseVisualStyleBackColor = true;
            // 
            // txtbx_func1_key_ccw
            // 
            this.txtbx_func1_key_ccw.ForeColor = System.Drawing.Color.Gray;
            resources.ApplyResources(this.txtbx_func1_key_ccw, "txtbx_func1_key_ccw");
            this.txtbx_func1_key_ccw.Name = "txtbx_func1_key_ccw";
            this.txtbx_func1_key_ccw.Tag = "1";
            this.txtbx_func1_key_ccw.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtbx_func_key_KeyDown);
            this.txtbx_func1_key_ccw.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtbx_func_key_KeyUp);
            this.txtbx_func1_key_ccw.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtbx_func_key_PreviewKeyDown);
            // 
            // chk_func1_win_ccw
            // 
            resources.ApplyResources(this.chk_func1_win_ccw, "chk_func1_win_ccw");
            this.chk_func1_win_ccw.Name = "chk_func1_win_ccw";
            this.chk_func1_win_ccw.UseVisualStyleBackColor = true;
            // 
            // chk_func1_alt_ccw
            // 
            resources.ApplyResources(this.chk_func1_alt_ccw, "chk_func1_alt_ccw");
            this.chk_func1_alt_ccw.Name = "chk_func1_alt_ccw";
            this.chk_func1_alt_ccw.UseVisualStyleBackColor = true;
            // 
            // chk_func1_shift_ccw
            // 
            resources.ApplyResources(this.chk_func1_shift_ccw, "chk_func1_shift_ccw");
            this.chk_func1_shift_ccw.Name = "chk_func1_shift_ccw";
            this.chk_func1_shift_ccw.UseVisualStyleBackColor = true;
            // 
            // chk_func1_ctrl_ccw
            // 
            resources.ApplyResources(this.chk_func1_ctrl_ccw, "chk_func1_ctrl_ccw");
            this.chk_func1_ctrl_ccw.Name = "chk_func1_ctrl_ccw";
            this.chk_func1_ctrl_ccw.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbtn_LED_Level_Light_func1);
            this.groupBox3.Controls.Add(this.rbtn_LED_Level_Normal_func1);
            this.groupBox3.Controls.Add(this.rbtn_LED_Level_Dark_func1);
            this.groupBox3.Controls.Add(this.lbl_LED_COLOR_NOW_func1);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.btn_LED_preview_func1);
            this.groupBox3.Controls.Add(this.lbl_LED_COLOR_9_func1);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.lbl_LED_COLOR_8_func1);
            this.groupBox3.Controls.Add(this.lbl_LED_COLOR_7_func1);
            this.groupBox3.Controls.Add(this.lbl_LED_COLOR_6_func1);
            this.groupBox3.Controls.Add(this.lbl_LED_COLOR_5_func1);
            this.groupBox3.Controls.Add(this.lbl_LED_COLOR_4_func1);
            this.groupBox3.Controls.Add(this.lbl_LED_COLOR_3_func1);
            this.groupBox3.Controls.Add(this.lbl_LED_COLOR_2_func1);
            this.groupBox3.Controls.Add(this.lbl_LED_COLOR_1_func1);
            this.groupBox3.Controls.Add(this.label37);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // rbtn_LED_Level_Light_func1
            // 
            resources.ApplyResources(this.rbtn_LED_Level_Light_func1, "rbtn_LED_Level_Light_func1");
            this.rbtn_LED_Level_Light_func1.Name = "rbtn_LED_Level_Light_func1";
            this.rbtn_LED_Level_Light_func1.TabStop = true;
            this.rbtn_LED_Level_Light_func1.UseVisualStyleBackColor = true;
            // 
            // rbtn_LED_Level_Normal_func1
            // 
            resources.ApplyResources(this.rbtn_LED_Level_Normal_func1, "rbtn_LED_Level_Normal_func1");
            this.rbtn_LED_Level_Normal_func1.Name = "rbtn_LED_Level_Normal_func1";
            this.rbtn_LED_Level_Normal_func1.TabStop = true;
            this.rbtn_LED_Level_Normal_func1.UseVisualStyleBackColor = true;
            // 
            // rbtn_LED_Level_Dark_func1
            // 
            resources.ApplyResources(this.rbtn_LED_Level_Dark_func1, "rbtn_LED_Level_Dark_func1");
            this.rbtn_LED_Level_Dark_func1.Name = "rbtn_LED_Level_Dark_func1";
            this.rbtn_LED_Level_Dark_func1.TabStop = true;
            this.rbtn_LED_Level_Dark_func1.UseVisualStyleBackColor = true;
            // 
            // lbl_LED_COLOR_NOW_func1
            // 
            this.lbl_LED_COLOR_NOW_func1.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lbl_LED_COLOR_NOW_func1, "lbl_LED_COLOR_NOW_func1");
            this.lbl_LED_COLOR_NOW_func1.Name = "lbl_LED_COLOR_NOW_func1";
            this.lbl_LED_COLOR_NOW_func1.Tag = "1";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.DimGray;
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // btn_LED_preview_func1
            // 
            resources.ApplyResources(this.btn_LED_preview_func1, "btn_LED_preview_func1");
            this.btn_LED_preview_func1.Name = "btn_LED_preview_func1";
            this.btn_LED_preview_func1.Tag = "0";
            this.btn_LED_preview_func1.UseVisualStyleBackColor = true;
            this.btn_LED_preview_func1.Click += new System.EventHandler(this.btn_LED_preview_func_Click);
            // 
            // lbl_LED_COLOR_9_func1
            // 
            this.lbl_LED_COLOR_9_func1.BackColor = System.Drawing.Color.Black;
            this.lbl_LED_COLOR_9_func1.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lbl_LED_COLOR_9_func1, "lbl_LED_COLOR_9_func1");
            this.lbl_LED_COLOR_9_func1.Name = "lbl_LED_COLOR_9_func1";
            this.lbl_LED_COLOR_9_func1.Tag = "0";
            this.lbl_LED_COLOR_9_func1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_func_MouseClick);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.trackBar_B_func1);
            this.groupBox4.Controls.Add(this.trackBar_G_func1);
            this.groupBox4.Controls.Add(this.trackBar_R_func1);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // trackBar_B_func1
            // 
            resources.ApplyResources(this.trackBar_B_func1, "trackBar_B_func1");
            this.trackBar_B_func1.Maximum = 60;
            this.trackBar_B_func1.Name = "trackBar_B_func1";
            this.trackBar_B_func1.Tag = "2";
            this.trackBar_B_func1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_B_func1.ValueChanged += new System.EventHandler(this.trackBar_RGB_func_ValueChanged);
            // 
            // trackBar_G_func1
            // 
            resources.ApplyResources(this.trackBar_G_func1, "trackBar_G_func1");
            this.trackBar_G_func1.Maximum = 60;
            this.trackBar_G_func1.Name = "trackBar_G_func1";
            this.trackBar_G_func1.Tag = "1";
            this.trackBar_G_func1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_G_func1.ValueChanged += new System.EventHandler(this.trackBar_RGB_func_ValueChanged);
            // 
            // trackBar_R_func1
            // 
            resources.ApplyResources(this.trackBar_R_func1, "trackBar_R_func1");
            this.trackBar_R_func1.Maximum = 60;
            this.trackBar_R_func1.Name = "trackBar_R_func1";
            this.trackBar_R_func1.Tag = "0";
            this.trackBar_R_func1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_R_func1.ValueChanged += new System.EventHandler(this.trackBar_RGB_func_ValueChanged);
            // 
            // lbl_LED_COLOR_8_func1
            // 
            this.lbl_LED_COLOR_8_func1.BackColor = System.Drawing.Color.Purple;
            resources.ApplyResources(this.lbl_LED_COLOR_8_func1, "lbl_LED_COLOR_8_func1");
            this.lbl_LED_COLOR_8_func1.Name = "lbl_LED_COLOR_8_func1";
            this.lbl_LED_COLOR_8_func1.Tag = "8";
            this.lbl_LED_COLOR_8_func1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_func_MouseClick);
            // 
            // lbl_LED_COLOR_7_func1
            // 
            this.lbl_LED_COLOR_7_func1.BackColor = System.Drawing.Color.Blue;
            resources.ApplyResources(this.lbl_LED_COLOR_7_func1, "lbl_LED_COLOR_7_func1");
            this.lbl_LED_COLOR_7_func1.Name = "lbl_LED_COLOR_7_func1";
            this.lbl_LED_COLOR_7_func1.Tag = "7";
            this.lbl_LED_COLOR_7_func1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_func_MouseClick);
            // 
            // lbl_LED_COLOR_6_func1
            // 
            this.lbl_LED_COLOR_6_func1.BackColor = System.Drawing.Color.Green;
            resources.ApplyResources(this.lbl_LED_COLOR_6_func1, "lbl_LED_COLOR_6_func1");
            this.lbl_LED_COLOR_6_func1.Name = "lbl_LED_COLOR_6_func1";
            this.lbl_LED_COLOR_6_func1.Tag = "6";
            this.lbl_LED_COLOR_6_func1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_func_MouseClick);
            // 
            // lbl_LED_COLOR_5_func1
            // 
            this.lbl_LED_COLOR_5_func1.BackColor = System.Drawing.Color.Turquoise;
            resources.ApplyResources(this.lbl_LED_COLOR_5_func1, "lbl_LED_COLOR_5_func1");
            this.lbl_LED_COLOR_5_func1.Name = "lbl_LED_COLOR_5_func1";
            this.lbl_LED_COLOR_5_func1.Tag = "5";
            this.lbl_LED_COLOR_5_func1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_func_MouseClick);
            // 
            // lbl_LED_COLOR_4_func1
            // 
            this.lbl_LED_COLOR_4_func1.BackColor = System.Drawing.Color.Yellow;
            resources.ApplyResources(this.lbl_LED_COLOR_4_func1, "lbl_LED_COLOR_4_func1");
            this.lbl_LED_COLOR_4_func1.Name = "lbl_LED_COLOR_4_func1";
            this.lbl_LED_COLOR_4_func1.Tag = "4";
            this.lbl_LED_COLOR_4_func1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_func_MouseClick);
            // 
            // lbl_LED_COLOR_3_func1
            // 
            this.lbl_LED_COLOR_3_func1.BackColor = System.Drawing.Color.Orange;
            resources.ApplyResources(this.lbl_LED_COLOR_3_func1, "lbl_LED_COLOR_3_func1");
            this.lbl_LED_COLOR_3_func1.Name = "lbl_LED_COLOR_3_func1";
            this.lbl_LED_COLOR_3_func1.Tag = "3";
            this.lbl_LED_COLOR_3_func1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_func_MouseClick);
            // 
            // lbl_LED_COLOR_2_func1
            // 
            this.lbl_LED_COLOR_2_func1.BackColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.lbl_LED_COLOR_2_func1, "lbl_LED_COLOR_2_func1");
            this.lbl_LED_COLOR_2_func1.Name = "lbl_LED_COLOR_2_func1";
            this.lbl_LED_COLOR_2_func1.Tag = "2";
            this.lbl_LED_COLOR_2_func1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_func_MouseClick);
            // 
            // lbl_LED_COLOR_1_func1
            // 
            this.lbl_LED_COLOR_1_func1.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lbl_LED_COLOR_1_func1, "lbl_LED_COLOR_1_func1");
            this.lbl_LED_COLOR_1_func1.Name = "lbl_LED_COLOR_1_func1";
            this.lbl_LED_COLOR_1_func1.Tag = "1";
            this.lbl_LED_COLOR_1_func1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_func_MouseClick);
            // 
            // label37
            // 
            this.label37.BackColor = System.Drawing.Color.DimGray;
            resources.ApplyResources(this.label37, "label37");
            this.label37.Name = "label37";
            // 
            // num_func1_sensivity_ccw
            // 
            resources.ApplyResources(this.num_func1_sensivity_ccw, "num_func1_sensivity_ccw");
            this.num_func1_sensivity_ccw.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_func1_sensivity_ccw.Name = "num_func1_sensivity_ccw";
            this.num_func1_sensivity_ccw.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // num_func1_sensivity_cw
            // 
            resources.ApplyResources(this.num_func1_sensivity_cw, "num_func1_sensivity_cw");
            this.num_func1_sensivity_cw.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_func1_sensivity_cw.Name = "num_func1_sensivity_cw";
            this.num_func1_sensivity_cw.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // cmbbx_func1_set_type_ccw
            // 
            this.cmbbx_func1_set_type_ccw.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbx_func1_set_type_ccw.FormattingEnabled = true;
            resources.ApplyResources(this.cmbbx_func1_set_type_ccw, "cmbbx_func1_set_type_ccw");
            this.cmbbx_func1_set_type_ccw.Name = "cmbbx_func1_set_type_ccw";
            this.cmbbx_func1_set_type_ccw.Tag = "1";
            this.cmbbx_func1_set_type_ccw.SelectedIndexChanged += new System.EventHandler(this.cmbbx_func_set_type_SelectedIndexChanged);
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // cmbbx_func1_set_type_cw
            // 
            this.cmbbx_func1_set_type_cw.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbx_func1_set_type_cw.FormattingEnabled = true;
            resources.ApplyResources(this.cmbbx_func1_set_type_cw, "cmbbx_func1_set_type_cw");
            this.cmbbx_func1_set_type_cw.Name = "cmbbx_func1_set_type_cw";
            this.cmbbx_func1_set_type_cw.Tag = "0";
            this.cmbbx_func1_set_type_cw.SelectedIndexChanged += new System.EventHandler(this.cmbbx_func_set_type_SelectedIndexChanged);
            // 
            // num_func1_y_ccw
            // 
            resources.ApplyResources(this.num_func1_y_ccw, "num_func1_y_ccw");
            this.num_func1_y_ccw.Name = "num_func1_y_ccw";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // num_func1_x_ccw
            // 
            resources.ApplyResources(this.num_func1_x_ccw, "num_func1_x_ccw");
            this.num_func1_x_ccw.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.num_func1_x_ccw.Minimum = new decimal(new int[] {
            127,
            0,
            0,
            -2147483648});
            this.num_func1_x_ccw.Name = "num_func1_x_ccw";
            this.num_func1_x_ccw.Value = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            // 
            // lbl_func1_title_cw
            // 
            resources.ApplyResources(this.lbl_func1_title_cw, "lbl_func1_title_cw");
            this.lbl_func1_title_cw.Name = "lbl_func1_title_cw";
            // 
            // lbl_func1_title_ccw
            // 
            resources.ApplyResources(this.lbl_func1_title_ccw, "lbl_func1_title_ccw");
            this.lbl_func1_title_ccw.Name = "lbl_func1_title_ccw";
            // 
            // num_func1_x_cw
            // 
            resources.ApplyResources(this.num_func1_x_cw, "num_func1_x_cw");
            this.num_func1_x_cw.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.num_func1_x_cw.Minimum = new decimal(new int[] {
            127,
            0,
            0,
            -2147483648});
            this.num_func1_x_cw.Name = "num_func1_x_cw";
            this.num_func1_x_cw.Value = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            // 
            // num_func1_y_cw
            // 
            resources.ApplyResources(this.num_func1_y_cw, "num_func1_y_cw");
            this.num_func1_y_cw.Name = "num_func1_y_cw";
            // 
            // lbl_function_set_icon
            // 
            resources.ApplyResources(this.lbl_function_set_icon, "lbl_function_set_icon");
            this.lbl_function_set_icon.ImageList = this.SetButton_imageList;
            this.lbl_function_set_icon.Name = "lbl_function_set_icon";
            this.lbl_function_set_icon.Tag = "0";
            this.lbl_function_set_icon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_function_set_icon_MouseClick);
            this.lbl_function_set_icon.MouseEnter += new System.EventHandler(this.lbl_MouseEnter);
            this.lbl_function_set_icon.MouseLeave += new System.EventHandler(this.lbl_MouseLeave);
            // 
            // SetButton_imageList
            // 
            this.SetButton_imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("SetButton_imageList.ImageStream")));
            this.SetButton_imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.SetButton_imageList.Images.SetKeyName(0, "Bn_SETTEI.png");
            this.SetButton_imageList.Images.SetKeyName(1, "Bn_SETTEI_ACTIVE.png");
            // 
            // tabPageButton
            // 
            this.tabPageButton.Controls.Add(this.lbl_button_mode3_select);
            this.tabPageButton.Controls.Add(this.lbl_button_mode2_select);
            this.tabPageButton.Controls.Add(this.lbl_button_mode1_select);
            this.tabPageButton.Controls.Add(this.gbx_encoder_button_setup);
            this.tabPageButton.Controls.Add(this.lbl_button_setting_set_icon);
            resources.ApplyResources(this.tabPageButton, "tabPageButton");
            this.tabPageButton.Name = "tabPageButton";
            this.tabPageButton.UseVisualStyleBackColor = true;
            // 
            // lbl_button_mode3_select
            // 
            this.lbl_button_mode3_select.BackColor = System.Drawing.Color.DarkGray;
            resources.ApplyResources(this.lbl_button_mode3_select, "lbl_button_mode3_select");
            this.lbl_button_mode3_select.Name = "lbl_button_mode3_select";
            this.lbl_button_mode3_select.Tag = "2";
            this.lbl_button_mode3_select.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_button_mode_select_MouseClick);
            // 
            // lbl_button_mode2_select
            // 
            this.lbl_button_mode2_select.BackColor = System.Drawing.Color.DarkGray;
            resources.ApplyResources(this.lbl_button_mode2_select, "lbl_button_mode2_select");
            this.lbl_button_mode2_select.Name = "lbl_button_mode2_select";
            this.lbl_button_mode2_select.Tag = "1";
            this.lbl_button_mode2_select.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_button_mode_select_MouseClick);
            // 
            // lbl_button_mode1_select
            // 
            this.lbl_button_mode1_select.BackColor = System.Drawing.Color.DarkGray;
            resources.ApplyResources(this.lbl_button_mode1_select, "lbl_button_mode1_select");
            this.lbl_button_mode1_select.Name = "lbl_button_mode1_select";
            this.lbl_button_mode1_select.Tag = "0";
            this.lbl_button_mode1_select.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_button_mode_select_MouseClick);
            // 
            // gbx_encoder_button_setup
            // 
            this.gbx_encoder_button_setup.Controls.Add(this.gbx_encoder_setup);
            this.gbx_encoder_button_setup.Controls.Add(this.gbx_LED_set);
            this.gbx_encoder_button_setup.Controls.Add(this.gbx_button_setup);
            resources.ApplyResources(this.gbx_encoder_button_setup, "gbx_encoder_button_setup");
            this.gbx_encoder_button_setup.Name = "gbx_encoder_button_setup";
            this.gbx_encoder_button_setup.TabStop = false;
            // 
            // gbx_encoder_setup
            // 
            this.gbx_encoder_setup.Controls.Add(this.cmbbx_encoder_default);
            this.gbx_encoder_setup.Controls.Add(this.label17);
            this.gbx_encoder_setup.Controls.Add(this.cmbbx_encoder_button);
            this.gbx_encoder_setup.Controls.Add(this.label34);
            resources.ApplyResources(this.gbx_encoder_setup, "gbx_encoder_setup");
            this.gbx_encoder_setup.Name = "gbx_encoder_setup";
            this.gbx_encoder_setup.TabStop = false;
            // 
            // cmbbx_encoder_default
            // 
            this.cmbbx_encoder_default.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbx_encoder_default.FormattingEnabled = true;
            resources.ApplyResources(this.cmbbx_encoder_default, "cmbbx_encoder_default");
            this.cmbbx_encoder_default.Name = "cmbbx_encoder_default";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // cmbbx_encoder_button
            // 
            this.cmbbx_encoder_button.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbx_encoder_button.FormattingEnabled = true;
            resources.ApplyResources(this.cmbbx_encoder_button, "cmbbx_encoder_button");
            this.cmbbx_encoder_button.Name = "cmbbx_encoder_button";
            // 
            // label34
            // 
            resources.ApplyResources(this.label34, "label34");
            this.label34.Name = "label34";
            // 
            // gbx_LED_set
            // 
            this.gbx_LED_set.Controls.Add(this.rbtn_LED_Level_Light);
            this.gbx_LED_set.Controls.Add(this.rbtn_LED_Level_Normal);
            this.gbx_LED_set.Controls.Add(this.rbtn_LED_Level_Dark);
            this.gbx_LED_set.Controls.Add(this.lbl_LED_COLOR_NOW);
            this.gbx_LED_set.Controls.Add(this.lbl_LED_color_now_border);
            this.gbx_LED_set.Controls.Add(this.btn_LED_preview);
            this.gbx_LED_set.Controls.Add(this.lbl_LED_COLOR_9);
            this.gbx_LED_set.Controls.Add(this.groupBox6);
            this.gbx_LED_set.Controls.Add(this.lbl_LED_COLOR_8);
            this.gbx_LED_set.Controls.Add(this.lbl_LED_COLOR_7);
            this.gbx_LED_set.Controls.Add(this.lbl_LED_COLOR_6);
            this.gbx_LED_set.Controls.Add(this.lbl_LED_COLOR_5);
            this.gbx_LED_set.Controls.Add(this.lbl_LED_COLOR_4);
            this.gbx_LED_set.Controls.Add(this.lbl_LED_COLOR_3);
            this.gbx_LED_set.Controls.Add(this.lbl_LED_COLOR_2);
            this.gbx_LED_set.Controls.Add(this.lbl_LED_COLOR_1);
            this.gbx_LED_set.Controls.Add(this.lbl_LED_color_border);
            resources.ApplyResources(this.gbx_LED_set, "gbx_LED_set");
            this.gbx_LED_set.Name = "gbx_LED_set";
            this.gbx_LED_set.TabStop = false;
            // 
            // rbtn_LED_Level_Light
            // 
            resources.ApplyResources(this.rbtn_LED_Level_Light, "rbtn_LED_Level_Light");
            this.rbtn_LED_Level_Light.Name = "rbtn_LED_Level_Light";
            this.rbtn_LED_Level_Light.TabStop = true;
            this.rbtn_LED_Level_Light.UseVisualStyleBackColor = true;
            // 
            // rbtn_LED_Level_Normal
            // 
            resources.ApplyResources(this.rbtn_LED_Level_Normal, "rbtn_LED_Level_Normal");
            this.rbtn_LED_Level_Normal.Name = "rbtn_LED_Level_Normal";
            this.rbtn_LED_Level_Normal.TabStop = true;
            this.rbtn_LED_Level_Normal.UseVisualStyleBackColor = true;
            // 
            // rbtn_LED_Level_Dark
            // 
            resources.ApplyResources(this.rbtn_LED_Level_Dark, "rbtn_LED_Level_Dark");
            this.rbtn_LED_Level_Dark.Name = "rbtn_LED_Level_Dark";
            this.rbtn_LED_Level_Dark.TabStop = true;
            this.rbtn_LED_Level_Dark.UseVisualStyleBackColor = true;
            // 
            // lbl_LED_COLOR_NOW
            // 
            this.lbl_LED_COLOR_NOW.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lbl_LED_COLOR_NOW, "lbl_LED_COLOR_NOW");
            this.lbl_LED_COLOR_NOW.Name = "lbl_LED_COLOR_NOW";
            this.lbl_LED_COLOR_NOW.Tag = "1";
            // 
            // lbl_LED_color_now_border
            // 
            this.lbl_LED_color_now_border.BackColor = System.Drawing.Color.DimGray;
            resources.ApplyResources(this.lbl_LED_color_now_border, "lbl_LED_color_now_border");
            this.lbl_LED_color_now_border.Name = "lbl_LED_color_now_border";
            // 
            // btn_LED_preview
            // 
            resources.ApplyResources(this.btn_LED_preview, "btn_LED_preview");
            this.btn_LED_preview.Name = "btn_LED_preview";
            this.btn_LED_preview.UseVisualStyleBackColor = true;
            this.btn_LED_preview.Click += new System.EventHandler(this.btn_LED_preview_Click);
            // 
            // lbl_LED_COLOR_9
            // 
            this.lbl_LED_COLOR_9.BackColor = System.Drawing.Color.Black;
            this.lbl_LED_COLOR_9.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lbl_LED_COLOR_9, "lbl_LED_COLOR_9");
            this.lbl_LED_COLOR_9.Name = "lbl_LED_COLOR_9";
            this.lbl_LED_COLOR_9.Tag = "0";
            this.lbl_LED_COLOR_9.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_MouseClick);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.trackBar_B);
            this.groupBox6.Controls.Add(this.trackBar_G);
            this.groupBox6.Controls.Add(this.trackBar_R);
            resources.ApplyResources(this.groupBox6, "groupBox6");
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.TabStop = false;
            // 
            // trackBar_B
            // 
            resources.ApplyResources(this.trackBar_B, "trackBar_B");
            this.trackBar_B.Maximum = 60;
            this.trackBar_B.Name = "trackBar_B";
            this.trackBar_B.Tag = "2";
            this.trackBar_B.ValueChanged += new System.EventHandler(this.trackBar_RGB_ValueChanged);
            // 
            // trackBar_G
            // 
            resources.ApplyResources(this.trackBar_G, "trackBar_G");
            this.trackBar_G.Maximum = 60;
            this.trackBar_G.Name = "trackBar_G";
            this.trackBar_G.Tag = "1";
            this.trackBar_G.ValueChanged += new System.EventHandler(this.trackBar_RGB_ValueChanged);
            // 
            // trackBar_R
            // 
            resources.ApplyResources(this.trackBar_R, "trackBar_R");
            this.trackBar_R.Maximum = 60;
            this.trackBar_R.Name = "trackBar_R";
            this.trackBar_R.Tag = "0";
            this.trackBar_R.ValueChanged += new System.EventHandler(this.trackBar_RGB_ValueChanged);
            // 
            // lbl_LED_COLOR_8
            // 
            this.lbl_LED_COLOR_8.BackColor = System.Drawing.Color.Purple;
            resources.ApplyResources(this.lbl_LED_COLOR_8, "lbl_LED_COLOR_8");
            this.lbl_LED_COLOR_8.Name = "lbl_LED_COLOR_8";
            this.lbl_LED_COLOR_8.Tag = "8";
            this.lbl_LED_COLOR_8.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_MouseClick);
            // 
            // lbl_LED_COLOR_7
            // 
            this.lbl_LED_COLOR_7.BackColor = System.Drawing.Color.Blue;
            resources.ApplyResources(this.lbl_LED_COLOR_7, "lbl_LED_COLOR_7");
            this.lbl_LED_COLOR_7.Name = "lbl_LED_COLOR_7";
            this.lbl_LED_COLOR_7.Tag = "7";
            this.lbl_LED_COLOR_7.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_MouseClick);
            // 
            // lbl_LED_COLOR_6
            // 
            this.lbl_LED_COLOR_6.BackColor = System.Drawing.Color.Green;
            resources.ApplyResources(this.lbl_LED_COLOR_6, "lbl_LED_COLOR_6");
            this.lbl_LED_COLOR_6.Name = "lbl_LED_COLOR_6";
            this.lbl_LED_COLOR_6.Tag = "6";
            this.lbl_LED_COLOR_6.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_MouseClick);
            // 
            // lbl_LED_COLOR_5
            // 
            this.lbl_LED_COLOR_5.BackColor = System.Drawing.Color.Turquoise;
            resources.ApplyResources(this.lbl_LED_COLOR_5, "lbl_LED_COLOR_5");
            this.lbl_LED_COLOR_5.Name = "lbl_LED_COLOR_5";
            this.lbl_LED_COLOR_5.Tag = "5";
            this.lbl_LED_COLOR_5.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_MouseClick);
            // 
            // lbl_LED_COLOR_4
            // 
            this.lbl_LED_COLOR_4.BackColor = System.Drawing.Color.Yellow;
            resources.ApplyResources(this.lbl_LED_COLOR_4, "lbl_LED_COLOR_4");
            this.lbl_LED_COLOR_4.Name = "lbl_LED_COLOR_4";
            this.lbl_LED_COLOR_4.Tag = "4";
            this.lbl_LED_COLOR_4.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_MouseClick);
            // 
            // lbl_LED_COLOR_3
            // 
            this.lbl_LED_COLOR_3.BackColor = System.Drawing.Color.Orange;
            resources.ApplyResources(this.lbl_LED_COLOR_3, "lbl_LED_COLOR_3");
            this.lbl_LED_COLOR_3.Name = "lbl_LED_COLOR_3";
            this.lbl_LED_COLOR_3.Tag = "3";
            this.lbl_LED_COLOR_3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_MouseClick);
            // 
            // lbl_LED_COLOR_2
            // 
            this.lbl_LED_COLOR_2.BackColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.lbl_LED_COLOR_2, "lbl_LED_COLOR_2");
            this.lbl_LED_COLOR_2.Name = "lbl_LED_COLOR_2";
            this.lbl_LED_COLOR_2.Tag = "2";
            this.lbl_LED_COLOR_2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_MouseClick);
            // 
            // lbl_LED_COLOR_1
            // 
            this.lbl_LED_COLOR_1.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lbl_LED_COLOR_1, "lbl_LED_COLOR_1");
            this.lbl_LED_COLOR_1.Name = "lbl_LED_COLOR_1";
            this.lbl_LED_COLOR_1.Tag = "1";
            this.lbl_LED_COLOR_1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_MouseClick);
            // 
            // lbl_LED_color_border
            // 
            this.lbl_LED_color_border.BackColor = System.Drawing.Color.DimGray;
            resources.ApplyResources(this.lbl_LED_color_border, "lbl_LED_color_border");
            this.lbl_LED_color_border.Name = "lbl_LED_color_border";
            // 
            // gbx_button_setup
            // 
            this.gbx_button_setup.Controls.Add(this.cmbbx_button_10);
            this.gbx_button_setup.Controls.Add(this.label33);
            this.gbx_button_setup.Controls.Add(this.cmbbx_button_9);
            this.gbx_button_setup.Controls.Add(this.label32);
            this.gbx_button_setup.Controls.Add(this.cmbbx_button_8);
            this.gbx_button_setup.Controls.Add(this.label31);
            this.gbx_button_setup.Controls.Add(this.cmbbx_button_7);
            this.gbx_button_setup.Controls.Add(this.label30);
            this.gbx_button_setup.Controls.Add(this.cmbbx_button_6);
            this.gbx_button_setup.Controls.Add(this.label29);
            this.gbx_button_setup.Controls.Add(this.cmbbx_button_5);
            this.gbx_button_setup.Controls.Add(this.label28);
            this.gbx_button_setup.Controls.Add(this.cmbbx_button_4);
            this.gbx_button_setup.Controls.Add(this.label27);
            this.gbx_button_setup.Controls.Add(this.cmbbx_button_3);
            this.gbx_button_setup.Controls.Add(this.label26);
            this.gbx_button_setup.Controls.Add(this.cmbbx_button_2);
            this.gbx_button_setup.Controls.Add(this.label25);
            this.gbx_button_setup.Controls.Add(this.cmbbx_button_1);
            this.gbx_button_setup.Controls.Add(this.label16);
            resources.ApplyResources(this.gbx_button_setup, "gbx_button_setup");
            this.gbx_button_setup.Name = "gbx_button_setup";
            this.gbx_button_setup.TabStop = false;
            // 
            // cmbbx_button_10
            // 
            this.cmbbx_button_10.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbx_button_10.FormattingEnabled = true;
            resources.ApplyResources(this.cmbbx_button_10, "cmbbx_button_10");
            this.cmbbx_button_10.Name = "cmbbx_button_10";
            // 
            // label33
            // 
            resources.ApplyResources(this.label33, "label33");
            this.label33.Name = "label33";
            // 
            // cmbbx_button_9
            // 
            this.cmbbx_button_9.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbx_button_9.FormattingEnabled = true;
            resources.ApplyResources(this.cmbbx_button_9, "cmbbx_button_9");
            this.cmbbx_button_9.Name = "cmbbx_button_9";
            // 
            // label32
            // 
            resources.ApplyResources(this.label32, "label32");
            this.label32.Name = "label32";
            // 
            // cmbbx_button_8
            // 
            this.cmbbx_button_8.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbx_button_8.FormattingEnabled = true;
            resources.ApplyResources(this.cmbbx_button_8, "cmbbx_button_8");
            this.cmbbx_button_8.Name = "cmbbx_button_8";
            // 
            // label31
            // 
            resources.ApplyResources(this.label31, "label31");
            this.label31.Name = "label31";
            // 
            // cmbbx_button_7
            // 
            this.cmbbx_button_7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbx_button_7.FormattingEnabled = true;
            resources.ApplyResources(this.cmbbx_button_7, "cmbbx_button_7");
            this.cmbbx_button_7.Name = "cmbbx_button_7";
            // 
            // label30
            // 
            resources.ApplyResources(this.label30, "label30");
            this.label30.Name = "label30";
            // 
            // cmbbx_button_6
            // 
            this.cmbbx_button_6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbx_button_6.FormattingEnabled = true;
            resources.ApplyResources(this.cmbbx_button_6, "cmbbx_button_6");
            this.cmbbx_button_6.Name = "cmbbx_button_6";
            // 
            // label29
            // 
            resources.ApplyResources(this.label29, "label29");
            this.label29.Name = "label29";
            // 
            // cmbbx_button_5
            // 
            this.cmbbx_button_5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbx_button_5.FormattingEnabled = true;
            resources.ApplyResources(this.cmbbx_button_5, "cmbbx_button_5");
            this.cmbbx_button_5.Name = "cmbbx_button_5";
            // 
            // label28
            // 
            resources.ApplyResources(this.label28, "label28");
            this.label28.Name = "label28";
            // 
            // cmbbx_button_4
            // 
            this.cmbbx_button_4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbx_button_4.FormattingEnabled = true;
            resources.ApplyResources(this.cmbbx_button_4, "cmbbx_button_4");
            this.cmbbx_button_4.Name = "cmbbx_button_4";
            // 
            // label27
            // 
            resources.ApplyResources(this.label27, "label27");
            this.label27.Name = "label27";
            // 
            // cmbbx_button_3
            // 
            this.cmbbx_button_3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbx_button_3.FormattingEnabled = true;
            resources.ApplyResources(this.cmbbx_button_3, "cmbbx_button_3");
            this.cmbbx_button_3.Name = "cmbbx_button_3";
            // 
            // label26
            // 
            resources.ApplyResources(this.label26, "label26");
            this.label26.Name = "label26";
            // 
            // cmbbx_button_2
            // 
            this.cmbbx_button_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbx_button_2.FormattingEnabled = true;
            resources.ApplyResources(this.cmbbx_button_2, "cmbbx_button_2");
            this.cmbbx_button_2.Name = "cmbbx_button_2";
            // 
            // label25
            // 
            resources.ApplyResources(this.label25, "label25");
            this.label25.Name = "label25";
            // 
            // cmbbx_button_1
            // 
            this.cmbbx_button_1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbx_button_1.FormattingEnabled = true;
            resources.ApplyResources(this.cmbbx_button_1, "cmbbx_button_1");
            this.cmbbx_button_1.Name = "cmbbx_button_1";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // lbl_button_setting_set_icon
            // 
            resources.ApplyResources(this.lbl_button_setting_set_icon, "lbl_button_setting_set_icon");
            this.lbl_button_setting_set_icon.ImageList = this.SetButton_imageList;
            this.lbl_button_setting_set_icon.Name = "lbl_button_setting_set_icon";
            this.lbl_button_setting_set_icon.Tag = "0";
            this.lbl_button_setting_set_icon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_button_setting_set_icon_MouseClick);
            this.lbl_button_setting_set_icon.MouseEnter += new System.EventHandler(this.lbl_MouseEnter);
            this.lbl_button_setting_set_icon.MouseLeave += new System.EventHandler(this.lbl_MouseLeave);
            // 
            // tabPageEncoderScript
            // 
            this.tabPageEncoderScript.Controls.Add(this.lbl_encoder_script_setting_set_icon);
            this.tabPageEncoderScript.Controls.Add(this.groupBox7);
            resources.ApplyResources(this.tabPageEncoderScript, "tabPageEncoderScript");
            this.tabPageEncoderScript.Name = "tabPageEncoderScript";
            this.tabPageEncoderScript.UseVisualStyleBackColor = true;
            // 
            // lbl_encoder_script_setting_set_icon
            // 
            resources.ApplyResources(this.lbl_encoder_script_setting_set_icon, "lbl_encoder_script_setting_set_icon");
            this.lbl_encoder_script_setting_set_icon.ImageList = this.SetButton_imageList;
            this.lbl_encoder_script_setting_set_icon.Name = "lbl_encoder_script_setting_set_icon";
            this.lbl_encoder_script_setting_set_icon.Tag = "0";
            this.lbl_encoder_script_setting_set_icon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_encoder_script_setting_set_icon_MouseClick);
            this.lbl_encoder_script_setting_set_icon.MouseEnter += new System.EventHandler(this.lbl_MouseEnter);
            this.lbl_encoder_script_setting_set_icon.MouseLeave += new System.EventHandler(this.lbl_MouseLeave);
            // 
            // groupBox7
            // 
            resources.ApplyResources(this.groupBox7, "groupBox7");
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.TabStop = false;
            // 
            // gbx_base_setting
            // 
            this.gbx_base_setting.BackColor = System.Drawing.Color.Transparent;
            this.gbx_base_setting.Controls.Add(this.gbx_keyboard_setting);
            this.gbx_base_setting.Controls.Add(this.chkbx_encoder_typematic);
            this.gbx_base_setting.Controls.Add(this.gbx_dial_func_LED_setting);
            this.gbx_base_setting.Controls.Add(this.gbx_mode_LED_setting);
            this.gbx_base_setting.Controls.Add(this.btn_base_setting_set);
            this.gbx_base_setting.Controls.Add(this.chkbx_led_sleep);
            resources.ApplyResources(this.gbx_base_setting, "gbx_base_setting");
            this.gbx_base_setting.Name = "gbx_base_setting";
            this.gbx_base_setting.TabStop = false;
            // 
            // gbx_keyboard_setting
            // 
            this.gbx_keyboard_setting.Controls.Add(this.btn_keyboard_setup_assistant);
            this.gbx_keyboard_setting.Controls.Add(this.rbtn_keyboard_ja);
            this.gbx_keyboard_setting.Controls.Add(this.rbtn_keyboard_us);
            resources.ApplyResources(this.gbx_keyboard_setting, "gbx_keyboard_setting");
            this.gbx_keyboard_setting.Name = "gbx_keyboard_setting";
            this.gbx_keyboard_setting.TabStop = false;
            // 
            // btn_keyboard_setup_assistant
            // 
            resources.ApplyResources(this.btn_keyboard_setup_assistant, "btn_keyboard_setup_assistant");
            this.btn_keyboard_setup_assistant.Name = "btn_keyboard_setup_assistant";
            this.btn_keyboard_setup_assistant.UseVisualStyleBackColor = true;
            this.btn_keyboard_setup_assistant.Click += new System.EventHandler(this.btn_keyboard_setup_assistant_Click);
            // 
            // rbtn_keyboard_ja
            // 
            resources.ApplyResources(this.rbtn_keyboard_ja, "rbtn_keyboard_ja");
            this.rbtn_keyboard_ja.Name = "rbtn_keyboard_ja";
            this.rbtn_keyboard_ja.TabStop = true;
            this.rbtn_keyboard_ja.UseVisualStyleBackColor = true;
            // 
            // rbtn_keyboard_us
            // 
            resources.ApplyResources(this.rbtn_keyboard_us, "rbtn_keyboard_us");
            this.rbtn_keyboard_us.Name = "rbtn_keyboard_us";
            this.rbtn_keyboard_us.TabStop = true;
            this.rbtn_keyboard_us.UseVisualStyleBackColor = true;
            // 
            // chkbx_encoder_typematic
            // 
            resources.ApplyResources(this.chkbx_encoder_typematic, "chkbx_encoder_typematic");
            this.chkbx_encoder_typematic.Name = "chkbx_encoder_typematic";
            this.chkbx_encoder_typematic.UseVisualStyleBackColor = true;
            // 
            // gbx_dial_func_LED_setting
            // 
            this.gbx_dial_func_LED_setting.Controls.Add(this.rbtn_func_led_on);
            this.gbx_dial_func_LED_setting.Controls.Add(this.rbtn_func_led_slow);
            this.gbx_dial_func_LED_setting.Controls.Add(this.rbtn_func_led_flash);
            resources.ApplyResources(this.gbx_dial_func_LED_setting, "gbx_dial_func_LED_setting");
            this.gbx_dial_func_LED_setting.Name = "gbx_dial_func_LED_setting";
            this.gbx_dial_func_LED_setting.TabStop = false;
            // 
            // rbtn_func_led_on
            // 
            resources.ApplyResources(this.rbtn_func_led_on, "rbtn_func_led_on");
            this.rbtn_func_led_on.Name = "rbtn_func_led_on";
            this.rbtn_func_led_on.TabStop = true;
            this.rbtn_func_led_on.UseVisualStyleBackColor = true;
            // 
            // rbtn_func_led_slow
            // 
            resources.ApplyResources(this.rbtn_func_led_slow, "rbtn_func_led_slow");
            this.rbtn_func_led_slow.Name = "rbtn_func_led_slow";
            this.rbtn_func_led_slow.TabStop = true;
            this.rbtn_func_led_slow.UseVisualStyleBackColor = true;
            // 
            // rbtn_func_led_flash
            // 
            resources.ApplyResources(this.rbtn_func_led_flash, "rbtn_func_led_flash");
            this.rbtn_func_led_flash.Name = "rbtn_func_led_flash";
            this.rbtn_func_led_flash.TabStop = true;
            this.rbtn_func_led_flash.UseVisualStyleBackColor = true;
            // 
            // gbx_mode_LED_setting
            // 
            this.gbx_mode_LED_setting.Controls.Add(this.chkbx_mode_led_off);
            this.gbx_mode_LED_setting.Controls.Add(this.lbl_mode_led_off_unit);
            this.gbx_mode_LED_setting.Controls.Add(this.num_mode_led_off_time);
            resources.ApplyResources(this.gbx_mode_LED_setting, "gbx_mode_LED_setting");
            this.gbx_mode_LED_setting.Name = "gbx_mode_LED_setting";
            this.gbx_mode_LED_setting.TabStop = false;
            // 
            // chkbx_mode_led_off
            // 
            resources.ApplyResources(this.chkbx_mode_led_off, "chkbx_mode_led_off");
            this.chkbx_mode_led_off.Name = "chkbx_mode_led_off";
            this.chkbx_mode_led_off.UseVisualStyleBackColor = true;
            this.chkbx_mode_led_off.CheckedChanged += new System.EventHandler(this.chkbx_mode_led_off_CheckedChanged);
            // 
            // lbl_mode_led_off_unit
            // 
            resources.ApplyResources(this.lbl_mode_led_off_unit, "lbl_mode_led_off_unit");
            this.lbl_mode_led_off_unit.Name = "lbl_mode_led_off_unit";
            // 
            // num_mode_led_off_time
            // 
            resources.ApplyResources(this.num_mode_led_off_time, "num_mode_led_off_time");
            this.num_mode_led_off_time.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.num_mode_led_off_time.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_mode_led_off_time.Name = "num_mode_led_off_time";
            this.num_mode_led_off_time.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btn_base_setting_set
            // 
            resources.ApplyResources(this.btn_base_setting_set, "btn_base_setting_set");
            this.btn_base_setting_set.Name = "btn_base_setting_set";
            this.btn_base_setting_set.UseVisualStyleBackColor = true;
            this.btn_base_setting_set.Click += new System.EventHandler(this.btn_base_setting_set_Click);
            // 
            // chkbx_led_sleep
            // 
            resources.ApplyResources(this.chkbx_led_sleep, "chkbx_led_sleep");
            this.chkbx_led_sleep.Name = "chkbx_led_sleep";
            this.chkbx_led_sleep.UseVisualStyleBackColor = true;
            // 
            // chkbx_encoder_script_loop
            // 
            resources.ApplyResources(this.chkbx_encoder_script_loop, "chkbx_encoder_script_loop");
            this.chkbx_encoder_script_loop.BackColor = System.Drawing.Color.Transparent;
            this.chkbx_encoder_script_loop.Name = "chkbx_encoder_script_loop";
            this.chkbx_encoder_script_loop.UseVisualStyleBackColor = false;
            // 
            // dgv_encoder_script
            // 
            this.dgv_encoder_script.AllowUserToAddRows = false;
            this.dgv_encoder_script.AllowUserToDeleteRows = false;
            this.dgv_encoder_script.AllowUserToResizeColumns = false;
            this.dgv_encoder_script.AllowUserToResizeRows = false;
            this.dgv_encoder_script.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgv_encoder_script.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_encoder_script.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgv_no,
            this.dgv_script_name});
            resources.ApplyResources(this.dgv_encoder_script, "dgv_encoder_script");
            this.dgv_encoder_script.MultiSelect = false;
            this.dgv_encoder_script.Name = "dgv_encoder_script";
            this.dgv_encoder_script.RowHeadersVisible = false;
            this.dgv_encoder_script.RowTemplate.Height = 21;
            this.dgv_encoder_script.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // dgv_no
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgv_no.DefaultCellStyle = dataGridViewCellStyle8;
            resources.ApplyResources(this.dgv_no, "dgv_no");
            this.dgv_no.Name = "dgv_no";
            this.dgv_no.ReadOnly = true;
            this.dgv_no.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_no.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgv_script_name
            // 
            resources.ApplyResources(this.dgv_script_name, "dgv_script_name");
            this.dgv_script_name.Name = "dgv_script_name";
            this.dgv_script_name.ReadOnly = true;
            this.dgv_script_name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // cmbbx_encoder_script_select_no
            // 
            this.cmbbx_encoder_script_select_no.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbbx_encoder_script_select_no, "cmbbx_encoder_script_select_no");
            this.cmbbx_encoder_script_select_no.FormattingEnabled = true;
            this.cmbbx_encoder_script_select_no.Name = "cmbbx_encoder_script_select_no";
            this.cmbbx_encoder_script_select_no.SelectedIndexChanged += new System.EventHandler(this.cmbbx_encoder_script_select_no_SelectedIndexChanged);
            // 
            // gbx_Script_Add_Info_Mouse
            // 
            this.gbx_Script_Add_Info_Mouse.BackColor = System.Drawing.Color.Black;
            this.gbx_Script_Add_Info_Mouse.Controls.Add(this.rbtn_Mouse05);
            this.gbx_Script_Add_Info_Mouse.Controls.Add(this.rbtn_Mouse04);
            this.gbx_Script_Add_Info_Mouse.Controls.Add(this.rbtn_Mouse03);
            this.gbx_Script_Add_Info_Mouse.Controls.Add(this.rbtn_Mouse02);
            this.gbx_Script_Add_Info_Mouse.Controls.Add(this.rbtn_Mouse01);
            this.gbx_Script_Add_Info_Mouse.Controls.Add(this.lbl_Script_Add_Info_Mouse_Msg);
            this.gbx_Script_Add_Info_Mouse.Controls.Add(this.lbl_Script_Add_Info_Mouse_Set);
            resources.ApplyResources(this.gbx_Script_Add_Info_Mouse, "gbx_Script_Add_Info_Mouse");
            this.gbx_Script_Add_Info_Mouse.Name = "gbx_Script_Add_Info_Mouse";
            this.gbx_Script_Add_Info_Mouse.TabStop = false;
            // 
            // rbtn_Mouse05
            // 
            resources.ApplyResources(this.rbtn_Mouse05, "rbtn_Mouse05");
            this.rbtn_Mouse05.ForeColor = System.Drawing.Color.White;
            this.rbtn_Mouse05.Name = "rbtn_Mouse05";
            this.rbtn_Mouse05.TabStop = true;
            this.rbtn_Mouse05.UseVisualStyleBackColor = true;
            // 
            // rbtn_Mouse04
            // 
            resources.ApplyResources(this.rbtn_Mouse04, "rbtn_Mouse04");
            this.rbtn_Mouse04.ForeColor = System.Drawing.Color.White;
            this.rbtn_Mouse04.Name = "rbtn_Mouse04";
            this.rbtn_Mouse04.TabStop = true;
            this.rbtn_Mouse04.UseVisualStyleBackColor = true;
            // 
            // rbtn_Mouse03
            // 
            resources.ApplyResources(this.rbtn_Mouse03, "rbtn_Mouse03");
            this.rbtn_Mouse03.ForeColor = System.Drawing.Color.White;
            this.rbtn_Mouse03.Name = "rbtn_Mouse03";
            this.rbtn_Mouse03.TabStop = true;
            this.rbtn_Mouse03.UseVisualStyleBackColor = true;
            // 
            // rbtn_Mouse02
            // 
            resources.ApplyResources(this.rbtn_Mouse02, "rbtn_Mouse02");
            this.rbtn_Mouse02.ForeColor = System.Drawing.Color.White;
            this.rbtn_Mouse02.Name = "rbtn_Mouse02";
            this.rbtn_Mouse02.TabStop = true;
            this.rbtn_Mouse02.UseVisualStyleBackColor = true;
            // 
            // rbtn_Mouse01
            // 
            resources.ApplyResources(this.rbtn_Mouse01, "rbtn_Mouse01");
            this.rbtn_Mouse01.ForeColor = System.Drawing.Color.White;
            this.rbtn_Mouse01.Name = "rbtn_Mouse01";
            this.rbtn_Mouse01.TabStop = true;
            this.rbtn_Mouse01.UseVisualStyleBackColor = true;
            // 
            // lbl_Script_Add_Info_Mouse_Msg
            // 
            this.lbl_Script_Add_Info_Mouse_Msg.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.lbl_Script_Add_Info_Mouse_Msg, "lbl_Script_Add_Info_Mouse_Msg");
            this.lbl_Script_Add_Info_Mouse_Msg.ForeColor = System.Drawing.Color.White;
            this.lbl_Script_Add_Info_Mouse_Msg.Name = "lbl_Script_Add_Info_Mouse_Msg";
            // 
            // lbl_Script_Add_Info_Mouse_Set
            // 
            this.lbl_Script_Add_Info_Mouse_Set.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lbl_Script_Add_Info_Mouse_Set, "lbl_Script_Add_Info_Mouse_Set");
            this.lbl_Script_Add_Info_Mouse_Set.Name = "lbl_Script_Add_Info_Mouse_Set";
            this.lbl_Script_Add_Info_Mouse_Set.Tag = "0";
            this.lbl_Script_Add_Info_Mouse_Set.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_Script_Add_Info_Mouse_Set_MouseClick);
            this.lbl_Script_Add_Info_Mouse_Set.MouseEnter += new System.EventHandler(this.lbl_MouseEnter);
            this.lbl_Script_Add_Info_Mouse_Set.MouseLeave += new System.EventHandler(this.lbl_MouseLeave);
            // 
            // gbx_Script_Add_Info_MultiMedia
            // 
            this.gbx_Script_Add_Info_MultiMedia.BackColor = System.Drawing.Color.Black;
            this.gbx_Script_Add_Info_MultiMedia.Controls.Add(this.rbtn_MultiMedia11);
            this.gbx_Script_Add_Info_MultiMedia.Controls.Add(this.rbtn_MultiMedia10);
            this.gbx_Script_Add_Info_MultiMedia.Controls.Add(this.rbtn_MultiMedia09);
            this.gbx_Script_Add_Info_MultiMedia.Controls.Add(this.rbtn_MultiMedia08);
            this.gbx_Script_Add_Info_MultiMedia.Controls.Add(this.rbtn_MultiMedia07);
            this.gbx_Script_Add_Info_MultiMedia.Controls.Add(this.rbtn_MultiMedia06);
            this.gbx_Script_Add_Info_MultiMedia.Controls.Add(this.rbtn_MultiMedia05);
            this.gbx_Script_Add_Info_MultiMedia.Controls.Add(this.rbtn_MultiMedia04);
            this.gbx_Script_Add_Info_MultiMedia.Controls.Add(this.rbtn_MultiMedia03);
            this.gbx_Script_Add_Info_MultiMedia.Controls.Add(this.rbtn_MultiMedia02);
            this.gbx_Script_Add_Info_MultiMedia.Controls.Add(this.rbtn_MultiMedia01);
            this.gbx_Script_Add_Info_MultiMedia.Controls.Add(this.lbl_Script_Add_Info_MultiMedia_Msg);
            this.gbx_Script_Add_Info_MultiMedia.Controls.Add(this.lbl_Script_Add_Info_MultiMedia_Set);
            resources.ApplyResources(this.gbx_Script_Add_Info_MultiMedia, "gbx_Script_Add_Info_MultiMedia");
            this.gbx_Script_Add_Info_MultiMedia.Name = "gbx_Script_Add_Info_MultiMedia";
            this.gbx_Script_Add_Info_MultiMedia.TabStop = false;
            // 
            // rbtn_MultiMedia11
            // 
            resources.ApplyResources(this.rbtn_MultiMedia11, "rbtn_MultiMedia11");
            this.rbtn_MultiMedia11.ForeColor = System.Drawing.Color.White;
            this.rbtn_MultiMedia11.Name = "rbtn_MultiMedia11";
            this.rbtn_MultiMedia11.TabStop = true;
            this.rbtn_MultiMedia11.UseVisualStyleBackColor = true;
            // 
            // rbtn_MultiMedia10
            // 
            resources.ApplyResources(this.rbtn_MultiMedia10, "rbtn_MultiMedia10");
            this.rbtn_MultiMedia10.ForeColor = System.Drawing.Color.White;
            this.rbtn_MultiMedia10.Name = "rbtn_MultiMedia10";
            this.rbtn_MultiMedia10.TabStop = true;
            this.rbtn_MultiMedia10.UseVisualStyleBackColor = true;
            // 
            // rbtn_MultiMedia09
            // 
            resources.ApplyResources(this.rbtn_MultiMedia09, "rbtn_MultiMedia09");
            this.rbtn_MultiMedia09.ForeColor = System.Drawing.Color.White;
            this.rbtn_MultiMedia09.Name = "rbtn_MultiMedia09";
            this.rbtn_MultiMedia09.TabStop = true;
            this.rbtn_MultiMedia09.UseVisualStyleBackColor = true;
            // 
            // rbtn_MultiMedia08
            // 
            resources.ApplyResources(this.rbtn_MultiMedia08, "rbtn_MultiMedia08");
            this.rbtn_MultiMedia08.ForeColor = System.Drawing.Color.White;
            this.rbtn_MultiMedia08.Name = "rbtn_MultiMedia08";
            this.rbtn_MultiMedia08.TabStop = true;
            this.rbtn_MultiMedia08.UseVisualStyleBackColor = true;
            // 
            // rbtn_MultiMedia07
            // 
            resources.ApplyResources(this.rbtn_MultiMedia07, "rbtn_MultiMedia07");
            this.rbtn_MultiMedia07.ForeColor = System.Drawing.Color.White;
            this.rbtn_MultiMedia07.Name = "rbtn_MultiMedia07";
            this.rbtn_MultiMedia07.TabStop = true;
            this.rbtn_MultiMedia07.UseVisualStyleBackColor = true;
            // 
            // rbtn_MultiMedia06
            // 
            resources.ApplyResources(this.rbtn_MultiMedia06, "rbtn_MultiMedia06");
            this.rbtn_MultiMedia06.ForeColor = System.Drawing.Color.White;
            this.rbtn_MultiMedia06.Name = "rbtn_MultiMedia06";
            this.rbtn_MultiMedia06.TabStop = true;
            this.rbtn_MultiMedia06.UseVisualStyleBackColor = true;
            // 
            // rbtn_MultiMedia05
            // 
            resources.ApplyResources(this.rbtn_MultiMedia05, "rbtn_MultiMedia05");
            this.rbtn_MultiMedia05.ForeColor = System.Drawing.Color.White;
            this.rbtn_MultiMedia05.Name = "rbtn_MultiMedia05";
            this.rbtn_MultiMedia05.TabStop = true;
            this.rbtn_MultiMedia05.UseVisualStyleBackColor = true;
            // 
            // rbtn_MultiMedia04
            // 
            resources.ApplyResources(this.rbtn_MultiMedia04, "rbtn_MultiMedia04");
            this.rbtn_MultiMedia04.ForeColor = System.Drawing.Color.White;
            this.rbtn_MultiMedia04.Name = "rbtn_MultiMedia04";
            this.rbtn_MultiMedia04.TabStop = true;
            this.rbtn_MultiMedia04.UseVisualStyleBackColor = true;
            // 
            // rbtn_MultiMedia03
            // 
            resources.ApplyResources(this.rbtn_MultiMedia03, "rbtn_MultiMedia03");
            this.rbtn_MultiMedia03.ForeColor = System.Drawing.Color.White;
            this.rbtn_MultiMedia03.Name = "rbtn_MultiMedia03";
            this.rbtn_MultiMedia03.TabStop = true;
            this.rbtn_MultiMedia03.UseVisualStyleBackColor = true;
            // 
            // rbtn_MultiMedia02
            // 
            resources.ApplyResources(this.rbtn_MultiMedia02, "rbtn_MultiMedia02");
            this.rbtn_MultiMedia02.ForeColor = System.Drawing.Color.White;
            this.rbtn_MultiMedia02.Name = "rbtn_MultiMedia02";
            this.rbtn_MultiMedia02.TabStop = true;
            this.rbtn_MultiMedia02.UseVisualStyleBackColor = true;
            // 
            // rbtn_MultiMedia01
            // 
            resources.ApplyResources(this.rbtn_MultiMedia01, "rbtn_MultiMedia01");
            this.rbtn_MultiMedia01.ForeColor = System.Drawing.Color.White;
            this.rbtn_MultiMedia01.Name = "rbtn_MultiMedia01";
            this.rbtn_MultiMedia01.TabStop = true;
            this.rbtn_MultiMedia01.UseVisualStyleBackColor = true;
            // 
            // lbl_Script_Add_Info_MultiMedia_Msg
            // 
            this.lbl_Script_Add_Info_MultiMedia_Msg.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.lbl_Script_Add_Info_MultiMedia_Msg, "lbl_Script_Add_Info_MultiMedia_Msg");
            this.lbl_Script_Add_Info_MultiMedia_Msg.ForeColor = System.Drawing.Color.White;
            this.lbl_Script_Add_Info_MultiMedia_Msg.Name = "lbl_Script_Add_Info_MultiMedia_Msg";
            // 
            // lbl_Script_Add_Info_MultiMedia_Set
            // 
            this.lbl_Script_Add_Info_MultiMedia_Set.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lbl_Script_Add_Info_MultiMedia_Set, "lbl_Script_Add_Info_MultiMedia_Set");
            this.lbl_Script_Add_Info_MultiMedia_Set.Name = "lbl_Script_Add_Info_MultiMedia_Set";
            this.lbl_Script_Add_Info_MultiMedia_Set.Tag = "0";
            this.lbl_Script_Add_Info_MultiMedia_Set.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_Script_Add_Info_MultiMedia_Set_MouseClick);
            this.lbl_Script_Add_Info_MultiMedia_Set.MouseEnter += new System.EventHandler(this.lbl_MouseEnter);
            this.lbl_Script_Add_Info_MultiMedia_Set.MouseLeave += new System.EventHandler(this.lbl_MouseLeave);
            // 
            // lbl_MultiMediaRelease_Icon
            // 
            this.lbl_MultiMediaRelease_Icon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lbl_MultiMediaRelease_Icon, "lbl_MultiMediaRelease_Icon");
            this.lbl_MultiMediaRelease_Icon.Name = "lbl_MultiMediaRelease_Icon";
            this.lbl_MultiMediaRelease_Icon.Tag = "16";
            this.lbl_MultiMediaRelease_Icon.DragOver += new System.Windows.Forms.DragEventHandler(this.lbl_Script_Icon_DragOver);
            this.lbl_MultiMediaRelease_Icon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbl_MultiMediaRelease_Icon_MouseDown);
            this.lbl_MultiMediaRelease_Icon.MouseEnter += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseEnter);
            this.lbl_MultiMediaRelease_Icon.MouseLeave += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseLeave);
            this.lbl_MultiMediaRelease_Icon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbl_Script_Icon_MouseMove);
            this.lbl_MultiMediaRelease_Icon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbl_Script_Icon_MouseUp);
            // 
            // lbl_MultiMediaPress_Icon
            // 
            this.lbl_MultiMediaPress_Icon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lbl_MultiMediaPress_Icon, "lbl_MultiMediaPress_Icon");
            this.lbl_MultiMediaPress_Icon.Name = "lbl_MultiMediaPress_Icon";
            this.lbl_MultiMediaPress_Icon.Tag = "15";
            this.lbl_MultiMediaPress_Icon.DragOver += new System.Windows.Forms.DragEventHandler(this.lbl_Script_Icon_DragOver);
            this.lbl_MultiMediaPress_Icon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbl_MultiMediaPress_Icon_MouseDown);
            this.lbl_MultiMediaPress_Icon.MouseEnter += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseEnter);
            this.lbl_MultiMediaPress_Icon.MouseLeave += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseLeave);
            this.lbl_MultiMediaPress_Icon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbl_Script_Icon_MouseMove);
            this.lbl_MultiMediaPress_Icon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbl_Script_Icon_MouseUp);
            // 
            // lbl_MouseMovePress_Icon
            // 
            this.lbl_MouseMovePress_Icon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lbl_MouseMovePress_Icon, "lbl_MouseMovePress_Icon");
            this.lbl_MouseMovePress_Icon.Name = "lbl_MouseMovePress_Icon";
            this.lbl_MouseMovePress_Icon.Tag = "14";
            this.lbl_MouseMovePress_Icon.DragOver += new System.Windows.Forms.DragEventHandler(this.lbl_Script_Icon_DragOver);
            this.lbl_MouseMovePress_Icon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbl_MouseMovePress_Icon_MouseDown);
            this.lbl_MouseMovePress_Icon.MouseEnter += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseEnter);
            this.lbl_MouseMovePress_Icon.MouseLeave += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseLeave);
            this.lbl_MouseMovePress_Icon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbl_Script_Icon_MouseMove);
            this.lbl_MouseMovePress_Icon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbl_Script_Icon_MouseUp);
            // 
            // pgb_process_status
            // 
            resources.ApplyResources(this.pgb_process_status, "pgb_process_status");
            this.pgb_process_status.Name = "pgb_process_status";
            // 
            // lbl_button_factory_reset_icon
            // 
            resources.ApplyResources(this.lbl_button_factory_reset_icon, "lbl_button_factory_reset_icon");
            this.lbl_button_factory_reset_icon.ImageList = this.Reset_Icon_imageList;
            this.lbl_button_factory_reset_icon.Name = "lbl_button_factory_reset_icon";
            this.lbl_button_factory_reset_icon.Tag = "0";
            this.lbl_button_factory_reset_icon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_button_factory_reset_icon_MouseClick);
            this.lbl_button_factory_reset_icon.MouseEnter += new System.EventHandler(this.lbl_MouseEnter);
            this.lbl_button_factory_reset_icon.MouseLeave += new System.EventHandler(this.lbl_MouseLeave);
            // 
            // Reset_Icon_imageList
            // 
            this.Reset_Icon_imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("Reset_Icon_imageList.ImageStream")));
            this.Reset_Icon_imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.Reset_Icon_imageList.Images.SetKeyName(0, "LEFTBN_DEFAULT.png");
            this.Reset_Icon_imageList.Images.SetKeyName(1, "LEFTBN_DEFAULT_ACTIVE.png");
            // 
            // lbl_reset_title
            // 
            this.lbl_reset_title.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lbl_reset_title, "lbl_reset_title");
            this.lbl_reset_title.Name = "lbl_reset_title";
            // 
            // lbl_reset_bg
            // 
            this.lbl_reset_bg.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lbl_reset_bg, "lbl_reset_bg");
            this.lbl_reset_bg.Name = "lbl_reset_bg";
            // 
            // lbl_REC_Icon
            // 
            resources.ApplyResources(this.lbl_REC_Icon, "lbl_REC_Icon");
            this.lbl_REC_Icon.Name = "lbl_REC_Icon";
            this.lbl_REC_Icon.Tag = "0";
            // 
            // lbl_Arrow_Dust
            // 
            resources.ApplyResources(this.lbl_Arrow_Dust, "lbl_Arrow_Dust");
            this.lbl_Arrow_Dust.Name = "lbl_Arrow_Dust";
            // 
            // lbl_progress_total_value
            // 
            this.lbl_progress_total_value.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lbl_progress_total_value, "lbl_progress_total_value");
            this.lbl_progress_total_value.ForeColor = System.Drawing.Color.DarkGray;
            this.lbl_progress_total_value.Name = "lbl_progress_total_value";
            // 
            // lbl_progress_now_value
            // 
            this.lbl_progress_now_value.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lbl_progress_now_value, "lbl_progress_now_value");
            this.lbl_progress_now_value.ForeColor = System.Drawing.Color.DarkGray;
            this.lbl_progress_now_value.Name = "lbl_progress_now_value";
            // 
            // lbl_progress_per
            // 
            this.lbl_progress_per.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lbl_progress_per, "lbl_progress_per");
            this.lbl_progress_per.ForeColor = System.Drawing.Color.DarkGray;
            this.lbl_progress_per.Name = "lbl_progress_per";
            // 
            // gbx_MacroREC
            // 
            this.gbx_MacroREC.BackColor = System.Drawing.Color.White;
            this.gbx_MacroREC.Controls.Add(this.lbl_Clear_btn);
            this.gbx_MacroREC.Controls.Add(this.lbl_REC_Btn);
            this.gbx_MacroREC.Controls.Add(this.lbl_REC_Icon);
            resources.ApplyResources(this.gbx_MacroREC, "gbx_MacroREC");
            this.gbx_MacroREC.Name = "gbx_MacroREC";
            this.gbx_MacroREC.TabStop = false;
            // 
            // lbl_Clear_btn
            // 
            this.lbl_Clear_btn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lbl_Clear_btn, "lbl_Clear_btn");
            this.lbl_Clear_btn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_Clear_btn.Name = "lbl_Clear_btn";
            this.lbl_Clear_btn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_Clear_btn_MouseClick);
            this.lbl_Clear_btn.MouseEnter += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseEnter);
            this.lbl_Clear_btn.MouseLeave += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseLeave);
            // 
            // lbl_REC_Btn
            // 
            this.lbl_REC_Btn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lbl_REC_Btn, "lbl_REC_Btn");
            this.lbl_REC_Btn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_REC_Btn.Name = "lbl_REC_Btn";
            this.lbl_REC_Btn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_REC_Btn_MouseClick);
            this.lbl_REC_Btn.MouseEnter += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseEnter);
            this.lbl_REC_Btn.MouseLeave += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseLeave);
            // 
            // gbx_MacroFileImportExport
            // 
            this.gbx_MacroFileImportExport.BackColor = System.Drawing.Color.White;
            this.gbx_MacroFileImportExport.Controls.Add(this.lbl_MacroFileExport_Icon);
            this.gbx_MacroFileImportExport.Controls.Add(this.lbl_MacroFileImportExportTitle_Icon);
            this.gbx_MacroFileImportExport.Controls.Add(this.lbl_MacroFileImport_Icon);
            resources.ApplyResources(this.gbx_MacroFileImportExport, "gbx_MacroFileImportExport");
            this.gbx_MacroFileImportExport.Name = "gbx_MacroFileImportExport";
            this.gbx_MacroFileImportExport.TabStop = false;
            // 
            // lbl_MacroFileExport_Icon
            // 
            this.lbl_MacroFileExport_Icon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lbl_MacroFileExport_Icon, "lbl_MacroFileExport_Icon");
            this.lbl_MacroFileExport_Icon.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_MacroFileExport_Icon.Name = "lbl_MacroFileExport_Icon";
            this.lbl_MacroFileExport_Icon.Tag = "0";
            this.lbl_MacroFileExport_Icon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_MacroFileExport_Icon_MouseClick);
            this.lbl_MacroFileExport_Icon.MouseEnter += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseEnter);
            this.lbl_MacroFileExport_Icon.MouseLeave += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseLeave);
            // 
            // lbl_MacroFileImportExportTitle_Icon
            // 
            resources.ApplyResources(this.lbl_MacroFileImportExportTitle_Icon, "lbl_MacroFileImportExportTitle_Icon");
            this.lbl_MacroFileImportExportTitle_Icon.Name = "lbl_MacroFileImportExportTitle_Icon";
            this.lbl_MacroFileImportExportTitle_Icon.Tag = "0";
            // 
            // lbl_MacroFileImport_Icon
            // 
            this.lbl_MacroFileImport_Icon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lbl_MacroFileImport_Icon, "lbl_MacroFileImport_Icon");
            this.lbl_MacroFileImport_Icon.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_MacroFileImport_Icon.Name = "lbl_MacroFileImport_Icon";
            this.lbl_MacroFileImport_Icon.Tag = "0";
            this.lbl_MacroFileImport_Icon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_MacroFileImport_Icon_MouseClick);
            this.lbl_MacroFileImport_Icon.MouseEnter += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseEnter);
            this.lbl_MacroFileImport_Icon.MouseLeave += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseLeave);
            // 
            // MacroFileImportExport_imageList
            // 
            this.MacroFileImportExport_imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("MacroFileImportExport_imageList.ImageStream")));
            this.MacroFileImportExport_imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.MacroFileImportExport_imageList.Images.SetKeyName(0, "BN_MACRO-HOZON.png");
            this.MacroFileImportExport_imageList.Images.SetKeyName(1, "BN_MACRO-HOZON_ACTIVE.png");
            this.MacroFileImportExport_imageList.Images.SetKeyName(2, "BN_MACRO-YOMIKOMI.png");
            this.MacroFileImportExport_imageList.Images.SetKeyName(3, "BN_MACRO-YOMIKOMI_ACTIVE.png");
            // 
            // gbx_Script_Add_Info_JoysticLever
            // 
            this.gbx_Script_Add_Info_JoysticLever.BackColor = System.Drawing.Color.Black;
            this.gbx_Script_Add_Info_JoysticLever.Controls.Add(this.lbl_Script_Add_Info_JoysticLever_Set);
            this.gbx_Script_Add_Info_JoysticLever.Controls.Add(this.lbl_Script_Add_Info_JoysticLever_X);
            this.gbx_Script_Add_Info_JoysticLever.Controls.Add(this.lbl_Script_Add_Info_JoysticLever_Y);
            this.gbx_Script_Add_Info_JoysticLever.Controls.Add(this.lbl_Script_Add_Info_JoysticLever_Msg);
            this.gbx_Script_Add_Info_JoysticLever.Controls.Add(this.txtbx_Script_Add_Info_JoysticLever_Y);
            this.gbx_Script_Add_Info_JoysticLever.Controls.Add(this.txtbx_Script_Add_Info_JoysticLever_X);
            resources.ApplyResources(this.gbx_Script_Add_Info_JoysticLever, "gbx_Script_Add_Info_JoysticLever");
            this.gbx_Script_Add_Info_JoysticLever.Name = "gbx_Script_Add_Info_JoysticLever";
            this.gbx_Script_Add_Info_JoysticLever.TabStop = false;
            // 
            // lbl_Script_Add_Info_JoysticLever_Set
            // 
            this.lbl_Script_Add_Info_JoysticLever_Set.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lbl_Script_Add_Info_JoysticLever_Set, "lbl_Script_Add_Info_JoysticLever_Set");
            this.lbl_Script_Add_Info_JoysticLever_Set.Name = "lbl_Script_Add_Info_JoysticLever_Set";
            this.lbl_Script_Add_Info_JoysticLever_Set.Tag = "0";
            this.lbl_Script_Add_Info_JoysticLever_Set.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_Script_Add_Info_JoysticLever_Set_MouseClick);
            this.lbl_Script_Add_Info_JoysticLever_Set.MouseEnter += new System.EventHandler(this.lbl_MouseEnter);
            this.lbl_Script_Add_Info_JoysticLever_Set.MouseLeave += new System.EventHandler(this.lbl_MouseLeave);
            // 
            // lbl_Script_Add_Info_JoysticLever_X
            // 
            this.lbl_Script_Add_Info_JoysticLever_X.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.lbl_Script_Add_Info_JoysticLever_X, "lbl_Script_Add_Info_JoysticLever_X");
            this.lbl_Script_Add_Info_JoysticLever_X.ForeColor = System.Drawing.Color.White;
            this.lbl_Script_Add_Info_JoysticLever_X.Name = "lbl_Script_Add_Info_JoysticLever_X";
            // 
            // lbl_Script_Add_Info_JoysticLever_Y
            // 
            this.lbl_Script_Add_Info_JoysticLever_Y.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.lbl_Script_Add_Info_JoysticLever_Y, "lbl_Script_Add_Info_JoysticLever_Y");
            this.lbl_Script_Add_Info_JoysticLever_Y.ForeColor = System.Drawing.Color.White;
            this.lbl_Script_Add_Info_JoysticLever_Y.Name = "lbl_Script_Add_Info_JoysticLever_Y";
            // 
            // lbl_Script_Add_Info_JoysticLever_Msg
            // 
            this.lbl_Script_Add_Info_JoysticLever_Msg.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.lbl_Script_Add_Info_JoysticLever_Msg, "lbl_Script_Add_Info_JoysticLever_Msg");
            this.lbl_Script_Add_Info_JoysticLever_Msg.ForeColor = System.Drawing.Color.White;
            this.lbl_Script_Add_Info_JoysticLever_Msg.Name = "lbl_Script_Add_Info_JoysticLever_Msg";
            // 
            // txtbx_Script_Add_Info_JoysticLever_Y
            // 
            resources.ApplyResources(this.txtbx_Script_Add_Info_JoysticLever_Y, "txtbx_Script_Add_Info_JoysticLever_Y");
            this.txtbx_Script_Add_Info_JoysticLever_Y.Name = "txtbx_Script_Add_Info_JoysticLever_Y";
            // 
            // txtbx_Script_Add_Info_JoysticLever_X
            // 
            resources.ApplyResources(this.txtbx_Script_Add_Info_JoysticLever_X, "txtbx_Script_Add_Info_JoysticLever_X");
            this.txtbx_Script_Add_Info_JoysticLever_X.Name = "txtbx_Script_Add_Info_JoysticLever_X";
            // 
            // gbx_Script_Add_Info_JoysticButton
            // 
            this.gbx_Script_Add_Info_JoysticButton.BackColor = System.Drawing.Color.Black;
            this.gbx_Script_Add_Info_JoysticButton.Controls.Add(this.rbtn_JoystickButton13);
            this.gbx_Script_Add_Info_JoysticButton.Controls.Add(this.rbtn_JoystickButton12);
            this.gbx_Script_Add_Info_JoysticButton.Controls.Add(this.rbtn_JoystickButton11);
            this.gbx_Script_Add_Info_JoysticButton.Controls.Add(this.rbtn_JoystickButton10);
            this.gbx_Script_Add_Info_JoysticButton.Controls.Add(this.rbtn_JoystickButton09);
            this.gbx_Script_Add_Info_JoysticButton.Controls.Add(this.rbtn_JoystickButton08);
            this.gbx_Script_Add_Info_JoysticButton.Controls.Add(this.rbtn_JoystickButton07);
            this.gbx_Script_Add_Info_JoysticButton.Controls.Add(this.rbtn_JoystickButton06);
            this.gbx_Script_Add_Info_JoysticButton.Controls.Add(this.rbtn_JoystickButton05);
            this.gbx_Script_Add_Info_JoysticButton.Controls.Add(this.rbtn_JoystickButton04);
            this.gbx_Script_Add_Info_JoysticButton.Controls.Add(this.rbtn_JoystickButton03);
            this.gbx_Script_Add_Info_JoysticButton.Controls.Add(this.rbtn_JoystickButton02);
            this.gbx_Script_Add_Info_JoysticButton.Controls.Add(this.rbtn_JoystickButton01);
            this.gbx_Script_Add_Info_JoysticButton.Controls.Add(this.lbl_Script_Add_Info_JoysticButton_Msg);
            this.gbx_Script_Add_Info_JoysticButton.Controls.Add(this.lbl_Script_Add_Info_JoysticButton_Set);
            resources.ApplyResources(this.gbx_Script_Add_Info_JoysticButton, "gbx_Script_Add_Info_JoysticButton");
            this.gbx_Script_Add_Info_JoysticButton.Name = "gbx_Script_Add_Info_JoysticButton";
            this.gbx_Script_Add_Info_JoysticButton.TabStop = false;
            // 
            // rbtn_JoystickButton13
            // 
            resources.ApplyResources(this.rbtn_JoystickButton13, "rbtn_JoystickButton13");
            this.rbtn_JoystickButton13.ForeColor = System.Drawing.Color.White;
            this.rbtn_JoystickButton13.Name = "rbtn_JoystickButton13";
            this.rbtn_JoystickButton13.TabStop = true;
            this.rbtn_JoystickButton13.UseVisualStyleBackColor = true;
            // 
            // rbtn_JoystickButton12
            // 
            resources.ApplyResources(this.rbtn_JoystickButton12, "rbtn_JoystickButton12");
            this.rbtn_JoystickButton12.ForeColor = System.Drawing.Color.White;
            this.rbtn_JoystickButton12.Name = "rbtn_JoystickButton12";
            this.rbtn_JoystickButton12.TabStop = true;
            this.rbtn_JoystickButton12.UseVisualStyleBackColor = true;
            // 
            // rbtn_JoystickButton11
            // 
            resources.ApplyResources(this.rbtn_JoystickButton11, "rbtn_JoystickButton11");
            this.rbtn_JoystickButton11.ForeColor = System.Drawing.Color.White;
            this.rbtn_JoystickButton11.Name = "rbtn_JoystickButton11";
            this.rbtn_JoystickButton11.TabStop = true;
            this.rbtn_JoystickButton11.UseVisualStyleBackColor = true;
            // 
            // rbtn_JoystickButton10
            // 
            resources.ApplyResources(this.rbtn_JoystickButton10, "rbtn_JoystickButton10");
            this.rbtn_JoystickButton10.ForeColor = System.Drawing.Color.White;
            this.rbtn_JoystickButton10.Name = "rbtn_JoystickButton10";
            this.rbtn_JoystickButton10.TabStop = true;
            this.rbtn_JoystickButton10.UseVisualStyleBackColor = true;
            // 
            // rbtn_JoystickButton09
            // 
            resources.ApplyResources(this.rbtn_JoystickButton09, "rbtn_JoystickButton09");
            this.rbtn_JoystickButton09.ForeColor = System.Drawing.Color.White;
            this.rbtn_JoystickButton09.Name = "rbtn_JoystickButton09";
            this.rbtn_JoystickButton09.TabStop = true;
            this.rbtn_JoystickButton09.UseVisualStyleBackColor = true;
            // 
            // rbtn_JoystickButton08
            // 
            resources.ApplyResources(this.rbtn_JoystickButton08, "rbtn_JoystickButton08");
            this.rbtn_JoystickButton08.ForeColor = System.Drawing.Color.White;
            this.rbtn_JoystickButton08.Name = "rbtn_JoystickButton08";
            this.rbtn_JoystickButton08.TabStop = true;
            this.rbtn_JoystickButton08.UseVisualStyleBackColor = true;
            // 
            // rbtn_JoystickButton07
            // 
            resources.ApplyResources(this.rbtn_JoystickButton07, "rbtn_JoystickButton07");
            this.rbtn_JoystickButton07.ForeColor = System.Drawing.Color.White;
            this.rbtn_JoystickButton07.Name = "rbtn_JoystickButton07";
            this.rbtn_JoystickButton07.TabStop = true;
            this.rbtn_JoystickButton07.UseVisualStyleBackColor = true;
            // 
            // rbtn_JoystickButton06
            // 
            resources.ApplyResources(this.rbtn_JoystickButton06, "rbtn_JoystickButton06");
            this.rbtn_JoystickButton06.ForeColor = System.Drawing.Color.White;
            this.rbtn_JoystickButton06.Name = "rbtn_JoystickButton06";
            this.rbtn_JoystickButton06.TabStop = true;
            this.rbtn_JoystickButton06.UseVisualStyleBackColor = true;
            // 
            // rbtn_JoystickButton05
            // 
            resources.ApplyResources(this.rbtn_JoystickButton05, "rbtn_JoystickButton05");
            this.rbtn_JoystickButton05.ForeColor = System.Drawing.Color.White;
            this.rbtn_JoystickButton05.Name = "rbtn_JoystickButton05";
            this.rbtn_JoystickButton05.TabStop = true;
            this.rbtn_JoystickButton05.UseVisualStyleBackColor = true;
            // 
            // rbtn_JoystickButton04
            // 
            resources.ApplyResources(this.rbtn_JoystickButton04, "rbtn_JoystickButton04");
            this.rbtn_JoystickButton04.ForeColor = System.Drawing.Color.White;
            this.rbtn_JoystickButton04.Name = "rbtn_JoystickButton04";
            this.rbtn_JoystickButton04.TabStop = true;
            this.rbtn_JoystickButton04.UseVisualStyleBackColor = true;
            // 
            // rbtn_JoystickButton03
            // 
            resources.ApplyResources(this.rbtn_JoystickButton03, "rbtn_JoystickButton03");
            this.rbtn_JoystickButton03.ForeColor = System.Drawing.Color.White;
            this.rbtn_JoystickButton03.Name = "rbtn_JoystickButton03";
            this.rbtn_JoystickButton03.TabStop = true;
            this.rbtn_JoystickButton03.UseVisualStyleBackColor = true;
            // 
            // rbtn_JoystickButton02
            // 
            resources.ApplyResources(this.rbtn_JoystickButton02, "rbtn_JoystickButton02");
            this.rbtn_JoystickButton02.ForeColor = System.Drawing.Color.White;
            this.rbtn_JoystickButton02.Name = "rbtn_JoystickButton02";
            this.rbtn_JoystickButton02.TabStop = true;
            this.rbtn_JoystickButton02.UseVisualStyleBackColor = true;
            // 
            // rbtn_JoystickButton01
            // 
            resources.ApplyResources(this.rbtn_JoystickButton01, "rbtn_JoystickButton01");
            this.rbtn_JoystickButton01.ForeColor = System.Drawing.Color.White;
            this.rbtn_JoystickButton01.Name = "rbtn_JoystickButton01";
            this.rbtn_JoystickButton01.TabStop = true;
            this.rbtn_JoystickButton01.UseVisualStyleBackColor = true;
            // 
            // lbl_Script_Add_Info_JoysticButton_Msg
            // 
            this.lbl_Script_Add_Info_JoysticButton_Msg.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.lbl_Script_Add_Info_JoysticButton_Msg, "lbl_Script_Add_Info_JoysticButton_Msg");
            this.lbl_Script_Add_Info_JoysticButton_Msg.ForeColor = System.Drawing.Color.White;
            this.lbl_Script_Add_Info_JoysticButton_Msg.Name = "lbl_Script_Add_Info_JoysticButton_Msg";
            // 
            // lbl_Script_Add_Info_JoysticButton_Set
            // 
            this.lbl_Script_Add_Info_JoysticButton_Set.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lbl_Script_Add_Info_JoysticButton_Set, "lbl_Script_Add_Info_JoysticButton_Set");
            this.lbl_Script_Add_Info_JoysticButton_Set.Name = "lbl_Script_Add_Info_JoysticButton_Set";
            this.lbl_Script_Add_Info_JoysticButton_Set.Tag = "0";
            this.lbl_Script_Add_Info_JoysticButton_Set.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_Script_Add_Info_JoysticButton_Set_MouseClick);
            this.lbl_Script_Add_Info_JoysticButton_Set.MouseEnter += new System.EventHandler(this.lbl_MouseEnter);
            this.lbl_Script_Add_Info_JoysticButton_Set.MouseLeave += new System.EventHandler(this.lbl_MouseLeave);
            // 
            // gbx_Script_Add_Info
            // 
            this.gbx_Script_Add_Info.BackColor = System.Drawing.Color.Black;
            this.gbx_Script_Add_Info.Controls.Add(this.lbl_Script_Add_Info);
            this.gbx_Script_Add_Info.Controls.Add(this.txtbx_Interval_Inputbox);
            this.gbx_Script_Add_Info.Controls.Add(this.lbl_Script_Add_Info_ClickArea);
            resources.ApplyResources(this.gbx_Script_Add_Info, "gbx_Script_Add_Info");
            this.gbx_Script_Add_Info.Name = "gbx_Script_Add_Info";
            this.gbx_Script_Add_Info.TabStop = false;
            // 
            // lbl_Script_Add_Info_ClickArea
            // 
            this.lbl_Script_Add_Info_ClickArea.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.lbl_Script_Add_Info_ClickArea, "lbl_Script_Add_Info_ClickArea");
            this.lbl_Script_Add_Info_ClickArea.Name = "lbl_Script_Add_Info_ClickArea";
            this.lbl_Script_Add_Info_ClickArea.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.lbl_Script_Add_Info_ClickArea.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            // 
            // lbl_Arrow_Icon7
            // 
            this.lbl_Arrow_Icon7.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lbl_Arrow_Icon7, "lbl_Arrow_Icon7");
            this.lbl_Arrow_Icon7.Name = "lbl_Arrow_Icon7";
            // 
            // lbl_Arrow_Icon6
            // 
            this.lbl_Arrow_Icon6.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lbl_Arrow_Icon6, "lbl_Arrow_Icon6");
            this.lbl_Arrow_Icon6.Name = "lbl_Arrow_Icon6";
            // 
            // lbl_Arrow_Icon5
            // 
            this.lbl_Arrow_Icon5.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lbl_Arrow_Icon5, "lbl_Arrow_Icon5");
            this.lbl_Arrow_Icon5.Name = "lbl_Arrow_Icon5";
            // 
            // lbl_Arrow_Icon4
            // 
            this.lbl_Arrow_Icon4.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lbl_Arrow_Icon4, "lbl_Arrow_Icon4");
            this.lbl_Arrow_Icon4.Name = "lbl_Arrow_Icon4";
            // 
            // lbl_Arrow_Icon3
            // 
            this.lbl_Arrow_Icon3.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lbl_Arrow_Icon3, "lbl_Arrow_Icon3");
            this.lbl_Arrow_Icon3.Name = "lbl_Arrow_Icon3";
            // 
            // lbl_Arrow_Icon2
            // 
            this.lbl_Arrow_Icon2.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lbl_Arrow_Icon2, "lbl_Arrow_Icon2");
            this.lbl_Arrow_Icon2.Name = "lbl_Arrow_Icon2";
            // 
            // lbl_MacroRead_Icon
            // 
            resources.ApplyResources(this.lbl_MacroRead_Icon, "lbl_MacroRead_Icon");
            this.lbl_MacroRead_Icon.ForeColor = System.Drawing.Color.White;
            this.lbl_MacroRead_Icon.Image = global::RevOmate.Properties.Resources.ScriptRead;
            this.lbl_MacroRead_Icon.Name = "lbl_MacroRead_Icon";
            this.lbl_MacroRead_Icon.Tag = "0";
            this.lbl_MacroRead_Icon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_MacroRead_Icon_MouseClick);
            this.lbl_MacroRead_Icon.MouseEnter += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseEnter);
            // 
            // DeviceReadWrite_imageList
            // 
            this.DeviceReadWrite_imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("DeviceReadWrite_imageList.ImageStream")));
            this.DeviceReadWrite_imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.DeviceReadWrite_imageList.Images.SetKeyName(0, "BACK_KAKIKOMI.png");
            this.DeviceReadWrite_imageList.Images.SetKeyName(1, "BACK_KAKIKOMI_ACTIVE.png");
            this.DeviceReadWrite_imageList.Images.SetKeyName(2, "BACK_YOMIKOMI.png");
            this.DeviceReadWrite_imageList.Images.SetKeyName(3, "BACK_YOMIKOMI_ACTIVE.png");
            // 
            // lbl_MacroWrite_Icon
            // 
            resources.ApplyResources(this.lbl_MacroWrite_Icon, "lbl_MacroWrite_Icon");
            this.lbl_MacroWrite_Icon.ForeColor = System.Drawing.Color.White;
            this.lbl_MacroWrite_Icon.Image = global::RevOmate.Properties.Resources.ScriptWrite;
            this.lbl_MacroWrite_Icon.Name = "lbl_MacroWrite_Icon";
            this.lbl_MacroWrite_Icon.Tag = "0";
            this.lbl_MacroWrite_Icon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_MacroWrite_Icon_MouseClick);
            this.lbl_MacroWrite_Icon.MouseEnter += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseEnter);
            // 
            // gbx_FileImportExport
            // 
            this.gbx_FileImportExport.Controls.Add(this.lbl_FileExport_Icon);
            this.gbx_FileImportExport.Controls.Add(this.lbl_FileImportExportTitle_Icon);
            this.gbx_FileImportExport.Controls.Add(this.lbl_FileImport_Icon);
            resources.ApplyResources(this.gbx_FileImportExport, "gbx_FileImportExport");
            this.gbx_FileImportExport.Name = "gbx_FileImportExport";
            this.gbx_FileImportExport.TabStop = false;
            // 
            // lbl_FileExport_Icon
            // 
            resources.ApplyResources(this.lbl_FileExport_Icon, "lbl_FileExport_Icon");
            this.lbl_FileExport_Icon.ImageList = this.FileImportExport_imageList;
            this.lbl_FileExport_Icon.Name = "lbl_FileExport_Icon";
            this.lbl_FileExport_Icon.Tag = "0";
            this.lbl_FileExport_Icon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_FileExport_Icon_MouseClick);
            this.lbl_FileExport_Icon.MouseEnter += new System.EventHandler(this.lbl_MouseEnter);
            this.lbl_FileExport_Icon.MouseLeave += new System.EventHandler(this.lbl_MouseLeave);
            // 
            // FileImportExport_imageList
            // 
            this.FileImportExport_imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("FileImportExport_imageList.ImageStream")));
            this.FileImportExport_imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.FileImportExport_imageList.Images.SetKeyName(0, "LEFTBN_SETTEI-KAKIKOMI.png");
            this.FileImportExport_imageList.Images.SetKeyName(1, "LEFTBN_SETTEI-KAKIKOMI_ACTIVE.png");
            this.FileImportExport_imageList.Images.SetKeyName(2, "LEFTBN_SETTEI-YOMIKOMI.png");
            this.FileImportExport_imageList.Images.SetKeyName(3, "LEFTBN_SETTEI-YOMIKOMI_ACTIVE.png");
            // 
            // lbl_FileImportExportTitle_Icon
            // 
            resources.ApplyResources(this.lbl_FileImportExportTitle_Icon, "lbl_FileImportExportTitle_Icon");
            this.lbl_FileImportExportTitle_Icon.Name = "lbl_FileImportExportTitle_Icon";
            this.lbl_FileImportExportTitle_Icon.Tag = "0";
            // 
            // lbl_FileImport_Icon
            // 
            resources.ApplyResources(this.lbl_FileImport_Icon, "lbl_FileImport_Icon");
            this.lbl_FileImport_Icon.ImageList = this.FileImportExport_imageList;
            this.lbl_FileImport_Icon.Name = "lbl_FileImport_Icon";
            this.lbl_FileImport_Icon.Tag = "0";
            this.lbl_FileImport_Icon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_FileImport_Icon_MouseClick);
            this.lbl_FileImport_Icon.MouseEnter += new System.EventHandler(this.lbl_MouseEnter);
            this.lbl_FileImport_Icon.MouseLeave += new System.EventHandler(this.lbl_MouseLeave);
            // 
            // lbl_JoyRightLeverRelease_Icon
            // 
            this.lbl_JoyRightLeverRelease_Icon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lbl_JoyRightLeverRelease_Icon, "lbl_JoyRightLeverRelease_Icon");
            this.lbl_JoyRightLeverRelease_Icon.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_JoyRightLeverRelease_Icon.Name = "lbl_JoyRightLeverRelease_Icon";
            this.lbl_JoyRightLeverRelease_Icon.Tag = "9";
            this.lbl_JoyRightLeverRelease_Icon.DragOver += new System.Windows.Forms.DragEventHandler(this.lbl_Script_Icon_DragOver);
            this.lbl_JoyRightLeverRelease_Icon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbl_JoyRightLeverRelease_Icon_MouseDown);
            this.lbl_JoyRightLeverRelease_Icon.MouseEnter += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseEnter);
            this.lbl_JoyRightLeverRelease_Icon.MouseLeave += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseLeave);
            this.lbl_JoyRightLeverRelease_Icon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbl_Script_Icon_MouseMove);
            this.lbl_JoyRightLeverRelease_Icon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbl_Script_Icon_MouseUp);
            // 
            // MacroEditIcon_imageList
            // 
            this.MacroEditIcon_imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("MacroEditIcon_imageList.ImageStream")));
            this.MacroEditIcon_imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.MacroEditIcon_imageList.Images.SetKeyName(0, "BN_RIGHT-BN02-OSU.png");
            this.MacroEditIcon_imageList.Images.SetKeyName(1, "BN_RIGHT-BN02-OSU_ACTIVE.png");
            this.MacroEditIcon_imageList.Images.SetKeyName(2, "BN_RIGHT-BN02-HANASU.png");
            this.MacroEditIcon_imageList.Images.SetKeyName(3, "BN_RIGHT-BN02-HANASU_ACTIVE.png");
            this.MacroEditIcon_imageList.Images.SetKeyName(4, "BN_RIGHT-BN03-OSU.png");
            this.MacroEditIcon_imageList.Images.SetKeyName(5, "BN_RIGHT-BN03-OSU_ACTIVE.png");
            this.MacroEditIcon_imageList.Images.SetKeyName(6, "BN_RIGHT-BN03-HANASU.png");
            this.MacroEditIcon_imageList.Images.SetKeyName(7, "BN_RIGHT-BN03-HANASU_ACTIVE.png");
            this.MacroEditIcon_imageList.Images.SetKeyName(8, "BN_RIGHT-BN04-OSU.png");
            this.MacroEditIcon_imageList.Images.SetKeyName(9, "BN_RIGHT-BN04-OSU_ACTIVE.png");
            this.MacroEditIcon_imageList.Images.SetKeyName(10, "BN_RIGHT-BN04-HANASU.png");
            this.MacroEditIcon_imageList.Images.SetKeyName(11, "BN_RIGHT-BN04-HANASU_ACTIVE.png");
            this.MacroEditIcon_imageList.Images.SetKeyName(12, "BN_RIGHT-BN05-OSU.png");
            this.MacroEditIcon_imageList.Images.SetKeyName(13, "BN_RIGHT-BN05-OSU_ACTIVE.png");
            this.MacroEditIcon_imageList.Images.SetKeyName(14, "BN_RIGHT-BN05-HANASU.png");
            this.MacroEditIcon_imageList.Images.SetKeyName(15, "BN_RIGHT-BN05-HANASU_ACTIVE.png");
            this.MacroEditIcon_imageList.Images.SetKeyName(16, "BN_RIGHT-BN06-OSU.png");
            this.MacroEditIcon_imageList.Images.SetKeyName(17, "BN_RIGHT-BN06-OSU_ACTIVE.png");
            this.MacroEditIcon_imageList.Images.SetKeyName(18, "BN_RIGHT-BN06-HANASU.png");
            this.MacroEditIcon_imageList.Images.SetKeyName(19, "BN_RIGHT-BN06-HANASU_ACTIVE.png");
            this.MacroEditIcon_imageList.Images.SetKeyName(20, "BN_RIGHT-BN07-OSU.png");
            this.MacroEditIcon_imageList.Images.SetKeyName(21, "BN_RIGHT-BN07-OSU_ACTIVE.png");
            this.MacroEditIcon_imageList.Images.SetKeyName(22, "BN_RIGHT-BN07-HANASU.png");
            this.MacroEditIcon_imageList.Images.SetKeyName(23, "BN_RIGHT-BN07-HANASU_ACTIVE.png");
            this.MacroEditIcon_imageList.Images.SetKeyName(24, "BN_RIGHT-BN08-MOUSE.png");
            this.MacroEditIcon_imageList.Images.SetKeyName(25, "BN_RIGHT-BN08-MOUSE_ACTIVE.png");
            this.MacroEditIcon_imageList.Images.SetKeyName(26, "BN_RIGHT-BN09-KANKAKU.png");
            this.MacroEditIcon_imageList.Images.SetKeyName(27, "BN_RIGHT-BN09-KANKAKU_ACTIVE.png");
            // 
            // lbl_JoyRightLeverPress_Icon
            // 
            this.lbl_JoyRightLeverPress_Icon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lbl_JoyRightLeverPress_Icon, "lbl_JoyRightLeverPress_Icon");
            this.lbl_JoyRightLeverPress_Icon.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_JoyRightLeverPress_Icon.Name = "lbl_JoyRightLeverPress_Icon";
            this.lbl_JoyRightLeverPress_Icon.Tag = "8";
            this.lbl_JoyRightLeverPress_Icon.DragOver += new System.Windows.Forms.DragEventHandler(this.lbl_Script_Icon_DragOver);
            this.lbl_JoyRightLeverPress_Icon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbl_JoyRightLeverPress_Icon_MouseDown);
            this.lbl_JoyRightLeverPress_Icon.MouseEnter += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseEnter);
            this.lbl_JoyRightLeverPress_Icon.MouseLeave += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseLeave);
            this.lbl_JoyRightLeverPress_Icon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbl_Script_Icon_MouseMove);
            this.lbl_JoyRightLeverPress_Icon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbl_Script_Icon_MouseUp);
            // 
            // lbl_JoyButtonRelease_Icon
            // 
            this.lbl_JoyButtonRelease_Icon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lbl_JoyButtonRelease_Icon, "lbl_JoyButtonRelease_Icon");
            this.lbl_JoyButtonRelease_Icon.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_JoyButtonRelease_Icon.Name = "lbl_JoyButtonRelease_Icon";
            this.lbl_JoyButtonRelease_Icon.Tag = "13";
            this.lbl_JoyButtonRelease_Icon.DragOver += new System.Windows.Forms.DragEventHandler(this.lbl_Script_Icon_DragOver);
            this.lbl_JoyButtonRelease_Icon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbl_JoyButtonRelease_Icon_MouseDown);
            this.lbl_JoyButtonRelease_Icon.MouseEnter += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseEnter);
            this.lbl_JoyButtonRelease_Icon.MouseLeave += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseLeave);
            this.lbl_JoyButtonRelease_Icon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbl_Script_Icon_MouseMove);
            this.lbl_JoyButtonRelease_Icon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbl_Script_Icon_MouseUp);
            // 
            // lbl_JoyButtonPress_Icon
            // 
            this.lbl_JoyButtonPress_Icon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lbl_JoyButtonPress_Icon, "lbl_JoyButtonPress_Icon");
            this.lbl_JoyButtonPress_Icon.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_JoyButtonPress_Icon.Name = "lbl_JoyButtonPress_Icon";
            this.lbl_JoyButtonPress_Icon.Tag = "12";
            this.lbl_JoyButtonPress_Icon.DragOver += new System.Windows.Forms.DragEventHandler(this.lbl_Script_Icon_DragOver);
            this.lbl_JoyButtonPress_Icon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbl_JoyButtonPress_Icon_MouseDown);
            this.lbl_JoyButtonPress_Icon.MouseEnter += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseEnter);
            this.lbl_JoyButtonPress_Icon.MouseLeave += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseLeave);
            this.lbl_JoyButtonPress_Icon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbl_Script_Icon_MouseMove);
            this.lbl_JoyButtonPress_Icon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbl_Script_Icon_MouseUp);
            // 
            // lbl_JoyHatSWRelease_Icon
            // 
            this.lbl_JoyHatSWRelease_Icon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lbl_JoyHatSWRelease_Icon, "lbl_JoyHatSWRelease_Icon");
            this.lbl_JoyHatSWRelease_Icon.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_JoyHatSWRelease_Icon.Name = "lbl_JoyHatSWRelease_Icon";
            this.lbl_JoyHatSWRelease_Icon.Tag = "11";
            this.lbl_JoyHatSWRelease_Icon.DragOver += new System.Windows.Forms.DragEventHandler(this.lbl_Script_Icon_DragOver);
            this.lbl_JoyHatSWRelease_Icon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbl_JoyHatSWRelease_Icon_MouseDown);
            this.lbl_JoyHatSWRelease_Icon.MouseEnter += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseEnter);
            this.lbl_JoyHatSWRelease_Icon.MouseLeave += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseLeave);
            this.lbl_JoyHatSWRelease_Icon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbl_Script_Icon_MouseMove);
            this.lbl_JoyHatSWRelease_Icon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbl_Script_Icon_MouseUp);
            // 
            // lbl_JoyHatSWPress_Icon
            // 
            this.lbl_JoyHatSWPress_Icon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lbl_JoyHatSWPress_Icon, "lbl_JoyHatSWPress_Icon");
            this.lbl_JoyHatSWPress_Icon.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_JoyHatSWPress_Icon.Name = "lbl_JoyHatSWPress_Icon";
            this.lbl_JoyHatSWPress_Icon.Tag = "10";
            this.lbl_JoyHatSWPress_Icon.DragOver += new System.Windows.Forms.DragEventHandler(this.lbl_Script_Icon_DragOver);
            this.lbl_JoyHatSWPress_Icon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbl_JoyHatSWPress_Icon_MouseDown);
            this.lbl_JoyHatSWPress_Icon.MouseEnter += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseEnter);
            this.lbl_JoyHatSWPress_Icon.MouseLeave += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseLeave);
            this.lbl_JoyHatSWPress_Icon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbl_Script_Icon_MouseMove);
            this.lbl_JoyHatSWPress_Icon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbl_Script_Icon_MouseUp);
            // 
            // lbl_JoyLeftLeverRelease_Icon
            // 
            this.lbl_JoyLeftLeverRelease_Icon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lbl_JoyLeftLeverRelease_Icon, "lbl_JoyLeftLeverRelease_Icon");
            this.lbl_JoyLeftLeverRelease_Icon.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_JoyLeftLeverRelease_Icon.Name = "lbl_JoyLeftLeverRelease_Icon";
            this.lbl_JoyLeftLeverRelease_Icon.Tag = "7";
            this.lbl_JoyLeftLeverRelease_Icon.DragOver += new System.Windows.Forms.DragEventHandler(this.lbl_Script_Icon_DragOver);
            this.lbl_JoyLeftLeverRelease_Icon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbl_JoyLeftLeverRelease_Icon_MouseDown);
            this.lbl_JoyLeftLeverRelease_Icon.MouseEnter += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseEnter);
            this.lbl_JoyLeftLeverRelease_Icon.MouseLeave += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseLeave);
            this.lbl_JoyLeftLeverRelease_Icon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbl_Script_Icon_MouseMove);
            this.lbl_JoyLeftLeverRelease_Icon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbl_Script_Icon_MouseUp);
            // 
            // lbl_JoyLeftLeverPress_Icon
            // 
            this.lbl_JoyLeftLeverPress_Icon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lbl_JoyLeftLeverPress_Icon, "lbl_JoyLeftLeverPress_Icon");
            this.lbl_JoyLeftLeverPress_Icon.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_JoyLeftLeverPress_Icon.Name = "lbl_JoyLeftLeverPress_Icon";
            this.lbl_JoyLeftLeverPress_Icon.Tag = "6";
            this.lbl_JoyLeftLeverPress_Icon.DragOver += new System.Windows.Forms.DragEventHandler(this.lbl_Script_Icon_DragOver);
            this.lbl_JoyLeftLeverPress_Icon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbl_JoyLeftLeverPress_Icon_MouseDown);
            this.lbl_JoyLeftLeverPress_Icon.MouseEnter += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseEnter);
            this.lbl_JoyLeftLeverPress_Icon.MouseLeave += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseLeave);
            this.lbl_JoyLeftLeverPress_Icon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbl_Script_Icon_MouseMove);
            this.lbl_JoyLeftLeverPress_Icon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbl_Script_Icon_MouseUp);
            // 
            // txtbx_script_no
            // 
            resources.ApplyResources(this.txtbx_script_no, "txtbx_script_no");
            this.txtbx_script_no.Name = "txtbx_script_no";
            this.txtbx_script_no.ReadOnly = true;
            // 
            // lbl_macro_editor_now_no
            // 
            this.lbl_macro_editor_now_no.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lbl_macro_editor_now_no, "lbl_macro_editor_now_no");
            this.lbl_macro_editor_now_no.Name = "lbl_macro_editor_now_no";
            // 
            // txtbx_script_name
            // 
            resources.ApplyResources(this.txtbx_script_name, "txtbx_script_name");
            this.txtbx_script_name.Name = "txtbx_script_name";
            // 
            // lbl_macro_editor_now_macro_name
            // 
            this.lbl_macro_editor_now_macro_name.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lbl_macro_editor_now_macro_name, "lbl_macro_editor_now_macro_name");
            this.lbl_macro_editor_now_macro_name.Name = "lbl_macro_editor_now_macro_name";
            // 
            // dgv_ScriptList
            // 
            this.dgv_ScriptList.AllowUserToAddRows = false;
            this.dgv_ScriptList.AllowUserToDeleteRows = false;
            this.dgv_ScriptList.AllowUserToResizeColumns = false;
            this.dgv_ScriptList.AllowUserToResizeRows = false;
            this.dgv_ScriptList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgv_ScriptList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ScriptList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgv_ScriptList_No,
            this.dgv_ScriptList_ScriptName});
            resources.ApplyResources(this.dgv_ScriptList, "dgv_ScriptList");
            this.dgv_ScriptList.MultiSelect = false;
            this.dgv_ScriptList.Name = "dgv_ScriptList";
            this.dgv_ScriptList.ReadOnly = true;
            this.dgv_ScriptList.RowHeadersVisible = false;
            this.dgv_ScriptList.RowTemplate.Height = 21;
            this.dgv_ScriptList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_ScriptList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_ScriptList_CellContentClick);
            // 
            // dgv_ScriptList_No
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgv_ScriptList_No.DefaultCellStyle = dataGridViewCellStyle9;
            resources.ApplyResources(this.dgv_ScriptList_No, "dgv_ScriptList_No");
            this.dgv_ScriptList_No.Name = "dgv_ScriptList_No";
            this.dgv_ScriptList_No.ReadOnly = true;
            this.dgv_ScriptList_No.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgv_ScriptList_ScriptName
            // 
            resources.ApplyResources(this.dgv_ScriptList_ScriptName, "dgv_ScriptList_ScriptName");
            this.dgv_ScriptList_ScriptName.Name = "dgv_ScriptList_ScriptName";
            this.dgv_ScriptList_ScriptName.ReadOnly = true;
            this.dgv_ScriptList_ScriptName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // lbl_Interval_Icon
            // 
            this.lbl_Interval_Icon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lbl_Interval_Icon, "lbl_Interval_Icon");
            this.lbl_Interval_Icon.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_Interval_Icon.Name = "lbl_Interval_Icon";
            this.lbl_Interval_Icon.Tag = "0";
            this.lbl_Interval_Icon.DragOver += new System.Windows.Forms.DragEventHandler(this.lbl_Script_Icon_DragOver);
            this.lbl_Interval_Icon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbl_Interval_Icon_MouseDown);
            this.lbl_Interval_Icon.MouseEnter += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseEnter);
            this.lbl_Interval_Icon.MouseLeave += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseLeave);
            this.lbl_Interval_Icon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbl_Script_Icon_MouseMove);
            this.lbl_Interval_Icon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbl_Script_Icon_MouseUp);
            // 
            // lbl_KeyRelease_Icon
            // 
            this.lbl_KeyRelease_Icon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lbl_KeyRelease_Icon, "lbl_KeyRelease_Icon");
            this.lbl_KeyRelease_Icon.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_KeyRelease_Icon.Name = "lbl_KeyRelease_Icon";
            this.lbl_KeyRelease_Icon.Tag = "2";
            this.lbl_KeyRelease_Icon.DragOver += new System.Windows.Forms.DragEventHandler(this.lbl_Script_Icon_DragOver);
            this.lbl_KeyRelease_Icon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbl_KeyRelease_Icon_MouseDown);
            this.lbl_KeyRelease_Icon.MouseEnter += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseEnter);
            this.lbl_KeyRelease_Icon.MouseLeave += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseLeave);
            this.lbl_KeyRelease_Icon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbl_Script_Icon_MouseMove);
            this.lbl_KeyRelease_Icon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbl_Script_Icon_MouseUp);
            // 
            // lbl_KeyPress_Icon
            // 
            this.lbl_KeyPress_Icon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lbl_KeyPress_Icon, "lbl_KeyPress_Icon");
            this.lbl_KeyPress_Icon.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_KeyPress_Icon.Name = "lbl_KeyPress_Icon";
            this.lbl_KeyPress_Icon.Tag = "1";
            this.lbl_KeyPress_Icon.DragOver += new System.Windows.Forms.DragEventHandler(this.lbl_Script_Icon_DragOver);
            this.lbl_KeyPress_Icon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbl_KeyPress_Icon_MouseDown);
            this.lbl_KeyPress_Icon.MouseEnter += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseEnter);
            this.lbl_KeyPress_Icon.MouseLeave += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseLeave);
            this.lbl_KeyPress_Icon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbl_Script_Icon_MouseMove);
            this.lbl_KeyPress_Icon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbl_Script_Icon_MouseUp);
            // 
            // lbl_MouseClick_Icon
            // 
            this.lbl_MouseClick_Icon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lbl_MouseClick_Icon, "lbl_MouseClick_Icon");
            this.lbl_MouseClick_Icon.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_MouseClick_Icon.Name = "lbl_MouseClick_Icon";
            this.lbl_MouseClick_Icon.Tag = "3";
            this.lbl_MouseClick_Icon.DragOver += new System.Windows.Forms.DragEventHandler(this.lbl_Script_Icon_DragOver);
            this.lbl_MouseClick_Icon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbl_MouseClick_Icon_MouseDown);
            this.lbl_MouseClick_Icon.MouseEnter += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseEnter);
            this.lbl_MouseClick_Icon.MouseLeave += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseLeave);
            this.lbl_MouseClick_Icon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbl_Script_Icon_MouseMove);
            this.lbl_MouseClick_Icon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbl_Script_Icon_MouseUp);
            // 
            // lbl_MouseRelease_Icon
            // 
            this.lbl_MouseRelease_Icon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lbl_MouseRelease_Icon, "lbl_MouseRelease_Icon");
            this.lbl_MouseRelease_Icon.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_MouseRelease_Icon.Name = "lbl_MouseRelease_Icon";
            this.lbl_MouseRelease_Icon.Tag = "4";
            this.lbl_MouseRelease_Icon.DragOver += new System.Windows.Forms.DragEventHandler(this.lbl_Script_Icon_DragOver);
            this.lbl_MouseRelease_Icon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbl_MouseRelease_Icon_MouseDown);
            this.lbl_MouseRelease_Icon.MouseEnter += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseEnter);
            this.lbl_MouseRelease_Icon.MouseLeave += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseLeave);
            this.lbl_MouseRelease_Icon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbl_Script_Icon_MouseMove);
            this.lbl_MouseRelease_Icon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbl_Script_Icon_MouseUp);
            // 
            // lbl_WheelScroll_Icon
            // 
            this.lbl_WheelScroll_Icon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lbl_WheelScroll_Icon, "lbl_WheelScroll_Icon");
            this.lbl_WheelScroll_Icon.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_WheelScroll_Icon.Name = "lbl_WheelScroll_Icon";
            this.lbl_WheelScroll_Icon.Tag = "5";
            this.lbl_WheelScroll_Icon.DragOver += new System.Windows.Forms.DragEventHandler(this.lbl_Script_Icon_DragOver);
            this.lbl_WheelScroll_Icon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbl_WheelScroll_Icon_MouseDown);
            this.lbl_WheelScroll_Icon.MouseEnter += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseEnter);
            this.lbl_WheelScroll_Icon.MouseLeave += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseLeave);
            this.lbl_WheelScroll_Icon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbl_Script_Icon_MouseMove);
            this.lbl_WheelScroll_Icon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbl_Script_Icon_MouseUp);
            // 
            // lbl_Dustbox_Icon
            // 
            this.lbl_Dustbox_Icon.AllowDrop = true;
            resources.ApplyResources(this.lbl_Dustbox_Icon, "lbl_Dustbox_Icon");
            this.lbl_Dustbox_Icon.ForeColor = System.Drawing.Color.Transparent;
            this.lbl_Dustbox_Icon.ImageList = this.DustBox_imageList;
            this.lbl_Dustbox_Icon.Name = "lbl_Dustbox_Icon";
            this.lbl_Dustbox_Icon.Tag = "0";
            this.lbl_Dustbox_Icon.DragDrop += new System.Windows.Forms.DragEventHandler(this.lbl_Dustbox_Icon_DragDrop);
            this.lbl_Dustbox_Icon.DragOver += new System.Windows.Forms.DragEventHandler(this.lbl_Dustbox_Icon_DragOver);
            this.lbl_Dustbox_Icon.DragLeave += new System.EventHandler(this.lbl_Dustbox_Icon_DragLeave);
            this.lbl_Dustbox_Icon.MouseEnter += new System.EventHandler(this.lbl_Dustbox_Icon_MouseEnter);
            this.lbl_Dustbox_Icon.MouseLeave += new System.EventHandler(this.lbl_Dustbox_Icon_MouseLeave);
            // 
            // DustBox_imageList
            // 
            this.DustBox_imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("DustBox_imageList.ImageStream")));
            this.DustBox_imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.DustBox_imageList.Images.SetKeyName(0, "MAIN3_MACRO_BN_DUST.png");
            this.DustBox_imageList.Images.SetKeyName(1, "MAIN3_MACRO_BN_DUST_OnMOUSE.png");
            // 
            // lbl_Arrow_Icon1
            // 
            this.lbl_Arrow_Icon1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lbl_Arrow_Icon1, "lbl_Arrow_Icon1");
            this.lbl_Arrow_Icon1.Name = "lbl_Arrow_Icon1";
            // 
            // chkbx_LED_preview
            // 
            resources.ApplyResources(this.chkbx_LED_preview, "chkbx_LED_preview");
            this.chkbx_LED_preview.Name = "chkbx_LED_preview";
            this.chkbx_LED_preview.UseVisualStyleBackColor = true;
            // 
            // PatternFileImportExport_imageList
            // 
            this.PatternFileImportExport_imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("PatternFileImportExport_imageList.ImageStream")));
            this.PatternFileImportExport_imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.PatternFileImportExport_imageList.Images.SetKeyName(0, "Bn02_ITIRAN.png");
            this.PatternFileImportExport_imageList.Images.SetKeyName(1, "Bn02_ITIRAN_ACTIVE.png");
            // 
            // Pattern_Icon_imageList
            // 
            this.Pattern_Icon_imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("Pattern_Icon_imageList.ImageStream")));
            this.Pattern_Icon_imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.Pattern_Icon_imageList.Images.SetKeyName(0, "PatternNone.png");
            this.Pattern_Icon_imageList.Images.SetKeyName(1, "PatternShort.png");
            this.Pattern_Icon_imageList.Images.SetKeyName(2, "PatternLong.png");
            // 
            // OnTimeSet_imageList
            // 
            this.OnTimeSet_imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("OnTimeSet_imageList.ImageStream")));
            this.OnTimeSet_imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.OnTimeSet_imageList.Images.SetKeyName(0, "Bn_KEISOKU.png");
            this.OnTimeSet_imageList.Images.SetKeyName(1, "Bn_KEISOKU_ACTIVE.png");
            this.OnTimeSet_imageList.Images.SetKeyName(2, "Bn_KEISOKU-TYUSI.png");
            this.OnTimeSet_imageList.Images.SetKeyName(3, "Bn_KEISOKU-TYUSI_ACTIVE.png");
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numud_led_brightness_level_set_val);
            this.groupBox2.Controls.Add(this.chkbx_led_debug);
            this.groupBox2.Controls.Add(this.btn_debug_led_duty_set);
            this.groupBox2.Controls.Add(this.numud_led_g_set_val);
            this.groupBox2.Controls.Add(this.numud_led_r_set_val);
            this.groupBox2.Controls.Add(this.numud_led_b_set_val);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // numud_led_brightness_level_set_val
            // 
            resources.ApplyResources(this.numud_led_brightness_level_set_val, "numud_led_brightness_level_set_val");
            this.numud_led_brightness_level_set_val.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numud_led_brightness_level_set_val.Name = "numud_led_brightness_level_set_val";
            // 
            // chkbx_led_debug
            // 
            resources.ApplyResources(this.chkbx_led_debug, "chkbx_led_debug");
            this.chkbx_led_debug.Name = "chkbx_led_debug";
            this.chkbx_led_debug.UseVisualStyleBackColor = true;
            // 
            // btn_debug_led_duty_set
            // 
            resources.ApplyResources(this.btn_debug_led_duty_set, "btn_debug_led_duty_set");
            this.btn_debug_led_duty_set.Name = "btn_debug_led_duty_set";
            this.btn_debug_led_duty_set.UseVisualStyleBackColor = true;
            this.btn_debug_led_duty_set.Click += new System.EventHandler(this.btn_debug_led_duty_set_Click);
            // 
            // numud_led_g_set_val
            // 
            resources.ApplyResources(this.numud_led_g_set_val, "numud_led_g_set_val");
            this.numud_led_g_set_val.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numud_led_g_set_val.Name = "numud_led_g_set_val";
            // 
            // numud_led_r_set_val
            // 
            resources.ApplyResources(this.numud_led_r_set_val, "numud_led_r_set_val");
            this.numud_led_r_set_val.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numud_led_r_set_val.Name = "numud_led_r_set_val";
            // 
            // numud_led_b_set_val
            // 
            resources.ApplyResources(this.numud_led_b_set_val, "numud_led_b_set_val");
            this.numud_led_b_set_val.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numud_led_b_set_val.Name = "numud_led_b_set_val";
            // 
            // lbl_FW_Version
            // 
            this.lbl_FW_Version.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lbl_FW_Version, "lbl_FW_Version");
            this.lbl_FW_Version.ForeColor = System.Drawing.Color.Silver;
            this.lbl_FW_Version.Name = "lbl_FW_Version";
            // 
            // btn_FlashErase
            // 
            resources.ApplyResources(this.btn_FlashErase, "btn_FlashErase");
            this.btn_FlashErase.Name = "btn_FlashErase";
            this.btn_FlashErase.UseVisualStyleBackColor = true;
            this.btn_FlashErase.Click += new System.EventHandler(this.btn_FlashErase_Click);
            // 
            // lbl_mode_status
            // 
            resources.ApplyResources(this.lbl_mode_status, "lbl_mode_status");
            this.lbl_mode_status.Name = "lbl_mode_status";
            // 
            // pnl_main
            // 
            this.pnl_main.BackColor = System.Drawing.Color.Transparent;
            this.pnl_main.Controls.Add(this.lbl_profile3_border);
            this.pnl_main.Controls.Add(this.lbl_profile2_border);
            this.pnl_main.Controls.Add(this.lbl_profile1_border);
            this.pnl_main.Controls.Add(this.lbl_sw1_func_name);
            this.pnl_main.Controls.Add(this.llbl_help);
            this.pnl_main.Controls.Add(this.lbl_Dial_func_name);
            this.pnl_main.Controls.Add(this.btn_system_setup);
            this.pnl_main.Controls.Add(this.btn_dial_macro_editor);
            this.pnl_main.Controls.Add(this.btn_macro_editor);
            this.pnl_main.Controls.Add(this.lbl_func_color_4);
            this.pnl_main.Controls.Add(this.lbl_func_color_3);
            this.pnl_main.Controls.Add(this.lbl_func_color_2);
            this.pnl_main.Controls.Add(this.lbl_func_color_1);
            this.pnl_main.Controls.Add(this.lbl_profile_color_3);
            this.pnl_main.Controls.Add(this.lbl_profile_color_2);
            this.pnl_main.Controls.Add(this.lbl_profile_color_1);
            this.pnl_main.Controls.Add(this.lbl_profile_3_select);
            this.pnl_main.Controls.Add(this.lbl_profile_2_select);
            this.pnl_main.Controls.Add(this.lbl_profile_1_select);
            this.pnl_main.Controls.Add(this.lbl_sw10_func_name);
            this.pnl_main.Controls.Add(this.lbl_sw9_func_name);
            this.pnl_main.Controls.Add(this.lbl_sw8_func_name);
            this.pnl_main.Controls.Add(this.lbl_sw7_func_name);
            this.pnl_main.Controls.Add(this.lbl_sw6_func_name);
            this.pnl_main.Controls.Add(this.lbl_sw5_func_name);
            this.pnl_main.Controls.Add(this.lbl_sw4_func_name);
            this.pnl_main.Controls.Add(this.lbl_sw3_func_name);
            this.pnl_main.Controls.Add(this.lbl_sw2_func_name);
            this.pnl_main.Controls.Add(this.lbl_Dial_func_name4);
            this.pnl_main.Controls.Add(this.lbl_Dial_func_name3);
            this.pnl_main.Controls.Add(this.lbl_Dial_func_name2);
            this.pnl_main.Controls.Add(this.lbl_Dial_func_name1);
            this.pnl_main.Controls.Add(this.pbx_mode_color);
            resources.ApplyResources(this.pnl_main, "pnl_main");
            this.pnl_main.Name = "pnl_main";
            // 
            // lbl_profile3_border
            // 
            this.lbl_profile3_border.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.lbl_profile3_border, "lbl_profile3_border");
            this.lbl_profile3_border.Name = "lbl_profile3_border";
            this.lbl_profile3_border.Tag = "2";
            // 
            // lbl_profile2_border
            // 
            this.lbl_profile2_border.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.lbl_profile2_border, "lbl_profile2_border");
            this.lbl_profile2_border.Name = "lbl_profile2_border";
            this.lbl_profile2_border.Tag = "1";
            // 
            // lbl_profile1_border
            // 
            this.lbl_profile1_border.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.lbl_profile1_border, "lbl_profile1_border");
            this.lbl_profile1_border.Name = "lbl_profile1_border";
            this.lbl_profile1_border.Tag = "0";
            // 
            // lbl_sw1_func_name
            // 
            resources.ApplyResources(this.lbl_sw1_func_name, "lbl_sw1_func_name");
            this.lbl_sw1_func_name.Name = "lbl_sw1_func_name";
            this.lbl_sw1_func_name.Tag = "0";
            this.lbl_sw1_func_name.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_sw_func_name_MouseClick);
            this.lbl_sw1_func_name.MouseEnter += new System.EventHandler(this.lbl_sw_func_name_MouseEnter);
            // 
            // llbl_help
            // 
            this.llbl_help.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.llbl_help, "llbl_help");
            this.llbl_help.Name = "llbl_help";
            this.llbl_help.TabStop = true;
            this.llbl_help.MouseClick += new System.Windows.Forms.MouseEventHandler(this.llbl_help_MouseClick);
            // 
            // lbl_Dial_func_name
            // 
            resources.ApplyResources(this.lbl_Dial_func_name, "lbl_Dial_func_name");
            this.lbl_Dial_func_name.Name = "lbl_Dial_func_name";
            this.lbl_Dial_func_name.Tag = "10";
            this.lbl_Dial_func_name.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_sw_func_name_MouseClick);
            this.lbl_Dial_func_name.MouseEnter += new System.EventHandler(this.lbl_sw_func_name_MouseEnter);
            // 
            // btn_system_setup
            // 
            this.btn_system_setup.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btn_system_setup, "btn_system_setup");
            this.btn_system_setup.Name = "btn_system_setup";
            this.btn_system_setup.UseVisualStyleBackColor = true;
            this.btn_system_setup.Click += new System.EventHandler(this.btn_system_setup_Click);
            // 
            // btn_dial_macro_editor
            // 
            this.btn_dial_macro_editor.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btn_dial_macro_editor, "btn_dial_macro_editor");
            this.btn_dial_macro_editor.Name = "btn_dial_macro_editor";
            this.btn_dial_macro_editor.UseVisualStyleBackColor = true;
            this.btn_dial_macro_editor.Click += new System.EventHandler(this.btn_dial_macro_editor_Click);
            // 
            // btn_macro_editor
            // 
            resources.ApplyResources(this.btn_macro_editor, "btn_macro_editor");
            this.btn_macro_editor.Name = "btn_macro_editor";
            this.btn_macro_editor.UseVisualStyleBackColor = true;
            this.btn_macro_editor.Click += new System.EventHandler(this.btn_macro_editor_Click);
            // 
            // lbl_func_color_4
            // 
            this.lbl_func_color_4.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lbl_func_color_4, "lbl_func_color_4");
            this.lbl_func_color_4.Name = "lbl_func_color_4";
            this.lbl_func_color_4.Tag = "3";
            this.lbl_func_color_4.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_func_color_MouseClick);
            // 
            // lbl_func_color_3
            // 
            this.lbl_func_color_3.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lbl_func_color_3, "lbl_func_color_3");
            this.lbl_func_color_3.Name = "lbl_func_color_3";
            this.lbl_func_color_3.Tag = "2";
            this.lbl_func_color_3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_func_color_MouseClick);
            // 
            // lbl_func_color_2
            // 
            this.lbl_func_color_2.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lbl_func_color_2, "lbl_func_color_2");
            this.lbl_func_color_2.Name = "lbl_func_color_2";
            this.lbl_func_color_2.Tag = "1";
            this.lbl_func_color_2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_func_color_MouseClick);
            // 
            // lbl_func_color_1
            // 
            this.lbl_func_color_1.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lbl_func_color_1, "lbl_func_color_1");
            this.lbl_func_color_1.Name = "lbl_func_color_1";
            this.lbl_func_color_1.Tag = "0";
            this.lbl_func_color_1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_func_color_MouseClick);
            // 
            // lbl_profile_color_3
            // 
            this.lbl_profile_color_3.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lbl_profile_color_3, "lbl_profile_color_3");
            this.lbl_profile_color_3.Name = "lbl_profile_color_3";
            this.lbl_profile_color_3.Tag = "2";
            this.lbl_profile_color_3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_profile_color_MouseClick);
            // 
            // lbl_profile_color_2
            // 
            this.lbl_profile_color_2.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lbl_profile_color_2, "lbl_profile_color_2");
            this.lbl_profile_color_2.Name = "lbl_profile_color_2";
            this.lbl_profile_color_2.Tag = "1";
            this.lbl_profile_color_2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_profile_color_MouseClick);
            // 
            // lbl_profile_color_1
            // 
            this.lbl_profile_color_1.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lbl_profile_color_1, "lbl_profile_color_1");
            this.lbl_profile_color_1.Name = "lbl_profile_color_1";
            this.lbl_profile_color_1.Tag = "0";
            this.lbl_profile_color_1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_profile_color_MouseClick);
            // 
            // lbl_profile_3_select
            // 
            resources.ApplyResources(this.lbl_profile_3_select, "lbl_profile_3_select");
            this.lbl_profile_3_select.Name = "lbl_profile_3_select";
            this.lbl_profile_3_select.Tag = "2";
            this.lbl_profile_3_select.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_profile_select_MouseClick);
            // 
            // lbl_profile_2_select
            // 
            resources.ApplyResources(this.lbl_profile_2_select, "lbl_profile_2_select");
            this.lbl_profile_2_select.Name = "lbl_profile_2_select";
            this.lbl_profile_2_select.Tag = "1";
            this.lbl_profile_2_select.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_profile_select_MouseClick);
            // 
            // lbl_profile_1_select
            // 
            resources.ApplyResources(this.lbl_profile_1_select, "lbl_profile_1_select");
            this.lbl_profile_1_select.Name = "lbl_profile_1_select";
            this.lbl_profile_1_select.Tag = "0";
            this.lbl_profile_1_select.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_profile_select_MouseClick);
            // 
            // lbl_sw10_func_name
            // 
            resources.ApplyResources(this.lbl_sw10_func_name, "lbl_sw10_func_name");
            this.lbl_sw10_func_name.Name = "lbl_sw10_func_name";
            this.lbl_sw10_func_name.Tag = "9";
            this.lbl_sw10_func_name.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_sw_func_name_MouseClick);
            this.lbl_sw10_func_name.MouseEnter += new System.EventHandler(this.lbl_sw_func_name_MouseEnter);
            // 
            // lbl_sw9_func_name
            // 
            resources.ApplyResources(this.lbl_sw9_func_name, "lbl_sw9_func_name");
            this.lbl_sw9_func_name.Name = "lbl_sw9_func_name";
            this.lbl_sw9_func_name.Tag = "8";
            this.lbl_sw9_func_name.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_sw_func_name_MouseClick);
            this.lbl_sw9_func_name.MouseEnter += new System.EventHandler(this.lbl_sw_func_name_MouseEnter);
            // 
            // lbl_sw8_func_name
            // 
            resources.ApplyResources(this.lbl_sw8_func_name, "lbl_sw8_func_name");
            this.lbl_sw8_func_name.Name = "lbl_sw8_func_name";
            this.lbl_sw8_func_name.Tag = "7";
            this.lbl_sw8_func_name.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_sw_func_name_MouseClick);
            this.lbl_sw8_func_name.MouseEnter += new System.EventHandler(this.lbl_sw_func_name_MouseEnter);
            // 
            // lbl_sw7_func_name
            // 
            resources.ApplyResources(this.lbl_sw7_func_name, "lbl_sw7_func_name");
            this.lbl_sw7_func_name.Name = "lbl_sw7_func_name";
            this.lbl_sw7_func_name.Tag = "6";
            this.lbl_sw7_func_name.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_sw_func_name_MouseClick);
            this.lbl_sw7_func_name.MouseEnter += new System.EventHandler(this.lbl_sw_func_name_MouseEnter);
            // 
            // lbl_sw6_func_name
            // 
            resources.ApplyResources(this.lbl_sw6_func_name, "lbl_sw6_func_name");
            this.lbl_sw6_func_name.Name = "lbl_sw6_func_name";
            this.lbl_sw6_func_name.Tag = "5";
            this.lbl_sw6_func_name.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_sw_func_name_MouseClick);
            this.lbl_sw6_func_name.MouseEnter += new System.EventHandler(this.lbl_sw_func_name_MouseEnter);
            // 
            // lbl_sw5_func_name
            // 
            resources.ApplyResources(this.lbl_sw5_func_name, "lbl_sw5_func_name");
            this.lbl_sw5_func_name.Name = "lbl_sw5_func_name";
            this.lbl_sw5_func_name.Tag = "4";
            this.lbl_sw5_func_name.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_sw_func_name_MouseClick);
            this.lbl_sw5_func_name.MouseEnter += new System.EventHandler(this.lbl_sw_func_name_MouseEnter);
            // 
            // lbl_sw4_func_name
            // 
            resources.ApplyResources(this.lbl_sw4_func_name, "lbl_sw4_func_name");
            this.lbl_sw4_func_name.Name = "lbl_sw4_func_name";
            this.lbl_sw4_func_name.Tag = "3";
            this.lbl_sw4_func_name.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_sw_func_name_MouseClick);
            this.lbl_sw4_func_name.MouseEnter += new System.EventHandler(this.lbl_sw_func_name_MouseEnter);
            // 
            // lbl_sw3_func_name
            // 
            resources.ApplyResources(this.lbl_sw3_func_name, "lbl_sw3_func_name");
            this.lbl_sw3_func_name.Name = "lbl_sw3_func_name";
            this.lbl_sw3_func_name.Tag = "2";
            this.lbl_sw3_func_name.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_sw_func_name_MouseClick);
            this.lbl_sw3_func_name.MouseEnter += new System.EventHandler(this.lbl_sw_func_name_MouseEnter);
            // 
            // lbl_sw2_func_name
            // 
            resources.ApplyResources(this.lbl_sw2_func_name, "lbl_sw2_func_name");
            this.lbl_sw2_func_name.Name = "lbl_sw2_func_name";
            this.lbl_sw2_func_name.Tag = "1";
            this.lbl_sw2_func_name.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_sw_func_name_MouseClick);
            this.lbl_sw2_func_name.MouseEnter += new System.EventHandler(this.lbl_sw_func_name_MouseEnter);
            // 
            // lbl_Dial_func_name4
            // 
            resources.ApplyResources(this.lbl_Dial_func_name4, "lbl_Dial_func_name4");
            this.lbl_Dial_func_name4.Name = "lbl_Dial_func_name4";
            this.lbl_Dial_func_name4.Tag = "3";
            this.lbl_Dial_func_name4.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_Dial_func_name_MouseClick);
            this.lbl_Dial_func_name4.MouseEnter += new System.EventHandler(this.lbl_Dial_func_name_MouseEnter);
            // 
            // lbl_Dial_func_name3
            // 
            resources.ApplyResources(this.lbl_Dial_func_name3, "lbl_Dial_func_name3");
            this.lbl_Dial_func_name3.Name = "lbl_Dial_func_name3";
            this.lbl_Dial_func_name3.Tag = "2";
            this.lbl_Dial_func_name3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_Dial_func_name_MouseClick);
            this.lbl_Dial_func_name3.MouseEnter += new System.EventHandler(this.lbl_Dial_func_name_MouseEnter);
            // 
            // lbl_Dial_func_name2
            // 
            resources.ApplyResources(this.lbl_Dial_func_name2, "lbl_Dial_func_name2");
            this.lbl_Dial_func_name2.Name = "lbl_Dial_func_name2";
            this.lbl_Dial_func_name2.Tag = "1";
            this.lbl_Dial_func_name2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_Dial_func_name_MouseClick);
            this.lbl_Dial_func_name2.MouseEnter += new System.EventHandler(this.lbl_Dial_func_name_MouseEnter);
            // 
            // lbl_Dial_func_name1
            // 
            resources.ApplyResources(this.lbl_Dial_func_name1, "lbl_Dial_func_name1");
            this.lbl_Dial_func_name1.Name = "lbl_Dial_func_name1";
            this.lbl_Dial_func_name1.Tag = "0";
            this.lbl_Dial_func_name1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_Dial_func_name_MouseClick);
            this.lbl_Dial_func_name1.MouseEnter += new System.EventHandler(this.lbl_Dial_func_name_MouseEnter);
            // 
            // pbx_mode_color
            // 
            this.pbx_mode_color.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.pbx_mode_color, "pbx_mode_color");
            this.pbx_mode_color.Name = "pbx_mode_color";
            this.pbx_mode_color.TabStop = false;
            // 
            // contextMenuStrip_sw_func
            // 
            this.contextMenuStrip_sw_func.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.機能設定画面ToolStripMenuItem,
            this.標準設定に戻すToolStripMenuItem,
            this.割り当て解除ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.インポートToolStripMenuItem,
            this.エクスポートToolStripMenuItem});
            this.contextMenuStrip_sw_func.Name = "contextMenuStrip_sw_func";
            resources.ApplyResources(this.contextMenuStrip_sw_func, "contextMenuStrip_sw_func");
            // 
            // 機能設定画面ToolStripMenuItem
            // 
            this.機能設定画面ToolStripMenuItem.Name = "機能設定画面ToolStripMenuItem";
            resources.ApplyResources(this.機能設定画面ToolStripMenuItem, "機能設定画面ToolStripMenuItem");
            this.機能設定画面ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_func_setting_Click);
            // 
            // 標準設定に戻すToolStripMenuItem
            // 
            this.標準設定に戻すToolStripMenuItem.Name = "標準設定に戻すToolStripMenuItem";
            resources.ApplyResources(this.標準設定に戻すToolStripMenuItem, "標準設定に戻すToolStripMenuItem");
            // 
            // 割り当て解除ToolStripMenuItem
            // 
            this.割り当て解除ToolStripMenuItem.Name = "割り当て解除ToolStripMenuItem";
            resources.ApplyResources(this.割り当て解除ToolStripMenuItem, "割り当て解除ToolStripMenuItem");
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // インポートToolStripMenuItem
            // 
            this.インポートToolStripMenuItem.Name = "インポートToolStripMenuItem";
            resources.ApplyResources(this.インポートToolStripMenuItem, "インポートToolStripMenuItem");
            // 
            // エクスポートToolStripMenuItem
            // 
            this.エクスポートToolStripMenuItem.Name = "エクスポートToolStripMenuItem";
            resources.ApplyResources(this.エクスポートToolStripMenuItem, "エクスポートToolStripMenuItem");
            // 
            // btn_macro_editor_close
            // 
            this.btn_macro_editor_close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btn_macro_editor_close, "btn_macro_editor_close");
            this.btn_macro_editor_close.Name = "btn_macro_editor_close";
            this.btn_macro_editor_close.UseVisualStyleBackColor = true;
            this.btn_macro_editor_close.Click += new System.EventHandler(this.btn_macro_editor_close_Click);
            // 
            // btn_dial_macro_editor_cancel
            // 
            this.btn_dial_macro_editor_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btn_dial_macro_editor_cancel, "btn_dial_macro_editor_cancel");
            this.btn_dial_macro_editor_cancel.Name = "btn_dial_macro_editor_cancel";
            this.btn_dial_macro_editor_cancel.UseVisualStyleBackColor = true;
            this.btn_dial_macro_editor_cancel.Click += new System.EventHandler(this.btn_dial_macro_editor_cancel_Click);
            // 
            // btn_dial_macro_editor_submit
            // 
            resources.ApplyResources(this.btn_dial_macro_editor_submit, "btn_dial_macro_editor_submit");
            this.btn_dial_macro_editor_submit.Name = "btn_dial_macro_editor_submit";
            this.btn_dial_macro_editor_submit.UseVisualStyleBackColor = true;
            this.btn_dial_macro_editor_submit.Click += new System.EventHandler(this.btn_dial_macro_editor_submit_Click);
            // 
            // btn_debug_tab_disp
            // 
            resources.ApplyResources(this.btn_debug_tab_disp, "btn_debug_tab_disp");
            this.btn_debug_tab_disp.Name = "btn_debug_tab_disp";
            this.btn_debug_tab_disp.UseVisualStyleBackColor = true;
            this.btn_debug_tab_disp.Click += new System.EventHandler(this.btn_debug_tab_disp_Click);
            // 
            // btn_debug_macro_disp
            // 
            resources.ApplyResources(this.btn_debug_macro_disp, "btn_debug_macro_disp");
            this.btn_debug_macro_disp.Name = "btn_debug_macro_disp";
            this.btn_debug_macro_disp.UseVisualStyleBackColor = true;
            this.btn_debug_macro_disp.Click += new System.EventHandler(this.btn_debug_macro_disp_Click);
            // 
            // btn_debug_dial_macro_disp
            // 
            resources.ApplyResources(this.btn_debug_dial_macro_disp, "btn_debug_dial_macro_disp");
            this.btn_debug_dial_macro_disp.Name = "btn_debug_dial_macro_disp";
            this.btn_debug_dial_macro_disp.UseVisualStyleBackColor = true;
            this.btn_debug_dial_macro_disp.Click += new System.EventHandler(this.btn_debug_dial_macro_disp_Click);
            // 
            // gbx_system_backup
            // 
            this.gbx_system_backup.BackColor = System.Drawing.Color.Transparent;
            this.gbx_system_backup.Controls.Add(this.lbl_system_backup_title);
            this.gbx_system_backup.Controls.Add(this.btn_system_default_set);
            this.gbx_system_backup.Controls.Add(this.lbl_default_setting_title);
            this.gbx_system_backup.Controls.Add(this.btn_system_backupfile_save);
            this.gbx_system_backup.Controls.Add(this.btn_system_backupfile_read);
            this.gbx_system_backup.Controls.Add(this.pgb_process_status);
            this.gbx_system_backup.Controls.Add(this.lbl_progress_total_value);
            this.gbx_system_backup.Controls.Add(this.lbl_progress_now_value);
            this.gbx_system_backup.Controls.Add(this.lbl_progress_per);
            resources.ApplyResources(this.gbx_system_backup, "gbx_system_backup");
            this.gbx_system_backup.Name = "gbx_system_backup";
            this.gbx_system_backup.TabStop = false;
            // 
            // lbl_system_backup_title
            // 
            resources.ApplyResources(this.lbl_system_backup_title, "lbl_system_backup_title");
            this.lbl_system_backup_title.Name = "lbl_system_backup_title";
            // 
            // btn_system_default_set
            // 
            resources.ApplyResources(this.btn_system_default_set, "btn_system_default_set");
            this.btn_system_default_set.Name = "btn_system_default_set";
            this.btn_system_default_set.UseVisualStyleBackColor = true;
            this.btn_system_default_set.Click += new System.EventHandler(this.btn_system_default_set_Click);
            // 
            // lbl_default_setting_title
            // 
            resources.ApplyResources(this.lbl_default_setting_title, "lbl_default_setting_title");
            this.lbl_default_setting_title.Name = "lbl_default_setting_title";
            // 
            // btn_system_backupfile_save
            // 
            resources.ApplyResources(this.btn_system_backupfile_save, "btn_system_backupfile_save");
            this.btn_system_backupfile_save.Name = "btn_system_backupfile_save";
            this.btn_system_backupfile_save.UseVisualStyleBackColor = true;
            this.btn_system_backupfile_save.Click += new System.EventHandler(this.btn_system_backupfile_save_Click);
            // 
            // btn_system_backupfile_read
            // 
            resources.ApplyResources(this.btn_system_backupfile_read, "btn_system_backupfile_read");
            this.btn_system_backupfile_read.Name = "btn_system_backupfile_read";
            this.btn_system_backupfile_read.UseVisualStyleBackColor = true;
            this.btn_system_backupfile_read.Click += new System.EventHandler(this.btn_system_backupfile_read_Click);
            // 
            // btn_system_factory_set
            // 
            resources.ApplyResources(this.btn_system_factory_set, "btn_system_factory_set");
            this.btn_system_factory_set.Name = "btn_system_factory_set";
            this.btn_system_factory_set.UseVisualStyleBackColor = true;
            this.btn_system_factory_set.Click += new System.EventHandler(this.btn_system_factory_set_Click);
            // 
            // lbl_factory_setting_title
            // 
            resources.ApplyResources(this.lbl_factory_setting_title, "lbl_factory_setting_title");
            this.lbl_factory_setting_title.Name = "lbl_factory_setting_title";
            // 
            // btn_system_setup_close
            // 
            this.btn_system_setup_close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btn_system_setup_close, "btn_system_setup_close");
            this.btn_system_setup_close.Name = "btn_system_setup_close";
            this.btn_system_setup_close.UseVisualStyleBackColor = true;
            this.btn_system_setup_close.Click += new System.EventHandler(this.btn_system_setup_close_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 1000;
            this.toolTip1.ReshowDelay = 100;
            // 
            // pnl_system_setup
            // 
            resources.ApplyResources(this.pnl_system_setup, "pnl_system_setup");
            this.pnl_system_setup.Controls.Add(this.btn_system_factory_set);
            this.pnl_system_setup.Controls.Add(this.llbl_system_setup_help);
            this.pnl_system_setup.Controls.Add(this.lbl_factory_setting_title);
            this.pnl_system_setup.Controls.Add(this.gbx_system_backup);
            this.pnl_system_setup.Controls.Add(this.gbx_base_setting);
            this.pnl_system_setup.Controls.Add(this.btn_system_setup_close);
            this.pnl_system_setup.Controls.Add(this.lbl_reset_bg);
            this.pnl_system_setup.Controls.Add(this.lbl_reset_title);
            this.pnl_system_setup.Controls.Add(this.gbx_FileImportExport);
            this.pnl_system_setup.Controls.Add(this.lbl_button_factory_reset_icon);
            this.pnl_system_setup.Name = "pnl_system_setup";
            // 
            // llbl_system_setup_help
            // 
            this.llbl_system_setup_help.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.llbl_system_setup_help, "llbl_system_setup_help");
            this.llbl_system_setup_help.Name = "llbl_system_setup_help";
            this.llbl_system_setup_help.TabStop = true;
            this.llbl_system_setup_help.MouseClick += new System.Windows.Forms.MouseEventHandler(this.llbl_system_setup_help_MouseClick);
            // 
            // pnl_dial_macro_editor
            // 
            resources.ApplyResources(this.pnl_dial_macro_editor, "pnl_dial_macro_editor");
            this.pnl_dial_macro_editor.Controls.Add(this.llbl_dial_macro_editor_help);
            this.pnl_dial_macro_editor.Controls.Add(this.btn_dial_macro_editor_cancel);
            this.pnl_dial_macro_editor.Controls.Add(this.cmbbx_encoder_script_select_no);
            this.pnl_dial_macro_editor.Controls.Add(this.btn_dial_macro_editor_submit);
            this.pnl_dial_macro_editor.Controls.Add(this.dgv_encoder_script);
            this.pnl_dial_macro_editor.Controls.Add(this.chkbx_encoder_script_loop);
            this.pnl_dial_macro_editor.Name = "pnl_dial_macro_editor";
            // 
            // llbl_dial_macro_editor_help
            // 
            this.llbl_dial_macro_editor_help.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.llbl_dial_macro_editor_help, "llbl_dial_macro_editor_help");
            this.llbl_dial_macro_editor_help.Name = "llbl_dial_macro_editor_help";
            this.llbl_dial_macro_editor_help.TabStop = true;
            this.llbl_dial_macro_editor_help.MouseClick += new System.Windows.Forms.MouseEventHandler(this.llbl_help_MouseClick);
            // 
            // pnl_macro_editor
            // 
            this.pnl_macro_editor.BackgroundImage = global::RevOmate.Properties.Resources.MAIN2_MACRO_BG;
            this.pnl_macro_editor.Controls.Add(this.lbl_macro_editor_area_title);
            this.pnl_macro_editor.Controls.Add(this.lbl_macro_list_title);
            this.pnl_macro_editor.Controls.Add(this.gbx_Script_Add_Info);
            this.pnl_macro_editor.Controls.Add(this.gbx_Script_Add_Info_JoysticButton);
            this.pnl_macro_editor.Controls.Add(this.gbx_Script_Add_Info_JoysticLever);
            this.pnl_macro_editor.Controls.Add(this.gbx_Script_Add_Info_Mouse);
            this.pnl_macro_editor.Controls.Add(this.gbx_Script_Add_Info_MultiMedia);
            this.pnl_macro_editor.Controls.Add(this.lbl_macro_editor_macro_create_title);
            this.pnl_macro_editor.Controls.Add(this.llbl_macro_editor_help);
            this.pnl_macro_editor.Controls.Add(this.gbx_script_command_list);
            this.pnl_macro_editor.Controls.Add(this.lbl_MacroRead_txt);
            this.pnl_macro_editor.Controls.Add(this.lbl_MacroWrite_txt);
            this.pnl_macro_editor.Controls.Add(this.dgv_ScriptList);
            this.pnl_macro_editor.Controls.Add(this.lbl_macro_editor_now_macro_name);
            this.pnl_macro_editor.Controls.Add(this.btn_Import);
            this.pnl_macro_editor.Controls.Add(this.gbx_MacroREC);
            this.pnl_macro_editor.Controls.Add(this.txtbx_script_name);
            this.pnl_macro_editor.Controls.Add(this.btn_macro_editor_close);
            this.pnl_macro_editor.Controls.Add(this.btn_ScriptWrite);
            this.pnl_macro_editor.Controls.Add(this.gbx_MacroFileImportExport);
            this.pnl_macro_editor.Controls.Add(this.lbl_MacroWrite_Icon);
            this.pnl_macro_editor.Controls.Add(this.lbl_macro_editor_now_no);
            this.pnl_macro_editor.Controls.Add(this.lbl_Arrow_Icon1);
            this.pnl_macro_editor.Controls.Add(this.btn_ScriptRead);
            this.pnl_macro_editor.Controls.Add(this.lbl_MacroRead_Icon);
            this.pnl_macro_editor.Controls.Add(this.lbl_Arrow_Dust);
            this.pnl_macro_editor.Controls.Add(this.lbl_Dustbox_Icon);
            this.pnl_macro_editor.Controls.Add(this.txtbx_script_no);
            this.pnl_macro_editor.Controls.Add(this.dgv_ScriptEditor);
            this.pnl_macro_editor.Controls.Add(this.lbl_Arrow_Icon2);
            this.pnl_macro_editor.Controls.Add(this.btn_Export);
            this.pnl_macro_editor.Controls.Add(this.lbl_Arrow_Icon3);
            this.pnl_macro_editor.Controls.Add(this.lbl_Arrow_Icon7);
            this.pnl_macro_editor.Controls.Add(this.lbl_Arrow_Icon6);
            this.pnl_macro_editor.Controls.Add(this.lbl_Arrow_Icon4);
            this.pnl_macro_editor.Controls.Add(this.lbl_Arrow_Icon5);
            resources.ApplyResources(this.pnl_macro_editor, "pnl_macro_editor");
            this.pnl_macro_editor.Name = "pnl_macro_editor";
            // 
            // lbl_macro_editor_area_title
            // 
            this.lbl_macro_editor_area_title.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lbl_macro_editor_area_title, "lbl_macro_editor_area_title");
            this.lbl_macro_editor_area_title.Name = "lbl_macro_editor_area_title";
            this.lbl_macro_editor_area_title.MouseEnter += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseEnter);
            // 
            // lbl_macro_list_title
            // 
            this.lbl_macro_list_title.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lbl_macro_list_title, "lbl_macro_list_title");
            this.lbl_macro_list_title.Name = "lbl_macro_list_title";
            this.lbl_macro_list_title.MouseEnter += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseEnter);
            // 
            // lbl_macro_editor_macro_create_title
            // 
            this.lbl_macro_editor_macro_create_title.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lbl_macro_editor_macro_create_title, "lbl_macro_editor_macro_create_title");
            this.lbl_macro_editor_macro_create_title.Name = "lbl_macro_editor_macro_create_title";
            this.lbl_macro_editor_macro_create_title.Tag = "0";
            // 
            // llbl_macro_editor_help
            // 
            this.llbl_macro_editor_help.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.llbl_macro_editor_help, "llbl_macro_editor_help");
            this.llbl_macro_editor_help.Name = "llbl_macro_editor_help";
            this.llbl_macro_editor_help.TabStop = true;
            this.llbl_macro_editor_help.MouseClick += new System.Windows.Forms.MouseEventHandler(this.llbl_help_MouseClick);
            // 
            // gbx_script_command_list
            // 
            this.gbx_script_command_list.BackColor = System.Drawing.Color.White;
            this.gbx_script_command_list.Controls.Add(this.lbl_script_command_title);
            this.gbx_script_command_list.Controls.Add(this.lbl_JoyLeftLeverPress_Icon);
            this.gbx_script_command_list.Controls.Add(this.lbl_JoyLeftLeverRelease_Icon);
            this.gbx_script_command_list.Controls.Add(this.lbl_MultiMediaPress_Icon);
            this.gbx_script_command_list.Controls.Add(this.lbl_MultiMediaRelease_Icon);
            this.gbx_script_command_list.Controls.Add(this.lbl_MouseMovePress_Icon);
            this.gbx_script_command_list.Controls.Add(this.lbl_JoyRightLeverPress_Icon);
            this.gbx_script_command_list.Controls.Add(this.lbl_JoyRightLeverRelease_Icon);
            this.gbx_script_command_list.Controls.Add(this.lbl_JoyHatSWPress_Icon);
            this.gbx_script_command_list.Controls.Add(this.lbl_JoyHatSWRelease_Icon);
            this.gbx_script_command_list.Controls.Add(this.lbl_JoyButtonPress_Icon);
            this.gbx_script_command_list.Controls.Add(this.lbl_JoyButtonRelease_Icon);
            this.gbx_script_command_list.Controls.Add(this.lbl_KeyPress_Icon);
            this.gbx_script_command_list.Controls.Add(this.lbl_KeyRelease_Icon);
            this.gbx_script_command_list.Controls.Add(this.lbl_MouseClick_Icon);
            this.gbx_script_command_list.Controls.Add(this.lbl_MouseRelease_Icon);
            this.gbx_script_command_list.Controls.Add(this.lbl_WheelScroll_Icon);
            this.gbx_script_command_list.Controls.Add(this.lbl_Interval_Icon);
            resources.ApplyResources(this.gbx_script_command_list, "gbx_script_command_list");
            this.gbx_script_command_list.Name = "gbx_script_command_list";
            this.gbx_script_command_list.TabStop = false;
            // 
            // lbl_script_command_title
            // 
            resources.ApplyResources(this.lbl_script_command_title, "lbl_script_command_title");
            this.lbl_script_command_title.Name = "lbl_script_command_title";
            // 
            // lbl_MacroRead_txt
            // 
            this.lbl_MacroRead_txt.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lbl_MacroRead_txt, "lbl_MacroRead_txt");
            this.lbl_MacroRead_txt.Name = "lbl_MacroRead_txt";
            this.lbl_MacroRead_txt.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_MacroRead_Icon_MouseClick);
            this.lbl_MacroRead_txt.MouseEnter += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseEnter);
            // 
            // lbl_MacroWrite_txt
            // 
            this.lbl_MacroWrite_txt.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lbl_MacroWrite_txt, "lbl_MacroWrite_txt");
            this.lbl_MacroWrite_txt.Name = "lbl_MacroWrite_txt";
            this.lbl_MacroWrite_txt.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_MacroWrite_Icon_MouseClick);
            this.lbl_MacroWrite_txt.MouseEnter += new System.EventHandler(this.lbl_Macro_Editor_Icon_MouseEnter);
            // 
            // pnl_keyboard_type_assist
            // 
            resources.ApplyResources(this.pnl_keyboard_type_assist, "pnl_keyboard_type_assist");
            this.pnl_keyboard_type_assist.Controls.Add(this.gbx_keyboard_setup_assis_complete);
            this.pnl_keyboard_type_assist.Controls.Add(this.lbl_keyboard_setup_assist_msg2);
            this.pnl_keyboard_type_assist.Controls.Add(this.lbl_keyboard_setup_assist_msg1);
            this.pnl_keyboard_type_assist.Controls.Add(this.lbl_keyboard_setup_assist_title);
            this.pnl_keyboard_type_assist.Controls.Add(this.pic_keyboard_setup_assist);
            this.pnl_keyboard_type_assist.Controls.Add(this.btn_keyboard_setup_set);
            this.pnl_keyboard_type_assist.Controls.Add(this.llbl_keyboard_setup_help);
            this.pnl_keyboard_type_assist.Controls.Add(this.btn_keyboard_setup_cancel);
            this.pnl_keyboard_type_assist.Name = "pnl_keyboard_type_assist";
            // 
            // gbx_keyboard_setup_assis_complete
            // 
            this.gbx_keyboard_setup_assis_complete.BackColor = System.Drawing.Color.Transparent;
            this.gbx_keyboard_setup_assis_complete.Controls.Add(this.lbl_keyboard_setup_assist_comp_type2);
            this.gbx_keyboard_setup_assis_complete.Controls.Add(this.lbl_keyboard_setup_assist_comp_type1);
            this.gbx_keyboard_setup_assis_complete.Controls.Add(this.rbtn_keyboard_setup_assist_type2);
            this.gbx_keyboard_setup_assis_complete.Controls.Add(this.rbtn_keyboard_setup_assist_type1);
            this.gbx_keyboard_setup_assis_complete.Controls.Add(this.lbl_keyboard_setup_assist_comp_msg3);
            this.gbx_keyboard_setup_assis_complete.Controls.Add(this.lbl_keyboard_setup_assist_comp_msg2);
            this.gbx_keyboard_setup_assis_complete.Controls.Add(this.lbl_keyboard_setup_assist_comp_msg1);
            resources.ApplyResources(this.gbx_keyboard_setup_assis_complete, "gbx_keyboard_setup_assis_complete");
            this.gbx_keyboard_setup_assis_complete.Name = "gbx_keyboard_setup_assis_complete";
            this.gbx_keyboard_setup_assis_complete.TabStop = false;
            // 
            // lbl_keyboard_setup_assist_comp_type2
            // 
            resources.ApplyResources(this.lbl_keyboard_setup_assist_comp_type2, "lbl_keyboard_setup_assist_comp_type2");
            this.lbl_keyboard_setup_assist_comp_type2.Name = "lbl_keyboard_setup_assist_comp_type2";
            // 
            // lbl_keyboard_setup_assist_comp_type1
            // 
            resources.ApplyResources(this.lbl_keyboard_setup_assist_comp_type1, "lbl_keyboard_setup_assist_comp_type1");
            this.lbl_keyboard_setup_assist_comp_type1.Name = "lbl_keyboard_setup_assist_comp_type1";
            // 
            // rbtn_keyboard_setup_assist_type2
            // 
            resources.ApplyResources(this.rbtn_keyboard_setup_assist_type2, "rbtn_keyboard_setup_assist_type2");
            this.rbtn_keyboard_setup_assist_type2.Name = "rbtn_keyboard_setup_assist_type2";
            this.rbtn_keyboard_setup_assist_type2.TabStop = true;
            this.rbtn_keyboard_setup_assist_type2.UseVisualStyleBackColor = true;
            this.rbtn_keyboard_setup_assist_type2.CheckedChanged += new System.EventHandler(this.rbtn_keyboard_setup_assist_type_CheckedChanged);
            // 
            // rbtn_keyboard_setup_assist_type1
            // 
            resources.ApplyResources(this.rbtn_keyboard_setup_assist_type1, "rbtn_keyboard_setup_assist_type1");
            this.rbtn_keyboard_setup_assist_type1.Name = "rbtn_keyboard_setup_assist_type1";
            this.rbtn_keyboard_setup_assist_type1.TabStop = true;
            this.rbtn_keyboard_setup_assist_type1.UseVisualStyleBackColor = true;
            this.rbtn_keyboard_setup_assist_type1.CheckedChanged += new System.EventHandler(this.rbtn_keyboard_setup_assist_type_CheckedChanged);
            // 
            // lbl_keyboard_setup_assist_comp_msg3
            // 
            resources.ApplyResources(this.lbl_keyboard_setup_assist_comp_msg3, "lbl_keyboard_setup_assist_comp_msg3");
            this.lbl_keyboard_setup_assist_comp_msg3.Name = "lbl_keyboard_setup_assist_comp_msg3";
            // 
            // lbl_keyboard_setup_assist_comp_msg2
            // 
            resources.ApplyResources(this.lbl_keyboard_setup_assist_comp_msg2, "lbl_keyboard_setup_assist_comp_msg2");
            this.lbl_keyboard_setup_assist_comp_msg2.Name = "lbl_keyboard_setup_assist_comp_msg2";
            // 
            // lbl_keyboard_setup_assist_comp_msg1
            // 
            resources.ApplyResources(this.lbl_keyboard_setup_assist_comp_msg1, "lbl_keyboard_setup_assist_comp_msg1");
            this.lbl_keyboard_setup_assist_comp_msg1.Name = "lbl_keyboard_setup_assist_comp_msg1";
            // 
            // lbl_keyboard_setup_assist_msg2
            // 
            this.lbl_keyboard_setup_assist_msg2.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lbl_keyboard_setup_assist_msg2, "lbl_keyboard_setup_assist_msg2");
            this.lbl_keyboard_setup_assist_msg2.Name = "lbl_keyboard_setup_assist_msg2";
            // 
            // lbl_keyboard_setup_assist_msg1
            // 
            this.lbl_keyboard_setup_assist_msg1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lbl_keyboard_setup_assist_msg1, "lbl_keyboard_setup_assist_msg1");
            this.lbl_keyboard_setup_assist_msg1.Name = "lbl_keyboard_setup_assist_msg1";
            // 
            // lbl_keyboard_setup_assist_title
            // 
            this.lbl_keyboard_setup_assist_title.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lbl_keyboard_setup_assist_title, "lbl_keyboard_setup_assist_title");
            this.lbl_keyboard_setup_assist_title.Name = "lbl_keyboard_setup_assist_title";
            // 
            // pic_keyboard_setup_assist
            // 
            this.pic_keyboard_setup_assist.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.pic_keyboard_setup_assist, "pic_keyboard_setup_assist");
            this.pic_keyboard_setup_assist.Name = "pic_keyboard_setup_assist";
            this.pic_keyboard_setup_assist.TabStop = false;
            // 
            // btn_keyboard_setup_set
            // 
            this.btn_keyboard_setup_set.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btn_keyboard_setup_set, "btn_keyboard_setup_set");
            this.btn_keyboard_setup_set.Name = "btn_keyboard_setup_set";
            this.btn_keyboard_setup_set.UseVisualStyleBackColor = true;
            this.btn_keyboard_setup_set.Click += new System.EventHandler(this.btn_keyboard_setup_set_Click);
            // 
            // llbl_keyboard_setup_help
            // 
            this.llbl_keyboard_setup_help.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.llbl_keyboard_setup_help, "llbl_keyboard_setup_help");
            this.llbl_keyboard_setup_help.Name = "llbl_keyboard_setup_help";
            this.llbl_keyboard_setup_help.TabStop = true;
            this.llbl_keyboard_setup_help.MouseClick += new System.Windows.Forms.MouseEventHandler(this.llbl_keyboard_setup_help_MouseClick);
            // 
            // btn_keyboard_setup_cancel
            // 
            this.btn_keyboard_setup_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btn_keyboard_setup_cancel, "btn_keyboard_setup_cancel");
            this.btn_keyboard_setup_cancel.Name = "btn_keyboard_setup_cancel";
            this.btn_keyboard_setup_cancel.UseVisualStyleBackColor = true;
            this.btn_keyboard_setup_cancel.Click += new System.EventHandler(this.btn_keyboard_setup_cancel_Click);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::RevOmate.Properties.Resources.MAIN1_BG;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.pnl_keyboard_type_assist);
            this.Controls.Add(this.pnl_system_setup);
            this.Controls.Add(this.pnl_macro_editor);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.pnl_dial_macro_editor);
            this.Controls.Add(this.btn_debug_dial_macro_disp);
            this.Controls.Add(this.btn_debug_macro_disp);
            this.Controls.Add(this.btn_debug_tab_disp);
            this.Controls.Add(this.pnl_main);
            this.Controls.Add(this.lbl_mode_status);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btn_FlashErase);
            this.Controls.Add(this.chkbx_LED_preview);
            this.Controls.Add(this.lbl_FW_Version);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.StatusBox_lbl);
            this.Controls.Add(this.StatusBox_txtbx);
            this.Controls.Add(this.dgv_FlashMemory);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ScriptEditor)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_FlashMemory)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPagePattern.ResumeLayout(false);
            this.gbx_func_setup.ResumeLayout(false);
            this.gbx_func_setup.PerformLayout();
            this.gbx_func4_setup.ResumeLayout(false);
            this.gbx_func4_setup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_func4_y_ccw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func4_x_ccw)).EndInit();
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_B_func4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_G_func4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_R_func4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func4_y_cw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func4_x_cw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func4_sensivity_ccw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func4_sensivity_cw)).EndInit();
            this.gbx_func3_setup.ResumeLayout(false);
            this.gbx_func3_setup.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_B_func3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_G_func3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_R_func3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func3_sensivity_ccw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func3_y_ccw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func3_x_ccw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func3_sensivity_cw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func3_y_cw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func3_x_cw)).EndInit();
            this.gbx_func2_setup.ResumeLayout(false);
            this.gbx_func2_setup.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_B_func2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_G_func2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_R_func2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func2_sensivity_ccw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func2_sensivity_cw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func2_y_ccw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func2_x_ccw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func2_y_cw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func2_x_cw)).EndInit();
            this.gbx_func1_setup.ResumeLayout(false);
            this.gbx_func1_setup.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_B_func1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_G_func1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_R_func1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func1_sensivity_ccw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func1_sensivity_cw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func1_y_ccw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func1_x_ccw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func1_x_cw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_func1_y_cw)).EndInit();
            this.tabPageButton.ResumeLayout(false);
            this.gbx_encoder_button_setup.ResumeLayout(false);
            this.gbx_encoder_setup.ResumeLayout(false);
            this.gbx_encoder_setup.PerformLayout();
            this.gbx_LED_set.ResumeLayout(false);
            this.gbx_LED_set.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_B)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_G)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_R)).EndInit();
            this.gbx_button_setup.ResumeLayout(false);
            this.gbx_button_setup.PerformLayout();
            this.tabPageEncoderScript.ResumeLayout(false);
            this.gbx_base_setting.ResumeLayout(false);
            this.gbx_keyboard_setting.ResumeLayout(false);
            this.gbx_keyboard_setting.PerformLayout();
            this.gbx_dial_func_LED_setting.ResumeLayout(false);
            this.gbx_mode_LED_setting.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.num_mode_led_off_time)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_encoder_script)).EndInit();
            this.gbx_Script_Add_Info_Mouse.ResumeLayout(false);
            this.gbx_Script_Add_Info_MultiMedia.ResumeLayout(false);
            this.gbx_MacroREC.ResumeLayout(false);
            this.gbx_MacroFileImportExport.ResumeLayout(false);
            this.gbx_Script_Add_Info_JoysticLever.ResumeLayout(false);
            this.gbx_Script_Add_Info_JoysticLever.PerformLayout();
            this.gbx_Script_Add_Info_JoysticButton.ResumeLayout(false);
            this.gbx_Script_Add_Info.ResumeLayout(false);
            this.gbx_Script_Add_Info.PerformLayout();
            this.gbx_FileImportExport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ScriptList)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numud_led_brightness_level_set_val)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_led_g_set_val)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_led_r_set_val)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numud_led_b_set_val)).EndInit();
            this.pnl_main.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbx_mode_color)).EndInit();
            this.contextMenuStrip_sw_func.ResumeLayout(false);
            this.gbx_system_backup.ResumeLayout(false);
            this.pnl_system_setup.ResumeLayout(false);
            this.pnl_dial_macro_editor.ResumeLayout(false);
            this.pnl_dial_macro_editor.PerformLayout();
            this.pnl_macro_editor.ResumeLayout(false);
            this.pnl_macro_editor.PerformLayout();
            this.gbx_script_command_list.ResumeLayout(false);
            this.pnl_keyboard_type_assist.ResumeLayout(false);
            this.gbx_keyboard_setup_assis_complete.ResumeLayout(false);
            this.gbx_keyboard_setup_assis_complete.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_keyboard_setup_assist)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label StatusBox_lbl;
        private System.Windows.Forms.TextBox StatusBox_txtbx;
        private System.ComponentModel.BackgroundWorker ReadWriteThread;
        private System.Windows.Forms.Timer FormUpdateTimer;
        private System.Windows.Forms.Label colum_lbl;
        private System.Windows.Forms.Label Debug_label1;
        private System.Windows.Forms.Label Debug_label2;
        private System.Windows.Forms.Label Debug_label4;
        private System.Windows.Forms.Label Debug_label3;
        private System.Windows.Forms.TextBox txtbx_EraseAns;
        private System.Windows.Forms.Button btn_Erase;
        private System.Windows.Forms.TextBox txtbx_EraseAddress;
        private System.Windows.Forms.Label lbl_EraseAddress;
        private System.Windows.Forms.Label lbl_Erase;
        private System.Windows.Forms.Button btn_Write;
        private System.Windows.Forms.Button btn_Read;
        private System.Windows.Forms.Label lbl_WriteData;
        private System.Windows.Forms.Label lbl_ReadSize;
        private System.Windows.Forms.Label lbl_WriteAddress;
        private System.Windows.Forms.Label lbl_ReadAddress;
        private System.Windows.Forms.Label lbl_Write;
        private System.Windows.Forms.Label lbl_Read;
        private System.Windows.Forms.TextBox txtbx_WriteAns;
        private System.Windows.Forms.TextBox txtbx_WriteData;
        private System.Windows.Forms.TextBox txtbx_WriteAddress;
        private System.Windows.Forms.TextBox txtbx_ReadData;
        private System.Windows.Forms.TextBox txtbx_ReadSize;
        private System.Windows.Forms.TextBox txtbx_ReadAddress;
        private System.Windows.Forms.DataGridView dgv_ScriptEditor;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbl_WheelScroll_Icon;
        private System.Windows.Forms.Label lbl_MouseRelease_Icon;
        private System.Windows.Forms.Label lbl_MouseClick_Icon;
        private System.Windows.Forms.Label lbl_KeyPress_Icon;
        private System.Windows.Forms.Label lbl_KeyRelease_Icon;
        private System.Windows.Forms.Label lbl_Interval_Icon;
        private System.Windows.Forms.Label lbl_Dustbox_Icon;
        private System.Windows.Forms.Label lbl_Clear_btn;
        private System.Windows.Forms.ImageList Script_Editor_Btn_imageList;
        private System.Windows.Forms.Label lbl_REC_Btn;
        private System.Windows.Forms.DataGridView dgv_FlashMemory;
        private System.Windows.Forms.DataGridViewTextBoxColumn F_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn F_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn F_SIZE;
        private System.Windows.Forms.Button btn_debug4;
        private System.Windows.Forms.Button btn_debug3;
        private System.Windows.Forms.Button btn_debug2;
        private System.Windows.Forms.Button btn_debug1;
        private System.Windows.Forms.Label lbl_Arrow_Dust;
        private System.Windows.Forms.Label lbl_Arrow_Icon1;
        private System.Windows.Forms.Button btn_Export;
        private System.Windows.Forms.Button btn_ScriptRead;
        private System.Windows.Forms.Button btn_ScriptWrite;
        private System.Windows.Forms.Label lbl_Script_Add_Info;
        private System.Windows.Forms.TextBox txtbx_Interval_Inputbox;
        private System.Windows.Forms.Button btn_Import;
        private System.Windows.Forms.SaveFileDialog ScriptFile_Save_saveFileDialog;
        private System.Windows.Forms.OpenFileDialog ScriptFile_Import_openFileDialog;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RichTextBox rtxtbx_debug_flash_read;
        private System.Windows.Forms.TextBox txtbx_debug_flash_read_size;
        private System.Windows.Forms.TextBox txtbx_debug_flash_read_addr;
        private System.Windows.Forms.Button btn_debug_flash_read;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageScript;
        private System.Windows.Forms.TabPage tabPagePattern;
        private System.Windows.Forms.DataGridView dgv_ScriptList;
        private System.Windows.Forms.TextBox txtbx_script_name;
        private System.Windows.Forms.Label lbl_macro_editor_now_macro_name;
        private System.Windows.Forms.TextBox txtbx_script_no;
        private System.Windows.Forms.Label lbl_macro_editor_now_no;
        private System.Windows.Forms.TextBox txtbx_debug01;
        private System.Windows.Forms.TabPage tabPageButton;
        private System.Windows.Forms.Label lbl_FW_Version;
        private System.Windows.Forms.GroupBox gbx_MacroREC;
        private System.Windows.Forms.GroupBox gbx_FileImportExport;
        private System.Windows.Forms.Label lbl_JoyRightLeverRelease_Icon;
        private System.Windows.Forms.Label lbl_JoyRightLeverPress_Icon;
        private System.Windows.Forms.Label lbl_JoyButtonRelease_Icon;
        private System.Windows.Forms.Label lbl_JoyButtonPress_Icon;
        private System.Windows.Forms.Label lbl_JoyHatSWRelease_Icon;
        private System.Windows.Forms.Label lbl_JoyHatSWPress_Icon;
        private System.Windows.Forms.Label lbl_JoyLeftLeverRelease_Icon;
        private System.Windows.Forms.Label lbl_JoyLeftLeverPress_Icon;
        private System.Windows.Forms.Label lbl_FileExport_Icon;
        private System.Windows.Forms.Label lbl_FileImport_Icon;
        private System.Windows.Forms.Label lbl_MacroRead_Icon;
        private System.Windows.Forms.Label lbl_MacroWrite_Icon;
        private System.Windows.Forms.Label lbl_Arrow_Icon7;
        private System.Windows.Forms.Label lbl_Arrow_Icon6;
        private System.Windows.Forms.Label lbl_Arrow_Icon5;
        private System.Windows.Forms.Label lbl_Arrow_Icon4;
        private System.Windows.Forms.Label lbl_Arrow_Icon3;
        private System.Windows.Forms.Label lbl_Arrow_Icon2;
        private System.Windows.Forms.Label lbl_REC_Icon;
        private System.Windows.Forms.Label lbl_FileImportExportTitle_Icon;
        private System.Windows.Forms.ImageList DustBox_imageList;
        private System.Windows.Forms.ImageList FileImportExport_imageList;
        private System.Windows.Forms.ImageList DeviceReadWrite_imageList;
        private System.Windows.Forms.ImageList MacroEditIcon_imageList;
        private System.Windows.Forms.Label lbl_function_set_icon;
        private System.Windows.Forms.ImageList SetButton_imageList;
        private System.Windows.Forms.Label lbl_button_setting_set_icon;
        private System.Windows.Forms.ImageList OnTimeSet_imageList;
        private System.Windows.Forms.GroupBox gbx_Script_Add_Info_JoysticLever;
        private System.Windows.Forms.Label lbl_Script_Add_Info_JoysticLever_Msg;
        private System.Windows.Forms.TextBox txtbx_Script_Add_Info_JoysticLever_Y;
        private System.Windows.Forms.TextBox txtbx_Script_Add_Info_JoysticLever_X;
        private System.Windows.Forms.GroupBox gbx_Script_Add_Info_JoysticButton;
        private System.Windows.Forms.Label lbl_Script_Add_Info_JoysticButton_Set;
        private System.Windows.Forms.Label lbl_Script_Add_Info_JoysticButton_Msg;
        private System.Windows.Forms.Label lbl_Script_Add_Info_JoysticLever_X;
        private System.Windows.Forms.Label lbl_Script_Add_Info_JoysticLever_Y;
        private System.Windows.Forms.Label lbl_Script_Add_Info_JoysticLever_Set;
        private System.Windows.Forms.GroupBox gbx_Script_Add_Info;
        private System.Windows.Forms.Label lbl_Script_Add_Info_ClickArea;
        private System.Windows.Forms.RadioButton rbtn_JoystickButton13;
        private System.Windows.Forms.RadioButton rbtn_JoystickButton12;
        private System.Windows.Forms.RadioButton rbtn_JoystickButton11;
        private System.Windows.Forms.RadioButton rbtn_JoystickButton10;
        private System.Windows.Forms.RadioButton rbtn_JoystickButton09;
        private System.Windows.Forms.RadioButton rbtn_JoystickButton08;
        private System.Windows.Forms.RadioButton rbtn_JoystickButton07;
        private System.Windows.Forms.RadioButton rbtn_JoystickButton06;
        private System.Windows.Forms.RadioButton rbtn_JoystickButton05;
        private System.Windows.Forms.RadioButton rbtn_JoystickButton04;
        private System.Windows.Forms.RadioButton rbtn_JoystickButton03;
        private System.Windows.Forms.RadioButton rbtn_JoystickButton02;
        private System.Windows.Forms.RadioButton rbtn_JoystickButton01;
        private System.Windows.Forms.ImageList Pattern_Icon_imageList;
        private System.Windows.Forms.GroupBox gbx_MacroFileImportExport;
        private System.Windows.Forms.Label lbl_MacroFileExport_Icon;
        private System.Windows.Forms.Label lbl_MacroFileImportExportTitle_Icon;
        private System.Windows.Forms.Label lbl_MacroFileImport_Icon;
        private System.Windows.Forms.ProgressBar pgb_process_status;
        private System.Windows.Forms.Label lbl_progress_total_value;
        private System.Windows.Forms.Label lbl_progress_now_value;
        private System.Windows.Forms.Label lbl_progress_per;
        private System.Windows.Forms.Button btn_FlashErase;
        private System.Windows.Forms.SaveFileDialog PatternListFile_saveFileDialog;
        private System.Windows.Forms.OpenFileDialog PatternListFile_openFileDialog;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_ScriptList_No;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_ScriptList_ScriptName;
        private System.Windows.Forms.ImageList MacroFileImportExport_imageList;
        private System.Windows.Forms.ImageList PatternFileImportExport_imageList;
        private System.Windows.Forms.Label lbl_button_factory_reset_icon;
        private System.Windows.Forms.ImageList Reset_Icon_imageList;
        private System.Windows.Forms.Label lbl_reset_bg;
        private System.Windows.Forms.Label lbl_reset_title;
        private System.Windows.Forms.NumericUpDown numud_led_r_set_val;
        private System.Windows.Forms.NumericUpDown numud_led_g_set_val;
        private System.Windows.Forms.NumericUpDown numud_led_b_set_val;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_debug_led_duty_set;
        private System.Windows.Forms.CheckBox chkbx_led_debug;
        private System.Windows.Forms.GroupBox gbx_button_setup;
        private System.Windows.Forms.ComboBox cmbbx_button_1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox gbx_LED_set;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TrackBar trackBar_B;
        private System.Windows.Forms.TrackBar trackBar_G;
        private System.Windows.Forms.TrackBar trackBar_R;
        private System.Windows.Forms.CheckBox chkbx_LED_preview;
        private System.Windows.Forms.Label lbl_LED_COLOR_8;
        private System.Windows.Forms.Label lbl_LED_COLOR_7;
        private System.Windows.Forms.Label lbl_LED_COLOR_6;
        private System.Windows.Forms.Label lbl_LED_COLOR_5;
        private System.Windows.Forms.Label lbl_LED_COLOR_4;
        private System.Windows.Forms.Label lbl_LED_COLOR_3;
        private System.Windows.Forms.Label lbl_LED_COLOR_2;
        private System.Windows.Forms.Label lbl_LED_COLOR_1;
        private System.Windows.Forms.GroupBox gbx_encoder_setup;
        private System.Windows.Forms.ComboBox cmbbx_encoder_button;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.ComboBox cmbbx_button_10;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.ComboBox cmbbx_button_9;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.ComboBox cmbbx_button_8;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.ComboBox cmbbx_button_7;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.ComboBox cmbbx_button_6;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.ComboBox cmbbx_button_5;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.ComboBox cmbbx_button_4;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.ComboBox cmbbx_button_3;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.ComboBox cmbbx_button_2;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.ComboBox cmbbx_encoder_default;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lbl_LED_COLOR_9;
        private System.Windows.Forms.Label lbl_button_mode3_select;
        private System.Windows.Forms.Label lbl_button_mode2_select;
        private System.Windows.Forms.Label lbl_button_mode1_select;
        private System.Windows.Forms.Label lbl_LED_color_border;
        private System.Windows.Forms.GroupBox gbx_encoder_button_setup;
        private System.Windows.Forms.Label lbl_func_mode3_select;
        private System.Windows.Forms.Label lbl_func_mode2_select;
        private System.Windows.Forms.Label lbl_func_mode1_select;
        private System.Windows.Forms.GroupBox gbx_func_setup;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lbl_LED_COLOR_9_func1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TrackBar trackBar_B_func1;
        private System.Windows.Forms.TrackBar trackBar_G_func1;
        private System.Windows.Forms.TrackBar trackBar_R_func1;
        private System.Windows.Forms.Label lbl_LED_COLOR_8_func1;
        private System.Windows.Forms.Label lbl_LED_COLOR_7_func1;
        private System.Windows.Forms.Label lbl_LED_COLOR_6_func1;
        private System.Windows.Forms.Label lbl_LED_COLOR_5_func1;
        private System.Windows.Forms.Label lbl_LED_COLOR_4_func1;
        private System.Windows.Forms.Label lbl_LED_COLOR_3_func1;
        private System.Windows.Forms.Label lbl_LED_COLOR_2_func1;
        private System.Windows.Forms.Label lbl_LED_COLOR_1_func1;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.GroupBox gbx_func1_setup;
        private System.Windows.Forms.NumericUpDown num_func1_sensivity_ccw;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown num_func1_sensivity_cw;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbbx_func1_set_type_ccw;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbbx_func1_set_type_cw;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox gbx_func3_setup;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.Label lbl_LED_COLOR_9_func3;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.TrackBar trackBar_B_func3;
        private System.Windows.Forms.TrackBar trackBar_G_func3;
        private System.Windows.Forms.TrackBar trackBar_R_func3;
        private System.Windows.Forms.Label lbl_LED_COLOR_8_func3;
        private System.Windows.Forms.Label lbl_LED_COLOR_7_func3;
        private System.Windows.Forms.Label lbl_LED_COLOR_6_func3;
        private System.Windows.Forms.Label lbl_LED_COLOR_5_func3;
        private System.Windows.Forms.Label lbl_LED_COLOR_4_func3;
        private System.Windows.Forms.Label lbl_LED_COLOR_3_func3;
        private System.Windows.Forms.Label lbl_LED_COLOR_2_func3;
        private System.Windows.Forms.Label lbl_LED_COLOR_1_func3;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.NumericUpDown num_func3_sensivity_ccw;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.NumericUpDown num_func3_sensivity_cw;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.ComboBox cmbbx_func3_set_type_ccw;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.ComboBox cmbbx_func3_set_type_cw;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.GroupBox gbx_func2_setup;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label lbl_LED_COLOR_9_func2;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.TrackBar trackBar_B_func2;
        private System.Windows.Forms.TrackBar trackBar_G_func2;
        private System.Windows.Forms.TrackBar trackBar_R_func2;
        private System.Windows.Forms.Label lbl_LED_COLOR_8_func2;
        private System.Windows.Forms.Label lbl_LED_COLOR_7_func2;
        private System.Windows.Forms.Label lbl_LED_COLOR_6_func2;
        private System.Windows.Forms.Label lbl_LED_COLOR_5_func2;
        private System.Windows.Forms.Label lbl_LED_COLOR_4_func2;
        private System.Windows.Forms.Label lbl_LED_COLOR_3_func2;
        private System.Windows.Forms.Label lbl_LED_COLOR_2_func2;
        private System.Windows.Forms.Label lbl_LED_COLOR_1_func2;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.NumericUpDown num_func2_sensivity_ccw;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.NumericUpDown num_func2_sensivity_cw;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.ComboBox cmbbx_func2_set_type_ccw;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.ComboBox cmbbx_func2_set_type_cw;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.GroupBox gbx_func4_setup;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.Label lbl_LED_COLOR_9_func4;
        private System.Windows.Forms.GroupBox groupBox15;
        private System.Windows.Forms.TrackBar trackBar_B_func4;
        private System.Windows.Forms.TrackBar trackBar_G_func4;
        private System.Windows.Forms.TrackBar trackBar_R_func4;
        private System.Windows.Forms.Label lbl_LED_COLOR_8_func4;
        private System.Windows.Forms.Label lbl_LED_COLOR_7_func4;
        private System.Windows.Forms.Label lbl_LED_COLOR_6_func4;
        private System.Windows.Forms.Label lbl_LED_COLOR_5_func4;
        private System.Windows.Forms.Label lbl_LED_COLOR_4_func4;
        private System.Windows.Forms.Label lbl_LED_COLOR_3_func4;
        private System.Windows.Forms.Label lbl_LED_COLOR_2_func4;
        private System.Windows.Forms.Label lbl_LED_COLOR_1_func4;
        private System.Windows.Forms.Label label75;
        private System.Windows.Forms.NumericUpDown num_func4_sensivity_ccw;
        private System.Windows.Forms.Label label76;
        private System.Windows.Forms.NumericUpDown num_func4_sensivity_cw;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.ComboBox cmbbx_func4_set_type_ccw;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.ComboBox cmbbx_func4_set_type_cw;
        private System.Windows.Forms.Label label79;
        private System.Windows.Forms.Button btn_LED_preview;
        private System.Windows.Forms.TextBox txtbx_func1_key_cw;
        private System.Windows.Forms.CheckBox chk_func1_win_cw;
        private System.Windows.Forms.CheckBox chk_func1_alt_cw;
        private System.Windows.Forms.CheckBox chk_func1_shift_cw;
        private System.Windows.Forms.CheckBox chk_func1_ctrl_cw;
        private System.Windows.Forms.TextBox txtbx_func1_key_ccw;
        private System.Windows.Forms.CheckBox chk_func1_win_ccw;
        private System.Windows.Forms.CheckBox chk_func1_alt_ccw;
        private System.Windows.Forms.CheckBox chk_func1_shift_ccw;
        private System.Windows.Forms.CheckBox chk_func1_ctrl_ccw;
        private System.Windows.Forms.Button btn_LED_preview_func4;
        private System.Windows.Forms.Button btn_LED_preview_func3;
        private System.Windows.Forms.Button btn_LED_preview_func2;
        private System.Windows.Forms.Button btn_LED_preview_func1;
        private System.Windows.Forms.TextBox txtbx_func4_key_cw;
        private System.Windows.Forms.CheckBox chk_func4_win_cw;
        private System.Windows.Forms.CheckBox chk_func4_alt_cw;
        private System.Windows.Forms.CheckBox chk_func4_shift_cw;
        private System.Windows.Forms.CheckBox chk_func4_ctrl_cw;
        private System.Windows.Forms.TextBox txtbx_func4_key_ccw;
        private System.Windows.Forms.CheckBox chk_func4_win_ccw;
        private System.Windows.Forms.CheckBox chk_func4_alt_ccw;
        private System.Windows.Forms.CheckBox chk_func4_shift_ccw;
        private System.Windows.Forms.CheckBox chk_func4_ctrl_ccw;
        private System.Windows.Forms.TextBox txtbx_func3_key_cw;
        private System.Windows.Forms.CheckBox chk_func3_win_cw;
        private System.Windows.Forms.CheckBox chk_func3_alt_cw;
        private System.Windows.Forms.CheckBox chk_func3_shift_cw;
        private System.Windows.Forms.CheckBox chk_func3_ctrl_cw;
        private System.Windows.Forms.TextBox txtbx_func3_key_ccw;
        private System.Windows.Forms.CheckBox chk_func3_win_ccw;
        private System.Windows.Forms.CheckBox chk_func3_alt_ccw;
        private System.Windows.Forms.CheckBox chk_func3_shift_ccw;
        private System.Windows.Forms.CheckBox chk_func3_ctrl_ccw;
        private System.Windows.Forms.TextBox txtbx_func2_key_cw;
        private System.Windows.Forms.CheckBox chk_func2_win_cw;
        private System.Windows.Forms.CheckBox chk_func2_alt_cw;
        private System.Windows.Forms.CheckBox chk_func2_shift_cw;
        private System.Windows.Forms.CheckBox chk_func2_ctrl_cw;
        private System.Windows.Forms.TextBox txtbx_func2_key_ccw;
        private System.Windows.Forms.CheckBox chk_func2_win_ccw;
        private System.Windows.Forms.CheckBox chk_func2_alt_ccw;
        private System.Windows.Forms.CheckBox chk_func2_shift_ccw;
        private System.Windows.Forms.CheckBox chk_func2_ctrl_ccw;
        private System.Windows.Forms.Label lbl_func1_title_cw;
        private System.Windows.Forms.NumericUpDown num_func1_x_cw;
        private System.Windows.Forms.NumericUpDown num_func1_y_cw;
        private System.Windows.Forms.NumericUpDown num_func1_y_ccw;
        private System.Windows.Forms.NumericUpDown num_func1_x_ccw;
        private System.Windows.Forms.Label lbl_func1_title_ccw;
        private System.Windows.Forms.NumericUpDown num_func2_y_cw;
        private System.Windows.Forms.NumericUpDown num_func2_x_cw;
        private System.Windows.Forms.Label lbl_func2_title_cw;
        private System.Windows.Forms.NumericUpDown num_func2_y_ccw;
        private System.Windows.Forms.NumericUpDown num_func2_x_ccw;
        private System.Windows.Forms.Label lbl_func2_title_ccw;
        private System.Windows.Forms.NumericUpDown num_func3_y_cw;
        private System.Windows.Forms.NumericUpDown num_func3_x_cw;
        private System.Windows.Forms.Label lbl_func3_title_cw;
        private System.Windows.Forms.NumericUpDown num_func3_y_ccw;
        private System.Windows.Forms.NumericUpDown num_func3_x_ccw;
        private System.Windows.Forms.Label lbl_func3_title_ccw;
        private System.Windows.Forms.NumericUpDown num_func4_y_cw;
        private System.Windows.Forms.NumericUpDown num_func4_x_cw;
        private System.Windows.Forms.Label lbl_func4_title_cw;
        private System.Windows.Forms.NumericUpDown num_func4_y_ccw;
        private System.Windows.Forms.NumericUpDown num_func4_x_ccw;
        private System.Windows.Forms.Label lbl_func4_title_ccw;
        private System.Windows.Forms.TextBox txtbx_function2_name;
        private System.Windows.Forms.TextBox txtbx_function3_name;
        private System.Windows.Forms.TextBox txtbx_function4_name;
        private System.Windows.Forms.TextBox txtbx_function1_name;
        private System.Windows.Forms.Button btn_func4_key_clr_ccw;
        private System.Windows.Forms.Button btn_func4_key_clr_cw;
        private System.Windows.Forms.Button btn_func3_key_clr_ccw;
        private System.Windows.Forms.Button btn_func3_key_clr_cw;
        private System.Windows.Forms.Button btn_func2_key_clr_ccw;
        private System.Windows.Forms.Button btn_func2_key_clr_cw;
        private System.Windows.Forms.Button btn_func1_key_clr_ccw;
        private System.Windows.Forms.Button btn_func1_key_clr_cw;
        private System.Windows.Forms.Label lbl_MultiMediaRelease_Icon;
        private System.Windows.Forms.Label lbl_MultiMediaPress_Icon;
        private System.Windows.Forms.Label lbl_MouseMovePress_Icon;
        private System.Windows.Forms.GroupBox gbx_Script_Add_Info_MultiMedia;
        private System.Windows.Forms.RadioButton rbtn_MultiMedia11;
        private System.Windows.Forms.RadioButton rbtn_MultiMedia10;
        private System.Windows.Forms.RadioButton rbtn_MultiMedia09;
        private System.Windows.Forms.RadioButton rbtn_MultiMedia08;
        private System.Windows.Forms.RadioButton rbtn_MultiMedia07;
        private System.Windows.Forms.RadioButton rbtn_MultiMedia06;
        private System.Windows.Forms.RadioButton rbtn_MultiMedia05;
        private System.Windows.Forms.RadioButton rbtn_MultiMedia04;
        private System.Windows.Forms.RadioButton rbtn_MultiMedia03;
        private System.Windows.Forms.RadioButton rbtn_MultiMedia02;
        private System.Windows.Forms.RadioButton rbtn_MultiMedia01;
        private System.Windows.Forms.Label lbl_Script_Add_Info_MultiMedia_Msg;
        private System.Windows.Forms.Label lbl_Script_Add_Info_MultiMedia_Set;
        private System.Windows.Forms.Label lbl_mode_status;
        private System.Windows.Forms.GroupBox gbx_Script_Add_Info_Mouse;
        private System.Windows.Forms.RadioButton rbtn_Mouse05;
        private System.Windows.Forms.RadioButton rbtn_Mouse04;
        private System.Windows.Forms.RadioButton rbtn_Mouse03;
        private System.Windows.Forms.RadioButton rbtn_Mouse02;
        private System.Windows.Forms.RadioButton rbtn_Mouse01;
        private System.Windows.Forms.Label lbl_Script_Add_Info_Mouse_Msg;
        private System.Windows.Forms.Label lbl_Script_Add_Info_Mouse_Set;
        private System.Windows.Forms.Label lbl_LED_COLOR_NOW_func4;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lbl_LED_COLOR_NOW_func3;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lbl_LED_COLOR_NOW_func2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lbl_LED_COLOR_NOW_func1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_LED_COLOR_NOW;
        private System.Windows.Forms.Label lbl_LED_color_now_border;
        private System.Windows.Forms.GroupBox gbx_base_setting;
        private System.Windows.Forms.Label lbl_mode_led_off_unit;
        private System.Windows.Forms.Button btn_base_setting_set;
        private System.Windows.Forms.NumericUpDown num_mode_led_off_time;
        private System.Windows.Forms.CheckBox chkbx_mode_led_off;
        private System.Windows.Forms.CheckBox chkbx_led_sleep;
        private System.Windows.Forms.GroupBox gbx_dial_func_LED_setting;
        private System.Windows.Forms.RadioButton rbtn_func_led_on;
        private System.Windows.Forms.RadioButton rbtn_func_led_slow;
        private System.Windows.Forms.RadioButton rbtn_func_led_flash;
        private System.Windows.Forms.GroupBox gbx_mode_LED_setting;
        private System.Windows.Forms.CheckBox chkbx_encoder_typematic;
        private System.Windows.Forms.TabPage tabPageEncoderScript;
        private System.Windows.Forms.Label lbl_encoder_script_setting_set_icon;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.DataGridView dgv_encoder_script;
        private System.Windows.Forms.ComboBox cmbbx_encoder_script_select_no;
        private System.Windows.Forms.RadioButton rbtn_LED_Level_Light_func4;
        private System.Windows.Forms.RadioButton rbtn_LED_Level_Normal_func4;
        private System.Windows.Forms.RadioButton rbtn_LED_Level_Dark_func4;
        private System.Windows.Forms.RadioButton rbtn_LED_Level_Light_func3;
        private System.Windows.Forms.RadioButton rbtn_LED_Level_Normal_func3;
        private System.Windows.Forms.RadioButton rbtn_LED_Level_Dark_func3;
        private System.Windows.Forms.RadioButton rbtn_LED_Level_Light_func2;
        private System.Windows.Forms.RadioButton rbtn_LED_Level_Normal_func2;
        private System.Windows.Forms.RadioButton rbtn_LED_Level_Dark_func2;
        private System.Windows.Forms.RadioButton rbtn_LED_Level_Light_func1;
        private System.Windows.Forms.RadioButton rbtn_LED_Level_Normal_func1;
        private System.Windows.Forms.RadioButton rbtn_LED_Level_Dark_func1;
        private System.Windows.Forms.RadioButton rbtn_LED_Level_Light;
        private System.Windows.Forms.RadioButton rbtn_LED_Level_Normal;
        private System.Windows.Forms.RadioButton rbtn_LED_Level_Dark;
        private System.Windows.Forms.NumericUpDown numud_led_brightness_level_set_val;
        private System.Windows.Forms.CheckBox chkbx_encoder_script_loop;
        private System.Windows.Forms.Panel pnl_main;
        private System.Windows.Forms.Label lbl_sw10_func_name;
        private System.Windows.Forms.Label lbl_sw9_func_name;
        private System.Windows.Forms.Label lbl_sw8_func_name;
        private System.Windows.Forms.Label lbl_sw7_func_name;
        private System.Windows.Forms.Label lbl_sw6_func_name;
        private System.Windows.Forms.Label lbl_sw5_func_name;
        private System.Windows.Forms.Label lbl_sw4_func_name;
        private System.Windows.Forms.Label lbl_sw3_func_name;
        private System.Windows.Forms.Label lbl_sw2_func_name;
        private System.Windows.Forms.Label lbl_sw1_func_name;
        private System.Windows.Forms.Label lbl_Dial_func_name;
        private System.Windows.Forms.Label lbl_Dial_func_name4;
        private System.Windows.Forms.Label lbl_Dial_func_name3;
        private System.Windows.Forms.Label lbl_Dial_func_name2;
        private System.Windows.Forms.Label lbl_Dial_func_name1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_sw_func;
        private System.Windows.Forms.ToolStripMenuItem 機能設定画面ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 標準設定に戻すToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 割り当て解除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem インポートToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem エクスポートToolStripMenuItem;
        private System.Windows.Forms.Label lbl_profile_color_3;
        private System.Windows.Forms.Label lbl_profile_color_2;
        private System.Windows.Forms.Label lbl_profile_color_1;
        private System.Windows.Forms.Label lbl_profile_3_select;
        private System.Windows.Forms.Label lbl_profile_2_select;
        private System.Windows.Forms.Label lbl_profile_1_select;
        private System.Windows.Forms.PictureBox pbx_mode_color;
        private System.Windows.Forms.Label lbl_func_color_4;
        private System.Windows.Forms.Label lbl_func_color_3;
        private System.Windows.Forms.Label lbl_func_color_2;
        private System.Windows.Forms.Label lbl_func_color_1;
        private System.Windows.Forms.Button btn_system_setup;
        private System.Windows.Forms.Button btn_dial_macro_editor;
        private System.Windows.Forms.Button btn_macro_editor;
        private System.Windows.Forms.Button btn_macro_editor_close;
        private System.Windows.Forms.Button btn_dial_macro_editor_cancel;
        private System.Windows.Forms.Button btn_dial_macro_editor_submit;
        private System.Windows.Forms.Button btn_debug_tab_disp;
        private System.Windows.Forms.Button btn_debug_macro_disp;
        private System.Windows.Forms.Button btn_debug_dial_macro_disp;
        private System.Windows.Forms.Button btn_system_setup_close;
        private System.Windows.Forms.GroupBox gbx_system_backup;
        private System.Windows.Forms.Label lbl_system_backup_title;
        private System.Windows.Forms.Button btn_system_default_set;
        private System.Windows.Forms.Label lbl_default_setting_title;
        private System.Windows.Forms.Button btn_system_backupfile_save;
        private System.Windows.Forms.Button btn_system_backupfile_read;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.LinkLabel llbl_help;
        private System.Windows.Forms.Label lbl_profile3_border;
        private System.Windows.Forms.Label lbl_profile2_border;
        private System.Windows.Forms.Label lbl_profile1_border;
        private System.Windows.Forms.Panel pnl_system_setup;
        private System.Windows.Forms.LinkLabel llbl_system_setup_help;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_no;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgv_script_name;
        private System.Windows.Forms.Panel pnl_dial_macro_editor;
        private System.Windows.Forms.Panel pnl_macro_editor;
        private System.Windows.Forms.Label lbl_MacroRead_txt;
        private System.Windows.Forms.Label lbl_MacroWrite_txt;
        private System.Windows.Forms.GroupBox gbx_script_command_list;
        private System.Windows.Forms.Label lbl_script_command_title;
        private System.Windows.Forms.LinkLabel llbl_dial_macro_editor_help;
        private System.Windows.Forms.LinkLabel llbl_macro_editor_help;
        private System.Windows.Forms.DataGridViewTextBoxColumn E_SIZE;
        private System.Windows.Forms.DataGridViewTextBoxColumn E_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn E_VALUE;
        private System.Windows.Forms.Label lbl_macro_editor_macro_create_title;
        private System.Windows.Forms.Label lbl_macro_editor_area_title;
        private System.Windows.Forms.Label lbl_macro_list_title;
        private System.Windows.Forms.GroupBox gbx_keyboard_setting;
        private System.Windows.Forms.RadioButton rbtn_keyboard_ja;
        private System.Windows.Forms.RadioButton rbtn_keyboard_us;
        private System.Windows.Forms.Button btn_system_factory_set;
        private System.Windows.Forms.Label lbl_factory_setting_title;
        private System.Windows.Forms.Panel pnl_keyboard_type_assist;
        private System.Windows.Forms.Button btn_keyboard_setup_set;
        private System.Windows.Forms.LinkLabel llbl_keyboard_setup_help;
        private System.Windows.Forms.Button btn_keyboard_setup_cancel;
        private System.Windows.Forms.GroupBox gbx_keyboard_setup_assis_complete;
        private System.Windows.Forms.Label lbl_keyboard_setup_assist_comp_type2;
        private System.Windows.Forms.Label lbl_keyboard_setup_assist_comp_type1;
        private System.Windows.Forms.RadioButton rbtn_keyboard_setup_assist_type2;
        private System.Windows.Forms.RadioButton rbtn_keyboard_setup_assist_type1;
        private System.Windows.Forms.Label lbl_keyboard_setup_assist_comp_msg3;
        private System.Windows.Forms.Label lbl_keyboard_setup_assist_comp_msg2;
        private System.Windows.Forms.Label lbl_keyboard_setup_assist_comp_msg1;
        private System.Windows.Forms.Label lbl_keyboard_setup_assist_msg2;
        private System.Windows.Forms.Label lbl_keyboard_setup_assist_msg1;
        private System.Windows.Forms.Label lbl_keyboard_setup_assist_title;
        private System.Windows.Forms.PictureBox pic_keyboard_setup_assist;
        private System.Windows.Forms.Button btn_keyboard_setup_assistant;
    }
}

