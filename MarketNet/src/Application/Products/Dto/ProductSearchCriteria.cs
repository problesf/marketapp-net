namespace MarketNet.Application.Products.Dto
{
    public record ProductSearchCriteria
    {
        public string? Code { get; init; }
        public string? Name { get; init; }
        public string? Description { get; init; }
        public decimal? MinPrice { get; init; }
        public decimal? MaxPrice { get; init; }
        public int? MinStock { get; init; }
        public int? MaxStock { get; init; }
        public decimal? MinTaxRate { get; init; }
        public decimal? MaxTaxRate { get; init; }
        public string? Currency { get; init; }
        public bool? IsActive { get; init; }
    }
}
