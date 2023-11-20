using Microsoft.Extensions.Configuration;

namespace Bitzen.Application.Crypto
{
    public class Criptografia
    {
        private readonly IConfiguration _configuration;
        public Criptografia(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public static string Encrypt(string valor)
        {
            return BCrypt.Net.BCrypt.HashPassword(valor, BCrypt.Net.BCrypt.GenerateSalt(12));
        }
        public static bool Verify(string valor, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(valor, hash);
        }
    }
}
