using Bitzen.Application.Core;
using Bitzen.Core.Events;

namespace Bitzen.Application.Queries
{
    public class SelecionarMotoristaQuery : Request<IEvent>
    {
        public int Id { get; private set; }
        public SelecionarMotoristaQuery(int id)
        {
            Id = id;
        }
    }
}
