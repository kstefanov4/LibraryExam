using Library.Models;

namespace Library.Contracts
{
    public interface IBookService
    {
        Task<IEnumerable<AllBookViewModelcs>> GetAllAsync();
        Task<MineViewModel> GetMineBooksAsync(string userId);
        Task AddBookToCollectionAsync(int bookId, string userId);
        Task RemoveBookFromCollectionAsync(int bookId, string userId);
    }
}
