﻿using Sunny.UI;
using System.Drawing;
using System.Windows.Forms;

namespace chat
{
    partial class RedTeamChatf
    {

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        public void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RedTeamChatf));
            this.about = new System.Windows.Forms.TabPage();
            this.link_jui = new Sunny.UI.UILinkLabel();
            this.uiSmoothLabel8 = new Sunny.UI.UISmoothLabel();
            this.console = new System.Windows.Forms.TabPage();
            this.consolee = new Sunny.UI.UIRichTextBox();
            this.setting = new System.Windows.Forms.TabPage();
            this.intranetfind = new Sunny.UI.UIButton();
            this.refreshfilel = new Sunny.UI.UIButton();
            this.discon = new Sunny.UI.UIButton();
            this.uiButton2 = new Sunny.UI.UIButton();
            this.connecttrit = new Sunny.UI.UITextBox();
            this.uiWaitingBar1 = new Sunny.UI.UIWaitingBar();
            this.con = new Sunny.UI.UIButton();
            this.uiLine2 = new Sunny.UI.UILine();
            this.uiSmoothLabel6 = new Sunny.UI.UISmoothLabel();
            this.port = new Sunny.UI.UIIntegerUpDown();
            this.uiSmoothLabel5 = new Sunny.UI.UISmoothLabel();
            this.uiSmoothLabel4 = new Sunny.UI.UISmoothLabel();
            this.server = new Sunny.UI.UITextBox();
            this.uiSmoothLabel3 = new Sunny.UI.UISmoothLabel();
            this.uiSmoothLabel2 = new Sunny.UI.UISmoothLabel();
            this.uiSmoothLabel1 = new Sunny.UI.UISmoothLabel();
            this.nameset = new Sunny.UI.UITextBox();
            this.chatroom = new System.Windows.Forms.TabPage();
            this.Emoji = new Sunny.UI.UIAvatar();
            this.sd = new Sunny.UI.UIButton();
            this.Atto = new Sunny.UI.UIComboBox();
            this.inputbutton = new Sunny.UI.UITextBox();
            this.uiFlowLayoutPanel1 = new Sunny.UI.UIFlowLayoutPanel();
            this.uiLine1 = new Sunny.UI.UILine();
            this.controlmenu1 = new Sunny.UI.UITabControlMenu();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Terminal = new Sunny.UI.UIRichTextBox();
            this.serverfile = new System.Windows.Forms.TabPage();
            this.Serverfilegrid = new Sunny.UI.UIDataGridView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.about.SuspendLayout();
            this.console.SuspendLayout();
            this.setting.SuspendLayout();
            this.chatroom.SuspendLayout();
            this.controlmenu1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.serverfile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Serverfilegrid)).BeginInit();
            this.SuspendLayout();
            // 
            // Header
            // 
            this.Header.FillColor = System.Drawing.Color.DarkTurquoise;
            this.Header.Location = new System.Drawing.Point(1128, 35);
            this.Header.Size = new System.Drawing.Size(1, 10);
            this.Header.Style = Sunny.UI.UIStyle.Custom;
            // 
            // Aside
            // 
            this.Aside.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Aside.MenuStyle = Sunny.UI.UIMenuStyle.Custom;
            this.Aside.ScrollBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Aside.ScrollBarHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Aside.ScrollBarPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Aside.Size = new System.Drawing.Size(1128, 537);
            this.Aside.Style = Sunny.UI.UIStyle.Custom;
            // 
            // about
            // 
            this.about.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.about.Controls.Add(this.link_jui);
            this.about.Controls.Add(this.uiSmoothLabel8);
            this.about.ImageIndex = 4;
            this.about.Location = new System.Drawing.Point(201, 0);
            this.about.Name = "about";
            this.about.Size = new System.Drawing.Size(927, 534);
            this.about.TabIndex = 3;
            this.about.Text = "about";
            // 
            // link_jui
            // 
            this.link_jui.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(155)))), ((int)(((byte)(40)))));
            this.link_jui.Font = new System.Drawing.Font("宋体", 14F);
            this.link_jui.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.link_jui.LinkColor = System.Drawing.Color.Teal;
            this.link_jui.Location = new System.Drawing.Point(30, 70);
            this.link_jui.Name = "link_jui";
            this.link_jui.Size = new System.Drawing.Size(318, 48);
            this.link_jui.Style = Sunny.UI.UIStyle.Custom;
            this.link_jui.TabIndex = 12;
            this.link_jui.TabStop = true;
            this.link_jui.Text = "https://github.com/BadJui";
            this.link_jui.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.link_jui.Click += new System.EventHandler(this.link_jui_Click);
            // 
            // uiSmoothLabel8
            // 
            this.uiSmoothLabel8.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSmoothLabel8.Location = new System.Drawing.Point(40, 38);
            this.uiSmoothLabel8.Name = "uiSmoothLabel8";
            this.uiSmoothLabel8.Size = new System.Drawing.Size(250, 32);
            this.uiSmoothLabel8.Style = Sunny.UI.UIStyle.Custom;
            this.uiSmoothLabel8.TabIndex = 11;
            this.uiSmoothLabel8.Text = "Powered by Bad_jui";
            // 
            // console
            // 
            this.console.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.console.Controls.Add(this.consolee);
            this.console.ImageIndex = 2;
            this.console.Location = new System.Drawing.Point(201, 0);
            this.console.Name = "console";
            this.console.Size = new System.Drawing.Size(927, 534);
            this.console.TabIndex = 2;
            this.console.Text = "console";
            // 
            // consolee
            // 
            this.consolee.FillColor = System.Drawing.Color.White;
            this.consolee.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.consolee.Location = new System.Drawing.Point(4, 5);
            this.consolee.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.consolee.MinimumSize = new System.Drawing.Size(1, 1);
            this.consolee.Name = "consolee";
            this.consolee.Padding = new System.Windows.Forms.Padding(2);
            this.consolee.ReadOnly = true;
            this.consolee.ShowText = false;
            this.consolee.Size = new System.Drawing.Size(919, 519);
            this.consolee.Style = Sunny.UI.UIStyle.Custom;
            this.consolee.TabIndex = 0;
            this.consolee.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // setting
            // 
            this.setting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.setting.Controls.Add(this.intranetfind);
            this.setting.Controls.Add(this.refreshfilel);
            this.setting.Controls.Add(this.discon);
            this.setting.Controls.Add(this.uiButton2);
            this.setting.Controls.Add(this.connecttrit);
            this.setting.Controls.Add(this.uiWaitingBar1);
            this.setting.Controls.Add(this.con);
            this.setting.Controls.Add(this.uiLine2);
            this.setting.Controls.Add(this.uiSmoothLabel6);
            this.setting.Controls.Add(this.port);
            this.setting.Controls.Add(this.uiSmoothLabel5);
            this.setting.Controls.Add(this.uiSmoothLabel4);
            this.setting.Controls.Add(this.server);
            this.setting.Controls.Add(this.uiSmoothLabel3);
            this.setting.Controls.Add(this.uiSmoothLabel2);
            this.setting.Controls.Add(this.uiSmoothLabel1);
            this.setting.Controls.Add(this.nameset);
            this.setting.ImageIndex = 1;
            this.setting.Location = new System.Drawing.Point(201, 0);
            this.setting.Name = "setting";
            this.setting.Size = new System.Drawing.Size(927, 534);
            this.setting.TabIndex = 1;
            this.setting.Text = "setting";
            // 
            // intranetfind
            // 
            this.intranetfind.Cursor = System.Windows.Forms.Cursors.Hand;
            this.intranetfind.Enabled = false;
            this.intranetfind.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.intranetfind.Font = new System.Drawing.Font("等线", 11.8F, System.Drawing.FontStyle.Bold);
            this.intranetfind.Location = new System.Drawing.Point(57, 209);
            this.intranetfind.MinimumSize = new System.Drawing.Size(1, 1);
            this.intranetfind.Name = "intranetfind";
            this.intranetfind.Size = new System.Drawing.Size(178, 56);
            this.intranetfind.Style = Sunny.UI.UIStyle.Custom;
            this.intranetfind.TabIndex = 21;
            this.intranetfind.Text = "IntranetServerFind";
            this.intranetfind.TipsFont = new System.Drawing.Font("等线", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.intranetfind.Click += new System.EventHandler(this.intranetfind_Click);
            // 
            // refreshfilel
            // 
            this.refreshfilel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.refreshfilel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.refreshfilel.Location = new System.Drawing.Point(57, 349);
            this.refreshfilel.MinimumSize = new System.Drawing.Size(1, 1);
            this.refreshfilel.Name = "refreshfilel";
            this.refreshfilel.Size = new System.Drawing.Size(178, 55);
            this.refreshfilel.Style = Sunny.UI.UIStyle.Custom;
            this.refreshfilel.TabIndex = 20;
            this.refreshfilel.Text = "refresh filelist";
            this.refreshfilel.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.refreshfilel.Click += new System.EventHandler(this.refreshfilel_Click);
            // 
            // discon
            // 
            this.discon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.discon.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.discon.Location = new System.Drawing.Point(630, 194);
            this.discon.MinimumSize = new System.Drawing.Size(1, 1);
            this.discon.Name = "discon";
            this.discon.Size = new System.Drawing.Size(219, 61);
            this.discon.Style = Sunny.UI.UIStyle.Custom;
            this.discon.TabIndex = 18;
            this.discon.Text = "disconnect";
            this.discon.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.discon.Click += new System.EventHandler(this.discon_Click);
            // 
            // uiButton2
            // 
            this.uiButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton2.Font = new System.Drawing.Font("宋体", 14F);
            this.uiButton2.Location = new System.Drawing.Point(57, 285);
            this.uiButton2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton2.Name = "uiButton2";
            this.uiButton2.Radius = 10;
            this.uiButton2.Size = new System.Drawing.Size(178, 48);
            this.uiButton2.Style = Sunny.UI.UIStyle.Custom;
            this.uiButton2.TabIndex = 15;
            this.uiButton2.Text = "Get userlist";
            this.uiButton2.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton2.Click += new System.EventHandler(this.uiButton2_Click);
            // 
            // connecttrit
            // 
            this.connecttrit.ButtonSymbolOffset = new System.Drawing.Point(0, 0);
            this.connecttrit.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.connecttrit.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.connecttrit.Location = new System.Drawing.Point(370, 402);
            this.connecttrit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.connecttrit.MinimumSize = new System.Drawing.Size(1, 16);
            this.connecttrit.Name = "connecttrit";
            this.connecttrit.Padding = new System.Windows.Forms.Padding(5);
            this.connecttrit.ReadOnly = true;
            this.connecttrit.ShowText = false;
            this.connecttrit.Size = new System.Drawing.Size(202, 60);
            this.connecttrit.Style = Sunny.UI.UIStyle.Custom;
            this.connecttrit.TabIndex = 14;
            this.connecttrit.Text = "Not connected";
            this.connecttrit.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.connecttrit.Watermark = "";
            // 
            // uiWaitingBar1
            // 
            this.uiWaitingBar1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiWaitingBar1.Location = new System.Drawing.Point(630, 398);
            this.uiWaitingBar1.MinimumSize = new System.Drawing.Size(70, 23);
            this.uiWaitingBar1.Name = "uiWaitingBar1";
            this.uiWaitingBar1.Size = new System.Drawing.Size(271, 64);
            this.uiWaitingBar1.Style = Sunny.UI.UIStyle.Custom;
            this.uiWaitingBar1.TabIndex = 13;
            this.uiWaitingBar1.Text = "waitbar1";
            // 
            // con
            // 
            this.con.Cursor = System.Windows.Forms.Cursors.Hand;
            this.con.Font = new System.Drawing.Font("宋体", 13F);
            this.con.Location = new System.Drawing.Point(370, 194);
            this.con.MinimumSize = new System.Drawing.Size(1, 1);
            this.con.Name = "con";
            this.con.Size = new System.Drawing.Size(218, 62);
            this.con.Style = Sunny.UI.UIStyle.Custom;
            this.con.TabIndex = 12;
            this.con.Text = "connect to server";
            this.con.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.con.Click += new System.EventHandler(this.con_Click);
            // 
            // uiLine2
            // 
            this.uiLine2.Direction = Sunny.UI.UILine.LineDirection.Vertical;
            this.uiLine2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLine2.LineColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.uiLine2.Location = new System.Drawing.Point(315, 3);
            this.uiLine2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiLine2.Name = "uiLine2";
            this.uiLine2.Size = new System.Drawing.Size(21, 528);
            this.uiLine2.Style = Sunny.UI.UIStyle.Custom;
            this.uiLine2.TabIndex = 11;
            // 
            // uiSmoothLabel6
            // 
            this.uiSmoothLabel6.Font = new System.Drawing.Font("等线", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSmoothLabel6.Location = new System.Drawing.Point(27, 132);
            this.uiSmoothLabel6.Name = "uiSmoothLabel6";
            this.uiSmoothLabel6.Size = new System.Drawing.Size(278, 52);
            this.uiSmoothLabel6.Style = Sunny.UI.UIStyle.Custom;
            this.uiSmoothLabel6.TabIndex = 9;
            this.uiSmoothLabel6.Text = "Set the name to \"anonymous\"\r\nto chat anonymously";
            // 
            // port
            // 
            this.port.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.port.Location = new System.Drawing.Point(676, 66);
            this.port.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.port.MinimumSize = new System.Drawing.Size(100, 0);
            this.port.Name = "port";
            this.port.ShowText = false;
            this.port.Size = new System.Drawing.Size(155, 49);
            this.port.Style = Sunny.UI.UIStyle.Custom;
            this.port.TabIndex = 8;
            this.port.Text = "uiIntegerUpDown1";
            this.port.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.port.Value = 8080;
            // 
            // uiSmoothLabel5
            // 
            this.uiSmoothLabel5.Font = new System.Drawing.Font("等线", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSmoothLabel5.Location = new System.Drawing.Point(657, 27);
            this.uiSmoothLabel5.Name = "uiSmoothLabel5";
            this.uiSmoothLabel5.Size = new System.Drawing.Size(210, 34);
            this.uiSmoothLabel5.Style = Sunny.UI.UIStyle.Custom;
            this.uiSmoothLabel5.TabIndex = 7;
            this.uiSmoothLabel5.Text = "set the server port here";
            // 
            // uiSmoothLabel4
            // 
            this.uiSmoothLabel4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSmoothLabel4.Location = new System.Drawing.Point(672, 131);
            this.uiSmoothLabel4.Name = "uiSmoothLabel4";
            this.uiSmoothLabel4.Size = new System.Drawing.Size(229, 34);
            this.uiSmoothLabel4.Style = Sunny.UI.UIStyle.Custom;
            this.uiSmoothLabel4.TabIndex = 6;
            this.uiSmoothLabel4.Text = "default port is 8080";
            // 
            // server
            // 
            this.server.ButtonSymbolOffset = new System.Drawing.Point(0, 0);
            this.server.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.server.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.server.Location = new System.Drawing.Point(360, 66);
            this.server.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.server.MinimumSize = new System.Drawing.Size(1, 16);
            this.server.Name = "server";
            this.server.Padding = new System.Windows.Forms.Padding(5);
            this.server.ShowText = false;
            this.server.Size = new System.Drawing.Size(273, 60);
            this.server.Style = Sunny.UI.UIStyle.Custom;
            this.server.TabIndex = 4;
            this.server.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.server.Watermark = "";
            // 
            // uiSmoothLabel3
            // 
            this.uiSmoothLabel3.Font = new System.Drawing.Font("等线", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSmoothLabel3.Location = new System.Drawing.Point(366, 131);
            this.uiSmoothLabel3.Name = "uiSmoothLabel3";
            this.uiSmoothLabel3.Size = new System.Drawing.Size(246, 34);
            this.uiSmoothLabel3.Style = Sunny.UI.UIStyle.Custom;
            this.uiSmoothLabel3.TabIndex = 3;
            this.uiSmoothLabel3.Text = "default ip is xxx.xxx.xxx.xxx";
            // 
            // uiSmoothLabel2
            // 
            this.uiSmoothLabel2.Font = new System.Drawing.Font("等线", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSmoothLabel2.Location = new System.Drawing.Point(390, 27);
            this.uiSmoothLabel2.Name = "uiSmoothLabel2";
            this.uiSmoothLabel2.Size = new System.Drawing.Size(206, 34);
            this.uiSmoothLabel2.Style = Sunny.UI.UIStyle.Custom;
            this.uiSmoothLabel2.TabIndex = 2;
            this.uiSmoothLabel2.Text = "set the server ip here";
            // 
            // uiSmoothLabel1
            // 
            this.uiSmoothLabel1.Font = new System.Drawing.Font("等线", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSmoothLabel1.Location = new System.Drawing.Point(86, 27);
            this.uiSmoothLabel1.Name = "uiSmoothLabel1";
            this.uiSmoothLabel1.Size = new System.Drawing.Size(149, 38);
            this.uiSmoothLabel1.Style = Sunny.UI.UIStyle.Custom;
            this.uiSmoothLabel1.TabIndex = 1;
            this.uiSmoothLabel1.Text = "set ur name here";
            // 
            // nameset
            // 
            this.nameset.ButtonSymbolOffset = new System.Drawing.Point(0, 0);
            this.nameset.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.nameset.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nameset.Location = new System.Drawing.Point(20, 66);
            this.nameset.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nameset.MinimumSize = new System.Drawing.Size(1, 16);
            this.nameset.Name = "nameset";
            this.nameset.Padding = new System.Windows.Forms.Padding(5);
            this.nameset.ShowText = false;
            this.nameset.Size = new System.Drawing.Size(285, 60);
            this.nameset.Style = Sunny.UI.UIStyle.Custom;
            this.nameset.TabIndex = 0;
            this.nameset.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.nameset.Watermark = "";
            // 
            // chatroom
            // 
            this.chatroom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.chatroom.Controls.Add(this.Emoji);
            this.chatroom.Controls.Add(this.sd);
            this.chatroom.Controls.Add(this.Atto);
            this.chatroom.Controls.Add(this.inputbutton);
            this.chatroom.Controls.Add(this.uiFlowLayoutPanel1);
            this.chatroom.Controls.Add(this.uiLine1);
            this.chatroom.ImageIndex = 0;
            this.chatroom.Location = new System.Drawing.Point(201, 0);
            this.chatroom.Name = "chatroom";
            this.chatroom.Size = new System.Drawing.Size(927, 534);
            this.chatroom.TabIndex = 0;
            this.chatroom.Text = "chatroom";
            // 
            // Emoji
            // 
            this.Emoji.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Emoji.Location = new System.Drawing.Point(836, 428);
            this.Emoji.MinimumSize = new System.Drawing.Size(1, 1);
            this.Emoji.Name = "Emoji";
            this.Emoji.Shape = Sunny.UI.UIShape.Square;
            this.Emoji.Size = new System.Drawing.Size(77, 76);
            this.Emoji.Style = Sunny.UI.UIStyle.Custom;
            this.Emoji.Symbol = 162961;
            this.Emoji.TabIndex = 4;
            this.Emoji.Text = "uiAvatar1";
            this.Emoji.Click += new System.EventHandler(this.Emoji_Click);
            // 
            // sd
            // 
            this.sd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.sd.Font = new System.Drawing.Font("等线", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sd.Location = new System.Drawing.Point(697, 428);
            this.sd.MinimumSize = new System.Drawing.Size(1, 1);
            this.sd.Name = "sd";
            this.sd.Size = new System.Drawing.Size(144, 76);
            this.sd.Style = Sunny.UI.UIStyle.Custom;
            this.sd.TabIndex = 1;
            this.sd.Text = "Send";
            this.sd.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sd.Click += new System.EventHandler(this.uiButton1_Click);
            // 
            // Atto
            // 
            this.Atto.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.Atto.DataSource = null;
            this.Atto.Enabled = false;
            this.Atto.FillColor = System.Drawing.Color.White;
            this.Atto.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Atto.Items.AddRange(new object[] {
            "@All"});
            this.Atto.Location = new System.Drawing.Point(9, 436);
            this.Atto.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Atto.MinimumSize = new System.Drawing.Size(63, 0);
            this.Atto.Name = "Atto";
            this.Atto.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.Atto.Size = new System.Drawing.Size(138, 60);
            this.Atto.Style = Sunny.UI.UIStyle.Custom;
            this.Atto.TabIndex = 3;
            this.Atto.TabStop = false;
            this.Atto.Text = "@All";
            this.Atto.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.Atto.Watermark = "";
            this.Atto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Atto_KeyDown);
            this.Atto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Atto_KeyPress);
            this.Atto.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Atto_MouseClick);
            // 
            // inputbutton
            // 
            this.inputbutton.ButtonSymbolOffset = new System.Drawing.Point(0, 0);
            this.inputbutton.CanEmpty = true;
            this.inputbutton.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.inputbutton.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.inputbutton.Location = new System.Drawing.Point(155, 428);
            this.inputbutton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.inputbutton.MinimumSize = new System.Drawing.Size(1, 16);
            this.inputbutton.Multiline = true;
            this.inputbutton.Name = "inputbutton";
            this.inputbutton.Padding = new System.Windows.Forms.Padding(5);
            this.inputbutton.ScrollBarBackColor = System.Drawing.Color.SkyBlue;
            this.inputbutton.ScrollBarHandleWidth = 3;
            this.inputbutton.ShowScrollBar = true;
            this.inputbutton.ShowText = false;
            this.inputbutton.Size = new System.Drawing.Size(535, 76);
            this.inputbutton.Style = Sunny.UI.UIStyle.Custom;
            this.inputbutton.TabIndex = 0;
            this.inputbutton.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.inputbutton.Watermark = "";
            this.inputbutton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.inputbutton_KeyDown);
            // 
            // uiFlowLayoutPanel1
            // 
            this.uiFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.uiFlowLayoutPanel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiFlowLayoutPanel1.ForeColor = System.Drawing.Color.Teal;
            this.uiFlowLayoutPanel1.Location = new System.Drawing.Point(9, 12);
            this.uiFlowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiFlowLayoutPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiFlowLayoutPanel1.Name = "uiFlowLayoutPanel1";
            this.uiFlowLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.uiFlowLayoutPanel1.ScrollBarWidth = 30;
            this.uiFlowLayoutPanel1.ShowText = false;
            this.uiFlowLayoutPanel1.Size = new System.Drawing.Size(904, 381);
            this.uiFlowLayoutPanel1.Style = Sunny.UI.UIStyle.Custom;
            this.uiFlowLayoutPanel1.TabIndex = 3;
            this.uiFlowLayoutPanel1.Text = "rcv";
            this.uiFlowLayoutPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiFlowLayoutPanel1.WrapContents = false;
            // 
            // uiLine1
            // 
            this.uiLine1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLine1.LineColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.uiLine1.Location = new System.Drawing.Point(-61, 392);
            this.uiLine1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiLine1.Name = "uiLine1";
            this.uiLine1.Size = new System.Drawing.Size(1001, 36);
            this.uiLine1.Style = Sunny.UI.UIStyle.Custom;
            this.uiLine1.TabIndex = 2;
            // 
            // controlmenu1
            // 
            this.controlmenu1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.controlmenu1.Controls.Add(this.chatroom);
            this.controlmenu1.Controls.Add(this.setting);
            this.controlmenu1.Controls.Add(this.console);
            this.controlmenu1.Controls.Add(this.tabPage1);
            this.controlmenu1.Controls.Add(this.serverfile);
            this.controlmenu1.Controls.Add(this.about);
            this.controlmenu1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.controlmenu1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.controlmenu1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.controlmenu1.ImageList = this.imageList1;
            this.controlmenu1.ItemSize = new System.Drawing.Size(200, 50);
            this.controlmenu1.Location = new System.Drawing.Point(0, 35);
            this.controlmenu1.MenuStyle = Sunny.UI.UIMenuStyle.Custom;
            this.controlmenu1.Multiline = true;
            this.controlmenu1.Name = "controlmenu1";
            this.controlmenu1.SelectedIndex = 0;
            this.controlmenu1.ShowToolTips = true;
            this.controlmenu1.Size = new System.Drawing.Size(1128, 534);
            this.controlmenu1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.controlmenu1.Style = Sunny.UI.UIStyle.Custom;
            this.controlmenu1.TabIndex = 2;
            this.controlmenu1.TabSelectedForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.controlmenu1.TabSelectedHighColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.controlmenu1.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.tabPage1.Controls.Add(this.Terminal);
            this.tabPage1.ImageIndex = 5;
            this.tabPage1.Location = new System.Drawing.Point(201, 0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(927, 534);
            this.tabPage1.TabIndex = 5;
            this.tabPage1.Text = "Terminal";
            // 
            // Terminal
            // 
            this.Terminal.AcceptsTab = true;
            this.Terminal.AutoWordSelection = false;
            this.Terminal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Terminal.FillColor = System.Drawing.Color.Black;
            this.Terminal.FillDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Terminal.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Terminal.ForeColor = System.Drawing.Color.LawnGreen;
            this.Terminal.ForeDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Terminal.HideSelection = false;
            this.Terminal.Location = new System.Drawing.Point(0, 0);
            this.Terminal.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Terminal.MinimumSize = new System.Drawing.Size(1, 1);
            this.Terminal.Name = "Terminal";
            this.Terminal.Padding = new System.Windows.Forms.Padding(2);
            this.Terminal.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Terminal.RectDisableColor = System.Drawing.Color.Blue;
            this.Terminal.ScrollBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Terminal.ShowText = false;
            this.Terminal.Size = new System.Drawing.Size(927, 534);
            this.Terminal.Style = Sunny.UI.UIStyle.Custom;
            this.Terminal.TabIndex = 0;
            this.Terminal.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.Terminal.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Terminal_MouseDown);
            this.Terminal.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Terminal_MouseUp);
            this.Terminal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBox1_KeyDown);
            // 
            // serverfile
            // 
            this.serverfile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.serverfile.Controls.Add(this.Serverfilegrid);
            this.serverfile.ImageIndex = 3;
            this.serverfile.Location = new System.Drawing.Point(201, 0);
            this.serverfile.Name = "serverfile";
            this.serverfile.Size = new System.Drawing.Size(927, 534);
            this.serverfile.TabIndex = 4;
            this.serverfile.Text = "Server-File";
            // 
            // Serverfilegrid
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.Serverfilegrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Serverfilegrid.BackgroundColor = System.Drawing.Color.White;
            this.Serverfilegrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Serverfilegrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Serverfilegrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Serverfilegrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.Serverfilegrid.EnableHeadersVisualStyles = false;
            this.Serverfilegrid.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Serverfilegrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.Serverfilegrid.Location = new System.Drawing.Point(3, 3);
            this.Serverfilegrid.Name = "Serverfilegrid";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Serverfilegrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.Serverfilegrid.RowHeadersWidth = 51;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Serverfilegrid.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.Serverfilegrid.RowTemplate.Height = 27;
            this.Serverfilegrid.ScrollBarRectColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.Serverfilegrid.SelectedIndex = -1;
            this.Serverfilegrid.Size = new System.Drawing.Size(921, 534);
            this.Serverfilegrid.StripeOddColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.Serverfilegrid.Style = Sunny.UI.UIStyle.Custom;
            this.Serverfilegrid.TabIndex = 1;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "icons8-发送-24.png");
            this.imageList1.Images.SetKeyName(1, "icons8-设置-24.png");
            this.imageList1.Images.SetKeyName(2, "icons8-仪表盘-24.png");
            this.imageList1.Images.SetKeyName(3, "icons8-共享文件夹-24.png");
            this.imageList1.Images.SetKeyName(4, "icons8-关于-24.png");
            this.imageList1.Images.SetKeyName(5, "icons8-命令行-24.png");
            // 
            // RedTeamChatf
            // 
            this.AcceptButton = this.sd;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1128, 572);
            this.CloseAskString = "Are you sure you want to exit?";
            this.Controls.Add(this.controlmenu1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsForbidAltF4 = true;
            this.MaximizeBox = false;
            this.Name = "RedTeamChatf";
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "RedTeamChat - Disconnected";
            this.TextAlignment = System.Drawing.StringAlignment.Center;
            this.ZoomScaleRect = new System.Drawing.Rectangle(19, 19, 1128, 505);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RedTeamChat_FormClosed);
            this.Controls.SetChildIndex(this.Aside, 0);
            this.Controls.SetChildIndex(this.controlmenu1, 0);
            this.Controls.SetChildIndex(this.Header, 0);
            this.about.ResumeLayout(false);
            this.console.ResumeLayout(false);
            this.setting.ResumeLayout(false);
            this.chatroom.ResumeLayout(false);
            this.controlmenu1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.serverfile.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Serverfilegrid)).EndInit();
            this.ResumeLayout(false);

        }


        #endregion

        public System.Windows.Forms.TabPage about;
        public System.Windows.Forms.TabPage console;
        public Sunny.UI.UIRichTextBox consolee;
        public System.Windows.Forms.TabPage setting;
        public Sunny.UI.UISmoothLabel uiSmoothLabel6;
        private Sunny.UI.UIIntegerUpDown port;
        private Sunny.UI.UISmoothLabel uiSmoothLabel5;
        private Sunny.UI.UISmoothLabel uiSmoothLabel4;
        private Sunny.UI.UITextBox server;
        private Sunny.UI.UISmoothLabel uiSmoothLabel3;
        private Sunny.UI.UISmoothLabel uiSmoothLabel2;
        public Sunny.UI.UISmoothLabel uiSmoothLabel1;
        public Sunny.UI.UITextBox nameset;
        public System.Windows.Forms.TabPage chatroom;
        public Sunny.UI.UILine uiLine1;
        public Sunny.UI.UIButton sd;
        public Sunny.UI.UITextBox inputbutton;
        public Sunny.UI.UITabControlMenu controlmenu1;
        private Sunny.UI.UILinkLabel link_jui;
        public Sunny.UI.UISmoothLabel uiSmoothLabel8;
        private Sunny.UI.UILine uiLine2;
        private Sunny.UI.UIButton con;
        private Sunny.UI.UIWaitingBar uiWaitingBar1;
        private Sunny.UI.UITextBox connecttrit;
        private Sunny.UI.UIButton uiButton2;
        private Sunny.UI.UIButton discon;
        private System.Windows.Forms.TabPage serverfile;
        private Sunny.UI.UIDataGridView Serverfilegrid;
        private Sunny.UI.UIButton refreshfilel;
        private Sunny.UI.UIFlowLayoutPanel uiFlowLayoutPanel1;
        private Sunny.UI.UIAvatar Emoji;
        private UIButton intranetfind;
        private ImageList imageList1;
        private System.ComponentModel.IContainer components;
        private TabPage tabPage1;
        private UIRichTextBox Terminal;
        private UIComboBox Atto;
    }
}