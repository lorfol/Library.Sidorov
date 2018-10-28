using Library.Domain.Core.Models;
using Library.Domain.Interfaces.Repositories;

namespace Library.Infrastructure.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(LibraryDbContext context)
            : base(context)
        {
        }
    }
}
