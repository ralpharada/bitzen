using Bitzen.Application.Core;
using Bitzen.Application.Mapper;
using Bitzen.Application.Queries;
using Bitzen.Application.Responses;
using Bitzen.Core.Events;
using Bitzen.Domain.Interfaces;
using MediatR;

namespace Bitzen.Application.Handlers
{
    public class ListarVeiculoHandler : IRequestHandler<ListarVeiculoQuery, IEvent>
    {
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly UsuarioAutenticado _usuarioAutenticado;
        private readonly IUsuarioRepository _usuarioRepository;

        public ListarVeiculoHandler(IVeiculoRepository veiculoRepository,
            UsuarioAutenticado usuarioAutenticado,
            IUsuarioRepository usuarioRepository)
        {
            _veiculoRepository = veiculoRepository;
            _usuarioAutenticado = usuarioAutenticado;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEvent> Handle(ListarVeiculoQuery request, CancellationToken cancellationToken)
        {
            var response = new List<VeiculoResponse>();
            try
            {
                #region Validação do usuário autenticado

                if (_usuarioAutenticado.Email == null)
                    return new ResultEvent(false, "Acesso expirado");

                var usuarioLogado = await _usuarioRepository.ObterPorEmailCadastroAtivo(_usuarioAutenticado.Email, cancellationToken);
                if (usuarioLogado == null)
                    return new ResultEvent(false, "Acesso expirado");

                #endregion

                var lista = await _veiculoRepository.ListarTodos(cancellationToken);
                response = VeiculoMapper<List<VeiculoResponse>>.Map(lista);

                return new ResultEvent(true, response, lista.Count());
            }
            catch (Exception ex)
            {
                return new ResultEvent(false, ex.Message);
            }
        }
    }
}
