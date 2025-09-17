using MarketNet.Application.Categories.Dto;
using MarketNet.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace MarketNet.Infraestructure.Persistence.Repositories.Impl
{
    public class CategoryRepositoryImpl : GenericRepositoryImpl<Category>, ICategoryRepository
    {
        public CategoryRepositoryImpl(AppDbContext context) : base(context) { }

        public Task<Category?> SearchById(long id)
        {
            return _context.Categories
                .Include(c => c.ParentCategory)
                .AsTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task<Category?> SearchBySlug(string slug)
        {
            return _context.Categories
                .Include(c => c.ParentCategory).AsTracking()
                .FirstOrDefaultAsync(c => c.Slug == slug);
        }

        public async Task<IEnumerable<Category>> Search(CategorySearchCriteria criteria)
        {
            IQueryable<Category> query = _dbSet.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(criteria.Slug))
            {
                query = query.Where(c => EF.Functions.ILike(c.Slug, criteria.Slug));
            }

            if (!string.IsNullOrWhiteSpace(criteria.Name))
            {
                var pattern = $"%{criteria.Name}%";
                query = query.Where(c => EF.Functions.ILike(c.Name, pattern));
            }

            if (!string.IsNullOrWhiteSpace(criteria.Description))
            {
                var pattern = $"%{criteria.Description}%";
                query = query.Where(c => c.Description != null && EF.Functions.ILike(c.Description, pattern));
            }

            if (criteria.ParentCategoryId.HasValue)
            {
                var pid = criteria.ParentCategoryId.Value;
                query = query.Where(c => c.ParentCategoryId == pid);
            }

            return await query
                .AsTracking()
                .Include(c => c.ParentCategory)
                .ToListAsync();
        }
    }
}
