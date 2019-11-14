namespace HomeWork.IoC
{
    using System.ComponentModel;

    public enum LifeTime
    {
        [Description("Single instance for each request")]
        Singleton,
        
        [Description("New instance for each request")]
        Transient
    }
}