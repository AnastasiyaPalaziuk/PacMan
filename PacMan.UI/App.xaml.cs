using NLog;

namespace PacMan.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public static Logger Log = LogManager.GetCurrentClassLogger();

        static App()
        {
           Log.Debug("Запуск приложения");
        }

    }
}
