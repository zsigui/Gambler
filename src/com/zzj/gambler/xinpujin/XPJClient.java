package com.zzj.gambler.xinpujin;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.UUID;

import com.google.gson.reflect.TypeToken;
import com.zzj.gambler.config.ErrorMsg;
import com.zzj.gambler.config.NetConfig;
import com.zzj.gambler.http.HttpUtil;
import com.zzj.gambler.http.HttpUtil.HttpCallback;
import com.zzj.gambler.model.ReqBetData;
import com.zzj.gambler.model.RespBase;
import com.zzj.gambler.model.RespBet;
import com.zzj.gambler.model.RespData;
import com.zzj.gambler.model.RespLeague;
import com.zzj.gambler.model.RespLogin;
import com.zzj.gambler.model.RespOdd;
import com.zzj.gambler.model.RespUser;
import com.zzj.gambler.utils.GsonWorker;
import com.zzj.gambler.utils.HttpClient;
import com.zzj.gambler.utils.LogUtil;
import com.zzj.gambler.utils.MD5Util;
import com.zzj.gambler.utils.VerifyCodeUtil;


public class XPJClient {

	private String mUsername;
	private String mPassowrd;
	private float mRemainMoney;
	private String mSessoin;
	private String mJSessionId;
	private HashMap<String, String> storedHeaders;

	private final String KEY_SESSION = "SESSION";
	private final String KEY_JSESSION_ID = "JSESSIONID";

	private static final int MAX_RETRY_LOGIN_COUNT = 4;
	/**
	 * 传入用于登录的用户名和密码，会默认生成一个随机的UUID作为SESSION用于Http请求操作
	 */
	public XPJClient(String username, String password) {
		this(username, password, UUID.randomUUID().toString());
	}

	/**
	 * 传入用于登录的用户名和密码以及指定的SESSION值
	 */
	public XPJClient(String username, String password, String session) {
		mUsername = username;
		mPassowrd = password;
		mSessoin = session;
		mJSessionId = MD5Util.digestInHex(UUID.randomUUID().toString(), HttpUtil.CHARSET);
		initStoredHeader();
	}

	/**
	 * 初始设置默认的Http请求头信息
	 **/
	private void initStoredHeader() {
		storedHeaders = new HashMap<String, String>();
		storedHeaders.put("X-Requested-With", "XMLHttpRequest");
		storedHeaders.put("Accept", "application/json, text/javascript, */*; q=0.01");
		storedHeaders.put("Accept-Encoding", "gzip, defalte");
		storedHeaders.put("Accept-Language", " zh-CN,zh;q=0.8,en;q=0.6");
		storedHeaders
				.put("User-Agent",
						"Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Ubuntu Chromium/58.0.3029.96 Chrome/58.0.3029.96 Safari/537.36");
		setCookie();
	}

	private void setCookie() {
		StringBuilder cookies = new StringBuilder();
		if (mSessoin != null) {
			cookies.append(KEY_SESSION).append("=").append(mSessoin).append(";");
		}
		if (mJSessionId != null) {
			cookies.append(KEY_JSESSION_ID).append("JSESSIONID=").append(mJSessionId).append(";");
		}
		if (cookies.length() > 0) {
			cookies.deleteCharAt(cookies.length() - 1);
			storedHeaders.put("Cookie", cookies.toString());
		}
	}

	private HashMap<String, String> constructKeyValMap(String... data) {
		HashMap<String, String> result = new HashMap<>();
		for (int i = 0; i < data.length; i += 2) {
			result.put(data[i], data[i + 1]);
		}
		return result;
	}

	/**
	 * 解析请求返回的Http头获取Set-Cookie信息
	 * 
	 * @param headers
	 */
	private void handleResponseHeaderForCookie(Map<String, List<String>> headers) {
		List<String> cookies = headers.get("Set-Cookie");
		if (cookies != null && !cookies.isEmpty()) {
			int i;
			String tmp;
			String[] kv;
			boolean needResetCookie = false;
			// 遍历所有Set-Cookie的值
			for (String cookie : cookies) {
				i = cookie.indexOf(';');
				tmp = cookie.substring(0, i == -1 ? cookie.length() : i).trim();
				kv = tmp.split("=");
				if (kv.length == 2) {
					kv[0] = kv[0].trim();
					kv[1] = kv[1].trim();
					if (KEY_SESSION.equalsIgnoreCase(kv[0]) && !kv[1].equalsIgnoreCase(mSessoin)) {
						needResetCookie = true;
						mSessoin = kv[1];
					} else if (KEY_JSESSION_ID.equalsIgnoreCase(kv[0]) && !kv[1].equalsIgnoreCase(mJSessionId)) {
						needResetCookie = true;
						mJSessionId = kv[1];
					}
				}
			}

			// Session和JSessionID变动，所以需要重新设置Cookie
			if (needResetCookie) {
				setCookie();
			}
		}
	}

