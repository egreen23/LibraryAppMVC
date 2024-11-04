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

        public async Task<Author> CreateAsync(Author authorModel)
        {
            await _context.Authors.AddAsync(authorModel);
            await _context.SaveChangesAsync();
            return authorModel;
        }

        public async Task<Author?> DeleteAsync(int id)
        {
            var authorModel = await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);

            if (authorModel == null) return null;

            _context.Authors.Remove(authorModel);
            await _context.SaveChangesAsync();
            return authorModel;
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await _context.Authors.AsNoTracking().ToListAsync();
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await _context.Authors.Include(b => b.Books).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<bool> IsExists(int id)
        {
            return await _context.Authors.AnyAsync(x => x.Id == id);
        }

        public async Task<Author?> UpdateAsync(int id, Author authorModel)
        {
            var existingauthor = await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);

            if (existingauthor == null) return null;

            existingauthor.Nome = authorModel.Nome;
            existingauthor.Cognome = authorModel.Cognome;
            existingauthor.Nazionalita = authorModel.Nazionalita;
            existingauthor.DoB = authorModel.DoB;
            existingauthor.LuogoNascita = authorModel.LuogoNascita;
            existingauthor.DoD = authorModel.DoD;
            _context.Authors.Update(existingauthor);
            await _context.SaveChangesAsync();
            return existingauthor;
        }
    }
}
