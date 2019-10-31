// <copyright file="EnumerableComparer.cs" company="Roman Moravskyi">
// Copyright (c) Roman Moravskyi. All rights reserved.
// </copyright>

namespace Epam.HomeWork.Lab7
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Compares two enumerables of T
    /// </summary>
    /// <typeparam name="T">Enumerable type</typeparam>
    public static class EnumerableComparer<T>
    {
        /// <summary>
        /// Gets unique elemets between two collections
        /// </summary>
        /// <param name="first">First collection</param>
        /// <param name="second">Second collection</param>
        /// <returns>IEnumerable of <see cref="T"/></returns>
        public static IEnumerable<T> GetUnique(IEnumerable<T> first, IEnumerable<T> second)
        {
            if (first == null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second == null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            var firstSet = new HashSet<T>(first);
            var secondSet = new HashSet<T>(second);

            firstSet.SymmetricExceptWith(secondSet);

            return firstSet;
        }

        /// <summary>
        /// Gets duplicate elemets between two collections
        /// </summary>
        /// <param name="first">First collection</param>
        /// <param name="second">Second collection</param>
        /// <returns>IEnumerable of <see cref="T"/></returns>
        public static IEnumerable<T> GetDuplicate(IEnumerable<T> first, IEnumerable<T> second)
        {
            if (first == null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second == null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            return first.Intersect(second);
        }
    }
}
