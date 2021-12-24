using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PdfMergeSplitTool
{
    public partial class ucSplitPages : UserControl
    {
        public ucSplitPages()
        {
            InitializeComponent();
        }

        private void chkText_CheckedChanged(object sender, EventArgs e)
        {
            txtText.Enabled = chkText.Checked;
        }
    }
}
