using Library.Domain.Core.Models;
using Library.Domain.Interfaces.Repositories;

namespace Library.Infrastructure.Data.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(LibraryDbContext context) : base(context)
        {
        }
    }
}
