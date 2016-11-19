using PacMan.UI.ViewModel;

namespace PacMan.UI.View
{
    /// <summary>
    /// Interaction logic for PlayGame.xaml
    /// </summary>
    public partial class PlayGame 
    {
        public PlayGame()

        {

            InitializeComponent();
            DataContext = new PlayGameViewModel(CanvasHost);

        }
    }
}
