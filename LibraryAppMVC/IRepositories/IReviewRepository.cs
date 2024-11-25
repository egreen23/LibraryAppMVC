using LibraryAppMVC.Models;

namespace LibraryAppMVC.IRepositories
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAllAsync();

        Task<Review> GetByIdAsync(int id);

        Task<int> CreateReviewAsync(Review review);

        Task<int> UpdateAsync(Review reviewModel);
        Task<int> DeleteAsync(int id);

        Task<IEnumerable<Review>> GetAllByUserAsync(string UserId);

        Task<Review> GetByIdByUserAsync(string UserId, int id);
    }
}
