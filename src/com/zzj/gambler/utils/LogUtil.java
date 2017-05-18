package com.zzj.gambler.utils;

public class LogUtil {

	private static final boolean DEBUG = true;

	public static void log(String content) {
		if (DEBUG) {
			System.out.println(content);
		}
	}

	public static void log(Throwable t) {
		if (DEBUG) {
			t.printStackTrace();
		}
	}
}
