using MarketNet.src.Domain.Enums;
using Microsoft.VisualBasic;

namespace MarketNet.src.Domain.Entities.Order
{
    public class Payment
    {
        public long Id { get; set; }

        public long OrderId { get; set; }

        public Provider Provider { get; set; }

        public Status Status { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public string ExternalReference { get; set; }

        public DateTime OcurredAt { get; set; }

        public Order Order { get; set; }




    }
}
