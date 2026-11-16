using System;
using System.Windows.Forms;
using QuanLyBanXoi.GUI;

namespace QuanLyBanXoi
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new FormMain());
        }
    }
}