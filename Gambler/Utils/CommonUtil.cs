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
            char tmp;
            char[] splits = new char[] { '\n', ' ', '(', ')', '（', '）', '[', ']', '【', '】' };
            while (start > 0)
            {
                tmp = content[start - 1];
                if (!splits.Contains(tmp))
                    start--;
                else break;
            }
            int len = content.Length - 1;
            while (end < len)
            {
                tmp = content[end + 1];
                if (!splits.Contains(tmp))
                    end++;
                else break;
            }
            rtb.Select(start, end - start + 1);
        }
    }
}
