using MarketApp.src.Domain.entities.order;
using MarketNet.src.Domain.entities.Inventory;
using MarketNet.src.Domain.entities.Reviews;
using MarketNet.src.Domain.Shared;

namespace MarketApp.src.Domain.entities.product
{

    public class Product : IEntity
    {
        public long Id { get; set; }
        public String Code { get; set; }

        public String Name { get; set; }
        public String Description { get; set; }
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
