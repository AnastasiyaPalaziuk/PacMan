using PacMan.Logic.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan.Logic.Model
{
    public class BadBoy : IMoveBody
    {
        public int CurrentCoordinateY { get; set; }
        public int CurrentCoordinateX { get; set; }
        private BoardElements _lastStep = BoardElements.Bonus;
        public BadBoy()
        {
        }
        public void StepLeft()
        {
                CurrentCoordinateX--;
        }
        public void StepRight()
        {
                CurrentCoordinateX++;
        }
        public void StepDown()
        {
                CurrentCoordinateY++;
        }
        public void StepUp()
        {
                CurrentCoordinateY--;
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
