using System.Drawing;
using System.Windows.Forms;

namespace ZbSkin.Tools
{
    public static class ControlExtension
    {
        public static void SetRegion(this Control control, int radius)
        {
            var rect = new Rectangle(0, 0, control.Width, control.Height);
            var path = GraphicsHelper.GetRoundedRectPath(rect, radius);
            control.Region = new Region(path);
            path.Dispose();
        }
    }
}
