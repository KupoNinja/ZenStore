using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using ZenStore.Models;

namespace ZenStore.Data
{
    public class ReviewsRepository
    {
        private readonly IDbConnection _db;

        public IEnumerable<Review> GetAll()
        {
            return _db.Query<Review>("SELECT * FROM reviews");
        }

        public Review GetById(string id)
        {
            return _db.QueryFirstOrDefault<Review>(
                "SELECT * FROM reviews WHERE id = @id",
                new { id }
            );
        }

        public IEnumerable<Review> GetAllByProduct(string id)
        {
            return _db.Query<Review>(
                "SELECT * FROM reviews WHERE productid = @id",
                new { id }
            );
        }

        public Review Create(Review review)
        {
            var sql = @"
                INSERT INTO reviews (id, name, description, rating, productid)
                VALUES (@Id, @Name, @Description, @Rating, @ProductId);";
            _db.Execute(sql, review);

            return review;
        }

        public Review Edit(Review review)
        {
            _db.Execute(@"
                UPDATE reviews SET 
                id = @Id, 
                name = @Name, 
                description = @Description, 
                rating = @Rating
                WHERE id = @Id;",
                review);

            return review;
        }

        public ReviewsRepository(IDbConnection db)
        {
            _db = db;
        }
    }
}