// <copyright file="PacmanRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pacman.Repository
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using Pacman.GameModel;

    /// <summary>
    /// Repository methods for saving and loading game and score.
    /// </summary>
    public class PacmanRepository : IGameState, IGameScoreboard
    {
        private string path = "../../../../../";

        /// <inheritdoc/>
        public PacmanModel LoadCurrentState(string username)
        {
            PacmanModel game;
            XmlSerializer deserializer = new XmlSerializer(typeof(PacmanModel));
            using (TextReader reader = new StreamReader(this.path + $"{username}State.xml"))
            {
                object obj = deserializer.Deserialize(reader);
                game = (PacmanModel)obj;
            }

            return game;
        }

        /// <inheritdoc/>
        public void SaveCurrentState(PacmanModel model)
        {
            XmlSerializer serializer2 = new XmlSerializer(typeof(PacmanModel));
            if (model != null)
            {
                using (TextWriter textWriter = new StreamWriter(this.path + $"{model.User.Username}State.xml"))
                {
                    serializer2.Serialize(textWriter, model);
                }
            }
        }

        /// <inheritdoc/>
        public List<User> LoadBestScore()
        {
            List<User> currentBest;
            XmlSerializer deserializer = new XmlSerializer(typeof(List<User>));
            using (TextReader reader = new StreamReader(this.path + "highScore.xml"))
            {
                object obj = deserializer.Deserialize(reader);
                currentBest = (List<User>)obj;
            }

            return currentBest;
        }

        /// <inheritdoc/>
        public void SaveBestScore(User user)
        {
            bool found = false;
            List<User> currentScores = this.LoadBestScore();
            foreach (User item in currentScores)
            {
                if (user != null)
                {
                    if (item.Username == user.Username)
                    {
                        item.Score = user.Score;
                        found = true;
                    }
                }
            }

            if (!found)
            {
                currentScores.Add(user);
            }

            currentScores = currentScores.OrderByDescending(x => x.Score).ToList();

            XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
            using (TextWriter textWriter = new StreamWriter(this.path + "highScore.xml"))
            {
                serializer.Serialize(textWriter, currentScores);
            }
        }
    }
}