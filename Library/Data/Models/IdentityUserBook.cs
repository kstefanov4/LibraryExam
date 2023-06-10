using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Data.Models
{
    [Comment("IdentytiUserBooks")]
    public class IdentityUserBook
    {
        [Required]
        public string CollectorId { get; set;}

        [ForeignKey(nameof(CollectorId))]
        public IdentityUser Collector { get; set; }

        [Required]
        public int BookId { get; set; }

        [ForeignKey(nameof(BookId))]
        public Book Book { get; set; }
    }
}
