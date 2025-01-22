using DAL.EF.Tables;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class UserRepo : Repo, IUserRepo
    {
        public User GetByUsername(string username)
        {
            return db.Users.FirstOrDefault(u => u.Username == username);
        }

        public bool Create(User user)
        {
            db.Users.Add(user);
            return db.SaveChanges() > 0;
        }

        public User GetByEmail(string email)
        {
            // Query the database to find the user by email
            return db.Users.FirstOrDefault(u => u.Email == email);
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }
    }
}
