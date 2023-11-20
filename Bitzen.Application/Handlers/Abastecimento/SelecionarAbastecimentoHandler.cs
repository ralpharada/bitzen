using Bitzen.Application.Core;
using Bitzen.Application.Mapper;
using Bitzen.Application.Queries;
using Bitzen.Application.Responses;
using Bitzen.Core.Events;
using Bitzen.Domain.Interfaces;
using MediatR;

namespace Bitzen.Application.Handlers
{
    public class SelecionarAbastecimentoHandler : IRequestHandler<SelecionarAbastecimentoQuery, IEvent>
    {
        private readonly IAbastecimentoRepository _abastecimentoRepository;
        private readonly UsuarioAutenticado _usuarioAutenticado;
        private readonly IUsuarioRepository _usuarioRepository;

        public SelecionarAbastecimentoHandler(IAbastecimentoRepository abastecimentoRepository,
            UsuarioAutenticado usuarioAutenticado,
            IUsuarioRepository usuarioRepository)
        {
            _abastecimentoRepository = abastecimentoRepository;
            _usuarioAutenticado = usuarioAutenticado;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEvent> Handle(SelecionarAbastecimentoQuery request, CancellationToken cancellationToken)
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

                var abastecimento = await _abastecimentoRepository.ObterPorId(request.Id, cancellationToken);
                var abastecimentoMap = AbastecimentoMapper<AbastecimentoResponse>.Map(abastecimento);
                if (abastecimento == null)
                    return new ResultEvent(success, "Abastecimento não localizado.");

                return new ResultEvent(true, abastecimentoMap);
            }
            catch (Exception ex)
            {
                return new ResultEvent(success, ex.Message);
            }

        }

    }
}