	private <T extends RespBase> void respOnError(RespCallback<T> callback, Throwable t) {
		LogUtil.log(t);
		if (callback != null) {
			callback.onError(t);
		}
	}
	
	private <T extends RespBase> void respOnFail(RespCallback<T> callback, int httpStatus, T data, String note) {
		if (callback != null) {
			if (!HttpUtil.isSucc(httpStatus)) {
				callback.onFailed(httpStatus, ErrorMsg.C_BAD_HTTP_REQUEST);
			} else if (data != null && data.msg != null && !data.msg.isEmpty()) {
				callback.onFailed(httpStatus, data.msg);
			} else if (note != null && !note.isEmpty()){
				callback.onFailed(httpStatus, note);
			} else {
				callback.onFailed(httpStatus, ErrorMsg.C_BAD_RESP_DATA);
			}
		}
	}
	
	private <T extends RespBase> void respOnSuccess(RespCallback<T> callback, T data) {
		if (callback != null) {
			callback.onSuccess(data);
		}
	}
	
	/**
	 * 执行登录请求，默认重试4次（针对验证码错误）
	 */
	public void login(final RespCallback<RespLogin> respCallback) {
		login(respCallback, MAX_RETRY_LOGIN_COUNT);
	}
	
	/**
	 * 执行指定次数的登录请求（针对验证码错误进行重试）
	 * @param respCallback
	 * @param retryCount 当val<0时表示无重复次数限制
	 */
	public void login(final RespCallback<RespLogin> respCallback, final int retryCount) {
		HashMap<String, String> queryMap = constructKeyValMap("timestamp", String.valueOf(System.currentTimeMillis()));
		
		HttpCallback<byte[]> callback = new HttpCallback<byte[]>() {

			@Override
			public byte[] convertBytes(byte[] data) {
				return data;
			}

			@Override
			public void onFinish(final int code, final byte[] data, Map<String, List<String>> headers) {
				if (HttpUtil.isSucc(code) && data != null) {
					handleResponseHeaderForCookie(headers);
					String vc = VerifyCodeUtil.readCode(data);
					if (vc.isEmpty() || vc.length() != 4) {
						if (retryCount == 0) {
							respOnFail(respCallback, code, null, ErrorMsg.C_FAIL_TO_VERIFY_CODE);
							return;
						}
						// 重新进行验证码请求
						login(respCallback, retryCount - 1);
					} else {
						loginInternal(vc, respCallback, retryCount);
					}
					return;
				}
				// 其他登录失败情况，地址或者获取失败等
				respOnFail(respCallback, code, null, null);
			}

			@Override
			public void onError(Throwable e) {
				respOnError(respCallback, e);
			}
		};
		HttpClient.doGet(NetConfig.URL_VERICODE, queryMap, storedHeaders, callback);
	}

	/**
	 * 执行带验证码的登录操作
	 * @param code 请求会话对应的验证码
	 * @param respCallback
	 */
	public void loginInternal(String code, final RespCallback<RespLogin> respCallback, final int retryCount) {
		HashMap<String, String> postMap = constructKeyValMap(
				"account", mUsername,
				"password", mPassowrd,
				"verifyCode", code);
		HttpCallback<RespLogin> callback = new HttpCallback<RespLogin>() {

			@Override
			public RespLogin convertBytes(byte[] data) {
				return GsonWorker.get().byteArrayToObject(data, RespLogin.class);
			}

			@Override
			public void onFinish(final int code, final RespLogin data, Map<String, List<String>> headers) {
				LogUtil.log("code = " + code);
				LogUtil.log("data = " + (data == null ? null : data.toString()));
				if (HttpUtil.isSucc(code) && data != null) {
					if (!data.success && ErrorMsg.S_BAD_CODE.equalsIgnoreCase(data.msg)) {
						if (retryCount == 0) {
							respOnFail(respCallback, code, data, null);
							return;
						}
						// 重新再请求一次
						login(respCallback, retryCount - 1);
						return;
					}
					
					handleResponseHeaderForCookie(headers);
					respOnSuccess(respCallback, data);
					return;
				}
				respOnFail(respCallback, code, data, null);
			}

			@Override
			public void onError(Throwable e) {
				respOnError(respCallback, e);
			}
		};
		HttpClient.doPost(NetConfig.URL_LOGIN, postMap, null, storedHeaders, callback);
	}

