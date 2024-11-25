using LibraryAppMVC.IRepositories;
using LibraryAppMVC.IServices;
using LibraryAppMVC.Models;

namespace LibraryAppMVC.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }
        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            return await _reviewRepository.GetAllAsync();
        }

        public async Task<Review> GetByIdAsync(int id)
        {
            return await _reviewRepository.GetByIdAsync(id);
        }

        public async Task<int> CreateReviewAsync(Review review)
        {
            return await _reviewRepository.CreateReviewAsync(review);
        }

        public async Task<int> UpdateReviewAsync(Review review)
        {
            return await _reviewRepository.UpdateAsync(review);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _reviewRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Review>> GetAllByUserAsync(string UserId)
        {
            return await _reviewRepository.GetAllByUserAsync(UserId);
        }

        public async Task<Review> GetByIdByUserAsync(string UserId, int id)
        {
            return await _reviewRepository.GetByIdByUserAsync(UserId, id);
        }

        
    }
}
