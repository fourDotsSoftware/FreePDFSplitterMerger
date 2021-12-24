using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

//-merge "c:\10\Csharp 4.0 - Complete Reference.pdf" "c:\10\Encyclopedia_of_Mind_Enhancing.merged.pdf" -outfilename:"mergedpdf.pdf" -pagefrom:10 -pageto:20

namespace PdfMergeSplitTool
{ 
    class ArgsHelper
    {
        public static void ExamineArgs()
        {
            //Console.WriteLine("silent=" + Module.Silent.ToString());
            for (int k = 0; k < Module.args.Length; k++)
            {
                //Module.ShowMessage(Directory.Exists(Module.args[0]).ToString());
                //Console.WriteLine("args[" + k.ToString() + "]=" + Module.args[k]);
                Module.args[k] = Module.args[k].Trim();

                if ((Module.args[k].StartsWith("\"") && Module.args[k].EndsWith("\"")) ||
                    (Module.args[k].StartsWith("'") && Module.args[k].EndsWith("'")))
                {
                    if (Module.args[k].Length > 2)
                    {
                        Module.args[k] = Module.args[k].Substring(1, Module.args[k].Length - 2);
                    }
                    else
                    {
                        Module.args[k] = "";
                    }
                }
            }

            string[] args = Module.args;

            if (args == null || args.Length==0) return;

            
                        

            try
            {
                bool add_subdirectories=false;
                bool init = true;               
                
                for (int k = 0; k < Module.args.Length; k++)
                {                    
                    if (Module.args[k].ToLower() == "/merge" ||
                        Module.args[k].ToLower() == "-merge")
                    {
                        Module.CmdMerge=true;
                    }
                    else if (Module.args[k].ToLower() == "/split" ||
                        Module.args[k].ToLower() == "-split")
                    {
                        Module.CmdSplit=true;
                    }
                    else if (Module.args[k].ToLower() == "/delete" ||
                        Module.args[k].ToLower() == "-delete")
                    {
                        Module.CmdDelete=true;
                    }
                    else if (Module.args[k].ToLower() == "/extract" ||
                        Module.args[k].ToLower() == "-extract")
                    {
                        Module.CmdExtract=true;
                    }
                    else if (Module.args[k].ToLower() == "/subdirs" ||
                        Module.args[k].ToLower() == "-subdirs")
                    {
                        Module.CmdAddSubdirectories = true;
                    }
                    else if (Module.args[k].ToLower() == "/h" ||
         Module.args[k].ToLower() == "-h" ||
         Module.args[k].ToLower() == "-?" ||
         Module.args[k].ToLower() == "/?")
                    {
                        ShowCommandUsage();
                    }
                    
                }

                for (int k=0;k<Module.args.Length;k++)
                {
                    if (System.IO.File.Exists(Module.args[k]))
                    {
                        if (Module.CmdMerge)
                        {
                            bool isimage = false;

                            try
                            {
                                if (!Module.args[k].ToLower().EndsWith(".pdf"))
                                {
                                    FreeImageHelper.LoadImage(Module.args[k]);
                                    isimage = true;
                                }
                            }
                            catch
                            {
                                Module.ShowError("Unrecognized file type !");
                                continue;
                            }

                            frmMain.Instance.AddFile(Module.args[k],isimage);
                        }
                        else
                        {
                            frmMain.Instance.AddFileSplit(Module.args[k]);
                        }
                    }
                    else if (System.IO.Directory.Exists(Module.args[k]))
                    {
                        if (Module.CmdMerge)
                        {
                            frmMain.Instance.AddFolder(Module.args[k]);
                        }
                        else
                        {
                            frmMain.Instance.AddFolderSplit(Module.args[k]);
                        }
                    }                    
                }

                frmOptionsDelete fdel= new frmOptionsDelete();
                frmOptionsExtract fex = new frmOptionsExtract();
                frmOptionsSplit fsplit = new frmOptionsSplit();
                frmOptions fmer = new frmOptions();

                frmOptionsSplit.Instance.Misc.chkOpenDestinationFolder.Checked = false;
                frmOptionsDelete.Instance.Misc.chkOpenDestinationFolder.Checked = false;
                frmOptionsExtract.Instance.Misc.chkOpenDestinationFolder.Checked = false;
                frmOptions.Instance.Misc.chkOpenDestinationFolder.Checked = false; 


                for (int k = 0; k < Module.args.Length; k++)
                {
                    if (Module.args[k].ToLower().StartsWith("/pagefrom:") ||
                        Module.args[k].ToLower().StartsWith("-pagefrom:"))
                    {
                        Module.CmdPageFrom = int.Parse(GetParameter(Module.args[k]));

                        fdel.DeleteExtractPages.nudFrom.Value = Module.CmdPageFrom;
                        fex.ExtractPages.nudFrom.Value = Module.CmdPageFrom;

                        fdel.DeleteExtractPages.chkPagesFromTo.Checked = true;
                        fex.ExtractPages.chkPagesFromTo.Checked = true;

                        for (int m = 0; m < frmMain.Instance.dtDocs.Rows.Count; m++)
                        {
                            PageRange pg = (PageRange)frmMain.Instance.dtDocs.Rows[m]["cTag"];
                            pg.PagesFrom = Module.CmdPageFrom;
                            pg.Pages = true;
                            pg.AllPages = false;
                        }

                    }
                    else if (Module.args[k].ToLower().StartsWith("/pageto:") ||
                        Module.args[k].ToLower().StartsWith("-pageto:"))
                    {
                        Module.CmdPageTo = int.Parse(GetParameter(Module.args[k]));

                        fdel.DeleteExtractPages.nudTo.Value = Module.CmdPageTo;
                        fex.ExtractPages.nudTo.Value = Module.CmdPageTo;

                        fdel.DeleteExtractPages.chkPagesFromTo.Checked = true;
                        fex.ExtractPages.chkPagesFromTo.Checked = true;

                        for (int m = 0; m < frmMain.Instance.dtDocs.Rows.Count; m++)
                        {
                            PageRange pg = (PageRange)frmMain.Instance.dtDocs.Rows[m]["cTag"];
                            pg.PagesTo = Module.CmdPageTo;
                            pg.Pages = true;
                            pg.AllPages = false;
                        }
                    }
                    else if (Module.args[k].ToLower().StartsWith("/pageevenfrom:") ||
                        Module.args[k].ToLower().StartsWith("-pageevenfrom:"))
                    {
                        Module.CmdPageEvenFrom = int.Parse(GetParameter(Module.args[k]));

                        fdel.DeleteExtractPages.nudEvenFrom.Value = Module.CmdPageEvenFrom;
                        fex.ExtractPages.nudEvenFrom.Value = Module.CmdPageEvenFrom;

                        fdel.DeleteExtractPages.chkEven.Checked = true;
                        fex.ExtractPages.chkEven.Checked = true;
                                                
                        for (int m = 0; m < frmMain.Instance.dtDocs.Rows.Count; m++)
                        {
                            PageRange pg = (PageRange)frmMain.Instance.dtDocs.Rows[m]["cTag"];
                            pg.PagesEvenFrom = Module.CmdPageEvenFrom;
                            pg.PagesEven = true;
                            pg.AllPages = false;
                            
                        }
                                                
                    }
                    else if (Module.args[k].ToLower().StartsWith("/pageevento:") ||
                        Module.args[k].ToLower().StartsWith("-pageevento:"))
                    {
                        Module.CmdPageEvenTo = int.Parse(GetParameter(Module.args[k]));

                        fdel.DeleteExtractPages.nudEvenTo.Value = Module.CmdPageEvenTo;
                        fex.ExtractPages.nudEvenTo.Value = Module.CmdPageEvenTo;

                        fdel.DeleteExtractPages.chkEven.Checked = true;
                        fex.ExtractPages.chkEven.Checked = true;

                        for (int m = 0; m < frmMain.Instance.dtDocs.Rows.Count; m++)
                        {
                            PageRange pg = (PageRange)frmMain.Instance.dtDocs.Rows[m]["cTag"];
                            pg.PagesEvenTo = Module.CmdPageEvenTo;
                            pg.PagesEven = true;
                            pg.AllPages = false;
                        }

                    }
                    else if (Module.args[k].ToLower().StartsWith("/pageoddfrom:") ||
                        Module.args[k].ToLower().StartsWith("-pageoddfrom:"))
                    {
                        Module.CmdPageOddFrom = int.Parse(GetParameter(Module.args[k]));

                        fdel.DeleteExtractPages.nudOddFrom.Value = Module.CmdPageOddFrom;
                        fex.ExtractPages.nudOddFrom.Value = Module.CmdPageOddFrom;

                        fdel.DeleteExtractPages.chkOdd.Checked = true;
                        fex.ExtractPages.chkOdd.Checked = true;

                        for (int m = 0; m < frmMain.Instance.dtDocs.Rows.Count; m++)
                        {
                            PageRange pg = (PageRange)frmMain.Instance.dtDocs.Rows[m]["cTag"];
                            pg.PagesOddFrom = Module.CmdPageOddFrom;
                            pg.PagesOdd = true;
                            pg.AllPages = false;
                        }
                    }
                    else if (Module.args[k].ToLower().StartsWith("/pageoddto:") ||
                        Module.args[k].ToLower().StartsWith("-pageoddto:"))
                    {
                        Module.CmdPageOddTo = int.Parse(GetParameter(Module.args[k]));

                        fdel.DeleteExtractPages.nudOddTo.Value = Module.CmdPageOddTo;
                        fex.ExtractPages.nudOddTo.Value = Module.CmdPageOddTo;

                        fdel.DeleteExtractPages.chkOdd.Checked = true;
                        fex.ExtractPages.chkOdd.Checked = true;

                        for (int m = 0; m < frmMain.Instance.dtDocs.Rows.Count; m++)
                        {
                            PageRange pg = (PageRange)frmMain.Instance.dtDocs.Rows[m]["cTag"];
                            pg.PagesOddTo = Module.CmdPageOddTo;
                            pg.PagesOdd = true;
                            pg.AllPages = false;
                        }
                    }
                    else if (Module.args[k].ToLower().StartsWith("/pagerange:") ||
                   Module.args[k].ToLower().StartsWith("-pagerange:"))
                    {
                        Module.CmdPageRange = GetParameter(Module.args[k]);

                        fdel.DeleteExtractPages.txtPageRanges.Text = Module.CmdPageRange;
                        fex.ExtractPages.txtPageRanges.Text = Module.CmdPageRange;
                        fsplit.SplitPages.txtPageRanges.Text = Module.CmdPageRange;

                        for (int m = 0; m < frmMain.Instance.dtDocs.Rows.Count; m++)
                        {
                            PageRange pg = (PageRange)frmMain.Instance.dtDocs.Rows[m]["cTag"];
                            pg.PageRanges = Module.CmdPageRange;
                            pg.AllPages = false;
                        }
                    }
                    else if (Module.args[k].ToLower().StartsWith("/pageevery:") ||
                   Module.args[k].ToLower().StartsWith("-pageevery:"))
                    {
                        Module.CmdPageEvery = int.Parse(GetParameter(Module.args[k]));

                        fdel.DeleteExtractPages.nudEvery.Value = Module.CmdPageEvery;
                        fex.ExtractPages.nudEvery.Value = Module.CmdPageEvery;
                        fsplit.SplitPages.nudEvery.Value = Module.CmdPageEvery;

                        fdel.DeleteExtractPages.chkEvery.Checked = true;
                        fex.ExtractPages.chkEvery.Checked = true;
                        fsplit.SplitPages.chkEvery.Checked = true;

                        for (int m = 0; m < frmMain.Instance.dtDocs.Rows.Count; m++)
                        {
                            PageRange pg = (PageRange)frmMain.Instance.dtDocs.Rows[m]["cTag"];
                            pg.PagesEveryValue = Module.CmdPageEvery;
                            pg.PagesEvery = true;
                            pg.AllPages = false;
                        }
                    }
                    else if (Module.args[k].ToLower().StartsWith("/pageeveryfrom:") ||
                        Module.args[k].ToLower().StartsWith("-pageeveryfrom:"))
                    {
                        Module.CmdPageEveryFrom = int.Parse(GetParameter(Module.args[k]));

                        fdel.DeleteExtractPages.nudEveryFrom.Value = Module.CmdPageEveryFrom;
                        fex.ExtractPages.nudEveryFrom.Value = Module.CmdPageEveryFrom;
                        fsplit.SplitPages.nudEveryFrom.Value = Module.CmdPageEveryFrom;

                        fdel.DeleteExtractPages.chkEvery.Checked = true;
                        fex.ExtractPages.chkEvery.Checked = true;
                        fsplit.SplitPages.chkEvery.Checked = true;

                        for (int m = 0; m < frmMain.Instance.dtDocs.Rows.Count; m++)
                        {
                            PageRange pg = (PageRange)frmMain.Instance.dtDocs.Rows[m]["cTag"];
                            pg.PagesEveryFrom = Module.CmdPageEveryFrom;
                            pg.PagesEvery = true;
                            pg.AllPages = false;
                        }
                    }
                    else if (Module.args[k].ToLower().StartsWith("/pageeveryto:") ||
                        Module.args[k].ToLower().StartsWith("-pageeveryto:"))
                    {
                        Module.CmdPageEveryTo = int.Parse(GetParameter(Module.args[k]));

                        fdel.DeleteExtractPages.nudEveryTo.Value = Module.CmdPageEveryTo;
                        fex.ExtractPages.nudEveryTo.Value = Module.CmdPageEveryTo;
                        fsplit.SplitPages.nudEveryTo.Value = Module.CmdPageEveryTo;

                        fdel.DeleteExtractPages.chkEvery.Checked = true;
                        fex.ExtractPages.chkEvery.Checked = true;
                        fsplit.SplitPages.chkEvery.Checked = true;

                        for (int m = 0; m < frmMain.Instance.dtDocs.Rows.Count; m++)
                        {
                            PageRange pg = (PageRange)frmMain.Instance.dtDocs.Rows[m]["cTag"];
                            pg.PagesEveryTo = Module.CmdPageEveryTo;
                            pg.PagesEvery = true;
                            pg.AllPages = false;
                        }

                    }
                    else if (Module.args[k].ToLower().StartsWith("/noexistingbookmarks")
                        || Module.args[k].ToLower().StartsWith("-noexistingbookmarks"))
                    {
                        fsplit.Bookmarks.chkAddExisting.Checked=false;
                        fmer.Bookmarks.chkAddExisting.Checked=false;                        
                    }
                    else if (Module.args[k].ToLower().StartsWith("/title:") ||
                   Module.args[k].ToLower().StartsWith("-title:"))
                    {
                        Module.CmdTitle = GetParameter(Module.args[k]);

                        fdel.Properties.txtTitle.Text = Module.CmdTitle;
                        fex.Properties.txtTitle.Text = Module.CmdTitle;
                        fsplit.Properties.txtTitle.Text = Module.CmdTitle;
                        fmer.Properties.txtTitle.Text = Module.CmdTitle;

                    }
                    else if (Module.args[k].ToLower().StartsWith("/subject:") ||
              Module.args[k].ToLower().StartsWith("-subject:"))
                    {
                        Module.CmdSubject = GetParameter(Module.args[k]);

                        fdel.Properties.txtSubject.Text = Module.CmdSubject;
                        fex.Properties.txtSubject.Text = Module.CmdSubject;
                        fsplit.Properties.txtSubject.Text = Module.CmdSubject;
                        fmer.Properties.txtSubject.Text = Module.CmdSubject;
                        
                    }
                    else if (Module.args[k].ToLower().StartsWith("/author:") ||
              Module.args[k].ToLower().StartsWith("-author:"))
                    {
                        Module.CmdAuthor = GetParameter(Module.args[k]);

                        fdel.Properties.txtAuthor.Text = Module.CmdAuthor;
                        fex.Properties.txtAuthor.Text = Module.CmdAuthor;
                        fsplit.Properties.txtAuthor.Text = Module.CmdAuthor;
                        fmer.Properties.txtAuthor.Text = Module.CmdAuthor;
                    }
                    else if (Module.args[k].ToLower().StartsWith("/keywords:") ||
              Module.args[k].ToLower().StartsWith("-keywords:"))
                    {
                        Module.CmdKeywords = GetParameter(Module.args[k]);

                        fdel.Properties.txtKeywords.Text = Module.CmdKeywords;
                        fex.Properties.txtKeywords.Text = Module.CmdKeywords;
                        fsplit.Properties.txtKeywords.Text = Module.CmdKeywords;
                        fmer.Properties.txtKeywords.Text = Module.CmdKeywords;
                    }
                    else if (Module.args[k].ToLower().StartsWith("/userpassword:") ||
              Module.args[k].ToLower().StartsWith("-userpassword:"))
                    {
                        Module.CmdUserPassword = GetParameter(Module.args[k]);

                        fdel.Properties.txtUserPassword.Text = Module.CmdUserPassword;
                        fex.Properties.txtUserPassword.Text = Module.CmdUserPassword;
                        fsplit.Properties.txtUserPassword.Text = Module.CmdUserPassword;
                        fmer.Properties.txtUserPassword.Text = Module.CmdUserPassword;
                    }
                    else if (Module.args[k].ToLower().StartsWith("/ownerpassword:") ||
              Module.args[k].ToLower().StartsWith("-ownerpassword:"))
                    {
                        Module.CmdOwnerPassword = GetParameter(Module.args[k]);

                        fdel.Properties.txtOwnerPassword.Text = Module.CmdOwnerPassword;
                        fex.Properties.txtOwnerPassword.Text = Module.CmdOwnerPassword;
                        fsplit.Properties.txtOwnerPassword.Text = Module.CmdOwnerPassword;
                        fmer.Properties.txtOwnerPassword.Text = Module.CmdOwnerPassword;
                    }
                    else if (Module.args[k].ToLower().StartsWith("/headertext:") ||
              Module.args[k].ToLower().StartsWith("-headertext:"))
                    {
                        Module.CmdHeaderText = GetParameter(Module.args[k]);

                        fdel.Header.txtHeader.Text = Module.CmdHeaderText;
                        fex.Header.txtHeader.Text = Module.CmdHeaderText;
                        fsplit.Header.txtHeader.Text = Module.CmdHeaderText;
                        fmer.Header.txtHeader.Text = Module.CmdHeaderText;
                    }
                    else if (Module.args[k].ToLower().StartsWith("/footertext:") ||
         Module.args[k].ToLower().StartsWith("-footertext:"))
                    {
                        Module.CmdFooterText = GetParameter(Module.args[k]);

                        fdel.Footer.txtFooter.Text = Module.CmdFooterText;
                        fex.Footer.txtFooter.Text = Module.CmdFooterText;
                        fsplit.Footer.txtFooter.Text = Module.CmdFooterText;
                        fmer.Footer.txtFooter.Text = Module.CmdFooterText;

                    }
                    else if (Module.args[k].ToLower().StartsWith("/splitbookmarks:") ||
    Module.args[k].ToLower().StartsWith("-splitbookmarks:"))
                    {
                        Module.CmdSplitBookmarks = int.Parse(GetParameter(Module.args[k]));

                        fsplit.SplitPages.nudBookmarkLevel.Value = Module.CmdSplitBookmarks;
                        fsplit.SplitPages.chkBookmarks.Checked = true;
                    }
                    else if (Module.args[k].ToLower().StartsWith("/splitblank:") ||
    Module.args[k].ToLower().StartsWith("-splitblank:"))
                    {
                        Module.CmdSplitBlank = int.Parse(GetParameter(Module.args[k]));

                        fsplit.SplitPages.nudBlankPages.Value = Module.CmdSplitBlank;
                        fsplit.SplitPages.chkBlankPages.Checked = true;

                    }
                    else if (Module.args[k].ToLower().StartsWith("/outfilename:") ||
    Module.args[k].ToLower().StartsWith("-outfilename:"))
                    {
                        Module.CmdOutputFile = GetParameter(Module.args[k]);

                        fdel.OutputFile.txtOutputPattern.Text = Module.CmdOutputFile;
                        fex.OutputFile.txtOutputPattern.Text = Module.CmdOutputFile;
                        fsplit.OutputFile.txtOutputPattern.Text = Module.CmdOutputFile;
                        fmer.OutputFile.txtOutputPattern.Text = Module.CmdOutputFile;

                    }
                    else if (Module.args[k].ToLower().StartsWith("/outfolder:") ||
    Module.args[k].ToLower().StartsWith("-outfolder:") ||
    Module.args[k].ToLower().StartsWith("/outputfolder:") ||
    Module.args[k].ToLower().StartsWith("-outputfolder:")    
    )
                    {
                        Module.CmdOutputFolder = GetParameter(Module.args[k]);

                        fdel.OutputFile.txtOutputFolder.Text = Module.CmdOutputFolder;
                        fex.OutputFile.txtOutputFolder.Text = Module.CmdOutputFolder;
                        fsplit.OutputFile.txtOutputFolder.Text = Module.CmdOutputFolder;
                        fmer.OutputFile.txtOutputFolder.Text = Module.CmdOutputFolder;

                        fdel.OutputFile.chkOther.Checked = true;
                        fex.OutputFile.chkOther.Checked = true;
                        fsplit.OutputFile.chkOther.Checked = true;
                        fmer.OutputFile.chkOther.Checked = true;
                    }
                    else if (Module.args[k].ToLower().StartsWith("/pagecontaining:") ||
                   Module.args[k].ToLower().StartsWith("-pagecontaining:"))
                    {
                        Module.CmdPageContaining = GetParameter(Module.args[k]);

                        fdel.DeleteExtractPages.txtText.Text = Module.CmdPageContaining;
                        fex.ExtractPages.txtText.Text = Module.CmdPageContaining;
                        fsplit.SplitPages.txtText.Text = Module.CmdPageContaining;

                        fdel.DeleteExtractPages.chkText.Checked = true;
                        fex.ExtractPages.chkText.Checked = true;
                        fsplit.SplitPages.chkText.Checked = true;

                        for (int m = 0; m < frmMain.Instance.dtDocs.Rows.Count; m++)
                        {
                            PageRange pg = (PageRange)frmMain.Instance.dtDocs.Rows[m]["cTag"];
                            pg.PageText = Module.CmdPageContaining;
                            pg.PagesContainingText = true;
                            pg.AllPages = false;
                        }
                    }
                }

                if (!Module.CmdMerge && !Module.CmdSplit && !Module.CmdExtract && !Module.CmdDelete)
                {
                    Module.ShowMessage("Please specify the action : -merge, -split,-delete, or -extract");
                    ShowCommandUsage();
                    return;
                }

                /*
                if (frmMain.Instance.lvDocs.Items.Count > 3 && frmAbout.LDT == String.Empty)
                {
                    Module.ShowMessage("Sorry, a full license is required in order to merge more than 3 Pdf Documents !");
                    return;
                }
                */

                if (Module.CmdOutputFile == String.Empty)
                {
                    Module.ShowMessage("Please specify an output filename !");
                    ShowCommandUsage();
                    return;
                }

                if (frmMain.Instance.dtDocs.Rows.Count == 0 && frmMain.Instance.dtSplit.Rows.Count == 0)
                {
                    Module.ShowMessage("Please specify pdf documents or images to be processed !");
                    ShowCommandUsage();
                    return;
                }

                if (Module.CmdOutputFolder!=String.Empty && !System.IO.Directory.Exists(Module.CmdOutputFolder))
                {
                    Module.ShowMessage("Please specify an existing output folder !");
                    ShowCommandUsage();
                    return;
                }
            }
            catch (Exception ex)
            {
                Module.ShowError("Error. Invalid Arguments !"+ex.ToString());
                ShowCommandUsage();
                return;
            }
            
            try
            {
                //Console.WriteLine("reading lists");

                //ReadListsResult res=ReadLists();
                //Console.WriteLine("suc=" + res.Success.ToString());
                //Console.WriteLine("err=" + res.err);
                /*
                if (!res.Success)
                {
                    Module.ShowError(res.err);
                    ShowCommandUsage();
                }
                else if (res.err!=String.Empty)
                {
                    Module.ShowError(res.err);
                }
                */
            }
            catch (Exception ex)
            {
                Module.ShowMessage("Error could not read List Files !");
                ShowCommandUsage();
                return;
            }

        }

