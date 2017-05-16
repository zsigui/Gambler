package com.zzj.gambler.utils;

import java.awt.image.BufferedImage;
import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.IOException;

import javax.imageio.ImageIO;

public class VerifyCodeUtil {

	public static BufferedImage ByteArrayToImg(byte[] bs) {
		ByteArrayInputStream bais = new ByteArrayInputStream(bs);
		try {
			return ImageIO.read(bais);
		} catch (IOException e) {
			e.printStackTrace();
			return null;
		}
	}
	
	public static byte[] ImgToByteArray(BufferedImage img, String format) {
		ByteArrayOutputStream baos = new ByteArrayOutputStream();
		try {
			ImageIO.write(img, format, baos);
			return baos.toByteArray();
		} catch (IOException e) {
			e.printStackTrace();
			return null;
		}
	}
	
	public static String readCode(byte[] bs) {
		return "";
	}
}
