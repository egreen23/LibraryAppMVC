using LibraryAppMVC.Models;
using LibraryAppMVC.ViewModels.Author;

namespace LibraryAppMVC.IServices
{
    public interface IAuthorService
    {
         Task<List<Author>> GetAllAuthorsAsync();
         Task<Author> GetByIdAsync(int id);

         Task<Author> CreateAsync(CreateAuthorViewModel author);

         Task<Author> UpdateAsync(int id, Author author);

         Task<bool> AuthorExists(int id);

         Task<Author?> DeleteAsync(int id);

        Task<List<Author>> GetDashboardAuthors(int numAuthors);
    }
}
