using MarketNet.src.Domain.entities.Inventory;
using MarketNet.src.Domain.entities.Reviews;
using MarketNet.src.Domain.Entities.Order;
using MarketNet.src.Domain.Shared;

namespace MarketNet.src.Domain.Entities.Products
{

    public class Product : IEntity
    {
        public long Id { get; set; }
        public string Code { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public decimal TaxRate { get; set; }
        public string Currency { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Category> Categories { get; set; } = new List<Category>();
        public ICollection<InventoryMovement> InventoryMovements { get; set; } = new List<InventoryMovement>();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
