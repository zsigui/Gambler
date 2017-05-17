package com.zzj.gambler.utils;

import java.awt.Color;
import java.awt.image.BufferedImage;
import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.IOException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import javax.imageio.ImageIO;

public class VerifyCodeUtil {

	private static final int VAL_DIFF_COLOR = 650;
	private static final int RGB_WHITE = -1;
	private static final int RGB_BLACK = -16777216;
	/**
	 * 加载的训练数据的地址，修改位置需要改变此处
	 */
	private static final String DEFAULT_TRAIN_DIR_NAME = "trainData";
	
	private static HashMap<BufferedImage, String> sTrainMap = null;

	public static BufferedImage byteArrayToImg(byte[] bs) {
		ByteArrayInputStream bais = new ByteArrayInputStream(bs);
		try {
			return ImageIO.read(bais);
		} catch (IOException e) {
			e.printStackTrace();
			return null;
		}
	}

	public static byte[] imgToByteArray(BufferedImage img, String format) {
		ByteArrayOutputStream baos = new ByteArrayOutputStream();
		try {
			ImageIO.write(img, format, baos);
			return baos.toByteArray();
		} catch (IOException e) {
			e.printStackTrace();
			return null;
		}
	}


	/**
	 * 读取判断字节数组形式图片的验证码信息
	 */
	public static String readCode(byte[] bs) {
		String result = "";
		try {
			BufferedImage img = binarizationImage(byteArrayToImg(bs));
			List<BufferedImage> listImg = splitImage(img);
			Map<BufferedImage, String> map = loadTrainData(DEFAULT_TRAIN_DIR_NAME);
			for (BufferedImage bi : listImg) {
				if (bi != null) {
					result += getSingleCharOcr(bi, map);
				}
			}
		} catch (Throwable t) {
			t.printStackTrace();
		}
		return result;
	}
	
	/**
	 * 将验证码图片进行黑白二值化处理
	 * 
	 * @param src
	 * @return
	 */
	private static BufferedImage binarizationImage(BufferedImage img) {
		int width = img.getWidth();
		int height = img.getHeight();
		Color color;
		int rgb;

		// 遍历所有的像素点，根据色差分割值处理为黑白二色
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				rgb = img.getRGB(i, j);
				color = new Color(rgb);
				if (color.getBlue() + color.getRed() + color.getGreen() >= VAL_DIFF_COLOR) {
					img.setRGB(i, j, RGB_WHITE);
				} else {
					img.setRGB(i, j, RGB_BLACK);
				}
			}
		}

		removeNoise(img, width, height);
		return img;
	}

	private static void removeNoise(BufferedImage img, int width, int height) {
		int rgb;
		// 将1像素边框清为白色
		for (int i = 0; i < width; i++) {
			img.setRGB(i, 0, RGB_WHITE);
		}
		for (int i = 0; i < width; i++) {
			img.setRGB(i, height - 1, RGB_WHITE);
		}
		for (int j = 0; j < height; j++) {
			img.setRGB(0, j, RGB_WHITE);
		}
		for (int j = 0; j < height; j++) {
			img.setRGB(width - 1, j, RGB_WHITE);
		}
		width = width - 1;
		height = height - 1;
		// 清除图片里离散点（定义为周边8个格子里白格子数>6）
		for (int i = 1; i < width; i++) {
			for (int j = 1; j < height; j++) {
				rgb = img.getRGB(i, j);
				if (rgb == RGB_BLACK && calAroundCount(img, i, j) > 6) {
					img.setRGB(i, j, Color.WHITE.getRGB());
				}
			}
		}
	}

	private static int calAroundCount(BufferedImage img, int i, int j) {
		int count = 0;
		if (img.getRGB(i - 1, j - 1) == RGB_WHITE)
			count++;
		if (img.getRGB(i - 1, j) == RGB_WHITE)
			count++;
		if (img.getRGB(i - 1, j + 1) == RGB_WHITE)
			count++;
		if (img.getRGB(i, j - 1) == RGB_WHITE)
			count++;
		if (img.getRGB(i, j + 1) == RGB_WHITE)
			count++;
		if (img.getRGB(i + 1, j - 1) == RGB_WHITE)
			count++;
		if (img.getRGB(i + 1, j) == RGB_WHITE)
			count++;
		if (img.getRGB(i + 1, j + 1) == RGB_WHITE)
			count++;
		return count;
	}

	private static boolean isBlack(int colorInt) {
		Color color = new Color(colorInt);
		if (color.getRed() + color.getGreen() + color.getBlue() < VAL_DIFF_COLOR) {
			return true;
		}
		return false;
	}

	private static List<BufferedImage> splitImage(BufferedImage img) throws Exception {
		// 每个字符宽度最大不能超过 18

		ArrayList<BufferedImage> subImgs = new ArrayList<>();
		int maxWidth = 18;
		int minWidth = 10;

		int width = img.getWidth();
		int height = img.getHeight();
		int x = 1, y;
		boolean isBlackInColume;
		while (true) {
			int startX = width, startY = height, endX = 0, endY = 0;
			int spiltIndex = x + maxWidth;
			for (; x < width - 1; x++) {
				isBlackInColume = false;
				for (y = 1; y < height - 1; y++) {
					if (isBlack(img.getRGB(x, y))) {
						if (startY > y) {
							startY = y;
						} else if (endY < y) {
							endY = y;
						}
						isBlackInColume = true;
					}
				}

				if (isBlackInColume) {
					if (startX > x) {
						startX = x;
					} else if (endX < x) {
						endX = x;
					}
				} else {
					if (endX != 0 && startX != width)
						break;
				}

				// 限制字符的大小
				if (endX - startX > maxWidth - 2)
					break;
				// 规定范围内没有，表示该字符识别失败，要跳过了
				if (startX == width && x > spiltIndex) {
					break;
				}
			}
			if (endX != 0 && endY != 0) {
				subImgs.add(img.getSubimage(startX, startY, endX - startX + 1, endY - startY + 1));
			} else {
				subImgs.add(null);
			}
			// 表示结尾剩余的空白不够继续
			if (width - x < minWidth)
				break;
		}

		return subImgs;
	}

	/**
	 * 加载训练数据实例
	 */
	private static Map<BufferedImage, String> loadTrainData(String trainDirName) throws Exception {
		if (sTrainMap == null || sTrainMap.isEmpty()) {
			sTrainMap = new HashMap<BufferedImage, String>();
			File dir = new File(trainDirName);
			File[] files = dir.listFiles();
			for (File file : files) {
				sTrainMap.put(ImageIO.read(file), String.valueOf(file.getName().charAt(0)));
			}
		}
		return sTrainMap;
	}

	private static String getSingleCharOcr(BufferedImage img, Map<BufferedImage, String> map) {
		String result = "#";
		int width = img.getWidth();
		int height = img.getHeight();
		int min = width * height;
		for (BufferedImage bi : map.keySet()) {
			int count = 0;
			if (Math.abs(bi.getWidth() - width) > 2)
				continue;
			int widthmin = width < bi.getWidth() ? width : bi.getWidth();
			int heightmin = height < bi.getHeight() ? height : bi.getHeight();
			Label1: for (int x = 0; x < widthmin; ++x) {
				for (int y = 0; y < heightmin; ++y) {
					if (isBlack(img.getRGB(x, y)) != isBlack(bi.getRGB(x, y))) {
						count++;
						if (count >= min)
							break Label1;
					}
				}
			}
			if (count < min) {
				min = count;
				result = map.get(bi);
			}
		}
		return result;
	}

}
