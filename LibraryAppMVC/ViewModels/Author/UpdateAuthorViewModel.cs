using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryAppMVC.ViewModels.Author
{
    public class UpdateAuthorViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        [Display(Name = "Nazionalità")]
        public string Nazionalita { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        [Display(Name = "Data di Nascita")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DoB { get; set; }
        [Display(Name = "Luogo di Nascita")]
        public string LuogoNascita { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        [Display(Name = "Data di Morte")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DoD { get; set; }

        public bool isAlive { get; set; }
    }
}
