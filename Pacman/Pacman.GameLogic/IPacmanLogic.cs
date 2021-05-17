// <copyright file="IPacmanLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pacman.GameLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;

    /// <summary>
    /// Defenition of functionalities that <see cref="PacmanLogic"/> must implement.
    /// </summary>
    public interface IPacmanLogic
    {
        /// <summary>
        /// Move model instance.
        /// </summary>
        /// <param name="dx">Direction x coordinate.</param>
        /// <param name="dy">Direction y coordinate.</param>
        /// <returns>Boolean value, to verify the if the operation was succesful.</returns>
        public bool Move(int dx, int dy);

        /// <summary>
        /// Move any ghost.
        /// </summary>
        /// <param name="p">Currnet ghost coordinates.</param>
        /// <param name="newX">New x coordinate.</param>
        /// <param name="newY">New y coordinate.</param>
        public void MoveGhost(Point p, out int newX, out int newY);

        /// <summary>
        /// Move Red Ghost on his specific way.
        /// </summary>
        /// <returns>Boolean value, to verify the if the operation was succesful.</returns>
        public bool MoveRedGhost();

        /// <summary>
        /// Move Blue Ghost on his specific way.
        /// </summary>
        /// <returns>Boolean value, to verify the if the operation was succesful.</returns>
        public bool MoveBlueGhost();

        /// <summary>
        /// Move Yellow Ghost on his specific way.
        /// </summary>
        /// <returns>Boolean value, to verify the if the operation was succesful.</returns>
        public bool MoveYellowGhost();

        /// <summary>
        /// Move Pink Ghost on his specific way.
        /// </summary>
        /// <returns>Boolean value, to verify the if the operation was succesful.</returns>
        public bool MovePinkGhost();

        /// <summary>
        /// Move Pacman.
        /// </summary>
        /// <param name="newX">New x coordinate.</param>
        /// <param name="newY">New y coordinate.</param>
        public void MovePacman(int newX, int newY);

        /// <summary>
        /// Eat dots.
        /// </summary>
        /// <param name="newX">Pacmans new x coordinate, used to check if there is dot on that position.</param>
        /// <param name="newY">Pacmans new y coordinate, used to check if there is dot on that position.</param>
        public void EatDots(int newX, int newY);

        /// <summary>
        /// Checks if the Pacman or Ghosts died.
        /// </summary>
        /// <returns>Boolean value, to verify the if the Pacman losts all of his lifes.</returns>
        public bool DidDie();

        /// <summary>
        /// Handels what happens when Pacman die.
        /// </summary>
        /// <returns>Boolean value, to verify the if the Pacman losts all of his lifes.</returns>
        public bool PacmanDied();

        /// <summary>
        /// Check if level is passed or not.
        /// </summary>
        /// <param name="matrix">Game map.</param>
        /// <returns>Boolean value, to verify if every dot was eaten.</returns>
        public bool NextLevel(bool[,] matrix);

        /// <summary>
        /// Eats the fruits.
        /// </summary>
        /// <param name="newX">Pacmans new x coordinate, used to check if there is fruit on that position.</param>
        /// <param name="newY">Pacmans new y coordinate, used to check if there is fruit on that position.</param>
        public void EatFruits(int newX, int newY);
    }
}