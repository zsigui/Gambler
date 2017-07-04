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

        public string mid;

        public string a1;

        public string a2;

        public string a3;

        public string a4;

        public string a5;

        public string a6;

        public string a7;

        public string a8;

        public string a9;

        public string a10;

        public string a11;

        public string a12;

        public string a13;

        public string a14;

        public string a15;

        public string a16;

        public string a17;

        public string a18;

        public string a19;

        public string a20;

        public string a21;

        public string a22;

        public string a23;

        public string a24;

        public string a25;

        public string a26;

        public string a27;

        public string a28;

        public string a29;

        public string a30;

        public string a31;

        public string a32;

        public string a33;

        public string a34;

        public string a35;

        public string a36;

        public string a37;

        public string a38;

        public string a39;

        public string a40;

        public string a41;

        public string a42;

        public string a43;

        public string a44;

        public string a45;
    }

}
