using LibraryAppMVC.IRepositories;
using LibraryAppMVC.IServices;
using LibraryAppMVC.Models;
using LibraryAppMVC.ViewModels.Author;

namespace LibraryAppMVC.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepo;

        public AuthorService(IAuthorRepository authorRepo)
        {
            _authorRepo = authorRepo;
        }

        public async Task<bool> AuthorExists(int id)
        {
            return await _authorRepo.IsExists(id);
        }

        public async Task<Author> CreateAsync(CreateAuthorViewModel authorVM)
        {
            var author = new Author
            {
                Nome = authorVM.Nome,
                Cognome = authorVM.Cognome,
                Nazionalita = authorVM.Nazionalita,
                DoB = authorVM.DoB,
                LuogoNascita = authorVM.LuogoNascita
            };
            if (authorVM.isAlive == true)
            {
                author.DoD = null;
            }
            else
            {
                author.DoD = authorVM.DoD;
            }
            return await _authorRepo.CreateAsync(author);
        }

        public async Task<Author?> DeleteAsync(int id)
        {
            return await _authorRepo.DeleteAsync(id);
        }

        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            return await _authorRepo.GetAllAsync();
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            return await _authorRepo.GetByIdAsync(id);
        }

        public async Task<Author> UpdateAsync(int id, Author author)
        {
            return await _authorRepo.UpdateAsync(id, author);
        }
        public async Task<List<Author>> GetDashboardAuthors(int numAuthors)
        {
            return await _authorRepo.GetDashboardAuthors(numAuthors);
        }
    }
}
