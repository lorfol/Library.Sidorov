﻿using AutoMapper;
using Library.App.ViewModels;
using Library.Domain.Core.Models;
using Library.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using System.Linq.Dynamic;

namespace Library.App.Controllers
{
    public class HomeController : Controller
    {
        IUnitOfWork unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // return view to display list of books with paging
        // searching and ordering books
        public ActionResult Index(string search, int? page, string orderBy = "Name", string orderDirection = "asc")
        {
            IPagedList<BookViewModel> listOfBooks;

            ViewBag.PreviosOrderBy = orderBy;
            ViewBag.PreviosOrderDirection = orderDirection;

            if (orderBy == "Author")
            {
                var authors = this.unitOfWork.Books.GetAll().SelectMany(b => b.Authors).ToList();
                var orderedAuthors = authors.OrderBy(string.Join(" ", "Name", orderDirection)).Distinct().ToList();
                var unicBooks = orderedAuthors.SelectMany(s => s.Books).Distinct();
                listOfBooks = Mapper.Map<IEnumerable<Book>, IEnumerable<BookViewModel>>(unicBooks).ToPagedList(page ?? 1, 10);

                return View(listOfBooks);
            }

            if (orderBy == "Publisher")
            {
                var publishers = this.unitOfWork.Books.GetAll().Select(b => b.Publisher).ToList();
                var orderedPublishers = publishers.OrderBy(string.Join(" ", "Name", orderDirection)).Distinct().ToList();
                var unicBooks = orderedPublishers.SelectMany(s => s.Books).Distinct();
                listOfBooks = Mapper.Map<IEnumerable<Book>, IEnumerable<BookViewModel>>(unicBooks).ToPagedList(page ?? 1, 10);

                return View(listOfBooks);
            }

            if (!string.IsNullOrEmpty(search))
            {
                var books = this.unitOfWork.Books.Find(f => f.Name.ToLower().Contains(search) ||
                f.Authors.Select(z => z).Any(g => g.Name.ToLower().Contains(search))).OrderBy(string.Join(" ", orderBy, orderDirection)).ToPagedList(page ?? 1, 10);
                listOfBooks = Mapper.Map<IEnumerable<Book>, IEnumerable<BookViewModel>>(books).ToPagedList(page ?? 1, 10);

                return View(listOfBooks);
            }
            else
            {
                var books = unitOfWork.Books.GetAll().OrderBy(string.Join(" ", orderBy, orderDirection)).ToList();
                listOfBooks = Mapper.Map<IEnumerable<Book>, IEnumerable<BookViewModel>>(books).ToPagedList(page ?? 1, 10);
                return View(listOfBooks);
            }
        }

        // return view details about book
        public ActionResult Details(int bookId)
        {
            var bookFromDb = unitOfWork.Books.GetById(bookId);

            if (bookFromDb != null)
            {
                var book = Mapper.Map<Book, BookViewModel>(bookFromDb);
                return View("Details", book);
            }

            return HttpNotFound();
        }
    }
}