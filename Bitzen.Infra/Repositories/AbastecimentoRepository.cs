using Bitzen.Domain.Interfaces;
using Bitzen.Domain.Models;
using Bitzen.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Bitzen.Infra.Repositories
{
    public class AbastecimentoRepository : IAbastecimentoRepository
    {
        private readonly AppDbContext _context;
        public AbastecimentoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<int> Salvar(IAbastecimento entity, CancellationToken cancellationToken)
        {
            try
            {
                _context.Abastecimentos.Add((Abastecimento)entity);
                return await _context.SaveChangesAsync(cancellationToken);
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IAbastecimento> ObterPorId(int id, CancellationToken cancellationToken)
        {
            return await _context.Abastecimentos.Include(x => x.Veiculo).Include(x => x.MotoristaResponsavel).Include(x => x.Combustivel).SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
        public async Task<IEnumerable<IAbastecimento>> ListarTodos(CancellationToken cancellationToken)
        {
            return await _context.Abastecimentos.Include(x => x.Veiculo).Include(x => x.MotoristaResponsavel).Include(x => x.Combustivel).OrderByDescending(x=>x.Id).ToListAsync(cancellationToken);
        }
    }
}
