using Bitzen.Application.Core;
using Bitzen.Core.Events;

namespace Bitzen.Application.Queries
{
    public class SelecionarAbastecimentoQuery : Request<IEvent>
    {
        public int Id { get; private set; }
        public SelecionarAbastecimentoQuery(int id)
        {
            Id = id;
        }
    }
}
