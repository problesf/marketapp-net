using MarketNet.Domain.entities.Customers;
using MarketNet.Domain.entities.Reviews;
namespace MarketNet.Domain.Entities.User;

public class CustomerProfile
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public User User { get; set; } = null!;

    public long? DefaultBillingAddressId { get; set; }

    public Address? DefaultBillingAddress { get; set; }

    public long? DefaultShippingAddressId { get; set; }

    public Address? DefaultShippingAddress { get; set; }

    public ICollection<Address> Addresses { get; set; }
    public ICollection<Review> Reviews { get; set; } = new List<Review>();

    public ICollection<Order.Order> Orders { get; private set; } = new List<Order.Order>();

    public CustomerProfile() { }
}

