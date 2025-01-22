using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.Tables
{
    public class Vote
    {
        public int Id { get; set; }

        // true = upvote, false = downvote
        public bool IsUpvote { get; set; }

        // Foreign key to the User who cast the vote
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        // Foreign key to the Review being voted on
        [ForeignKey("Review")]
        public int ReviewId { get; set; }
        public virtual Review Review { get; set; }
    }

}
