// <copyright file="PacmanRenderer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pacman.Renderer
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using Pacman.GameModel;

    /// <summary>
    /// Pacman Renderer class that will create viusals for Pacman Model.
    /// </summary>
    public class PacmanRenderer : IPacmanRenderer
    {
        private PacmanModel model;
        private Drawing background;
        private Drawing walls;
        private Drawing pacman;
        private Drawing redGhost;
        private Drawing pinkGhost;
        private Drawing yellowGhost;
        private Drawing blueGhost;
        private Drawing heavyDot;
        private Drawing lightDot;
        private Drawing scores;
        private Drawing lifes;
        private Drawing fruit;
        private Point pacmanPosition;
        private Point redGhostPosition;
        private Point blueGhostPosition;
        private Point pinkGhostPosition;
        private Point yellowGhostPosition;
        private Dictionary<string, Brush> brushes = new Dictionary<string, Brush>();
        private Typeface font = new Typeface("Arial");
        private string path = "../../../../../Pacman.GameModel/Images/";

        /// <summary>
        /// Initializes a new instance of the <see cref="PacmanRenderer"/> class.
        /// </summary>
        /// <param name="model">Pacman model itself.</param>
        public PacmanRenderer(PacmanModel model)
        {
            this.model = model;
        }

        private Brush PacmanBrush
        {
            get { return this.GetBrush(this.path + "pacman.png", false); }
        }

        private Brush RedGhostBrush
        {
            get { return this.GetBrush(this.path + "red.png", false); }
        }

        private Brush BlueGhostBrush
        {
            get { return this.GetBrush(this.path + "blue.png", false); }
        }

        private Brush YelowGhostBrush
        {
            get { return this.GetBrush(this.path + "yellow.png", false); }
        }

        private Brush PinkGhostBrush
        {
            get { return this.GetBrush(this.path + "pink.png", false); }
        }

        private Brush WallBrush
        {
            get { return this.GetBrush(this.path + "wall.png", true); }
        }

        private Brush PacmanLifesLeft
        {
            get { return this.GetBrush(this.path + "lifes_left.png", true); }
        }

        private Brush PacmanLifesRight
        {
            get { return this.GetBrush(this.path + "lifes_right.png", true); }
        }

        private Brush PacmanLifesDown
        {
            get { return this.GetBrush(this.path + "lifes_down.png", true); }
        }

        private Brush PacmanLifesUp
        {
            get { return this.GetBrush(this.path + "lifes_up.png", true); }
        }

        private Brush KillableBrush
        {
            get { return this.GetBrush(this.path + "killable.png", false); }
        }

        private Brush StrawberryBrush
        {
            get { return this.GetBrush(this.path + "strawberry.png", true); }
        }

        private Brush CherryBrush
        {
            get { return this.GetBrush(this.path + "cherry.png", true); }
        }

        private Brush OrangeBrush
        {
            get { return this.GetBrush(this.path + "orange.png", true); }
        }

        /// <inheritdoc/>
        public Drawing BuildDrawing()
        {
            DrawingGroup drawingGroup = new DrawingGroup();
            drawingGroup.Children.Add(this.GetBackground());
            drawingGroup.Children.Add(this.GetWalls());
            drawingGroup.Children.Add(this.GetScore());
            drawingGroup.Children.Add(this.GetPacmanLifes());
            drawingGroup.Children.Add(this.GetRedGhost());
            drawingGroup.Children.Add(this.GetBlueGhost());
            drawingGroup.Children.Add(this.GetYellowGhost());
            drawingGroup.Children.Add(this.GetPinkGhost());
            drawingGroup.Children.Add(this.GetFruit(this.model.Level));
            drawingGroup.Children.Add(this.GetPacman());
            drawingGroup.Children.Add(this.GetHeavyDots());
            drawingGroup.Children.Add(this.GetLightDots());
            return drawingGroup;
        }

        private Brush GetBrush(string fileName, bool isTiled)
        {
            if (!this.brushes.ContainsKey(fileName))
            {
                BitmapImage img = new BitmapImage(new Uri(fileName, UriKind.Relative));
                ImageBrush ib = new ImageBrush(img);
                if (isTiled)
                {
                    ib.TileMode = TileMode.Tile;
                    ib.Viewport = new System.Windows.Rect(0, 0, this.model.TileSize, this.model.TileSize);
                    ib.ViewportUnits = BrushMappingMode.Absolute;
                }

                this.brushes.Add(fileName, ib);
            }

            return this.brushes[fileName];
        }

        private Drawing GetPacmanLifes()
        {
            FormattedText text = new FormattedText(
                "Lifes : " + this.model.PacmanLifes.ToString(CultureInfo.CurrentCulture),
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                this.font,
                16,
                Brushes.Black);

            this.lifes = new GeometryDrawing(
                Brushes.Green,
                new Pen(Brushes.Red, 1),
                text.BuildGeometry(new Point(this.model.GameWidth / 3, 5)));
            return this.lifes;
        }

        private Drawing GetScore()
        {
            FormattedText text = new FormattedText(
                "Score : " + this.model.Score.ToString(CultureInfo.CurrentCulture),
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                this.font,
                16,
                Brushes.Black);

            this.scores = new GeometryDrawing(
                Brushes.Green,
                new Pen(Brushes.Red, 1),
                text.BuildGeometry(new Point(5, 5)));
            return this.scores;
        }

        private Drawing GetBackground()
        {
            if (this.background == null)
            {
                Geometry bgGeometry = new RectangleGeometry(
                    new Rect(0, 0, this.model.GameWidth, this.model.GameHeight));
                this.background = new GeometryDrawing(Config.BackgroundBrush, null, bgGeometry);
            }

            return this.background;
        }

        private Drawing GetRedGhost()
        {
            if (this.redGhost == null || this.redGhostPosition != this.model.RedGhost)
            {
                if (this.model.CanEat == 0)
                {
                    Geometry bgGeometry = new RectangleGeometry(
                   new Rect(this.model.RedGhost.X * this.model.TileSize, this.model.RedGhost.Y * this.model.TileSize, this.model.TileSize, this.model.TileSize));
                    this.redGhost = new GeometryDrawing(this.RedGhostBrush, null, bgGeometry);
                    this.redGhostPosition = this.model.RedGhost;
                }
                else
                {
                    Geometry bgGeometry = new RectangleGeometry(
                   new Rect(this.model.RedGhost.X * this.model.TileSize, this.model.RedGhost.Y * this.model.TileSize, this.model.TileSize, this.model.TileSize));
                    this.redGhost = new GeometryDrawing(this.KillableBrush, null, bgGeometry);
                    this.redGhostPosition = this.model.RedGhost;
                }
            }

            return this.redGhost;
        }

        private Drawing GetBlueGhost()
        {
            if (this.blueGhost == null || this.blueGhostPosition != this.model.BlueGhost)
            {
                if (this.model.CanEat == 0)
                {
                    Geometry bgGeometry = new RectangleGeometry(
                   new Rect(this.model.BlueGhost.X * this.model.TileSize, this.model.BlueGhost.Y * this.model.TileSize, this.model.TileSize, this.model.TileSize));
                    this.blueGhost = new GeometryDrawing(this.BlueGhostBrush, null, bgGeometry);
                    this.blueGhostPosition = this.model.BlueGhost;
                }
                else
                {
                    Geometry bgGeometry = new RectangleGeometry(
                  new Rect(this.model.BlueGhost.X * this.model.TileSize, this.model.BlueGhost.Y * this.model.TileSize, this.model.TileSize, this.model.TileSize));
                    this.blueGhost = new GeometryDrawing(this.KillableBrush, null, bgGeometry);
                    this.blueGhostPosition = this.model.BlueGhost;
                }
            }

            return this.blueGhost;
        }

        private Drawing GetYellowGhost()
        {
            if (this.yellowGhost == null || this.yellowGhostPosition != this.model.YellowGhost)
            {
                if (this.model.CanEat == 0)
                {
                    Geometry bgGeometry = new RectangleGeometry(
                       new Rect(this.model.YellowGhost.X * this.model.TileSize, this.model.YellowGhost.Y * this.model.TileSize, this.model.TileSize, this.model.TileSize));
                    this.yellowGhost = new GeometryDrawing(this.YelowGhostBrush, null, bgGeometry);
                    this.yellowGhostPosition = this.model.YellowGhost;
                }
                else
                {
                    Geometry bgGeometry = new RectangleGeometry(
                      new Rect(this.model.YellowGhost.X * this.model.TileSize, this.model.YellowGhost.Y * this.model.TileSize, this.model.TileSize, this.model.TileSize));
                    this.yellowGhost = new GeometryDrawing(this.KillableBrush, null, bgGeometry);
                    this.yellowGhostPosition = this.model.YellowGhost;
                }
            }

            return this.yellowGhost;
        }

        private Drawing GetPinkGhost()
        {
            if (this.pinkGhost == null || this.pinkGhostPosition != this.model.PinkGhost)
            {
                if (this.model.CanEat == 0)
                {
                    Geometry bgGeometry = new RectangleGeometry(
                   new Rect(this.model.PinkGhost.X * this.model.TileSize, this.model.PinkGhost.Y * this.model.TileSize, this.model.TileSize, this.model.TileSize));
                    this.pinkGhost = new GeometryDrawing(this.PinkGhostBrush, null, bgGeometry);
                    this.pinkGhostPosition = this.model.PinkGhost;
                }
                else
                {
                    Geometry bgGeometry = new RectangleGeometry(
                    new Rect(this.model.PinkGhost.X * this.model.TileSize, this.model.PinkGhost.Y * this.model.TileSize, this.model.TileSize, this.model.TileSize));
                    this.pinkGhost = new GeometryDrawing(this.KillableBrush, null, bgGeometry);
                    this.pinkGhostPosition = this.model.PinkGhost;
                }
            }

            return this.pinkGhost;
        }

        private Drawing GetWalls()
        {
            if (this.walls == null)
            {
                GeometryGroup g = new GeometryGroup();
                for (int i = 0; i < this.model.Walls.GetLength(0); i++)
                {
                    for (int j = 0; j < this.model.Walls.GetLength(1); j++)
                    {
                        if (this.model.Walls[i, j])
                        {
                            Geometry box = new RectangleGeometry(
                                new Rect(i * this.model.TileSize, j * this.model.TileSize, this.model.TileSize, this.model.TileSize));
                            g.Children.Add(box);
                        }
                    }
                }

                this.walls = new GeometryDrawing(this.WallBrush, null, g);
            }

            return this.walls;
        }

        private Drawing GetPacman()
        {
            if (this.pacman == null || this.pacmanPosition != this.model.Pacman)
            {
                Geometry g;
                switch (this.model.CurrentDirection)
                {
                    case Direction.Stopped:
                        g = new RectangleGeometry(
                    new Rect(this.model.Pacman.X * this.model.TileSize, this.model.Pacman.Y * this.model.TileSize, this.model.TileSize, this.model.TileSize));
                        this.pacman = new GeometryDrawing(this.PacmanBrush, null, g);
                        this.pacmanPosition = this.model.Pacman;
                        break;
                    case Direction.Left:
                        g = new RectangleGeometry(
                    new Rect(this.model.Pacman.X * this.model.TileSize, this.model.Pacman.Y * this.model.TileSize, this.model.TileSize, this.model.TileSize));
                        this.pacman = new GeometryDrawing(this.PacmanLifesLeft, null, g);
                        this.pacmanPosition = this.model.Pacman;
                        break;
                    case Direction.Right:
                        g = new RectangleGeometry(
                  new Rect(this.model.Pacman.X * this.model.TileSize, this.model.Pacman.Y * this.model.TileSize, this.model.TileSize, this.model.TileSize));
                        this.pacman = new GeometryDrawing(this.PacmanLifesRight, null, g);
                        this.pacmanPosition = this.model.Pacman;
                        break;
                    case Direction.Up:
                        g = new RectangleGeometry(
                  new Rect(this.model.Pacman.X * this.model.TileSize, this.model.Pacman.Y * this.model.TileSize, this.model.TileSize, this.model.TileSize));
                        this.pacman = new GeometryDrawing(this.PacmanLifesUp, null, g);
                        this.pacmanPosition = this.model.Pacman;
                        break;
                    case Direction.Down:
                        g = new RectangleGeometry(
                  new Rect(this.model.Pacman.X * this.model.TileSize, this.model.Pacman.Y * this.model.TileSize, this.model.TileSize, this.model.TileSize));
                        this.pacman = new GeometryDrawing(this.PacmanLifesDown, null, g);
                        this.pacmanPosition = this.model.Pacman;
                        break;
                    default:
                        break;
                }
            }

            return this.pacman;
        }

        private Drawing GetFruit(int level)
        {
            GeometryGroup g = new GeometryGroup();
            for (int i = 0; i < this.model.Fruits.GetLength(0); i++)
            {
                for (int j = 0; j < this.model.Fruits.GetLength(1); j++)
                {
                    if (this.model.Fruits[i, j])
                    {
                        Geometry box = new RectangleGeometry(
                           new Rect((int)(i * this.model.TileSize), (int)(j * this.model.TileSize), this.model.TileSize, this.model.TileSize));
                        g.Children.Add(box);
                    }
                }
            }

            if (level == 0)
            {
                this.fruit = new GeometryDrawing(this.StrawberryBrush, null, g);
            }
            else if (level == 1)
            {
                this.fruit = new GeometryDrawing(this.CherryBrush, null, g);
            }
            else if (level == 2)
            {
                this.fruit = new GeometryDrawing(this.OrangeBrush, null, g);
            }

            return this.fruit;
        }

        private Drawing GetHeavyDots()
        {
            GeometryGroup g = new GeometryGroup();
            for (int i = 0; i < this.model.HeavyDot.GetLength(0); i++)
            {
                for (int j = 0; j < this.model.HeavyDot.GetLength(1); j++)
                {
                    if (this.model.HeavyDot[i, j])
                    {
                        Geometry box = new EllipseGeometry(
                            new Rect((i * this.model.TileSize) + (this.model.TileSize / 2) - 5, (j * this.model.TileSize) + (this.model.TileSize / 2) - 5, Config.HeavyDotSize, Config.HeavyDotSize));
                        g.Children.Add(box);
                    }
                }
            }

            this.heavyDot = new GeometryDrawing(Config.DotBrush, null, g);

            return this.heavyDot;
        }

        private Drawing GetLightDots()
        {
            GeometryGroup g = new GeometryGroup();
            for (int i = 0; i < this.model.LightDots.GetLength(0); i++)
            {
                for (int j = 0; j < this.model.LightDots.GetLength(1); j++)
                {
                    if (this.model.LightDots[i, j])
                    {
                        Geometry box = new EllipseGeometry(
                            new Rect((i * this.model.TileSize) + (this.model.TileSize / 2), (j * this.model.TileSize) + (this.model.TileSize / 2), Config.LightDotSize, Config.LightDotSize));
                        g.Children.Add(box);
                    }
                }
            }

            this.lightDot = new GeometryDrawing(Config.DotBrush, null, g);
            return this.lightDot;
        }
    }
}