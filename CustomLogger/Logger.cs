// <copyright file="Logger.cs" company="Roman Moravskyi">
// Copyright (c) Roman Moravskyi. All rights reserved.
// </copyright>

namespace CustomLogger
{
    using System;
    using System.Text;

    /// <summary>
    /// Logs messages
    /// </summary>
    public class Logger : ILogger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Logger" /> class.
        /// </summary>
        /// <param name="name">Logger name</param>
        /// <param name="logFactory">Logger factory</param>
        /// <param name="configuration">Logging Configuration</param>
        internal Logger(string name, LogFactory logFactory, LoggingConfiguration configuration)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.LogFactory = logFactory ?? throw new ArgumentNullException(nameof(logFactory));
            this.Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Gets Logger name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets factory that created this logger
        /// </summary>
        public LogFactory LogFactory { get; }

        /// <summary>
        /// Gets or sets LoggingConfiguration
        /// </summary>
        internal LoggingConfiguration Configuration { get; set; }

        /// <summary>
        /// Logs message at <see cref="LogLevel.Debug"/>
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="exception">Logged exception</param>
        public void Debug(string message, Exception exception)
        {
            this.Log(message: $"{GetTimeStamp()}: {message}\n" +
                $"Exception: {GetInfoFromException(exception, LogLevel.Debug)}");
        }

        /// <summary>
        /// Logs message at <see cref="LogLevel.Error"/>
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="exception">Logged exception</param>
        public void Error(string message, Exception exception)
        {
            this.Log($"{GetTimeStamp()}: {message}\n" +
                $"Exception: {GetInfoFromException(exception, LogLevel.Error)}");
        }

        /// <summary>
        /// Logs message at <see cref="LogLevel.Fatal"/>
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="exception">Logged exception</param>
        public void Fatal(string message, Exception exception)
        {
            this.Log($"{GetTimeStamp()}: {message}\n" +
                $"Exception: {GetInfoFromException(exception, LogLevel.Fatal)}");
        }

        /// <summary>
        /// Logs message at <see cref="LogLevel.Info"/>
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="exception">Logged exception</param>
        public void Info(string message, Exception exception)
        {
            this.Log($"{GetTimeStamp()}: {message}\n" +
                $"Exception: {GetInfoFromException(exception, LogLevel.Info)}");
        }

        /// <summary>
        /// Logs message
        /// </summary>
        /// <param name="message">Log message</param>
        public void LogMessage(string message)
        {
            this.Log($"{GetTimeStamp()}: {message}");
        }

        /// <summary>
        /// Logs message at specific log level
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="level">Log level</param>
        /// <param name="exception">Logged exception</param>
        public void LogMessage(string message, LogLevel level, Exception exception)
        {
            this.Log($"{GetTimeStamp()}: {message}\n" +
                $"Exception: {GetInfoFromException(exception, level)}");
        }

        /// <summary>
        /// Logs message at <see cref="LogLevel.Trace"/>
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="exception">Logged exception</param>
        public void Trace(string message, Exception exception)
        {
            this.Log($"{GetTimeStamp()}: {message}\n" +
                $"Exception: {GetInfoFromException(exception, LogLevel.Trace)}");
        }

        /// <summary>
        /// Logs message at <see cref="LogLevel.Warn"/>
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="exception">Logged exception</param>
        public void Warn(string message, Exception exception)
        {
            this.Log($"{GetTimeStamp()}: {message}\n" +
                $"Exception: {GetInfoFromException(exception, LogLevel.Warn)}");
        }

        /// <summary>
        /// Formats log message correspondingly to log level
        /// </summary>
        /// <param name="e">Logged exception</param>
        /// <param name="level">Log level</param>
        /// <returns>Log string</returns>
        private static string GetInfoFromException(Exception e, LogLevel level)
        {
            var strBuilder = new StringBuilder();

            if (level <= LogLevel.Trace)
            {
                AppendTraceInfo(e, strBuilder);
            }

            if (level <= LogLevel.Debug)
            {
                AppendDebugInfo(e, strBuilder);
            }

            if (level <= LogLevel.Warn)
            {
                strBuilder.Append($"Exception message: {e?.Message};");
            }

            return strBuilder.ToString();
        }

        /// <summary>
        /// Appends Debug Info
        /// </summary>
        /// <param name="e">Logged exception</param>
        /// <param name="strBuilder">Log message string builder</param>
        private static void AppendDebugInfo(Exception e, StringBuilder strBuilder)
        {
            if (e is null)
            {
                return;
            }

            if (strBuilder is null)
            {
                throw new ArgumentNullException(nameof(strBuilder));
            }

            strBuilder.Append($"Module Name: {e.TargetSite.Module.FullyQualifiedName}");
            strBuilder.Append($"TargetSite Name: {e.TargetSite.Name}");
            strBuilder.Append($"Source: {e.Source}");
            strBuilder.Append($"Help link: {e.HelpLink}");
        }

        /// <summary>
        /// Appends Trace Info
        /// </summary>
        /// <param name="e">Logged exception</param>
        /// <param name="strBuilder">Log message string builder</param>
        private static void AppendTraceInfo(Exception e, StringBuilder strBuilder)
        {
            if (e == null)
            {
                return;
            }

            if (strBuilder is null)
            {
                throw new ArgumentNullException(nameof(strBuilder));
            }

            strBuilder.Append($"Stack trace: {e.StackTrace}; ");
            strBuilder.Append("Inner exception info: ");
            strBuilder.Append(GetInfoFromException(e?.InnerException, LogLevel.Trace));
        }

        /// <summary>
        /// Gets timestamp
        /// </summary>
        /// <returns>Timestamp string</returns>
        private static string GetTimeStamp()
        {
            return DateTime.Now.ToString();
        }

        /// <summary>
        /// Logs message
        /// </summary>
        /// <param name="message">Log message</param>
        private void Log(string message)
        {
            this.WriteToAllTargets($"{message}\n");
        }

        /// <summary>
        /// Logs message async
        /// </summary>
        /// <param name="message">Log message</param>
        private void LogAsync(string message)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Writes message to all targets
        /// </summary>
        /// <param name="message">Log message</param>
        private void WriteToAllTargets(string message)
        {
            var targetWriters = this.Configuration.GetTargetWriters();

            foreach (var writer in targetWriters)
            {
                writer.WriteMessage(message);
            }
        }

        /// <summary>
        /// Writes message to all targets async
        /// </summary>
        /// <param name="message">Log message</param>
        private void WriteToAllTargetsAsync(string message)
        {
            throw new NotImplementedException();
        }
    }
}
