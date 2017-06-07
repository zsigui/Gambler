using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Module.XPJ.Model
{
    public class XPJOddData
    {
        public int gid;

        public string home;

        public string guest;

        public string league;

        public long openTime;

        public bool live;

        // 由 ‘scoreH : scoreC’ 拼合
        public string scoreH;

        public string scoreC;

        /// <summary>
        /// 已经进行的时间
        /// </summary>
        public string retimeset;

        /// <summary>
        /// 主队独赢赔率
        /// </summary>
        public float ior_MH;

        /// <summary>
        /// 客队独赢赔率
        /// </summary>
        public float ior_MC;

        /// <summary>
        /// 全场-平局赔率
        /// </summary>
        public float ior_MN;

        /// <summary>
        /// 全场-主队让球赔率
        /// </summary>
        public float ior_RH;

        /// <summary>
        /// 全场-客队让球赔率
        /// </summary>
        public float ior_RC;

        /// <summary>
        /// 同CON_RC，让球数说明，首字符'-'表示客让主，无则表示主让客
        /// </summary>
        public string CON_RH;

        /// <summary>
        /// 全场-大小-大于赔率
        /// </summary>
        public float ior_OUH;

        /// <summary>
        /// 全场-大小-小于赔率
        /// </summary>
        public float ior_OUC;

        /// <summary>
        /// 同CON_OUC，大于小于某值说明
        /// </summary>
        public string CON_OUH;

        /// <summary>
        /// 半场-主队独赢赔率
        /// </summary>
        public float ior_HMH;

        /// <summary>
        /// 半场-客队独赢赔率
        /// </summary>
        public float ior_HMC;

        /// <summary>
        /// 半场-平局赔率
        /// </summary>
        public float ior_HMN;

        /// <summary>
        /// 半场-主队让球赔率
        /// </summary>
        public float ior_HRH;

        /// <summary>
        /// 半场-客队让球赔率
        /// </summary>
        public float ior_HRC;

        /// <summary>
        /// 半场让球数说明，'-'表示客让主
        /// </summary>
        public string CON_HRH;

        /// <summary>
        /// 半场-大小-大于赔率
        /// </summary>
        public float ior_HOUH;

        /// <summary>
        /// 半场-大小-小于赔率
        /// </summary>
        public float ior_HOUC;

        /// <summary>
        /// 半场大于小于某个值说明
        /// </summary>
        public string CON_HOUH;

        /// <summary>
        /// 单双-单赔率
        /// </summary>
        public float ior_EOO;

        /// <summary>
        /// 单双-双赔率
        /// </summary>
        public float ior_EOE;
    }
}
