using PacMan.Logic.Model;
using PacMan.UI.Concrete;
using PacMan.UI.View;
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
    class PlayGameVM
    {
        private bool _canExecute;
        private  ICommand _scale;
        private ICommand _dragMove;
        private ICommand _exit;
        private ICommand _moveManLeft;
        //public PlayGameVM()
        //{
        //    _canExecute = true;
            
        //}
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
            var currentWin = Application.Current.Windows[0];
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
            var currentWin = Application.Current.Windows[0];
            currentWin.Close();
            menu.Show();
        }
        /// <summary>
        /// /////////////////////////
        /// </summary>
        private int _boardSize = 18;
        private Grid _CellHost { get; set; }
        private Man _man;
        private Board board;


        public PlayGameVM(Grid CellHost)
        {
            _canExecute = true;
            _man = new Man
            {
                CurrentCoordinateX = 9,
                CurrentCoordinateY = 9
            };
            board = new Board(_boardSize);
            _CellHost = CellHost;
            ViewBoard();
        }
        private void ViewBoard()
        {
            for (int i = 0; i < _boardSize; i++)
            {
                _CellHost.ColumnDefinitions.Add(new ColumnDefinition());
                _CellHost.RowDefinitions.Add(new RowDefinition());
            }
            for (int j = 0; j < _boardSize; j++)
            {
                for (int i = 0; i < _boardSize; i++)
                {
                    Cell c = new Cell(i, j);
                    if (board.BoardElement[i, j] == 0)
                    {
                        c.ContentElement.Background = new SolidColorBrush(Colors.White);
                    }
                    else
                    {
                        c.ContentElement.Background = new SolidColorBrush(Colors.Black);
                    }
                    Grid.SetColumn(c, i);
                    Grid.SetRow(c, j);
                    _CellHost.Children.Add(c);
                }

            }
            board.BoardElement[_man.CurrentCoordinateX, _man.CurrentCoordinateY] = 2;
            var Item = _CellHost.Children
                .Cast<UIElement>()
                .FirstOrDefault(i => Grid.GetColumn(i) == _man.CurrentCoordinateY && Grid.GetRow(i) == _man.CurrentCoordinateX);
            ((Cell)Item).ContentElement.Background = new SolidColorBrush(Colors.Yellow);

        }

        public ICommand MoveManLeft
        {
            get
            {
                return _moveManLeft ?? (_moveManLeft = new CommandHandler(() => MoveManLeftAction(), _canExecute));
            }
        }

        private void MoveManLeftAction()
        {
            if (_man.CurrentCoordinateX != 0 && board.BoardElement[_man.CurrentCoordinateY - 1, _man.CurrentCoordinateX] == 0)
            {

                board.BoardElement[_man.CurrentCoordinateY, _man.CurrentCoordinateX] = 0;

                var OldItem = _CellHost.Children
            .Cast<UIElement>()
            .FirstOrDefault(i => Grid.GetColumn(i) == _man.CurrentCoordinateY && Grid.GetRow(i) == _man.CurrentCoordinateX);
            ((Cell)OldItem).ContentElement.Background = new SolidColorBrush(Colors.White);


                _man.CurrentCoordinateY--;

                board.BoardElement[_man.CurrentCoordinateY, _man.CurrentCoordinateX] = 2;

                var NewItem = _CellHost.Children
            .Cast<UIElement>()
            .FirstOrDefault(i => Grid.GetColumn(i) == _man.CurrentCoordinateY && Grid.GetRow(i) == _man.CurrentCoordinateX);

                ((Cell)NewItem).ContentElement.Background = new SolidColorBrush(Colors.Yellow);

            }
        }


    }
}
