namespace Epam.HomeWork.Runner
{
    using System;
    using System.Collections.Generic;
    using Epam.HomeWork.Lab1Runner;
    using Epam.HomeWork.Lab2Runner;
    using Epam.HomeWork.Lab3Runner;
    using Epam.HomeWork.Lab4Runner;
    using Epam.HomeWork.Common;
    using NLog;

    public static class Program
    {
        public static void Main()
        { 
            Logger logger = LogManager.GetCurrentClassLogger();

            try
            {               
                RunLabs(logger);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            { 
                LogManager.Shutdown();
            }
        }

        private static void RunLabs(Logger logger)
        {
            foreach (var runner in GetLabRunners())
            {
                ConsoleHelper.WriteHeaderMessage(runner.Description, ConsoleColor.Yellow, ConsoleColor.Black);
                Console.WriteLine();

                runner.RunConsoleLab();

                Console.WriteLine();
                if (!runner.Success)
                {
                    foreach (var error in runner.Errors)
                    {
                        Console.WriteLine($"Error: {error}");
                        logger.Error(error);
                    }
                }
            }
        }

        private static IEnumerable<IConsoleLabRunner> GetLabRunners()
        {
            yield return new Lab1Runner();
            yield return new Lab2Runner();
            yield return new Lab3Runner();
            yield return new Lab4Runner();
        }
    }
}
