namespace Bitzen.Domain.Interfaces
{
    public interface IMotoristaRepository
    {
        Task<int> Salvar(IMotorista entity, CancellationToken cancellationToken);
        Task<IMotorista> ObterPorId(int id, CancellationToken cancellationToken);
        Task<bool> ExcluirPorId(int id, CancellationToken cancellationToken);
        Task<IEnumerable<IMotorista>> ListarTodos(CancellationToken cancellationToken);
        Task<bool> VerificaSeExistePorCPF(string cpf, CancellationToken cancellationToken);
    }

}
