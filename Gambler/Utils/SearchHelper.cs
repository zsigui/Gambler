using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Utils
{
    public class SearchHelper
    {
        private static int[] GetKmpNext(string pattern)
        {
            int[] next = new int[pattern.Length];
            next[0] = -1;
            if (pattern.Length < 2)
                return next;
            next[1] = 0;
            int i = 2, j = 0;
            while (i < pattern.Length)
            {
                if (pattern[i - 1] == pattern[j])
                {
                    next[i++] = ++j;
                }
                else
                {
                    j = next[j];
                    if (j == -1)
                    {
                        next[i++] = ++j;
                    }
                }
            }
            return next;
        }

        /// <summary>
        /// 查找判断字符串中是否存在关键字列表中的关键字
        /// </summary>
        /// <param name="source">待搜索字符串</param>
        /// <param name="keywords">关键字列表</param>
        /// <returns>查找结果</returns>
        public static bool Find(string source, string[] keywords)
        {
            int wordCount = keywords.Length;
            int[][] nexts = new int[wordCount][];
            int i = 0;
            for (i = 0; i < wordCount; i++)
            {
                nexts[i] = GetKmpNext(keywords[i]);
            }
            i = 0;
            int[] j = new int[nexts.Length];
            while (i < source.Length)
            {
                for (int k = 0; k < wordCount; k++)
                {
                    if (source[i] == keywords[k][j[k]])
                    {
                        j[k]++;
                    }
                    else
                    {
                        j[k] = nexts[k][j[k]];
                        if (j[k] == -1)
                        {
                            j[k]++;
                        }
                    }
                    if (j[k] >= keywords[k].Length)
                        return true;
                }
                i++;
            }
            return false;
        }
        
    }
}
