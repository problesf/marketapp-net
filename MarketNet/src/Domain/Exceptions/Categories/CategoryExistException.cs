namespace MarketNet.Domain.Exceptions.Categories
{
    [Serializable]
    class CategoryExistException : Exception
    {
        public CategoryExistException()
        {
        }

        public CategoryExistException(string? message) : base(message)
        {
        }

        public CategoryExistException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
