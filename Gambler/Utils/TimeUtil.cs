using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Utils
{
    public class TimeUtil
    {
        public static long CurrentTimeMillis()
        {
            return (long)((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds);
        }
    }
}
