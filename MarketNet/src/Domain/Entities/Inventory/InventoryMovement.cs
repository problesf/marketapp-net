using MarketNet.Domain.Entities.Products;
using MarketNet.Domain.Enums;

namespace MarketNet.Domain.entities.Inventory
{
    public class InventoryMovement
    {
        public long Id { get; set; }

        public long ProductId { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }

        public Reason Reason { get; set; }

        public string Reference { get; set; }

        public InventoryMovement(long id, long productId, Product product, int quantity, Reason reason, string reference)
        {
            Id = id;
            ProductId = productId;
            Product = product;
            Quantity = quantity;
            Reason = reason;
            Reference = reference;
        }
    }
}
