using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAppMVC.ViewModels.Book
{
    public class CreateBookViewModel
    {
        public string Titolo { get; set; }
        [Display(Name = "Quantità")]
        public int Quantita { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Data di Pubblicazione")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataPubblicazione { get; set; }
        public string Genere { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal Prezzo { get; set; }
        public int? AuthorId { get; set; }
        public SelectList? AuthorsList { get; set; }
    }
}
