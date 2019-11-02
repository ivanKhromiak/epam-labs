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
    using Epam.HomeWork.Common.IO;
    using Epam.HomeWork.LabRunners.Common;

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
            Writer = new ConsoleWriter();
            Reader = new ConsoleReader();
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
        /// Console writer
        /// </summary>
        public static ConsoleWriter Writer { get; }

        /// <summary>
        /// Console reader
        /// </summary>
        public static ConsoleReader Reader { get; }

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
                ConsoleWriterHelper
                    .WriteHeaderMessage(
                        runner.Description, 
                        Writer, 
                        ConsoleColor.Yellow,
                        ConsoleColor.Black);

                Writer.WriteLine(string.Empty);

                runner.Run();

                Console.WriteLine();
                if (!runner.Success)
                {
                    foreach (var error in runner.Errors)
                    {
                        Writer.WriteLine($"Error: {error}");
                        logger.LogMessage(error);
                    }
                }
            }

            Writer.WriteLine("Press any key to exit...");
            Reader.ReadLine();
        }

        /// <summary>
        /// Gets all runners referenced in this project
        /// </summary>
        /// <returns>List of runners</returns>
        private static IEnumerable<ILabRunner> GetLabRunners()
        {
            return ReflectionScanner
                .Scan<ILabRunner>(SearchOption.AllDirectories)
                .OrderBy(r => r.GetType().Name);
        }
    }
}
