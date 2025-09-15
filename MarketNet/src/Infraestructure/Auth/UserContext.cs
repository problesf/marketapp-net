using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MarketNet.Application.Common.Interfaces;

namespace MarketNet.Infrastructure.Auth
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _http;

        public UserContext(IHttpContextAccessor http) => _http = http;

        public long? UserId
        {
            get
            {
                var sub = Principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
                       ?? Principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrWhiteSpace(sub))
                    throw new UnauthorizedAccessException("No se encontró el claim 'sub' en el token.");

                return long.Parse(sub);
            }
        }

        public ClaimsPrincipal Principal => _http.HttpContext?.User ?? new ClaimsPrincipal();
    }
}
