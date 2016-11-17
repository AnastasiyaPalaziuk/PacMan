using System;

namespace PacMan.Logic.Model
{
    public class Board
    {
        public int Size { get; }

        public BoardElements[,] BoardElement { get; set; }

        public int QualityBonus { get; set; }

        public Board(int size)
        {
            Size = size;
            BoardElement = new BoardElements[size, size];
            GenerateMap();
        }
       
        public void UpdateBoard()
        {
            GenerateMap();

        }
        public void AddComponents(int x,int y, BoardElements type)
        {
            if (BoardElement[x, y] == BoardElements.Bonus && type == BoardElements.Man) 
                QualityBonus--;
                BoardElement[x, y] = type;
            
         
        }

        private void GenerateMap()
        {
            Random random = new Random();

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (i == 0 || j == 0 || i == Size - 1 || j == Size - 1)
                    {
                        BoardElement[i, j] = BoardElements.Bonus;
                       QualityBonus++;
                    }
                    else
                    {
                        if (BoardElement[i - 1, j + 1] == BoardElements.Bonus)
                        {
                            if ((random.Next()) % 2 == 0)
                            {
                                BoardElement[i, j] = BoardElements.Bonus;
                                QualityBonus++;

                            }
                            else
                                BoardElement[i, j] = BoardElements.Wall;
                        }
                        else
                        {
                            BoardElement[i, j] = BoardElements.Bonus;
                            QualityBonus++;

                        }

                    }
                }
            }
        }
    }
}
