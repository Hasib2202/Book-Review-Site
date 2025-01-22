using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserRepo
    {

        User GetByUsername(string username);
        bool Create(User user);

        User GetByEmail(string email);

        User Get(int id);

    }
}
