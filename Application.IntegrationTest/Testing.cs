using Application.IntegrationTests;
using MarketNet.Application.Common.Interfaces;
using MarketNet.Infraestructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Application.IntegrationTest
{
    [SetUpFixture]
    public class Testing
    {
        private static WebApplicationFactory<Program> _factory = null!;
        private static ISender _mediator = null!;
        private static DbContext _dbContext;
        private static IUserContext _userContext;
        private static IConfiguration _configuration = null!;
        private static IServiceScopeFactory _scopeFactory = null!;

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            _factory = new CustomWebApplicationFactory();
            _ = _factory.Server; // fuerza inicialización del host

            _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
            _mediator = _factory.Services.GetRequiredService<ISender>();
            _configuration = _factory.Services.GetRequiredService<IConfiguration>();
            _dbContext = _factory.Services.GetRequiredService<AppDbContext>();

            var db = _factory.Services.GetRequiredService<AppDbContext>();
            Console.WriteLine($"[TEST] Provider = {db.Database.ProviderName}");

            Assert.That(db.Database.ProviderName,
                Is.EqualTo("Microsoft.EntityFrameworkCore.InMemory"),
                "El proveedor de EF durante los tests debe ser InMemory");
        }
        public static async Task ResetState()
        {
            if (_configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                _dbContext.Database.EnsureDeleted();
                _dbContext.Database.EnsureCreated();
                _dbContext.ChangeTracker.Clear();
            }
        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request) => await _mediator.Send(request!);

        public static async Task<TEntity> FindByKeysAsync<TEntity>(
            object[] keys, params string[] includes) where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();
            var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var entity = await ctx.FindAsync<TEntity>(keys);
            if (entity is null) return null;

            foreach (var nav in includes)
                await ctx.Entry(entity).Navigation(nav).LoadAsync();

            return entity;
        }

        public static async Task<List<TEntity>> FindAll<TEntity>() where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            return await context.Set<TEntity>().ToListAsync();
        }

        public static async Task<int> AddAsync<TEntity>(TEntity entity) where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await context.AddAsync(entity);

            return await context.SaveChangesAsync();
        }
    }
}
