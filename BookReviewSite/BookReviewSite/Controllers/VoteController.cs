using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookReviewSite.Controllers
{
    [RoutePrefix("api/vote")]
    public class VoteController : ApiController
    {
        [HttpPost]
        [Route("upvote")]
        public HttpResponseMessage UpvoteReview(int userId, int reviewId)
        {
            var success = VoteService.UpvoteReview(userId, reviewId);
            if (success)
                return Request.CreateResponse(HttpStatusCode.OK, "Review upvoted successfully.");
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Upvote failed.");
        }

        [HttpPost]
        [Route("downvote")]
        public HttpResponseMessage DownvoteReview(int userId, int reviewId)
        {
            var success = VoteService.DownvoteReview(userId, reviewId);
            if (success)
                return Request.CreateResponse(HttpStatusCode.OK, "Review downvoted successfully.");
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Downvote failed.");
        }

        [HttpGet]
        [Route("count/upvotes/{reviewId}")]
        public HttpResponseMessage GetUpvoteCount(int reviewId)
        {
            var count = VoteService.GetUpvoteCount(reviewId);
            return Request.CreateResponse(HttpStatusCode.OK, count);
        }

        [HttpGet]
        [Route("count/downvotes/{reviewId}")]
        public HttpResponseMessage GetDownvoteCount(int reviewId)
        {
            var count = VoteService.GetDownvoteCount(reviewId);
            return Request.CreateResponse(HttpStatusCode.OK, count);
        }
    }

}
