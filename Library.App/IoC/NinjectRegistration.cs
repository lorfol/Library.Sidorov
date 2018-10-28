using Library.Domain.Interfaces;
using Library.Infrastructure.Data;
using Ninject.Modules;

namespace Library.App.IoC
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}