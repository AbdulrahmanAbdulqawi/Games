// <copyright file="IGameScoreboard.cs" company="PlaceholderCompany">
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
    public interface IGameScoreboard
    {
        /// <summary>
        /// Loads scores from xml.
        /// </summary>
        /// <returns>List of users.</returns>
        public List<User> LoadBestScore();

        /// <summary>
        /// Saves scores into xml.
        /// </summary>
        /// <param name="user">Current user that is playing.</param>
        public void SaveBestScore(User user);
    }
}