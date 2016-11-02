using PacMan.UI.Concrete;
using PacMan.UI.Concrete.Logic;
using PacMan.UI.Model;
using PacMan.UI.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace PacMan.UI.ViewModel
{
    class PlayGameVM : INotifyPropertyChanged
    {
        #region ICommand parametrs 

        private ICommand _scale;
        private ICommand _dragMove;
        private ICommand _exit;
        private ICommand _moveManLeft;
        private ICommand _moveManRight;
        private ICommand _moveManUp;
        private ICommand _moveManDown;

        #endregion
        private bool _canExecute;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private GameLogic _game;

        private Thread thread;
        private SaveScore saveScore = new SaveScore(0);
        #region Window`s Action
        public ICommand Scale
        {
            get
            {
                return _scale ?? (_scale = new CommandHandler(() => ScaleChange(), _canExecute));
            }
        }

        private void ScaleChange()
        {
            var currentWin = System.Windows.Application.Current.Windows[0];
            if (currentWin.WindowState.Equals(WindowState.Normal))
            {
                currentWin.WindowState = WindowState.Maximized;
            }
            else
            {
                currentWin.WindowState = WindowState.Normal;
            }
        }
        public ICommand DragMove
        {
            get
            {
                return _dragMove ?? (_dragMove = new CommandHandler(() => DragMoveAction(), _canExecute));
            }
        }

        private void DragMoveAction()
        {
            var currentWin = System.Windows.Application.Current.Windows[0];
            currentWin.DragMove();
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

            saveScore.Dispatcher.Invoke(() =>
            {
                saveScore = new SaveScore(_game.Score);
                _game.KillThread();
                thread.Abort();
                var currentWin = System.Windows.Application.Current.Windows;
                saveScore.Show();
                currentWin[0].Close();
                if (currentWin[1] != null) currentWin[1].Close();/*ГРАБЛИ*/
            });

        }
        #endregion
        public PlayGameVM(Grid CanvasHost)
        {
            _game = new GameLogic(CanvasHost);
            ColorManager.SetCanvasHost(CanvasHost);
            _canExecute = true;
            GamesLoop();
        }
        private void GamesLoop()
        {
            

            _game.StartGame();
            thread = new Thread(() =>
            {
                while (_game.IsPlay())
                {
                    RaisePropertyChanged("Score");
                    RaisePropertyChanged("Life");
                }
                ExitAction();
            });
            thread.Start();
           
        }

        #region Commands for move Man
        public ICommand MoveManLeft
        {
            get
            {
                return _moveManLeft ?? (_moveManLeft = new CommandHandler(() => _game.MoveAction(Side.Left), _canExecute));
            }
        }
        public ICommand MoveManRight
        {
            get
            {
                return _moveManRight ?? (_moveManRight = new CommandHandler(() => _game.MoveAction(Side.Right), _canExecute));
            }
        }
        public ICommand MoveManDown
        {
            get
            {
                return _moveManDown ?? (_moveManDown = new CommandHandler(() => _game.MoveAction(Side.Down), _canExecute));
            }
        }
        public ICommand MoveManUp
        {

            get
            {
                return _moveManUp ?? (_moveManUp = new CommandHandler(() => _game.MoveAction(Side.Up), _canExecute));
            }
        }

        #endregion


        public string Score
        {
            get
            {
                return "Score: " + _game.Score;
            }

        }

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Life
        {
            get
            {
                return "Life: " + _game.Lifes;
            }
        }
        public string Level
        {
            get { return "Level: " + _game.Level ; }
        }

    }
}
