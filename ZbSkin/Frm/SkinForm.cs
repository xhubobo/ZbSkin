using System;
using System.Drawing;
using System.Windows.Forms;

namespace ZbSkin.Frm
{
    public class SkinForm : Form
    {
        //窗体移动参考控件
        protected Control MoveTarget { get; set; }

        //最小化、最大化、关闭
        protected PictureBox SysButtonMin { get; set; }
        protected PictureBox SysButtonMax { get; set; }
        protected PictureBox SysButtonClose { get; set; }
        protected string CloseInfo { get; set; }

        public SkinForm()
        {
            //无边框
            FormBorderStyle = FormBorderStyle.None;
        }

        public void InitLoad()
        {
            //最大化时不会遮盖任务栏
            MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width,
                Screen.PrimaryScreen.WorkingArea.Height);

            //窗体移动支持
            InitWindowMove();

            //系统按钮事件绑定
            InitSysButton();
        }

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
            if (MoveTarget != null)
            {
                MoveTarget.MouseUp += Form_MouseUp;
                MoveTarget.MouseMove += Form_MouseMove;
                MoveTarget.MouseDown += Form_MouseDown;
            }
        }

        //鼠标按下
        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            //鼠标左键按下
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            //鼠标在标题栏
            var mousePos = e.Location;
            if (!MoveTarget.Bounds.Contains(mousePos))
            {
                return;
            }

            //在非系统按钮处按下鼠标
            //             if (pictureBox_min.Bounds.Contains(mousePos) ||
            //                 pictureBox_max.Bounds.Contains(mousePos) ||
            //                 pictureBox_close.Bounds.Contains(mousePos))
            //             {
            //                 return;
            //             }

            mousePos = PointToScreen(mousePos);
            var xOffset = mousePos.X - Location.X;
            var yOffset = mousePos.Y - Location.Y;
            _windowOffset = new Point(-xOffset, -yOffset);
            _windowMoveFlag = true; //开始移动
        }

        //鼠标移动
        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (_windowMoveFlag)
            {
                var mousePos = MousePosition;
                mousePos.Offset(_windowOffset);
                Location = mousePos;
            }
        }

        //鼠标松开
        private void Form_MouseUp(object sender, MouseEventArgs e)
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
            InitSysButton(SysButtonMin);
            InitSysButton(SysButtonMax);
            InitSysButton(SysButtonClose);
        }

        private void InitSysButton(PictureBox sysButton)
        {
            if (sysButton != null)
            {
                sysButton.MouseDown += pictureBox_MouseDown;
                sysButton.MouseEnter += pictureBox_MouseEnter;
                sysButton.MouseLeave += pictureBox_MouseLeave;
                sysButton.MouseUp += pictureBox_MouseUp;
            }
        }
        
        //设置窗体的最大化、最小化和关闭按钮的单击事件
        private void SysButtonClick(object sender)
        {
            var picBox = sender as PictureBox;
            if (picBox == null)
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

                    break;
                case "title_close":
                    Close();
                    break;
            }
        }
        
        //控制图片的切换状态
        protected void ImageSwitch(object sender, int type)
        {
            var picBox = sender as PictureBox;
            if (picBox == null)
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

        private void pictureBox_MouseEnter(object sender, EventArgs e)
        {
            ImageSwitch(sender, 0);
        }

        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            ImageSwitch(sender, 1);
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            ImageSwitch(sender, 2);
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            SysButtonClick(sender);
            Invalidate();
        }

        #endregion
    }
}
