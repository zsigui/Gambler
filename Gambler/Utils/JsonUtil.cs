using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Utils
{
    public class JsonUtil
    {
        private static object lockObj = new object();
        private static JsonSerializerSettings sSettings = null;

        public static JsonSerializerSettings DefaultSetting()
        {
            if (sSettings == null)
            {
                lock (lockObj) {
                    if (sSettings == null)
                    {
                        sSettings = new JsonSerializerSettings();
                        sSettings.NullValueHandling = NullValueHandling.Ignore;
                    }
                }
            }
            return sSettings;
        }

        public static string toJson<T>(T obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj, Formatting.None, DefaultSetting());
            }
            catch (Exception e)
            {
                LogUtil.Write(e);
                return "";
            }
        }

        public static T fromJson<T>(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json, DefaultSetting());
            }
            catch (Exception e)
            {
                Console.WriteLine(json);
                LogUtil.Write(e);
                return default(T);
            }
        }
    }
}
