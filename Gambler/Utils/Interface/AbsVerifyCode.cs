using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace Gambler.Utils.Interface
{
    public class AbsVerifyCode : IVerifyCode
    {
        // 二值化的判断阈值
        protected int VAL_DIFF_COLOR = 650;
        private static readonly int RGB_WHITE = -1;
        private static readonly int RGB_BLACK = -16777216;
        private static readonly string DEFAULT_TRAIN_DIR_NAME = "trainData";

        private Dictionary<Bitmap, char> sTrainDict;
        private string _trainDataPath;

        public AbsVerifyCode(string trainDataPath)
        {
            if (trainDataPath == null || trainDataPath == "")
                this._trainDataPath = DEFAULT_TRAIN_DIR_NAME;
            else
                this._trainDataPath = trainDataPath;
        }

        public Bitmap Binarization(Bitmap srcImg)
        {
            int width = srcImg.Width;
            int height = srcImg.Height;

            Color color;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    color = srcImg.GetPixel(x, y);
                    if (IsBlack(color))
                    {
                        srcImg.SetPixel(x, y, Color.Black);
                    }
                    else
                    {
                        srcImg.SetPixel(x, y, Color.White);
                    }
                }
            }
            return srcImg;
        }

        public Bitmap RemoveNoise(Bitmap srcImg)
        {

            int x, y;

            int width = srcImg.Width, height = srcImg.Height;
            for (x = 0; x < width; x++)
            {
                srcImg.SetPixel(x, 0, Color.White);
                srcImg.SetPixel(x, height - 1, Color.White);
            }
            for (y = 0; y < height; y++)
            {
                srcImg.SetPixel(0, y, Color.White);
                srcImg.SetPixel(width - 1, y, Color.White);
            }

            width -= 1;
            height -= 1;
            Color color;
            // 清除图片里离散点（定义为周边8个格子里白格子数>6）
            for (x = 1; x < width; x++)
            {
                for (y = 1; y < height; y++)
                {
                    color = srcImg.GetPixel(x, y);
                    if (color == Color.Black && CalculateArroundCount(srcImg, x, y) > 6)
                    {
                        srcImg.SetPixel(x, y, Color.White);
                    }
                }
            }

            return srcImg;
        }

        private int CalculateArroundCount(Bitmap srcImg, int x, int y)
        {
            int count = 0;
            if (srcImg.GetPixel(x - 1, y - 1) == Color.White)
                count++;
            if (srcImg.GetPixel(x - 1, y) == Color.White)
                count++;
            if (srcImg.GetPixel(x - 1, y + 1) == Color.White)
                count++;
            if (srcImg.GetPixel(x, y - 1) == Color.White)
                count++;
            if (srcImg.GetPixel(x, y + 1) == Color.White)
                count++;
            if (srcImg.GetPixel(x + 1, y - 1) == Color.White)
                count++;
            if (srcImg.GetPixel(x + 1, y) == Color.White)
                count++;
            if (srcImg.GetPixel(x + 1, y + 1) == Color.White)
                count++;
            return count;
        }

        private bool IsBlack(Color color)
        {
            return color.R + color.B + color.G < VAL_DIFF_COLOR;
        }

        private List<Bitmap> SpiltImage(Bitmap srcImg)
        {
            List<Bitmap> subImgs = new List<Bitmap>();
            int maxWidth = 18;
            int minWidth = 10;

            int width = srcImg.Width;
            int height = srcImg.Height;
            int rwidth = width - 1, rheight = height - 1;
            int x = 1, y;
            bool isBlackInColume;

            int startX, startY, endX, endY, spiltIndex;

            int k = 0;
            while (true)
            {
                startX = width;
                startY = height;
                endX = 0;
                endY = 0;
                spiltIndex = x + maxWidth;

                for (; x < rwidth; x++)
                {
                    isBlackInColume = false;
                    for (y = 1; y < rheight; y++)
                    {
                        if (IsBlack(srcImg.GetPixel(x, y)))
                        {
                            if (startY > y)
                                startY = y;
                            else if (endY < y)
                                endY = y;

                            isBlackInColume = true;
                        }
                    }

                    if (isBlackInColume)
                    {
                        if (startX > x)
                            startX = x;
                        else if (endX < x)
                            endX = x;
                    }
                    else
                    {
                        if (endX != 0 && startX != width)
                            break;
                    }

                    // 限制字符大小
                    if (endX - startX > maxWidth - 2)
                        break;
                    // 规定范围内没有，表示字符识别失败，要跳了
                    if (startX == width && x > spiltIndex)
                        break;
                }


                if (endX != 0 && endY != 0)
                {
                    Bitmap b = GetSubImage(srcImg, startX, startY, endX - startX + 1, endY - startY + 1);
                    subImgs.Add(b);
                }
                else
                {
                    subImgs.Add(null);
                }
                // 表示结尾的剩余空白不够继续
                if (width - x < minWidth)
                    break;
            }

            return subImgs;
        }

        private Bitmap GetSubImage(Bitmap srcImg, int x, int y, int width, int height)
        {
            Bitmap subBmp = new Bitmap(width, height);
            for (int i = 0, u = x; i < width; i++, u++)
            {
                for (int j = 0, v = y; j < height; j++, v++)
                {
                    subBmp.SetPixel(i, j, srcImg.GetPixel(u, v));
                }
            }
            return subBmp;
        }

        private Dictionary<Bitmap, char> LoadTrainData(string trainDir)
        {
            if (sTrainDict == null || sTrainDict.Count == 0)
            {
                sTrainDict = new Dictionary<Bitmap, char>();
                IEnumerable<string> paths = FileUtil.ReadFromPath(trainDir, new string[] { ".png", ".jpg", ".jpeg" });
                string filename;
                foreach (string path in paths)
                {
                    sTrainDict.Add(ImageUtil.Read(path), Path.GetFileName(path).ToCharArray()[0]);
                }

            }
            return sTrainDict;
        }

        private char FindSinleCharOcr(Bitmap srcImg, Dictionary<Bitmap, char> trainDict)
        {
            char result = '_';
            int width = srcImg.Width;
            int height = srcImg.Height;
            int min = width * height;
            int count, minW, minH, subW, subH, x, y;
            bool jumpInnerBreak;
            foreach (Bitmap bmp in trainDict.Keys)
            {
                count = 0;
                subW = bmp.Width;
                subH = bmp.Height;
                if (Math.Abs(subW - width) > 2)
                {
                    continue;
                }
                minW = Math.Min(subW, width);
                minH = Math.Min(subH, height);

                // 指示以下跳转内部循环
                jumpInnerBreak = false;
                for (x = 0; x < minW; x++)
                {
                    for (y = 0; y < minH; y++)
                    {
                        if (IsBlack(srcImg.GetPixel(x, y)) != IsBlack(bmp.GetPixel(x, y)))
                        {
                            count++;
                            if (count >= min)
                            {
                                jumpInnerBreak = true;
                                break;
                            }
                        }
                    }

                    if (jumpInnerBreak)
                    {
                        break;
                    }
                }

                if (count < min)
                {
                    min = count;
                    result = trainDict[bmp];
                }

            }
            return result;
        }

        public string ParseCode(byte[] imgBytes)
        {
            Bitmap validBmp = ImageUtil.BytesToBitmap(imgBytes);
            Console.WriteLine("ParseCode 成功将 byte[] 转换为 Bitmap");
            // 二值化处理
            validBmp = Binarization(validBmp);
            // 消除噪声点
            validBmp = RemoveNoise(validBmp);
            // 进行图形切割
            List<Bitmap> bmpList = SpiltImage(validBmp);
            Dictionary<Bitmap, char> dict = LoadTrainData(this._trainDataPath);
            StringBuilder builder = new StringBuilder("");
            foreach (Bitmap bmp in bmpList)
            {
                if (bmp != null)
                {
                    builder.Append(FindSinleCharOcr(bmp, dict));
                }
            }

            string result = builder.ToString();
            if (result.Contains("_"))
                return "";
            return result;
        }

        public void TrainData(string trainPath, string outputPath)
        {
            if (!Directory.Exists(trainPath))
            {
                Console.WriteLine("找不到文件夹: " + trainPath);
                return;
            }
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }

            Bitmap tmpBmp;
            List<Bitmap> subImgs;
            int index = 0;
            foreach (string fImg in FileUtil.ReadFromPath(trainPath, new string[] { ".jpg", ".jpeg", ".png" }))
            {
                tmpBmp = Binarization(ImageUtil.Read(fImg));
                subImgs = SpiltImage(tmpBmp);
                tmpBmp.Dispose();
                for (int j = 0; j < subImgs.Count; j++)
                {
                    tmpBmp = subImgs[j];
                    if (tmpBmp != null)
                        ImageUtil.Write(tmpBmp, String.Format("{0}\\{1}-{2}.jpg", outputPath, Path.GetFileName(fImg).ToCharArray()[j], (index ++)));
                }
            }

        }

        //         protected Dictionary<string, string> ConstructKeyValDict(params string[] data)
        //         {
        //             Dictionary<string, string> dict = new Dictionary<string, string>();
        //             int len = data.Length - 1;
        //             for (int i = 0; i < len; i += 2)
        //             {
        //                 dict.Add(data[i], data[i + 1]);
        //             }
        //             return dict;
        //         }
        //         private void DownloadHFValid()
        //         {
        //             ThreadUtil.RunOnThread(() =>
        //             {
        //                 Console.WriteLine("开始下载!");
        //                 for (int i = 0; i < 30; i++)
        //                 {
        //                     Dictionary<string, string> queryDict = ConstructKeyValDict("s", "" + new Random().NextDouble());
        // 
        //                     HttpUtil.Get<byte[]>(HFConfig.URL_VERICODE, null, null, queryDict,
        //                        (data) =>
        //                        {
        //                            return IOUtil.Read(data);
        //                        },
        //                        (statusCode, data, cookies) =>
        //                        {
        //                        if (HttpUtil.IsCodeSucc(statusCode) && data != null)
        //                        {
        //                            string dir = Application.StartupPath + "\\Resources\\Download";
        //                            if (!Directory.Exists(dir))
        //                                Directory.CreateDirectory(dir);
        // 
        //                            using (FileStream fs = File.OpenWrite(dir + "\\" + i + ".jpg"))
        //                            {
        //                                fs.Write(data, 0, data.Length);
        //                                fs.Flush();
        //                            }
        //                            Console.WriteLine(i + "的验证码：" + vedo.ParseCode(data));
        //                                Console.WriteLine(i + " Finished!");
        //                            }
        // 
        //                        }, null);
        //                 }
        //             });
        //         }
    }
}
