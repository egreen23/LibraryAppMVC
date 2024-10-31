using System.ComponentModel.DataAnnotations;

namespace LibraryAppMVC.Models
{
    public class UserAddress
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Indirizzo { get; set; }
        [Required, StringLength(30), Display(Name = "Città")]
        public string Citta { get; set; }
        [Required, StringLength(30)]
        public string Stato { get; set; }

        //one-to-one relationship

        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
