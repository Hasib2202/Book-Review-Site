using DAL.EF.Tables;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class VoteService
    {
        public static bool UpvoteReview(int userId, int reviewId)
        {
            var repo = DataAccess.VoteRepo();
            var existingVote = repo.GetUserVote(userId, reviewId);

            if (existingVote != null)
            {
                if (existingVote.IsUpvote) return false;  // Already upvoted
                repo.RemoveVote(existingVote.Id);  // Remove downvote before upvoting
            }

            var vote = new Vote { UserId = userId, ReviewId = reviewId, IsUpvote = true };
            return repo.AddVote(vote);
        }

        public static bool DownvoteReview(int userId, int reviewId)
        {
            var repo = DataAccess.VoteRepo();
            var existingVote = repo.GetUserVote(userId, reviewId);

            if (existingVote != null)
            {
                if (!existingVote.IsUpvote) return false;  // Already downvoted
                repo.RemoveVote(existingVote.Id);  // Remove upvote before downvoting
            }

            var vote = new Vote { UserId = userId, ReviewId = reviewId, IsUpvote = false };
            return repo.AddVote(vote);
        }

        public static int GetUpvoteCount(int reviewId)
        {
            var repo = DataAccess.VoteRepo();
            return repo.GetUpvoteCount(reviewId);
        }

        public static int GetDownvoteCount(int reviewId)
        {
            var repo = DataAccess.VoteRepo();
            return repo.GetDownvoteCount(reviewId);
        }
    }
}

