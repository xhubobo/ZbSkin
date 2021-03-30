using System.Windows.Forms;

namespace ZbSkin.Tools
{
    public static class TooltipHelper
    {
        public static void ShowTooltip(this Control control, ToolTip tip, string message)
        {
            var pos = Control.MousePosition;
            var x = control.PointToClient(pos).X;
            var y = control.PointToClient(pos).Y;
            tip.Show(message, control, x, y);
            tip.Active = true;
        }

        public static void HideTooltip(this Control control, ToolTip tip)
        {
            tip.Hide(control);
        }
    }
}
