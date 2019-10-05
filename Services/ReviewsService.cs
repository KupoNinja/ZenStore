using System;
using System.Collections.Generic;
using System.Linq;
using ZenStore.Data;
using ZenStore.Models;

namespace ZenStore.Services
{
    public class ReviewsService
    {
        private readonly ReviewsRepository _repo;

        public List<Review> GetReviews()
        {
            return _repo.GetAll().ToList();
        }

        public Review GetReviewById(string id)
        {
            var review = _repo.GetById(id);
            if (review == null) { throw new Exception("You're taking empty mind too far. This review doesn't even exist."); }

            return review;
        }

        public List<Review> GetReviewsByProduct(string id)
        {
            return _repo.GetAllByProduct(id).ToList();
        }

        public Review AddReview(Review reviewData)
        {
            reviewData.Id = Guid.NewGuid().ToString();
            _repo.Create(reviewData);

            return reviewData;
        }

        public Review EditReview(Review reviewData)
        {
            var review = _repo.GetById(reviewData.Id);
            if (review == null) { throw new Exception("You're taking empty mind too far. This review doesn't even exist."); }
            review.Name = reviewData.Name;
            review.Description = reviewData.Description;

            return _repo.Edit(review);
        }

        public ReviewsService(ReviewsRepository repo)
        {
            _repo = repo;
        }
    }
}