package com.zzj.gambler.http;

import java.io.UnsupportedEncodingException;
import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLEncoder;
import java.util.HashMap;
import java.util.Map;

import sun.net.www.http.HttpClient;

public class HttpUtil {

	public void get(String requestUrl, HashMap<String, String> queryMap, HashMap<String, String> headerMap) {
		try {
			
			String realUrl = constructUrl(requestUrl, queryMap);
			
			URL url = new URL(realUrl);
			HttpURLConnection connection = (HttpURLConnection) url.openConnection();
			
			addHeader(connection, headerMap);
		} catch (Throwable t) {

		}
	}

	private void addHeader(HttpURLConnection connection, HashMap<String, String> headerMap) {
		for (Map.Entry<String, String> header : headerMap.entrySet()) {
			connection.addRequestProperty(header.getKey(), header.getValue());
		
		}
	}

	private String constructUrl(String requestUrl, HashMap<String, String> queryMap) {
		StringBuilder builder = new StringBuilder(requestUrl);
		if (builder.indexOf("?") == -1) {
			builder.append('?');
		}
		if (builder.charAt(builder.length() - 1)  != '?') {
			builder.append('&');
		}
		try {
			for (Map.Entry<String, String> entry : queryMap.entrySet()) {
					builder.append(entry.getKey()).append("=").append(URLEncoder.encode(entry.getValue(), "UTF-8")).append('&');	
			}
		} catch (UnsupportedEncodingException e) {
			e.printStackTrace();
		}
		builder.deleteCharAt(builder.length() - 1);
		return builder.toString();
	}
}
