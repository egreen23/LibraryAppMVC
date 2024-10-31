using LibraryAppMVC.IRepositories;
using LibraryAppMVC.IServices;
using LibraryAppMVC.Models;

namespace LibraryAppMVC.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepo;

        public AuthorService(IAuthorRepository authorRepo)
        {
            _authorRepo = authorRepo;
        }
        public Task<List<Author>> GetAllAuthorsAsync()
        {
            return _authorRepo.GetAllAsync();
        }
    }
}
