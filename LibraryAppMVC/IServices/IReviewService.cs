using LibraryAppMVC.Models;

namespace LibraryAppMVC.IServices
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetAllAsync();

        Task<Review> GetByIdAsync(int id);

        Task<int> CreateReviewAsync(Review review);

        Task<int> UpdateReviewAsync(Review review);

        Task<int> DeleteAsync(int id);

        Task<IEnumerable<Review>> GetAllByUserAsync(string UserId);

        Task<Review> GetByIdByUserAsync(string UserId, int id);
    }
}
