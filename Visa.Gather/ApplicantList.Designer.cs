namespace Visa.Gather
{
    partial class ApplicantList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicantList));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonVideo = new System.Windows.Forms.Button();
            this.buttonFinger = new System.Windows.Forms.Button();
            this.buttonFind = new System.Windows.Forms.Button();
            this.textBoxNumber = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxTelphone = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxGender = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxSur = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxGive = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPassport = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.护照号码 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.护照到期日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Givename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Surname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.性别 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.国籍 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.电话 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.邮箱 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.预约序号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.跟踪ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.提交状态 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.预约日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.预约时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.访问国家 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.指纹采集状态 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.材料采集状态 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox1.Controls.Add(this.buttonVideo);
            this.groupBox1.Controls.Add(this.buttonFinger);
            this.groupBox1.Controls.Add(this.buttonFind);
            this.groupBox1.Controls.Add(this.textBoxNumber);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBoxTelphone);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.comboBoxGender);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBoxSur);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxGive);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxPassport);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(10, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(964, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // buttonVideo
            // 
            this.buttonVideo.Location = new System.Drawing.Point(732, 59);
            this.buttonVideo.Name = "buttonVideo";
            this.buttonVideo.Size = new System.Drawing.Size(100, 26);
            this.buttonVideo.TabIndex = 14;
            this.buttonVideo.Text = "视频采集";
            this.buttonVideo.UseVisualStyleBackColor = true;
            this.buttonVideo.Click += new System.EventHandler(this.buttonVideo_Click);
            // 
            // buttonFinger
            // 
            this.buttonFinger.Location = new System.Drawing.Point(732, 26);
            this.buttonFinger.Name = "buttonFinger";
            this.buttonFinger.Size = new System.Drawing.Size(100, 26);
            this.buttonFinger.TabIndex = 13;
            this.buttonFinger.Text = "指纹采集";
            this.buttonFinger.UseVisualStyleBackColor = true;
            this.buttonFinger.Click += new System.EventHandler(this.buttonFinger_Click);
            // 
            // buttonFind
            // 
            this.buttonFind.Location = new System.Drawing.Point(631, 26);
            this.buttonFind.Name = "buttonFind";
            this.buttonFind.Size = new System.Drawing.Size(78, 26);
            this.buttonFind.TabIndex = 12;
            this.buttonFind.Text = "查   询";
            this.buttonFind.UseVisualStyleBackColor = true;
            this.buttonFind.Click += new System.EventHandler(this.buttonFind_Click);
            // 
            // textBoxNumber
            // 
            this.textBoxNumber.Location = new System.Drawing.Point(492, 62);
            this.textBoxNumber.Name = "textBoxNumber";
            this.textBoxNumber.Size = new System.Drawing.Size(100, 23);
            this.textBoxNumber.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(423, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 14);
            this.label6.TabIndex = 10;
            this.label6.Text = "预约序号";
            // 
            // textBoxTelphone
            // 
            this.textBoxTelphone.Location = new System.Drawing.Point(295, 61);
            this.textBoxTelphone.Name = "textBoxTelphone";
            this.textBoxTelphone.Size = new System.Drawing.Size(100, 23);
            this.textBoxTelphone.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(226, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 14);
            this.label5.TabIndex = 8;
            this.label5.Text = "电    话";
            // 
            // comboBoxGender
            // 
            this.comboBoxGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGender.FormattingEnabled = true;
            this.comboBoxGender.Items.AddRange(new object[] {
            "全部",
            "男",
            "女"});
            this.comboBoxGender.Location = new System.Drawing.Point(85, 61);
            this.comboBoxGender.Name = "comboBoxGender";
            this.comboBoxGender.Size = new System.Drawing.Size(100, 21);
            this.comboBoxGender.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 14);
            this.label4.TabIndex = 6;
            this.label4.Text = "性    别";
            // 
            // textBoxSur
            // 
            this.textBoxSur.Location = new System.Drawing.Point(492, 25);
            this.textBoxSur.Name = "textBoxSur";
            this.textBoxSur.Size = new System.Drawing.Size(100, 23);
            this.textBoxSur.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(401, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 14);
            this.label3.TabIndex = 4;
            this.label3.Text = "Surname(姓)";
            // 
            // textBoxGive
            // 
            this.textBoxGive.Location = new System.Drawing.Point(295, 23);
            this.textBoxGive.Name = "textBoxGive";
            this.textBoxGive.Size = new System.Drawing.Size(100, 23);
            this.textBoxGive.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(203, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "Givename(名)";
            // 
            // textBoxPassport
            // 
            this.textBoxPassport.Location = new System.Drawing.Point(85, 23);
            this.textBoxPassport.Name = "textBoxPassport";
            this.textBoxPassport.Size = new System.Drawing.Size(100, 23);
            this.textBoxPassport.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "护照号码";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.护照号码,
            this.护照到期日期,
            this.Givename,
            this.Surname,
            this.性别,
            this.国籍,
            this.电话,
            this.邮箱,
            this.预约序号,
            this.跟踪ID,
            this.提交状态,
            this.预约日期,
            this.预约时间,
            this.访问国家,
            this.指纹采集状态,
            this.材料采集状态});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(10, 100);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(964, 531);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "Id";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // 护照号码
            // 
            this.护照号码.DataPropertyName = "Passport";
            this.护照号码.HeaderText = "护照号码";
            this.护照号码.Name = "护照号码";
            this.护照号码.ReadOnly = true;
            // 
            // 护照到期日期
            // 
            this.护照到期日期.DataPropertyName = "Expirydate";
            this.护照到期日期.HeaderText = "护照到期日期";
            this.护照到期日期.Name = "护照到期日期";
            this.护照到期日期.ReadOnly = true;
            // 
            // Givename
            // 
            this.Givename.DataPropertyName = "Givename";
            this.Givename.HeaderText = "Givename";
            this.Givename.Name = "Givename";
            this.Givename.ReadOnly = true;
            // 
            // Surname
            // 
            this.Surname.DataPropertyName = "Surname";
            this.Surname.HeaderText = "Surname";
            this.Surname.Name = "Surname";
            this.Surname.ReadOnly = true;
            // 
            // 性别
            // 
            this.性别.DataPropertyName = "Gender";
            this.性别.HeaderText = "性别";
            this.性别.Name = "性别";
            this.性别.ReadOnly = true;
            // 
            // 国籍
            // 
            this.国籍.DataPropertyName = "Nationality";
            this.国籍.HeaderText = "国籍";
            this.国籍.Name = "国籍";
            this.国籍.ReadOnly = true;
            // 
            // 电话
            // 
            this.电话.DataPropertyName = "Cellphone";
            this.电话.HeaderText = "电话";
            this.电话.Name = "电话";
            this.电话.ReadOnly = true;
            // 
            // 邮箱
            // 
            this.邮箱.DataPropertyName = "Email";
            this.邮箱.HeaderText = "邮箱";
            this.邮箱.Name = "邮箱";
            this.邮箱.ReadOnly = true;
            // 
            // 预约序号
            // 
            this.预约序号.DataPropertyName = "ApplyNumber";
            this.预约序号.HeaderText = "预约序号";
            this.预约序号.Name = "预约序号";
            this.预约序号.ReadOnly = true;
            // 
            // 跟踪ID
            // 
            this.跟踪ID.DataPropertyName = "TransactionId";
            this.跟踪ID.HeaderText = "跟踪ID";
            this.跟踪ID.Name = "跟踪ID";
            this.跟踪ID.ReadOnly = true;
            // 
            // 提交状态
            // 
            this.提交状态.DataPropertyName = "ApStatus";
            this.提交状态.HeaderText = "提交状态";
            this.提交状态.Name = "提交状态";
            this.提交状态.ReadOnly = true;
            // 
            // 预约日期
            // 
            this.预约日期.DataPropertyName = "DateName";
            this.预约日期.HeaderText = "预约日期";
            this.预约日期.Name = "预约日期";
            this.预约日期.ReadOnly = true;
            // 
            // 预约时间
            // 
            this.预约时间.DataPropertyName = "TimeRange";
            this.预约时间.HeaderText = "预约时间";
            this.预约时间.Name = "预约时间";
            this.预约时间.ReadOnly = true;
            // 
            // 访问国家
            // 
            this.访问国家.DataPropertyName = "VisitCountry";
            this.访问国家.HeaderText = "访问国家";
            this.访问国家.Name = "访问国家";
            this.访问国家.ReadOnly = true;
            // 
            // 指纹采集状态
            // 
            this.指纹采集状态.DataPropertyName = "fingerStatus";
            this.指纹采集状态.HeaderText = "指纹采集状态";
            this.指纹采集状态.Name = "指纹采集状态";
            this.指纹采集状态.ReadOnly = true;
            // 
            // 材料采集状态
            // 
            this.材料采集状态.DataPropertyName = "videoStatus";
            this.材料采集状态.HeaderText = "材料采集状态";
            this.材料采集状态.Name = "材料采集状态";
            this.材料采集状态.ReadOnly = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Visa.Gather.Properties.Resources.ajax_loader;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Location = new System.Drawing.Point(589, 439);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(220, 19);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // ApplicantList
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(984, 641);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ApplicantList";
            this.Padding = new System.Windows.Forms.Padding(10, 0, 10, 10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "申请人";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ApplicantList_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSur;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxGive;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPassport;
        private System.Windows.Forms.Button buttonFind;
        private System.Windows.Forms.TextBox textBoxNumber;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxTelphone;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxGender;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonVideo;
        private System.Windows.Forms.Button buttonFinger;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn 护照号码;
        private System.Windows.Forms.DataGridViewTextBoxColumn 护照到期日期;
        private System.Windows.Forms.DataGridViewTextBoxColumn Givename;
        private System.Windows.Forms.DataGridViewTextBoxColumn Surname;
        private System.Windows.Forms.DataGridViewTextBoxColumn 性别;
        private System.Windows.Forms.DataGridViewTextBoxColumn 国籍;
        private System.Windows.Forms.DataGridViewTextBoxColumn 电话;
        private System.Windows.Forms.DataGridViewTextBoxColumn 邮箱;
        private System.Windows.Forms.DataGridViewTextBoxColumn 预约序号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 跟踪ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn 提交状态;
        private System.Windows.Forms.DataGridViewTextBoxColumn 预约日期;
        private System.Windows.Forms.DataGridViewTextBoxColumn 预约时间;
        private System.Windows.Forms.DataGridViewTextBoxColumn 访问国家;
        private System.Windows.Forms.DataGridViewTextBoxColumn 指纹采集状态;
        private System.Windows.Forms.DataGridViewTextBoxColumn 材料采集状态;
    }
}