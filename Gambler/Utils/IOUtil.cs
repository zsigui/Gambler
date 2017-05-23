using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Utils
{
    public class IOUtil
    {

        public static byte[] Read(Stream stream)
        {
            byte[] result = null;

            using (BufferedStream sr = new BufferedStream(stream))
            {
                byte[] bs = new byte[2048];
                int len;
                MemoryStream ms = new MemoryStream();
                while ((len = sr.Read(bs, 0, bs.Length)) != 0)
                {
                    ms.Write(bs, 0, len);
                }
                result = ms.ToArray();
                ms.Close();
            }
            return result;
        }

        public static string ReadString(Stream stream, Encoding encoding)
        {
            return encoding.GetString(Read(stream));
        }

        public static string ReadString(Stream stream)
        {
            return ReadString(stream, Encoding.UTF8);
        }
    }
}
