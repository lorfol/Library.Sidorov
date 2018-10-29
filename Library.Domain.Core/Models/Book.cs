using System;
using System.Collections.Generic;

namespace Library.Domain.Core.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //public int AuthorId { get; set; }

        //public virtual Author Author { get; set; }

        public string Description { get; set; }

        public int Count { get; set; }

        public DateTime PublicationDate { get; set; }

        public virtual IEnumerable<Order> Orders { get; set; }
    }
}
