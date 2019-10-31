// <copyright file="ILogger.cs" company="Roman Moravskyi">
// Copyright (c) Roman Moravskyi. All rights reserved.
// </copyright>

namespace CustomLogger
{
    using System;

    /// <summary>
    /// Common interface for loggers
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs message at <see cref="LogLevel.Debug"/>
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="exception">Logged exception</param>
        void Debug(string message, Exception exception);

        /// <summary>
        /// Logs message at <see cref="LogLevel.Error"/>
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="exception">Logged exception</param>
        void Error(string message, Exception exception);

        /// <summary>
        /// Logs message at <see cref="LogLevel.Fatal"/>
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="exception">Logged exception</param>
        void Fatal(string message, Exception exception);

        /// <summary>
        /// Logs message at <see cref="LogLevel.Info"/>
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="exception">Logged exception</param>
        void Info(string message, Exception exception);

        /// <summary>
        /// Logs message at <see cref="LogLevel.Trace"/>
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="exception">Logged exception</param>
        void Trace(string message, Exception exception);

        /// <summary>
        /// Logs message at <see cref="LogLevel.Warn"/>
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="exception">Logged exception</param>
        void Warn(string message, Exception exception);

        /// <summary>
        /// Logs message
        /// </summary>
        /// <param name="message">Log message</param>
        void LogMessage(string message);

        /// <summary>
        /// Logs message at specific level
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="level">Log level</param>
        /// <param name="exception">Logged exception</param>
        void LogMessage(string message, LogLevel level, Exception exception);
    }
}
