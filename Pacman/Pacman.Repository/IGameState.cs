// <copyright file="IGameState.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pacman.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Pacman.GameModel;

    /// <summary>
    /// Defenition of functionalities that <see cref="PacmanRepository"/> must implement.
    /// </summary>
    public interface IGameState
    {
        /// <summary>
        /// Loads current game state from xml.
        /// </summary>
        /// <param name="username">User that we want to load game for.</param>
        /// <returns>Pacman model that contains all values of that game.</returns>
        public PacmanModel LoadCurrentState(string username);

        /// <summary>
        /// Saves current game state to xml.
        /// </summary>
        /// <param name="model">PacmanModel that we will fill with values from current game.</param>
        public void SaveCurrentState(PacmanModel model);
    }
}