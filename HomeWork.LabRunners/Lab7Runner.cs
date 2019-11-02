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
    using Epam.HomeWork.Common.IO;
    using Epam.HomeWork.Lab7;
    using Epam.HomeWork.LabRunners.Common;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Runner for Lab7
    /// </summary>
    public class Lab7Runner : ILabRunner
    {
        /// <summary>
        /// App configuration
        /// </summary>
        private readonly IConfigurationRoot configuration;

        /// <summary>
        /// Stats writer
        /// </summary>
        private IStatisticWriter<string> statisticWriter;

        /// <summary>
        /// Initializes a new instance of the <see cref="Lab7Runner"/> class.
        /// </summary>
        public Lab7Runner()
        {
            this.Errors = new List<string>();
            this.Success = false;

            this.configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            this.ConfigureTargetWriter();

            this.Writer = new ConsoleWriter();
            this.Reader = new ConsoleReader();
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
        /// Gets or sets writer
        /// </summary>
        public IWriter Writer { get; set; }

        /// <summary>
        /// Gets or sets reader
        /// </summary>
        public IReader Reader { get; set; }

        /// <summary>
        /// Runs console lab
        /// </summary>
        public void Run()
        {
            if (this.Writer == null)
            {
                throw new NullReferenceException($"{nameof(this.Writer)} value cannot be null");
            }

            if (this.Reader == null)
            {
                throw new NullReferenceException($"{nameof(this.Reader)} value cannot be null");
            }

            this.Success = true;

            try
            {
                this.GetDirectoriesNames(out string firstDirName, out string secondDirName);

                DirectoryInfo firstDir = new DirectoryInfo(firstDirName);
                DirectoryInfo secondDir = new DirectoryInfo(secondDirName);

                this.RunDuplicateFilesTask(firstDir, secondDir, this.statisticWriter);
                this.RunUniqueFilesTask(firstDir, secondDir, this.statisticWriter);

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
                this.Errors.Add($"{e.TargetSite.Name}: {e.Message}");
            }
            catch (PathTooLongException e)
            {
                this.Success = false;
                this.Errors.Add($"{e.TargetSite.Name}: {e.Message}");
            }
            catch (Exception e)
            {
                this.Success = false;
                throw e;
            }

            this.Writer.WriteLine("\t\nPress any key to continue...");
            this.Reader.ReadKey();
        }

        #region Tasks

        /// <summary>
        /// Runs Duplicate Files Task
        /// </summary>
        /// <param name="firstDir">First directory</param>
        /// <param name="secondDir">Second directory</param>
        /// <param name="statisticWriter">Output writer</param>
        private void RunDuplicateFilesTask(
            DirectoryInfo firstDir,
            DirectoryInfo secondDir,
            IStatisticWriter<string> statisticWriter)
        {
            if (firstDir == null)
            {
                throw new ArgumentNullException(nameof(firstDir));
            }

            if (secondDir == null)
            {
                throw new ArgumentNullException(nameof(secondDir));
            }

            if (statisticWriter == null)
            {
                throw new ArgumentNullException(nameof(statisticWriter));
            }

            ConsoleWriterHelper
                .WriteHeaderMessage(
                    "Task 1: Info about duplicate files in two directories:\n",
                    this.Writer);

            var watch = new Stopwatch();

            watch.Start();
            var duplicateFiles = DirectoryComparer.GetDuplicateFiles(firstDir, secondDir);
            watch.Stop();

            this.WriteDuplicateFilesStats(firstDir, secondDir, statisticWriter, duplicateFiles);

            this.Writer.WriteLine($"\tDuplicate files task finished." +
                $" Elapsed time: {watch.ElapsedMilliseconds} ms.\n");
        }

        /// <summary>
        /// Writes statistic about duplicate files
        /// </summary>
        /// <param name="firstDir">First directory</param>
        /// <param name="secondDir">Second directory</param>
        /// <param name="statisticWriter">Output writer</param>
        /// <param name="duplicateFiles">Duplicate files</param>
        private void WriteDuplicateFilesStats(
            DirectoryInfo firstDir,
            DirectoryInfo secondDir,
            IStatisticWriter<string> statisticWriter,
            IEnumerable<string> duplicateFiles)
        {
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

            statisticWriter.WriteData(
                new[] { directoryStatsInfo, fileStatInfo },
                "Duplicate Files");
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

            ConsoleWriterHelper
                .WriteHeaderMessage(
                    "Task 2: Info about unique diles in two directories:\n",
                    this.Writer);

            var watch = new Stopwatch();

            watch.Start();
            var uniqueFiles = DirectoryComparer.GetUniqueFiles(firstDir, secondDir);
            watch.Stop();

            this.WriteUniqueFilesStats(firstDir, secondDir, writer, uniqueFiles);

            this.Writer.WriteLine($"\tUnique files task finished. Elapsed time: {watch.ElapsedMilliseconds} ms.\n");
        }

        /// <summary>
        /// Writes statistic about unique files
        /// </summary>
        /// <param name="firstDir">First directory</param>
        /// <param name="secondDir">Second directory</param>
        /// <param name="statisticWriter">Output writer</param>
        /// <param name="uniqueFiles">Unique files</param>
        private void WriteUniqueFilesStats(
            DirectoryInfo firstDir,
            DirectoryInfo secondDir,
            IStatisticWriter<string> statisticWriter,
            IEnumerable<string> uniqueFiles)
        {
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

            statisticWriter.WriteData(
                new[] { directoryStatsInfo, fileStatInfo },
                "Unique Files");
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
            ConsoleWriterHelper
                .WriteHeaderMessage(
                    "Task 3: Info about unique elements in two excel columns:\n",
                    this.Writer);

            IExcelColumnReader reader = new SyncfusionExcelColumnReader();

            var watch = new Stopwatch();
            watch.Start();

            var firstRange = reader.GetData(firstCol, firstColStartRange, filename);
            var secondRange = reader.GetData(secondCol, secondColStartRange, filename);
            var uniqueElements = EnumerableComparer<string>.GetUnique(firstRange, secondRange);

            this.WriteUniqueColumnsElementsStats(firstCol, secondCol, filename, uniqueElements);

            watch.Stop();

            this.Writer.WriteLine($"]tUnique excel elements task finished. Elapsed time: {watch.ElapsedMilliseconds} ms.\n");
        }

        /// <summary>
        /// Writes stats about unique elements between two columns
        /// </summary>
        /// <param name="firstCol">First column name</param>
        /// <param name="secondCol">Second column name</param>
        /// <param name="filename">Excel filename</param>
        /// <param name="uniqueElements">List of unique elements</param>
        private void WriteUniqueColumnsElementsStats(
            string firstCol,
            string secondCol,
            string filename,
            IEnumerable<string> uniqueElements)
        {
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

            this.statisticWriter.WriteData(new[] { colsStats, elementsStats }, "Unique Elements");
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

                this.statisticWriter = new FilesStatisticsExcelWriter(outputDir, filename);
            }
            else if (this.configuration["Lab7.TargetOutput"] == "Console")
            {
                this.statisticWriter = new FilesStatisticsConsoleWriter();
            }
            else
            {
                throw new LabConfigurationException("Lab7Runner: No target output is not specified.");
            }
        }

        #endregion
    }
}
