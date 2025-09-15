using System.Security.Claims;

namespace MarketNet.Application.Common.Interfaces
{
    public interface IUserContext
    {
        long? UserId { get; }
        ClaimsPrincipal Principal { get; }
    }
}
