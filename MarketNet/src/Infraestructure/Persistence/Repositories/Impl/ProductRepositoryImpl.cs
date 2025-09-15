using MarketNet.Application.Products.Dto;
using MarketNet.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace MarketNet.Infraestructure.Persistence.Repositories.Impl
{
    public class ProductRepositoryImpl : GenericRepositoryImpl<Product>, IProductRepository
    {
        public ProductRepositoryImpl(AppDbContext context) : base(context) { }

        public Task<Product?> SearchById(long id)
        {
            return _context.Products
                .AsNoTracking()
                .Include(p => p.Categories)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task<Product?> SearchByProductCode(string productCode)
        {
            return _context.Products
                .AsNoTracking()
                .Include(p => p.Categories)
                .FirstOrDefaultAsync(p => EF.Functions.ILike(p.Code, productCode));
        }

        public async Task<IEnumerable<Product>> SearchProductsAsync(ProductSearchCriteria criteria)
        {

            IQueryable<Product> query = _dbSet.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(criteria.Code))
            {
                query = query.Where(p => EF.Functions.ILike(p.Code, criteria.Code));
            }

            if (!string.IsNullOrWhiteSpace(criteria.Name))
            {
                var pattern = $"%{criteria.Name}%";
                query = query.Where(p => EF.Functions.ILike(p.Name, pattern));
            }

            if (!string.IsNullOrWhiteSpace(criteria.Description))
            {
                var pattern = $"%{criteria.Description}%";
                query = query.Where(p => p.Description != null && EF.Functions.ILike(p.Description, pattern));
            }

            if (criteria.MinPrice.HasValue)
                query = query.Where(p => p.Price >= criteria.MinPrice.Value);

            if (criteria.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= criteria.MaxPrice.Value);

            if (criteria.MinStock.HasValue)
                query = query.Where(p => p.Stock >= criteria.MinStock.Value);

            if (criteria.MaxStock.HasValue)
                query = query.Where(p => p.Stock <= criteria.MaxStock.Value);

            if (criteria.MinTaxRate.HasValue)
                query = query.Where(p => p.TaxRate >= criteria.MinTaxRate.Value);

            if (criteria.MaxTaxRate.HasValue)
                query = query.Where(p => p.TaxRate <= criteria.MaxTaxRate.Value);

            if (!string.IsNullOrWhiteSpace(criteria.Currency))
            {
                query = query.Where(p => EF.Functions.ILike(p.Currency, criteria.Currency));
            }

            if (criteria.IsActive.HasValue)
            {
                query = query.Where(p => p.IsActive == criteria.IsActive.Value);

            }

            return await query
                .Include(p => p.Categories)
                .ToListAsync();
        }
    }
}
