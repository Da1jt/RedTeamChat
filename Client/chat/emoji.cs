using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sunny.UI;

namespace chat
{

    public partial class emoji : UIAsideMainFrame
    {

        public UIMarkLabel uiMarkLabel1;
        public UIImageButton doge;
        public static class GlobalData
        {
            public static string emojiset { get; set; }
        }
        public emoji()
        {
            InitializeComponent();
            GlobalData.emojiset = "";
        }

        public void InitializeComponent()
        {
            this.uiMarkLabel1 = new Sunny.UI.UIMarkLabel();
            this.doge = new Sunny.UI.UIImageButton();
            ((System.ComponentModel.ISupportInitialize)(this.doge)).BeginInit();
            this.SuspendLayout();
            // 
            // Aside
            // 
            this.Aside.LineColor = System.Drawing.Color.Black;
            this.Aside.Margin = new System.Windows.Forms.Padding(0);
            this.Aside.MenuStyle = Sunny.UI.UIMenuStyle.Custom;
            this.Aside.Size = new System.Drawing.Size(853, 523);
            this.Aside.Style = Sunny.UI.UIStyle.Custom;
            // 
            // uiMarkLabel1
            // 
            this.uiMarkLabel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiMarkLabel1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiMarkLabel1.Location = new System.Drawing.Point(19, 138);
            this.uiMarkLabel1.Name = "uiMarkLabel1";
            this.uiMarkLabel1.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.uiMarkLabel1.Size = new System.Drawing.Size(148, 48);
            this.uiMarkLabel1.Style = Sunny.UI.UIStyle.Custom;
            this.uiMarkLabel1.TabIndex = 5;
            this.uiMarkLabel1.Text = "Doge";
            this.uiMarkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiMarkLabel1.Click += new System.EventHandler(this.doge_Click);
            // 
            // doge
            // 
            this.doge.BackgroundImage = global::chat.Resource1.Doge;
            this.doge.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.doge.Cursor = System.Windows.Forms.Cursors.Hand;
            this.doge.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.doge.Location = new System.Drawing.Point(52, 56);
            this.doge.Name = "doge";
            this.doge.Size = new System.Drawing.Size(88, 79);
            this.doge.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.doge.Style = Sunny.UI.UIStyle.Custom;
            this.doge.TabIndex = 4;
            this.doge.TabStop = false;
            this.doge.Text = null;
            this.doge.Click += new System.EventHandler(this.doge_Click);
            // 
            // emoji
            // 
            this.ClientSize = new System.Drawing.Size(853, 558);
            this.ControlBoxFillHoverColor = System.Drawing.Color.Black;
            this.Controls.Add(this.uiMarkLabel1);
            this.Controls.Add(this.doge);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "emoji";
            this.Opacity = 0.8D;
            this.RectColor = System.Drawing.Color.Black;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "EmojiForm";
            this.TitleColor = System.Drawing.Color.DimGray;
            this.ZoomScaleRect = new System.Drawing.Rectangle(19, 19, 818, 537);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.emoji_FormClosed);
            this.Controls.SetChildIndex(this.Aside, 0);
            this.Controls.SetChildIndex(this.doge, 0);
            this.Controls.SetChildIndex(this.uiMarkLabel1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.doge)).EndInit();
            this.ResumeLayout(false);

        }

        public void emoji_Load(object sender, EventArgs e)
        {

        }
        private void Emoji_closed(object sender, EventArgs e)
        {
            
        }
        public void doge_Click(object sender, EventArgs e)
        {
            GlobalData.emojiset = "Doge";
            Close();
        }

        private void emoji_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            Close();
        }
    }
}
