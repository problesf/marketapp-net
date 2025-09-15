using MarketNet.Domain.Entities.User;

namespace MarketNet.src.Infraestructure.Auth.Services
{
    public interface IJwtTokenService
    {
        public (string AccessToken, IEnumerable<String> Roles) CreateAccessToken(User user, IEnumerable<String> roles, IEnumerable<KeyValuePair<string, string>>? extraClaims = null);
    }

}
