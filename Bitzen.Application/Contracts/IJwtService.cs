using Bitzen.Domain.Interfaces;
namespace Bitzen.Application.Contracts
{
    public interface IJwtService
    {
        IJsonWebToken GenerateUsuarioToken(IUsuario user);
    }
}
