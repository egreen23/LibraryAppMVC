using LibraryAppMVC.IRepositories;
using LibraryAppMVC.IServices;
using LibraryAppMVC.Models;
using LibraryAppMVC.ViewModels.Loan;

namespace LibraryAppMVC.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepo;

        public LoanService(ILoanRepository loanRepo)
        {
            _loanRepo = loanRepo;
        }
        public async Task<Loan> CreateAsync(Loan loanModel)
        {
            await _loanRepo.CreateAsync(loanModel);
            return loanModel;
        }

        public async Task<Loan?> DeleteAsync(int id)
        {
            return await _loanRepo.DeleteAsync(id);
        }

        public async Task<List<IndexLoanViewModel>> GetAllAsync()
        {
            var loans = await _loanRepo.GetAllAsync();
            List<IndexLoanViewModel> result = new List<IndexLoanViewModel>();
            foreach (var loan in loans)
            {

                IndexLoanViewModel l = new IndexLoanViewModel
                {
                    Id = loan.Id,
                    Totale = loan.Totale,
                    DataInizio = loan.DataInizio,
                    DataFine = loan.DataFine,
                    UserFullName = loan.User.Nome + " " + loan.User.Cognome
                };
                result.Add(l);
            }

            return result;
        }

        public async Task<List<IndexLoanViewModel>> GetAllByUserIdAsync(string UserId)
        {
            var loans = await _loanRepo.GetAllByUserIdAsync(UserId);
            List<IndexLoanViewModel> result = new List<IndexLoanViewModel>();
            foreach (var loan in loans)
            {

                IndexLoanViewModel l = new IndexLoanViewModel
                {
                    Id = loan.Id,
                    Totale = loan.Totale,
                    DataInizio = loan.DataInizio,
                    DataFine = loan.DataFine,
                    UserFullName = loan.User.Nome + " " + loan.User.Cognome
                };
                result.Add(l);
            }

            return result;
        }


        public async Task<DetailLoanViewModel> GetByIdAsync(int id)
        {
            DetailLoanStruct[] list = await _loanRepo.GetByIdAsync(id);
            DetailLoanViewModel result = new DetailLoanViewModel
            {
                aLoan = new Loan(),
                bookList = []
            };

            result.aLoan.Totale = list[0].Totale;
            result.aLoan.DataFine = list[0].DataFine;
            result.aLoan.DataInizio = list[0].DataInizio;
            result.aLoan.Id = list[0].Id;
            foreach (var l in list)
            {
                BookLoanStruct bookinfo = new()
                {
                    Titolo = l.Titolo,
                    Prezzo = l.Prezzo,
                    BookId = l.BookId
                };

                result.bookList.Add(bookinfo);
            }
            return result;
        }

        public async Task<bool> LoanExists(int id)
        {
            return await _loanRepo.IsExists(id);
        }


    }
}
