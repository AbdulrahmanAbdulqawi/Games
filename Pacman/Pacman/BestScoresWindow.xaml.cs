// <copyright file="BestScoresWindow.xaml.cs" company="PlaceholderCompany">
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
    using Pacman.GameModel;
    using Pacman.Repository;

    /// <summary>
    /// Interaction logic for BestScoresWindow.xaml.
    /// </summary>
    public partial class BestScoresWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BestScoresWindow"/> class.
        /// </summary>
        public BestScoresWindow()
        {
            PacmanRepository repo = new PacmanRepository();
            List<User> users = repo.LoadBestScore();

            this.InitializeComponent();

            this.scores.ItemsSource = users;
        }
    }
}