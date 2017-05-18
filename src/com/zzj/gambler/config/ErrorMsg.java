package com.zzj.gambler.config;

public final class ErrorMsg {

	// 服务器返回
	public static final String S_BAD_CODE = "验证码错误！";
	
	// 客户端自定义
	public static final String C_EXEC_EXCEPTION = "逻辑执行异常";
	public static final String C_BAD_HTTP_REQUEST = "HTTP请求出错";
	public static final String C_FAIL_TO_VERIFY_CODE = "识别验证码失败";
	public static final String C_BAD_RESP_DATA = "请求返回的服务器数据错误";
}
