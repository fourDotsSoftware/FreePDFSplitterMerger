using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PdfMergeSplitTool
{
    public partial class ucBookmarks : UserControl
    {      
        public ucBookmarks()
        {
            InitializeComponent();

            chkAddExisting.Checked = Properties.Settings.Default.OptAddExistingBookmarks;
        }

        public ucBookmarks(bool only_add_existing)            
        {
            InitializeComponent();

            chkAddExisting.Checked = Properties.Settings.Default.OptAddExistingBookmarks;

            txtExternalFile.Visible = false;
            btnBrowse.Visible = false;
            chkFilenames.Visible = false;
            chkFilePaths.Visible = false;
            chkListFile.Visible = false;
            chkTitles.Visible = false;
            
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtExternalFile.Text = openFileDialog1.FileName;
            }
        }

        private void chkFilenames_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ch = (CheckBox)sender;

            if (ch.Checked)
            {
                /*
                if (ch != chkFilenames) chkFilenames.Checked = false;
                if (ch != chkListFile) chkListFile.Checked = false;
                //if (ch != chkNoBookmarks) chkNoBookmarks.Checked = false;
                if (ch != chkTitles) chkTitles.Checked = false;
                if (ch != chkFilePaths) chkFilePaths.Checked = false;
                */

            }
        }
    }
}
