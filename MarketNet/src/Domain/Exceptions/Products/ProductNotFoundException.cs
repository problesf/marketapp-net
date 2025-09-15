using MarketNet.Domain.Exceptions.Base;

namespace MarketNet.Domain.Exceptions.Products
{

    [Serializable]
    public class ProductNotFoundException : BaseException
    {
        public ProductNotFoundException(long id)
            : base("product_not_found", 404, $"No se encontró el producto con Id {id}.")
        {
        }

        public ProductNotFoundException(string code)
            : base("product_not_found", 404, $"No se encontró el producto con código {code}.")
        {
        }

        public ProductNotFoundException(string message, Exception? inner)
            : base("product_not_found", 404, message, inner)
        {
        }
    }
}