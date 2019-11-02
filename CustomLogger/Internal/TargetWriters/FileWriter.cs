namespace CustomLogger.Internal
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
            this._loggingStream = loggingStream
                ?? throw new ArgumentNullException("loggingStream", "FileWriter() null FileStream ref!");
        }

        public void WriteMessage(string message)
        {
            this._loggingStream.Write(new UTF8Encoding(true).GetBytes(message));
            this._loggingStream.Flush();
        }

        public async Task WriteMessageAsync(string message)
        {
            await this._loggingStream.WriteAsync(new UTF8Encoding(true).GetBytes(message));
            await this._loggingStream.FlushAsync();
        }

        public void Flush()
        {
            this._loggingStream.Flush();
        }

        #region IDisposable
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this._loggingStream.Flush();
                    this._loggingStream.Dispose();
                }
                this.disposedValue = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }
        #endregion
    }
}
