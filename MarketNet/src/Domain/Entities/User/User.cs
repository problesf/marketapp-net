using MarketNet.Domain.Shared;

namespace MarketNet.Domain.Entities.User;

public class User : IEntity
{
    public long? Id { get; set; }
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; }
    public CustomerProfile? CustomerProfile { get; set; }
    public SellerProfile? SellerProfile { get; set; }

    public bool IsCustomer => CustomerProfile is not null;
    public bool IsSeller => SellerProfile is not null;

    public User() { }
}

