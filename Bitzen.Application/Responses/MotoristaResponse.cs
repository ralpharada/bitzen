namespace Bitzen.Application.Responses
{
    public class MotoristaResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string CPF { get; set; } = null!;
        public string NumeroCNH { get; set; } = null!;
        public string CategoriaCNH { get; set; } = null!;
        public string DataNascimento { get; set; } = null!;
        public bool Status { get; set; }
    }
}
