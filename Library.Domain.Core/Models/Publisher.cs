using System.Collections.Generic;

namespace Library.Domain.Core.Models
{
    public class Publisher
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
