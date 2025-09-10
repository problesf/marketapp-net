namespace MarketNet.src.Domain.entities.Customers
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

        public bool IsDefaultBilling { get; set; }

        public bool IsDefaultShipping { get; set; }

        public Customer Customer { get; set; }
    }
}
