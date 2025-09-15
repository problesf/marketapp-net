using MarketNet.Domain.entities.Customers;
using MarketNet.Domain.Entities.Products;
using MarketNet.Domain.Entities.User;

namespace MarketNet.Domain.entities.Reviews
{
    public class Review
    {
        public long Id { get; set; }

        public long ProductId { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }

        public bool isApproved { get; set; }

        public DateTime CreatedAt { get; set; }

        public Product Product { get; set; }

        public CustomerProfile CustomerProfile { get; set; }
        public long CustomerProfileId { get; set; }
        public Review(){}
        public Review(long id, long productId, int rating, string comment, bool isApproved, DateTime createdAt, Product product, CustomerProfile customerProfile)
        {
            Id = id;
            ProductId = productId;
            Rating = rating;
            Comment = comment;
            this.isApproved = isApproved;
            CreatedAt = createdAt;
            Product = product;
            CustomerProfile = customerProfile;
        }
    }
}
