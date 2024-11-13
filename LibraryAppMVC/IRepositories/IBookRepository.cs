using LibraryAppMVC.Models;

namespace LibraryAppMVC.IRepositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync();

        Task<Book?> GetByIdAsync(int id);
        Task<Book> CreateAsync(Book bookModel);
        Task<Book?> UpdateAsync(int id, Book bookModel);
        Task<Book?> DeleteAsync(int id);
        Task<bool> IsExists(int id);

        Task<Book?> UpdateQuantitaByIdAsync(int id, int addQuantita);

    }
}
