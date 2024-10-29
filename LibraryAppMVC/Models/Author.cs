using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAppMVC.Models
{
    public class Author
    {
        public int Id { get; set; }
        [Required, StringLength(30)]
        public string Nome { get; set; } 
        [Required, StringLength(50)]
        public string Cognome { get; set; }
        [Display(Name = "Nazionalità")]
        public string Nazionalita { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        [Display(Name = "Data di Nascita")]
        [DisplayFormat(DataFormatString = "{0:dd-mm-yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "date")]
        public DateTime DoB { get; set; }
        [Display(Name = "Luogo di Nascita")]
        public string LuogoNascita { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        [Display(Name = "Data di Morte")]
        [DisplayFormat(DataFormatString = "{0:dd-mm-yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "date")]
        public DateTime? DoD { get; set; }

        //navigation property one-to-many
        public IList<Book> Books { get; set; }


    }
}
