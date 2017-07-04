using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Module.X469
{
    public class X469Config
    {
        public static readonly string URL_DOMAIN = "www.469355.com";
        /*
	     * 数据请求的过程
	     */
        private static readonly string URL_HOST_NAME = "https://www.469355.com/member/aspx";

        /**
        * 获取登录所需要的验证码 <br />
        * GET : _r (0~1间随机数)
        */
        public static readonly string URL_VERICODE = URL_HOST_NAME + "/verification_code.aspx";
        /**
         * 请求格式查看：JS返回 login_bbin.js?v=6.6?v=66
         * 
         * 请求登录 <br />
         * GET: action=checklogin
         * POST : username、password、captcha 
         * 登录成功会set-Cookie: sid=?
         * 返回：result = 0 用户名密码错误  1/2 验证码错误 3 登录成功 5 请求失败 6 账号被禁用 7 账号审核中
         * 
         * 
         * 判断是否登录 <br />
         * Cookie: ASP.NET_SessionId, sid
         * GET: action=islogin
         * 返回：result = 0 未登录 1 已登录
         */
        public static readonly string URL_LOGIN = URL_HOST_NAME + "/do.aspx?action=checklogin";

        public static readonly string URL_USER = URL_HOST_NAME + "/do.aspx?action=islogin";

        /**
         * 获取滚球博彩数据 <br />
         * GET: action=re, _={utc_time}, keyword={%league%}(可空，模糊搜索), data=json, uid, page（可空）
         * 
         * 返回：a0: 分类头忽略，a1: 联赛ID，a2：主队，a3：客队，a4：主队ID，a5：客队ID，a6：，a7：，a8：，a9：，a10：全场让球数，
         * a11：全场让球-主赔率，a12：全场让球-客赔率，a13：全场大小数，a14：全场大-赔率，a15：全场小-赔率，a16：主队进球数，a17：客队进球数，
         * a18：日期，a19：比赛已进行时间，a20：全场让球-主让客，a21：全场让球-客让主，a22：全场大小-大，a23：全场大小-小，a24：，
         * a25：，a26：联赛名称，a27：，a28：，a29：，a30：上半让球数，a31：上半让球-主赔率，a32：上半让球-客赔率，a33：上半大小数，
         * a34：上半大-赔率，a35：上半小-赔率，a36：上半让球-主让客，a37：上半让球-客让主，a38：上半大小-大，a39：上半大小-小，a40：，a41：，a42：，a43：，a44：
         * mid：盘口ID
         * 
         **/
        public static readonly string URL_ODD_RB = "https://a600g.lq2222.org/sport/football.aspx";

        /**
         * 进行博彩下注 <br />
         * 
         * GET: uid （携带 sid 的 Cookie 执行 GET 请求 https://www.469355.com/sport.aspx 并解析返回的HTML页面获取 <iframe /> 标签下的 src ，从 url query 参数中提取 uid）
         * POST: money（下注金额）, bet（H：主/小 C：客/大）, rate（盘口利率）, ltype（下注类型 9 全场让球 10 全场大小）, mid（盘口ID）, auto=1（是否接受自动下注）
         * 
         * 返回字符串： fail: "false|{msg}"  否则成功
         **/
        public static readonly string URL_BET = "https://a600g.lq2222.org/sport/order_ft.aspx";
    }
}
