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
        private int[,] _boardElement;
        public int[,] BoardElement
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
        public Board(int size)
        {
            this._size = size;
            _boardElement = new int[size, size];
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
                        _boardElement[i, j] = 0;
                    else
                    {
                        if (_boardElement[i - 1, j + 1] == 0 /*&& _boardElement[i - 1, j - 1] == 0*/)
                            _boardElement[i, j] = (random.Next()) % 2;

                        else
                            _boardElement[i, j] = 0;

                    }
                }
            }
        }
    }
}
