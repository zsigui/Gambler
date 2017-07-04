using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Module.X469.Model
{
    [JsonObject(MemberSerialization.OptOut)]
    public class X469Login
    {
        /// <summary>
        /// 0 用户名密码错误 1 验证码错误 2 验证码超时 3 登录成功 other 请求失败
        /// </summary>
        public string result;
    }
}
