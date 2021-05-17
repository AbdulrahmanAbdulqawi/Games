// <copyright file="RandomNumber.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pacman.GameLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Custume Random number generator class.
    /// </summary>
    public static class RandomNumber
    {
        private static readonly RNGCryptoServiceProvider Generator = new RNGCryptoServiceProvider();

        /// <summary>
        /// Method that generates random number between minimum Value and maximum Value.
        /// </summary>
        /// <param name="minimumValue">Minimum Value.</param>
        /// <param name="maximumValue">Maximum Value.</param>
        /// <returns>Random int between minimumValue and maximumValue.</returns>
        public static int Between(int minimumValue, int maximumValue)
        {
            byte[] randomNumber = new byte[1];

            Generator.GetBytes(randomNumber);

            double asciiValueOfRandomCharacter = Convert.ToDouble(randomNumber[0]);

            // We are using Math.Max, and substracting 0.00000000001,
            // to ensure "multiplier" will always be between 0.0 and .99999999999
            // Otherwise, it's possible for it to be "1", which causes problems in our rounding.
            double multiplier = Math.Max(0, (asciiValueOfRandomCharacter / 255d) - 0.00000000001d);

            // We need to add one to the range, to allow for the rounding done with Math.Floor
            int range = maximumValue - minimumValue + 1;

            double randomValueInRange = Math.Floor(multiplier * range);

            return (int)(minimumValue + randomValueInRange);
        }
    }
}