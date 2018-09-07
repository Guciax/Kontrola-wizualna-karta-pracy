using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontrola_wizualna_karta_pracy
{
    public class ImageSynchronizer
    {
        public static void DoSynchronization()
        {
            if (!CheckNetworkDriveConnection()) return;

            DirectoryInfo basePath = new DirectoryInfo(@"C:\TempImages\");
            if (!basePath.Exists) return;
            DirectoryInfo[] listOfDataFolders = basePath.GetDirectories();

            List<DirectoryInfo> FoldersToDelete = new List<DirectoryInfo>();
            foreach (var dateFolder in listOfDataFolders)
            {
                DirectoryInfo[] lotFolders = dateFolder.GetDirectories();
                foreach (var lotFolder in lotFolders)
                {
                    if (TryCopyImages(lotFolder))
                    {
                        FoldersToDelete.Add(lotFolder);
                    }
                }
            }

            foreach (var folder in FoldersToDelete)
            {
                folder.Delete(true) ;
            }

            foreach (var dateDir in listOfDataFolders)
            {
                var lotDirs = dateDir.GetDirectories();
                if (lotDirs.Count()==0)
                {
                    try
                    {
                        dateDir.Delete();
                    }
                    catch { }
                }
            }
        }

        private static bool CheckNetworkDriveConnection()
        {
            var pDrive = new DirectoryInfo(@"P:\");
            if (pDrive.Exists) return true;

            ConnectPDrive();
            if (pDrive.Exists)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool TryCopyImages(DirectoryInfo lotFolder)
        {
            var localFiles = lotFolder.GetFiles();
            string[] pathSplitted = lotFolder.FullName.Split('\\');
            string date = pathSplitted[2];
            string lot = pathSplitted[3];

            var netDir = new DirectoryInfo(Path.Combine(@"P:\Kontrola_Wzrokowa", date, lot));

            Copy(lotFolder.FullName, netDir.FullName);
            
            if (netDir.Exists)
            {
                var netFiles = netDir.GetFiles();
                if (netFiles.Count() >= localFiles.Count())
                {
                    return true;
                }
            }
            return false;
            
        }

        private static void Copy(string sourceDir, string targetDir)
        {
            Directory.CreateDirectory(targetDir);

            foreach (var file in Directory.GetFiles(sourceDir))
                File.Copy(file, Path.Combine(targetDir, Path.GetFileName(file)),true);

        }

        private static void ConnectPDrive()
        {
            Process myProcess = new Process();
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            myProcess.StartInfo.CreateNoWindow = true;
            myProcess.StartInfo.UseShellExecute = false;
            myProcess.StartInfo.FileName = "cmd.exe";
            myProcess.StartInfo.Arguments = @"/c net use P: \\mstms005\shared /user:eprod plfm!234 /PERSISTENT:NO";
            myProcess.EnableRaisingEvents = true;
            myProcess.Start();
            myProcess.WaitForExit();
        }
    }
}
