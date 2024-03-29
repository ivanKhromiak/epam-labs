﻿// <copyright file="FilesStatisticsConsoleWriter.cs" company="Roman Moravskyi">
// Copyright (c) Roman Moravskyi. All rights reserved.
// </copyright>

namespace Epam.HomeWork.LabRunners.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Epam.HomeWork.Common;
    using Epam.HomeWork.Common.IO;

    /// <summary>
    /// Used to write files stats into console
    /// </summary>
    public class FilesStatisticsConsoleWriter : IStatisticWriter<string>
    {
        /// <summary>
        /// Console writer
        /// </summary>
        private readonly ConsoleWriter writer = new ConsoleWriter();

        /// <summary>
        /// Writes statistics into console
        /// </summary>
        /// <param name="statistics">List of statistics</param>
        /// <param name="statsName">Statistics name</param>
        public void WriteData(IEnumerable<StatisticInfo<string>> statistics, string statsName)
        {
            foreach (var stat in statistics)
            {
                ConsoleWriterHelper.WriteHeaderMessage($"{stat.Name}, count: {stat.Data.Count()}", 
                    writer, 
                    ConsoleColor.Magenta, 
                    ConsoleColor.Black);
             
                foreach (var item in stat.Data)
                {
                    Console.WriteLine($"{stat.DataItemName}: {item}");
                }
            }
        }
    }
}