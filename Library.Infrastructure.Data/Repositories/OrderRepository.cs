using Library.Domain.Core.Models;
using Library.Domain.Interfaces.Repositories;

namespace Library.Infrastructure.Data.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(LibraryDbContext context)
            : base(context)
        {
        }
    }
}
