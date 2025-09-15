using MarketNet.Domain.Exceptions.Base;

namespace MarketNet.Domain.Exceptions.Categories
{
    [Serializable]
    public class CategoryNotFoundException : BaseException
    {
        public CategoryNotFoundException(long id)
            : base("category_not_found", 404, $"No se encontró la categoría con Id {id}.")
        {
        }

        public CategoryNotFoundException(string slug)
            : base("category_not_found", 404, $"No se encontró la categoría con slug {slug}.")
        {
        }

        public CategoryNotFoundException(string message, Exception? inner)
            : base("category_not_found", 404, message, inner)
        {
        }
    }
}