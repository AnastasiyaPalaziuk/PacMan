using PacMan.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace PacMan.UI.Concrete.Logic
{
    public static class ColorManager
    {
        private static Grid _CanvasHost;
        //public ColorManager(Grid CanvasHost)
        //{
        //    _CanvasHost = CanvasHost;
        //}
        public static void SetCanvasHost(Grid CanvasHost)
        {
            _CanvasHost = CanvasHost;
        }
        public static void ChangeElementColor(int i, int j, BoardElements boardElement)
        {
            _CanvasHost.Dispatcher
            .Invoke(new Action(() =>
                {
                    var Item =
                        _CanvasHost.Children.Cast<UIElement>()
                            .FirstOrDefault(item => Grid.GetColumn(item) == j && Grid.GetRow(item) == i);
                    if(Item!=null)
                        ((Canvas)Item).Background = ChangeColor(boardElement);
                }));
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
