using DAL.EF.Tables;
using DAL.EF;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class RecommendationRepo : Repo, IRecommendationRepo
    {
        public List<Recommendation> GetRecommendationsByUserId(int userId)
        {
            try
            {
                var recommendations = db.Recommendations
                    .Where(r => r.UserId == userId)
                    .Select(r => new
                    {
                        r.Id,
                        r.UserId,
                        r.BookId
                    })
                    .ToList();

                // Manually map the results to the Recommendation entity
                List<Recommendation> result = recommendations.Select(r => new Recommendation
                {
                    Id = r.Id,
                    UserId = r.UserId,
                    BookId = r.BookId
                }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetRecommendationsByUserId: " + ex.Message);
            }
        }

        public void AddRecommendation(Recommendation recommendation)
        {
            try
            {
                // Verify if the book exists
                var book = db.Books.Find(recommendation.BookId);
                if (book == null)
                {
                    throw new Exception("Book not found with ID: " + recommendation.BookId);
                }

                // Verify if the user exists
                var user = db.Users.Find(recommendation.UserId);
                if (user == null)
                {
                    throw new Exception("User not found with ID: " + recommendation.UserId);
                }

                // Check for existing recommendation
                bool exists = false;
                foreach (var rec in db.Recommendations)
                {
                    if (rec.UserId == recommendation.UserId && rec.BookId == recommendation.BookId)
                    {
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                {
                    db.Recommendations.Add(recommendation);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // Log the exception here
                throw new Exception("Error in AddRecommendation: " + ex.Message);
            }
        }
    }

}
