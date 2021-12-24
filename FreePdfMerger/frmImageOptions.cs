using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PdfMergeSplitTool
{
    public partial class frmImageOptions : PdfMergeSplitTool.CustomForm
    {
        private PageRange SelectedPageRange;

        public frmImageOptions(PageRange pagerange,string filepath)
        {
            InitializeComponent();
            SelectedPageRange = pagerange;
            txtImage.Text = filepath;
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectedPageRange.VertiAlignValue = cmbVertiAlign.SelectedIndex;
            SelectedPageRange.VertiAlign = chkVerticalAlignment.Checked;
            SelectedPageRange.ScaledWidth = (int)nudWidth.Value;
            SelectedPageRange.ScaledSize = chkScaledWidth.Checked;
            SelectedPageRange.ScaledPercetnageValue = (int)nudPercentage.Value;
            SelectedPageRange.ScaledPercentage = chkScalePercentage.Checked;
            SelectedPageRange.ScaledHeight = (int)nudHeight.Value;
            SelectedPageRange.HorizAlignValue = cmbHorizAlign.SelectedIndex;
            SelectedPageRange.HorizAlign = chkHorizontalAlignment.Checked;
            SelectedPageRange.AbsolutePosition = chkAbsolutePos.Checked;
            SelectedPageRange.AbsX = (int)nudPosX.Value;
            SelectedPageRange.AbsY = (int)nudPosY.Value;
            SelectedPageRange.ScaleToFit = chkScaleFit.Checked;
                    
                         
            
            this.DialogResult = DialogResult.OK;
        }

        private void frmImageOptions_Load(object sender, EventArgs e)
        {
            Bitmap bmp = null;

            try
            {
                bmp = FreeImageHelper.LoadImage(txtImage.Text);
                pictureBox1.Image = bmp;
                txtWidth.Text = bmp.Width.ToString();
                txtHeight.Text = bmp.Height.ToString();

            }
            catch
            {
                Module.ShowMessage("Unrecognized Image Format !");
                this.DialogResult = DialogResult.Cancel;
                return;
            }

            cmbHorizAlign.Items.Add(TranslateHelper.Translate("Left"));
            cmbHorizAlign.Items.Add(TranslateHelper.Translate("Center Horizontally"));
            cmbHorizAlign.Items.Add(TranslateHelper.Translate("Right"));
            cmbHorizAlign.Items.Add(TranslateHelper.Translate("Center"));

            cmbVertiAlign.Items.Add(TranslateHelper.Translate("Top"));
            cmbVertiAlign.Items.Add(TranslateHelper.Translate("Middle"));
            cmbVertiAlign.Items.Add(TranslateHelper.Translate("Bottom"));

            if (SelectedPageRange.ScaleToFit)
            {
                chkScaleFit.Checked = true;
            }

            if (SelectedPageRange.ScaledWidth == -1)
            {
                int thumbwidth = 550;
                int thumbheight = 600;

                int new_thumbheight2 = (int)(thumbwidth * bmp.Height / bmp.Width);
                int new_thumbwidth2 = (int)(thumbheight * bmp.Width / bmp.Height);

                if (bmp.Height <= 600 && bmp.Width <= 550)
                {
                    nudWidth.Value = bmp.Width;
                    nudHeight.Value = bmp.Height;
                }
                else
                {
                    if (new_thumbwidth2 > 550)
                    {
                        nudWidth.Value = thumbwidth;
                        nudHeight.Value = new_thumbheight2;
                    }
                    else
                    {
                        nudWidth.Value = new_thumbwidth2;
                        nudHeight.Value = thumbheight;
                    }
                }
            }
            else
            {
                nudWidth.Value = SelectedPageRange.ScaledWidth;
                nudHeight.Value = SelectedPageRange.ScaledHeight;
            }

            if (SelectedPageRange.ScaledPercetnageValue == -1)
            {
                nudPercentage.Value = 100;
            }
            else
            {
                nudPercentage.Value = SelectedPageRange.ScaledPercetnageValue;

            }
            if (SelectedPageRange.ScaledSize)
            {
                chkScaledWidth.Checked = true;
            }

            if (SelectedPageRange.ScaledPercentage)
            {
                chkScalePercentage.Checked = true;
            }

            if (SelectedPageRange.AbsX == -1)
            {
                nudPosX.Value = 0;
                nudPosY.Value = 0;
            }
            else
            {
                nudPosX.Value = SelectedPageRange.AbsX;
                nudPosY.Value = SelectedPageRange.AbsY;
            }

            cmbHorizAlign.SelectedIndex = SelectedPageRange.HorizAlignValue;
            cmbVertiAlign.SelectedIndex = SelectedPageRange.VertiAlignValue;

            chkHorizontalAlignment.Checked = SelectedPageRange.HorizAlign;
            chkVerticalAlignment.Checked = SelectedPageRange.VertiAlign;

        }

        private void chkScaledWidth_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ch = (CheckBox)sender;
            if (ch.Checked)
            {
                if (ch != chkScaledWidth) chkScaledWidth.Checked = false;
                if (ch != chkScalePercentage) chkScalePercentage.Checked = false;
                if (ch != chkScaleFit) chkScaleFit.Checked = false;
            }
            else if (!chkScaleFit.Checked && !chkScalePercentage.Checked && !chkScaledWidth.Checked)
            {
                ch.Checked = true;
            }
            
        }

        private void chkAbsolutePos_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ch = (CheckBox)sender;

            if (ch.Checked)
            {
                if (ch != chkHorizontalAlignment) chkHorizontalAlignment.Checked = false;
                if (ch != chkAbsolutePos) chkAbsolutePos.Checked = false;
            }
            else if (!chkHorizontalAlignment.Checked && !chkAbsolutePos.Checked)
            {
                ch.Checked = true;
            }
            
        }
    }
}

