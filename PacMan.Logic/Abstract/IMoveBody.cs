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
        ImageBrush MoveLeft();
        ImageBrush MoveRight();
        ImageBrush MoveUp();
        ImageBrush MoveDown();

    }
}
