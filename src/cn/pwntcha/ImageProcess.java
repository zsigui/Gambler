package cn.pwntcha;
import java.awt.Color;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.FileFilter;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import javax.imageio.ImageIO;
//
//import org.apache.http.HttpEntity;
//import org.apache.http.HttpResponse;
//import org.apache.http.client.HttpClient;
//import org.apache.http.client.methods.HttpGet;
//import org.apache.http.impl.client.DefaultHttpClient;
//import org.apache.http.protocol.BasicHttpContext;
//

/**
 * ����һ���Զ�ʶ����֤��ĳ���Ҫ���Ǽ򵥵���֤�룬�̶���С���̶�λ�ã��̶����壻���崿ɫ��ã��粻����Ҫ�޸Ĵ��롣
 * 
 * @author acer
 *
 */
public class ImageProcess {
    // �������������֤���Ŀ¼
    private static final String DOWNLOAD_DIR = "temp";

    // ����Ѿ���ֿ��ĵ�������ͼƬ��Ŀ¼�����ȶ���
    private static final String TRAIN_DIR = "train";

    // ��űȶԽ����Ŀ¼����������֤���������������ļ����ǳ�ֱ�ۣ�
    private static final String RESULT_DIR = "result";

    // ��űȶ�ͼƬ��������ֵ�Map
    private static Map<BufferedImage, String> trainMap = new HashMap<BufferedImage, String>();
    
    // ͼƬ����������Ҫʲô����ͼƬ���������Ƽ��ɡ��磺jpg/gif/.png
    static class ImageFileFilter implements FileFilter {
        private String postfix = ".jpg";
        
        public ImageFileFilter(String postfix) {
            if(!postfix.startsWith("."))
                postfix = "." + postfix;
            
            this.postfix = postfix;
        }
        
        @Override
        public boolean accept(File pathname) {
            return pathname.getName().toLowerCase().endsWith(postfix);
        }
    }

