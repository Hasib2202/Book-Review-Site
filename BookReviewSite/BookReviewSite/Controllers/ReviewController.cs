using BLL.DTOs;
using BLL.Services;
using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookReviewSite.Controllers
{
    [RoutePrefix("api/review")]
    public class ReviewController : ApiController
    {
        [HttpPost]
        [Route("add")]
        public HttpResponseMessage AddReview(ReviewDTO review)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            var result = ReviewService.Add(review);
            if (result)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Review added successfully");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed to add review");
        }

        [HttpGet]
        [Route("book/{bookId}")]
        public HttpResponseMessage GetReviewsByBookId(int bookId)
        {
            var data = ReviewService.GetByBookId(bookId);
            if (data != null && data.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "No reviews found for this book");
        }

        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage DeleteReview(int id)
        {
            var result = ReviewService.Delete(id);
            if (result)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Review deleted successfully");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed to delete review");
        }


        [HttpGet]
        [Route("details")]
        public HttpResponseMessage GetReviewDetails()
        {
            var data = ReviewService.GetReviewDetails();
            if (data != null && data.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "No reviews found");
        }

        [HttpGet]
        [Route("details/user/{userId}")]
        public HttpResponseMessage GetReviewDetailsByUserId(int userId)
        {
            var data = ReviewService.GetReviewDetailsByUserId(userId);
            if (data != null && data.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "No reviews found for this user.");
        }

        [HttpGet]
        [Route("details/review/{reviewId}")]
        public HttpResponseMessage GetReviewDetailsByReviewId(int reviewId)
        {
            var data = ReviewService.GetReviewDetailsByReviewId(reviewId);
            if (data != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "No review found with this ID.");
        }

        [HttpGet]
        [Route("search")]
        public HttpResponseMessage SearchReviews([FromUri] string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Keyword is required.");
            }

            var data = ReviewService.SearchReviews(keyword);
            if (data != null && data.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "No reviews found.");
        }

        [HttpGet]
        [Route("reviewer/{userId}")]
        public HttpResponseMessage GetReviewerProfile(int userId)
        {
            User profile = ReviewService.GetReviewerProfile(userId);
            if (profile == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Reviewer not found");
            }

            // Grouping reviews by their ID to remove duplicates
            var uniqueReviews = profile.Reviews
                                       .GroupBy(r => r.Id)
                                       .Select(group => group.First())
                                       .ToList();

            List<object> reviewsList = new List<object>();
            foreach (var review in uniqueReviews)
            {
                reviewsList.Add(new
                {
                    Id = review.Id,
                    Content = review.Content,
                    Rating = review.Rating,
                    CreatedAt = review.CreatedAt,
                    BookTitle = review.Book.Title
                });
            }

            var result = new
            {
                profile.Id,
                profile.Name,
                profile.Email,
                Reviews = reviewsList
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

    }

}
