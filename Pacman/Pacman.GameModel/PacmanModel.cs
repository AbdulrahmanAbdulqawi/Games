// <copyright file="PacmanModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pacman.GameModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Xml.Serialization;

    /// <summary>
    /// All propreties that PacmaModel contains.
    /// </summary>
    [Serializable]
    public class PacmanModel : IPacmanModel
    {
        private List<int> heavyDotList;
        private List<int> fruitsList;
        private List<int> lightDotsList;
        private int score;

        /// <summary>
        /// Initializes a new instance of the <see cref="PacmanModel"/> class.
        /// </summary>
        public PacmanModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PacmanModel"/> class.
        /// </summary>
        /// <param name="width">Game width.</param>
        /// <param name="height">Game height.</param>
        public PacmanModel(double width, double height)
        {
            this.CurrentDirection = Direction.Stopped;
            this.Level = 2;
            this.User = new User("Pavle", 0);
            this.GameWidth = width;
            this.GameHeight = height;
            this.CanEat = 0;
            this.Eaten = 0;
        }

        /// <summary>
        /// Gets or sets fruits.
        /// </summary>
        [XmlIgnore]
        public bool[,] Fruits { get; set; }

        /// <summary>
        /// Gets or sets Walls.
        /// </summary>
        [XmlIgnore]
        public bool[,] Walls { get; set; }

        /// <summary>
        /// Gets or sets Pacman coordinates.
        /// </summary>
        public Point Pacman { get; set; }

        /// <summary>
        /// Gets or sets HeavyDot arrray.
        /// </summary>
        [XmlIgnore]
        public bool[,] HeavyDot { get; set; }

        /// <summary>
        /// Gets or sets HeavyDotList.
        /// </summary>
        public List<int> HeavyDotList
        {
            get
            {
                if (this.HeavyDot != null)
                {
                    this.heavyDotList = new List<int>();
                    this.heavyDotList.Add(this.HeavyDot.GetLength(0));
                    this.heavyDotList.Add(this.HeavyDot.GetLength(1));

                    foreach (bool item in this.HeavyDot)
                    {
                        this.heavyDotList.Add(item ? 1 : 0);
                    }

                    return this.heavyDotList;
                }

                return this.heavyDotList;
            }

            set
            {
                this.heavyDotList = value;
            }
        }

        /// <summary>
        /// Gets or sets LightDots array.
        /// </summary>
        [XmlIgnore]
        public bool[,] LightDots { get; set; }

        /// <summary>
        /// Gets or sets Light Dots List.
        /// </summary>
        public List<int> LightDotsList
        {
            get
            {
                if (this.LightDots != null)
                {
                    this.lightDotsList = new List<int>();
                    this.lightDotsList.Add(this.LightDots.GetLength(0));
                    this.lightDotsList.Add(this.LightDots.GetLength(1));

                    foreach (bool item in this.LightDots)
                    {
                        this.lightDotsList.Add(item ? 1 : 0);
                    }

                    return this.lightDotsList;
                }

                return this.lightDotsList;
            }

            set
            {
                this.lightDotsList = value;
            }
        }

        /// <summary>
        /// Gets or sets HeavyDotList.
        /// </summary>
        public List<int> FruitsList
        {
            get
            {
                if (this.Fruits != null)
                {
                    this.fruitsList = new List<int>();
                    this.fruitsList.Add(this.Fruits.GetLength(0));
                    this.fruitsList.Add(this.Fruits.GetLength(1));

                    foreach (bool item in this.Fruits)
                    {
                        this.fruitsList.Add(item ? 1 : 0);
                    }

                    return this.fruitsList;
                }

                return this.fruitsList;
            }

            set
            {
                this.fruitsList = value;
            }
        }

        /// <summary>
        /// Gets or sets Map.
        /// </summary>
        [XmlIgnore]
        public char[,] Map { get; set; }

        /// <summary>
        /// Gets or sets Red Ghost coordinates.
        /// </summary>
        public Point RedGhost { get; set; }

        /// <summary>
        /// Gets or sets Yellow Ghost coordinates.
        /// </summary>
        public Point YellowGhost { get; set; }

        /// <summary>
        ///  Gets or sets Blue Ghost coordinates.
        /// </summary>
        public Point BlueGhost { get; set; }

        /// <summary>
        ///  Gets or sets Pink Ghost coordinates.
        /// </summary>
        public Point PinkGhost { get; set; }

        /// <summary>
        ///  Gets or sets Level.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        ///  Gets or sets number of Pacman Lifes.
        /// </summary>
        public int PacmanLifes { get; set; }

        /// <summary>
        ///  Gets or sets Game Width.
        /// </summary>
        public double GameWidth { get; set; }

        /// <summary>
        ///  Gets or sets Game Height.
        /// </summary>
        public double GameHeight { get; set; }

        /// <summary>
        ///  Gets or sets Tile Size.
        /// </summary>
        public double TileSize { get; set; }

        /// <summary>
        /// Gets or sets Score.
        /// </summary>
        public int Score
        {
            get
            {
                return this.score;
            }

            set
            {
                this.score = value;
                if (this.score > 5000)
                {
                    this.PacmanLifes++;
                }
            }
        }

        /// <summary>
        /// Gets or sets User.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets CanEat.
        /// </summary>
        public int CanEat { get; set; }

        /// <summary>
        /// Gets or sets Eaten.
        /// </summary>
        public int Eaten { get; set; }

        /// <summary>
        /// Gets or sets Current Direction.
        /// </summary>
        public Direction CurrentDirection { get; set; }

        /// <inheritdoc/>
        public void Transform()
        {
            int row = this.heavyDotList[0];
            int col = this.heavyDotList[1];
            this.HeavyDot = new bool[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    this.HeavyDot[i, j] = this.heavyDotList[(i * col) + j + 2] == 1 ? true : false;
                }
            }

            row = this.lightDotsList[0];
            col = this.lightDotsList[1];
            this.LightDots = new bool[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    this.LightDots[i, j] = this.lightDotsList[(i * col) + j + 2] == 1 ? true : false;
                }
            }

            row = this.fruitsList[0];
            col = this.fruitsList[1];
            this.Fruits = new bool[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    this.Fruits[i, j] = this.fruitsList[(i * col) + j + 2] == 1 ? true : false;
                }
            }
        }
    }
}