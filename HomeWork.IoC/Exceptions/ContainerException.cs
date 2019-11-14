namespace HomeWork.IoC.Exceptions
{
    using System;

    [Serializable]
    public class ContainerException : ApplicationException
    {
        public ContainerException() { }
        public ContainerException(string message) : base(message) { }
        public ContainerException(string message, Exception inner) : base(message, inner) { }
        protected ContainerException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
