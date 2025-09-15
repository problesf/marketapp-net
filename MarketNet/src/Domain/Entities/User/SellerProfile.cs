using MarketNet.Domain.Entities.Products;

namespace MarketNet.Domain.Entities.User;

public class SellerProfile
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public User User { get; set; } = null!;

    public string StoreName { get; set; } = null!;
    public string? PayoutAccount { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();

    private SellerProfile() { }
    public SellerProfile(string storeName, string? payoutAccount = null)
    {
        StoreName = storeName;
        PayoutAccount = payoutAccount;
    }
}

