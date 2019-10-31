// <copyright file="LogManager.cs" company="Roman Moravskyi">
// Copyright (c) Roman Moravskyi. All rights reserved.
// </copyright>

namespace CustomLogger
{
    using System;

    /// <summary>
    /// Static class for loggers managing
    /// </summary>
    public static class LogManager
    {
        /// <summary>
        /// Logging Configuration
        /// </summary>
        private static LoggingConfiguration config;

        /// <summary>
        /// Initializes static members of the <see cref="LogManager" /> class.
        /// </summary>
        static LogManager()
        {
            LogFactory = new LogFactory();
        }

        /// <summary>
        /// Gets or sets Logging Configuration
        /// </summary>
        public static LoggingConfiguration Configuration
        {
            get => config;
            set
            {
                config = value;
                LogFactory.LoggingConfiguration = config;
            }
        }

        /// <summary>
        /// Gets log factory
        /// </summary>
        public static LogFactory LogFactory { get; }

        /// <summary>
        /// Gets new or already created logger
        /// </summary>
        /// <param name="name">Logger name</param>
        /// <returns><see cref="Logger"/></returns>
        public static Logger GetLogger(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("name cannot be null, empty or whitespace", nameof(name));
            }

            return LogFactory.GetLogger(name);
        }

        /// <summary>
        /// Gets new or already created logger for specific type
        /// </summary>
        /// <param name="name">Logger name</param>
        /// <param name="type">Logger type</param>
        /// <returns><see cref="Logger"/></returns>
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
