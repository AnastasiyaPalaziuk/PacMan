using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Threading;
using PacMan.UI.Model;
using PacMan.UI.Abstract;

namespace PacMan.UI.Concrete.Logic
{
    public class MoveBadBoy : IBadBoyMoveAlgorithm
    {
        private Random random = new Random();
        private BadBoy _badBoy;
        private Board _board;
        private Man _man;
        private int _boardSize;
        public MoveBadBoy(Board board, int boardSize,Man man, Grid canvasHost)
        {
            _boardSize = boardSize;
            _board = board;
            _man = man;
            //_colorManager = new ColorManager(canvasHost);

            _badBoy = new BadBoy();
            _board.AddComponents(_badBoy.CurrentCoordinateX, _badBoy.CurrentCoordinateY, BoardElements.BadBoy);
          
        }
        private bool CheckCell(BoardElements boardElement)
        {
            switch (boardElement)
            {
                case BoardElements.Way:
                    break;
                case BoardElements.Bonus:
                    break;
                case BoardElements.BadBoy:
                    break;
                case BoardElements.Man:
                    break;
                case BoardElements.Wall: return false;
            }
         
            return true;
        }
        private void NewCell()
        {
            _board.BoardElement[_badBoy.CurrentCoordinateX, _badBoy.CurrentCoordinateY] = BoardElements.BadBoy;
          ColorManager.ChangeElementColor(_badBoy.CurrentCoordinateY, _badBoy.CurrentCoordinateX, BoardElements.BadBoy);
        }

        private void OldCell()
        {
            _board.BoardElement[_badBoy.CurrentCoordinateX, _badBoy.CurrentCoordinateY] = _badBoy.LastStep;
            ColorManager.ChangeElementColor(_badBoy.CurrentCoordinateY, _badBoy.CurrentCoordinateX, _badBoy.LastStep);
        }

        public void Steping()
        {
            OldCell();
            RandomsMoveAction();
            NewCell();
        }
        public void Steping(int i)
        {
            OldCell();
            switch (i)
            {
                case 0: FirstAlgorithm();break;
                case 1: SecondAlgorithm();break;
                case 2:  ThirdAlgorithm();break;
                default:  RandomsMoveAction();break;
            }
            
            NewCell();
        }

        private void ThirdAlgorithm()
        {
            throw new NotImplementedException();
        }

        private void SecondAlgorithm()
        {
            throw new NotImplementedException();
        }

        private void FirstAlgorithm()
        {
            throw new NotImplementedException();
        }

        private void RandomsMoveAction()
        {
           
                switch (random.Next() %4 )
                {
                    case 0:
                        if (_badBoy.CurrentCoordinateY != _boardSize - 1 && CheckCell(_board.BoardElement[_badBoy.CurrentCoordinateX, _badBoy.CurrentCoordinateY + 1]))
                        {
                            _badBoy.LastStep = _board.BoardElement[_badBoy.CurrentCoordinateX, _badBoy.CurrentCoordinateY + 1];
                            _badBoy.StepDown();
                        }
                        break;
                    case 1:
                        if (_badBoy.CurrentCoordinateX != 0 && CheckCell(_board.BoardElement[_badBoy.CurrentCoordinateX - 1, _badBoy.CurrentCoordinateY]))
                        {
                            _badBoy.LastStep = _board.BoardElement[_badBoy.CurrentCoordinateX - 1, _badBoy.CurrentCoordinateY];
                            _badBoy.StepLeft();

                        }
                        break;
                    case 2:
                        if (_badBoy.CurrentCoordinateX != _boardSize - 1 && CheckCell(_board.BoardElement[_badBoy.CurrentCoordinateX + 1, _badBoy.CurrentCoordinateY]))
                        {
                            _badBoy.LastStep = _board.BoardElement[_badBoy.CurrentCoordinateX + 1, _badBoy.CurrentCoordinateY];
                            _badBoy.StepRight();
                        }
                        break;
                    case 3:
                        if (_badBoy.CurrentCoordinateY != 0 && CheckCell(_board.BoardElement[_badBoy.CurrentCoordinateX, _badBoy.CurrentCoordinateY - 1]))
                        {
                            _badBoy.LastStep = _board.BoardElement[_badBoy.CurrentCoordinateX, _badBoy.CurrentCoordinateY - 1];
                            _badBoy.StepUp();
                        }
                        break;
                
            }
        }

        public void SetCoordinates(int x,int y)
        {
            _badBoy.LastStep = BoardElements.Bonus;
            _badBoy.CurrentCoordinateX = x;
            _badBoy.CurrentCoordinateY = y;

        }
        public  int GetCurrentX()
        {
            return _badBoy.CurrentCoordinateX;
            
        }
        public int GetCurrentY()
        {
            return _badBoy.CurrentCoordinateY;
        }
        public BoardElements LastStep
        {
            get
            {
                return _badBoy.LastStep;
            }
            set
            {
                _badBoy.LastStep = value;
            }
        }
    }
}
