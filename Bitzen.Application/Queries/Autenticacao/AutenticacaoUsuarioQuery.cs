using Bitzen.Application.Core;
using Bitzen.Core.Events;

namespace Bitzen.Application.Queries
{
    public class AutenticacaoUsuarioQuery : Request<IEvent>
    {
        public string Email { get; }
        public string Password { get; }
        public AutenticacaoUsuarioQuery(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
