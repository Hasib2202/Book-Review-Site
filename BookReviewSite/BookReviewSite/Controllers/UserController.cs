using BLL.DTOs;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace BookReviewSite.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        [HttpPost]
        [Route("register")]
        public HttpResponseMessage Register(UserDTO user)
        {
            // Check if the model is valid
            if (!ModelState.IsValid)
            {
                // Return validation errors
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, GetModelStateErrors());
            }

            var result = UserService.Register(user);
            if (result)
                return Request.CreateResponse(HttpStatusCode.OK, "Registration successful");

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Email already exists. Please try another email.");
        }

        [HttpPost]
        [Route("login")]
        public HttpResponseMessage Login(LoginDTO login)
        {
            // Check if the model is valid
            if (!ModelState.IsValid)
            {
                // Return validation errors
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, GetModelStateErrors());
            }

            var result = UserService.Login(login.Username, login.Password);
            if (result != null)
            {
                string welcomeMessage = result.Role == "Admin" ? "Welcome Admin" : "Welcome User";
                return Request.CreateResponse(HttpStatusCode.OK, new { Message = welcomeMessage, Id = result.Id, Name = result.Name, Username = result.Username, Email = result.Email,Role = result.Role, Token = result.Token });
            }
            return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid credentials");
        }

        private string GetModelStateErrors()
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                            .Select(e => e.ErrorMessage)
                                            .ToList();
            return string.Join(", ", errors);
        }

        [HttpPost]
        [Route("logout")]
        public HttpResponseMessage Logout()
        {
            var authHeader = Request.Headers.Authorization;
            if (authHeader != null && authHeader.Scheme == "Bearer")
            {
                var token = authHeader.Parameter;
                var result = UserService.InvalidateSessionToken(token);

                if (result)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "User logged out successfully.");
                }
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "No valid session found for logout.");
        }
    }

}
