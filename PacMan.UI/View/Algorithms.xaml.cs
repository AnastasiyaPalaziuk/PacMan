using System.Windows;
using System.Windows.Controls;
using PacMan.UI.ViewModel;

namespace PacMan.UI.View
{
    /// <summary>
    /// Interaction logic for Algorithms.xaml
    /// </summary>
    public partial class Algorithms : Window
    {
        public Algorithms()
        {
            InitializeComponent();
            DataContext = new AlgorithmsMenuViewModel(AlgorithmsList);
        }
    }
}
