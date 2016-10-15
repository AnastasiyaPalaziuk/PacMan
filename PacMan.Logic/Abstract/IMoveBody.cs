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
        void StepLeft();
        void StepRight();
        void StepUp();
        void StepDown();

    }
}
