using Gambler.Utils.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.XPJ
{
    public class XPJClient
    {
        IVerifyCode _verifyCode = new XPJVerifyCode();
    }
}
