using MarketNet.Domain.Enums;
using MarketNet.Domain.valueObjects;
using Microsoft.VisualBasic;

namespace MarketNet.Domain.Entities.Order
{
    public class Order
    {
        public long Id { get; set; }

        public int OrderNumber { get; set; }

        public long CustomerId { get; set; }

        public Status Status { get; set; }

        public decimal SubTotal { get; set; }

        public decimal TaxTotal { get; set; }

        public decimal ShippingTotal { get; set; }

        public decimal DiscountTotal { get; set; }

        public decimal GrandTotal { get; set; }

        public string Currency { get; set; }

        public DateTime PlaceAt { get; set; }

        public DateTime? PaidAt { get; set; }

        public DateTime? CancelledAt { get; set; }

        public DateTime? DeliveredAt { get; set; }

        public ICollection<OrderItem> Items { get; set; }

        public ICollection<Payment> Payments { get; set; }

        public ICollection<Shipment> Shipments { get; set; }
        public AddressSnapshot ShippingAddressSnapshot { get; }
        public AddressSnapshot BillingAddressSnapshot { get; }
    }
}
