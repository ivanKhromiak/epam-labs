using HomeWork.IoC;
using NUnit.Framework;

namespace HomeWork.Ioc.Tests
{
    public class ContainerTests
    {
        private IContainer container;

        [SetUp]
        public void Setup()
        {
            container = new Container();

            container.For<IService>().Use<ConcreteService>(LifeTime.Singleton);
            container.For<ILogger>().Use<Logger>(LifeTime.Singleton);
            container.For(typeof(ICrudRepository<>)).Use(typeof(CrudRepository<>), LifeTime.Singleton);
        }

        [Test]
        public void ResolvesSimpleTypes()
        {
            var service = container.Resolve<IService>();

            Assert.AreEqual(typeof(ConcreteService), service.GetType());
        }

        [Test]
        public void ResolvesSingletones()
        {
            var firstService = container.Resolve<IService>();
            var secondService = container.Resolve<IService>();

            Assert.IsTrue(ReferenceEquals(firstService, secondService));
        }

        [Test]
        public void ResolvesConstructorDependencies()
        {
            var repository = container.Resolve<ICrudRepository<Employee>>();

            Assert.AreEqual(typeof(CrudRepository<Employee>), repository.GetType());

            repository = repository as CrudRepository<Employee>;

            Assert.AreEqual(typeof(Logger), repository.Logger.GetType());
        }

        [Test]
        public void ResolvesSingletonUnboundGenericDependencies()
        {
            var firstRepository = container.Resolve<ICrudRepository<Employee>>();
            var secondRepository = container.Resolve<ICrudRepository<Employee>>();

            Assert.IsTrue(ReferenceEquals(firstRepository, secondRepository));
        }
    }
}