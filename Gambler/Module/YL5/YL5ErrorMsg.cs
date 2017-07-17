using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Module.YL5
{
    public class YL5ErrorMsg : BaseError
    {
        // 服务端返回自定义错误码范围 10000 ~ 19999
        public static readonly int I_S_ERR_MSG_FROM_SERVER = 10000;
        public static readonly string S_ERROR_VERIFY_CODE = "验证码错误";
    }
}
