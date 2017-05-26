using Gambler.Module.HF.Model;
using Gambler.Utils;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace Gambler.Module.HF
{
    public class HFClient : BaseClient
    {
        private string _account;
        private string _password;

        private HFUser _user;
        private CookieCollection _cookies;
        private WebHeaderCollection _headers;

        private HFVerifyCode _verifyCode = new HFVerifyCode(Application.StartupPath + "\\Resources\\HF_trainData");

        private CookieCollection _cookiesForH8;

        public HFClient(string account, string password)
        {
            _account = account;
            _password = password;
            InitStoredHeader();
        }

        private void InitStoredHeader()
        {
            _headers = new WebHeaderCollection();
            _headers.Add("X-Requested-Width", "XMLHttpRequest");
            _headers.Add("Accept-Encoding", "gzip, defalte");
            _headers.Add("Accept-Language", "zh-CN,zh;q=0.8,en;q=0.6");
            SetCookie();
        }

        private void SetCookie()
        {
            if (_cookies == null)
            {
                _cookies = new CookieCollection();
            }
            _cookies.Add(new Cookie("tt", "" + new Random().NextDouble(), "/", HFConfig.URL_DOMAIN));
            _cookies.Add(new Cookie("vcode", "1", "/", HFConfig.URL_DOMAIN));
        }

        protected void RespOnFail<T>(OnFailedHandler callback, int httpStatus, HFRespBase<T> data)
        {
            if (callback != null)
            {
                if (!HttpUtil.IsCodeSucc(httpStatus))
                {
                    callback.Invoke(httpStatus, HFErrorMsg.I_C_BAD_HTTP_REQUEST, HFErrorMsg.C_BAD_HTTP_REQUEST);
                }
                else if (data != null && !HFErrorMsg.IsSuccess(data.code))
                {
                    callback.Invoke(httpStatus, data.code, HFErrorMsg.GetMessageByCode(data.code));
                }
                else
                {
                    callback.Invoke(httpStatus, HFErrorMsg.I_C_BAD_RESP_DATA, HFErrorMsg.C_BAD_RESP_DATA);
                }
            }
        }


        private Boolean IsSuccess<T>(int statusCode, HFRespBase<T> data)
        {
            return HttpUtil.IsCodeSucc(statusCode) && data != null && HFErrorMsg.IsSuccess(data.code);
        }

        public void Login(OnSuccessHandler<HFRespBase<HFUser>> onSuccess, OnFailedHandler onFail, OnErrorHandler onError)
        {
            Login(4, onSuccess, onFail, onError);
        }

        public void Login(int retryCount, OnSuccessHandler<HFRespBase<HFUser>> onSuccess, OnFailedHandler onFail, OnErrorHandler onError)
        {

            Dictionary<string, string> queryDict = ConstructKeyValDict("s", "" + new Random().NextDouble());

            HttpUtil.Get<byte[]>(HFConfig.URL_VERICODE, _headers, _cookies, queryDict,
               (data) =>
               {
                   return IOUtil.Read(data);
               },
               (statusCode, data, cookies) =>
               {
                   if (HttpUtil.IsCodeSucc(statusCode) && data != null)
                   {
                       string vc = _verifyCode.ParseCode(data);
                       Console.WriteLine("验证码: " + vc);
                       if (String.IsNullOrEmpty(vc) || vc.Length != 4)
                       {
                           if (retryCount == 0)
                           {
                               if (onFail != null)
                                   onFail.Invoke(statusCode, HFErrorMsg.I_C_FAIL_TO_VERIFY_CODE, HFErrorMsg.C_FAIL_TO_VERIFY_CODE);
                               return;
                           }

                           Login(retryCount - 1, onSuccess, onFail, onError);

                           // 重新进行验证码请求
                       }
                       else
                       {
                           _cookies.Add(cookies);
                           LoginByCode(vc, retryCount, onSuccess, onFail, onError);
                       }
                       return;
                   }

                   RespOnFail<HFUser>(onFail, statusCode, null);
               },
               (e) =>
               {
                   RespOnError(onError, e);
               });
        }

        public void LoginByCode(string code, int retryCount,
            OnSuccessHandler<HFRespBase<HFUser>> onSuccess, OnFailedHandler onFail, OnErrorHandler onError)
        {
            Dictionary<string, string> bodyDict = ConstructKeyValDict(
                 "username", _account,
                 "password", _password,
                 "varcode", code);
            HttpUtil.Post(HFConfig.URL_LOGIN, _headers, _cookies, bodyDict,
                (data) =>
                {
                    return JsonUtil.fromJson<HFRespBase<HFUser>>(IOUtil.ReadString(data));
                },
               (statusCode, data, cookies) =>
               {

                   if (IsSuccess(statusCode, data))
                   {
                       string userCookie = JsonUtil.toJson(data.data);
                       cookies.Add(new Cookie("usercookie", System.Web.HttpUtility.UrlEncode(userCookie, Encoding.UTF8)));
                       _user = data.data;
                       RespOnSuccess(onSuccess, data);
                       return;
                   }

                   if (retryCount != 0)
                   {
                       Login(retryCount - 1, onSuccess, onFail, onError);
                       return;
                   }
                   Logout(null, null, null);
                   RespOnFail(onFail, statusCode, data);
               },
               (e) =>
               {
                   Logout(null, null, null);
                   RespOnError(onError, e);
               });
        }


        public void Logout(OnSuccessHandler<HFRespBase<int>> onSuccess, OnFailedHandler onFail, OnErrorHandler onError)
        {
            if (_user == null)
            {
                RespOnSuccess(onSuccess, null);
                return;
            }
            Dictionary<string, string> bodyDict = ConstructKeyValDict(
                 "username", _user.username,
                 "oid", _user.oid);
            HttpUtil.Post(HFConfig.URL_LOGOUT, _headers, _cookies, bodyDict,
                (data) =>
                {
                    return JsonUtil.fromJson<HFRespBase<int>>(IOUtil.ReadString(data));
                },
               (statusCode, data, cookies) =>
               {

                   if (IsSuccess(statusCode, data))
                   {
                       RespOnSuccess(onSuccess, data);
                       return;
                   }
                   RespOnFail(onFail, statusCode, data);
               },
               (e) =>
               {
                   RespOnError(onError, e);
               });

            // 成功与否都当成退出游戏
            _user = null;
        }

        // 以下获取H8相关页面内容（包括Live）

        public void LoginForH8(OnSuccessHandler<string> onSuccess, OnFailedHandler onFail, OnErrorHandler onError)
        {
            if (_user == null)
            {
                if (onFail != null)
                {
                    onFail.Invoke(200, HFErrorMsg.I_LOGOUT, HFErrorMsg.GetMessageByCode(HFErrorMsg.I_LOGOUT));
                }
                return;
            }
            Dictionary<string, string> queryDict = ConstructKeyValDict(
                 "action", "h8",
                 "username", _user.username,
                 "oid", _user.oid,
                 "lottoType", "PC",
                 "r", "" + new Random().NextDouble());
            HttpUtil.Get(HFConfig.URL_LOGIN_H8, _headers, _cookies, queryDict,
                (data) =>
                {
                    return IOUtil.ReadString(data);
                },
                (statusCode, data, cookies) =>
                {

                    if (HttpUtil.IsCodeSucc(statusCode) && !String.IsNullOrEmpty(data))
                    {
                        Console.WriteLine("获取H8链接是: " + data);
                        LoginForH8Valid(data, onSuccess, onFail, onError);
                        return;
                    }
                    RespOnFail<string>(onFail, statusCode, null);
                }, 
                (e) =>
                {
                    RespOnError(onError, e);
                });
        }

        private void InitDefaultH8Cookie()
        {
            if (_cookiesForH8 == null)
            {
                _cookiesForH8 = new CookieCollection();
                // 英文为默认，50ikpw55hofypvu1m44ssh55 
                _cookiesForH8.Add(new Cookie("ASP.NET_SessionId", "ycpvbs55lxxo1i45hwslm1qu", "/", "sp4ywqb1.mywinday.com"));
            }
        }

        private void LoginForH8Valid(string url, OnSuccessHandler<string> onSuccess, OnFailedHandler onFail, OnErrorHandler onError)
        {
            HttpUtil.Get(url, _headers, _cookiesForH8, null,
                (data) =>
                {
                    return IOUtil.ReadString(data);
                },
                (statusCode, data, cookies) =>
                {
                    Console.WriteLine("H8验证结果: " + data);
                    // 执行成功会重定向操作
                    if (statusCode == 302 && data != null && data.Contains("http://sp4ywqb1.mywinday.com/main.aspx"))
                    {
                        InitDefaultH8Cookie();
                        _cookiesForH8.Add(cookies);
                        RespOnSuccess(onSuccess, data);
                        return;
                    }
                    RespOnFail<string>(onFail, statusCode, null);
                },
                (e) =>
                {
                    RespOnError(onError, e);
                });
        }

        /**
         * 获取今日直播比赛的ID列表
         */
        public void GetOddData(OnSuccessHandler<string> onSuccess, OnFailedHandler onFail, OnErrorHandler onError)
        {
            // 实际不需要Cookie就可以直接请求= =
            InitDefaultH8Cookie();
            // 各个参数含义还有待理解
            Dictionary<string, string> queryDict = ConstructKeyValDict(
                "ov", "1",
                "ot", "r",
                "mt", "0",
                "t", "" + TimeUtil.CurrentTimeMillis(),
                "tf", "-1",
                "TFStatus", "0",
                "update", "false",
                "r", "1889011725");
            //             string data = FileUtil.ReadContentFromFilePath(Application.StartupPath + "\\data.html", Encoding.GetEncoding("GBK"));
            //              //执行成功会重定向操作
            //              //返回的为 XML文件，需要进行解析
            //             ParseOddDataXml(data);
            //             RespOnSuccess(onSuccess, data);
            HttpUtil.Post(HFConfig.URL_ODD_DATA_BS, _headers, _cookiesForH8, queryDict, null,
                (data) =>
                {
                    return IOUtil.ReadString(data);
                },
                (statusCode, data, cookies) =>
                {
                    if (HttpUtil.IsCodeSucc(statusCode) && !String.IsNullOrEmpty(data))
                    {
                        ParseOddDataXml(data);
                        RespOnSuccess(onSuccess, data);
                        return;
                    }
                    RespOnFail<string>(onFail, statusCode, null);
                },
                (e) =>
                {
                    RespOnError(onError, e);
                });
        }

        private void ParseOddDataXml(string htmlContent)
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
                                    matchs.Add(ExtractMatchFromNode(node, newsetLeague));
                                }
                                else
                                {
                                    newsetLeague = node.FirstChild.InnerText;
                                }
                                break;
                            case "GridAltRunItem":
                                // 该节点为赛事节点<tr class="GridAltRunItem" />
                                matchs.Add(ExtractMatchFromNode(node, newsetLeague));
                                break;
                        }
                    }
                }
            }

            for (int i = 0; i < matchs.Count; i++)
            {
                HFSimpleMatch tmpMatch = matchs[i];
                Console.WriteLine(String.Format("赛事ID:{0}，{1}:{2} 比分 {3}，所属联赛: {4}",
                    tmpMatch.MID, tmpMatch.Home, tmpMatch.Away, tmpMatch.Score, tmpMatch.League));
            }
        }

        private HFSimpleMatch ExtractMatchFromNode(HtmlNode node, string league)
        {
            HFSimpleMatch match = new HFSimpleMatch();
            match.League = league;
            HtmlNodeCollection list = node.ChildNodes;
            HtmlNodeCollection tmpList = list[0].ChildNodes;
            match.Score = tmpList[0].InnerText;
            match.Time = tmpList[1].InnerText;
            tmpList = list[1].FirstChild.FirstChild.ChildNodes;
            HtmlNodeCollection tmpList2 = tmpList[1].FirstChild.ChildNodes;
            match.Home = tmpList2[0].InnerText;
            match.Away = tmpList2[1].InnerText;
            match.MID = FindMatchPattern(tmpList[1].ParentNode.InnerHtml, @"LiveCast.aspx\?Id=(\d+)?");
            return match;
        }

        //         private void ParseOddDataXml(string htmlContent)
        //         {
        //             List<HFSimpleMatch> matchs = new List<HFSimpleMatch>();
        //             string newsetLeague = "";
        //             
        //             Lexer lexer = new Lexer(htmlContent);
        // 
        //             Parser parser = new Parser(lexer);
        //             NodeFilter filter = new HasAttributeFilter("class", "GridHeaderRun");
        //             NodeList nodes = parser.Parse(filter);
        //             if (nodes.Count > 0)
        //             {
        //                 nodes = nodes[0].Parent.Children;
        //             }
        // 
        //             ITag tmpTag;
        //             string clsValue;
        //             HFSimpleMatch tmpMatch;
        //             for (int i = 0; i < nodes.Count; i++)
        //             {
        //                 tmpTag = nodes[i] as ITag;
        //                 clsValue = tmpTag.GetAttribute("class");
        //                 if (clsValue != null)
        //                 {
        //                     switch (clsValue)
        //                     {
        //                         case "GridRunItem":
        //                              该节点可能是联赛名节点，进行判别 <tr class="GridRunItem" />
        //                             if (tmpTag.Children.Count > 1)
        //                             {
        //                                  有两个子节点，判断为赛事节点（多个 <td /> 节点）
        //                                 tmpMatch = ExtractMatchFromNode(tmpTag, newsetLeague);
        //                                 matchs.Add(tmpMatch);
        //                             } else
        //                             {
        //                                 newsetLeague = tmpTag.FirstChild.FirstChild.FirstChild.FirstChild.Children[2].FirstChild.GetText();
        //                             }
        //                             break;
        //                         case "GridAltRunItem":
        //                              该节点为赛事节点 <tr class="GridAltRunItem" />
        //                             tmpMatch = ExtractMatchFromNode(tmpTag, newsetLeague);
        //                             matchs.Add(tmpMatch);
        //                             break;
        //                     }
        //                 }
        //             }
        // 
        //             for (int i = 0; i < matchs.Count; i++)
        //             {
        //                 tmpMatch = matchs[i];
        //                 Console.WriteLine(String.Format("赛事ID:{0}，{1}:{2} 比分 {3}，所属联赛: {4}",
        //                     tmpMatch.MID, tmpMatch.Home, tmpMatch.Away, tmpMatch.Score, tmpMatch.League));
        //             }
        //         }
        // 
        //         private HFSimpleMatch ExtractMatchFromNode(ITag tmpTag, string league)
        //         {
        //             HFSimpleMatch match = new HFSimpleMatch();
        //             match.League = league;
        //             NodeList list = tmpTag.Children;
        //             NodeList tmpList = list[0].Children;
        //             match.Score = tmpList[0].GetText();
        //             match.Time = tmpList[1].FirstChild.GetText();
        //             tmpList = list[1].FirstChild.FirstChild.FirstChild.Children;
        //             NodeList tmpList2 = tmpList[1].FirstChild.FirstChild.Children;
        //             match.Home = tmpList2[0].FirstChild.FirstChild.GetText();
        //             match.Away = tmpList2[1].FirstChild.FirstChild.GetText();
        //             match.MID = FindMatchPattern(tmpList[2].ToString(), @"LiveCast.aspx\?Id=(\d+)?");
        //             return match;
        //         }

        private string FindMatchPattern(string source, string pattern)
        {
            Regex regex = new Regex(pattern);
            Match mc = regex.Match(source);
            // 查找不到通常说明没有该直播Live标签，则ID为空
            if (mc.Success)
                return mc.Groups[1].Value;
            return "";
        }
        
    }
}
