using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FileWatcher
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IntPtr DLL1;
        RunSetting runSetting;

        [DllImport("DLL1.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Create();

        [DllImport("DLL1.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IsElevated(IntPtr lip);

        [DllImport("DLL1.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
        public static extern void RunWithAdmin(IntPtr lip, char[] path);

        [DllImport("DLL1.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.BStr)]
        public static extern string GetCatalogs(IntPtr lip, char[] path);

        [DllImport("DLL1.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
        public static extern bool IsChangeCatalog(IntPtr lip, char[] path);

        [DllImport("DLL1.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
        public static extern void Token(IntPtr lip);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            runSetting.UAC = true;
            runSetting.Serializable();

            RunWithAdmin(DLL1, Assembly.GetExecutingAssembly().Location.ToCharArray());

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DLL1 = Create();

            bool f = IsElevated(DLL1);
            this.Title = f.ToString();

            Process info = Process.GetCurrentProcess();
            runSetting = new RunSetting(string.Empty, info.Id, false);
            runSetting.Serializable();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Token(DLL1);
            string ssss = @"E:\CCleaner\*";
            string s = GetCatalogs(DLL1, ssss.ToCharArray());

            List<string> catalogs = new List<string>();
            catalogs.AddRange(s.Split('|'));
            catalogs.Remove(catalogs.Last());
            if (catalogs[0] == "." && catalogs[1] == "..")
            {
                catalogs.Remove(catalogs[0]);
                catalogs[0] = "...";
            }
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string ssss = @"E:\Новая папка";
            await Task.Run<bool>(() => IsChangeCatalog(DLL1, ssss.ToCharArray()));
        }
        
    }
}
