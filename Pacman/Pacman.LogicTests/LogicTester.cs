// <copyright file="LogicTester.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pacman.LogicTests
{
    using System.IO;
    using System.Windows;
    using Moq;
    using NUnit.Framework;
    using Pacman.GameLogic;
    using Pacman.GameModel;

    /// <summary>
    /// Logic test for PacmanLogic class.
    /// </summary>
    [TestFixture]
    public class LogicTester
    {
        private Mock<PacmanModel> pacmanModelMock;
        private string lvl = "../../../../../Pacman.GameModel/Levels/L00.lvl";

        /// <summary>
        /// Setiting the enviroment.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.pacmanModelMock = new Mock<PacmanModel>();
        }

        /// <summary>
        /// Tests if the ctor creates a PacmanLogic instance for a new game.
        /// </summary>
        [Test]
        public void TestConstructorNewGameOK()
        {
            PacmanLogic pacmanLogic = new PacmanLogic(this.pacmanModelMock.Object, this.lvl, true);
            Assert.IsInstanceOf(typeof(PacmanLogic), pacmanLogic);
        }

        /// <summary>
        /// Tests if the ctor creates a PacmanLogic instance for a loaded game.
        /// </summary>
        [Test]
        public void TestConstructorLoadGameOK()
        {
            PacmanLogic pacmanLogic = new PacmanLogic(this.pacmanModelMock.Object, this.lvl, true);
            Assert.IsInstanceOf(typeof(PacmanLogic), pacmanLogic);
        }

        /// <summary>
        /// Tests if the ctor throw a FileNotFoundException if the level file cannot be found.
        /// </summary>
        [Test]
        public void TestConstructorError()
        {
            Assert.Throws<FileNotFoundException>(() => new PacmanLogic(this.pacmanModelMock.Object, "non-existing-file", true));
        }

        /// <summary>
        /// Tests if the NextLevel returns with True when all dots has been eaten.
        /// </summary>
        [Test]
        public void TestNextLevelTrue()
        {
            bool[,] matrix = new bool[19, 33];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = false;
                }
            }

            PacmanLogic pacmanLogic = new PacmanLogic(this.pacmanModelMock.Object, this.lvl, true);
            bool isNextLevel = pacmanLogic.NextLevel(matrix);

            Assert.AreEqual(true, isNextLevel);
        }

        /// <summary>
        /// Tests if the NextLevel returns with False when all dots has not been eaten.
        /// </summary>
        [Test]
        public void TestNextLevelFalse()
        {
            bool[,] matrix = new bool[19, 33];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (i % 2 == 0)
                    {
                        matrix[i, j] = true;
                    }
                    else
                    {
                        matrix[i, j] = false;
                    }
                }
            }

            PacmanLogic pacmanLogic = new PacmanLogic(this.pacmanModelMock.Object, this.lvl, true);
            bool isNextLevel = pacmanLogic.NextLevel(matrix);

            Assert.AreEqual(false, isNextLevel);
        }

        /// <summary>
        /// Tests if Pacman eats heavyDot properly.
        /// </summary>
        [Test]
        public void TestEatHeavyDotsTrue()
        {
            bool[,] matrix = new bool[19, 33];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (i == 2 && j == 2)
                    {
                        matrix[i, j] = true;
                    }
                    else
                    {
                        matrix[i, j] = false;
                    }
                }
            }

            this.pacmanModelMock.Object.Score = 2;
            this.pacmanModelMock.Object.HeavyDot = matrix;
            this.pacmanModelMock.Object.LightDots = new bool[19, 33];

            PacmanLogic pacmanLogic = new PacmanLogic(this.pacmanModelMock.Object, this.lvl, false);

            pacmanLogic.EatDots(2, 2);

            Assert.AreEqual(12, this.pacmanModelMock.Object.Score);
        }

        /// <summary>
        /// Tests if Pacman doesnt eats heavyDot properly.
        /// </summary>
        [Test]
        public void TestEatHeavyDotsFalse()
        {
            bool[,] matrix = new bool[19, 33];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = false;
                }
            }

            this.pacmanModelMock.Object.Score = 2;
            this.pacmanModelMock.Object.HeavyDot = matrix;
            this.pacmanModelMock.Object.LightDots = new bool[19, 33];

            PacmanLogic pacmanLogic = new PacmanLogic(this.pacmanModelMock.Object, this.lvl, false);

            pacmanLogic.EatDots(2, 2);

            Assert.AreEqual(2, this.pacmanModelMock.Object.Score);
        }

        /// <summary>
        /// Tests if Pacman eats lightDot properly.
        /// </summary>
        [Test]
        public void TestEatLightDotsTrue()
        {
            bool[,] matrix = new bool[19, 33];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (i == 2 && j == 2)
                    {
                        matrix[i, j] = true;
                    }
                    else
                    {
                        matrix[i, j] = false;
                    }
                }
            }

            this.pacmanModelMock.Object.Score = 2;
            this.pacmanModelMock.Object.HeavyDot = new bool[19, 33];
            this.pacmanModelMock.Object.LightDots = matrix;

            PacmanLogic pacmanLogic = new PacmanLogic(this.pacmanModelMock.Object, this.lvl, false);

            pacmanLogic.EatDots(2, 2);

            Assert.AreEqual(4, this.pacmanModelMock.Object.Score);
        }

        /// <summary>
        /// Tests if Pacman doesnt eats lightDot properly.
        /// </summary>
        [Test]
        public void TestEatLightDotsFalse()
        {
            bool[,] matrix = new bool[19, 33];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = false;
                }
            }

            this.pacmanModelMock.Object.Score = 2;
            this.pacmanModelMock.Object.HeavyDot = new bool[19, 33];
            this.pacmanModelMock.Object.LightDots = matrix;

            PacmanLogic pacmanLogic = new PacmanLogic(this.pacmanModelMock.Object, this.lvl, false);

            pacmanLogic.EatDots(2, 2);

            Assert.AreEqual(2, this.pacmanModelMock.Object.Score);
        }

        /// <summary>
        /// Tests if MovePacman moves Pacman properly.
        /// </summary>
        [Test]
        public void TestMovePacmanTrue()
        {
            Point expected = new Point(4, 3);
            this.pacmanModelMock.Object.Pacman = new Point(4, 4);
            this.pacmanModelMock.Object.RedGhost = new Point(5, 5);

            PacmanLogic pacmanLogic = new PacmanLogic(this.pacmanModelMock.Object, this.lvl, false);

            pacmanLogic.MovePacman(4, 3);

            Assert.AreEqual(expected, this.pacmanModelMock.Object.Pacman);
        }

        /// <summary>
        /// Tests if MovePacman doesnt move Pacman properly.
        /// </summary>
        [Test]
        public void TestMovePacmanFalse()
        {
            Point old = new Point(4, 4);
            this.pacmanModelMock.Object.Pacman = old;
            this.pacmanModelMock.Object.RedGhost = old;

            PacmanLogic pacmanLogic = new PacmanLogic(this.pacmanModelMock.Object, this.lvl, false);

            pacmanLogic.MovePacman(2, 2);

            Assert.AreEqual(old, this.pacmanModelMock.Object.Pacman);
        }

        /// <summary>
        /// Tests if PacmanDied returns with True when Pacman dies.
        /// </summary>
        [Test]
        public void TestPacmanDidDiedTrue()
        {
            Point expected = new Point(1, 30);
            this.pacmanModelMock.Object.Pacman = new Point(4, 4);
            this.pacmanModelMock.Object.RedGhost = new Point(4, 4);
            this.pacmanModelMock.Object.PacmanLifes = 1;
            PacmanLogic pacmanLogic = new PacmanLogic(this.pacmanModelMock.Object, this.lvl, false);

            bool died = pacmanLogic.PacmanDied();

            Assert.AreEqual(true, died);
            Assert.AreEqual(expected, this.pacmanModelMock.Object.Pacman);
            Assert.AreEqual(0, this.pacmanModelMock.Object.PacmanLifes);
        }

        /// <summary>
        /// Tests if PacmanDied returns with False but Pacman still loses life.
        /// </summary>
        [Test]
        public void TestPacmanDidDiedFalseDidLoseLifeTrue()
        {
            Point expected = new Point(1, 30);
            this.pacmanModelMock.Object.Pacman = new Point(4, 4);
            this.pacmanModelMock.Object.RedGhost = new Point(4, 4);
            this.pacmanModelMock.Object.PacmanLifes = 2;
            PacmanLogic pacmanLogic = new PacmanLogic(this.pacmanModelMock.Object, this.lvl, false);

            bool died = pacmanLogic.PacmanDied();

            Assert.AreEqual(false, died);
            Assert.AreEqual(expected, this.pacmanModelMock.Object.Pacman);
            Assert.AreEqual(1, this.pacmanModelMock.Object.PacmanLifes);
        }

        /// <summary>
        /// Tests if PacmanDied returns with False and Pacman doesnt lose life.
        /// </summary>
        [Test]
        public void TestPacmanDidDiedFalseDidLoseLifeFalse()
        {
            Point expected = new Point(4, 4);
            this.pacmanModelMock.Object.Pacman = new Point(4, 4);
            this.pacmanModelMock.Object.RedGhost = new Point(5, 4);
            this.pacmanModelMock.Object.PacmanLifes = 2;
            PacmanLogic pacmanLogic = new PacmanLogic(this.pacmanModelMock.Object, this.lvl, false);

            bool died = pacmanLogic.PacmanDied();

            Assert.AreEqual(false, died);
            Assert.AreEqual(expected, this.pacmanModelMock.Object.Pacman);
            Assert.AreEqual(2, this.pacmanModelMock.Object.PacmanLifes);
        }

        /// <summary>
        /// Test if Pacman eats Red Ghost properly.
        /// </summary>
        [Test]
        public void TestEatGhost()
        {
            Point expected = new Point(11, 15);
            this.pacmanModelMock.Object.Pacman = new Point(4, 4);
            this.pacmanModelMock.Object.RedGhost = new Point(4, 4);
            this.pacmanModelMock.Object.Level = 1;
            this.pacmanModelMock.Object.Eaten = 0;
            this.pacmanModelMock.Object.Score = 0;
            PacmanLogic pacmanLogic = new PacmanLogic(this.pacmanModelMock.Object, this.lvl, false);

            pacmanLogic.PacmanEatPinkGhost();
            Assert.AreEqual(1, this.pacmanModelMock.Object.Eaten);
            Assert.AreEqual(expected, this.pacmanModelMock.Object.PinkGhost);
            Assert.AreEqual(100, this.pacmanModelMock.Object.Score);
        }

        /// <summary>
        /// Test if Ghost is moving inside of walls.
        /// </summary>
        [Test]
        public void TestMoveDummyGhostDirectly()
        {
            bool[,] matrix = new bool[19, 33];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = false;
                }
            }

            this.pacmanModelMock.Object.Pacman = new Point(4, 4);
            this.pacmanModelMock.Object.Walls = matrix;

            PacmanLogic pacmanLogic = new PacmanLogic(this.pacmanModelMock.Object, this.lvl, false);
            int newX, newY;
            pacmanLogic.MoveGhost(new Point(5, 4), out newX, out newY);

            Assert.AreEqual(4, newX);
            Assert.AreEqual(4, newY);
        }
    }
}