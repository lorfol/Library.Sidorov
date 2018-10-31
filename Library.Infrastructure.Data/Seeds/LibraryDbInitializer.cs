using Library.Domain.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace Library.Infrastructure.Data.Seeds
{
    public class LibraryDbInitializer : CreateDatabaseIfNotExists<LibraryDbContext>
    {
        protected override void Seed(LibraryDbContext dbContext)
        {
            if (!dbContext.Authors.Any())
            {
                string file = HostingEnvironment.MapPath(@"/Library.Infrastructure.Data/Seeds/JsonData/AuthorsStartData.json");
                string p = "..\\Library.Sidorov\\Library.Infrastructure.Data\\Seeds\\JsonData\\AuthorsStartData.json";
                string path = @"D:\VS_Projects\Library.Sidorov\Library.Infrastructure.Data\Seeds\JsonData\AuthorsStartData.json";
                var authors = JsonConvert.DeserializeObject<IEnumerable<Author>>(File.ReadAllText(path));
                foreach (Author item in authors)
                {
                    dbContext.Authors.Add(item);
                }

                dbContext.SaveChanges();
            }

            if (!dbContext.Books.Any())
            {
                string file = HostingEnvironment.MapPath(@"/Library.Infrastructure.Data/Seeds/JsonData/BooksStartData.json");
                string p = "..\\Library.Sidorov\\Library.Infrastructure.Data\\Seeds\\JsonData\\BooksStartData.json";
                string path = @"D:\VS_Projects\Library.Sidorov\Library.Infrastructure.Data\Seeds\JsonData\BooksStartData.json";

                var books = JsonConvert.DeserializeObject<IEnumerable<Book>>(File.ReadAllText(path));
                foreach (Book item in books)
                {
                    var authors = new List<Author>();
                    item.Authors.ToList().ForEach(f =>
                    {
                        var authorFromDb = dbContext.Authors.Find(f.Id);
                        authors.Add(authorFromDb);
                    });
                    item.Authors = authors;
                    dbContext.Books.Add(item);
                }

                dbContext.SaveChanges();
            }
        }
    }
}
