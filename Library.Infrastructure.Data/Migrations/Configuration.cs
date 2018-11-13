namespace Library.Infrastructure.Data.Migrations
{
    using Library.Domain.Core.Models;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Web.Hosting;

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

            if (!context.Authors.Any())
            {
                string file = HostingEnvironment.MapPath(@"/Library.Infrastructure.Data/Seeds/JsonData/AuthorsStartData.json");
                string p = "..\\Library\\Library.Infrastructure.Data\\Seeds\\JsonData\\AuthorsStartData.json";
                string path = @"D:\VS_Projects\Library\Library.Infrastructure.Data\Seeds\JsonData\AuthorsStartData.json";
                var authors = JsonConvert.DeserializeObject<IEnumerable<Author>>(File.ReadAllText(path));
                foreach (Author item in authors)
                {
                    context.Authors.Add(item);
                }

                context.SaveChanges();
            }

            if (!context.Publishers.Any())
            {
                string file = HostingEnvironment.MapPath(@"/Library.Infrastructure.Data/Seeds/JsonData/AuthorsStartData.json");
                string p = "..\\Library\\Library.Infrastructure.Data\\Seeds\\JsonData\\AuthorsStartData.json";
                string path = @"D:\VS_Projects\Library\Library.Infrastructure.Data\Seeds\JsonData\PublishersStartData.json";
                var publishers = JsonConvert.DeserializeObject<IEnumerable<Publisher>>(File.ReadAllText(path));
                foreach (Publisher item in publishers)
                {
                    context.Publishers.Add(item);
                }

                context.SaveChanges();
            }

            if (!context.Books.Any())
            {
                string file = HostingEnvironment.MapPath(@"/Library.Infrastructure.Data/Seeds/JsonData/BooksStartData.json");
                string p = "..\\Library\\Library.Infrastructure.Data\\Seeds\\JsonData\\BooksStartData.json";
                string path = @"D:\VS_Projects\Library\Library.Infrastructure.Data\Seeds\JsonData\BooksStartData.json";

                var books = JsonConvert.DeserializeObject<IEnumerable<Book>>(File.ReadAllText(path));
                foreach (Book item in books)
                {
                    var authors = new List<Author>();
                    item.Authors.ToList().ForEach(f =>
                    {
                        var authorFromDb = context.Authors.Find(f.Id);
                        authors.Add(authorFromDb);
                    });
                    item.Authors = authors;
                    context.Books.Add(item);
                }

                context.SaveChanges();
            }
        }
    }
}
