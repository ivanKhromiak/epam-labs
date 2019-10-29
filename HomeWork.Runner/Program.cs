namespace Epam.HomeWork.Runner
{
    using System;
    using System.Collections.Generic;
    using Epam.HomeWork.Common;
    using CustomLogger;
    using System.IO;

    //using NLog;

    public static class Program
    {
        private const string LoggerName = "LabRunnerLogger";

        public static string Path { get; } = Directory.GetCurrentDirectory() + @"\log.txt";

        public static void Main()
        {
            Run();
        }

        private static void Run()
        {
            var config = new LoggingConfiguration();

            config.AddFileTarget(Path)
                  .AddConsoleTarget();

            LogManager.Configuration = config;
            Logger logger = LogManager.GetLogger(LoggerName);

            try
            {
                RunLabs(logger);
            }
            catch (Exception ex)
            {
                logger.Trace("Stopped program because of exception", ex);
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
                        logger.LogMessage(error, LogLevel.Error);
                    }
                }
            }
        }

        private static IEnumerable<IConsoleLabRunner> GetLabRunners()
        {
            return ReflectionScanner.Scan<IConsoleLabRunner>();
        }
    }
}
