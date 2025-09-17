using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MarketNet.Application.Common.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;

namespace Application.IntegrationTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureAppConfiguration((ctx, cfg) =>
            {
                var env = ctx.HostingEnvironment;
                cfg.AddJsonFile("appsettings.json", optional: true)
                   .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                   .AddEnvironmentVariables();
            });

            builder.ConfigureServices((ctx, services) =>
            {
                // 🔹 Mockear el IUserContext
                services.RemoveAll<IUserContext>();
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, "1"),
                    new Claim(ClaimTypes.NameIdentifier, "1"),
                    new Claim(ClaimTypes.Email, "test@example.com"),
                    new Claim(ClaimTypes.Name, "UsuarioPrueba")
                };
                var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "TestAuth"));
                var mockUserContext = new Mock<IUserContext>();
                mockUserContext.SetupGet(x => x.UserId).Returns(42);
                mockUserContext.SetupGet(x => x.Principal).Returns(principal);
                services.AddTransient<IUserContext>(_ => mockUserContext.Object);


            });
        }
    }
}
