using DAL.EF.Tables;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class BookRepo : Repo, IBookRepo
    {
        public bool Create(Book book)
        {
            db.Books.Add(book);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var book = Get(id);
            db.Books.Remove(book);
            return db.SaveChanges() > 0;
        }

        public Book Get(int id)
        {
            return db.Books.Find(id);
        }

        public List<Book> Get()
        {
            return db.Books.ToList();
        }

        public bool Update(Book book)
        {
            var existingBook = Get(book.Id);
            db.Entry(existingBook).CurrentValues.SetValues(book);
            return db.SaveChanges() > 0;
        }

        //public List<Book> GetBooksByAuthorId(int authorId)
        //{
        //    return db.Books.Where(b => b.AuthorId == authorId).ToList();
        //}

        public List<Book> GetBooksByAuthorId(int authorId)
        {
            List<Book> books = new List<Book>();

            // Fetching books written by the author using a loop (no lambda expression)
            foreach (var b in db.Books)
            {
                if (b.AuthorId == authorId)
                {
                    books.Add(b);
                }
            }

            return books;
        }
    }
}
