using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.XPJ
{
    public class XPJErrorMsg
    {
        public static readonly int I_SUCCESS = 0;
        public static readonly string SUCCESS = "成功";

        // 服务端返回自定义错误码范围 10000 ~ 19999
        public static readonly int I_S_ERR_MSG_FROM_SERVER = 10000;
        public static readonly string S_ERROR_VERIFY_CODE = "验证码错误！";
        

        // 客户端自定义错误码范围 20000 ~ 29999
        public static readonly int I_C_EXEC_EXCEPTION = 21000;
        public static readonly string C_EXEC_EXCEPTION = "逻辑执行异常";
        public static readonly int I_C_FAIL_TO_VERIFY_CODE = 21001;
        public static readonly string C_FAIL_TO_VERIFY_CODE = "识别验证码失败";

        public static readonly int I_C_BAD_HTTP_REQUEST = 22000;
        public static readonly string C_BAD_HTTP_REQUEST = "HTTP请求出错";


        public static readonly int I_C_BAD_RESP_DATA = 23000;
        public static readonly string C_BAD_RESP_DATA = "请求返回的服务器数据错误";


    }
}
