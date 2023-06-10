using Library.Contracts;
using Library.Data;
using Library.Data.Models;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext context;

        public BookService(LibraryDbContext _context)
        {
            context = _context;
        }

        public async Task AddBookToCollectionAsync(int bookId, string userId)
        {
            var user = await context.Users
                .Where(x => x.Id == userId)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            var book = await context.Books.FirstOrDefaultAsync(u => u.Id == bookId);

            if (book == null)
            {
                throw new ArgumentException("Invalid Book ID");
            }

            if (!context.IdentityUserBooks.Any(m => m.BookId == bookId && m.CollectorId == userId))
            {
                context.IdentityUserBooks.Add(new IdentityUserBook()
                {
                    BookId = book.Id,
                    CollectorId = user.Id,
                    Book = book,
                    Collector = user
                });

                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<AllBookViewModelcs>> GetAllAsync()
        {
            var entities = await context.Books
                .Include(x => x.Category)
                .ToListAsync();

            return entities.Select(x => new AllBookViewModelcs
            {
                Title = x.Title,
                Author = x.Author,
                Rating = x.Rating,
                Id = x.Id,
                ImageUrl = x.ImageUrl,
                Category = x?.Category?.Name
            });
        }
    }
}
