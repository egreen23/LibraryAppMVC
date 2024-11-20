using LibraryAppMVC.Models;

namespace LibraryAppMVC.IRepositories
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAllAsync();
        Task<Author?> GetByIdAsync(int id);
        Task<Author> CreateAsync(Author authorModel);
        Task<Author?> UpdateAsync(int id, Author authorModel);
        Task<Author?> DeleteAsync(int id);
        Task<bool> IsExists(int id);

        Task<List<Author>> GetDashboardAuthors(int numAuthors);
    }
}
