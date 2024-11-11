using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAppMVC.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        [StringLength(70)]
        public string Titolo { get; set; }
        [Display(Name = "Quantità")]
        public int Quantita { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Data di Pubblicazione")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "date")]
        public DateTime DataPubblicazione { get; set; }
        public string Genere { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal Prezzo { get; set; }

        //many-to-one
        public int? AuthorId { get; set; }
        public Author? Author { get; set; }
        //many-to-many
        //public IList<LoanBook>? LoanBooks { get; set; }


    }
}
