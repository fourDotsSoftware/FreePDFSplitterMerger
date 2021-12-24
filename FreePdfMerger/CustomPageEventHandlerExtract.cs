using System;
using System.Collections.Generic;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace PdfMergeSplitTool
{
    class CustomPageEventHandlerExtract : PdfPageEventHelper
    {

        /*
         * We use a __single__ Image instance that's assigned __once__;
         * the image bytes added **ONCE** to the PDF file. If you create 
         * separate Image instances in OnEndPage()/OnEndPage(), for example,
         * you'll end up with a much bigger file size.
         */

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            if (!ExtractHelper.addfooter && !ExtractHelper.addfooterimage && !ExtractHelper.addheader
                && !ExtractHelper.addheaderimage)
            {
                return;
            }

            if ((ExtractHelper.addheader || ExtractHelper.addheaderimage) && (ExtractHelper.ShouldAddHeader(ExtractHelper.CurrentPage)
                || ExtractHelper.ShouldAddHeaderImage(ExtractHelper.CurrentPage)))
            {
                ExtractHelper.CurrentPageHeader++;
                PdfPTable head = null;

                if (ExtractHelper.addheader && ExtractHelper.addheaderimage)
                {
                    head = new PdfPTable(2);
                }
                else
                {
                    head = new PdfPTable(1);
                }

                // cell height 
                float cellHeight = document.TopMargin;
                // PDF document size      
                Rectangle page = document.PageSize;
                // create two column table

                head.TotalWidth = page.Width - 2 * document.LeftMargin;

                // add image; PdfPCell() overload sizes image to fit cell

                PdfPCell c2h = null;
                PdfPCell ch = null;

                if (ExtractHelper.addheader)
                {
                    // add the header text
                    c2h = new PdfPCell(new Phrase(ExtractHelper.HeaderText.Replace("[page]", ExtractHelper.CurrentPageHeader.ToString())
                    .Replace("[PAGE]", ExtractHelper.CurrentPageHeader.ToString()),
                      new Font(Font.FontFamily.COURIER, 8))
                    );
                    c2h.Border = PdfPCell.NO_BORDER;
                    c2h.VerticalAlignment = Element.ALIGN_MIDDLE;

                    if (frmOptionsExtract.Instance.Header.chkTextPositionCenter.Checked)
                    {
                        c2h.HorizontalAlignment = Element.ALIGN_CENTER;
                    }
                    else if (frmOptionsExtract.Instance.Header.chkTextPositionLeft.Checked)
                    {
                        c2h.HorizontalAlignment = Element.ALIGN_LEFT;
                    }
                    if (frmOptionsExtract.Instance.Header.chkTextPositionRight.Checked)
                    {
                        c2h.HorizontalAlignment = Element.ALIGN_RIGHT;
                    }


                    c2h.FixedHeight = cellHeight;

                }


                if (ExtractHelper.addheaderimage)
                {
                    ch = new PdfPCell(ExtractHelper.HeaderImage, true);
                    //c.VerticalAlignment = Element.ALIGN_TOP;
                    if (frmOptionsExtract.Instance.Header.chkImagePositionCenter.Checked)
                    {
                        ch.HorizontalAlignment = Element.ALIGN_CENTER;
                    }
                    else if (frmOptionsExtract.Instance.Header.chkImagePositionLeft.Checked)
                    {
                        ch.HorizontalAlignment = Element.ALIGN_LEFT;
                    }
                    if (frmOptionsExtract.Instance.Header.chkImagePositionRight.Checked)
                    {
                        ch.HorizontalAlignment = Element.ALIGN_RIGHT;
                    }

                    ch.FixedHeight = cellHeight;
                    ch.Border = PdfPCell.NO_BORDER;


                }

                if (c2h != null && ch != null)
                {
                    if (frmOptionsExtract.Instance.Header.chkTextPositionLeft.Checked)
                    {
                        head.AddCell(c2h);

                        if (frmOptionsExtract.Instance.Header.chkImagePositionCenter.Checked)
                        {
                            ch.HorizontalAlignment = Element.ALIGN_LEFT;
                        }

                        head.AddCell(ch);
                    }
                    else if (frmOptionsExtract.Instance.Header.chkTextPositionRight.Checked)
                    {
                        head.AddCell(ch);

                        if (frmOptionsExtract.Instance.Header.chkImagePositionCenter.Checked)
                        {
                            ch.HorizontalAlignment = Element.ALIGN_RIGHT;
                        }

                        head.AddCell(c2h);
                    }
                    else if (frmOptionsExtract.Instance.Header.chkTextPositionCenter.Checked)
                    {
                        c2h.HorizontalAlignment = Element.ALIGN_RIGHT;

                        head.AddCell(c2h);

                        if (frmOptionsExtract.Instance.Header.chkImagePositionCenter.Checked)
                        {
                            ch.HorizontalAlignment = Element.ALIGN_LEFT;
                        }

                        head.AddCell(ch);
                    }
                }
                else if (ch != null)
                {
                    head.AddCell(ch);
                }
                else if (c2h != null)
                {
                    head.AddCell(c2h);
                }

                // since the table header is implemented using a PdfPTable, we call
                // WriteSelectedRows(), which requires absolute positions!
                head.WriteSelectedRows(
                  0, -1,  // first/last row; -1 flags all write all rows
                  document.LeftMargin,      // left offset
                    // ** bottom** yPos of the table
                    //page.Height - cellHeight + head.TotalHeight,
                  page.Height - cellHeight + head.TotalHeight,

                  writer.DirectContent

                );

            }

            if ((ExtractHelper.addfooter || ExtractHelper.addfooterimage) && (ExtractHelper.ShouldAddFooter(ExtractHelper.CurrentPage)
                || ExtractHelper.ShouldAddFooterImage(ExtractHelper.CurrentPage)))
            {
                ExtractHelper.CurrentPageFooter++;

                // cell height 
                float cellHeight = document.BottomMargin;
                // PDF document size      
                Rectangle page = document.PageSize;

                PdfPCell c2h = null;
                PdfPCell ch = null;

                // create two column table
                PdfPTable head = null;
                if (ExtractHelper.addfooter && ExtractHelper.addfooterimage)
                {
                    head = new PdfPTable(2);
                }
                else
                {
                    head = new PdfPTable(1);
                }

                head.TotalWidth = page.Width - 2 * document.LeftMargin;

                // add image; PdfPCell() overload sizes image to fit cell

                if (ExtractHelper.addfooterimage)
                {
                    ch = new PdfPCell(ExtractHelper.FooterImage, true);
                    //c.VerticalAlignment = Element.ALIGN_TOP;
                    if (frmOptionsExtract.Instance.Footer.chkImagePositionCenter.Checked)
                    {
                        ch.HorizontalAlignment = Element.ALIGN_CENTER;
                    }
                    else if (frmOptionsExtract.Instance.Footer.chkImagePositionLeft.Checked)
                    {
                        ch.HorizontalAlignment = Element.ALIGN_LEFT;
                    }
                    if (frmOptionsExtract.Instance.Footer.chkImagePositionRight.Checked)
                    {
                        ch.HorizontalAlignment = Element.ALIGN_RIGHT;
                    }

                    ch.FixedHeight = cellHeight;
                    ch.Border = PdfPCell.NO_BORDER;

                    //head.AddCell(c);
                }



                if (ExtractHelper.addfooter)
                {
                    // add the header text
                    c2h = new PdfPCell(new Phrase(ExtractHelper.FooterText.Replace("[page]", ExtractHelper.CurrentPageFooter.ToString())
                    .Replace("[PAGE]", ExtractHelper.CurrentPageFooter.ToString()),
                      new Font(Font.FontFamily.COURIER, 8))
                    );
                    c2h.Border = PdfPCell.NO_BORDER;
                    c2h.VerticalAlignment = Element.ALIGN_MIDDLE;

                    if (frmOptionsExtract.Instance.Footer.chkTextPositionCenter.Checked)
                    {
                        c2h.HorizontalAlignment = Element.ALIGN_CENTER;
                    }
                    else if (frmOptionsExtract.Instance.Footer.chkTextPositionLeft.Checked)
                    {
                        c2h.HorizontalAlignment = Element.ALIGN_LEFT;
                    }
                    if (frmOptionsExtract.Instance.Footer.chkTextPositionRight.Checked)
                    {
                        c2h.HorizontalAlignment = Element.ALIGN_RIGHT;
                    }

                    c2h.FixedHeight = cellHeight;

                    head.AddCell(c2h);
                }

                if (c2h != null && ch != null)
                {
                    if (frmOptionsExtract.Instance.Header.chkTextPositionLeft.Checked)
                    {
                        head.AddCell(c2h);

                        if (frmOptionsExtract.Instance.Header.chkImagePositionCenter.Checked)
                        {
                            ch.HorizontalAlignment = Element.ALIGN_LEFT;
                        }

                        head.AddCell(ch);
                    }
                    else if (frmOptionsExtract.Instance.Header.chkTextPositionRight.Checked)
                    {
                        head.AddCell(ch);

                        if (frmOptionsExtract.Instance.Header.chkImagePositionCenter.Checked)
                        {
                            ch.HorizontalAlignment = Element.ALIGN_RIGHT;
                        }

                        head.AddCell(c2h);
                    }
                    else if (frmOptionsExtract.Instance.Header.chkTextPositionCenter.Checked)
                    {
                        c2h.HorizontalAlignment = Element.ALIGN_RIGHT;

                        head.AddCell(c2h);

                        if (frmOptionsExtract.Instance.Header.chkImagePositionCenter.Checked)
                        {
                            ch.HorizontalAlignment = Element.ALIGN_LEFT;
                        }

                        head.AddCell(ch);
                    }
                }
                else if (ch != null)
                {
                    head.AddCell(ch);
                }
                else if (c2h != null)
                {
                    head.AddCell(c2h);
                }

                // since the table header is implemented using a PdfPTable, we call
                // WriteSelectedRows(), which requires absolute positions!
                head.WriteSelectedRows(
                  0, -1,  // first/last row; -1 flags all write all rows
                  document.LeftMargin,      // left offset
                    // ** bottom** yPos of the table
                    //page.Height-(page.Height-cellHeight), //+ head.TotalHeight,
                  document.BottomMargin,

                  //head.TotalHeight,
                  writer.DirectContent

                );



            }



        }

    }

}
