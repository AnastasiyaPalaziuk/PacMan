using PacMan.UI.ViewModel;

namespace PacMan.UI
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu
    {
        public Menu()
        {
            App.Log.Trace("Запуск главного меню");    
            InitializeComponent();
            DataContext = new MenuViewModel();
            App.Log.Trace("Меню запущенно");


        }


    }
}
