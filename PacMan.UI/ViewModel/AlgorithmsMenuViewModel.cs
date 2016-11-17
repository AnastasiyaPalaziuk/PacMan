using PacMan.UI.Concrete;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PacMan.Logic.Concrete;

namespace PacMan.UI.ViewModel
{
    public class AlgorithmsMenuViewModel
    {
        private ICommand _cancel;
        private ICommand _dragMove;
        private ICommand _selectItem;
        private static DataGrid _algorithmsList;
        private static readonly LoaderPlugins Plugins = new LoaderPlugins();

        private readonly bool _canExecute;

        public AlgorithmsMenuViewModel(DataGrid algorithmsList)
        {
            _canExecute = true;
            _algorithmsList = algorithmsList;
            _algorithmsList.ItemsSource = Plugins.Plugins;
        }
        public ICommand Cancel => _cancel ?? (_cancel = new CommandHandler(ExitAction, _canExecute));
        public ICommand SelectItem => _selectItem ?? (_selectItem = new CommandHandler(Selecting, _canExecute));

        private static void Selecting()
        {
            CurrentAlgorithm.Value = Plugins.Plugins[_algorithmsList.SelectedIndex];
            ExitAction();
        }

        public ICommand DragMove => _dragMove ?? (_dragMove = new CommandHandler(DragMoveAction, _canExecute));

        private static void DragMoveAction()
        {

            var currentWin = Application.Current.Windows[0];
            currentWin?.DragMove();

        }

        private static void ExitAction()
        {

            var menu = new Menu();
            
            menu.Show();
            var currentWin = Application.Current.Windows[0];
            currentWin?.Close();
        }


    }
}
