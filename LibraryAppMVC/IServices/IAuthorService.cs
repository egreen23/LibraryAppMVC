using LibraryAppMVC.Models;

namespace LibraryAppMVC.IServices
{
    public interface IAuthorService
    {
        public Task<List<Author>> GetAllAuthorsAsync();
    }
}
