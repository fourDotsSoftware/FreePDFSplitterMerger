namespace PdfMergeSplitTool
{
    partial class ucOutputOther
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucOutputOther));
            this.txtOutputFolder = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.txtOutputPattern = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkSameFolder = new System.Windows.Forms.CheckBox();
            this.chkOther = new System.Windows.Forms.CheckBox();
            this.niceLine2 = new PdfMergeSplitTool.NiceLine();
            this.niceLine1 = new PdfMergeSplitTool.NiceLine();
            this.lblSplitBookmarks = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtOutputFolder
            // 
            this.txtOutputFolder.Location = new System.Drawing.Point(86, 56);
            this.txtOutputFolder.Name = "txtOutputFolder";
            this.txtOutputFolder.Size = new System.Drawing.Size(398, 20);
            this.txtOutputFolder.TabIndex = 49;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBrowse.Location = new System.Drawing.Point(490, 54);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(139, 22);
            this.btnBrowse.TabIndex = 48;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtOutputPattern
            // 
            this.txtOutputPattern.Location = new System.Drawing.Point(13, 107);
            this.txtOutputPattern.Name = "txtOutputPattern";
            this.txtOutputPattern.Size = new System.Drawing.Size(310, 20);
            this.txtOutputPattern.TabIndex = 51;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label2.Location = new System.Drawing.Point(19, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(249, 111);
            this.label2.TabIndex = 53;
            this.label2.Text = "Enter\r\n[page] for Page Number,\r\n[file] for original filename";
            // 
            // chkSameFolder
            // 
            this.chkSameFolder.AutoSize = true;
            this.chkSameFolder.Checked = true;
            this.chkSameFolder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSameFolder.Location = new System.Drawing.Point(22, 24);
            this.chkSameFolder.Name = "chkSameFolder";
            this.chkSameFolder.Size = new System.Drawing.Size(167, 17);
            this.chkSameFolder.TabIndex = 54;
            this.chkSameFolder.Text = "Same Folder as Original File(s)";
            this.chkSameFolder.UseVisualStyleBackColor = true;
            this.chkSameFolder.CheckedChanged += new System.EventHandler(this.chkSameFolder_CheckedChanged);
            // 
            // chkOther
            // 
            this.chkOther.AutoSize = true;
            this.chkOther.Location = new System.Drawing.Point(22, 58);
            this.chkOther.Name = "chkOther";
            this.chkOther.Size = new System.Drawing.Size(58, 17);
            this.chkOther.TabIndex = 55;
            this.chkOther.Text = "Other :";
            this.chkOther.UseVisualStyleBackColor = true;
            this.chkOther.CheckedChanged += new System.EventHandler(this.chkSameFolder_CheckedChanged);
            // 
            // niceLine2
            // 
            this.niceLine2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.niceLine2.BackColor = System.Drawing.Color.Transparent;
            this.niceLine2.Caption = "Output Filename Pattern";
            this.niceLine2.Location = new System.Drawing.Point(8, 84);
            this.niceLine2.Name = "niceLine2";
            this.niceLine2.Size = new System.Drawing.Size(649, 15);
            this.niceLine2.TabIndex = 52;
            // 
            // niceLine1
            // 
            this.niceLine1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.niceLine1.BackColor = System.Drawing.Color.Transparent;
            this.niceLine1.Caption = "Output Folder";
            this.niceLine1.Location = new System.Drawing.Point(3, 3);
            this.niceLine1.Name = "niceLine1";
            this.niceLine1.Size = new System.Drawing.Size(649, 15);
            this.niceLine1.TabIndex = 50;
            // 
            // lblSplitBookmarks
            // 
            this.lblSplitBookmarks.BackColor = System.Drawing.Color.Transparent;
            this.lblSplitBookmarks.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblSplitBookmarks.Location = new System.Drawing.Point(274, 146);
            this.lblSplitBookmarks.Name = "lblSplitBookmarks";
            this.lblSplitBookmarks.Size = new System.Drawing.Size(355, 111);
            this.lblSplitBookmarks.TabIndex = 56;
            this.lblSplitBookmarks.Text = resources.GetString("lblSplitBookmarks.Text");
            // 
            // ucOutputOther
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblSplitBookmarks);
            this.Controls.Add(this.chkOther);
            this.Controls.Add(this.chkSameFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.niceLine2);
            this.Controls.Add(this.txtOutputPattern);
            this.Controls.Add(this.niceLine1);
            this.Controls.Add(this.txtOutputFolder);
            this.Controls.Add(this.btnBrowse);
            this.Name = "ucOutputOther";
            this.Size = new System.Drawing.Size(639, 406);
            this.Load += new System.EventHandler(this.ucOutputOther_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public NiceLine niceLine1;
        public System.Windows.Forms.TextBox txtOutputFolder;
        public System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        public NiceLine niceLine2;
        public System.Windows.Forms.TextBox txtOutputPattern;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.CheckBox chkSameFolder;
        public System.Windows.Forms.CheckBox chkOther;
        public System.Windows.Forms.Label lblSplitBookmarks;
    }
}
