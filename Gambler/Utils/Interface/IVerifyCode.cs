using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Utils.Interface
{
    public interface IVerifyCode
    {
        String ParseCode(Stream stream);
    }
}
