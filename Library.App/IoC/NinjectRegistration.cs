﻿using Library.Domain.Interfaces;
using Library.Infrastructure.Business;
using Library.Infrastructure.Data;
using Library.Services.Interfaces;
using Ninject.Modules;

namespace Library.App.IoC
{
    public class NinjectRegistrations : NinjectModule
    {
        // global dependency registration
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IOrdersService>().To<OrdersService>();
        }
    }
}