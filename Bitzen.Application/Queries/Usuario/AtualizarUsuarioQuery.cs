using Bitzen.Application.Core;
using Bitzen.Core.Events;

namespace Bitzen.Application.Queries
{
    public class AtualizarUsuarioQuery : Request<IEvent>
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public bool Status { get; private set; }
        public AtualizarUsuarioQuery(int id,  string nome, string email,string senha, bool status)
        {
            Id = id;
            Nome = nome.Trim();
            Email = email.Trim();
            Status = status;
            Senha = senha.Trim();
        }
    }
}
