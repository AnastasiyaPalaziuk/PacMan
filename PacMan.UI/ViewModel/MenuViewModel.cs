using PacMan.UI.Concrete;
using PacMan.UI.View;
using System;
using System.Windows;
using System.Windows.Input;
using PacMan.Logic.Abstract;
using PacMan.Logic.Concrete;

namespace PacMan.UI.ViewModel
{
   public class MenuViewModel
    {
        public MenuViewModel()
        {
            _canExecute = true;
        }

        private ICommand _exit;
        private ICommand _dragMove;
        private ICommand _startGame;
        private ICommand _score;
        private ICommand _selectAlgorithm;


        private readonly bool _canExecute;

        public ICommand SelectAlgorithm => _selectAlgorithm ?? (_selectAlgorithm = new CommandHandler(AlgorithmsShow, _canExecute));

        private static void AlgorithmsShow()
        {

            var currentWin = Application.Current.Windows[0];
            var algorithms = new Algorithms();
            currentWin?.Close();
            algorithms.Show();
        }

        public ICommand Exit => _exit ?? (_exit = new CommandHandler(ExitAction, _canExecute));

        private static void ExitAction()
        {
            App.Log.Trace("Выход из программы");
           Environment.Exit(0);
        }
        public ICommand DragMove => _dragMove ?? (_dragMove = new CommandHandler(DragMoveAction, _canExecute));

        private static void DragMoveAction()
        {
            App.Log.Trace("Перемещение окна по экрану");
           var currentWin = Application.Current.Windows[0];
            currentWin?.DragMove();
        }

        public ICommand StartGame => _startGame ?? (_startGame = new CommandHandler(StartGameAction, _canExecute));

        private static void StartGameAction()
        {

            var currentWin = Application.Current.Windows[0];
            

            var start = new PlayGame();
            App.Log.Trace("Главное меню закрывается");
            currentWin?.Close();
            App.Log.Trace("Отображение окна игры");
            start.Show();
           
        }

        public ICommand Score => _score ?? (_score = new CommandHandler(ShowAction, _canExecute));

        private static void ShowAction()
        {
            var currentWin = Application.Current.Windows[0];
            App.Log.Trace("Главное меню закрывается");

            var scoreView = new ScoreView();
            App.Log.Trace("Отображение окна Score");

            currentWin?.Close();
            scoreView.Show();
            
        }
    }
}
