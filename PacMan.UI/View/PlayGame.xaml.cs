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
            App.Log.Trace("Запуск игры");

            InitializeComponent();
            DataContext = new PlayGameViewModel(CanvasHost);
            App.Log.Trace("Игра запущенна");

        }
    }
}
