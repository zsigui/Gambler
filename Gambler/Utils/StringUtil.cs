using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Utils
{
    public class StringUtil
    {
        /// <summary>
        /// 将繁体中文转为简体中文(会出现个别繁体字没有转换成功的情况)
        /// </summary>
        /// <param name="traditional">繁体中文字符串</param>
        /// <returns>简体中文字符串，如果转换失败，返回输入字符串</returns>
        public static string TraditionalToSimple(string traditional)
        {
            string result = Strings.StrConv(traditional, VbStrConv.SimplifiedChinese);
            return String.IsNullOrEmpty(result) ? traditional : result;
        }

        /// <summary>
        /// 将简体中文转为繁体中文(会出现个别简体字没有转换成功的情况)
        /// </summary>
        /// <param name="simple">繁体中文字符串</param>
        /// <returns>简体中文字符串，如果转换失败，返回输入字符串</returns>
        public static string SimpleToTraditional(string simple)
        {
            string result = Strings.StrConv(simple, VbStrConv.TraditionalChinese);
            return String.IsNullOrEmpty(result) ? simple : result;
        }
    }
}
