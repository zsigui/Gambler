package com.zzj.gambler;

import java.util.Map;

/**
 * 对外提供Api的封装类
 * 
 * @author zsigui
 */
public class GamblerApi {

	private static class Singleton {
		private static final GamblerApi INSTANCE = new GamblerApi();
	}
	
	private GamblerApi() {}
	
	/**
	 * 获取此类的惟一实例
	 * @return
	 */
	public static GamblerApi getInstance(){
		return Singleton.INSTANCE;
	}
	
	public static Map<String, Object> obtainResult() {
		return null;
	}
}
