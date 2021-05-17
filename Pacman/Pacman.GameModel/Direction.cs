// <copyright file="Direction.cs" company="PlaceholderCompany">
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
    /// Direction in which models can move.
    /// </summary>
    public enum Direction
    {
        /// <summary>
        /// Stop.
        /// </summary>
        Stopped,

        /// <summary>
        /// Left.
        /// </summary>
        Left,

        /// <summary>
        /// Right.
        /// </summary>
        Right,

        /// <summary>
        /// Up.
        /// </summary>
        Up,

        /// <summary>
        /// Down.
        /// </summary>
        Down,
    }
}