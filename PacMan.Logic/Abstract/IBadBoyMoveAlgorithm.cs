using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan.Logic.Abstract
{
   public interface IBadBoyMoveAlgorithm
    {
        void Stepping(int i);
        void Stepping();
    }
}
