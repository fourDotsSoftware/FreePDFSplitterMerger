namespace PdfMergeSplitTool
{
    partial class ucDeleteExtractPages
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
            this.chkOdd = new System.Windows.Forms.CheckBox();
            this.nudEveryTo = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.nudEveryFrom = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.nudTo = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.nudFrom = new System.Windows.Forms.NumericUpDown();
            this.chkPagesFromTo = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.nudEvery = new System.Windows.Forms.NumericUpDown();
            this.chkEvery = new System.Windows.Forms.CheckBox();
            this.nudEvenTo = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.nudEvenFrom = new System.Windows.Forms.NumericUpDown();
            this.chkEven = new System.Windows.Forms.CheckBox();
            this.nudOddTo = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nudOddFrom = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPageRanges = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.niceLine1 = new PdfMergeSplitTool.NiceLine();
            this.groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEveryTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEveryFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEvery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEvenTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEvenFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOddTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOddFrom)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.BackColor = System.Drawing.Color.Transparent;
            this.groupBox.Controls.Add(this.niceLine1);
            this.groupBox.Controls.Add(this.txtText);
            this.groupBox.Controls.Add(this.chkText);
            this.groupBox.Controls.Add(this.chkOdd);
            this.groupBox.Controls.Add(this.nudEveryTo);
            this.groupBox.Controls.Add(this.label10);
            this.groupBox.Controls.Add(this.nudEveryFrom);
            this.groupBox.Controls.Add(this.label9);
            this.groupBox.Controls.Add(this.nudTo);
            this.groupBox.Controls.Add(this.label8);
            this.groupBox.Controls.Add(this.nudFrom);
            this.groupBox.Controls.Add(this.chkPagesFromTo);
            this.groupBox.Controls.Add(this.label7);
            this.groupBox.Controls.Add(this.nudEvery);
            this.groupBox.Controls.Add(this.chkEvery);
            this.groupBox.Controls.Add(this.nudEvenTo);
            this.groupBox.Controls.Add(this.label6);
            this.groupBox.Controls.Add(this.nudEvenFrom);
            this.groupBox.Controls.Add(this.chkEven);
            this.groupBox.Controls.Add(this.nudOddTo);
            this.groupBox.Controls.Add(this.label5);
            this.groupBox.Controls.Add(this.nudOddFrom);
            this.groupBox.Controls.Add(this.label4);
            this.groupBox.Controls.Add(this.txtPageRanges);
            this.groupBox.Controls.Add(this.label1);
            this.groupBox.Location = new System.Drawing.Point(8, 3);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(559, 238);
            this.groupBox.TabIndex = 32;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Delete";
            // 
            // txtText
            // 
            this.txtText.Enabled = false;
            this.txtText.Location = new System.Drawing.Point(176, 178);
            this.txtText.Name = "txtText";
            this.txtText.Size = new System.Drawing.Size(347, 55);
            this.txtText.TabIndex = 32;
            this.txtText.Text = "";
            // 
            // chkText
            // 
            this.chkText.AutoSize = true;
            this.chkText.BackColor = System.Drawing.Color.Transparent;
            this.chkText.Location = new System.Drawing.Point(32, 180);
            this.chkText.Name = "chkText";
            this.chkText.Size = new System.Drawing.Size(138, 17);
            this.chkText.TabIndex = 31;
            this.chkText.Text = "Pages containing Text :";
            this.chkText.UseVisualStyleBackColor = false;
            this.chkText.CheckedChanged += new System.EventHandler(this.chkText_CheckedChanged);
            // 
            // chkOdd
            // 
            this.chkOdd.AutoSize = true;
            this.chkOdd.BackColor = System.Drawing.Color.Transparent;
            this.chkOdd.Location = new System.Drawing.Point(32, 102);
            this.chkOdd.Name = "chkOdd";
            this.chkOdd.Size = new System.Drawing.Size(105, 17);
            this.chkOdd.TabIndex = 30;
            this.chkOdd.Text = "Odd Pages from ";
            this.chkOdd.UseVisualStyleBackColor = false;
            // 
            // nudEveryTo
            // 
            this.nudEveryTo.Location = new System.Drawing.Point(407, 149);
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
            this.label10.Location = new System.Drawing.Point(385, 151);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(16, 13);
            this.label10.TabIndex = 28;
            this.label10.Text = "to";
            // 
            // nudEveryFrom
            // 
            this.nudEveryFrom.Location = new System.Drawing.Point(300, 149);
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
            this.label9.Location = new System.Drawing.Point(263, 151);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(30, 13);
            this.label9.TabIndex = 26;
            this.label9.Text = "from ";
            // 
            // nudTo
            // 
            this.nudTo.Location = new System.Drawing.Point(247, 78);
            this.nudTo.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudTo.Name = "nudTo";
            this.nudTo.Size = new System.Drawing.Size(80, 20);
            this.nudTo.TabIndex = 22;
            this.nudTo.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(225, 80);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(16, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "to";
            // 
            // nudFrom
            // 
            this.nudFrom.Location = new System.Drawing.Point(140, 78);
            this.nudFrom.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudFrom.Name = "nudFrom";
            this.nudFrom.Size = new System.Drawing.Size(80, 20);
            this.nudFrom.TabIndex = 20;
            this.nudFrom.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // chkPagesFromTo
            // 
            this.chkPagesFromTo.AutoSize = true;
            this.chkPagesFromTo.BackColor = System.Drawing.Color.Transparent;
            this.chkPagesFromTo.Location = new System.Drawing.Point(32, 79);
            this.chkPagesFromTo.Name = "chkPagesFromTo";
            this.chkPagesFromTo.Size = new System.Drawing.Size(82, 17);
            this.chkPagesFromTo.TabIndex = 19;
            this.chkPagesFromTo.Text = "Pages from ";
            this.chkPagesFromTo.UseVisualStyleBackColor = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(226, 151);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Pages ";
            // 
            // nudEvery
            // 
            this.nudEvery.Location = new System.Drawing.Point(140, 149);
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
            this.chkEvery.Location = new System.Drawing.Point(32, 150);
            this.chkEvery.Name = "chkEvery";
            this.chkEvery.Size = new System.Drawing.Size(56, 17);
            this.chkEvery.TabIndex = 16;
            this.chkEvery.Text = "Every ";
            this.chkEvery.UseVisualStyleBackColor = false;
            // 
            // nudEvenTo
            // 
            this.nudEvenTo.Location = new System.Drawing.Point(247, 124);
            this.nudEvenTo.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudEvenTo.Name = "nudEvenTo";
            this.nudEvenTo.Size = new System.Drawing.Size(80, 20);
            this.nudEvenTo.TabIndex = 15;
            this.nudEvenTo.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(225, 126);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "to";
            // 
            // nudEvenFrom
            // 
            this.nudEvenFrom.Location = new System.Drawing.Point(140, 124);
            this.nudEvenFrom.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudEvenFrom.Name = "nudEvenFrom";
            this.nudEvenFrom.Size = new System.Drawing.Size(80, 20);
            this.nudEvenFrom.TabIndex = 13;
            this.nudEvenFrom.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // chkEven
            // 
            this.chkEven.AutoSize = true;
            this.chkEven.BackColor = System.Drawing.Color.Transparent;
            this.chkEven.Location = new System.Drawing.Point(32, 125);
            this.chkEven.Name = "chkEven";
            this.chkEven.Size = new System.Drawing.Size(110, 17);
            this.chkEven.TabIndex = 12;
            this.chkEven.Text = "Even Pages from ";
            this.chkEven.UseVisualStyleBackColor = false;
            // 
            // nudOddTo
            // 
            this.nudOddTo.Location = new System.Drawing.Point(247, 101);
            this.nudOddTo.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudOddTo.Name = "nudOddTo";
            this.nudOddTo.Size = new System.Drawing.Size(80, 20);
            this.nudOddTo.TabIndex = 10;
            this.nudOddTo.Value = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(226, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "to";
            // 
            // nudOddFrom
            // 
            this.nudOddFrom.Location = new System.Drawing.Point(140, 101);
            this.nudOddFrom.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudOddFrom.Name = "nudOddFrom";
            this.nudOddFrom.Size = new System.Drawing.Size(80, 20);
            this.nudOddFrom.TabIndex = 8;
            this.nudOddFrom.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label4.Location = new System.Drawing.Point(32, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(253, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "( comma separated, for example : 1-15,20-30,50-80 )";
            // 
            // txtPageRanges
            // 
            this.txtPageRanges.Location = new System.Drawing.Point(128, 16);
            this.txtPageRanges.Name = "txtPageRanges";
            this.txtPageRanges.Size = new System.Drawing.Size(383, 20);
            this.txtPageRanges.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(32, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Page Ranges :";
            // 
            // niceLine1
            // 
            this.niceLine1.Location = new System.Drawing.Point(6, 58);
            this.niceLine1.Name = "niceLine1";
            this.niceLine1.Size = new System.Drawing.Size(538, 15);
            this.niceLine1.TabIndex = 36;
            // 
            // ucDeleteExtractPages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox);
            this.Name = "ucDeleteExtractPages";
            this.Size = new System.Drawing.Size(574, 258);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEveryTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEveryFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEvery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEvenTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEvenFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOddTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOddFrom)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox;
        public System.Windows.Forms.NumericUpDown nudEveryTo;
        public System.Windows.Forms.Label label10;
        public System.Windows.Forms.NumericUpDown nudEveryFrom;
        public System.Windows.Forms.Label label9;
        public System.Windows.Forms.NumericUpDown nudTo;
        public System.Windows.Forms.Label label8;
        public System.Windows.Forms.NumericUpDown nudFrom;
        public System.Windows.Forms.CheckBox chkPagesFromTo;
        public System.Windows.Forms.Label label7;
        public System.Windows.Forms.NumericUpDown nudEvery;
        public System.Windows.Forms.CheckBox chkEvery;
        public System.Windows.Forms.NumericUpDown nudEvenTo;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.NumericUpDown nudEvenFrom;
        public System.Windows.Forms.CheckBox chkEven;
        public System.Windows.Forms.NumericUpDown nudOddTo;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.NumericUpDown nudOddFrom;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtPageRanges;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.CheckBox chkOdd;
        public System.Windows.Forms.CheckBox chkText;
        public System.Windows.Forms.RichTextBox txtText;
        private NiceLine niceLine1;
    }
}
