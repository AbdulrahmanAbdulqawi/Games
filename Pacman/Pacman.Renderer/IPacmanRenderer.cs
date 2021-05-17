// <copyright file="IPacmanRenderer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pacman.Renderer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Media;

    /// <summary>
    /// Defenition of functionalities that <see cref="PacmanRenderer"/> must implement.
    /// </summary>
    public interface IPacmanRenderer
    {
        /*/// <summary>
        /// Sets all properties to null.
        /// </summary>
        public void Reset();*/

        /// <summary>
        /// Draw the full game model.
        /// </summary>
        /// <returns>Drawing of Pacman Model.</returns>
        public Drawing BuildDrawing();
    }
}