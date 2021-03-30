using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace ZbSkin.Share
{
    public class FontManager
    {
        private readonly Dictionary<FontType, FontFamily> _fontFamilyDic;

        public void InitControlFont(Control control, FontType fontType, string text, int fontSize)
        {
            var fontFamily = GetFontFamily(fontType);
            if (fontFamily == null)
            {
                return;
            }

            control.Font = new Font(fontFamily, fontSize);
            control.Text = text;
        }

        public Font GetFont(FontType fontType, int fontSize)
        {
            var fontFamily = GetFontFamily(fontType);
            return fontFamily == null ? null : new Font(fontFamily, fontSize);
        }

        public Font GetFont(string fontFileName, int fontSize)
        {
            if (string.IsNullOrWhiteSpace(fontFileName))
            {
                return null;
            }

            try
            {
                var appPath = Application.StartupPath;
                var font = new PrivateFontCollection();
                font.AddFontFile($"{appPath}/{fontFileName}"); //字体的路径及名字
                return new Font(font.Families[0], fontSize);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void Init()
        {
            var appPath = Application.StartupPath;
            AddFont(appPath, FontType.IconFont, "iconfont.ttf");
            AddFont(appPath, FontType.QuartzMs, "QuartzMS.ttf");
            AddFont(appPath, FontType.FzPty, "FZPTY.ttf");
        }

        private void AddFont(string appPath, FontType fontType, string fontFileName)
        {
            try
            {
                var font = new PrivateFontCollection();
                font.AddFontFile($"{appPath}/{fontFileName}"); //字体的路径及名字
                _fontFamilyDic.Add(fontType, font.Families[0]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private FontFamily GetFontFamily(FontType fontType)
        {
            return _fontFamilyDic.ContainsKey(fontType) ? _fontFamilyDic[fontType] : null;
        }

        #region 单例模式

        private FontManager()
        {
            _fontFamilyDic = new Dictionary<FontType, FontFamily>();
            Init();
        }

        private static FontManager _instance;
        private static readonly object LockHelper = new object();

        public static FontManager Instance
        {
            get
            {
                if (null != _instance)
                {
                    return _instance;
                }

                lock (LockHelper)
                {
                    _instance = _instance ?? new FontManager();
                }

                return _instance;
            }
        }

        #endregion

        public enum FontType
        {
            [Description("IconFont")]
            IconFont,

            [Description("Quartz MS")]
            QuartzMs,

            [Description("方正胖头鱼简体")]
            FzPty,

            [Description("实时数据字体")]
            RealtimeData
        }
    }
}
