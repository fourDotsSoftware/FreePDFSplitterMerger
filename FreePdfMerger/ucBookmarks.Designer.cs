namespace PdfMergeSplitTool
{
    partial class ucBookmarks
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
            this.chkFilenames = new System.Windows.Forms.CheckBox();
            this.chkTitles = new System.Windows.Forms.CheckBox();
            this.chkListFile = new System.Windows.Forms.CheckBox();
            this.txtExternalFile = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.chkFilePaths = new System.Windows.Forms.CheckBox();
            this.niceLine1 = new PdfMergeSplitTool.NiceLine();
            this.chkAddExisting = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chkFilenames
            // 
            this.chkFilenames.AutoSize = true;
            this.chkFilenames.BackColor = System.Drawing.Color.Transparent;
            this.chkFilenames.Location = new System.Drawing.Point(42, 31);
            this.chkFilenames.Name = "chkFilenames";
            this.chkFilenames.Size = new System.Drawing.Size(95, 17);
            this.chkFilenames.TabIndex = 0;
            this.chkFilenames.Text = "Use Filenames";
            this.chkFilenames.UseVisualStyleBackColor = false;
            this.chkFilenames.CheckedChanged += new System.EventHandler(this.chkFilenames_CheckedChanged);
            // 
            // chkTitles
            // 
            this.chkTitles.AutoSize = true;
            this.chkTitles.BackColor = System.Drawing.Color.Transparent;
            this.chkTitles.Location = new System.Drawing.Point(39, 77);
            this.chkTitles.Name = "chkTitles";
            this.chkTitles.Size = new System.Drawing.Size(125, 17);
            this.chkTitles.TabIndex = 1;
            this.chkTitles.Text = "Use Document Titles";
            this.chkTitles.UseVisualStyleBackColor = false;
            this.chkTitles.CheckedChanged += new System.EventHandler(this.chkFilenames_CheckedChanged);
            // 
            // chkListFile
            // 
            this.chkListFile.AutoSize = true;
            this.chkListFile.BackColor = System.Drawing.Color.Transparent;
            this.chkListFile.Location = new System.Drawing.Point(40, 100);
            this.chkListFile.Name = "chkListFile";
            this.chkListFile.Size = new System.Drawing.Size(124, 17);
            this.chkListFile.TabIndex = 2;
            this.chkListFile.Text = "Use External List File";
            this.chkListFile.UseVisualStyleBackColor = false;
            this.chkListFile.CheckedChanged += new System.EventHandler(this.chkFilenames_CheckedChanged);
            // 
            // txtExternalFile
            // 
            this.txtExternalFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtExternalFile.Location = new System.Drawing.Point(170, 98);
            this.txtExternalFile.Name = "txtExternalFile";
            this.txtExternalFile.ReadOnly = true;
            this.txtExternalFile.Size = new System.Drawing.Size(344, 20);
            this.txtExternalFile.TabIndex = 5;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBrowse.Location = new System.Drawing.Point(520, 96);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(139, 22);
            this.btnBrowse.TabIndex = 7;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // chkFilePaths
            // 
            this.chkFilePaths.AutoSize = true;
            this.chkFilePaths.BackColor = System.Drawing.Color.Transparent;
            this.chkFilePaths.Location = new System.Drawing.Point(41, 54);
            this.chkFilePaths.Name = "chkFilePaths";
            this.chkFilePaths.Size = new System.Drawing.Size(94, 17);
            this.chkFilePaths.TabIndex = 8;
            this.chkFilePaths.Text = "Use File Paths";
            this.chkFilePaths.UseVisualStyleBackColor = false;
            this.chkFilePaths.CheckedChanged += new System.EventHandler(this.chkFilenames_CheckedChanged);
            // 
            // niceLine1
            // 
            this.niceLine1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.niceLine1.BackColor = System.Drawing.Color.Transparent;
            this.niceLine1.Caption = "Bookmarks";
            this.niceLine1.Location = new System.Drawing.Point(17, 3);
            this.niceLine1.Name = "niceLine1";
            this.niceLine1.Size = new System.Drawing.Size(656, 15);
            this.niceLine1.TabIndex = 4;
            // 
            // chkAddExisting
            // 
            this.chkAddExisting.AutoSize = true;
            this.chkAddExisting.BackColor = System.Drawing.Color.Transparent;
            this.chkAddExisting.Checked = true;
            this.chkAddExisting.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAddExisting.Location = new System.Drawing.Point(39, 151);
            this.chkAddExisting.Name = "chkAddExisting";
            this.chkAddExisting.Size = new System.Drawing.Size(140, 17);
            this.chkAddExisting.TabIndex = 9;
            this.chkAddExisting.Text = "Add Existing Bookmarks";
            this.chkAddExisting.UseVisualStyleBackColor = false;
            // 
            // ucBookmarks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Controls.Add(this.chkAddExisting);
            this.Controls.Add(this.chkFilePaths);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtExternalFile);
            this.Controls.Add(this.niceLine1);
            this.Controls.Add(this.chkListFile);
            this.Controls.Add(this.chkTitles);
            this.Controls.Add(this.chkFilenames);
            this.Name = "ucBookmarks";
            this.Size = new System.Drawing.Size(689, 189);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.CheckBox chkFilenames;
        public System.Windows.Forms.CheckBox chkTitles;
        public System.Windows.Forms.CheckBox chkListFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        public NiceLine niceLine1;
        public System.Windows.Forms.TextBox txtExternalFile;
        public System.Windows.Forms.Button btnBrowse;
        public System.Windows.Forms.CheckBox chkFilePaths;
        public System.Windows.Forms.CheckBox chkAddExisting;
    }
}
