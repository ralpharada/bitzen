using Bitzen.Domain.Interfaces;

namespace Bitzen.Domain.Models
{
    public class Combustivel : ICombustivel
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = null!;
        public decimal Preco { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
