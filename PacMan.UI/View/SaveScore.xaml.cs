using PacMan.UI.ViewModel;

namespace PacMan.UI.View
{
    /// <summary>
    /// Interaction logic for SaveScore.xaml
    /// </summary>
    public partial class SaveScore
    {
        public SaveScore(int score)
        {
            InitializeComponent();
            DataContext = new SaveScoreViewModel(score);
        }
    }
}
