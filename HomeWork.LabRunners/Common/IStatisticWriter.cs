// <copyright file="IStatisticWriter.cs" company="Roman Moravskyi">
// Copyright (c) Roman Moravskyi. All rights reserved.
// </copyright>

namespace Epam.HomeWork.LabRunners.Common
{
    using System.Collections.Generic;

    /// <summary>
    /// Common interface for stats writers
    /// </summary>
    /// <typeparam name="T">Stats data type</typeparam>
    public interface IStatisticWriter<T>
    {
        /// <summary>
        /// Writes statistics about files
        /// </summary>
        /// <param name="statistics">List of <see cref="StatisticInfo{T}"/></param>
        /// <param name="statsName">Name of the statistics/></param>
        void WriteData(IEnumerable<StatisticInfo<T>> statistics, string statsName);
    }
}
