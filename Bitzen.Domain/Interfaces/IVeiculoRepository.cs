namespace Bitzen.Domain.Interfaces
{
    public interface IVeiculoRepository
    {
        Task<int> Salvar(IVeiculo entity, CancellationToken cancellationToken);
        Task<IVeiculo> ObterPorId(int id, CancellationToken cancellationToken);
        Task<bool> ExcluirPorId(int id, CancellationToken cancellationToken);
        Task<IEnumerable<IVeiculo>> ListarTodos(CancellationToken cancellationToken);
        Task<bool> VerificaSeExistePorPlaca(string placa, CancellationToken cancellationToken);
    }

}
