using Bitzen.Application.Core;
using Bitzen.Core.Events;

namespace Bitzen.Application.Queries
{
    public class ExcluirVeiculoQuery : Request<IEvent>
    {
        public int Id { get; private set; }
        public ExcluirVeiculoQuery(int id)
        {
            Id = id;
        }
    }
}
