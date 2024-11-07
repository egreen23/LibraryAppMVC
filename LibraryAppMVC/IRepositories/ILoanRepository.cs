using LibraryAppMVC.Models;
using LibraryAppMVC.ViewModels.Loan;

namespace LibraryAppMVC.IRepositories
{
    public interface ILoanRepository
    {
        Task<List<Loan>> GetAllAsync();
        Task<DetailLoanStruct[]> GetByIdAsync(int id);
        //Task<List<DetailLoanViewModel>> GetByIdAsync(int id);
        Task<Loan> CreateAsync(Loan loanModel);
        Task<Loan?> DeleteAsync(int id);
    }
}
