using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PacMan.Logic.Model
{
    public class Board
    {

        private int _size;
        private int _qualityBonus;
        private BoardElements[,] _boardElement;
        public BoardElements[,] BoardElement
        {
            get
            {
                return _boardElement;
            }
            set
            {
                _boardElement = value;
            }
        }
        public int QualityBonus
        {
            get { return _qualityBonus; }
            set
            {
                _qualityBonus = value;
            }
        }
        public Board(int size)
        {
            this._size = size;
            _boardElement = new BoardElements[size, size];
            GenerateMap();
        }
        public Board(int size, int level)
        {
            this._size = size;
            _boardElement = new BoardElements[size, size];
            GenerateMap();
        }
        public void UpdateBoard()
        {
            GenerateMap();

        }

        private void GenerateMap()
        {
            Random random = new Random();

            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    if (i == 0 || j == 0 || i == _size - 1 || j == _size - 1)
                    {
                        _boardElement[i, j] = BoardElements.Bonus;
                        _qualityBonus++;
                    }
                    else
                    {
                        if (_boardElement[i - 1, j + 1] == BoardElements.Bonus /*&& _boardElement[i - 1, j - 1] == 0*/)
                        {
                            if ((random.Next()) % 2 == 0)
                            {
                                _boardElement[i, j] = BoardElements.Bonus;
                                _qualityBonus++;

                            }
                            else
                                _boardElement[i, j] = BoardElements.Wall;
                        }
                        else
                        {
                            _boardElement[i, j] = BoardElements.Bonus;
                            _qualityBonus++;

                        }

                    }
                }
            }
        }
    }
}
