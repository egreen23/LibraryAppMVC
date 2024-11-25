using System.ComponentModel.DataAnnotations;

namespace LibraryAppMVC.Models
{
    public class Review
    {
        public int Id { get; set; }
        [Required]
        public string Testo { get; set; }

        public ApplicationUser? User { get; set; }

        public string? UserID { get; set; }

        public Book? Book { get; set; }

        public int? BookId { get; set; }
    }
}
