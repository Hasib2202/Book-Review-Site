using DAL.EF.Tables;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    // Service - AuthorService.cs
    public static class AuthorService
    {
        // Service - AuthorService.cs
        public static Author GetAuthorProfile(int authorId)
        {
            var authorRepo = DataAccess.AuthorRepo();
            var bookRepo = DataAccess.BookRepo();

            Author author = authorRepo.Get(authorId);
            if (author != null)
            {
                // Retrieve books written by the author
                var books = bookRepo.GetBooksByAuthorId(authorId);

                // Removing duplicate books using GroupBy (if there are any duplicates)
                author.Books = books.GroupBy(b => b.Id)
                                    .Select(group => group.First())
                                    .ToList();
            }

            return author;
        }

    }


}
