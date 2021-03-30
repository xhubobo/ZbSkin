using System.Drawing;
using System.Drawing.Imaging;

namespace ZbSkin.Tools
{
    public static class ImageHelper
    {
        /// <summary>
        /// 马赛克处理
        /// </summary>
        /// <param name="bitmap">位图</param>
        /// <param name="startX">起始坐标</param>
        /// <param name="effectWidth">影响范围 每一个格子数</param>
        /// <returns>处理后的位图</returns>
        public static Bitmap AdjustTobMosaic(Bitmap bitmap, int startX, int effectWidth)
        {
            if (startX >= bitmap.Width)
            {
                return bitmap;
            }

            if (startX < 0)
            {
                startX = 0;
            }

            // 差异最多的就是以照一定范围取样 玩之后直接去下一个范围
            for (var heightOfffset = 0; heightOfffset < bitmap.Height; heightOfffset += effectWidth)
            {
                for (var widthOffset = startX; widthOffset < bitmap.Width; widthOffset += effectWidth)
                {
                    int avgR = 0, avgG = 0, avgB = 0;
                    var blurPixelCount = 0;

                    for (var x = widthOffset; (x < widthOffset + effectWidth && x < bitmap.Width); x++)
                    {
                        for (var y = heightOfffset; (y < heightOfffset + effectWidth && y < bitmap.Height); y++)
                        {
                            var pixel = bitmap.GetPixel(x, y);

                            avgR += pixel.R;
                            avgG += pixel.G;
                            avgB += pixel.B;

                            blurPixelCount++;
                        }
                    }

                    // 计算范围平均
                    avgR /= blurPixelCount;
                    avgG /= blurPixelCount;
                    avgB /= blurPixelCount;

                    // 所有范围内都设定此值
                    for (var x = widthOffset; (x < widthOffset + effectWidth && x < bitmap.Width); x++)
                    {
                        for (var y = heightOfffset; (y < heightOfffset + effectWidth && y < bitmap.Height); y++)
                        {
                            var newColor = Color.FromArgb(avgR, avgG, avgB);
                            bitmap.SetPixel(x, y, newColor);
                        }
                    }
                }
            }

            return bitmap;
        }

        /// <summary>
        /// 获取位图中ARGB值最小像素的颜色值
        /// </summary>
        /// <param name="bmp">位图</param>
        /// <param name="bmpWidth">位图处理宽度</param>
        /// <param name="colorWidth">处理颜色的宽度</param>
        /// <returns>目标颜色</returns>
        public static Color GetMinColor(Bitmap bmp, int bmpWidth, int colorWidth)
        {
            var minColor = new Color();
            var minColorValue = int.MaxValue;

            for (var i = 0; i < colorWidth; i++)
            {
                for (var j = 0; j < bmp.Height; j++)
                {
                    var color = bmp.GetPixel(bmpWidth - i - 1, j);
                    var colorValue = color.ToArgb();
                    if (colorValue < minColorValue)
                    {
                        minColorValue = colorValue;
                        minColor = color;
                    }
                }
            }

            return minColor;
        }

        /// <summary>
        /// 获取位图中ARGB值最大像素的颜色值
        /// </summary>
        /// <param name="bmp">位图</param>
        /// <param name="bmpWidth">位图处理宽度</param>
        /// <param name="colorWidth">处理颜色的宽度</param>
        /// <returns>目标颜色</returns>
        public static Color GetMaxColor(Bitmap bmp, int bmpWidth, int colorWidth)
        {
            var maxColor = new Color();
            var maxColorValue = int.MinValue;

            for (var i = 0; i < colorWidth; i++)
            {
                for (var j = 0; j < bmp.Height; j++)
                {
                    var color = bmp.GetPixel(bmpWidth - i - 1, j);
                    var colorValue = color.ToArgb();
                    if (colorValue > maxColorValue)
                    {
                        maxColorValue = colorValue;
                        maxColor = color;
                    }
                }
            }

            return maxColor;
        }

