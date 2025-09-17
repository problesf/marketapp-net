using MarketNet.Domain.Entities.Products;
using MarketNet.src.Infraestructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MarketNet.Infraestructure.Persistence.Repositories.Impl
{
    public class PAttributeRepositoryImpl : GenericRepositoryImpl<PAttribute>, IPAttributeRepository
    {
        public PAttributeRepositoryImpl(AppDbContext context) : base(context) { }

        public async Task<bool> DropByProductId(long productId)
        {
            return await _context.PAttributes.Where(pa => pa.ProductId == productId).ExecuteDeleteAsync() > 0;
        }

        public async Task<IEnumerable<PAttribute>> SearchByProduct(long productId)
        {
            return await _context.PAttributes.AsTracking().Where(pa => pa.ProductId == productId).ToListAsync();
        }


    }
}
