namespace MarketNet.Infraestructure.Auth
{
    public static class PasswordHasher
    {
        public static bool Verify(string plain, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(plain, storedHash);
        }
    }
}
