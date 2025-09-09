namespace MarketApp.src.Domain.entities
{
    public class Address
    {
        public long Id { get; set; }

        public long CustomerId { get; set; }

        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string PostalCode { get; set; }

        public Boolean IsDefaultBilling { get; set; }

        public Boolean IsDefaultShipping { get; set; }

        public Customer Customer { get; set; }
    }
}
