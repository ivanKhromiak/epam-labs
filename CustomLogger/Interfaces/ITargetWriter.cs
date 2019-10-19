using System;
using System.Collections.Generic;
using System.Text;

namespace CustomLogger.Interfaces
{
    public interface ITargetWriter
    {
        void WriteMessage(string message);
        void WriteMessage(string message, LogLevel level);
    }
}
