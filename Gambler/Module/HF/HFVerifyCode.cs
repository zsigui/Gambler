using Gambler.Utils;
using Gambler.Utils.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Module.HF
{
    public class HFVerifyCode : AbsVerifyCode
    {
        
        public HFVerifyCode(string trainDataPath) : base(trainDataPath)
        {
            VAL_DIFF_COLOR = 450;
        }
    }
}
