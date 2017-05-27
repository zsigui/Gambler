using Gambler.Module.HF.Model;
using Gambler.Utils;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Gambler.Module
{
    public class HFHtmlParser
    {

        public static List<HFSimpleMatch> ParseOddDataXml(string htmlContent)
        {
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(htmlContent);
            HtmlAgilityPack.HtmlNode node = htmlDoc.DocumentNode
                .SelectSingleNode("//tr[@class='GridHeaderRun']").ParentNode;
            HtmlAgilityPack.HtmlNodeCollection nodes = node.ChildNodes;

            List<HFSimpleMatch> matchs = new List<HFSimpleMatch>();
            string newsetLeague = "";
            int count = nodes.Count;
            string clsValue;
            HtmlAttribute atrrs;
            HFSimpleMatch tmpMatch;
            for (int i = 0; i < count; i++)
            {
                node = nodes[i];
                atrrs = node.Attributes["class"];
                if (atrrs != null)
                {
                    clsValue = atrrs.Value;
                    if (!String.IsNullOrEmpty(clsValue))
                    {
                        switch (clsValue)
                        {
                            case "GridRunItem":
                                // 该节点可能是联赛名节点，进行判别 < tr class="GridRunItem" />
                                if (node.ChildNodes.Count > 1)
                                {
                                    //有两个子节点，判断为赛事节点（多个<td /> 节点）
                                    tmpMatch = ExtractMatchFromNode(node, newsetLeague);
                                    if (tmpMatch != null)
                                        matchs.Add(tmpMatch);
                                }
                                else
                                {
                                    try
                                    {
                                        newsetLeague = StringUtil.TraditionalToSimple(ReplcaeBlank(node.FirstChild.InnerText, ""));
                                    }
                                    catch (Exception e) { }
                                }
                                break;
                            case "GridAltRunItem":
                                // 该节点为赛事节点<tr class="GridAltRunItem" />
                                tmpMatch = ExtractMatchFromNode(node, newsetLeague);
                                if (tmpMatch != null)
                                    matchs.Add(tmpMatch);
                                break;
                        }
                    }
                }
            }

            for (int i = 0; i < matchs.Count; i++)
            {
                tmpMatch = matchs[i];
                Console.WriteLine(String.Format("赛事ID:{0}，{1}:{2} 比分 {3}，所属联赛: {4}",
                    tmpMatch.MID, tmpMatch.Home, tmpMatch.Away, tmpMatch.Score, tmpMatch.League));
            }
            return matchs;
        }

        private static HFSimpleMatch ExtractMatchFromNode(HtmlNode node, string league)
        {
            if (String.IsNullOrEmpty(league))
                return null;
            try
            {
                HFSimpleMatch match = new HFSimpleMatch();
                match.League = league;
                HtmlNodeCollection list = node.ChildNodes;
                HtmlNodeCollection tmpList = list[0].ChildNodes;
                match.Score = StringUtil.TraditionalToSimple(ReplcaeBlank(tmpList[0].InnerText, ""));
                match.Time = StringUtil.TraditionalToSimple(ReplcaeBlank(tmpList[1].InnerText, ""));
                tmpList = list[1].FirstChild.FirstChild.ChildNodes;
                HtmlNodeCollection tmpList2 = tmpList[1].FirstChild.ChildNodes;
                match.Home = StringUtil.TraditionalToSimple(ReplcaeBlank(tmpList2[0].InnerText, ""));
                match.Away = StringUtil.TraditionalToSimple(ReplcaeBlank(tmpList2[1].InnerText, ""));
                match.MID = StringUtil.TraditionalToSimple(ReplcaeBlank(FindMatchPattern(tmpList[1].ParentNode.InnerHtml, @"LiveCast.aspx\?Id=(\d+)?"), ""));
                return match;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private static string FindMatchPattern(string source, string pattern)
        {
            Regex regex = new Regex(pattern);
            Match mc = regex.Match(source);
            // 查找不到通常说明没有该直播Live标签，则ID为空
            if (mc.Success)
                return mc.Groups[1].Value;
            return "";
        }

        private static string ReplcaeBlank(string input, string defaultVal)
        {
            return String.IsNullOrEmpty(input)? defaultVal : input.Replace("&nbsp;", "").Trim();
        }
    }
}
