using Bitzen.Application.Core;
using Bitzen.Application.Queries;
using Bitzen.Core.Events;
using Bitzen.Domain.Interfaces;
using MediatR;

namespace Bitzen.Application.Handlers
{
    public class ExcluirVeiculoHandler : IRequestHandler<ExcluirVeiculoQuery, IEvent>
    {
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly UsuarioAutenticado _usuarioAutenticado;
        private readonly IUsuarioRepository _usuarioRepository;

        public ExcluirVeiculoHandler(IVeiculoRepository veiculoRepository,
            UsuarioAutenticado usuarioAutenticado,
            IUsuarioRepository usuarioRepository)
        {
            _veiculoRepository = veiculoRepository;
            _usuarioAutenticado = usuarioAutenticado;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEvent> Handle(ExcluirVeiculoQuery request, CancellationToken cancellationToken)
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

                #region Validação dos campos

                var verificacao = await _veiculoRepository.ObterPorId(request.Id, cancellationToken);
                if (verificacao == null)
                    return new ResultEvent(success, "Veiculo não localizado.");

                #endregion

                success = await _veiculoRepository.ExcluirPorId(request.Id, cancellationToken);

                return new ResultEvent(success, success ? "Veiculo excluído com sucesso!" : "Falha ao excluir o veiculo!");
            }
            catch (Exception ex)
            {
                return new ResultEvent(success, ex.Message);
            }

        }

    }
}
