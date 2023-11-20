using Bitzen.Domain.Interfaces;

namespace Bitzen.Domain.Models
{
    public class JsonWebToken: IJsonWebToken
    {
        public string AccessToken { get; set; } = null!;
        public IRefreshToken RefreshToken { get; set; } = null!;
        public string TokenType { get; set; } = "bearer";
        public long ExpiresIn { get; set; }
    }
}
