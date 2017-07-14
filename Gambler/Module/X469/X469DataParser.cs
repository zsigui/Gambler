using Gambler.Module.X469.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Module.X469
{
    public class X469DataParser
    {
        public static Dictionary<string, List<X469OddItem>> TransformRespDataToXPJOddDataDict(X469OddData resp, out List<X469OddItem> sourceData)
        {
            if (resp == null || resp.results == null || resp.results.Count <= 0)
            {
                sourceData = null;
                return null;
            }
            Dictionary<string, List<X469OddItem>> retData = new Dictionary<string, List<X469OddItem>>();
            List<X469OddItem> tmpValue;
            sourceData = new List<X469OddItem>(resp.results.Count);
            foreach (X469OddItem tmp in resp.results)
            {
                if (tmp != null)
                {
                    if (!retData.TryGetValue(tmp.a26, out tmpValue))
                    {
                        tmpValue = new List<X469OddItem>();
                        retData.Add(tmp.a26, tmpValue);
                    }
                    sourceData.Add(tmp);
                    tmpValue.Add(tmp);
                }
            }
            return retData;
        }
    }
}
