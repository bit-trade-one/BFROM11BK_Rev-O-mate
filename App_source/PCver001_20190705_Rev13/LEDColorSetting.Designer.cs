namespace RevOmate
{
    partial class LEDColorSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LEDColorSetting));
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_submit = new System.Windows.Forms.Button();
            this.gbx_LED_set = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbl_LED_Brightness_title = new System.Windows.Forms.Label();
            this.rbtn_LED_Level_Light = new System.Windows.Forms.RadioButton();
            this.rbtn_LED_Level_Dark = new System.Windows.Forms.RadioButton();
            this.rbtn_LED_Level_Normal = new System.Windows.Forms.RadioButton();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btn_color_select = new System.Windows.Forms.Button();
            this.num_LED_Duty_B = new System.Windows.Forms.NumericUpDown();
            this.num_LED_Duty_G = new System.Windows.Forms.NumericUpDown();
            this.num_LED_Duty_R = new System.Windows.Forms.NumericUpDown();
            this.lbl_color_b = new System.Windows.Forms.Label();
            this.lbl_color_g = new System.Windows.Forms.Label();
            this.lbl_color_r = new System.Windows.Forms.Label();
            this.trackBar_B = new System.Windows.Forms.TrackBar();
            this.trackBar_G = new System.Windows.Forms.TrackBar();
            this.trackBar_R = new System.Windows.Forms.TrackBar();
            this.lbl_LED_COLOR_NOW = new System.Windows.Forms.Label();
            this.lbl_LED_color_now_border = new System.Windows.Forms.Label();
            this.btn_LED_preview = new System.Windows.Forms.Button();
            this.lbl_LED_COLOR_9 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_8 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_7 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_6 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_5 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_4 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_3 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_2 = new System.Windows.Forms.Label();
            this.lbl_LED_COLOR_1 = new System.Windows.Forms.Label();
            this.lbl_LED_color_border = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gbx_led_color_select = new System.Windows.Forms.GroupBox();
            this.lbl_led_color_select_msg = new System.Windows.Forms.Label();
            this.lbl_led_color_select_position = new System.Windows.Forms.Label();
            this.pic_led_color_select = new System.Windows.Forms.PictureBox();
            this.gbx_LED_set.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_LED_Duty_B)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_LED_Duty_G)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_LED_Duty_R)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_B)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_G)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_R)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.gbx_led_color_select.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_led_color_select)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_cancel
            // 
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.Location = new System.Drawing.Point(345, 300);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(150, 34);
            this.btn_cancel.TabIndex = 191;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_submit
            // 
            this.btn_submit.Location = new System.Drawing.Point(185, 300);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(150, 34);
            this.btn_submit.TabIndex = 190;
            this.btn_submit.Text = "Submit";
            this.btn_submit.UseVisualStyleBackColor = true;
            this.btn_submit.Click += new System.EventHandler(this.btn_submit_Click);
            // 
            // gbx_LED_set
            // 
            this.gbx_LED_set.Controls.Add(this.groupBox2);
            this.gbx_LED_set.Controls.Add(this.groupBox6);
            this.gbx_LED_set.Location = new System.Drawing.Point(15, 15);
            this.gbx_LED_set.Name = "gbx_LED_set";
            this.gbx_LED_set.Size = new System.Drawing.Size(485, 270);
            this.gbx_LED_set.TabIndex = 100;
            this.gbx_LED_set.TabStop = false;
            this.gbx_LED_set.Text = "LED設定";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbl_LED_Brightness_title);
            this.groupBox2.Controls.Add(this.rbtn_LED_Level_Light);
            this.groupBox2.Controls.Add(this.rbtn_LED_Level_Dark);
            this.groupBox2.Controls.Add(this.rbtn_LED_Level_Normal);
            this.groupBox2.Location = new System.Drawing.Point(15, 200);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(455, 54);
            this.groupBox2.TabIndex = 193;
            this.groupBox2.TabStop = false;
            // 
            // lbl_LED_Brightness_title
            // 
            this.lbl_LED_Brightness_title.Location = new System.Drawing.Point(5, 19);
            this.lbl_LED_Brightness_title.Name = "lbl_LED_Brightness_title";
            this.lbl_LED_Brightness_title.Size = new System.Drawing.Size(100, 28);
            this.lbl_LED_Brightness_title.TabIndex = 193;
            this.lbl_LED_Brightness_title.Text = "Brightness";
            this.lbl_LED_Brightness_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rbtn_LED_Level_Light
            // 
            this.rbtn_LED_Level_Light.Location = new System.Drawing.Point(330, 24);
            this.rbtn_LED_Level_Light.Name = "rbtn_LED_Level_Light";
            this.rbtn_LED_Level_Light.Size = new System.Drawing.Size(110, 20);
            this.rbtn_LED_Level_Light.TabIndex = 162;
            this.rbtn_LED_Level_Light.TabStop = true;
            this.rbtn_LED_Level_Light.Text = "明";
            this.rbtn_LED_Level_Light.UseVisualStyleBackColor = true;
            this.rbtn_LED_Level_Light.CheckedChanged += new System.EventHandler(this.rbtn_LED_Level_CheckedChanged);
            // 
            // rbtn_LED_Level_Dark
            // 
            this.rbtn_LED_Level_Dark.Location = new System.Drawing.Point(110, 24);
            this.rbtn_LED_Level_Dark.Name = "rbtn_LED_Level_Dark";
            this.rbtn_LED_Level_Dark.Size = new System.Drawing.Size(110, 20);
            this.rbtn_LED_Level_Dark.TabIndex = 160;
            this.rbtn_LED_Level_Dark.TabStop = true;
            this.rbtn_LED_Level_Dark.Text = "暗";
            this.rbtn_LED_Level_Dark.UseVisualStyleBackColor = true;
            this.rbtn_LED_Level_Dark.CheckedChanged += new System.EventHandler(this.rbtn_LED_Level_CheckedChanged);
            // 
            // rbtn_LED_Level_Normal
            // 
            this.rbtn_LED_Level_Normal.Location = new System.Drawing.Point(220, 24);
            this.rbtn_LED_Level_Normal.Name = "rbtn_LED_Level_Normal";
            this.rbtn_LED_Level_Normal.Size = new System.Drawing.Size(110, 20);
            this.rbtn_LED_Level_Normal.TabIndex = 161;
            this.rbtn_LED_Level_Normal.TabStop = true;
            this.rbtn_LED_Level_Normal.Text = "普通";
            this.rbtn_LED_Level_Normal.UseVisualStyleBackColor = true;
            this.rbtn_LED_Level_Normal.CheckedChanged += new System.EventHandler(this.rbtn_LED_Level_CheckedChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btn_color_select);
            this.groupBox6.Controls.Add(this.num_LED_Duty_B);
            this.groupBox6.Controls.Add(this.num_LED_Duty_G);
            this.groupBox6.Controls.Add(this.num_LED_Duty_R);
            this.groupBox6.Controls.Add(this.lbl_color_b);
            this.groupBox6.Controls.Add(this.lbl_color_g);
            this.groupBox6.Controls.Add(this.lbl_color_r);
            this.groupBox6.Controls.Add(this.trackBar_B);
            this.groupBox6.Controls.Add(this.trackBar_G);
            this.groupBox6.Controls.Add(this.trackBar_R);
            this.groupBox6.Location = new System.Drawing.Point(15, 20);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(455, 170);
            this.groupBox6.TabIndex = 150;
            this.groupBox6.TabStop = false;
            // 
            // btn_color_select
            // 
            this.btn_color_select.Location = new System.Drawing.Point(134, 20);
            this.btn_color_select.Name = "btn_color_select";
            this.btn_color_select.Size = new System.Drawing.Size(186, 26);
            this.btn_color_select.TabIndex = 193;
            this.btn_color_select.Text = "色の選択";
            this.btn_color_select.UseVisualStyleBackColor = true;
            this.btn_color_select.Click += new System.EventHandler(this.btn_color_select_Click);
            // 
            // num_LED_Duty_B
            // 
            this.num_LED_Duty_B.Location = new System.Drawing.Point(385, 125);
            this.num_LED_Duty_B.Name = "num_LED_Duty_B";
            this.num_LED_Duty_B.Size = new System.Drawing.Size(60, 23);
            this.num_LED_Duty_B.TabIndex = 164;
            this.num_LED_Duty_B.Tag = "2";
            this.num_LED_Duty_B.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.num_LED_Duty_B.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.num_LED_Duty_B.ValueChanged += new System.EventHandler(this.num_LED_Duty_ValueChanged);
            // 
            // num_LED_Duty_G
            // 
            this.num_LED_Duty_G.Location = new System.Drawing.Point(385, 90);
            this.num_LED_Duty_G.Name = "num_LED_Duty_G";
            this.num_LED_Duty_G.Size = new System.Drawing.Size(60, 23);
            this.num_LED_Duty_G.TabIndex = 163;
            this.num_LED_Duty_G.Tag = "1";
            this.num_LED_Duty_G.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.num_LED_Duty_G.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.num_LED_Duty_G.ValueChanged += new System.EventHandler(this.num_LED_Duty_ValueChanged);
            // 
            // num_LED_Duty_R
            // 
            this.num_LED_Duty_R.Location = new System.Drawing.Point(385, 55);
            this.num_LED_Duty_R.Name = "num_LED_Duty_R";
            this.num_LED_Duty_R.Size = new System.Drawing.Size(60, 23);
            this.num_LED_Duty_R.TabIndex = 157;
            this.num_LED_Duty_R.Tag = "0";
            this.num_LED_Duty_R.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.num_LED_Duty_R.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.num_LED_Duty_R.ValueChanged += new System.EventHandler(this.num_LED_Duty_ValueChanged);
            // 
            // lbl_color_b
            // 
            this.lbl_color_b.Location = new System.Drawing.Point(5, 122);
            this.lbl_color_b.Name = "lbl_color_b";
            this.lbl_color_b.Size = new System.Drawing.Size(50, 28);
            this.lbl_color_b.TabIndex = 155;
            this.lbl_color_b.Text = "B";
            this.lbl_color_b.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_color_g
            // 
            this.lbl_color_g.Location = new System.Drawing.Point(5, 87);
            this.lbl_color_g.Name = "lbl_color_g";
            this.lbl_color_g.Size = new System.Drawing.Size(50, 28);
            this.lbl_color_g.TabIndex = 153;
            this.lbl_color_g.Text = "G";
            this.lbl_color_g.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_color_r
            // 
            this.lbl_color_r.Location = new System.Drawing.Point(5, 52);
            this.lbl_color_r.Name = "lbl_color_r";
            this.lbl_color_r.Size = new System.Drawing.Size(50, 28);
            this.lbl_color_r.TabIndex = 151;
            this.lbl_color_r.Text = "R";
            this.lbl_color_r.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBar_B
            // 
            this.trackBar_B.Location = new System.Drawing.Point(60, 122);
            this.trackBar_B.Maximum = 60;
            this.trackBar_B.Name = "trackBar_B";
            this.trackBar_B.Size = new System.Drawing.Size(320, 45);
            this.trackBar_B.TabIndex = 156;
            this.trackBar_B.Tag = "2";
            this.trackBar_B.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
            // 
            // trackBar_G
            // 
            this.trackBar_G.Location = new System.Drawing.Point(60, 87);
            this.trackBar_G.Maximum = 60;
            this.trackBar_G.Name = "trackBar_G";
            this.trackBar_G.Size = new System.Drawing.Size(320, 45);
            this.trackBar_G.TabIndex = 154;
            this.trackBar_G.Tag = "1";
            this.trackBar_G.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
            // 
            // trackBar_R
            // 
            this.trackBar_R.Location = new System.Drawing.Point(60, 52);
            this.trackBar_R.Maximum = 60;
            this.trackBar_R.Name = "trackBar_R";
            this.trackBar_R.Size = new System.Drawing.Size(320, 45);
            this.trackBar_R.TabIndex = 152;
            this.trackBar_R.Tag = "0";
            this.trackBar_R.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
            // 
            // lbl_LED_COLOR_NOW
            // 
            this.lbl_LED_COLOR_NOW.BackColor = System.Drawing.Color.White;
            this.lbl_LED_COLOR_NOW.Location = new System.Drawing.Point(21, 19);
            this.lbl_LED_COLOR_NOW.Name = "lbl_LED_COLOR_NOW";
            this.lbl_LED_COLOR_NOW.Size = new System.Drawing.Size(50, 20);
            this.lbl_LED_COLOR_NOW.TabIndex = 101;
            this.lbl_LED_COLOR_NOW.Tag = "1";
            // 
            // lbl_LED_color_now_border
            // 
            this.lbl_LED_color_now_border.BackColor = System.Drawing.Color.DimGray;
            this.lbl_LED_color_now_border.Location = new System.Drawing.Point(20, 18);
            this.lbl_LED_color_now_border.Name = "lbl_LED_color_now_border";
            this.lbl_LED_color_now_border.Size = new System.Drawing.Size(52, 22);
            this.lbl_LED_color_now_border.TabIndex = 992;
            // 
            // btn_LED_preview
            // 
            this.btn_LED_preview.Location = new System.Drawing.Point(81, 17);
            this.btn_LED_preview.Name = "btn_LED_preview";
            this.btn_LED_preview.Size = new System.Drawing.Size(150, 25);
            this.btn_LED_preview.TabIndex = 102;
            this.btn_LED_preview.Text = "プレビュー";
            this.btn_LED_preview.UseVisualStyleBackColor = true;
            this.btn_LED_preview.Click += new System.EventHandler(this.btn_LED_preview_Click);
            // 
            // lbl_LED_COLOR_9
            // 
            this.lbl_LED_COLOR_9.BackColor = System.Drawing.Color.Black;
            this.lbl_LED_COLOR_9.ForeColor = System.Drawing.Color.White;
            this.lbl_LED_COLOR_9.Location = new System.Drawing.Point(301, 44);
            this.lbl_LED_COLOR_9.Name = "lbl_LED_COLOR_9";
            this.lbl_LED_COLOR_9.Size = new System.Drawing.Size(89, 25);
            this.lbl_LED_COLOR_9.TabIndex = 119;
            this.lbl_LED_COLOR_9.Tag = "0";
            this.lbl_LED_COLOR_9.Text = "OFF";
            this.lbl_LED_COLOR_9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_LED_COLOR_9.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_MouseClick);
            // 
            // lbl_LED_COLOR_8
            // 
            this.lbl_LED_COLOR_8.BackColor = System.Drawing.Color.Purple;
            this.lbl_LED_COLOR_8.Location = new System.Drawing.Point(266, 44);
            this.lbl_LED_COLOR_8.Name = "lbl_LED_COLOR_8";
            this.lbl_LED_COLOR_8.Size = new System.Drawing.Size(35, 25);
            this.lbl_LED_COLOR_8.TabIndex = 118;
            this.lbl_LED_COLOR_8.Tag = "8";
            this.lbl_LED_COLOR_8.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_MouseClick);
            // 
            // lbl_LED_COLOR_7
            // 
            this.lbl_LED_COLOR_7.BackColor = System.Drawing.Color.Blue;
            this.lbl_LED_COLOR_7.Location = new System.Drawing.Point(231, 44);
            this.lbl_LED_COLOR_7.Name = "lbl_LED_COLOR_7";
            this.lbl_LED_COLOR_7.Size = new System.Drawing.Size(35, 25);
            this.lbl_LED_COLOR_7.TabIndex = 117;
            this.lbl_LED_COLOR_7.Tag = "7";
            this.lbl_LED_COLOR_7.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_MouseClick);
            // 
            // lbl_LED_COLOR_6
            // 
            this.lbl_LED_COLOR_6.BackColor = System.Drawing.Color.Green;
            this.lbl_LED_COLOR_6.Location = new System.Drawing.Point(196, 44);
            this.lbl_LED_COLOR_6.Name = "lbl_LED_COLOR_6";
            this.lbl_LED_COLOR_6.Size = new System.Drawing.Size(35, 25);
            this.lbl_LED_COLOR_6.TabIndex = 116;
            this.lbl_LED_COLOR_6.Tag = "6";
            this.lbl_LED_COLOR_6.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_MouseClick);
            // 
            // lbl_LED_COLOR_5
            // 
            this.lbl_LED_COLOR_5.BackColor = System.Drawing.Color.Turquoise;
            this.lbl_LED_COLOR_5.Location = new System.Drawing.Point(161, 44);
            this.lbl_LED_COLOR_5.Name = "lbl_LED_COLOR_5";
            this.lbl_LED_COLOR_5.Size = new System.Drawing.Size(35, 25);
            this.lbl_LED_COLOR_5.TabIndex = 115;
            this.lbl_LED_COLOR_5.Tag = "5";
            this.lbl_LED_COLOR_5.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_MouseClick);
            // 
            // lbl_LED_COLOR_4
            // 
            this.lbl_LED_COLOR_4.BackColor = System.Drawing.Color.Yellow;
            this.lbl_LED_COLOR_4.Location = new System.Drawing.Point(126, 44);
            this.lbl_LED_COLOR_4.Name = "lbl_LED_COLOR_4";
            this.lbl_LED_COLOR_4.Size = new System.Drawing.Size(35, 25);
            this.lbl_LED_COLOR_4.TabIndex = 114;
            this.lbl_LED_COLOR_4.Tag = "4";
            this.lbl_LED_COLOR_4.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_MouseClick);
            // 
            // lbl_LED_COLOR_3
            // 
            this.lbl_LED_COLOR_3.BackColor = System.Drawing.Color.Orange;
            this.lbl_LED_COLOR_3.Location = new System.Drawing.Point(91, 44);
            this.lbl_LED_COLOR_3.Name = "lbl_LED_COLOR_3";
            this.lbl_LED_COLOR_3.Size = new System.Drawing.Size(35, 25);
            this.lbl_LED_COLOR_3.TabIndex = 113;
            this.lbl_LED_COLOR_3.Tag = "3";
            this.lbl_LED_COLOR_3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_MouseClick);
            // 
            // lbl_LED_COLOR_2
            // 
            this.lbl_LED_COLOR_2.BackColor = System.Drawing.Color.Red;
            this.lbl_LED_COLOR_2.Location = new System.Drawing.Point(56, 44);
            this.lbl_LED_COLOR_2.Name = "lbl_LED_COLOR_2";
            this.lbl_LED_COLOR_2.Size = new System.Drawing.Size(35, 25);
            this.lbl_LED_COLOR_2.TabIndex = 112;
            this.lbl_LED_COLOR_2.Tag = "2";
            this.lbl_LED_COLOR_2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_MouseClick);
            // 
            // lbl_LED_COLOR_1
            // 
            this.lbl_LED_COLOR_1.BackColor = System.Drawing.Color.White;
            this.lbl_LED_COLOR_1.Location = new System.Drawing.Point(21, 44);
            this.lbl_LED_COLOR_1.Name = "lbl_LED_COLOR_1";
            this.lbl_LED_COLOR_1.Size = new System.Drawing.Size(35, 25);
            this.lbl_LED_COLOR_1.TabIndex = 111;
            this.lbl_LED_COLOR_1.Tag = "1";
            this.lbl_LED_COLOR_1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbl_LED_COLOR_MouseClick);
            // 
            // lbl_LED_color_border
            // 
            this.lbl_LED_color_border.BackColor = System.Drawing.Color.DimGray;
            this.lbl_LED_color_border.Location = new System.Drawing.Point(20, 43);
            this.lbl_LED_color_border.Name = "lbl_LED_color_border";
            this.lbl_LED_color_border.Size = new System.Drawing.Size(371, 27);
            this.lbl_LED_color_border.TabIndex = 110;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_LED_COLOR_NOW);
            this.groupBox1.Controls.Add(this.lbl_LED_COLOR_1);
            this.groupBox1.Controls.Add(this.lbl_LED_COLOR_2);
            this.groupBox1.Controls.Add(this.lbl_LED_color_now_border);
            this.groupBox1.Controls.Add(this.lbl_LED_COLOR_3);
            this.groupBox1.Controls.Add(this.btn_LED_preview);
            this.groupBox1.Controls.Add(this.lbl_LED_COLOR_4);
            this.groupBox1.Controls.Add(this.lbl_LED_COLOR_9);
            this.groupBox1.Controls.Add(this.lbl_LED_COLOR_5);
            this.groupBox1.Controls.Add(this.lbl_LED_COLOR_6);
            this.groupBox1.Controls.Add(this.lbl_LED_COLOR_8);
            this.groupBox1.Controls.Add(this.lbl_LED_COLOR_7);
            this.groupBox1.Controls.Add(this.lbl_LED_color_border);
            this.groupBox1.Location = new System.Drawing.Point(12, 336);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(398, 100);
            this.groupBox1.TabIndex = 192;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            this.groupBox1.Visible = false;
            // 
            // gbx_led_color_select
            // 
            this.gbx_led_color_select.Controls.Add(this.lbl_led_color_select_msg);
            this.gbx_led_color_select.Controls.Add(this.lbl_led_color_select_position);
            this.gbx_led_color_select.Controls.Add(this.pic_led_color_select);
            this.gbx_led_color_select.Location = new System.Drawing.Point(515, 15);
            this.gbx_led_color_select.Name = "gbx_led_color_select";
            this.gbx_led_color_select.Size = new System.Drawing.Size(290, 320);
            this.gbx_led_color_select.TabIndex = 903;
            this.gbx_led_color_select.TabStop = false;
            this.gbx_led_color_select.Text = "色選択";
            // 
            // lbl_led_color_select_msg
            // 
            this.lbl_led_color_select_msg.BackColor = System.Drawing.Color.Transparent;
            this.lbl_led_color_select_msg.ForeColor = System.Drawing.Color.Red;
            this.lbl_led_color_select_msg.Location = new System.Drawing.Point(50, 10);
            this.lbl_led_color_select_msg.Name = "lbl_led_color_select_msg";
            this.lbl_led_color_select_msg.Size = new System.Drawing.Size(230, 16);
            this.lbl_led_color_select_msg.TabIndex = 903;
            // 
            // lbl_led_color_select_position
            // 
            this.lbl_led_color_select_position.BackColor = System.Drawing.Color.White;
            this.lbl_led_color_select_position.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_led_color_select_position.Location = new System.Drawing.Point(20, 19);
            this.lbl_led_color_select_position.Name = "lbl_led_color_select_position";
            this.lbl_led_color_select_position.Size = new System.Drawing.Size(3, 3);
            this.lbl_led_color_select_position.TabIndex = 903;
            this.lbl_led_color_select_position.Visible = false;
            // 
            // pic_led_color_select
            // 
            this.pic_led_color_select.Image = global::RevOmate.Properties.Resources.LED_COLOR1;
            this.pic_led_color_select.Location = new System.Drawing.Point(10, 25);
            this.pic_led_color_select.Name = "pic_led_color_select";
            this.pic_led_color_select.Size = new System.Drawing.Size(269, 286);
            this.pic_led_color_select.TabIndex = 901;
            this.pic_led_color_select.TabStop = false;
            this.pic_led_color_select.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pic_led_color_select_MouseClick);
            // 
            // LEDColorSetting
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btn_cancel;
            this.ClientSize = new System.Drawing.Size(816, 346);
            this.ControlBox = false;
            this.Controls.Add(this.gbx_led_color_select);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_submit);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbx_LED_set);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LEDColorSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "LEDColorSetting";
            this.Load += new System.EventHandler(this.LEDColorSetting_Load);
            this.gbx_LED_set.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_LED_Duty_B)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_LED_Duty_G)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_LED_Duty_R)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_B)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_G)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_R)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.gbx_led_color_select.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_led_color_select)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_submit;
        private System.Windows.Forms.GroupBox gbx_LED_set;
        private System.Windows.Forms.RadioButton rbtn_LED_Level_Light;
        private System.Windows.Forms.RadioButton rbtn_LED_Level_Normal;
        private System.Windows.Forms.RadioButton rbtn_LED_Level_Dark;
        private System.Windows.Forms.Label lbl_LED_COLOR_NOW;
        private System.Windows.Forms.Label lbl_LED_color_now_border;
        private System.Windows.Forms.Button btn_LED_preview;
        private System.Windows.Forms.Label lbl_LED_COLOR_9;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TrackBar trackBar_B;
        private System.Windows.Forms.TrackBar trackBar_G;
        private System.Windows.Forms.TrackBar trackBar_R;
        private System.Windows.Forms.Label lbl_LED_COLOR_8;
        private System.Windows.Forms.Label lbl_LED_COLOR_7;
        private System.Windows.Forms.Label lbl_LED_COLOR_6;
        private System.Windows.Forms.Label lbl_LED_COLOR_5;
        private System.Windows.Forms.Label lbl_LED_COLOR_4;
        private System.Windows.Forms.Label lbl_LED_COLOR_3;
        private System.Windows.Forms.Label lbl_LED_COLOR_2;
        private System.Windows.Forms.Label lbl_LED_COLOR_1;
        private System.Windows.Forms.Label lbl_LED_color_border;
        private System.Windows.Forms.Label lbl_color_b;
        private System.Windows.Forms.Label lbl_color_g;
        private System.Windows.Forms.Label lbl_color_r;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown num_LED_Duty_R;
        private System.Windows.Forms.NumericUpDown num_LED_Duty_B;
        private System.Windows.Forms.NumericUpDown num_LED_Duty_G;
        private System.Windows.Forms.Label lbl_LED_Brightness_title;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_color_select;
        private System.Windows.Forms.GroupBox gbx_led_color_select;
        private System.Windows.Forms.Label lbl_led_color_select_msg;
        private System.Windows.Forms.Label lbl_led_color_select_position;
        private System.Windows.Forms.PictureBox pic_led_color_select;
    }
}