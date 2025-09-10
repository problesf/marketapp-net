using MarketApp.src.Domain.entities.product;
using MarketApp.src.Domain.enums;

namespace MarketNet.src.Domain.entities.Inventory
{
    public class InventoryMovement
    {
        public long Id { get; set; }

        public long ProductId { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }

        public Reason Reason { get; set; }

        public string Reference { get; set; }
    }
}
