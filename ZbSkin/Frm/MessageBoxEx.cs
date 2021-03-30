using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using ZbSkin.Share;
using ZbSkin.Tools;

namespace ZbSkin.Frm
{
    public partial class MessageBoxEx : SkinForm
    {
        private const string NotifyIconError = "\ue634";
        private const string NotifyIconInfo = "\ue653";
        private const string NotifyIconQuestion = "\ue621";
        private const string NotifyIconWarning = "\ue6aa";
        private const string NotifyIconTitle = "\ue682";

        private readonly int _labelImgFont = 75;

        /// <summary>
        /// 结果，用户点击确定Result=true
        /// </summary>
        public bool Result { get; private set; }

        private void CommonInit()
        {
            MoveTarget = panel_title;
            SysButtonClose = pictureBox_close;
            Result = false;

            panel_title.BackColor = CommonPara.SkinColor;
            //this.label_image.ForeColor = CommonPara.SkinColor;
        }

        public MessageBoxEx()
        {
            InitializeComponent();
            CommonInit();

            label_title.Text = string.Empty; //标题
            label_message.Text = string.Empty; //内容
            label_image.Text = NotifyIconWarning; //图片
            btn_cancel.Visible = false; //隐藏取消按钮
        }

        public MessageBoxEx(EnumNotifyType type, string message)
        {
            InitializeComponent();
            CommonInit();

            label_title.Text = type.GetDescription(); //标题
            label_message.Text = message; //内容
            btn_cancel.Visible = false; //隐藏取消按钮

            //type
            switch (type)
            {
                case EnumNotifyType.Error:
                    label_image.Text = NotifyIconError;
                    _labelImgFont = 68;
                    break;
                case EnumNotifyType.Warning:
                    label_image.Text = NotifyIconWarning;
                    _labelImgFont = 68;
                    break;
                case EnumNotifyType.Info:
                    label_image.Text = NotifyIconInfo;
                    _labelImgFont = 75;
                    break;
                case EnumNotifyType.Question:
                    label_image.Text = NotifyIconQuestion;
                    _labelImgFont = 75;
                    btn_cancel.Visible = true;
                    break;
            }
        }

        #region 窗体消息

        private void MessageBoxEx_Load(object sender, EventArgs e)
        {
            Text = label_title.Text; //将标题与label_title绑定
            InitLoad();

            FontManager.Instance.InitControlFont(label_icon, FontManager.FontType.IconFont, NotifyIconTitle, 20);
            FontManager.Instance.InitControlFont(label_image, FontManager.FontType.IconFont, label_image.Text, _labelImgFont);
            ControlTool.SetControlEnabled(label_icon, false);
            ControlTool.SetControlEnabled(label_title, false);

            if (!btn_ok.Visible)
            {
                btn_cancel.Left = (Width - btn_cancel.Width) / 2;
            }
            else if (!btn_cancel.Visible)
            {
                btn_ok.Left = (Width - btn_ok.Width) / 2;
            }

            if (Owner == null || !Owner.Visible || Owner.WindowState == FormWindowState.Minimized)
            {
                CenterToScreen();
            }
            else
            {
                Left = Owner.Left + (Owner.Width - Width) / 2;
                Top = Owner.Top + (Owner.Height - Height) / 2;
            }
        }

        private void MessageBoxEx_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            var p = new Pen(CommonPara.SkinColor, 2);
            g.DrawRectangle(p, panel_title.Left, panel_title.Top, Width, Height);
        }

        #endregion

        private void btn_ok_Click(object sender, EventArgs e)
        {
            Result = true;
            Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Result = false;
            Close();
        }

        #region public static methods

        /// <summary>
        /// 提示错误消息
        /// </summary>
        public static void Error(string mes, Form owner = null)
        {
            Show(EnumNotifyType.Error, mes, owner);
        }

        /// <summary>
        /// 提示普通消息
        /// </summary>
        public static void Info(string mes, Form owner = null)
        {
            Show(EnumNotifyType.Info, mes, owner);
        }

        /// <summary>
        /// 提示警告消息
        /// </summary>
        public static void Warning(string mes, Form owner = null)
        {
            Show(EnumNotifyType.Warning, mes, owner);
        }

        /// <summary>
        /// 提示询问消息
        /// </summary>
        public static bool Question(string mes, Form owner = null)
        {
            return Show(EnumNotifyType.Question, mes, owner);
        }

        private static bool Show(EnumNotifyType type, string mes, Form owner = null)
        {
            var mb = new MessageBoxEx(type, mes) {Owner = owner};
            mb.ShowDialog();
            return mb.Result;
        }

        #endregion

        /// <summary>
        /// 通知消息类型
        /// </summary>
        public enum EnumNotifyType
        {
            [Description("错误")] Error,
            [Description("警告")] Warning,
            [Description("提示信息")] Info,
            [Description("询问信息")] Question,
        }
    }

    public static class TypeExtension
    {
        private static readonly NameValueCollection Nvc = GetEnumDescription(typeof(MessageBoxEx.EnumNotifyType));

        public static string GetDescription(this MessageBoxEx.EnumNotifyType type)
        {
            return Nvc.Get(type.ToString());
        }

        /// <summary>
        /// 获取枚举描述信息
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static NameValueCollection GetEnumDescription(Type enumType)
        {
            var nvc = new NameValueCollection();
            var type = typeof(DescriptionAttribute);

            foreach (FieldInfo fi in enumType.GetFields())
            {
                var array = fi.GetCustomAttributes(type, true);
                if (array.Length > 0)
                {
                    nvc.Add(fi.Name, ((DescriptionAttribute) array[0]).Description);
                }
            }

            return nvc;
        }
    }
}
