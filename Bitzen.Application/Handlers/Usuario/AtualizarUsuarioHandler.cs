using Bitzen.Application.Core;
using Bitzen.Application.Crypto;
using Bitzen.Application.Queries;
using Bitzen.Core.Events;
using Bitzen.Domain.Interfaces;
using MediatR;

namespace Bitzen.Application.Handlers
{
    public class AtualizarUsuarioHandler : IRequestHandler<AtualizarUsuarioQuery, IEvent>
    {
        private readonly IUsuarioRepository _repository;
        private readonly UsuarioAutenticado _usuarioAutenticado;
        public AtualizarUsuarioHandler(IUsuarioRepository repository,
            UsuarioAutenticado usuarioAutenticado)
        {
            _repository = repository;
            _usuarioAutenticado = usuarioAutenticado;
        }

        public async Task<IEvent> Handle(AtualizarUsuarioQuery request, CancellationToken cancellationToken)
        {
            var success = false;
            try
            {
                IUsuario usuarioLogado = await _repository.ObterPorEmailCadastroAtivo(_usuarioAutenticado.Email, cancellationToken);
                if (usuarioLogado == null)
                    return new ResultEvent(success, "Acesso expirado");

                var usuario = await _repository.ObterPorId(request.Id, cancellationToken);
                if (usuario == null)
                    return new ResultEvent(success, "Não foi possível localizar o usuário.");

                var existsUser = await _repository.ObterPorEmailCadastroAtivo(request.Email, cancellationToken);
                if (existsUser != null && existsUser.Id != usuario.Id)
                    return new ResultEvent(success, "Jà existe um usuário com esse e-mail");

                usuario.Nome = request.Nome;
                usuario.Email = request.Email;
                usuario.Status = request.Status;
                if (!string.IsNullOrEmpty(request.Senha))
                    usuario.Senha = Criptografia.Encrypt(request.Senha);

                var result = await _repository.Salvar(usuario, cancellationToken);
                success = result > 0;

                return new ResultEvent(success, success ? "Usuário atualizado com sucesso!" : "Nenhuma atualização realizada.");
            }
            catch (Exception ex)
            {
                return new ResultEvent(success, ex.Message);
            }
        }

    }
}
