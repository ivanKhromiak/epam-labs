// <copyright file="LoggingConfiguration.cs" company="Roman Moravskyi">
// Copyright (c) Roman Moravskyi. All rights reserved.
// </copyright>

namespace CustomLogger
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CustomLogger.Internal;

    /// <summary>
    /// Represents Logging Configuration
    /// </summary>
    public class LoggingConfiguration : IDisposable
    {
        /// <summary>
        /// List of targets
        /// </summary>
        private readonly List<Target> targets;

        /// <summary>
        /// Cache List of targets
        /// </summary>
        private readonly List<Target> targetsCache;

        /// <summary>
        /// List of writers
        /// </summary>
        private List<ITargetWriter> writers;

        /// <summary>
        /// Disposed value
        /// </summary>
        private bool disposedValue = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingConfiguration" /> class.
        /// </summary>
        public LoggingConfiguration()
        {
            this.targets = new List<Target>();
            this.targetsCache = new List<Target>();
            this.writers = null;
        }

        /// <summary>
        /// Event for config changes
        /// </summary>
        internal event Action LoggingConfigChanged;

        /// <summary>
        /// Adds file target
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>current object</returns>
        public LoggingConfiguration AddFileTarget(string path)
        {
            // target already exists
            if (!this.IsAlreadyAddedFileTarget(path))
            {
                this.targets.Add(new FileTarget(path));
            }

            return this;
        }

        /// <summary>
        /// Adds console target
        /// </summary>
        /// <returns>current object</returns>
        public LoggingConfiguration AddConsoleTarget()
        {
            // target already exists
            if (!this.IsAlreadyAddedTarget<ConsoleTarget>())
            {
                this.targets.Add(new ConsoleTarget());
            }

            return this;
        }

        /// <summary>
        /// Resets configurations
        /// </summary>
        public void Reset()
        {
            this.targets.Clear();
            this.targetsCache.Clear();
            this.ClearOldWriters();
        }

        /// <summary>
        /// Disposes current object
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        /// <summary>
        /// Gets current list of <see cref="ITargetWriter"/>
        /// </summary>
        /// <returns>list of <see cref="ITargetWriter"/></returns>
        internal IEnumerable<ITargetWriter> GetTargetWriters()
        {
            if (this.writers == null || !this.CheckCahceCoherency())
            {
                this.ReconfigTargetWriters();
            }

            this.targetsCache.AddRange(this.targets);
            this.targets.Clear();

            return this.writers;
        }

        /// <summary>
        /// Disposes current object
        /// </summary>
        /// <param name="disposing">Disposed value</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.ClearOldWriters();
                }

                this.disposedValue = true;
            }
        }

        /// <summary>
        /// Checks cache coherency
        /// </summary>
        /// <returns>value indicating if cache is up-to-date</returns>
        private bool CheckCahceCoherency()
        {
            return this.targets.Count + this.targetsCache.Count == this.writers.Count;
        }

        /// <summary>
        /// Reconfigures target writers
        /// </summary>
        private void ReconfigTargetWriters()
        {
            if (this.writers == null)
            {
                this.writers = new List<ITargetWriter>();
            }

            foreach (var target in this.targets)
            {
                this.writers.Add(target.GetTargetWriter());
            }

            this.LoggingConfigChanged?.Invoke();
        }

        /// <summary>
        /// Clears writers
        /// </summary>
        private void ClearOldWriters()
        {
            if (this.writers != null)
            {
                foreach (var writer in this.writers)
                {
                    if (writer is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                }

                this.writers.Clear();
                this.writers = null;
            }
        }

        /// <summary>
        /// Checks if <see cref="Target"/> already added
        /// </summary>
        /// <typeparam name="T">Target type</typeparam>
        /// <returns>value indicating whether Target already added</returns>
        private bool IsAlreadyAddedTarget<T>()
            where T : Target
        {
            bool isPresentTarget = false;

            if (this.targets.Count > 0)
            {
                isPresentTarget = this.targets.Any(t => t is T);
            }

            if (this.targetsCache.Count > 0)
            {
                isPresentTarget = this.targetsCache.Any(t => t is T);
            }

            return isPresentTarget;
        }

        /// <summary>
        /// Checks if <see cref="FileTarget"/> already added
        /// </summary>
        /// <param name="path">FileTarget path</param>
        /// <returns>value indicating weather FileTarget already added</returns>
        private bool IsAlreadyAddedFileTarget(string path)
        {
            bool Predicate(Target t)
            {
                return t is FileTarget tar && tar.Path == path;
            }

            bool isPresentTarget = false;

            if (this.targets.Count > 0)
            {
                isPresentTarget = this.targets.Any(t => Predicate(t));
            }

            if (this.targetsCache.Count > 0)
            {
                isPresentTarget = this.targetsCache.Any(t => Predicate(t));
            }

            return isPresentTarget;
        }
    }
}
