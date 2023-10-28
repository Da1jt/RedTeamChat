using Sunny.UI;
using Sunny.UI.Win32;
using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Runtime.InteropServices.ComTypes;

namespace chat
{
    public partial class inpfilename : Form
    {
        public UITextBox filenm;
        public UIButton change;

        public inpfilename()
        {

        }

        public void InitializeComponent()
        {
            this.change = new Sunny.UI.UIButton();
            this.filenm = new Sunny.UI.UITextBox();
            this.SuspendLayout();
            // 
            // change
            // 
            this.change.Cursor = System.Windows.Forms.Cursors.Hand;
            this.change.Font = new System.Drawing.Font("宋体", 14F);
            this.change.Location = new System.Drawing.Point(27, 82);
            this.change.MinimumSize = new System.Drawing.Size(1, 1);
            this.change.Name = "change";
            this.change.Size = new System.Drawing.Size(171, 49);
            this.change.TabIndex = 0;
            this.change.Text = "change name";
            this.change.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.change.Click += new System.EventHandler(this.change_Click_1);
            // 
            // filenm
            // 
            this.filenm.ButtonSymbolOffset = new System.Drawing.Point(0, 0);
            this.filenm.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.filenm.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.filenm.Location = new System.Drawing.Point(27, 14);
            this.filenm.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.filenm.MinimumSize = new System.Drawing.Size(1, 16);
            this.filenm.Name = "filenm";
            this.filenm.Padding = new System.Windows.Forms.Padding(5);
            this.filenm.ShowText = false;
            this.filenm.Size = new System.Drawing.Size(154, 60);
            this.filenm.TabIndex = 1;
            this.filenm.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.filenm.Watermark = "";
            this.filenm.TextChanged += new System.EventHandler(this.newnm_TextChanged);
            // 
            // inpfilename
            // 
            this.ClientSize = new System.Drawing.Size(219, 156);
            this.Controls.Add(this.filenm);
            this.Controls.Add(this.change);
            this.Name = "inpfilename";
            this.ResumeLayout(false);

        }
        public static class GlobalVariables
        {
            public static string Filenamechange { get; set; }
        }
        private void newnm_TextChanged(object sender, EventArgs e)
        {

        }

        private void change_Click_1(object sender, EventArgs e)
        {
            GlobalVariables.Filenamechange = filenm.Text;
        }
    }
}
