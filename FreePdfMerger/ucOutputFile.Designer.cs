namespace PdfMergeSplitTool
{
    partial class ucOutputFile
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtOutputFile = new System.Windows.Forms.TextBox();
            this.chkFolders = new System.Windows.Forms.CheckBox();
            this.chkGroup = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkHour = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkWeek = new System.Windows.Forms.CheckBox();
            this.chkYear = new System.Windows.Forms.CheckBox();
            this.chkDay = new System.Windows.Forms.CheckBox();
            this.chkMonth = new System.Windows.Forms.CheckBox();
            this.chkSeparateDate = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nudHeight = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.nudWidth = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.niceLine1 = new PdfMergeSplitTool.NiceLine();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBrowse.Location = new System.Drawing.Point(424, 35);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(139, 22);
            this.btnBrowse.TabIndex = 8;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtOutputFile
            // 
            this.txtOutputFile.Location = new System.Drawing.Point(20, 37);
            this.txtOutputFile.Name = "txtOutputFile";
            this.txtOutputFile.Size = new System.Drawing.Size(398, 20);
            this.txtOutputFile.TabIndex = 43;
            // 
            // chkFolders
            // 
            this.chkFolders.AutoSize = true;
            this.chkFolders.Location = new System.Drawing.Point(20, 28);
            this.chkFolders.Name = "chkFolders";
            this.chkFolders.Size = new System.Drawing.Size(144, 17);
            this.chkFolders.TabIndex = 45;
            this.chkFolders.Text = "Separate Files by Folders";
            this.chkFolders.UseVisualStyleBackColor = true;
            this.chkFolders.CheckedChanged += new System.EventHandler(this.chkFolders_CheckedChanged);
            // 
            // chkGroup
            // 
            this.chkGroup.AutoSize = true;
            this.chkGroup.Location = new System.Drawing.Point(20, 52);
            this.chkGroup.Name = "chkGroup";
            this.chkGroup.Size = new System.Drawing.Size(163, 17);
            this.chkGroup.TabIndex = 46;
            this.chkGroup.Text = "Separate Files by Name Part.";
            this.chkGroup.UseVisualStyleBackColor = true;
            this.chkGroup.CheckedChanged += new System.EventHandler(this.chkFolders_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.chkHour);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.chkWeek);
            this.groupBox1.Controls.Add(this.chkYear);
            this.groupBox1.Controls.Add(this.chkDay);
            this.groupBox1.Controls.Add(this.chkMonth);
            this.groupBox1.Controls.Add(this.chkSeparateDate);
            this.groupBox1.Location = new System.Drawing.Point(13, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(603, 138);
            this.groupBox1.TabIndex = 48;
            this.groupBox1.TabStop = false;
            // 
            // chkHour
            // 
            this.chkHour.AutoSize = true;
            this.chkHour.Location = new System.Drawing.Point(187, 77);
            this.chkHour.Name = "chkHour";
            this.chkHour.Size = new System.Drawing.Size(79, 17);
            this.chkHour.TabIndex = 54;
            this.chkHour.Text = "Same Hour";
            this.chkHour.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(74, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 53;
            this.label3.Text = "Group by :";
            // 
            // chkWeek
            // 
            this.chkWeek.AutoSize = true;
            this.chkWeek.Location = new System.Drawing.Point(66, 54);
            this.chkWeek.Name = "chkWeek";
            this.chkWeek.Size = new System.Drawing.Size(85, 17);
            this.chkWeek.TabIndex = 51;
            this.chkWeek.Text = "Same Week";
            this.chkWeek.UseVisualStyleBackColor = true;
            // 
            // chkYear
            // 
            this.chkYear.AutoSize = true;
            this.chkYear.Location = new System.Drawing.Point(187, 54);
            this.chkYear.Name = "chkYear";
            this.chkYear.Size = new System.Drawing.Size(78, 17);
            this.chkYear.TabIndex = 50;
            this.chkYear.Text = "Same Year";
            this.chkYear.UseVisualStyleBackColor = true;
            // 
            // chkDay
            // 
            this.chkDay.AutoSize = true;
            this.chkDay.Location = new System.Drawing.Point(66, 100);
            this.chkDay.Name = "chkDay";
            this.chkDay.Size = new System.Drawing.Size(75, 17);
            this.chkDay.TabIndex = 49;
            this.chkDay.Text = "Same Day";
            this.chkDay.UseVisualStyleBackColor = true;
            // 
            // chkMonth
            // 
            this.chkMonth.AutoSize = true;
            this.chkMonth.Location = new System.Drawing.Point(66, 77);
            this.chkMonth.Name = "chkMonth";
            this.chkMonth.Size = new System.Drawing.Size(86, 17);
            this.chkMonth.TabIndex = 48;
            this.chkMonth.Text = "Same Month";
            this.chkMonth.UseVisualStyleBackColor = true;
            // 
            // chkSeparateDate
            // 
            this.chkSeparateDate.AutoSize = true;
            this.chkSeparateDate.Location = new System.Drawing.Point(5, 0);
            this.chkSeparateDate.Name = "chkSeparateDate";
            this.chkSeparateDate.Size = new System.Drawing.Size(152, 17);
            this.chkSeparateDate.TabIndex = 47;
            this.chkSeparateDate.Text = "Separate Files by File Date";
            this.chkSeparateDate.UseVisualStyleBackColor = true;
            this.chkSeparateDate.CheckedChanged += new System.EventHandler(this.chkFolders_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(407, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 52;
            this.label1.Text = "character to";
            // 
            // nudHeight
            // 
            this.nudHeight.Location = new System.Drawing.Point(477, 50);
            this.nudHeight.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudHeight.Name = "nudHeight";
            this.nudHeight.Size = new System.Drawing.Size(80, 20);
            this.nudHeight.TabIndex = 51;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(186, 53);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(129, 13);
            this.label8.TabIndex = 50;
            this.label8.Text = "Group by name part from :";
            // 
            // nudWidth
            // 
            this.nudWidth.Location = new System.Drawing.Point(321, 50);
            this.nudWidth.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudWidth.Name = "nudWidth";
            this.nudWidth.Size = new System.Drawing.Size(80, 20);
            this.nudWidth.TabIndex = 49;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(563, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 53;
            this.label2.Text = "character.";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.nudHeight);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.nudWidth);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.chkGroup);
            this.groupBox2.Controls.Add(this.chkFolders);
            this.groupBox2.Location = new System.Drawing.Point(4, 90);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(627, 250);
            this.groupBox2.TabIndex = 54;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Separate Output Documents";
            this.groupBox2.Visible = false;
            // 
            // niceLine1
            // 
            this.niceLine1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.niceLine1.BackColor = System.Drawing.Color.Transparent;
            this.niceLine1.Caption = "Output File";
            this.niceLine1.Location = new System.Drawing.Point(15, 14);
            this.niceLine1.Name = "niceLine1";
            this.niceLine1.Size = new System.Drawing.Size(601, 15);
            this.niceLine1.TabIndex = 47;
            // 
            // ucOutputFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.niceLine1);
            this.Controls.Add(this.txtOutputFile);
            this.Controls.Add(this.btnBrowse);
            this.Name = "ucOutputFile";
            this.Size = new System.Drawing.Size(644, 355);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        public System.Windows.Forms.Button btnBrowse;
        public System.Windows.Forms.TextBox txtOutputFile;
        public System.Windows.Forms.CheckBox chkFolders;
        public System.Windows.Forms.CheckBox chkGroup;
        public NiceLine niceLine1;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.CheckBox chkSeparateDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudHeight;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nudWidth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.CheckBox chkWeek;
        public System.Windows.Forms.CheckBox chkYear;
        public System.Windows.Forms.CheckBox chkDay;
        public System.Windows.Forms.CheckBox chkMonth;
        public System.Windows.Forms.CheckBox chkHour;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}
