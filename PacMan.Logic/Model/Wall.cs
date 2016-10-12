using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PacMan.Logic.Model
{
   public class Wall : DependencyObject
    {
        public int X { get; protected set; }
        public int Y { get; protected set; }
        public int Length { get; protected set; }
        public bool IsVertical { get; protected set; }
    }
}
