using DAL.EF.Tables;
using DAL.Interfaces;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DataAccess
    {

        public static IUserRepo UserData()
        {
            return new UserRepo();
        }

        public static IBookRepo BookRepo()
        {
            return new BookRepo();
        }

        public static IReviewRepo ReviewRepo()
        {
            return new ReviewRepo();
        }


        public static IVoteRepo VoteRepo()
        {
            return new VoteRepo();
        }

        public static IUserRepo UserRepo()
        {
            return new UserRepo();
        }

        public static IAuthorRepo AuthorRepo()
        {
            return new AuthorRepo();
        }

        public static IRecommendationRepo RecommendationRepo()
        {
            return new RecommendationRepo();
        }

    }
}
