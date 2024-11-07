
using LibraryAppMVC.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryAppMVC.ViewModels.Loan
{
    public struct DetailLoanStruct
    {
        public int Id { get; set; }
        public decimal Totale { get; set; }
        public DateTime DataInizio { get; set; }
        public DateTime DataFine { get; set; }
        public string Titolo { get; set; }
        public decimal Prezzo { get; set; }

    }
}
