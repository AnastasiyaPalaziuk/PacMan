using PacMan.Domain.Concrete;
using PacMan.Domain.Entities;
using PacMan.UI.Concrete;
using System.Windows;
using System.Windows.Input;

namespace PacMan.UI.ViewModel
{
    public class SaveScoreViewModel
    {
        private readonly int _score;
        private ICommand _add;
        private ICommand _cansel;
        private readonly bool _canExecute;
        private ICommand _dragMove;

        public SaveScoreViewModel(int score)
        {
            _score = score;
            _canExecute = true;
        }

        public string CurrentScore => _score.ToString();

        public string Name { get; set; }

        public ICommand Add => _add ?? (_add = new CommandHandler(AddItemAction, _canExecute));

        public ICommand Cancel => _cansel ?? (_cansel = new CommandHandler(ExitAction, _canExecute));

        public ICommand DragMove => _dragMove ?? (_dragMove = new CommandHandler(DragMoveAction, _canExecute));

        private void DragMoveAction()
        {

            var currentWin = Application.Current.Windows[0];
            currentWin?.DragMove();
            App.Log.Trace("Перемещение окна Save Score по экрану");

        }

        private static void ExitAction()
        {
           
            var menu = new Menu();
            menu.Show();
            var currentWin = Application.Current.Windows[0];
            currentWin?.Close();
        }


        private void AddItemAction()
        {
            var context = new EfPlayerRepository();
            var player = new Player
            {
                Name = Name,
                Score = _score
            };
            context.AddPlayer(player);
            ExitAction();
        }
    }
}
