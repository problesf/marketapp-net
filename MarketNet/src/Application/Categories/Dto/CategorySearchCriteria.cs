namespace MarketNet.Application.Categories.Dto
{
    public record CategorySearchCriteria
    {
        public long? Id { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public string? Description { get; set; }

        public long? ParentCategoryId { get; set; }
    }
}
