namespace HomeWork.Ioc.Tests
{
    public interface IService
    {
    }

    public class ConcreteService : IService
    {

    }

    public interface ILogger
    {

    }

    public class Logger : ILogger
    {

    }

    public interface ICrudRepository<T>
    {
        ILogger Logger { get; }
    }

    public class CrudRepository<T> : ICrudRepository<T>
    {
        public CrudRepository(ILogger logger)
        {
            this.Logger = logger;
        }

        public ILogger Logger { get; }
    }

    public class Employee
    {

    }

    public class Customer
    {

    }
}
