using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Module
{
    public class BaseError
    {
        public static readonly int I_SUCCESS = 0;
        public static readonly string SUCCESS = "成功";
        
        public static readonly int I_C_EXEC_EXCEPTION = 100;
        public static readonly string C_EXEC_EXCEPTION = "逻辑执行异常";
        public static readonly int I_C_FAIL_TO_VERIFY_CODE = 101;
        public static readonly string C_FAIL_TO_VERIFY_CODE = "识别验证码失败";
        public static readonly int I_C_ACCOUNT_PWD_ERROR = 102;
        public static readonly string C_ACCOUNT_PWD_ERROR = "用户名或密码错误";
        public static readonly int I_C_NO_LOGIN = 103;
        public static readonly string C_NO_LOGIN = "尚未登录";

        public static readonly int I_C_BAD_HTTP_REQUEST = 200;
        public static readonly string C_BAD_HTTP_REQUEST = "HTTP请求出错";


        public static readonly int I_C_BAD_RESP_DATA = 300;
        public static readonly string C_BAD_RESP_DATA = "请求返回的服务器数据错误";
    }
}
