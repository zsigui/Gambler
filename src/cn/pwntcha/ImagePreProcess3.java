package cn.pwntcha;

import java.awt.Color;
import java.awt.color.ColorSpace;
import java.awt.image.BufferedImage;
import java.awt.image.ColorConvertOp;
import java.io.File;
import java.io.FileOutputStream;
import java.io.InputStream;
import java.io.OutputStream;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import javax.imageio.ImageIO;

import org.apache.commons.httpclient.HttpClient;
import org.apache.commons.httpclient.HttpStatus;
import org.apache.commons.httpclient.methods.GetMethod;
import org.apache.commons.io.IOUtils;

public class ImagePreProcess3 {

	private static Map<BufferedImage, String> trainMap = null;
	private static int index = 0;

	private static final int VAL_DIFF_COLOR = 650;
	private static final int RGB_WHITE = -1;
	private static final int RGB_BLACK = -16777216;

	public static boolean isBlack(int colorInt) {
		Color color = new Color(colorInt);
		if (color.getRed() + color.getGreen() + color.getBlue() < VAL_DIFF_COLOR) {
			return true;
		}
		return false;
	}

	public static boolean isWhite(int colorInt) {
		Color color = new Color(colorInt);
		if (color.getRed() + color.getGreen() + color.getBlue() >= VAL_DIFF_COLOR) {
			return true;
		}
		return false;
	}

	public static BufferedImage removeBackgroud(String picFile) throws Exception {
		BufferedImage img = ImageIO.read(new File(picFile));

		int width = img.getWidth();
		int height = img.getHeight();
		Color color;
		int rgb;
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

		// 去除离散点

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
		int count;
		// rgb(BLACK) == -16777216 , rgb(WHITE) == -1
		for (int i = 1; i < width; i++) {
			for (int j = 1; j < height; j++) {
				rgb = img.getRGB(i, j);
				if (rgb == RGB_BLACK && calAround(img, i, j) > 6) {
					img.setRGB(i, j, Color.WHITE.getRGB());
				}
			}
		}
		return img;
	}

	private static int calAround(BufferedImage img, int i, int j) {
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

	public static BufferedImage removeBlank(BufferedImage img) throws Exception {
		int width = img.getWidth();
		int height = img.getHeight();
		int start = 0;
		int end = 0;
		Label1: for (int y = 0; y < height; ++y) {
			for (int x = 0; x < width; ++x) {
				if (isBlack(img.getRGB(x, y))) {
					start = y;
					break Label1;
				}
			}
		}
		Label2: for (int y = height - 1; y >= 0; --y) {
			for (int x = 0; x < width; ++x) {
				if (isBlack(img.getRGB(x, y))) {
					end = y;
					break Label2;
				}
			}
		}
		return img.getSubimage(0, start, width, end - start + 1);
	}

	public static List<BufferedImage> splitImage(BufferedImage img, String filename, List<Integer> skipIndex) throws Exception {
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
				if (endX - startX > maxWidth - 2) break;
				// 规定范围内没有，表示该字符识别失败，要跳过了
				if (startX == width && x > spiltIndex) {
					if (skipIndex != null)
						skipIndex.add(x / maxWidth - 1);
					System.out.println(filename +  "跳过的下标" + (x / maxWidth - 1));
					break;
				}
			}
			if (endX != 0 && endY != 0) {
				subImgs.add(img.getSubimage(startX, startY, endX - startX + 1, endY - startY + 1));
			} else {
				subImgs.add(null);
			}
			// 表示结尾剩余的空白不够继续
			if (width - x <  minWidth) break;
		}
		
		return subImgs;
	}

	public static Map<BufferedImage, String> loadTrainData() throws Exception {
		if (trainMap == null) {
			Map<BufferedImage, String> map = new HashMap<BufferedImage, String>();
			File dir = new File("train");
			File[] files = dir.listFiles();
			for (File file : files) {
				map.put(ImageIO.read(file), file.getName().charAt(0) + "");
			}
			trainMap = map;
		}
		return trainMap;
	}

	public static String getSingleCharOcr(BufferedImage img, Map<BufferedImage, String> map) {
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

	public static String getAllOcr(String file) throws Exception {
		BufferedImage img = removeBackgroud(file);
		List<BufferedImage> listImg = splitImage(img, file.substring(file.lastIndexOf("/") + 1), null);
		Map<BufferedImage, String> map = loadTrainData();
		String result = "";
		for (BufferedImage bi : listImg) {
			if (bi != null) {
				result += getSingleCharOcr(bi, map);
			}
		}
		ImageIO.write(img, "JPG", new File("result", result + ".jpg"));
		return result;
	}

	public static void downloadImage() {
		HttpClient httpClient = new HttpClient();
		GetMethod getMethod = new GetMethod("http://www.1559501.com/verifycode.do?timestamp=1495001869307");
		for (int i = 0; i < 30; i++) {
			try {
				// 执行getMethod
				int statusCode = httpClient.executeMethod(getMethod);
				if (statusCode != HttpStatus.SC_OK) {
					System.err.println("Method failed: " + getMethod.getStatusLine());
				}
				// 读取内容
				String picName = "img2/" + i + ".jpg";
				InputStream inputStream = getMethod.getResponseBodyAsStream();
				OutputStream outStream = new FileOutputStream(picName);
				IOUtils.copy(inputStream, outStream);
				outStream.close();
				System.out.println(i + "OK!");
			} catch (Exception e) {
				e.printStackTrace();
			} finally {
				// 释放连接
				getMethod.releaseConnection();
			}
		}
	}

	public static void trainData() throws Exception {
		File dir = new File("temp");
		File[] files = dir.listFiles();
		List<Integer> skipIndex = new ArrayList<Integer>();
		for (File file : files) {
			skipIndex.clear();
			BufferedImage img = removeBackgroud("temp/" + file.getName());
			List<BufferedImage> listImg = splitImage(img, file.getName(), skipIndex);
			for (int j = 0; j < listImg.size(); ++j) {
				if (listImg.get(j) != null)
					ImageIO.write(listImg.get(j), "JPG", new File("train", file.getName().charAt(j) + "-" + (index++)
							+ ".jpg"));
			}
		}
	}

	/**
	 * @param args
	 * @throws Exception
	 */
	public static void main(String[] args) throws Exception {
//		trainData();
		 downloadImage();
//		 File dir = new File("img2");
//		 for (File img : dir.listFiles()) {
//		 String text = getAllOcr(img.getAbsolutePath());
//		 System.out.println(img.getName() + " = " + text);
//		 }
		/*
		 * for (int i = 0; i < 30; ++i) { String text = getAllOcr("img3/" + i +
		 * ".jpg"); System.out.println(i + ".jpg = " + text); }
		 */
	}
}
