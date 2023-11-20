using Bitzen.Application.Core;
using Bitzen.Application.Queries;
using Bitzen.Core.Events;
using Bitzen.Domain.Interfaces;
using MediatR;

namespace Bitzen.Application.Handlers
{
    public class SelecionarCombustivelHandler : IRequestHandler<SelecionarCombustivelQuery, IEvent>
    {
        private readonly ICombustivelRepository _combustivelRepository;
        private readonly UsuarioAutenticado _usuarioAutenticado;
        private readonly IUsuarioRepository _usuarioRepository;

        public SelecionarCombustivelHandler(ICombustivelRepository combustivelRepository,
            UsuarioAutenticado usuarioAutenticado,
            IUsuarioRepository usuarioRepository)
        {
            _combustivelRepository = combustivelRepository;
            _usuarioAutenticado = usuarioAutenticado;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEvent> Handle(SelecionarCombustivelQuery request, CancellationToken cancellationToken)
        {
            var success = false;
            try
            {
                #region Validação do usuário autenticado

                if (_usuarioAutenticado.Email == null)
                    return new ResultEvent(success, "Acesso expirado");

                var usuarioLogado = await _usuarioRepository.ObterPorEmailCadastroAtivo(_usuarioAutenticado.Email, cancellationToken);
                if (usuarioLogado == null)
                    return new ResultEvent(success, "Acesso expirado");

                #endregion

                var combustivel = await _combustivelRepository.ObterPorId(request.Id, cancellationToken);
         
                if (combustivel == null)
                    return new ResultEvent(success, "Combustivel não localizado.");

                return new ResultEvent(true, combustivel);
            }
            catch (Exception ex)
            {
                return new ResultEvent(success, ex.Message);
            }

        }

    }
}
