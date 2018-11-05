using Library.Domain.Core.Models;
using Library.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Data.Repositories
{
    public class PublisherRepository : BaseRepository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(LibraryDbContext context) : base(context)
        {
        }
    }
}
