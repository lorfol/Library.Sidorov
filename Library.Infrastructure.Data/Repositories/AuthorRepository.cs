using Library.Domain.Core.Models;
using Library.Domain.Interfaces.Repositories;

namespace Library.Infrastructure.Data.Repositories
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(LibraryDbContext context) : base(context)
        {
        }
    }
}
