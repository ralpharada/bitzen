namespace Bitzen.Domain.Interfaces
{
    public interface ICombustivel
    {
        int Id { get; set; }
        string Descricao { get; set; }
        decimal Preco { get; set; }
        DateTime DataCadastro { get; set; }
    }

}
