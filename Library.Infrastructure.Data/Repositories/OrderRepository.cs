using Library.Domain.Core.Models;
using Library.Domain.Interfaces.Repositories;
using System.Data.Entity;
using System.Linq;

namespace Library.Infrastructure.Data.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(LibraryDbContext context)
            : base(context)
        {
        }

        public override Order GetById(object id)
        {
            return this.entities.Include(f => f.Book).FirstOrDefault(z => z.Id == id.ToString());
        }
    }
}
