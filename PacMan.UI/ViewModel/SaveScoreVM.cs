using PacMan.Domain;
using PacMan.Domain.Concrete;
using PacMan.Domain.Entities;
using PacMan.UI.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PacMan.UI.ViewModel
{
    class SaveScoreVM
    {
        private string _name;
        private int _score;
        private ICommand _add;
        private ICommand _cansel;
        private bool _canExecute;
        private ICommand _dragMove;

        public SaveScoreVM(int score)
        {
            _score = score;
            _canExecute = true;
        }

        public string CurrentScore
        {
            get
            {
                return _score.ToString();
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;

            }
        }

        public ICommand Add
        {
            get
            {
                return _add ?? (_add = new CommandHandler(AddItemAction, _canExecute));
            }
            
        }
        public ICommand Cansel
        {
            get
            {
                return _cansel ?? (_cansel = new CommandHandler(ExitAction, _canExecute));
            }
        }
        public ICommand DragMove
        {
            get
            {
                return _dragMove ?? (_dragMove = new CommandHandler(() => DragMoveAction(), _canExecute));
            }
        }

        private void DragMoveAction()
        {
            var currentWin = Application.Current.Windows[0];
            currentWin.DragMove();


        }

        private void ExitAction()
        {
           
            Menu menu = new Menu();
            menu.Show();
            var currentWin = Application.Current.Windows[0];
            currentWin.Close();
        }


        private void AddItemAction()
        {
            EFPlayerRepository context = new EFPlayerRepository();
            Player player = new Player();
            player.Name = _name;
            player.Score = _score;
            context.AddPlayer(player);
            ExitAction();
        }
    }
}
