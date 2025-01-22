using AutoMapper;
using BLL.DTOs;
using DAL.EF.Tables;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ReviewService
    {
        private static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Review, ReviewDTO>();
                cfg.CreateMap<ReviewDTO, Review>();
            });
            return new Mapper(config);
        }

        public static bool Add(ReviewDTO review)
        {
            var repo = DataAccess.ReviewRepo();
            var entity = GetMapper().Map<Review>(review);
            entity.CreatedAt = DateTime.UtcNow;
            return repo.Create(entity);
        }

        public static List<ReviewDTO> GetByBookId(int bookId)
        {
            var repo = DataAccess.ReviewRepo();
            var reviews = repo.GetByBookId(bookId);
            return GetMapper().Map<List<ReviewDTO>>(reviews);
        }

        public static bool Delete(int id)
        {
            var repo = DataAccess.ReviewRepo();
            return repo.Delete(id);
        }

     

        public static List<object> GetReviewDetails()
        {
            var repo = DataAccess.ReviewRepo();
            return repo.GetReviewDetails();
        }

        public static List<object> GetReviewDetailsByUserId(int userId)
        {
            var repo = DataAccess.ReviewRepo();
            return repo.GetReviewDetailsByUserId(userId);
        }

        public static object GetReviewDetailsByReviewId(int reviewId)
        {
            var repo = DataAccess.ReviewRepo();
            return repo.GetReviewDetailsByReviewId(reviewId);
        }

        public static List<object> SearchReviews(string keyword)
        {
            var repo = DataAccess.ReviewRepo();
            return repo.SearchReviews(keyword);
        }

        public static User GetReviewerProfile(int userId)
        {
            var userRepo = DataAccess.UserRepo();
            var reviewRepo = DataAccess.ReviewRepo();

            User user = userRepo.Get(userId);
            if (user != null)
            {
                var reviews = reviewRepo.GetReviewsByUserId(userId);

                // Removing duplicate reviews using GroupBy
                user.Reviews = reviews.GroupBy(r => r.Id)
                                      .Select(group => group.First())
                                      .ToList();
            }
            return user;
        }



    }

}