        private static string GetParameter(string arg)
        {
            int spos = arg.IndexOf(":");
            if (spos == arg.Length - 1) return "";
            else
            {
                string str=arg.Substring(spos + 1);

                if ((str.StartsWith("\"") && str.EndsWith("\"")) ||
                    (str.StartsWith("'") && str.EndsWith("'")))
                {
                    if (str.Length > 2)
                    {
                        str = str.Substring(1, str.Length - 2);
                    }
                    else
                    {
                        str = "";
                    }
                }

                return str;
            }
        }

        public static void ShowCommandUsage()
        {
            string msg = GetCommandUsage();

            Module.ShowMessage(msg);

            Environment.Exit(0);
        }
        public static string GetCommandUsage()
        {
            string msg = "Merges, splits , extracts and deletes pages from Pdf Files.\n\n" +
            "FreePDFSplitterMerger.exe [-merge|-split|-delete|-extract] [[file|directory]]" +
            "[/pagefrom:PAGE_FROM_VALUE] " +
            "[/pageto:PAGE_TO_VALUE] " +
            "[/pagerange:PAGE_RANGE_VALUE] " +
            "[/pageevenfrom:PAGE_EVEN_FROM_VALUE] " +
            "[/pageevento:PAGE_EVEN_TO_VALUE] " +
            "[/pageoddfrom:PAGE_ODD_FROM_VALUE] " +
            "[/pageoddto:PAGE_ODD_TO_VALUE] " +
            "[/pageevery:PAGE_EVERY_VALUE] " +
            "[/pageeveryfrom:PAGE_EVERY_FROM_VALUE] " +
            "[/pageeveryto:PAGE_EVERY_TO_VALUE] " +
            "[/pagecontaining:PAGE_CONTAINING_TEXT_VALUE] " +
            "[/title:TITLE_VALUE] " +
            "[/author:AUTHOR_VALUE] " +
            "[/subject:SUBJECT_VALUE] " +
            "[/keywords:KEYWORDS_VALUE] " +
            "[/userpassword:USER_PASSWORD_VALUE] " +
            "[/ownerpassword:OWNER_PASSWORD_VALUE] " +
            "[/subdirs] " +
            "[/noexistingbookmarks]" +
            "[splitbookmarks:SPLIT_BOOKMARKS_LEVEL_VALUE] " +
            "[splitblank:SPLIT_BLANK_PAGES_VALUE] " +
            "[headertext:HEADER_TEXT_VALUE] " +
            "[footertext:FOOTER_TEXT_VALUE] " +
            "[outfilename:OUTPUT_FILENAME_PATTERN] " +
            "[outfolder:OUTPUT_FOLDER_VALUE] " +
            "[/?]\n\n\n" +
            "file : one or more pdf or image files to be processed.\n" +
            "directory : one or more directories containing pdf or images files to be processed.\n" +
            "pagefrom: process pages from\n" +
            "pageto: process pages to\n" +
            "pageevenfrom: process even pages from\n" +
            "pageevento: process even pages to\n" +
            "pageoddfrom: process odd pages from\n" +
            "pageoddto: process odd pages to\n" +
            "pageevery: process every PAGE_EVERY_VALUE pages\n" +
            "pageeveryfrom: process every PAGE_EVERY_VALUE pages starting from this value\n" +
            "pageeveryto: process every PAGE_EVERY_VALUE pages starting till this value\n" +
            "pagecontaining: process only pages containing text\n" +
            "title : Document title\n" +
            "author: Document author\n" +
            "subject: Document subject\n" +
            "keywords: Document keywords\n" +
            "userpassword: Document user password\n" +
            "ownerpassword: Document owner password\n" +
            "subdirs : process also subdirectories of specified directories\n" +
            "splitbookmarks: split document by bookmarks on this level value\n" +
            "noexistingbookmarks : do not add existing bookmarks to output file\n"+
            "splitblank : split document by number of continuous blank pages\n"+
            "headertext: Header text\n"+
            "footertext : Footer Text\n"+
            "outfilename : Output filename pattern. Enter [page] for page number and [file] for filename.\n" +
            "outfolder: Output folder value (if different than the folder of the first file)\n"+
            "/? : show help\n";

            return msg;
        }

