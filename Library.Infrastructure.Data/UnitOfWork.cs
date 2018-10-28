using Library.Domain.Interfaces;
using Library.Domain.Interfaces.Repositories;
using Library.Infrastructure.Data.Repositories;
using System;

namespace Library.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork()
        {
            dbContext = new LibraryDbContext();
        }

        private readonly LibraryDbContext dbContext;
        private IBookRepository booksRepository;
        private IOrderRepository ordersRepository;

        private bool disposed = false;

        public IBookRepository Books => this.booksRepository ?? (this.booksRepository = new BookRepository(this.dbContext));
        public IOrderRepository Orders => this.ordersRepository ?? (this.ordersRepository = new OrderRepository(this.dbContext));

        public void Save()
        {
            this.dbContext.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.dbContext.Dispose();
                }

                this.disposed = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
