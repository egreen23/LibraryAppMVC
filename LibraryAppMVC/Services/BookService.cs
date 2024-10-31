using LibraryAppMVC.Dtos;
using LibraryAppMVC.IRepositories;
using LibraryAppMVC.IServices;
using LibraryAppMVC.Mappers;
using LibraryAppMVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LibraryAppMVC.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepo;
        public BookService(IBookRepository bookRepo)
        {
            _bookRepo = bookRepo;
        }

        public async Task<Book> CreateAsync(Book book)
        {
            await _bookRepo.CreateAsync(book);
            return book;

        }

        public async Task<List<BookDto>> GetAllAsync()
        {
            var books = await _bookRepo.GetAllAsync(); //LIST
            var booksDto = books.Select(x => x.toBookDto()); //IEnumerable
            List<BookDto> listbooks = booksDto.ToList();
            return listbooks;
        }

        public async Task<BookDto> GetByIdAsync(int id)
        {
            var book = await _bookRepo.GetByIdAsync(id);
            var bookDto = book.toBookDto();
            return bookDto;

        }

        public async Task<Book?> UpdateAsync(int id, Book editedBook)
        {
            //var existingbook = await _bookRepo.GetByIdAsync(id);

            //if (existingbook == null) return null;

            return await _bookRepo.UpdateAsync(id, editedBook);

        }

        public async Task<bool> BookExists(int id)
        {
            return await _bookRepo.IsExists(id);
        }

        public async Task<Book?> DeleteAsync(int id)
        {
            return await _bookRepo.DeleteAsync(id);
        }
    }
}
