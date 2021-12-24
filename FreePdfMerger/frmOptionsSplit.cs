using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PdfMergeSplitTool
{
    public partial class frmOptionsSplit : PdfMergeSplitTool.frmOptionsParent
    {
        public static frmOptionsSplit Instance = null;

        public ucBookmarks Bookmarks = new ucBookmarks(true);
        public ucFooter Footer = new ucFooter(true);
        public ucHeader Header = new ucHeader(true);
        public ucProperties Properties = new ucProperties();
        public ucOutputOther OutputFile = new ucOutputOther();
        public ucMisc Misc = new ucMisc();
        public ucSplitPages SplitPages = new ucSplitPages();

        public frmOptionsSplit()
        {
            InitializeComponent();

            Instance = this;
        }

        private void frmOptionsSplit_Load(object sender, EventArgs e)
        {
            lstSteps.Items.Add(TranslateHelper.Translate("Split Pages"));
            lstSteps.Items.Add(TranslateHelper.Translate("Output File"));
            lstSteps.Items.Add(TranslateHelper.Translate("Bookmarks"));
            lstSteps.Items.Add(TranslateHelper.Translate("Header"));
            lstSteps.Items.Add(TranslateHelper.Translate("Footer"));
            lstSteps.Items.Add(TranslateHelper.Translate("Properties & Passwords"));
            lstSteps.Items.Add(TranslateHelper.Translate("Misc"));

            SplitPages.BackColor = Color.Transparent;
            OutputFile.BackColor = Color.Transparent;
            Bookmarks.BackColor = Color.Transparent;
            Footer.BackColor = Color.Transparent;
            Header.BackColor = Color.Transparent;
            Misc.BackColor = Color.Transparent;
            Properties.BackColor = Color.Transparent;

            this.Controls.Add(SplitPages);
            this.Controls.Add(OutputFile);
            this.Controls.Add(Bookmarks);
            this.Controls.Add(Footer);
            this.Controls.Add(Header);
            this.Controls.Add(Misc);
            this.Controls.Add(Properties);

            HideAllSteps();

            lstSteps.SelectedIndex = 0;
            lstSteps_SelectedIndexChanged(null, null);

            OutputFile.txtOutputPattern.Text = "[file].Page[page].pdf";

            OutputFile.txtOutputFolder.Text = System.IO.Path.GetDirectoryName(frmMain.Instance.dtSplit.Rows[0]["cfilepath"].ToString());

            Header.chkContinueNumbering.Visible = false;
            Footer.chkContinueNumbering.Visible = false;
        }

        private void HideAllSteps()
        {
            SplitPages.Visible = false;
            OutputFile.Visible = false;
            Bookmarks.Visible = false;
            Footer.Visible = false;
            Header.Visible = false;
            Misc.Visible = false;
            Properties.Visible = false;

            SplitPages.Top = 0;
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
                    SplitPages.Left = lstSteps.Right;
                    SplitPages.Visible = true;
                    break;

                case 1:
                    OutputFile.Left = lstSteps.Right;
                    OutputFile.Visible = true;
                    break;
                case 2:
                    Bookmarks.Left = lstSteps.Right;
                    Bookmarks.Visible = true;
                    break;
                case 3:
                    Header.Left = lstSteps.Right;
                    Header.Visible = true;
                    break;
                case 4:
                    Footer.Left = lstSteps.Right;
                    Footer.Visible = true;
                    break;
                case 5:
                    Properties.Left = lstSteps.Right;
                    Properties.Visible = true;
                    break;
                case 6:
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
                bool suc = SplitHelper.Split();
                
                this.Cursor = null;

                if (suc)
                {
                    Module.ShowMessage("Operation completed successfully !");
                    this.DialogResult = DialogResult.OK;
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

