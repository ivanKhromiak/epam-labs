namespace CustomLogger.TargetWriters
{
    using System;
    using CustomLogger.Interfaces;

    public class FileWriter : ITargetWriter
    {
        public FileWriter(string filename)
        {
            Filename = filename;
        }

        private string Filename { get; }

        public void WriteMessage(string message)
        {
            throw new NotImplementedException();
        }

        public void WriteMessage(string message, LogLevel level)
        {
            throw new NotImplementedException();
        }
    }
}
