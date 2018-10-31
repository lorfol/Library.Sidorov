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

        public string Description { get; set; }

        public int Count { get; set; }

        public DateTime PublicationDate { get; set; }

        public virtual IEnumerable<Order> Orders { get; set; }

        //  TODO: mb add field 'BookStatus' to manage is book available or in reeding room or out of library
    }
}
