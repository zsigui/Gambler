using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Module.X469.Model
{
    /// <summary>
    /// a0: 分类头忽略，a1: 联赛ID，a2：主队，a3：客队，a4：主队ID，a5：客队ID，a6：，a7：，a8：，a9：，a10：全场让球数，
    /// a11：全场让球-主赔率，a12：全场让球-客赔率，a13：全场大小数，a14：全场大-赔率，a15：全场小-赔率，a16：主队进球数，a17：客队进球数，
    /// a18：日期，a19：比赛已进行时间，a20：全场让球-主让客，a21：全场让球-客让主，a22：全场大小-大，a23：全场大小-小，a24：，
    /// a25：，a26：联赛名称，a27：，a28：，a29：，a30：上半让球数，a31：上半让球-主赔率，a32：上半让球-客赔率，a33：上半大小数，
    /// a34：上半大-赔率，a35：上半小-赔率，a36：上半让球-主让客，a37：上半让球-客让主，a38：上半大小-大，a39：上半大小-小，a40：，a41：，a42：，a43：，a44：
    /// mid：盘口ID
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class X469OddItem
    {
        /// <summary>
        /// 盘口ID
        /// </summary>
        public string mid;
        
        /// <summary>
        /// 联赛ID
        /// </summary>
        public string a1;

        /// <summary>
        /// 主队
        /// </summary>
        public string a2;

        /// <summary>
        /// 客队
        /// </summary>
        public string a3;

        /// <summary>
        /// 主队ID
        /// </summary>
        public string a4;

        /// <summary>
        /// 客队ID
        /// </summary>
        public string a5;

        public string a6;

        public string a7;

        public string a8;

        public string a9;

        /// <summary>
        /// 全场让球数
        /// </summary>
        public string a10;

        /// <summary>
        /// 全场让球-主 赔率
        /// </summary>
        public string a11;

        /// <summary>
        /// 全场让球-客 赔率
        /// </summary>
        public string a12;

        /// <summary>
        /// 全场大小数
        /// </summary>
        public string a13;

        /// <summary>
        /// 全场大小-大 赔率
        /// </summary>
        public string a14;

        /// <summary>
        /// 全场大小-小 赔率
        /// </summary>
        public string a15;

        /// <summary>
        /// 主队进球数
        /// </summary>
        public string a16;

        /// <summary>
        /// 客队进球数
        /// </summary>
        public string a17;

        /// <summary>
        /// 日期
        /// </summary>
        public string a18;

        /// <summary>
        /// 比赛已进行时间
        /// </summary>
        public string a19;

        /// <summary>
        /// 全场让球-主让客（值跟a10一致）
        /// </summary>
        public string a20;

        /// <summary>
        /// 全场让球-客让主
        /// </summary>
        public string a21;

        /// <summary>
        /// 全场大小-大（值跟a13一致）
        /// </summary>
        public string a22;

        /// <summary>
        /// 全场大小-小
        /// </summary>
        public string a23;

        public string a24;

        public string a25;

        /// <summary>
        /// 联赛名称
        /// </summary>
        public string a26;

        public string a27;

        public string a28;

        public string a29;

        /// <summary>
        /// 上半场让球数
        /// </summary>
        public string a30;

        /// <summary>
        /// 上半场让球-主赔率
        /// </summary>
        public string a31;

        /// <summary>
        /// 上半场让球-客赔率
        /// </summary>
        public string a32;

        /// <summary>
        /// 上半场大小数
        /// </summary>
        public string a33;

        /// <summary>
        /// 上半场大小-大 赔率
        /// </summary>
        public string a34;

        /// <summary>
        /// 上半场大小-小 赔率
        /// </summary>
        public string a35;

        /// <summary>
        /// 上半场让球-主让客
        /// </summary>
        public string a36;

        /// <summary>
        /// 上半场让球-客让主
        /// </summary>
        public string a37;

        /// <summary>
        /// 上半场大小-大
        /// </summary>
        public string a38;

        /// <summary>
        /// 上半场大小-小
        /// </summary>
        public string a39;

        public string a40;

        public string a41;

        public string a42;

        public string a43;

        public string a44;
    }

}
