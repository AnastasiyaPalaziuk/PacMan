using NLog;
using PacMan.Logic.Concrete;
using PacMan.Logic.Model;
using System;

namespace PacMan.Logic.Logic
{
    public class MoveBadBoy {

        private readonly BadBoy _badBoy;
        private readonly Board _board;
        private readonly Man _man;
        private Logger _log = LogManager.GetCurrentClassLogger(); 
        public MoveBadBoy(Board board, Man man)
        {
            _board = board;
            _man = man;
            _badBoy = new BadBoy(board);

        }
        private void NewCell()
        {
            _board.BoardElement[_badBoy.CurrentCoordinateX, _badBoy.CurrentCoordinateY] = BoardElements.BadBoy;
            try
            {
                ColorManager.ChangeElementColor(_badBoy.CurrentCoordinateY, _badBoy.CurrentCoordinateX, BoardElements.BadBoy);
            }
            catch (NullReferenceException e)
            {
                _log.Error("Error change element`s color. \n{0}", e.Message);

            }
        }

        private void OldCell()
        {
            _board.BoardElement[_badBoy.CurrentCoordinateX, _badBoy.CurrentCoordinateY] = _badBoy.LastStep;
            try
            {
                ColorManager.ChangeElementColor(_badBoy.CurrentCoordinateY, _badBoy.CurrentCoordinateX, _badBoy.LastStep);
            }
            catch (NullReferenceException e)
            {
                _log.Error("Error change element`s color. \n{0}", e.Message);

            }
        }

        public  void Stepping()
        {
            OldCell();
            CurrentAlgorithm.Value.Run(_badBoy,_man);
            NewCell();
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
