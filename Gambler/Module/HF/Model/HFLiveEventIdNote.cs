using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gambler.Module.HF.Model
{
    /*
    {
        START_RT1:'0', STOP_RT1:'1', START_RT2:'2', STOP_RT2:'3',
        START_OT1:'4', STOP_OT1:'5', START_OT2:'6', STOP_OT2:'7', START_PEN:'8', STOP_PEN:'9',
        RT1_T1:'10', RT1_T2:'11', RT2_T1:'12', RT2_T2:'13', OT1_T1:'14', OT1_T2:'15', OT2_T1:'16', OT2_T2:'17', PEN_T1:'18', PEN_T2:'19',STOP_GAME:'20',CONF_STAT:'280',
        SAFE:'128', DANGER:'129', INJURY:'132', P_CO:'133',P_LU:'134',NAS:'135',SHAKE_HANDS:'136',FLIP_COIN:'137',SILENT:'138',
        PRIZE:'139', PHOTO:'140', GAME_START:'141',PENALTY_MISS:'142', POSSIBLE_RED_CAR:'143', POSSIBLE_PENALTY:'144',
        NO_RC:'145',NO_PENALTY:'146',RETAKE:'147',RESTART:'148',N_PENALTY_SCORER:'150',POSSIBLE_FREE_KICK:'207',
        NO_FREE_KICK:'208', REFEREE_BALL:'209', EXTRA_TIME:'260', JERSEY_CHANGE:'524',HALF_MIN_NO_UPDATE:'999',
        AT1:'1024',CR1:'1025',DAT1:'1026',DFK1:'1027',FK1:'1028',GOAL1:'1029',CGOAL1:'1030',PEN1:'1031', RC1:'1032',SH1:'1033',
        YC1:'1034',SHG1:'1039',SHB1:'1040',SHW1:'1041',F1:'1042',O1:'1043',KO1:'1044',YRC1:'1045',
        CYC_RC1:'1046', CRC1:'1047',CYC1:'1048',CPEN1:'1049',CCR1:'1050',SAFE1:'1051', DANGER1:'1052',GK1:'1053',TI1:'1054',SUB1:'1055',
        DSH1:'1056',SAVE1:'1057',BLOCKED1:'1058',RPEN1:'1059',MPEN1:'1060',PPEN1:'1062'
        AT2:'2048',CR2:'2049',DAT2:'2050',DFK2:'2051',FK2:'2052',GOAL2:'2053',CGOAL2:'2054',PEN2:'2055', RC2:'2056',SH2:'2057',
        YC2:'2058',SHG2:'2063',SHB2:'2064',SHW2:'2065',F2:'2066',O2:'2067',KO2:'2068',YRC2:'2069',
        CYC_RC2:'2070', CRC2:'2071',CYC2:'2072',CPEN2:'2073',CCR2:'2074',SAFE2:'2075', DANGER2:'2076',GK2:'2077',TI2:'2078',SUB2:'2079',
        DSH2: '2080', SAVE2: '2081', BLOCKED2: '2082', RPEN2: '2083', MPEN2: '2084',PPEN2:'2086'
        //GENERAL
        OFFSET: '9901', PITCH: '9902', PLAYERS: '9903', SETTING: '9904', STANDBY: '9909', WEATHER: '9947', WIND: '9948', STAT: '9950',
        STAT_US: '9951', STAT_OK: '9952', TEAM_BET: '9955', ATTN: '9991', CALL_LS: '9992', CALL_OK: '9993', CARD: '9994', COMM_LS: '9995', COMM_OK: '9996',
        COMMT:'9997', GS:'9998', NEU_GD:'9999',
        //TEAM
        SC11:'9905', SC12:'9906', SC21:'9907', SC22:'9908', TIATK1:'9941', TIATK2:'9942', TIDGR1:'9943', TIDGR2:'9944', TISAF1:'9945', TISAF2:'9946',
        GOAL_OWN1:'9961', GOAL_OWN2:'9962', GOAL_PEN1:'9963', GOAL_PEN2:'9964', GOAL_PENM1:'9965', GOAL_PENM2:'9966', KICK_RES1:'9967', KICK_RES2:'9968',
        CROSS1:'9971', CROSS2:'9972', FK_ATK1:'9981', FK_ATK2:'9982', FK_DGR1:'9983', FK_DGR2:'9984', FK_SAF1:'9985', FK_SAF2:'9986'
    } 
    */
    /// <summary>
    /// 主客队CID组成由 小于1000的数 + (1 主队 2 客队)， 如 可能点球是144，则主队可能点球为 1144
    /// </summary>
    public class HFLiveEventIdNote
    {
        /// <summary>
        /// 0
        /// </summary>
        public const string START_RT1 = "0";
        /// <summary>
        /// 1
        /// </summary>
        public const string STOP_RT1 = "1";
        /// <summary>
        /// 2
        /// </summary>
        public const string START_RT2 = "2";
        /// <summary>
        /// 3
        /// </summary>
        public const string STOP_RT2 = "3";
        /// <summary>
        /// 4
        /// </summary>
        public const string START_OT1 = "4";
        /// <summary>
        /// 5
        /// </summary>
        public const string STOP_OT1 = "5";
        /// <summary>
        /// 6
        /// </summary>
        public const string START_OT2 = "6";
        /// <summary>
        /// 7
        /// </summary>
        public const string STOP_OT2 = "7";
        /// <summary>
        /// 开始点球
        /// </summary>
        public const string START_PEN = "8";
        /// <summary>
        /// 停止点球
        /// </summary>
        public const string STOP_PEN = "9";
        /// <summary>
        /// 10
        /// </summary>
        public const string RT1_T1 = "10";
        /// <summary>
        /// 11
        /// </summary>
        public const string RT1_T2 = "11";
        /// <summary>
        /// 12
        /// </summary>
        public const string RT2_T1 = "12";
        /// <summary>
        /// 13
        /// </summary>
        public const string RT2_T2 = "13";
        /// <summary>
        /// 14
        /// </summary>
        public const string OT1_T1 = "14";
        /// <summary>
        /// 15
        /// </summary>
        public const string OT1_T2 = "15";
        /// <summary>
        /// 16
        /// </summary>
        public const string OT2_T1 = "16";
        /// <summary>
        /// 17
        /// </summary>
        public const string OT2_T2 = "17";
        /// <summary>
        /// 队伍1点球大战
        /// </summary>
        public const string PEN_T1 = "18";
        /// <summary>
        /// 队伍2点球大战
        /// </summary>
        public const string PEN_T2 = "19";
        /// <summary>
        /// 20
        /// </summary>
        public const string STOP_GAME = "20";
        /// <summary>
        /// 280
        /// </summary>
        public const string CONF_STAT = "280";
        /// <summary>
        /// 128
        /// </summary>
        public const string SAFE = "128";
        /// <summary>
        /// 危险进攻
        /// </summary>
        public const string DANGER = "129";
        /// <summary>
        /// 伤停
        /// </summary>
        public const string INJURY = "132";
        /// <summary>
        /// 133
        /// </summary>
        public const string P_CO = "133";
        /// <summary>
        /// 134
        /// </summary>
        public const string P_LU = "134";
        /// <summary>
        /// 135
        /// </summary>
        public const string NAS = "135";
        /// <summary>
        /// 136
        /// </summary>
        public const string SHAKE_HANDS = "136";
        /// <summary>
        /// 137
        /// </summary>
        public const string FLIP_COIN = "137";
        /// <summary>
        /// 138
        /// </summary>
        public const string SILENT = "138";
        /// <summary>
        /// 139
        /// </summary>
        public const string PRIZE = "139";
        /// <summary>
        /// 140
        /// </summary>
        public const string PHOTO = "140";
        /// <summary>
        /// 141
        /// </summary>
        public const string GAME_START = "141";
        /// <summary>
        /// 点球取消
        /// </summary>
        public const string PENALTY_MISS = "142";
        /// <summary>
        /// 可能红牌
        /// </summary>
        public const string POSSIBLE_RED_CAR = "143";
        /// <summary>
        /// 可能点球
        /// </summary>
        public const string POSSIBLE_PENALTY = "144";
        /// <summary>
        /// 没有红牌
        /// </summary>
        public const string NO_RC = "145";
        /// <summary>
        /// 无点球
        /// </summary>
        public const string NO_PENALTY = "146";
        /// <summary>
        /// 147
        /// </summary>
        public const string RETAKE = "147";
        /// <summary>
        /// 148
        /// </summary>
        public const string RESTART = "148";
        /// <summary>
        /// 无点球得分
        /// </summary>
        public const string N_PENALTY_SCORER = "150";
        /// <summary>
        /// 可能任意球
        /// </summary>
        public const string POSSIBLE_FRE= "207";
        /// <summary>
        /// 无任意球
        /// </summary>
        public const string NO_FREE_KICK = "208";
        /// <summary>
        /// 209
        /// </summary>
        public const string REFEREE_BALL = "209";
        /// <summary>
        /// 260
        /// </summary>
        public const string EXTRA_TIME = "260";
        /// <summary>
        /// 524
        /// </summary>
        public const string JERSEY_CHANGE = "524";
        /// <summary>
        /// 999
        /// </summary>
        public const string HALF_MIN_NO_UPDATE = "999";

        /// <summary>
        /// 进攻（主队）
        /// </summary>
        public const string AT1 = "1024";
        /// <summary>
        /// 角球（主队）
        /// </summary>
        public const string CR1 = "1025";
        /// <summary>
        /// 危险进攻（主队）
        /// </summary>
        public const string DAT1 = "1026";
        /// <summary>
        /// 危险任意球（主队）
        /// </summary>
        public const string DFK1 = "1027";
        /// <summary>
        /// 任意球（主队）
        /// </summary>
        public const string FK1 = "1028";
        /// <summary>
        /// 得分（主队）
        /// </summary>
        public const string GOAL1 = "1029";
        /// <summary>
        /// 取消得分（主队）
        /// </summary>
        public const string CGOAL1 = "1030";
        /// <summary>
        /// 点球（主队）
        /// </summary>
        public const string PEN1 = "1031";
        /// <summary>
        /// 红牌（主队）
        /// </summary>
        public const string RC1 = "1032";
        /// <summary>
        /// 射门（主队）
        /// </summary>
        public const string SH1 = "1033";
        /// <summary>
        /// 黄牌（主队）
        /// </summary>
        public const string YC1 = "1034";
        /// <summary>
        /// 射正（主队）
        /// </summary>
        public const string SHG1 = "1039";
        /// <summary>
        /// 射偏（主队）
        /// </summary>
        public const string SHB1 = "1040";
        /// <summary>
        /// 1041
        /// </summary>
        public const string SHW1 = "1041";
        /// <summary>
        /// 1042
        /// </summary>
        public const string F1 = "1042";
        /// <summary>
        /// 越位Offside（主队）
        /// </summary>
        public const string O1 = "1043";
        /// <summary>
        /// 1044
        /// </summary>
        public const string KO1 = "1044";
        /// <summary>
        /// 黄/红牌
        /// </summary>
        public const string YRC1 = "1045";
        /// <summary>
        /// 黄/红牌取消
        /// </summary>
        public const string CYC_RC1 = "1046";
        /// <summary>
        /// 1047
        /// </summary>
        public const string CRC1 = "1047";
        /// <summary>
        /// 1048
        /// </summary>
        public const string CYC1 = "1048";
        /// <summary>
        /// 点球取消（主队）
        /// </summary>
        public const string CPEN1 = "1049";
        /// <summary>
        /// 角球取消（主队）
        /// </summary>
        public const string CCR1 = "1050";
        /// <summary>
        /// 控球（主队）
        /// </summary>
        public const string SAFE1 = "1051";
        /// <summary>
        /// 危险进攻（主队）
        /// </summary>
        public const string DANGER1 = "1052";
        /// <summary>
        /// 门球（主队）
        /// </summary>
        public const string GK1 = "1053";
        /// <summary>
        /// 1054
        /// </summary>
        public const string TI1 = "1054";
        /// <summary>
        /// 换人（主队）
        /// </summary>
        public const string SUB1 = "1055";
        /// <summary>
        /// 1056
        /// </summary>
        public const string DSH1 = "1056";
        /// <summary>
        /// 1057
        /// </summary>
        public const string SAVE1 = "1057";
        /// <summary>
        /// 1058
        /// </summary>
        public const string BLOCKED1 = "1058";
        /// <summary>
        /// 1059
        /// </summary>
        public const string RPEN1 = "1059";
        /// <summary>
        /// 点球失误（主队）
        /// </summary>
        public const string MPEN1 = "1060";
        /// <summary>
        /// 可能点球（主队）
        /// </summary>
        public const string PPEN1 = "1062";
        /// <summary>
        /// 2048
        /// </summary>
        public const string AT2 = "2048";
        /// <summary>
        /// 2049
        /// </summary>
        public const string CR2 = "2049";
        /// <summary>
        /// 2050
        /// </summary>
        public const string DAT2 = "2050";
        /// <summary>
        /// 危险任意球（客队）
        /// </summary>
        public const string DFK2 = "2051";
        /// <summary>
        /// 任意球（客队）
        /// </summary>
        public const string FK2 = "2052";
        /// <summary>
        /// 得分（客队）
        /// </summary>
        public const string GOAL2 = "2053";
        /// <summary>
        /// 取消得分（主队）
        /// </summary>
        public const string CGOAL2 = "2054";
        /// <summary>
        /// 点球（客队）
        /// </summary>
        public const string PEN2 = "2055";
        /// <summary>
        /// 2056
        /// </summary>
        public const string RC2 = "2056";
        /// <summary>
        /// 2057
        /// </summary>
        public const string SH2 = "2057";
        /// <summary>
        /// 2058
        /// </summary>
        public const string YC2 = "2058";
        /// <summary>
        /// 射正（客队）
        /// </summary>
        public const string SHG2 = "2063";
        /// <summary>
        /// 射偏（客队）
        /// </summary>
        public const string SHB2 = "2064";
        /// <summary>
        /// 2065
        /// </summary>
        public const string SHW2 = "2065";
        /// <summary>
        /// 2066
        /// </summary>
        public const string F2 = "2066";
        /// <summary>
        /// 2067
        /// </summary>
        public const string O2 = "2067";
        /// <summary>
        /// 2068
        /// </summary>
        public const string KO2 = "2068";
        /// <summary>
        /// 红/黄牌（客队）
        /// </summary>
        public const string YRC2 = "2069";
        /// <summary>
        /// 取消红/黄牌（客队）
        /// </summary>
        public const string CYC_RC2 = "2070";
        /// <summary>
        /// 2071
        /// </summary>
        public const string CRC2 = "2071";
        /// <summary>
        /// 2072
        /// </summary>
        public const string CYC2 = "2072";
        /// <summary>
        /// 点球取消（客队）
        /// </summary>
        public const string CPEN2 = "2073";
        /// <summary>
        /// 角球取消（客队）
        /// </summary>
        public const string CCR2 = "2074";
        /// <summary>
        /// 控球（客队）
        /// </summary>
        public const string SAFE2 = "2075";
        /// <summary>
        /// 危险进攻（客队）
        /// </summary>
        public const string DANGER2 = "2076";
        /// <summary>
        /// 门球（客队）
        /// </summary>
        public const string GK2 = "2077";
        /// <summary>
        /// 2078
        /// </summary>
        public const string TI2 = "2078";
        /// <summary>
        /// 换人（客队）
        /// </summary>
        public const string SUB2 = "2079";
        /// <summary>
        /// 2080
        /// </summary>
        public const string DSH2 = "2080";
        /// <summary>
        /// 控球（客队）
        /// </summary>
        public const string SAVE2 = "2081";
        /// <summary>
        /// 2082
        /// </summary>
        public const string BLOCKED2 = "2082";
        /// <summary>
        /// 2083
        /// </summary>
        public const string RPEN2 = "2083";
        /// <summary>
        /// 点球失误（客队）
        /// </summary>
        public const string MPEN2 = "2084";
        /// <summary>
        /// 可能点球（客队）
        /// </summary>
        public const string PPEN2 = "2086";
        /// <summary>
        /// 9901
        /// </summary>
        public const string OFFSET = "9901";
        /// <summary>
        /// 9902
        /// </summary>
        public const string PITCH = "9902";
        /// <summary>
        /// 9903
        /// </summary>
        public const string PLAYERS = "9903";
        /// <summary>
        /// 9904
        /// </summary>
        public const string SETTING = "9904";
        /// <summary>
        /// 9909
        /// </summary>
        public const string STANDBY = "9909";
        /// <summary>
        /// 9947
        /// </summary>
        public const string WEATHER = "9947";
        /// <summary>
        /// 9948
        /// </summary>
        public const string WIND = "9948";
        /// <summary>
        /// 9950
        /// </summary>
        public const string STAT = "9950";
        /// <summary>
        /// 9951
        /// </summary>
        public const string STAT_US = "9951";
        /// <summary>
        /// 9952
        /// </summary>
        public const string STAT_OK = "9952";
        /// <summary>
        /// 9955
        /// </summary>
        public const string TEAM_BET = "9955";
        /// <summary>
        /// 9991
        /// </summary>
        public const string ATTN = "9991";
        /// <summary>
        /// 9992
        /// </summary>
        public const string CALL_LS = "9992";
        /// <summary>
        /// 9993
        /// </summary>
        public const string CALL_OK = "9993";
        /// <summary>
        /// 9994
        /// </summary>
        public const string CARD = "9994";
        /// <summary>
        /// 9995
        /// </summary>
        public const string COMM_LS = "9995";
        /// <summary>
        /// 9996
        /// </summary>
        public const string COMM_OK = "9996";
        /// <summary>
        /// 9997
        /// </summary>
        public const string COMMT = "9997";
        /// <summary>
        /// 9998
        /// </summary>
        public const string GS = "9998";
        /// <summary>
        /// 9999
        /// </summary>
        public const string NEU_GD = "9999";
        /// <summary>
        /// 9905
        /// </summary>
        public const string SC11 = "9905";
        /// <summary>
        /// 9906
        /// </summary>
        public const string SC12 = "9906";
        /// <summary>
        /// 9907
        /// </summary>
        public const string SC21 = "9907";
        /// <summary>
        /// 9908
        /// </summary>
        public const string SC22 = "9908";
        /// <summary>
        /// 9941
        /// </summary>
        public const string TIATK1 = "9941";
        /// <summary>
        /// 9942
        /// </summary>
        public const string TIATK2 = "9942";
        /// <summary>
        /// 9943
        /// </summary>
        public const string TIDGR1 = "9943";
        /// <summary>
        /// 9944
        /// </summary>
        public const string TIDGR2 = "9944";
        /// <summary>
        /// 9945
        /// </summary>
        public const string TISAF1 = "9945";
        /// <summary>
        /// 9946
        /// </summary>
        public const string TISAF2 = "9946";
        /// <summary>
        /// 9961
        /// </summary>
        public const string GOAL_OWN1 = "9961";
        /// <summary>
        /// 9962
        /// </summary>
        public const string GOAL_OWN2 = "9962";
        /// <summary>
        /// 9963
        /// </summary>
        public const string GOAL_PEN1 = "9963";
        /// <summary>
        /// 9964
        /// </summary>
        public const string GOAL_PEN2 = "9964";
        /// <summary>
        /// 9965
        /// </summary>
        public const string GOAL_PENM1 = "9965";
        /// <summary>
        /// 9966
        /// </summary>
        public const string GOAL_PENM2 = "9966";
        /// <summary>
        /// 9967
        /// </summary>
        public const string KICK_RES1 = "9967";
        /// <summary>
        /// 9968
        /// </summary>
        public const string KICK_RES2 = "9968";
        /// <summary>
        /// 9971
        /// </summary>
        public const string CROSS1 = "9971";
        /// <summary>
        /// 9972
        /// </summary>
        public const string CROSS2 = "9972";
        /// <summary>
        /// 9981
        /// </summary>
        public const string FK_ATK1 = "9981";
        /// <summary>
        /// 9982
        /// </summary>
        public const string FK_ATK2 = "9982";
        /// <summary>
        /// 9983
        /// </summary>
        public const string FK_DGR1 = "9983";
        /// <summary>
        /// 9984
        /// </summary>
        public const string FK_DGR2 = "9984";
        /// <summary>
        /// 9985
        /// </summary>
        public const string FK_SAF1 = "9985";
        /// <summary>
        /// 9986
        /// </summary>
        public const string FK_SAF2 = "9986";
    }
}
