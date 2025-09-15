namespace MarketNet.Infraestructure.Auth.Parameters
{
    public class JwtParameters
    {
        public string Issuer { get; init; } = default!;
        public string Key { get; init; } = default!;
        public int AccessTokenMinutes { get; init; } = 120;
    }
}
