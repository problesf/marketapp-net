using MarketNet.src.Application.Categories.Dto;
using MarketNet.src.Domain.Entities.Products;
using MarketNet.src.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MarketNet.src.Infraestructure.Repositories.Impl
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public Task<Category> SearchById(long id)
        {
            return _context.Categories.Where(c => c.Id == id).Include(c => c.ParentCategory).FirstOrDefaultAsync();
        }

        public Task<Category> SearchBySlug(string slug)
        {
            return _context.Categories.Where(c => c.Slug == slug).Include(c => c.ParentCategory).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Category>> Search(CategorySearchCriteria criteria)
        {
            IQueryable<Category> query = _dbSet;

            if (!string.IsNullOrWhiteSpace(criteria.Slug))
            {
                query = query.Where(p => p.Slug.Contains(criteria.Slug, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(criteria.Name))
            {
                query = query.Where(p => p.Name.Contains(criteria.Name, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(criteria.Description))
            {
                query = query.Where(p => p.Description.Contains(criteria.Description, StringComparison.OrdinalIgnoreCase));
            }

            if (criteria.ParentCategoryId.HasValue)
            {
                query = query.Where(p => p.ParentCategoryId >= criteria.ParentCategoryId.Value);
            }

            return await query.Include(c => c.ParentCategory).ToListAsync();
        }
    }
}