        /// <summary>
        /// 获取位图中的平均颜色值
        /// </summary>
        /// <param name="bmp">位图</param>
        /// <param name="bmpWidth">位图处理宽度</param>
        /// <param name="colorWidth">处理颜色的宽度</param>
        /// <returns>目标颜色</returns>
        public static Color GetAvgColor(Bitmap bmp, int bmpWidth, int colorWidth)
        {
            Color color;
            var sumA = 0;
            var sumR = 0;
            var sumG = 0;
            var sumB = 0;

            for (int i = 0; i < colorWidth; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    color = bmp.GetPixel(bmpWidth - i - 1, j);
                    sumA += color.A;
                    sumR += color.R;
                    sumG += color.G;
                    sumB += color.B;
                }
            }

            color = Color.FromArgb(
                (byte) (sumA / colorWidth / bmp.Height),
                (byte) (sumR / colorWidth / bmp.Height),
                (byte) (sumG / colorWidth / bmp.Height),
                (byte) (sumB / colorWidth / bmp.Height));
            return color;
        }

        /// <summary>
        /// 绘制9宫格图像
        /// </summary>
        /// <param name="g">Graphics对象</param>
        /// <param name="image">9宫格原始图像</param>
        /// <param name="dstWidth">目标图形宽度</param>
        /// <param name="dstHeight">目标图形高度</param>
        /// <param name="s9Width">9宫格图像保留宽度</param>
        /// <param name="s9Height">9宫格图像保留高度</param>
        /// <param name="alpha">透明通道值</param>
        public static void Draw9Scale(Graphics g, Image image,
            int dstWidth, int dstHeight, int s9Width, int s9Height, int alpha)
        {
            if (image == null)
            {
                return;
            }

            //9宫格
            var width = s9Width;
            var height = s9Height;
            var width1 = image.Width - width * 2;
            var height1 = image.Height - height * 2;
            var width2 = dstWidth - width * 2;
            var height2 = dstHeight - height * 2;

            //透明属性
            var imageAttributes = SetPictureAlpha(alpha);

            //左上
            g.DrawImage(image, new Rectangle(0, 0, width, height),
                0, 0, width, height, GraphicsUnit.Pixel, imageAttributes);
            //右上
            g.DrawImage(image, new Rectangle(width + width2, 0, width, height),
                width + width1, 0, width, height, GraphicsUnit.Pixel, imageAttributes);
            //左下
            g.DrawImage(image, new Rectangle(0, height + height2, width, height),
                0, height + height1, width, height, GraphicsUnit.Pixel, imageAttributes);
            //右下
            g.DrawImage(image, new Rectangle(width + width2, height + height2, width, height),
                width + width1, height + height1, width, height, GraphicsUnit.Pixel, imageAttributes);

            //上
            g.DrawImage(image, new Rectangle(width, 0, width2, height),
                width, 0, width1, height, GraphicsUnit.Pixel, imageAttributes);
            //下
            g.DrawImage(image, new Rectangle(width, height + height2, width2, height),
                width, height + height1, width1, height, GraphicsUnit.Pixel, imageAttributes);
            //左
            g.DrawImage(image, new Rectangle(0, height, width, height2),
                0, height, width, height1, GraphicsUnit.Pixel, imageAttributes);
            //右
            g.DrawImage(image, new Rectangle(width + width2, height, width, height2),
                width + width1, height, width, height1, GraphicsUnit.Pixel, imageAttributes);

            //中
            g.DrawImage(image, new Rectangle(width, height, width2, height2),
                width, height, width1, height1, GraphicsUnit.Pixel, imageAttributes);
        }

        private static ImageAttributes SetPictureAlpha(int alpha)
        {
            //颜色矩阵
            float[][] matrixItems =
            {
                new float[] {1, 0, 0, 0, 0},
                new float[] {0, 1, 0, 0, 0},
                new float[] {0, 0, 1, 0, 0},
                // ReSharper disable once RedundantExplicitArrayCreation
                new float[] {0, 0, 0, alpha / 255f, 0},
                new float[] {0, 0, 0, 0, 1}
            };

            var colorMatrix = new ColorMatrix(matrixItems);
            var imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            return imageAttributes;
        }
    }
}
