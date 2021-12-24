using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PdfMergeSplitTool
{
    public partial class ucOutputFile : UserControl
    {
        public ucOutputFile()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtOutputFile.Text = openFileDialog1.FileName;
            }
        }

        private void chkFolders_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ch = (CheckBox)sender;

            if (ch.Checked)
            {
                if (ch != chkSeparateDate) chkSeparateDate.Checked = false;
                if (ch != chkFolders) chkFolders.Checked = false;
                if (ch != chkGroup) chkGroup.Checked = false;
            }
        }
    }
}
