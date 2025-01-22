using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL.Interfaces
{
    public interface IReviewRepo : IRepo<Review, int, bool>
    {
        List<Review> GetByBookId(int bookId);
        Review Get(int id);
        bool Create(Review review);
        bool Delete(int id);



        List<object> GetReviewDetails();

        List<object> GetReviewDetailsByUserId(int userId);
        object GetReviewDetailsByReviewId(int reviewId);

        List<object> SearchReviews(string keyword);

        List<Review> GetReviewsByUserId(int userId);
    }
}
