namespace Library.Infrastructure.Data.Migrations
{
    using Library.Domain.Core.Models;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Library.Infrastructure.Data.LibraryDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Library.Infrastructure.Data.LibraryDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if (!context.Books.Any())
            {
                string path = @"D:\VS_Projects\Library.Sidorov\Library.Infrastructure.Data\Seeds\JsonData\BooksStartData.json";
                using (StreamReader jsonData = new StreamReader(path))
                {
                    var books = JsonConvert.DeserializeObject<IEnumerable<Book>>(jsonData.ReadToEnd());
                    foreach (Book item in books)
                    {
                        context.Books.Add(item);
                    }
                }
            }
        }
    }
}
