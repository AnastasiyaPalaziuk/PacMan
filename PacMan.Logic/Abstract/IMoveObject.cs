using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;

namespace PacMan.Logic.Abstract
{
    public interface IMoveObject
    {
        int CurrentCoordinateY { get; set; }
        int CurrentCoordinateX { get; set; }
        bool StepLeft();
        bool StepRight();
        bool StepUp();
        bool StepDown();

    }
}
