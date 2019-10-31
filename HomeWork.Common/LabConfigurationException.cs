// <copyright file="LabConfigurationException.cs" company="Roman Moravskyi">
// Copyright (c) Roman Moravskyi. All rights reserved.
// </copyright>

namespace Epam.HomeWork.Lab7Runner
{
    using System;

    /// <summary>
    /// Represents lab configurations errors
    /// </summary>
    [Serializable]
    public class LabConfigurationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LabConfigurationException" /> class.
        /// </summary>
        public LabConfigurationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LabConfigurationException" /> class.
        /// </summary>
        /// <param name="message">Exception message</param>
        public LabConfigurationException(string message) 
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LabConfigurationException" /> class.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="inner">Inner exception</param>
        public LabConfigurationException(string message, Exception inner) 
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LabConfigurationException" /> class.
        /// </summary>
        /// <param name="info">Exception message</param>
        /// <param name="context">Streaming context</param>
        protected LabConfigurationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
