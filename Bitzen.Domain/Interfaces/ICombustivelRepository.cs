namespace Bitzen.Domain.Interfaces
{
    public interface ICombustivelRepository
    {
        Task<int> Salvar(ICombustivel entity, CancellationToken cancellationToken);
        Task<ICombustivel> ObterPorId(int id, CancellationToken cancellationToken);
        Task<IEnumerable<ICombustivel>> ListarTodos(CancellationToken cancellationToken);
        Task<bool> VerificaSeExistePorNome(string descricao, CancellationToken cancellationToken);
    }

}
