using LibraryAppMVC.Models;
using LibraryAppMVC.ViewModels.Loan;

namespace LibraryAppMVC.IServices
{
    public interface ILoanService
    {
        Task<List<IndexLoanViewModel>> GetAllAsync();
        Task<DetailLoanViewModel> GetByIdAsync(int id);
        //Task<DetailLoanStruct> GetByIdAsync(int id);
        Task<List<IndexLoanViewModel>> GetAllByUserIdAsync(string UserId);
        Task<Loan> CreateAsync(Loan loanModel);
        Task<Loan?> DeleteAsync(int id);

        Task<bool> LoanExists(int id);
    }
}
