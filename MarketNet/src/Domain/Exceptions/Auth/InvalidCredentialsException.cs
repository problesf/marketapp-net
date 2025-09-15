using MarketNet.Domain.Exceptions.Base;

namespace MarketNet.Domain.Exceptions.Auth
{
    public sealed class InvalidCredentialsException : BaseException
    {
        public InvalidCredentialsException()
            : base("login_failed", 401, $"Credenciales incorrectas.")
        {
        }
    }

}