        public static string GetFilenameWithParameterValues(string filename)
        {
            filename = filename.Replace("[PAGE_FROM_VALUE]", Module.CmdPageFrom.ToString());
            filename = filename.Replace("[FOOTER_TEXT_VALUE]", Module.CmdFooterText);
            filename = filename.Replace("[HEADER_TEXT_VALUE]", Module.CmdHeaderText);
            filename = filename.Replace("[SPLIT_BLANK_PAGES_VALUE]", Module.CmdSplitBlank.ToString());
            filename = filename.Replace("[SPLIT_BOOKMARKS_LEVEL_VALUE]", Module.CmdSplitBookmarks.ToString());
            filename = filename.Replace("[OWNER_PASSWORD_VALUE]", Module.CmdOwnerPassword);
            filename = filename.Replace("[USER_PASSWORD_VALUE]", Module.CmdUserPassword);
            filename = filename.Replace("[KEYWORDS_VALUE]", Module.CmdKeywords);
            filename = filename.Replace("[SUBJECT_VALUE]", Module.CmdSubject);
            filename = filename.Replace("[AUTHOR_VALUE]", Module.CmdAuthor);
            filename = filename.Replace("[TITLE_VALUE]", Module.CmdTitle);
            filename = filename.Replace("[PAGE_CONTAINING_TEXT_VALUE]", Module.CmdPageContaining);
            filename = filename.Replace("[PAGE_EVERY_TO_VALUE]", Module.CmdPageEveryTo.ToString());
            filename = filename.Replace("[PAGE_EVERY_FROM_VALUE]", Module.CmdPageEveryFrom.ToString());
            filename = filename.Replace("[PAGE_EVERY_VALUE]", Module.CmdPageEvery.ToString());
            filename = filename.Replace("[PAGE_ODD_TO_VALUE]", Module.CmdPageOddTo.ToString());
            filename = filename.Replace("[PAGE_ODD_FROM_VALUE]", Module.CmdPageOddFrom.ToString());
            filename = filename.Replace("[PAGE_EVEN_TO_VALUE]", Module.CmdPageEvenTo.ToString());
            filename = filename.Replace("[PAGE_EVEN_FROM_VALUE]", Module.CmdPageEvenFrom.ToString());
            filename = filename.Replace("[PAGE_TO_VALUE]", Module.CmdPageTo.ToString());

            filename = filename.Replace("[SUBDIRS]", Module.CmdAddSubdirectories.ToString());
            filename = filename.Replace("[PAGE_RANGE_VALUE]", Module.CmdPageRange);

            return filename;
        }
        public static bool IsCommandLine()
        {
            if (Module.args == null || Module.args.Length == 0)
            {
                return false;
            }
            
            if (System.IO.File.Exists(Module.args[0]) || System.IO.Directory.Exists(Module.args[0]))
            {
                return false;
            }

            for (int k = 0; k < Module.args.Length; k++)
            {
                if (Module.args[k] == "-visual")
                {
                    return false;
                }
            }

            Module.IsCommandLine = true;
            return true;
        }

        public static bool IsFromWindowsExplorer()
        {
            if (Module.args == null || Module.args.Length == 0)
            {
                return false;
            }

            if (Module.args.Length==1 && (System.IO.File.Exists(Module.args[0]) || System.IO.Directory.Exists(Module.args[0])))
            {
                return true;
            }

            for (int k = 0; k < Module.args.Length; k++)
            {
                if (Module.args[k] == "-visual")
                {
                    Module.IsFromWindowsExplorer = true;
                    return true;
                }
            }

            Module.IsFromWindowsExplorer = false;
            return false;
        }

        public static void ExecuteCommandLine()
        {
            if (Module.CmdMerge)
            {
                MergeHelper.Merge();
            }
            else if (Module.CmdSplit)
            {
                SplitHelper.Split();
            }
            else if (Module.CmdDelete)
            {
                DeleteHelper.DeletePages();
            }
            else if (Module.CmdExtract)
            {
                ExtractHelper.ExtractPages();
            }
        }
                
    }

    public class ReadListsResult
    {
        public bool Success = true;
        public string err = "";
    }
}
