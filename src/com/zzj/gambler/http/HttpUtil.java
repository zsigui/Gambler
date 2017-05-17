package com.zzj.gambler.http;

import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.ByteArrayOutputStream;
import java.io.Closeable;
import java.io.IOException;
import java.io.InputStream;
import java.io.UnsupportedEncodingException;
import java.net.HttpURLConnection;
import java.net.Proxy;
import java.net.URL;
import java.net.URLEncoder;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.zip.GZIPInputStream;

import com.zzj.gambler.utils.ByteArrayPool;

public class HttpUtil {

	private static final int NET_CONNECT_TIMEOUT = 10_000;
	private static final int NET_READ_TIMEOUT = 30_000;
	public static final String CHARSET = "UTF-8";
	
	public interface Method {
		String GET = "GET";
		String POST = "POST";
		String PUT = "PUT";
		String Delete = "DELETE";
	}

	public static <T> void doRequest(String requestUrl, HashMap<String, String> queryMap,
			HashMap<String, String> bodyMap, String jsonBody, String requestMethod, HashMap<String, String> headerMap,
			HttpRequest request, HttpCallback<T> callback) {

		if (requestUrl == null || requestUrl.isEmpty())
			throw new IllegalArgumentException("requestUrl 不允许为空");
		if (requestMethod == null || requestMethod.isEmpty())
			throw new IllegalArgumentException("requestMethod 不允许为空");
		if (callback == null)
			throw new IllegalArgumentException("callback 不允许为null");

		try {

			String realUrl = constructUrl(requestUrl, queryMap);

			URL url = new URL(realUrl);
			Proxy p = null;
			if (request != null) {
				p = request.configProxy();
			}
			// 打开HttpURLConnection
			HttpURLConnection connection;
			if (p == null) {
				connection = (HttpURLConnection) url.openConnection();
			} else {
				connection = (HttpURLConnection) url.openConnection(p);
			}
			configDefaultConnection(connection);
			addHeader(connection, headerMap);
			connection.setRequestMethod(requestMethod);
			
			writeOutputStream(bodyMap, jsonBody, connection);

			InputStream in = getInputStream(connection);

			int code = connection.getResponseCode();
			T data = null;
			Map<String, List<String>> headers = null;
			if (code >= 200 && code < 400) {
				data = callback.convertBytes(readBuffer(in));
				headers = connection.getHeaderFields();
			} else {
				closeIO(in);
			}
			connection.disconnect();
			callback.onFinish(code, data, headers);
		} catch (Throwable t) {
			callback.onError(t);
		}
	}

	private static void writeOutputStream(HashMap<String, String> bodyMap, String jsonBody, HttpURLConnection connection)
			throws IOException, UnsupportedEncodingException {
		if ((bodyMap != null && bodyMap.size() > 0) || (jsonBody != null && !jsonBody.isEmpty())) {
			// 需要传输数据，所以进行设置DoOutput为true
			connection.setDoOutput(true);
			String outputContent;
			if (jsonBody != null) {
				connection.setRequestProperty("Content-Type", "application/json; charset=" + CHARSET);
				outputContent = jsonBody;
			} else {
				connection.setRequestProperty("Content-Type", "application/x-www-form-urlencoded; charset=" + CHARSET);
				outputContent = concatPostData(bodyMap);
			}
			BufferedOutputStream bos = new BufferedOutputStream(connection.getOutputStream());
			bos.write(outputContent.getBytes(CHARSET));
			bos.flush();
			closeIO(bos);

		} else {
			connection.setRequestProperty("Content-Type", "*/*; charset=" + CHARSET);
			connection.setDoOutput(false);
		}
	}

//	private static String concatPostData(HashMap<String, String> bodyMap) {
//		StringBuilder builder = new StringBuilder();
//		for (Map.Entry<String, String> entry : bodyMap.entrySet()) {
//			builder.append(entry.getKey()).append("=").append(entry.getValue());
//		}
//		String result = builder.toString();
//		try {
//			return URLEncoder.encode(result, CHARSET);
//		} catch (UnsupportedEncodingException e) {
//			return result;
//		}
//	}
	
