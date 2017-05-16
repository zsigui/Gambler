package com.zzj.gambler.xinpujin;

import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.UUID;

import com.zzj.gambler.config.NetConfig;
import com.zzj.gambler.http.HttpUtil;
import com.zzj.gambler.http.HttpUtil.HttpCallback;
import com.zzj.gambler.model.RespLogin;
import com.zzj.gambler.model.RespUser;
import com.zzj.gambler.utils.GsonWorker;
import com.zzj.gambler.utils.HttpClient;
import com.zzj.gambler.utils.LogUtil;
import com.zzj.gambler.utils.VerifyCodeUtil;

public class XPJClient {

	private String mUsername;
	private String mPassowrd;
	private float mRemainMoney;
	private HashMap<String, String> storedHeaders;

	public XPJClient(String username, String password) {
		storedHeaders = new HashMap<String, String>();
		storedHeaders.put("Cookie", "SESSION=" + UUID.randomUUID());
		mUsername = username;
		mPassowrd = password;
	}
	
	public XPJClient(String username, String password, String session) {
		storedHeaders = new HashMap<String, String>();
		storedHeaders.put("Cookie", "SESSION=" + session);
		mUsername = username;
		mPassowrd = password;
	}

	private void addCookieHeader(Map<String, List<String>> headers) {
		List<String> cookies = headers.get("Set-Cookie");
		if (cookies != null && !cookies.isEmpty()) {
			StringBuilder builder = new StringBuilder();
			int i;
			for (String cookie : cookies) {
				i = cookie.indexOf(';');
				builder.append(cookie.substring(0, i == -1 ? cookie.length() : i).trim()).append(';');
			}
			storedHeaders.clear();
			storedHeaders.put("Cookie", builder.toString());
			LogUtil.log("addCookieHeader : cookie = " + storedHeaders.get("Cookie"));
		}
	}

	public void Login() {
		HashMap<String, String> queryMap = new HashMap<>();
		queryMap.put("timestamp", String.valueOf(System.currentTimeMillis()));
		HttpCallback<byte[]> callback = new HttpCallback<byte[]>() {

			@Override
			public byte[] convertBytes(byte[] data) {
				return data;
			}

			@Override
			public void onFinish(int code, byte[] data, Map<String, List<String>> headers) {
				if (HttpUtil.isSucc(code) && data != null) {
					addCookieHeader(headers);
					String vc = VerifyCodeUtil.readCode(data);
					loginInternal(vc);
				}
			}

			@Override
			public void onError(Throwable e) {
			}
		};
		HttpClient.doGet(NetConfig.URL_VERICODE, queryMap, storedHeaders, callback);
	}

	public void loginInternal(String code) {
		HashMap<String, String> postMap = new HashMap<>();
		postMap.put("account", mUsername);
		postMap.put("password", mPassowrd);
		postMap.put("verifyCode", code);
		HttpCallback<RespLogin> callback = new HttpCallback<RespLogin>() {

			@Override
			public RespLogin convertBytes(byte[] data) {
				return GsonWorker.get().byteArrayToObject(data, RespLogin.class);
			}

			@Override
			public void onFinish(int code, RespLogin data, Map<String, List<String>> headers) {
				LogUtil.log("code = " + code);
				LogUtil.log("data = " + (data == null ? null : data.toString()));
			}

			@Override
			public void onError(Throwable e) {
				e.printStackTrace();
			}
		};
		HttpClient.doPost(NetConfig.URL_LOGIN, postMap, null, storedHeaders, callback);
	}
	
	public void requestUserInfo() {
		HttpCallback<RespUser> callback = new HttpCallback<RespUser>() {

			@Override
			public RespUser convertBytes(byte[] data) {
				return GsonWorker.get().byteArrayToObject(data, RespUser.class);
			}

			@Override
			public void onFinish(int code, RespUser data, Map<String, List<String>> headers) {
				if (HttpUtil.isSucc(code) && data != null) {
					mRemainMoney = data.money;
				}
			}

			@Override
			public void onError(Throwable e) {
			}
		};
		HttpClient.doPost(NetConfig.URL_USER_INFO, null, null, storedHeaders, callback);
	}
	
	private void requestBetData() {
		
	}
	
	private void requestLeagueData() {
		
	}
}
