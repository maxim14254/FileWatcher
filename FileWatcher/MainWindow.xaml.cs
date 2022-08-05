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

        [DllImport("DLL1.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Create();

        [DllImport("DLL1.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IsElevated(IntPtr lip);

        [DllImport("DLL1.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void RunWithAdmin(IntPtr lip, char[] path);

        public MainWindow()
        {
            InitializeComponent();

            DLL1 = Create();


            bool f = IsElevated(DLL1);
            this.Title = f.ToString();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RunWithAdmin(DLL1, Assembly.GetExecutingAssembly().Location.ToCharArray());

            //Process info = Process.GetCurrentProcess();
            //info.FileName = Assembly.GetExecutingAssembly().Location;
            //info.UseShellExecute = true;
            //info.Verb = "runas"; // Provides Run as Administrator
            //info.Arguments = "-uca";
            //info.CreateNoWindow = false;

            //if (Process.Start(info) != null)
            //{
            //    // The user accepted the UAC prompt.
            //}
        }

    }
}
