using Gambler.Model.XPJ;
using Gambler.Utils;
using Gambler.XPJ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Module.XPJ.Model
{
    public class XPJDataParser
    {

        public static Dictionary<string, List<XPJOddData>> TransformRespDataToXPJOddDataDict(RespData resp, out List<XPJOddData> sourceData)
        {
            if (resp == null || resp.games == null || resp.games.Count == 0
                || resp.headers == null || resp.headers.Count == 0)
            {
                sourceData = null;
                return null;
            }
            List<List<string>> games = resp.games;
            Dictionary<string, List<XPJOddData>> retData = new Dictionary<string, List<XPJOddData>>();
            List<XPJOddData> tmpValue;
            XPJOddData tmp;
            sourceData = new List<XPJOddData>(games.Count);
            foreach (List<string> g in games)
            {
                tmp = TransformListToXPJOddData(g, resp.headers);
                if (tmp != null)
                {
                    if (!retData.TryGetValue(tmp.league, out tmpValue))
                    {
                        tmpValue = new List<XPJOddData>();
                        retData.Add(tmp.league, tmpValue);
                    }
                    sourceData.Add(tmp);
                    tmpValue.Add(tmp);
                }
            }
            return retData;
        }

        public static List<XPJOddData> TransformRespDataToXPJOddDataList(RespData resp)
        {
            if (resp == null || resp.games == null || resp.games.Count == 0
                || resp.headers == null || resp.headers.Count == 0)
                return null;
            List<List<string>> games = resp.games;
            List<XPJOddData> retData = new List<XPJOddData>(games.Count);
            XPJOddData tmp;
            foreach (List<string> g in games)
            {
                tmp = TransformListToXPJOddData(g, resp.headers);
                if (tmp != null)
                {
                    retData.Add(tmp);
                }
            }
            
            return retData;
        }
        
        public static XPJOddData TransformListToXPJOddData(List<string> values, List<string> keys)
        {
            if (values == null)
                return null;
            XPJOddData data = new XPJOddData();
            data.gid = ValueParse.ParseInt(values[keys.IndexOf("gid")]);
            data.home = values[keys.IndexOf("home")];
            data.guest = values[keys.IndexOf("guest")];
            data.league = values[keys.IndexOf("league")];
            data.openTime = ValueParse.ParseLong(values[keys.IndexOf("openTime")]);
            data.live = ValueParse.ParseBoolean(values[keys.IndexOf("live")]);
            data.score = values[keys.IndexOf("scoreH")] + " : " + values[keys.IndexOf("scoreC")];
            data.retimeset = values[keys.IndexOf("retimeset")];
            // 全场
            data.ior_MH = ValueParse.ParseFloat(values[keys.IndexOf("ior_MH")]);
            data.ior_MC = ValueParse.ParseFloat(values[keys.IndexOf("ior_MC")]);
            data.ior_MN = ValueParse.ParseFloat(values[keys.IndexOf("ior_MN")]);
            float[] ior = XPJRatioHelper.GetIOR(values[keys.IndexOf("ior_RH")], values[keys.IndexOf("ior_RC")]);
            data.ior_RH = ior[0];
            data.ior_RC = ior[1];
            data.CON_RH = values[keys.IndexOf("CON_RH")];
            ior = XPJRatioHelper.GetIOR(values[keys.IndexOf("ior_OUH")], values[keys.IndexOf("ior_OUC")]);
            data.ior_OUH = ior[0];
            data.ior_OUC = ior[1];
            data.CON_OUH = values[keys.IndexOf("CON_OUH")];
            ior = XPJRatioHelper.GetIORForEO(values[keys.IndexOf("ior_EOO")], values[keys.IndexOf("ior_EOE")]);
            data.ior_EOO = ior[0];
            data.ior_EOE = ior[1];
            // 半场
            data.ior_HMH = ValueParse.ParseFloat(values[keys.IndexOf("ior_HMH")]);
            data.ior_HMC = ValueParse.ParseFloat(values[keys.IndexOf("ior_HMC")]);
            data.ior_HMN = ValueParse.ParseFloat(values[keys.IndexOf("ior_HMN")]);
            ior = XPJRatioHelper.GetIOR(values[keys.IndexOf("ior_HRH")], values[keys.IndexOf("ior_HRC")]);
            data.ior_HRH = ior[0];
            data.ior_HRC = ior[1];
            data.CON_HRH = values[keys.IndexOf("CON_HRH")];
            ior = XPJRatioHelper.GetIOR(values[keys.IndexOf("ior_HOUH")], values[keys.IndexOf("ior_HOUC")]);
            data.ior_HOUH = ior[0];
            data.ior_HOUC = ior[1];
            data.CON_HOUH = values[keys.IndexOf("CON_HOUH")];
            return data;
        }
    }
}
