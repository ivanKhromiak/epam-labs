using System;
using CustomLogger.Interfaces;

namespace CustomLogger
{
    public class Logger : ILogger
    {
        private ITargetWriter[] _writers;

        protected internal Logger()
        {

        }

        public string Name { get; }

        public LogFactory LogFactory { get; }

        public void Debug(string message)
        {
            throw new NotImplementedException();
        }

        public void Debug(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Error(string message)
        {
            throw new NotImplementedException();
        }

        public void Error(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Fatal(string message)
        {
            throw new NotImplementedException();
        }

        public void Fatal(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Info(string message)
        {
            throw new NotImplementedException();
        }

        public void Info(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void LogMessage(string message, LogLevel level)
        {
            throw new NotImplementedException();
        }

        public void Trace(string message)
        {
            throw new NotImplementedException();
        }

        public void Trace(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Warn(string message)
        {
            throw new NotImplementedException();
        }

        public void Warn(string message, Exception exception)
        {
            throw new NotImplementedException();
        }
    }
}
