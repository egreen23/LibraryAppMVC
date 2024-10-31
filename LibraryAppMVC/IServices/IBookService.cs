using LibraryAppMVC.Dtos;
using LibraryAppMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAppMVC.IServices
{
    public interface IBookService
    {
        public Task<List<BookDto>> GetAllAsync();

        public Task<BookDto> GetByIdAsync(int id);

        public Task<Book> CreateAsync(Book book);

        public Task<Book> UpdateAsync(int id, Book book);

        public Task<bool> BookExists(int id);

        public Task<Book?> DeleteAsync(int id);


    }
}
