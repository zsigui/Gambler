package com.zzj.gambler.utils;

import java.util.HashMap;

import com.zzj.gambler.http.HttpUtil;
import com.zzj.gambler.http.HttpUtil.HttpCallback;

public class HttpClient {

	
	public static  <T>  void doGet(String requestUrl, HashMap<String, String> queryMap, 
			HashMap<String, String> headerMap, HttpCallback<T> callback) {
		HttpUtil.doRequest(requestUrl, queryMap, null, null, HttpUtil.Method.GET, headerMap, null, callback);
	}
	
	public static <T> void doPost(String requestUrl, HashMap<String, String> postMap, String jsonContent,
			HashMap<String, String> headerMap, HttpCallback<T> callback) {
		HttpUtil.doRequest(requestUrl, null, postMap, jsonContent, HttpUtil.Method.POST, headerMap, null, callback);
	}
}
