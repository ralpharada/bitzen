namespace Bitzen.Application.Services
{
    public static class JwtSettings
    {
        public static string Secret = "fedaf7d1233b48e197b9287d492b708e";
        public static int ValidForMinutes = 480;
        public static int RefreshTokenValidForMinutes = 2880;
        public static DateTime IssuedAt => DateTime.UtcNow;
        public static DateTime AccessTokenExpiration => IssuedAt.AddMinutes(ValidForMinutes);
        public static DateTime RefreshTokenExpiration => IssuedAt.AddMinutes(RefreshTokenValidForMinutes);
    }
}
