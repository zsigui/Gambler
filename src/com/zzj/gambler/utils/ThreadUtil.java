package com.zzj.gambler.utils;

public class ThreadUtil {

	/**
	 * 设定并发任务数为 4，任务请求超时时间为 5 秒
	 */
	private static NetExecutor sNetExecutor = new NetExecutor(4, 5000);
	
	public static void execNetWork(Runnable r) {
		sNetExecutor.execute(r); 
	}
	
	public static void runOnNewThread(Runnable r) {
		Thread t = new Thread(r);
		t.setPriority(Thread.NORM_PRIORITY  - 1);
		t.setDaemon(false);
		t.start();
	}
	
	
}
