using MarketApp.src.Domain.entities.product;
using MarketNet.src.Domain.entities.Customers;

namespace MarketNet.src.Domain.entities.Reviews
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


    }
}
