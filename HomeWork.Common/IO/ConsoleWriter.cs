// <copyright file="ConsoleWriter.cs" company="Roman Moravskyi">
// Copyright (c) Roman Moravskyi. All rights reserved.
// </copyright>

namespace Epam.HomeWork.Common.IO
{
    using System;

    /// <summary>
    /// Class for writing to standart output stream.
    /// </summary>
    public class ConsoleWriter : IWriter
    {
        /// <summary>
        /// Writes message, followed by a current line terminator, to the standard output stream.
        /// </summary>
        /// <param name="message">Output message</param>
        /// <exception cref="System.IO.IOException"></exception>
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Writes message to the standard output stream.
        /// </summary>
        /// <param name="message">Output message</param>
        /// <exception cref="System.IO.IOException"></exception>
        public void Write(string message)
        {
            Console.Write(message);
        }

        /// <summary>
        /// Writes <see cref="char"/> to the standard output stream.
        /// </summary>
        /// <param name="ch">Output character</param>
        /// <exception cref="System.IO.IOException"></exception>
        public void Write(char ch)
        {
            Console.Write(ch);
        }

        /// <summary>
        /// Writes message to the standard output stream with specified fore- and background color.
        /// </summary>
        /// <param name="message">Output message</param>
        /// <param name="textColor">Foreground color</param>
        /// <param name="backgroundColor">Background color</param>
        public static void WriteHeaderMessage(string message, ConsoleColor textColor, ConsoleColor backgroundColor)
        {
            var consoleBackgroundColor = Console.BackgroundColor;
            Console.BackgroundColor = backgroundColor;

            WriteHeaderMessage(message, textColor);

            Console.BackgroundColor = consoleBackgroundColor;
            Console.Write('\n');
        }

        /// <summary>
        /// Writes message to the standard output stream with specified foreground color.
        /// </summary>
        /// <param name="message">Output message</param>
        /// <param name="textColor">Foreground color</param>
        private static void WriteHeaderMessage(string message, ConsoleColor textColor)
        {
            var consoleForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = textColor;

            foreach (char ch in message)
            {
                Console.Write(ch);
            }

            Console.ForegroundColor = consoleForegroundColor;
        }
    }
}
