using MarketNet.Domain.Exceptions.Base;

namespace MarketNet.Domain.Exceptions.Products
{
    [Serializable]
    public class ProductExistException : BaseException
    {
        public ProductExistException(string code)
            : base("product_exists", 409, $"Ya existe un producto con código {code}.")
        {
        }

        public ProductExistException(string message, Exception? inner)
            : base("product_exists", 409, message, inner)
        {
        }
    }
}