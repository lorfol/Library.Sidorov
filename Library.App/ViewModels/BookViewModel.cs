using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.App.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Publisher { get; set; }

        public string Description { get; set; }

        public int Count { get; set; }

        public DateTime PublicationDate { get; set; }

        public string Status { get; set; }
    }
}