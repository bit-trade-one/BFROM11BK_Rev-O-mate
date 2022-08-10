namespace RevOmate
{
    partial class FuncSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FuncSetting));
            this.lbx_category = new System.Windows.Forms.ListBox();
            this.lbx_func_list = new System.Windows.Forms.ListBox();
            this.btn_submit = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.lbl_title_func = new System.Windows.Forms.Label();
            this.lbl_title_category = new System.Windows.Forms.Label();
            this.lbl_debug01 = new System.Windows.Forms.Label();
            this.lbl_title_category2 = new System.Windows.Forms.Label();
            this.lbx_category2 = new System.Windows.Forms.ListBox();
            this.lbl_default_func_title = new System.Windows.Forms.Label();
            this.cmbbx_encoder_default_func = new System.Windows.Forms.ComboBox();
            this.lbl_setting_value_title = new System.Windows.Forms.Label();
            this.lbl_setting_value = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gbx_encoder_default_func = new System.Windows.Forms.GroupBox();
            this.llbl_help = new System.Windows.Forms.LinkLabel();
            this.pnl_func_setting = new System.Windows.Forms.Panel();
            this.btn_sw_func_key_clr = new System.Windows.Forms.Button();
            this.txtbx_sw_func_key = new System.Windows.Forms.TextBox();
            this.chk_sw_func_win = new System.Windows.Forms.CheckBox();
            this.chk_sw_func_alt = new System.Windows.Forms.CheckBox();
            this.chk_sw_func_shift = new System.Windows.Forms.CheckBox();
            this.chk_sw_func_ctrl = new System.Windows.Forms.CheckBox();
            this.lbl_sw_func_title = new System.Windows.Forms.Label();
            this.num_sw_func_x = new System.Windows.Forms.NumericUpDown();
            this.num_sw_func_y = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            this.gbx_encoder_default_func.SuspendLayout();
            this.pnl_func_setting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_sw_func_x)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_sw_func_y)).BeginInit();
            this.SuspendLayout();
            // 
            // lbx_category
            // 
            this.lbx_category.FormattingEnabled = true;
            this.lbx_category.ItemHeight = 15;
            this.lbx_category.Location = new System.Drawing.Point(20, 115);
            this.lbx_category.Name = "lbx_category";
            this.lbx_category.Size = new System.Drawing.Size(180, 334);
            this.lbx_category.TabIndex = 30;
            this.lbx_category.SelectedIndexChanged += new System.EventHandler(this.lbx_category_SelectedIndexChanged);
            // 
            // lbx_func_list
            // 
            this.lbx_func_list.FormattingEnabled = true;
            this.lbx_func_list.ItemHeight = 15;
            this.lbx_func_list.Location = new System.Drawing.Point(390, 115);
            this.lbx_func_list.Name = "lbx_func_list";
            this.lbx_func_list.Size = new System.Drawing.Size(250, 334);
            this.lbx_func_list.TabIndex = 32;
            this.lbx_func_list.SelectedIndexChanged += new System.EventHandler(this.lbx_func_list_SelectedIndexChanged);
            // 
            // btn_submit
            // 
            this.btn_submit.Location = new System.Drawing.Point(790, 480);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(150, 34);
            this.btn_submit.TabIndex = 100;
            this.btn_submit.Text = "Submit";
            this.btn_submit.UseVisualStyleBackColor = true;
            this.btn_submit.Click += new System.EventHandler(this.btn_submit_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.Location = new System.Drawing.Point(950, 480);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(150, 34);
            this.btn_cancel.TabIndex = 101;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // lbl_title_func
            // 
            this.lbl_title_func.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_title_func.Location = new System.Drawing.Point(390, 85);
            this.lbl_title_func.Name = "lbl_title_func";
            this.lbl_title_func.Size = new System.Drawing.Size(575, 25);
            this.lbl_title_func.TabIndex = 23;
            this.lbl_title_func.Text = "func list";
            this.lbl_title_func.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lbl_title_category
            // 
            this.lbl_title_category.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_title_category.Location = new System.Drawing.Point(20, 85);
            this.lbl_title_category.Name = "lbl_title_category";
            this.lbl_title_category.Size = new System.Drawing.Size(250, 25);
            this.lbl_title_category.TabIndex = 21;
            this.lbl_title_category.Text = "category";
            this.lbl_title_category.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lbl_debug01
            // 
            this.lbl_debug01.AutoSize = true;
            this.lbl_debug01.Location = new System.Drawing.Point(255, 19);
            this.lbl_debug01.Name = "lbl_debug01";
            this.lbl_debug01.Size = new System.Drawing.Size(41, 15);
            this.lbl_debug01.TabIndex = 999;
            this.lbl_debug01.Text = "label1";
            // 
            // lbl_title_category2
            // 
            this.lbl_title_category2.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_title_category2.Location = new System.Drawing.Point(205, 85);
            this.lbl_title_category2.Name = "lbl_title_category2";
            this.lbl_title_category2.Size = new System.Drawing.Size(250, 25);
            this.lbl_title_category2.TabIndex = 22;
            this.lbl_title_category2.Text = "app";
            this.lbl_title_category2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lbx_category2
            // 
            this.lbx_category2.FormattingEnabled = true;
            this.lbx_category2.ItemHeight = 15;
            this.lbx_category2.Location = new System.Drawing.Point(205, 115);
            this.lbx_category2.Name = "lbx_category2";
            this.lbx_category2.Size = new System.Drawing.Size(180, 334);
            this.lbx_category2.TabIndex = 31;
            this.lbx_category2.SelectedIndexChanged += new System.EventHandler(this.lbx_category2_list_SelectedIndexChanged);
            // 
            // lbl_default_func_title
            // 
            this.lbl_default_func_title.Location = new System.Drawing.Point(15, 15);
            this.lbl_default_func_title.Name = "lbl_default_func_title";
            this.lbl_default_func_title.Size = new System.Drawing.Size(400, 25);
            this.lbl_default_func_title.TabIndex = 40;
            this.lbl_default_func_title.Text = "デフォルト機能";
            this.lbl_default_func_title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_default_func_title.Visible = false;
            // 
            // cmbbx_encoder_default_func
            // 
            this.cmbbx_encoder_default_func.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbx_encoder_default_func.FormattingEnabled = true;
            this.cmbbx_encoder_default_func.Location = new System.Drawing.Point(15, 40);
            this.cmbbx_encoder_default_func.Name = "cmbbx_encoder_default_func";
            this.cmbbx_encoder_default_func.Size = new System.Drawing.Size(400, 23);
            this.cmbbx_encoder_default_func.TabIndex = 41;
            this.cmbbx_encoder_default_func.Visible = false;
            this.cmbbx_encoder_default_func.SelectedIndexChanged += new System.EventHandler(this.cmbbx_encoder_default_func_SelectedIndexChanged);
            // 
            // lbl_setting_value_title
            // 
            this.lbl_setting_value_title.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_setting_value_title.Location = new System.Drawing.Point(10, 15);
            this.lbl_setting_value_title.Name = "lbl_setting_value_title";
            this.lbl_setting_value_title.Size = new System.Drawing.Size(495, 25);
            this.lbl_setting_value_title.TabIndex = 11;
            this.lbl_setting_value_title.Text = "set";
            // 
            // lbl_setting_value
            // 
            this.lbl_setting_value.Font = new System.Drawing.Font("Meiryo UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_setting_value.Location = new System.Drawing.Point(10, 45);
            this.lbl_setting_value.Name = "lbl_setting_value";
            this.lbl_setting_value.Size = new System.Drawing.Size(1069, 25);
            this.lbl_setting_value.TabIndex = 12;
            this.lbl_setting_value.Text = "set";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_setting_value_title);
            this.groupBox1.Controls.Add(this.lbl_setting_value);
            this.groupBox1.Controls.Add(this.lbl_debug01);
            this.groupBox1.Location = new System.Drawing.Point(20, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1085, 75);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // gbx_encoder_default_func
            // 
            this.gbx_encoder_default_func.Controls.Add(this.lbl_default_func_title);
            this.gbx_encoder_default_func.Controls.Add(this.cmbbx_encoder_default_func);
            this.gbx_encoder_default_func.Location = new System.Drawing.Point(20, 450);
            this.gbx_encoder_default_func.Name = "gbx_encoder_default_func";
            this.gbx_encoder_default_func.Size = new System.Drawing.Size(430, 70);
            this.gbx_encoder_default_func.TabIndex = 102;
            this.gbx_encoder_default_func.TabStop = false;
            this.gbx_encoder_default_func.Visible = false;
            // 
            // llbl_help
            // 
            this.llbl_help.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.llbl_help.Location = new System.Drawing.Point(730, 480);
            this.llbl_help.Name = "llbl_help";
            this.llbl_help.Size = new System.Drawing.Size(50, 34);
            this.llbl_help.TabIndex = 103;
            this.llbl_help.TabStop = true;
            this.llbl_help.Text = "help";
            this.llbl_help.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.llbl_help.MouseClick += new System.Windows.Forms.MouseEventHandler(this.llbl_help_MouseClick);
            // 
            // pnl_func_setting
            // 
            this.pnl_func_setting.BackColor = System.Drawing.Color.White;
            this.pnl_func_setting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_func_setting.Controls.Add(this.btn_sw_func_key_clr);
            this.pnl_func_setting.Controls.Add(this.txtbx_sw_func_key);
            this.pnl_func_setting.Controls.Add(this.chk_sw_func_win);
            this.pnl_func_setting.Controls.Add(this.chk_sw_func_alt);
            this.pnl_func_setting.Controls.Add(this.chk_sw_func_shift);
            this.pnl_func_setting.Controls.Add(this.chk_sw_func_ctrl);
            this.pnl_func_setting.Controls.Add(this.lbl_sw_func_title);
            this.pnl_func_setting.Controls.Add(this.num_sw_func_x);
            this.pnl_func_setting.Controls.Add(this.num_sw_func_y);
            this.pnl_func_setting.Location = new System.Drawing.Point(650, 115);
            this.pnl_func_setting.Name = "pnl_func_setting";
            this.pnl_func_setting.Size = new System.Drawing.Size(450, 334);
            this.pnl_func_setting.TabIndex = 105;
            this.pnl_func_setting.Tag = "0";
            // 
            // btn_sw_func_key_clr
            // 
            this.btn_sw_func_key_clr.Location = new System.Drawing.Point(250, 14);
            this.btn_sw_func_key_clr.Name = "btn_sw_func_key_clr";
            this.btn_sw_func_key_clr.Size = new System.Drawing.Size(120, 25);
            this.btn_sw_func_key_clr.TabIndex = 114;
            this.btn_sw_func_key_clr.Tag = "0";
            this.btn_sw_func_key_clr.Text = "clr";
            this.btn_sw_func_key_clr.UseVisualStyleBackColor = true;
            this.btn_sw_func_key_clr.Click += new System.EventHandler(this.btn_sw_func_key_clr_Click);
            // 
            // txtbx_sw_func_key
            // 
            this.txtbx_sw_func_key.ForeColor = System.Drawing.Color.Gray;
            this.txtbx_sw_func_key.Location = new System.Drawing.Point(20, 15);
            this.txtbx_sw_func_key.Name = "txtbx_sw_func_key";
            this.txtbx_sw_func_key.Size = new System.Drawing.Size(230, 23);
            this.txtbx_sw_func_key.TabIndex = 113;
            this.txtbx_sw_func_key.Tag = "0";
            this.txtbx_sw_func_key.Text = "key input";
            this.txtbx_sw_func_key.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtbx_sw_func_key_KeyDown);
            this.txtbx_sw_func_key.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtbx_sw_func_key_KeyUp);
            this.txtbx_sw_func_key.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtbx_sw_func_key_PreviewKeyDown);
            // 
            // chk_sw_func_win
            // 
            this.chk_sw_func_win.Location = new System.Drawing.Point(320, 45);
            this.chk_sw_func_win.Name = "chk_sw_func_win";
            this.chk_sw_func_win.Size = new System.Drawing.Size(100, 20);
            this.chk_sw_func_win.TabIndex = 118;
            this.chk_sw_func_win.Text = "WIN";
            this.chk_sw_func_win.UseVisualStyleBackColor = true;
            // 
            // chk_sw_func_alt
            // 
            this.chk_sw_func_alt.Location = new System.Drawing.Point(220, 45);
            this.chk_sw_func_alt.Name = "chk_sw_func_alt";
            this.chk_sw_func_alt.Size = new System.Drawing.Size(100, 20);
            this.chk_sw_func_alt.TabIndex = 117;
            this.chk_sw_func_alt.Text = "ALT";
            this.chk_sw_func_alt.UseVisualStyleBackColor = true;
            // 
            // chk_sw_func_shift
            // 
            this.chk_sw_func_shift.Location = new System.Drawing.Point(120, 45);
            this.chk_sw_func_shift.Name = "chk_sw_func_shift";
            this.chk_sw_func_shift.Size = new System.Drawing.Size(100, 20);
            this.chk_sw_func_shift.TabIndex = 116;
            this.chk_sw_func_shift.Text = "SHIFT";
            this.chk_sw_func_shift.UseVisualStyleBackColor = true;
            // 
            // chk_sw_func_ctrl
            // 
            this.chk_sw_func_ctrl.Location = new System.Drawing.Point(20, 45);
            this.chk_sw_func_ctrl.Name = "chk_sw_func_ctrl";
            this.chk_sw_func_ctrl.Size = new System.Drawing.Size(100, 20);
            this.chk_sw_func_ctrl.TabIndex = 115;
            this.chk_sw_func_ctrl.Text = "CTRL";
            this.chk_sw_func_ctrl.UseVisualStyleBackColor = true;
            // 
            // lbl_sw_func_title
            // 
            this.lbl_sw_func_title.Location = new System.Drawing.Point(20, 18);
            this.lbl_sw_func_title.Name = "lbl_sw_func_title";
            this.lbl_sw_func_title.Size = new System.Drawing.Size(225, 20);
            this.lbl_sw_func_title.TabIndex = 130;
            this.lbl_sw_func_title.Text = "X-Y vector";
            // 
            // num_sw_func_x
            // 
            this.num_sw_func_x.Location = new System.Drawing.Point(250, 15);
            this.num_sw_func_x.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.num_sw_func_x.Minimum = new decimal(new int[] {
            127,
            0,
            0,
            -2147483648});
            this.num_sw_func_x.Name = "num_sw_func_x";
            this.num_sw_func_x.Size = new System.Drawing.Size(90, 23);
            this.num_sw_func_x.TabIndex = 131;
            this.num_sw_func_x.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.num_sw_func_x.Value = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            // 
            // num_sw_func_y
            // 
            this.num_sw_func_y.Location = new System.Drawing.Point(350, 15);
            this.num_sw_func_y.Name = "num_sw_func_y";
            this.num_sw_func_y.Size = new System.Drawing.Size(90, 23);
            this.num_sw_func_y.TabIndex = 132;
            this.num_sw_func_y.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // FuncSetting
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1124, 526);
            this.Controls.Add(this.pnl_func_setting);
            this.Controls.Add(this.llbl_help);
            this.Controls.Add(this.gbx_encoder_default_func);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbl_title_category2);
            this.Controls.Add(this.lbx_category2);
            this.Controls.Add(this.lbl_title_category);
            this.Controls.Add(this.lbl_title_func);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_submit);
            this.Controls.Add(this.lbx_func_list);
            this.Controls.Add(this.lbx_category);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FuncSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FuncSetting";
            this.Load += new System.EventHandler(this.FuncSetting_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbx_encoder_default_func.ResumeLayout(false);
            this.pnl_func_setting.ResumeLayout(false);
            this.pnl_func_setting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_sw_func_x)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_sw_func_y)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbx_category;
        private System.Windows.Forms.ListBox lbx_func_list;
        private System.Windows.Forms.Button btn_submit;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Label lbl_title_func;
        private System.Windows.Forms.Label lbl_title_category;
        private System.Windows.Forms.Label lbl_debug01;
        private System.Windows.Forms.Label lbl_title_category2;
        private System.Windows.Forms.ListBox lbx_category2;
        private System.Windows.Forms.Label lbl_default_func_title;
        private System.Windows.Forms.ComboBox cmbbx_encoder_default_func;
        private System.Windows.Forms.Label lbl_setting_value_title;
        private System.Windows.Forms.Label lbl_setting_value;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gbx_encoder_default_func;
        private System.Windows.Forms.LinkLabel llbl_help;
        private System.Windows.Forms.Panel pnl_func_setting;
        private System.Windows.Forms.Button btn_sw_func_key_clr;
        private System.Windows.Forms.TextBox txtbx_sw_func_key;
        private System.Windows.Forms.CheckBox chk_sw_func_win;
        private System.Windows.Forms.CheckBox chk_sw_func_alt;
        private System.Windows.Forms.CheckBox chk_sw_func_shift;
        private System.Windows.Forms.CheckBox chk_sw_func_ctrl;
        private System.Windows.Forms.Label lbl_sw_func_title;
        private System.Windows.Forms.NumericUpDown num_sw_func_x;
        private System.Windows.Forms.NumericUpDown num_sw_func_y;
    }
}