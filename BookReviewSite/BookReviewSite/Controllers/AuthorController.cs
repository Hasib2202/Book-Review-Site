using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookReviewSite.Controllers
{
    [RoutePrefix("api/author")]
    public class AuthorController : ApiController
    {
        // Controller - AuthorController.cs
        // Controller - AuthorController.cs
        [HttpGet]
        [Route("author/{authorId}")]
        public HttpResponseMessage GetAuthorProfile(int authorId)
        {
            var profile = AuthorService.GetAuthorProfile(authorId);
            if (profile == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Author not found");
            }

            // Grouping books by their ID to remove duplicates
            var uniqueBooks = profile.Books
                                     .GroupBy(b => b.Id)
                                     .Select(group => group.First())
                                     .ToList();

            List<object> booksList = new List<object>();
            foreach (var book in uniqueBooks)
            {
                booksList.Add(new
                {
                    Id = book.Id,
                    Title = book.Title,
                    Genre = book.Genre,
                    Description = book.Description
                });
            }

            var result = new
            {
                profile.Id,
                profile.Name,
                profile.Biography,
                Books = booksList
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


    }

}