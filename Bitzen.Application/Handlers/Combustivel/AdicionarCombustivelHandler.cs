using Bitzen.Application.Core;
using Bitzen.Application.Mapper;
using Bitzen.Application.Queries;
using Bitzen.Core.Events;
using Bitzen.Domain.Interfaces;
using Bitzen.Domain.Models;
using MediatR;

namespace Bitzen.Application.Handlers
{
    public class AdicionarCombustivelHandler : IRequestHandler<AdicionarCombustivelQuery, IEvent>
    {
        private readonly ICombustivelRepository _combustivelRepository;
        private readonly UsuarioAutenticado _usuarioAutenticado;
        private readonly IUsuarioRepository _usuarioRepository;

        public AdicionarCombustivelHandler(ICombustivelRepository combustivelRepository,
            UsuarioAutenticado usuarioAutenticado,
            IUsuarioRepository usuarioRepository)
        {
            _combustivelRepository = combustivelRepository;
            _usuarioAutenticado = usuarioAutenticado;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEvent> Handle(AdicionarCombustivelQuery request, CancellationToken cancellationToken)
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

                var verificacao = await _combustivelRepository.VerificaSeExistePorNome(request.Descricao, cancellationToken);
                if (verificacao)
                    return new ResultEvent(success, "Jà existe um combustível com essa nomenclatura");

                if (String.IsNullOrWhiteSpace(request.Descricao))
                    return new ResultEvent(success, "Preencha todos os campos obrigatórios.");

                if (request.Descricao.Length > 20)
                    return new ResultEvent(success, "O campo Descrição não pode conter acima de 20 caracteres.");

                if (request.Preco < 0)
                    return new ResultEvent(success, "O campo Preço não pode ser menor que zero.");

                #endregion

                var combustivel = CombustivelMapper<Combustivel>.Map(request);
                combustivel.DataCadastro = DateTime.Now;
                var upsertedId = await _combustivelRepository.Salvar(combustivel, cancellationToken);
                success = upsertedId > 0;
              
                return new ResultEvent(success, success ? "Combustível cadastrado com sucesso!" : "Falha ao cadastrar o combustível!", null, upsertedId);
            }
            catch (Exception ex)
            {
                return new ResultEvent(success, ex.Message);
            }

        }

    }
}
