using DAL.EF.Tables;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace DAL.Repos
{
    internal class ReviewRepo : Repo, IReviewRepo
    {
        public bool Create(Review review)
        {
            db.Reviews.Add(review);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var review = Get(id);
            if (review != null)
            {
                db.Reviews.Remove(review);
                return db.SaveChanges() > 0;
            }
            return false;
        }

        public Review Get(int id)
        {
            return db.Reviews.Find(id);
        }

        public List<Review> GetByBookId(int bookId)
        {
            return db.Reviews.Where(r => r.BookId == bookId).ToList();
        }



        public List<object> GetReviewDetails()
        {
            var result = (from review in db.Reviews
                          join book in db.Books on review.BookId equals book.Id
                          join author in db.Authors on book.AuthorId equals author.Id
                          select new
                          {
                              BookTitle = book.Title,
                              AuthorName = author.Name,
                              ReviewContent = review.Content,
                              Rating = review.Rating
                          }).ToList<object>();

            return result;
        }

        public List<object> GetReviewDetailsByUserId(int userId)
        {
            return (from review in db.Reviews
                    join book in db.Books on review.BookId equals book.Id
                    join author in db.Authors on book.AuthorId equals author.Id
                    where review.UserId == userId
                    select new
                    {
                        BookTitle = book.Title,
                        AuthorName = author.Name,
                        ReviewContent = review.Content,
                        Rating = review.Rating
                    }).ToList<object>();
        }

        public object GetReviewDetailsByReviewId(int reviewId)
        {
            return (from review in db.Reviews
                    join book in db.Books on review.BookId equals book.Id
                    join author in db.Authors on book.AuthorId equals author.Id
                    where review.Id == reviewId
                    select new
                    {
                        BookTitle = book.Title,
                        AuthorName = author.Name,
                        ReviewContent = review.Content,
                        Rating = review.Rating
                    }).FirstOrDefault<object>();
        }

        public List<object> SearchReviews(string keyword)
        {
            return (from review in db.Reviews
                    join book in db.Books on review.BookId equals book.Id
                    join author in db.Authors on book.AuthorId equals author.Id
                    where book.Title.Contains(keyword) || author.Name.Contains(keyword)
                    select new
                    {
                        BookTitle = book.Title,
                        AuthorName = author.Name,
                        ReviewContent = review.Content,
                        Rating = review.Rating
                    }).ToList<object>();
        }

        public List<Review> GetReviewsByUserId(int userId)
        {
            List<Review> userReviews = new List<Review>();

            foreach (var review in db.Reviews.Include("Book").ToList())
            {
                if (review.UserId == userId)
                {
                    userReviews.Add(review);
                }
            }
            return userReviews;
        }


    }

}
