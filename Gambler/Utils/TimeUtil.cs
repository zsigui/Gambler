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

        public static string FormatTime(long timestamp, string format)
        {
            DateTime dt1970 = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            long t = dt1970.Ticks + timestamp * 10000;
            return new DateTime(t).ToString(format);
        }
    }
}
