// <copyright file="StatisticInfo.cs" company="Roman Moravskyi">
// Copyright (c) Roman Moravskyi. All rights reserved.
// </copyright>

namespace Epam.HomeWork.LabRunners.Common
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents statistics info
    /// </summary>
    /// <typeparam name="T">Data type</typeparam>
    public class StatisticInfo<T>
    {
        /// <summary>
        /// Gets or sets name of the statistics
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets statistic data
        /// </summary>
        public IEnumerable<T> Data { get; set; }

        /// <summary>
        /// Gets or sets name of the data item
        /// </summary>
        public string DataItemName { get; set; }
    }
}
