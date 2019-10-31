// <copyright file="IFilesStatisticWriter.cs" company="Roman Moravskyi">
// Copyright (c) Roman Moravskyi. All rights reserved.
// </copyright>

namespace Epam.HomeWork.Lab7Runner
{
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Common interface for files stats writers
    /// </summary>
    public interface IFilesStatisticWriter
    {
        /// <summary>
        /// Writes statistics about files
        /// </summary>
        /// <param name="filenames">List of filenames</param>
        /// <param name="statsName">Statistics name</param>
        /// <param name="directories">List of directories involved</param>
        void WriteFilenameData(IEnumerable<string> filenames, string statsName, IEnumerable<DirectoryInfo> directories);
    }
}
