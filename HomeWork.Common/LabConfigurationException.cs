namespace Epam.HomeWork.Lab7Runner
{
    using System;

    [Serializable]
    public class LabConfigurationException : Exception
    {
        public LabConfigurationException() { }
        public LabConfigurationException(string message) : base(message) { }
        public LabConfigurationException(string message, Exception inner) : base(message, inner) { }
        protected LabConfigurationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
