package com.zzj.gambler.model;

public class RespBet {

	public boolean success;
	
	/**
	 * 失败请求的消息
	 */
	public String msg;
	
	/**
	 * 失败时新的赔率值
	 */
	public int newOdds;
	
	/**
	 * 成功时的下注单号
	 */
	public String code;
}