	/**
	 * 获取用户当前账号的余额
	 */
	public void requestUserInfo(final RespCallback<RespUser> respCallback) {
		HttpCallback<RespUser> callback = new HttpCallback<RespUser>() {

			@Override
			public RespUser convertBytes(byte[] data) {
				return GsonWorker.get().byteArrayToObject(data, RespUser.class);
			}

			@Override
			public void onFinish(int code, RespUser data, Map<String, List<String>> headers) {
				if (HttpUtil.isSucc(code) && data != null && data.success) {
					mRemainMoney = data.money;
					respOnSuccess(respCallback, data);
					return;
				}
				respOnFail(respCallback, code, data, null);
			}

			@Override
			public void onError(Throwable e) {
				respOnError(respCallback, e);
			}
		};
		HttpClient.doPost(NetConfig.URL_USER_INFO, null, null, storedHeaders, callback);
	}

	/**
	 * 获取指定游戏类型的所有联赛名称
	 */
	public void requestLeagueData(String gameType, final RespCallback<RespLeague> respCallback) {
		HashMap<String, String> postMap = constructKeyValMap("gameType", gameType);
		HttpCallback<RespLeague> callback = new HttpCallback<RespLeague>() {

			@Override
			public RespLeague convertBytes(byte[] data) {
				return GsonWorker.get().byteArrayToObject(data, RespLeague.class);
			}

			@Override
			public void onFinish(final int code, final RespLeague data, Map<String, List<String>> headers) {
				if (HttpUtil.isSucc(code) && data != null && data.success) {
					respOnSuccess(respCallback, data);
					return;
				}
				respOnFail(respCallback, code, data, null);
			}

			@Override
			public void onError(Throwable e) {
				respOnError(respCallback, e);
			}
		};
		HttpClient.doPost(NetConfig.URL_SPORT_LEGUES, postMap, null, storedHeaders, callback);
	}

	/**
	 * 获取指定游戏类型的所有联赛对应的盘口数据 (第一页数据，按时间排序)
	 */
	public void requestOddData(String gameType, RespCallback<RespData> respCallback) {
		requestOddData(gameType, 1, 1, null, respCallback);
	}
	
	/**
	 * 获取指定游戏类型的指定联赛名称对应的盘口数据 (指定页数，指定排序类型)
	 * 
	 * @param gameType 游戏类型
	 * @param pageNo 页码 1~
	 * @param sortType 排序类型 1 按时间 2 按联盟
	 * @param leagues 指定联赛名称数组
	 * @param respCallback
	 */
	public void requestOddData(String gameType, int pageNo, int sortType, String[] leagues, final RespCallback<RespData> respCallback) {
		HashMap<String, String> postMap = constructKeyValMap(
				"gameType", gameType,
				"pageNo", String.valueOf(pageNo),
				"sortType", String.valueOf(sortType));
		if (leagues != null && leagues.length > 0) {
			postMap.put("showLegs", GsonWorker.get().ObjectToJson(leagues, new TypeToken<String[]>(){}.getType()));
		}
		HttpCallback<RespData> callback = new HttpCallback<RespData>() {

			@Override
			public RespData convertBytes(byte[] data) {
				return GsonWorker.get().byteArrayToObject(data, RespData.class);
			}

			@Override
			public void onFinish(final int code, final RespData data, Map<String, List<String>> headers) {
				if (HttpUtil.isSucc(code) && data != null && data.games != null) {
					respOnSuccess(respCallback, data);
					return;
				}
				respOnFail(respCallback, code, data, null);
			}

			@Override
			public void onError(Throwable e) {
				respOnError(respCallback, e);
			}
		};
		HttpClient.doPost(NetConfig.URL_SPORT_DATA, postMap, null, storedHeaders, callback);
	}
	
	/**
	 * 请求特定盘口数据
	 */
	public void requestSpecificOdd(ReqBetData reqBetData, final RespCallback<RespOdd> respCallback) {
		HashMap<String, String> postMap = constructKeyValMap("data",
				GsonWorker.get().ObjectToJson(reqBetData, ReqBetData.class));
		HttpCallback<RespOdd> callback = new HttpCallback<RespOdd>() {

			@Override
			public RespOdd convertBytes(byte[] data) {
				return GsonWorker.get().byteArrayToObject(data, RespOdd.class);
			}

			@Override
			public void onFinish(final int code, final RespOdd data, Map<String, List<String>> headers) {
				if (HttpUtil.isSucc(code) && data != null && data.odds != null) {
					respOnSuccess(respCallback, data);
					return;
				}
				respOnFail(respCallback, code, data, null);
			}

			@Override
			public void onError(Throwable e) {
				respOnError(respCallback, e);
			}
		};
		HttpClient.doPost(NetConfig.URL_GET_ODD, postMap, null, storedHeaders, callback);
	}
	
