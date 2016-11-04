using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Threading;
using PacMan.Logic.Concrete;
using PacMan.Logic.Model;
using PacMan.Logic.Abstract;

namespace PacMan.Logic.Logic
{
    public class MoveBadBoy {

        private Random random = new Random();
        private BadBoy _badBoy;
        private Board _board;
        private Man _man;
        private LoaderPlugins _plugins;
        public MoveBadBoy(Board board, Man man, Grid canvasHost)
        {
            _board = board;
            _man = man;
            _plugins = new LoaderPlugins();
            _badBoy = new BadBoy(board);

        }
        private void NewCell()
        {
            _board.BoardElement[_badBoy.CurrentCoordinateX, _badBoy.CurrentCoordinateY] = BoardElements.BadBoy;
            ColorManager.ChangeElementColor(_badBoy.CurrentCoordinateY, _badBoy.CurrentCoordinateX, BoardElements.BadBoy);
        }

        private void OldCell()
        {
            _board.BoardElement[_badBoy.CurrentCoordinateX, _badBoy.CurrentCoordinateY] = _badBoy.LastStep;
            ColorManager.ChangeElementColor(_badBoy.CurrentCoordinateY, _badBoy.CurrentCoordinateX, _badBoy.LastStep);
        }

        public void Stepping(int i)
        {
             OldCell();
            switch (i)
            {
                case 0:
                    _plugins.Plugins.FirstOrDefault(item => item.PluginName == "FirstAlgorithm").Run(_badBoy,_man);
                     break;
                case 1:
                    _plugins.Plugins.FirstOrDefault(item => item.PluginName == "SecondAlgorithm").Run(_badBoy, _man);
                    break;
                default:
                    _plugins.Plugins.FirstOrDefault(item => item.PluginName == "RandomAlgorithm").Run(_badBoy, _man);
                     break;
            }

            NewCell();
        }



        public void SetCoordinates(int x, int y)
        {
            _badBoy.LastStep = BoardElements.Bonus;
            _badBoy.CurrentCoordinateX = x;
            _badBoy.CurrentCoordinateY = y;

        }
        public int GetCurrentX()
        {
            return _badBoy.CurrentCoordinateX;

        }
        public int GetCurrentY()
        {
            return _badBoy.CurrentCoordinateY;
        }
        public BoardElements LastStep
        {
            get
            {
                return _badBoy.LastStep;
            }
            set
            {
                _badBoy.LastStep = value;
            }
        }
    }
}
