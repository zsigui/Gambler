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
         * POST : username、password、varcode 
         */
        public static readonly string URL_LOGIN = URL_HOST_ACTION + "login";
        /**
         * 请求退出登录 <br />
         * POST : username、oid 
         */
        public static readonly string URL_LOGOUT = URL_HOST_ACTION + "logout";

        public static readonly string URL_H8_HOST = "http://sp4ywqb1.mywinday.com/";
        /**
         * H8 游戏数据
         * 
         * URL参数: ov=0&ot=t&tf=-1&TFStatus=0&update=false&r=2105276540&mt=0&wd=&t=1495619773291
         * 
         * POST
         */
        public static readonly string URL_ODD_DATA = URL_H8_HOST + "_view/Odds2GenRun.aspx";
        
    }
}
