using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.ComponentModel;
using System.Threading;
using PacMan.Logic.Model;
using PacMan.Logic.Concrete;

namespace PacMan.UI.Concrete.Logic
{
    public class MoveMan
    {
        private Man _man;
        private Board _board;
        private Bonus _bonus;
        public Man Man
        {
            get
            {
                return _man;
            }
        }


        public MoveMan(Board board, Grid canvasHost)

        {
            _board = board;
            _man = new Man(board);
            _bonus = new Bonus();
            SetCoordinates();
            _board.AddComponents(_man.CurrentCoordinateX, _man.CurrentCoordinateY, BoardElements.Man);
           
        }



    public void AddBonusValue()
    {
        _man.Score += _bonus.Value;
        _board.QualityBonus--;
    }
    public void MoveAction(Side key)
        {
            OldCell();
            switch (key)
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

            _man.CurrentCoordinateX = _board.Size / 2;
            _man.CurrentCoordinateY = _board.Size / 2;
            
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
