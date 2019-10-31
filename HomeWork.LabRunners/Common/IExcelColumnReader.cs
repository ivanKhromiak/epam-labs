// <copyright file="IExcelColumnReader.cs" company="Roman Moravskyi">
// Copyright (c) Roman Moravskyi. All rights reserved.
// </copyright>

namespace Epam.HomeWork.LabRunners.Common
{
    using System.Collections.Generic;

    /// <summary>
    /// Common interface for excel column readers
    /// </summary>
    public interface IExcelColumnReader
    {
        /// <summary>
        /// Gets a range data from specific column
        /// </summary>
        /// <param name="columnName">Name of the column</param>
        /// <param name="startRange">Start index</param>
        /// <param name="inputFileName">File to read from</param>
        /// <returns>List of data</returns>
        IEnumerable<string> GetData(string columnName, int startRange, string inputFileName);
    }
}