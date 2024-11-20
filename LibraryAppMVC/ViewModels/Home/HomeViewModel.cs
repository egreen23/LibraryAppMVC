namespace LibraryAppMVC.ViewModels.Home
{
    public class HomeViewModel
    {
        public IEnumerable<BookDashboard> books { get; set; }

        public IEnumerable<AuthorDashboard> authors { get; set; }
    }
}
