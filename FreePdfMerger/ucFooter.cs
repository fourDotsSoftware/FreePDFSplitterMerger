using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PdfMergeSplitTool
{
    public partial class ucFooter : UserControl
    {
        public ucFooter()
        {
            InitializeComponent();
        }

        private bool _unlimited_end = false;

        public ucFooter(bool unlimited_end)
        {
            InitializeComponent();
            _unlimited_end = unlimited_end;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = Module.ImageFilter;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFooterImage.Text = openFileDialog1.FileName;
            }
        }

        private void chkTextPositionLeft_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ch = (CheckBox)sender;

            if (ch.Checked)
            {
                if (ch != chkTextPositionLeft) chkTextPositionLeft.Checked = false;
                if (ch != chkTextPositionRight) chkTextPositionRight.Checked = false;
                if (ch != chkTextPositionCenter) chkTextPositionCenter.Checked = false;

            }
        }

        private void chkImagePositionLeft_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ch = (CheckBox)sender;

            if (ch.Checked)
            {
                if (ch != chkImagePositionLeft) chkImagePositionLeft.Checked = false;
                if (ch != chkImagePositionRight) chkImagePositionRight.Checked = false;
                if (ch != chkImagePositionCenter) chkImagePositionCenter.Checked = false;

            }
        }

        private void ucFooter_Load(object sender, EventArgs e)
        {
            if (!_unlimited_end)
            {
                if (frmMain.Instance != null)
                {
                    nudFrom.Maximum = int.Parse(frmMain.Instance.txtTotalPages.Text);
                    nudFrom.Minimum = 1;

                    nudTo.Maximum = int.Parse(frmMain.Instance.txtTotalPages.Text);
                    nudTo.Minimum = 1;

                    nudFrom.Value = 1;
                    nudTo.Value = int.Parse(frmMain.Instance.txtTotalPages.Text);
                }
            }
            else
            {
                nudFrom.Maximum = 100000;
                nudFrom.Value = 1;
                nudTo.Maximum = 100000;
                nudTo.Value = 10000;
            }
        }
    }
}
