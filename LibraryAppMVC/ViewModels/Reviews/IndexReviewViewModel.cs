using System.ComponentModel.DataAnnotations;

namespace LibraryAppMVC.ViewModels.Reviews
{
    public class IndexReviewViewModel
    {
        public int Id { get; set; }

        public string Testo { get; set; }
        [Display(Name = "Titolo")]
        public string TitoloBook { get; set; }
        public int BookId { get; set; }

        public string UserName { get; set; }
    }
}
