using Sunny.UI;
using Sunny.UI.Win32;
using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System.Data;

namespace chat
{
    public partial class RedTeamChat : UIAsideHeaderMainFrame
    {

        public RedTeamChat()
        {
            InitializeComponent();
            discon.Hide();
        }
        [DllImport("user32.dll")]
        public static extern bool FlashWindow(IntPtr hwnd, bool bInvert);
        bool contrigger = false, connectb = false, autodisconnect = false;
        private Socket clientSocket;

        public void send(string msge, string name, string time, string type)
        {

            if (type == "common")
            {
                try
                {
                    byte[] data = Encoding.UTF8.GetBytes(msge + "//" + name + "//" + time);
                    clientSocket.Send(data);
                    inputbutton.Text = "";
                }
                catch (Exception e)
                {
                    consolee.Invoke(new Action(() =>
                    {
                        consolee.Text += "Exception: " + e.Message + "\n";
                    }));
                }
            }
            else if (type == "list")
            {
                try
                {
                    byte[] data = Encoding.UTF8.GetBytes("list");
                    clientSocket.Send(data);
                }
                catch (Exception e)
                {
                    consolee.Invoke(new Action(() =>
                    {
                        consolee.Text += "Exception: " + e.Message + "\n";
                    }));
                }
            }
        }

