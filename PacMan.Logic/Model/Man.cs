using PacMan.Logic.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net.Mime;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PacMan.Logic.Model
{
    public class Man 
    {
        private int _life = 3;
        public int Life {
            get { return _life; }
            set
            {
                _life = value;
            }
        }
        public int Score { get; set; }
        public int CurrentCoordinateX { get; set; }
        public int CurrentCoordinateY { get; set; }

        public Man()
        {
            
        }
     
    }
}
