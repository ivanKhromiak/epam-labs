namespace HomeWork.IoC.Exceptions
{
    using System;

    [Serializable]
    public class NotResolvedException : ContainerException
    {
        public NotResolvedException() { }
        public NotResolvedException(string message) : base(message) { }
        public NotResolvedException(string message, Exception inner) : base(message, inner) { }
        protected NotResolvedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
