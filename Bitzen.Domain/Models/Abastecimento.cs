using Bitzen.Domain.Interfaces;

namespace Bitzen.Domain.Models
{
    public partial class Abastecimento: IAbastecimento
    {
        public int Id { get; set; }
        public Veiculo Veiculo { get; set; } = null!;
        public int VeiculoId { get; set; }
        public Motorista MotoristaResponsavel { get; set; } = null!;
        public int MotoristaResponsavelId { get; set; }
        public string Data { get; set; } = null!;
        public Combustivel Combustivel { get; set; } = null!;
        public int CombustivelId { get; set; }
        public int QuantidadeAbastecida { get; set; }
        public decimal CombustivelPreco { get; set; }
        public decimal TotalAbastecimento { get; set; }
    }
}
