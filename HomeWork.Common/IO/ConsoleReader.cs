// <copyright file="ConsoleWriter.cs" company="Roman Moravskyi">
// Copyright (c) Roman Moravskyi. All rights reserved.
// </copyright>

namespace Epam.HomeWork.Common.IO
{
    using System;

    /// <summary>
    /// Class for reading from standard input stream.
    /// </summary>
    public class ConsoleReader : IReader
    {
        /// <summary>
        /// Reads the next character from standard input stream.
        /// </summary>
        /// <returns><see cref="int"/> value representing character</returns>
        /// <exception cref="System.IO.IOException"></exception>
        public ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey();
        }

        /// <summary>
        /// Reads the next line of characters from standard input stream.
        /// </summary>
        /// <returns>Line of characters</returns>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="OutOfMemoryException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        /// <summary>
        /// Tries to convert input string from standard input to <see cref="int"/> representation.
        /// </summary>
        /// <param name="input">Result variable</param>
        /// <returns>Value indicating weather conversion succeeded.</returns>
        public bool TryRead(out int input)
        {
            return int.TryParse(this.ReadLine(), out input);
        }

        /// <summary>
        /// Tries to convert input string from standard input to <see cref="long"/> representation.
        /// </summary>
        /// <param name="input">Result variable</param>
        /// <returns>Value indicating weather conversion succeeded.</returns>
        public bool TryRead(out long input)
        {
            return long.TryParse(this.ReadLine(), out input);
        }

        /// <summary>
        /// Tries to convert input string from standard input to <see cref="double"/> representation.
        /// </summary>
        /// <param name="input">Result variable</param>
        /// <returns>Value indicating weather conversion succeeded.</returns>
        public bool TryRead(out double input)
        {
            return double.TryParse(this.ReadLine(), out input);
        }

        /// <summary>
        /// Tries to convert input string from standard input to <see cref="float"/> representation.
        /// </summary>
        /// <param name="input">Result variable</param>
        /// <returns>Value indicating weather conversion succeeded.</returns>
        public bool TryRead(out float input)
        {
            return float.TryParse(this.ReadLine(), out input);
        }
    }
}
