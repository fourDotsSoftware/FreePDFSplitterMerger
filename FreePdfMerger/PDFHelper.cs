using System;
using System.Collections.Generic;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace PdfMergeSplitTool
{
    public class PDFHelper
    {
        public static void MakeRemoteNamedDestinationsLocal(string filepath)
        {
            string tmpfile = System.IO.Path.GetTempFileName() + ".pdf";
            
            System.IO.File.Move(filepath, tmpfile);

            PdfReader reader = new PdfReader(tmpfile);
            // Convert the remote destinations into local destinations
            reader.MakeRemoteNamedDestinationsLocal();
            using (FileStream ms2 = new FileStream(filepath, FileMode.Create, FileAccess.Write))
            {
                // Create a new PDF containing the local destinations
                using (PdfStamper stamper = new PdfStamper(reader, ms2))
                {
                }
            }
            
        }
    }
}
