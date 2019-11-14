namespace HomeWork.IoC
{
    using System;

    public interface IContainerBuilder
    {
        IContainerBuilder Use<TDest>(LifeTime lifeTime);

        IContainerBuilder Use(Type destType, LifeTime lifeTime);
    }
}