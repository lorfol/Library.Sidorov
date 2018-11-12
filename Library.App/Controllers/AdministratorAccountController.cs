using Library.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using PagedList;
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

        public ActionResult ManageCreateBook()
        {
            BookCreateViewModel viewModel = new BookCreateViewModel();

            var authors = this.unitOfWork.Authors.GetAll().ToList();
            var publishers = this.unitOfWork.Publishers.GetAll().ToList();

            viewModel.MultiSelectedAuthors = new MultiSelectList(authors, "Id", "Name");
            viewModel.SelectedPublishers = new SelectList(publishers, "Id", "Name");

            return View(viewModel);
        }

        public ActionResult ManageUpdateBook(int bookId)
        {
            BookUpdateViewModel viewModel = new BookUpdateViewModel();

            var book = this.unitOfWork.Books.GetById(bookId);

            viewModel = Mapper.Map<Book, BookUpdateViewModel>(book);

            var authors = this.unitOfWork.Authors.GetAll().ToList();
            var publishers = this.unitOfWork.Publishers.GetAll().ToList();

            viewModel.SelectedPublishers = new SelectList(publishers, "Id", "Name", book.PublisherId);

            viewModel.SelectedAuthors = book.Authors?
                .Where(aut => authors.Select(t => t.Id).Any(n => n == aut.Id))
                .Select(f => f.Id).ToList();

            viewModel.MultiSelectedAuthors = new MultiSelectList(authors, "Id", "Name", viewModel.SelectedAuthors);

            ViewBag.Id = bookId;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateBook(BookCreateViewModel book)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(422);
            }
            var newBook = Mapper.Map<BookCreateViewModel, Book>(book);
            newBook.Authors = this.unitOfWork.Authors.Find(f => book.SelectedAuthors.Any(p => p == f.Id)).ToList();
            this.unitOfWork.Books.Create(newBook);
            this.unitOfWork.Save();

            return RedirectToAction("Index", "Home");
        }

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
            var newAuthorsIds = book.SelectedAuthors.Except(bookFromDb.Authors.Select(s => s.Id));
            var newAuthors = this.unitOfWork.Authors.Find(f => newAuthorsIds.Any(a => a == f.Id));
            bookFromDb.Authors.AddRange(newAuthors);
            this.unitOfWork.Books.Update(bookFromDb.Id, bookFromDb);
            this.unitOfWork.Save();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult DeleteBook(int bookId)
        {
            this.unitOfWork.Books.Delete(this.unitOfWork.Books.GetById(bookId));
            this.unitOfWork.Save();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ManageCreateAuthor()
        {
            AuthorCreateViewModel authorCreateViewModel = new AuthorCreateViewModel();

            return View(authorCreateViewModel);
        }

        public ActionResult ManageCreatePublisher()
        {
            PublisherCreateViewModel publisherCreateViewModel = new PublisherCreateViewModel();

            return View(publisherCreateViewModel);
        }

        [HttpPost]
        public ActionResult CreateAuthor(AuthorCreateViewModel viewModel)
        {
            var newAuthor = Mapper.Map<AuthorCreateViewModel, Author>(viewModel);
            this.unitOfWork.Authors.Create(newAuthor);
            this.unitOfWork.Save();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult CreatePublisher(PublisherCreateViewModel viewModel)
        {
            var newPublisher = Mapper.Map<PublisherCreateViewModel, Publisher>(viewModel);
            this.unitOfWork.Publishers.Create(newPublisher);
            this.unitOfWork.Save();

            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> ManageUsers()
        {
            var userRole = ApplicationUserManager.RoleManager.Roles.FirstOrDefault(f => f.Name == "user").Id;
            var users = await UserManager.Users.Where(t => t.Roles.Any(p => p.RoleId == userRole)).ToListAsync();

            return View(users);
        }

        public async Task<ActionResult> ManageLibrarians()
        {
            var role = ApplicationUserManager.RoleManager.Roles.FirstOrDefault(f => f.Name == "librarian").Id;
            var librarians = await UserManager.Users.Where(t => t.Roles.Any(p => p.RoleId == role)).ToListAsync();

            return View(librarians);
        }

        public async Task<RedirectToRouteResult> CreateLibrarian(string userId)
        {
            var user = UserManager.Users.FirstOrDefault(u => u.Id == userId);
            await UserManager.AddToRoleAsync(userId, "librarian");
            await UserManager.RemoveFromRoleAsync(userId, "user");

            return RedirectToAction("ManageUsers");
        }

        public async Task<RedirectToRouteResult> DeleteLibrarian(string userId)
        {
            var user = UserManager.Users.FirstOrDefault(u => u.Id == userId);
            await UserManager.AddToRoleAsync(userId, "user");
            await UserManager.RemoveFromRoleAsync(userId, "librarian");

            return RedirectToAction("ManageLibrarians");
        }

        public async Task<RedirectToRouteResult> BanUser(string userId)
        {
            var user = UserManager.Users.FirstOrDefault(u => u.Id == userId);

            if (!UserManager.IsInRole(userId, "banned"))
            {
                await UserManager.AddToRoleAsync(userId, "banned");
                user.IsBanned = true;
            }
            else if (UserManager.IsInRole(userId, "banned"))
            {
                user.IsBanned = true;
            }

            return RedirectToAction("ManageUsers");
        }

        public async Task<RedirectToRouteResult> UnbanUser(string userId)
        {
            var user = UserManager.Users.FirstOrDefault(u => u.Id == userId);

            if (UserManager.IsInRole(userId, "banned"))
            {
                await UserManager.RemoveFromRoleAsync(userId, "banned");
                user.IsBanned = false;
            }
            else if (!UserManager.IsInRole(userId, "banned"))
            {
                user.IsBanned = false;
            }

            return RedirectToAction("ManageUsers");
        }
    }
}