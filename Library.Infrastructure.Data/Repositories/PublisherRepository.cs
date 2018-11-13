using Library.Domain.Core.Models;
using Library.Domain.Interfaces.Repositories;

namespace Library.Infrastructure.Data.Repositories
{
    public class PublisherRepository : BaseRepository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(LibraryDbContext context) : base(context)
        {
        }
    }
}
