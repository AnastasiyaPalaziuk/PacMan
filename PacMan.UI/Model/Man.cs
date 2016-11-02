using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net.Mime;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PacMan.UI.Abstract;

namespace PacMan.UI.Model
{
    public class Man : IMoveBody
    {
        public int Life { get; set; } = 100;

        public int Score { get; set; }

        public int CurrentCoordinateY { get; set; }
        public int CurrentCoordinateX { get; set; }

        public Man()
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
    }
}