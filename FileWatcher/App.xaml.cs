using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace FileWatcher
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string AppId = "jfi589r0-rkgw-rk49-gk0v-rj49frb45kk9";

        private readonly Semaphore instancesAllowed = new Semaphore(1, 1, AppId);

        private bool IsRunning { set; get; }

        private void OnExit(object sender, ExitEventArgs e)
        {
            if (this.IsRunning)
                this.instancesAllowed.Release();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            if (this.instancesAllowed.WaitOne(1000))
            {
                //new MainWindow().Show();
                this.IsRunning = true;
                return;
            }

            this.Shutdown();
        }
    }
}
