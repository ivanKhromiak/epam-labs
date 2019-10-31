// <copyright file="FilesStatisticsConsoleWriter.cs" company="Roman Moravskyi">
// Copyright (c) Roman Moravskyi. All rights reserved.
// </copyright>

namespace Epam.HomeWork.Lab7Runner
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Epam.HomeWork.Common;

    /// <summary>
    /// Used to write files stats into console
    /// </summary>
    public class FilesStatisticsConsoleWriter : IFilesStatisticWriter
    {
        /// <summary>
        /// Writes statistics into console
        /// </summary>
        /// <param name="filenames">List of filenames</param>
        /// <param name="statsName">Statistics name</param>
        /// <param name="directories">List of directories involved</param>
        public void WriteFilenameData(IEnumerable<string> filenames, string statsName, IEnumerable<DirectoryInfo> directories)
        {
            ConsoleHelper.WriteHeaderMessage(
                $"{statsName}, count: {filenames.Count()}",
                ConsoleColor.Magenta, 
                ConsoleColor.Black);

            Console.WriteLine($"Directories count: {directories.Count()}");

            foreach (var dir in directories)
            {
                Console.WriteLine($"Directory: {dir}");
            }
            
            foreach (var item in filenames)
            {
                Console.WriteLine($"Filename: {item}");
            }
        }
    }
}
