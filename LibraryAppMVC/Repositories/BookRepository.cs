using LibraryAppMVC.Data;
using LibraryAppMVC.IRepositories;
using LibraryAppMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAppMVC.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;
        public BookRepository(LibraryDbContext context)
        {
            _context = context; 
        }


        public async Task<Book> CreateAsync(Book bookModel)
        {
            await _context.Books.AddAsync(bookModel);
            await _context.SaveChangesAsync();
            return bookModel;
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _context.Books.Include(b => b.Author).AsNoTracking().ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books.Include(b => b.Author).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Book?> UpdateAsync(int id, Book bookModel)
        {
            var existingbook = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);

            if (existingbook == null) return null;

            existingbook.Titolo = bookModel.Titolo;
            existingbook.Prezzo = bookModel.Prezzo;
            existingbook.Quantita = bookModel.Quantita;
            existingbook.DataPubblicazione = bookModel.DataPubblicazione;
            existingbook.Genere = bookModel.Genere;
            existingbook.AuthorId = bookModel.AuthorId;
            _context.Books.Update(existingbook);
            await _context.SaveChangesAsync();
            return existingbook;
        }

        public async Task<Book?> DeleteAsync(int id)
        {
            var bookModel = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);

            if (bookModel == null) return null;

            _context.Books.Remove(bookModel);
            await _context.SaveChangesAsync();
            return bookModel;
        }

        public async Task<bool> IsExists(int id)
        {
            return await _context.Books.AnyAsync(x => x.Id == id);
        }

        public async Task<Book?> UpdateQuantitaByIdAsync(int id, int addQuantita)
        {
            var existingbook = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);

            if (existingbook == null) return null;

            existingbook.Quantita += addQuantita;

            _context.Books.Update(existingbook);
            await _context.SaveChangesAsync();
            return existingbook;
        }
    }
}
