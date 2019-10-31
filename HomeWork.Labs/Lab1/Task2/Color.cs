namespace Epam.HomeWork.Lab1.Task2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// enum for Colors with random values due to task
    /// </summary>
    public enum Colors
    {
        Red = 7,
        Green = 420,
        Cyan = 9,
        Magenta = 89,
        Yellow = 8
    };

    /// <summary>
    /// Extension class for enum Colors
    /// </summary>
    public static class ColorsExtension
    {
        /// <summary>
        /// Gets all possible colors from enum Colors
        /// </summary>
        /// <param name="color">enum Color var</param>
        /// <returns>IEnumarable of strings formated like 'Green=420'</returns>
        public static IEnumerable<string> GetAllColors(this Colors color)
        {
            return Enum.GetValues(typeof(Colors))
                .Cast<int>()
                .OrderBy(val => val)
                .Select(val => $"{(Colors)val}={val}");
        }
    }
}
