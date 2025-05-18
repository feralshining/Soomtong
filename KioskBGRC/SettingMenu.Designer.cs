
namespace KioskBGRC
{
    partial class SettingMenu
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_title = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_resrc = new FontAwesome.Sharp.IconButton();
            this.txt_resource_path = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt_programPath = new System.Windows.Forms.TextBox();
            this.btn_program = new FontAwesome.Sharp.IconButton();
            this.btn_network = new FontAwesome.Sharp.IconButton();
            this.btn_debug = new FontAwesome.Sharp.IconButton();
            this.btn_exit = new FontAwesome.Sharp.IconButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txt_listfile_path = new System.Windows.Forms.TextBox();
            this.btn_file = new FontAwesome.Sharp.IconButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.timePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label_chk_2 = new System.Windows.Forms.Label();
            this.label_chk_1 = new System.Windows.Forms.Label();
            this.timePicker1 = new System.Windows.Forms.DateTimePicker();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txt_dailylogPath = new System.Windows.Forms.TextBox();
            this.btn_savedailydir = new FontAwesome.Sharp.IconButton();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.serialportCB = new System.Windows.Forms.ComboBox();
            this.label_qr_1 = new System.Windows.Forms.Label();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.txt_bannerMain = new System.Windows.Forms.TextBox();
            this.btn_bannerMain = new FontAwesome.Sharp.IconButton();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(73)))), ((int)(((byte)(252)))));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.label_title);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1080, 66);
            this.panel1.TabIndex = 1;
            this.panel1.DoubleClick += new System.EventHandler(this.panel1_DoubleClick);
            // 
            // label_title
            // 
            this.label_title.AutoSize = true;
            this.label_title.Font = new System.Drawing.Font("여기어때 잘난체 2 TTF", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_title.ForeColor = System.Drawing.Color.White;
            this.label_title.Location = new System.Drawing.Point(501, 16);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(94, 44);
            this.label_title.TabIndex = 2;
            this.label_title.Text = "설정";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_resrc);
            this.groupBox1.Controls.Add(this.txt_resource_path);
            this.groupBox1.Font = new System.Drawing.Font("여기어때 잘난체 2 TTF", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox1.Location = new System.Drawing.Point(10, 22);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(397, 99);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "리소스 폴더 설정 (설정 후 재시작 필수)";
            // 
            // btn_resrc
            // 
            this.btn_resrc.Font = new System.Drawing.Font("여기어때 잘난체 2 TTF", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_resrc.IconChar = FontAwesome.Sharp.IconChar.Sitemap;
            this.btn_resrc.IconColor = System.Drawing.Color.Black;
            this.btn_resrc.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btn_resrc.IconSize = 30;
            this.btn_resrc.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_resrc.Location = new System.Drawing.Point(6, 47);
            this.btn_resrc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_resrc.Name = "btn_resrc";
            this.btn_resrc.Size = new System.Drawing.Size(384, 46);
            this.btn_resrc.TabIndex = 9;
            this.btn_resrc.Text = "리소스 설정";
            this.btn_resrc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_resrc.UseVisualStyleBackColor = true;
            this.btn_resrc.Click += new System.EventHandler(this.btn_resrc_Click);
            // 
            // txt_resource_path
            // 
            this.txt_resource_path.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(227)))), ((int)(((byte)(255)))));
            this.txt_resource_path.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_resource_path.Font = new System.Drawing.Font("여기어때 잘난체 2 TTF", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_resource_path.Location = new System.Drawing.Point(6, 24);
            this.txt_resource_path.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_resource_path.Multiline = true;
            this.txt_resource_path.Name = "txt_resource_path";
            this.txt_resource_path.ReadOnly = true;
            this.txt_resource_path.Size = new System.Drawing.Size(384, 19);
            this.txt_resource_path.TabIndex = 8;
            this.txt_resource_path.Text = "설정 필요";
            this.txt_resource_path.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txt_programPath);
            this.groupBox2.Controls.Add(this.btn_program);
            this.groupBox2.Font = new System.Drawing.Font("여기어때 잘난체 2 TTF", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox2.Location = new System.Drawing.Point(10, 238);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(397, 99);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "뉴스레터 폴더 설정 (설정 후 재시작 필수)";
            // 
            // txt_programPath
            // 
            this.txt_programPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(227)))), ((int)(((byte)(255)))));
            this.txt_programPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_programPath.Font = new System.Drawing.Font("여기어때 잘난체 2 TTF", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_programPath.Location = new System.Drawing.Point(6, 24);
            this.txt_programPath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_programPath.Multiline = true;
            this.txt_programPath.Name = "txt_programPath";
            this.txt_programPath.ReadOnly = true;
            this.txt_programPath.Size = new System.Drawing.Size(384, 19);
            this.txt_programPath.TabIndex = 7;
            this.txt_programPath.Text = "설정 필요";
            this.txt_programPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btn_program
            // 
            this.btn_program.Font = new System.Drawing.Font("여기어때 잘난체 2 TTF", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_program.IconChar = FontAwesome.Sharp.IconChar.FolderBlank;
            this.btn_program.IconColor = System.Drawing.Color.Black;
            this.btn_program.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btn_program.IconSize = 30;
            this.btn_program.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_program.Location = new System.Drawing.Point(6, 47);
            this.btn_program.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_program.Name = "btn_program";
            this.btn_program.Size = new System.Drawing.Size(384, 48);
            this.btn_program.TabIndex = 2;
            this.btn_program.Text = "폴더 경로 설정";
            this.btn_program.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_program.UseVisualStyleBackColor = true;
            this.btn_program.Click += new System.EventHandler(this.btn_program_Click);
            // 
            // btn_network
            // 
            this.btn_network.Font = new System.Drawing.Font("여기어때 잘난체 2 TTF", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_network.IconChar = FontAwesome.Sharp.IconChar.WifiStrong;
            this.btn_network.IconColor = System.Drawing.Color.Black;
            this.btn_network.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btn_network.IconSize = 30;
            this.btn_network.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_network.Location = new System.Drawing.Point(142, 17);
            this.btn_network.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_network.Name = "btn_network";
            this.btn_network.Size = new System.Drawing.Size(128, 74);
            this.btn_network.TabIndex = 15;
            this.btn_network.Text = "인터넷\r\n 상태 : None";
            this.btn_network.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_network.UseVisualStyleBackColor = true;
            this.btn_network.Click += new System.EventHandler(this.network_btn_Click);
            // 
            // btn_debug
            // 
            this.btn_debug.Font = new System.Drawing.Font("여기어때 잘난체 2 TTF", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_debug.IconChar = FontAwesome.Sharp.IconChar.FileText;
            this.btn_debug.IconColor = System.Drawing.Color.Black;
            this.btn_debug.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btn_debug.IconSize = 30;
            this.btn_debug.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_debug.Location = new System.Drawing.Point(6, 17);
            this.btn_debug.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_debug.Name = "btn_debug";
            this.btn_debug.Size = new System.Drawing.Size(128, 74);
            this.btn_debug.TabIndex = 14;
            this.btn_debug.Text = "로그\r\n확인";
            this.btn_debug.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_debug.UseVisualStyleBackColor = true;
            this.btn_debug.Click += new System.EventHandler(this.btn_debug_Click);
            // 
            // btn_exit
            // 
            this.btn_exit.Font = new System.Drawing.Font("여기어때 잘난체 2 TTF", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_exit.IconChar = FontAwesome.Sharp.IconChar.SignOutAlt;
            this.btn_exit.IconColor = System.Drawing.Color.Black;
            this.btn_exit.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btn_exit.IconSize = 30;
            this.btn_exit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_exit.Location = new System.Drawing.Point(279, 16);
            this.btn_exit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(128, 74);
            this.btn_exit.TabIndex = 13;
            this.btn_exit.Text = "앱 종료";
            this.btn_exit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_exit.UseVisualStyleBackColor = true;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txt_listfile_path);
            this.groupBox4.Controls.Add(this.btn_file);
            this.groupBox4.Font = new System.Drawing.Font("여기어때 잘난체 2 TTF", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox4.Location = new System.Drawing.Point(10, 25);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Size = new System.Drawing.Size(397, 99);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "명단 경로 설정 (설정 후 재시작 필수)";
            // 
            // txt_listfile_path
            // 
            this.txt_listfile_path.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(227)))), ((int)(((byte)(255)))));
            this.txt_listfile_path.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_listfile_path.Font = new System.Drawing.Font("여기어때 잘난체 2 TTF", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_listfile_path.Location = new System.Drawing.Point(6, 24);
            this.txt_listfile_path.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_listfile_path.Multiline = true;
            this.txt_listfile_path.Name = "txt_listfile_path";
            this.txt_listfile_path.ReadOnly = true;
            this.txt_listfile_path.Size = new System.Drawing.Size(384, 19);
            this.txt_listfile_path.TabIndex = 7;
            this.txt_listfile_path.Text = "설정 필요";
            this.txt_listfile_path.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btn_file
            // 
            this.btn_file.Font = new System.Drawing.Font("여기어때 잘난체 2 TTF", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_file.IconChar = FontAwesome.Sharp.IconChar.File;
            this.btn_file.IconColor = System.Drawing.Color.Black;
            this.btn_file.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btn_file.IconSize = 30;
            this.btn_file.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_file.Location = new System.Drawing.Point(6, 47);
            this.btn_file.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_file.Name = "btn_file";
            this.btn_file.Size = new System.Drawing.Size(384, 46);
            this.btn_file.TabIndex = 2;
            this.btn_file.Text = "명단 경로 설정";
            this.btn_file.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_file.UseVisualStyleBackColor = true;
            this.btn_file.Click += new System.EventHandler(this.btn_file_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.timePicker2);
            this.groupBox5.Controls.Add(this.label_chk_2);
            this.groupBox5.Controls.Add(this.label_chk_1);
            this.groupBox5.Controls.Add(this.timePicker1);
            this.groupBox5.Font = new System.Drawing.Font("여기어때 잘난체 2 TTF", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox5.Location = new System.Drawing.Point(10, 132);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox5.Size = new System.Drawing.Size(397, 99);
            this.groupBox5.TabIndex = 11;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "체크 시간 설정";
            // 
            // timePicker2
            // 
            this.timePicker2.CustomFormat = "HH:mm";
            this.timePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timePicker2.Location = new System.Drawing.Point(295, 44);
            this.timePicker2.Name = "timePicker2";
            this.timePicker2.ShowUpDown = true;
            this.timePicker2.Size = new System.Drawing.Size(80, 25);
            this.timePicker2.TabIndex = 11;
            this.timePicker2.Value = new System.DateTime(2024, 7, 2, 12, 30, 0, 0);
            this.timePicker2.ValueChanged += new System.EventHandler(this.timePicker2_ValueChanged);
            // 
            // label_chk_2
            // 
            this.label_chk_2.AutoSize = true;
            this.label_chk_2.Location = new System.Drawing.Point(262, 50);
            this.label_chk_2.Name = "label_chk_2";
            this.label_chk_2.Size = new System.Drawing.Size(18, 17);
            this.label_chk_2.TabIndex = 10;
            this.label_chk_2.Text = "~";
            // 
            // label_chk_1
            // 
            this.label_chk_1.AutoSize = true;
            this.label_chk_1.Location = new System.Drawing.Point(39, 47);
            this.label_chk_1.Name = "label_chk_1";
            this.label_chk_1.Size = new System.Drawing.Size(114, 17);
            this.label_chk_1.TabIndex = 9;
            this.label_chk_1.Text = "체크 가능 시간 :";
            // 
            // timePicker1
            // 
            this.timePicker1.CustomFormat = "HH:mm";
            this.timePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timePicker1.Location = new System.Drawing.Point(163, 44);
            this.timePicker1.Name = "timePicker1";
            this.timePicker1.ShowUpDown = true;
            this.timePicker1.Size = new System.Drawing.Size(80, 25);
            this.timePicker1.TabIndex = 8;
            this.timePicker1.Value = new System.DateTime(2024, 7, 2, 11, 0, 0, 0);
            this.timePicker1.ValueChanged += new System.EventHandler(this.timePicker1_ValueChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.groupBox3);
            this.groupBox6.Controls.Add(this.groupBox4);
            this.groupBox6.Controls.Add(this.groupBox5);
            this.groupBox6.Font = new System.Drawing.Font("여기어때 잘난체 2 TTF", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox6.Location = new System.Drawing.Point(536, 87);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox6.Size = new System.Drawing.Size(413, 346);
            this.groupBox6.TabIndex = 12;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "무료급식사업 설정";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txt_dailylogPath);
            this.groupBox3.Controls.Add(this.btn_savedailydir);
            this.groupBox3.Font = new System.Drawing.Font("여기어때 잘난체 2 TTF", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox3.Location = new System.Drawing.Point(10, 235);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(397, 99);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "일일 식사내역 저장 경로 설정 (설정 후 재시작 필수)";
            // 
            // txt_dailylogPath
            // 
            this.txt_dailylogPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(227)))), ((int)(((byte)(255)))));
            this.txt_dailylogPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_dailylogPath.Font = new System.Drawing.Font("여기어때 잘난체 2 TTF", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_dailylogPath.Location = new System.Drawing.Point(6, 24);
            this.txt_dailylogPath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_dailylogPath.Multiline = true;
            this.txt_dailylogPath.Name = "txt_dailylogPath";
            this.txt_dailylogPath.ReadOnly = true;
            this.txt_dailylogPath.Size = new System.Drawing.Size(384, 19);
            this.txt_dailylogPath.TabIndex = 7;
            this.txt_dailylogPath.Text = "설정 필요";
            this.txt_dailylogPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btn_savedailydir
            // 
            this.btn_savedailydir.Font = new System.Drawing.Font("여기어때 잘난체 2 TTF", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_savedailydir.IconChar = FontAwesome.Sharp.IconChar.FolderBlank;
            this.btn_savedailydir.IconColor = System.Drawing.Color.Black;
            this.btn_savedailydir.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btn_savedailydir.IconSize = 30;
            this.btn_savedailydir.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_savedailydir.Location = new System.Drawing.Point(6, 47);
            this.btn_savedailydir.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_savedailydir.Name = "btn_savedailydir";
            this.btn_savedailydir.Size = new System.Drawing.Size(384, 48);
            this.btn_savedailydir.TabIndex = 2;
            this.btn_savedailydir.Text = "식사내역 저장 경로 설정";
            this.btn_savedailydir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_savedailydir.UseVisualStyleBackColor = true;
            this.btn_savedailydir.Click += new System.EventHandler(this.btn_savedailydir_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.groupBox8);
            this.groupBox7.Controls.Add(this.groupBox9);
            this.groupBox7.Controls.Add(this.groupBox1);
            this.groupBox7.Controls.Add(this.groupBox2);
            this.groupBox7.Font = new System.Drawing.Font("여기어때 잘난체 2 TTF", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox7.Location = new System.Drawing.Point(102, 84);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox7.Size = new System.Drawing.Size(413, 452);
            this.groupBox7.TabIndex = 13;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "키오스크 설정";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.serialportCB);
            this.groupBox8.Controls.Add(this.label_qr_1);
            this.groupBox8.Font = new System.Drawing.Font("여기어때 잘난체 2 TTF", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox8.Location = new System.Drawing.Point(10, 341);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox8.Size = new System.Drawing.Size(397, 95);
            this.groupBox8.TabIndex = 13;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "시리얼 포트 설정 (설정 후 재시작 필수)";
            // 
            // serialportCB
            // 
            this.serialportCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.serialportCB.FormattingEnabled = true;
            this.serialportCB.Location = new System.Drawing.Point(213, 43);
            this.serialportCB.Name = "serialportCB";
            this.serialportCB.Size = new System.Drawing.Size(121, 25);
            this.serialportCB.TabIndex = 12;
            this.serialportCB.SelectedValueChanged += new System.EventHandler(this.serialportCB_SelectedValueChanged);
            // 
            // label_qr_1
            // 
            this.label_qr_1.AutoSize = true;
            this.label_qr_1.Location = new System.Drawing.Point(67, 48);
            this.label_qr_1.Name = "label_qr_1";
            this.label_qr_1.Size = new System.Drawing.Size(140, 17);
            this.label_qr_1.TabIndex = 11;
            this.label_qr_1.Text = "QR 스캔 포트 설정 :";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.txt_bannerMain);
            this.groupBox9.Controls.Add(this.btn_bannerMain);
            this.groupBox9.Font = new System.Drawing.Font("여기어때 잘난체 2 TTF", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox9.Location = new System.Drawing.Point(10, 130);
            this.groupBox9.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox9.Size = new System.Drawing.Size(399, 99);
            this.groupBox9.TabIndex = 8;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "메인 배너 설정 (설정 후 재시작 필수)";
            // 
            // txt_bannerMain
            // 
            this.txt_bannerMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(227)))), ((int)(((byte)(255)))));
            this.txt_bannerMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_bannerMain.Font = new System.Drawing.Font("여기어때 잘난체 2 TTF", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_bannerMain.Location = new System.Drawing.Point(6, 24);
            this.txt_bannerMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_bannerMain.Multiline = true;
            this.txt_bannerMain.Name = "txt_bannerMain";
            this.txt_bannerMain.ReadOnly = true;
            this.txt_bannerMain.Size = new System.Drawing.Size(384, 19);
            this.txt_bannerMain.TabIndex = 7;
            this.txt_bannerMain.Text = "설정 필요";
            this.txt_bannerMain.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btn_bannerMain
            // 
            this.btn_bannerMain.Font = new System.Drawing.Font("여기어때 잘난체 2 TTF", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_bannerMain.IconChar = FontAwesome.Sharp.IconChar.Image;
            this.btn_bannerMain.IconColor = System.Drawing.Color.Black;
            this.btn_bannerMain.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btn_bannerMain.IconSize = 30;
            this.btn_bannerMain.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_bannerMain.Location = new System.Drawing.Point(6, 47);
            this.btn_bannerMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_bannerMain.Name = "btn_bannerMain";
            this.btn_bannerMain.Size = new System.Drawing.Size(384, 48);
            this.btn_bannerMain.TabIndex = 2;
            this.btn_bannerMain.Text = "메인 배너 설정";
            this.btn_bannerMain.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_bannerMain.UseVisualStyleBackColor = true;
            this.btn_bannerMain.Click += new System.EventHandler(this.btn_bannerMain_Click);
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.btn_debug);
            this.groupBox10.Controls.Add(this.btn_exit);
            this.groupBox10.Controls.Add(this.btn_network);
            this.groupBox10.Font = new System.Drawing.Font("여기어때 잘난체 2 TTF", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox10.Location = new System.Drawing.Point(536, 435);
            this.groupBox10.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox10.Size = new System.Drawing.Size(413, 99);
            this.groupBox10.TabIndex = 10;
            this.groupBox10.TabStop = false;
            // 
            // SettingMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(1080, 600);
            this.Controls.Add(this.groupBox10);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.panel1);
            this.DisplayHeader = false;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "SettingMenu";
            this.Padding = new System.Windows.Forms.Padding(21, 38, 21, 20);
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Style = MetroFramework.MetroColorStyle.Default;
            this.Text = "설정 창";
            this.Load += new System.EventHandler(this.SettingMenu_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label label_title;
        public FontAwesome.Sharp.IconButton btn_program;
        public System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.TextBox txt_programPath;
        public FontAwesome.Sharp.IconButton btn_exit;
        public FontAwesome.Sharp.IconButton btn_debug;
        public System.Windows.Forms.GroupBox groupBox4;
        public System.Windows.Forms.TextBox txt_listfile_path;
        public FontAwesome.Sharp.IconButton btn_file;
        public FontAwesome.Sharp.IconButton btn_network;
        public System.Windows.Forms.GroupBox groupBox5;
        public System.Windows.Forms.Label label_chk_2;
        public System.Windows.Forms.Label label_chk_1;
        public System.Windows.Forms.DateTimePicker timePicker2;
        public System.Windows.Forms.DateTimePicker timePicker1;
        public FontAwesome.Sharp.IconButton btn_resrc;
        public System.Windows.Forms.TextBox txt_resource_path;
        public System.Windows.Forms.GroupBox groupBox6;
        public System.Windows.Forms.GroupBox groupBox7;
        public System.Windows.Forms.GroupBox groupBox9;
        public System.Windows.Forms.TextBox txt_bannerMain;
        public FontAwesome.Sharp.IconButton btn_bannerMain;
        public System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.TextBox txt_dailylogPath;
        public FontAwesome.Sharp.IconButton btn_savedailydir;
        public System.Windows.Forms.GroupBox groupBox8;
        public System.Windows.Forms.Label label_qr_1;
        public System.Windows.Forms.GroupBox groupBox10;
        public System.Windows.Forms.ComboBox serialportCB;
    }
}