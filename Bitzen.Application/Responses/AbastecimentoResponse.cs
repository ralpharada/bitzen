namespace Bitzen.Application.Responses
{
    public class AbastecimentoResponse
    {
        public int Id { get; set; }
        public VeiculoResponse Veiculo { get; set; } = null!;
        public MotoristaResponse MotoristaResponsavel { get; set; } = null!;
        public CombustivelResponse Combustivel { get; set; } = null!;
        public string Data { get; set; } = null!;
        public int QuantidadeAbastecida { get; set; }
        public decimal CombustivelPreco { get; set; }
        public decimal TotalAbastecimento { get; set; }
    }
}
