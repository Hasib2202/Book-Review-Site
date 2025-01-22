using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.Tables
{
    public class Recommendation
    {
        public int Id { get; set; }

        // Foreign Key to User
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        // Foreign Key to Book
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        public DateTime RecommendedAt { get; set; }
    }

}
