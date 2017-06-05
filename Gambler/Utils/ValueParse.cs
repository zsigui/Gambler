using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Utils
{
    public class ValueParse
    {
        public static long ParseLong(string key, long defVal = 0)
        {
            long result;
            if (long.TryParse(key, out result))
            {
                return result;
            }
            return defVal;
        }


        public static bool ParseBoolean(string key, bool defVal = false)
        {
            bool result;
            if (bool.TryParse(key, out result))
            {
                return result;
            }
            return false;
        }

        public static int ParseInt(string key, int defVal = 0)
        {
            int result;
            if (Int32.TryParse(key, out result))
            {
                return result;
            }
            return defVal;
        }

        public static float ParseFloat(string key, float defVal = 0f)
        {
            float result;
            if (float.TryParse(key, out result))
            {
                return result;
            }
            return defVal;
        }
    }
}
