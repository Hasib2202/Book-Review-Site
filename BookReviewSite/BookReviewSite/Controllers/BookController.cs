using BLL.DTOs;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookReviewSite.Controllers
{
    [RoutePrefix("api/book")]
    public class BookController : ApiController
    {
        [HttpGet]
        [Route("all")]
        public HttpResponseMessage GetAllBooks()
        {
            var data = BookService.Get();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage GetBookById(int id)
        {
            var data = BookService.Get(id);
            if (data != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Book not found");
        }

        [HttpPost]
        [Route("add")]
        public HttpResponseMessage AddBook(BookDTO book)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            var result = BookService.Add(book);
            if (result)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Book added successfully");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed to add book");
        }

        [HttpPut]
        [Route("{id}")]
        public HttpResponseMessage UpdateBook(int id, BookDTO book)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            var result = BookService.Update(id, book);
            if (result)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Book updated successfully");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed to update book");
        }

        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage DeleteBook(int id)
        {
            var result = BookService.Delete(id);
            if (result)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Book deleted successfully");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed to delete book");
        }
    }
}
