using System;

namespace CustomLogger
{
    public static class LogManager
    {
        private static LoggingConfiguration _config;

        static LogManager()
        {
            LogFactory = new LogFactory();
        }
            
        public static LoggingConfiguration Configuration
        {
            get => _config;
            set
            {
                _config = value;
                LogFactory.LoggingConfiguration = _config;
            }
        }

        public static LogFactory LogFactory { get; }

        public static Logger GetLogger(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("name cannot be null, empty or whitespace", nameof(name));
            }

            return LogFactory.GetLogger(name);
        }

        public static Logger GetLoggerForType(string name, Type type)
        {
            return GetLogger($"{name}{type?.FullName}");
        }

        /// <summary>
        /// Call this method to shut down all loggers
        /// </summary>
        public static void Shutdown()
        {
            Configuration.Dispose();
            Configuration = new LoggingConfiguration();
            LogFactory.LoggingConfiguration = new LoggingConfiguration();
        }

    }
}
