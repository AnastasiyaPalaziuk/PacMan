using PacMan.UI.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PacMan.UI.ViewModel
{
    class LevelVM
    {
        private ICommand _displayLevel;
        private int _level=1;

        private bool _canExecute;

        public LevelVM(int level)
        {
            _level = level;
        }
        public LevelVM()
        {
            
        }

        public string DisplayLevel
        {
            get { return "Level: " + _level; }
            
        }
    }
}
