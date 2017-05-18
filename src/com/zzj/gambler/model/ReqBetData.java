package com.zzj.gambler.model;

import java.util.ArrayList;

public class ReqBetData {

	/**
	 * 盘口 <br >
	 * H : 香港变盘（输水盘）  M : 马来盘  I : 印尼盘  E : 欧洲盘  其它 : 香港盘 <br />
	 * 暂时测试后其它盘口获取赔率是失败的
	 */
	public String plate = "H";
	
	/**
	 * gameType 类型参数值说明：由三部分组成，用 '_' 进行连接，FT_FT_MN
	 * 1.
	 * RB : 滚球
	 * TD : 今日赛事
	 * FT :  早盘
	 * 2. 
	 * FT : 足球
	 * BK : 篮球
	 * 3.
	 * MN : 独赢 ＆ 让球 ＆ 大小 ＆ 单 / 双
	 * TI : 波胆
	 * BC : 总入球
	 * HF : 半场 / 全场
	 * MX : 综合过关
	 * CH : 冠军
	 */
	public String gameType;
	
	public String money;
	
	public boolean acceptBestOdds = true;
	
	public ArrayList<ReqBetItem> items;
}
