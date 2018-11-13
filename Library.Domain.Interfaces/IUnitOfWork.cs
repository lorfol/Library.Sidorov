using Library.Domain.Interfaces.Repositories;
using System;

namespace Library.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository Books { get; }

        IOrderRepository Orders { get; }

        IAuthorRepository Authors { get;  }

        IPublisherRepository Publishers { get; }

        void Save();
    }
}
