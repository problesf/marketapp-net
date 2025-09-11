using MarketNet.src.Domain.Shared;

namespace MarketNet.src.Domain.Entities.Products
{
    public class Category : IEntity
    {
        public Category()
        {
            ChildCategories = new List<Category>();
            Products = new List<Product>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }

        public long? ParentCategoryId { get; set; }
        public virtual Category? ParentCategory { get; set; }
        public virtual ICollection<Category> ChildCategories { get; set; } = new List<Category>();
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
