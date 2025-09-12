using MarketNet.Domain.Shared;

namespace MarketNet.Domain.Entities.Products
{
    public class Category : IEntity
    {
        public Category(long? id, string name, string slug, string description, long? parentCategoryId, Category? parentCategory)
        {
            Id = id;
            Name = name;
            Slug = slug;
            Description = description;
            ParentCategoryId = parentCategoryId;
        }

        public long? Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }

        public long? ParentCategoryId { get; set; }
        public virtual Category? ParentCategory { get; set; }
        public virtual ICollection<Category> ChildCategories { get; set; } = new List<Category>();
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
