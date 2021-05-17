// <copyright file="MainWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pacman
{
    using System;
    using System.Collections.Generic;
    using System.IO;
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
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void NewGameClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.username.Text))
            {
                MessageBox.Show("Please enter username");
                return;
            }

            new GameWindow(this.username.Text, false).ShowDialog();

            // this.Close();
        }

        private void LoadGameClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.username.Text))
            {
                MessageBox.Show("Please enter username");
                return;
            }

            if (!File.Exists($"../../../../../{this.username.Text}State.xml"))
            {
                MessageBox.Show($"{this.username.Text}, you didn't save game.");
                return;
            }

            new GameWindow(this.username.Text, true).ShowDialog();

            // this.Close();
        }

        private void BestScoresClick(object sender, RoutedEventArgs e)
        {
            new BestScoresWindow().ShowDialog();
        }
    }
}