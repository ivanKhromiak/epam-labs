namespace HomeWork.IoC
{
    using System;

    public partial class Container
    {
        public class ContainerBuilder : IContainerBuilder
        {
            private readonly Type sourceType;
            private readonly Container container;

            public ContainerBuilder(Type sourceType, Container container)
            {
                this.sourceType = sourceType;
                this.container = container;
            }

            public IContainerBuilder Use<TDest>(LifeTime lifeTime)
            {
                return this.Use(typeof(TDest), lifeTime);
            }

            public IContainerBuilder Use(Type destType, LifeTime lifeTime)
            {
                var newDescriptor = new ServiceDescriptor
                {
                    ServiceLifeTime = lifeTime,
                    ServiceType = destType,
                    ServiceImplementation = null
                };

                this.container.servicesMap.AddOrUpdate(this.sourceType, newDescriptor, (src, descriptor) => newDescriptor);

                return this;
            }
        }
    }
}