    static {
        try {
            // ��TRAIN_DIRĿ¼�Ĺ��ȶԵ�ͼƬװ�ؽ���
            File dir = new File(TRAIN_DIR);
            File[] files = dir.listFiles(new ImageFileFilter("jpg"));
            for (File file : files) {
                trainMap.put(ImageIO.read(file), file.getName().charAt(0) + "");
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    // 1.������֤�룺�������֤��ͼƬ���ص�ָ��Ŀ¼��Ҫ����ֿ��ܵ���֤�루�������֣���Ӧ���У����磺0-9��
//    private void downloadImage() throws Exception {
//        HttpClient httpClient = new DefaultHttpClient();
//        for (int i = 0; i < 10; i++) {
//            String url = "http://www.yoursite.com/yz.php";
//            HttpGet getMethod = new HttpGet(url);
//            try {
//                HttpResponse response = httpClient.execute(getMethod, new BasicHttpContext());
//                HttpEntity entity = response.getEntity();
//                InputStream instream = entity.getContent(); 
//                OutputStream outstream = new FileOutputStream(new File(DOWNLOAD_DIR, i + ".jpg"));
//                int l = -1;
//                byte[] tmp = new byte[2048]; 
//                while ((l = instream.read(tmp)) != -1) {
//                    outstream.write(tmp);
//                } 
//                outstream.close();
//            } finally {
//                getMethod.releaseConnection();
//            }
//        }
//
//        System.out.println("������֤����ϣ�");
//    }
    
    // 2.ȥ��ͼ��������أ��Ǳ��������ֻ�ǿ�����߾��ȶ��ѣ���
    public static BufferedImage removeInterference(BufferedImage image)  
            throws Exception {  
        int width = image.getWidth();  
        int height = image.getHeight();  
        for (int x = 0; x < width; ++x) {  
            for (int y = 0; y < height; ++y) {  
                if (isFontColor(image.getRGB(x, y))) {
                    // �����ǰ����������ɫ�������ܱ��Ƿ�Ϊ��ɫ���綼����ɾ�������ء�
                    int roundWhiteCount = 0;
                    if(isWhiteColor(image, x+1, y+1))
                        roundWhiteCount++;
                    if(isWhiteColor(image, x+1, y-1))
                        roundWhiteCount++;
                    if(isWhiteColor(image, x-1, y+1))
                        roundWhiteCount++;
                    if(isWhiteColor(image, x-1, y-1))
                        roundWhiteCount++;
                    if(roundWhiteCount == 4) {
                        image.setRGB(x, y, Color.WHITE.getRGB());  
                    }
                } 
            }  
        }  
        return image;  
     }
    
    // ȡ��ָ��λ�õ���ɫ�Ƿ�Ϊ��ɫ����������߽磬����true
    // �������Ǵ�removeInterference������ժȡ�����ġ��������ñ����������塣
    private static boolean isWhiteColor(BufferedImage image, int x, int y) throws Exception {
        if(x < 0 || y < 0) return true;
        if(x >= image.getWidth() || y >= image.getHeight()) return true;

        Color color = new Color(image.getRGB(x, y));
        
        return color.equals(Color.WHITE)?true:false;
    }

    // 3.�жϲ����֤��ı�׼�����Ƕ�����֤���а����ĸ����ֵ�x��y����ֵ�������ǵĿ�ȣ�width�����߶ȣ�height����
    private static List<BufferedImage> splitImage(BufferedImage image) throws Exception {
        final int DIGIT_WIDTH = 19;
        final int DIGIT_HEIGHT = 17;

        List<BufferedImage> digitImageList = new ArrayList<BufferedImage>();
        digitImageList.add(image.getSubimage(2, 2, DIGIT_WIDTH, DIGIT_HEIGHT));
        digitImageList.add(image.getSubimage(20, 2, DIGIT_WIDTH, DIGIT_HEIGHT));
        digitImageList.add(image.getSubimage(40, 2, DIGIT_WIDTH, DIGIT_HEIGHT));
        digitImageList.add(image.getSubimage(60, 2, DIGIT_WIDTH, DIGIT_HEIGHT));

        return digitImageList;
    }

    // 4.�ж��������ɫ���壺����������rgb������ɫ��������ʾ���������Ӧ������ʾ�������ҳ�����
    private static boolean isFontColor(int colorInt) {
        Color color = new Color(colorInt);

        return color.getRed() + color.getGreen() + color.getBlue() == 340;
    }


    // 5.�����ص���֤��ͼƬȫ����ֵ���һ��Ŀ¼��
    public void generateStdDigitImgage() throws Exception {
        File dir = new File(DOWNLOAD_DIR);
        File[] files = dir.listFiles(new ImageFileFilter("jpg"));
        
        int counter = 0;
        for (File file : files) {
            BufferedImage image = ImageIO.read(file);
            removeInterference(image); 
            List<BufferedImage> digitImageList = splitImage(image);
            for (int i = 0; i < digitImageList.size(); i++) {
                BufferedImage bi = digitImageList.get(i);
                ImageIO.write(bi, "JPG", new File(TRAIN_DIR, "temp_" + counter++ + ".jpg"));
            }
        }
        System.out.println("���ɹ��ȶԵ�ͼƬ��ϣ��뵽Ŀ¼���ֹ�ʶ��������ͼƬ����ɾ�������޹�ͼƬ��");
    }
    

    // 7.�����ж�Ч�������з��������Ե���rgb��ֵ���Դﵽ�ߵķֱ��ʡ�
    // Ŀǰ�˷����ṩ������жϽ����ͬʱ����Ŀ��Ŀ¼�������жϽ������������֤��ͼƬ�����������Ч����
    public void testDownloadImage() throws Exception {
        File dir = new File(DOWNLOAD_DIR);
        File[] files = dir.listFiles(new ImageFileFilter("jpg"));
        
        for (File file : files) {
            String validateCode = getValidateCode(file);
            System.out.println(file.getName() + "=" + validateCode);
        }
        
        System.out.println("�ж���ϣ��뵽���Ŀ¼���Ч����");
    }
    
    /**
     * 8.�ṩ�����ӿڵ��á�
     * @param file
     * @return
     * @throws Exception
     */
    public static String getValidateCode(File file) throws Exception {
        // װ��ͼƬ
        BufferedImage image = ImageIO.read(file);
        removeInterference(image);
        // ���ͼƬ
        List<BufferedImage> digitImageList = splitImage(image);

        // ѭ��ÿһλ����ͼ���бȶ�
        StringBuilder sb = new StringBuilder();
        for (BufferedImage digitImage : digitImageList) {
            String result = "";
            int width = digitImage.getWidth();
            int height = digitImage.getHeight();
            
            // ��С�Ĳ�ͬ��������ʼֵΪ�����أ���ֵԽС��Խ��
            int minDiffCount = width * height;
            for (BufferedImage bi : trainMap.keySet()) {
                // ��ÿһλ����ͼ���ֵ��еĽ��а����رȽ�
                int currDiffCount = 0; // �����رȽϲ�ͬ�Ĵ���
                outer : for (int x = 0; x < width; ++x) {
                    for (int y = 0; y < height; ++y) {
                        if (isFontColor(digitImage.getRGB(x, y)) != isFontColor(bi.getRGB(x, y))) {
                            // �����رȽ������ͬ�����1��
                            currDiffCount++;
                            // ���ֵ����minDiffCount�������ٱȽ��ˣ���Ϊ����Ҫ����С��minDiffCount��
                            if (currDiffCount >= minDiffCount) 
                                break outer;
                        }
                    }
                }
                if (currDiffCount < minDiffCount) {
                    // ����˭�����С��������ʱ��ֵ�������
                    minDiffCount = currDiffCount;
                    result = trainMap.get(bi);
                }
            }
            sb.append(result);
        }        
        ImageIO.write(image, "JPG", new File(RESULT_DIR, sb.toString() + ".jpg"));
        
        return sb.toString();
    }

    public static void main(String[] args) throws Exception {
        ImageProcess ins = new ImageProcess();
        
        // ��1����������֤�뵽DOWNLOAD_DIR
//        ins.downloadImage();
        
        // ��2����ȥ�����ŵ�����
        File dir = new File(DOWNLOAD_DIR);
        File[] files = dir.listFiles();
        for (File file : files) {
            BufferedImage image = ImageIO.read(file);
            removeInterference(image);
            ImageIO.write(image, "JPG", file);
            System.out.println("�ɹ�����" + file.getName());
        }
        
        // ��3�����жϲ����֤��ı�׼
        // ͨ��PhotoShop����֤�벢�Ŵ�۲죬������Ľ���ο�splitImage()�����еı���
        
        // ��4�����ж��������ɫ����
        // ͨ��PhotoShop����֤�벢�Ŵ�۲죬�����������ɫ��rgb��ֵ��������340����Ϊ�Ǵ�ɫ��
        
        // ��5���������ص���֤��ͼƬȫ����ֵ�TRAIN_DIRĿ¼��
//        ins.generateStdDigitImgage();
        
        // ��6�����ֹ������ļ�
        // ����Դ��������ѡ��TRAIN_DIR���ֱ��ҳ���ʾ0-9���ֵ��ļ�����������������������ɾ���������еġ�
        
        // ��7���������ж�Ч�������к��RESULT_DIR������ļ����Ƿ�����֤������һ�¡�
        ins.testDownloadImage();
        
        // ��8�����ṩ�����ӿڵ��á�
//        String validateCode = ImageProcess.getValidateCode(new File(DOWNLOAD_DIR, "0.jpg"));
//        System.out.println("��֤��Ϊ��" + validateCode);
    }
}
