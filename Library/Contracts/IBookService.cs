using Library.Models;

namespace Library.Contracts
{
    public interface IBookService
    {
        Task<IEnumerable<AllBookViewModelcs>> GetAllAsync();
        Task AddBookToCollectionAsync(int bookId, string userId);
    }
}
