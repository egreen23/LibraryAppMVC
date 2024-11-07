using LibraryAppMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryAppMVC.ViewModels.Loan
{
    public class CreateLoanViewModel
    {
        public DateTime DataInizio { get; set; }

        public DateTime DataFine { get; set; }

        public SelectList? Books { get; set; }

        public int? BookId { get; set; }

        public List<LibraryAppMVC.Models.Book> booksAdded { get; set; } = [];
        public decimal Totale { get; set; }
    }
}
