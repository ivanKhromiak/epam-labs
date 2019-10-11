namespace Epam.HomeWork.Runner
{
    using System;
    using System.Collections.Generic;
    using Epam.HomeWork.Lab1Runner;
    using Epam.HomeWork.Lab2Runner;
    using Epam.HomeWork.Common;

    public static class Program
    {
        public static void Main()
        {
            foreach(var runner in GetLabRunners())
            {
                ConsoleHelper.WriteHeaderMessage(runner.Description, ConsoleColor.Yellow, ConsoleColor.Black);
                Console.WriteLine();

                runner.RunConsoleLab();

                Console.WriteLine();
                if(!runner.Succes)
                {
                    foreach(var error in runner.Errors)
                    {
                        ConsoleHelper.WriteHeaderMessage(error, ConsoleColor.Yellow, ConsoleColor.Black);
                    }
                }
                Console.Clear();
            }
        }

        private static IEnumerable<IConsoleLabRunner> GetLabRunners()
        {
            yield return new Lab1Runner();
            yield return new Lab2Runner();
        }
    }
}
