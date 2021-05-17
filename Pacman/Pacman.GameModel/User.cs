// <copyright file="User.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pacman.GameModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// User class.
    /// </summary>
    [Serializable]
    public class User
    {
        private string username;
        private int score;

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="username">User Name.</param>
        /// <param name="score">Current score.</param>
        public User(string username, int score)
        {
            this.username = username;
            this.score = score;
        }

        /// <summary>
        /// Gets or sets username.
        /// </summary>
        public string Username { get => this.username; set => this.username = value; }

        /// <summary>
        /// Gets or sets score.
        /// </summary>
        public int Score { get => this.score; set => this.score = value; }
    }
}