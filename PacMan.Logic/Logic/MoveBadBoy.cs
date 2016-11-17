using System.Linq;
using PacMan.Logic.Concrete;
using PacMan.Logic.Model;

namespace PacMan.Logic.Logic
{
    public class MoveBadBoy {

        private readonly BadBoy _badBoy;
        private readonly Board _board;
        private readonly Man _man;
        //private Logger _log = LogManager.GetCurrentClassLogger(); 
        private static readonly LoaderPlugins Plugins= new LoaderPlugins();
        public MoveBadBoy(Board board, Man man)
        {
            _board = board;
            _man = man;
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
                    Plugins.Plugins.FirstOrDefault(item => item.PluginName == "FirstAlgorithm")?.Run(_badBoy,_man);
                     break;
                case 1:
                    Plugins.Plugins.FirstOrDefault(item => item.PluginName == "SecondAlgorithm")?.Run(_badBoy, _man);
                    break;
                default:
                    Plugins.Plugins.FirstOrDefault(item => item.PluginName == "RandomAlgorithm")?.Run(_badBoy, _man);
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
