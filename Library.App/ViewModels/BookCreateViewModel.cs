using Library.App.Utils.Validation;
using Library.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
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

        [Required]
        public string Name { get; set; }

        public List<int> SelectedAuthors { get; set; }

        public MultiSelectList MultiSelectedAuthors { get; set; }

        public SelectList SelectedPublishers { get; set; }

        public int SelectedPublisher { get; set; }

        public string Description { get; set; }

        //[IntegerValidator(MinValue = 0)]
        public int Count { get; set; }

        public string PublicationYear { get; set; }
    }
}