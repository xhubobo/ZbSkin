using System.Drawing;

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
    }
}
