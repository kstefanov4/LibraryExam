using Library.Data.Models;

namespace Library.Models
{
    public class MineViewModel
    {
        public IEnumerable<BookViewModel> Books { get; set; } = new List<BookViewModel>();
    }
}
