using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IVoteRepo
    {
        bool AddVote(Vote vote);
        bool RemoveVote(int voteId);
        int GetUpvoteCount(int reviewId);
        int GetDownvoteCount(int reviewId);
        Vote GetUserVote(int userId, int reviewId);
    }

}
