namespace Bitzen.Domain.Interfaces
{
    public interface IJsonWebToken
    {
        string AccessToken { get; set; }
        IRefreshToken RefreshToken { get; set; }
        string TokenType { get; set; }
        long ExpiresIn { get; set; }
    }

}
