using PacMan.Logic.Abstract;
using System.Threading;
using NLog;

namespace PacMan.Logic.Model
{
    public class Man : IMoveObject
    {
        public int Life { get; set; } = 5;
        private readonly Board _board;
        private readonly Logger _log = LogManager.GetCurrentClassLogger();

        public int Score { get; set; }

        public int CurrentCoordinateY { get; set; }
        public int CurrentCoordinateX { get; set; }

        public Man(Board board)
        {
            _board = board;
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
            Score++;
            _board.QualityBonus--;
        }

        public bool StepLeft()
        {
            if (CurrentCoordinateX != 0 && CheckCell(_board.BoardElement[CurrentCoordinateX - 1, CurrentCoordinateY]))
            {
                CurrentCoordinateX--;
                _log.Trace("Шаг влево главного героя");

                return true;
            }
            return false;
        }

        public bool StepRight()
        {
            if (CurrentCoordinateX != _board.Size - 1 && CheckCell(_board.BoardElement[CurrentCoordinateX + 1, CurrentCoordinateY]))
            {

                CurrentCoordinateX++;
                _log.Trace("Шаг вправо главного героя");

                return true;
            }
            return false;
        }

        public bool StepDown()
        {
            if (CurrentCoordinateY != _board.Size - 1 && CheckCell(_board.BoardElement[CurrentCoordinateX, CurrentCoordinateY + 1]))
            {
                CurrentCoordinateY++;
                _log.Trace("Шаг вниз главного героя");
                return true;
            }
            return false;
        }

        public bool StepUp()
        {
            if (CurrentCoordinateY != 0 && CheckCell(_board.BoardElement[CurrentCoordinateX, CurrentCoordinateY - 1]))
            {
                CurrentCoordinateY--;
                _log.Trace("Шаг вверх главного героя");
                return true;
            }
            return false;
        }
    }
}