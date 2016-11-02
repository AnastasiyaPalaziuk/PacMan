using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.ComponentModel;
using System.Threading;
using PacMan.UI.ViewModel;
using PacMan.UI.Model;

namespace PacMan.UI.Concrete.Logic
{
    public class MoveMan
    {
        private Man _man;
        private Board _board;
        private int _boardSize;
        private Bonus _bonus;

        public Man Man
        {
            get
            {
                return _man;
            }
        }


        public MoveMan(Board board, int boardSize, Grid canvasHost)

        {
            _bonus = new Bonus();
            _boardSize = boardSize;
            _board = board;
            _man = new Man();
            SetCoordinates();
            _board.AddComponents(_man.CurrentCoordinateX, _man.CurrentCoordinateY, BoardElements.Man);
           
        }


        private bool CheckCell(BoardElements boardElement)
        {
            switch (boardElement)
            {
                case BoardElements.Way:
                    break;
                case BoardElements.Bonus:
                    AddBonusValue();
                    
                    break;
                case BoardElements.BadBoy:
                    Thread.Sleep(300);
                    break;
                case BoardElements.Wall: return false;
            }
            
            return true;

            
        }
        public void AddBonusValue()
        {
            _man.Score += _bonus.Value;
            _board.QualityBonus--;
        }
        public void MoveAction(Side key)
        {
            switch (key)
            {
                case Side.Left:
                    if (_man.CurrentCoordinateX != 0 && CheckCell(_board.BoardElement[_man.CurrentCoordinateX - 1, _man.CurrentCoordinateY]))
                        OneStep(Side.Left);
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
           
        }

        private void NewCell()
        {

            _board.BoardElement[_man.CurrentCoordinateX, _man.CurrentCoordinateY] = BoardElements.Man;
            ColorManager.ChangeElementColor(_man.CurrentCoordinateY, _man.CurrentCoordinateX, BoardElements.Man);
        }

        private void OldCell()
        {
            _board.BoardElement[_man.CurrentCoordinateX, _man.CurrentCoordinateY] = BoardElements.Way;
            ColorManager.ChangeElementColor(_man.CurrentCoordinateY, _man.CurrentCoordinateX, BoardElements.Way);
        }

        private void OneStep(Side sideKey)
        {
            OldCell();
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
            NewCell();
        }

      
        public int CurrentScore
        {
            get
            {
                return _man.Score;
            }
            set
            {
                _man.Score = value;
            }
        }
        public void SetCoordinates()
        {

            _man.CurrentCoordinateX = _boardSize / 2;
            _man.CurrentCoordinateY = _boardSize / 2;
            
        }
        public int GetCurrentX()
        {
            return _man.CurrentCoordinateX;

        }
        public int GetCurrentY()
        {
            return _man.CurrentCoordinateY;
        }

        public int Lifes
        {
            get
            {
                return _man.Life;
            }
            set
            {
                _man.Life = value;
            }
        }

    }
}
