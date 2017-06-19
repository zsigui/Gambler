using Gambler.Module.XPJ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Utils
{
    public class SearchUtil
    {

        public static List<XPJOddData> FilterBySearch(string searchText, List<XPJOddData> source)
        {
            List<XPJOddData> listData = new List<XPJOddData>();
            if (!String.IsNullOrEmpty(searchText))
            {
                string[] keys = searchText.Split(' ');
                foreach (XPJOddData d in source)
                {
                    if (FindSimilar(d, keys))
                    {
                        listData.Add(d);
                    }
                }
            }
            else
            {
                foreach (XPJOddData d in source)
                {
                    listData.Add(d);
                }
            }
            return listData;
        }

        private static bool FindSimilar(XPJOddData d, string[] keys)
        {
            LevenshteinDistance tool = LevenshteinDistance.DefaultInstance;
            float cmpRatio = 0.75f;
            string tmp;
            // 0ahl 0ahl  高三位表示存在，低三位表示最终结果 
            // 低三位分别表示 a, h, l，当 l 与其他同时存在，需要满足 l, h 或者 l, a
            int b = 0;
            int tmpB = 0;
            foreach (string k in keys)
            {
                if (!String.IsNullOrEmpty(k))
                {
                    if (k.StartsWith("l:"))
                    {
                        b |= 0x10;
                        tmp = k.Substring(2);
                        if (tool.Cmp(tmp, d.league) > cmpRatio || d.league.Contains(tmp))
                        {
                            b |= 0x01;
                        }
                    }
                    else if (k.StartsWith("h:"))
                    {
                        b |= 0x20;
                        tmp = k.Substring(2);
                        if (tool.Cmp(tmp, d.home) > cmpRatio || d.home.Contains(tmp))
                        {
                            b |= 0x2;
                        }
                    }
                    else if (k.StartsWith("a:"))
                    {
                        b |= 0x40;
                        tmp = k.Substring(2);
                        if (tool.Cmp(tmp, d.guest) > cmpRatio || d.guest.Contains(tmp))
                        {
                            b |= 0x4;
                        }
                    }
                    else
                    {
                        return tool.Cmp(k, d.league) > cmpRatio
                            || tool.Cmp(k, d.home) > cmpRatio
                            || tool.Cmp(k, d.guest) > cmpRatio
                            || d.league.Contains(k)
                            || d.home.Contains(k)
                            || d.guest.Contains(k);
                    }

                    tmpB = (b >> 4);
                    if (((tmpB & 0x3) == 3 && (b & 0x3) == 3)
                        || ((tmpB & 0x5) != 5) && (b & 0x5) == 5)
                    {
                        // 满足条件，即是联赛名跟队伍名同时存在且都各自有存在匹配情况
                        return true;
                    }
                }
            }
            tmpB = (b >> 4);
            return ((tmpB & 0x1) == 0 && (b & 0x6) > 0) || ((tmpB & 0x1) == 1 && (tmpB & 0x6) == 0);
        }

        public static List<XPJOddData> FilterByLeague(string leagueVal, Dictionary<string, List<XPJOddData>> odd, List<XPJOddData> source)
        {
            List<XPJOddData> listData = new List<XPJOddData>();
            if (!String.IsNullOrEmpty(leagueVal) && odd.ContainsKey(leagueVal))
            {
                foreach (XPJOddData d in odd[leagueVal])
                {
                    listData.Add(d);
                }
            }
            else
            {
                foreach (XPJOddData d in source)
                {
                    listData.Add(d);
                }
            }
            return listData;
        }
    }
}
