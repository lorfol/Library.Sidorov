using Library.Domain.Core.Enums;
using System;

namespace Library.Domain.Core.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public virtual Book Book { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime TakenDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public float LateFine { get; set; }
    }
}
