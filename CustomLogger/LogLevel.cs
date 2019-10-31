// <copyright file="LogLevel.cs" company="Roman Moravskyi">
// Copyright (c) Roman Moravskyi. All rights reserved.
// </copyright>

namespace CustomLogger
{
    using System;

    /// <summary>
    /// Level of Log Message: Trace - highest, Info - lowest
    /// </summary>
    public class LogLevel
    {
        /// <summary>
        /// Initializes static members of the <see cref="LogLevel" /> class.
        /// </summary>
        static LogLevel()
        {
            Info = new LogLevel(0);
            Warn = new LogLevel(Info.Level + 1);
            Error = new LogLevel(Warn.Level + 1);
            Fatal = new LogLevel(Error.Level + 1);
            Debug = new LogLevel(Fatal.Level + 1);
            Trace = new LogLevel(Debug.Level + 1);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogLevel" /> class.
        /// </summary>
        /// <param name="level">value of log level</param>
        private LogLevel(int level)
        {
            this.Level = level;
        }

        /// <summary>
        /// Gets Trace LogLevel instance
        /// </summary>
        public static LogLevel Trace { get; }

        /// <summary>
        /// Gets Debug LogLevel instance
        /// </summary>
        public static LogLevel Debug { get; }

        /// <summary>
        /// Gets Fatal LogLevel instance
        /// </summary>
        public static LogLevel Fatal { get; }

        /// <summary>
        /// Gets Error LogLevel instance
        /// </summary>
        public static LogLevel Error { get; }

        /// <summary>
        /// Gets Warn LogLevel instance
        /// </summary>
        public static LogLevel Warn { get; }

        /// <summary>
        /// Gets Info LogLevel instance
        /// </summary>
        public static LogLevel Info { get; }

        /// <summary>
        /// Gets level value
        /// </summary>
        public int Level { get; }

        /// <summary>
        /// Compares two levels
        /// </summary>
        /// <param name="left">left <see cref="LogLevel"/></param>
        /// <param name="right">right <see cref="LogLevel"/></param>
        /// <returns>true if left is lower/equal</returns>
        public static bool operator <=(LogLevel left, LogLevel right)
        {
            return left.Level <= right.Level;
        }

        /// <summary>
        /// Compares two levels
        /// </summary>
        /// <param name="left">left <see cref="LogLevel"/></param>
        /// <param name="right">right <see cref="LogLevel"/></param>
        /// <returns>true if left is higher/equal</returns>
        public static bool operator >=(LogLevel left, LogLevel right)
        {
            return left.Level <= right.Level;
        }

        /// <summary>
        /// Compares two levels
        /// </summary>
        /// <param name="left">left <see cref="LogLevel"/></param>
        /// <param name="right">right <see cref="LogLevel"/></param>
        /// <returns>true if left is lower</returns>
        public static bool operator <(LogLevel left, LogLevel right)
        {
            return left.Level < right.Level;
        }

        /// <summary>
        /// Compares two levels
        /// </summary>
        /// <param name="left">left <see cref="LogLevel"/></param>
        /// <param name="right">right <see cref="LogLevel"/></param>
        /// <returns>true if left is higher</returns>
        public static bool operator >(LogLevel left, LogLevel right)
        {
            return left.Level < right.Level;
        }

        /// <summary>
        /// Compares two levels
        /// </summary>
        /// <param name="left">left <see cref="LogLevel"/></param>
        /// <param name="right">right <see cref="LogLevel"/></param>
        /// <returns>true if equal</returns>
        public static bool operator ==(LogLevel left, LogLevel right)
        {
            return left.Level == right.Level;
        }

        /// <summary>
        /// Compares two levels
        /// </summary>
        /// <param name="left">left <see cref="LogLevel"/></param>
        /// <param name="right">right <see cref="LogLevel"/></param>
        /// <returns>true if not equal</returns>
        public static bool operator !=(LogLevel left, LogLevel right)
        {
            return left.Level != right.Level;
        }

        /// <summary>
        /// Compares to other
        /// </summary>
        /// <param name="obj">other <see cref="LogLevel"/></param>
        /// <returns>true if equal</returns>
        public override bool Equals(object obj)
        {
            return obj is LogLevel level && level.Level == this.Level;
        }

        /// <summary>
        /// Gets hash code of current object
        /// </summary>
        /// <returns>hash code of current object</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(this.Level);
        }
    }
}
