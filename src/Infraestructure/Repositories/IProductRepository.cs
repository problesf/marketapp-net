using MarketApp.src.Domain.entities.product;
using MarketNet.src.Application.Products.Criteria;

namespace MarketNet.src.Infraestructure.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> SearchProductsAsync(ProductSearchCriteria criteria);

    }
}
