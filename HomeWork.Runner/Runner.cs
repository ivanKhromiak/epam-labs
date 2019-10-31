// <copyright file="Runner.cs" company="Roman Moravskyi">
// Copyright (c) Roman Moravskyi. All rights reserved.
// </copyright>

namespace Epam.HomeWork.Runner
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using CustomLogger;
    using Epam.HomeWork.Common;
    
    /// <summary>
    /// Runner for all labs
    /// </summary>
    public static class Runner
    {
        /// <summary>
        /// Initializes static members of the <see cref="Runner" /> class.
        /// </summary>
        static Runner()
        {
            LoggerName = "LabRunnerLogger";
            LogFilename = Directory.GetCurrentDirectory() + @"\log.txt";
        }

        /// <summary>
        /// Gets or sets Logger Name for this class
        /// </summary>
        public static string LoggerName { get; set; } 

        /// <summary>
        /// Gets or sets filename of log file
        /// </summary>
        public static string LogFilename { get; set; } 

        /// <summary>
        /// Starts the runner
        /// </summary>
        public static void Run()
        {
            var config = new LoggingConfiguration();

            config.AddFileTarget(LogFilename)
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

        /// <summary>
        /// Runs each lab runner referenced in this project
        /// </summary>
        /// <param name="logger">Logger for errors</param>
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

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }

        /// <summary>
        /// Gets all runners referenced in this project
        /// </summary>
        /// <returns>List of runners</returns>
        private static IEnumerable<IConsoleLabRunner> GetLabRunners()
        {
            return ReflectionScanner
                .Scan<IConsoleLabRunner>(SearchOption.AllDirectories)
                .OrderBy(r => r.GetType().Name);
        }
    }
}
