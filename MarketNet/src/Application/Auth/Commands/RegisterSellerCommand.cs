using MarketNet.Domain.Entities.User;
using MarketNet.src.Application.Auth.Dto;
using MarketNet.src.Domain.Exceptions.Auth;
using MarketNet.src.Infraestructure.Auth.Services;
using MarketNet.src.Infraestructure.Repositories;
using MediatR;

namespace MarketNet.src.Application.Auth.Commands
{
    public record RegisterSellerCommand : IRequest<LoginResult>
    {
        public string Email { get; init; }
        public string Password { get; init; }
        public string StoreName { get; init; }
        public string? PayoutAccount { get; init; }

    }

    public class RegisterSellerCommandHandler(IUserRepository userRepository, IJwtTokenService jwtTokenService) : IRequestHandler<RegisterSellerCommand, LoginResult>
    {

        public async Task<LoginResult> Handle(RegisterSellerCommand request, CancellationToken cancellationToken)
        {


            var exists = await userRepository.EmailExistsAsync(request.Email);
            if (exists) throw new RegisterEmailExistException("Email ya registrado.");


            var user = new User
            {
                Email = request.Email.Trim(),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                SellerProfile = new(request.StoreName, request.PayoutAccount)
            };


            await userRepository.AddAsync(user);
            await userRepository.SaveAsync(cancellationToken);
            var (token, roles) = jwtTokenService.CreateAccessToken(user, ["Seller"]);
            return new LoginResult(token, roles);

        }

    }
}
