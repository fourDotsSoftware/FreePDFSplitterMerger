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
using Microsoft.Win32;

namespace PdfMergeSplitTool
{
    public partial class frmMain : PdfMergeSplitTool.CustomForm
    {
        public static frmMain _Instance = null;

        public static frmMain Instance
        {
            get {
                if (_Instance == null)
                {
                    _Instance = new frmMain();
                    _Instance.SetupOnLoad();
                }

                return _Instance;
            }

            set
            {
                _Instance = value;
            }
        }

        public DataTable dtDocs = new DataTable("table");
        public DataTable dtSplit = new DataTable("table");
        public string DefaultPassword = "";

        public frmMain()
        {
            InitializeComponent();
            Instance = this;

            if (Module.IsCommandLine)
            {
                this.Visible = false;
                this.ShowInTaskbar = false;
            }
            else
            {
                if (Properties.Settings.Default.ShowPromotion)
                {
                    frmPromotion fp = new frmPromotion();
                    fp.Show(this);
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            //iTextSharp.text.Document doc = new iTextSharp.text.Document();

            PdfReader reader = new PdfReader(@"c:\4\head first ajax.pdf");

            Document doc = new Document(reader.GetPageSizeWithRotation(1));

            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(destinationFile, FileMode.Create));
            ;
            doc.Open();
            PdfContentByte cb = writer.DirectContent;

            /*
            for (int k = 1; k <= reader.NumberOfPages; k++)
            {
                doc.SetPageSize(reader.GetPageSizeWithRotation(i));
                PdfDictionary pdfdict=pdfReader.GetPageN(k);
                PdfImportedPage imp=pdfWriter.GetImportedPage(pdfReader, k);
                pdfWriter.Add(imp);

            }
            */
        }

        private void AddVisual(string[] argsvisual)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                //Module.ShowMessage("Is From Windows Explorer");

                int merge = -1;

                for (int k = 0; k < argsvisual.Length; k++)
                {
                    //Module.ShowMessage(argsvisual[k]);

                    if (argsvisual[k] == "-split")
                    {
                        merge = 0;
                        break;
                    }
                    else if (argsvisual[k] == "-merge")
                    {
                        merge = 1;
                        break;
                    }
                }

                for (int k = 0; k < argsvisual.Length; k++)
                {
                    if (merge==1 || merge==-1)
                    {
                        if (System.IO.File.Exists(argsvisual[k]))
                        {
                            bool isimage = false;

                            try
                            {
                                if (!argsvisual[k].ToLower().EndsWith(".pdf"))
                                {
                                    FreeImageHelper.LoadImage(argsvisual[k]);
                                    isimage = true;
                                }
                            }
                            catch
                            {
                                Module.ShowError("Unrecognized file type !");
                                continue;
                            }

                            AddFile(argsvisual[k], isimage);

                        }
                        else if (System.IO.Directory.Exists(argsvisual[k]))
                        {
                            AddFolder(argsvisual[k]);
                        }
                    }
                    
                    if (merge==0 || merge==-1)
                    {
                        if (System.IO.File.Exists(argsvisual[k]))
                        {
                            bool isimage = false;

                            try
                            {
                                if (!argsvisual[k].ToLower().EndsWith(".pdf"))
                                {
                                    FreeImageHelper.LoadImage(argsvisual[k]);
                                    isimage = true;
                                }
                            }
                            catch
                            {
                                Module.ShowError("Unrecognized file type !");
                                continue;
                            }

                            AddFileSplit(argsvisual[k]);

                        }
                        else if (System.IO.Directory.Exists(argsvisual[k]))
                        {
                            AddFolderSplit(argsvisual[k]);
                        }
                    }

                }
            }
            finally
            {
                this.Cursor = null;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            SetupOnLoad();
            LoadSizeAndPosition();

            if (!Module.IsCommandLine)
            {
                UpdateHelper.InitializeCheckVersionWeek();
            }
        }

        private void LoadSizeAndPosition()
        {
            try
            {
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Free PDF Splitter Merger";
                string filepath = folder + "\\size_and_position.dat";

                if (System.IO.File.Exists(filepath))
                {
                    StreamReader sr = new StreamReader(filepath);

                    this.Width = int.Parse(sr.ReadLine());
                    this.Height = int.Parse(sr.ReadLine());
                    this.Left = int.Parse(sr.ReadLine());
                    this.Top = int.Parse(sr.ReadLine());

                    if (this.Width < 300) this.Width = 300;
                    if (this.Height < 300) this.Height = 300;

                    if (this.Left < 0 || this.Left>Screen.PrimaryScreen.WorkingArea.Right-this.Width) this.Left = 0;
                    if (this.Top < 0 || this.Top > Screen.PrimaryScreen.WorkingArea.Bottom - this.Height) this.Top = 0;
                }
            }
            catch { }
        }

