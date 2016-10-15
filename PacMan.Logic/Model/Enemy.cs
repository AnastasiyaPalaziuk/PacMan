using PacMan.Logic.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan.Logic.Model
{
    class Enemy : IMoveBody
    {
        public int CurrentCoordinateY { get; set; }
        public int CurrentCoordinateX { get; set; }
        private Random random = new Random();

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
        public void RandomMoveBody()
        {
            switch (random.Next()%4)
            {
                case 0:StepDown();break;
                case 1:StepLeft(); break;
                case 2:StepRight(); break;
                case 3:StepUp() ; break;
            }
        }
    }
}
