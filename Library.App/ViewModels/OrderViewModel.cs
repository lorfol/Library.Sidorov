using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.App.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public int UserId { get; set; }

        public string Status { get; set; }

        public DateTime TakenDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public float LateFine { get; set; }
    }
}