using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Model.XPJ
{
    [JsonObject(MemberSerialization.OptOut)]
    public class RespData : RespBase
    {
        
        public int pageCount;
        public DataGameCount gameCount;
        /**
         * 按games每一项里值的意义请看 header，一一对应关系 
         */
        public List<List<string>> games;
        /**
         * 
         * [["gid","home","guest","homeCode","guestCode","league","openTime","live","matchId",
         * "scoreC","scoreH","retimeset","lastestscoreC","lastestscoreH","redcardH","redcardC",
         * "ior_MH","ior_MC","ior_MN","ior_RH","CON_RH","ior_RC","CON_RC","ior_OUH","CON_OUH",
         * "ior_OUC","CON_OUC","ior_EOO","ior_EOE","ior_HMH","ior_HMC","ior_HMN","ior_HRH","CON_HRH",
         * "ior_HRC","CON_HRC","ior_HOUH","CON_HOUH","ior_HOUC","CON_HOUC"]
         * <br />
         * 按顺序值类型说明: <br />
         * 盘口id、主队、客队、主队编码、客队编码、赛事名称、时间戳(ms)、是否开盘、赛事ID、
         * 客队滚球得分、主队滚球得分、半场^已进行时间(1H^06，表示上半场6分)、最新客队滚球得分、最新主队滚球得分、[]、[]、
         * 主队独赢赔率、客队独赢赔率、平局赔率、主队全场-让球赔率、说明（针对上一项补充，下同）、客队全场-让球赔率、说明、全场-大小大于赔率、说明、
         * 全场-大小小于赔率、说明、单数球赔率、双数球赔率、主队半场-独赢赔率、客队半场-独赢赔率、和局赔率、主队半场-让球赔率、说明、
         * 客队半场-让球赔率、说明、半场-大小大于赔率、说明、半场大小小于赔率、说明
         * <br />
         * 
         * <br />
         * ior 赔率    con 对赔率的说明 （比如如果是大小，表示大于或小于某个值，对于让球 - 开头表示客让主） <br />
         *  末尾的字符含义：C 代表 客队（大小则代表小） H 代表主队（大小代表大） N 代表和局 <br />
         *  头部的字符 H 有则表示半场，无则表示全场 <br />
         *  M 独赢  R 让球  OU 大小  EO 单双-但  EOE 单双-双 <br />
         * 对于篮球，还有 nowSession (OT加时、HT半场、H1上半场、H2下半场、Qn第n节)，lastTime (剩余时间)，
         * lastGoal (上次得分的队伍，H主队，A客队)， OUHO (主队得分大于特定值) ，OUHU(主队得分小于特定值) <br />
         * <br />
         * <br />
         * 
         * P.S. 
         * <br />
         * 几种赛事情况介绍： <br />
         * (1) 独赢 & 让球 & 大小 & 单/双 ，介绍如上
         * (2) 波胆（预测足球塞比分），为 ior_HxCy，x,y分别表示主客场得分， ior_OVH 表示其它得分情况 <br />
         * (3) 总入球，为 ior_Txy，表示进球数为 [x, y]，iorOVER 表示其他之上
         * (4) 半场 / 全场，为 ior_Fxy，x表示上半场，y表示下半场，取值都为 H、C、N
         * (5) 综合过关，跟 (1) 类似
         * (6) 冠军 ( 得后续补充 )
         */
        public List<string> headers;
    }
}
