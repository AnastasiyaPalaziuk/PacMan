using PacMan.Logic.Model;
using PacMan.UI.Concrete;
using PacMan.UI.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PacMan.UI.ViewModel
{
    class MenuVM 
    {
        public MenuVM()
        {
            _canExecute = true;
        }

        private ICommand _exit;
        private ICommand _dragMove;
        private ICommand _startGame;
        private ICommand _score;

        private bool _canExecute;

        public ICommand Exit
        {
            get
            {
                return _exit ?? (_exit = new CommandHandler(() => ExitAction(), _canExecute));
            }
        }
        private void ExitAction()
        {
            Environment.Exit(0);
        }
        public ICommand DragMove
        {
            get
            {
                return _dragMove ?? (_dragMove = new CommandHandler(() => DragMoveAction(),_canExecute));
            }
        }

        private void DragMoveAction()
        {
            var currentWin = Application.Current.Windows[0];
            currentWin.DragMove();
                
        }

        public ICommand StartGame
        {
            get
            {
                return _startGame ?? (_startGame = new CommandHandler(() => StartGameAction(), _canExecute));
            }
        }

        private void StartGameAction()
        {
            var currentWin = Application.Current.Windows[0];
            currentWin.Hide();

            PlayGame start = new PlayGame();
           

            start.Show();
            currentWin.Close();
        }

    }
}
