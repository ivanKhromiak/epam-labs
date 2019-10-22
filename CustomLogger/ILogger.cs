using System;

namespace CustomLogger
{
    /// <summary>
    /// Common interface for loggers
    /// </summary>
    public interface ILogger
    {
        void Debug(string message, Exception exception);
        void Error(string message, Exception exception);
        void Fatal(string message, Exception exception);
        void Info(string message, Exception exception);
        void Trace(string message, Exception exception);
        void Warn(string message, Exception exception);
        void LogMessage(string message, LogLevel level);
        void LogMessage(string message, LogLevel level, Exception exception);
    }
}
