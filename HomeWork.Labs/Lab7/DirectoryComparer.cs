// <copyright file="DirectoryComparer.cs" company="Roman Moravskyi">
// Copyright (c) Roman Moravskyi. All rights reserved.
// </copyright>

namespace Epam.HomeWork.Lab7
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Static class for comparing two directories
    /// </summary>
    public static class DirectoryComparer
    {
        /// <summary>
        /// Returns filenames that have same name and exist in both directories
        /// </summary>
        /// <param name="firstDirectory">First directory info</param>
        /// <param name="secondDirectory">Second directory info</param>
        /// <returns>List of strings</returns>
        public static IEnumerable<string> GetDuplicateFiles(DirectoryInfo firstDirectory, DirectoryInfo secondDirectory)
        {
            if (firstDirectory == null)
            {
                throw new System.ArgumentNullException(nameof(firstDirectory));
            }

            if (secondDirectory == null)
            {
                throw new System.ArgumentNullException(nameof(secondDirectory));
            }

            var firstDirFilenames = firstDirectory
                .EnumerateFiles("*", SearchOption.AllDirectories)
                .Select(fi => fi.Name);

            var secondDirFilenames = secondDirectory
                .EnumerateFiles("*", SearchOption.AllDirectories)
                .Select(fi => fi.Name);

            return EnumerableComparer<string>.GetDuplicate(firstDirFilenames, secondDirFilenames);
        }

        /// <summary>
        /// Returns filenames that are unique for two directories
        /// </summary>
        /// <param name="firstDirectory"></param>
        /// <param name="secondDirectory"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetUniqueFiles(DirectoryInfo firstDirectory, DirectoryInfo secondDirectory)
        {
            if (firstDirectory == null)
            {
                throw new System.ArgumentNullException(nameof(firstDirectory));
            }

            if (secondDirectory == null)
            {
                throw new System.ArgumentNullException(nameof(secondDirectory));
            }

            var firstDirFilenames = firstDirectory
                .EnumerateFiles("*", SearchOption.AllDirectories)
                .Select(fi => fi.Name);

            var secondDirFilenames = secondDirectory
                .EnumerateFiles("*", SearchOption.AllDirectories)
                .Select(fi => fi.Name);
            
            return EnumerableComparer<string>.GetUnique(firstDirFilenames, secondDirFilenames);
        }
    }
}
