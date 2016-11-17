using PacMan.Logic.Model;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PacMan.Logic.Concrete
{
    public static class ColorManager
    {
        private static Grid _canvasHost;
        //public ColorManager(Grid CanvasHost)
        //{
        //    _CanvasHost = CanvasHost;
        //}
        public static void SetCanvasHost(Grid canvasHost)
        {
            _canvasHost = canvasHost;
        }
        public static void ChangeElementColor(int i, int j, BoardElements boardElement)
        {
            _canvasHost.Dispatcher
            .Invoke(() =>
                {
                    if (_canvasHost.Children.Cast<UIElement>()
                            .FirstOrDefault(item => Grid.GetColumn(item) == j && Grid.GetRow(item) == i) == null)
                        return;
                    {
                        var canvas = (Canvas) _canvasHost.Children.Cast<UIElement>()
                            .FirstOrDefault(item => Grid.GetColumn(item) == j && Grid.GetRow(item) == i);
                        if (canvas != null)
                            canvas.Background = ChangeColor(boardElement);
                    }
                });
        }

        public static SolidColorBrush ChangeColor(BoardElements element)
        {
            switch (element)
            {
                case BoardElements.BadBoy:
                    return new SolidColorBrush(Colors.Green);
                case BoardElements.Bonus:
                    return new SolidColorBrush(Colors.Red);
                case BoardElements.Man:
                    return new SolidColorBrush(Colors.Yellow);
                case BoardElements.Way:
                    return new SolidColorBrush(Colors.White);
                case BoardElements.Wall:
                    return new SolidColorBrush(Colors.Black);
                default: return new SolidColorBrush(Colors.Brown);
            }

        }

    }
}
