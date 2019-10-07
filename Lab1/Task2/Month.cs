namespace Epam.HomeWork.Lab1.Task2
{
    using System;

    /// <summary>
    /// enum for all monthes strating from Jan = 1
    /// </summary>
    public enum Monthes { Jan = 1, Feb, Mar, Apr, May, Jun, Jul, Aug, Sep, Oct, Nov, Dec};

    public static class Month
    {
        /// <summary>
        /// Gets a month from enum Monthes by number
        /// </summary>
        /// <param name="n">month number (1-12)</param>
        /// <returns>returns short name of a month</returns>
        public static string GetMonth(int n)
        {
            if(Enum.IsDefined(typeof(Monthes), n))
            {
                return ((Monthes)n).ToString();
            }
            throw new ArgumentException($"No month that corresponds to value{n}");
        }
    }
}
