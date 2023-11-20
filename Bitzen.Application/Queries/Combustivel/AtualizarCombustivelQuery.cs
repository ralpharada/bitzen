using Bitzen.Application.Core;
using Bitzen.Core.Events;

namespace Bitzen.Application.Queries
{
    public class AtualizarCombustivelQuery : Request<IEvent>
    {
        public int Id { get; private set; }
        public string Descricao { get; private set; }
        public decimal Preco { get; private set; }
        public AtualizarCombustivelQuery(int id,  string descricao, decimal preco)
        {
            Id = id;
            Descricao = descricao.Trim();
            Preco = preco;
        }
    }
}
