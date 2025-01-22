using BLL.Services;
using DAL;
using DAL.EF.Tables;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookReviewSite.Controllers
{

    [RoutePrefix("api/recommendations")]
    public class RecommendationController : ApiController
    {
        [HttpGet]
        [Route("user/{userId}")]
        public HttpResponseMessage GetRecommendations(int userId)
        {
            try
            {
                var userRepo = DataAccess.UserRepo();
                var user = userRepo.Get(userId);
                if (user == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, $"User not found with ID: {userId}");
                }

                var recommendations = BookRecommendationService.GetBookRecommendations(userId);

                if (recommendations == null || recommendations.Count == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, $"No recommendations found for user ID: {userId}");
                }

                var result = recommendations.Select(r => new
                {
                    RecommendationId = r.Id,
                    UserId = r.UserId,
                    BookId = r.BookId
                }).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error: " + ex.Message);
            }
        }



        [HttpPost]
        [Route("user/{userId}/book/{bookId}")]
        public HttpResponseMessage RecommendBook(int userId, int bookId)
        {
            try
            {
                // First check if user exists
                var userRepo = DataAccess.UserRepo();
                var user = userRepo.Get(userId);
                if (user == null)
                {
                    return Request.CreateResponse(
                        HttpStatusCode.NotFound,
                        "User not found with ID: " + userId);
                }

                // Then check if book exists
                var bookRepo = DataAccess.BookRepo();
                var book = bookRepo.Get(bookId);
                if (book == null)
                {
                    return Request.CreateResponse(
                        HttpStatusCode.NotFound,
                        "Book not found with ID: " + bookId);
                }

                var recommendation = new Recommendation
                {
                    UserId = userId,
                    BookId = bookId,
                    RecommendedAt = DateTime.Now
                };

                var recommendationRepo = DataAccess.RecommendationRepo();
                recommendationRepo.AddRecommendation(recommendation);

                return Request.CreateResponse(
                    HttpStatusCode.Created,
                    "Book successfully recommended");
            }
            catch (Exception ex)
            {
                // Return the actual error message in development
                return Request.CreateResponse(
                    HttpStatusCode.InternalServerError,
                    "Error: " + ex.Message);
            }
        }
    }


}
