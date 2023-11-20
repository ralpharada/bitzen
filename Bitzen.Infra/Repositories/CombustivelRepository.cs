using Bitzen.Domain.Interfaces;
using Bitzen.Domain.Models;
using Bitzen.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Bitzen.Infra.Repositories
{
    public class CombustivelRepository : ICombustivelRepository
    {
        private readonly AppDbContext _context;
        public CombustivelRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<int> Salvar(ICombustivel entity, CancellationToken cancellationToken)
        {
            var combustivel = await _context.Combustiveis.FindAsync(entity.Id);

            if (combustivel != null)
            {
                _context.Entry(combustivel).CurrentValues.SetValues(entity);
            }
            else
            {
                _context.Combustiveis.Add((Combustivel)entity);
            }

            return await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<bool> VerificaSeExistePorNome(string descricao, CancellationToken cancellationToken)
        {
            return await _context.Combustiveis.AnyAsync(x => x.Descricao.ToLower() == descricao.ToLower(), cancellationToken);
        }
        public async Task<ICombustivel> ObterPorId(int id, CancellationToken cancellationToken)
        {
            return await _context.Combustiveis.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
        public async Task<IEnumerable<ICombustivel>> ListarTodos(CancellationToken cancellationToken)
        {
            return await _context.Combustiveis.OrderBy(x => x.Descricao).ToListAsync(cancellationToken);
        }
    }
}
