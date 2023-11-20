using Bitzen.Domain.Interfaces;

namespace Bitzen.Application.Responses
{
    public class VeiculoResponse
    {
        public int Id { get; set; }
        public string Placa { get; set; } = null!;
        public string NomeVeiculo { get; set; } = null!;
        public ICombustivel Combustivel { get; set; } = null!;
        public string Fabricante { get; set; } = null!;
        public int AnoFabricacao { get; set; }
        public int CapacidadeMaximaTanque { get; set; }
        public string Observacoes { get; set; } = null!;
    }
}
