using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Utils.Interface
{
    public interface IDataAdapter<P, R>
    {
        R convertTo(P param);
    }
}
