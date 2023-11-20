using Bitzen.Domain.Interfaces;
using Bitzen.Domain.Models;
using Bitzen.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Bitzen.Infra.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _context;
        public RefreshTokenRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AtualizarPorUsuarioId(IRefreshToken refreshToken)
        {
            var currentRefreshToken = await _context.RefreshTokens.SingleOrDefaultAsync(x => x.UsuarioId.Equals(refreshToken.UsuarioId));
            if (currentRefreshToken != null)
            {
                _context.RefreshTokens.Remove(currentRefreshToken);
            }
            _context.RefreshTokens.Add((RefreshToken)refreshToken);
            await _context.SaveChangesAsync();
        }
        public async Task<RefreshToken> ObterPorChaveUsuario(string refreshToken)
        {
            return await _context.RefreshTokens.SingleOrDefaultAsync(x => x.Token == refreshToken);
        }
    }
}
