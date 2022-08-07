using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
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

        public ObservableCollection<File> Files { get; set; }
        public File File { get; set; }

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
            Files = new ObservableCollection<File>();
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog DirDialog = new FolderBrowserDialog();
            DirDialog.Description = "Выбор директории";
            DirDialog.SelectedPath = @"C:\";

            if (DirDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                label.Content = DirDialog.SelectedPath.Last() == '\\' ? DirDialog.SelectedPath.Remove(DirDialog.SelectedPath.Length - 1) : DirDialog.SelectedPath;
                GetCatalogs(label.Content.ToString());
            }
        }

        private void GetCatalogs(string str)
        {
            string path = $"{str}\\*";
            string s = GetCatalogs(DLL1, path.ToCharArray());

            Files.Clear();
            var rez = s.Split('|').ToList();
            rez.Remove(rez.Last());

            if (rez[0] == "." && rez[1] == "..")
            {
                rez.Remove(rez[0]);
                rez[0] = "...";
            }

            FileInfo file;
            string size;
            string date;
            for (int i = 0; i < rez.Count; ++i)
            {
                path = String.Format("{0}\\{1}", label.Content.ToString(), rez[i]);
                if (rez[i] == "...")
                {
                    var arr = path.Split('\\');
                    path = String.Join("\\", arr.Take(arr.Length - 2));
                    file = new FileInfo(label.Content.ToString());
                }
                else
                    file = new FileInfo(String.Format("{0}\\{1}", label.Content.ToString(), rez[i]));

                if (file.Attributes.ToString().Split(',').Any(w => w.Equals(" System")))
                    continue;

                if (file.Attributes == FileAttributes.Archive)
                    size = file.Length.ToString();
                else
                    size = "Папка";

                date = $"{file.LastWriteTime.ToShortDateString()} {file.LastWriteTime.ToShortTimeString()}";

                Files.Add(new File(rez[i], path, size, date));
            }

            Refresh(str);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DLL1 = Create();

            bool f = IsElevated(DLL1);
            button.IsEnabled = !f;

            Process info = Process.GetCurrentProcess();
            runSetting = new RunSetting(null, null, false);
            runSetting.Deserializable();

            if (runSetting.UAC)
            {
                label.Content = runSetting.Path;
                GetCatalogs(label.Content.ToString());
            }
            runSetting.ProcessId = info.Id;
            runSetting.UAC = false;
            runSetting.Path = string.Empty;
            runSetting.Serializable();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            runSetting.UAC = true;
            runSetting.Path = label.Content?.ToString();
            runSetting.Serializable();

            RunWithAdmin(DLL1, Assembly.GetExecutingAssembly().Location.ToCharArray());
        }

        private async void Refresh(string path)
        {
            if(await Task.Run<bool>(() => IsChangeCatalog(DLL1, path.ToCharArray())))
            {
                GetCatalogs(path);
            }
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            File item = (File)(sender as System.Windows.Controls.DataGridRow).Item;
            
            if (item.Size == "Папка")
            {
                label.Content = item.Path;
                GetCatalogs(item.Path);
            }
            else
            {
                System.Diagnostics.Process.Start(item.Path);
            }
        }
    }
}
