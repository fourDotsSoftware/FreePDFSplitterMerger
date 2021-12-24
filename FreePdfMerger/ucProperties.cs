using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PdfMergeSplitTool
{
    public partial class ucProperties : UserControl
    {
        public ucProperties()
        {
            InitializeComponent();
        }

        private void chkProperties_CheckedChanged(object sender, EventArgs e)
        {
            txtAuthor.Enabled = !chkProperties.Checked;
            txtKeywords.Enabled = !chkProperties.Checked;
            txtSubject.Enabled = !chkProperties.Checked;
            txtTitle.Enabled = !chkProperties.Checked;
        }

        /*
        private void chkPasswords_CheckedChanged(object sender, EventArgs e)
        {
            txtOwnerPassword.Enabled = !chkPasswords.Checked;
            txtUserPassword.Enabled = !chkPasswords.Checked;
        }
        */

        private void ucProperties_Load(object sender, EventArgs e)
        {
            if (this.ParentForm is frmOptions)
            {
             //   chkPasswords.Text=TranslateHelper.Translate("Keep the same as the First Document's one.");
                chkProperties.Text = TranslateHelper.Translate("Keep the same as the First Document's one.");
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
}
