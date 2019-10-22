namespace CustomLogger
{
    using System;
    using System.Collections.Generic;
    using CustomLogger.Internal;

    public class LoggingConfiguration : IDisposable
    {
        private readonly List<Target> _targets;
        private List<ITargetWriter> _writers;

        internal event Action LoggingConfigChanged;

        public LoggingConfiguration()
        {
            _targets = new List<Target>();
            _writers = null;
        }

        public LoggingConfiguration AddFileTarget(string path)
        {
            // target already exists
            foreach(var target in _targets)
            {
                if(target is FileTarget fileTarget && fileTarget.Path == path)
                {
                    return this;
                }
            }

            _targets.Add(new FileTarget(path));
            ReconfigTargetWriters();

            return this;
        }

        public LoggingConfiguration AddConsoleTarget()
        {
            _targets.Add(new ConsoleTarget());
            ReconfigTargetWriters();

            return this;
        }

        public void Reset()
        {
            _targets.Clear();
            ReconfigTargetWriters();
        }

        internal IEnumerable<ITargetWriter> GetTargetWriters()
        {
            if (_writers == null)
            {
                ReconfigTargetWriters();
            }

            return _writers;
        }

        private void ReconfigTargetWriters()
        {
            ClearOldWriters();
            SetNewWriters();

            LoggingConfigChanged?.Invoke();
        }

        private void SetNewWriters()
        {
            _writers = new List<ITargetWriter>();
            foreach (var target in _targets)
            {
                _writers.Add(target.GetTargetWriter());
            }
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
