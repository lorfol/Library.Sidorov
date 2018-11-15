using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Library.Domain.Core.Models;
using Library.Domain.Interfaces.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Library.Tests
{
    [TestClass]
    public class BaseRepositoryTest
    {
        public TestContext TestContext { get; set; }

        private Mock<IRepository<Author>> mockRepository;

        [TestInitialize]
        public void SetupContext()
        {
            this.mockRepository = new Mock<IRepository<Author>>();
            List<Author> authors = this.GetAuthors();

            this.mockRepository.Setup(mr => mr.GetAll()).Returns(authors);

            this.mockRepository.Setup(mr => mr.Create(It.IsAny<Author>())).Returns(
               (Author target) =>
               {
                   target.Id = authors.Count() + 1;
                   authors.Add(target);
                   return target;
               });

            this.mockRepository.Setup(f => f.Delete(It.IsAny<Author>()))
                .Callback((Author a) =>
                {
                    authors.Remove(a);
                });

            this.mockRepository.Setup(mr => mr.Update(It.IsAny<object>(), It.IsAny<Author>()))
                .Callback((object id, Author a) =>
                {
                    var author = authors.FirstOrDefault(f => f.Id == (int)id);
                    author = a;
                });

            this.mockRepository.Setup(f => f.GetById(It.IsAny<object>()))
                .Returns<object>(z => authors.FirstOrDefault(f => f.Id == (int)z));

            this.mockRepository.Setup(f => f.Find(It.IsAny<Expression<Func<Author, bool>>>()))
                .Returns<Expression<Func<Author, bool>>>(z =>
                authors.Where(Expression.Lambda<Func<Author, bool>>(z.Body, z.Parameters).Compile()));

        }

        private List<Author> GetAuthors()
        {
            List<Author> authors = new List<Author>()
            {
                new Author { Id = 1, Name = "Jeffrey Richter" },
                new Author { Id = 2, Name = "Adam Freeman" },
                new Author { Id = 3, Name = "John Seena" }
            };

            return authors;
        }

        [TestMethod]
        [DataRow(4)]
        public void CanInsertEntity(int id)
        {
            Author author = new Author { Name = "New Author" };
            this.mockRepository.Object.Create(author);
            var enititiesCount = this.mockRepository.Object.GetAll().Count();
            Assert.AreEqual(id, enititiesCount);
        }

        [TestMethod]
        [DataRow(2)]
        public void CanUpdateEntity(int id)
        {
            // Arrange
            Author author = this.mockRepository.Object.GetById(id);
            author.Name = "Updated Author";

            // Act
            this.mockRepository.Object.Update(id, author);
            Author updatedAuthor = this.mockRepository.Object.GetById(id);

            // Assert
            Assert.AreEqual("Updated Author", updatedAuthor.Name);
        }

        [TestMethod]
        [DataRow(3)]
        public void CanDeleteEntity(int id)
        {
            // Arrange
            Author author = this.mockRepository.Object.GetById(id);

            // Act
            this.mockRepository.Object.Delete(author);

            // Assert
            Assert.AreEqual(2, this.mockRepository.Object.GetAll().Count());
        }

        [TestMethod]
        public void ShouldReturnThreeItems()
        {
            // Arrange
            IEnumerable<Author> authors = this.mockRepository.Object.GetAll();

            // Assert
            Assert.AreEqual(3, authors.Count());
        }
    }
}
