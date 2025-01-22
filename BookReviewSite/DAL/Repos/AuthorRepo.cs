using DAL.EF.Tables;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    // Repository - AuthorRepo.cs
    public class AuthorRepo : Repo, IAuthorRepo
    {
        public Author Get(int id)
        {
            Author author = null;

            // Fetching the author by ID manually (no lambda expression)
            foreach (var a in db.Authors)
            {
                if (a.Id == id)
                {
                    author = a;
                    break;  
                }
            }

            return author;
        }

        public List<Author> GetAll()
        {
            List<Author> authors = new List<Author>();
            foreach (var a in db.Authors)
            {
                authors.Add(a);
            }

            return authors;
        }
    }

}
