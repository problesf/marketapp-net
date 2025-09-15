using MarketNet.Domain.Entities.User;
using MarketNet.Infraestructure.Persistence;
using MarketNet.Infraestructure.Repositories.Impl;
using Microsoft.EntityFrameworkCore;

namespace MarketNet.src.Infraestructure.Repositories;

public sealed class UserRepositoryImpl : GenericRepositoryImpl<User>, IUserRepository
{
    public UserRepositoryImpl(AppDbContext context) : base(context) { }

    public async Task<User?> FindByEmailAsync(string email, bool includeProfiles = false, CancellationToken ct = default)
    {
        IQueryable<User> q = _context.Users;
        if (includeProfiles)
            q = q.Include(u => u.CustomerProfile).Include(u => u.SellerProfile);

        return await q.FirstOrDefaultAsync(u => u.Email == email, ct);
    }


    public Task<bool> EmailExistsAsync(string email, CancellationToken ct = default)
        => _context.Users.AnyAsync(u => u.Email == email, ct);

    public Task<bool> HasCustomerProfileAsync(long userId, CancellationToken ct = default)
        => _context.CustomerProfiles.AnyAsync(p => p.UserId == userId, ct);

    public Task<bool> HasSellerProfileAsync(long userId, CancellationToken ct = default)
        => _context.SellerProfiles.AnyAsync(p => p.UserId == userId, ct);

    public async Task AddCustomerProfileAsync(long userId, CustomerProfile profile, CancellationToken ct = default)
    {
        profile.UserId = userId;
        _context.CustomerProfiles.Add(profile);
        await _context.SaveChangesAsync(ct);
    }

    public async Task AddSellerProfileAsync(long userId, SellerProfile profile, CancellationToken ct = default)
    {
        profile.UserId = userId;
        _context.SellerProfiles.Add(profile);
        await _context.SaveChangesAsync(ct);
    }
}
