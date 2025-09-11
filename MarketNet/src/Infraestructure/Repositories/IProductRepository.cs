using MarketNet.src.Application.Products.Dto;
using MarketNet.src.Domain.Entities.Products;

namespace MarketNet.src.Infraestructure.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> SearchProductsAsync(ProductSearchCriteria criteria);
        Task<Product> SearchByProductCode(string productCode);
        Task<Product> SearchById(long id);

    }
}
