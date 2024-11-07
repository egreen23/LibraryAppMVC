using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryAppMVC.ViewModels.Loan
{
    public class IndexLoanViewModel
    {
        public int Id { get; set; }
        public decimal Totale { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Data Inizio")]
        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataInizio { get; set; } = DateTime.Now;
        [DataType(DataType.Date)]
        [Display(Name = "Data Fine")]
        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataFine { get; set; }
        [Display(Name = "Utente")]
        public string UserFullName { get; set; }
    }
}
