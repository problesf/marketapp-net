using MarketNet.src.Application.Categories.Dto;

namespace MarketNet.src.Application.Products.Dto
{
    public class ProductDto
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
        public List<CategoryDto> Categories { get; set; } = new();
    }
}
