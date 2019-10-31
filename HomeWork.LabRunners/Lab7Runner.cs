// <copyright file="Lab7Runner.cs" company="Roman Moravskyi">
// Copyright (c) Roman Moravskyi. All rights reserved.
// </copyright>

namespace Epam.HomeWork.Lab7Runner
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Epam.HomeWork.Common;
    using Epam.HomeWork.Lab7;
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
        private IFilesStatisticWriter writer;

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

        /// <summary>
        /// Gets directories names from configuration
        /// </summary>
        /// <param name="firstDirName">First directory name</param>
        /// <param name="secondDirName">Second directory name</param>
        private void GetDirectoriesNames(out string firstDirName, out string secondDirName)
        {
            firstDirName = this.configuration["FirstDirectoryName"];
            secondDirName = this.configuration["SecondDirectoryName"];

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
        /// Runs Duplicate Files Task
        /// </summary>
        /// <param name="firstDir">First directory</param>
        /// <param name="secondDir">Second directory</param>
        /// <param name="writer">Output writer</param>
        private void RunDuplicateFilesTask(
            DirectoryInfo firstDir,
            DirectoryInfo secondDir,
            IFilesStatisticWriter writer)
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

            var duplicateFiles = DirectoryComparer.GetDuplicateFiles(firstDir, secondDir);

            writer.WriteFilenameData(
                duplicateFiles, 
                "Duplicate files", 
                new List<DirectoryInfo> { firstDir, secondDir });

            Console.WriteLine("Duplicate files task ended.");
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
            IFilesStatisticWriter writer)
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

            var uniqueFiles = DirectoryComparer.GetUniqueFiles(firstDir, secondDir);

            writer.WriteFilenameData(
                uniqueFiles,
                "Unique files",
                new List<DirectoryInfo> { firstDir, secondDir });

            Console.WriteLine("Unique files task ended.");
        }

        /// <summary>
        /// Configures writer
        /// </summary>
        private void ConfigureTargetWriter()
        {
            if (this.configuration["TargetOutput"] == "File")
            {
                string outputDir = this.configuration["TargertOutputDirectory"];
                string filename = this.configuration["OutputFilename"];

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
            else if (this.configuration["TargetOutput"] == "Console")
            {
                this.writer = new FilesStatisticsConsoleWriter();
            }
            else
            {
                throw new LabConfigurationException("Lab7Runner: No target output is not specified.");
            }
        }
    }
}
