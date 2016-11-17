using System;
using System.Windows.Input;

namespace PacMan.UI.Concrete
{
    public class CommandHandler : ICommand
    {

        private readonly Action _action;
        private bool _canExecute;
        public CommandHandler(Action action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute {
            get
            {
                return _canExecute;
            }
            set { _canExecute = value; }
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action();
        }

        bool ICommand.CanExecute(object parameter)
        {
            return _canExecute;
        }
    }
}
