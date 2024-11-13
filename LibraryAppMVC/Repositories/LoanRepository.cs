using LibraryAppMVC.Data;
using LibraryAppMVC.IRepositories;
using LibraryAppMVC.Models;
using LibraryAppMVC.ViewModels.Loan;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace LibraryAppMVC.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly LibraryDbContext _context;
        public LoanRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<Loan> CreateAsync(Loan loanModel)
        {
            await _context.Loans.AddAsync(loanModel);
            await _context.SaveChangesAsync();
            return loanModel;
        }

        public async Task<Loan?> DeleteAsync(int id)
        {
            var loanModel = await _context.Loans.FirstOrDefaultAsync(x => x.Id == id);

            if (loanModel == null) return null;

            _context.Loans.Remove(loanModel);
            await _context.SaveChangesAsync();
            return loanModel;
        }

        public async Task<List<Loan>> GetAllAsync()
        {
            return await _context.Loans.Include(l => l.User).AsNoTracking().ToListAsync();
        }

        public async Task<List<Loan>> GetAllByUserIdAsync(string UserId)
        {
            return await _context.Loans.Include(l => l.User).Where(u => u.UserId == UserId).AsNoTracking().ToListAsync();
        }

        public async Task<DetailLoanStruct[]> GetByIdAsync(int id)
        {
            var query = from loan in _context.Loans
                        join loanbook in _context.LoanBooks on loan.Id equals loanbook.LoanId
                        join book in _context.Books on loanbook.BookId equals book.Id
                        where loan.Id == id
                        select new DetailLoanStruct
                        {
                            Id = loan.Id,
                            Totale = loan.Totale,
                            DataInizio = loan.DataInizio,
                            DataFine = loan.DataFine,
                            Titolo = book.Titolo,
                            BookId = book.Id,
                            Prezzo = book.Prezzo
                        };
            //IEnumerable c = query.ToList();
            return await query.ToArrayAsync();
        }

        public async Task<bool> IsExists(int id)
        {
            return await _context.Loans.AnyAsync(x => x.Id == id);
        }


        //public async Task<prova> GetByIdAsync(int id)
        //{
        //    var query = from loan in _context.Loans
        //                join loanbook in _context.LoanBooks on loan.Id equals loanbook.LoanId
        //                join book in _context.Books on loanbook.BookId equals book.Id into bookgroup
        //                group loan by loanbook.Id;
        //    //select new prova
        //    //{
        //    //    Totale = loan.Totale,
        //    //    aBook = bookgroup.ToList()
        //    //};
        //    //IEnumerable c = query.ToList();
        //    var c = query.ToList();
        //    return new prova();
        //}
    }
}
