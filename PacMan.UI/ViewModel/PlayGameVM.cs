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
    class PlayGameVM
    {
        // private object valueLocker = new object();
        private bool _canExecute;
        private ICommand _scale;
        private ICommand _dragMove;
        private ICommand _exit;
        private ICommand _moveManLeft;
        private ICommand _moveManRight;
        private ICommand _moveManUp;
        private ICommand _moveManDown;
        private int _level = 0;
        //private ICommand _score;
        public event PropertyChangedEventHandler PropertyChanged;
        private int _boardSize = 10;
        private Grid _CanvasHost;
        private bool gridIsUsed = false;
        private Man _man;
        private Board board;

        private Bonus bonus;

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
            Menu menu = new Menu();
            var currentWin = System.Windows.Application.Current.Windows[0];
            currentWin.Close();
            menu.Show();
        }

        #endregion

        public PlayGameVM(Grid CanvasHost)
        {


            _canExecute = true;
            _CanvasHost = CanvasHost;
            bonus = new Bonus();
            _man = new Man();
            StartGame();
        }

        private void StartGame()
        {
            _level++;
            DisplayLevel();
            if (board == null) board = new Board(_boardSize);
            else
                board.UpdateBoard();
            //bonus = new Bonus();
            _man.CurrentCoordinateX = _boardSize / 2;
            _man.CurrentCoordinateY = _boardSize / 2;
            ViewBoard();
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
                    switch (board.BoardElement[i, j])
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
            if (board.BoardElement[_man.CurrentCoordinateY, _man.CurrentCoordinateX] == BoardElements.Bonus)
                board.QualityBonus--;
            board.BoardElement[_man.CurrentCoordinateY, _man.CurrentCoordinateX] = BoardElements.Man;
            //var Item = _CanvasHost.Children
            //    .Cast<UIElement>()
            //    .FirstOrDefault(i => Grid.GetColumn(i) == _man.CurrentCoordinateY && Grid.GetRow(i) == _man.CurrentCoordinateX);
            //((Canvas)Item).Background = new SolidColorBrush(Colors.Yellow);


        }


        private void ChangeColor(int i, int j, SolidColorBrush color)
        {
            var Item = _CanvasHost.Children
.Cast<UIElement>()
.FirstOrDefault(item => Grid.GetColumn(item) == j && Grid.GetRow(item) == i);
            ((Canvas)Item).Background = color;


        }
        
        #region Commands for move Man
        public ICommand MoveManLeft
        {
            get
            {
                return _moveManLeft ?? (_moveManLeft = new CommandHandler(() => MoveAction(KeyPress.Left), _canExecute));
            }
        }
        public ICommand MoveManRight
        {
            get
            {
                return _moveManRight ?? (_moveManRight = new CommandHandler(() => MoveAction(KeyPress.Right), _canExecute));
            }
        }
        public ICommand MoveManDown
        {
            get
            {
                return _moveManDown ?? (_moveManDown = new CommandHandler(() => MoveAction(KeyPress.Down), _canExecute));
            }
        }
        public ICommand MoveManUp
        {
            get
            {
                return _moveManUp ?? (_moveManUp = new CommandHandler(() => MoveAction(KeyPress.Up), _canExecute));
            }
        }

        #endregion


        #region Move Man
        private void MoveAction(KeyPress key)
        {
            switch (key)
            {
                case KeyPress.Left:

                    if (_man.CurrentCoordinateX != 0 && CheckCell(board.BoardElement[_man.CurrentCoordinateX - 1, _man.CurrentCoordinateY]))
                        OneStep(KeyPress.Left);
                    break;
                case KeyPress.Right:
                    if (_man.CurrentCoordinateX != _boardSize - 1 && CheckCell(board.BoardElement[_man.CurrentCoordinateX + 1, _man.CurrentCoordinateY]))
                        OneStep(KeyPress.Right);
                    break;
                case KeyPress.Up:
                    if (_man.CurrentCoordinateY != 0 && CheckCell(board.BoardElement[_man.CurrentCoordinateX, _man.CurrentCoordinateY - 1]))
                        OneStep(KeyPress.Up);
                    break;
                case KeyPress.Down:
                    if (_man.CurrentCoordinateY != _boardSize - 1 && CheckCell(board.BoardElement[_man.CurrentCoordinateX, _man.CurrentCoordinateY + 1]))
                        OneStep(KeyPress.Down);
                    break;
            }
            if (board.QualityBonus == 0)
                StartGame();
        }
        private bool CheckCell(BoardElements boardElement)
        {
            switch (boardElement)
            {
                case BoardElements.Way:
                    break;
                case BoardElements.Bonus:
                    _man.Score += bonus.Value;
                    board.QualityBonus--;
                    break;
                case BoardElements.Wall: return false;
            }
            return true;
        }


        private void OneStep(KeyPress key)
        {
            OldCell();
            switch (key)
            {
                case KeyPress.Left:
                    _man.StepLeft();
                    break;
                case KeyPress.Right:
                    _man.StepRight();
                    break;
                case KeyPress.Up:
                    _man.StepUp();
                    break;
                case KeyPress.Down:
                    _man.StepDown();
                    break;
            }
            NewCell();
        }


        #endregion


        public string Score
        {
            get
            {
                return "Score: " + _man.Score;
            }

        }

        private void OldCell()
        {
            board.BoardElement[_man.CurrentCoordinateX, _man.CurrentCoordinateY] = BoardElements.Way;
            ChangeColor(_man.CurrentCoordinateY, _man.CurrentCoordinateX, new SolidColorBrush(Colors.White));
        }
        private void NewCell()
        {
            board.BoardElement[_man.CurrentCoordinateX, _man.CurrentCoordinateY] = BoardElements.Man;
            ChangeColor(_man.CurrentCoordinateY, _man.CurrentCoordinateX, new SolidColorBrush(Colors.Yellow));

        }

    }
}
