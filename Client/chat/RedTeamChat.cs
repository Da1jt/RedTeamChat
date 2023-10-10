using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI.Win32;
using System.Diagnostics;

namespace chat
{
    internal static class RedTeamChat
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new RedTeamChatf());
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
    }
}
