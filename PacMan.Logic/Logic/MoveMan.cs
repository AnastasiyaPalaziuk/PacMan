using PacMan.Logic.Concrete;
using PacMan.Logic.Model;
using NLog;
using System;

namespace PacMan.Logic.Logic
{
    public class MoveMan
    {
        private readonly Board _board;
        private readonly Bonus _bonus;
        private Logger _log = LogManager.GetCurrentClassLogger();
        public Man Man { get; }


        public MoveMan(Board board)

        {
            _board = board;
            Man = new Man(board);
            _bonus = new Bonus();
            SetCoordinates();
            _board.AddComponents(Man.CurrentCoordinateX, Man.CurrentCoordinateY, BoardElements.Man);
           
        }



    public void AddBonusValue()
    {
        Man.Score += _bonus.Value;
        _board.QualityBonus--;
    }
    public void MoveAction(Side key)
        {
            OldCell();
            switch (key)
            {
                case Side.Left:
                    Man.StepLeft();
                    break;
                case Side.Right:
                    Man.StepRight();
                    break;
                case Side.Up:
                    Man.StepUp();
                    break;
                case Side.Down:
                    Man.StepDown();
                    break;
            }
            NewCell();

        }

        private void NewCell()
        {

            _board.BoardElement[Man.CurrentCoordinateX, Man.CurrentCoordinateY] = BoardElements.Man;
            try
            {
                ColorManager.ChangeElementColor(Man.CurrentCoordinateY, Man.CurrentCoordinateX, BoardElements.Man);
            }
            catch (NullReferenceException e)
            {
                _log.Error("Error change element`s color. \n{0}", e.Message);

            }

        }

        private void OldCell()
        {
            _board.BoardElement[Man.CurrentCoordinateX, Man.CurrentCoordinateY] = BoardElements.Way;
            try
            {
                ColorManager.ChangeElementColor(Man.CurrentCoordinateY, Man.CurrentCoordinateX, BoardElements.Way);
            }
            catch (NullReferenceException e)
            {
                _log.Error("Error change element`s color. \n{0}", e.Message);

            }
        }
      
        public int CurrentScore
        {
            get
            {
                return Man.Score;
            }
            set
            {
                Man.Score = value;
            }
        }
        public void SetCoordinates()
        {

            Man.CurrentCoordinateX = _board.Size / 2;
            Man.CurrentCoordinateY = _board.Size / 2;
            
        }
        public int GetCurrentX()
        {
            return Man.CurrentCoordinateX;

        }
        public int GetCurrentY()
        {
            return Man.CurrentCoordinateY;
        }

        public int Lifes
        {
            get
            {
                return Man.Life;
            }
            set
            {
                Man.Life = value;
            }
        }

    }
}
