namespace MarketNet.src.Application.Auth.Dto
{
    public record LoginResult(string AccessToken, IEnumerable<String> Roles);

}
