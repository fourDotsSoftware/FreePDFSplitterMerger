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
    class SplitHelper
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

        private static List<string> bkmarks_short_text = new List<string>();
        private static List<string> bkmarks_full_text = new List<string>();
        private static List<int> bkmarks_level = new List<int>();

        private static int bkmarks_current_bookmark_index = -1;

        //private static int current_range_index = 0;

        //private static string bkmarks_current_full_text = "";

        
        public static bool Split()
        {
            //bkmarks_current_full_text = "";
            

            List<string> sourceFiles = new List<string>();
            List<string> sourcePasswords = new List<string>();
            //List<Dictionary<String, Object>> bookmarks =
            //      new List<Dictionary<String, Object>>();

            //current_range_index = 0;

            int page_offset = 0;

            //List<string> list_bookmarks = new List<string>();                                         

            for (int k = 0; k < frmMain.Instance.dtSplit.Rows.Count; k++)
            {
                sourceFiles.Add(frmMain.Instance.dtSplit.Rows[k]["cfilepath"].ToString());
                sourcePasswords.Add(frmMain.Instance.dtSplit.Rows[k]["cpassword"].ToString());                
            }



            PdfReader reader = null;
            Document document = null;
            PdfWriter writer = null;

            string finalDestinationFile = "";
            string destinationFile = "";

            string first_result_file = "";

            string author = "";
            string keywords = "";
            string subject = "";
            string title = "";

            bool second_doc = false;

            try
            {
                for (int f = 0; f < sourceFiles.Count; f++)
                {
                    finalDestinationFile = "";
                    destinationFile = "";

                    int blank_pages_count = 0;
                    Bookmarks bookmarks = null;

                    //string userpassword = "";

                    // we create a reader for a certain document
                    int n = 1;

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
                    //MessageBox.Show(Encoding.Default.GetString(reader.ComputeUserPassword()));

                    if (frmOptionsSplit.Instance.Bookmarks.chkAddExisting.Checked)
                    {
                        reader.ConsolidateNamedDestinations();
                    }

                    // step 1: creation of a document-object
                    document = new Document(reader.GetPageSizeWithRotation(1));
                    /*
                    byte[] buserpwd = reader.ComputeUserPassword();
                    if (buserpwd != null)
                    {
                        userpassword = Encoding.Default.GetString(buserpwd);
                    }
                    */

                    destinationFile = System.IO.Path.GetTempFileName();

                    // step 2: we create a writer that listens to the document
                    writer = PdfWriter.GetInstance(document, new FileStream(destinationFile, FileMode.Create));
                    ;
                    // step 3: we open the document
                    document.Open();

                    if (!frmOptionsSplit.Instance.Properties.chkProperties.Checked)
                    {
                        document.AddAuthor(frmOptionsSplit.Instance.Properties.txtAuthor.Text);
                        document.AddKeywords(frmOptionsSplit.Instance.Properties.txtKeywords.Text);
                        document.AddSubject(frmOptionsSplit.Instance.Properties.txtSubject.Text);
                        document.AddTitle(frmOptionsSplit.Instance.Properties.txtTitle.Text);
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

                    string ownerpassword = "";
                    string userpassword = "";

                    ownerpassword = frmOptionsSplit.Instance.Properties.txtOwnerPassword.Text;
                    userpassword = frmOptionsSplit.Instance.Properties.txtUserPassword.Text;
                    

                    addheader = false;
                    addheaderimage = false;
                    addfooter = false;
                    addfooterimage = false;

                    if (frmOptionsSplit.Instance.Header.txtHeader.Text != String.Empty)
                    {
                        addheader = true;
                    }

                    if (frmOptionsSplit.Instance.Footer.txtFooter.Text != String.Empty)
                    {
                        addfooter = true;
                    }

                    if (frmOptionsSplit.Instance.Header.txtHeaderImage.Text != String.Empty
                    && System.IO.File.Exists(frmOptionsSplit.Instance.Header.txtHeaderImage.Text))
                    {
                        addheaderimage = true;
                    }

                    if (frmOptionsSplit.Instance.Footer.txtFooter.Text != String.Empty
                    && System.IO.File.Exists(frmOptionsSplit.Instance.Footer.txtFooterImage.Text))
                    {
                        addfooterimage = true;
                    }

                    HeaderImage = null;
                    FooterImage = null;

                    if (addheaderimage)
                    {
                        System.Drawing.Image imgHeader = null;

                        try
                        {
                            imgHeader = (System.Drawing.Image)FreeImageHelper.LoadImage(frmOptionsSplit.Instance.Header.txtHeaderImage.Text);
                        }
                        catch
                        {
                            Module.ShowMessage("Error. Could not load Header Image File !");
                            return false;

                        }

                        string tempheader = System.IO.Path.GetTempFileName();
                        imgHeader.Save(tempheader);
                        HeaderImage = Image.GetInstance(tempheader);
                    }

                    if (addheader)
                    {
                        int totalpagesh = CalculateTotalPagesHeader(n);

                        HeaderText = frmOptionsSplit.Instance.Header.txtHeader.Text.Replace("[pagenum]", totalpagesh.ToString())
                        .Replace("[PAGENUM]", totalpagesh.ToString()).Replace("[date]", DateTime.Now.ToShortDateString()).
                        Replace("[DATE]", DateTime.Now.ToShortDateString()).Replace("[title]", frmOptionsSplit.Instance.Properties.txtTitle.Text).
                        Replace("[TITLE]", frmOptionsSplit.Instance.Properties.txtTitle.Text).Replace("[author]", frmOptionsSplit.Instance.Properties.txtAuthor.Text).
                        Replace("[AUTHOR]", frmOptionsSplit.Instance.Properties.txtAuthor.Text).Replace("[subject]", frmOptionsSplit.Instance.Properties.txtSubject.Text).
                        Replace("[SUBJECT]", frmOptionsSplit.Instance.Properties.txtSubject.Text);
                    }

                    if (addfooter)
                    {
                        int totalpagesf = CalculateTotalPagesFooter(n);

                        FooterText = frmOptionsSplit.Instance.Footer.txtFooter.Text.Replace("[pagenum]", totalpagesf.ToString())
                        .Replace("[PAGENUM]", totalpagesf.ToString()).Replace("[date]", DateTime.Now.ToShortDateString()).
                        Replace("[DATE]", DateTime.Now.ToShortDateString()).Replace("[title]", frmOptionsSplit.Instance.Properties.txtTitle.Text).
                        Replace("[TITLE]", frmOptionsSplit.Instance.Properties.txtTitle.Text).Replace("[author]", frmOptionsSplit.Instance.Properties.txtAuthor.Text).
                        Replace("[AUTHOR]", frmOptionsSplit.Instance.Properties.txtAuthor.Text).Replace("[subject]", frmOptionsSplit.Instance.Properties.txtSubject.Text).
                        Replace("[SUBJECT]", frmOptionsSplit.Instance.Properties.txtSubject.Text);
                    }

                    if (addfooterimage)
                    {
                        System.Drawing.Image imgFooter = null;

                        try
                        {
                            imgFooter = (System.Drawing.Image)FreeImageHelper.LoadImage(frmOptionsSplit.Instance.Footer.txtFooterImage.Text);
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

                    CustomPageEventHandlerSplit e = new CustomPageEventHandlerSplit();
                    writer.PageEvent = e;

                    CurrentPageHeader = 0;
                    CurrentPageFooter = 0;
                    CurrentPage = 0;

                    PdfContentByte cb = writer.DirectContent;
                    PdfImportedPage page = null;
                    int rotation;

                    // we get bookmarks in order to know when to split

                    IList<Dictionary<String, Object>> bkmarks = new List<Dictionary<String, Object>>();                                      
                    
                    if (reader != null)
                    {
                        bkmarks=SimpleBookmark.GetBookmark(reader);
                    }

                    if (bkmarks != null)
                    {                        
                        bookmarks = GetBookmarksTree(bkmarks);
                    }

                    // end

                    // step 4: we add content

                    int i = 0;

                    while (i < n)
                    {
                        i++;

                        if (!ShouldAddPage(i))
                        {
                            CurrentPage++;
                            continue;
                        }

                        byte[] bcontent = reader.GetPageContent(i);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            ms.Write(bcontent, 0, bcontent.Length);

                            const int blankPdfSize = 0;

                            if (ms.Length < blankPdfSize)
                            {
                                blank_pages_count++;
                            }
                            else
                            {
                                blank_pages_count = 0;
                            }
                        }

                        if (CurrentPage >0  && ShouldSplit(i,reader,bookmarks,blank_pages_count))
                        {
                            //document.Close();                            

                            if (frmOptionsSplit.Instance.Bookmarks.chkAddExisting.Checked)
                            {
                                try
                                {
                                    writer.AddNamedDestinations(SimpleNamedDestination.GetNamedDestination(reader, false), -page_offset);
                                }
                                catch
                                {
                                    writer.AddNamedDestinations(SimpleNamedDestination.GetNamedDestination(reader, true), -page_offset);
                                }

                                IList<Dictionary<String, Object>> tmp = new List<Dictionary<String, Object>>();
                                List<Dictionary<String, Object>> bookmarks_write = new List<Dictionary<String, Object>>();
                                if (reader != null)
                                {
                                    tmp = SimpleBookmark.GetBookmark(reader);
                                }

                                if (tmp != null)
                                {
                                    SimpleBookmark.ShiftPageNumbers(tmp, -page_offset, null);

                                    for (int m = 0; m < tmp.Count; m++)
                                    {
                                        bookmarks_write.Add(tmp[m]);
                                    }
                                }

                                writer.Outlines = bookmarks_write;
                            }

                            if (document.IsOpen())
                            {
                                document.Close();   
                            }

                            page_offset = CurrentPage;

                            if (frmOptionsSplit.Instance.Bookmarks.chkAddExisting.Checked)
                            {
                                PDFHelper.MakeRemoteNamedDestinationsLocal(destinationFile);
                            }

                            //EncryptDocument(destinationFile, finalDestinationFile, Encoding.Default.GetString(reader.ComputeUserPassword()), sourcePasswords[f]);
                            EncryptDocument(destinationFile, finalDestinationFile,ownerpassword,userpassword);

                            // step 1: creation of a document-object
                            document = new Document(reader.GetPageSizeWithRotation(i));
                            /*
                            byte[] buserpwd = reader.ComputeUserPassword();
                            if (buserpwd != null)
                            {
                                userpassword = Encoding.Default.GetString(buserpwd);
                            }
                            */

                            destinationFile = System.IO.Path.GetTempFileName();

                            // step 2: we create a writer that listens to the document
                            writer = PdfWriter.GetInstance(document, new FileStream(destinationFile, FileMode.Create));
                            ;
                            // step 3: we open the document
                            document.Open();
                            cb = writer.DirectContent;

                            finalDestinationFile = "";

                            CurrentPageHeader = 0;
                            CurrentPageFooter = 0;
                            
                        }                                               
                        
                        if (finalDestinationFile == String.Empty)
                        {
                            finalDestinationFile = GetFinalDestinationFile(sourceFiles[f], i);

                            if (first_result_file == String.Empty)
                            {
                                first_result_file = finalDestinationFile;
                            }

                        }

                        //document.SetPageSize(reader.GetPageSizeWithRotation(i));

                        Rectangle rect = new Rectangle(reader.GetPageSizeWithRotation(i).Width, reader.GetPageSizeWithRotation(i).Height);
                        document.SetPageSize(rect);

                        document.NewPage();
                        
                        page = writer.GetImportedPage(reader, i);

                        rotation = reader.GetPageRotation(i);
                        if (rotation == 90 || rotation == 270)
                        {
                            cb.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(i).Height);
                        }
                        else
                        {
                            cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                        }
                                                
                        CurrentPage++;
                        //CurrentPageHeader++;
                        //CurrentPageFooter++;

                    }

                    // step 5: we close the document

                    if (frmOptionsSplit.Instance.Bookmarks.chkAddExisting.Checked)
                    {
                        try
                        {
                            writer.AddNamedDestinations(SimpleNamedDestination.GetNamedDestination(reader, false), -page_offset);
                        }
                        catch
                        {
                            writer.AddNamedDestinations(SimpleNamedDestination.GetNamedDestination(reader, true), -page_offset);
                        }

                        IList<Dictionary<String, Object>> tmp1 = new List<Dictionary<String, Object>>();
                        List<Dictionary<String, Object>> bookmarks_write1 = new List<Dictionary<String, Object>>();
                        if (reader != null)
                        {
                            tmp1 = SimpleBookmark.GetBookmark(reader);
                        }

                        if (tmp1 != null)
                        {
                            SimpleBookmark.ShiftPageNumbers(tmp1, -page_offset, null);

                            for (int m = 0; m < tmp1.Count; m++)
                            {
                                bookmarks_write1.Add(tmp1[m]);
                            }
                        }

                        writer.Outlines = bookmarks_write1;
                    }

                    if (document.IsOpen())
                    {
                        document.Close();
                    }

                    if (frmOptionsSplit.Instance.Bookmarks.chkAddExisting.Checked)
                    {
                        PDFHelper.MakeRemoteNamedDestinationsLocal(destinationFile);
                    }

                    EncryptDocument(destinationFile, finalDestinationFile,ownerpassword,userpassword);

                    //EncryptDocument(destinationFile, finalDestinationFile, userpassword, Password);
                }


                if (frmOptionsSplit.Instance.Misc.chkDeleteOriginals.Checked)
                {
                    string errd = "";
                    for (int k = 0; k < frmMain.Instance.dtSplit.Rows.Count; k++)
                    {
                        try
                        {
                            System.IO.File.Delete(frmMain.Instance.dtSplit.Rows[k]["cfilepath"].ToString());
                        }
                        catch (Exception exd)
                        {
                            errd = "Error could not delete File : " + frmMain.Instance.dtSplit.Rows[k]["cfilepath"].ToString() + " \n\n" + exd.Message;
                        }
                    }


                    if (errd != String.Empty)
                    {
                        Module.ShowMessage(errd);
                    }
                }

                if (frmOptionsSplit.Instance.Misc.chkOpenDestinationFolder.Checked)
                {
                    System.Diagnostics.Process.Start("explorer.exe", "/select,\"" + first_result_file+"\"");

                    /*
                    if (frmOptionsSplit.Instance.OutputFile.chkOther.Checked)
                    {
                        System.Diagnostics.Process.Start(frmOptionsSplit.Instance.OutputFile.txtOutputFolder.Text);
                    }
                    else
                    {
                        System.Diagnostics.Process.Start(System.IO.Path.GetDirectoryName(frmMain.Instance.lvDocsSplit.Items[0].SubItems[1].Text));
                    }
                    */
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

        public static bool ShouldAddHeader(int pagenum)
        {
            if (frmOptionsSplit.Instance.Header.txtHeader.Text != String.Empty)
            {
                if (pagenum < frmOptionsSplit.Instance.Header.nudFrom.Value ||
                    pagenum > frmOptionsSplit.Instance.Header.nudTo.Value)
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
            if (frmOptionsSplit.Instance.Header.txtHeaderImage.Text != String.Empty
                && System.IO.File.Exists(frmOptionsSplit.Instance.Header.txtHeaderImage.Text))
            {
                if (pagenum < frmOptionsSplit.Instance.Header.nudFrom.Value ||
                    pagenum > frmOptionsSplit.Instance.Header.nudTo.Value)
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
            if (frmOptionsSplit.Instance.Footer.txtFooter.Text != String.Empty)
            {
                if (pagenum < frmOptionsSplit.Instance.Footer.nudFrom.Value ||
                    pagenum > frmOptionsSplit.Instance.Footer.nudTo.Value)
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
            if (frmOptionsSplit.Instance.Footer.txtFooterImage.Text != String.Empty
                && System.IO.File.Exists(frmOptionsSplit.Instance.Footer.txtFooterImage.Text))
            {
                if (pagenum < frmOptionsSplit.Instance.Footer.nudFrom.Value ||
                    pagenum > frmOptionsSplit.Instance.Footer.nudTo.Value)
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

        public static bool ShouldSplit(int pagenum,PdfReader reader,Bookmarks bk,int blank_pages_count)
        {
            bool split = false;

            bkmarks_current_bookmark_index = -1;

            if (frmOptionsSplit.Instance.SplitPages.chkBookmarks.Checked)
            {
                bkmarks_full_text.Clear();
                bkmarks_short_text.Clear();
                bkmarks_level.Clear();

                int ibklevel = (int)frmOptionsSplit.Instance.SplitPages.nudBookmarkLevel.Value;
                List<int> bkpages = new List<int>();
                GetBookmarkPagesAtLevel(bk, 0, ibklevel, ref bkpages,"");

                if (bkpages.Contains(pagenum))
                {
                    bkmarks_current_bookmark_index = bkpages.IndexOf(pagenum);

                    return true;
                }
            }


            if (frmOptionsSplit.Instance.SplitPages.chkBlankPages.Checked)
            {
                int ibl = (int)frmOptionsSplit.Instance.SplitPages.nudBlankPages.Value;

                if (blank_pages_count!=0 && blank_pages_count % ibl==0)
                {
                    return true;
                }
            }

            if (frmOptionsSplit.Instance.SplitPages.chkEvery.Checked)
            {
                if (pagenum < frmOptionsSplit.Instance.SplitPages.nudEveryFrom.Value || pagenum > frmOptionsSplit.Instance.SplitPages.nudEveryTo.Value)
                {

                }
                else
                {
                    int ieveryfrom = (int)frmOptionsSplit.Instance.SplitPages.nudEveryFrom.Value;

                    if ((pagenum-ieveryfrom) % frmOptionsSplit.Instance.SplitPages.nudEvery.Value == 0)
                    {
                        return true;
                    }
                }
            }

            if (frmOptionsSplit.Instance.SplitPages.txtPageRanges.Text.Trim() != String.Empty)
            {
                string[] ranges = frmOptionsSplit.Instance.SplitPages.txtPageRanges.Text.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                for (int k = 0; k < ranges.Length; k++)
                {
                    string[] range = ranges[k].Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);

                    int ifrom = int.Parse(range[0]);
                    int ito = int.Parse(range[1]);

                    if (pagenum == ifrom)
                    {
                        return true;
                    }

                    
                }

            }

            if (frmOptionsSplit.Instance.SplitPages.chkText.Checked)
            {
                if (iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, pagenum).Contains(
                    frmOptionsSplit.Instance.SplitPages.txtText.Text))
                {
                    return true;
                }
            }

            return false;
        }

        private static void GetBookmarkPagesAtLevel(Bookmarks bk, int level,int desired_level,ref List<int> bkpages,string bkmark_current_full_text)
        {
            if (level == desired_level)
            {
                bkpages.Add(bk.Page);
                bkmarks_short_text.Add(bk.Text);
                bkmarks_full_text.Add((bkmark_current_full_text!=string.Empty ? bkmark_current_full_text + "." : "") + bk.Text);
                bkmarks_level.Add(level);
            }

            if (level < desired_level)
            {
                bkmark_current_full_text = (bkmark_current_full_text!=string.Empty ? bkmark_current_full_text + "." : "") + bk.Text;

                for (int m = 0; m < bk.ChildBookmarks.Count; m++)
                {                    
                    GetBookmarkPagesAtLevel(bk.ChildBookmarks[m], level+1, desired_level, ref bkpages,bkmark_current_full_text);
                }
            }
        }

        public static bool ShouldAddPage(int pagenum)
        {                      
            if (frmOptionsSplit.Instance.SplitPages.txtPageRanges.Text.Trim() != String.Empty)
            {
                string[] ranges = frmOptionsSplit.Instance.SplitPages.txtPageRanges.Text.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

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


                return false;
            }
            

            return true;            
        }

        private static int CalculateTotalPagesHeader(int numpages)
        {
            int total = 0;

            for (int k = 1; k <= numpages; k++)
            {
                if (ShouldAddPage(k) && ShouldAddHeader(k))
                {
                    total++;
                }
            }

            return total;
        }

        private static int CalculateTotalPagesFooter(int numpages)
        {
            int total = 0;

            for (int k = 1; k <= numpages; k++)
            {
                if (ShouldAddPage(k) && ShouldAddFooter(k))
                {
                    total++;
                }
            }

            return total;
        }

        private static string GetFinalDestinationFile(string filepath, int ipage)
        {
            string filename = frmOptionsSplit.Instance.OutputFile.txtOutputPattern.Text;
            filename = filename.Replace("[page]", ipage.ToString());
            filename = filename.Replace("[PAGE]", ipage.ToString());

            filename = filename.Replace("[file]", System.IO.Path.GetFileNameWithoutExtension(filepath));
            filename = filename.Replace("[FILE]", System.IO.Path.GetFileNameWithoutExtension(filepath));

            if (bkmarks_current_bookmark_index != -1)
            {
                filename = filename.Replace("[BOOKMARK_SHORT_TEXT]", bkmarks_short_text[bkmarks_current_bookmark_index]);
                filename = filename.Replace("[BOOKMARK_FULL_TEXT]", bkmarks_full_text[bkmarks_current_bookmark_index]);
                filename = filename.Replace("[BOOKMARK_LEVEL]", bkmarks_level[bkmarks_current_bookmark_index].ToString());

            }
            else
            {
                filename = filename.Replace("[BOOKMARK_SHORT_TEXT]", "");
                filename = filename.Replace("[BOOKMARK_FULL_TEXT]", "");
                filename = filename.Replace("[BOOKMARK_LEVEL]", "0");
            }

            filename = filename.Replace("[SPLIT_BOOKMARKS_LEVEL_VALUE]", frmOptionsSplit.Instance.SplitPages.nudBookmarkLevel.Value.ToString());

            /*
            filename=filename.Replace("[PAGE_FROM_VALUE]",frmOptionsSplit.Instance.SplitPages.nudEveryFrom.Value.ToString());
            filename=filename.Replace("[FOOTER_TEXT_VALUE]",frmOptionsSplit.Instance.Footer.txtFooter.Text);
            filename=filename.Replace("[HEADER_TEXT_VALUE]",frmOptionsSplit.Instance.Header.txtHeader.Text);
            filename=filename.Replace("[SPLIT_BLANK_PAGES_VALUE]",frmOptionsSplit.Instance.SplitPages.nudBlankPages.Value.ToString());
            filename=filename.Replace("[SPLIT_BOOKMARKS_LEVEL_VALUE]",frmOptionsSplit.Instance.SplitPages.nudBookmarkLevel.Value.ToString());
            filename=filename.Replace("[OWNER_PASSWORD_VALUE]",frmOptionsSplit.Instance.Properties.txtOwnerPassword.Text);
            filename=filename.Replace("[USER_PASSWORD_VALUE]",frmOptionsSplit.Instance.Properties.txtUserPassword.Text);
            filename=filename.Replace("[KEYWORDS_VALUE]",frmOptionsSplit.Instance.Properties.txtKeywords.Text);
            filename=filename.Replace("[SUBJECT_VALUE]",frmOptionsSplit.Instance.Properties.txtSubject.Text);
            filename=filename.Replace("[AUTHOR_VALUE]",frmOptionsSplit.Instance.Properties.txtAuthor.Text);
            filename=filename.Replace("[TITLE_VALUE]",frmOptionsSplit.Instance.Properties.txtTitle.Text);
            filename=filename.Replace("[PAGE_CONTAINING_TEXT_VALUE]",frmOptionsSplit.Instance.SplitPages.txtText.Text);
            filename=filename.Replace("[PAGE_EVERY_TO_VALUE]",frmOptionsSplit.Instance.SplitPages.nudEveryTo.Value.ToString());
            filename=filename.Replace("[PAGE_EVERY_FROM_VALUE]",frmOptionsSplit.Instance.SplitPages.nudEveryFrom.Value.ToString());
            filename=filename.Replace("[PAGE_EVERY_VALUE]",frmOptionsSplit.Instance.SplitPages.nudEvery.Value.ToString());
            filename=filename.Replace("[PAGE_ODD_TO_VALUE]",frmOptionsSplit.Instance.SplitPages);
            filename=filename.Replace("[TITLE_VALUE]",frmOptionsSplit.Instance.Properties.txtTitle.Text);
            */

            // ----------------

            filename = ArgsHelper.GetFilenameWithParameterValues(filename);

            filename = Module.RemoveIllegalCharactersFromFilepath(filename);

            int max_path_length = 259;

            if (frmOptionsSplit.Instance.OutputFile.chkSameFolder.Checked)
            {
                string fullfp = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(filepath), filename);

                if (fullfp.Length > max_path_length)
                {
                    string first = fullfp.Substring(0, max_path_length - 4);

                    return first + ".pdf";
                }
                
                return System.IO.Path.Combine(System.IO.Path.GetDirectoryName(filepath), filename);
            }
            else
            {
                string fullfp = System.IO.Path.Combine(frmOptionsSplit.Instance.OutputFile.txtOutputFolder.Text, filename);

                if (fullfp.Length > max_path_length)
                {
                    string first = fullfp.Substring(0, max_path_length - 4);

                    return first + ".pdf";
                }

                return System.IO.Path.Combine(frmOptionsSplit.Instance.OutputFile.txtOutputFolder.Text, filename);
            }

        }

        private static void EncryptDocument(string destinationFile, string finalDestinationFile, string ownerpassword, string userpassword)
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
                        if (frmOptionsSplit.Instance.Properties.chkAnnotations.Checked)
                        {
                            permissions |= PdfWriter.ALLOW_MODIFY_ANNOTATIONS;
                        }

                        if (frmOptionsSplit.Instance.Properties.chkAssembly.Checked)
                        {
                            permissions |= PdfWriter.ALLOW_ASSEMBLY;
                        }

                        if (frmOptionsSplit.Instance.Properties.chkCopy.Checked)
                        {
                            permissions |= PdfWriter.ALLOW_COPY;
                        }

                        if (frmOptionsSplit.Instance.Properties.chkFormFill.Checked)
                        {
                            permissions |= PdfWriter.ALLOW_FILL_IN;
                        }

                        if (frmOptionsSplit.Instance.Properties.chkHighPrinting.Checked)
                        {
                            permissions |= PdfWriter.ALLOW_PRINTING;
                        }

                        if (frmOptionsSplit.Instance.Properties.chkLowPrinting.Checked)
                        {
                            permissions |= PdfWriter.ALLOW_DEGRADED_PRINTING;
                        }

                        if (frmOptionsSplit.Instance.Properties.chkModifyContents.Checked)
                        {
                            permissions |= PdfWriter.ALLOW_MODIFY_CONTENTS;
                        }

                        if (frmOptionsSplit.Instance.Properties.chkScreenReaders.Checked)
                        {
                            permissions |= PdfWriter.ALLOW_SCREENREADERS;
                        }

                        PdfEncryptor.Encrypt(reader2, output, true, userpassword, ownerpassword, permissions);
                    }
                }

                System.IO.File.Delete(destinationFile);
            }
        }

        private static Bookmarks GetBookmarksTree(IList<Dictionary<String, Object>> bookmarks)
        {
            Bookmarks bk = new Bookmarks();

            GetBookmarksTreeLevel(bookmarks, ref bk);

            return bk;
        }

        private static void GetBookmarksTreeLevel(IList<Dictionary<String, Object>> bookmarks,ref Bookmarks bk)
        {
            int ipage = 0;
            
            for (int k = 0; k < bookmarks.Count; k++)
            {
                Bookmarks bknew = new Bookmarks();

                if (bookmarks[k].ContainsKey("Page"))
                {
                    string spage = bookmarks[k]["Page"].ToString();
                    int ipos = spage.IndexOf(" ");
                    ipage = int.Parse(spage.Substring(0, ipos));

                    if (!bk.ChildBookmarks.Contains(bknew))
                    {
                        bk.ChildBookmarks.Add(bknew);
                    }

                    bknew.Page = ipage;
                }

                if (bookmarks[k].ContainsKey("Title"))
                {                    
                    if (!bk.ChildBookmarks.Contains(bknew))
                    {
                        bk.ChildBookmarks.Add(bknew);
                    }

                    bknew.Text = bookmarks[k]["Title"].ToString();
                }

                if (bookmarks[k].ContainsKey("Kids"))
                {
                    IList<Dictionary<String, Object>> childbk = (IList<Dictionary<String, Object>>)bookmarks[k]["Kids"];
                    if (!bk.ChildBookmarks.Contains(bknew))
                    {
                        bk.ChildBookmarks.Add(bknew);
                    }
                    
                    GetBookmarksTreeLevel(childbk, ref bknew);
                }
            }
        }
        
    }

    public class Bookmarks
    {
        public List<Bookmarks> ChildBookmarks = new List<Bookmarks>();
        public int Page = -1;
        public string Text = "";

        public Bookmarks()
        {

        }

        public Bookmarks this[int k]
        {
            get
            {
                return ChildBookmarks[k];
            }
            set
            {
                ChildBookmarks[k] = value;
            }
        }
    }
}
