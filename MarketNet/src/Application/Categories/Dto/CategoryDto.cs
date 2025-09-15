namespace MarketNet.Application.Categories.Dto
{
    public class CategoryDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }

        public long? ParentCategoryId { get; set; }
        public CategoryBriefDto? ParentCategory { get; set; }
        public List<CategoryChildDto> ChildCategories { get; set; } = new();
    }

    public class CategoryBriefDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
    }

    public class CategoryChildDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
    }
}
