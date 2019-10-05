using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ZenStore.Models;
using ZenStore.Services;

namespace ZenStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ReviewsService _rs;

        [HttpGet]
        public ActionResult<IEnumerable<Review>> Get()
        {
            try
            {
                return Ok(_rs.GetReviews());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Review> GetById(string id)
        {
            try
            {
                return Ok(_rs.GetReviewById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult<Review> Create([FromBody] Review reviewData)
        {
            try
            {
                Review review = _rs.AddReview(reviewData);
                return Created("api/reviews/" + review.Id, review);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Review> Edit(string id, [FromBody] Review reviewData)
        {
            try
            {
                reviewData.Id = id;
                return Ok(_rs.EditReview(reviewData));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public ReviewsController(ReviewsService rs)
        {
            _rs = rs;
        }
    }
}