using PacMan.Domain.Concrete;
using PacMan.UI.Concrete;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PacMan.UI.ViewModel
{
     public class ScoreViewViewModel
    {
        private readonly DataGrid _playersScore;
        private ICommand _exit;
        private readonly bool _canExecute;
        private ICommand _dragMove;

        public ScoreViewViewModel(DataGrid playersScore)
        {
            _playersScore = playersScore;
           // _PlayersScore.RowBackground = (Color)ColorConverter.ConvertFromString("#E5250909");
            _canExecute = true;
            RelationshipDbAndDataGrid();
        }

        private void RelationshipDbAndDataGrid()
        {
            var context = new EfPlayerRepository();
            //int length = context.Players.Count<Player>();
            var sortedPlayers = from player in context.Players
                                orderby player.Score descending
                                select new { player.Name, player.Score};
            _playersScore.ItemsSource = sortedPlayers;
        }
        public ICommand DragMove => _dragMove ?? (_dragMove = new CommandHandler(DragMoveAction, _canExecute));
        public ICommand Exit => _exit ?? (_exit = new CommandHandler(ExitAction, _canExecute));

        private void ExitAction()
        {
            var menu = new Menu();
            var currentWin = Application.Current.Windows[0];
            currentWin?.Close();
            menu.Show();


        }
        private static void DragMoveAction()
        {
            var currentWin = Application.Current.Windows[0];
            currentWin?.DragMove();
        }


    }
}
