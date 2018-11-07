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

        public string UserId { get; set; }

        public string Status { get; set; }

        public string TakenDate { get; set; }

        public string ReturnDate { get; set; }

        public float LateFine { get; set; }

        public bool IsInReadingRoom { get; set; }
    }
}