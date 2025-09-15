using MarketNet.Domain.Entities.User;

namespace MarketNet.Domain.entities.Customers
{
    public class Address
    {
        public long Id { get; set; }

        public string Line1 { get; set; }

        public string? Line2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string PostalCode { get; set; }

        public bool IsDefaultBilling { get; set; }

        public bool IsDefaultShipping { get; set; }

        public CustomerProfile CustomerProfile { get; set; }
        public long CustomerProfileId { get; set; }

        public Address(){}
        public Address(long id, string line1, string line2, string city, string state, string country, string postalCode, bool isDefaultBilling, bool isDefaultShipping, CustomerProfile customerProfile)
        {
            Id = id;
            Line1 = line1;
            Line2 = line2;
            City = city;
            State = state;
            Country = country;
            PostalCode = postalCode;
            IsDefaultBilling = isDefaultBilling;
            IsDefaultShipping = isDefaultShipping;
            CustomerProfile = customerProfile;
        }
    }
}
