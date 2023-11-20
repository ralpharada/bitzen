using Bitzen.Application.Core;
using Bitzen.Application.Crypto;
using Bitzen.Application.Mapper;
using Bitzen.Application.Queries;
using Bitzen.Core.Events;
using Bitzen.Domain.Interfaces;
using Bitzen.Domain.Models;
using MediatR;

namespace Bitzen.Application.Handlers
{
    public class AdicionarUsuarioHandler : IRequestHandler<AdicionarUsuarioQuery, IEvent>
    {
        private readonly IUsuarioRepository _repository;
        private readonly UsuarioAutenticado _usuarioAutenticado;

        public AdicionarUsuarioHandler(IUsuarioRepository repository,
            UsuarioAutenticado usuarioAutenticado)
        {
            _repository = repository;
            _usuarioAutenticado = usuarioAutenticado;
        }

        public async Task<IEvent> Handle(AdicionarUsuarioQuery request, CancellationToken cancellationToken)
        {
            var success = false;
            try
            {
                if (_usuarioAutenticado.Email == null)
                    return new ResultEvent(success, "Acesso expirado");

                var usuarioLogado = await _repository.ObterPorEmailCadastroAtivo(_usuarioAutenticado.Email, cancellationToken);
                if (usuarioLogado == null)
                    return new ResultEvent(success, "Acesso expirado");

                if (String.IsNullOrWhiteSpace(request.Nome))
                    return new ResultEvent(success, "O campo Nome é obrigatório.");

                if (String.IsNullOrWhiteSpace(request.Email))
                    return new ResultEvent(success, "O campo Email é obrigatório.");

                if (String.IsNullOrWhiteSpace(request.Senha))
                    return new ResultEvent(success, "O campo Senha é obrigatório.");

                var existsUser = await _repository.ObterPorEmailCadastroAtivo(request.Email, cancellationToken);
                if (existsUser != null)
                    return new ResultEvent(success, "Jà existe um usuário com esse e-mail");

                var usuario = UsuarioMapper<Usuario>.Map(request);
                usuario.DataCadastro = DateTime.Now;
                usuario.Senha = Criptografia.Encrypt(request.Senha);
                var upsertedId = await _repository.Salvar(usuario, cancellationToken);
                success = upsertedId > 0;
              
                return new ResultEvent(success, success ? "Usuário cadastrado com sucesso!" : "Falha ao cadastrar o usuário!", null, upsertedId);
            }
            catch (Exception ex)
            {
                return new ResultEvent(success, ex.Message);
            }

        }

    }
}
