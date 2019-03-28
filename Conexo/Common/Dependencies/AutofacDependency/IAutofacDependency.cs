using System;
namespace Common.Dependencies.AutofacDependency
{
    public interface IAutofacDependency
    {
        Autofac.IContainer GetContainer();
    }
}