        private void SaveSizeAndPosition()
        {
            try
            {
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Free PDF Splitter Merger";

                if (!System.IO.Directory.Exists(folder))
                {
                    System.IO.Directory.CreateDirectory(folder);
                }

                string filepath = folder + "\\size_and_position.dat";

                StreamWriter sw = new StreamWriter(filepath);
                sw.WriteLine(this.Width.ToString());
                sw.WriteLine(this.Height.ToString());
                sw.WriteLine(this.Left.ToString());
                sw.WriteLine(this.Top.ToString());

                sw.Flush();
                sw.Close();
            }
            catch { }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout f = new frmAbout();
            f.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAddFiles_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = Module.DialogFilesFilter;
            openFileDialog1.Multiselect = true;
            
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;

                    for (int k = 0; k < openFileDialog1.FileNames.Length; k++)
                    {
                        bool isimage = false;

                        try
                        {
                            if (!openFileDialog1.FileNames[k].ToLower().EndsWith(".pdf"))
                            {
                                FreeImageHelper.LoadImage(openFileDialog1.FileNames[k]);
                                isimage = true;
                            }
                        }
                        catch
                        {
                            Module.ShowError("Unrecognized file type !");
                            continue;
                        }

                        AddFile(openFileDialog1.FileNames[k],isimage);
                    }
                }
                finally
                {
                    this.Cursor = null;
                }
            }
        }

        public void AddFile(string filepath,bool isimage)
        {
            DataRow dr = dtDocs.NewRow();
            
            dr["cOrder"]=(dtDocs.Rows.Count + 1).ToString();
            FileInfo fi=new FileInfo(filepath);

            long sizekb=fi.Length/1024;
            dr["cfilename"]=fi.Name;
            dr["cfilepath"]=filepath;
            dr["csize"]=sizekb.ToString() + "KB";
            dr["cmodified"]=fi.LastWriteTime.ToString();

            GetTitleAndNumberOfPagesResult res = null;

            if (!isimage)
            {
                res = GetTitleAndNumberOfPages(filepath,ref dr);

                if (res == null)
                {
                    throw new Exception("Error while examining file");
                }

                dr["ctitle"]=res.Title;
            }
            else
            {
                dr["ctitle"]="";
            }
            
            dr["ctype"]=FileInfoHelper.GetFileTypeDescription(filepath);

            if (!isimage)
            {
                dr["cpages"]=res.NumberOfPages.ToString();
            }
            else
            {
                dr["cpages"]="1";
            }

            //li.SubItems.Add("...");

            PageRange pg= new PageRange();
            dr["ctag"]= pg;

            if (isimage)
            {
                pg.IsImage = true;
                pg.NumberOfPages = 1;
            }

            dtDocs.Rows.Add(dr);

            //SetupColumnsWidth();
            SetupTotalPages();
        }

        public void AddFileSplit(string filepath)
        {
            FileInfo fi = new FileInfo(filepath);

            long sizekb = fi.Length / 1024;

            DataRow dr = dtSplit.NewRow();

            dr["cfilename"]= fi.Name;

            //li.SubItems.Add(fi.Name);
            dr["cfilepath"]=filepath;
            dr["csize"]=sizekb.ToString() + "KB";
            dr["cmodified"]=fi.LastWriteTime.ToString();

            GetTitleAndNumberOfPagesResult res = null;
            res = GetTitleAndNumberOfPages(filepath, ref dr);

            if (res == null)
            {
                throw new Exception("Error while examining file");
            }

            dr["ctitle"]=res.Title;

            dr["ctype"]=FileInfoHelper.GetFileTypeDescription(filepath);
            dr["cpages"]=res.NumberOfPages.ToString();

            dtSplit.Rows.Add(dr);
            //SetupColumnsWidthSplit();
            SetupTotalPagesSplit();

            dgSplit.Invalidate();
        }

        
        public void SetupTotalPages()
        {
            int total = 0;

            for (int k = 0; k < dtDocs.Rows.Count; k++)
            {
                PageRange pg = (PageRange)dtDocs.Rows[k]["cTag"];
                if (pg.IncludedNumberOfPages == -1)
                {
                    int pagenum = int.Parse(dtDocs.Rows[k]["cPages"].ToString());
                    total += pagenum;
                }
                else
                {
                    total += pg.IncludedNumberOfPages;
                }
            }

            txtTotalPages.Text = total.ToString();
        }

        public void SetupTotalPagesSplit()
        {
            int total = 0;

            for (int k = 0; k < dtSplit.Rows.Count; k++)
            {
                int pagenum = int.Parse(dtSplit.Rows[k]["cPages"].ToString());
                total += pagenum;

            }

            txtTotalPagesSplit.Text = total.ToString();
        }
               

        private GetTitleAndNumberOfPagesResult GetTitleAndNumberOfPages(string filepath,ref DataRow dr)
        {
            GetTitleAndNumberOfPagesResult res = new GetTitleAndNumberOfPagesResult();

            if (!filepath.ToString().ToLower().EndsWith(".pdf"))
            {
                res.Title = "";
                res.NumberOfPages = 1;

                return res;
            }


            try
            {

                PdfReader reader=null;

                try
                {
                   reader  = new PdfReader(filepath);
                }
                catch (iTextSharp.text.pdf.BadPasswordException  pex)
                {
                                    
                //if (reader.IsEncrypted())
                //{
            //        reader.Close();

                    bool berror = false;

                    while (true)
                    {
                        try
                        {
                            if (DefaultPassword == "" || berror)
                            {
                                frmPassword f2 = new frmPassword(filepath);
                                DialogResult dres = f2.ShowDialog();

                                if (dres == DialogResult.OK)
                                {
                                    reader = new PdfReader(filepath, Encoding.Default.GetBytes(f2.txtPassword.Text));

                                    if (f2.chkPassword.Checked)
                                    {
                                        DefaultPassword = f2.txtPassword.Text;
                                    }

                                    dr["cpassword"] = f2.txtPassword.Text;

                                    break;
                                }
                                else if (dres == DialogResult.Cancel)
                                {
                                    Module.ShowMessage("Unable to Decrypt Document ! - " + filepath);
                                    return null;
                                }
                            }
                            else
                            {
                                reader = new PdfReader(filepath, Encoding.Default.GetBytes(DefaultPassword));

                                dr["cpassword"] = DefaultPassword;
                                break;
                            }
                        }
                        catch (iTextSharp.text.pdf.BadPasswordException pex2)
                        {
                            Module.ShowMessage("Unable to Decrypt Document ! - " + filepath);
                            berror = true;
                           //DefaultPassword = "";
                        }
                    }
                }

                res.NumberOfPages = reader.NumberOfPages;

                if (reader.Info.ContainsKey("Title"))
                {
                    res.Title=reader.Info["Title"];
                }
                else
                {
                    res.Title = "";
                }
            }
            catch
            {
                return null;
            }

            return res;
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            if (dgDocs.SelectedCells == null) return;
                      

            List<DataRow> seli = new List<DataRow>();
            List<int> selcol = new List<int>();

            int colind = dgDocs.SelectedCells[0].ColumnIndex;
                        

            for (int k = 0; k < dgDocs.SelectedCells.Count; k++)
            {
                if (selcol.IndexOf(dgDocs.SelectedCells[k].RowIndex) < 0)
                {
                    selcol.Add(dgDocs.SelectedCells[k].RowIndex);
                }
            }

            selcol.Sort();

            for (int k = 0; k < selcol.Count; k++)
            {
                seli.Add(dtDocs.Rows[selcol[k]]);
            }

            while (dgDocs.SelectedCells.Count > 0)
            {
                dgDocs.SelectedCells[0].Selected = false;

            }

            dgDocs.CurrentCell = null;

            List<int> tosel = new List<int>();

            for (int k =0;k<selcol.Count;k++)
            {
                if (selcol[k] != 0 && seli.IndexOf(dtDocs.Rows[selcol[k]-1])<0)
                {                                      
                    MoveDataRow(dtDocs, selcol[k], selcol[k] - 1);
                    tosel.Add(selcol[k] - 1);
                    
                    /*
                    dtDocs.Rows.RemoveAt(selcol[k]);
                    dtDocs.Rows.InsertAt(seli[k],selcol[k] - 1);
                    */
                }                
            }

            for (int k = 0; k < tosel.Count; k++)
            {
                dgDocs.Rows[tosel[k]].Cells[colind].Selected = true;
            }

            SetupOrderColumn();

            dgDocs.Select();
            dgDocs.Focus();
        }

        private void MoveDataRow(DataTable dt, int RowIndex, int DestinationRowIndex)
        {
            DataRow dr = dt.Rows[RowIndex];

            DataRow dr2 = dt.NewRow();

            for (int k = 0; k < dt.Columns.Count; k++)
            {
                dr2[k] = dr[k];
            }

            dt.Rows.Remove(dr);

            dt.Rows.InsertAt(dr2, DestinationRowIndex);

            dgDocs.CurrentCell = null;
        }


        private void SetupOrderColumn()
        {
            for (int k = 0; k < dgDocs.Rows.Count; k++)
            {
                int j=k+1;

                dtDocs.Rows[k]["cOrder"] = j.ToString();
            }
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            if (dgDocs.SelectedCells == null) return;

            List<DataRow> seli = new List<DataRow>();
            List<int> selcol = new List<int>();

            int colind = dgDocs.SelectedCells[0].ColumnIndex;

            for (int k = 0; k < dgDocs.SelectedCells.Count; k++)
            {
                if (selcol.IndexOf(dgDocs.SelectedCells[k].RowIndex) < 0)
                {
                    selcol.Add(dgDocs.SelectedCells[k].RowIndex);
                }
            }

            selcol.Sort();

            for (int k = 0; k < selcol.Count; k++)
            {
                seli.Add(dtDocs.Rows[selcol[k]]);
            }

            List<int> tosel = new List<int>();

            for (int k =selcol.Count-1; k >=0; k--)
            {
                if (selcol[k] != dgDocs.Rows.Count-1 && (seli.IndexOf(dtDocs.Rows[selcol[k] + 1]) < 0))
                {
                    MoveDataRow(dtDocs, selcol[k], selcol[k] + 1);

                    tosel.Add(selcol[k] + 1);
                    //dtDocs.Rows.RemoveAt(selcol[k]);
                    //dtDocs.Rows.InsertAt(seli[k],selcol[k] + 1);
                    
                }
            }
            
            for (int k = 0; k < tosel.Count; k++)
            {
                dgDocs.Rows[tosel[k]].Cells[colind].Selected = true;
            }

            SetupOrderColumn();

            dgDocs.Select();
            dgDocs.Focus();

        }

        private void btnMoveFirst_Click(object sender, EventArgs e)
        {
            if (dgDocs.SelectedCells == null) return;

            List<DataRow> seli = new List<DataRow>();
            List<int> selcol = new List<int>();

            int colind = dgDocs.SelectedCells[0].ColumnIndex;

            for (int k = 0; k < dgDocs.SelectedCells.Count; k++)
            {
                if (selcol.IndexOf(dgDocs.SelectedCells[k].RowIndex) < 0)
                {
                    selcol.Add(dgDocs.SelectedCells[k].RowIndex);
                }
            }

            selcol.Sort();

            for (int k = 0; k < selcol.Count; k++)
            {
                seli.Add(dtDocs.Rows[selcol[k]]);
            }

            for (int k = 0; k < selcol.Count; k++)
            {
                if (selcol[k] != 0)
                {
                    MoveDataRow(dtDocs, selcol[k], k);
                    //dtDocs.Rows.RemoveAt(selcol[k]);
                    //dtDocs.Rows.InsertAt(seli[k],k);

                }
            }


            for (int k = 0; k < seli.Count; k++)
            {
                dgDocs.Rows[k].Cells[colind].Selected = true;
            }

            SetupOrderColumn();

            dgDocs.Select();
            dgDocs.Focus();
        }

        private void btnMoveLast_Click(object sender, EventArgs e)
        {
            if (dgDocs.SelectedCells == null) return;

            List<DataRow> seli = new List<DataRow>();
            List<int> selcol = new List<int>();

            int colind = dgDocs.SelectedCells[0].ColumnIndex;

            for (int k = 0; k < dgDocs.SelectedCells.Count; k++)
            {
                if (selcol.IndexOf(dgDocs.SelectedCells[k].RowIndex) < 0)
                {
                    selcol.Add(dgDocs.SelectedCells[k].RowIndex);
                }
            }

            selcol.Sort();

            for (int k = 0; k < selcol.Count; k++)
            {
                seli.Add(dtDocs.Rows[selcol[k]]);
            }

            int j = 0;

            for (int k = selcol.Count - 1; k >= 0; k--)
            {
                if (selcol[k] != dtDocs.Rows.Count - 1)
                {
                    MoveDataRow(dtDocs, selcol[k], dtDocs.Rows.Count-1 - j);
                    //dtDocs.Rows.RemoveAt(selcol[k]);
                    //dtDocs.Rows.InsertAt(seli[k],dtDocs.Rows.Count-j);

                }

                j++;
            }


            for (int k = 0; k < seli.Count; k++)
            {
                dgDocs.Rows[dtDocs.Rows.Count-1-k].Cells[colind].Selected = true;
            }


            SetupOrderColumn();

            dgDocs.Select();
            dgDocs.Focus();

        }

        /*
        private void SetupColumnsWidthSplit()
        {
            if (lvDocsSplit.Items.Count > 0)
            {
                for (int l = 0; l < lvDocsSplit.Columns.Count; l++)
                {

                    if (lvDocsSplit.Columns[l].Text.Length > lvDocsSplit.Items[lvDocsSplit.Items.Count - 1].SubItems[l].Text.Length)
                    {
                        lvDocsSplit.Columns[l].Width = -2;
                    }
                    else
                    {
                        lvDocsSplit.Columns[l].Width = -1;
                    }
                }
            }
        }
          */             

        private void btnAddFolder_Click(object sender, EventArgs e)
        {            
            folderBrowserDialog1.SelectedPath = "";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                AddFolder(folderBrowserDialog1.SelectedPath);   
            }
        }

        public void AddFolder(string folder_path)
        {
            string[] filez = null;

            if (Module.IsCommandLine)
            {
                if (Module.CmdAddSubdirectories)
                {
                    filez = System.IO.Directory.GetFiles(folder_path, "*.*", SearchOption.AllDirectories);
                }
                else
                {
                    filez = System.IO.Directory.GetFiles(folder_path, "*.*", SearchOption.TopDirectoryOnly);
                }
            }
            else
            {

                if (System.IO.Directory.GetDirectories(folder_path).Length > 0)
                {
                    DialogResult dres = Module.ShowQuestionDialog("Would you like to add also Subdirectories ?", "Add Subdirectories ?");

                    if (dres == DialogResult.Yes)
                    {
                        filez = System.IO.Directory.GetFiles(folder_path, "*.*", SearchOption.AllDirectories);
                    }
                    else
                    {
                        filez = System.IO.Directory.GetFiles(folder_path, "*.*", SearchOption.TopDirectoryOnly);
                    }
                }
                else
                {
                    filez = System.IO.Directory.GetFiles(folder_path, "*.*", SearchOption.TopDirectoryOnly);
                }
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;

                for (int k = 0; k < filez.Length; k++)
                {
                    if (filez[k].ToLower().EndsWith(".pdf"))
                    {
                        AddFile(filez[k], false);
                    }
                    else
                    {
                        try
                        {
                            FreeImageHelper.LoadImage(filez[k]);
                            AddFile(filez[k], true);
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
            }
            finally
            {
                this.Cursor = null;
            }

        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            if (dgDocs.Rows.Count == 0) return;

           
            if (dgDocs.SelectedCells.Count == 0)
            {
                PageRange pg = (PageRange)dtDocs.Rows[0]["cTag"];
                if (!pg.IsImage)
                {
                    frmDocOptions f = new frmDocOptions((PageRange)dtDocs.Rows[0]["cTag"], dtDocs.Rows[0]["cFilepath"].ToString(), int.Parse(dtDocs.Rows[0]["cpages"].ToString()), dtDocs.Rows[0]["cpassword"].ToString());
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        dtDocs.Rows[0]["cmergerange"] = pg.PageRanges;
                    }

                    dgDocs.Invalidate();
                }
                else
                {
                    frmImageOptions f = new frmImageOptions((PageRange)dtDocs.Rows[0]["cTag"],dtDocs.Rows[0]["cFilepath"].ToString());
                    f.ShowDialog();
                }
            }
            else
            {
                PageRange pg = (PageRange)dtDocs.Rows[dgDocs.SelectedCells[0].RowIndex]["cTag"];

                if (!pg.IsImage)
                {
                    frmDocOptions f = new frmDocOptions((PageRange)dtDocs.Rows[dgDocs.SelectedCells[0].RowIndex]["cTag"], dtDocs.Rows[dgDocs.SelectedCells[0].RowIndex]["cFilepath"].ToString(), int.Parse(dtDocs.Rows[dgDocs.SelectedCells[0].RowIndex]["cpages"].ToString()), dtDocs.Rows[dgDocs.SelectedCells[0].RowIndex]["cpassword"].ToString());
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        dtDocs.Rows[dgDocs.SelectedCells[0].RowIndex]["cmergerange"] = pg.PageRanges;
                    }

                    dgDocs.Invalidate();
                }
                else
                {
                    frmImageOptions f = new frmImageOptions((PageRange)dtDocs.Rows[dgDocs.SelectedCells[0].RowIndex]["cTag"], dtDocs.Rows[dgDocs.SelectedCells[0].RowIndex]["cFilepath"].ToString());
                    f.ShowDialog();
                }
            }

        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            frmOptions f = new frmOptions();
            f.ShowDialog();
        }

        private void btnRemoveFile_Click(object sender, EventArgs e)
        {
            List<DataRow> lstrows = new List<DataRow>();

            for (int k = 0; k < dgDocs.SelectedCells.Count; k++)
            {
                if (lstrows.IndexOf(dtDocs.Rows[dgDocs.SelectedCells[k].RowIndex]) < 0)
                {
                    lstrows.Add(dtDocs.Rows[dgDocs.SelectedCells[k].RowIndex]);
                }
            }

            for (int k = 0; k < lstrows.Count; k++)
            {
                dtDocs.Rows.Remove(lstrows[k]);

            }
            /*
            while (dgDocs.SelectedCells.Count > 0)
            {
                dtDocs.Rows.RemoveAt(dgDocs.SelectedCells[0].RowIndex);
            }
            */
            SetupTotalPages();
            SetupOrderColumn();
            //SetupColumnsWidth();
            dgDocs.Invalidate();
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            if (dtDocs.Rows.Count == 0)
            {
                Module.ShowMessage("Please add some Documents or Images first !");
                return;
            }

            /*
            if (lvDocs.Items.Count > 3 && frmAbout.LDT==String.Empty)
            {
                frmFullActivate fa = new frmFullActivate();
                fa.ShowDialog();
                return;

            }
            */

            frmOptions f = new frmOptions();
            f.ShowDialog();
        }

        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            for (int k = 0; k < dgDocs.SelectedCells.Count; k++)
            {
                DataRow lvi = dtDocs.NewRow();

                for (int m = 1; m < dtDocs.Columns.Count; m++)
                {
                    if (dtDocs.Columns[m].ColumnName.ToLower() == "ctag")
                    {
                        PageRange pg = dtDocs.Rows[dgDocs.SelectedCells[k].RowIndex][m] as PageRange;
                        lvi[m] = pg.Clone();

                    }
                    else
                    {
                        lvi[m] = dtDocs.Rows[dgDocs.SelectedCells[k].RowIndex][m];
                    }
                }

                dtDocs.Rows.Add(lvi);
            }

            SetupTotalPages();
            SetupOrderColumn();
            //SetupColumnsWidth();
        }

        private void tiHelpFeedback_Click(object sender, EventArgs e)
        {
            /*
            frmUninstallQuestionnaire f = new frmUninstallQuestionnaire(false);
            f.ShowDialog();
            */

            System.Diagnostics.Process.Start("https://www.4dots-software.com/support/bugfeature.php?app=" + System.Web.HttpUtility.UrlEncode(Module.ShortApplicationTitle));
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgDocs.SelectedCells.Count == 0) return;

            System.Diagnostics.Process.Start(dgDocs.Rows[dgDocs.SelectedCells[0].RowIndex].Cells["colFilepath"].Value.ToString());

        }

        private void documentSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnSettings_Click(null, null);
        }

        private void duplicateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnDuplicate_Click(null, null);
        }

        private bool IsDragging = false;
                
        private void lvDocs_DragOver(object sender, DragEventArgs e)
        {
            // Set a constant to define the autoscroll region
            const Single scrollRegion = 20;

            // See where the cursor is
            Point pt = dgDocs.PointToClient(Cursor.Position);

            DataGridView.HitTestInfo hiti = dgDocs.HitTest(pt.X, pt.Y);

            try
            {
                // See if we need to scroll up or down
                if ((pt.Y + scrollRegion) > dgDocs.Height)
                {
                    // Call the API to scroll down
                    Module.SendMessage(dgDocs.Handle, (int)277, (int)1, 0);
                }
                else if (pt.Y < (dgDocs.Top + scrollRegion))
                {
                    // Call thje API to scroll up
                    Module.SendMessage(dgDocs.Handle, (int)277, (int)0, 0);
                }
            }
            catch { }

            if (hiti.Type!=DataGridViewHitTestType.None)
            {
                //dgDocs.SelectedCells.Clear();
                //dgDocs.Rows[hiti.RowIndex].Cells[hiti.ColumnIndex]

                dgDocs.CurrentCell = dgDocs.Rows[hiti.RowIndex].Cells[hiti.ColumnIndex];

                IsDragging = true;
                if ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
                {
                    e.Effect = DragDropEffects.Copy;
                }
            }
            else
            {
                IsDragging = true;
                if ((e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move)
                {
                    e.Effect = DragDropEffects.Move;
                }
            }
        }

        private void dgDocs_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop,false))
            {
                IsDragging = true;
                e.Effect = DragDropEffects.All;

            }
            else
            {
                e.Effect = DragDropEffects.None;
            }

        }

        private void dgDocs_DragDrop(object sender, DragEventArgs e)
        {          
            Point pt = dgDocs.PointToClient(Cursor.Position);
            DataGridView.HitTestInfo hiti = dgDocs.HitTest(pt.X, pt.Y);

            //if (hiti.Item != null)
            //{

            int ind = -1;
            if (hiti.RowIndex!=-1)
            {
                ind = hiti.RowIndex;
            }

            List<DataRow> lst = new List<DataRow>();

            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                string[] filez = (string[])e.Data.GetData(DataFormats.FileDrop);

                for (int k = 0; k < filez.Length; k++)
                {
                    bool isimage = false;

                    try
                    {
                        if (!filez[k].ToLower().EndsWith(".pdf"))
                        {
                            FreeImageHelper.LoadImage(filez[k]);
                            isimage = true;
                        }
                    }
                    catch
                    {
                        Module.ShowError("Unrecognized file type !"+" - "+filez[k]);
                        continue;
                    }

                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        AddFile(filez[k], isimage);
                    }
                    finally
                    {
                        this.Cursor = null;
                    }

                    lst.Add(dtDocs.Rows[dtDocs.Rows.Count - 1]);
                }

                /*
                if (hiti.RowIndex!=-1 && (dtDocs.Rows.Count != lst.Count))
                {
                    for (int k = 0; k < lst.Count; k++)
                    {
                        dtDocs.Rows.Remove(lst[k]);
                    }

                    for (int k = 0; k < lst.Count; k++)
                    {
                        dtDocs.Rows.InsertAt(lst[k],ind + k);
                    }
                }*/
            }


            //}

        }

        #region - Drag and Drop lvDocsSplit

        private void lvDocsSplit_DragOver(object sender, DragEventArgs e)
        {
            // Set a constant to define the autoscroll region
            const Single scrollRegion = 20;

            // See where the cursor is
            Point pt = dgSplit.PointToClient(Cursor.Position);

            DataGridView.HitTestInfo hiti = dgSplit.HitTest(pt.X, pt.Y);
            try
            {
                // See if we need to scroll up or down
                if ((pt.Y + scrollRegion) > dgSplit.Height)
                {
                    // Call the API to scroll down
                    Module.SendMessage(dgSplit.Handle, (int)277, (int)1, 0);
                }
                else if (pt.Y < (dgSplit.Top + scrollRegion))
                {
                    // Call thje API to scroll up
                    Module.SendMessage(dgSplit.Handle, (int)277, (int)0, 0);
                }
            }
            catch { }


            if (hiti.Type==DataGridViewHitTestType.Cell)
            {
                while (dgSplit.SelectedCells.Count > 0)
                {
                    dgSplit.SelectedCells[0].Selected = false;
                }

                dgSplit.CurrentCell = dgSplit.Rows[hiti.RowIndex].Cells[hiti.ColumnIndex];
                //dgSplit.Rows[hiti.RowIndex].Cells[hiti.ColumnIndex].Selected = true;
                

                IsDragging = true;
                if ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
                {
                    e.Effect = DragDropEffects.Copy;
                }
            }
            else
            {
                IsDragging = true;
                if ((e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move)
                {
                    e.Effect = DragDropEffects.Move;
                }
            }
        }

        private void lvDocsSplit_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                IsDragging = true;
                e.Effect = DragDropEffects.All;

            }
            else
            {
                e.Effect = DragDropEffects.None;
            }

        }

        private void lvDocsSplit_DragDrop(object sender, DragEventArgs e)
        {

            Point pt = dgSplit.PointToClient(Cursor.Position);
            DataGridView.HitTestInfo hiti = dgSplit.HitTest(pt.X, pt.Y);

            //if (hiti.Item != null)
            //{

            int ind = -1;
            if (hiti.Type==DataGridViewHitTestType.Cell)
            {
                ind = hiti.RowIndex;
            }

           // List<ListViewItem> lst = new List<ListViewItem>();

            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                string[] filez = (string[])e.Data.GetData(DataFormats.FileDrop);

                for (int k = 0; k < filez.Length; k++)
                {
                    if (!filez[k].ToLower().EndsWith(".pdf"))
                    {
                        Module.ShowError("Unrecognized file type !" + " - " + filez[k]);
                        continue;
                    }

                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        AddFileSplit(filez[k]);
                    }
                    finally
                    {
                        this.Cursor = null;
                    }

             //       lst.Add(lvDocsSplit.Items[lvDocsSplit.Items.Count - 1]);
                }

                /*
                if (hiti.Item != null && (lvDocsSplit.Items.Count != lst.Count))
                {
                    for (int k = 0; k < lst.Count; k++)
                    {
                        lvDocsSplit.Items.Remove(lst[k]);
                    }

                    for (int k = 0; k < lst.Count; k++)
                    {
                        lvDocsSplit.Items.Insert(ind + k, lst[k]);
                    }
                }*/
            }


            //}

        }

