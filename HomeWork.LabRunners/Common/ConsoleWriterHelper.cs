namespace Epam.HomeWork.LabRunners.Common
{
    using System;
    using Epam.HomeWork.Common.IO;

    public static class ConsoleWriterHelper
    {
        /// <summary>
        /// Writes header message with <see cref="IWriter"/>.
        /// If <see cref="IWriter"/> is <see cref="ConsoleWriter"/> then calls method 
        /// <see cref="ConsoleWriter.WriteHeaderMessage(string, ConsoleColor.Yellow, ConsoleColor.Black)"/>
        /// else calls <see cref="IWriter.WriteLine(string)"/>
        /// </summary>
        /// <param name="headerMessage">Header message</param>
        /// <param name="writer"><see cref="IWriter"/> writer</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void WriteHeaderMessage(string headerMessage, IWriter writer)
        {
            if (writer == null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            WriteHeaderMessage(headerMessage, writer, ConsoleColor.Red, Console.BackgroundColor);          
        }

        /// <summary>
        /// Writes header message with <see cref="IWriter"/>.
        /// If <see cref="IWriter"/> is <see cref="ConsoleWriter"/> then calls method 
        /// <see cref="ConsoleWriter.WriteHeaderMessage(string, ConsoleColor, ConsoleColor)"/>
        /// else calls <see cref="IWriter.WriteLine(string)"/>
        /// </summary>
        /// <param name="headerMessage">Header message</param>
        /// <param name="writer"><see cref="IWriter"/> writer</param>
        /// <param name="textColor">Text color</param>
        /// <param name="backgroundColor">Background color</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void WriteHeaderMessage(
            string headerMessage, 
            IWriter writer, 
            ConsoleColor textColor, 
            ConsoleColor backgroundColor)
        {
            if (writer == null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            if (writer is ConsoleWriter)
            {
                ConsoleWriter.WriteHeaderMessage(
                    headerMessage,
                    textColor,
                    backgroundColor);
            }
            else
            {
                writer.WriteLine(headerMessage);
            }
        }
    }
}
