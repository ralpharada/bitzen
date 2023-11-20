using Bitzen.Domain.Models;

namespace Bitzen.Domain.Interfaces
{
    public interface IVeiculo
    {
        int Id { get; set; }
        string Placa { get; set; }
        string NomeVeiculo { get; set; }
        Combustivel Combustivel { get; set; }
        int CombustivelId { get; set; }
        string Fabricante { get; set; }
        int AnoFabricacao { get; set; }
        int CapacidadeMaximaTanque { get; set; }
        string Observacoes { get; set; }
        DateTime DataCadastro { get; set; }
        DateTime? DataExclusao { get; set; }
    }

}
