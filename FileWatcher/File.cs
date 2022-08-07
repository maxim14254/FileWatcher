using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileWatcher
{
    public class File : INotifyPropertyChanged
    {
        string catalog;
        string name;
        string path;
        string size;
        string date;

        public File(string name, string path, string size, string date)
        {
            this.name = name;
            this.path = path;
            this.size = size;
            this.date = date;
        }

        public string Catalog
        {
            get
            {
                return catalog;
            }
            set
            {
                catalog = value;
                OnPropertyChanged(value);
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
                OnPropertyChanged(value);
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
                OnPropertyChanged(value);
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
                OnPropertyChanged(value);
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
                OnPropertyChanged(value);
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
