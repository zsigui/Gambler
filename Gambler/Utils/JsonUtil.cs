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
            return JsonConvert.SerializeObject(obj, Formatting.None, DefaultSetting());
        }

        public static T fromJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, DefaultSetting());
        }
    }
}
