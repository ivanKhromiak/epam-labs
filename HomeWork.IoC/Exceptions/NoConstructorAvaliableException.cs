namespace HomeWork.IoC.Exceptions
{
    using System;

    [Serializable]
    public class NoConstructorAvaliableException : ContainerException
    {
        public NoConstructorAvaliableException() { }
        public NoConstructorAvaliableException(string message) : base(message) { }
        public NoConstructorAvaliableException(string message, Exception inner) : base(message, inner) { }
        protected NoConstructorAvaliableException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
