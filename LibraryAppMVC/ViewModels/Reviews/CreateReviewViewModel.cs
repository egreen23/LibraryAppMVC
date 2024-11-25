using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryAppMVC.ViewModels.Reviews
{
    public class CreateReviewViewModel
    {
        public string Testo { get; set; }
        public string? UserId { get; set; }
        public int? BookId { get; set; }
        public SelectList? BooksList { get; set; }
    }
}
