using MarketNet.Application.Categories.Dto;
using MarketNet.Domain.Entities.Products;

namespace MarketNet.Infraestructure.Persistence.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> Search(CategorySearchCriteria criteria);
        Task<Category> SearchById(long id);
        Task<Category> SearchBySlug(string slug);
    }
}
