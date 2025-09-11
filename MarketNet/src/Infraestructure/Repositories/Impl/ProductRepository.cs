using MarketNet.src.Application.Products.Dto;
using MarketNet.src.Domain.Entities.Products;
using MarketNet.src.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MarketNet.src.Infraestructure.Repositories.Impl
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public Task<Product> SearchById(long id)
        {
            return _context.Products.Where(p => p.Id == id).Include(p => p.Categories).FirstOrDefaultAsync();
        }

        public Task<Product> SearchByProductCode(string productCode)
        {
            return _context.Products.Where(p => p.Code == productCode).Include(p => p.Categories).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> SearchProductsAsync(ProductSearchCriteria criteria)
        {
            IQueryable<Product> query = _dbSet;

            if (!string.IsNullOrWhiteSpace(criteria.Code))
            {
                query = query.Where(p => p.Code.Contains(criteria.Code, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(criteria.Name))
            {
                query = query.Where(p => p.Name.Contains(criteria.Name, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(criteria.Description))
            {
                query = query.Where(p => p.Description.Contains(criteria.Description, StringComparison.OrdinalIgnoreCase));
            }

            if (criteria.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= criteria.MinPrice.Value);
            }

            if (criteria.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= criteria.MaxPrice.Value);
            }

            if (criteria.MinStock.HasValue)
            {
                query = query.Where(p => p.Stock >= criteria.MinStock.Value);
            }

            if (criteria.MaxStock.HasValue)
            {
                query = query.Where(p => p.Stock <= criteria.MaxStock.Value);
            }

            if (criteria.MinTaxRate.HasValue)
            {
                query = query.Where(p => p.TaxRate >= criteria.MinTaxRate.Value);
            }

            if (criteria.MaxTaxRate.HasValue)
            {
                query = query.Where(p => p.TaxRate <= criteria.MaxTaxRate.Value);
            }

            if (!string.IsNullOrWhiteSpace(criteria.Currency))
            {
                query = query.Where(p => p.Currency.Equals(criteria.Currency, StringComparison.OrdinalIgnoreCase));
            }

            if (criteria.IsActive.HasValue)
            {
                query = query.Where(p => p.IsActive == criteria.IsActive.Value);
            }

            return await query.Include(p => p.Categories).ToListAsync();
        }
    }
}
