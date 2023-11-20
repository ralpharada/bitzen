using Bitzen.Application.Core;
using Bitzen.Application.Queries;
using Bitzen.Core.Events;
using Bitzen.Domain.Interfaces;
using MediatR;

namespace Bitzen.Application.Handlers
{
    public class ExcluirMotoristaHandler : IRequestHandler<ExcluirMotoristaQuery, IEvent>
    {
        private readonly IMotoristaRepository _motoristaRepository;
        private readonly UsuarioAutenticado _usuarioAutenticado;
        private readonly IUsuarioRepository _usuarioRepository;

        public ExcluirMotoristaHandler(IMotoristaRepository motoristaRepository,
            UsuarioAutenticado usuarioAutenticado,
            IUsuarioRepository usuarioRepository)
        {
            _motoristaRepository = motoristaRepository;
            _usuarioAutenticado = usuarioAutenticado;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEvent> Handle(ExcluirMotoristaQuery request, CancellationToken cancellationToken)
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

                var verificacao = await _motoristaRepository.ObterPorId(request.Id, cancellationToken);
                if (verificacao == null)
                    return new ResultEvent(success, "Motorista não localizado.");

                #endregion

                success = await _motoristaRepository.ExcluirPorId(request.Id, cancellationToken);

                return new ResultEvent(success, success ? "Motorista excluído com sucesso!" : "Falha ao excluir o motorista!");
            }
            catch (Exception ex)
            {
                return new ResultEvent(success, ex.Message);
            }

        }

    }
}
