namespace CustomLogger
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CustomLogger.Internal;

    public class LoggingConfiguration : IDisposable
    {
        private readonly List<Target> _targets;
        private readonly List<Target> _targetsCache;
        private List<ITargetWriter> _writers;

        internal event Action LoggingConfigChanged;

        public LoggingConfiguration()
        {
            _targets = new List<Target>();
            _targetsCache = new List<Target>();
            _writers = null;
        }

        public LoggingConfiguration AddFileTarget(string path)
        {
            // target already exists
            if (!IsAlreadyAddedFileTarget(path))
            {
                _targets.Add(new FileTarget(path));
            }

            return this;
        }

        public LoggingConfiguration AddConsoleTarget()
        {
            // target already exists
            if (!IsAlreadyAddedTarget<ConsoleTarget>())
            {
                _targets.Add(new ConsoleTarget());
            }

            return this;
        }

        private bool IsAlreadyAddedTarget<T>()
            where T : Target
        {
            bool isPresentTarget = false;

            if (_targets.Count > 0)
            {
                isPresentTarget = _targets.Any(t => t is T);
            }
            if (_targetsCache.Count > 0)
            {
                isPresentTarget = _targetsCache.Any(t => t is T);
            }

            return isPresentTarget;
        }

        private bool IsAlreadyAddedFileTarget(string path)
        {
            bool Predicate(Target t)
            {
                return t is FileTarget tar && tar.Path == path;
            }

            bool isPresentTarget = false;

            if (_targets.Count > 0)
            {
                isPresentTarget = _targets.Any(t => Predicate(t));
            }
            if (_targetsCache.Count > 0)
            {
                isPresentTarget = _targetsCache.Any(t => Predicate(t));
            }

            return isPresentTarget;
        }

        public void Reset()
        {
            _targets.Clear();
            _targetsCache.Clear();
            ClearOldWriters();
        }

        internal IEnumerable<ITargetWriter> GetTargetWriters()
        {
            if (_writers == null || !DoAllTargetsHaveWriters())
            {
                ReconfigTargetWriters();
            }

            _targetsCache.AddRange(_targets);
            _targets.Clear();

            return _writers;
        }

        private bool DoAllTargetsHaveWriters()
        {
            return _targets.Count + _targetsCache.Count == _writers.Count;
        }

        private void ReconfigTargetWriters()
        {
            if (_writers == null)
            {
                _writers = new List<ITargetWriter>();
            }

            foreach (var target in _targets)
            {
                _writers.Add(target.GetTargetWriter());
            }

            LoggingConfigChanged?.Invoke();
        }

        private void ClearOldWriters()
        {
            if (_writers != null)
            {
                foreach (var writer in _writers)
                {
                    if (writer is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                }
                _writers.Clear();
                _writers = null;
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
                    ClearOldWriters();
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
