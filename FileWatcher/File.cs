using FileWatcher.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Threading;

namespace FileWatcher
{
    public class File : INotifyPropertyChanged
    {
        BitmapSource imgSource;
        string name;
        string path;
        string size;
        string date;

        public File(string name, ref string path, ref string size, ref string date, Dictionary<string, BitmapSource> icons, string nameExtension)
        {
            this.name = name;
            this.path = path;
            this.size = size;
            this.date = date;
            //Bitmap icon;

            //try
            //{
            //    icon = System.Drawing.Icon.ExtractAssociatedIcon(path).ToBitmap();
            //}
            //catch
            //{
            //    icon = Resources._2.ToBitmap();
            //    nameExtension = "path";
            //}

            //if (!icons.ContainsKey(nameExtension.ToLower()))
            //{
            //    var img = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(icon.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            //    icons.Add(nameExtension.ToLower(), img);
            //}
            
            //imgSource = icons[nameExtension.ToLower()];
            //icon.Dispose();
        }

        public BitmapSource ImgSource
        {
            get
            {
                return imgSource;
            }
            set
            {
                imgSource = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public string Path
        {
            get
            {
                return path;
            }
            set
            {
                path = value;
            }

        }

        public string Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
            }
        }

        public string Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }
            return false;
        }
    }
}
