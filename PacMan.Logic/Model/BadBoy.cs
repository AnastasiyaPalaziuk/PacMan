using PacMan.Logic.Abstract;

namespace PacMan.Logic.Model
{
    public class BadBoy : IMoveObject
    {
        public int CurrentCoordinateY { get; set; }
        public int CurrentCoordinateX { get; set; }
        private readonly Board _board;

        private BoardElements _lastStep = BoardElements.Bonus;
        public BadBoy(Board board, int x,int y)
        {
            _board = board;
            CurrentCoordinateX = x;
            CurrentCoordinateY = y;
        }
        public BadBoy(Board board)
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
                    break;
                case BoardElements.BadBoy:
                    break;
                case BoardElements.Man:
                    break;
                case BoardElements.Wall: return false;
            }

            return true;
        }

        public bool StepLeft()
        {
            if (CurrentCoordinateX != 0 && CheckCell(_board.BoardElement[CurrentCoordinateX - 1, CurrentCoordinateY]))
            {
                LastStep = _board.BoardElement[CurrentCoordinateX - 1, CurrentCoordinateY];
                CurrentCoordinateX--;
                return true;
            }
            return false;
        }
        public bool StepRight()
        {
            if (CurrentCoordinateX != _board.Size - 1 && CheckCell(_board.BoardElement[CurrentCoordinateX + 1, CurrentCoordinateY]))
            {
                LastStep = _board.BoardElement[CurrentCoordinateX + 1, CurrentCoordinateY];
                CurrentCoordinateX++;
                return true;
            }
            return false;
        }
        public bool StepDown()
        {
            if (CurrentCoordinateY != _board.Size - 1 && CheckCell(_board.BoardElement[CurrentCoordinateX, CurrentCoordinateY + 1]))
            {
                LastStep = _board.BoardElement[CurrentCoordinateX, CurrentCoordinateY + 1];
                CurrentCoordinateY++;
                return true;
            }
            return false;

        }
        public bool StepUp()
        {
            if (CurrentCoordinateY != 0 && CheckCell(_board.BoardElement[CurrentCoordinateX, CurrentCoordinateY - 1]))
            {
                LastStep = _board.BoardElement[CurrentCoordinateX, CurrentCoordinateY - 1];
                CurrentCoordinateY--;
                return true;
            }
            return false;
        }
        public BoardElements LastStep
        {
            get
            {
                return _lastStep;
                
            }
            set
            {
                _lastStep = value;
            }

        }
    }
}
