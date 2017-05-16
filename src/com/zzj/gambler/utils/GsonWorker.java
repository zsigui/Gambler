package com.zzj.gambler.utils;

import java.io.UnsupportedEncodingException;
import java.lang.reflect.Type;

import com.google.gson.Gson;

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
		try {
			String string = new String(bs, "UTF-8");
			System.out.println(string);
			return getGson().fromJson(string, t);
		} catch (UnsupportedEncodingException e) {
			e.printStackTrace();
			return null;
		}
	}
}
