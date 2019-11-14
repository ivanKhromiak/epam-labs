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
            var employeeRepository = repository as CrudRepository<Employee>;

            Assert.AreEqual(typeof(CrudRepository<Employee>), repository.GetType());
            Assert.AreEqual(typeof(Logger), employeeRepository.Logger.GetType());
        }

        [Test]
        public void ResolvesSingletonUnboundGenericDependencies() 
        {
            var employeeRepo = container.Resolve<ICrudRepository<Employee>>();
            var customerRepo = container.Resolve<ICrudRepository<Customer>>();

            var secondEmployeeRepo = container.Resolve<ICrudRepository<Employee>>();
            var secondCustomerRepo = container.Resolve<ICrudRepository<Customer>>();

            Assert.AreEqual(typeof(CrudRepository<Employee>), employeeRepo.GetType());
            Assert.AreEqual(typeof(CrudRepository<Customer>), customerRepo.GetType());
            Assert.IsTrue(ReferenceEquals(employeeRepo, secondEmployeeRepo));
            Assert.IsTrue(ReferenceEquals(customerRepo, secondCustomerRepo));
        }
    }
}