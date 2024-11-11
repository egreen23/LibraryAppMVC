using LibraryAppMVC.Models;
using LibraryAppMVC.ViewModels.Author;

namespace LibraryAppMVC.IServices
{
    public interface IAuthorService
    {
        public Task<List<Author>> GetAllAuthorsAsync();
        public Task<Author> GetByIdAsync(int id);

        public Task<Author> CreateAsync(CreateAuthorViewModel author);

        public Task<Author> UpdateAsync(int id, Author author);

        public Task<bool> AuthorExists(int id);

        public Task<Author?> DeleteAsync(int id);
    }
}
