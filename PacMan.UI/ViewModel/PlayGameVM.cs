using PacMan.Logic.Concrete;
using PacMan.Logic.Logic;
using PacMan.Logic.Model;
using PacMan.UI.Concrete;
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
        private Level level;

        private Thread thread;
        private SaveScore saveScore = new SaveScore(0);
        #region Window`s Action
        public ICommand DragMove
        {
            get
            {
                return _dragMove ?? (_dragMove = new CommandHandler(() => DragMoveAction(), _canExecute));
            }
        }

        private void DragMoveAction()
        {
            App.log.Trace("Перемещение окна PlayGame по экрану");

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
            App.log.Trace("Игра окончена");
            saveScore.Dispatcher.Invoke(() =>
            {
                saveScore = new SaveScore(_game.Score);
                _game.KillThread();
                thread.Abort();
                App.log.Debug("Убит поток {0}", thread.Name);
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
        private bool IsUsed = false;
        private void GamesLoop()
        {
            App.log.Trace("Запуск жизненного цикла игры");
            _game.StartGame();
            DisplayLevel();
            thread = new Thread(() =>
            {
                
                while (_game.IsPlay())
                {
                    RaisePropertyChanged("Score");
                    RaisePropertyChanged("Life");
                    if (_game.ChangeLevel() && !IsUsed)
                    {
                        _game.Level++;
                        DisplayLevel();
                        RaisePropertyChanged("Level");
                        IsUsed = true;
                    }
                    if (!_game.ChangeLevel()) IsUsed = false;
                }
                ExitAction();
            });
            thread.Name = "Game`s Loop";
            App.log.Debug("Старт потока {0}",thread.Name);

            thread.Start();
           
        }
        private void DisplayLevel()
        {
            App.log.Trace("Отображение текущего уровня игры");

            if (level == null)
            {

                level = new Level(_game.Level);
                level.Show();
                Thread.Sleep(1500);
                level.Close();
            }
            else
            {
                level.Dispatcher.Invoke(new Action(() =>
                {
                    level = new Level(_game.Level);
                    level.Show();
                    Thread.Sleep(1500);
                    level.Close();
                }));
            }
            App.log.Trace("Окно текущего уровня закрыто");
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
