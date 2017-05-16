package com.zzj.gambler.model;

public class RespBet extends RespBase {

	/**
	 * 失败时新的赔率值
	 */
	public int newOdds;
	
	/**
	 * 成功时的下注单号
	 */
	public String code;
}
