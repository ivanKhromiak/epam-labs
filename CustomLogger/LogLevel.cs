using System;

namespace CustomLogger
{
    /// <summary>
    /// Detalisation level of Log Message: Trace - higest, Info - lowest
    /// </summary>
    public class LogLevel
    {
        static LogLevel()
        {
            Info = new LogLevel(0);
            Warn = new LogLevel(Info.Level + 1);
            Error = new LogLevel(Warn.Level + 1);
            Fatal = new LogLevel(Error.Level + 1);
            Debug = new LogLevel(Fatal.Level + 1);
            Trace = new LogLevel(Debug.Level + 1);
        }

        public static LogLevel Trace { get; }
        public static LogLevel Debug { get; }
        public static LogLevel Fatal { get; }
        public static LogLevel Error { get; }
        public static LogLevel Warn { get; }
        public static LogLevel Info { get; }
        
        private LogLevel(int level)
        {
            Level = level;
        }

        public int Level { get; }

        public static bool operator <=(LogLevel left, LogLevel right)
        {
            return left.Level <= right.Level;
        }

        public static bool operator >=(LogLevel left, LogLevel right)
        {
            return left.Level <= right.Level;
        }

        public static bool operator <(LogLevel left, LogLevel right)
        {
            return left.Level < right.Level;
        }

        public static bool operator >(LogLevel left, LogLevel right)
        {
            return left.Level < right.Level;
        }

        public static bool operator ==(LogLevel left, LogLevel right)
        {
            return left.Level == right.Level;
        }

        public static bool operator !=(LogLevel left, LogLevel right)
        {
            return left.Level != right.Level;
        }

        public override bool Equals(object obj)
        {
            return obj is LogLevel level && level.Level == Level;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Level);
        }
    };
}
