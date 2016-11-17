using PacMan.UI.ViewModel;

namespace PacMan.UI.View
{
    /// <summary>
    /// Interaction logic for Level.xaml
    /// </summary>
    public partial class Level 
    {

        public Level()
        {
            InitializeComponent();
            DataContext = new LevelViewModel();
        }

     

        public Level(int level)
        {
            InitializeComponent();
            DataContext = new LevelViewModel(level);
        }
        
    }
}
