using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using ZbSkin.Tools;

namespace ZbSkin.Frm
{
    public partial class SkinMainForm : Form
    {
        [Browsable(true), Category("Custom"), Description("Property:FormIcon")]
        public Bitmap FormIcon { get; set; }

        //UI线程的同步上下文
        protected SynchronizationContext SyncContext { get; }

        /// <summary>
        /// 启用皮肤按钮
        /// </summary>
        protected bool EnableSkinButton
        {
            get { return pictureBox_skin.Enabled; }
            set
            {
                pictureBox_skin.Visible = value;
                pictureBox_skin.Enabled = value;
            }
        }

        /// <summary>
        /// 启用最大化按钮
        /// </summary>
        protected bool EnableMaxButton
        {
            get { return pictureBox_max.Enabled; }
            set { pictureBox_max.Enabled = value; }
        }

        /// <summary>
        /// 启用底部
        /// </summary>
        protected bool EnableBottom
        {
            get { return panel_bottom.Enabled; }
            set
            {
                panel_bottom.Visible = value;
                panel_bottom.Enabled = value;
            }
        }

        /// <summary>
        /// 窗口标题
        /// </summary>
        protected string Title
        {
            set
            {
                label_title.Text = value;
                Text = value;
            }
        }

        protected bool LoadMax { private get; set; }

        protected Panel TopPanel => panel_top;
        protected Panel ContentPanel => panel_content;
        protected Panel BottomPanel => panel_bottom;

        protected int TopPanelOffset => label_title.Right;

        //强制关闭窗口，不再询问
        private bool _forceClose;

        public SkinMainForm()
        {
            InitializeComponent();

            SyncContext = SynchronizationContext.Current;

            InitWindowMove();
            InitSysButton();
            InitDoubleClick();
        }

        #region Form事件

        private void SkinMainForm_Load(object sender, EventArgs e)
        {
            //最大化时不会遮盖任务栏
            MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);

            //鼠标点击穿透控件
            ControlTool.SetControlEnabled(pictureBox_icon, false);
            ControlTool.SetControlEnabled(label_title, false);

            //默认次世代
            ChangeSkin("next_gen");

            if (LoadMax)
            {
                WindowState = FormWindowState.Maximized;
                ImageSwitch(pictureBox_max, 1);
            }

            if (FormIcon != null)
            {
                pictureBox_icon.Image = FormIcon;
            }
        }

