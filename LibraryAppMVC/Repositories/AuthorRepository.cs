using LibraryAppMVC.Data;
using LibraryAppMVC.IRepositories;
using LibraryAppMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAppMVC.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryDbContext _context;

        public AuthorRepository(LibraryDbContext context)
        {
            _context = context; 
        }
        public Task<List<Author>> GetAllAsync()
        {
            return _context.Authors.ToListAsync();
        }
    }
}
