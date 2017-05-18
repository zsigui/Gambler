package com.zzj.gambler.xinpujin;

/**
 * 帮助转换赔率
 * @author zsigui
 *
 */
public class XPJHelper {

	/**
	 * 获取香港深水盘口汇率
	 * @param hR
	 * @param cR
	 * @return 0 -> H 1 -> C
	 */
	private float[] getHK_ior(float hR, float cR) {
		float[] result = new float[2];
		if (hR <= 1000 && cR <= 1000) {
			result[0] = (float) (Math.floor(hR / 10 + 0.0001f) * 10);
			result[1] = (float) (Math.floor(cR / 10 + 0.0001f) * 10);
			return result;
		}
		float line = 2000 - (hR + cR);
		String nowType;
		float lowR, nowR, highR;
		if (hR > cR) {
			lowR = cR;
			nowType = "C";
		} else {
			lowR = hR;
			nowType = "H";
		}
		
		if (((2000 - line) - lowR) > 1000) {
			// 对盘马来盘
			nowR =  (lowR + line) * (-1);
		} else {
			nowR = (2000 - line) - lowR;
		}
		if (nowR < 0) {
			highR = (float) Math.floor(Math.abs(1000 / nowR) * 1000);
		} else {
			highR = (2000 - line - nowR);
		}
		if (nowType.equalsIgnoreCase("H")) {
			result[0] = (float) (Math.floor(lowR / 10 + 0.0001f) * 10);
			result[1] = (float) (Math.floor(highR / 10 + 0.0001f) * 10);
		} else {
			result[0] = (float) (Math.floor(highR / 10 + 0.0001f) * 10);
			result[1] = (float) (Math.floor(lowR / 10 + 0.0001f) * 10);
		}
		return result;
	}
	
	private float[] getIor(String oddType, float hR, float cR) {
		float[] result = new float[2];
		
		hR = (float) (Math.floor((hR * 1000) + 0.001) / 1000);
		cR = (float) (Math.floor((cR * 1000) + 0.001) / 1000);
		
		if (hR < 11) hR *= 1000;
		if (cR < 11) cR *= 1000;
		
		if ("H".equalsIgnoreCase(oddType)) {
			// 香港输水盘
			result = getHK_ior(hR, cR);
		} else if ("M".equalsIgnoreCase(oddType)) {
			// 马来盘
		} else if ("I".equalsIgnoreCase(oddType)) {
			// 印尼盘
		} else if ("E".equalsIgnoreCase(oddType)) {
			// 欧洲盘
		} else {
			// 香港盘
			result[0] = hR;
			result[1] = cR;
		}
		
		result[0] /= 1000;
		result[1] /= 1000;
		
		return result;
	}
	
	/**
	 * 根据盘口及获取到的基础赔率转换为对应的真正赔率
	 * @param oddType 盘口类型
	 * @param hR 主队赔率
	 * @param cR 客队赔率
	 * @return 下标0：hR 下标1：cR
	 */
	public String[] getIor(String oddType, String hR, String cR) {
		String[] result = new String[2];
		if (hR != null && !hR.isEmpty()
				&& cR != null && !cR.isEmpty()) {
			float[] rs = getIor(oddType, Float.parseFloat(hR), Float.parseFloat(cR));
			result[0] = String.valueOf(rs[0]);
			result[1] = String.valueOf(rs[1]);
		} else {
			result[0] = "";
			result[1] = "";
		}
		return result;
	}
}
