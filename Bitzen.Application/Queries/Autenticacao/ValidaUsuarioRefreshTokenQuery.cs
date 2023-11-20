using Bitzen.Application.Core;
using Bitzen.Core.Events;

namespace Bitzen.Application.Queries
{
    public class ValidaUsuarioRefreshTokenQuery : Request<IEvent>
    {
        public string RefreshToken { get; }

        public ValidaUsuarioRefreshTokenQuery(string refreshToken)
        {
            RefreshToken = refreshToken;
        }
    }
}
