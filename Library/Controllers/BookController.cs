using Library.Contracts;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Security.Claims;

namespace Library.Controllers
{
    public class BookController : BaseController
    {
        private readonly IBookService bookService;

        public BookController(IBookService _bookService)
        {
            bookService = _bookService;
        }
        public async Task<IActionResult> All()
        {
            var model = await bookService.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            AddBookViewModel? book = await bookService.GetBookById(id);

            if (book == null)
            {
                return RedirectToAction(nameof(All));
            }

            var categoties = await bookService.GetAllCategory();

            book.Categories = categoties;

            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddBookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await bookService.EditBookAsync(id, model);
                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong");

                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await bookService.AddBookAsync(model);
                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong");

                return View(model);
            }
        }

        public async Task<IActionResult> Add()
        {
            var model = new AddBookViewModel();

            var categoties = await bookService.GetAllCategory();

            model.Categories = categoties;

            return View(model);
        }

        public async Task<IActionResult> RemoveFromCollection(int id)
        {
            try
            {
                await bookService.RemoveBookFromCollectionAsync(id, GetUserId());
            }
            catch (ArgumentException)
            {
            }

            return RedirectToAction("Mine");
        }

        public async Task<IActionResult> Mine()
        {
            var model = await bookService.GetMineBooksAsync(GetUserId());
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCollection(int id)
        {
            try
            {
                await bookService.AddBookToCollectionAsync(id, GetUserId());
            }
            catch (Exception)
            {

                throw;
            }

            return RedirectToAction("All");
        }
    }
}
