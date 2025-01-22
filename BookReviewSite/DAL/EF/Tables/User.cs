using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.Tables
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // Added role property
        public string Role { get; set; } = "User"; // Default role is "User"

        // Navigation property (One to Many)
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }

        public User()
        {
            Reviews = new List<Review>();
            Votes = new List<Vote>();
        }
    }

}
