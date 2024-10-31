using LibraryAppMVC.Models;

namespace LibraryAppMVC.IRepositories
{
    public interface IAuthorRepository
    {
        public Task<List<Author>> GetAllAsync();
    }
}
