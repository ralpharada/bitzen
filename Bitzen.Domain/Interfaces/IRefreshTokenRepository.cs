using Bitzen.Domain.Models;

namespace Bitzen.Domain.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task AtualizarPorUsuarioId(IRefreshToken refreshToken);
        Task<RefreshToken> ObterPorChaveUsuario(string refreshToken);
    }

}
