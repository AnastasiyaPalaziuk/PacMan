using PacMan.Logic.Abstract;
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
            InitializeComponent();
            DataContext = new MenuViewModel();
        }


    }
}
