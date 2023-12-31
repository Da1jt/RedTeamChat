﻿using Sunny.UI;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using static chat.emoji;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace chat
{
    public partial class RedTeamChatf : UIAsideHeaderMainFrame
    {
        public RedTeamChatf()
        {
            INIProcess();
        }

        void INIProcess()
        {
            try
            {
                InitializeComponent();
                discon.Hide();
                Application.EnableVisualStyles();
                refreshfilel.Enabled = false;
                server.Text = "127.0.0.1";
                Terminal.Text += $"->Ini Success\n->Use 'help' to get help\n[{DateTime.Now}]  cmd> ";
                this.Icon = Properties.Resources.chat;
                priviousnumb = Terminal.Text.Length - 1;
                AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
                Serverfilegrid.Dock = DockStyle.Fill;
                Serverfilegrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                Serverfilegrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                Serverfilegrid.AllowUserToAddRows = false;
                Serverfilegrid.ReadOnly = true;
                Serverfilegrid.AllowUserToDeleteRows = false;
                consolee.Text += "Initial Success\n";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Err");
                consolee.Text += "Initial Failed:\n" + e.Message + "\n";
            }
        }
        [DllImport("user32.dll")]
        private static extern bool FlashWindow(IntPtr hwnd, bool bInvert);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);
        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            AssemblyName assemblyName = new AssemblyName(args.Name);

            string resourceName = "chat.Resources." + assemblyName.Name + ".dll";

            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                if (stream != null)
                {
                    byte[] assemblyData = new byte[stream.Length];
                    stream.Read(assemblyData, 0, assemblyData.Length);
                    return Assembly.Load(assemblyData);
                }
                return null;
            }
        }

        bool _contrigger = false, _connectb = false;
        private Socket _clientSocket;
        public void Send(string msge, string time, string type)
        {
            string name = nameset.Text; string towho = Atto.Text;
            if (type == "common")
            {
                try
                {
                    byte[] data = Encoding.UTF8.GetBytes(msge + "//" + name + "//" + time + "//" + towho);
                    _clientSocket.Send(data);
                    inputbutton.Text = "";
                }
                catch (Exception e)
                {
                    consolee.Invoke(new Action(() =>
                    {
                        consolee.Text += "Exception1: " + e.Message + "\n";
                    }));
                }
            }
            else if (type == "list")
            {
                try
                {
                    byte[] data = Encoding.UTF8.GetBytes("list");
                    _clientSocket.Send(data);
                }
                catch (Exception e)
                {
                    consolee.Invoke(new Action(() =>
                    {
                        consolee.Text += "Exception2: " + e.Message + "\n";
                    }));
                }
            }
            else if (type == "encode")
            {
                try
                {
                    string dataString = msge + "//" + name + "//" + time + "//" + towho;
                    byte[] dataBytes = Encoding.UTF8.GetBytes(dataString);
                    string base64EncodedData = Base64Encode(Encoding.UTF8, dataBytes.ToString());
                    _clientSocket.Send(Encoding.UTF8.GetBytes(base64EncodedData));
                    inputbutton.Text = "";
                }
                catch (Exception e)
                {
                    consolee.Invoke(new Action(() =>
                    {
                        consolee.Text += "Exception3: " + e.Message + "\n";
                    }));
                }
            }
            else if (type == "cmd")
            {
                try
                {
                    byte[] data = Encoding.UTF8.GetBytes("cmd%%" + msge + "//" + name + "//" + time);
                    _clientSocket.Send(data);
                }
                catch (Exception e)
                {
                    consolee.Invoke(new Action(() =>
                    {
                        consolee.Text += "Exception4: " + e.Message + "\n";
                    }));
                }
            }
        }
        public int Typedetect(string rcvdata)
        {
            switch (rcvdata)
            {
                case "common": return 1;
                case "list": return 2;
                case "log": return 3;
                case "file": return 4;
                case "emsg": return 5;
                default: return 0;
            }
        }
        public void ReceiveData(object clientSocket)
        {

            Socket socket = (Socket)clientSocket;
            byte[] buffer = new byte[2048];

            try
            {
                while (true)
                {
                    int bytesRead = socket.Receive(buffer);
                    string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    string[] aft = receivedData.Split(new string[] { "%%" }, StringSplitOptions.None);
                    string pt1 = aft[0], pt2 = aft[1];
                    int typedet = Typedetect(pt1);
                    reception_analysis(pt2, typedet);
                }
            }
            catch (Exception e)
            {
                if (e.Message == "远程主机强迫关闭了一个现有的连接。")
                {
                    Lableadd("服务器断开连接", "sysinfo", "none");
                    Disconnectgo();
                }
                consolee.Invoke(new Action(() =>
                {
                    consolee.Text += "Exception5: " + e.Message + "\n";
                }));
            }
        }

        public int TagNumbGet(string originstring, string tag)
        {
            MatchCollection matches = Regex.Matches(originstring, tag);
            return matches.Count;
        }
        public int TagDetect(string inp)
        {
            for (int i = 0; i < inp.Length - 1; i++)
            {
                if (inp[i] == '#' && inp[i + 1] == '#') return 1;
                else if (inp[i] == '/' && inp[i + 1] == '/') return 0;
                else if (inp[i] == '%' && inp[i + 1] == '%') return 3;
                else if (inp[i] == '@' && inp[i + 1] == '@' && inp[i + 2] == '@') return 4;
                else if (inp[i] == '-' && inp[i + 1] == '>') return 5;
            }
            return 2;
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            DateTime currentTime = DateTime.Now;
            if (inputbutton.Text == "")
            {
                MessageBox.Show("empty is not allowed");
            }
            else if (inputbutton.Text.Length < 2)
            {
                if (nameset.Text == "")
                {
                    MessageBox.Show("name cannot be blank");
                }
                else if (_contrigger && !(Valider(inputbutton.Text, "")))
                {
                    Send(inputbutton.Text, currentTime.ToString(), "common");
                }

            }
            else if (!(Valider(inputbutton.Text, "")))
            {

                if (nameset.Text == "")
                {
                    MessageBox.Show("name cannot be blank");
                }
                else if (!(Valider(nameset.Text, "name")))
                {
                    Send(inputbutton.Text, currentTime.ToString(), "common");
                }
            }
        }

        private bool Valider(string i, string t)
        {
            if (_contrigger)
            {
                switch (TagDetect(i))
                {
                    case 0:
                        MessageBox.Show("// is not allowed in " + t);
                        return true;
                    case 1:
                        MessageBox.Show("## is not allowed in " + t);
                        return true;
                    case 3:
                        MessageBox.Show("%% is not allowed in " + t);
                        return true;
                    case 4:
                        MessageBox.Show("@@@ is not allowed in " + t);
                        return true;
                    case 5:
                        MessageBox.Show("-> is not allowed in " + t);
                        return true;
                    default:
                        return false;
                }
            }
            else
            {
                MessageBox.Show("U need to connect to the server before sending");
                return true;
            }
        }
        private void link_jui_Click(object sender, EventArgs e)
        {
            string JUIlink = "https://github.com/BadJui/";
            System.Diagnostics.Process.Start(JUIlink);
        }

        public void con_Click(object sender, EventArgs e)
        {
            if (_connectb)
            {
                try
                {
                    _clientSocket.Shutdown(SocketShutdown.Both);
                    nameset.Enabled = true;
                    con.Enabled = true;
                    connecttrit.Text = "Disconnected";
                }
                catch (Exception exception)
                {
                    consolee.Invoke(new Action(() =>
                    {
                        consolee.Text += "Exception6: " + exception.Message + "\n";
                    }));
                    throw;
                }
            }
            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _contrigger = false;
            if (nameset.Text == "")
            {
                MessageBox.Show("empty is not allowed");
            }
            else if (!con.Enabled)
            {
                MessageBox.Show("U need to disconnect before connect to other server");
            }
            else
            {
                try
                {
                    IPAddress serverIp;
                    if (!IPAddress.TryParse(server.Text, out serverIp))
                    {
                        IPAddress[] ipAddresses = Dns.GetHostAddresses(server.Text);
                        if (ipAddresses.Length != 1)
                        {
                            StringBuilder buildstr = new StringBuilder();
                            string[] dips = new string[ipAddresses.Length];
                            for (int i = 0; i < ipAddresses.Length; i++)
                            {
                                dips[i] = ipAddresses[i].ToString();
                            }
                            string finout = "[The domain name contains multiple IP addresses. Please specify a single IP or use an IP-only domain name]\nIncluded IPs:\n";
                            for (int i = 0; i < Math.Min(6, dips.Length); i++)
                            {
                                buildstr.Append(dips[i] + "\n");
                            }
                            finout += buildstr.ToString();
                            if (dips.Length > 6)
                            {
                                finout += "...";
                            }
                            MessageBox.Show(finout, "Error");
                            return;
                        }
                        serverIp = ipAddresses[0];
                    }
                    int serverPort = port.Value;
                    IPEndPoint serverEp = new IPEndPoint(serverIp, serverPort);
                    _clientSocket.Connect(serverEp);
                    consolee.Text += $"------------\nConnected to the server {serverIp}:{serverPort} \n------------\n";
                    Tips();
                    Thread receiveThread = new Thread(ReceiveData);
                    receiveThread.Start(_clientSocket);
                    _contrigger = true;
                    this.Text = $"RedTeamChat - {nameset.Text}  - {serverIp}:{serverPort}";
                    connecttrit.Text = "Connected";
                    nameset.Enabled = false;
                    con.Enabled = false;
                    controlmenu1.SelectedTab = chatroom;
                    discon.Show();
                    Send("@@@Joined the server@@@", DateTime.Now.ToString(), "common");
                    Send("none", "none", "list");
                    Atto.Enabled = true;
                    
                    Lableadd("Self-Connected", "sysinfo", "none");
                }
                catch (Exception ex)
                {
                    this.Invoke(new Action(() =>
                    {
                        consolee.Text += "Exception7: " + ex.Message + "\n";
                        MessageBox.Show(ex.Message, "Err");
                    }
                    ));
                }
            }
        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (_contrigger)
                {
                    Send("none", "none", "list");
                    consolee.Text += "Request -Get Userlist- sended\n";

                }
                else
                {
                    consolee.Text += "U need to connect before Getting Userlist\n";
                }

                controlmenu1.SelectedTab = console;
            }
            catch (Exception exception)
            {
                consolee.Text += exception.Message + "\n";
                throw;
            }
        }
        private void reception_analysis(string receptioninfo, int typedet)
        {
            if (typedet == 1)
            {
                ana_common(receptioninfo);
            }
            else if (typedet == 2)
            {
                Tips();
                int tagn = TagNumbGet(receptioninfo, "//");
                string[] parts = receptioninfo.Split(new string[] { "//" }, StringSplitOptions.None);
                this.Invoke(new Action(() =>
                {
                    Atto.Items.Clear();
                    Atto.Items.Add("@All");
                    consolee.Text += "[----Online-UserList-Start---\n";
                    for (int i = 0; i <= tagn; i++)
                    {
                        consolee.Text += " " + parts[i] + "\n";
                        Atto.Items.Add($"@{parts[i]}");
                    }
                    consolee.Text += "----Online-UserList-End---]\n";
                }));
            }
            else if (typedet == 3)
            {
                Tips();
                try
                {
                    int tagn = TagNumbGet(receptioninfo, "##");
                    string[] parts = receptioninfo.Split(new string[] { "##" }, StringSplitOptions.None);
                    this.Invoke(new Action((() =>
                    {
                        Lableadd("[-------Chat-history-begin-------", "text", "none");
                    })));

                    for (int i = 0; i < tagn; i++)
                    {
                        parts[i] = parts[i].Replace("%r%", "\r\n");
                        ana_common(parts[i]);
                        Thread.Sleep(100);

                    }
                    this.Invoke(new Action((() =>
                    {
                        Lableadd("-------Chat-history-end---------]", "text", "none");
                    })));
                }
                catch (Exception e)
                {
                    this.Invoke(new Action(() =>
                    {
                        consolee.Text += e.Message;
                    }));
                    throw;
                }
            }
            else if (typedet == 4)
            {
                File_System(receptioninfo);
            }
            else if (typedet == 5)
            {
                string decoded = Base64Decode(receptioninfo);
                if (decoded.StartsWith(nameset.Text))
                {
                    ana_common(decoded);
                }
            }
            else
            {
                this.Invoke(new Action(() =>
                {
                    consolee.Text += "Received unknown data-type from " + server.Text + ":" + port.Value + "\n";
                }));
            }
        }

        private void ana_common(string raw)
        {
            string[] parts = raw.Split(new string[] { "//" }, StringSplitOptions.None);
            string firstString, secondString, thirdString, fourthString;
            if (parts.Length == 4)
            {
                firstString = parts[0];
                secondString = parts[1];
                thirdString = parts[2];
                fourthString = parts[3];

                if (fourthString.EndsWith(nameset.Text) || secondString == nameset.Text || fourthString == "@All")
                {
                    Tips();
                }
                else
                {
                    return;
                }

                this.Invoke(new Action((() =>
                {
                    string col = String.Empty;
                    if (fourthString.EndsWith(nameset.Text) ||
                        (secondString == nameset.Text) && !fourthString.EndsWith("@All"))  
                    {
                        col = "oran";
                    }
                    else
                    {
                        col = "origin";
                    }
                    if (firstString.StartsWith("[") && firstString.EndsWith("]"))
                    {
                        if (secondString == nameset.Text)
                        {
                            lock (lockObject)
                            {
                                Lableadd(" User:  " + secondString + "(me)->" + fourthString + " time:  " + thirdString,
                                    "text", col);
                            }
                        }
                        else
                        {
                            lock (lockObject)
                            {
                                Lableadd(" User:  " + secondString + "->" + fourthString + " time:  " + thirdString,
                                    "text", col);
                            }
                        }
                        lock (lockObject)
                        {
                            Emojianalysis(firstString);
                        }
                    }
                    else if (firstString.EndsWith("@@@"))
                    {
                        if (secondString == nameset.Text)
                        {
                            lock (lockObject)
                            {
                                Lableadd(" User:  " + secondString + "(me) time:  " + thirdString + "\n[" + firstString + "]", "sysinfo", "none");
                            }
                        }
                        else
                        {
                            lock (lockObject)
                            {
                                Lableadd(" User:  " + secondString + " time:  " + thirdString + "\n[" + firstString + "]", "sysinfo", "none");
                            }
                        }
                    }
                    else
                    {
                        if (secondString == nameset.Text)
                        {
                            lock (lockObject)
                            {
                                Lableadd(
                                    " User:  " + secondString + "(me)->" + fourthString + " time:  " + thirdString +
                                    "\n", "text", col);
                                Lableadd(firstString, "msgbox", col);
                            }
                        }
                        else
                        {
                            lock (lockObject)
                            {
                                Lableadd(
                                    " User:  " + secondString + "->" + fourthString + " time:  " + thirdString + "\n",
                                    "text", col);
                                Lableadd(firstString, "msgbox", col);
                            }

                        }
                    }
                    Thread.Sleep(100);
                })));
            }
        }

        private void discon_Click(object sender, EventArgs e)
        {
            Disconnectgo();
        }

        private void Disconnectgo()
        {
            if (_contrigger)
            {
                try
                {
                    this.Invoke(new Action((() =>
                    {
                        DateTime currentTime = DateTime.Now;
                        Send("@@@Exit the server@@@", currentTime.ToString(), "common");
                        _clientSocket.Shutdown(SocketShutdown.Both);
                        connecttrit.Text = "Disconnected";
                        nameset.Enabled = true;
                        _contrigger = false;
                        Atto.Enabled = false;
                        consolee.Text += "Disconnected\n";
                        this.Text = "RedTeamChat - Disconnected";
                        Lableadd("Self-[Disconnected]", "sysinfo", "none");
                        discon.Hide();
                        con.Enabled = true;
                    })));
                }
                catch (Exception exception)
                {
                    this.Invoke(new Action((() =>
                    {
                        consolee.Text += exception + "\n";
                    })));
                    throw;
                }
            }
        }
        public void Tips()
        {
            this.Invoke(new Action((() =>
            {
                IntPtr windowHandle = Process.GetCurrentProcess().MainWindowHandle;
                FlashWindow(windowHandle, true);
            })));
        }

        private int Filedef(string rd)
        {
            if (rd.StartsWith("FileDisplay")) return 1;
            else if (rd.StartsWith("FileInfo")) return 2;
            else return 0;
        }

        private void File_System(string rawdata)
        {
            /*int rete = Filedef(rawdata);
            if (rete == 1)
            {
                string raw2 = string.Empty;
                raw2 = rawdata.Substring(11);
                try
                {
                    int tagn = TagNumbGet(rawdata, "//");
                    string[] parts = rawdata.Split(new string[] { "//" }, StringSplitOptions.None);
                    for (int i = 0; i < tagn; i++)
                    {

                    }
                }
                catch (Exception e)
                {
                    this.Invoke(new Action(() =>
                    {
                        consolee.Text += e.Message;
                    }));
                    throw;
                }
            }
            else if (rete == 2)
            {
                
            }*/
        }
        private void refreshfilel_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("文件名");
            dataTable.Columns.Add("大小");
            dataTable.Columns.Add("更改日期");

            // 添加文件项
            DataRow row1 = dataTable.NewRow();
            row1["文件名"] = "File1";
            row1["大小"] = "10 KB";
            row1["更改日期"] = "2021-09-01";
            dataTable.Rows.Add(row1);

            DataRow row2 = dataTable.NewRow();
            row2["文件名"] = "File2";
            row2["大小"] = "5 KB";
            row2["更改日期"] = "2021-09-02";
            dataTable.Rows.Add(row2);

            DataRow row3 = dataTable.NewRow();
            row3["文件名"] = "File3";
            row3["大小"] = "8 KB";
            row3["更改日期"] = "2021-09-03";
            dataTable.Rows.Add(row3);

            // 创建右键菜单的实例
            ContextMenuStrip contextMenuStrip1 = new ContextMenuStrip();

            // 创建菜单项 "移除文件"
            ToolStripMenuItem removeFileToolStripMenuItem = new ToolStripMenuItem("移除文件");
            removeFileToolStripMenuItem.Click += RemoveFileToolStripMenuItem_Click;

            // 创建菜单项 "重命名"
            ToolStripMenuItem renameFileToolStripMenuItem = new ToolStripMenuItem("重命名");
            renameFileToolStripMenuItem.Click += RenameFileToolStripMenuItem_Click;

            // 将菜单项添加到右键菜单中
            contextMenuStrip1.Items.Add(removeFileToolStripMenuItem);
            contextMenuStrip1.Items.Add(renameFileToolStripMenuItem);

            // 将右键菜单关联到 DataGridView 控件
            Serverfilegrid.ContextMenuStrip = contextMenuStrip1;
            Serverfilegrid.DataSource = dataTable;
        }

        private void RemoveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 获取当前选中的行
            DataGridViewRow selectedRow = Serverfilegrid.CurrentRow;

            if (selectedRow != null)
            {
                // 获取选中行的文件名数据
                string fileName = selectedRow.Cells["文件名"].Value.ToString();
                MessageBox.Show($"移除文件: {fileName}", "提示");

                // 移除选中的行
                Serverfilegrid.Rows.Remove(selectedRow);
            }
        }
        private void Emoji_Click(object sender, EventArgs e)
        {
            if (_contrigger)
            {
                emoji emojiForm = new emoji();
                emojiForm.ShowDialog();
                Emojisend(GlobalData.emojiset);

            }
            else
            {
                MessageBox.Show("Please connect first", "Not allowed");
            }
        }
        public void Emojisend(string data)
        {
            DateTime currentTime = DateTime.Now;
            if (emoji.GlobalData.emojiset != "none")
            {
                Send("[" + emoji.GlobalData.emojiset + "]", currentTime.ToString(), "common");
            }

        }
        private void RenameFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 获取当前选中的行
            DataGridViewRow selectedRow = Serverfilegrid.CurrentRow;

            if (selectedRow != null)
            {
                string oldFileName = selectedRow.Cells["文件名"].Value.ToString();

                inpfilename inputDialog = new inpfilename();

                if (inputDialog.ShowDialog() == DialogResult.OK)
                {
                    if (oldFileName == inpfilename.GlobalVariables.Filenamechange)
                    {
                        MessageBox.Show("File name has not changed");
                    }
                    else
                    {
                        Send(inpfilename.GlobalVariables.Filenamechange, "", "file");
                    }
                }
            }
        }

        private readonly object lockObject = new object();
        private void Lableadd(string inp, string type, string color)
        {
            lock (lockObject)
            {
                if (type == "text")
                {
                    this.Invoke(new Action((() =>
                    {
                        Label label = new Label();
                        label.Text = inp;
                        label.AutoSize = true;
                        if (color == "oran")
                        {
                            label.ForeColor = Color.Orange;
                        }
                        else if (color == "origin")
                        {
                            label.ForeColor = Color.Teal;
                        }
                        
                        uiFlowLayoutPanel1.Controls.Add(label);
                        uiFlowLayoutPanel1.ScrollControlIntoView(label);
                    })));
                }
                else if (type == "msgbox")
                {
                    UIRichTextBox richTextBox = new UIRichTextBox();
                    if (color == "oran")
                    {
                        richTextBox.FillColor = Color.Orange;
                    }
                    else if (color == "origin")
                    {
                        richTextBox.FillColor = Color.SkyBlue;
                    }
                    richTextBox.Text = inp;
                    richTextBox.ReadOnly = true;
                    richTextBox.Radius = 11;
                    richTextBox.AutoWordSelection = true;
                    richTextBox.Font = new System.Drawing.Font("等线", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    AdjustRichTextBoxSize(richTextBox);
                    uiFlowLayoutPanel1.Controls.Add(richTextBox);
                }
                else if (type == "img")
                {
                    this.Invoke(new Action((() =>
                    {
                        try
                        {
                            PictureBox pictureBox = new PictureBox();
                            pictureBox.Size = new Size(200, 200);
                            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                            Image image = Image.FromFile("C:\\Users\\" + Environment.UserName + "\\RedTeamTemp\\" + inp);
                            pictureBox.Image = image;
                            uiFlowLayoutPanel1.Controls.Add(pictureBox);
                            uiFlowLayoutPanel1.ScrollControlIntoView(pictureBox);
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message, "Err");
                            consolee.Text += e.Message + "\n";
                        }
                    })));
                }
                else if (type == "emoji")
                {
                    this.Invoke(new Action(() =>
                    {
                        PictureBox pictureBox = new PictureBox();
                        pictureBox.Size = new Size(80, 80);
                        pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                        System.Reflection.Assembly assembly = GetType().Assembly;
                        Stream emg = assembly.GetManifestResourceStream("chat.Resources." + inp + ".png");
                        pictureBox.Image = Image.FromStream(emg);
                        uiFlowLayoutPanel1.Controls.Add(pictureBox);
                    }));
                }
                else if (type == "sysinfo")
                {
                    this.Invoke(new Action((() =>
                    {
                        Label label = new Label();
                        label.Text = inp;
                        label.AutoSize = true;
                        label.ForeColor = Color.Gray;
                        uiFlowLayoutPanel1.Controls.Add(label);
                        uiFlowLayoutPanel1.ScrollControlIntoView(label);
                    })));
                }
            }
        }
        public void Emojianalysis(string emojiname)
        {
            switch (emojiname)
            {
                case "[Doge]": Lableadd("Doge", "emoji", "none");
                    break;
                case "[E]": Lableadd("E", "emoji", "none");
                    break;
                default: Lableadd(emojiname, "text", "none");
                    break;
            }
        }
        private void RedTeamChat_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            DateTime currentTime = DateTime.Now;
            if (_contrigger)
            {
                Send("@@@Exit the server@@@", currentTime.ToString(), "common");
                _clientSocket.Shutdown(SocketShutdown.Both);
            }
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "taskkill",
                Arguments = "/pid " + Process.GetCurrentProcess().Id + " /f",
                CreateNoWindow = true,
                UseShellExecute = false
            };
            Process process = new Process
            {
                StartInfo = psi
            };
            process.Start();
        }
        private void AdjustRichTextBoxSize(UIRichTextBox richTextBox)
        {
            using (Graphics graphics = richTextBox.CreateGraphics())
            {
                SizeF textSize = graphics.MeasureString(richTextBox.Text, richTextBox.Font);
                richTextBox.Size = new Size((int)textSize.Width + 20, (int)textSize.Height + 20);
            }
        }
        public static string Base64Encode(Encoding encodeType, string source)
        {
            string encode = string.Empty;
            byte[] bytes = encodeType.GetBytes(source);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = source;
            }
            return encode;
        }
        public static string Base64Decode(string result)
        {
            return Base64Decode(Encoding.UTF8, result);
        }
        public static string Base64Decode(Encoding encodeType, string result)
        {
            string decode = string.Empty;
            byte[] bytes = Convert.FromBase64String(result);
            try
            {
                decode = encodeType.GetString(bytes);
            }
            catch
            {
                decode = result;
            }
            return decode;
        }
        private TcpListener _listener;
        private bool _isListening = false;

        private void intranetfind_Click(object sender, EventArgs e)
        {
            if (_isListening)
            {
                _isListening = false;
            }
            else
            {
                _isListening = true;
            }
            Interanet();
        }

        /*private void intranetfind_Click(object sender, EventArgs e)
{
       this.Invoke(new Action((() =>
       {
           consolee.Text += "Interanet Server Scan Start\n";
       })));
       listener = new Socket(SocketType.Dgram, ProtocolType.Udp); // 初始化listener
       IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 8085);
       listener.Bind(endPoint);
       intranetThread = new Thread((() => interanet(listener)));
       intranetThread.Start();
       Thread.Sleep(TimeSpan.FromSeconds(1));

}*/

        private bool wfcmd = false;
        private int hisnmb = 0, hisselect = 0, priviousnumb;
        private string RCEKey = String.Empty;
        private string[] cmdhiStrings = new string[1337];
        private string _terminalipset = "127.0.0.1", _terminalportset = "8084";
        private async void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!Terminal.Text.EndsWith(" "))
            {
                Terminal.Text += " ";
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                Terminal.ReadOnly = true;
                
                try
                {
                    int currentLineIndex = Terminal.GetLineFromCharIndex(Terminal.SelectionStart);
                    string previousLine = Terminal.Lines[currentLineIndex];
                    previousLine = previousLine.Substring(26, previousLine.Length - 27);
                    if (previousLine.StartsWith("   "))
                    {
                        previousLine = previousLine.Substring(1, previousLine.Length);
                    }
                    else if (previousLine.EndsWith("   "))
                    {
                        previousLine = previousLine.Substring(0, previousLine.Length - 1);
                    }

                    
                    cmdhiStrings[hisnmb] = previousLine;
                    hisnmb++;
                    hisselect = hisnmb;
                    Terminal.Text += "\n";
                    if (previousLine == "help")
                    {
                        Terminal.Text +=
                            "rkey [your rcekey]    Set the RCEKey\nrmkey              Remove the RCEKey\ncls                 Clear The Screen\nti [ip]   Set Target Terminal Ip(Default Localhost)\ntp [post]   Set Target Terminal Port(Default 8084)\ncc           Remove Connect Setting\ncfg         Show Connect Setting\n";
                    }
                    else if (previousLine.StartsWith("rkey "))
                    {
                        if (RCEKey == String.Empty)
                        {
                            RCEKey = previousLine.Substring(5);
                            Terminal.Text += "->RCEKey Set\n";
                        }
                        else
                        {
                            Terminal.Text += "->RCEKey Already Set\n";
                        }
                    }
                    else if (previousLine == "rmkey")
                    {
                        if (RCEKey == String.Empty)
                            Terminal.Text += "->RCEKey Was Empty\n";
                        else
                        {
                            RCEKey = String.Empty;
                            Terminal.Text += "->RCEKey Removed\n";
                        }
                    }
                    else if (previousLine == "cls")
                        Terminal.Text = "->Ini Success\n";
                    else if (previousLine.StartsWith("ti "))
                    {
                        if (IPAddress.TryParse(previousLine.Substring(3), out IPAddress ip))
                        {
                            _terminalipset = previousLine.Substring(3);
                            Terminal.Text += $"->Terminal Terget Set:{_terminalipset}:{_terminalportset}\n";
                        }
                        else
                            Terminal.Text += $"->Invalid IP\nTerminal Terget:{_terminalipset}:{_terminalportset}\n";
                        
                    }
                    else if (previousLine.StartsWith("tp "))
                    {
                        if (previousLine.Substring(3).ToInt() >= 0 && previousLine.ToInt() <= 65535)
                        {
                            _terminalportset = previousLine.Substring(3);
                            Terminal.Text += $"->Terminal Terget Set:{_terminalipset}:{_terminalportset}\n";
                        }
                        else
                            Terminal.Text += $"->Invalid Port\nTerminal Terget:{_terminalipset}:{_terminalportset}\n";
                        

                    }
                    else if (previousLine == "cc")
                    {
                        _terminalipset = "127.0.0.1";
                        _terminalportset = "8084";
                        Terminal.Text += "->Terminal Setting Clear\n";
                    }
                    else if (previousLine == "cfg")
                        Terminal.Text += $"->Terminal Target {_terminalipset}:{_terminalportset}\n";
                    else
                    {
                        if (previousLine != String.Empty && previousLine != "cmd")
                        {
                                if (RCEKey != String.Empty)
                                {
                                    string url = $"http://{_terminalipset}:{_terminalportset}/?command={previousLine}";
                                    Uri requestUri = new Uri(url);
                                    HttpClientHandler handler = new HttpClientHandler();
                                    using (HttpClient client = new HttpClient(handler))
                                    {
                                        try
                                        {
                                            HttpResponseMessage response = await client.GetAsync(requestUri);
                                            response.EnsureSuccessStatusCode();
                                            string responseBody = await response.Content.ReadAsStringAsync();
                                            if (responseBody == string.Empty)
                                                responseBody = $"'{previousLine}' 不是内部或外部命令，也不是可运行的程序";

                                            Terminal.Text += $"\n->{responseBody}\n[{DateTime.Now}]  cmd> ";
                                            Terminal.SelectionLength = 0;
                                            wfcmd = true;
                                            this.Invoke(new Action((() =>
                                            {
                                                Terminal.Focus();
                                                SendKeys.Send("{TAB}");
                                                SendKeys.Send("{Right}");
                                                SendKeys.Send("{BACKSPACE}");
                                            })));
                                        }
                                        catch (HttpRequestException ex)
                                        {
                                            consolee.Text += ($"请求失败: {ex.Message}\n");
                                            Terminal.Text += $"\n->Access Deniel\n[{DateTime.Now}]  cmd> ";
                                            Terminal.SelectionStart = Terminal.Text.Length;
                                            Terminal.SelectionLength = 0;
                                            wfcmd = true;
                                            this.Invoke(new Action((() =>
                                            {
                                                IntPtr foregroundWindowHandle = GetForegroundWindow();
                                                GetWindowThreadProcessId(foregroundWindowHandle,
                                                    out int foregroundProcessId);
                                                int currentProcessId = Process.GetCurrentProcess().Id;
                                                if (foregroundProcessId == currentProcessId)
                                                {
                                                    Terminal.Focus();
                                                    SendKeys.Send("{TAB}");
                                                    SendKeys.Send("{Right}");
                                                    SendKeys.Send("{BACKSPACE}");
                                                }
                                                else
                                                {
                                                    controlmenu1.Focus();
                                                }
                                            })));
                                        }
                                    }
                                }
                                else
                                    Terminal.Text += "RCEKey Was Empty\n";
                        }
                    }
                    if (!wfcmd)
                    {
                        Terminal.Text += $"[{DateTime.Now}]  cmd> ";
                        Terminal.SelectionStart = Terminal.Text.Length;
                        Terminal.SelectionLength = 0;
                        this.Invoke(new Action((() =>
                        {
                            SendKeys.Send("{TAB}");
                            SendKeys.Send("{Right}");
                            SendKeys.Send("{BACKSPACE}");

                        })));
                    }
                    
                    wfcmd = false;
                    Terminal.Text = Terminal.Text.Substring(0, Terminal.Text.Length - 1);
                    priviousnumb = Terminal.Text.Length - 1;
                    Terminal.SelectionStart-=2;
                    SendKeys.Send("{BACKSPACE}");
                }
                catch (Exception exception)
                {
                    Terminal.Text += $"\n[{DateTime.Now}]  cmd> ";
                    consolee.Text += exception.Message + "\n";
                    SendKeys.Send("{TAB}");
                    SendKeys.Send("{BACKSPACE}");
                }

                Terminal.ReadOnly = false;
            }
            else if (e.KeyCode == Keys.Back)
            {
                e.Handled = true;
                int cursorPosition = Terminal.SelectionStart;

                if (GetCurrentLineText().Length > 27)
                {
                    string ltxet = String.Empty;
                    StringBuilder buildstr = new StringBuilder();
                    for (int i = 1; i <= 6; i++)
                        buildstr.Append(Terminal.Text[cursorPosition - i]);
                    ltxet = buildstr.ToString();
                    if (ltxet != ">dmc ]")
                    {
                        Terminal.Text = Terminal.Text.Remove(cursorPosition - 1, 1);
                        Terminal.SelectionStart = cursorPosition - 1;
                    }

                }
            }
            else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {

                e.Handled = true;
                try
                {
                    if (e.KeyCode == Keys.Left)
                    {
                        int cursorPosition = Terminal.SelectionStart;
                        string ltxet = String.Empty;
                        StringBuilder buildstr = new StringBuilder();
                        for (int i = 1; i <= 4; i++)
                            buildstr.Append(Terminal.Text[cursorPosition - i]);

                        ltxet = buildstr.ToString();
                        if (ltxet != ">dmc")
                            Terminal.SelectionStart -= 1;
                    }
                    else if (e.KeyCode == Keys.Right && Terminal.SelectionStart != Terminal.Text.Length - 1)
                    {
                        Terminal.SelectionStart++;
                    }
                    else if (e.KeyCode == Keys.Up)
                    {
                        if (hisselect != 0)
                        {
                            --hisselect;
                            if (hisselect < 0)
                                hisselect++;
                            Terminal.Text = Terminal.Text.Substring(0, priviousnumb + 1) + cmdhiStrings[hisselect] + " ";
                            SendKeys.Send("{TAB}");
                            SendKeys.Send("{Right}");
                            SendKeys.Send("{BACKSPACE}");
                            Terminal.SelectionStart = Terminal.Text.Length;
                        }
                    }
                    else if (e.KeyCode == Keys.Down)
                    {
                        if (hisselect < hisnmb)
                            hisselect++;
                        
                        int cursorPosition = Terminal.SelectionStart;
                        int currentLineIndex = Terminal.GetLineFromCharIndex(cursorPosition);
                        MessageBox.Show(Terminal.Lines[currentLineIndex], Terminal.Lines[currentLineIndex].Length.ToString());
                        Terminal.Lines[currentLineIndex] = Terminal.Lines[currentLineIndex].Substring(27, Terminal.Lines[currentLineIndex].Length);

                        //Terminal.Text = Terminal.Text.Substring(0, priviousnumb + 1) + cmdhiStrings[hisselect] + " ";
                        //SendKeys.Send("{TAB}");
                        //SendKeys.Send("{Right}");
                        //SendKeys.Send("{BACKSPACE}");

                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
            else if (e.KeyCode == Keys.Tab)
            {
                e.Handled = true;
                Terminal.SelectionStart = Terminal.Text.Length - 1;
                Terminal.SelectionLength = 0;
            }
        }

        private string GetCurrentLineText()
        {
            int cursorPosition = Terminal.SelectionStart;
            int currentLineIndex = Terminal.GetLineFromCharIndex(cursorPosition);
            string currentLineText = Terminal.Lines[currentLineIndex];
            return currentLineText;
        }

        private void inputbutton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                e.Handled = true;
                inputbutton.Text += "\n";
            }
        }

        private void Atto_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void Atto_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Atto_MouseClick(object sender, MouseEventArgs e)
        {
            Send("none", "none", "list");
        }

        private void Terminal_MouseDown(object sender, MouseEventArgs e)
        {
            Terminal.SelectionStart = Terminal.TextLength - 1;
            Terminal.SelectionLength = 0;
        }


        public virtual void Terminal_MouseUp(object sender, MouseEventArgs e)
        {
            Terminal.SelectionStart = Terminal.TextLength - 1;
            Terminal.SelectionLength = 0;
        }

        private void Interanet()
        {
            if (_isListening)
            {
                int port_ = 1234;
                _listener = new TcpListener(IPAddress.Any, port_);
                _listener.Start();

                _listener.BeginAcceptTcpClient(OnClientConnected, null);

                connecttrit.Text = "Listening";
                _isListening = true;
            }
            else
            {
                _listener.Stop();
                _listener = null;

                connecttrit.Text = "StopListening";
                _isListening = false;
            }
        }
        private void OnClientConnected(IAsyncResult ar)
        {
            TcpClient client = _listener.EndAcceptTcpClient(ar);

            string message = "getip";
            byte[] data = Encoding.UTF8.GetBytes(message);
            client.GetStream().Write(data, 0, data.Length);
            //client.Close();
            _listener.BeginAcceptTcpClient(OnClientConnected, null);
        }
    }
}