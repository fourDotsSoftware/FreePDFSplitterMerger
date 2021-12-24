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
    class DeleteHelper
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
        
        public static bool DeletePages()
        {
            List<string> sourceFiles = new List<string>();
            List<string> sourcePasswords = new List<string>();
            //List<Dictionary<String, Object>> bookmarks =
              //      new List<Dictionary<String, Object>>();

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

            try
            {
                for (int f = 0; f < sourceFiles.Count; f++)
                {
                    finalDestinationFile = "";
                    destinationFile = "";

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

                    if (!frmOptionsDelete.Instance.Properties.chkProperties.Checked)
                    {
                        document.AddAuthor(frmOptionsDelete.Instance.Properties.txtAuthor.Text);
                        document.AddKeywords(frmOptionsDelete.Instance.Properties.txtKeywords.Text);
                        document.AddSubject(frmOptionsDelete.Instance.Properties.txtSubject.Text);
                        document.AddTitle(frmOptionsDelete.Instance.Properties.txtTitle.Text);
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

                    ownerpassword = frmOptionsDelete.Instance.Properties.txtOwnerPassword.Text;
                    userpassword = frmOptionsDelete.Instance.Properties.txtUserPassword.Text;

                    addheader = false;
                    addheaderimage = false;
                    addfooter = false;
                    addfooterimage = false;

                    if (frmOptionsDelete.Instance.Header.txtHeader.Text != String.Empty)
                    {
                        addheader = true;
                    }

                    if (frmOptionsDelete.Instance.Footer.txtFooter.Text != String.Empty)
                    {
                        addfooter = true;
                    }

                    if (frmOptionsDelete.Instance.Header.txtHeaderImage.Text != String.Empty
                    && System.IO.File.Exists(frmOptionsDelete.Instance.Header.txtHeaderImage.Text))
                    {
                        addheaderimage = true;
                    }

                    if (frmOptionsDelete.Instance.Footer.txtFooter.Text != String.Empty
                    && System.IO.File.Exists(frmOptionsDelete.Instance.Footer.txtFooterImage.Text))
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
                            imgHeader = (System.Drawing.Image)FreeImageHelper.LoadImage(frmOptionsDelete.Instance.Header.txtHeaderImage.Text);
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
                        int totalpagesh = CalculateTotalPagesHeader(n,reader);

                        HeaderText = frmOptionsDelete.Instance.Header.txtHeader.Text.Replace("[pagenum]", totalpagesh.ToString())
                        .Replace("[PAGENUM]", totalpagesh.ToString()).Replace("[date]", DateTime.Now.ToShortDateString()).
                        Replace("[DATE]", DateTime.Now.ToShortDateString()).Replace("[title]", frmOptionsDelete.Instance.Properties.txtTitle.Text).
                        Replace("[TITLE]", frmOptionsDelete.Instance.Properties.txtTitle.Text).Replace("[author]", frmOptionsDelete.Instance.Properties.txtAuthor.Text).
                        Replace("[AUTHOR]", frmOptionsDelete.Instance.Properties.txtAuthor.Text).Replace("[subject]", frmOptionsDelete.Instance.Properties.txtSubject.Text).
                        Replace("[SUBJECT]", frmOptionsDelete.Instance.Properties.txtSubject.Text);
                    }

                    if (addfooter)
                    {
                        int totalpagesf = CalculateTotalPagesFooter(n,reader);

                        FooterText = frmOptionsDelete.Instance.Footer.txtFooter.Text.Replace("[pagenum]", totalpagesf.ToString())
                        .Replace("[PAGENUM]", totalpagesf.ToString()).Replace("[date]", DateTime.Now.ToShortDateString()).
                        Replace("[DATE]", DateTime.Now.ToShortDateString()).Replace("[title]", frmOptionsDelete.Instance.Properties.txtTitle.Text).
                        Replace("[TITLE]", frmOptionsDelete.Instance.Properties.txtTitle.Text).Replace("[author]", frmOptionsDelete.Instance.Properties.txtAuthor.Text).
                        Replace("[AUTHOR]", frmOptionsDelete.Instance.Properties.txtAuthor.Text).Replace("[subject]", frmOptionsDelete.Instance.Properties.txtSubject.Text).
                        Replace("[SUBJECT]", frmOptionsDelete.Instance.Properties.txtSubject.Text);
                    }

                    if (addfooterimage)
                    {
                        System.Drawing.Image imgFooter = null;

                        try
                        {
                            imgFooter = (System.Drawing.Image)FreeImageHelper.LoadImage(frmOptionsDelete.Instance.Footer.txtFooterImage.Text);
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

                    CustomPageEventHandlerDelete e = new CustomPageEventHandlerDelete();
                    writer.PageEvent = e;

                    CurrentPageHeader = 0;
                    CurrentPageFooter = 0;
                    CurrentPage = 0;

                    PdfContentByte cb = writer.DirectContent;
                    PdfImportedPage page = null;
                    int rotation;



                    // step 4: we add content

                    int i = 0;

                    while (i < n)
                    {
                        i++;

                        if (!ShouldAddPage(i,reader))
                        {
                            continue;
                        }
                        else
                        {
                            if (finalDestinationFile == String.Empty)
                            {
                                finalDestinationFile = GetFinalDestinationFile(sourceFiles[f], i);

                                if (first_result_file == String.Empty)
                                {
                                    first_result_file = finalDestinationFile;
                                }
                                
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
                        
                        page_offset++;

                        CurrentPage++;
                        //CurrentPageHeader++;
                        //CurrentPageFooter++;

                    }

                    // step 5: we close the document
                    document.Close();

                    EncryptDocument(destinationFile, finalDestinationFile,ownerpassword,userpassword);
                }
                                             

                if (frmOptionsDelete.Instance.Misc.chkDeleteOriginals.Checked)
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

                if (frmOptionsDelete.Instance.Misc.chkOpenDestinationFolder.Checked)
                {
                    System.Diagnostics.Process.Start("explorer.exe", "/select,\"" + first_result_file+"\"");

                    /*
                    if (frmOptionsDelete.Instance.OutputFile.chkOther.Checked)
                    {
                        System.Diagnostics.Process.Start(frmOptionsDelete.Instance.OutputFile.txtOutputFolder.Text);
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
            if (frmOptionsDelete.Instance.Header.txtHeader.Text != String.Empty)
            {
                if (pagenum < frmOptionsDelete.Instance.Header.nudFrom.Value ||
                    pagenum > frmOptionsDelete.Instance.Header.nudTo.Value)
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
            if (frmOptionsDelete.Instance.Header.txtHeaderImage.Text != String.Empty
                && System.IO.File.Exists(frmOptionsDelete.Instance.Header.txtHeaderImage.Text))
            {
                if (pagenum < frmOptionsDelete.Instance.Header.nudFrom.Value ||
                    pagenum > frmOptionsDelete.Instance.Header.nudTo.Value)
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
            if (frmOptionsDelete.Instance.Footer.txtFooter.Text != String.Empty)
            {
                if (pagenum < frmOptionsDelete.Instance.Footer.nudFrom.Value ||
                    pagenum > frmOptionsDelete.Instance.Footer.nudTo.Value)
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
            if (frmOptionsDelete.Instance.Footer.txtFooterImage.Text != String.Empty
                && System.IO.File.Exists(frmOptionsDelete.Instance.Footer.txtFooterImage.Text))
            {
                if (pagenum < frmOptionsDelete.Instance.Footer.nudFrom.Value ||
                    pagenum > frmOptionsDelete.Instance.Footer.nudTo.Value)
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

        public static bool ShouldAddPage(int pagenum,PdfReader reader)
        {            
            bool add = true;

            if (frmOptionsDelete.Instance.DeleteExtractPages.chkPagesFromTo.Checked)
            {
                if (pagenum < frmOptionsDelete.Instance.DeleteExtractPages.nudFrom.Value || pagenum > frmOptionsDelete.Instance.DeleteExtractPages.nudTo.Value)
                {

                }
                else
                {
                    return false;

                }

            }

            if (frmOptionsDelete.Instance.DeleteExtractPages.chkOdd.Checked)
            {
                if (pagenum % 2 != 0)
                {
                    if (pagenum < frmOptionsDelete.Instance.DeleteExtractPages.nudOddFrom.Value || pagenum > frmOptionsDelete.Instance.DeleteExtractPages.nudOddTo.Value)
                    {

                    }
                    else
                    {
                        return false;
                    }
                }
            }

            if (frmOptionsDelete.Instance.DeleteExtractPages.chkEven.Checked)
            {
                if (pagenum % 2 == 0)
                {

                    if (pagenum < frmOptionsDelete.Instance.DeleteExtractPages.nudEvenFrom.Value || pagenum > frmOptionsDelete.Instance.DeleteExtractPages.nudEvenTo.Value)
                    {

                    }
                    else
                    {
                        return false;
                    }
                }
            }

            if (frmOptionsDelete.Instance.DeleteExtractPages.chkEvery.Checked)
            {
                if (pagenum < frmOptionsDelete.Instance.DeleteExtractPages.nudEveryFrom.Value || pagenum > frmOptionsDelete.Instance.DeleteExtractPages.nudEveryTo.Value)
                {

                }
                else
                {
                    int ieveryfrom = (int)frmOptionsDelete.Instance.DeleteExtractPages.nudEveryFrom.Value;

                    if ((pagenum-ieveryfrom) % frmOptionsDelete.Instance.DeleteExtractPages.nudEvery.Value == 0)
                    {
                        return false;
                    }
                }
            }

            if (frmOptionsDelete.Instance.DeleteExtractPages.txtPageRanges.Text.Trim() != String.Empty)
            {
                string[] ranges = frmOptionsDelete.Instance.DeleteExtractPages.txtPageRanges.Text.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

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
                        return false;
                    }
                }

            }

            if (frmOptionsDelete.Instance.DeleteExtractPages.chkText.Checked)
            {
                if (iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, pagenum).Contains(
                    frmOptionsDelete.Instance.DeleteExtractPages.txtText.Text))
                {
                    return true;
                }
            }

            return true;
        }

        private static int CalculateTotalPagesHeader(int numpages,PdfReader reader)
        {
            int total = 0;

            for (int k = 1; k <= numpages; k++)
            {
                if (ShouldAddPage(k,reader) && ShouldAddHeader(k))
                {
                    total++;
                }
            }

            return total;
        }

        private static int CalculateTotalPagesFooter(int numpages,PdfReader reader)
        {
            int total = 0;

            for (int k = 1; k <= numpages; k++)
            {
                if (ShouldAddPage(k,reader) && ShouldAddFooter(k))
                {
                    total++;
                }
            }

            return total;
        }

        private static string GetFinalDestinationFile(string filepath, int ipage)
        {
            string filename = frmOptionsDelete.Instance.OutputFile.txtOutputPattern.Text;
            filename = filename.Replace("[page]", ipage.ToString());
            filename = filename.Replace("[PAGE]", ipage.ToString());

            filename = filename.Replace("[file]", System.IO.Path.GetFileNameWithoutExtension(filepath));
            filename = filename.Replace("[FILE]", System.IO.Path.GetFileNameWithoutExtension(filepath));

            filename = ArgsHelper.GetFilenameWithParameterValues(filename); 

            if (frmOptionsDelete.Instance.OutputFile.chkSameFolder.Checked)
            {
                return System.IO.Path.Combine(System.IO.Path.GetDirectoryName(filepath), filename);
            }
            else
            {
                return System.IO.Path.Combine(frmOptionsDelete.Instance.OutputFile.txtOutputFolder.Text, filename);
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
                        if (frmOptionsDelete.Instance.Properties.chkAnnotations.Checked)
                        {
                            permissions |= PdfWriter.ALLOW_MODIFY_ANNOTATIONS;
                        }

                        if (frmOptionsDelete.Instance.Properties.chkAssembly.Checked)
                        {
                            permissions |= PdfWriter.ALLOW_ASSEMBLY;
                        }

                        if (frmOptionsDelete.Instance.Properties.chkCopy.Checked)
                        {
                            permissions |= PdfWriter.ALLOW_COPY;
                        }

                        if (frmOptionsDelete.Instance.Properties.chkFormFill.Checked)
                        {
                            permissions |= PdfWriter.ALLOW_FILL_IN;
                        }

                        if (frmOptionsDelete.Instance.Properties.chkHighPrinting.Checked)
                        {
                            permissions |= PdfWriter.ALLOW_PRINTING;
                        }

                        if (frmOptionsDelete.Instance.Properties.chkLowPrinting.Checked)
                        {
                            permissions |= PdfWriter.ALLOW_DEGRADED_PRINTING;
                        }

                        if (frmOptionsDelete.Instance.Properties.chkModifyContents.Checked)
                        {
                            permissions |= PdfWriter.ALLOW_MODIFY_CONTENTS;
                        }

                        if (frmOptionsDelete.Instance.Properties.chkScreenReaders.Checked)
                        {
                            permissions |= PdfWriter.ALLOW_SCREENREADERS;
                        }

                        PdfEncryptor.Encrypt(reader2, output, true, userpassword, ownerpassword, permissions);
                    }
                }

                System.IO.File.Delete(destinationFile);
            }
        }
    }
}
