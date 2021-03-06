﻿using Library.Domain.Interfaces;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using Library.Domain.Core.Models;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Library.Infrastructure.Data;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using Library.App.ViewModels;
using AutoMapper;

namespace Library.App.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdministratorAccountController : Controller
    {
        private ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

        IUnitOfWork unitOfWork;

        public AdministratorAccountController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // return view for creating new book
        public ActionResult ManageCreateBook()
        {
            BookCreateViewModel viewModel = new BookCreateViewModel();

            var authors = this.unitOfWork.Authors.GetAll().OrderBy(a => a.Name).ToList();
            var publishers = this.unitOfWork.Publishers.GetAll().OrderBy(p => p.Name).ToList();

            viewModel.MultiSelectedAuthors = new MultiSelectList(authors, "Id", "Name");
            viewModel.SelectedPublishers = new SelectList(publishers, "Id", "Name");

            return View(viewModel);
        }

        // return view for updating book
        public ActionResult ManageUpdateBook(int bookId)
        {
            BookUpdateViewModel viewModel = new BookUpdateViewModel();

            var book = this.unitOfWork.Books.GetById(bookId);

            viewModel = Mapper.Map<Book, BookUpdateViewModel>(book);

            var authors = this.unitOfWork.Authors.GetAll().OrderBy(a => a.Name).ToList();
            var publishers = this.unitOfWork.Publishers.GetAll().OrderBy(p => p.Name).ToList();

            viewModel.SelectedPublishers = new SelectList(publishers, "Id", "Name", book.PublisherId);

            viewModel.SelectedAuthors = book.Authors?
                .Where(aut => authors.Select(t => t.Id).Any(n => n == aut.Id))
                .Select(f => f.Id).ToList();

            viewModel.MultiSelectedAuthors = new MultiSelectList(authors, "Id", "Name", viewModel.SelectedAuthors);

            ViewBag.Id = bookId;

            return View(viewModel);
        }

        // creating new book and save it in db
        [HttpPost]
        public ActionResult CreateBook(BookCreateViewModel book)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(422);
            }

            var newBook = Mapper.Map<BookCreateViewModel, Book>(book);

            if (book.SelectedAuthors != null)
            {
                newBook.Authors = this.unitOfWork.Authors.Find(f => book.SelectedAuthors.Any(p => p == f.Id)).ToList();
            }

            this.unitOfWork.Books.Create(newBook);
            this.unitOfWork.Save();

            return RedirectToAction("Index", "Home");
        }

        // updating new book and save it in db
        [HttpPost]
        public ActionResult UpdateBook(BookUpdateViewModel book)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(422);
            }
            var bookFromDb = this.unitOfWork.Books.GetById(book.Id);
            Mapper.Map<BookUpdateViewModel, Book>(book, bookFromDb);
            bookFromDb.Authors.RemoveAll(a => book.SelectedAuthors.All(f => f != a.Id));

            if (book.SelectedAuthors != null)
            {
                var newAuthorsIds = book.SelectedAuthors.Except(bookFromDb.Authors.Select(s => s.Id));
                var newAuthors = this.unitOfWork.Authors.Find(f => newAuthorsIds.Any(a => a == f.Id));
                bookFromDb.Authors.AddRange(newAuthors);
            }

            this.unitOfWork.Books.Update(bookFromDb.Id, bookFromDb);
            this.unitOfWork.Save();

            return RedirectToAction("Index", "Home");
        }

        // delete book from db
        public ActionResult DeleteBook(int bookId)
        {
            this.unitOfWork.Books.Delete(this.unitOfWork.Books.GetById(bookId));
            this.unitOfWork.Save();

            return RedirectToAction("Index", "Home");
        }

        // return form for creating new author
        public ActionResult ManageCreateAuthor()
        {
            AuthorCreateViewModel authorCreateViewModel = new AuthorCreateViewModel();

            return View(authorCreateViewModel);
        }

        // return form for creating new publisher
        public ActionResult ManageCreatePublisher()
        {
            PublisherCreateViewModel publisherCreateViewModel = new PublisherCreateViewModel();

            return View(publisherCreateViewModel);
        }

        // creating new author and save it in db
        [HttpPost]
        public ActionResult CreateAuthor(AuthorCreateViewModel viewModel)
        {
            var newAuthor = Mapper.Map<AuthorCreateViewModel, Author>(viewModel);
            this.unitOfWork.Authors.Create(newAuthor);
            this.unitOfWork.Save();

            return RedirectToAction("Index", "Home");
        }

        // creating new publisher and save it in db
        [HttpPost]
        public ActionResult CreatePublisher(PublisherCreateViewModel viewModel)
        {
            var newPublisher = Mapper.Map<PublisherCreateViewModel, Publisher>(viewModel);
            this.unitOfWork.Publishers.Create(newPublisher);
            this.unitOfWork.Save();

            return RedirectToAction("Index", "Home");
        }

        // return list of users
        public async Task<ActionResult> ManageUsers()
        {
            var userRole = ApplicationUserManager.RoleManager.Roles.FirstOrDefault(f => f.Name == "user").Id;
            var users = await UserManager.Users.Where(t => t.Roles.Any(p => p.RoleId == userRole)).ToListAsync();

            return View(users);
        }

        // return list of librarians
        public async Task<ActionResult> ManageLibrarians()
        {
            var role = ApplicationUserManager.RoleManager.Roles.FirstOrDefault(f => f.Name == "librarian").Id;
            var librarians = await UserManager.Users.Where(t => t.Roles.Any(p => p.RoleId == role)).ToListAsync();

            return View(librarians);
        }

        // set librarian role to user
        public async Task<RedirectToRouteResult> CreateLibrarian(string userId)
        {
            var user = UserManager.Users.FirstOrDefault(u => u.Id == userId);
            await UserManager.AddToRoleAsync(userId, "librarian");
            await UserManager.RemoveFromRoleAsync(userId, "user");

            return RedirectToAction("ManageUsers");
        }

        // remove librarian role to user
        public async Task<RedirectToRouteResult> DeleteLibrarian(string userId)
        {
            var user = UserManager.Users.FirstOrDefault(u => u.Id == userId);
            await UserManager.AddToRoleAsync(userId, "user");
            await UserManager.RemoveFromRoleAsync(userId, "librarian");

            return RedirectToAction("ManageLibrarians");
        }

        // ban user
        public async Task<RedirectToRouteResult> BanUser(string userId)
        {
            var user = UserManager.Users.FirstOrDefault(u => u.Id == userId);

            if (!UserManager.IsInRole(userId, "banned"))
            {
                await UserManager.AddToRoleAsync(userId, "banned");
                user.IsBanned = true;
                UserManager.Update(user);
            }
            else if (UserManager.IsInRole(userId, "banned"))
            {
                user.IsBanned = true;
                UserManager.Update(user);
            }

            return RedirectToAction("ManageUsers");
        }

        // unban user
        public async Task<RedirectToRouteResult> UnbanUser(string userId)
        {
            var user = UserManager.Users.FirstOrDefault(u => u.Id == userId);

            if (UserManager.IsInRole(userId, "banned"))
            {
                await UserManager.RemoveFromRoleAsync(userId, "banned");
                user.IsBanned = false;
                UserManager.Update(user);
            }
            else if (!UserManager.IsInRole(userId, "banned"))
            {
                user.IsBanned = false;
                UserManager.Update(user);
            }

            return RedirectToAction("ManageUsers");
        }
    }
}