// <copyright file="Config.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pacman.GameModel
{
    using System;
    using System.Windows.Media;

    /// <summary>
    /// Configuration properties.
    /// </summary>
    public static class Config
    {
        // public static Brush borderBrush = Brushes.DarkBlue;

        /// <summary>
        /// Background Brush.
        /// </summary>
        public static readonly Brush BackgroundBrush = Brushes.Black;

        /// <summary>
        /// Dot Brush.
        /// </summary>
        public static readonly Brush DotBrush = Brushes.Gold;

        /// <summary>
        /// Heavy Dot Size.
        /// </summary>
        public static readonly int HeavyDotSize = 12;

        /// <summary>
        /// Light Dot Size.
        /// </summary>
        public static readonly int LightDotSize = 4;
    }
}