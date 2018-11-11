﻿using Library.App.Utils.Validation;
using Library.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.App.ViewModels
{
    public class BookUpdateViewModel
    {
        public BookUpdateViewModel()
        {
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<int> SelectedAuthors { get; set; }

        public MultiSelectList MultiSelectedAuthors { get; set; }

        public SelectList SelectedPublishers { get; set; }

        public int SelectedPublisher { get; set; }

        public string Description { get; set; }

         // TODO: !!!!!!!!!!! atrib
        public int Count { get; set; }

        public DateTime PublicationDate { get; set; }
    }
}