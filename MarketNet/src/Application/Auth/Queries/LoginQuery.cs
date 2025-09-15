using MarketNet.Application.Auth.Dto;
using MarketNet.Domain.Exceptions.Auth;
using MarketNet.Infraestructure.Auth;
using MarketNet.Infraestructure.Auth.Services;
using MarketNet.Infraestructure.Persistence.Repositories;
using MediatR;

namespace MarketNet.Application.Auth.Queries
{
    public record LoginQuery : IRequest<LoginResult>
    {
        public string Email { get; init; }
        public string Password { get; init; }
    }

    public class LoginQueryHandler(IUserRepository userRepository, IJwtTokenService jwtTokenService) : IRequestHandler<LoginQuery, LoginResult>
    {
        public async Task<LoginResult> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepository.FindByEmailAsync(request.Email, true);

            if (user is null) throw new InvalidCredentialsException();
            if (!PasswordHasher.Verify(request.Password, user.PasswordHash)) throw new InvalidCredentialsException();

            var userRoles = new List<string>();
            if (user.IsCustomer)
            {
                userRoles.Add("Customer");
            }
            if (user.IsSeller)
            {
                userRoles.Add("Seller");
            }
            var (token, roles) = jwtTokenService.CreateAccessToken(user, [.. userRoles]);



            return new LoginResult(token, roles);
        }


    }
}


