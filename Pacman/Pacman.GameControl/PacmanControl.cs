// <copyright file="PacmanControl.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pacman.GameControl
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;
    using System.Xml.Serialization;
    using Pacman.GameLogic;
    using Pacman.GameModel;
    using Pacman.Renderer;
    using Pacman.Repository;

    /// <summary>
    /// Pacman WPF Control class.
    /// </summary>
    public class PacmanControl : FrameworkElement
    {
        private PacmanLogic logic;
        private PacmanModel model;
        private PacmanRenderer renderer;
        private Stopwatch stw;
        private DispatcherTimer tickTimer;
        private bool isLoaded;
        private string username;
        private PacmanRepository repo = new PacmanRepository();
        private Direction currentDir = Direction.Stopped;
        private string lvl = "../../../../../Pacman.GameModel/Levels/L0";

        /// <summary>
        /// Initializes a new instance of the <see cref="PacmanControl"/> class.
        /// </summary>
        /// <param name="username">User name.</param>
        /// <param name="isLoaded">Boolean value to determin if we have to load or start new game.</param>
        public PacmanControl(string username, bool isLoaded)
        {
            this.username = username;
            this.isLoaded = isLoaded;
            this.Loaded += this.GameControl_Loaded;
        }

        /// <summary>
        /// Ovveride of OnRender method, used for drawing PacmanModel.
        /// </summary>
        /// <param name="drawingContext">Pacman model as visual content.</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            if (this.renderer != null && drawingContext != null)
            {
                drawingContext.DrawDrawing(this.renderer.BuildDrawing());
            }
        }

        private void GameControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.LoadGame(0);
        }

        private void LoadGame(int level, int score = 0)
        {
            this.stw = new Stopwatch();
            if (this.isLoaded)
            {
                this.model = this.repo.LoadCurrentState(this.username);
                this.model.Transform();
            }
            else
            {
                this.model = new PacmanModel(this.ActualWidth, this.ActualHeight);
                this.model.User = new User(this.username, 0);
                this.model.Score = score;
                this.model.Level = level;
            }

            this.logic = new PacmanLogic(this.model, this.lvl + $"{this.model.Level}.lvl", !this.isLoaded);
            this.renderer = new PacmanRenderer(this.model);
            this.isLoaded = false;
            Window win = Window.GetWindow(this);
            if (win != null)
            {
                if (this.tickTimer != null)
                {
                    this.tickTimer.Stop();
                }

                this.tickTimer = new DispatcherTimer();
                this.tickTimer.Interval = TimeSpan.FromMilliseconds(200);
                this.tickTimer.Tick += this.TickTimer_Tick;
                this.tickTimer.Start();
                win.KeyDown += this.Win_KeyDown;
            }

            this.InvalidateVisual();
            this.stw.Start();
        }

        private void MoveGhosts()
        {
            this.logic.MoveRedGhost();
            this.logic.MoveBlueGhost();
            this.logic.MoveYellowGhost();
            this.logic.MovePinkGhost();
        }

        private void TickTimer_Tick(object sender, EventArgs e)
        {
            this.MoveGhosts();

            if (this.logic.DidDie())
            {
                this.model.User.Score = this.model.Score;
                List<User> currentBest = this.repo.LoadBestScore();
                if (currentBest.FindAll(x => x.Score > this.model.User.Score).Count == 0)
                {
                    MessageBox.Show("Game over!\nYou have new best score!");
                }
                else
                {
                    MessageBox.Show("Game over! ");
                }

                this.repo.SaveBestScore(this.model.User);
                this.tickTimer.Stop();
                this.stw.Stop();
                Window.GetWindow(this).Close();
            }

            bool newLevel = false;
            switch (this.currentDir)
            {
                // case Direction.Stopped:
                //    newLevel = logic.Move(0, 0);
                //    break;
                case Direction.Left:
                    newLevel = this.logic.Move(-1, 0);
                    break;
                case Direction.Right:
                    newLevel = this.logic.Move(1, 0);
                    break;
                case Direction.Up:
                    newLevel = this.logic.Move(0, -1);
                    break;
                case Direction.Down:
                    this.logic.Move(0, 1);
                    break;
            }

            this.model.CurrentDirection = this.currentDir;

            if (newLevel)
            {
                MessageBox.Show("Next Level!");
                this.model.Level += 1;
                this.LoadGame(this.model.Level, this.model.Score);
            }

            this.InvalidateVisual();
        }

        private void Win_KeyDown(object sender, KeyEventArgs e)
        {
            bool finished = false;
            switch (e.Key)
            {
                case Key.Up: this.currentDir = Direction.Up; break;
                case Key.Down: this.currentDir = Direction.Down; break;
                case Key.Left: this.currentDir = Direction.Left; break;
                case Key.Right: this.currentDir = Direction.Right; break;
                case Key.S:
                    this.repo.SaveCurrentState(this.model);
                    MessageBox.Show("Your current game is saved!");
                    this.tickTimer.Stop();
                    this.stw.Stop();
                    Window.GetWindow(this).Close();
                    return;
            }

            this.InvalidateVisual();
            if (finished)
            {
                this.stw.Stop();
                MessageBox.Show("Game over! ");
            }
        }
    }
}