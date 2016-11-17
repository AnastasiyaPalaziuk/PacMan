using PacMan.Logic.Concrete;
using PacMan.Logic.Logic;
using PacMan.Logic.Model;
using PacMan.UI.Concrete;
using PacMan.UI.View;
using System.ComponentModel;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Input;

namespace PacMan.UI.ViewModel
{
    public class PlayGameViewModel : INotifyPropertyChanged
    {
        #region ICommand parametrs 

        private ICommand _dragMove;
        private ICommand _exit;
        private ICommand _moveManLeft;
        private ICommand _moveManRight;
        private ICommand _moveManUp;
        private ICommand _moveManDown;

        #endregion
        private readonly bool _canExecute;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private readonly GameLogic _game;
        private Level _level;

        private Thread _thread;
        private SaveScore _saveScore = new SaveScore(0);
        #region Window`s Action
        public ICommand DragMove
        {
            get
            {
                return _dragMove ?? (_dragMove = new CommandHandler(() => DragMoveAction(), _canExecute));
            }
        }

        private static void DragMoveAction()
        {
            App.Log.Trace("Перемещение окна PlayGame по экрану");

            var currentWin = System.Windows.Application.Current.Windows[0];
            currentWin?.DragMove();
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
            App.Log.Trace("Игра окончена");
            _saveScore.Dispatcher.Invoke(() =>
            {
                _saveScore = new SaveScore(_game.Score);
                _game.KillThread();
                _thread.Abort();
                App.Log.Debug("Убит поток {0}", _thread.Name);
                var currentWin = System.Windows.Application.Current.Windows;
                _saveScore.Show();
                currentWin[0]?.Close();
                currentWin[1]?.Close();/*ГРАБЛИ*/
            });

        }
        #endregion
        public PlayGameViewModel(Grid canvasHost)
        {
            _game = new GameLogic(canvasHost);
            ColorManager.SetCanvasHost(canvasHost);
            _canExecute = true;
            GamesLoop();
        }
        private bool _isUsed;
        private void GamesLoop()
        {
            App.Log.Trace("Запуск жизненного цикла игры");
            _game.StartGame();
            DisplayLevel();
            _thread = new Thread(() =>
            {
                while (_game.IsPlay())
                {
                    RaisePropertyChanged("Score");
                    RaisePropertyChanged("Life");
                    if (_game.ChangeLevel() && !_isUsed)
                    {
                        _game.Level++;
                        DisplayLevel();
                        RaisePropertyChanged("Level");
                        _isUsed = true;
                    }
                    if (!_game.ChangeLevel()) _isUsed = false;
                }
                ExitAction();
            }) {Name = "Game`s Loop"};
            App.Log.Debug("Старт потока {0}",_thread.Name);

            _thread.Start();
           
        }
        private void DisplayLevel()
        {
            App.Log.Trace("Отображение текущего уровня игры");

            if (_level == null)
            {

                _level = new Level(_game.Level);
                _level.Show();
                Thread.Sleep(1500);
                _level.Close();
            }
            else
            {
                _level.Dispatcher.Invoke(() =>
                {
                    _level = new Level(_game.Level);
                    _level.Show();
                    Thread.Sleep(1500);
                    _level.Close();
                });
            }
            App.Log.Trace("Окно текущего уровня закрыто");
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
