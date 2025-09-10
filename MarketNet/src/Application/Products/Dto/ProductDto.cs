namespace MarketNet.src.Application.Products.Dto
{
    public class ProductDto
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
    }
}
