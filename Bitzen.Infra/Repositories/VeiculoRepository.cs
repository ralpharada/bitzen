using Bitzen.Domain.Interfaces;
using Bitzen.Domain.Models;
using Bitzen.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Bitzen.Infra.Repositories
{
    public class VeiculoRepository : IVeiculoRepository
    {
        private readonly AppDbContext _context;
        public VeiculoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<int> Salvar(IVeiculo entity, CancellationToken cancellationToken)
        {
            var combustivel = await _context.Veiculos.FindAsync(entity.Id);

            if (combustivel != null)
            {
                _context.Entry(combustivel).CurrentValues.SetValues(entity);
            }
            else
            {
                _context.Veiculos.Add((Veiculo)entity);
            }

            return await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<IVeiculo> ObterPorId(int id, CancellationToken cancellationToken)
        {
            return await _context.Veiculos.Include(x=>x.Combustivel).SingleOrDefaultAsync(x => x.Id == id && x.DataExclusao == null, cancellationToken);
        }
        public async Task<IEnumerable<IVeiculo>> ListarTodos(CancellationToken cancellationToken)
        {
            return await _context.Veiculos.Include(x => x.Combustivel).Where(x => x.DataExclusao == null).ToListAsync(cancellationToken);
        }

        public async Task<bool> ExcluirPorId(int id, CancellationToken cancellationToken)
        {
            try
            {
                var motorista = await _context.Veiculos.SingleOrDefaultAsync(x => x.Id == id && x.DataExclusao == null, cancellationToken);
                if (motorista != null)
                {
                    motorista.DataExclusao = DateTime.Now;
                    _context.Entry(motorista).CurrentValues.SetValues(motorista);
                    return await _context.SaveChangesAsync(cancellationToken) > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> VerificaSeExistePorPlaca(string placa, CancellationToken cancellationToken)
        {
            return await _context.Veiculos.AnyAsync(x => x.Placa == placa && x.DataExclusao == null, cancellationToken);
        }
    }
}
