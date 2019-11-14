namespace HomeWork.IoC
{
    using System;

    public interface IContainer
    {
        IContainerBuilder For<TSource>();

        IContainerBuilder For(Type sourceType);

        TDest Resolve<TDest>();

        object Resolve(Type destType);
    }
}
