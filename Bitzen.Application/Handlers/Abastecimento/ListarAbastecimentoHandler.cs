using Bitzen.Application.Core;
using Bitzen.Application.Mapper;
using Bitzen.Application.Queries;
using Bitzen.Application.Responses;
using Bitzen.Core.Events;
using Bitzen.Domain.Interfaces;
using MediatR;

namespace Bitzen.Application.Handlers
{
    public class ListarAbastecimentoHandler : IRequestHandler<ListarAbastecimentoQuery, IEvent>
    {
        private readonly IAbastecimentoRepository _abastecimentoRepository;
        private readonly UsuarioAutenticado _usuarioAutenticado;
        private readonly IUsuarioRepository _usuarioRepository;

        public ListarAbastecimentoHandler(IAbastecimentoRepository abastecimentoRepository,
            UsuarioAutenticado usuarioAutenticado,
            IUsuarioRepository usuarioRepository)
        {
            _abastecimentoRepository = abastecimentoRepository;
            _usuarioAutenticado = usuarioAutenticado;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEvent> Handle(ListarAbastecimentoQuery request, CancellationToken cancellationToken)
        {
            var response = new List<AbastecimentoResponse>();
            try
            {
                #region Validação do usuário autenticado

                if (_usuarioAutenticado.Email == null)
                    return new ResultEvent(false, "Acesso expirado");

                var usuarioLogado = await _usuarioRepository.ObterPorEmailCadastroAtivo(_usuarioAutenticado.Email, cancellationToken);
                if (usuarioLogado == null)
                    return new ResultEvent(false, "Acesso expirado");

                #endregion

                var lista = await _abastecimentoRepository.ListarTodos(cancellationToken);
                response = AbastecimentoMapper<List<AbastecimentoResponse>>.Map(lista);

                return new ResultEvent(true, response, lista.Count());
            }
            catch (Exception ex)
            {
                return new ResultEvent(false, ex.Message);
            }
        }
    }
}
