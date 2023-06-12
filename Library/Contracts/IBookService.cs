using Library.Data.Models;
using Library.Models;

namespace Library.Contracts
{
    public interface IBookService
    {
        Task<IEnumerable<AllBookViewModelcs>> GetAllAsync();
        Task<AddBookViewModel?> GetBookById(int id);
        Task<MineViewModel> GetMineBooksAsync(string userId);
        Task AddBookToCollectionAsync(int bookId, string userId);
        Task AddBookAsync(AddBookViewModel model);
        Task EditBookAsync(int id, AddBookViewModel model);
        Task<IEnumerable<Category>> GetAllCategory();
        Task RemoveBookFromCollectionAsync(int bookId, string userId);
    }
}
