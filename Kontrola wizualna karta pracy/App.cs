using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kontrola_wizualna_karta_pracy
{
    public partial class App
    {
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(String lpClassName, String lpWindowName);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public static void bringToFront(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);

            foreach (Process p in processes)
            {
                IntPtr windowHandle = p.MainWindowHandle;
                SetForegroundWindow(windowHandle);
            }
            
        }

        public static bool ConnectNetworkDrives()
        {
            System.Diagnostics.ProcessStartInfo ProcStartInfo =  new System.Diagnostics.ProcessStartInfo("cmd");
            ProcStartInfo.RedirectStandardOutput = true;
            ProcStartInfo.UseShellExecute = false;
            ProcStartInfo.CreateNoWindow = false;
            ProcStartInfo.RedirectStandardError = true;
            System.Diagnostics.Process MyProcess = new System.Diagnostics.Process();
            ProcStartInfo.Arguments = @"/c start /wait \\mstms001\NETLOGON\logon.bat";
            MyProcess.StartInfo = ProcStartInfo;
            MyProcess.Start();
            MyProcess.WaitForExit();
            return true;
        }

        public static void RunOrBringToFront(string processName)
        {
            string processPath = ConfigurationManager.AppSettings["MatchingerPath"];

            Process[] processes = Process.GetProcessesByName(processName);

            if (processes.Length == 0) // Process not running
            {
                bool fileAvailable = true;
                if (!System.IO.Directory.Exists(@"Y:\APPS\"))
                {
                    fileAvailable = false;
                    try
                    {
                        if (ConnectNetworkDrives())
                        {
                            fileAvailable = true;
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Brak sieci!");
                    }
                }
                if (fileAvailable) Process.Start(processPath);
            }
            else // Process running
            {
                bringToFront(processName);
            }
        }

    }
}