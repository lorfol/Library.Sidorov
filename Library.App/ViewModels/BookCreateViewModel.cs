using Library.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.App.ViewModels
{
    public class BookCreateViewModel
    {
        public BookCreateViewModel()
        {
        }

        public BookCreateViewModel(List<Publisher> publishers, List<Author> authors)
        {
            this.Authors = authors;
            this.Publishers = publishers;
            this.SelectedAuthors = new List<int>();
        }

        public string Name { get; set; }

        public List<int> SelectedAuthors { get; set; }

        public MultiSelectList MultiSelectedAuthors { get; set; }

        public virtual List<Author> Authors { get; set; }

        public int SelectedPublisher { get; set; }

        public virtual List<Publisher> Publishers { get; set; }

        public string Description { get; set; }

        public int Count { get; set; }

        public DateTime PublicationDate { get; set; }
    }
}