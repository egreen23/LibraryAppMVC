using LibraryAppMVC.Models;

namespace LibraryAppMVC.ViewModels.Author
{
    public class IndexAuthorViewModel
    {
        public IEnumerable<LibraryAppMVC.Models.Author> Authors { get; set; }

        public string NameSortOrder { get; set; }
        public string DateSortOrder { get; set; }

        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string Term { get; set; }
        public string OrderBy { get; set; }

        //public string currentSort { get; set; }

        //public string currentFilter { get; set; }


    }
}
