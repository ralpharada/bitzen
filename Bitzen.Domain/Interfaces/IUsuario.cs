namespace Bitzen.Domain.Interfaces
{
    public interface IUsuario
    {
        int Id { get; set; }
        string Nome { get; set; }
        string Email { get; set; }
        string Senha { get; set; }
        bool Status { get; set; }
        DateTime DataCadastro { get; set; }
    }

}
