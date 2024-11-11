
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAppMVC.Models
{
    public class AppUser : IdentityUser
    {
        [Required, StringLength(50)]
        public string Nome { get; set; }
        [Required, StringLength(50)]
        public string Cognome { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Data di Nascita")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "date")]
        public DateTime DoB { get; set; }
    }
}
