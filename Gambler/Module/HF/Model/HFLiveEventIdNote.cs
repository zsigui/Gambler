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
        DSH1:'1056',SAVE1:'1057',BLOCKED1:'1058',RPEN1:'1059',MPEN1:'1060',
        AT2:'2048',CR2:'2049',DAT2:'2050',DFK2:'2051',FK2:'2052',GOAL2:'2053',CGOAL2:'2054',PEN2:'2055', RC2:'2056',SH2:'2057',
        YC2:'2058',SHG2:'2063',SHB2:'2064',SHW2:'2065',F2:'2066',O2:'2067',KO2:'2068',YRC2:'2069',
        CYC_RC2:'2070', CRC2:'2071',CYC2:'2072',CPEN2:'2073',CCR2:'2074',SAFE2:'2075', DANGER2:'2076',GK2:'2077',TI2:'2078',SUB2:'2079',
        DSH2: '2080', SAVE2: '2081', BLOCKED2: '2082', RPEN2: '2083', MPEN2: '2084',
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
        public string START_RT1 = "0";
        /// <summary>
        /// 1
        /// </summary>
        public string STOP_RT1 = "1";
        /// <summary>
        /// 2
        /// </summary>
        public string START_RT2 = "2";
        /// <summary>
        /// 3
        /// </summary>
        public string STOP_RT2 = "3";
        /// <summary>
        /// 4
        /// </summary>
        public string START_OT1 = "4";
        /// <summary>
        /// 5
        /// </summary>
        public string STOP_OT1 = "5";
        /// <summary>
        /// 6
        /// </summary>
        public string START_OT2 = "6";
        /// <summary>
        /// 7
        /// </summary>
        public string STOP_OT2 = "7";
        /// <summary>
        /// 8
        /// </summary>
        public string START_PEN = "8";
        /// <summary>
        /// 9
        /// </summary>
        public string STOP_PEN = "9";
        /// <summary>
        /// 10
        /// </summary>
        public string RT1_T1 = "10";
        /// <summary>
        /// 11
        /// </summary>
        public string RT1_T2 = "11";
        /// <summary>
        /// 12
        /// </summary>
        public string RT2_T1 = "12";
        /// <summary>
        /// 13
        /// </summary>
        public string RT2_T2 = "13";
        /// <summary>
        /// 14
        /// </summary>
        public string OT1_T1 = "14";
        /// <summary>
        /// 15
        /// </summary>
        public string OT1_T2 = "15";
        /// <summary>
        /// 16
        /// </summary>
        public string OT2_T1 = "16";
        /// <summary>
        /// 17
        /// </summary>
        public string OT2_T2 = "17";
        /// <summary>
        /// 队伍1点球大战
        /// </summary>
        public string PEN_T1 = "18";
        /// <summary>
        /// 队伍2点球大战
        /// </summary>
        public string PEN_T2 = "19";
        /// <summary>
        /// 20
        /// </summary>
        public string STOP_GAME = "20";
        /// <summary>
        /// 280
        /// </summary>
        public string CONF_STAT = "280";
        /// <summary>
        /// 128
        /// </summary>
        public string SAFE = "128";
        /// <summary>
        /// 危险进攻
        /// </summary>
        public string DANGER = "129";
        /// <summary>
        /// 伤停
        /// </summary>
        public string INJURY = "132";
        /// <summary>
        /// 133
        /// </summary>
        public string P_CO = "133";
        /// <summary>
        /// 134
        /// </summary>
        public string P_LU = "134";
        /// <summary>
        /// 135
        /// </summary>
        public string NAS = "135";
        /// <summary>
        /// 136
        /// </summary>
        public string SHAKE_HANDS = "136";
        /// <summary>
        /// 137
        /// </summary>
        public string FLIP_COIN = "137";
        /// <summary>
        /// 138
        /// </summary>
        public string SILENT = "138";
        /// <summary>
        /// 139
        /// </summary>
        public string PRIZE = "139";
        /// <summary>
        /// 140
        /// </summary>
        public string PHOTO = "140";
        /// <summary>
        /// 141
        /// </summary>
        public string GAME_START = "141";
        /// <summary>
        /// 点球取消
        /// </summary>
        public string PENALTY_MISS = "142";
        /// <summary>
        /// 可能红牌
        /// </summary>
        public string POSSIBLE_RED_CAR = "143";
        /// <summary>
        /// 可能点球
        /// </summary>
        public string POSSIBLE_PENALTY = "144";
        /// <summary>
        /// 没有红牌
        /// </summary>
        public string NO_RC = "145";
        /// <summary>
        /// 无点球
        /// </summary>
        public string NO_PENALTY = "146";
        /// <summary>
        /// 147
        /// </summary>
        public string RETAKE = "147";
        /// <summary>
        /// 148
        /// </summary>
        public string RESTART = "148";
        /// <summary>
        /// 无点球得分
        /// </summary>
        public string N_PENALTY_SCORER = "150";
        /// <summary>
        /// 可能任意球
        /// </summary>
        public string POSSIBLE_FRE= "207";
        /// <summary>
        /// 无任意球
        /// </summary>
        public string NO_FREE_KICK = "208";
        /// <summary>
        /// 209
        /// </summary>
        public string REFEREE_BALL = "209";
        /// <summary>
        /// 260
        /// </summary>
        public string EXTRA_TIME = "260";
        /// <summary>
        /// 524
        /// </summary>
        public string JERSEY_CHANGE = "524";
        /// <summary>
        /// 999
        /// </summary>
        public string HALF_MIN_NO_UPDATE = "999";

        /// <summary>
        /// 进攻（主队）
        /// </summary>
        public string AT1 = "1024";
        /// <summary>
        /// 角球（主队）
        /// </summary>
        public string CR1 = "1025";
        /// <summary>
        /// 危险进攻（主队）
        /// </summary>
        public string DAT1 = "1026";
        /// <summary>
        /// 危险任意球（主队）
        /// </summary>
        public string DFK1 = "1027";
        /// <summary>
        /// 任意球（主队）
        /// </summary>
        public string FK1 = "1028";
        /// <summary>
        /// 得分（主队）
        /// </summary>
        public string GOAL1 = "1029";
        /// <summary>
        /// 取消得分（主队）
        /// </summary>
        public string CGOAL1 = "1030";
        /// <summary>
        /// 点球（主队）
        /// </summary>
        public string PEN1 = "1031";
        /// <summary>
        /// 红牌（主队）
        /// </summary>
        public string RC1 = "1032";
        /// <summary>
        /// 射门（主队）
        /// </summary>
        public string SH1 = "1033";
        /// <summary>
        /// 黄牌（主队）
        /// </summary>
        public string YC1 = "1034";
        /// <summary>
        /// 射正（主队）
        /// </summary>
        public string SHG1 = "1039";
        /// <summary>
        /// 射偏（主队）
        /// </summary>
        public string SHB1 = "1040";
        /// <summary>
        /// 1041
        /// </summary>
        public string SHW1 = "";
        /// <summary>
        /// 1042
        /// </summary>
        public string F1 = "";
        /// <summary>
        /// 1043
        /// </summary>
        public string O1 = "";
        /// <summary>
        /// 1044
        /// </summary>
        public string KO1 = "";
        /// <summary>
        /// 黄/红牌
        /// </summary>
        public string YRC1 = "1045";
        /// <summary>
        /// 黄/红牌取消
        /// </summary>
        public string CYC_RC1 = "1046";
        /// <summary>
        /// 1047
        /// </summary>
        public string CRC1 = "";
        /// <summary>
        /// 1048
        /// </summary>
        public string CYC1 = "";
        /// <summary>
        /// 点球取消（主队）
        /// </summary>
        public string CPEN1 = "1049";
        /// <summary>
        /// 角球取消（主队）
        /// </summary>
        public string CCR1 = "1050";
        /// <summary>
        /// 控球（主队）
        /// </summary>
        public string SAFE1 = "1051";
        /// <summary>
        /// 危险进攻（主队）
        /// </summary>
        public string DANGER1 = "1052";
        /// <summary>
        /// 门球（主队）
        /// </summary>
        public string GK1 = "1053";
        /// <summary>
        /// 1054
        /// </summary>
        public string TI1 = "";
        /// <summary>
        /// 换人（主队）
        /// </summary>
        public string SUB1 = "1055";
        /// <summary>
        /// 1056
        /// </summary>
        public string DSH1 = "";
        /// <summary>
        /// 1057
        /// </summary>
        public string SAVE1 = "";
        /// <summary>
        /// 1058
        /// </summary>
        public string BLOCKED1 = "";
        /// <summary>
        /// 1059
        /// </summary>
        public string RPEN1 = "";
        /// <summary>
        /// 点球失误（主队）
        /// </summary>
        public string MPEN1 = "1060";
        /// <summary>
        /// 2048
        /// </summary>
        public string AT2 = "";
        /// <summary>
        /// 2049
        /// </summary>
        public string CR2 = "";
        /// <summary>
        /// 2050
        /// </summary>
        public string DAT2 = "";
        /// <summary>
        /// 危险任意球（客队）
        /// </summary>
        public string DFK2 = "2051";
        /// <summary>
        /// 任意球（客队）
        /// </summary>
        public string FK2 = "2052";
        /// <summary>
        /// 得分（客队）
        /// </summary>
        public string GOAL2 = "2053";
        /// <summary>
        /// 取消得分（主队）
        /// </summary>
        public string CGOAL2 = "2054";
        /// <summary>
        /// 点球（客队）
        /// </summary>
        public string PEN2 = "2055";
        /// <summary>
        /// 2056
        /// </summary>
        public string RC2 = "";
        /// <summary>
        /// 2057
        /// </summary>
        public string SH2 = "";
        /// <summary>
        /// 2058
        /// </summary>
        public string YC2 = "";
        /// <summary>
        /// 射正（客队）
        /// </summary>
        public string SHG2 = "2063";
        /// <summary>
        /// 射偏（客队）
        /// </summary>
        public string SHB2 = "2064";
        /// <summary>
        /// 2065
        /// </summary>
        public string SHW2 = "";
        /// <summary>
        /// 2066
        /// </summary>
        public string F2 = "";
        /// <summary>
        /// 2067
        /// </summary>
        public string O2 = "2067";
        /// <summary>
        /// 2068
        /// </summary>
        public string KO2 = "2068";
        /// <summary>
        /// 红/黄牌（客队）
        /// </summary>
        public string YRC2 = "2069";
        /// <summary>
        /// 取消红/黄牌（客队）
        /// </summary>
        public string CYC_RC2 = "2070";
        /// <summary>
        /// 2071
        /// </summary>
        public string CRC2 = "2071";
        /// <summary>
        /// 2072
        /// </summary>
        public string CYC2 = "2072";
        /// <summary>
        /// 点球取消（客队）
        /// </summary>
        public string CPEN2 = "2073";
        /// <summary>
        /// 角球取消（客队）
        /// </summary>
        public string CCR2 = "2074";
        /// <summary>
        /// 控球（客队）
        /// </summary>
        public string SAFE2 = "2075";
        /// <summary>
        /// 危险进攻（客队）
        /// </summary>
        public string DANGER2 = "2076";
        /// <summary>
        /// 门球（客队）
        /// </summary>
        public string GK2 = "2077";
        /// <summary>
        /// 2078
        /// </summary>
        public string TI2 = "2078";
        /// <summary>
        /// 换人（客队）
        /// </summary>
        public string SUB2 = "2079";
        /// <summary>
        /// 2080
        /// </summary>
        public string DSH2 = "2080";
        /// <summary>
        /// 控球（客队）
        /// </summary>
        public string SAVE2 = "2081";
        /// <summary>
        /// 2082
        /// </summary>
        public string BLOCKED2 = "";
        /// <summary>
        /// 2083
        /// </summary>
        public string RPEN2 = "";
        /// <summary>
        /// 点球失误（客队）
        /// </summary>
        public string MPEN2 = "2084";
        /// <summary>
        /// 9901
        /// </summary>
        public string OFFSET = "";
        /// <summary>
        /// 9902
        /// </summary>
        public string PITCH = "";
        /// <summary>
        /// 9903
        /// </summary>
        public string PLAYERS = "";
        /// <summary>
        /// 9904
        /// </summary>
        public string SETTING = "";
        /// <summary>
        /// 9909
        /// </summary>
        public string STANDBY = "";
        /// <summary>
        /// 9947
        /// </summary>
        public string WEATHER = "";
        /// <summary>
        /// 9948
        /// </summary>
        public string WIND = "";
        /// <summary>
        /// 9950
        /// </summary>
        public string STAT = "";
        /// <summary>
        /// 9951
        /// </summary>
        public string STAT_US = "";
        /// <summary>
        /// 9952
        /// </summary>
        public string STAT_OK = "";
        /// <summary>
        /// 9955
        /// </summary>
        public string TEAM_BET = "";
        /// <summary>
        /// 9991
        /// </summary>
        public string ATTN = "";
        /// <summary>
        /// 9992
        /// </summary>
        public string CALL_LS = "";
        /// <summary>
        /// 9993
        /// </summary>
        public string CALL_OK = "";
        /// <summary>
        /// 9994
        /// </summary>
        public string CARD = "";
        /// <summary>
        /// 9995
        /// </summary>
        public string COMM_LS = "";
        /// <summary>
        /// 9996
        /// </summary>
        public string COMM_OK = "";
        /// <summary>
        /// 9997
        /// </summary>
        public string COMMT = "";
        /// <summary>
        /// 9998
        /// </summary>
        public string GS = "";
        /// <summary>
        /// 9999
        /// </summary>
        public string NEU_GD = "";
        /// <summary>
        /// 9905
        /// </summary>
        public string SC11 = "";
        /// <summary>
        /// 9906
        /// </summary>
        public string SC12 = "";
        /// <summary>
        /// 9907
        /// </summary>
        public string SC21 = "";
        /// <summary>
        /// 9908
        /// </summary>
        public string SC22 = "";
        /// <summary>
        /// 9941
        /// </summary>
        public string TIATK1 = "";
        /// <summary>
        /// 9942
        /// </summary>
        public string TIATK2 = "";
        /// <summary>
        /// 9943
        /// </summary>
        public string TIDGR1 = "";
        /// <summary>
        /// 9944
        /// </summary>
        public string TIDGR2 = "";
        /// <summary>
        /// 9945
        /// </summary>
        public string TISAF1 = "";
        /// <summary>
        /// 9946
        /// </summary>
        public string TISAF2 = "";
        /// <summary>
        /// 9961
        /// </summary>
        public string GOAL_OWN1 = "";
        /// <summary>
        /// 9962
        /// </summary>
        public string GOAL_OWN2 = "";
        /// <summary>
        /// 9963
        /// </summary>
        public string GOAL_PEN1 = "";
        /// <summary>
        /// 9964
        /// </summary>
        public string GOAL_PEN2 = "";
        /// <summary>
        /// 9965
        /// </summary>
        public string GOAL_PENM1 = "";
        /// <summary>
        /// 9966
        /// </summary>
        public string GOAL_PENM2 = "";
        /// <summary>
        /// 9967
        /// </summary>
        public string KICK_RES1 = "";
        /// <summary>
        /// 9968
        /// </summary>
        public string KICK_RES2 = "";
        /// <summary>
        /// 9971
        /// </summary>
        public string CROSS1 = "";
        /// <summary>
        /// 9972
        /// </summary>
        public string CROSS2 = "";
        /// <summary>
        /// 9981
        /// </summary>
        public string FK_ATK1 = "";
        /// <summary>
        /// 9982
        /// </summary>
        public string FK_ATK2 = "";
        /// <summary>
        /// 9983
        /// </summary>
        public string FK_DGR1 = "";
        /// <summary>
        /// 9984
        /// </summary>
        public string FK_DGR2 = "";
        /// <summary>
        /// 9985
        /// </summary>
        public string FK_SAF1 = "";
        /// <summary>
        /// 9986
        /// </summary>
        public string FK_SAF2 = "";
    }
}
