﻿using Gambler.Utils.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Gambler.Utils;
using System.IO;

namespace Gambler.XPJ
{
    public class XPJVerifyCode : AbsVerifyCode
    {
        // 二值化的判断阈值
        private static readonly int VAL_DIFF_COLOR = 650;
        private static readonly int RGB_WHITE = -1;
        private static readonly int RGB_BLACK = -16777216;
        private static readonly string DEFAULT_TRAIN_DIR_NAME = "trainData";

        public Dictionary<Bitmap, char> sTrainDict;

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
                srcImg.SetPixel(width - 1, height, Color.White);
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
                    for (y = 1; y < height - 1; y++)
                    {
                        if (IsBlack(srcImg.GetPixel(x, y)))
                        {
                            if (startY > y)
                                startY = y;
                            else if (endY > y)
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
                    subImgs.Add(GetSubImage(srcImg, startX, startY, endX - startX + 1, endY - startY + 1));
                else
                    subImgs.Add(null);

                // 表示结尾的剩余空白不够继续
                if (width - x < minWidth)
                    break;
            }

            return subImgs;
        }

        private Bitmap GetSubImage(Bitmap srcImg, int x, int y, int width, int height)
        {
            Bitmap subBmp = new Bitmap(width, height);
            int srcW = x + width;
            int srcH = y + height;
            for (int i = 0; x < srcW; i++, x++)
            {
                for (int j = 0; y < srcH; j++, y++)
                {
                    subBmp.SetPixel(i, j, srcImg.GetPixel(x, y));
                }
            }
            return subBmp;
        }

        private Dictionary<Bitmap, char> LoadTrainData(string trainDir)
        {
            if (sTrainDict == null || sTrainDict.Count == 0)
            {
                sTrainDict = new Dictionary<Bitmap, char>();
                IEnumerable<string> paths = FileUtil.ReadFromPath(trainDir, new string[]{"png", "jpg", "jpeg"});
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
                if (Math.Abs(bmp.Width - width) > 2)
                    continue;
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
                            if (count >= min) {
                                jumpInnerBreak = true;
                                break;
                            }
                        }
                    }

                    if (jumpInnerBreak)
                        break;
                }

                if (count < min)
                {
                    min = count;
                    result = trainDict[bmp];
                }

            }
            return result;
        }

        public override string ParseCode(Stream stream)
        {
            Bitmap validBmp = new Bitmap(stream);
            List<Bitmap> bmpList = SpiltImage(validBmp);
            Dictionary<Bitmap, char> dict = LoadTrainData(DEFAULT_TRAIN_DIR_NAME);
            StringBuilder builder = new StringBuilder("");
            foreach (Bitmap bmp in bmpList)
            {
                if (bmp != null)
                    builder.Append(FindSinleCharOcr(bmp, dict));
            }
            return builder.ToString();
        }
    }
}
