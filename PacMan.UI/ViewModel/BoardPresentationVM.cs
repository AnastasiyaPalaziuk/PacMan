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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;

namespace PacMan.UI.ViewModel
{
    public class BoardPresentationVM
    {
        //private int _cols = 2;
        //private int _rows = 2;


        //private int _boardSize = 18;
        //private Grid _CellHost { get; set; }
        //private ICommand _moveManLeft;
        //private Man _man;
        //private bool _canExecute;
        //private Board board;
 

        //public BoardPresentationVM(Grid CellHost)
        //{
        //    _man = new Man
        //    {
        //        CurrentCoordinateX = 9,
        //        CurrentCoordinateY = 9
        //    };
        //    board = new Board(_boardSize);
        //    _CellHost = CellHost;
        //    ViewBoard();
        //}
        //private void ViewBoard()
        //{
        //    for(int i = 0; i < _boardSize; i++)
        //    {
        //        _CellHost.ColumnDefinitions.Add(new ColumnDefinition());
        //        _CellHost.RowDefinitions.Add(new RowDefinition());
        //    }
        //    for(int i = 0; i < _boardSize; i++)
        //    {
        //        for(int j = 0; j < _boardSize; j++)
        //        {
        //            Cell c = new Cell(i, j);
        //            if (board.BoardElement[i, j] == 0)
        //            {
        //                c.ContentElement.Background = new SolidColorBrush(Colors.White);
        //            }
        //            else
        //            {
        //                c.ContentElement.Background = new SolidColorBrush(Colors.Black);
        //            }
        //            Grid.SetColumn(c, i);
        //            Grid.SetRow(c, j);
        //            _CellHost.Children.Add(c);
        //        }
                
        //   }
        //    board.BoardElement[_man.CurrentCoordinateX, _man.CurrentCoordinateY] = 2;
        //    var Item = _CellHost.Children
        //        .Cast<UIElement>()
        //        .FirstOrDefault(i => Grid.GetColumn(i)  == _man.CurrentCoordinateY && Grid.GetRow(i) == _man.CurrentCoordinateX);
        //    ((Cell)Item).ContentElement.Background = new SolidColorBrush(Colors.Yellow);
           
        //}

        //public ICommand MoveManLeft
        //{
        //    get
        //    {
        //        return _moveManLeft ?? (_moveManLeft = new CommandHandler(() => MoveManLeftAction(), _canExecute));
        //    }
        //}

        //private void MoveManLeftAction()
        //{
        //    //if (_man.CurrentCoordinateX!=0 && board.BoardElement[_man.CurrentCoordinateX - 1, _man.CurrentCoordinateY] == 0)
        //    //{
        //    board.BoardElement[_man.CurrentCoordinateX, _man.CurrentCoordinateY] = 0;
        //    var OldItem = _CellHost.Children
        //    .Cast<UIElement>()
        //    .FirstOrDefault(i => Grid.GetColumn(i) == _man.CurrentCoordinateY && Grid.GetRow(i) == _man.CurrentCoordinateX);
        //    ((Cell)OldItem).ContentElement.Background = new SolidColorBrush(Colors.White);
        //    _man.CurrentCoordinateX--;
        //    board.BoardElement[_man.CurrentCoordinateX, _man.CurrentCoordinateY] = 2;
        //    var NewItem = _CellHost.Children
        //    .Cast<UIElement>()
        //    .FirstOrDefault(i => Grid.GetColumn(i) == _man.CurrentCoordinateY && Grid.GetRow(i) == _man.CurrentCoordinateX);
        //    ((Cell)NewItem).ContentElement.Background = new SolidColorBrush(Colors.Yellow);

        //    //}
        //}


    }
}

