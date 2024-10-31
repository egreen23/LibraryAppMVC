using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryAppMVC.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Titolo { get; set; }
        [Display(Name = "Quantità")]
        public int Quantita { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Data di Pubblicazione")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataPubblicazione { get; set; }
        public string Genere { get; set; } 
        public decimal Prezzo { get; set; }
        [Display(Name = "Autore")]
        public string AuthorFullname { get; set; }

        public int? AuthorId { get; set; }
    }
}
