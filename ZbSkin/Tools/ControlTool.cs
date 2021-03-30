using System;
using System.Windows.Forms;

namespace ZbSkin.Tools
{
    public static class ControlTool
    {
        [System.Runtime.InteropServices.DllImport("user32.dll ")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [System.Runtime.InteropServices.DllImport("user32.dll ")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        // ReSharper disable once InconsistentNaming
        private const int GWL_STYLE = -16;

        // ReSharper disable once InconsistentNaming
        private const int WS_DISABLED = 0x8000000;

        /// <summary>
        /// 设置控件可用状态
        /// </summary>
        /// <param name="c">控件</param>
        /// <param name="enabled">可用状态</param>
        /// <remarks>不可用时鼠标可以穿透控件</remarks>>
        public static void SetControlEnabled(Control c, bool enabled)
        {
            if (enabled)
            {
                SetWindowLong(c.Handle, GWL_STYLE, (~WS_DISABLED) & GetWindowLong(c.Handle, GWL_STYLE));
            }
            else
            {
                SetWindowLong(c.Handle, GWL_STYLE, WS_DISABLED + GetWindowLong(c.Handle, GWL_STYLE));
            }
        }
    }
}
