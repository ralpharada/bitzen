using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Bitzen.Application.Libraries
{
    public class Cookie
    {
        private IHttpContextAccessor _context;
        private IConfiguration _configuration;

        public Cookie(IHttpContextAccessor context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public void Cadastrar(string Key, string Valor)
        {
            CookieOptions Options = new()
            {
                Expires = DateTime.Now.AddDays(7),
                IsEssential = true
            };

            var ValorCrypt = StringCipher.Encrypt(Valor, _configuration.GetSection("KeyCrypt").Value);

            _context.HttpContext.Response.Cookies.Append(Key, ValorCrypt, Options);
        }
        public void Atualizar(string Key, string Valor)
        {
            if (Existe(Key))
            {
                Remover(Key);
            }
            Cadastrar(Key, Valor);
        }
        public void Remover(string Key)
        {
            _context.HttpContext.Response.Cookies.Delete(Key);
        }
        public string Consultar(string Key, bool Cript = true)
        {
            var valor = _context.HttpContext.Request.Cookies[Key];

            if (Cript)
            {
                valor = StringCipher.Decrypt(valor, _configuration.GetSection("KeyCrypt").Value);
            }
            return valor;
        }
        public bool Existe(string Key)
        {
            if (_context.HttpContext.Request.Cookies[Key] == null)
            {
                return false;
            }

            return true;
        }
        public void RemoverTodos()
        {
            var ListaCookie = _context.HttpContext.Request.Cookies.ToList();
            foreach (var cookie in ListaCookie)
            {
                Remover(cookie.Key);
            }

        }
    }
}
