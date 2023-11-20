
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Bitzen.Application.Core
{
    public class UsuarioAutenticado
    {
        private readonly IHttpContextAccessor _accessor;

        public UsuarioAutenticado(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Email => _accessor.HttpContext.User.Identity.Name;
        public string Name => GetClaimsIdentity().FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;
        public string PrimarySid => GetClaimsIdentity().FirstOrDefault(a => a.Type == ClaimTypes.PrimarySid)?.Value;

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }
    }
}
