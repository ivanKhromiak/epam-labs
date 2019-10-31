// <copyright file="Lab7Runner.cs" company="Roman Moravskyi">
// Copyright (c) Roman Moravskyi. All rights reserved.
// </copyright>

namespace Epam.HomeWork.Lab7Runner
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using Epam.HomeWork.Common;
    using Epam.HomeWork.Lab7;
    using Epam.HomeWork.LabRunners.Common;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Runner for Lab7
    /// </summary>
    public class Lab7Runner : IConsoleLabRunner
    {
        /// <summary>
        /// App configuration
        /// </summary>
        private readonly IConfigurationRoot configuration;

        /// <summary>
        /// Stats writer
        /// </summary>
        private IStatisticWriter<string> writer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Lab7Runner" /> class.
        /// </summary>
        public Lab7Runner()
        {
            this.Errors = new List<string>();
            this.Success = false;
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            this.configuration = builder.Build();

            this.ConfigureTargetWriter();
        }

        /// <summary>
        /// Gets description of lab runner 
        /// </summary>
        public string Description => "Lab7: Directory Comparer";

        /// <summary>
        /// Gets list of errors
        /// </summary>
        public IList<string> Errors { get; }

        /// <summary>
        /// Gets a value indicating whether lab run successfully
        /// </summary>
        public bool Success { get; private set; }

        /// <summary>
        /// Runs console lab
        /// </summary>
        public void RunConsoleLab()
        {
            this.Success = true;

            try
            {
                this.GetDirectoriesNames(out string firstDirName, out string secondDirName);

                DirectoryInfo firstDir = new DirectoryInfo(firstDirName);
                DirectoryInfo secondDir = new DirectoryInfo(secondDirName);

                this.RunDuplicateFilesTask(firstDir, secondDir, this.writer);
                this.RunUniqueFilesTask(firstDir, secondDir, this.writer);

                this.GetExcelConfigData(
                    out string firstColName,
                    out string secondColName,
                    out string filename,
                    out int firstColStartRange,
                    out int secondColStartRange);

                this.RunExcelColumnComparerTask(
                    firstColName,
                    secondColName,
                    filename,
                    firstColStartRange,
                    secondColStartRange);
            }
            catch (ArgumentException e)
            {
                this.Success = false;
                this.Errors.Add(e.Message);
            }
            catch (PathTooLongException e)
            {
                this.Success = false;
                this.Errors.Add(e.Message);
            }
        }

        #region Tasks

        /// <summary>
        /// Runs Duplicate Files Task
        /// </summary>
        /// <param name="firstDir">First directory</param>
        /// <param name="secondDir">Second directory</param>
        /// <param name="writer">Output writer</param>
        private void RunDuplicateFilesTask(
            DirectoryInfo firstDir,
            DirectoryInfo secondDir,
            IStatisticWriter<string> writer)
        {
            if (firstDir == null)
            {
                throw new ArgumentNullException(nameof(firstDir));
            }

            if (secondDir == null)
            {
                throw new ArgumentNullException(nameof(secondDir));
            }

            if (writer == null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            ConsoleHelper.WriteHeaderMessage(
                "Task 1: Info about duplicate files in two directories...\n",
                ConsoleColor.Yellow,
                ConsoleColor.Black);

            var watch = new Stopwatch();

            watch.Start();
            var duplicateFiles = DirectoryComparer.GetDuplicateFiles(firstDir, secondDir);
            watch.Stop();

            var fileStatInfo = new StatisticInfo<string>
            {
                Name = "Duplicate files",
                Data = duplicateFiles,
                DataItemName = "Filename"
            };

            var directoryStatsInfo = new StatisticInfo<string>
            {
                Name = "Directories involved",
                Data = new[] { firstDir.FullName, secondDir.FullName },
                DataItemName = "Directory"
            };

            writer.WriteData(
                new[] { directoryStatsInfo, fileStatInfo },
                "Duplicate Files");

            Console.WriteLine($"Duplicate files task finished. Elapsed time: {watch.ElapsedMilliseconds} ms.\n");
        }

        /// <summary>
        /// Runs Unique Files Task
        /// </summary>
        /// <param name="firstDir">First directory</param>
        /// <param name="secondDir">Second directory</param>
        /// <param name="writer">Output writer</param>
        private void RunUniqueFilesTask(
            DirectoryInfo firstDir,
            DirectoryInfo secondDir,
            IStatisticWriter<string> writer)
        {
            if (firstDir == null)
            {
                throw new ArgumentNullException(nameof(firstDir));
            }

            if (secondDir == null)
            {
                throw new ArgumentNullException(nameof(secondDir));
            }

            if (writer == null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            ConsoleHelper.WriteHeaderMessage(
                "Task 2: Info about unique diles in two directories...\n",
                ConsoleColor.Yellow,
                ConsoleColor.Black);

            var watch = new Stopwatch();

            watch.Start();
            var uniqueFiles = DirectoryComparer.GetUniqueFiles(firstDir, secondDir);
            watch.Stop();

            var fileStatInfo = new StatisticInfo<string>
            {
                Name = "Unique files",
                Data = uniqueFiles,
                DataItemName = "Filename"
            };

            var directoryStatsInfo = new StatisticInfo<string>
            {
                Name = "Directories involved",
                Data = new[] { firstDir.FullName, secondDir.FullName },
                DataItemName = "Directory"
            };

            writer.WriteData(
                new[] { directoryStatsInfo, fileStatInfo },
                "Unique Files");

            Console.WriteLine($"Unique files task finished. Elapsed time: {watch.ElapsedMilliseconds} ms.\n");
        }

        /// <summary>
        /// Runs an Excel Column Comparer Task
        /// </summary>
        /// <param name="firstCol">Name of first column</param>
        /// <param name="secondCol">Name of second column</param>
        /// <param name="filename">Name of input file</param>
        /// <param name="firstColStartRange">First column start range</param>
        /// <param name="secondColStartRange">Second column start range</param>
        private void RunExcelColumnComparerTask(
            string firstCol,
            string secondCol,
            string filename,
            int firstColStartRange,
            int secondColStartRange)
        {
            ConsoleHelper.WriteHeaderMessage(
                "Task 3: Info about unique elements in two excel columns...\n",
                ConsoleColor.Yellow,
                ConsoleColor.Black);

            IExcelColumnReader reader = new SyncfusionExcelColumnReader();
            var watch = new Stopwatch();

            watch.Start();

            var firstRange = reader.GetData(firstCol, firstColStartRange, filename);
            var secondRange = reader.GetData(secondCol, secondColStartRange, filename);

            var uniqueElements = EnumerableComparer<string>.GetUnique(firstRange, secondRange);

            var elementsStats = new StatisticInfo<string>
            {
                Name = "Unique excel column elements",
                Data = uniqueElements,
                DataItemName = "Element"
            };

            var colsStats = new StatisticInfo<string>
            {
                Name = $"Columns of {filename}",
                Data = new[] { firstCol, secondCol },
                DataItemName = "Column"
            };

            this.writer.WriteData(new[] { colsStats, elementsStats }, "Unique Elements");

            watch.Stop();

            Console.WriteLine($"Unique excel elements task finished. Elapsed time: {watch.ElapsedMilliseconds} ms.\n");
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Gets directories names from configuration
        /// </summary>
        /// <param name="firstDirName">First directory name</param>
        /// <param name="secondDirName">Second directory name</param>
        private void GetDirectoriesNames(out string firstDirName, out string secondDirName)
        {
            firstDirName = this.configuration["Lab7.FirstDirectoryName"];
            secondDirName = this.configuration["Lab7.SecondDirectoryName"];

            if (string.IsNullOrWhiteSpace(firstDirName) || !Directory.Exists(firstDirName))
            {
                throw new LabConfigurationException("Lab7Runner: FirstDirectoryName is not " +
                    "specified or directory does not exist!");
            }

            if (string.IsNullOrWhiteSpace(secondDirName) || !Directory.Exists(secondDirName))
            {
                throw new LabConfigurationException("Lab7Runner: SecondDirectoryName is not " +
                    "specified or directory does not exist!");
            }
        }

        /// <summary>
        /// Gets data from config
        /// </summary>
        /// <param name="firstColName">Name of first column</param>
        /// <param name="secondColName">Name of second column</param>
        /// <param name="filename">Name of input file</param>
        /// <param name="firstColStartRange">First column start range</param>
        /// <param name="secondColStartRange">Second column start range</param>
        private void GetExcelConfigData(
            out string firstColName,
            out string secondColName,
            out string filename,
            out int firstColStartRange,
            out int secondColStartRange)
        {
            filename = this.configuration["Lab7.InputExcelFile"];
            firstColName = this.configuration["Lab7.FirstColumn"];
            secondColName = this.configuration["Lab7.SecondColumn"];

            if (!int.TryParse(this.configuration["Lab7.FirstColumnStartRange"], out firstColStartRange))
            {
                throw new LabConfigurationException("Lab7.FirstColumnStartRange does not exist.");
            }

            if (!int.TryParse(this.configuration["Lab7.SecondColumnStartRange"], out secondColStartRange))
            {
                throw new LabConfigurationException("Lab7.SecondColumnStartRange does not exist.");
            }

            if (string.IsNullOrWhiteSpace(firstColName))
            {
                throw new LabConfigurationException("Lab7.FirstColumn does not exist.");
            }

            if (string.IsNullOrWhiteSpace(secondColName))
            {
                throw new LabConfigurationException("Lab7.SecondColumn does not exist.");
            }

            if (string.IsNullOrWhiteSpace(filename))
            {
                throw new LabConfigurationException("Lab7.InputExcelFile does not exist.");
            }

            if (!File.Exists(filename))
            {
                throw new LabConfigurationException("Lab7.InputExcelFile does not exist.");
            }
        }

        /// <summary>
        /// Configures writer
        /// </summary>
        private void ConfigureTargetWriter()
        {
            if (this.configuration["Lab7.TargetOutput"] == "File")
            {
                string outputDir = this.configuration["Lab7.TargertOutputDirectory"];
                string filename = this.configuration["Lab7.OutputFilename"];

                if (string.IsNullOrWhiteSpace(outputDir) || !Directory.Exists(outputDir))
                {
                    throw new LabConfigurationException("Lab7Runner: Output directory is not specified or does not exist.");
                }

                if (string.IsNullOrWhiteSpace(filename))
                {
                    throw new LabConfigurationException("Lab7Runner: Output filename is not specified.");
                }

                this.writer = new FilesStatisticsExcelWriter(outputDir, filename);
            }
            else if (this.configuration["Lab7.TargetOutput"] == "Console")
            {
                this.writer = new FilesStatisticsConsoleWriter();
            }
            else
            {
                throw new LabConfigurationException("Lab7Runner: No target output is not specified.");
            }
        }

        #endregion
    }
}
