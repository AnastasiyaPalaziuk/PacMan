using PacMan.UI.Model;
using PacMan.UI.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PacMan.UI.Concrete.Logic
{
    public class GameLogic
    {
        private MoveMan _moveMan;
        private MoveBadBoy[] _moveBadBoy;
        private static int _badBoyQuality =3;
        private int _level = 0;
        private Grid _CanvasHost;
        private bool gridIsUsed = false;
        private Board _board;
        private int _boardSize = 15;
        private Level level;
        private Thread thread;
        public GameLogic(Grid CanvasHost)
        {
            _CanvasHost = CanvasHost;
            InicializeComponents();
            RunParallelThread();

        }
        public int Score
        {
            get
            {
                return _moveMan.CurrentScore;
            }

        }
        public int Lifes
        {
            get
            {
                return _moveMan.Lifes;
            }
        }
        public int Level
        {
            get
            {
                return _level;
            }
            
        }
        private void InicializeComponents()
        {

            _moveBadBoy = new MoveBadBoy[_badBoyQuality];
            _board = new Board(_boardSize);
            _moveMan = new MoveMan(_board, _boardSize, _CanvasHost);
            for (int i = 0; i < _badBoyQuality; i++)
            {
                _moveBadBoy[i] = new MoveBadBoy(_board, _boardSize,_moveMan.Man, _CanvasHost);


            }
            _moveBadBoy[0].SetCoordinates(0, 0);
            _moveBadBoy[1].SetCoordinates(_boardSize - 1, _boardSize - 1);
            _moveBadBoy[2].SetCoordinates(0, _boardSize - 1);

            for (int i = 0; i < _badBoyQuality; i++)
            {
                _board.AddComponents(_moveBadBoy[0].GetCurrentX(), _moveBadBoy[0].GetCurrentY(), BoardElements.BadBoy);

            }
        }
        private void RunParallelThread()
        {
            thread = new Thread(SomeMethod);
            thread.Name = "trololo";
            thread.Start();

        }
        public void StartGame()
        {
            _level++;
            DisplayLevel();
            ViewBoard();
            
        }
        public bool IsPlay()
        {
            if (Lifes > 0)
                return true;
            else return false;
        }
        private void SomeMethod()
        {
            while (_board.QualityBonus > 0)
            {
                Thread.Sleep(300);
                for (int i = 0; i < _badBoyQuality; i++)
                {
                    _moveBadBoy[i].Stepping(i);
                    CheckCollision();
                }
            }
            StartGame();
            SomeMethod();
        }


        private void ViewBoard()
        {
            setBoardComponents();
        }
        private void setBoardComponents()
        {
            _CanvasHost.Dispatcher.Invoke(new Action(() =>
            {
                if (gridIsUsed)
                {
                    clearGrid();
                    _board.UpdateBoard();
                    for (int i = 0; i < _badBoyQuality; i++)
                    {
                        _moveBadBoy[i].SetCoordinates(0, 0);
                        _board.AddComponents(_moveBadBoy[i].GetCurrentX(), _moveBadBoy[i].GetCurrentY(), BoardElements.BadBoy);
                    }
                    _moveMan.SetCoordinates();
                    _board.AddComponents(_moveMan.GetCurrentX(), _moveMan.GetCurrentY(), BoardElements.Man);
                }
                else
                {
                    for (int i = 0; i < _boardSize; i++)
                    {
                        _CanvasHost.ColumnDefinitions.Add(new ColumnDefinition());
                        _CanvasHost.RowDefinitions.Add(new RowDefinition());
                        gridIsUsed = true;
                    }

                }

                for (int j = 0; j < _boardSize; j++)
                {
                    for (int i = 0; i < _boardSize; i++)
                    {
                        Canvas c = new Canvas();
                        c.Background = ColorManager.ChangeColor(_board.BoardElement[i, j]);

                        Grid.SetColumn(c, i);
                        Grid.SetRow(c, j);
                        _CanvasHost.Children.Add(c);
                    }

                }
            }));
        }
        private void clearGrid()
        {


            for (int i = 0; i < _boardSize; i++)
            {
                for (int j = 0; j < _boardSize; j++)
                {
                    var Item = _CanvasHost.Children
                        .Cast<UIElement>()
                        .FirstOrDefault(item => Grid.GetColumn(item) == j && Grid.GetRow(item) == i);
                    _CanvasHost.Children.Remove(Item);

                }
            }

        }
        private void CheckCollision()
        {
            for (int i = 0; i < _badBoyQuality; i++)
            {
                if (_moveBadBoy[i].GetCurrentX() == _moveMan.GetCurrentX() && _moveBadBoy[i].GetCurrentY() == _moveMan.GetCurrentY())
                {
                    if (_moveBadBoy[i].LastStep == BoardElements.Bonus)
                    {
                        _moveMan.AddBonusValue();
                        _moveBadBoy[i].LastStep = BoardElements.Way;
                    }
                    if (_moveBadBoy[i].LastStep == BoardElements.Man) _moveBadBoy[i].LastStep = BoardElements.Way;
                    _moveMan.Lifes--;
                    _moveMan.SetCoordinates();
                    _board.AddComponents(_moveMan.GetCurrentX(), _moveMan.GetCurrentY(), BoardElements.Man);
                    ColorManager.ChangeElementColor(_moveMan.GetCurrentX(), _moveMan.GetCurrentY(), BoardElements.Man);

                }
            }
        }

        public void MoveAction(Side key)
        {
            CheckCollision();
            _moveMan.MoveAction(key);
        }
        public void KillThread()
        {
            thread.Abort();
        }
        private void DisplayLevel()
        {
            if (level == null)
            {

                level = new Level(_level);
                level.Show();
                Thread.Sleep(1500);
                level.Close();
            }
            else
            {
                level.Dispatcher.Invoke(new Action(() =>
                {
                    level = new Level(_level);
                    level.Show();
                    Thread.Sleep(1500);
                    level.Close();
                }));
            }
        }
    }
}
