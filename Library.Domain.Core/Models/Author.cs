using System.Collections.Generic;

namespace Library.Domain.Core.Models
{
    public class Author
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual IEnumerable<Book> Books { get; set; }
    }
}
