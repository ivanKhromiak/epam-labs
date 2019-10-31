// <copyright file="ReflectionScanner.cs" company="Roman Moravskyi">
// Copyright (c) Roman Moravskyi. All rights reserved.
// </copyright>

namespace Epam.HomeWork.Runner
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Scans for all types in current project
    /// </summary>
    public class ReflectionScanner
    {
        /// <summary>
        /// Base directory 
        /// </summary>
        private static readonly string BaseDir = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// Gets list of created instances of Type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">Type to search</typeparam>
        /// <param name="option">Search options</param>
        /// <returns>List of instances of Type <typeparamref name="T"/></returns>
        public static List<T> Scan<T>(SearchOption option)
        {
            var result = new List<T>();
            var filePaths = ScanLibs(option);

            foreach (var filePath in filePaths)
            {
                var a = Assembly.LoadFrom(filePath);
                var allTypes = a.GetTypes();

                var typeslist = allTypes.Where(p =>
                {
                    return typeof(T).IsAssignableFrom(p)
                    && p.Name != typeof(T).Name;
                });

                foreach (var item in typeslist)
                {
                    var newInstance = (T)a.CreateInstance(item.ToString());

                    if (newInstance == null)
                    {
                        throw new Exception($"Could not initialize instance in dll: {filePath}");
                    }

                    result.Add(newInstance);
                }
            }

            return result;
        }

        /// <summary>
        /// Scans for libraries in <see cref="BaseDir"/>
        /// </summary>
        /// <param name="option">Search options</param>
        /// <returns>Array of directories names</returns>
        private static string[] ScanLibs(SearchOption option)
        {
            return Directory.GetFiles(BaseDir, "*.dll", option);
        }
    }
}
