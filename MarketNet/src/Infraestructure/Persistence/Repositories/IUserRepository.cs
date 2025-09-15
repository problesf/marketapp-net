using MarketNet.Domain.Entities.User;

namespace MarketNet.Infraestructure.Persistence.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> FindByEmailAsync(string email, bool includeProfiles = false, CancellationToken ct = default);
        Task<bool> EmailExistsAsync(string email, CancellationToken ct = default);
        Task<bool> HasCustomerProfileAsync(long userId, CancellationToken ct = default);
        Task<bool> HasSellerProfileAsync(long userId, CancellationToken ct = default);
        Task AddCustomerProfileAsync(long userId, CustomerProfile profile, CancellationToken ct = default);
        Task AddSellerProfileAsync(long userId, SellerProfile profile, CancellationToken ct = default);
    }

}
