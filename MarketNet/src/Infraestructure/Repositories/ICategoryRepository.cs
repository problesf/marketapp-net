using MarketNet.src.Application.Categories.Dto;
using MarketNet.src.Domain.Entities.Products;

namespace MarketNet.src.Infraestructure.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> Search(CategorySearchCriteria criteria);
        Task<Category> SearchById(long id);
        Task<Category> SearchBySlug(string slug);
    }
}
