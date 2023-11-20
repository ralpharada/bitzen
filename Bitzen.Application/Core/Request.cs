using MediatR;

namespace Bitzen.Application.Core
{
    public abstract class Request<TResponse> :  IRequest<TResponse>
    {

    }
}
