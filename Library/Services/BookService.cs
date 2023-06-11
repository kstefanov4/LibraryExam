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

        public async Task<MineViewModel> GetMineBooksAsync(string userId)
        {
            var entities = await context.IdentityUserBooks
                .Where(x => x.CollectorId == userId)
                .Select(x => new BookViewModel
                {
                    Title = x.Book.Title,
                    Author = x.Book.Author,
                    Id = x.Book.Id,
                    ImageUrl = x.Book.ImageUrl,
                    Description = x.Book.Description,
                    Category = x.Book.Category.Name
                }).ToListAsync();

            return new MineViewModel
            {
                Books = entities
            };
            
        }

        public async Task RemoveBookFromCollectionAsync(int bookId, string userId)
        {
            var userBook = await context.IdentityUserBooks
                .FirstOrDefaultAsync(x => x.BookId == bookId && x.CollectorId == userId);

            if (userBook != null)
            {
                context.IdentityUserBooks.Remove(userBook);
                await context.SaveChangesAsync();
            }
        }
    }
}
