namespace MarketNet.Domain.Exceptions.Base
{
    [Serializable]
    public abstract class BaseException : Exception
    {
        public string ErrorType { get; }   // ej: "category_exists"
        public int StatusCode { get; }     // ej: 409

        protected BaseException(string errorType, int statusCode, string? message = null, Exception? innerException = null)
            : base(message, innerException)
        {
            ErrorType = errorType;
            StatusCode = statusCode;
        }
    }
}