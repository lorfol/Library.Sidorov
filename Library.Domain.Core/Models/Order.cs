using Library.Domain.Core.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Domain.Core.Models
{
    public class Order
    {
        public string Id { get; set; }

        public int BookId { get; set; }

        public virtual Book Book { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime TakenDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public float LateFine { get; set; }

        public bool IsAtReadingRoom { get; set; }
    }
}
