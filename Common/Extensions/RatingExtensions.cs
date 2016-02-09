using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions
{
    public static class RatingExtensions
    {
        /// <summary>
        /// Round to 2 digits percentage decimal
        /// </summary>
        /// <param name="input">Input decimal rating data</param>
        /// <returns>2 decimals number</returns>
        public static decimal RoundPercentage(this decimal input)
        {
            return Math.Round(input*100, 2, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// This extension returns integer rating number to more meaningful
        /// name. It only convert integer number not extra string name.
        /// </summary>
        /// <param name="input">Api returned rating name</param>
        /// <returns>User friendly ratning name</returns>
        public static string GetStarName(this string input)
        {
            int rate;
            var isNumeric = int.TryParse(input, out rate);
            return isNumeric ? $"{input} - Star" : input;
        }
    }
}
