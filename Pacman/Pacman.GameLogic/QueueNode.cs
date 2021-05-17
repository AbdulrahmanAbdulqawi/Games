// <copyright file="QueueNode.cs" company="PlaceholderCompany">
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
    /// Data Structure for queue used in ShortesPath class.
    /// </summary>
    public class QueueNode
    {
        private List<Point> pt;

        // cell's distance of from the source
        private int dist;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueNode"/> class.
        /// </summary>
        /// <param name="pt">List of coordinats.</param>
        /// <param name="dist">Distance.</param>
        public QueueNode(List<Point> pt, int dist)
        {
            this.pt = pt;
            this.dist = dist;
        }

        /// <summary>
        /// Gets or sets pt.
        /// </summary>
        public List<Point> Pt { get => this.pt; set => this.pt = value; }

        /// <summary>
        /// Gets or sets dist.
        /// </summary>
        public int Dist { get => this.dist; set => this.dist = value; }
    }
}