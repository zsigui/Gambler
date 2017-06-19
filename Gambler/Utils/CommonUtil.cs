using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gambler.Utils
{
    public class CommonUtil
    {
        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="msg"></param>
        public static void Copy(string msg)
        {
            Clipboard.SetDataObject(msg);
        }

        /// <summary>
        /// 剪切
        /// </summary>
        /// <returns></returns>
        public static string Paste()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent(DataFormats.Text))
            {
                return (string)data.GetData(DataFormats.Text);
            }
            return "";
        }

        public static void SelectText(TextBoxBase rtb)
        {
            string content = rtb.Text;
            int start = rtb.SelectionStart;
            int end = start;
            while (start > 0)
            {
                if (content[start - 1] != '\n'
                    && content[start - 1] != ' ')
                    start--;
                else break;
            }
            int len = content.Length - 1;
            while (end < len)
            {
                if (content[end + 1] != '\n'
                    && content[end + 1] != ' ')
                    end++;
                else break;
            }
            rtb.Select(start, end - start + 1);
        }
    }
}
