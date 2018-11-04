using Library.Domain.Core.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Library.Infrastructure.Data
{
    public class LibraryDbContext: IdentityDbContext<User>
    {
        public LibraryDbContext(): base("DefaultConnection")
        {}

        public DbSet<Book> Books { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Publisher> Publishers { get; set; }

        public static LibraryDbContext Create()
        {
            return new LibraryDbContext();
        }
    }
}
