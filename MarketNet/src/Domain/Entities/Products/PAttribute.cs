using MarketNet.Domain.Shared;

namespace MarketNet.Domain.Entities.Products
{
    public class PAttribute : IEntity
    {
        public long? Id { get; set; }
        public string Name { get; set; }

        public string Value { get; set; }

        public long ProductId { get; set; }
        public Product Product { get; set; } = default!;

    }
}