	private static String concatPostData(HashMap<String, String> bodyMap) {
		StringBuilder builder = new StringBuilder();
		try {
			for (Map.Entry<String, String> entry : bodyMap.entrySet()) {
				builder.append(entry.getKey()).append("=").append(URLEncoder.encode(entry.getValue(), CHARSET)).append("&");
			}
			if (builder.length() > 0) {
				builder.deleteCharAt(builder.length() - 1);
			}
		} catch (UnsupportedEncodingException e) {
			e.printStackTrace();
		}
		return builder.toString();
	}

	private static void configDefaultConnection(HttpURLConnection connection) {
		connection.setConnectTimeout(NET_CONNECT_TIMEOUT);
		connection.setReadTimeout(NET_READ_TIMEOUT);
		connection.setDoInput(true);
		connection.setDefaultUseCaches(false);
	}

	private static void addHeader(HttpURLConnection connection, HashMap<String, String> headerMap) {
		for (Map.Entry<String, String> header : headerMap.entrySet()) {
			if (header.getKey().equalsIgnoreCase("cookie")) {
				connection.addRequestProperty(header.getKey(), header.getValue());
			} else {
				connection.setRequestProperty(header.getKey(), header.getValue());
			}
		}
	}

	private static String constructUrl(String requestUrl, HashMap<String, String> queryMap) {
		if (queryMap == null || queryMap.size() == 0)
			return requestUrl;

		StringBuilder builder = new StringBuilder(requestUrl);
		if (builder.indexOf("?") == -1) {
			builder.append('?');
		}
		if (builder.charAt(builder.length() - 1) != '?') {
			builder.append('&');
		}
		try {
			for (Map.Entry<String, String> entry : queryMap.entrySet()) {
				builder.append(entry.getKey()).append("=").append(URLEncoder.encode(entry.getValue(), "UTF-8"))
						.append('&');
			}
		} catch (UnsupportedEncodingException e) {
			e.printStackTrace();
		}
		builder.deleteCharAt(builder.length() - 1);
		return builder.toString();
	}

	/**
	 * 判断是否是gzip压缩流
	 */
	private static boolean isGzipStream(final HttpURLConnection connection) {
		String encoding = connection.getContentEncoding();
		return encoding != null && encoding.contains("gzip");
	}

	private static InputStream getInputStream(final HttpURLConnection connection) throws IOException {
		if (isGzipStream(connection)) {
			// 使用Gzip流方式进行读取
			return new GZIPInputStream(connection.getInputStream());
		}
		return connection.getInputStream();
	}

	private static byte[] readBuffer(InputStream in) {
		ByteArrayOutputStream baos = null;
		byte[] result = ByteArrayPool.getInstance().obtain(0);
		try {
			byte[] buffer = ByteArrayPool.getInstance().obtain(1024);
			int len;
			BufferedInputStream bis = new BufferedInputStream(in);
			baos = new ByteArrayOutputStream();
			while ((len = bis.read(buffer)) != -1) {
				baos.write(buffer, 0, len);
			}
			result = baos.toByteArray();
			baos.close();
		} catch (IOException e) {
			e.printStackTrace();
		} finally {
			closeIO(in, baos);
		}
		return result;
	}

	private static void closeIO(Closeable... cs) {
		if (cs != null && cs.length > 0) {
			try {
				for (Closeable c : cs) {
					if (c != null) {
						c.close();
					}
				}
			} catch (IOException ignored) {
			}
		}
	}

	public interface HttpRequest {

		Proxy configProxy();
	}

	public interface HttpCallback<T> {
		T convertBytes(byte[] data);

		void onFinish(int code, T data, Map<String, List<String>> headers);

		void onError(Throwable e);
	}

	public static boolean isSucc(int code) {
		return code >= 200 && code < 400;
	}
}
