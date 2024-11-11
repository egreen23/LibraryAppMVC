using LibraryAppMVC.Data;
using LibraryAppMVC.IRepositories;
using LibraryAppMVC.Models;
using LibraryAppMVC.ViewModels.Loan;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace LibraryAppMVC.Repositories
{
    public class LoanRepository /*: ILoanRepository*/
    {
        private readonly LibraryDbContext _context;
        public LoanRepository(LibraryDbContext context)
        {
            _context = context;
        }
       

        //public async Task<List<Loan>> GetAllAsync()
        //{
        //    return await _context.Loans.Include(l => l.User).AsNoTracking().ToListAsync();
        //}

        //public async Task<DetailLoanStruct[]> GetByIdAsync(int id)
        //{
        //    var query = from loan in _context.Loans
        //                join loanbook in _context.LoanBooks on loan.Id equals loanbook.LoanId
        //                join book in _context.Books on loanbook.BookId equals book.Id
        //                where loan.Id == id
        //                select new DetailLoanStruct
        //                {
        //                    Id = loan.Id,
        //                    Totale = loan.Totale,
        //                    DataInizio = loan.DataInizio,
        //                    DataFine = loan.DataFine,
        //                    Titolo = book.Titolo,
        //                    Prezzo = book.Prezzo
        //                };
        //    //IEnumerable c = query.ToList();
        //    return await query.ToArrayAsync();
        //}

        //public async Task<prova> GetByIdAsync(int id)
        //{
        //    var query = from loan in _context.Loans
        //                join loanbook in _context.LoanBooks on loan.Id equals loanbook.LoanId
        //                join book in _context.Books on loanbook.BookId equals book.Id into bookgroup
        //                group loan by loanbook.Id;
        //                //select new prova
        //                //{
        //                //    Totale = loan.Totale,
        //                //    aBook = bookgroup.ToList()
        //                //};
        //    //IEnumerable c = query.ToList();
        //    var c = query.ToList();
        //    return new prova();
        //}
    }
}
