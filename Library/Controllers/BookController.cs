using Library.Contracts;
using Microsoft.AspNetCore.Mvc;
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
