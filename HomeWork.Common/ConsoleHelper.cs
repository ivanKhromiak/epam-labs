namespace Epam.HomeWork.Common
{
    using System;

    public static class ConsoleHelper
    {
        private static void WriteHeaderMessage(string message, ConsoleColor textColor)
        {
            var consoleForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = textColor;
            foreach(char ch in message)
            {
                Console.Write(ch);
            }
            Console.ForegroundColor = consoleForegroundColor;
        }

        public static void WriteHeaderMessage(string message, ConsoleColor textColor, ConsoleColor backgroundColor)
        {
            var consoleBackgroundColor = Console.BackgroundColor;
            Console.BackgroundColor = backgroundColor;
            WriteHeaderMessage(message, textColor);
            Console.BackgroundColor = consoleBackgroundColor;
            Console.Write('\n');
        }
    }
}
