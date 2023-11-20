using Bitzen.Application.Core;
using Bitzen.Core.Events;

namespace Bitzen.Application.Queries
{
    public class ExcluirMotoristaQuery : Request<IEvent>
    {
        public int Id { get; private set; }
        public ExcluirMotoristaQuery(int id)
        {
            Id = id;
        }
    }
}
