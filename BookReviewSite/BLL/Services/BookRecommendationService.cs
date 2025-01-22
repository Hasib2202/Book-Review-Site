using DAL.EF.Tables;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repos;

namespace BLL.Services
{
    // BookRecommendationService.cs
    public static class BookRecommendationService
    {
        public static List<Recommendation> GetBookRecommendations(int userId)
        {
            var recommendationRepo = DataAccess.RecommendationRepo();
            return recommendationRepo.GetRecommendationsByUserId(userId);
        }

        public static bool RecommendBook(int userId, int bookId)
        {
            try
            {
                // Verify user exists
                var userRepo = DataAccess.UserRepo();
                var user = userRepo.Get(userId);
                if (user == null)
                {
                    return false;
                }

                // Verify book exists
                var bookRepo = DataAccess.BookRepo();
                var book = bookRepo.Get(bookId);
                if (book == null)
                {
                    return false;
                }

                // Create recommendation
                var recommendation = new Recommendation();
                recommendation.UserId = userId;
                recommendation.BookId = bookId;
                recommendation.RecommendedAt = DateTime.Now;

                var recommendationRepo = DataAccess.RecommendationRepo();
                recommendationRepo.AddRecommendation(recommendation);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }


}
