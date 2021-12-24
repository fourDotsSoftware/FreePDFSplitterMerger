using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PdfMergeSplitTool
{
    public partial class ucExtractPages : UserControl
    {
        public ucExtractPages()
        {
            InitializeComponent();
        }

        private void chkText_CheckedChanged(object sender, EventArgs e)
        {
            txtText.Enabled = chkText.Checked;
        }

        private void groupBox_Enter(object sender, EventArgs e)
        {

        }
    }
}
