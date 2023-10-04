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

namespace chat
{
    public partial class Form1 : UIAsideHeaderMainFrame
    {

        public Form1()
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
                                parts[i]=parts[i].Replace("%r%", "\r\n");
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
            if (connectb && autodisconnect)
            {
                try
                {
                    clientSocket.Shutdown(SocketShutdown.Both);
                    contrigger = false;
                    nameset.Enabled = true;
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
            }
            try
            {
                IPAddress serverIP = IPAddress.Parse(server.Text);
                int serverPort = port.Value;
                IPEndPoint serverEP = new IPEndPoint(serverIP, serverPort);
                clientSocket.Connect(serverEP);
                consolee.Text += "Connected to the server " + serverIP + " port " + serverPort + "\n------------\n";
                tips();
                Thread receiveThread = new Thread(ReceiveData);
                receiveThread.Start(clientSocket);
                contrigger = true;
                connecttrit.Text = "Connected";
                nameset.Enabled = false;
                connecttrit.Enabled = false;
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
            discon.Show();
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
                        rcv.Text = "Disconnected";
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
                discon.Hide();
                nameset.Enabled = true;
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
        
    }
}
