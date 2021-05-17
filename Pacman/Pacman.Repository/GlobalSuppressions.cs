// <copyright file="GlobalSuppressions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Design", "CA1002:Do not expose generic lists", Justification = "The list will not be inherited.", Scope = "member", Target = "~M:Pacman.Repository.IGameScoreboard.LoadBestScore~System.Collections.Generic.List{Pacman.GameModel.User}")]
[assembly: SuppressMessage("Security", "CA5369:Use XmlReader For Deserialize", Justification = "No need.", Scope = "member", Target = "~M:Pacman.Repository.PacmanRepository.LoadCurrentState(System.String)~Pacman.GameModel.PacmanModel")]
[assembly: SuppressMessage("Security", "CA5369:Use XmlReader For Deserialize", Justification = "No need.", Scope = "member", Target = "~M:Pacman.Repository.PacmanRepository.LoadBestScore~System.Collections.Generic.List{Pacman.GameModel.User}")]