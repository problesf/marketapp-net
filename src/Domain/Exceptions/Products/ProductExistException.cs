namespace MarketNet.src.Domain.Exceptions.Products
{
    [Serializable]
    class ProductExistException : Exception
    {
        public ProductExistException()
        {
        }

        public ProductExistException(string? message) : base(message)
        {
        }

        public ProductExistException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
