using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;

namespace PacMan.Logic.Abstract
{
    public interface IMoveBody
    {
        bool StepLeft();
        bool StepRight();
        bool StepUp();
        bool StepDown();

    }
}
