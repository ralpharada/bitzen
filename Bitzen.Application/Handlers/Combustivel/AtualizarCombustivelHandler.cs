using Bitzen.Application.Core;
using Bitzen.Application.Queries;
using Bitzen.Core.Events;
using Bitzen.Domain.Interfaces;
using MediatR;

namespace Bitzen.Application.Handlers
{
    public class AtualizarCombustivelHandler : IRequestHandler<AtualizarCombustivelQuery, IEvent>
    {
        private readonly ICombustivelRepository _combustivelRepository;
        private readonly UsuarioAutenticado _usuarioAutenticado;
        private readonly IUsuarioRepository _usuarioRepository;
        public AtualizarCombustivelHandler(ICombustivelRepository combustivelRepository,
            UsuarioAutenticado usuarioAutenticado,
            IUsuarioRepository usuarioRepository)
        {
            _combustivelRepository = combustivelRepository;
            _usuarioAutenticado = usuarioAutenticado;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEvent> Handle(AtualizarCombustivelQuery request, CancellationToken cancellationToken)
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

                if (String.IsNullOrWhiteSpace(request.Descricao))
                    return new ResultEvent(success, "Preencha todos os campos obrigatórios.");

                if (request.Descricao.Length > 20)
                    return new ResultEvent(success, "O campo Descrição não pode conter acima de 20 caracteres.");

                if (request.Preco < 0)
                    return new ResultEvent(success, "O campo Preço não pode ser menor que zero.");

                #endregion

                var combustivel = await _combustivelRepository.ObterPorId(request.Id, cancellationToken);
                combustivel.Descricao = request.Descricao;
                combustivel.Preco = request.Preco;

                var result = await _combustivelRepository.Salvar(combustivel, cancellationToken);
                success = result > 0;

                return new ResultEvent(success, success ? "Combustível atualizado com sucesso!" : "Nenhuma atualização realizada.");
            }
            catch (Exception ex)
            {
                return new ResultEvent(success, ex.Message);
            }
        }

    }
}
