using Bitzen.Application.Core;
using Bitzen.Core.Events;

namespace Bitzen.Application.Queries
{
    public class AdicionarMotoristaQuery : Request<IEvent>
    {
        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public string NumeroCNH { get; private set; }
        public string CategoriaCNH { get; private set; }
        public string DataNascimento { get; private set; }
        public bool Status { get; private set; }
        public AdicionarMotoristaQuery(string nome, string cpf, string numeroCNH, string categoriaCNH, string dataNascimento, bool status)
        {
            Nome = nome.Trim();
            CPF = cpf.Trim();
            NumeroCNH = numeroCNH.Trim();
            CategoriaCNH = categoriaCNH.Trim();
            DataNascimento = dataNascimento;
            Status = status;
        }
    }
}
