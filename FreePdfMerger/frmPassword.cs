using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PdfMergeSplitTool
{
    public partial class frmPassword : PdfMergeSplitTool.CustomForm
    {
        public frmPassword(string filepath)
        {
            InitializeComponent();
            txtPdfFile.Text = filepath;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == "")
            {
                Module.ShowMessage("Please insert a valid Password !");
                return;

            }
            this.DialogResult = DialogResult.OK;
        }

        private void frmPassword_Load(object sender, EventArgs e)
        {

        }
    }
}

