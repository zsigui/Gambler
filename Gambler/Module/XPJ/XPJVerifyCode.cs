using Gambler.Utils.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Gambler.Utils;
using System.IO;
using System.Windows.Forms;

namespace Gambler.XPJ
{
    public class XPJVerifyCode : AbsVerifyCode
    {
        public XPJVerifyCode(string trainDataPath) : base(trainDataPath)
        {
        }
    }
}
