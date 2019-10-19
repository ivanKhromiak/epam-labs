using System;
using System.Collections.Generic;

namespace CustomLogger
{
    public class LogFactory
    {
        public LogFactory()
        {

        }

        public LogFactory(LoggingConfiguration configuration)
        {

        }

        public Logger GetLogger(string name)
        {
            return new Logger();
        }

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

        private class LoggerCache
        {
            private readonly Dictionary<LoggerCacheKey, WeakReference<Logger>> _cache
                = new Dictionary<LoggerCacheKey, WeakReference<Logger>>();

            public void InsertOrUpdate(LoggerCacheKey key, Logger logger)
            {
                _cache[key] = new WeakReference<Logger>(logger);
            }

            public Logger Retrieve(LoggerCacheKey key)
            {
                if(_cache.TryGetValue(key, out var logger))
                {
                    if(logger.TryGetTarget(out Logger target))
                    {
                        return target;
                    }
                }
                return null;
            }

            public IList<Logger> GetLoggers()
            {
                var loggers = new List<Logger>();

                return loggers;
            }
        }

    }
}
