using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Utils
{
    public class Md5Util
    {
        public static byte[] Encrypt(byte[] content)
        {
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(content);
            return hash;
        }

        public static string EncryptToHex(string data, Encoding charset)
        {
            byte[] hash = Encrypt(charset.GetBytes(data));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                builder.Append(hash[i].ToString("X2"));
            }
            return builder.ToString();
        }

        public static string EncryptToHex(string data)
        {
            return EncryptToHex(data, Encoding.UTF8);
        }
    }
}
