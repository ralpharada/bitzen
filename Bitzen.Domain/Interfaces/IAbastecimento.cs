using Bitzen.Domain.Models;

namespace Bitzen.Domain.Interfaces
{
    public interface IAbastecimento
    {
        int Id { get; set; }
        Veiculo Veiculo { get; set; }
        int VeiculoId { get; set; }
        Motorista MotoristaResponsavel { get; set; }
        int MotoristaResponsavelId { get; set; }
        string Data { get; set; }
        Combustivel Combustivel { get; set; }
        int CombustivelId { get; set; }
        int QuantidadeAbastecida { get; set; }
        decimal CombustivelPreco { get; set; }
        decimal TotalAbastecimento { get; set; }
    }

}