        public int typedetect(string rcvdata)
        {
            if (rcvdata == "common") return 1;
            else if (rcvdata == "list") return 2;
            else if (rcvdata == "log") return 3;
            else if (rcvdata =="file") return 4;
            return 0;
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
                    int typedet = typedetect(pt1);
                    if (typedet == 1)
                    {
                        string[] parts = pt2.Split(new string[] { "//" }, StringSplitOptions.None);
                        string firstString, secondString, thirdString;
                        if (parts.Length == 3)
                        {
                            firstString = parts[0];
                            secondString = parts[1];
                            thirdString = parts[2];
                            if (nameset.Text != secondString)
                            {
                                tips();
                            }
                            this.Invoke(new Action(() =>
                            {
                                rcv.Text += "User: " + secondString + " time: " + thirdString + "\n" + firstString + "\n";
                            }));
                        }
                    }
                    else if (typedet == 2)
                    {
                        tips();
                        int Tagn = TagNumbGet(pt2, "//");
                        string[] parts = pt2.Split(new string[] { "//" }, StringSplitOptions.None);
                        this.Invoke(new Action(() =>
                        {
                            consolee.Text += "-----------\n";
                        }));
                        for (int i = 0; i < Tagn; i++)
                        {
                            this.Invoke(new Action(() =>
                            {
                                consolee.Text += parts[i] + "\n";
                            }));
                        }
                        this.Invoke(new Action(() =>
                        {
                            consolee.Text += "-----------\n";
                        }));
                    }
                    else if (typedet == 3)
                    {
                        tips();
                        try
                        {
                            int Tagn = TagNumbGet(pt2, "##");
                            string[] parts = pt2.Split(new string[] { "##" }, StringSplitOptions.None);
                            string chatHistory = ""; // 用于累积聊天历史记录的字符串变量

                            for (int i = 0; i < Tagn; i++)
                            {
                                parts[i] = parts[i].Replace("%r%", "\r\n");
                                string[] part = parts[i].Split(new string[] { "//" }, StringSplitOptions.None);
                                string firstString, secondString, thirdString;

                                if (part.Length == 3)
                                {
                                    firstString = part[0];
                                    secondString = part[1];
                                    thirdString = part[2];

                                    // 将每次迭代的结果累积到chatHistory字符串中
                                    chatHistory += "User: " + secondString + " time: " + thirdString + "\n" + firstString + "\n";
                                }
                            }

                            this.Invoke(new Action((() =>
                            {
                                rcv.Text += "-------Chat history begin-------\n";
                                rcv.Text += chatHistory; // 将累积的结果添加到rcv.Text中
                                rcv.Text += "-------Chat history end---------\n";
                            })));

                            this.Invoke(new Action(() =>
                            {
                                consolee.Text += chatHistory; // 将累积的结果添加到consolee.Text中
                            }));
                        }
                        catch (Exception e)
                        {
                            this.Invoke(new Action(() =>
                            {
                                consolee.Text += e.Message;
                            }));
                            throw;
                        }
                    }else if (typedet == 4) 
                    {
                        
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            consolee.Text += "Received unknown data-type from " + server.Text + ":" + port.Value + "\n";
                        }));
                    }
                }
            }
            catch (Exception e)
            {
                consolee.Invoke(new Action(() =>
                {
                    consolee.Text += "Exception: " + e.Message + "\n";
                }));
            }
        }

        private void uiTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void uiTextBox1_TextChanged_1(object sender, EventArgs e)
        {

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
                if (inp[i] == '#' && inp[i + 1] == '#')
                {
                    return 1;
                }
                else if (inp[i] == '/' && inp[i + 1] == '/')
                {
                    return 0;
                }
                else if (inp[i] == '%' && inp[i + 1] == '%')
                {
                    return 3;
                }
                else if (inp[i] == '@' && inp[i + 1] == '@' && inp[i + 2] == '@')
                {
                    return 4;
                }
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
                else if (TagDetect(nameset.Text) == 0)
                {
                    MessageBox.Show("// is not allowed in name");
                }
                else if (TagDetect(nameset.Text) == 1)
                {
                    MessageBox.Show("## is not allowed in name");
                }
                else if (TagDetect(nameset.Text) == 3)
                {
                    MessageBox.Show("%% is not allowed in name");
                }
                else if (TagDetect(nameset.Text) == 4)
                {
                    MessageBox.Show("@@@ is not allowed in name");
                }
                else if (contrigger == true)
                {
                    send(inputbutton.Text, nameset.Text, currentTime.ToString(), "common");
                }

            }
            else if (TagDetect(inputbutton.Text) == 0)
            {
                MessageBox.Show("// is not allowed");
            }
            else if (TagDetect(inputbutton.Text) == 1)
            {
                MessageBox.Show("## is not allowed");
            }
            else if (TagDetect(inputbutton.Text) == 3)
            {
                MessageBox.Show("%% is not allowed");
            }
            else if (TagDetect(inputbutton.Text) == 4)
            {
                MessageBox.Show("@@@ is not allowed");
            }
            else
            {
                if (nameset.Text == "")
                {
                    MessageBox.Show("name cannot be blank");
                }
                else if (TagDetect(nameset.Text) == 0)
                {
                    MessageBox.Show("// is not allowed in name");
                }
                else if (TagDetect(nameset.Text) == 1)
                {
                    MessageBox.Show("## is not allowed in name");
                }
                else if (TagDetect(nameset.Text) == 3)
                {
                    MessageBox.Show("%% is not allowed in name");
                }
                else if (TagDetect(nameset.Text) == 4)
                {
                    MessageBox.Show("@@@ is not allowed in name");
                }
                else if (!contrigger)
                {
                    MessageBox.Show("U need to connect to the server before sending");
                }
                else
                {
                    send(inputbutton.Text, nameset.Text, currentTime.ToString(), "common");
                }
            }
        }
        private void inputbutton_TextChanged(object sender, EventArgs e)
        {

        }

        private void uiSmoothLabel1_Click(object sender, EventArgs e)
        {

        }

        private void uiTextBox1_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void consolee_TextChanged(object sender, EventArgs e)
        {

        }

        private void server_TextChanged(object sender, EventArgs e)
        {

        }

        private void uiSmoothLabel3_Click(object sender, EventArgs e)
        {

        }

        private void uiSmoothLabel5_Click(object sender, EventArgs e)
        {

        }

        private void link_jui_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/BadJui/");
        }

        public void con_Click(object sender, EventArgs e)
        {
            /*DataTable dataTable = new DataTable();
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
            uiDataGridView1.ContextMenuStrip = contextMenuStrip1;
            uiDataGridView1.ReadOnly = true;
            uiDataGridView1.DataSource = dataTable;
            uiDataGridView1.Dock = DockStyle.Fill;
            uiDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            uiDataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            uiDataGridView1.AllowUserToAddRows = false;*/

            if (connectb && autodisconnect)
            {
                try
                {
                    clientSocket.Shutdown(SocketShutdown.Both);
                    contrigger = false;
                    nameset.Enabled = true;
                    con.Enabled = true;
                    connecttrit.Text = "Disconnected";
                }
                catch (Exception exception)
                {
                    consolee.Invoke(new Action(() =>
                    {
                        consolee.Text += "Exception: " + exception.Message + "\n";
                    }));
                    throw;
                }
            }

            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            contrigger = false;
            if (nameset.Text=="")
            {
                MessageBox.Show("empty is not allowed");
            }else if (con.Enabled == false)
            {
                MessageBox.Show("U need to disconnect before connect to other server");
            }
            else
            {
                try
                {
                    IPAddress serverIP = IPAddress.Parse(server.Text);
                    int serverPort = port.Value;
                    IPEndPoint serverEP = new IPEndPoint(serverIP, serverPort);
                    clientSocket.Connect(serverEP);
                    consolee.Text += "Connected to the server " + serverIP + " port " + serverPort + "\n------------\n";
                    tips();
                    rcv.Text = "";
                    Thread receiveThread = new Thread(ReceiveData);
                    receiveThread.Start(clientSocket);
                    contrigger = true;
                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = "daemon.exe",
                        Arguments = serverIP.ToString()+serverPort.ToString()+nameset.Text,
                        CreateNoWindow = true,
                        UseShellExecute = false
                    };

                    Process process = new Process
                    {
                        StartInfo = psi
                    };

                    process.Start();
                    connecttrit.Text = "Connected";
                    nameset.Enabled = false;
                    con.Enabled = false;
                    discon.Show();
                    send("@@@Joined the server@@@", nameset.Text, DateTime.Now.ToString(), "common");
                }
                catch (Exception ex)
                {
                    this.Invoke(new Action(() =>
                        {
                            consolee.Text += "Exception: " + ex.Message + "\n";
                        }
                    ));
                    throw;

                }
            }
        }

        private void connecttrit_TextChanged(object sender, EventArgs e)
        {

        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (contrigger)
                {
                    send("none", "none", "none", "list");
                    consolee.Text += "Request -Get Userlist- sended\n";
                }
                else
                {
                    consolee.Text += "U need to connect before Getting Userlist\n";
                }
            }
            catch (Exception exception)
            {
                consolee.Text += exception.Message + "\n";
                throw;
            }
        }

        private void uiSmoothLabel7_Click(object sender, EventArgs e)
        {

        }

        private void uiSwitch1_ValueChanged(object sender, bool value)
        {
            autodisconnect = true;
        }

        private void discon_Click(object sender, EventArgs e)
        {
            if (contrigger)
            {
                try
                {
                    clientSocket.Shutdown(SocketShutdown.Both);
                    this.Invoke(new Action((() =>
                    {
                        connecttrit.Text = "Disconnected";
                        nameset.Enabled = true;
                        rcv.Text = "Disconnected\n";
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

        private void log_Click(object sender, EventArgs e)
        {
            try
            {
                if (contrigger)
                {
                    send("none", "none", "none", "log");
                    this.Invoke(new Action((() =>
                    {
                        consolee.Text += "Request -Get history- sended\n";
                    })));
                }
                else
                {
                    this.Invoke(new Action((() =>
                    {
                        consolee.Text += "U need to connect before Getting history\n";
                    })));
                }
            }
            catch (Exception exception)
            {
                this.Invoke(new Action((() =>
                {
                    consolee.Text += exception.Message + "\n";
                })));
                throw;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void uiScrollingText1_Click(object sender, EventArgs e)
        {

        }

        private void uiWaitingBar1_Click(object sender, EventArgs e)
        {

        }

        public void tips()
        {
            this.Invoke(new Action((() =>
            {
                IntPtr windowHandle = Process.GetCurrentProcess().MainWindowHandle;
                FlashWindow(windowHandle, true);
            })));
        }

        private void uiDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void RemoveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 获取当前选中的行
            DataGridViewRow selectedRow = uiDataGridView1.CurrentRow;

            if (selectedRow != null)
            {
                // 获取选中行的文件名数据
                string fileName = selectedRow.Cells["文件名"].Value.ToString();
                MessageBox.Show($"移除文件: {fileName}", "提示");

                // 移除选中的行
                uiDataGridView1.Rows.Remove(selectedRow);
            }
        }
        private void RenameFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 获取当前选中的行
            DataGridViewRow selectedRow = uiDataGridView1.CurrentRow;

            if (selectedRow != null)
            {
                // 获取选中行的文件名数据
                string oldFileName = selectedRow.Cells["文件名"].Value.ToString();

                // 创建一个新的输入对话框窗口
                inpfilename inputDialog = new inpfilename();

                // 设置对话框窗口的初始文件名
                inputDialog.filenm.Text = oldFileName;

                // 显示输入对话框窗口并获取用户输入的新文件名
                if (inputDialog.ShowDialog() == DialogResult.OK)
                {
                    // 获取用户输入的新文件名
                    string newFileName = inputDialog.filenm.Text;
                    if (oldFileName==inputDialog.filenm.Text)
                    {
                        MessageBox.Show("File name has not changed");
                    }
                    else
                    {
                        send(inputDialog.filenm.Text,nameset.Text,"","file");
                    }
                }
            }
        }
    }
}
