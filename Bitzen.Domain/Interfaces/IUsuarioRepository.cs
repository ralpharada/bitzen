namespace Bitzen.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<int> Salvar(IUsuario entity, CancellationToken cancellationToken);
        Task<IUsuario> ObterPorEmailCadastroAtivo(string email, CancellationToken cancellationToken);
        Task<IUsuario> ObterPorId(int id, CancellationToken cancellationToken);
        Task<IEnumerable<IUsuario>> ListarTodos(CancellationToken cancellationToken);
    }

}
