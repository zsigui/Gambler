using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Module.HF
{
    public class HFConfig
    {

        // cookie 说明
        // tt = Math.random()
        // vcode = 1  (验证码请求时需要添加)
        // page_key = index (可不加)
        // PHPSESSID = xxx (由vcode请求后返回)
        // usercookie = xxx (由login返回的用户数据的json)
        public static readonly string URL_DOMAIN = "www.33hf33.com";
        /*
	     * 数据请求的过程
	     */
        private static readonly string URL_HOST_NAME = "http://www.33hf33.com";
        private static readonly string URL_HOST_ACTION = URL_HOST_NAME + "/php/action.php?action=";
        /**
         * 获取登录所需要的验证码 <br />
         * GET : s =  Math.random() * 10
         */
        public static readonly string URL_VERICODE = URL_HOST_NAME + "/php/vcode.php";
        /**
         * 请求登录 <br />
         * POST : username、password、varcode <br />
         * 
         */
        public static readonly string URL_LOGIN = URL_HOST_ACTION + "login";
        public static readonly string URL_LOGIN_H8 = URL_HOST_NAME + "/php/login.php";
        /**
         * 请求退出登录 <br />
         * POST : username、oid 
         */
        public static readonly string URL_LOGOUT = URL_HOST_ACTION + "logout";

        /**
         * 进入该页面流程：
         * 1. http://www.33hf33.com/php/vcode.php 获取验证码(返回 PHPSESSID 的 Cookie 进行后续)
         * 2. http://www.33hf33.com/php/action.php?action=login 执行登录(POST 携带参数)
         * 3. http://33hf33.com/php/login.php?action=h8&username=kaokkyyzz&oid=6f8f9bf9280964cbf98d214625bd971c&lottoType=PC&r=0.10532572414960861 获取
         * http://sp4ywqb1.mywinday.com/public/validate.aspx?us=3r88kaokkyyzz&k=62fd8177bd4c44739723e86de5f6ed39&lang=zh-cn&accType=HK&r=2041379373 这一url
         * 4. http://sp4ywqb1.mywinday.com/public/validate.aspx?us=3r88kaokkyyzz&k=62fd8177bd4c44739723e86de5f6ed39&lang=zh-cn&accType=HK&r=2041379373 执行获取 .ASPXAUTH 的 Cookie
         * 
         * PS.需要登录后，使用登录获取的数据请求H8的信息，参数 action = h8 & username = " + getUserCookie("username") + "&oid=" + getUserCookie("oid")+"&lottoType=PC&r="+Math.random()
         * lottoType : 登录代理，移动设备PM，计算机PC
         */
        public static readonly string URL_H8_HOST = "http://sp4ywqb1.mywinday.com/";
        /*
         * H8 游戏数据
         *
         * 
         * URL参数: ov=0&ot=t&tf=-1&TFStatus=0&update=false&r=2105276540&mt=0&wd=&t=1495619773291
         * 
         * GET参数说明： 
         * ot 取值 e（早盘） r（滚球） t（今日）
         * ov 取值 0（热门排序） 1（时间排序）
         * mt 取值 0（全部盘口） 1（主要盘口）
         * POST
         * 
         * COOKIE : ASP.NET_SessionId=ycpvbs55lxxo1i45hwslm1qu;(固定值，不添加会变成英文)、.ASPXAUTH（其实没什么作用）、a=1、td_cookide(非必须)
         *
         * 实际请求链接： URL_H8_HOST + "_view/OddsXGenRun.aspx"
         * X 取值 2 亚盘&大小   3   4 混合   5 全场/上半场 单/双 & 1X2   DC 双重机会
         */

        /// <summary>
        /// 亚盘&大小
        /// </summary>
        public static readonly string URL_ODD_DATA_BS = URL_H8_HOST + "_view/Odds2GenRun.aspx";
        /// <summary>
        /// 混合
        /// </summary>
        public static readonly string URL_ODD_DATA_MIX = URL_H8_HOST + "_view/Odds4Gen.aspx";
        /// <summary>
        /// 全场/上半场 单/双 & 1X2
        /// </summary>
        public static readonly string URL_ODD_DATA_OU = URL_H8_HOST + "_view/Odds5GenRun.aspx";
        /// <summary>
        /// 全场/上半场 波胆
        /// </summary>
        public static readonly string URL_ODD_DATA_CS = URL_H8_HOST + "_view/CSOdds1GenRun.aspx";
        /// <summary>
        /// 全场/半场
        /// </summary>
        public static readonly string URL_ODD_DATA_M = URL_H8_HOST + "_view/HTFTOdds1Gen.aspx";
        /// <summary>
        /// 全场/上半场 总入球
        /// </summary>
        public static readonly string URL_ODD_DATA_TG = URL_H8_HOST + "_view/TGOdds1GenRun.aspx";
        /// <summary>
        /// 全场/上半场 最先得分/最后得分
        /// </summary>
        public static readonly string URL_ODD_DATA_FGLG = URL_H8_HOST + "_view/FGLGOdds1Gen.aspx";
        /// <summary>
        /// 冠军
        /// </summary>
        public static readonly string URL_ODD_DATA_CPN = URL_H8_HOST + "_view/Odds50Gen.aspx";
        /**
         * 直播跳转，不过实际无作用
         */
        public static readonly string URL_LIVE_CAST = URL_H8_HOST + "_view/LiveCast.aspx?Id=824432&SocOddsId=7182293&isShowLiveCast=1";

        /**
         * 获取联赛数据，这个设置应该是绑定Cookie来的，需要解析HTML获取和设置，暂无必要
         */
        public static readonly string URL_LEAGUE_DATA = URL_H8_HOST + "_View/SelectLeague.aspx ";
        /**
         * referer: https://realtime.inplay.club/livecenter/match.html?k=b8477a2902424b02a22e4a93d5338d98&us=3r88kaokkyyzz&l=CN
         * 
         * 关键: matchId，需要由 URL_H8_HOST 这个页面返回的数据获取到，_ 表示时间戳，随机值即可
         * 
         * 返回:
         * [{
         *  "MID": 824432,
         *  "CID": "10",
         *  "EID": 17,
         *  "Info": "Start 1st half, kickoff:",
         *  "T": "0",
         *  "SID": "0"
         * }]
         * 
         * 后续请求： https://realtime.inplay.club/livecenter/data.aspx?matchId=824432&startEventId=385&endEventId=405&_=1495676642833
         * 
         * 额外: startEventId，endEventId, (请求的 startEventId 为 返回 JSON 的 EID， endEventID 为 EID+20 ), conf=1(说明比赛信息，可选)，refresh=1(当前赛事进攻数，射门数等)
         *
         * 
         * P.S. 
         * 事件说明 可以查找 CID 与 Info 对应的内容
         * https://realtime.inplay.club/livecenter/min/1.json // 英文
         * https://realtime.inplay.club/livecenter/min/2.json // 部分中文
         *
         * CID 对应内容: 1 代表主队 2 代表客队
         *  
         *
         */
        public static readonly string URL_REAL_TIME = "https://realtime.inplay.club/livecenter/data.aspx";
       

    }
}
