using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Core.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Count { get; set; }

        public DateTime PublicationDate { get; set; }

        public virtual IEnumerable<Order> Orders { get; set; }
    }
}
