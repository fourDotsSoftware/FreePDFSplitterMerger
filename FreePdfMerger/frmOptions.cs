using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PdfMergeSplitTool
{
    public partial class frmOptions : PdfMergeSplitTool.CustomForm
    {
        public static frmOptions Instance = null;

        public frmOptions()
        {
            InitializeComponent();
            Instance = this;
        }

        public ucBookmarks Bookmarks = new ucBookmarks();
        public ucFooter Footer = new ucFooter();
        public ucHeader Header = new ucHeader();
        public ucMisc Misc = new ucMisc();
        public ucProperties Properties = new ucProperties();
        public ucOutputOther OutputFile = new ucOutputOther();

        private void frmOptions_Load(object sender, EventArgs e)
        {
            lstSteps.Items.Add(TranslateHelper.Translate("Output File"));
            lstSteps.Items.Add(TranslateHelper.Translate("Bookmarks"));
            lstSteps.Items.Add(TranslateHelper.Translate("Header"));
            lstSteps.Items.Add(TranslateHelper.Translate("Footer"));
            lstSteps.Items.Add(TranslateHelper.Translate("Properties & Passwords"));
            lstSteps.Items.Add(TranslateHelper.Translate("Misc"));

            OutputFile.BackColor = Color.Transparent;
            Bookmarks.BackColor = Color.Transparent;
            Footer.BackColor = Color.Transparent;
            Header.BackColor = Color.Transparent;
            Misc.BackColor = Color.Transparent;
            Properties.BackColor = Color.Transparent;

            this.Controls.Add(OutputFile);
            this.Controls.Add(Bookmarks);
            this.Controls.Add(Footer);
            this.Controls.Add(Header);
            this.Controls.Add(Misc);
            this.Controls.Add(Properties);

            HideAllSteps();

            lstSteps.SelectedIndex = 0;
            lstSteps_SelectedIndexChanged(null, null);

            /*
            OutputFile.txtOutputFile.Text=
                System.IO.Path.Combine(System.IO.Path.GetDirectoryName(frmMain.Instance.lvDocs.Items[0].SubItems[2].Text),               
                System.IO.Path.GetFileNameWithoutExtension(frmMain.Instance.lvDocs.Items[0].SubItems[2].Text) + ".merged.pdf");
            */

            OutputFile.txtOutputPattern.Text = "[file].merged.pdf";
            OutputFile.txtOutputFolder.Text = System.IO.Path.GetDirectoryName(frmMain.Instance.dtDocs.Rows[0]["cfilepath"].ToString());

            Properties.txtUserPassword.Text = frmMain.Instance.dtDocs.Rows[0]["cpassword"].ToString();
        }

        private void HideAllSteps()
        {
            OutputFile.Visible = false;
            Bookmarks.Visible = false;
            Footer.Visible = false;
            Header.Visible = false;
            Misc.Visible = false;
            Properties.Visible = false;

            OutputFile.Top = 0;
            Bookmarks.Top = 0;
            Footer.Top = 0;
            Header.Top = 0;
            Misc.Top = 0;
            Properties.Top = 0;
        }

        private void lstSteps_SelectedIndexChanged(object sender, EventArgs e)
        {
            HideAllSteps();

            switch (lstSteps.SelectedIndex)
            {
                case 0:
                    OutputFile.Left = lstSteps.Right;
                    OutputFile.Visible = true;
                    break;
                case 1 :
                    Bookmarks.Left = lstSteps.Right;
                    Bookmarks.Visible = true;
                    break;
                case 2:
                    Header.Left = lstSteps.Right;
                    Header.Visible = true;
                    break;
                case 3:
                    Footer.Left = lstSteps.Right;
                    Footer.Visible = true;
                    break;
                case 4:
                    Properties.Left = lstSteps.Right;
                    Properties.Visible = true;
                    break;
                case 5:
                    Misc.Left = lstSteps.Right;
                    Misc.Visible = true;
                    break;
                

            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (lstSteps.SelectedIndex == lstSteps.Items.Count - 1)
            {
                return;
            }
            else
            {
                lstSteps.SelectedIndex++;
                lstSteps_SelectedIndexChanged(null, null);
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            

            if (lstSteps.SelectedIndex == 0)
            {
                return;
            }
            else
            {
                lstSteps.SelectedIndex--;
                lstSteps_SelectedIndexChanged(null, null);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (OutputFile.chkOther.Checked && !System.IO.Directory.Exists(OutputFile.txtOutputFolder.Text))
            {
                Module.ShowMessage("Please specify a valid Output Folder !");
                return;
            }

            if (OutputFile.txtOutputPattern.Text.Trim() == String.Empty)
            {
                Module.ShowMessage("Please specify a valid Output Filename !");
                return;
            }


            try
            {
                this.Cursor = Cursors.WaitCursor;
                bool suc=MergeHelper.Merge();
                this.Cursor = null;

                if (suc)
                {
                    Module.ShowMessage("Operation completed successfully !");
                }
                else
                {
                    Module.ShowMessage("Operation completed with Errors !");
                }

            }
            catch (Exception ex)
            {
                this.Cursor = null;
                Module.ShowError("Operation completed with Errors !", ex);
            }
            finally
            {
                this.Cursor = null;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}

