using System;
using System.Collections.Generic;

namespace CustomLogger
{
    /// <summary>
    /// Manages Loggers and creates new ones
    /// </summary>
    public class LogFactory : IDisposable
    {
        private readonly LoggerCache _loggerCache = new LoggerCache();
        private LoggingConfiguration _loggingConfiguration;
        private readonly object _syncRoot = new object();

        internal LogFactory() : this(new LoggingConfiguration())
        { }

        internal LogFactory(LoggingConfiguration configuration)
        {
            LoggingConfiguration = configuration
                ?? throw new ArgumentNullException(nameof(configuration));

            LoggingConfiguration.LoggingConfigChanged += LoggingConfigChanged;
        }

        public LoggingConfiguration LoggingConfiguration
        {
            get => _loggingConfiguration;
            set
            {
                _loggingConfiguration = value
                    ?? throw new ArgumentNullException(nameof(value));

                ReconfigLoggers(LoggingConfiguration);
            }
        }

        /// <summary>
        /// Event handler for LoggingConfiguration.LoggingConfigChanged
        /// </summary>
        private void LoggingConfigChanged()
        {
            ReconfigLoggers(LoggingConfiguration);
        }

        /// <summary>
        /// Reconfigs loggers with new configuration
        /// </summary>
        /// <param name="loggingConfiguration"></param>
        private void ReconfigLoggers(LoggingConfiguration loggingConfiguration)
        {
            IList<Logger> loggers;

            lock (_syncRoot)
            {
                loggers = _loggerCache.GetLoggers();
            }

            foreach (var logger in loggers)
            {
                logger.Configuration = loggingConfiguration;
            }
        }

        /// <summary>
        /// Gets a logger from cache or creates new logger
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Logger GetLogger(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("name cannot be null, empty or whitespace", nameof(name));
            }

            Logger logger = null;

            lock (_syncRoot)
            {
                var cacheKey = new LoggerCacheKey(name, typeof(Logger));
                logger = _loggerCache.Retrieve(cacheKey);

                if (logger == null)
                {
                    logger = new Logger(name, this, LoggingConfiguration);
                    _loggerCache.InsertOrUpdate(cacheKey, logger);
                }
            }

            return logger;
        }

        /// <summary>
        /// Key for Dictionary of Logger
        /// </summary>
        private class LoggerCacheKey
        {
            public LoggerCacheKey(string name, Type type)
            {
                Name = name;
                Type = type;
            }

            public string Name { get; }
            public Type Type { get; }

            public override bool Equals(object obj)
            {
                return obj is LoggerCacheKey key &&
                       Name == key.Name &&
                       EqualityComparer<Type>.Default.Equals(Type, key.Type);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Name, Type);
            }
        }

        /// <summary>
        /// Cache for storing loggers
        /// </summary>
        private class LoggerCache
        {
            private readonly Dictionary<LoggerCacheKey, WeakReference<Logger>> _cache
                = new Dictionary<LoggerCacheKey, WeakReference<Logger>>();

            /// <summary>
            /// Inserts or updates logger
            /// </summary>
            /// <param name="key"></param>
            /// <param name="logger"></param>
            public void InsertOrUpdate(LoggerCacheKey key, Logger logger)
            {
                _cache[key] = new WeakReference<Logger>(logger);
            }

            /// <summary>
            /// Retrieve a logger from cache
            /// </summary>
            /// <param name="key"></param>
            /// <returns>Logger, null if not alive</returns>
            public Logger Retrieve(LoggerCacheKey key)
            {
                if (_cache.TryGetValue(key, out var logger))
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
            /// <returns>IList<Loggers></returns>
            public IList<Logger> GetLoggers()
            {
                var loggers = new List<Logger>();

                foreach(WeakReference<Logger> reference in _cache.Values)
                {
                    if(reference.TryGetTarget(out Logger logger))
                    {
                        loggers.Add(logger);
                    }
                }

                return loggers;
            }
        }

        #region IDisposable
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    LoggingConfiguration.Dispose();
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
