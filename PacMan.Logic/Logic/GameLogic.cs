using NLog;
using PacMan.Logic.Concrete;
using PacMan.Logic.Model;
using PacMan.UI.Concrete.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PacMan.Logic.Logic
{
    public class GameLogic
    {

        private Logger log = LogManager.GetCurrentClassLogger();
        private MoveMan _moveMan;
        private MoveBadBoy[] _moveBadBoy;
        private static int _badBoyQuality = 3;
        private int _level = 1;
        private Grid _CanvasHost;
        private bool gridIsUsed = false;
        private Board _board;
        private int _boardSize = 15;
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
            set
            {
                _level = value;
            }
            
        }
        private void InicializeComponents()
        {

            _moveBadBoy = new MoveBadBoy[_badBoyQuality];
            _board = new Board(_boardSize);
            _moveMan = new MoveMan(_board, _CanvasHost);
            for (int i = 0; i < _badBoyQuality; i++)
            {
                _moveBadBoy[i] = new MoveBadBoy(_board, _moveMan.Man, _CanvasHost);
            }

        
        _moveBadBoy[0].SetCoordinates(0, 0);
        _moveBadBoy[1].SetCoordinates(_boardSize - 1, _boardSize - 1);
        _moveBadBoy[2].SetCoordinates(0, _boardSize - 1);

            for (int i = 0; i<_badBoyQuality; i++)
            {
                _board.AddComponents(_moveBadBoy[0].GetCurrentX(), _moveBadBoy[0].GetCurrentY(), BoardElements.BadBoy);

            }
}
        private void RunParallelThread()
        {
            thread = new Thread(SomeMethod);
            thread.Name = "Move Bad Boys";
            log.Debug("Запуск потока {0}", thread.Name);
            thread.Start();

        }
        public void StartGame()
        {
           
            setBoardComponents();

            

        }
        public bool IsPlay()
        {
            if (Lifes > 0)
                return true;
            else return false;
        }
        public bool ChangeLevel()
        {
            if (_board.QualityBonus > 0)
                return false;
            else
            {
                log.Trace("Изменения свойства Level");
                return true; }
        }
        private void SomeMethod()
        {
            
            while (_board.QualityBonus > 0)
            {
                Thread.Sleep(15000);
                for (int i = 0; i < _badBoyQuality; i++)
                {
                    _moveBadBoy[i].Stepping(i);
                    CheckCollision();
                }
            }
            StartGame();
            SomeMethod();
        }
       
        private void setBoardComponents()
        {

            _CanvasHost.Dispatcher.Invoke(new Action(() =>
            {
                log.Trace("Старт добавления элементов на игральное поле");
                if (gridIsUsed)
                {
                    clearGrid();
                    _board.UpdateBoard();
                    _moveBadBoy[0].SetCoordinates(0, 0);
                    _moveBadBoy[1].SetCoordinates(_boardSize - 1, _boardSize - 1);
                    _moveBadBoy[2].SetCoordinates(0, _boardSize - 1);
                    for (int i = 0; i < _badBoyQuality; i++)
                    {
                        _board.AddComponents(_moveBadBoy[0].GetCurrentX(), _moveBadBoy[0].GetCurrentY(), BoardElements.BadBoy);

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
                log.Trace("Элементы добавлены");

            }));
        }
        private void clearGrid()
        {

            log.Trace("Старт очистки игрального поля");

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
            log.Trace("игральное поле очищенно");

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
                    log.Trace("Изменение свойства Lifes");

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
            log.Debug("Убит поток {0}",thread.Name);
            thread.Abort();
        }
    }
}
