using MarketNet.Domain.Entities.Products;

namespace MarketNet.Domain.Entities.Order
{
    public class OrderItem
    {
        public long Id { get; set; }

        public long OrderId { get; set; }

        public long ProductId { get; set; }

        public string CodeSnapshot{ get; set; }
        public string ProductNameSnapshot{ get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public decimal TaxRate { get; set; }

        public decimal LineTotal { get; set; }
        public Order Order { get; set; }
        public virtual Product Product { get; set; } 


    }
}
