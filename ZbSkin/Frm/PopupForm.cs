using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ZbSkin.Tools;

namespace ZbSkin.Frm
{
    public partial class PopupForm : SkinForm
    {
        [Browsable(true)]
        [Description("窗体标题")]
        public string FrmTitle
        {
            set
            {
                label_title.Text = value;
                Text = label_title.Text;
            }
        }

        protected Panel TitlePanel => panel_title;

        public PopupForm()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterParent;
            MoveTarget = panel_title;
            SysButtonClose = pictureBox_close;

            //标题
            label_title.Text = string.Empty;
            panel_title.BackColor = CommonPara.SkinColor;
        }

        private void PopupForm_Load(object sender, EventArgs e)
        {
            Text = label_title.Text;
            ControlTool.SetControlEnabled(label_title, false);
            InitLoad();
        }

        private void PopupForm_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            var p = new Pen(CommonPara.SkinColor, 2);
            g.DrawRectangle(p, panel_title.Left, panel_title.Top, Width, Height);
        }

        private void PopupForm_SizeChanged(object sender, EventArgs e)
        {
        }
    }
}
