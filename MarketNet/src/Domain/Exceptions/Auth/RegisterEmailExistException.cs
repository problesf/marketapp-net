using MarketNet.Domain.Exceptions.Base;

namespace MarketNet.src.Domain.Exceptions.Auth
{
    public class RegisterEmailExistException : BaseException
    {
        public RegisterEmailExistException(string email)
            : base("email_exists", 409, $"Ya existe una usuario con email {email}.")
        {
        }

    }
}
