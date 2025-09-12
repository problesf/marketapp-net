using MarketNet.Domain.entities.Inventory;
using MarketNet.Domain.entities.Reviews;
using MarketNet.Domain.Entities.Order;
using MarketNet.Domain.Shared;

namespace MarketNet.Domain.Entities.Products
{

    public class Product : IEntity
    {
        public long? Id { get; set; }
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
        

        public Product(long? id, string code, string name, string description, decimal price, int stock, decimal taxRate, string currency, bool isActive)
        {
            Id = id;
            Code = code;
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            TaxRate = taxRate;
            Currency = currency;
            IsActive = isActive;
        }
    }
}
