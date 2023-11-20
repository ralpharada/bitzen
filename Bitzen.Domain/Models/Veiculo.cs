using Bitzen.Domain.Interfaces;

namespace Bitzen.Domain.Models
{
    public partial class Veiculo : IVeiculo
    {
        public int Id { get; set; }
        public string Placa { get; set; } = null!;
        public string NomeVeiculo { get; set; } = null!;
        public Combustivel Combustivel { get; set; } = null!;
        public int CombustivelId { get; set; } 
        public string Fabricante { get; set; } = null!;
        public int AnoFabricacao { get; set; }
        public int CapacidadeMaximaTanque { get; set; }
        public string Observacoes { get; set; } = null!;
        public DateTime DataCadastro { get; set; }
        public DateTime? DataExclusao { get; set; } = null!;
    }
}
