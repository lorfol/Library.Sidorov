namespace Library.Infrastructure.Data.Migrations
{
    using Library.Domain.Core.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Web;
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

            //if (System.Diagnostics.Debugger.IsAttached == false)
            //    System.Diagnostics.Debugger.Launch();

            //if (!context.Authors.Any())
            //{
            //    string path = @"D:\VS_Projects\Library.Sidorov\Library.Infrastructure.Data\Seeds\JsonData\AuthorsStartData.json";
            //    using (StreamReader jsonData = new StreamReader(path))
            //    {
            //        var authors = JsonConvert.DeserializeObject<IEnumerable<Author>>(jsonData.ReadToEnd());
            //        foreach (Author item in authors)
            //        {
            //            context.Authors.Add(item);
            //        }
            //        var x = context.Authors.ToList();
            //        context.SaveChanges();
            //    }
            //}

            // -------------------------------------------------

            //if (!context.Books.Any())
            //{
            //    string file = HostingEnvironment.MapPath(@"/Library.Infrastructure.Data/Seeds/JsonData/BooksStartData.json");
            //    string p = "..\\Library.Sidorov\\Library.Infrastructure.Data\\Seeds\\JsonData\\BooksStartData.json";
            //    string path = @"D:\VS_Projects\Library.Sidorov\Library.Infrastructure.Data\Seeds\JsonData\BooksStartData.json";
            //    using (StreamReader jsonData = new StreamReader(file))
            //    {
            //        var books = JsonConvert.DeserializeObject<IEnumerable<Book>>(jsonData.ReadToEnd());
            //        foreach (Book item in books)
            //        {
            //            context.Books.Add(item);
            //        }
            //    }
            //}
        }
    }
}
