﻿using PacMan.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PacMan.UI.View
{
    /// <summary>
    /// Interaction logic for PlayGame.xaml
    /// </summary>
    public partial class PlayGame : Window
    {
        public PlayGame()

        {
            App.log.Trace("Запуск игры");

            InitializeComponent();
            DataContext = new PlayGameVM(CanvasHost);
            App.log.Trace("Игра запущенна");

        }
    }
}
