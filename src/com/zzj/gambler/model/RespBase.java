package com.zzj.gambler.model;

public class RespBase {

	public boolean success;
	
	/**
	 * 失败请求的消息
	 */
	public String msg;

	@Override
	public String toString() {
//		return super.toString();
		return "success = " + success + ", msg = " + msg;
	}
}
