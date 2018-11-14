using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Library.App.Controllers;
using Library.App.ViewModels;
using Library.Domain.Core.Models;
using Library.Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Library.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        [DataRow("harry", 1, "Name", "asc")]
        public void IndexViewResultShouldReturnTenBooks(string search, int? page, string orderBy, string orderDirection)
        {
            // Arrange
            var mock = new Mock<IUnitOfWork>();
            List<Book> books = new List<Book>()
            {
                 new Book { Name = "Harry potter" },
                 new Book { Name = "Via C#" }
            };

            mock.Setup(f => f.Books.Find(It.IsAny<Expression<Func<Book, bool>>>()))
                .Returns((Expression<Func<Book, bool>> b) => books.Where(f => f.Name.ToLower().Contains(search) ||
                f.Authors.Select(z => z).Any(g => g.Name.ToLower().Contains(search))));

            var controller = new HomeController(mock.Object);

            // Act
            var result = controller.Index(search, page, orderBy, orderDirection) as ViewResult;

            // Assert
            var x1 = result.Model as IEnumerable<BookViewModel>;
            Assert.AreEqual(1, x1.ToList().Count);
        }
    }
}
