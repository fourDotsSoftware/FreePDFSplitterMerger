using System;
using System.Collections.Generic;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace PdfMergeSplitTool
{
    class CustomPageEventHandlerDelete : PdfPageEventHelper
    {

        /*
         * We use a __single__ Image instance that's assigned __once__;
         * the image bytes added **ONCE** to the PDF file. If you create 
         * separate Image instances in OnEndPage()/OnEndPage(), for example,
         * you'll end up with a much bigger file size.
         */

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            if (!DeleteHelper.addfooter && !DeleteHelper.addfooterimage && !DeleteHelper.addheader
                && !DeleteHelper.addheaderimage)
            {
                return;
            }

            if ((DeleteHelper.addheader || DeleteHelper.addheaderimage) && (DeleteHelper.ShouldAddHeader(DeleteHelper.CurrentPage)
                || DeleteHelper.ShouldAddHeaderImage(DeleteHelper.CurrentPage)))
            {
                DeleteHelper.CurrentPageHeader++;
                PdfPTable head = null;

                if (DeleteHelper.addheader && DeleteHelper.addheaderimage)
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

                if (DeleteHelper.addheader)
                {
                    // add the header text
                    c2h = new PdfPCell(new Phrase(DeleteHelper.HeaderText.Replace("[page]", DeleteHelper.CurrentPageHeader.ToString())
                    .Replace("[PAGE]", DeleteHelper.CurrentPageHeader.ToString()),
                      new Font(Font.FontFamily.COURIER, 8))
                    );
                    c2h.Border = PdfPCell.NO_BORDER;
                    c2h.VerticalAlignment = Element.ALIGN_MIDDLE;

                    if (frmOptionsDelete.Instance.Header.chkTextPositionCenter.Checked)
                    {
                        c2h.HorizontalAlignment = Element.ALIGN_CENTER;
                    }
                    else if (frmOptionsDelete.Instance.Header.chkTextPositionLeft.Checked)
                    {
                        c2h.HorizontalAlignment = Element.ALIGN_LEFT;
                    }
                    if (frmOptionsDelete.Instance.Header.chkTextPositionRight.Checked)
                    {
                        c2h.HorizontalAlignment = Element.ALIGN_RIGHT;
                    }


                    c2h.FixedHeight = cellHeight;

                }


                if (DeleteHelper.addheaderimage)
                {
                    ch = new PdfPCell(DeleteHelper.HeaderImage, true);
                    //c.VerticalAlignment = Element.ALIGN_TOP;
                    if (frmOptionsDelete.Instance.Header.chkImagePositionCenter.Checked)
                    {
                        ch.HorizontalAlignment = Element.ALIGN_CENTER;
                    }
                    else if (frmOptionsDelete.Instance.Header.chkImagePositionLeft.Checked)
                    {
                        ch.HorizontalAlignment = Element.ALIGN_LEFT;
                    }
                    if (frmOptionsDelete.Instance.Header.chkImagePositionRight.Checked)
                    {
                        ch.HorizontalAlignment = Element.ALIGN_RIGHT;
                    }

                    ch.FixedHeight = cellHeight;
                    ch.Border = PdfPCell.NO_BORDER;


                }

                if (c2h != null && ch != null)
                {
                    if (frmOptionsDelete.Instance.Header.chkTextPositionLeft.Checked)
                    {
                        head.AddCell(c2h);

                        if (frmOptionsDelete.Instance.Header.chkImagePositionCenter.Checked)
                        {
                            ch.HorizontalAlignment = Element.ALIGN_LEFT;
                        }

                        head.AddCell(ch);
                    }
                    else if (frmOptionsDelete.Instance.Header.chkTextPositionRight.Checked)
                    {
                        head.AddCell(ch);

                        if (frmOptionsDelete.Instance.Header.chkImagePositionCenter.Checked)
                        {
                            ch.HorizontalAlignment = Element.ALIGN_RIGHT;
                        }

                        head.AddCell(c2h);
                    }
                    else if (frmOptionsDelete.Instance.Header.chkTextPositionCenter.Checked)
                    {
                        c2h.HorizontalAlignment = Element.ALIGN_RIGHT;

                        head.AddCell(c2h);

                        if (frmOptionsDelete.Instance.Header.chkImagePositionCenter.Checked)
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

            if ((DeleteHelper.addfooter || DeleteHelper.addfooterimage) && (DeleteHelper.ShouldAddFooter(DeleteHelper.CurrentPage)
                || DeleteHelper.ShouldAddFooterImage(DeleteHelper.CurrentPage)))
            {
                DeleteHelper.CurrentPageFooter++;

                // cell height 
                float cellHeight = document.BottomMargin;
                // PDF document size      
                Rectangle page = document.PageSize;

                PdfPCell c2h = null;
                PdfPCell ch = null;

                // create two column table
                PdfPTable head = null;
                if (DeleteHelper.addfooter && DeleteHelper.addfooterimage)
                {
                    head = new PdfPTable(2);
                }
                else
                {
                    head = new PdfPTable(1);
                }

                head.TotalWidth = page.Width - 2 * document.LeftMargin;

                // add image; PdfPCell() overload sizes image to fit cell

                if (DeleteHelper.addfooterimage)
                {
                    ch = new PdfPCell(DeleteHelper.FooterImage, true);
                    //c.VerticalAlignment = Element.ALIGN_TOP;
                    if (frmOptionsDelete.Instance.Footer.chkImagePositionCenter.Checked)
                    {
                        ch.HorizontalAlignment = Element.ALIGN_CENTER;
                    }
                    else if (frmOptionsDelete.Instance.Footer.chkImagePositionLeft.Checked)
                    {
                        ch.HorizontalAlignment = Element.ALIGN_LEFT;
                    }
                    if (frmOptionsDelete.Instance.Footer.chkImagePositionRight.Checked)
                    {
                        ch.HorizontalAlignment = Element.ALIGN_RIGHT;
                    }

                    ch.FixedHeight = cellHeight;
                    ch.Border = PdfPCell.NO_BORDER;

                    //head.AddCell(c);
                }



                if (DeleteHelper.addfooter)
                {
                    // add the header text
                    c2h = new PdfPCell(new Phrase(DeleteHelper.FooterText.Replace("[page]", DeleteHelper.CurrentPageFooter.ToString())
                    .Replace("[PAGE]", DeleteHelper.CurrentPageFooter.ToString()),
                      new Font(Font.FontFamily.COURIER, 8))
                    );
                    c2h.Border = PdfPCell.NO_BORDER;
                    c2h.VerticalAlignment = Element.ALIGN_MIDDLE;

                    if (frmOptionsDelete.Instance.Footer.chkTextPositionCenter.Checked)
                    {
                        c2h.HorizontalAlignment = Element.ALIGN_CENTER;
                    }
                    else if (frmOptionsDelete.Instance.Footer.chkTextPositionLeft.Checked)
                    {
                        c2h.HorizontalAlignment = Element.ALIGN_LEFT;
                    }
                    if (frmOptionsDelete.Instance.Footer.chkTextPositionRight.Checked)
                    {
                        c2h.HorizontalAlignment = Element.ALIGN_RIGHT;
                    }

                    c2h.FixedHeight = cellHeight;

                    head.AddCell(c2h);
                }

                if (c2h != null && ch != null)
                {
                    if (frmOptionsDelete.Instance.Header.chkTextPositionLeft.Checked)
                    {
                        head.AddCell(c2h);

                        if (frmOptionsDelete.Instance.Header.chkImagePositionCenter.Checked)
                        {
                            ch.HorizontalAlignment = Element.ALIGN_LEFT;
                        }

                        head.AddCell(ch);
                    }
                    else if (frmOptionsDelete.Instance.Header.chkTextPositionRight.Checked)
                    {
                        head.AddCell(ch);

                        if (frmOptionsDelete.Instance.Header.chkImagePositionCenter.Checked)
                        {
                            ch.HorizontalAlignment = Element.ALIGN_RIGHT;
                        }

                        head.AddCell(c2h);
                    }
                    else if (frmOptionsDelete.Instance.Header.chkTextPositionCenter.Checked)
                    {
                        c2h.HorizontalAlignment = Element.ALIGN_RIGHT;

                        head.AddCell(c2h);

                        if (frmOptionsDelete.Instance.Header.chkImagePositionCenter.Checked)
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
