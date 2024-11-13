namespace LibraryAppMVC.ViewModels.Loan
{
    public class LoanItemViewModel
    {
        public int BookId { get; set; }
        public string Titolo { get; set; }

        public decimal Prezzo { get; set; }

        public int? AuthorId { get; set; }
    }
}
