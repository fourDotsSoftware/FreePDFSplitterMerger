namespace PdfMergeSplitTool
{
    partial class ucMisc
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
            this.chkDeleteOriginals = new System.Windows.Forms.CheckBox();
            this.chkOpenDestinationFolder = new System.Windows.Forms.CheckBox();
            this.niceLine1 = new PdfMergeSplitTool.NiceLine();
            this.SuspendLayout();
            // 
            // chkDeleteOriginals
            // 
            this.chkDeleteOriginals.AutoSize = true;
            this.chkDeleteOriginals.BackColor = System.Drawing.Color.Transparent;
            this.chkDeleteOriginals.Location = new System.Drawing.Point(31, 43);
            this.chkDeleteOriginals.Name = "chkDeleteOriginals";
            this.chkDeleteOriginals.Size = new System.Drawing.Size(227, 17);
            this.chkDeleteOriginals.TabIndex = 25;
            this.chkDeleteOriginals.Text = "Delete Originals after Operation completes.";
            this.chkDeleteOriginals.UseVisualStyleBackColor = false;
            // 
            // chkOpenDestinationFolder
            // 
            this.chkOpenDestinationFolder.AutoSize = true;
            this.chkOpenDestinationFolder.BackColor = System.Drawing.Color.Transparent;
            this.chkOpenDestinationFolder.Checked = true;
            this.chkOpenDestinationFolder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOpenDestinationFolder.Location = new System.Drawing.Point(31, 66);
            this.chkOpenDestinationFolder.Name = "chkOpenDestinationFolder";
            this.chkOpenDestinationFolder.Size = new System.Drawing.Size(267, 17);
            this.chkOpenDestinationFolder.TabIndex = 26;
            this.chkOpenDestinationFolder.Text = "Open Destination Folder after Operation completes.";
            this.chkOpenDestinationFolder.UseVisualStyleBackColor = false;
            // 
            // niceLine1
            // 
            this.niceLine1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.niceLine1.BackColor = System.Drawing.Color.Transparent;
            this.niceLine1.Caption = "Misc";
            this.niceLine1.Location = new System.Drawing.Point(14, 6);
            this.niceLine1.Name = "niceLine1";
            this.niceLine1.Size = new System.Drawing.Size(500, 15);
            this.niceLine1.TabIndex = 24;
            // 
            // ucMisc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkOpenDestinationFolder);
            this.Controls.Add(this.chkDeleteOriginals);
            this.Controls.Add(this.niceLine1);
            this.Name = "ucMisc";
            this.Size = new System.Drawing.Size(530, 269);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.CheckBox chkDeleteOriginals;
        public NiceLine niceLine1;
        public System.Windows.Forms.CheckBox chkOpenDestinationFolder;

    }
}
