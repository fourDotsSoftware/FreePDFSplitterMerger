using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using iTextSharp;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text;

namespace PdfMergeSplitTool
{
    public partial class frmDocOptions : PdfMergeSplitTool.CustomForm
    {
        private PageRange SelectedPageRange;
        private string Filepath = "";
        private int NumberOfPages = -1;
        public string Password = "";

        public frmDocOptions(PageRange pr,string filepath,int numpages,string password)
        {
            InitializeComponent();
            SelectedPageRange = pr;
            Filepath = filepath;
            NumberOfPages = numpages;
            Password = password;
        }

        private void frmDocOptions_Load(object sender, EventArgs e)
        {
            txtDocument.Text = Filepath;
            nudOddFrom.Increment = 2;
            nudOddTo.Increment = 2;
            nudEvenFrom.Increment = 2;
            nudEvenTo.Increment = 2;

                           
                nudEvenTo.Maximum = NumberOfPages;

                if (NumberOfPages > 1)
                {
                    nudEvenFrom.Value = 2;
                }
                else
                {
                    chkEven.Enabled = false;
                    chkEven.Checked = false;
                }

                if (NumberOfPages % 2 == 0)
                {
                    nudEvenTo.Value = NumberOfPages;
                }
                else
                {
                    nudEvenTo.Value = NumberOfPages - 1;
                    
                }

                nudEvenTo.Maximum = NumberOfPages;
                nudEvenFrom.Maximum = NumberOfPages;

                nudOddTo.Maximum = NumberOfPages;

                nudFrom.Value = 1;
                nudOddFrom.Value = 1;

                if (NumberOfPages % 2 == 0)
                {
                    nudOddTo.Value = NumberOfPages - 1;
                }
                else
                {
                    nudOddTo.Value = NumberOfPages;
                }

                nudOddTo.Maximum = NumberOfPages;
                nudOddFrom.Maximum = NumberOfPages;
                nudFrom.Maximum = NumberOfPages;
                nudTo.Maximum = NumberOfPages;

                nudTo.Value = NumberOfPages;
                

                nudEvery.Maximum = NumberOfPages;

                if (NumberOfPages > 1)
                {
                    nudEvery.Value = 2;
                }
                else
                {
                    chkEvery.Enabled = false;
                    chkEvery.Checked = false;
                }
            
            
                nudEveryFrom.Maximum = NumberOfPages;
                nudEveryTo.Maximum = NumberOfPages;

                nudEveryFrom.Value = nudFrom.Value;
                nudEveryTo.Value = nudTo.Value;
                
                txtPages.Text = NumberOfPages.ToString();
                SelectedPageRange.NumberOfPages = NumberOfPages;

               
            
            
            if (SelectedPageRange.Initialized)
            {
            
                txtPages.Text = SelectedPageRange.NumberOfPages.ToString();
                nudEvenFrom.Value = SelectedPageRange.PagesEvenFrom;
                nudEvenTo.Value = SelectedPageRange.PagesEvenTo;
                nudEvery.Value = SelectedPageRange.PagesEveryValue;
                nudEveryFrom.Value = SelectedPageRange.PagesEveryFrom;
                nudEveryTo.Value = SelectedPageRange.PagesEveryTo;
                nudFrom.Value = SelectedPageRange.PagesFrom;
                nudTo.Value = SelectedPageRange.PagesTo;
                nudOddFrom.Value = SelectedPageRange.PagesOddFrom;
                nudOddTo.Value = SelectedPageRange.PagesOddTo;

                chkEven.Checked=SelectedPageRange.PagesEven;
                chkEvery.Checked=SelectedPageRange.PagesEvery;
                chkOdd.Checked=SelectedPageRange.PagesOdd;
                chkPagesFromTo.Checked = SelectedPageRange.Pages;
                chkAllPages.Checked = SelectedPageRange.AllPages;

                txtPageRanges.Text = SelectedPageRange.PageRanges;

                chkAllPages_CheckedChanged(null, null);
            }
        }

        private void nudOddFrom_Validating(object sender, CancelEventArgs e)
        {
            NumericUpDown nu = (NumericUpDown)sender;

            if (nu.Value % 2 == 0)
            {
                Module.ShowMessage("Please insert an odd number !");
                e.Cancel = true;
            }
        }

        private void nudEvenFrom_Validating(object sender, CancelEventArgs e)
        {
            NumericUpDown nu = (NumericUpDown)sender;

            if (nu.Value % 2 != 0)
            {
                Module.ShowMessage("Please insert an even number !");
                e.Cancel = true;
            }
        }

        private bool ValidatePageRanges()
        {
            if (txtPageRanges.Text.Trim() == String.Empty) return true;

            try
            {
                string[] ranges = txtPageRanges.Text.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                for (int k = 0; k < ranges.Length; k++)
                {
                    string[] range = ranges[k].Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);

                    int ifrom = int.Parse(range[0]);
                    int ito = int.Parse(range[1]);

                    int ipages = int.Parse(txtPages.Text);
                    if (ifrom > 0 && ifrom <= ipages)
                    {

                    }
                    else
                    {
                        return false;
                    }

                    if (ito > 0 && ito >= ifrom)
                    {

                    }
                    else
                    {
                        return false;
                    }



                }
            }
            catch
            {
                return false;
            }

