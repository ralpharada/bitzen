using Bitzen.Domain.Interfaces;
using Bitzen.Domain.Models;
using Bitzen.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Bitzen.Infra.Repositories
{
    public class MotoristaRepository : IMotoristaRepository
    {
        private readonly AppDbContext _context;
        public MotoristaRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<int> Salvar(IMotorista entity, CancellationToken cancellationToken)
        {
            var combustivel = await _context.Motoristas.FindAsync(entity.Id);

            if (combustivel != null)
            {
                _context.Entry(combustivel).CurrentValues.SetValues(entity);
            }
            else
            {
                _context.Motoristas.Add((Motorista)entity);
            }

            return await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<IMotorista> ObterPorId(int id, CancellationToken cancellationToken)
        {
            return await _context.Motoristas.SingleOrDefaultAsync(x => x.Id == id && x.DataExclusao == null, cancellationToken);
        }
        public async Task<IEnumerable<IMotorista>> ListarTodos(CancellationToken cancellationToken)
        {
            return await _context.Motoristas.Where(x => x.DataExclusao == null).ToListAsync(cancellationToken);
        }

        public async Task<bool> ExcluirPorId(int id, CancellationToken cancellationToken)
        {
            try
            {
                var motorista = await _context.Motoristas.SingleOrDefaultAsync(x => x.Id == id && x.DataExclusao == null, cancellationToken);
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

        public async Task<bool> VerificaSeExistePorCPF(string cpf, CancellationToken cancellationToken)
        {
            return await _context.Motoristas.AnyAsync(x => x.CPF == cpf && x.DataExclusao == null, cancellationToken);
        }
    }
}
