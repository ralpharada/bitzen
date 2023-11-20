using Bitzen.Application.Core;
using Bitzen.Core.Events;

namespace Bitzen.Application.Queries
{
    public class SelecionarVeiculoQuery : Request<IEvent>
    {
        public int Id { get; private set; }
        public SelecionarVeiculoQuery(int id)
        {
            Id = id;
        }
    }
}
