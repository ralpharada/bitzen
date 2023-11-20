using Bitzen.Application.Core;
using Bitzen.Application.Queries;
using Bitzen.Core.Events;
using Bitzen.Domain.Interfaces;
using MediatR;

namespace Bitzen.Application.Handlers
{
    public class ObterUsuarioLogadoHandler : IRequestHandler<ObterUsuarioLogadoQuery, IEvent>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly UsuarioAutenticado _usuarioAutenticado;

        public ObterUsuarioLogadoHandler(IUsuarioRepository usuarioRepository,
            UsuarioAutenticado usuarioAutenticado)
        {
            _usuarioRepository = usuarioRepository;
            _usuarioAutenticado = usuarioAutenticado;
        }

        public async Task<IEvent> Handle(ObterUsuarioLogadoQuery request, CancellationToken cancellationToken)
        {
            var success = false;
            try
            {
                if (_usuarioAutenticado.Email == null)
                    return new ResultEvent(success, "Acesso expirado");

                var usuario = await _usuarioRepository.ObterPorEmailCadastroAtivo(_usuarioAutenticado.Email, cancellationToken);
                if (usuario == null)
                    return new ResultEvent(success, "Acesso expirado");

                return new ResultEvent(true, usuario);
            }
            catch (Exception ex)
            {
                return new ResultEvent(success, ex.Message);
            }

        }

    }
}