#endregion

        // end drag and drop

        private void btnAddFileSplit_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = Module.DialogFilesFilter;
            openFileDialog1.Multiselect = true;

            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;

                    for (int k = 0; k < openFileDialog1.FileNames.Length; k++)
                    {                                                
                        if (!openFileDialog1.FileNames[k].ToLower().EndsWith(".pdf"))
                        {
                            Module.ShowError("Unrecognized file type !");
                            continue;
                        }
                        
                        AddFileSplit(openFileDialog1.FileNames[k]);
                    }
                }
                finally
                {
                    this.Cursor = null;
                }
            }

            SetupTotalPagesSplit();
        }

        private void btnRemoveFileSplit_Click(object sender, EventArgs e)
        {
            List<DataRow> lstrows = new List<DataRow>();

            for (int k = 0; k < dgSplit.SelectedCells.Count; k++)
            {
                if (lstrows.IndexOf(dtSplit.Rows[dgSplit.SelectedCells[k].RowIndex]) < 0)
                {
                    lstrows.Add(dtSplit.Rows[dgSplit.SelectedCells[k].RowIndex]);
                }
            }

            for (int k = 0; k < lstrows.Count; k++)
            {
                dtSplit.Rows.Remove(lstrows[k]);

            }
            /*
            while (dgSplit.SelectedCells.Count > 0)
            {
                dtSplit.Rows.RemoveAt(dgSplit.SelectedCells[0].RowIndex);
            }                       
            */
            //SetupColumnsWidthSplit();
            SetupTotalPagesSplit();
            dgSplit.Invalidate();
        }

        public void AddFolderSplit(string folder_path)
        {
            string[] filez = null;

            if (Module.IsCommandLine)
            {
                if (Module.CmdAddSubdirectories)
                {
                    filez = System.IO.Directory.GetFiles(folder_path, "*.*", SearchOption.AllDirectories);
                }
                else
                {
                    filez = System.IO.Directory.GetFiles(folder_path, "*.*", SearchOption.TopDirectoryOnly);
                }
            }
            else
            {
                if (System.IO.Directory.GetDirectories(folder_path).Length > 0)
                {
                    DialogResult dres = Module.ShowQuestionDialog("Would you like to add also Subdirectories ?", "Add Subdirectories ?");

                    if (dres == DialogResult.Yes)
                    {
                        filez = System.IO.Directory.GetFiles(folder_path, "*.*", SearchOption.AllDirectories);
                    }
                    else
                    {
                        filez = System.IO.Directory.GetFiles(folder_path, "*.*", SearchOption.TopDirectoryOnly);
                    }
                }
                else
                {
                    filez = System.IO.Directory.GetFiles(folder_path, "*.*", SearchOption.TopDirectoryOnly);
                }
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;

                for (int k = 0; k < filez.Length; k++)
                {
                    if (filez[k].ToLower().EndsWith(".pdf"))
                    {
                        AddFileSplit(filez[k]);
                    }
                }
            }
            finally
            {
                this.Cursor = null;
            }
        }

        private void btnAddFolderSplit_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = "";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                AddFolderSplit(folderBrowserDialog1.SelectedPath);                
            }
        }

        private void btnOptionsSplit_Click(object sender, EventArgs e)
        {

        }

        private void btnSplit_Click(object sender, EventArgs e)
        {
            if (dgSplit.Rows.Count == 0)
            {
                Module.ShowMessage("Please add some Documents first !");
                return;
            }

            frmOptionsSplit f = new frmOptionsSplit();
            f.ShowDialog();
        }
           
        

        

        private void btnDeletePages_Click(object sender, EventArgs e)
        {
            if (dgSplit.Rows.Count == 0)
            {
                Module.ShowMessage("Please add some Documents first !");
                return;
            }

            frmOptionsDelete f = new frmOptionsDelete();
            f.ShowDialog();
            
        }

        private void btnExtractPages_Click(object sender, EventArgs e)
        {
            if (dgSplit.Rows.Count == 0)
            {
                Module.ShowMessage("Please add some Documents first !");
                return;
            }

            frmOptionsExtract f = new frmOptionsExtract();
            f.ShowDialog();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dtSplit.Rows.Clear();
            dgSplit.Invalidate();
        }        
        private void buyNowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Module.StoreUrl);
        }

        private void registerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        protected override void WndProc(ref Message m)
        {
            /*
            if (m.Msg == Program.WM_MULTIPLE_SEARCH_REPLACE)
            {
                MessageBox.Show(m.GetLParam(typeof(string)).ToString());
                lstIncludePaths.Items.Add(m.GetLParam(typeof(string)));
            }
            base.WndProc(ref m);
            */

            if (m.Msg == MessageHelper.WM_COPYDATA)
            {
                MessageHelper.COPYDATASTRUCT mystr = new MessageHelper.COPYDATASTRUCT();
                Type mytype = mystr.GetType();
                mystr = (MessageHelper.COPYDATASTRUCT)m.GetLParam(mytype);
                //MessageBox.Show(mystr.lpData);

                string arg = mystr.lpData;

                //MessageBox.Show("RECEIVED MESSAGE :" + arg);
                string[] args = arg.Split(new string[] { "|||" }, StringSplitOptions.RemoveEmptyEntries);

                //frmClientImages.Instance.ShowSendToMenuForm(args);
                for (int n = 1; n <= args.GetUpperBound(0); n++)
                {
                    //MessageBox.Show(args[n]);
                }

                AddVisual(args);


            }
            else if (m.Msg == MessageHelper.WM_ACTIVATEAPP)
            {
                this.WindowState = FormWindowState.Normal;
                this.Show();
            }



            base.WndProc(ref m);
        }

        private void helpGuideToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            //if (Module.SelectedLanguage == "en-US")
            //{
            System.Diagnostics.Process.Start(Application.StartupPath + "\\Free PDF Splitter Merger - Users Manual.chm");
            //}
            //else
            //{
            //    System.Diagnostics.Process.Start(Application.StartupPath + "\\" + Module.SelectedLanguage + "\\PdfMergeSplitTool - Help Manual.chm");
            //}            
        }

        private void btnClearMerge_Click(object sender, EventArgs e)
        {
            dtDocs.Rows.Clear();

            SetupTotalPages();
            SetupOrderColumn();
            //SetupColumnsWidth();
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           
            if (dgSplit.SelectedCells.Count == 0) return;

            System.Diagnostics.Process.Start(dgSplit.Rows[dgSplit.SelectedCells[0].RowIndex].Cells[colFilepath2.Index].Value.ToString());
        }

        private void SetupOnLoad()
        {
            this.Text = Module.ApplicationTitle;
            //this.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            //this.Left = 0;

            if (dtDocs.Columns.Count == 0)
            {
                dtDocs.Columns.Add("cOrder");
                dtDocs.Columns.Add("cFilename");
                dtDocs.Columns.Add("cFilepath");
                dtDocs.Columns.Add("cSize");
                dtDocs.Columns.Add("cType");
                dtDocs.Columns.Add("cModified");
                dtDocs.Columns.Add("cTitle");
                dtDocs.Columns.Add("cPages");
                dtDocs.Columns.Add("cMergeRange");
                dtDocs.Columns.Add("cMergeOptions");
                dtDocs.Columns.Add("cTag", typeof(PageRange));
                dtDocs.Columns.Add("cPassword");

            }

            dgDocs.AutoGenerateColumns = false;

            dgDocs.DataSource = dtDocs;

            dgDocs.CellContentClick += dgDocs_CellContentClick;

            if (dtSplit.Columns.Count == 0)
            {                
                dtSplit.Columns.Add("cFilename");
                dtSplit.Columns.Add("cFilepath");
                dtSplit.Columns.Add("cSize");
                dtSplit.Columns.Add("cType");
                dtSplit.Columns.Add("cModified");
                dtSplit.Columns.Add("cTitle");
                dtSplit.Columns.Add("cPages");
                dtSplit.Columns.Add("cPassword");

            }

            dgSplit.AutoGenerateColumns = false;

            dgSplit.DataSource = dtSplit;


            if (ArgsHelper.IsFromWindowsExplorer())
            {
                AddVisual(Module.args);
                Module.args = null;
            }

            AddLanguageMenuItems();

            tiOptionsAddBookmarks.Checked = Properties.Settings.Default.OptAddExistingBookmarks;

            if (downloadToolStripMenuItem.DropDownItems.Count == 0)
            {
                DownloadSuggestionsHelper ds = new DownloadSuggestionsHelper();
                ds.SetupDownloadMenuItems(downloadToolStripMenuItem);
            }
        }

        void dgDocs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex!=-1 && e.ColumnIndex == dgDocs.Columns["colMergeOptions"].Index)
            {
                btnSettings_Click(null, null);
            }
        }


        #region Localization


        private void AddLanguageMenuItems()
        {
            for (int k = 0; k < frmLanguage.LangCodes.Count; k++)
            {
                ToolStripMenuItem ti = new ToolStripMenuItem();
                ti.Text = frmLanguage.LangDesc[k];
                ti.Tag = frmLanguage.LangCodes[k];
                ti.Image = frmLanguage.LangImg[k];
                ti.Click += new EventHandler(tiLang_Click);
                languageToolStripMenuItem.DropDownItems.Add(ti);
            }
        }

        void tiLang_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem ti = (ToolStripMenuItem)sender;
            string langcode = ti.Tag.ToString();
            ChangeLanguage(langcode);

            for (int k = 0; k < languageToolStripMenuItem.DropDownItems.Count; k++)
            {
                ToolStripMenuItem til = (ToolStripMenuItem)languageToolStripMenuItem.DropDownItems[k];
                if (til == ti)
                {
                    til.Checked = true;
                }
                else
                {
                    til.Checked = false;
                }
            }
        }

        private void ChangeLanguage(string language_code)
        {
            RegistryKey key = Registry.CurrentUser;
            try
            {
                key = key.OpenSubKey("SOFTWARE\\4dots Software\\PdfMergeSplitTool", true);

                key.SetValue("Language", language_code);
                Program.SetLanguage();
                //key.SetValue("Menu Item Caption", TranslateHelper.Translate("Remove PDF Password"));
            }
            catch (Exception ex)
            {
                Module.ShowError(ex);
                return;
            }
            finally
            {
                key.Close();
            }



            this.Controls.Clear();            

            InitializeComponent();
            
            SetupOnLoad();

        }

        #endregion

        private void dgDocs_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            
            dgDocs.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
        }

        private void dgDocs_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dgDocs.Columns[e.ColumnIndex] as DataGridViewButtonColumn == null)
            {
                dgDocs.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
            }
        }

        private void dgDocs_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == colMergeRange.Index)
            {
                if (!ValidatePageRanges(e.FormattedValue.ToString(),int.Parse(dgDocs.Rows[e.RowIndex].Cells[colPages.Index].Value.ToString())))
                {
                    Module.ShowMessage("Invalid Page Ranges !");
                    e.Cancel = true;
                    return;
                }
                else if (e.FormattedValue.ToString().Trim() != String.Empty)
                {
                    PageRange pg = (PageRange)dtDocs.Rows[e.RowIndex]["ctag"];
                    pg.AllPages = false;
                    pg.PageRanges = e.FormattedValue.ToString();
                    pg.Initialized = true;
                }
                
            }
        }

        private bool ValidatePageRanges(string txtpageranges,int ipages)
        {
            if (txtpageranges.Trim() == String.Empty) return true;

            try
            {
                string[] ranges = txtpageranges.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                for (int k = 0; k < ranges.Length; k++)
                {
                    string[] range = ranges[k].Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);

                    int ifrom = int.Parse(range[0]);
                    int ito = int.Parse(range[1]);


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

        private void downloadFreePDFPasswordRemoverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DownloadFile("FreePDFPasswordRemover");
        }

        private void DownloadFile(string file)
        {
            System.Diagnostics.Process.Start("http://www.4dots-software.com/downloads/download.php?file=" + file);
        }

        private void downloadFreePDFImagesExtractorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DownloadFile("FreePDFImageExtractor");
        }

        private void downloadFreePDFProtectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DownloadFile("PDFEncrypter");
        }

        private void downloadFreePDFToTextConverterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DownloadFile("FreePDFToTextConverter");
        }

        private void downloadFreeFileUnlockerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DownloadFile("FreeFileUnlocker");
        }

        private void downloadEmptyFolderCleanerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DownloadFile("EmptyFolderCleaner");
        }

        private void downloadDocusTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DownloadFile("FreeDocusTree");
        }

        private void downloadFreePDFMetadataEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DownloadFile("FreePDFMetadataEditor");
        }

        private void downloadMD5HashCheckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DownloadFile("MD5HashCheck");
        }

        private void downloadSplitByteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DownloadFile("SplitByte");
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveSizeAndPosition();

            Properties.Settings.Default.Save();
        }

        private void tiOptionsAddBookmarks_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.OptAddExistingBookmarks = tiOptionsAddBookmarks.Checked;
        }

        private void visit4dotsSoftwareWebsiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.4dots-software.com");
        }

        private void pleaseDonateToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.4dots-software.com/donate.php");
        }

        private void dotsSoftwarePRODUCTCATALOGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.4dots-software.com/downloads/4dots-Software-PRODUCT-CATALOG.pdf");
        }

        private void checkForNewVersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateHelper.CheckVersion(false);
        }

        private void tryOnlineVersionAtOnlinepdfappscomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://onlinepdfapps.com");
        }

        private void commandLineArgumentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMessage fm = new frmMessage(true);
            fm.ShowDialog(this);
        }
    }
}

