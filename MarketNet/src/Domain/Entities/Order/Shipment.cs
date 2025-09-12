using MarketNet.Domain.Enums;
using Microsoft.VisualBasic;

namespace MarketNet.Domain.Entities.Order
{
    public class Shipment
    {
        public long Id { get; set; }

        public long OrderId { get; set; }

        public string Carrier { get; set; }

        public string TrackingNumber { get; set; }

        public Status Status { get; set; }

        public DateTime ShippedAt {  get; set; }

        public DateTime? DeliveredAt { get; set; }

        public decimal Cost { get; set; }
        public Order Order { get; set; }

    }
}
