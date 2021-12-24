using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PdfMergeSplitTool
{
    public partial class ucOutputOther : UserControl
    {
        public ucOutputOther()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = "";

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtOutputFolder.Text = folderBrowserDialog1.SelectedPath;
                chkOther.Checked = true;
                chkSameFolder.Checked = false;
            }
        }

        private void chkSameFolder_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ch = (CheckBox)sender;

            if (ch.Checked)
            {
                if (ch != chkOther) chkOther.Checked = false;
                if (ch != chkSameFolder) chkSameFolder.Checked = false;
            }

            if (!chkOther.Checked && !chkSameFolder.Checked)
            {
                ch.Checked = true;
            }
        }

        private void ucOutputOther_Load(object sender, EventArgs e)
        {
            if (!(this.TopLevelControl is frmOptionsSplit))
            {
                lblSplitBookmarks.Visible = false;
            }
        }
    }
}
