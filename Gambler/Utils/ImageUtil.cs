using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Utils
{
    class ImageUtil
    {
        public static Bitmap BytesToBitmap(byte[] bsImg)
        {
            Bitmap bmp = null;
            using (var ms = new MemoryStream(bsImg))
            {
                bmp = new Bitmap(ms);
            }    
            return bmp;
        }

        public static byte[] BitmapToBytes(Bitmap bmp)
        {
            byte[] bsImg = null;
            using (var ms = new MemoryStream())
            {
                bmp.Save(ms, bmp.RawFormat);
                bsImg = ms.ToArray();
            } 
            return bsImg;
        }

        public static byte[] ImageToBytes(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        public static Image BytesToImage(byte[] bsImg)
        {
            Image img = null;
            using (var ms = new MemoryStream(bsImg))
            {
                img = Image.FromStream(ms);
            }
            return img;
        }

        public static Bitmap Read(string filepath)
        {
            Bitmap bmp = null;
            using (var fs = File.OpenRead(filepath))
            {
                byte[] bs = new byte[fs.Length];
                fs.Read(bs, 0, bs.Length);
                bmp = new Bitmap(fs);
            }
            return bmp;
        }

        public static void Write(Bitmap bitmap, string filepath)
        {

            Bitmap newBmp = new Bitmap(bitmap.Width, bitmap.Height);
            Graphics g = Graphics.FromImage(newBmp);
            g.DrawImage(bitmap, 0, 0);
            newBmp.Save(filepath);
        }
    }
}
