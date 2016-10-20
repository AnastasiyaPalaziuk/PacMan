using PacMan.Domain.Concrete;
using PacMan.Domain.Entities;
using PacMan.UI.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PacMan.UI.ViewModel
{
    class ScoreViewVM
    {
        private DataGrid _PlayersScore;
        private ICommand _exit;
        private bool _canExecute;
        private ICommand _dragMove;

        public ScoreViewVM(DataGrid PlayersScore)
        {
            _PlayersScore = PlayersScore;
           // _PlayersScore.RowBackground = (Color)ColorConverter.ConvertFromString("#E5250909");
            _canExecute = true;
            RelationshipDbAndDataGrid();
        }

        private void RelationshipDbAndDataGrid()
        {
            EFPlayerRepository context = new EFPlayerRepository();
            //int length = context.Players.Count<Player>();
            var SortedPlayers = from player in context.Players
                                orderby player.Score descending
                                select new { player.Name, player.Score};
            _PlayersScore.ItemsSource = SortedPlayers;
        }
        public ICommand DragMove
        {
            get
            {
                return _dragMove ?? (_dragMove = new CommandHandler(() => DragMoveAction(), _canExecute));
            }
        }

  


        public ICommand Exit
        {
            get
            {
                return _exit ?? (_exit = new CommandHandler(() => ExitAction(), _canExecute));
            }
        }
        private void ExitAction()
        {
            Menu menu = new Menu();
            var currentWin = System.Windows.Application.Current.Windows[0];
            currentWin.Close();
            menu.Show();


        }
        private void DragMoveAction()
        {
            var currentWin = Application.Current.Windows[0];
            currentWin.DragMove();

        }


    }
}
