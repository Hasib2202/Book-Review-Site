using DAL.EF.Tables;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class VoteRepo : Repo, IVoteRepo
    {
        public bool AddVote(Vote vote)
        {
            db.Votes.Add(vote);
            return db.SaveChanges() > 0;
        }

        public bool RemoveVote(int voteId)
        {
            var vote = db.Votes.Find(voteId);
            if (vote != null)
            {
                db.Votes.Remove(vote);
                return db.SaveChanges() > 0;
            }
            return false;
        }

        public int GetUpvoteCount(int reviewId)
        {
            return db.Votes.Count(v => v.ReviewId == reviewId && v.IsUpvote);
        }

        public int GetDownvoteCount(int reviewId)
        {
            return db.Votes.Count(v => v.ReviewId == reviewId && !v.IsUpvote);
        }

        public Vote GetUserVote(int userId, int reviewId)
        {
            return db.Votes.FirstOrDefault(v => v.UserId == userId && v.ReviewId == reviewId);
        }
    }

}
