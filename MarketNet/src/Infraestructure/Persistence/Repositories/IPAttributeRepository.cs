using MarketNet.Domain.Entities.Products;
using MarketNet.Infraestructure.Persistence.Repositories;

namespace MarketNet.src.Infraestructure.Persistence.Repositories
{
    public interface IPAttributeRepository : IRepository<PAttribute>
    {
        Task<IEnumerable<PAttribute>> SearchByProduct(long productId);
        Task<Boolean> DropByProductId(long productId);

    }
}
