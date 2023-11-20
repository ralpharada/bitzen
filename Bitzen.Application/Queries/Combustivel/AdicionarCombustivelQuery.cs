using Bitzen.Application.Core;
using Bitzen.Core.Events;

namespace Bitzen.Application.Queries
{
    public class AdicionarCombustivelQuery : Request<IEvent>
    {
        public string Descricao { get; private set; }
        public decimal Preco { get; private set; }
        public AdicionarCombustivelQuery(string descricao, decimal preco)
        {
            Descricao = descricao.Trim();
            Preco = preco;
        }
    }
}
