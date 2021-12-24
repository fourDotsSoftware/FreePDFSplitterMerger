namespace PdfMergeSplitTool
{
    partial class ucFooter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucFooter));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkImagePositionRight = new System.Windows.Forms.CheckBox();
            this.chkImagePositionCenter = new System.Windows.Forms.CheckBox();
            this.chkImagePositionLeft = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtFooterImage = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkTextPositionRight = new System.Windows.Forms.CheckBox();
            this.chkTextPositionCenter = new System.Windows.Forms.CheckBox();
            this.chkTextPositionLeft = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFooter = new System.Windows.Forms.TextBox();
            this.chkContinueNumbering = new System.Windows.Forms.CheckBox();
            this.nudTo = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.nudFrom = new System.Windows.Forms.NumericUpDown();
            this.chkPagesFromTo = new System.Windows.Forms.CheckBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.niceLine1 = new PdfMergeSplitTool.NiceLine();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFrom)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkImagePositionRight);
            this.groupBox2.Controls.Add(this.chkImagePositionCenter);
            this.groupBox2.Controls.Add(this.chkImagePositionLeft);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnBrowse);
            this.groupBox2.Controls.Add(this.txtFooterImage);
            this.groupBox2.Location = new System.Drawing.Point(20, 244);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(620, 97);
            this.groupBox2.TabIndex = 55;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Footer Image";
            // 
            // chkImagePositionRight
            // 
            this.chkImagePositionRight.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkImagePositionRight.AutoSize = true;
            this.chkImagePositionRight.Image = global::PdfMergeSplitTool.Properties.Resources.text_align_right;
            this.chkImagePositionRight.Location = new System.Drawing.Point(244, 59);
            this.chkImagePositionRight.Name = "chkImagePositionRight";
            this.chkImagePositionRight.Size = new System.Drawing.Size(58, 23);
            this.chkImagePositionRight.TabIndex = 49;
            this.chkImagePositionRight.Text = "Right";
            this.chkImagePositionRight.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.chkImagePositionRight.UseVisualStyleBackColor = true;
            this.chkImagePositionRight.CheckedChanged += new System.EventHandler(this.chkImagePositionLeft_CheckedChanged);
            // 
            // chkImagePositionCenter
            // 
            this.chkImagePositionCenter.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkImagePositionCenter.AutoSize = true;
            this.chkImagePositionCenter.Checked = true;
            this.chkImagePositionCenter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkImagePositionCenter.Image = global::PdfMergeSplitTool.Properties.Resources.text_align_center;
            this.chkImagePositionCenter.Location = new System.Drawing.Point(164, 59);
            this.chkImagePositionCenter.Name = "chkImagePositionCenter";
            this.chkImagePositionCenter.Size = new System.Drawing.Size(64, 23);
            this.chkImagePositionCenter.TabIndex = 48;
            this.chkImagePositionCenter.Text = "Center";
            this.chkImagePositionCenter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.chkImagePositionCenter.UseVisualStyleBackColor = true;
            this.chkImagePositionCenter.CheckedChanged += new System.EventHandler(this.chkImagePositionLeft_CheckedChanged);
            // 
            // chkImagePositionLeft
            // 
            this.chkImagePositionLeft.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkImagePositionLeft.AutoSize = true;
            this.chkImagePositionLeft.Image = global::PdfMergeSplitTool.Properties.Resources.text_align_left;
            this.chkImagePositionLeft.Location = new System.Drawing.Point(92, 59);
            this.chkImagePositionLeft.Name = "chkImagePositionLeft";
            this.chkImagePositionLeft.Size = new System.Drawing.Size(51, 23);
            this.chkImagePositionLeft.TabIndex = 47;
            this.chkImagePositionLeft.Text = "Left";
            this.chkImagePositionLeft.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.chkImagePositionLeft.UseVisualStyleBackColor = true;
            this.chkImagePositionLeft.CheckedChanged += new System.EventHandler(this.chkImagePositionLeft_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(36, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 46;
            this.label3.Text = "Position :";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBrowse.Location = new System.Drawing.Point(469, 26);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(139, 22);
            this.btnBrowse.TabIndex = 45;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtFooterImage
            // 
            this.txtFooterImage.BackColor = System.Drawing.Color.White;
            this.txtFooterImage.Location = new System.Drawing.Point(14, 28);
            this.txtFooterImage.Name = "txtFooterImage";
            this.txtFooterImage.Size = new System.Drawing.Size(449, 20);
            this.txtFooterImage.TabIndex = 44;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkTextPositionRight);
            this.groupBox1.Controls.Add(this.chkTextPositionCenter);
            this.groupBox1.Controls.Add(this.chkTextPositionLeft);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtFooter);
            this.groupBox1.Location = new System.Drawing.Point(15, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(625, 212);
            this.groupBox1.TabIndex = 54;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Footer Text";
            // 
            // chkTextPositionRight
            // 
            this.chkTextPositionRight.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkTextPositionRight.AutoSize = true;
            this.chkTextPositionRight.Image = global::PdfMergeSplitTool.Properties.Resources.text_align_right;
            this.chkTextPositionRight.Location = new System.Drawing.Point(249, 166);
            this.chkTextPositionRight.Name = "chkTextPositionRight";
            this.chkTextPositionRight.Size = new System.Drawing.Size(58, 23);
            this.chkTextPositionRight.TabIndex = 49;
            this.chkTextPositionRight.Text = "Right";
            this.chkTextPositionRight.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.chkTextPositionRight.UseVisualStyleBackColor = true;
            this.chkTextPositionRight.CheckedChanged += new System.EventHandler(this.chkTextPositionLeft_CheckedChanged);
            // 
            // chkTextPositionCenter
            // 
            this.chkTextPositionCenter.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkTextPositionCenter.AutoSize = true;
            this.chkTextPositionCenter.Checked = true;
            this.chkTextPositionCenter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTextPositionCenter.Image = global::PdfMergeSplitTool.Properties.Resources.text_align_center;
            this.chkTextPositionCenter.Location = new System.Drawing.Point(169, 166);
            this.chkTextPositionCenter.Name = "chkTextPositionCenter";
            this.chkTextPositionCenter.Size = new System.Drawing.Size(64, 23);
            this.chkTextPositionCenter.TabIndex = 48;
            this.chkTextPositionCenter.Text = "Center";
            this.chkTextPositionCenter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.chkTextPositionCenter.UseVisualStyleBackColor = true;
            this.chkTextPositionCenter.CheckedChanged += new System.EventHandler(this.chkTextPositionLeft_CheckedChanged);
            // 
            // chkTextPositionLeft
            // 
            this.chkTextPositionLeft.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkTextPositionLeft.AutoSize = true;
            this.chkTextPositionLeft.Image = global::PdfMergeSplitTool.Properties.Resources.text_align_left;
            this.chkTextPositionLeft.Location = new System.Drawing.Point(97, 166);
            this.chkTextPositionLeft.Name = "chkTextPositionLeft";
            this.chkTextPositionLeft.Size = new System.Drawing.Size(51, 23);
            this.chkTextPositionLeft.TabIndex = 47;
            this.chkTextPositionLeft.Text = "Left";
            this.chkTextPositionLeft.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.chkTextPositionLeft.UseVisualStyleBackColor = true;
            this.chkTextPositionLeft.CheckedChanged += new System.EventHandler(this.chkTextPositionLeft_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(41, 171);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 46;
            this.label1.Text = "Position :";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label2.Location = new System.Drawing.Point(11, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(249, 121);
            this.label2.TabIndex = 45;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // txtFooter
            // 
            this.txtFooter.Location = new System.Drawing.Point(14, 19);
            this.txtFooter.Name = "txtFooter";
            this.txtFooter.Size = new System.Drawing.Size(505, 20);
            this.txtFooter.TabIndex = 44;
            // 
            // chkContinueNumbering
            // 
            this.chkContinueNumbering.AutoSize = true;
            this.chkContinueNumbering.BackColor = System.Drawing.Color.Transparent;
            this.chkContinueNumbering.Checked = true;
            this.chkContinueNumbering.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkContinueNumbering.Location = new System.Drawing.Point(39, 393);
            this.chkContinueNumbering.Name = "chkContinueNumbering";
            this.chkContinueNumbering.Size = new System.Drawing.Size(240, 17);
            this.chkContinueNumbering.TabIndex = 53;
            this.chkContinueNumbering.Text = "Continue Numbering from previous Document";
            this.chkContinueNumbering.UseVisualStyleBackColor = false;
            // 
            // nudTo
            // 
            this.nudTo.Location = new System.Drawing.Point(226, 356);
            this.nudTo.Name = "nudTo";
            this.nudTo.Size = new System.Drawing.Size(80, 20);
            this.nudTo.TabIndex = 52;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(204, 358);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(16, 13);
            this.label8.TabIndex = 51;
            this.label8.Text = "to";
            // 
            // nudFrom
            // 
            this.nudFrom.Location = new System.Drawing.Point(119, 356);
            this.nudFrom.Name = "nudFrom";
            this.nudFrom.Size = new System.Drawing.Size(80, 20);
            this.nudFrom.TabIndex = 50;
            // 
            // chkPagesFromTo
            // 
            this.chkPagesFromTo.AutoSize = true;
            this.chkPagesFromTo.BackColor = System.Drawing.Color.Transparent;
            this.chkPagesFromTo.Checked = true;
            this.chkPagesFromTo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPagesFromTo.Location = new System.Drawing.Point(39, 357);
            this.chkPagesFromTo.Name = "chkPagesFromTo";
            this.chkPagesFromTo.Size = new System.Drawing.Size(74, 17);
            this.chkPagesFromTo.TabIndex = 49;
            this.chkPagesFromTo.Text = "Start from ";
            this.chkPagesFromTo.UseVisualStyleBackColor = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // niceLine1
            // 
            this.niceLine1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.niceLine1.BackColor = System.Drawing.Color.Transparent;
            this.niceLine1.Caption = "Footer";
            this.niceLine1.Location = new System.Drawing.Point(5, 5);
            this.niceLine1.Name = "niceLine1";
            this.niceLine1.Size = new System.Drawing.Size(648, 15);
            this.niceLine1.TabIndex = 34;
            // 
            // ucFooter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chkContinueNumbering);
            this.Controls.Add(this.nudTo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.nudFrom);
            this.Controls.Add(this.chkPagesFromTo);
            this.Controls.Add(this.niceLine1);
            this.Name = "ucFooter";
            this.Size = new System.Drawing.Size(666, 435);
            this.Load += new System.EventHandler(this.ucFooter_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFrom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public NiceLine niceLine1;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.CheckBox chkImagePositionRight;
        public System.Windows.Forms.CheckBox chkImagePositionCenter;
        public System.Windows.Forms.CheckBox chkImagePositionLeft;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Button btnBrowse;
        public System.Windows.Forms.TextBox txtFooterImage;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.CheckBox chkTextPositionRight;
        public System.Windows.Forms.CheckBox chkTextPositionCenter;
        public System.Windows.Forms.CheckBox chkTextPositionLeft;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtFooter;
        public System.Windows.Forms.CheckBox chkContinueNumbering;
        public System.Windows.Forms.NumericUpDown nudTo;
        public System.Windows.Forms.Label label8;
        public System.Windows.Forms.NumericUpDown nudFrom;
        public System.Windows.Forms.CheckBox chkPagesFromTo;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}
