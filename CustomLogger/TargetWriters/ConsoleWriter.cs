using System;
using System.Collections.Generic;
using System.Text;
using CustomLogger.Interfaces;

namespace CustomLogger.TargetWriters
{
    public class ConsoleWriter : ITargetWriter
    {
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
