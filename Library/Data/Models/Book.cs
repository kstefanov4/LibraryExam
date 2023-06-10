using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Data.Models
{
    [Comment("Library book")]
    public class Book
    {
        [Key]
        [Comment("Book Id")]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Comment("Book Title")]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(50)]
        [Comment("Book Author")]
        public string Author { get; set; } = null!;

        [Required]
        [StringLength(5000)]
        [Comment("Book Description")]
        public string Description { get; set; } = null!;

        [Required]
        [Comment("Book ImageUrl")]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Comment("Book Rating")]
        public decimal Rating { get; set; }

        [Required]
        [Comment("Book CategoryId")]
        public int CategoryId { get; set; }

        [Comment("Book Category")]
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

        [Comment("Book UsersBooks")]
        IEnumerable<IdentityUserBook> UsersBooks = new List<IdentityUserBook>();
    }
}
