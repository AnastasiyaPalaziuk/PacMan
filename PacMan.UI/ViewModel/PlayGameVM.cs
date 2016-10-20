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
        private object valueLocker = new object();
        private object valueLocker2 = new object();

        private bool _canExecute;
        private ICommand _scale;
        private ICommand _dragMove;
        private ICommand _exit;
        private ICommand _moveManLeft;
        private ICommand _moveManRight;
        private ICommand _moveManUp;
        private ICommand _moveManDown;
        private int _level = 0;
        private Random random = new Random();
        //private ICommand _score;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private int _boardSize = 20;
        private Grid _CanvasHost;
        private bool gridIsUsed = false;
        private Man _man;
        private Board _board;
        private Bonus _bonus;
        private BadBoy _badBoy;
        private Thread thread;
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
            SaveScore saveScore = new SaveScore(_man.Score);
            var currentWin = System.Windows.Application.Current.Windows[0];
            currentWin.Close();
            saveScore.Show();

        }

        #endregion

        #region Hide++++++++++++++++++++



        public PlayGameVM(Grid CanvasHost)
        {

            _canExecute = true;
            _CanvasHost = CanvasHost;
            _bonus = new Bonus();
            _man = new Man();
            _badBoy = new BadBoy()
            {
                CurrentCoordinateX = 0,
                CurrentCoordinateY = 0
            };
            StartGame();
        }

        private void StartGame()
        {
            _level++;
            RaisePropertyChanged("Level");
            DisplayLevel();
            //if (_board == null) _board = new Board(_boardSize);
            //else
            _board = new Board(_boardSize);
            //_board.UpdateBoard();
            //bonus = new Bonus();
            _man.CurrentCoordinateX = _boardSize / 2;
            _man.CurrentCoordinateY = _boardSize / 2;
            ViewBoard();
            //thread = new Thread(OneStepBadBoy);
            //thread.Start();

        }
        private void DisplayLevel()
        {
            Level level = new Level(_level);
            level.Show();
            Thread.Sleep(1500);
            level.Close();
        }

        private void ViewBoard()
        {
            setManOnBoard();
            setBadBoysOnBoard();
            setBoardComponents();

        }
        private void setBoardComponents()
        {
            if (gridIsUsed)
            {
                clearGrid();
            }
            else
            {
                for (int i = 0; i < _boardSize; i++)
                {
                    _CanvasHost.ColumnDefinitions.Add(new ColumnDefinition());
                    _CanvasHost.RowDefinitions.Add(new RowDefinition());
                    gridIsUsed = true;
                }

            }
            for (int j = 0; j < _boardSize; j++)
            {
                for (int i = 0; i < _boardSize; i++)
                {
                    Canvas c = new Canvas();
                    switch (_board.BoardElement[i, j])
                    {
                        case BoardElements.Way:
                            c.Background = new SolidColorBrush(Colors.White);
                            break;
                        case BoardElements.Wall:
                            c.Background = new SolidColorBrush(Colors.Black);
                            break;
                        case BoardElements.Bonus:
                            c.Background = new SolidColorBrush(Colors.Red); break;
                        case BoardElements.Man:
                            c.Background = new SolidColorBrush(Colors.Yellow); break;
                        case BoardElements.BadBoy:
                            c.Background = new SolidColorBrush(Colors.Green); break;
                    }

                    Grid.SetColumn(c, i);
                    Grid.SetRow(c, j);
                    _CanvasHost.Children.Add(c);
                }

            }
        }
        private void clearGrid()
        {
            for (int i = 0; i < _boardSize; i++)
            {
                for (int j = 0; j < _boardSize; j++)
                {
                    var Item = _CanvasHost.Children
                        .Cast<UIElement>()
                        .FirstOrDefault(item => Grid.GetColumn(item) == j && Grid.GetRow(item) == i);
                    _CanvasHost.Children.Remove(Item);
                }
            }
        }

        private void setManOnBoard()
        {
            if (_board.BoardElement[_man.CurrentCoordinateY, _man.CurrentCoordinateX] == BoardElements.Bonus)
                _board.QualityBonus--;
            _board.BoardElement[_man.CurrentCoordinateY, _man.CurrentCoordinateX] = BoardElements.Man;
            //var Item = _CanvasHost.Children
            //    .Cast<UIElement>()
            //    .FirstOrDefault(i => Grid.GetColumn(i) == _man.CurrentCoordinateY && Grid.GetRow(i) == _man.CurrentCoordinateX);
            //((Canvas)Item).Background = new SolidColorBrush(Colors.Yellow);


        }
        private void setBadBoysOnBoard()
        {
            _board.QualityBonus--;
            _board.BoardElement[_badBoy.CurrentCoordinateY, _badBoy.CurrentCoordinateX] = BoardElements.BadBoy;
            //var Item = _CanvasHost.Children
            //    .Cast<UIElement>()
            //    .FirstOrDefault(i => Grid.GetColumn(i) == _man.CurrentCoordinateY && Grid.GetRow(i) == _man.CurrentCoordinateX);
            //((Canvas)Item).Background = new SolidColorBrush(Colors.Yellow);


        }

        private void ChangeElementColor(int i, int j, SolidColorBrush color)
        {
            Object obj = new object();
            Dispatcher.CurrentDispatcher.Invoke(new Action(() =>
            {
                var Item =
                    _CanvasHost.Children.Cast<UIElement>()
                        .FirstOrDefault(item => Grid.GetColumn(item) == j && Grid.GetRow(item) == i);
                ((Canvas) Item).Background = color;
            }));
        }

        private SolidColorBrush ChangeColor(BoardElements element)
        {
            switch (element)
            {
                case BoardElements.BadBoy:
                    return new SolidColorBrush(Colors.Green);
                case BoardElements.Bonus:
                    return new SolidColorBrush(Colors.Red);
                case BoardElements.Man:
                    return new SolidColorBrush(Colors.Yellow);
                case BoardElements.Way:
                    return new SolidColorBrush(Colors.White);
                case BoardElements.Wall:
                    return new SolidColorBrush(Colors.Black);
                default: return new SolidColorBrush(Colors.Brown);
            }

        }

        #region Commands for move Man
        public ICommand MoveManLeft
        {
            get
            {
                return _moveManLeft ?? (_moveManLeft = new CommandHandler(() => MoveAction(Side.Left), _canExecute));
            }
        }
        public ICommand MoveManRight
        {
            get
            {
                return _moveManRight ?? (_moveManRight = new CommandHandler(() => MoveAction(Side.Right), _canExecute));
            }
        }
        public ICommand MoveManDown
        {
            get
            {
                return _moveManDown ?? (_moveManDown = new CommandHandler(() => MoveAction(Side.Down), _canExecute));
            }
        }
        public ICommand MoveManUp
        {

            get
            {
                return _moveManUp ?? (_moveManUp = new CommandHandler(() => MoveAction(Side.Up), _canExecute));
            }
        }

        #endregion


        #region Move Man
        private bool isPressedLeft = true;
        private void MoveActionLeft()
        {

            Thread.Sleep(400);
            if (_man.CurrentCoordinateX != 0 && CheckCell(_board.BoardElement[_man.CurrentCoordinateX - 1, _man.CurrentCoordinateY]))
                OneStep(Side.Left);
            else
                isPressedLeft = false;

        }

        private void MoveAction(Side key)
        {
            switch (key)
            {
                case Side.Left:
                    if (_man.CurrentCoordinateX != 0 && CheckCell(_board.BoardElement[_man.CurrentCoordinateX - 1, _man.CurrentCoordinateY]))
                    {
                        OneStepBadBoy();
                        OneStep(Side.Left);
                    }
                    break;
                case Side.Right:
                    if (_man.CurrentCoordinateX != _boardSize - 1 && CheckCell(_board.BoardElement[_man.CurrentCoordinateX + 1, _man.CurrentCoordinateY]))
                        OneStep(Side.Right);
                    break;
                case Side.Up:
                    if (_man.CurrentCoordinateY != 0 && CheckCell(_board.BoardElement[_man.CurrentCoordinateX, _man.CurrentCoordinateY - 1]))
                        OneStep(Side.Up);
                    break;
                case Side.Down:
                    if (_man.CurrentCoordinateY != _boardSize - 1 && CheckCell(_board.BoardElement[_man.CurrentCoordinateX, _man.CurrentCoordinateY + 1]))
                        OneStep(Side.Down);
                    break;
            }
            if (_board.QualityBonus == 0)
                StartGame();

        }
        private bool CheckCell(BoardElements boardElement)
        {
            switch (boardElement)
            {
                case BoardElements.Way:
                    break;
                case BoardElements.Bonus:
                    _man.Score += _bonus.Value;
                    _board.QualityBonus--;
                    RaisePropertyChanged("Score");
                    break;
                case BoardElements.Wall: return false;
            }
            return true;
        }


        private void OneStep(object key)
        {
            Side sideKey = (Side)key;
            OldCellForMan();
            switch (sideKey)
            {
                case Side.Left:
                    _man.StepLeft();
                    break;
                case Side.Right:
                    _man.StepRight();
                    break;
                case Side.Up:
                    _man.StepUp();
                    break;
                case Side.Down:
                    _man.StepDown();
                    break;
            }

            NewCellForMan();
            //  Thread.Sleep(400);

        }

        private void OldCellForMan()
        {

            _board.BoardElement[_man.CurrentCoordinateX, _man.CurrentCoordinateY] = BoardElements.Way;
            ChangeElementColor(_man.CurrentCoordinateY, _man.CurrentCoordinateX, ChangeColor(BoardElements.Way));
        }
        private void NewCellForMan()
        {

            _board.BoardElement[_man.CurrentCoordinateX, _man.CurrentCoordinateY] = BoardElements.Man;
            ChangeElementColor(_man.CurrentCoordinateY, _man.CurrentCoordinateX, ChangeColor(BoardElements.Man));

        }

        #endregion

        #endregion
        public string Score
        {
            get
            {
                return "Score: " + _man.Score;
            }

        }

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Move Bad Boy

        private void OneStepBadBoy()
        {
            OldCellForBadBoy();
            RandomMoveBodyBadBoy();
            NewCellForBadBoy();
        }
        private void OldCellForBadBoy()
        {
            _board.BoardElement[_badBoy.CurrentCoordinateX, _badBoy.CurrentCoordinateY] = _badBoy.LastStep;
            ChangeElementColor(_badBoy.CurrentCoordinateY, _badBoy.CurrentCoordinateX, ChangeColor(_badBoy.LastStep));
        }
        private void NewCellForBadBoy()
        {
            _board.BoardElement[_badBoy.CurrentCoordinateX, _badBoy.CurrentCoordinateY] = BoardElements.BadBoy;
            ChangeElementColor(_badBoy.CurrentCoordinateY, _badBoy.CurrentCoordinateX, ChangeColor(BoardElements.BadBoy));

        }
        private void RandomMoveBodyBadBoy()
        {
            switch (random.Next() % 4)
            {
                case 0:
                    if (_badBoy.CurrentCoordinateY != _boardSize - 1)
                    {
                        _badBoy.LastStep = _board.BoardElement[_badBoy.CurrentCoordinateX, _badBoy.CurrentCoordinateY + 1];
                        _badBoy.StepDown();
                    }
                    break;
                case 1:
                    if (_badBoy.CurrentCoordinateX != 0)
                    {
                        _badBoy.LastStep = _board.BoardElement[_badBoy.CurrentCoordinateX - 1, _badBoy.CurrentCoordinateY];
                        _badBoy.StepLeft();

                    }
                    break;
                case 2:
                    if (_badBoy.CurrentCoordinateX != _boardSize - 1)
                    {
                        _badBoy.LastStep = _board.BoardElement[_badBoy.CurrentCoordinateX + 1, _badBoy.CurrentCoordinateY];
                        _badBoy.StepRight();
                    }
                    break;
                case 3:
                    if (_badBoy.CurrentCoordinateY != 0)
                    {
                        _badBoy.LastStep = _board.BoardElement[_badBoy.CurrentCoordinateX, _badBoy.CurrentCoordinateY - 1];
                        _badBoy.StepUp();
                    }
                    break;
            }
        }
        #endregion
        public string Level
        {
            get { return "Level: " + _level; }
        }

    }
}
