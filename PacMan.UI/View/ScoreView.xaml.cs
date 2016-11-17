using PacMan.UI.ViewModel;

namespace PacMan.UI.View
{
    /// <summary>
    /// Interaction logic for ScoreView.xaml
    /// </summary>
    public partial class ScoreView
    {
        public ScoreView()
        {
            InitializeComponent();
            DataContext = new ScoreViewViewModel(PlayersScore);
        }
    }
}
