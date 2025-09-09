using MarketApp.src.Domain.entities.product;

namespace MarketApp.src.Domain.entities
{
    public class Review
    {
        public long Id { get; set; }

        public long ProductId { get; set; }

        public long CustomerId { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }

        public Boolean isApproved { get; set; }

        public DateTime CreatedAt { get; set; }

        public Product Product { get; set; }

        public Customer Customer { get; set; }


    }
}
