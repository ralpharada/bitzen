using Bitzen.Application.Core;
using Bitzen.Core.Events;

namespace Bitzen.Application.Queries
{
    public class SelecionarUsuarioQuery : Request<IEvent>
    {
        public int Id { get; private set; }
        public SelecionarUsuarioQuery(int id)
        {
            Id = id;
        }
    }
}
