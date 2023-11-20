using Bitzen.Application.Core;
using Bitzen.Core.Events;

namespace Bitzen.Application.Queries
{
    public class SelecionarCombustivelQuery : Request<IEvent>
    {
        public int Id { get; private set; }
        public SelecionarCombustivelQuery(int id)
        {
            Id = id;
        }
    }
}
