using Dapper;
using LibraryAppMVC.IRepositories;
using LibraryAppMVC.Models;
using Microsoft.Data.SqlClient;

namespace LibraryAppMVC.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly string _connectionString;

        public ReviewRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            var sql = GetReviewSql(false);
            var reviews = await QueryReviewsAsync(sql);
            return reviews;
  
        }

        public async Task<IEnumerable<Review>> GetAllByUserAsync(string UserId)
        {
            var sql = GetUserReviewSql(false);
            var reviews = await QueryReviewsAsync(sql, new { userId = UserId });
            return reviews;

        }

        public async Task<Review> GetByIdByUserAsync(string UserId, int id)
        {
            var sql = GetUserReviewSql(true);
            var reviews = await QueryReviewsAsync(sql, new { userId = UserId, Id = id });
            return reviews.FirstOrDefault();

        }

        public async Task<Review> GetByIdAsync(int id) 
        {
            var sql = GetReviewSql(true);
            var reviews = await QueryReviewsAsync(sql, new {Id = id});
            return reviews.FirstOrDefault();
        }

        public async Task<int> CreateReviewAsync(Review review)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = @"
                              INSERT INTO Review (Testo, UserID, BookId)
                              VALUES (@Testo, @UserID, @BookId)";
                return await connection.ExecuteAsync(sql, review);
            }
        }

        private string GetReviewSql(bool withWhereClause)
        {
            var sql = @"SELECT r.*, au.*, b.*
                            FROM Review r
                            LEFT JOIN ApplicationUser au ON r.UserID = au.Id
                            LEFT JOIN Book b ON r.BookId = b.Id
                             ";
            if (withWhereClause)
            {
                sql += " WHERE r.Id = @Id";
            }

            return sql;
        }

        private string GetUserReviewSql(bool withWhereClause)
        {
            var sql = @"SELECT r.*, au.*, b.*
                            FROM Review r
                            LEFT JOIN ApplicationUser au ON r.UserID = au.Id
                            LEFT JOIN Book b ON r.BookId = b.Id
                            WHERE au.Id = @userId
                             ";
            if (withWhereClause)
            {
                sql += " AND r.Id = @Id";
            }

            return sql;
        }

        private async Task<IEnumerable<Review>> QueryReviewsAsync(string sql, object? parameters=null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var ReviewDictionary = new Dictionary<int, Review>();

                var reviews = await connection.QueryAsync<Review, ApplicationUser, Book, Review>(
                    sql,
                    (Review, ApplicationUser, Book) =>
                    {
                        if (!ReviewDictionary.TryGetValue(Review.Id, out var currentReview))
                        {
                            currentReview = Review;
                            currentReview.Book = Book;
                            currentReview.User = ApplicationUser;
                            ReviewDictionary.Add(currentReview.Id, currentReview);
                        }

                        return currentReview;
                    }, parameters, splitOn: "Id, Id");

                return ReviewDictionary.Values;
            }
        }

        public async Task<int> UpdateAsync(Review reviewModel)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.ExecuteAsync(@"UPDATE Review 
                                          SET Testo = @Testo, BookId = @BookId
                                          WHERE Id = @Id ", reviewModel);
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.ExecuteAsync(@"DELETE FROM Review
                                                       WHERE Id = @Id", new { Id = id });
            }
        }

       
    }
}
