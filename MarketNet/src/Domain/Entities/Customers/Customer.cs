using MarketNet.Domain.entities.Reviews;

namespace MarketNet.Domain.entities.Customers
{
    public class Customer
    {
        public long Id { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public long DefaultBillingAddressId { get; set; }

        public Address DefaultBillingAddress { get; set; }

        public long DefaultShippingAddressId { get; set; }

        public Address DefaultShippingAddress { get; set; }

        public ICollection<Address> Addresses { get; set; }
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public Customer(long id, string email, string fullName, long defaultBillingAddressId, Address defaultBillingAddress, long defaultShippingAddressId, Address defaultShippingAddress, ICollection<Address> addresses)
        {
            Id = id;
            Email = email;
            FullName = fullName;
            DefaultBillingAddressId = defaultBillingAddressId;
            DefaultBillingAddress = defaultBillingAddress;
            DefaultShippingAddressId = defaultShippingAddressId;
            DefaultShippingAddress = defaultShippingAddress;
            Addresses = addresses;
        }
    }
}
