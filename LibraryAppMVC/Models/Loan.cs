﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAppMVC.Models
{
    public class Loan
    {
        public Loan()
        {
            LoanBooks = new List<LoanBook>();
        }
        public int Id { get; set; }
        [Column(TypeName = "decimal(18,2)"), Required]
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
        //many-to-one
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
        //many-to-many
        public IList<LoanBook>? LoanBooks { get; set; }
    }
}
