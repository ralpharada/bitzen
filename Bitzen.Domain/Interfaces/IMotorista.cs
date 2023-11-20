
namespace Bitzen.Domain.Interfaces
{
    public interface IMotorista
    {
        int Id { get; set; }
        string Nome { get; set; }
        string CPF { get; set; }
        string NumeroCNH { get; set; }
        string CategoriaCNH { get; set; }
        string DataNascimento { get; set; }
        bool Status { get; set; }
        DateTime DataCadastro { get; set; }
        public DateTime? DataExclusao { get; set; }
    }

}
