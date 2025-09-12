namespace MarketNet.Domain.entities.Customers
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

        public Address(long id, long customerId, string line1, string line2, string city, string state, string country, string postalCode, bool isDefaultBilling, bool isDefaultShipping, Customer customer)
        {
            Id = id;
            CustomerId = customerId;
            Line1 = line1;
            Line2 = line2;
            City = city;
            State = state;
            Country = country;
            PostalCode = postalCode;
            IsDefaultBilling = isDefaultBilling;
            IsDefaultShipping = isDefaultShipping;
            Customer = customer;
        }
    }
}
