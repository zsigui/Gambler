package com.zzj.gambler.config;

public class NetConfig {

	// gameType 类型参数值说明：由三部分组成，用 '_' 进行连接，FT_FT_MN
	// 1.
	// RB : 滚球
	// TD : 今日赛事
	// FT :  早盘
	// 2. 
	// FT : 足球
	// BK : 篮球
	// 3.
	// MN : 独赢 ＆ 让球 ＆ 大小 ＆ 单 / 双
	// TI : 波胆
	// BC : 总入球
	// HF : 半场 / 全场
	// MX : 综合过关
	// CH : 冠军
	//  
	// pageNo 类型值说明：从 1 开始的页码数
	//
	// sortType 类型值说明： 1 按联盟排序     2 按时间排序
	//
	// showLegs 类型值说明：表示搜索的联盟名称，字符串数组类型 [""]，没有该值表示默认全选
	//
	// P.S. POST 和 GET 数据需要执行 UTF8_UrlEncode 操作
	
	/*
	 * 数据请求的过程
	 */
	private static final String URL_HOST_NAME = "http://www.1559501.com"; 
	/**
	 * 获取登录所需要的验证码 <br />
	 * GET : timestamp (单位:ms)
	 */
	public static final String URL_VERICODE = URL_HOST_NAME + "/verifycode.do";
	/**
	 * 请求登录 <br />
	 * POST : account、password、verifyCode 
	 */
	public static final String URL_LOGIN = URL_HOST_NAME + "/login.do";
	/**
	 * 获取用户账号信息 <br />
	 * POST
	 */
	public static final String URL_USER_INFO = URL_HOST_NAME + "/curstinfo.do";
	/**
	 * 获取下注数据名称 <br />
	 * POST : gameType、pageNo、sortType、showLegs
	 */
	public static final String URL_SPORT_DATA = URL_HOST_NAME + "/sports/hg/getData.do";
	/**
	 * 获取请求的联赛名称 <br />
	 * POST : gameType <br />
	 */
	public static final String URL_SPORT_LEGUES = URL_HOST_NAME + "/sports/hg/getLeaues.do";
	/**
	 * 执行下注请求 <br />
	 * POST : {"money": (float), "accptBestOdds": (boolean), "plate":"H", "gameType": (String) , "items": []}
	 */
	public static final String URL_BET = "/sports/hg/bet/bet.do";
}
