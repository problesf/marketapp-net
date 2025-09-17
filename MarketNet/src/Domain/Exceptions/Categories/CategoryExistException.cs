using MarketNet.Domain.Exceptions.Base;

namespace MarketNet.Domain.Exceptions.Categories
{
    [Serializable]
    public class CategoryExistException : BaseException
    {
        public CategoryExistException(string slug)
            : base("category_exists", 409, $"Ya existe una categoría con slug {slug}.")
        {
        }
        public CategoryExistException(long id)
            : base("category_exists", 409, $"Ya existe con id {id}.")
        {
        }

        public CategoryExistException(string slug, Exception? inner)
            : base("category_exists", 409, $"Ya existe una categoría con slug {slug}.", inner)
        {
        }
    }
}