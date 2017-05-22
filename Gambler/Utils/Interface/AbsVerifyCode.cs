using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Utils.Interface
{
    public abstract class AbsVerifyCode : IVerifyCode
    {
        public abstract string ParseCode(Stream stream);


    }
}
