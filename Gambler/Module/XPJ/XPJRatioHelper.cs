using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.XPJ
{
    public class XPJRatioHelper
    {

        /**
	 * 获取香港深水盘口汇率
	 * @param hR
	 * @param cR
	 * @return 0 -> H 1 -> C
	 */
        private float[] getHK_ior(float hR, float cR)
        {
            float[] result = new float[2];
            if (hR <= 1000 && cR <= 1000)
            {
                result[0] = (float)(Math.Floor(hR / 10 + 0.0001f) * 10);
                result[1] = (float)(Math.Floor(cR / 10 + 0.0001f) * 10);
                return result;
            }
            float line = 2000 - (hR + cR);
            string nowType;
            float lowR, nowR, highR;
            if (hR > cR)
            {
                lowR = cR;
                nowType = "C";
            }
            else
            {
                lowR = hR;
                nowType = "H";
            }

            if (((2000 - line) - lowR) > 1000)
            {
                // 对盘马来盘
                nowR = (lowR + line) * (-1);
            }
            else
            {
                nowR = (2000 - line) - lowR;
            }
            if (nowR < 0)
            {
                highR = (float)Math.Floor(Math.Abs(1000 / nowR) * 1000);
            }
            else
            {
                highR = (2000 - line - nowR);
            }
            if (nowType.Equals("H", StringComparison.OrdinalIgnoreCase))
            {
                result[0] = (float)(Math.Floor(lowR / 10 + 0.0001f) * 10);
                result[1] = (float)(Math.Floor(highR / 10 + 0.0001f) * 10);
            }
            else
            {
                result[0] = (float)(Math.Floor(highR / 10 + 0.0001f) * 10);
                result[1] = (float)(Math.Floor(lowR / 10 + 0.0001f) * 10);
            }
            return result;
        }

        private float[] getIor(string oddType, float hR, float cR)
        {
            float[] result = new float[2];

            hR = (float)(Math.Floor((hR * 1000) + 0.001) / 1000);
            cR = (float)(Math.Floor((cR * 1000) + 0.001) / 1000);

            if (hR < 11) hR *= 1000;
            if (cR < 11) cR *= 1000;

            if ("H".Equals(oddType, StringComparison.OrdinalIgnoreCase))
            {
                // 香港输水盘
                result = getHK_ior(hR, cR);
            }
            else if ("M".Equals(oddType, StringComparison.OrdinalIgnoreCase))
            {
                // 马来盘
            }
            else if ("I".Equals(oddType, StringComparison.OrdinalIgnoreCase))
            {
                // 印尼盘
            }
            else if ("E".Equals(oddType, StringComparison.OrdinalIgnoreCase))
            {
                // 欧洲盘
            }
            else
            {
                // 香港盘
                result[0] = hR;
                result[1] = cR;
            }

            result[0] /= 1000;
            result[1] /= 1000;

            return result;
        }

        /**
         * 根据盘口及获取到的基础赔率转换为对应的真正赔率
         * @param oddType 盘口类型
         * @param hR 主队赔率
         * @param cR 客队赔率
         * @return 下标0：hR 下标1：cR
         */
        public string[] getIor(string oddType, string hR, string cR)
        {
            string[] result = new string[2];
            if (!String.IsNullOrEmpty(hR)
                    && !String.IsNullOrEmpty(cR))
            {
                float[] rs = getIor(oddType, float.Parse(hR), float.Parse(cR));
                result[0] = string.Format("{0:N2}", rs[0]);
                result[1] = string.Format("{0:N2}", rs[1]);
            }
            else
            {
                result[0] = "";
                result[1] = "";
            }
            return result;
        }
    }
}
