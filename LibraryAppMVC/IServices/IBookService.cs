using LibraryAppMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAppMVC.IServices
{
    public interface IBookService
    {
        Task<List<Book>> GetAllAsync();

        Task<Book> GetByIdAsync(int id);

        Task<Book> CreateAsync(Book book);

        Task<Book> UpdateAsync(int id, Book book);

        Task<bool> BookExists(int id);

        Task<Book?> DeleteAsync(int id);

        Task<List<Book>> GetDashboardBooks(int numBooks);

        Task<Book?> UpdateQuantitaByIdAsync(int id, int addQuantita);
    }
}
