using Bitzen.Application.Core;
using Bitzen.Application.Mapper;
using Bitzen.Application.Queries;
using Bitzen.Application.Responses;
using Bitzen.Core.Events;
using Bitzen.Domain.Interfaces;
using MediatR;

namespace Bitzen.Application.Handlers
{
    public class SelecionarVeiculoHandler : IRequestHandler<SelecionarVeiculoQuery, IEvent>
    {
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly UsuarioAutenticado _usuarioAutenticado;
        private readonly IUsuarioRepository _usuarioRepository;

        public SelecionarVeiculoHandler(IVeiculoRepository veiculoRepository,
            UsuarioAutenticado usuarioAutenticado,
            IUsuarioRepository usuarioRepository)
        {
            _veiculoRepository = veiculoRepository;
            _usuarioAutenticado = usuarioAutenticado;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEvent> Handle(SelecionarVeiculoQuery request, CancellationToken cancellationToken)
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

                var veiculo = await _veiculoRepository.ObterPorId(request.Id, cancellationToken);
                var veiculoMap = VeiculoMapper<VeiculoResponse>.Map(veiculo);
                if (veiculo == null)
                    return new ResultEvent(success, "Veiculo não localizado.");

                return new ResultEvent(true, veiculoMap);
            }
            catch (Exception ex)
            {
                return new ResultEvent(success, ex.Message);
            }

        }

    }
}
