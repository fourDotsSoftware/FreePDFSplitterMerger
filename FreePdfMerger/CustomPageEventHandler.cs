using System;
using System.Collections.Generic;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace PdfMergeSplitTool
{
    class CustomPageEventHandler : PdfPageEventHelper
    {

        /*
         * We use a __single__ Image instance that's assigned __once__;
         * the image bytes added **ONCE** to the PDF file. If you create 
         * separate Image instances in OnEndPage()/OnEndPage(), for example,
         * you'll end up with a much bigger file size.
         */               

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            if (!MergeHelper.addfooter && !MergeHelper.addfooterimage && !MergeHelper.addheader
                && !MergeHelper.addheaderimage)
            {
                return;
            }                       

            if ((MergeHelper.addheader || MergeHelper.addheaderimage) && (MergeHelper.ShouldAddHeader(MergeHelper.CurrentPage)
                || MergeHelper.ShouldAddHeaderImage(MergeHelper.CurrentPage)))
            {
                MergeHelper.CurrentPageHeader++;
                PdfPTable head = null;

                if (MergeHelper.addheader && MergeHelper.addheaderimage)
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

                if (MergeHelper.addheader)
                {
                    // add the header text
                    c2h = new PdfPCell(new Phrase(MergeHelper.HeaderText.Replace("[page]", MergeHelper.CurrentPageHeader.ToString())
                    .Replace("[PAGE]", MergeHelper.CurrentPageHeader.ToString()),
                      new Font(Font.FontFamily.COURIER, 8))
                    );
                    c2h.Border = PdfPCell.NO_BORDER;
                    c2h.VerticalAlignment = Element.ALIGN_MIDDLE;

                    if (frmOptions.Instance.Header.chkTextPositionCenter.Checked)
                    {
                        c2h.HorizontalAlignment = Element.ALIGN_CENTER;
                    }
                    else if (frmOptions.Instance.Header.chkTextPositionLeft.Checked)
                    {
                        c2h.HorizontalAlignment = Element.ALIGN_LEFT;
                    }
                    if (frmOptions.Instance.Header.chkTextPositionRight.Checked)
                    {
                        c2h.HorizontalAlignment = Element.ALIGN_RIGHT;
                    }


                    c2h.FixedHeight = cellHeight;
                    
                }


                if (MergeHelper.addheaderimage)
                {
                    ch = new PdfPCell(MergeHelper.HeaderImage, true);
                    //c.VerticalAlignment = Element.ALIGN_TOP;
                    if (frmOptions.Instance.Header.chkImagePositionCenter.Checked)
                    {
                        ch.HorizontalAlignment = Element.ALIGN_CENTER;
                    }
                    else if (frmOptions.Instance.Header.chkImagePositionLeft.Checked)
                    {
                        ch.HorizontalAlignment = Element.ALIGN_LEFT;
                    }
                    if (frmOptions.Instance.Header.chkImagePositionRight.Checked)
                    {
                        ch.HorizontalAlignment = Element.ALIGN_RIGHT;
                    }

                    ch.FixedHeight = cellHeight;
                    ch.Border = PdfPCell.NO_BORDER;
                    
                    
                }

                if (c2h != null && ch != null)
                {
                    if (frmOptions.Instance.Header.chkTextPositionLeft.Checked)
                    {
                        head.AddCell(c2h);

                        if (frmOptions.Instance.Header.chkImagePositionCenter.Checked)
                        {
                            ch.HorizontalAlignment = Element.ALIGN_LEFT;
                        }

                        head.AddCell(ch);
                    }
                    else if (frmOptions.Instance.Header.chkTextPositionRight.Checked)
                    {
                        head.AddCell(ch);

                        if (frmOptions.Instance.Header.chkImagePositionCenter.Checked)
                        {
                            ch.HorizontalAlignment = Element.ALIGN_RIGHT;
                        }

                        head.AddCell(c2h);
                    }
                    else if (frmOptions.Instance.Header.chkTextPositionCenter.Checked)
                    {
                        c2h.HorizontalAlignment = Element.ALIGN_RIGHT;

                        head.AddCell(c2h);

                        if (frmOptions.Instance.Header.chkImagePositionCenter.Checked)
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

            if ((MergeHelper.addfooter || MergeHelper.addfooterimage) && (MergeHelper.ShouldAddFooter(MergeHelper.CurrentPage)
                || MergeHelper.ShouldAddFooterImage(MergeHelper.CurrentPage)))
            {
                MergeHelper.CurrentPageFooter++;

                // cell height 
                float cellHeight = document.BottomMargin;
                // PDF document size      
                Rectangle page = document.PageSize;

                PdfPCell c2h = null;
                PdfPCell ch = null;

                // create two column table
                PdfPTable head = null;
                if (MergeHelper.addfooter && MergeHelper.addfooterimage)
                {
                    head = new PdfPTable(2);
                }
                else
                {
                    head = new PdfPTable(1);
                }

                head.TotalWidth = page.Width - 2 * document.LeftMargin;

                // add image; PdfPCell() overload sizes image to fit cell

                if (MergeHelper.addfooterimage)
                {
                    ch = new PdfPCell(MergeHelper.FooterImage, true);
                    //c.VerticalAlignment = Element.ALIGN_TOP;
                    if (frmOptions.Instance.Footer.chkImagePositionCenter.Checked)
                    {
                        ch.HorizontalAlignment = Element.ALIGN_CENTER;
                    }
                    else if (frmOptions.Instance.Footer.chkImagePositionLeft.Checked)
                    {
                        ch.HorizontalAlignment = Element.ALIGN_LEFT;
                    }
                    if (frmOptions.Instance.Footer.chkImagePositionRight.Checked)
                    {
                        ch.HorizontalAlignment = Element.ALIGN_RIGHT;
                    }

                    ch.FixedHeight = cellHeight;
                    ch.Border = PdfPCell.NO_BORDER;
                    
                    //head.AddCell(c);
                }

                

                if (MergeHelper.addfooter)
                {
                    // add the header text
                    c2h = new PdfPCell(new Phrase(MergeHelper.FooterText.Replace("[page]", MergeHelper.CurrentPageFooter.ToString())
                    .Replace("[PAGE]", MergeHelper.CurrentPageFooter.ToString()),
                      new Font(Font.FontFamily.COURIER, 8))
                    );
                    c2h.Border = PdfPCell.NO_BORDER;
                    c2h.VerticalAlignment = Element.ALIGN_MIDDLE;

                    if (frmOptions.Instance.Footer.chkTextPositionCenter.Checked)
                    {
                        c2h.HorizontalAlignment = Element.ALIGN_CENTER;
                    }
                    else if (frmOptions.Instance.Footer.chkTextPositionLeft.Checked)
                    {
                        c2h.HorizontalAlignment = Element.ALIGN_LEFT;
                    }
                    if (frmOptions.Instance.Footer.chkTextPositionRight.Checked)
                    {
                        c2h.HorizontalAlignment = Element.ALIGN_RIGHT;
                    }
                    
                    c2h.FixedHeight = cellHeight;
                    
                    head.AddCell(c2h);
                }

                if (c2h != null && ch != null)
                {
                    if (frmOptions.Instance.Header.chkTextPositionLeft.Checked)
                    {
                        head.AddCell(c2h);

                        if (frmOptions.Instance.Header.chkImagePositionCenter.Checked)
                        {
                            ch.HorizontalAlignment = Element.ALIGN_LEFT;
                        }

                        head.AddCell(ch);
                    }
                    else if (frmOptions.Instance.Header.chkTextPositionRight.Checked)
                    {
                        head.AddCell(ch);

                        if (frmOptions.Instance.Header.chkImagePositionCenter.Checked)
                        {
                            ch.HorizontalAlignment = Element.ALIGN_RIGHT;
                        }

                        head.AddCell(c2h);
                    }
                    else if (frmOptions.Instance.Header.chkTextPositionCenter.Checked)
                    {
                        c2h.HorizontalAlignment = Element.ALIGN_RIGHT;

                        head.AddCell(c2h);

                        if (frmOptions.Instance.Header.chkImagePositionCenter.Checked)
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
