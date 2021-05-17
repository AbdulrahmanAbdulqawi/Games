// <copyright file="IPacmanModel.cs" company="PlaceholderCompany">
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
    ///  Defenition of functionalities that <see cref="PacmanModel"/> must implement.
    /// </summary>
    public interface IPacmanModel
    {
        /// <summary>
        /// Trasnform arrays to list for saving and loading via XML.
        /// </summary>
        public void Transform();
    }
}