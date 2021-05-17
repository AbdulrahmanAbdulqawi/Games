// <copyright file="GameWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pacman
{
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
    using Pacman.GameControl;

    /// <summary>
    /// Interaction logic for GameWindow.xaml.
    /// </summary>
    public partial class GameWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameWindow"/> class.
        /// </summary>
        /// <param name="username">User name.</param>
        /// <param name="isLoad">Boolean value to determin if we have to load or start new game.</param>
        public GameWindow(string username, bool isLoad)
        {
            this.Content = new PacmanControl(username, isLoad);
            this.InitializeComponent();
        }
    }
}