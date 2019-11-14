namespace HomeWork.IoC
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Reflection;
    using HomeWork.IoC.Exceptions;

    public partial class Container : IContainer
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
            return (TDest)this.Resolve(typeof(TDest));
        }

        public object Resolve(Type sourceType)
        {
            object instance;

            if (!sourceType.IsAbstract)
            {
                instance = this.CreateInstance(sourceType);
            }
            else if (this.servicesMap.ContainsKey(sourceType))
            {
                instance = this.CreateInstance(this.servicesMap[sourceType]);
            }
            else if (sourceType.IsGenericType && this.servicesMap.ContainsKey(sourceType.GetGenericTypeDefinition()))
            {
                instance = this.CreateGenericInstance(sourceType);
            }
            else
            {
                throw new NotResolvedException($"Cannot resolve type: {sourceType.FullName}");
            }

            return instance;
        }

        private object CreateGenericInstance(Type sourceType)
        {
            object instance = null;
            var destTypeDesc = this.servicesMap[sourceType.GetGenericTypeDefinition()];
            var closedDestType = destTypeDesc.ServiceType.MakeGenericType(sourceType.GetGenericArguments());

            if (destTypeDesc.ServiceLifeTime == LifeTime.Transient)
            {
                instance = this.CreateInstance(closedDestType);
            }
            else if (destTypeDesc.ServiceLifeTime == LifeTime.Singleton)
            {
                if (!this.genericServicesMap.ContainsKey(closedDestType))
                {
                    this.genericServicesMap.TryAdd(closedDestType, this.CreateInstance(closedDestType));
                }

                instance = this.genericServicesMap[closedDestType];
            }

            return instance;
        }

        private object CreateInstance(ServiceDescriptor serviceDescriptor)
        {
            object service = null;

            if (serviceDescriptor.ServiceLifeTime == LifeTime.Transient)
            {
                return this.CreateInstance(serviceDescriptor.ServiceType);
            }
            else if (serviceDescriptor.ServiceLifeTime == LifeTime.Singleton)
            {
                if (serviceDescriptor.ServiceImplementation == null)
                {
                    serviceDescriptor.ServiceImplementation = this.CreateInstance(serviceDescriptor.ServiceType);
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
                                 .Select(p => this.Resolve(p.ParameterType))
                                 .ToArray();

            return Activator.CreateInstance(serviceType, ctorParams);
        }
    }
}
