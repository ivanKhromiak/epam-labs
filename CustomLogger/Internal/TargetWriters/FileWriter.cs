﻿namespace CustomLogger.Internal
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    internal class FileWriter : ITargetWriter, IDisposable
    {
        private readonly FileStream _loggingStream = null;

        internal FileWriter(FileStream loggingStream)
        {
            _loggingStream = loggingStream
                ?? throw new ArgumentNullException("loggingStream", "FileWriter() null FileStream ref!");
        }

        public void WriteMessage(string message)
        {
            _loggingStream.Write(new UTF8Encoding(true).GetBytes(message));
        }

        public async Task WriteMessageAsync(string message)
        {
            await _loggingStream.WriteAsync(new UTF8Encoding(true).GetBytes(message));
        }

        public void Flush()
        {
            _loggingStream.Flush();
        }

        #region IDisposable
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _loggingStream.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}