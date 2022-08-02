using System;
using System.Collections.Generic;
using System.Linq;
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
        [DllImport("DLL1.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Create();

        [DllImport("DLL1.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool IsElevated(IntPtr lip);

        public MainWindow()
        {
            InitializeComponent();

            var aa = Create();
            bool f = IsElevated(aa);

            MessageBox.Show(f.ToString());
        }
    }
}
