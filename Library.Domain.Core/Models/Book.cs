using Library.Domain.Core.Enums;
using System;
using System.Collections.Generic;

namespace Library.Domain.Core.Models
{
    public class Book
    {
        public Book()
        {
            Authors = new List<Author>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Author> Authors { get; set; }

        public int? PublisherId { get; set; }

        public virtual Publisher Publisher { get; set; }

        public string Description { get; set; }

        public int Count { get; set; }

        public DateTime PublicationDate { get; set; }

        public virtual IEnumerable<Order> Orders { get; set; }

        public BookStatus Status { get; set; }
    }
}
