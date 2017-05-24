using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.XPJ
{
    public class XPJConfig
    {
        public static readonly string URL_DOMAIN = "www.1559501.com";
        /*
	     * 数据请求的过程
	     */
        private static readonly string URL_HOST_NAME = "http://www.1559501.com";
        /**
         * 获取登录所需要的验证码 <br />
         * GET : timestamp (单位:ms)
         */
        public static readonly string URL_VERICODE = URL_HOST_NAME + "/verifycode.do";
        /**
         * 请求登录 <br />
         * POST : account、password、verifyCode 
         */
        public static readonly string URL_LOGIN = URL_HOST_NAME + "/login.do";
        /**
         * 获取用户账号信息 <br />
         * POST
         */
        public static readonly string URL_USER_INFO = URL_HOST_NAME + "/meminfo.do";
        /**
         * 获取下注数据名称 <br />
         * POST : gameType、pageNo、sortType、showLegs
         */
        public static readonly string URL_ODD_DATA = URL_HOST_NAME + "/sports/hg/getData.do";
        /**
         * 获取请求的联赛名称 <br />
         * POST : gameType <br />
         */
        public static readonly string URL_SPORT_LEGUES = URL_HOST_NAME + "/sports/hg/getLeaues.do";
        /**
         * 获取指定盘口的赔率情况 <br />
         * POST : data ( json字符串 ) <br />
         * 值结构：{"plate":"H", "gameType": (string) , "items": [{"gid": (int), "odds": (string), "type": (string), "project": (string)}]} <br />
         * 示例：data={"plate":"H","gameType":"FT_TD_MN","items":[{"gid":2738302,"odds":"0.88","type":"ior_OUH","project":"9"}]} (project根据盘口类型可能不存在)
         */
        public static readonly string URL_GET_ODD = URL_HOST_NAME + "/sports/hg/getOdds.do";
        /**
         * 执行下注请求 <br />
         * POST : data ( 值结构： {"money": (string), "acceptBestOdds": (boolean), "plate":"H", "gameType": (string) , "items": []} ) <br />
         * 值结构 ：跟上面的除了多了 money 跟 accptBestOdds 两个外其它一致
         */
        public static readonly string URL_BET = URL_HOST_NAME + "/sports/hg/bet/bet.do";
    }
}
