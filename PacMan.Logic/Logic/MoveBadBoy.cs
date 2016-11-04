using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Threading;
using PacMan.Logic.Concrete;
using PacMan.Logic.Model;
using PacMan.Logic.Abstract;

namespace PacMan.Logic.Logic
{
    public class MoveBadBoy : IBadBoyMoveAlgorithm
    {
        private Random random = new Random();
        private BadBoy _badBoy;
        private Board _board;
        private Man _man;
        public MoveBadBoy(Board board, Man man, Grid canvasHost)
        {
            _board = board;
            _man = man;

            _badBoy = new BadBoy(board);

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

        public void Stepping()
        {
            OldCell();
            RandomsMoveAction();
            NewCell();
        }
        public void Stepping(int i)
        {
             OldCell();
            switch (i)
            {
                case 0: FirstAlgorithm(); break;
                case 1: SecondAlgorithm(); break;
                default: Stepping(); break;
            }

            NewCell();
        }


        private void SecondAlgorithm()
        {
            if (_badBoy.CurrentCoordinateY != _man.CurrentCoordinateY)
            {
                if (_badBoy.CurrentCoordinateY > _man.CurrentCoordinateY)
                {
                    if (!_badBoy.StepUp())
                    {
                        _badBoy.StepLeft();
                    }
                }
                else
                {
                    if (!_badBoy.StepDown())
                        _badBoy.StepRight();
                }
            }
            else if (_badBoy.CurrentCoordinateX != _man.CurrentCoordinateX)
            {
                if (_badBoy.CurrentCoordinateX > _man.CurrentCoordinateX)
                {
                    if (!_badBoy.StepLeft())
                    {
                        _badBoy.StepDown();
                    }
                }
                else
                {
                    if (!_badBoy.StepRight())
                        _badBoy.StepUp();
                }
            }

        }

        private void FirstAlgorithm()
        {
            if (_badBoy.CurrentCoordinateX != _man.CurrentCoordinateX)
            {
                if (_badBoy.CurrentCoordinateX > _man.CurrentCoordinateX)
                {
                    if (!_badBoy.StepLeft())
                    {
                        _badBoy.StepDown();
                    }
                }
                else
                {
                    if (!_badBoy.StepRight())
                        _badBoy.StepUp();
                }
            }
            else if (_badBoy.CurrentCoordinateY != _man.CurrentCoordinateY)
            {
                if (_badBoy.CurrentCoordinateY > _man.CurrentCoordinateY)
                {
                    if (!_badBoy.StepUp())
                    {
                        _badBoy.StepLeft();
                    }
                }
                else
                {
                    if (!_badBoy.StepDown())
                        _badBoy.StepRight();
                }
            }

        }

        private void RandomsMoveAction()
        {
            //switch (random.Next() % 4)
            //{
            //    case 0:
            //       _badBoy.StepDown();
            //        break;
            //    case 1:
            //        _badBoy.StepLeft();
            //        break;
            //    case 2:
            //        _badBoy.StepRight();
            //        break;
            //    case 3:
            //        _badBoy.StepUp();
            //        break;

            //}
        }

        public void SetCoordinates(int x, int y)
        {
            _badBoy.LastStep = BoardElements.Bonus;
            _badBoy.CurrentCoordinateX = x;
            _badBoy.CurrentCoordinateY = y;

        }
        public int GetCurrentX()
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
