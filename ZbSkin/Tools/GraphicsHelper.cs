using System.Drawing;
using System.Drawing.Drawing2D;

namespace ZbSkin.Tools
{
    public static class GraphicsHelper
    {
        /// <summary>
        /// 绘制圆角路径
        /// </summary>
        /// <param name="rect">矩形区域</param>
        /// <param name="radius">圆角半径</param>
        /// <returns>绘制路径</returns>
        public static GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            var diameter = radius;
            var arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
            var path = new GraphicsPath();

            // 左上角
            path.AddArc(arcRect, 180, 90);

            // 右上角
            arcRect.X = rect.Right - diameter;
            path.AddArc(arcRect, 270, 90);

            // 右下角
            arcRect.Y = rect.Bottom - diameter;
            path.AddArc(arcRect, 0, 90);

            // 左下角
            arcRect.X = rect.Left;
            path.AddArc(arcRect, 90, 90);

            //闭合曲线
            path.CloseFigure();
            return path;
        }
    }
}
