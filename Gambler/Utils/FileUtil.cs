using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Utils
{
    class FileUtil
    {
        public static List<string> ReadFromPath(string path, IEnumerable<string> exts)
        {
            List<string> result = new List<string>();
            IEnumerable<string> paths = Directory.GetFiles(path, "*.*").Where(f => exts.Contains(Path.GetExtension(f).ToLower()));
            foreach (string p in paths)
            {
                result.Add(p);
            }
            return result;
        }

        //
        // 摘要：
        //     从指定的文件路径中读取文件，如果该路径下文件不存在或者不可读，返回 null。
        // 
        public static string ReadContentFromFilePath(string path)
        {
            if (path == null || path == "")
                return null;

            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }

            return null;
        }

        public static bool WriteContentToFilePath(string path, string content)
        {
            if (path == null || path == "")
                return false;

            File.WriteAllText(path, content);
            return true;
        }
    }
}
