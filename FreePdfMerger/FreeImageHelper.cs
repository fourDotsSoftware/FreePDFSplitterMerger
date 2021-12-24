using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FreeImageAPI;
using System.Drawing.Imaging;

namespace PdfMergeSplitTool
{
    class FreeImageHelper
    {
        private static FIBITMAP dib = new FIBITMAP();

        public static LoadImageReturn LoadImageWithFileType(string filepath)
        {
            if (!dib.IsNull)
                FreeImage.Unload(dib);

            dib = new FIBITMAP();

            // Safely unload to prevent memory leak.
            //FreeImage.UnloadEx(ref dib);

            // Load the example bitmap.
            dib = FreeImage.LoadEx(filepath);

            // Check whether loading succeeded.
            if (dib.IsNull)
            {
                throw new Exception("No Image");
            }

            // Convert the FreeImage-Bitmap into a .NET bitmap

            /*
            if (FreeImage.GetPalette(dib) != IntPtr.Zero)
            {
                byte[] Transparency = new byte[1];
                Transparency[0] = 0x00;
                FreeImage.SetTransparencyTable(dib, Transparency);
                FreeImage.SetTransparent(dib, true);
            }
            */

            Bitmap bitmap = FreeImage.GetBitmap(dib);

            LoadImageReturn lr = new LoadImageReturn();
            lr.img = bitmap;
            lr.FileType = FreeImage.GetFileType(filepath, 0);

            return lr;
        }

        public static Bitmap LoadImage(string filepath)
        {
            if (!dib.IsNull)
                FreeImage.Unload(dib);

            dib = new FIBITMAP();

            // Safely unload to prevent memory leak.
            //FreeImage.UnloadEx(ref dib);

            // Load the example bitmap.
            dib = FreeImage.LoadEx(filepath);
                        
            // Check whether loading succeeded.
            if (dib.IsNull)
            {
                throw new Exception("No Image");
            }

            /*
            if (FreeImage.GetPalette(dib) != IntPtr.Zero)
            {
                byte[] Transparency = new byte[1];
                Transparency[0] = 0x00;
                FreeImage.SetTransparencyTable(dib, Transparency);
                FreeImage.SetTransparent(dib, true);
            }
            */
            // Convert the FreeImage-Bitmap into a .NET bitmap
            Bitmap bitmap = FreeImage.GetBitmap(dib);

            return bitmap;
        }

        
    }

    public class LoadImageReturn
    {
        public Bitmap img = null;
        public FREE_IMAGE_FORMAT FileType = FREE_IMAGE_FORMAT.FIF_JPEG;
    }
}
