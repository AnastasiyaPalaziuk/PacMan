using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PacMan.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Logger log = LogManager.GetCurrentClassLogger();

        static App()
        {
           App.log.Debug("Запуск приложения");
        }

    }
}
