using Library.Data.Models;

namespace Library.Models
{
    public class MineViewModel
    {
        public IEnumerable<Book> Books { get; set; } = new List<Book>();
    }
}
