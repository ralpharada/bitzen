using Bitzen.Domain.Interfaces;
using Bitzen.Domain.Models;
using Bitzen.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Bitzen.Infra.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;
        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<int> Salvar(IUsuario entity, CancellationToken cancellationToken)
        {
            var usuario = await _context.Usuarios.FindAsync(entity.Id);

            if (usuario != null)
            {
                _context.Entry(usuario).CurrentValues.SetValues(entity);
            }
            else
            {
                _context.Usuarios.Add((Usuario)entity);
            }

            return await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<IUsuario> ObterPorEmailCadastroAtivo(string email, CancellationToken cancellationToken)
        {
            return await _context.Usuarios.SingleOrDefaultAsync(x => x.Email == email && x.Status, cancellationToken);
        }
        public async Task<IUsuario> ObterPorId(int id, CancellationToken cancellationToken)
        {
            return await _context.Usuarios.SingleOrDefaultAsync(x => x.Id == id && x.Status, cancellationToken);
        }
        public async Task<IEnumerable<IUsuario>> ListarTodos( CancellationToken cancellationToken)
        {
            return await _context.Usuarios.ToListAsync(cancellationToken);
        }
    }
}
