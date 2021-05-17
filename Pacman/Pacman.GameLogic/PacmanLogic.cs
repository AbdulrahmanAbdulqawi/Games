// <copyright file="PacmanLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pacman.GameLogic
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using Pacman.GameModel;

    /// <summary>
    /// Pacman game logic class.
    /// </summary>
    public class PacmanLogic : IPacmanLogic
    {
        private PacmanModel model;
        private int width;
        private int height;

        /// <summary>
        /// Initializes a new instance of the <see cref="PacmanLogic"/> class.
        /// </summary>
        /// <param name="model">Pacman model itself.</param>
        /// <param name="fname">Level path.</param>
        /// <param name="newGame">Boolean value determing if the game is new or loaded.</param>
        public PacmanLogic(PacmanModel model, string fname, bool newGame)
        {
            this.model = model;
            if (!File.Exists(fname))
            {
                throw new FileNotFoundException("File Not Found");
            }

            string[] lines = File.ReadAllLines(fname);
            this.width = int.Parse(lines[0], CultureInfo.CurrentCulture);
            this.height = int.Parse(lines[1], CultureInfo.CurrentCulture);
            this.model.Walls = new bool[this.width, this.height];
            this.model.Map = new char[this.width, this.height];
            this.model.TileSize = Math.Min(this.model.GameWidth / this.width, this.model.GameHeight / this.height);

            if (newGame)
            {
                this.NewGame(lines);
            }
            else
            {
                this.LoadGame(lines);
            }
        }

        /// <inheritdoc/>
        public bool NextLevel(bool[,] matrix)
        {
            if (matrix != null)
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (matrix[i, j])
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        /// <inheritdoc/>
        public void MovePacman(int newX, int newY)
        {
            if (this.model.Pacman.Equals(this.model.RedGhost) || this.model.Pacman.Equals(this.model.BlueGhost) || this.model.Pacman.Equals(this.model.YellowGhost) || this.model.Pacman.Equals(this.model.PinkGhost))
            {
            }
            else
            {
                this.model.Pacman = new Point(newX, newY);
            }
        }

        /// <summary>
        /// Method that handles what happens when Pacman eats Pink Ghost.
        /// </summary>
        public void PacmanEatPinkGhost()
        {
            this.model.Eaten++;
            this.model.Score += this.model.Eaten * 100;
            if (this.model.Level < 2)
            {
                this.model.PinkGhost = new Point(11, 15);
            }
            else if (this.model.Level == 2)
            {
                this.model.PinkGhost = new Point(11, 15);
            }
        }

        /// <inheritdoc/>
        public bool PacmanDied()
        {
            if (this.model.Pacman.Equals(this.model.RedGhost) || this.model.Pacman.Equals(this.model.BlueGhost) || this.model.Pacman.Equals(this.model.YellowGhost) || this.model.Pacman.Equals(this.model.PinkGhost))
            {
                this.model.PacmanLifes--;
                this.model.Pacman = new Point(1, 30);
            }

            if (this.model.PacmanLifes == 0)
            {
                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        public bool DidDie()
        {
            this.model.CanEat = this.model.CanEat != 0 ? this.model.CanEat - 1 : 0;
            if (this.model.CanEat == 0)
            {
                if (this.PacmanDied())
                {
                    return true;
                }
            }
            else
            {
                if (this.model.Pacman.Equals(this.model.RedGhost))
                {
                    this.PacmanEatRedGhost();
                }
                else if (this.model.Pacman.Equals(this.model.BlueGhost))
                {
                    this.PacmanEatBlueGhost();
                }
                else if (this.model.Pacman.Equals(this.model.YellowGhost))
                {
                    this.PacmanEatYellowGhost();
                }
                else if (this.model.Pacman.Equals(this.model.PinkGhost))
                {
                    this.PacmanEatPinkGhost();
                }
            }

            return false;
        }

        /// <inheritdoc/>
        public bool MoveRedGhost()
        {
            Point p = ShortestPath.BFS(this.model.Walls, this.model.RedGhost, this.model.Pacman);
            if ((p.X != -1 || p.Y != -1) && this.model.CanEat == 0)
            {
                this.model.RedGhost = p;
                return false;
            }

            int newX;
            int newY;
            this.MoveGhost(this.model.RedGhost, out newX, out newY);
            if (newX >= 0 && newX < this.model.Walls.GetLength(0) && newY < this.model.Walls.GetLength(1) && !this.model.Walls[newX, newY])
            {
                this.model.RedGhost = new Point(newX, newY);
            }

            return false;
        }

        /// <inheritdoc/>
        public bool MoveBlueGhost()
        {
            /*Point p = ShortestPath.BFS(this.model.Walls, this.model.BlueGhost, this.model.Pacman);
            if (p.X != -1 || p.Y != -1)
            {
                this.model.BlueGhost = p;
                return false;

            }
            */

            bool finished = false;

            int newX;
            int newY;
            this.MoveGhost(this.model.BlueGhost, out newX, out newY);
            if (newX >= 0 && newX < this.model.Walls.GetLength(0) && newY < this.model.Walls.GetLength(1) && !this.model.Walls[newX, newY])
            {
                this.model.BlueGhost = new Point(newX, newY);
            }

            return finished;
        }

        /// <inheritdoc/>
        public bool MoveYellowGhost()
        {
            /*Point p = ShortestPath.BFS(this.model.Walls, this.model.YellowGhost, this.model.Pacman);
            if ((p.X != -1 || p.Y != -1) && this.model.CanEat == 0)
            {
                this.model.YellowGhost = p;
                return false;

            } */

            bool finished = false;

            int newX;
            int newY;

            this.MoveGhost(this.model.YellowGhost, out newX, out newY);
            if (newX >= 0 && newX < this.model.Walls.GetLength(0) && newY < this.model.Walls.GetLength(1) && !this.model.Walls[newX, newY])
            {
                this.model.YellowGhost = new Point(newX, newY);
            }

            return finished;
        }

        /// <inheritdoc/>
        public bool MovePinkGhost()
        {
            /*Point p = ShortestPath.BFS(this.model.Walls, this.model.PinkGhost, this.model.Pacman);
            if (p.X != -1 || p.Y != -1)
            {
                this.model.PinkGhost = p;
                return false;
            } */

            bool finished = false;

            int newX;
            int newY;
            this.MoveGhost(this.model.PinkGhost, out newX, out newY);
            if (newX >= 0 && newX < this.model.Walls.GetLength(0) && newY < this.model.Walls.GetLength(1) && !this.model.Walls[newX, newY])
            {
                this.model.PinkGhost = new Point(newX, newY);
            }

            return finished;
        }

        /// <inheritdoc/>
        public void MoveGhost(Point p, out int newX, out int newY)
        {
            newX = (int)p.X;
            newY = (int)p.Y;

            if (p.X == this.model.Pacman.X)
            {
                newY = this.model.Pacman.Y > p.Y ? newY + 1 : newY - 1;
                if (!this.model.Walls[newX, newY])
                {
                    return;
                }
            }
            else if (p.Y == this.model.Pacman.Y)
            {
                newX = this.model.Pacman.X > p.X ? newX + 1 : newX - 1;

                if (!this.model.Walls[newX, newY])
                {
                    return;
                }
            }

            int randomNum = RandomNumber.Between(0, 3);

            if (randomNum == 0)
            {
                newX = (int)p.X - 1;
            }
            else if (randomNum == 1)
            {
                newX = (int)p.X + 1;
            }
            else if (randomNum == 2)
            {
                newY = (int)p.Y + 1;
            }
            else if (randomNum == 3)
            {
                newY = (int)p.Y - 1;
            }
        }

        /// <inheritdoc/>
        public void EatDots(int newX, int newY)
        {
            if (this.model.HeavyDot[newX, newY])
            {
                this.model.HeavyDot[newX, newY] = false;
                this.model.Score += 10;
                this.model.CanEat = 50;
            }
            else if (this.model.LightDots[newX, newY])
            {
                this.model.LightDots[newX, newY] = false;
                this.model.Score += 2;
            }
        }

        /// <inheritdoc/>
        public void EatFruits(int newX, int newY)
        {
            if (this.model.Fruits[newX, newY])
            {
                this.model.Fruits[newX, newY] = false;
                this.model.Score += (this.model.Level * 200) + 100;
            }
        }

        /// <inheritdoc/>
        public bool Move(int dx, int dy)
        {
            int newX;
            int newY;
            bool teleport = false;

            newX = (int)(this.model.Pacman.X + dx);
            newY = (int)(this.model.Pacman.Y + dy);
            int tempX, tempY;
            if (teleport = this.Teleport(newX, /*newY,*/ out tempX, out tempY))
            {
                newX = tempX;
                newY = tempY;
            }

            if (teleport || (newX >= 0 && newX < this.model.Walls.GetLength(0) && newY < this.model.Walls.GetLength(1) && !this.model.Walls[newX, newY]))
            {
                this.EatDots(newX, newY);
                this.EatFruits(newX, newY);
                if (this.DidNextLevel(this.model.LightDots, this.model.HeavyDot, this.model.Fruits))
                {
                    return true;
                }

                this.MovePacman(newX, newY);
            }

            return false;
        }

        private void NewGame(string[] lines)
        {
            this.model.LightDots = new bool[this.width, this.height];
            this.model.HeavyDot = new bool[this.width, this.height];
            this.model.Fruits = new bool[this.width, this.height];
            this.model.PacmanLifes = 3;
            for (int x = 0; x < this.width; x++)
            {
                for (int y = 0; y < this.height; y++)
                {
                    char current = lines[y + 2][x];
                    this.model.Walls[x, y] = current == '#';

                    if (current == 'P')
                    {
                        this.model.Pacman = new Point(x, y);
                    }

                    if (current == 'r')
                    {
                        this.model.RedGhost = new Point(x, y);
                    }

                    if (current == 'p')
                    {
                        this.model.PinkGhost = new Point(x, y);
                    }

                    if (current == 'y')
                    {
                        this.model.YellowGhost = new Point(x, y);
                    }

                    if (current == 'b')
                    {
                        this.model.BlueGhost = new Point(x, y);
                    }

                    this.model.Fruits[x, y] = current == 'f';
                    this.model.LightDots[x, y] = current == '.';
                    this.model.HeavyDot[x, y] = current == '@';

                    this.model.Map[x, y] = current;
                }
            }
        }

        private void LoadGame(string[] lines)
        {
            for (int x = 0; x < this.width; x++)
            {
                for (int y = 0; y < this.height; y++)
                {
                    char current = lines[y + 2][x];
                    this.model.Walls[x, y] = current == '#';
                    this.model.Map[x, y] = current;
                }
            }
        }

        private bool Teleport(int newX, /*int newY,*/ out int x, out int y)
        {
            x = 0;
            y = 0;
            if (this.model.Level == 1)
            {
                if (this.model.Pacman.X == 27 && newX == 28 && this.model.Pacman.Y == 15)
                {
                    x = 0;
                    y = 15;
                    return true;
                }
                else if (this.model.Pacman.X == 0 && newX == -1 && this.model.Pacman.Y == 15)
                {
                    x = 27;
                    y = 15;
                    return true;
                }
            }
            else if (this.model.Level == 2)
            {
                if (this.model.Pacman.X == 0 && newX == -1 && this.model.Pacman.Y == 15)
                {
                    x = 55;
                    y = 15;
                    return true;
                }
                else if (this.model.Pacman.X == 55 && newX == 56 && this.model.Pacman.Y == 15)
                {
                    x = 0;
                    y = 15;
                    return true;
                }
            }

            return false;
        }

        private bool DidNextLevel(bool[,] lightDots, bool[,] heavyDots, bool[,] fruits)
        {
            return this.NextLevel(lightDots) && this.NextLevel(heavyDots) && this.NextLevel(fruits);
        }

        private void PacmanEatRedGhost()
        {
            this.model.Eaten++;
            this.model.Score += this.model.Eaten * 100;
            if (this.model.Level < 2)
            {
                this.model.RedGhost = new Point(13, 15);
            }
            else if (this.model.Level == 2)
            {
                this.model.RedGhost = new Point(39, 15);
            }
        }

        private void PacmanEatBlueGhost()
        {
            this.model.Eaten++;
            this.model.Score += this.model.Eaten * 100;
            if (this.model.Level < 2)
            {
                this.model.BlueGhost = new Point(13, 12);
            }
            else if (this.model.Level == 2)
            {
                this.model.BlueGhost = new Point(44, 15);
            }
        }

        private void PacmanEatYellowGhost()
        {
            this.model.Eaten++;
            this.model.Score += this.model.Eaten * 100;
            if (this.model.Level < 2)
            {
                this.model.YellowGhost = new Point(15, 15);
            }
            else if (this.model.Level == 2)
            {
                this.model.YellowGhost = new Point(16, 15);
            }
        }
    }
}