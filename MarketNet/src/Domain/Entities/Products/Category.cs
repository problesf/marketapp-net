using MarketNet.Domain.Shared;

namespace MarketNet.Domain.Entities.Products
{
    public class Category : IEntity
    {
        private Category() { }

        public Category(string name, string slug, string description, long? parentCategoryId, List<Category> childCategories)
        {
            Name = name;
            Slug = slug;
            Description = description;
            ParentCategoryId = parentCategoryId;
            ChildCategories = childCategories;
        }

        public long? Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;

        public long? ParentCategoryId { get; set; }
        public Category? ParentCategory { get; set; }
        public ICollection<Category> ChildCategories { get; set; } = new List<Category>();
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}