	/**
	 * 请求下注
	 */
	public void requestBet(ReqBetData reqBetData, final RespCallback<RespBet> respCallback) {
		HashMap<String, String> postMap = constructKeyValMap("data",
				GsonWorker.get().ObjectToJson(reqBetData, ReqBetData.class));
		HttpCallback<RespBet> callback = new HttpCallback<RespBet>() {

			@Override
			public RespBet convertBytes(byte[] data) {
				return GsonWorker.get().byteArrayToObject(data, RespBet.class);
			}

			@Override
			public void onFinish(int code, RespBet data, Map<String, List<String>> headers) {
				if (HttpUtil.isSucc(code) && data != null && data.success) {
					respOnSuccess(respCallback, data);
					return;
				}
				respOnFail(respCallback, code, data, null);
			}

			@Override
			public void onError(Throwable e) {
				respOnError(respCallback, e);
			}
		};
		HttpClient.doPost(NetConfig.URL_BET, postMap, null, storedHeaders, callback);
	}

	/**
	 * 定义可以请求的GameType类型 <br />
	 * 
	 * gameType 类型参数值说明：由三部分组成，用 '_' 进行连接，FT_FT_MN <br />
	 * 1.<br />
	 * RB : 滚球
	 * TD : 今日赛事
	 * FT :  早盘
	 * 2. <br />
	 * FT : 足球
	 * BK : 篮球
	 * 3.<br />
	 * MN : 独赢 ＆ 让球 ＆ 大小 ＆ 单 / 双
	 * TI : 波胆
	 * BC : 总入球
	 * HF : 半场 / 全场
	 * MX : 综合过关
	 * CH : 冠军
	 */
	public interface GameType {
		String FT_RB_MN = "FT_RB_MN";
		String FT_RB_TI = "FT_RB_TI";
		String FT_RB_BC = "FT_RB_BC";
		String FT_RB_HF = "FT_RB_HF";
		
		String BK_RB_MN = "BK_RB_MN";
		
		String FT_TD_MN = "FT_TD_MN";
		String FT_TD_TI = "FT_TD_TI";
		String FT_TD_BC = "FT_TD_BC";
		String FT_TD_HF = "FT_TD_HF";
		String FT_TD_MX = "FT_TD_MX";
		String FT_TD_CH = "FT_TD_CH";
		
		String BK_TD_MN = "BK_TD_MN";
		String BK_TD_MX = "BK_TD_MX";
		
		String FT_FT_MN = "FT_FT_MN";
		String FT_FT_TI = "FT_FT_TI";
		String FT_FT_BC = "FT_FT_BC";
		String FT_FT_HF = "FT_FT_HF";
		String FT_FT_MX = "FT_FT_MX";
		String FT_FT_CH = "FT_FT_CH";
		
		String BK_FT_MN = "BK_FT_MN";
		String BK_FT_MX = "BK_FT_MX";
		String BK_FT_CH = "BK_FT_CH";
		
	}
	
	/**
	 * 执行网络请求之后的回调接口
	 */
	public interface RespCallback<T extends RespBase> {
		/**
		 * 网络请求成功，并过滤服务器错误后返回成功结果
		 * 
		 * @param data
		 *            返回的 Data 数据不为空
		 */
		void onSuccess(T data);

		/**
		 * 网络请求过程出错，可能是 Http 请求错误，也可能是请求参数错误之类的服务器错误
		 * 
		 * @param httpStatus
		 *            错误请求状态码，当为 >=200 && < 400 时表示服务器错误
		 * @param errorMsg
		 *            服务器错误时返回的错误信息
		 */
		void onFailed(int httpStatus, String errorMsg);

		/**
		 * 请求执行过程出现的错误，一般为客户端错误
		 * 
		 * @param t
		 *            执行错误抛出的异常
		 */
		void onError(Throwable t);
	}

	/**
	 * 返回找到的第一个符合搜索条件的比赛盘口数据
	 * 
	 * @param data
	 * @param host
	 * @param client
	 * @param league
	 * @return
	 */
	public static ArrayList<String> searchMatch(RespData data, String host, String client, String league) {
		for (ArrayList<String> item : data.games) {
			if (item.get(1).equalsIgnoreCase(host)
					&& item.get(2).equalsIgnoreCase(client)
					&& item.get(5).equalsIgnoreCase(league)) {
				return item;
			}
		}
		return null;
	}
	
	public static int searchIorIndex(ArrayList<String> header, String ior){
		for (int i = 0; i < header.size(); i++) {
			if (header.get(i).equalsIgnoreCase(ior))
				return i;
		}
		return -1;
	}
}
