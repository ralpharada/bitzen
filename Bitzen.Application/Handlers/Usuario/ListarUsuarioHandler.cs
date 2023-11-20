using Bitzen.Application.Core;
using Bitzen.Application.Mapper;
using Bitzen.Application.Queries;
using Bitzen.Application.Responses;
using Bitzen.Core.Events;
using Bitzen.Domain.Interfaces;
using MediatR;

namespace Bitzen.Application.Handlers
{
    public class ListarUsuarioHandler : IRequestHandler<ListarUsuarioQuery, IEvent>
    {
        private readonly IUsuarioRepository _repository;
        private readonly UsuarioAutenticado _usuarioAutenticado;

        public ListarUsuarioHandler(IUsuarioRepository repository,
            UsuarioAutenticado usuarioAutenticado)
        {
            _repository = repository;
            _usuarioAutenticado = usuarioAutenticado;
        }

        public async Task<IEvent> Handle(ListarUsuarioQuery request, CancellationToken cancellationToken)
        {
            var response = new List<UsuarioResponse>();
            try
            {
                if (_usuarioAutenticado.Email == null)
                    return new ResultEvent(false, "Acesso expirado");

                var usuarioLogado = await _repository.ObterPorEmailCadastroAtivo(_usuarioAutenticado.Email, cancellationToken);
                if (usuarioLogado == null)
                    return new ResultEvent(false, "Acesso expirado");

                var lista = await _repository.ListarTodos(cancellationToken);
                response = UsuarioMapper<List<UsuarioResponse>>.Map(lista);

                return new ResultEvent(true, response, lista.Count());
            }
            catch (Exception ex)
            {
                return new ResultEvent(false, ex.Message);
            }
        }
    }
}
