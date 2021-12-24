namespace PdfMergeSplitTool
{
    partial class ucSplitPages
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
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.txtText = new System.Windows.Forms.RichTextBox();
            this.chkText = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nudBlankPages = new System.Windows.Forms.NumericUpDown();
            this.chkBlankPages = new System.Windows.Forms.CheckBox();
            this.nudBookmarkLevel = new System.Windows.Forms.NumericUpDown();
            this.chkBookmarks = new System.Windows.Forms.CheckBox();
            this.nudEveryTo = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.nudEveryFrom = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.nudEvery = new System.Windows.Forms.NumericUpDown();
            this.chkEvery = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPageRanges = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.niceLine1 = new PdfMergeSplitTool.NiceLine();
            this.groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBlankPages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBookmarkLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEveryTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEveryFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEvery)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.BackColor = System.Drawing.Color.Transparent;
            this.groupBox.Controls.Add(this.niceLine1);
            this.groupBox.Controls.Add(this.txtText);
            this.groupBox.Controls.Add(this.chkText);
            this.groupBox.Controls.Add(this.label2);
            this.groupBox.Controls.Add(this.nudBlankPages);
            this.groupBox.Controls.Add(this.chkBlankPages);
            this.groupBox.Controls.Add(this.nudBookmarkLevel);
            this.groupBox.Controls.Add(this.chkBookmarks);
            this.groupBox.Controls.Add(this.nudEveryTo);
            this.groupBox.Controls.Add(this.label10);
            this.groupBox.Controls.Add(this.nudEveryFrom);
            this.groupBox.Controls.Add(this.label9);
            this.groupBox.Controls.Add(this.label7);
            this.groupBox.Controls.Add(this.nudEvery);
            this.groupBox.Controls.Add(this.chkEvery);
            this.groupBox.Controls.Add(this.label4);
            this.groupBox.Controls.Add(this.txtPageRanges);
            this.groupBox.Controls.Add(this.label1);
            this.groupBox.Location = new System.Drawing.Point(3, 3);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(559, 256);
            this.groupBox.TabIndex = 33;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Split Pages";
            // 
            // txtText
            // 
            this.txtText.Enabled = false;
            this.txtText.Location = new System.Drawing.Point(196, 181);
            this.txtText.Name = "txtText";
            this.txtText.Size = new System.Drawing.Size(347, 55);
            this.txtText.TabIndex = 36;
            this.txtText.Text = "";
            // 
            // chkText
            // 
            this.chkText.AutoSize = true;
            this.chkText.BackColor = System.Drawing.Color.Transparent;
            this.chkText.Location = new System.Drawing.Point(26, 183);
            this.chkText.Name = "chkText";
            this.chkText.Size = new System.Drawing.Size(175, 17);
            this.chkText.TabIndex = 35;
            this.chkText.Text = "Split by Pages containing Text :";
            this.chkText.UseVisualStyleBackColor = false;
            this.chkText.CheckedChanged += new System.EventHandler(this.chkText_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(282, 148);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 13);
            this.label2.TabIndex = 34;
            this.label2.Text = "blank Pages in a Row.";
            // 
            // nudBlankPages
            // 
            this.nudBlankPages.Location = new System.Drawing.Point(196, 146);
            this.nudBlankPages.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudBlankPages.Name = "nudBlankPages";
            this.nudBlankPages.Size = new System.Drawing.Size(80, 20);
            this.nudBlankPages.TabIndex = 33;
            this.nudBlankPages.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // chkBlankPages
            // 
            this.chkBlankPages.AutoSize = true;
            this.chkBlankPages.BackColor = System.Drawing.Color.Transparent;
            this.chkBlankPages.Location = new System.Drawing.Point(28, 147);
            this.chkBlankPages.Name = "chkBlankPages";
            this.chkBlankPages.Size = new System.Drawing.Size(170, 17);
            this.chkBlankPages.TabIndex = 32;
            this.chkBlankPages.Text = "Split by blank Pages, split after";
            this.chkBlankPages.UseVisualStyleBackColor = false;
            // 
            // nudBookmarkLevel
            // 
            this.nudBookmarkLevel.Location = new System.Drawing.Point(196, 113);
            this.nudBookmarkLevel.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudBookmarkLevel.Name = "nudBookmarkLevel";
            this.nudBookmarkLevel.Size = new System.Drawing.Size(80, 20);
            this.nudBookmarkLevel.TabIndex = 31;
            this.nudBookmarkLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // chkBookmarks
            // 
            this.chkBookmarks.AutoSize = true;
            this.chkBookmarks.BackColor = System.Drawing.Color.Transparent;
            this.chkBookmarks.Location = new System.Drawing.Point(28, 114);
            this.chkBookmarks.Name = "chkBookmarks";
            this.chkBookmarks.Size = new System.Drawing.Size(162, 17);
            this.chkBookmarks.TabIndex = 30;
            this.chkBookmarks.Text = "Split by Bookmarks, at level :";
            this.chkBookmarks.UseVisualStyleBackColor = false;
            // 
            // nudEveryTo
            // 
            this.nudEveryTo.Location = new System.Drawing.Point(403, 78);
            this.nudEveryTo.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudEveryTo.Name = "nudEveryTo";
            this.nudEveryTo.Size = new System.Drawing.Size(80, 20);
            this.nudEveryTo.TabIndex = 29;
            this.nudEveryTo.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(381, 80);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(16, 13);
            this.label10.TabIndex = 28;
            this.label10.Text = "to";
            // 
            // nudEveryFrom
            // 
            this.nudEveryFrom.Location = new System.Drawing.Point(296, 78);
            this.nudEveryFrom.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudEveryFrom.Name = "nudEveryFrom";
            this.nudEveryFrom.Size = new System.Drawing.Size(80, 20);
            this.nudEveryFrom.TabIndex = 27;
            this.nudEveryFrom.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Location = new System.Drawing.Point(259, 80);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(30, 13);
            this.label9.TabIndex = 26;
            this.label9.Text = "from ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(222, 80);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Pages ";
            // 
            // nudEvery
            // 
            this.nudEvery.Location = new System.Drawing.Point(136, 78);
            this.nudEvery.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudEvery.Name = "nudEvery";
            this.nudEvery.Size = new System.Drawing.Size(80, 20);
            this.nudEvery.TabIndex = 17;
            this.nudEvery.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // chkEvery
            // 
            this.chkEvery.AutoSize = true;
            this.chkEvery.BackColor = System.Drawing.Color.Transparent;
            this.chkEvery.Location = new System.Drawing.Point(28, 79);
            this.chkEvery.Name = "chkEvery";
            this.chkEvery.Size = new System.Drawing.Size(56, 17);
            this.chkEvery.TabIndex = 16;
            this.chkEvery.Text = "Every ";
            this.chkEvery.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label4.Location = new System.Drawing.Point(25, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(253, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "( comma separated, for example : 1-15,20-30,50-80 )";
            // 
            // txtPageRanges
            // 
            this.txtPageRanges.Location = new System.Drawing.Point(121, 22);
            this.txtPageRanges.Name = "txtPageRanges";
            this.txtPageRanges.Size = new System.Drawing.Size(347, 20);
            this.txtPageRanges.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(25, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Page Ranges :";
            // 
            // niceLine1
            // 
            this.niceLine1.Location = new System.Drawing.Point(5, 62);
            this.niceLine1.Name = "niceLine1";
            this.niceLine1.Size = new System.Drawing.Size(538, 15);
            this.niceLine1.TabIndex = 37;
            // 
            // ucSplitPages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox);
            this.Name = "ucSplitPages";
            this.Size = new System.Drawing.Size(573, 271);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBlankPages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBookmarkLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEveryTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEveryFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEvery)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox;
        public System.Windows.Forms.NumericUpDown nudEveryTo;
        public System.Windows.Forms.Label label10;
        public System.Windows.Forms.NumericUpDown nudEveryFrom;
        public System.Windows.Forms.Label label9;
        public System.Windows.Forms.Label label7;
        public System.Windows.Forms.NumericUpDown nudEvery;
        public System.Windows.Forms.CheckBox chkEvery;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtPageRanges;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.NumericUpDown nudBlankPages;
        public System.Windows.Forms.CheckBox chkBlankPages;
        public System.Windows.Forms.NumericUpDown nudBookmarkLevel;
        public System.Windows.Forms.CheckBox chkBookmarks;
        public System.Windows.Forms.CheckBox chkText;
        public System.Windows.Forms.RichTextBox txtText;
        private NiceLine niceLine1;
    }
}
