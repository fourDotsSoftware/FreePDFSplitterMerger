using System;
using System.Collections.Generic;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace PdfMergeSplitTool
{
    class MergeHelper
    {
        public static bool addheader = false;
        public static bool addheaderimage = false;
        public static bool addfooter = false;
        public static bool addfooterimage = false;
        public static Image HeaderImage = null;
        public static Image FooterImage = null;
        public static int CurrentPage = -1;
        public static int CurrentPageHeader = -1;
        public static int CurrentPageFooter = -1;
        public static string HeaderText = "";
        public static string FooterText = "";
        public static int TotalPages = 0;

        public static string Password = "";
            
        public static bool Merge()
        {
            List<string> sourceFiles = new List<string>();
            List<PageRange> sourcePageRanges = new List<PageRange>();
            List<string> sourcePasswords = new List<string>();

            List<Dictionary<String, Object>> bookmarks =
                    new List<Dictionary<String, Object>>();

            string author = "";
            string keywords = "";
            string subject = "";
            string title = "";

            string ownerpassword = "";
            string userpassword = "";
            int permissions = 0;


            int page_offset = 0;
            
            List<string> list_bookmarks = new List<string>();


            if (frmOptions.Instance.Bookmarks.chkListFile.Checked)
            {
                if (frmOptions.Instance.Bookmarks.txtExternalFile.Text != String.Empty)
                {
                    if (!System.IO.File.Exists(frmOptions.Instance.Bookmarks.txtExternalFile.Text))
                    {
                        Module.ShowMessage("Error. External List File that should be used for Bookmarks was not found !");
                        return false;
                    }
                    else
                    {
                        using (StreamReader sr = new StreamReader(frmOptions.Instance.Bookmarks.txtExternalFile.Text))
                        {
                            string line = null;
                            while ((line = sr.ReadLine()) != null)
                            {
                                list_bookmarks.Add(line);
                            }
                        }

                    }
                }

            }
            
            for (int k = 0; k < frmMain.Instance.dtDocs.Rows.Count; k++)
            {
                sourceFiles.Add(frmMain.Instance.dtDocs.Rows[k]["cfilepath"].ToString());
                sourcePageRanges.Add((PageRange)frmMain.Instance.dtDocs.Rows[k]["cTag"]);
                sourcePasswords.Add(frmMain.Instance.dtDocs.Rows[k]["cpassword"].ToString());
            }

            string finalDestinationFile = "";
            //string destinationFile = destinationFile = System.IO.Path.GetTempFileName();
            string destinationFile = System.IO.Path.GetTempFileName();

            PdfReader reader = null;
            Document document = null;
            PdfWriter writer=null;
            

            try
            {
                int f = 0;
                // we create a reader for a certain document
                
                int n = 1;

                if (!sourcePageRanges[f].IsImage)
                {
                    try {
                    if (sourcePasswords[f] != String.Empty)
                    {
                        reader = new PdfReader(sourceFiles[f], Encoding.Default.GetBytes(sourcePasswords[f]));
                    }
                    else
                    {
                        reader = new PdfReader(sourceFiles[f]);
                    }
                    
                    //if (reader.IsEncrypted())
                    //{

                      //  reader.Close();
                    }
                    catch (iTextSharp.text.pdf.BadPasswordException pex)
                    {
                        bool berror = false;

                        while (true)
                        {
                            try
                            {
                                if (frmMain.Instance.DefaultPassword == "" || berror)
                                {
                                    frmPassword f2 = new frmPassword(sourceFiles[f]);
                                    DialogResult dres = f2.ShowDialog();

                                    if (dres == DialogResult.OK)
                                    {
                                        reader = new PdfReader(sourceFiles[f], Encoding.Default.GetBytes(f2.txtPassword.Text));

                                        if (f2.chkPassword.Checked)
                                        {
                                            frmMain.Instance.DefaultPassword = f2.txtPassword.Text;
                                        }

                                        sourcePasswords[f] = f2.txtPassword.Text;

                                        break;
                                    }
                                    else if (dres == DialogResult.Cancel)
                                    {
                                        Module.ShowMessage("Unable to Decrypt Document ! - " + sourceFiles[f]);
                                        return false;
                                    }
                                }
                                else
                                {
                                    reader = new PdfReader(sourceFiles[f], Encoding.Default.GetBytes(frmMain.Instance.DefaultPassword));

                                    sourcePasswords[f] = frmMain.Instance.DefaultPassword;

                                    break;
                                }
                            }
                            catch
                            {
                                Module.ShowMessage("Unable to Decrypt Document ! - " + sourceFiles[f]);
                                //Password = "";
                                berror = true;
                            }
                        }
                    }
                    
                    // we retrieve the total number of pages
                    n = reader.NumberOfPages;
                }                                

                if (reader != null)
                {
                    // step 1: creation of a document-object
                    document = new Document(reader.GetPageSizeWithRotation(1));
                }
                else
                {
                    document = new Document(new Rectangle(600f, 800f));
                }

                // step 2: we create a writer that listens to the document

                if (frmOptions.Instance.Bookmarks.chkAddExisting.Checked)
                {
                    reader.ConsolidateNamedDestinations();
                }

                writer = PdfWriter.GetInstance(document, new FileStream(destinationFile, FileMode.Create));                
               
                // step 3: we open the document
                document.Open();

                if (!frmOptions.Instance.Properties.chkProperties.Checked)
                {
                    document.AddAuthor(frmOptions.Instance.Properties.txtAuthor.Text);
                    document.AddKeywords(frmOptions.Instance.Properties.txtKeywords.Text);
                    document.AddSubject(frmOptions.Instance.Properties.txtSubject.Text);
                    document.AddTitle(frmOptions.Instance.Properties.txtTitle.Text);
                }
                else
                {
                    if (reader.Info.ContainsKey("Author"))
                    {
                        author = reader.Info["Author"];
                    }

                    if (reader.Info.ContainsKey("Keywords"))
                    {
                        keywords = reader.Info["Keywords"];
                    }

                    if (reader.Info.ContainsKey("Title"))
                    {
                        title = reader.Info["Title"];
                    }

                    if (reader.Info.ContainsKey("Subject"))
                    {
                        subject = reader.Info["Subject"];
                    }

                    document.AddAuthor(author);
                    document.AddSubject(subject);
                    document.AddTitle(title);
                    document.AddKeywords(keywords);

                }

                if (f == 0)
                {
                    ownerpassword = frmOptions.Instance.Properties.txtOwnerPassword.Text;
                    userpassword = frmOptions.Instance.Properties.txtUserPassword.Text;
                                       
                }

                addheader = false;
                addheaderimage = false;
                addfooter = false;
                addfooterimage = false;                

                if (frmOptions.Instance.Header.txtHeader.Text != String.Empty)
                {
                    addheader = true;
                }

                if (frmOptions.Instance.Footer.txtFooter.Text != String.Empty)
                {
                    addfooter = true;
                }

                if (frmOptions.Instance.Header.txtHeaderImage.Text != String.Empty
                && System.IO.File.Exists(frmOptions.Instance.Header.txtHeaderImage.Text))
                {
                    addheaderimage = true;
                }

                if (frmOptions.Instance.Footer.txtFooter.Text != String.Empty
                && System.IO.File.Exists(frmOptions.Instance.Footer.txtFooterImage.Text))
                {
                    addfooterimage = true;
                }

                HeaderImage = null;
                FooterImage = null;

                TotalPages = int.Parse(frmMain.Instance.txtTotalPages.Text);
                
                if (addheaderimage)
                {
                    System.Drawing.Image imgHeader=null;

                    try 
                    {
                        imgHeader=(System.Drawing.Image)FreeImageHelper.LoadImage(frmOptions.Instance.Header.txtHeaderImage.Text);
                    }
                    catch 
                    {
                        Module.ShowMessage("Error. Could not load Header Image File !");
                        return false;
                    
                    }

                    string tempheader=System.IO.Path.GetTempFileName();
                    imgHeader.Save(tempheader);
                    HeaderImage = Image.GetInstance(tempheader);
                }

                if (addheader)
                {                                        
                    int totalpagesh = (int)frmOptions.Instance.Header.nudTo.Value - (int)frmOptions.Instance.Header.nudFrom.Value + 1;
                    
                    HeaderText = frmOptions.Instance.Header.txtHeader.Text.Replace("[pagenum]", totalpagesh.ToString())
                    .Replace("[PAGENUM]", totalpagesh.ToString()).Replace("[date]", DateTime.Now.ToShortDateString()).
                    Replace("[DATE]", DateTime.Now.ToShortDateString()).Replace("[title]", frmOptions.Instance.Properties.txtTitle.Text).
                    Replace("[TITLE]", frmOptions.Instance.Properties.txtTitle.Text).Replace("[author]", frmOptions.Instance.Properties.txtAuthor.Text).
                    Replace("[AUTHOR]", frmOptions.Instance.Properties.txtAuthor.Text).Replace("[subject]", frmOptions.Instance.Properties.txtSubject.Text).
                    Replace("[SUBJECT]", frmOptions.Instance.Properties.txtSubject.Text);
                }

                if (addfooter)
                {
                    int totalpagesf = (int)frmOptions.Instance.Footer.nudTo.Value - (int)frmOptions.Instance.Footer.nudFrom.Value + 1;

                    FooterText = frmOptions.Instance.Footer.txtFooter.Text.Replace("[pagenum]", totalpagesf.ToString())
                    .Replace("[PAGENUM]", totalpagesf.ToString()).Replace("[date]", DateTime.Now.ToShortDateString()).
                    Replace("[DATE]", DateTime.Now.ToShortDateString()).Replace("[title]", frmOptions.Instance.Properties.txtTitle.Text).
                    Replace("[TITLE]", frmOptions.Instance.Properties.txtTitle.Text).Replace("[author]", frmOptions.Instance.Properties.txtAuthor.Text).
                    Replace("[AUTHOR]", frmOptions.Instance.Properties.txtAuthor.Text).Replace("[subject]", frmOptions.Instance.Properties.txtSubject.Text).
                    Replace("[SUBJECT]", frmOptions.Instance.Properties.txtSubject.Text);
                }

                if (addfooterimage)
                {
                    System.Drawing.Image imgFooter = null;

                    try
                    {
                        imgFooter = (System.Drawing.Image)FreeImageHelper.LoadImage(frmOptions.Instance.Footer.txtFooterImage.Text);
                    }
                    catch
                    {
                        Module.ShowMessage("Error. Could not load Footer Image File !");
                        return false;

                    }

                    string tempfooter = System.IO.Path.GetTempFileName();
                    imgFooter.Save(tempfooter);
                    FooterImage = Image.GetInstance(tempfooter);
                }

                CustomPageEventHandler e = new CustomPageEventHandler();
                writer.PageEvent = e;

                CurrentPageHeader = 0;
                CurrentPageFooter = 0;
                CurrentPage = 0;

                PdfContentByte cb = writer.DirectContent;
                PdfImportedPage page = null;
                int rotation;
                // step 4: we add content
                while (f < sourceFiles.Count)
                {
                    int i = 0;

                    if (!frmOptions.Instance.Header.chkContinueNumbering.Checked)
                    {
                        CurrentPageHeader = 0;
                    }

                    if (!frmOptions.Instance.Footer.chkContinueNumbering.Checked)
                    {
                        CurrentPageFooter = 0;
                    }

                    if (frmOptions.Instance.Bookmarks.chkListFile.Checked)
                    {
                        if (f < list_bookmarks.Count)
                        {      
                            bookmarks.Add(CreateBookmark(page_offset+1,list_bookmarks[f]));
                        }
                    }
                    if (frmOptions.Instance.Bookmarks.chkFilenames.Checked)
                    {
                        bookmarks.Add(CreateBookmark(page_offset+1, System.IO.Path.GetFileName(sourceFiles[f])));                        
                    }
                    if (frmOptions.Instance.Bookmarks.chkFilePaths.Checked)
                    {
                        bookmarks.Add(CreateBookmark(page_offset+1, sourceFiles[f]));
                    }
                    if (frmOptions.Instance.Bookmarks.chkTitles.Checked)
                    {
                        string title2 = "";

                        if (reader.Info.ContainsKey("Title"))
                        {
                            title2 = reader.Info["Title"];
                        }
                        else
                        {
                            title2 = "";
                        }

                        bookmarks.Add(CreateBookmark(page_offset+1, title2));                        
                    }

                    if (frmOptions.Instance.Bookmarks.chkAddExisting.Checked)
                    {
                        IList<Dictionary<String, Object>> tmp = new List<Dictionary<String, Object>>();
                        if (reader != null)
                        {
                            tmp = SimpleBookmark.GetBookmark(reader);
                        }

                        if (tmp != null)
                        {
                            SimpleBookmark.ShiftPageNumbers(tmp, page_offset, null);

                            for (int m = 0; m < tmp.Count; m++)
                            {
                                bookmarks.Add(tmp[m]);
                            }
                        }
                    }

                    while (i < n)
                    {
                        i++;
                        

                        if (!ShouldAddPage(i,sourcePageRanges[f],reader))
                        {
                            continue;
                        }

                        if (finalDestinationFile == String.Empty)
                        {
                            finalDestinationFile = GetFinalDestinationFile(sourceFiles[f], i);                           
                        }

                        if (reader != null)
                        {
                            Rectangle rect = new Rectangle(reader.GetPageSizeWithRotation(i).Width, reader.GetPageSizeWithRotation(i).Height);
                            document.SetPageSize(rect);                                                       
                        }
                        else
                        {
                            document.SetPageSize(new Rectangle(600f, 800f));
                        }
                                               

                        document.NewPage();

                        if (reader != null)
                        {
                            int ipage = i;

                            if (sourcePageRanges[f].ImportReversed)
                            {
                                ipage=reader.NumberOfPages - i;
                            }
                                                        
                            page = writer.GetImportedPage(reader, ipage);
                            
                            rotation = reader.GetPageRotation(ipage);

                            float width = reader.GetPageSize(ipage).Width;
                            float height = reader.GetPageSize(ipage).Height;
                            if (width > height)
                            {
                                PdfDictionary pageDict = reader.GetPageN(i);
                                pageDict.Put(PdfName.ROTATE, new PdfNumber(90));
                            }

                            cb.SetTextMatrix(reader.GetPageSizeWithRotation(ipage).Left, reader.GetPageSizeWithRotation(ipage).Top);

                            if (rotation == 90 || rotation == 270)
                            {
                                cb.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(ipage).Height);
                            }
                            else
                            {
                                cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                            }                            
                            
                        }
                        else
                        {                            
                            Image img = Image.GetInstance((System.Drawing.Image)FreeImageHelper.LoadImage(sourceFiles[f]), BaseColor.BLACK);
                                                        

                            /*
                            PdfPTable pdfTable = new PdfPTable(1);
                            PdfPCell pCell = null;
                            //pdfTable.WidthPercentage = 100;

                            
                            pCell = new PdfPCell();                            
                            pCell.Border = 0;
                            pCell.Padding = 0f;
                            
                            if (sourcePageRanges[f].HorizAlign)
                            {
                                //img.Alignment = iTextSharp.text.Element.ALIGN_CENTER;                                
                                if (sourcePageRanges[f].HorizAlignValue==0)
                                {
                                    pdfTable.HorizontalAlignment=iTextSharp.text.Element.ALIGN_LEFT;
                                }
                                else if (sourcePageRanges[f].HorizAlignValue==1)
                                {
                                    pdfTable.HorizontalAlignment=iTextSharp.text.Element.ALIGN_CENTER;
                                }
                                if (sourcePageRanges[f].HorizAlignValue==2)
                                {
                                    pdfTable.HorizontalAlignment=iTextSharp.text.Element.ALIGN_RIGHT;
                                }
                            }

                            
                            if (sourcePageRanges[f].VertiAlign)
                            {
                                PageSize.POSTCARD 
                                //img.Alignment = iTextSharp.text.Element.ALIGN_CENTER;                                
                                if (sourcePageRanges[f].VertiAlignValue==0)
                                {
                                    pCell.VerticalAlignment=iTextSharp.text.Element.ALIGN_TOP;
                                }
                                else if (sourcePageRanges[f].VertiAlignValue==1)
                                {
                                    pCell.VerticalAlignment=iTextSharp.text.Element.ALIGN_MIDDLE;
                                }
                                if (sourcePageRanges[f].VertiAlignValue==2)
                                {
                                    pCell.VerticalAlignment=iTextSharp.text.Element.ALIGN_BOTTOM;
                                }
                            }
                            
                            */

                            
                            if (sourcePageRanges[f].ScaledPercentage)
                            {
                                img.ScalePercent((float)sourcePageRanges[f].ScaledPercetnageValue);
                            }

                            if (sourcePageRanges[f].ScaledSize)
                            {
                                img.ScaleAbsolute((float)sourcePageRanges[f].ScaledWidth, (float)sourcePageRanges[f].ScaledHeight);
                            }

                            if (sourcePageRanges[f].ScaleToFit)
                            {
                                if (reader != null)
                                {
                                    img.ScaleToFit(page.Width, page.Height);
                                }
                                else
                                {
                                    img.ScaleToFit(600f, 650f);
                                }
                            }
                            /*
                            pCell.AddElement(img);
                            pdfTable.AddCell(pCell);
                            document.Add(pdfTable);
                            //pdfTable.WriteSelectedRows(0, -1, 0f, 800f, cb);
                            */                            

                            if (sourcePageRanges[f].AbsolutePosition)
                            {
                                img.SetAbsolutePosition((float)sourcePageRanges[f].AbsX, (float)sourcePageRanges[f].AbsY);
                            }
                            else
                            {

                                if (page == null)
                                {
                                    img.SetAbsolutePosition((PageSize.A4.Width - img.ScaledWidth) / 2, (PageSize.A4.Height - img.ScaledHeight) / 2);
                                }
                                else
                                {
                                    img.SetAbsolutePosition((page.Width - img.ScaledWidth) / 2, (page.Height - img.ScaledHeight) / 2);
                                }

                                /*
                                if (sourcePageRanges[f].HorizAlignValue == 3)
                                {
                                    
                                }
                                else
                                {                                    
                                        //img.Alignment = iTextSharp.text.Element.ALIGN_CENTER;                                
                                        if (sourcePageRanges[f].HorizAlignValue == 0)
                                        {
                                            img.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
                                        }
                                        else if (sourcePageRanges[f].HorizAlignValue == 1)
                                        {
                                            img.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                                        }
                                        if (sourcePageRanges[f].HorizAlignValue == 2)
                                        {
                                            img.Alignment = iTextSharp.text.Element.ALIGN_RIGHT;
                                        }                                    
                                }
                                */
                            }

                            cb.AddImage(img);
                        }
                       
                        
                        Console.WriteLine("Processed page " + i);

                        page_offset++;

                        CurrentPage++;
                        //CurrentPageHeader++;
                        //CurrentPageFooter++;

                        
                    }

                    /*
                    PRAcroForm form = reader.AcroForm;
                    if (form != null && writer != null)
                    {
                        writer.Add(form);
                        //writer.CopyAcroForm(reader);
                    }
                    */

                    if (frmOptions.Instance.Bookmarks.chkAddExisting.Checked)
                    {
                        try
                        {
                            writer.AddNamedDestinations(SimpleNamedDestination.GetNamedDestination(reader, false), page_offset);
                        }
                        catch
                        {
                            writer.AddNamedDestinations(SimpleNamedDestination.GetNamedDestination(reader, true), page_offset);
                        }
                    }

                    f++;
                    if (f < sourceFiles.Count)
                    {
                        if (!sourcePageRanges[f].IsImage)
                        {
                            try
                            {
                                if (sourcePasswords[f] != String.Empty)
                                {
                                    reader = new PdfReader(sourceFiles[f], Encoding.Default.GetBytes(sourcePasswords[f]));
                                }
                                else
                                {
                                    reader = new PdfReader(sourceFiles[f]);
                                }

                                //if (reader.IsEncrypted())
                                //{

                                //  reader.Close();
                            }
                            catch (iTextSharp.text.pdf.BadPasswordException pex)
                            {
                                bool berror = false;

                                while (true)
                                {
                                    try
                                    {
                                        if (frmMain.Instance.DefaultPassword == "" || berror)
                                        {
                                            frmPassword f2 = new frmPassword(sourceFiles[f]);
                                            DialogResult dres = f2.ShowDialog();

                                            if (dres == DialogResult.OK)
                                            {
                                                reader = new PdfReader(sourceFiles[f], Encoding.Default.GetBytes(f2.txtPassword.Text));

                                                if (f2.chkPassword.Checked)
                                                {
                                                    frmMain.Instance.DefaultPassword = f2.txtPassword.Text;
                                                }

                                                sourcePasswords[f] = f2.txtPassword.Text;

                                                break;
                                            }
                                            else if (dres == DialogResult.Cancel)
                                            {
                                                Module.ShowMessage("Unable to Decrypt Document ! - " + sourceFiles[f]);
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            reader = new PdfReader(sourceFiles[f], Encoding.Default.GetBytes(frmMain.Instance.DefaultPassword));

                                            sourcePasswords[f] = frmMain.Instance.DefaultPassword;

                                            break;
                                        }
                                    }
                                    catch
                                    {
                                        Module.ShowMessage("Unable to Decrypt Document ! - " + sourceFiles[f]);
                                        //Password = "";
                                        berror = true;
                                    }
                                }
                            }
                            
                            // we retrieve the total number of pages
                            n = reader.NumberOfPages;
                        }
                        else
                        {
                            n = 1;
                        }

                        if (frmOptions.Instance.Bookmarks.chkAddExisting.Checked)
                        {
                            reader.ConsolidateNamedDestinations();
                        }

                        Console.WriteLine("There are " + n + " pages in the original file.");
                    }
                }

                

                writer.Outlines = bookmarks;
                // step 5: we close the document
                document.Close();

                if (frmOptions.Instance.Bookmarks.chkAddExisting.Checked)
                {
                    PDFHelper.MakeRemoteNamedDestinationsLocal(destinationFile);
                }

                EncryptDocument(destinationFile,finalDestinationFile,ownerpassword,userpassword);

                if (frmOptions.Instance.Misc.chkDeleteOriginals.Checked)
                {
                    string errd = "";
                    for (int k = 0; k < frmMain.Instance.dtDocs.Rows.Count; k++)
                    {
                        try
                        {
                            System.IO.File.Delete(frmMain.Instance.dtDocs.Rows[k]["cfilepath"].ToString());
                        }
                        catch (Exception exd)
                        {
                            errd = "Error could not delete File : " + frmMain.Instance.dtDocs.Rows[k]["cfilepath"].ToString() + " \n\n" + exd.Message;
                        }
                    }


                    if (errd != String.Empty)
                    {
                        Module.ShowMessage(errd);
                    }
                }

                if (frmOptions.Instance.Misc.chkOpenDestinationFolder.Checked)
                {
                    System.Diagnostics.Process.Start("explorer.exe", "/select,\"" + finalDestinationFile+"\"");
                    
                }

                

            }
            catch (Exception e)
            {              
                if (document != null)
                {
                    document.Close();
                }

                if (System.IO.File.Exists(destinationFile))
                {
                    try
                    {
                        System.IO.File.Delete(destinationFile);
                    }
                    catch { }
                }

                throw (e);
                Console.Error.WriteLine(e.Message);
                Console.Error.WriteLine(e.StackTrace);
            }

            return true;
        }

        private static string DecryptDocument(string filepath,string password)
        {            
            /*
            PdfReader reader = new PdfReader(filepath,Encoding.Default.GetBytes(password));
            string outf=System.IO.Path.GetDirectoryName(filepath)+"\\"+
                System.IO.Path.GetFileNameWithoutExtension(filepath)+".decrypted.pdf";

            PdfStamper stamper = new PdfStamper(reader,new FileOutputStream(outf));
            stamper.close();
             */

            return "";
        }

        private static void EncryptDocument(string destinationFile,string finalDestinationFile,string ownerpassword,string userpassword)
        {
           // if (frmOptions.Instance.Properties.txtOwnerPassword.Text == String.Empty
          //          && frmOptions.Instance.Properties.txtUserPassword.Text == String.Empty)

            if (ownerpassword == String.Empty
                   && userpassword == String.Empty)
            {
                if (System.IO.File.Exists(finalDestinationFile))
                {
                    System.IO.File.Delete(finalDestinationFile);
                }

                System.IO.File.Move(destinationFile, finalDestinationFile);
            }
            else
            {
                using (Stream input = new FileStream(destinationFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (Stream output = new FileStream(finalDestinationFile, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        PdfReader reader2 = new PdfReader(input);
                    //    string ownerpassword = null;
                    //    string userpassword = null;

                      //  if (frmOptions.Instance.Properties.txtOwnerPassword.Text != string.Empty)
                       // {
                       //     ownerpassword = frmOptions.Instance.Properties.txtOwnerPassword.Text;
                       // }

                      //  if (frmOptions.Instance.Properties.txtUserPassword.Text != string.Empty)
                      //  {
                       //     userpassword = frmOptions.Instance.Properties.txtUserPassword.Text;
                      //  }

                        if (ownerpassword == String.Empty)
                        {
                            ownerpassword = null;
                        }

                        if (userpassword == String.Empty)
                        {
                            userpassword = null;
                        }

                        int permissions = 0;
                        if (frmOptions.Instance.Properties.chkAnnotations.Checked)
                        {
                            permissions |= PdfWriter.ALLOW_MODIFY_ANNOTATIONS;
                        }

                        if (frmOptions.Instance.Properties.chkAssembly.Checked)
                        {
                            permissions |= PdfWriter.ALLOW_ASSEMBLY;
                        }

                        if (frmOptions.Instance.Properties.chkCopy.Checked)
                        {
                            permissions |= PdfWriter.ALLOW_COPY;
                        }

                        if (frmOptions.Instance.Properties.chkFormFill.Checked)
                        {
                            permissions |= PdfWriter.ALLOW_FILL_IN;
                        }

                        if (frmOptions.Instance.Properties.chkHighPrinting.Checked)
                        {
                            permissions |= PdfWriter.ALLOW_PRINTING;
                        }

                        if (frmOptions.Instance.Properties.chkLowPrinting.Checked)
                        {
                            permissions |= PdfWriter.ALLOW_DEGRADED_PRINTING;
                        }

                        if (frmOptions.Instance.Properties.chkModifyContents.Checked)
                        {
                            permissions |= PdfWriter.ALLOW_MODIFY_CONTENTS;
                        }

                        if (frmOptions.Instance.Properties.chkScreenReaders.Checked)
                        {
                            permissions |= PdfWriter.ALLOW_SCREENREADERS;
                        }

                        PdfEncryptor.Encrypt(reader2, output, true, userpassword, ownerpassword,permissions);
                    }
                }

                System.IO.File.Delete(destinationFile);
            }
        }

        public static bool ShouldAddHeader(int pagenum)
        {
            if (frmOptions.Instance.Header.txtHeader.Text != String.Empty)
            {
                if (pagenum < frmOptions.Instance.Header.nudFrom.Value ||
                    pagenum > frmOptions.Instance.Header.nudTo.Value)
                {
                    return false;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ShouldAddHeaderImage(int pagenum)
        {
            if (frmOptions.Instance.Header.txtHeaderImage.Text != String.Empty
                && System.IO.File.Exists(frmOptions.Instance.Header.txtHeaderImage.Text))
            {
                if (pagenum < frmOptions.Instance.Header.nudFrom.Value ||
                    pagenum > frmOptions.Instance.Header.nudTo.Value)
                {
                    return false;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ShouldAddFooter(int pagenum)
        {
            if (frmOptions.Instance.Footer.txtFooter.Text != String.Empty)
            {
                if (pagenum < frmOptions.Instance.Footer.nudFrom.Value ||
                    pagenum > frmOptions.Instance.Footer.nudTo.Value)
                {
                    return false;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ShouldAddFooterImage(int pagenum)
        {
            if (frmOptions.Instance.Footer.txtFooterImage.Text != String.Empty
                && System.IO.File.Exists(frmOptions.Instance.Footer.txtFooterImage.Text))
            {
                if (pagenum < frmOptions.Instance.Footer.nudFrom.Value ||
                    pagenum > frmOptions.Instance.Footer.nudTo.Value)
                {
                    return false;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ShouldAddPage(int pagenum, PageRange pagerange,PdfReader reader)
        {
            if (pagerange.IsImage) return true;

            if (pagerange.AllPages)
            {
                return true;
            }

            bool add = false;

            if (pagerange.Pages)
            {
                if (pagenum < pagerange.PagesFrom || pagenum > pagerange.PagesTo)
                {

                }
                else
                {
                    return true;

                }

            }

            if (pagerange.PagesOdd)
            {
                if (pagenum % 2 != 0)
                {
                    if (pagenum < pagerange.PagesOddFrom || pagenum > pagerange.PagesOddTo)
                    {

                    }
                    else
                    {
                        return true;
                    }
                }
            }

            if (pagerange.PagesEven)
            {
                if (pagenum % 2 == 0)
                {

                    if (pagenum < pagerange.PagesEvenFrom || pagenum > pagerange.PagesEvenTo)
                    {

                    }
                    else
                    {
                        return true;
                    }
                }
            }

            if (pagerange.PagesEvery)
            {
                if (pagenum < pagerange.PagesEveryFrom || pagenum > pagerange.PagesEveryTo)
                {

                }
                else
                {
                    int ieveryfrom = (int)pagerange.PagesEveryFrom;

                    if ((pagenum-ieveryfrom) % pagerange.PagesEveryValue == 0)
                    {
                        return true;
                    }
                }
            }

            if (pagerange.PageRanges.Trim() != String.Empty)
            {
                string[] ranges = pagerange.PageRanges.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                for (int k = 0; k < ranges.Length; k++)
                {
                    string[] range = ranges[k].Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);

                    int ifrom = int.Parse(range[0]);
                    int ito = int.Parse(range[1]);

                    if (pagenum < ifrom || pagenum > ito)
                    {

                    }
                    else
                    {
                        return true;
                    }
                }

            }

            if (pagerange.PagesContainingText && reader!=null)
            {
                if (iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, pagenum).Contains(
                    pagerange.PageText))
                {
                    return true;
                }
            }
            return false;
        }

        private static Dictionary<String, Object> CreateBookmark(int pagenum,string title)
        {
            Dictionary<String, Object> dict = new Dictionary<String, Object>();
            dict.Add("Title", title);
            dict.Add("Action", "GoTo");
            dict.Add("Page", pagenum.ToString());

            return dict;
        }

        private static string GetFinalDestinationFile(string filepath, int ipage)
        {
            string filename = frmOptions.Instance.OutputFile.txtOutputPattern.Text;
            filename = filename.Replace("[page]", ipage.ToString());
            filename = filename.Replace("[PAGE]", ipage.ToString());

            filename = filename.Replace("[file]", System.IO.Path.GetFileNameWithoutExtension(filepath));
            filename = filename.Replace("[FILE]", System.IO.Path.GetFileNameWithoutExtension(filepath));

            filename = ArgsHelper.GetFilenameWithParameterValues(filename); 

            if (frmOptions.Instance.OutputFile.chkSameFolder.Checked)
            {
                return System.IO.Path.Combine(System.IO.Path.GetDirectoryName(filepath), filename);
            }
            else
            {
                return System.IO.Path.Combine(frmOptions.Instance.OutputFile.txtOutputFolder.Text, filename);
            }

        }
    }

    public class GetTitleAndNumberOfPagesResult
    {
        public string Title = "";
        public int NumberOfPages = -1;
    }
}