            return true;
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!ValidatePageRanges())
            {
                Module.ShowMessage("Invalid Page Ranges !");
                txtPageRanges.Focus();
                txtPageRanges.SelectAll();
                return;
            }


            SelectedPageRange.Initialized = true;

            SelectedPageRange.NumberOfPages=int.Parse(txtPages.Text);
            SelectedPageRange.PagesEvenFrom=(int)nudEvenFrom.Value;
            SelectedPageRange.PagesEvenTo = (int)nudEvenTo.Value;
            SelectedPageRange.PagesEveryValue = (int)nudEvery.Value;
            SelectedPageRange.PagesEveryFrom = (int)nudEveryFrom.Value;
            SelectedPageRange.PagesEveryTo = (int)nudEveryTo.Value;
            SelectedPageRange.PagesFrom = (int)nudFrom.Value;
            SelectedPageRange.PagesTo = (int)nudTo.Value;
            SelectedPageRange.PagesOddFrom = (int)nudOddFrom.Value;
            SelectedPageRange.PagesOddTo = (int)nudOddTo.Value;

            SelectedPageRange.PagesEven = chkEven.Checked;
            SelectedPageRange.PagesEvery = chkEvery.Checked;
            SelectedPageRange.PagesOdd = chkOdd.Checked;
            SelectedPageRange.Pages = chkPagesFromTo.Checked;

            SelectedPageRange.AllPages = chkAllPages.Checked;

            SelectedPageRange.PagesContainingText = chkText.Checked;
            SelectedPageRange.PageText = "";

            SelectedPageRange.IncludedNumberOfPages = 0;

            if (txtPageRanges.Text.Trim() != String.Empty)
            {
                SelectedPageRange.PageRanges=txtPageRanges.Text.Trim();
            }


            PdfReader reader = null;

            if (chkText.Checked)
            {
                
                try
                {
                    if (Password == string.Empty)
                    {
                        reader = new PdfReader(Filepath);
                    }
                    else
                    {
                        reader = new PdfReader(Filepath,Encoding.Default.GetBytes(Password));
                    }
                }
                catch (iTextSharp.text.pdf.BadPasswordException pex)
                {
                //if (reader.IsEncrypted())
                //{
                //    reader.Close();
                    bool berror = false;

                    while (true)
                    {
                        try
                        {
                            if (frmMain.Instance.DefaultPassword == "" || berror)
                            {
                                frmPassword f2 = new frmPassword(Filepath);
                                DialogResult dres = f2.ShowDialog();

                                if (dres == DialogResult.OK)
                                {
                                    reader = new PdfReader(Filepath, Encoding.Default.GetBytes(f2.txtPassword.Text));

                                    if (f2.chkPassword.Checked)
                                    {
                                        frmMain.Instance.DefaultPassword = f2.txtPassword.Text;
                                    }

                                    Password = frmMain.Instance.DefaultPassword;
                                    break;
                                }
                                else if (dres == DialogResult.Cancel)
                                {
                                    Module.ShowMessage("Unable to Decrypt Document ! - " + Filepath);
                                    return;
                                }
                            }
                            else
                            {
                                reader = new PdfReader(Filepath, Encoding.Default.GetBytes(frmMain.Instance.DefaultPassword));
                                Password = frmMain.Instance.DefaultPassword;
                                break;
                            }
                        }
                        catch 
                        {
                            Module.ShowMessage("Unable to Decrypt Document ! - " + Filepath);
                            //Password = "";
                            berror = true;
                            reader = null;

                            return;
                        }
                    }
                }
            }

            for (int k = 1; k <= SelectedPageRange.NumberOfPages; k++)
            {               
                if (MergeHelper.ShouldAddPage(k,SelectedPageRange,reader))
                {
                    SelectedPageRange.IncludedNumberOfPages++;
                }
            }

            frmMain.Instance.SetupTotalPages();

            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void chkAllPages_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllPages.Checked)
            {
                groupBox2.Enabled = false;
            }
            else
            {
                groupBox2.Enabled = true;
            }
        }

        private void txtPageRanges_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtPageRanges.Text.Trim() != String.Empty)
                {
                    string[] ranges = txtPageRanges.Text.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                    for (int k = 0; k < ranges.Length; k++)
                    {
                        string[] range = ranges[k].Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);

                        int ifrom = int.Parse(range[0]);
                        int ito = int.Parse(range[1]);                        
                    }
                }
            }
            catch
            {
                Module.ShowMessage("Please insert valid page ranges, comma separated, e.g. 15-20,25-40,50-60");
                e.Cancel = true;
            }
        }

        private void chkText_CheckedChanged(object sender, EventArgs e)
        {
            txtText.Enabled = chkText.Checked;
        }
    }

    
}

