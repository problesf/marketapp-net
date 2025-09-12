using MarketNet.Domain.entities.Customers;
using MarketNet.Domain.Entities.Products;

namespace MarketNet.Domain.entities.Reviews
{
    public class Review
    {
        public long Id { get; set; }

        public long ProductId { get; set; }

        public long CustomerId { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }

        public bool isApproved { get; set; }

        public DateTime CreatedAt { get; set; }

        public Product Product { get; set; }

        public Customer Customer { get; set; }


        public Review(long id, long productId, long customerId, int rating, string comment, bool isApproved, DateTime createdAt, Product product, Customer customer)
        {
            Id = id;
            ProductId = productId;
            CustomerId = customerId;
            Rating = rating;
            Comment = comment;
            this.isApproved = isApproved;
            CreatedAt = createdAt;
            Product = product;
            Customer = customer;
        }
    }
}