        private void SkinMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_forceClose)
            {
                e.Cancel = !MessageBoxEx.Question("确定退出软件吗？", this);
            }
        }

        private void SkinMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            DoFormClosed();
        }

        protected virtual void DoFormClosed()
        {
            Dispose();
            Environment.Exit(0);
        }

        protected void ForceClose()
        {
            _forceClose = true;
            Close();
        }

        private void SkinMainForm_SizeChanged(object sender, EventArgs e)
        {
            //relocation content frame
            //panel_content.Width = Width - 4;
            //panel_content.Height = Height - panel_top.Height - panel_bottom.Height - 2;
            //panel_content.Left = 2;
            //panel_content.Top = panel_top.Height;
            panel_content.Width = Width - 4;
            panel_content.Height = EnableBottom
                ? Height - panel_top.Height - panel_bottom.Height
                : Height - panel_top.Height - 2;
            panel_content.Left = 2;
            panel_content.Top = panel_top.Height;

            Invalidate(false);
        }

        private void SkinMainForm_Resize(object sender, EventArgs e)
        {
            //ImageSwitch(pictureBox_max, 1);
            Invalidate();
        }

        private void SkinMainForm_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            var p = new Pen(_skinAvgColor, 2);
            g.DrawRectangle(p, 0, 0, Width, Height);
        }

        #endregion

        #region 窗体移动

        /* 首先将窗体的边框样式修改为None，让窗体没有标题栏
         * 实现这个效果使用了三个事件：鼠标按下、鼠标弹起、鼠标移动
         * 鼠标按下时更改变量isMouseDown标记窗体可以随鼠标的移动而移动
         * 鼠标移动时根据鼠标的移动量更改窗体的location属性，实现窗体移动
         * 鼠标弹起时更改变量isMouseDown标记窗体不可以随鼠标的移动而移动
         */

        //窗体移动标识
        private bool _windowMoveFlag;

        //窗体原始坐标和鼠标按下时的距离
        private Point _windowOffset;

        /// <summary>
        /// 窗体移动支持
        /// </summary>
        private void InitWindowMove()
        {
            panel_top.MouseUp += PanelContent_MouseUp;
            panel_top.MouseMove += PanelContent_MouseMove;
            panel_top.MouseDown += PanelContent_MouseDown;
        }

        //鼠标按下
        private void PanelContent_MouseDown(object sender, MouseEventArgs e)
        {
            //鼠标左键按下
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            //鼠标在标题栏
            var mousePos = e.Location;
            if (!panel_top.Bounds.Contains(mousePos))
            {
                return;
            }

            mousePos = PointToScreen(mousePos);
            var xOffset = mousePos.X - Location.X;
            var yOffset = mousePos.Y - Location.Y;
            _windowOffset = new Point(-xOffset, -yOffset);
            _windowMoveFlag = true; //开始移动
        }

        //鼠标移动
        private void PanelContent_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_windowMoveFlag)
            {
                return;
            }

            var mousePos = MousePosition;
            mousePos.Offset(_windowOffset);
            Location = mousePos;
        }

        //鼠标松开
        private void PanelContent_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //停止移动
                _windowMoveFlag = false;
            }
        }

        #endregion

        #region Sys Buttons

        /// <summary>
        /// 系统按钮事件绑定
        /// </summary>
        private void InitSysButton()
        {
            InitSysButton(pictureBox_min);
            InitSysButton(pictureBox_max);
            InitSysButton(pictureBox_close);
            InitSysButton(pictureBox_skin);
        }

        private void InitSysButton(Control sysButton)
        {
            if (sysButton == null)
            {
                return;
            }

            sysButton.MouseDown += pictureBoxIcon_MouseDown;
            sysButton.MouseEnter += pictureBoxIcon_MouseEnter;
            sysButton.MouseLeave += pictureBoxIcon_MouseLeave;
            sysButton.MouseUp += pictureBoxIcon_MouseUp;
        }

        /// <summary>
        /// 设置窗体的最大化、最小化和关闭按钮的单击事件
        /// </summary>
        private void SysButtonClick(object sender)
        {
            var picBox = sender as PictureBox;
            if (picBox == null || !picBox.Enabled)
            {
                return;
            }

            var mousePos = MousePosition;
            mousePos = PointToClient(mousePos);
            if (!picBox.Bounds.Contains(mousePos))
            {
                return;
            }

            var tag = picBox.Tag.ToString();
            switch (tag)
            {
                case "title_skin":
                {
                    contextMenuStrip_skin.Show(MousePosition);
                }
                    break;
                case "title_min":
                    WindowState = FormWindowState.Minimized;
                    break;
                case "title_max":
                    if (WindowState == FormWindowState.Maximized)
                    {
                        WindowState = FormWindowState.Normal;
                    }
                    else if (WindowState == FormWindowState.Normal)
                    {
                        WindowState = FormWindowState.Maximized;
                    }

                    ImageSwitch(pictureBox_max, 1);
                    break;
                case "title_close":
                    CloseForm();
                    break;
            }
        }

        protected virtual void CloseForm()
        {
            Close();
        }

        /// <summary>
        /// 控制图片的切换状态
        /// </summary>
        protected void ImageSwitch(object sender, int type)
        {
            var picBox = sender as PictureBox;
            if (picBox == null || !picBox.Enabled)
            {
                return;
            }

            var tag = picBox.Tag.ToString();
            switch (tag)
            {
                case "title_skin":
                    switch (type)
                    {
                        case 0: //MouseEnter
                            picBox.Image = Properties.Resources.SkinMouseBack;
                            break;
                        case 1: //MouseLeave
                            picBox.Image = Properties.Resources.SkinNormalBack;
                            break;
                        case 2: //MouseDown
                            picBox.Image = Properties.Resources.SkinDownBack;
                            break;
                    }

                    break;
                case "title_min":
                    switch (type)
                    {
                        case 0: //MouseEnter
                            picBox.Image = Properties.Resources.MiniMouseBack;
                            break;
                        case 1: //MouseLeave
                            picBox.Image = Properties.Resources.MiniNormlBack;
                            break;
                        case 2: //MouseDown
                            picBox.Image = Properties.Resources.MiniDownBack;
                            break;
                    }

                    break;
                case "title_max":
                    if (WindowState == FormWindowState.Maximized)
                    {
                        switch (type)
                        {
                            case 0: //MouseEnter
                                picBox.Image = Properties.Resources.RestoreMouseBack;
                                break;
                            case 1: //MouseLeave
                                picBox.Image = Properties.Resources.RestoreNormlBack;
                                break;
                            case 2: //MouseDown
                                picBox.Image = Properties.Resources.RestoreDownBack;
                                break;
                        }
                    }
                    else if (WindowState == FormWindowState.Normal)
                    {
                        switch (type)
                        {
                            case 0: //MouseEnter
                                picBox.Image = Properties.Resources.MaxMouseBack;
                                break;
                            case 1: //MouseLeave
                                picBox.Image = Properties.Resources.MaxNormlBack;
                                break;
                            case 2: //MouseDown
                                picBox.Image = Properties.Resources.MaxDownBack;
                                break;
                        }
                    }

                    break;
                case "title_close":
                    switch (type)
                    {
                        case 0: //MouseEnter
                            picBox.Image = Properties.Resources.CloseMouseBack;
                            break;
                        case 1: //MouseLeave
                            picBox.Image = Properties.Resources.CloseNormlBack;
                            break;
                        case 2: //MouseDown
                            picBox.Image = Properties.Resources.CloseDownBack;
                            break;
                    }

                    break;
            }
        }

        private void pictureBoxIcon_MouseEnter(object sender, EventArgs e)
        {
            ImageSwitch(sender, 0);
        }

        private void pictureBoxIcon_MouseLeave(object sender, EventArgs e)
        {
            ImageSwitch(sender, 1);
        }

        private void pictureBoxIcon_MouseDown(object sender, MouseEventArgs e)
        {
            ImageSwitch(sender, 2);
        }

        private void pictureBoxIcon_MouseUp(object sender, MouseEventArgs e)
        {
            SysButtonClick(sender);
            //this.Invalidate();
        }

        #endregion

        #region 双击最大最小化

        private void InitDoubleClick()
        {
            panel_top.DoubleClick += Form_DoubleClick;
        }

        private void Form_DoubleClick(object sender, EventArgs e)
        {
            if (!EnableMaxButton)
            {
                return;
            }

            //重置移动标记，防止双击还原后马上移动窗体
            _windowMoveFlag = false;

            WindowState = (WindowState == FormWindowState.Normal)
                ? FormWindowState.Maximized
                : FormWindowState.Normal;

            ImageSwitch(pictureBox_max, 1);
        }

        #endregion

        #region 换肤

        private Color _skinAvgColor = Color.Transparent;
        private string _skinName = string.Empty;

        //换肤菜单点击响应
        private void SkinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //uncheck all menu items
            foreach (var item in contextMenuStrip_skin.Items)
            {
                var menuItem = item as ToolStripMenuItem;
                if (menuItem != null)
                {
                    menuItem.Checked = false;
                }
            }

            var toolStripMenuItem = sender as ToolStripMenuItem;
            if (toolStripMenuItem == null)
            {
                return;
            }

            //check current menu item
            toolStripMenuItem.Checked = true;

            ChangeSkin(toolStripMenuItem.Tag.ToString());
        }

        private void ChangeSkin(string skinName)
        {
            if (_skinName == skinName)
            {
                return;
            }

            _skinName = skinName;

            //get skin
            Bitmap bmp;
            switch (_skinName)
            {
                case "next_gen": //次世代
                    bmp = Properties.Resources.skin01;
                    break;
                case "festival_red": //喜庆红
                    bmp = Properties.Resources.skin03;
                    break;
                case "eye_green": //护眼绿
                    bmp = Properties.Resources.skin06;
                    break;
                case "grass": //青草地
                    bmp = Properties.Resources.skin13;
                    break;
                case "state_grid": //国网绿
                    bmp = Properties.Resources.skin25;
                    break;
                default:
                    return;
            }

            //setup skin
            panel_top.BackgroundImage = bmp;

            //bottom
            _skinAvgColor = ImageHelper.GetAvgColor(bmp, panel_top.Width, 100);
            panel_bottom.BackColor = _skinAvgColor;
            CommonPara.SkinColor = _skinAvgColor;

            //刷新边框线
            Invalidate(false);
        }

        #endregion
    }
}
