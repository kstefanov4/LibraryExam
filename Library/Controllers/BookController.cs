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

        [HttpPost]
        public async Task<IActionResult> AddToCollection(int id)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                await bookService.AddBookToCollectionAsync(id, userId);
            }
            catch (Exception)
            {

                throw;
            }

            return View();
        }
    }
}
