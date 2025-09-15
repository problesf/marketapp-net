using MarketNet.Application.Products.Dto;
using MarketNet.Domain.Entities.Products;

namespace MarketNet.Infraestructure.Persistence.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> SearchProductsAsync(ProductSearchCriteria criteria);
        Task<Product> SearchByProductCode(string productCode);
        Task<Product> SearchById(long id);

    }
}
