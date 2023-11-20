namespace Bitzen.Domain.Interfaces
{
    public interface IAbastecimentoRepository
    {
        Task<int> Salvar(IAbastecimento entity, CancellationToken cancellationToken);
        Task<IAbastecimento> ObterPorId(int id, CancellationToken cancellationToken);
        Task<IEnumerable<IAbastecimento>> ListarTodos(CancellationToken cancellationToken);
    }

}
