using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MarketNet.Domain.Entities.User;
using MarketNet.src.Infraestructure.Auth.Parameters;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MarketNet.src.Infraestructure.Auth.Services.Impl
{
    public class JwtTokenServiceImpl : IJwtTokenService
    {

        private readonly JwtParameters _parameters;
        public JwtTokenServiceImpl(IOptions<JwtParameters> parameters) => _parameters = parameters.Value;

        public (string AccessToken, IEnumerable<String> Roles) CreateAccessToken(User user, IEnumerable<String> roles, IEnumerable<KeyValuePair<string, string>>? extraClaims = null)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_parameters.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
                {
                    new (JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new (JwtRegisteredClaimNames.UniqueName, user.Email),
                    new (ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new (ClaimTypes.Name, user.Email)
                };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                issuer: _parameters.Issuer,
                audience: null,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);


            return (jwt, roles);
        }
    }
}
