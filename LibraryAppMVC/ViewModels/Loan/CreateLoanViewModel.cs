﻿using LibraryAppMVC.Models;
using LibraryAppMVC.ViewModels.Book;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LibraryAppMVC.ViewModels.Loan
{
    public class CreateLoanViewModel
    {
        [Display(Name = "Data Inizio")]
        public DateTime DataInizio { get; set; } = DateTime.Now;
        [Display(Name = "Data Fine")]
        public DateTime DataFine { get; set; } = DateTime.Now.AddYears(1);
        public IEnumerable<IndexBookViewModel> Books { get; set; }

        public List<LoanItemViewModel> BooksAddedToCart { get; set; } = [];
        public decimal Totale { get; set; }

        public string? TermTitle { get; set; }

        public string? TermAuthor { get; set; }
    }
}
