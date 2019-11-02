// <copyright file="LogFactory.cs" company="Roman Moravskyi">
// Copyright (c) Roman Moravskyi. All rights reserved.
// </copyright>

namespace CustomLogger
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Manages Loggers and creates new ones
    /// </summary>
    public class LogFactory : IDisposable
    {
        /// <summary>
        /// Sync Root
        /// </summary>
        private readonly object syncRoot = new object();

        /// <summary>
        /// Cache for created loggers
        /// </summary>
        private readonly LoggerCache loggerCache = new LoggerCache();

        /// <summary>
        /// Current logging configuration
        /// </summary>
        private LoggingConfiguration loggingConfiguration;

        /// <summary>
        /// Disposed Value
        /// </summary>
        private bool disposedValue = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogFactory" /> class.
        /// </summary>
        internal LogFactory() : this(new LoggingConfiguration())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogFactory" /> class.
        /// </summary>
        /// <param name="configuration">Logging Configuration</param>
        internal LogFactory(LoggingConfiguration configuration)
        {
            this.LoggingConfiguration = configuration
                ?? throw new ArgumentNullException(nameof(configuration));

            this.LoggingConfiguration.LoggingConfigChanged += this.LoggingConfigChanged;
        }

        /// <summary>
        /// Gets or sets Logging Configuration
        /// </summary>
        public LoggingConfiguration LoggingConfiguration
        {
            get => this.loggingConfiguration;
            set
            {
                this.loggingConfiguration = value
                    ?? throw new ArgumentNullException(nameof(value));

                this.ReconfigLoggers(this.LoggingConfiguration);
            }
        }

        /// <summary>
        /// Gets a logger from cache or creates new logger
        /// </summary>
        /// <param name="name">Name of a logger</param>
        /// <returns><see cref="Logger"/></returns>
        public Logger GetLogger(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("name cannot be null, empty or whitespace", nameof(name));
            }

            Logger logger = null;

            lock (this.syncRoot)
            {
                var cacheKey = new LoggerCacheKey(name, typeof(Logger));
                logger = this.loggerCache.Retrieve(cacheKey);

                if (logger == null)
                {
                    logger = new Logger(name, this, this.LoggingConfiguration);
                    this.loggerCache.InsertOrUpdate(cacheKey, logger);
                }
            }

            return logger;
        }

        /// <summary>
        /// Dispose current object
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        /// <summary>
        /// Dispose current object
        /// </summary>
        /// <param name="disposing">Disposed value</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.LoggingConfiguration.Dispose();
                }

                this.disposedValue = true;
            }
        }

        /// <summary>
        /// Event handler for LoggingConfiguration.LoggingConfigChanged
        /// </summary>
        private void LoggingConfigChanged()
        {
            this.ReconfigLoggers(this.LoggingConfiguration);
        }

        /// <summary>
        /// Sets new configuration to all loggers
        /// </summary>
        /// <param name="loggingConfiguration">Logging configuration</param>
        private void ReconfigLoggers(LoggingConfiguration loggingConfiguration)
        {
            IList<Logger> loggers;

            lock (this.syncRoot)
            {
                loggers = this.loggerCache.GetLoggers();
            }

            foreach (var logger in loggers)
            {
                logger.Configuration = loggingConfiguration;
            }
        }

        /// <summary>
        /// Key for Dictionary of Logger
        /// </summary>
        private class LoggerCacheKey
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="LoggerCacheKey" /> class.
            /// </summary>
            /// <param name="name">Logger name</param>
            /// <param name="type">Logger type</param>
            public LoggerCacheKey(string name, Type type)
            {
                this.Name = name;
                this.Type = type;
            }

            /// <summary>
            /// Gets a logger name
            /// </summary>
            public string Name { get; }

            /// <summary>
            /// Gets a logger Type/>
            /// </summary>
            public Type Type { get; }

            /// <summary>
            /// Checks if two keys are equal
            /// </summary>
            /// <param name="obj">Other key</param>
            /// <returns>value indicating whether two keys are equal</returns>
            public override bool Equals(object obj)
            {
                return obj is LoggerCacheKey key &&
                       this.Name == key.Name &&
                       EqualityComparer<Type>.Default.Equals(this.Type, key.Type);
            }

            /// <summary>
            /// Gets HashCode
            /// </summary>
            /// <returns>HashCode of current object</returns>
            public override int GetHashCode()
            {
                return HashCode.Combine(this.Name, this.Type);
            }
        }

        /// <summary>
        /// Cache for storing loggers
        /// </summary>
        private class LoggerCache
        {
            /// <summary>
            /// Dictionary of <see cref="LoggerCacheKey"/> and <see cref="WeakReference{Logger}"/>
            /// </summary>
            private readonly Dictionary<LoggerCacheKey, WeakReference<Logger>> loggersCache
                = new Dictionary<LoggerCacheKey, WeakReference<Logger>>();

            /// <summary>
            /// Inserts or updates logger
            /// </summary>
            /// <param name="key">Logger cache key</param>
            /// <param name="logger">Logger name</param>
            public void InsertOrUpdate(LoggerCacheKey key, Logger logger)
            {
                this.loggersCache[key] = new WeakReference<Logger>(logger);
            }

            /// <summary>
            /// Retrieve a logger from cache
            /// </summary>
            /// <param name="key">Logger cache key</param>
            /// <returns>Logger, null if not alive</returns>
            public Logger Retrieve(LoggerCacheKey key)
            {
                if (this.loggersCache.TryGetValue(key, out var logger))
                {
                    if (logger.TryGetTarget(out Logger target))
                    {
                        // logger is alive and still in the cache
                        return target;
                    }
                }

                return null;
            }

            /// <summary>
            /// Returns all alive loggers in the cache
            /// </summary>
            /// <returns>List of Loggers</returns>
            public IList<Logger> GetLoggers()
            {
                var loggers = new List<Logger>();

                foreach (WeakReference<Logger> reference in this.loggersCache.Values)
                {
                    if (reference.TryGetTarget(out Logger logger))
                    {
                        loggers.Add(logger);
                    }
                }

                return loggers;
            }
        }
    }
}
