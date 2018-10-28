using Library.Domain.Interfaces.Repositories;
using Library.Infrastructure.Data.Repositories;
using System;

namespace Library.Infrastructure.Data
{
    public class UnitOfWork : IDisposable
    {
        public UnitOfWork()
        {
            dbContext = new LibraryDbContext();
        }

        private readonly LibraryDbContext dbContext;
        private IBookRepository bookRepository;
        private IUserRepository userRepository;
        private IOrderRepository orderRepository;

        private bool disposed = false;

        public IBookRepository Books => this.bookRepository ?? (this.bookRepository = new BookRepository(this.dbContext));
        public IUserRepository Users => this.userRepository ?? (this.userRepository = new UserRepository(this.dbContext));
        public IOrderRepository Orders => this.orderRepository ?? (this.orderRepository = new OrderRepository(this.dbContext));

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
