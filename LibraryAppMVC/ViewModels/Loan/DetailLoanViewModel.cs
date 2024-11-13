using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LibraryAppMVC.Models;

namespace LibraryAppMVC.ViewModels.Loan
{
    public class DetailLoanViewModel
    {
        public LibraryAppMVC.Models.Loan? aLoan { get; set; }

        public List<BookLoanStruct>? bookList { get; set; }


    }
}
