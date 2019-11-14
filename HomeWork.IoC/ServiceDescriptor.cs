namespace HomeWork.IoC
{
    using System;

    internal class ServiceDescriptor
    {
        public Type ServiceType { get; set; }

        public LifeTime ServiceLifeTime { get; set; }

        public object ServiceImplementation { get; set; }
    }
}
