package com.zzj.gambler.utils;

import java.io.UnsupportedEncodingException;
import java.lang.reflect.Type;

import com.google.gson.Gson;
import com.zzj.gambler.model.ReqBetData;

public class GsonWorker {
	
	private static GsonWorker sInstance = new GsonWorker();
	public static GsonWorker get() {
		return sInstance;
	}

	private Gson gson;
	
	public Gson getGson() {
		if (gson == null) {
			gson = new Gson();
		}
		return gson;
	}
	
	public <T> T byteArrayToObject(byte[] bs, Type t) {
		String string = "";
		try {
			string = new String(bs, "UTF-8");
			return getGson().fromJson(string, t);
		} catch (UnsupportedEncodingException e) {
			LogUtil.log("to parse : " + string);
			LogUtil.log(e);
			return null;
		}
	}

	public <T> T StringToObject(String json, Type t) {
			return getGson().fromJson(json, t);
	}
	
	public String ObjectToJson(Object obj, Type t) {
		return getGson().toJson(obj, t);
	}
}
