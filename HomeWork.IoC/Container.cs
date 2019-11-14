namespace HomeWork.IoC
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Reflection;
    using HomeWork.IoC.Exceptions;

    public class Container : IContainer
    {
        private readonly ConcurrentDictionary<Type, ServiceDescriptor> servicesMap;
        private readonly ConcurrentDictionary<Type, object> genericServicesMap;

        public Container()
        {
            this.servicesMap = new ConcurrentDictionary<Type, ServiceDescriptor>();
            this.genericServicesMap = new ConcurrentDictionary<Type, object>();
        }

        public IContainerBuilder For<TSource>()
        {
            return this.For(typeof(TSource));
        }

        public IContainerBuilder For(Type sourceType)
        {
            return new ContainerBuilder(sourceType, this);
        }

        public TDest Resolve<TDest>()
        {
            return (TDest)Resolve(typeof(TDest));
        }

        public object Resolve(Type sourceType)
        {
            object instance;

            if (!sourceType.IsAbstract)
            {
                instance = CreateInstance(sourceType);
            }
            else if (servicesMap.ContainsKey(sourceType))
            {
                instance = CreateInstance(servicesMap[sourceType]);
            }
            else if (sourceType.IsGenericType && servicesMap.ContainsKey(sourceType.GetGenericTypeDefinition()))
            {
                instance = CreateGenericInstance(sourceType);
            }
            else
            {
                throw new NotResolvedException($"Cannot resolve for type: {sourceType.FullName}");
            }

            return instance;
        }

        private object CreateGenericInstance(Type sourceType)
        {
            object instance = null;
            var destTypeDesc = servicesMap[sourceType.GetGenericTypeDefinition()];
            var closedDestType = destTypeDesc.ServiceType.MakeGenericType(sourceType.GetGenericArguments());

            if (destTypeDesc.ServiceLifeTime == LifeTime.Transient)
            {
                instance = CreateInstance(closedDestType);
            }
            else if (destTypeDesc.ServiceLifeTime == LifeTime.Singleton)
            {
                if (!genericServicesMap.ContainsKey(closedDestType))
                {
                    genericServicesMap.TryAdd(closedDestType, CreateInstance(closedDestType));
                }

                instance = genericServicesMap[closedDestType];
            }

            return instance;
        }

        private object CreateInstance(ServiceDescriptor serviceDescriptor)
        {
            object service = null;

            if (serviceDescriptor.ServiceLifeTime == LifeTime.Transient)
            {
                return CreateInstance(serviceDescriptor.ServiceType);
            }
            else if (serviceDescriptor.ServiceLifeTime == LifeTime.Singleton)
            {
                if (serviceDescriptor.ServiceImplementation == null)
                {
                    serviceDescriptor.ServiceImplementation = CreateInstance(serviceDescriptor.ServiceType);
                }

                service = serviceDescriptor.ServiceImplementation;
            }

            return service;
        }

        private object CreateInstance(Type serviceType)
        {
            ConstructorInfo ctor = serviceType.GetConstructors()
                                  .OrderByDescending(c => c.GetParameters().Length)
                                  .FirstOrDefault();

            if (ctor == null)
            {
                throw new NoConstructorAvaliableException(
                    $"No constructor avaliable for type: {serviceType.FullName}");
            }

            var ctorParams = ctor.GetParameters()
                                 .Select(p => Resolve(p.ParameterType))
                                 .ToArray();

            return Activator.CreateInstance(serviceType, ctorParams);
        }

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
