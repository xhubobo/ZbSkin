namespace ZbSkin.Frm
{
    partial class SkinMainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel_bottom = new System.Windows.Forms.Panel();
            this.panel_content = new System.Windows.Forms.Panel();
            this.contextMenuStrip_skin = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.次世代ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.护眼绿ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.青草地ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.喜庆红ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel_top = new System.Windows.Forms.Panel();
            this.pictureBox_max = new System.Windows.Forms.PictureBox();
            this.pictureBox_min = new System.Windows.Forms.PictureBox();
            this.pictureBox_skin = new System.Windows.Forms.PictureBox();
            this.pictureBox_close = new System.Windows.Forms.PictureBox();
            this.label_title = new System.Windows.Forms.Label();
            this.pictureBox_icon = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip_skin.SuspendLayout();
            this.panel_top.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_max)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_min)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_skin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_icon)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_bottom
            // 
            this.panel_bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.panel_bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_bottom.Location = new System.Drawing.Point(0, 560);
            this.panel_bottom.Name = "panel_bottom";
            this.panel_bottom.Size = new System.Drawing.Size(800, 40);
            this.panel_bottom.TabIndex = 1;
            // 
            // panel_content
            // 
            this.panel_content.BackColor = System.Drawing.Color.LightBlue;
            this.panel_content.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel_content.Location = new System.Drawing.Point(34, 112);
            this.panel_content.Name = "panel_content";
            this.panel_content.Size = new System.Drawing.Size(729, 400);
            this.panel_content.TabIndex = 2;
            // 
            // contextMenuStrip_skin
            // 
            this.contextMenuStrip_skin.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.次世代ToolStripMenuItem,
            this.护眼绿ToolStripMenuItem,
            this.青草地ToolStripMenuItem,
            this.喜庆红ToolStripMenuItem});
            this.contextMenuStrip_skin.Name = "contextMenuStrip_skin";
            this.contextMenuStrip_skin.Size = new System.Drawing.Size(113, 92);
            // 
            // 次世代ToolStripMenuItem
            // 
            this.次世代ToolStripMenuItem.Checked = true;
            this.次世代ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.次世代ToolStripMenuItem.Name = "次世代ToolStripMenuItem";
            this.次世代ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.次世代ToolStripMenuItem.Tag = "next_gen";
            this.次世代ToolStripMenuItem.Text = "次世代";
            this.次世代ToolStripMenuItem.Click += new System.EventHandler(this.SkinToolStripMenuItem_Click);
            // 
            // 护眼绿ToolStripMenuItem
            // 
            this.护眼绿ToolStripMenuItem.Name = "护眼绿ToolStripMenuItem";
            this.护眼绿ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.护眼绿ToolStripMenuItem.Tag = "eye_green";
            this.护眼绿ToolStripMenuItem.Text = "护眼绿";
            this.护眼绿ToolStripMenuItem.Click += new System.EventHandler(this.SkinToolStripMenuItem_Click);
            // 
            // 青草地ToolStripMenuItem
            // 
            this.青草地ToolStripMenuItem.Name = "青草地ToolStripMenuItem";
            this.青草地ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.青草地ToolStripMenuItem.Tag = "grass";
            this.青草地ToolStripMenuItem.Text = "青草地";
            this.青草地ToolStripMenuItem.Click += new System.EventHandler(this.SkinToolStripMenuItem_Click);
            // 
            // 喜庆红ToolStripMenuItem
            // 
            this.喜庆红ToolStripMenuItem.Name = "喜庆红ToolStripMenuItem";
            this.喜庆红ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.喜庆红ToolStripMenuItem.Tag = "festival_red";
            this.喜庆红ToolStripMenuItem.Text = "喜庆红";
            this.喜庆红ToolStripMenuItem.Click += new System.EventHandler(this.SkinToolStripMenuItem_Click);
            // 
            // panel_top
            // 
            this.panel_top.BackgroundImage = global::ZbSkin.Properties.Resources.skin01;
            this.panel_top.Controls.Add(this.pictureBox_max);
            this.panel_top.Controls.Add(this.pictureBox_min);
            this.panel_top.Controls.Add(this.pictureBox_skin);
            this.panel_top.Controls.Add(this.pictureBox_close);
            this.panel_top.Controls.Add(this.label_title);
            this.panel_top.Controls.Add(this.pictureBox_icon);
            this.panel_top.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_top.Location = new System.Drawing.Point(0, 0);
            this.panel_top.Name = "panel_top";
            this.panel_top.Size = new System.Drawing.Size(800, 80);
            this.panel_top.TabIndex = 0;
            // 
            // pictureBox_max
            // 
            this.pictureBox_max.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_max.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_max.Image = global::ZbSkin.Properties.Resources.MaxNormlBack;
            this.pictureBox_max.Location = new System.Drawing.Point(736, 2);
            this.pictureBox_max.Name = "pictureBox_max";
            this.pictureBox_max.Size = new System.Drawing.Size(27, 22);
            this.pictureBox_max.TabIndex = 9;
            this.pictureBox_max.TabStop = false;
            this.pictureBox_max.Tag = "title_max";
            // 
            // pictureBox_min
            // 
            this.pictureBox_min.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_min.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_min.Image = global::ZbSkin.Properties.Resources.MiniNormlBack;
            this.pictureBox_min.Location = new System.Drawing.Point(704, 2);
            this.pictureBox_min.Name = "pictureBox_min";
            this.pictureBox_min.Size = new System.Drawing.Size(27, 22);
            this.pictureBox_min.TabIndex = 8;
            this.pictureBox_min.TabStop = false;
            this.pictureBox_min.Tag = "title_min";
            // 
            // pictureBox_skin
            // 
            this.pictureBox_skin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_skin.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_skin.Image = global::ZbSkin.Properties.Resources.SkinNormalBack;
            this.pictureBox_skin.Location = new System.Drawing.Point(672, 2);
            this.pictureBox_skin.Name = "pictureBox_skin";
            this.pictureBox_skin.Size = new System.Drawing.Size(27, 22);
            this.pictureBox_skin.TabIndex = 7;
            this.pictureBox_skin.TabStop = false;
            this.pictureBox_skin.Tag = "title_skin";
            // 
            // pictureBox_close
            // 
            this.pictureBox_close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_close.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_close.Image = global::ZbSkin.Properties.Resources.CloseNormlBack;
            this.pictureBox_close.Location = new System.Drawing.Point(768, 2);
            this.pictureBox_close.Name = "pictureBox_close";
            this.pictureBox_close.Size = new System.Drawing.Size(27, 22);
            this.pictureBox_close.TabIndex = 6;
            this.pictureBox_close.TabStop = false;
            this.pictureBox_close.Tag = "title_close";
            // 
            // label_title
            // 
            this.label_title.AutoSize = true;
            this.label_title.BackColor = System.Drawing.Color.Transparent;
            this.label_title.Font = new System.Drawing.Font("宋体", 18F);
            this.label_title.ForeColor = System.Drawing.Color.White;
            this.label_title.Location = new System.Drawing.Point(80, 25);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(154, 24);
            this.label_title.TabIndex = 1;
            this.label_title.Text = "电脑关机助手";
            // 
            // pictureBox_icon
            // 
            this.pictureBox_icon.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_icon.Image = global::ZbSkin.Properties.Resources.device;
            this.pictureBox_icon.Location = new System.Drawing.Point(10, 10);
            this.pictureBox_icon.Name = "pictureBox_icon";
            this.pictureBox_icon.Size = new System.Drawing.Size(60, 60);
            this.pictureBox_icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_icon.TabIndex = 0;
            this.pictureBox_icon.TabStop = false;
            // 
            // SkinMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.panel_content);
            this.Controls.Add(this.panel_bottom);
            this.Controls.Add(this.panel_top);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SkinMainForm";
            this.Text = "SkinMainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SkinMainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SkinMainForm_FormClosed);
            this.Load += new System.EventHandler(this.SkinMainForm_Load);
            this.SizeChanged += new System.EventHandler(this.SkinMainForm_SizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SkinMainForm_Paint);
            this.Resize += new System.EventHandler(this.SkinMainForm_Resize);
            this.contextMenuStrip_skin.ResumeLayout(false);
            this.panel_top.ResumeLayout(false);
            this.panel_top.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_max)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_min)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_skin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_icon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_top;
        private System.Windows.Forms.PictureBox pictureBox_icon;
        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.PictureBox pictureBox_max;
        private System.Windows.Forms.PictureBox pictureBox_min;
        private System.Windows.Forms.PictureBox pictureBox_skin;
        private System.Windows.Forms.PictureBox pictureBox_close;
        private System.Windows.Forms.Panel panel_bottom;
        private System.Windows.Forms.Panel panel_content;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_skin;
        private System.Windows.Forms.ToolStripMenuItem 次世代ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 护眼绿ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 青草地ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 喜庆红ToolStripMenuItem;
    }
}